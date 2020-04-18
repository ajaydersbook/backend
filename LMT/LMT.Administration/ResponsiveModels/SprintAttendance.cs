using LMT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace LMT.Administration.ResponsiveModels
{
	[DataContract]
	public class SprintAttendance : LogBaseClass
	{
		[DataMember]
		public int ID { get; set; }
		[DataMember]
		public int SprintID { get; set; }
		[DataMember]
		public string Name { get; set; }
		[DataMember]
		public string TeamName { get; set; }

		[DataMember]
		public string Monday { get; set; }
		[DataMember]
		public string Tuesday { get; set; }

		[DataMember]
		public string WedNesday { get; set; }
		[DataMember]
		public string Thursday { get; set; }
		[DataMember]
		public string Friday { get; set; }

		[DataMember]
		public string Monday2 { get; set; }
		[DataMember]
		public string Tuesday2 { get; set; }

		[DataMember]
		public string WedNesday2 { get; set; }
		[DataMember]
		public string Thursday2 { get; set; }

		[DataMember]
		public string Friday2 { get; set; }

		[DataMember]
		public string Monday3 { get; set; }
		[DataMember]
		public string Tuesday3 { get; set; }

		[DataMember]
		public string WedNesday3 { get; set; }
		[DataMember]
		public string Thursday3 { get; set; }

		[DataMember]
		public string Friday3 { get; set; }
	}
}
