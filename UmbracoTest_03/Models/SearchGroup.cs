using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UmbracoTest_03.Models
{
    public class SearchGroup
    {
        public string[] FieldsToSearchIn { get; set; }
        public string[] SearchTerms { get; set; }

        public SearchGroup(string[] fieldsToSearchIn, string[] searchTerms)
        {
            FieldsToSearchIn = fieldsToSearchIn;
            SearchTerms = searchTerms;
        }
    }
}