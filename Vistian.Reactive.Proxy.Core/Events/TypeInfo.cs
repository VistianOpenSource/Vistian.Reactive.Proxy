using System;
using System.Reflection;


namespace Vistian.Reactive.Proxy.Events
{
    public class TypeInfo: ITypeInfo
    {
        public string Name { get; private set; }
        public string Namespace { get; private set; }

        public TypeInfo(Type type)
        {
            Name = (type).Name;
            Namespace = type.Namespace;
        }
    }
}
