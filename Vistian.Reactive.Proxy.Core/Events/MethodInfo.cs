using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Vistian.Reactive.Proxy.Utils;

namespace Vistian.Reactive.Proxy.Events
{
    public class MethodInfo: IMethodInfo
    {
        public string Namespace { get; private set; }
        public string DeclaringType { get; private set; }
        public string Name { get; private set; }
        public string Signature { get; private set; }

        public MethodInfo(MethodBase method)
        {
            Namespace = method.Name;
            DeclaringType = (method.DeclaringType).Name;
            Name = GetName(method);
            Signature = Name + " (" + GetArguments(method) + ")";
        }

        string GetName(MethodBase method)
        {
            if (method.IsGenericMethod)
            {
                var genericArgs = method.GetGenericArguments();
                return method.Name + "<" + string.Join((string) ", ", (IEnumerable<string>) genericArgs.Select(TypeUtils.ToFriendlyName)) + ">";
            }

            return method.Name;
        }

        string GetArguments(MethodBase method)
        {
            var arguments = new List<string>();

            foreach (var arg in method.GetParameters())
            {
                arguments.Add(GetArgument(arg));
            }

            return string.Join(", ", arguments);
        }

        string GetArgument(ParameterInfo arg)
        {
            if (arg.ParameterType.IsGenericParameter)
            {
            }

            return TypeUtils.ToFriendlyName(arg.ParameterType) + " " + arg.Name;
        }
    }
}
