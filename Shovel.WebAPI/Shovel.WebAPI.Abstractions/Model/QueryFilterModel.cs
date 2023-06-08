using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Shovel.WebAPI.Abstractions.Model
{
    public class QueryFilterModel
    {
        [FromQuery(Name = "$orderby")]
        public string OrderBy { get; set; } = string.Empty;

        [FromQuery(Name = "$filter")]
        public string Filter { get; set; } = string.Empty;

        public Dictionary<string, string> ParsedFilter 
        { 
            get
            {
                var filterResult = new Dictionary<string, string>();
                foreach (var filter in Filter.Split(" and "))
                {
                    if (filter.Contains("(id "))
                    {
                        continue;
                    }

                    var deletingCount = 0;
                    for (; deletingCount < filter.Length; deletingCount++)
                    {
                        if (filter[deletingCount] == '\'')
                            break;
                    }

                    var str = filter
                        .Remove(0, deletingCount)
                        .Replace("tolower(", "");

                    var reversedFilter = filter.Reverse<char>().ToArray();
                    deletingCount = 0;
                    for (; deletingCount < filter.Length; deletingCount++)
                    {
                        if (reversedFilter[deletingCount] != ')')
                            break;
                    }
                    str = str.Remove(str.Length - deletingCount);
                    var res = str.Split(',');

                    filterResult.Add(res.Last(), res.First().Replace("'", ""));
                }

                return filterResult;
            } 
        }

        [FromQuery(Name = "$top")]
        public int Top { get;set; }

        [FromQuery(Name = "$inlinecount")]
        public string InlineCount { get;set; } = string.Empty;

        [FromQuery(Name = "$skip")]
        public int Skip { get;set; }
    }
}
