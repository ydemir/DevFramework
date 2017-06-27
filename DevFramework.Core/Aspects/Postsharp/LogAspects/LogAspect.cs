using DevFramework.Core.CrossCuttingConcerns.Logging.Log4Net;
using PostSharp.Aspects;
using PostSharp.Extensibility;
using System;
using System.Linq;
using System.Reflection;
using DevFramework.Core.CrossCuttingConcerns.Logging;

namespace DevFramework.Core.Aspects.Postsharp.LogAspects
{
    [Serializable]
    [MulticastAttributeUsage(MulticastTargets.Method,TargetMemberAttributes =MulticastAttributes.Instance)]
   public class LogAspect:OnMethodBoundaryAspect
    {
       private Type _loggerType;

       private LoggerService _loggerService;

        public LogAspect(Type loggerType)
        {
            _loggerType = loggerType;
        }

        //Bir instance üretmek için bu override dan yararlanıyoruz.
        public override void RuntimeInitialize(MethodBase method)
        {
            if (_loggerType.BaseType!=typeof(LoggerService))
            {
                throw new Exception("Wrong logger type");
            }
            _loggerService = (LoggerService)Activator.CreateInstance(_loggerType);
            base.RuntimeInitialize(method);
        }

        public override void OnEntry(MethodExecutionArgs args)
        {
            if (!_loggerService.IsInfoEnabled)
            {
                return;
            }

            try
            {
                var logParameters = args.Method.GetParameters().Select((t, i) => new LogParameter
                {
                    Name = t.Name,
                    Type = t.ParameterType.Name,
                    Value = args.Arguments.GetArgument(i)
                }).ToList();
                var logDetail = new LogDetail
                {
                    FullName = args.Method.DeclaringType == null ? null : args.Method.DeclaringType.Name,
                    MethodName = args.Method.Name,
                    Parameters = logParameters
                };

                _loggerService.Info(logDetail);
            }
            catch (Exception)
            {

              
            }
        }

    }
}
