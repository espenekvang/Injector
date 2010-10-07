using System;

namespace Injector
{
    public class InstanceFactory
    {
        public static TInterface Make<TInterface, TInterfaceImpl>() where TInterfaceImpl : class, TInterface, new ()
        {
            return new TInterfaceImpl();
        }
    }
}
