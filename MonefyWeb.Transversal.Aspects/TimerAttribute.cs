using log4net;
using MethodBoundaryAspect.Fody.Attributes;

namespace MonefyWeb.Transversal.Aspects
{

    [Serializable]
    public class TimerAttribute : OnMethodBoundaryAspect
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(TimerAttribute));

        private DateTime startTime;

        public override void OnEntry(MethodExecutionArgs args)
        {
            startTime = DateTime.Now;
        }

        public override void OnExit(MethodExecutionArgs args)
        {
            string className = args.Method.DeclaringType?.Name ?? "UnknownClass";
            string methodName = args.Method.Name;

            TimeSpan executionTime = DateTime.Now - startTime;

            Logger.Info(string.Format(Properties.Resources.TimerAspectMessage, className, methodName, executionTime.TotalMilliseconds));
        }
    }
}
