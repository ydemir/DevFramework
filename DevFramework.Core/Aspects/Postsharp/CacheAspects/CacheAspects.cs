using DevFramework.Core.CrossCuttingConcerns.Caching;
using PostSharp.Aspects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace DevFramework.Core.Aspects.Postsharp.CacheAspects
{
    [Serializable]
   public class CacheAspects:MethodInterceptionAspect
    {
        Type _cacheType;
        private int _cacheByMunite;
        private ICacheManager _cacheManager;

        public CacheAspects(Type cacheType, int cacheByMunite=60)
        {
            _cacheType = cacheType;
            _cacheByMunite = cacheByMunite;
        }

        //hangi cachemanager methodu gelirse onu çağırmak için bu methodu override ediyoruz.
        public override void RuntimeInitialize(MethodBase method)
        {
            //gönderilen type bir cache manager türünde değilse
            if (typeof(ICacheManager).IsAssignableFrom(_cacheType)==false)
            {
                throw new Exception("Wrong cache manager");
            }
            _cacheManager = (ICacheManager)Activator.CreateInstance(_cacheType);
            base.RuntimeInitialize(method);
        }
        //Methoda girmeden çalıştırılacak 
        public override void OnInvoke(MethodInterceptionArgs args)
        {
            var methodName = string.Format("{0}.{1}.{2}",
                args.Method.ReflectedType.Namespace,
                args.Method.Name);
            var arguments = args.Arguments.ToList();
            var key = string.Format("{0}({1})", methodName,
                string.Join(",", arguments.Select(x => x != null ? x.ToString() : "<Null>")));

            if (_cacheManager.isAdd(key))
            {
                args.ReturnValue = _cacheManager.Get<object>(key);
            }
            base.OnInvoke(args);
            _cacheManager.Add(key, args.ReturnValue, _cacheByMunite);
        }
    }
}
