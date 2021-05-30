using System;
using System.Threading.Tasks;
using AutoRest.Client.Processing;
using AutoRest.Client.Tests.Fixtures;
using AutoRest.Client.Tests.HttpClients.HttpBin;
using Moq;
using NUnit.Framework;

namespace AutoRest.Client.Tests
{
    public class MiddlewareTests
    {
        [SetUp]
        public void Setup()
        {
            SyncCommonMiddlewareFixture.Called = false;
            AsyncCommonMiddlewareFixture.Called = false;
        }
        
        [Test]
        public async Task Should_Call_Middlewares_From_Assembly()
        {
            var client = HttpClientFixture.GetHttpBinClient(configuration =>
            {
                configuration.AddMiddlewares(new []
                {
                    GetType().Assembly
                });
            });

            var result = await client.GetHeaderAsync();
            
            Assert.True(SyncCommonMiddlewareFixture.Called);
            Assert.True(AsyncCommonMiddlewareFixture.Called);
            Assert.False(FakeClientMiddleware.Called);
        }

        [Test]
        public async Task Should_Check_Middlewares_Calling()
        {
            var commonMiddleware = MakeMiddlewareMock<RestCallMiddleware>();
            var typedMiddleware = MakeMiddlewareMock<RestCallMiddleware<IHttpBinAnythingClient>>();

            var client = HttpClientFixture.GetHttpBinClient(configuration =>
            {
                configuration.AddMiddleware(commonMiddleware.Object);
                configuration.AddMiddleware(typedMiddleware.Object);
                configuration.AddMiddleware(typeof(AsyncCommonMiddlewareFixture));
            });

            var result = await client.GetHeaderAsync();

            commonMiddleware.Verify(x => x.Invoke(It.IsAny<ExecutionContext>(),
                It.IsAny<Action<ExecutionContext>>()), Times.Once);
            
            typedMiddleware.Verify(x => x.Invoke(It.IsAny<ExecutionContext>(),
                It.IsAny<Action<ExecutionContext>>()), Times.Once);

            Assert.True(AsyncCommonMiddlewareFixture.Called);
        }

        private static Mock<TMiddleware> MakeMiddlewareMock<TMiddleware>()
            where TMiddleware : RestCallMiddleware
        {
            var mock = new Mock<TMiddleware>();
            
            mock.Setup(x => x.Invoke(It.IsAny<ExecutionContext>(), It.IsAny<Action<ExecutionContext>>()))
                .Callback<ExecutionContext,Action<ExecutionContext>>((context, next) =>
                {
                    next(context);
                });

            return mock;
        }
    }
}