using LMT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace LMT.ResponsiveModels
{
	[DataContract]
	public class Student:LogBaseClass
	{
		[DataMember]
		public int ID { get; set; }
		[DataMember]
		public string StudentName { get; set; }
		[DataMember]
		public int BranchID { get; set; }
		[DataMember]
		public string BranchName { get; set; }
		[DataMember]
		public string Gender { get; set; }
		[DataMember]
		public string Mobile { get; set; }
		[DataMember]
		public DateTime DateOfBirth { get; set; }
		[DataMember]
		public string City { get; set; }
		[DataMember]

		public int Pincode { get; set; }
		[DataMember]

		public string Address { get; set; }
		[DataMember]
		public string Email { get; set; }
		[DataMember]
		public string Password { get; set; }
	}
}
