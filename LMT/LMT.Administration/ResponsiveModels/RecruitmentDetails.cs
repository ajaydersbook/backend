using LMT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace LMT.Administration.ResponsiveModels
{
	[DataContract]
	public class RecruitmentDetails: LogBaseClass
	{
		[DataMember]
		public int JobCode { get; set; }
		[DataMember]
		public string Description { get; set; }
		[DataMember]
		public string Skills { get; set; }
		[DataMember]
		public byte MinExp { get; set; }
		[DataMember]
		public byte MaxExp { get; set; }
		[DataMember]
		public byte TechRounds { get; set; }
		[DataMember]
		public byte HRRounds { get; set; }
		[DataMember]
		public string Status { get; set; }
		[DataMember]
		public string Title { get; set; }
		[DataMember]
		public List<Collection> SkillsList { get; set; }

	}
}
