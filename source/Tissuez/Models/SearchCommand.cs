using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tissuez.Models
{
    public class SearchCommand
    {
        public string RepositoryName { get; set; }
        public string RepositoryOwner { get; set; }
        public string ApiKey { get; set; }
        public bool ResultsFound { get; set; }
    }
}