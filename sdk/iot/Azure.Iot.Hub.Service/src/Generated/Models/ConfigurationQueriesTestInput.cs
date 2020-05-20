// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

// <auto-generated/>

#nullable disable

namespace Azure.Iot.Hub.Service.Models
{
    /// <summary> The ConfigurationQueriesTestInput. </summary>
    public partial class ConfigurationQueriesTestInput
    {
        /// <summary> Initializes a new instance of ConfigurationQueriesTestInput. </summary>
        public ConfigurationQueriesTestInput()
        {
        }

        /// <summary> Initializes a new instance of ConfigurationQueriesTestInput. </summary>
        /// <param name="targetCondition"> The query used to define targeted devices or modules. The query is based on twin tags and/or reported properties. </param>
        /// <param name="customMetricQueries"> Queries on twin reported properties. </param>
        internal ConfigurationQueriesTestInput(string targetCondition, string customMetricQueries)
        {
            TargetCondition = targetCondition;
            CustomMetricQueries = customMetricQueries;
        }

        /// <summary> The query used to define targeted devices or modules. The query is based on twin tags and/or reported properties. </summary>
        public string TargetCondition { get; set; }
        /// <summary> Queries on twin reported properties. </summary>
        public string CustomMetricQueries { get; set; }
    }
}