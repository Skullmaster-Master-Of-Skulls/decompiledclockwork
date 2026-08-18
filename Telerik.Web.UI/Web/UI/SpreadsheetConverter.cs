using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Web.Script.Serialization;
using Telerik.Web.Spreadsheet;

namespace Telerik.Web.UI
{
	// Token: 0x0200089E RID: 2206
	internal class SpreadsheetConverter : JavaScriptConverter
	{
		// Token: 0x0600520D RID: 21005 RVA: 0x000FF9F6 File Offset: 0x000FDBF6
		public override object Deserialize(IDictionary<string, object> dictionary, Type type, JavaScriptSerializer serializer)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600520E RID: 21006 RVA: 0x000FFA00 File Offset: 0x000FDC00
		public override IDictionary<string, object> Serialize(object obj, JavaScriptSerializer serializer)
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			foreach (PropertyInfo propertyInfo in obj.GetType().GetProperties())
			{
				object value = propertyInfo.GetValue(obj, null);
				DataMemberAttribute dataMemberAttribute = propertyInfo.GetCustomAttributes(typeof(DataMemberAttribute), false).First<object>() as DataMemberAttribute;
				if (dataMemberAttribute.EmitDefaultValue || !object.Equals(value, SpreadsheetConverter.GetDefaultValue(propertyInfo.PropertyType)))
				{
					dictionary[dataMemberAttribute.Name] = value;
				}
			}
			return dictionary;
		}

		// Token: 0x0600520F RID: 21007 RVA: 0x000FFA88 File Offset: 0x000FDC88
		public static object GetDefaultValue(Type type)
		{
			object result = null;
			if (type.IsValueType)
			{
				result = Activator.CreateInstance(type);
			}
			return result;
		}

		// Token: 0x17001AE0 RID: 6880
		// (get) Token: 0x06005210 RID: 21008 RVA: 0x000FFAA8 File Offset: 0x000FDCA8
		public override IEnumerable<Type> SupportedTypes
		{
			get
			{
				return new Type[]
				{
					typeof(BorderStyle),
					typeof(Cell),
					typeof(Column),
					typeof(Filter),
					typeof(FilterColumn),
					typeof(Row),
					typeof(Sort),
					typeof(SortColumn),
					typeof(Validation),
					typeof(Worksheet)
				};
			}
		}
	}
}
