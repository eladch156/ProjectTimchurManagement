namespace WebApplication1.Database
{
    /// <summary>
    /// Wrapper for the Cache object to confirm singleton use.
    /// </summary>
    public class SingletonCache
    {
        private static Cache _instance;
        // Empty constructor, protected.
        protected SingletonCache()
        {

        }
        /// <summary>
        /// Gets the singleton stance via lazy initialization.
        /// </summary>
        /// <returns>Instance of the singleton Cache.</returns>
        public static Cache Instance()
        {
            // Note: this is not thread safe.
            if (_instance == null)
            {
                _instance = new Cache();
            }
            return _instance;
        }
    }
}
