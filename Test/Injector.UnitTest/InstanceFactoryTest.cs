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

        [TestMethod]
        public void Make_MakeCalledTwiceOnSameInterface_TwoDifferentInstancesReturned()
        {
            var createdInstance1 = InstanceFactory.Make<IFakeClass, FakeClass>();
            var createdInstance2 = InstanceFactory.Make<IFakeClass, FakeClass>();

            Assert.AreNotEqual(createdInstance1, createdInstance2);
        }

        [TestMethod]
        public void Make_ClassHasNonEmptyConstructor_InstanceCreatedWithConstructor()
        {
            var createdInstance = InstanceFactory.Make<IFakeClass>(()=> new FakeClassWithNoEmptyConstructor("someArgument"));

            Assert.AreEqual(typeof(FakeClassWithNoEmptyConstructor), createdInstance.GetType());
        }

        [TestMethod]
        public void Make_InstanceHasBeenReplaced_CreatedInstanceIsOfReplacedType()
        {
            InstanceFactory.ReplaceDefault<IFakeClass>(() => new ReplacingClass());

            var createdInstance = InstanceFactory.Make<IFakeClass, FakeClass>();

            Assert.AreEqual(typeof(ReplacingClass), createdInstance.GetType());
        }

        [TestMethod]
        public void Make_InstanceReplacedButResetCalled_CreatedInstanceIsOfDefaultType()
        {
            InstanceFactory.ReplaceDefault<IFakeClass>(() => new ReplacingClass());
            InstanceFactory.Reset();

            var createdInstance = InstanceFactory.Make<IFakeClass, FakeClass>();

            Assert.AreEqual(typeof(FakeClass), createdInstance.GetType());
        }

        private interface IFakeClass
        {
            
        }
        
        private class FakeClass : IFakeClass        
        {
            
        }

        private class FakeClassWithNoEmptyConstructor : IFakeClass
        {
            public FakeClassWithNoEmptyConstructor(string someArgument) { }
        }

        private class ReplacingClass : IFakeClass
        {
        }
    }
}
