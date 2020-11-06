using System;
using System.Collections.Generic;
using System.Text;

namespace KnowMath.Helpers
{
    public class KernelHelper
    {
        public static List<string> Level = new List<string>
        {
            "Beginer",
            "Intermidiate",
            "Advanced"
        };

        public static List<KeyValuePair<string, string>> Arithmetic = new List<KeyValuePair<string, string>>
        {
            {new KeyValuePair<string, string>("Addition","+") },
            {new KeyValuePair<string, string>("Multplication","*") },
            {new KeyValuePair<string, string>("Subtraction","-") },
            {new KeyValuePair<string, string>("Divistion","/") },
        };

        public static string GetOperator(string arithmetic)
        {
            if (Arithmetic.Find(x=>x.Key == arithmetic).Value != null)
            {
                return Arithmetic.Find(x => x.Key == arithmetic).Value;
            }

            return default;
        }
    }
}
