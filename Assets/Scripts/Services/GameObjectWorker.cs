namespace Asteroids.Services
{
    internal static class GameObjectWorker
    {
        public static bool TryGetAbstract<T>(this object obj, out T typeObject)
        {
            if(obj is T)
            {
                typeObject = (T) obj;
                return true;
            }
            else
            {
                typeObject = default;
                return false;
            }
        }
    }
}