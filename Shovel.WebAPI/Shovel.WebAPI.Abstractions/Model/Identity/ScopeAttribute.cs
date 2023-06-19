using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shovel.WebAPI.Abstractions.Model.Identity
{
    public class ScopeAttribute : Attribute
    {
        public string Scope { get; set; }

        public ScopeAttribute(string scope)
        {
            Scope = scope;
        }
    }
}
