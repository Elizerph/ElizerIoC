namespace ElizerIoC.Test
{
    internal class FlexTransformer : IDataTransformer
    {
        public Func<int, int> Func { get; set; } = e => e;

        public int Transform(int source)
        {
            return Func(source);
        }
    }
}
