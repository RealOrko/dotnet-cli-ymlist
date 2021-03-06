using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using yamlist.Modules.IO.Json.Model;
using yamlist.Modules.IO.Json.Model.Meta;

namespace yamlist.Modules.IO.Json.Converters
{
    public class JobPlanConverter: JsonConverter
    {
        private readonly Type[] _types;

        public JobPlanConverter()
        {
            _types = new[]
            {
                typeof(JobPlan),
                typeof(JobPlanEnsure),
            };
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (value is JobPlan jp)
            {
                WriteJobPlan(writer, serializer, jp);
            }
        }

        private static void WriteJobPlan(JsonWriter writer, JsonSerializer serializer, JobPlan jp)
        {
            if (jp.AnchorCall != null)
            {
                writer.WriteValue(jp.AnchorCall.Method);
                return;
            }
            
            writer.WriteStartObject();

            if (jp.TaskAnchorDeclaration != null)
            {
                writer.WritePropertyName(jp.TaskAnchorDeclaration.Method);
                writer.WriteValue(1);
            }
            
            if (jp.InParallel != null && jp.InParallel.Count > 0)
            {
                writer.WritePropertyName("in_parallel");
                writer.WriteStartArray();
                foreach (var jpp in jp.InParallel)
                {
                    WriteJobPlan(writer, serializer, jpp);
                }
                writer.WriteEndArray();
            }

            if (jp.Do != null && jp.Do.Count > 0)
            {
                writer.WritePropertyName("do");
                writer.WriteStartArray();
                foreach (var jpp in jp.Do)
                {
                    WriteJobPlan(writer, serializer, jpp);
                }
                writer.WriteEndArray();
            }

            if (jp.Try != null)
            {
                writer.WritePropertyName("try");
                WriteJobPlan(writer, serializer, jp.Try);
            }
                
            if (!string.IsNullOrEmpty(jp.SetPipeline))
            {
                writer.WritePropertyName("set_pipeline");
                writer.WriteValue(jp.SetPipeline);
            }

            if (!string.IsNullOrEmpty(jp.Task))
            {
                writer.WritePropertyName("task");
                writer.WriteValue(jp.Task);
            }

            if (!string.IsNullOrEmpty(jp.File))
            {
                writer.WritePropertyName("file");
                writer.WriteValue(jp.File);
            }

            if (!string.IsNullOrEmpty(jp.Image))
            {
                writer.WritePropertyName("image");
                writer.WriteValue(jp.Image);
            }

            if (!string.IsNullOrEmpty(jp.Get))
            {
                writer.WritePropertyName("get");
                writer.WriteValue(jp.Get);
            }

            if (!string.IsNullOrEmpty(jp.Put))
            {
                writer.WritePropertyName("put");
                writer.WriteValue(jp.Put);
            }

            if (!string.IsNullOrEmpty(jp.Resource))
            {
                writer.WritePropertyName("resource");
                writer.WriteValue(jp.Resource);
            }

            if (!string.IsNullOrEmpty(jp.Trigger))
            {
                writer.WritePropertyName("trigger");
                writer.WriteValue(jp.Trigger);
            }

            if (jp.Attempts > 0)
            {
                writer.WritePropertyName("attempts");
                writer.WriteValue(jp.Attempts);
            }

            if (jp.MergeCall != null)
            {
                writer.WritePropertyName(jp.MergeCall.Name);
                writer.WriteValue(jp.MergeCall.Method);
            }

            if (jp.VarFiles != null && jp.VarFiles.Count > 0)
            {
                writer.WritePropertyName("var_files");
                writer.WriteStartArray();

                foreach (var varFile in jp.VarFiles)
                {
                    writer.WriteValue(varFile);
                }

                writer.WriteEndArray();
            }

            if (jp.Passed != null && jp.Passed.Count > 0)
            {
                writer.WritePropertyName("passed");
                writer.WriteStartArray();

                foreach (var passed in jp.Passed)
                {
                    writer.WriteValue(passed);
                }

                writer.WriteEndArray();
            }

            if (jp.InputMapping != null && jp.InputMapping.Count > 0)
            {
                WriteJobPlanInputMapping(writer, serializer, jp);
            }

            if (jp.Params != null && jp.Params.Count > 0)
            {
                WriteJobPlanParams(writer, serializer, jp);
            }
            
            if (jp.Config != null || jp.ConfigAnchorCall != null || jp.ConfigAnchorDeclaration != null)
            {
                WriteJobPlanConfig(writer, serializer, jp);
            }

            if (jp.OutputMapping != null && jp.OutputMapping.Count > 0)
            {
                WriteJobPlanOutputMapping(writer, serializer, jp);
            }
            
            if (jp.GetParams != null && jp.GetParams.Count > 0)
            {
                WriteJobPlanGetParams(writer, serializer, jp);
            }
            
            if (jp.PutParams != null && jp.PutParams.Count > 0)
            {
                WriteJobPlanPutParams(writer, serializer, jp);
            }
            
            if (jp.Ensure != null)
            {
                WriteJobPlanEnsure(writer, serializer, jp);
            }
            
            writer.WriteEndObject();
        }

        private static void WriteJobPlanEnsure(JsonWriter writer, JsonSerializer serializer, JobPlan jp)
        {
            writer.WritePropertyName("ensure");

            if (jp.Ensure.Value != null)
            {
                writer.WriteValue(jp.Ensure.Value);
            }
            else
            {
                WriteJobPlan(writer, serializer, jp.Ensure);
            }
        }

        private static void WriteJobPlanPutParams(JsonWriter writer, JsonSerializer serializer, JobPlan jp)
        {
            writer.WritePropertyName("put_params");
            writer.WriteStartObject();

            foreach (var kv in jp.PutParams)
            {
                writer.WritePropertyName(kv.Key);
                if (kv.Value is JArray arr)
                {
                    arr.WriteTo(writer);
                }
                else if (kv.Value is JObject obj)
                {
                    obj.WriteTo(writer);
                }
                else
                {
                    writer.WriteValue(kv.Value);
                }
            }

            writer.WriteEndObject();
        }

        private static void WriteJobPlanGetParams(JsonWriter writer, JsonSerializer serializer, JobPlan jp)
        {
            writer.WritePropertyName("get_params");
            writer.WriteStartObject();

            foreach (var kv in jp.GetParams)
            {
                writer.WritePropertyName(kv.Key);
                if (kv.Value is JArray arr)
                {
                    arr.WriteTo(writer);
                }
                else if (kv.Value is JObject obj)
                {
                    obj.WriteTo(writer);
                }
                else
                {
                    writer.WriteValue(kv.Value);
                }
            }

            writer.WriteEndObject();
        }

        private static void WriteJobPlanOutputMapping(JsonWriter writer, JsonSerializer serializer, JobPlan jp)
        {
            writer.WritePropertyName("output_mapping");
            writer.WriteStartObject();

            foreach (var kv in jp.OutputMapping)
            {
                writer.WritePropertyName(kv.Key);
                writer.WriteValue(kv.Value);
            }

            writer.WriteEndObject();
        }

        private static void WriteJobPlanInputMapping(JsonWriter writer, JsonSerializer serializer, JobPlan jp)
        {
            writer.WritePropertyName("input_mapping");
            writer.WriteStartObject();

            foreach (var kv in jp.InputMapping)
            {
                writer.WritePropertyName(kv.Key);
                writer.WriteValue(kv.Value);
            }

            writer.WriteEndObject();
        }

        private static void WriteJobPlanParams(JsonWriter writer, JsonSerializer serializer, JobPlan jp)
        {
            writer.WritePropertyName("params");
            writer.WriteStartObject();

            foreach (var kv in jp.Params)
            {
                writer.WritePropertyName(kv.Key);
                if (kv.Value is JArray arr)
                {
                    arr.WriteTo(writer);
                }
                else if (kv.Value is JObject obj)
                {
                    obj.WriteTo(writer);
                }
                else
                {
                    if (kv.Value != null)
                    {
                        writer.WriteValue(kv.Value);
                    }
                    else
                    {
                        writer.WriteNull();
                    }
                }
            }

            writer.WriteEndObject();
        }

        private static void WriteJobPlanConfig(JsonWriter writer, JsonSerializer serializer, JobPlan jp)
        {
            if (jp.ConfigAnchorCall != null)
            {
                writer.WritePropertyName("config");
                writer.WriteValue(jp.ConfigAnchorCall.Method);
                return;
            }

            if (jp.ConfigAnchorDeclaration != null)
            {
                writer.WritePropertyName(jp.ConfigAnchorDeclaration.Method);                
            }
            else
            {
                writer.WritePropertyName("config");
            }
            
            serializer.Serialize(writer, jp.Config, typeof(JobPlanConfig));
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            JobPlan jobPlan = null;
            if (objectType == typeof(JobPlan))
            {
                jobPlan = new JobPlan();
            } 
            else if (objectType == typeof(JobPlanEnsure))
            {
                jobPlan = new JobPlanEnsure();
            }

            if (reader.Value?.ToString()?.StartsWith("_anchor_call") != null)
            {
                var jobPlanAnchorCall = new JobPlan();
                jobPlanAnchorCall.AnchorCall = new AnchorCall();
                jobPlanAnchorCall.AnchorCall.Method = reader.Value.ToString();
                return jobPlanAnchorCall;
            }
            
            var jObject = JObject.Load(reader);

            foreach (var property in jObject.Properties())
            {
                if (property.Name.StartsWith("_anchor_list_decl"))
                {
                    jobPlan.TaskAnchorDeclaration = new AnchorDeclaration();
                    jobPlan.TaskAnchorDeclaration.Method = property.Name;
                }
                
                if (property.Name == "in_parallel")
                {
                    var stepsFound = false;
                    var pObject = JsonConvert.DeserializeObject(property.Value.ToString());
                    if (pObject is JObject pjObj)
                    {
                        foreach (var pjProp in pjObj)
                        {
                            if (pjProp.Key == "steps")
                            {
                                stepsFound = true;
                                jobPlan.InParallel = JsonConvert.DeserializeObject<List<JobPlan>>(pjProp.Value.ToString(), Converter.Settings);
                            }
                        }
                    }

                    if (!stepsFound)
                    {
                        jobPlan.InParallel = JsonConvert.DeserializeObject<List<JobPlan>>(property.Value.ToString(), Converter.Settings);
                    }
                    continue;
                }

                if (property.Name == "do")
                {
                    jobPlan.Do = JsonConvert.DeserializeObject<List<JobPlan>>(property.Value.ToString(), Converter.Settings);
                    continue;
                }

                if (property.Name == "try")
                {
                    jobPlan.Try = JsonConvert.DeserializeObject<JobPlan>(property.Value.ToString(), Converter.Settings);
                }

                if (property.Name == "task")
                {
                    jobPlan.Task = property.Value?.ToString();
                    continue;
                }

                if (property.Name == "config")
                {
                    if (property.Value.ToString().StartsWith("_anchor_call"))
                    {
                        jobPlan.ConfigAnchorCall = new AnchorCall();
                        jobPlan.ConfigAnchorCall.Method = property.Value.ToString();
                    }
                    else
                    {
                        jobPlan.Config = JsonConvert.DeserializeObject<JobPlanConfig>(property.Value.ToString(), Converter.Settings);
                    }
                    continue;
                }

                if (property.Name.StartsWith("config_anchor_decl_"))
                {
                    jobPlan.ConfigAnchorDeclaration = new AnchorDeclaration();
                    jobPlan.ConfigAnchorDeclaration.Method = property.Name;
                    jobPlan.Config = JsonConvert.DeserializeObject<JobPlanConfig>(property.Value.ToString(), Converter.Settings);
                }
                
                if (property.Name == "set_pipeline")
                {
                    jobPlan.SetPipeline = property.Value.ToString();
                    continue;
                }
                
                if (property.Name == "var_files")
                {
                    jobPlan.VarFiles = JsonConvert.DeserializeObject<List<string>>(property.Value?.ToString());
                    continue;
                }

                if (property.Name == "get")
                {
                    jobPlan.Get = property.Value.ToString();
                    continue;
                }
                
                if (property.Name == "put")
                {
                    jobPlan.Put = property.Value.ToString();
                    continue;
                }
                
                if (property.Name == "attempts")
                {
                    jobPlan.Attempts = Convert.ToInt32(property.Value.ToString());
                    continue;
                }
                
                if (property.Name == "trigger")
                {
                    jobPlan.Trigger = property.Value.ToString();
                    continue;
                }
                
                if (property.Name == "passed")
                {
                    jobPlan.Passed = JsonConvert.DeserializeObject<List<string>>(property.Value?.ToString());
                    continue;
                }
                
                if (property.Name == "resource")
                {
                    jobPlan.Resource = property.Value?.ToString();
                    continue;
                }
                
                if (property.Name == "ensure")
                {
                    jobPlan.Ensure = new JobPlanEnsure();
                    if (property.Value.GetType() == typeof(JValue))
                    {
                        jobPlan.Ensure.Value = property.Value?.ToString();
                    } 
                    else if (property.Value.GetType() == typeof(JObject))
                    {
                        jobPlan.Ensure = JsonConvert.DeserializeObject<JobPlanEnsure>(property.Value.ToString(), Converter.Settings);
                    }                    
                    continue;
                }
                
                if (property.Name == "image")
                {
                    jobPlan.Image = property.Value?.ToString();
                    continue;
                }
                
                if (property.Name == "file")
                {
                    jobPlan.File = property.Value?.ToString();
                    continue;
                }
                
                if (property.Name == "params")
                {
                    jobPlan.Params = JsonConvert.DeserializeObject<Dictionary<string, dynamic>>(property.Value?.ToString());
                    continue;
                }
                
                if (property.Name == "get_params")
                {
                    jobPlan.GetParams = JsonConvert.DeserializeObject<Dictionary<string, dynamic>>(property.Value?.ToString());
                    continue;
                }
                
                if (property.Name == "put_params")
                {
                    jobPlan.PutParams = JsonConvert.DeserializeObject<Dictionary<string, dynamic>>(property.Value?.ToString());
                    continue;
                }
                
                if (property.Name == "input_mapping")
                {
                    jobPlan.InputMapping = JsonConvert.DeserializeObject<Dictionary<string, string>>(property.Value?.ToString());
                    continue;
                }
                
                if (property.Name == "output_mapping")
                {
                    jobPlan.OutputMapping = JsonConvert.DeserializeObject<Dictionary<string, string>>(property.Value?.ToString());
                }

                if (property.Name.StartsWith("_merge"))
                {
                    var mergeCall = new MergeCall();
                    mergeCall.Name = property.Name;
                    mergeCall.Method = property.Value?.ToString();
                    jobPlan.MergeCall = mergeCall;
                }

            }

            return jobPlan;
        }

        public override bool CanRead => true;

        public override bool CanConvert(Type objectType)
        {
            return _types.Any(t => t == objectType);
        }
    }
}