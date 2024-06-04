using System;
using UnityEditor;


    public static class Utilities
    {
        public static T Do<T>(this T self, Action<T> apply)
        {
            apply.Invoke(self);
            return self;
        }

    }
