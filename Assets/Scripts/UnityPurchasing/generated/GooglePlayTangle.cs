// WARNING: Do not modify! Generated file.

namespace UnityEngine.Purchasing.Security {
    public class GooglePlayTangle
    {
        private static byte[] data = System.Convert.FromBase64String("pdmn3Q9gszzzIDe8GLRVe6lJs5pV7yJfV0JFP2WGig9UxxFEzV3XGLW27qNtB4rY0zC+512PMp99mnv50L4jGqz9wLH9nPhoO9T5ofiu6cK2FpwlIyfVBj9MEsWHd/4N6bIOrXvuK2lHeXKhnm9th5rmXj2tLGsBmJEdyeU0gSQ/w0mz8RlEohc3x6xu7ePs3G7t5u5u7e3sQLhtsCIALq7LryAYJjmAjn4+ajLinFgiwSergIM9pgcuWNO3znU8j/IdxfmAraGsv/WCWXRVtpSQP+1BQJpsld8a5u/TCvvPVTMwNWE/VA3n+Vj3duSJ3G7tztzh6uXGaqRqG+Ht7e3p7O/Jf0S4XVO9VIHHxdO9dkHC6xaB1X/4Ks7hTysAV+7v7ezt");
        private static int[] order = new int[] { 8,13,2,9,7,6,6,13,9,13,11,12,13,13,14 };
        private static int key = 236;

        public static readonly bool IsPopulated = true;

        public static byte[] Data() {
        	if (IsPopulated == false)
        		return null;
            return Obfuscator.DeObfuscate(data, order, key);
        }
    }
}
