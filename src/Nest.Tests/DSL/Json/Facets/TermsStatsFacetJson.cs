﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Nest.DSL;
using Nest.TestData.Domain;

namespace Nest.Tests.Dsl.Json.Facets
{
  [TestFixture]
  public class TermsStatsFacetJson
  {
    [Test]
    public void TermsStats()
    {
      var s = new SearchDescriptor<ElasticSearchProject>()
        .From(0)
        .Size(10)
        .Query(@"{ raw : ""query""}")
        .FacetTermsStats(ts => ts
          .KeyField(f=>f.Name)
          .ValueField(f=>f.LOC)
        );
      var json = ElasticClient.Serialize(s);
      var expected = @"{ from: 0, size: 10, 
          facets :  {
            ""name.sort"" :  {
                terms_stats : {
                    key_field : ""name.sort"",
                    value_field : ""loc.sort""
                } 
            }
          }, query : { raw : ""query""}
      }";
      Assert.True(json.JsonEquals(expected), json);
    }
    [Test]
    public void TermsStatsScript()
    {
      var s = new SearchDescriptor<ElasticSearchProject>()
        .From(0)
        .Size(10)
        .Query(@"{ raw : ""query""}")
        .FacetTermsStats("date_minute", ts => ts
          .KeyScript("doc['date'].date.minuteOfHour * factor1")
          .ValueScript("doc['num1'].value * factor2")
          .Order(TermsStatsOrder.reverse_max)
          .Params(p=>p
            .Add("factor1", 2)
            .Add("factor2", 3)
            .Add("randomString", "stringy")
          )
        );
      var json = ElasticClient.Serialize(s);
      var expected = @"{ from: 0, size: 10, 
          facets :  {
            ""date_minute"" :  {
                terms_stats : {
                    key_script : ""doc['date'].date.minuteOfHour * factor1"",
                    value_script : ""doc['num1'].value * factor2"",
                    order : ""reverse_max"",
                    params : {
                      factor1 : 2,
                      factor2 : 3,
                      randomString: ""stringy""
                    }
                } 
            }
          }, query : { raw : ""query""}
      }";
      Assert.True(json.JsonEquals(expected), json);
    }
  }
}
