namespace ElizerIoC
{
    public class Container : IContainer
    {
        private readonly Dictionary<Type, Dictionary<string, Func<object>>> _registeredTypes = new();

        public void Register<T1, T2>() where T1 : T2, new()
        {
            Register(typeof(T2), () => new T1(), string.Empty);
        }

        public void Register<T>(T instance)
        {
            if (instance == null)
                throw new ArgumentNullException(nameof(instance));
            Register(typeof(T), () => instance, string.Empty);
        }

        public void Register<T1, T2>(string name) where T1 : T2, new()
        {
            if (name == null)
                throw new ArgumentNullException(nameof(name));
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("Name cannot be empty");
            Register(typeof(T2), () => new T1(), name);
        }

        public void Register<T>(string name, T instance)
        {
            if (instance == null)
                throw new ArgumentNullException(nameof(instance));
            if (name == null)
                throw new ArgumentNullException(nameof(name));
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("Name cannot be empty");
            Register(typeof(T), () => instance, name);
        }

        private void Register(Type intf, Func<object> factory, string name)
        {
            if (!_registeredTypes.TryGetValue(intf, out var namedTypes))
            {
                namedTypes = new Dictionary<string, Func<object>>();
                _registeredTypes[intf] = namedTypes;
            }
            namedTypes[name] = factory;
        }

        public T Resolve<T>()
        {
            return ResolveInner<T>(string.Empty);
        }

        public T Resolve<T>(string name)
        {
            if (name == null)
                throw new ArgumentNullException(nameof(name));
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("Name cannot be empty");
            return ResolveInner<T>(name);
        }

        private T ResolveInner<T>(string name)
        {
            var intf = typeof(T);
            if (!_registeredTypes.TryGetValue(intf, out var namedTypes))
            {
                namedTypes = new Dictionary<string, Func<object>>();
                _registeredTypes[intf] = namedTypes;
            }
            if (namedTypes.TryGetValue(name, out var factory))
                return (T)factory();
            else
                throw new InvalidOperationException($"No registered type for {intf}");
        }
    }
}