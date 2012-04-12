using System;
using System.IO;
using System.Text.RegularExpressions;
using ConsoleOptions;

namespace findstr2
{
    class Program
    {
        static void Main(string[] args)
        {
            string rawRegex = "";
            string filePath = ".\\";
            bool multiLine=false;
            RegexOptions regOptions = RegexOptions.Compiled;
            var opts = new Options("Finds strings matching a regular expression in a file")
                           {
                               new Option( x => rawRegex = x, "regex","The regex text being searched for matches."),
                               new Option(new[] {"l", "multiline"}, () => multiLine=true, "If file will be consolidated to a single line before being searched."),
                               new Option(new[] {"i", "ignorecase"}, ()=> regOptions|=RegexOptions.IgnoreCase, "If casing should be ignored during the search." ),
                               new Option(x=> filePath=x, "file", "The file to be searched"),
                           };
            if(!opts.Parse(args))
            {
                return;
            }
            Regex searchPattern = new Regex(rawRegex, regOptions);
            string text = multiLine ? string.Join("",File.ReadAllLines(filePath)) : File.ReadAllText(filePath);
            
            

            var matchs = searchPattern.Matches(text);
            foreach (var m in matchs)
            {
                Console.WriteLine(m.ToString());
            }

        }
    }
}
