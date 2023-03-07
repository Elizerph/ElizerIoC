namespace ElizerIoC
{
    public interface IContainer
    {
        void Register<T1, T2>() where T1 : T2, new();
        void Register<T>(T instance);
        void Register<T1, T2>(string name) where T1 : T2, new();
        void Register<T>(string name, T instance);
        T Resolve<T>();
        T Resolve<T>(string name);
    }
}
