using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LINQ
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] names = { "Анна", "Станислав", "Ольга", "Сева", "Ольга", "Никита" };
            var students = new[] {
                    new { studentID = 1, FirstName = "Anna", Country = "Belarus",  Spec = "Poit" },
                    new { studentID = 2, FirstName = "Melena", Country  = "Bulgaria",  Spec = "Poit" },
                    new { studentID = 3, FirstName = "Lena", Country  = "Germany", Spec = "Isit" }
            };

            //синтаксис
            // Использование точечной нотации
            IEnumerable<string> rezult1 = names.Where(n => n.Length < 6)
                                               .Select(n => n);

            // Использование синтаксиса выражения запроса
            IEnumerable<string> rezult2 = from n in names
                                          where n.Length < 6
                                          select n;

            Console.WriteLine("-----------Where-----------");
            IEnumerable<string> whereNames =  names.Where(p => p.StartsWith("С"));
            foreach (string c in whereNames)
                Console.WriteLine(c + " ");


            Console.WriteLine("-----------Select-----------");
            IEnumerable<int> nameLen = names.Select(p => p.Length);
            foreach (int c in nameLen)
                Console.Write(c + " ");
            IEnumerable<int> nameLen2 = from p in names
                                        select p.Length;
            var nameLen3 = names.Select(p => new { p, p.Length });

            Console.WriteLine("\n-----------Where and Select -----------");
            IEnumerable<string> aNames = names.Where(n => String.Equals(n, "Ольга"))
                                              .Select(n => n);
            foreach (string name in aNames)
                           Console.Write(name + " " );
            IEnumerable<string> aNames3 =    from n in names
                                             where String.Equals(n, "Ольга")
                                             select n;
            Console.WriteLine("\n-----------Where and Select -----------");
            IEnumerable<string> aStud =  students.Where(s => s.Country.StartsWith("B"))
                                                 .Where(c => c.Spec.Equals("Poit"))
                                                 .Select(n => n.FirstName);
            foreach (string name in aStud)
                Console.Write(name + " ");

            Console.WriteLine("\n-----------Отложенность -----------");
            IEnumerable<int> nameL2 = from p in names
                                        select p.Length;
            names[2] = "Де";
            foreach (int name in nameL2)
                 Console.Write(name + " ");

            Console.WriteLine("\n-----------SelectMany -----------");
            IEnumerable<char> letters =   names.SelectMany(p => p.ToArray());
            foreach (char c in letters)
                Console.Write(c + " ");

            Console.WriteLine("\n-----------Take -----------");
            IEnumerable<string> group = names.Take(2);
            foreach (string c in group)
                Console.Write(c + " ");

            Console.WriteLine("\n-----------TakeWhilep -----------");
            IEnumerable<string> shortNames = names.TakeWhile(p => p.Length < 4);
            foreach (string c in shortNames)
                Console.Write(c + " ");

            Console.WriteLine("\n-----------Skip -----------");
            IEnumerable<string> skipnames = names.Skip(2);
            foreach (string c in skipnames)
                Console.Write(c + " ");

         
            Console.WriteLine("\n-----------SkipWhile -----------"); 
            IEnumerable<string> skipWnames = names.SkipWhile(s => s.StartsWith("A"));
            foreach (string c in skipWnames)
                Console.WriteLine(c + " ");

            Console.WriteLine("\n-----------Concat-----------");
            IEnumerable<string> connames = names.Take(1)
                                                .Concat(names.Skip(3));
            foreach (string c in connames)
                Console.Write(c + " ");

            Console.WriteLine("\n-----------OrderBy-----------");
            IEnumerable<string> aSpecStud =  students.OrderBy(s => s.Spec)
                                                     .OrderBy(s => s.FirstName)
                                                     .Select(n => n.Spec + " " + n.FirstName);
            IEnumerable<string> aSpecStud2 =    from s in students
                                                 orderby s.Spec
                                                 orderby s.FirstName
                                                 select s.Spec + " " + s.FirstName;

            foreach (string c in aSpecStud)
                Console.WriteLine(c + " ");


            IEnumerable<string> namesOT = names.OrderBy(s => s.Length).
                                                ThenBy(s => s);
            IEnumerable<string> namesOTh = names.OrderBy(s => s.Length).
                                                 ThenByDescending(s => s);
            foreach (string c in namesOT)
                Console.WriteLine(c + " ");

            Console.WriteLine("\n-----------Join-----------");

            int[] key = { 1, 4, 5, 7 };

            var sometype = names
             .Join(
              key,           //  внутренняя
              w => w.Length,  //  внешний ключ выбора
              q => q,         // внутренний ключ выбора
              (w, q) => new   // результат
                  {
                  id = w,
                  name = string.Format("{0} ", q),
              });

            foreach (var item in sometype)
                Console.WriteLine(item);

            Console.WriteLine("\n-----------GroupBy-----------");
            IEnumerable<IGrouping<int, string>> outerSequence =
               names.GroupBy(o => o.Length);

            foreach (var item in outerSequence)
            {
                Console.WriteLine(item.Key);
                foreach (var element in item)
                    Console.WriteLine(element);
            }

            var GroupedBySpec = students.GroupBy(s => s.Spec);

            foreach (var name in GroupedBySpec)
            {
                Console.WriteLine(name.Key + " " + name.Count());
                foreach (var m in name)
                {
                    Console.WriteLine(m.FirstName);
                }
            }
            Console.WriteLine("\n-----------Distinct-----------");
            IEnumerable<string> nums = names.Distinct();

            foreach (var item in nums)
                Console.WriteLine(item);

            Console.WriteLine("\n-----------Union-----------");
            IEnumerable<string> names9 = names.Take(1);
            IEnumerable<string> names10 = names.Skip(3);
            IEnumerable<string> union =  names9.Union<string>(names10);
            foreach (var item in union)
                Console.WriteLine(item);

            Console.WriteLine("\n-----------Intersect -----------");
            IEnumerable<string> names12 = names.Take(2);
            IEnumerable<string> names13 = names.Skip(1);

            IEnumerable<string> inter = names12.Intersect<string>(names13);
            foreach (var item in inter)
                Console.WriteLine(item);

            Console.WriteLine("\n-----------Cast -----------");
            var seq = names.Cast<int>();
            Console.WriteLine("Тип данных seq: " + seq.GetType());
            

            Console.WriteLine("\n----------OfType -----------");
            ArrayList ala = new ArrayList();
            ala.Add(new SByte());
            ala.Add(new Decimal(23));
            ala.Add(new String('0', 8));
            
            var oftypeala = ala.OfType<Decimal>();
            foreach (var item in oftypeala)
                Console.WriteLine(item);


            Console.WriteLine("\n----------Lookup -----------");
            ILookup<int, string> lookup = names.ToLookup(y => y.Length);

            foreach (var u in lookup)
            {
                Console.WriteLine(u.Key);
                foreach (var y in u)
                    Console.WriteLine(y);
            }

            IEnumerable<string> actors = lookup[4];
            foreach (var u in actors)
                Console.WriteLine(u);
            Console.WriteLine("----------------------------------");


            Console.WriteLine("\n----------Any-----------");
            bool rex = names.Any(s => s.StartsWith("О"));
            Console.WriteLine(rex);

            Console.WriteLine("\n----------Range -----------");
            long ccount = Enumerable.Range(8, 98)
                                    .Concat(Enumerable.Range(1, 23))
                                    .LongCount(s => s > 67);
                     
            Console.WriteLine(ccount);

            Console.WriteLine("\n----------Repeat -----------");
            IEnumerable<int> nqq = Enumerable.Repeat(10, 5);
            foreach (var y in nqq)
                Console.WriteLine(y);
            Console.WriteLine("\n----------ToList -----------");
            List<string> auto = names.ToList();
            foreach (var y in auto)
                Console.WriteLine(y);
            Console.WriteLine("\n----------ToDictionary -----------");
            string[] rnames = { "Анна", "Станислав", "Ольга" };
            Dictionary<int, string> eDictionary =  rnames.ToDictionary(k => k.Length);

            foreach (var i in eDictionary)
                Console.Write(i.Key + "  " + i.Value);

            Console.WriteLine("\n----------Single -----------");
            string sst = names.Where(s => s.Length == 5).Single();
            Console.Write(sst);

            Console.WriteLine("\n----------All -----------");
            var allnames = names.All(s => s.Length > 5);
              Console.WriteLine(allnames);
            Console.WriteLine("\n----------Max -----------");
            IEnumerable<string> aaStud = students.Where(s => s.Country.StartsWith("B"))
                                                .Where(c => c.Spec.Equals("Poit"))
                                                 .Select(n => n.FirstName);

            Console.WriteLine(aaStud.Max());

        }
    }
}
