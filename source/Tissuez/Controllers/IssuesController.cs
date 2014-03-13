using Octokit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tissuez.Models;

namespace Tissuez.Controllers
{
    public class IssuesController : Controller
    {
        public ActionResult Overview()
        {
            var model = new SearchCommand();
            return View(model);
        }

        //public ActionResult List(string owner, string name)
        public ActionResult List(SearchCommand command)
        {
            // get a person api key through https://github.com/settings/applications#personal-access-tokens

            var github = new GitHubClient(new ProductHeaderValue("Tissues-for-MVC"));
            if (!String.IsNullOrEmpty(command.ApiKey))
                github.Credentials = new Credentials(command.ApiKey);
            
            var issues = github.Issue.GetForRepository(command.RepositoryOwner, command.RepositoryName);

            command.ResultsFound = issues.Result.Any();
            var model = new SearchResults()
                {
                    Issues = issues.Result,
                    SearchRequest = command
                };

            return View(model);
        }

        public ActionResult Export(SearchCommand command)
        {
            return View();
        }
    }
}