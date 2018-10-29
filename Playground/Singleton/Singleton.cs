using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Playground
{
    public class StandardSingleton : ICloneable
    {
        private static StandardSingleton instance;

        private StandardSingleton() { }

        public static StandardSingleton GetInstance()
        {
            if (instance == null)
                instance = new StandardSingleton();
            return instance;
        }

        // Prevents cloning of the singleton
        public object Clone()
        {
            throw new InvalidOperationException("Cloning a singleton object is not allowed.");
        }
    }

    // Thread safe
    public sealed class DoubleCheckLockingSingleton : ICloneable
    {
        private static DoubleCheckLockingSingleton instance = null;
        private static readonly object InstanceLock = new object();

        private DoubleCheckLockingSingleton() { }

        public static DoubleCheckLockingSingleton GetInstance
        {
            get
            {
                if (instance == null)
                {
                    lock (InstanceLock)
                    {
                        if (instance == null)
                        {
                            instance = new DoubleCheckLockingSingleton();
                        }
                    }
                }
                return instance;
            }
        }

        // Prevents cloning of the singleton
        public object Clone()
        {
            throw new InvalidOperationException("Cloning a singleton object is not allowed.");
        }
    }

    // Thread safe
    public sealed class EarlySingleton : ICloneable
    {
        // Create instance eagerly
        private static EarlySingleton instance = new EarlySingleton();

        private EarlySingleton() { }

        public static EarlySingleton GetInstance()
        {
            return instance; // Just return the instance
        }

        // Prevents cloning of the singleton
        public object Clone()
        {
            throw new InvalidOperationException("Cloning a singleton object is not allowed.");
        }
    }
}