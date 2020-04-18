using LMT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace LMT.Administration.ResponsiveModels
{
	[DataContract]
	public class DropdownClass: LogBaseClass
	{
		[DataMember]
		public int DropID { get; set; }
		[DataMember]
		public string DropValue { get; set; }
	}
}
