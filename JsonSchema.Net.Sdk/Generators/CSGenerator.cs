﻿using System;
using System.IO;
using JsonSchema.Net.Sdk.Utils;
using NJsonSchema;
using NJsonSchema.CodeGeneration.CSharp;

namespace JsonSchema.Net.Sdk.Generators
{
    public class CSGenerator : GeneratorBase
    {
        public override TargetLanguage TargetLanguage => TargetLanguage.CS;

        protected override bool Generate(JsonSchemaItem item, JsonSchemaItemMetadata metadata)
        {
            try
            {
                var outputFilePath = item.GetOutputFilePath(TargetLanguage);
                var schema = JsonSchema4.FromFileAsync(item.SchemaFile).Result;
                var settings = new CSharpGeneratorSettings
                {
                    Namespace = metadata.Namespace,
                };
                var code = new CSharpGenerator(schema, settings).GenerateFile();
                File.WriteAllText(outputFilePath, code);

                return true;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, item.SchemaFile);
                return false;
            }
        }
    }
}