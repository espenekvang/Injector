using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Injector.UnitTest
{
    [TestClass]
    public class InstanceFactoryTest
    {
        [TestMethod]
        public void Make_FakeClassIsSpecified_ReturnedInstanceIsOfFakeClassType()
        {
            var createdInstance = InstanceFactory.Make<IFakeClass, FakeClass>();

            Assert.AreEqual(typeof(FakeClass), createdInstance.GetType());

        }

        private interface IFakeClass
        {
            
        }
        private class FakeClass : IFakeClass
        {
            
        }
    }
}
