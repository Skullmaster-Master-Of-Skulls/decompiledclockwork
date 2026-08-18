using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;

namespace Telerik.Web.UI.Gantt
{
	// Token: 0x020002FF RID: 767
	internal class CustomFieldConverter : JavaScriptConverter
	{
		// Token: 0x06001A39 RID: 6713 RVA: 0x00055402 File Offset: 0x00053602
		public override object Deserialize(IDictionary<string, object> dictionary, Type type, JavaScriptSerializer serializer)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001A3A RID: 6714 RVA: 0x0005540C File Offset: 0x0005360C
		public override IDictionary<string, object> Serialize(object obj, JavaScriptSerializer serializer)
		{
			GanttCustomField ganttCustomField = (GanttCustomField)obj;
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			dictionary["propertyName"] = ganttCustomField.PropertyName;
			dictionary["clientPropertyName"] = ganttCustomField.ClientPropertyName;
			dictionary["defaultValue"] = ganttCustomField.DefaultValue;
			dictionary["type"] = ganttCustomField.Type;
			return dictionary;
		}

		// Token: 0x170008D3 RID: 2259
		// (get) Token: 0x06001A3B RID: 6715 RVA: 0x00055470 File Offset: 0x00053670
		public override IEnumerable<Type> SupportedTypes
		{
			get
			{
				return new Type[]
				{
					typeof(GanttCustomField)
				};
			}
		}
	}
}
