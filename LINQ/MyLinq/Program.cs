using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLinq
{

    public static class Someclass{
        static public IEnumerable<string> FindMy(this IEnumerable<string> values, Func<string, bool> test)
        {
            var resut = new List<string>();
            foreach (var str in values)
            {
                Console.WriteLine("I was here {0}", str);
                if (test(str))
                {
                    resut.Add(str);
                }

            }
            return resut;
        }

        static public IEnumerable<string> FindL(this IEnumerable<string> values, Func<string, bool> test)
        {

            foreach (var str in values)
            {
                Console.WriteLine("I was here {0}", str);
                if (test(str))
                {
                    yield return str;
                }
            }
        }
    }


class Program
    {
        static void Main(string[] args)
        {

            string[] names = new string[] { "Ольга", "Станислав", "Ольга", "Сева", "Ольга" };
            // Новый опретор
            var rez = names.FindMy(n => n.StartsWith("О")).Take(1);
            foreach(var s in rez)
            Console.WriteLine(s);
            var rez2 = names.FindL(n => n.StartsWith("О")).Take(1);
            foreach (var s in rez2)
                Console.WriteLine(s);
        }
    }
}
