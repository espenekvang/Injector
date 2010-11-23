using System;
using Injector.Collection;

namespace Injector
{
    public class InstanceFactory
    {
        private static SynchronizedDictionary _replacedInstances = new SynchronizedDictionary();
 
        public static TInterface Make<TInterface, TInterfaceImpl>() where TInterfaceImpl : class, TInterface, new ()
        {            
            return Make<TInterface>(() => new TInterfaceImpl());
        }

        public static TInterface Make<TInterface>(Func<TInterface> constructor)
        {
            var key = typeof(TInterface).ToString();
            object replacedInstanceCreator = _replacedInstances.Get(key);

            if (replacedInstanceCreator != null)
            {
                return ((Func<TInterface>)replacedInstanceCreator).Invoke();
            }

            return constructor.Invoke();
        }

        public static void ReplaceDefault<TInterface>(Func<TInterface> replacingInstance)
        {
            var key = typeof(TInterface).ToString();
            _replacedInstances.Add(key, replacingInstance);
        }

        public static void Reset()
        {
            _replacedInstances.RemoveAll();
        }
    }
}
