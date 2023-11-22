using MethodBoundaryAspect.Fody.Attributes;
using MonefyWeb.Transversal.Utils;

namespace MonefyWeb.Transversal.Aspects
{
    [Serializable]
    public class LogAttribute : OnMethodBoundaryAspect
    {
        private readonly static ILogger _logger = new Logger();

        public override void OnEntry(MethodExecutionArgs args)
        {
            string className = args.Method.DeclaringType?.Name ?? "UnknownClass";
            string methodName = args.Method.Name;
            string parameters = string.Join(", ", args.Arguments);

            _logger.Info(string.Format(Properties.Resources.TimerAspectMessage, className, methodName, parameters));
        }

        public override void OnExit(MethodExecutionArgs args)
        {
            string className = args.Method.DeclaringType?.Name ?? "UnknownClass";
            string methodName = args.Method.Name;

            _logger.Info(string.Format(Properties.Resources.TimerAspectMessage, className, methodName));
        }
    }
}
