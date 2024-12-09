using Routers;
string helpMessage = """
                        Usage: Routers <path-to-input-file> <path-to-output-file> - to build a configuration for topology written in input file
                               Routers -h || --help - to print this message
                        """;

if (args.Length != 2 || args[0] == "-h" || args[0] == "--help")
{
    Console.Write(helpMessage);
    return 0;
}
string inputfile = args[0];
string outputfile = args[1];
if (inputfile == null || outputfile == null)
{
    Console.Error.WriteLine("Incorrect input");
    return 1;
}
if (!File.Exists(args[0]))
{
    Console.Error.WriteLine($"File {inputfile} was not found");
    return 1;
}
string? pathToDirectory = Path.GetDirectoryName(outputfile);
if (!Directory.Exists(pathToDirectory) && pathToDirectory != string.Empty)
{
    Console.Error.WriteLine( $"There is no such directory {pathToDirectory}");
    return 1;
}

try
{
    Configurator.Configure(inputfile, outputfile);
}
catch (NetworkIsNotConnectedException exception)
{
    Console.Error.WriteLine(exception.Message);
    return 1;
}

Console.WriteLine($"Configuration for topology given in {inputfile} was build and written to {outputfile}");

return 0;