using System;
using System.IO;
using yamlist.Modules.Commands;
using yamlist.Modules.Commands.Attributes;

namespace yamlist.Commands
{
    [Binds(typeof(FormatArguments))]
    public class FormatCommand
    {
        public FormatCommand(Context context)
        {
            Context = context;
        }

        public Context Context { get; }

        public int Execute(FormatArguments args)
        {
            var jsonWriter = new StringWriter();
            var toJson = new ToJsonCommand(Context) {Out = jsonWriter};
            toJson.Execute(new ToJsonArguments {InputFile = args.InputFile});
            
            File.WriteAllText("left.json", jsonWriter.ToString());
            
            var pipeline = Modules.IO.Json.Converter.JsonToConcourse(jsonWriter.ToString());
            var concourseJson = Modules.IO.Json.Converter.ConcourseToJson(pipeline);

            File.WriteAllText("right.json", concourseJson);
            
            var yamlWriter = new StringWriter();
            var toYaml = new ToYamlCommand(Context) {Out = yamlWriter};
            toYaml.Input = concourseJson;
            toYaml.Execute(new ToYamlArguments());

            Console.WriteLine(yamlWriter.ToString());

            return 0;
        }
    }
}