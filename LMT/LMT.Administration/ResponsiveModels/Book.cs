using LMT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace LMT.ResponsiveModels
{
	[DataContract]
	public class Book: LogBaseClass
	{
		[DataMember]
		public int ID { get; set; }
		[DataMember]

		public string BookName { get; set; }
		[DataMember]

		public int PublicationID { get; set; }
		[DataMember]

		public string PublicationName { get; set; }
		[DataMember]
		public string Author { get; set; }
		[DataMember]
		public string Details { get; set; }

		[DataMember]
		public int Quantity { get; set; }

		[DataMember]
		public int AvailableQuantity { get; set; }

		[DataMember]
		public int RentQuantity { get; set; }

		[DataMember]
		public int Price { get; set; }


		[DataMember]
		public string BookPhotoID { get; set; }
		[DataMember]
		public string Branches { get; set; }


		[DataMember]
		public List<Collection> BranchesList { get; set; }

		[DataMember]
		public string col1 { get; set; }
		[DataMember]
		public int BranchID { get; set; }
		[DataMember]
		public string BranchName { get; set; }
	
		[DataMember]
		public int StudentID { get; set; }
		[DataMember]
		public int Days { get; set; }
	}


}
