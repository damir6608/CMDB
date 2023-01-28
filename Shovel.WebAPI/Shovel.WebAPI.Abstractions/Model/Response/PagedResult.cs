using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shovel.WebAPI.Abstractions.Model.Response
{
    /// <summary>
    /// Represents a data grid paged load result
    /// </summary>
    public class PagedResult
    {
        public PagedResult(IEnumerable data)
        {
            Data = data;
        }

        /// <summary>
        /// A resulting dataset
        /// </summary>
        public IEnumerable Data { get; }

        /// <summary>
        /// The total number of data objects in the resulting dataset (without paging)
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public int? TotalCount { get; set; }

        /// <summary>
        /// Total summary calculation results
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public IEnumerable<double>? Summary { get; set; }
    }
}
