using System.CommandLine;
using System.CommandLine.Parsing; 

namespace CommandLineArg;

public class App
{
    public static void Main(string[] args)
    {
        
        RootCommand rootCommand = new RootCommand("Root command")
        {
            new Option<string>("--name")
            {
                Description = "Pass a name as option"
            },
            new Option<int>("--number")
            {
                Description = "Pass a number as option"
            }
        };

        ParseResult parseResult = rootCommand.Parse(args);

        if (parseResult.Errors.Count > 0)
        {
            foreach(ParseError error in parseResult.Errors)
            {
                Console.Error.WriteLine(error.Message);
            }
        }

        Console.WriteLine(parseResult.GetValue<string>("--name"));
        Console.WriteLine(parseResult.GetValue<int>("--number"));
    }
}

