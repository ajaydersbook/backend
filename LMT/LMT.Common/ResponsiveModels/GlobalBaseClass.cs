using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace LMT.Models
{
	[DataContract]
	public class GlobalBaseClass
	{
		public int TotalCount { get; set; }

		[DataMember]
		public int CreatedBy { get; set; }
		[DataMember]
		public string CreatedByName { get; set; }
		[DataMember]
		public DateTime? CreatedDate { get; set; }

		[DataMember]
		public int UpdatedBy { get; set; }
		[DataMember]
		public string UpdatedByName { get; set; }
		[DataMember]
		public DateTime? UpdatedDate { get; set; }
		[DataMember]
		[JsonIgnore]
		public int DeletedBy { get; set; }
		[DataMember]
		public DateTime? DeletedDate { get; set; }
		[DataMember]
		public string DeletedByName { get; set; }
		[DataMember]
		[JsonIgnore]
		public int ApprovedBy { get; set; }
		[DataMember]
		public DateTime? ApprovedDate { get; set; }
		[DataMember]
		public string ApprovedByName { get; set; }
		[DataMember]
		[JsonIgnore]
		public int RestoredBy { get; set; }
		[DataMember]
		public DateTime? RestoredDate { get; set; }
		[DataMember]
		public string RestoredByName { get; set; }
	}
	[DataContract]
	public class SearchResult
	{
		public List<object> Rows { get; set; }
		public int TotalRows { get; set; }
	}
	public class LogBaseClass : GlobalBaseClass
	{
		[DataMember]
		public string Actions { get; set; }
		[DataMember]
		public string ActionStatus { get; set; }
		[DataMember]
		public int AuthorizedBy { get; set; }
		[DataMember]
		public string AuthorizedByName { get; set; }
		[DataMember]
		public DateTime? AuthorizedDate { get; set; }
		[DataMember]
		public int RequestedBy { get; set; }
		[DataMember]
		public string RequestedByName { get; set; }
		[DataMember]
		public DateTime? RequestedDate { get; set; }
		[DataMember]
		public string Remarks { get; set; }
		[DataMember]
		public string Reason { get; set; }
		//[DataMember]
		//public int TotalRows { get; set; }
	}

	public class DateRangeFilter
	{
		public DateTime? CreatedDateStart { get; set; }
		public DateTime? CreatedDateEnd { get; set; }
		public DateTime? UpdatedDateStart { get; set; }
		public DateTime? UpdatedDateEnd { get; set; }
		public DateTime? InterviewDateStart { get; set; }
		public DateTime? InterviewDateEnd { get; set; }
		public DateTime? ActionDateStart { get; set; }
		public DateTime? ActionDateEnd { get; set; }
	}

	public class DateRange : DateRangeFilter
	{
		public DateTime? EffectiveDateStart { get; set; }
		public DateTime? EffectiveDateEnd { get; set; }
		public DateTime? ClosingDateStart { get; set; }
		public DateTime? ClosingDateEnd { get; set; }
	}

	public class Status
	{
		public int Code { get; set; }
		public string Message { get; set; }
		public dynamic Results { get; set; }
	}

	public class Pagination
	{
		public int PageSize { get; set; }
		public int PageNumber { get; set; }
		public int TotalRows { get; set; }
	}

	public class SortingData : Pagination
	{
		public string SortColumn { get; set; }
		public string SortOrder { get; set; }
	}

	[DataContract]
	public class ResponseModel
	{
		[DataMember(Name = "Response")]
		public Status status { get; set; }

		[DataMember(Name = "Pagination", EmitDefaultValue = false)]
		public Pagination pagination { get; set; }

		[DataMember(Name = "Data")]
		public object data { get; set; }
	}

	public class DefaultPageSize
	{
		public const int PageSize = 15;
	}

	public class Group
	{
		public int ID { get; set; }
		public int ParentID { get; set; }
		public string Name { get; set; }
		public List<Group> Children { get; set; }
	}

	public class DepartmentGroup
	{
		public int ID { get; set; }
		public int ParentID { get; set; }
		public string DepartmentName { get; set; }
		public List<DepartmentGroup> Children { get; set; }
	}
	public class Collection
	{
		[JsonIgnore]
		public string RefID { get; set; }
		public int ID { get; set; }
		public string Name { get; set; }
	}
}

