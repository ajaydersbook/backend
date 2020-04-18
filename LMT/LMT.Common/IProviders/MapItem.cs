using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LMT.IProviders
{
	public class MapItem
	{
		public Type Type { get; private set; }
		public string DataRetriveType { get; private set; }
		public string PropertyName { get; private set; }

		public MapItem(Type type, string dataRetriveType, string propertyName)
		{
			Type = type;
			DataRetriveType = dataRetriveType;
			PropertyName = propertyName;
		}
	}
	public class DataRetriveType
	{
		public static string FirstOrDefault = "FirstOrDefault";
		public static string List = "List";
	}
	public class ReadItem
	{
		public static List<T> ItemOf<T>(dynamic x)
		{

			//IList objList = (IList)x;

			//IEnumerable<T> list = objList.Cast<T>();

			//List<T> parents = new List<T>(list);

			//IList data = (IList)d;
			//IEnumerable<T> list = data.Cast<T>();
			return new List<T>(x);
		}
	}
}
