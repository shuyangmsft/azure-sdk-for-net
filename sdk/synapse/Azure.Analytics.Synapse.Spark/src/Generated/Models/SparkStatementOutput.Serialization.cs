// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

// <auto-generated/>

#nullable disable

using System.Collections.Generic;
using System.Text.Json;
using Azure.Core;

namespace Azure.Analytics.Synapse.Spark.Models
{
    public partial class SparkStatementOutput
    {
        internal static SparkStatementOutput DeserializeSparkStatementOutput(JsonElement element)
        {
            Optional<string> status = default;
            int executionCount = default;
            Optional<object> data = default;
            Optional<string> ename = default;
            Optional<string> evalue = default;
            Optional<IReadOnlyList<string>> traceback = default;
            foreach (var property in element.EnumerateObject())
            {
                if (property.NameEquals("status"))
                {
                    status = property.Value.GetString();
                    continue;
                }
                if (property.NameEquals("execution_count"))
                {
                    executionCount = property.Value.GetInt32();
                    continue;
                }
                if (property.NameEquals("data"))
                {
                    data = property.Value.GetObject();
                    continue;
                }
                if (property.NameEquals("ename"))
                {
                    ename = property.Value.GetString();
                    continue;
                }
                if (property.NameEquals("evalue"))
                {
                    evalue = property.Value.GetString();
                    continue;
                }
                if (property.NameEquals("traceback"))
                {
                    List<string> array = new List<string>();
                    foreach (var item in property.Value.EnumerateArray())
                    {
                        array.Add(item.GetString());
                    }
                    traceback = array;
                    continue;
                }
            }
            return new SparkStatementOutput(status.Value, executionCount, data.Value, ename.Value, evalue.Value, Optional.ToList(traceback));
        }
    }
}