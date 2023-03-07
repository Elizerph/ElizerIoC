namespace ElizerIoC.Test
{
    [TestClass]
    public class ContainerTest
    {
        [TestMethod]
        public void ResolveRegisteredType()
        {
            var container = new Container();
            container.Register<FlexTransformer, IDataTransformer>();

            var t = container.Resolve<IDataTransformer>();

            Assert.AreEqual(5, t.Transform(5));
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void ResolveUnregistered()
        {
            var container = new Container();

            var t = container.Resolve<IDataTransformer>();

            Assert.AreEqual(5, t.Transform(5));
        }

        [TestMethod]
        public void ResolveRegisteredInstance()
        {
            var container = new Container();
            var instance = new FlexTransformer()
            {
                Func = e => e - 1
            };
            container.Register<IDataTransformer>(instance);

            var t = container.Resolve<IDataTransformer>();

            Assert.AreEqual(4, t.Transform(5));
        }

        [TestMethod]
        public void ResolveRegisteredNamedType()
        {
            var container = new Container();

            container.Register<FlexTransformer, IDataTransformer>("a");

            var t = container.Resolve<IDataTransformer>("a");

            Assert.AreEqual(5, t.Transform(5));
        }
    }
}