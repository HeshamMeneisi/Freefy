using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Freefy
{
    class Cache
    {
        static Dictionary<object, object> cache = new Dictionary<object, object>();

        public static void Stash(object key, object obj)
        {
            cache[GetKey(key, obj)] = obj;
        }

        private static string GetKey(object key, object obj)
        {
            return obj.GetType().ToString() + "_" + key;
        }

        public static void Remove<T>(string key)
        {
            string innerKey = GetKey(key, typeof(T));
            cache.Remove(innerKey);
        }

        private static string GetKey(object key, Type t)
        {
            return t.ToString() + "_" + key;
        }

        public static bool Lookup<T>(object key, out T obj)
        {
            string innerk = GetKey(key, typeof(T));
            bool hit = cache.Keys.Contains(innerk);
            if (hit)
                obj = (T)cache[innerk];
            else
                obj = default(T);
            return hit;
        }

        public static bool TryUpdate<T>(object key, ref T obj)
        {
            string innerk = GetKey(key, typeof(T));
            bool hit = cache.Keys.Contains(innerk);
            if (hit)
                obj = (T)cache[innerk];
            return hit;
        }

        public static void Clear()
        {
            cache.Clear();
        }
    }
}
