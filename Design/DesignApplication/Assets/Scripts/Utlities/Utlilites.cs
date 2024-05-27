using System;
using UnityEditor;


    public static class Utilities
    {
        public static T Do<T>(this T self, Action<T> apply)
        {
            apply.Invoke(self);
            return self;
        }

       public static string GetItemName(EItemType eItemType){
        return eItemType switch  
        {
            EItemType.Computer => "Computer",
            EItemType.RecycleBin => "RecycleBin",
            EItemType.TV => "TV",
            EItemType.VR => "VR",
            _ => "",
        };
       }
    }
