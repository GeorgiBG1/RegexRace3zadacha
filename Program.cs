using System.Linq;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace RegexRace
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Top3();
        }
        public static void Top3()
        {           
            List<string> names = Console.ReadLine()!.Split(", ").ToList();
            string input = Console.ReadLine()!;
            List<Racer> racs = new List<Racer>();
            Racer racer = new Racer(input, input);
            while (input != "end of race")
            {
            for (int i = 0; i < names.Count(); i++)
            {
                if (names[i] == racer.Name)
                {
                    racs.Add(racer);
                }
            }
                input = Console.ReadLine()!;
                racer = new Racer(input, input);
            }
            Console.Clear();
            Console.WriteLine(string.Join(", ", names));
            racs = racs.DistinctBy(x => x.Name).ToList();
            if (racs.Count() >= 3)
            {
                var names1 = racs
                    .OrderByDescending(d => d.Distance)
                    .Take(3)
                    .Select(r => r.Name)
                    .ToList();
                //var distance1 = racs
                //    .OrderByDescending(d => d.Distance)
                //    .Take(3)
                //    .Select(r => r.Distance).ToList();
                Console.WriteLine($"1st place: {names1[0]} "/*and distance: {distance1[0]}"*/);
                Console.WriteLine($"2nd place: {names1[1]} "/*and distance: {distance1[1]}"*/);
                Console.WriteLine($"3rd place: {names1[2]} "/*and distance: {distance1[2]}"*/);
            }
        }
    }
    public class Racer
    {
        private string stringPattern = @"(?<name>[A-Za-z*]*)";
        private string numPattern = @"(?<distance>[0-9])";
        private int sum;
        private List<string> stringNumbers2 = new List<string>();
        public string? Name { get; }
        public int Distance => sum;
        public Racer(string name, string distance)
        {
            MatchCollection matches = Regex.Matches(name, stringPattern);
            List<string> names = matches.Select(m => m.Groups["name"].Value).ToList();
            string name1 = string.Concat(names);
            this.Name = name1;
            MatchCollection matches2 = Regex.Matches(distance, numPattern);
            List<string> stringNumbers = matches2.Select(sN => sN.Groups["distance"].Value).ToList();
            for (int i = 0; i < stringNumbers.Count(); i++)
            {
                if (stringNumbers[i] != "")
                {
                    stringNumbers2.Add(stringNumbers[i]);
                }
            }
            List<int> numbers = new List<int>();
            for (int i = 0; i < stringNumbers2.Count; i++)
            {
                numbers.Add(Convert.ToInt32(stringNumbers2[i]));
            }
            sum = numbers.Sum();
        }
    }
}