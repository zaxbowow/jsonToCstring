using System.CommandLine;
using System.ComponentModel.Design;
using Newtonsoft.Json.Linq;

// See https://aka.ms/new-console-template for more information
var arrayNameOption = new Option<string>(
    name: "-n",
    description: "The name for the array",
    parseArgument: result => {
        if (result.Tokens.Count == 0) {
            result.ErrorMessage = "Array name not specified";
            return null;
        }
        string arrayName = result.Tokens.Single().Value;
        return arrayName;
    }) { IsRequired = true };

var inputFileOption = new Option<FileInfo>(
    name: "-i",
    description: "The uncompressed json input file name",
    parseArgument: result =>
    {
        if (result.Tokens.Count == 0)
        {
            result.ErrorMessage = "Input file not specified";
            return null;
        }
        string? filePath = result.Tokens.Single().Value;
        if (!File.Exists(filePath))
        {
            result.ErrorMessage = "Input file does not exist";
            return null;
        }
        else
        {
            return new FileInfo(filePath);
        }
    }){IsRequired = true };

var outputFileOption = new Option<FileInfo>(
    name: "-o",
    description: "The c file output name"){IsRequired = true };

var lineLengthOption = new Option<int>(
    name: "-l",
    description: "Characters per line for c source file (default 95)",
    parseArgument: result => {
        if (result.Tokens.Count == 0) {
            return 120;
        }
        string? filePath = result.Tokens.Single().Value;
        int lineLength;
        if (int.TryParse(result.Tokens.Single().Value, out lineLength) == false) {
            result.ErrorMessage = "Invalid line length";
            return 0;
        } else {
            return lineLength;
        }
    }) { IsRequired = false };

var appendOption = new Option<bool>(
    name: "-a",
    description: "Append to existing file",
    parseArgument: result => {
        return true;
    }) { IsRequired = false };

var rootCommand = new RootCommand("Json to C source file");

rootCommand.AddOption(arrayNameOption);
rootCommand.AddOption(inputFileOption);
rootCommand.AddOption(outputFileOption);
rootCommand.AddOption(lineLengthOption);
rootCommand.AddOption(appendOption);

rootCommand.SetHandler((string arrayName, FileInfo infile, FileInfo outfile, int lineLength, bool append) =>
    {
        if (lineLength == 0) lineLength = 95;
        string uncompressedJson;
        using (StreamReader sr = infile.OpenText()) { uncompressedJson = sr.ReadToEnd(); }
        var jtoken = JToken.Parse(uncompressedJson);
        string compressedJson = jtoken.ToString(Newtonsoft.Json.Formatting.None);
        compressedJson = compressedJson.Replace("\"", "\\\"");

        string startString = "const char " + arrayName + "[] = ";
        int indent = startString.Length;
        int endsInSlash = 0; //  If line ends with "/" but not with "//", push it to next line.

        using (StreamWriter sw = append ? outfile.AppendText() : outfile.CreateText()) {
            for (int i=0; i < compressedJson.Length; i += lineLength - endsInSlash)  {
                if (i==0) sw.Write(startString); else sw.Write(new string(' ', indent));
                string thisLine = compressedJson.Substring(i, Math.Min(lineLength, compressedJson.Length - i));
                if (thisLine.EndsWith("\\") && !thisLine.EndsWith("\\\\")) {
                    endsInSlash = 1;
                    thisLine = thisLine.Substring(0, thisLine.Length - 1);
                } else endsInSlash = 0;
                sw.Write("\"" + thisLine + "\"");
                if (i + lineLength - endsInSlash < compressedJson.Length)
                    sw.Write("\r\n");
                else
                    sw.Write(";\r\n\r\n");
            }
        }
    }, arrayNameOption, inputFileOption, outputFileOption, lineLengthOption, appendOption);

return await rootCommand.InvokeAsync(args);

