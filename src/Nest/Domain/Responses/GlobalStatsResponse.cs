﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.Linq.Expressions;
using Nest.DSL;
using Nest.Domain;

namespace Nest
{
	[JsonObject]
	public class GlobalStatsResponse : BaseResponse
	{
		public GlobalStatsResponse()
		{
			this.IsValid = true;
		}

		[JsonProperty(PropertyName = "ok")]
		public bool OK { get; internal set; }

		[JsonProperty(PropertyName = "_shards")]
		public ShardsMetaData Shards { get; internal set; }

		[JsonProperty(PropertyName = "_all")]
		public GlobalStats Stats { get; set; }

	}
}