using LMT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace LMT.ResponsiveModels
{
	[DataContract]
	public class Publication:LogBaseClass
	{
		[DataMember]
		public int PublicationID { get; set; }
		[DataMember]
		public string PublicationName { get; set; }
		//[DataMember]
		//public int TotalCount { get; set; }
	}
}
