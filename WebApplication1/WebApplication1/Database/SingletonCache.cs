namespace WebApplication1.Database
{
    /// <summary>
    /// Wrapper for the Cache object to confirm singleton use.
    /// </summary>
    public class SingletonCache
    {
        private static Cache _instance;
        // Constructor is 'protected'
        protected SingletonCache() 
        {

        }

        public static Cache Instance()
        {

            // Uses lazy initialization.

            // Note: this is not thread safe.
            if (_instance == null)
            {
                _instance = new Cache();
            }
            return _instance;
        }
    }
}
