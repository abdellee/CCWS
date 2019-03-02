/**
* Copyright 2018 IBM Corp. All Rights Reserved.
*
* Licensed under the Apache License, Version 2.0 (the "License");
* you may not use this file except in compliance with the License.
* You may obtain a copy of the License at
*
*      http://www.apache.org/licenses/LICENSE-2.0
*
* Unless required by applicable law or agreed to in writing, software
* distributed under the License is distributed on an "AS IS" BASIS,
* WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
* See the License for the specific language governing permissions and
* limitations under the License.
*
*/

using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace IBM.WatsonDeveloperCloud.CompareComply.v1.Model
{
    /// <summary>
    /// Termination dates identified in the input document.
    /// </summary>
    public class TerminationDates : BaseModel
    {
        /// <summary>
        /// The confidence level in the identification of the termination date.
        /// </summary>
        /// <value>
        /// The confidence level in the identification of the termination date.
        /// </value>
        [JsonConverter(typeof(StringEnumConverter))]
        public enum ConfidenceLevelEnum
        {
            
            /// <summary>
            /// Enum HIGH for High
            /// </summary>
            [EnumMember(Value = "High")]
            HIGH,
            
            /// <summary>
            /// Enum MEDIUM for Medium
            /// </summary>
            [EnumMember(Value = "Medium")]
            MEDIUM,
            
            /// <summary>
            /// Enum LOW for Low
            /// </summary>
            [EnumMember(Value = "Low")]
            LOW
        }

        /// <summary>
        /// The confidence level in the identification of the termination date.
        /// </summary>
        [JsonProperty("confidence_level", NullValueHandling = NullValueHandling.Ignore)]
        public ConfidenceLevelEnum? ConfidenceLevel { get; set; }
        /// <summary>
        /// The termination date.
        /// </summary>
        [JsonProperty("text", NullValueHandling = NullValueHandling.Ignore)]
        public string Text { get; set; }
        /// <summary>
        /// The numeric location of the identified element in the document, represented with two integers labeled
        /// `begin` and `end`.
        /// </summary>
        [JsonProperty("location", NullValueHandling = NullValueHandling.Ignore)]
        public Location Location { get; set; }
    }

}
