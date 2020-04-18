using LMT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LMT.Administration.ResponsiveModels
{
	public class NCRRuleConfiguration : LogBaseClass
	{
		public string NCRType { get; set; }

		public string status { get; set; }

		public string NCRTabName { get; set; }
	}
}
