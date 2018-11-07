using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLINQ
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("\n----------AsParallel() -----------");
            var source = Enumerable.Range(10, 20000);
            var parallelQuery = from num in source.AsParallel()
                                where num % 100 == 0 && num % 3 == 0
                                select num;


            parallelQuery.ForAll((e) => Console.WriteLine(e));

            Console.WriteLine("\n----------Timing -----------");
            var list = Enumerable.Range(10, 20000);
            var sw = new Stopwatch();

            sw.Restart();
            var result = (from l in list.AsParallel()
                          where l > 14536
                          select l).ToList();
            sw.Stop();

            Console.WriteLine($"call .AsParallel() before:  { sw.ElapsedMilliseconds} ");

            sw.Restart();
            result = (from l in list
                      where l > 14536
                      select l).AsParallel().ToList();
            sw.Stop();

            Console.WriteLine($"call .AsParallel() after: { sw.ElapsedMilliseconds} ");

            Console.WriteLine("\n----------ForAll -----------");
            var source2 = Enumerable.Range(10, 100);
            
            (from num in source2.AsParallel()
             where num % 2 == 0
             select num)
             .ForAll((n) => Console.WriteLine(n));
            Console.WriteLine("\n----------AsOrdered() -----------");
            (from num in source2.AsParallel().AsOrdered()
             where num % 2 == 0
             select num).ForAll((n) => Console.WriteLine(n));

        }
    }
}
