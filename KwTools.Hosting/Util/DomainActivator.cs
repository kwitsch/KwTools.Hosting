using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace KwTools.Hosting.Util
{
    class DomainActivator
    {
        private static readonly Lazy<string> _CurrentAssemblyName = new Lazy<string>(Assembly.GetAssembly(typeof(DomainActivator)).FullName);
        private readonly Lazy<List<Assembly>> _AssemblyCache = new Lazy<List<Assembly>>(()=> 
        {
            var res = new List<Assembly>();
            foreach (var a in AppDomain.CurrentDomain.GetAssemblies())
            {
                if (AddCheck(a.FullName, res))
                {
                    AddToAssemblyCache(a, res);
                }
            }
            return res;
        });
        private readonly Dictionary<Type, object> _ObjectCache = new Dictionary<Type, object>();

        public DomainActivator()
        {
            
        }
        public IEnumerable<T> GetInstances<T>()
        {
            Debug.WriteLine($"GetInstances<{typeof(T).Name}>:");
            foreach (var t in GetImplementingTypes<T>())
            {
                Debug.WriteLine($"- {t.FullName}");
                if(_ObjectCache.Keys.Contains(t))
                {
                    yield return (T)_ObjectCache[t];
                }
                else
                {
                    var res = Activator.CreateInstance(t);
                    _ObjectCache.Add(t, res);
                    yield return (T)res;
                }
            }
        }


        public IEnumerable<Type> GetImplementingTypes<T>()
        {
            foreach (var a in _AssemblyCache.Value)
            {
                foreach (var t in a.GetTypes()
                                   .Where(t => t.GetInterfaces().Contains(typeof(T))
                                           && !t.IsInterface
                                           && !t.IsAbstract))
                {
                    yield return t;
                }
            }
        }

        #region internals
        private static bool AddCheck(string name, List<Assembly> cacheBuild)
        {
            var parts = name.Split(',');
            if (parts[0].StartsWith("System.")
             || parts[0].StartsWith("Microsoft.")
             || parts[0].Equals("testhost")
             || parts[0].Equals("netstandard")
             || name.Equals(_CurrentAssemblyName.Value)
             || cacheBuild.Any(l=>l.FullName==name))
            {
                return false;
            }
            return true;
        }

        private static void AddToAssemblyCache(Assembly assembly, List<Assembly> cacheBuild)
        {
            cacheBuild.Add(assembly);
            foreach (var a in assembly.GetReferencedAssemblies())
            {
                if (AddCheck(a.FullName, cacheBuild))
                {
                    var asm = Assembly.Load(a);
                    AddToAssemblyCache(asm, cacheBuild);
                }
            }

        }
        #endregion
    }
}
