using Moq;
using Newtonsoft.Json.Linq;
using ServerApp;
using System;
using Xunit;

namespace XUnitTest
{
    public class ObservableTests
    {
        private MockRepository mockRepository;

        public class MapObserver : ServerApp.IObserver<JObject>
        {


            public MapObserver()
            {
               
            }
            public void Update(JObject data)
            {
                if (data == null)
                {

                }
            }
        }
        public ObservableTests()
        {
            this.mockRepository = new MockRepository(MockBehavior.Strict);


        }

        private Observable<JObject> CreateObservable()
        {
            return new Observable<JObject>();
        }

        [Fact]
        public void Register_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var observable = this.CreateObservable();
            MapObserver observer = new MapObserver();

            // Act
            

            // Assert
            try
            {
                observable.Register(observer);
                Assert.True(true);
            }
            catch 
            {

                Assert.True(false);
            }
            
            this.mockRepository.VerifyAll();
        }

        [Fact]
        public void Unregister_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var observable = this.CreateObservable();
            MapObserver observer = new MapObserver();


            // Assert
            try
            {
                observable.Unregister(
                observer);
                Assert.True(true);
            }
            catch
            {

                Assert.True(false);
            }
            this.mockRepository.VerifyAll();
        }

        [Fact]
        public void Notify_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var observable = this.CreateObservable();

            
            try
            {
                observable.Notify();
                Assert.True(true);
            }
            catch
            {

                Assert.True(false);
            }
            // Assert
            this.mockRepository.VerifyAll();
        }
    }
}
