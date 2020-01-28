using System;
using System.Collections.Generic;
using System.Text;
using TechTalk.SpecFlow;

namespace PxTransformAutomation.StepDefinition
{
    [Binding]
    class PhysicianSearchStepDef
    {

        [Given(@"user has physician API for search")]
        public void GivenUserHasPhysicianAPIForSearch()
        {
            ScenarioContext.Current.Pending();
        }

        [Given(@"User had provided the required header information (.*),(.*)")]
        public void GivenUserHadProvidedTheRequiredHeaderInformation(string p0, string p1, Table table)
        {
            ScenarioContext.Current.Pending();
        }

        [When(@"user hits the API to search for physician whose name contains ""(.*)""")]
        public void WhenUserHitsTheAPIToSearchForPhysicianWhoseNameContains(string p0)
        {
            ScenarioContext.Current.Pending();
        }

        [Then(@"user should be able to get (.*) as status code")]
        public void ThenUserShouldBeAbleToGetAsStatusCode(int p0)
        {
            ScenarioContext.Current.Pending();
        }

    }
}
