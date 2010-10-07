using System;
using Injector.Collection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Injector.UnitTest
{
    [TestClass]
    public class SynchronizedDictionaryTest
    {
        private SynchronizedDictionaryTable<FakeClass> _collection;

        [TestInitialize]
        public void InitializeTest()
        {
            _collection = new SynchronizedDictionaryTable<FakeClass>();
        }

        [TestMethod]
        public void Get_NoObjectsAdded_ReturnValueIsNull()
        {
            //act
            var instance = _collection.Get("MyKey");

            //assert
            Assert.IsNull(instance);
        }

        [TestMethod]
        public void Get_ObjectWithKeyMyKeyAdded_ReturnValueIsObject()
        {
            //arrange
            var myObject = new FakeClass();
            _collection.Add("MyKey", myObject);

            //act
            var returnValue = _collection.Get("MyKey");

            //assert
            Assert.AreEqual(myObject, returnValue);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Add_ObjectWithKeyAlreadyAdded_ExceptionThrown()
        {
            //arrange
            var myObject = new FakeClass();
            _collection.Add("MyKey", myObject);

            //act
            _collection.Add("MyKey", new FakeClass());
        }
        
        [TestMethod]
        public void Add_ObjectWithKeyExistsButOverrideExistingValueIsTrue_ExceptionNotThrown()
        {
            //arrange
            var myObject = new FakeClass();
            _collection.Add("MyKey", myObject, true);

            //act
            _collection.Add("MyKey", new FakeClass(), true);
        }

        [TestMethod]
        public void Get_ObjectWithKeyExistsButOverrideExistingValueIsTrue_NewestObjectReturned()
        {
            //arrange
            var myObject = new FakeClass();
            var newObject = new FakeClass();
            _collection.Add("MyKey", myObject, true);
            _collection.Add("MyKey", newObject, true);

            //act
            var returnValue = _collection.Get("MyKey");

            //assert
            Assert.AreEqual(newObject, returnValue);
        }

        private class FakeClass
        {
            
        }
    }
}
