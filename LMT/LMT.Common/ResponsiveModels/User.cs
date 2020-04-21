using LMT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace LMT.Administration.ResponsiveModels
{
	[DataContract]
	public class User:LogBaseClass
	{
		[DataMember]
		public int ID { get; set; }
		[DataMember]
		public string Name { get; set; }
		[DataMember]
		public string LoginID { get; set; }
		[DataMember]
		public string Password { get; set; }
		[DataMember]
		public string GroupName { get; set; }
		[DataMember]
		public string Email { get; set; }
		[DataMember]
		public string UserName { get; set; }
		[DataMember]
		public string HashedText { get; set; }
	}
}
