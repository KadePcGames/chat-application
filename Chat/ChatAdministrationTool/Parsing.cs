using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatAdministrationTool
{
    class Parsing
    {
        public static string[] RemoveNulls(string[] data)
        {
            List<string> temp = data.ToList();
            temp.RemoveAt(temp.Count - 1);

            return temp.ToArray();
        }

        public static string[] RemoveTilIndex(string[] data, int minIndex)
        {
            List<string> temp = data.ToList();

            for (int i = 0; i < data.Length; i++)
            {
                if (i == minIndex)
                    break;

                temp.RemoveAt(i);
            }

            return temp.ToArray();
        }

        public static string ArrayToString(string[] arr, string joinString = "\\")
        {
            StringBuilder builder = new StringBuilder();

            foreach (var index in arr)
                builder.Append(index + joinString);

            return builder.ToString().TrimEnd();
        }
    }
}
