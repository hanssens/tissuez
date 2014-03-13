using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tissuez.Models
{
    public class SearchResults
    {
        public IEnumerable<Octokit.Issue> Issues { get; set; }
        public SearchCommand SearchRequest { get; set; }
    }
}