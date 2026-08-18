using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;

namespace Telerik.Web.UI.Scheduling
{
	// Token: 0x02001A2B RID: 6699
	internal class ResourceTypeConverter : ExplicitJavaScriptConverter
	{
		// Token: 0x0601042C RID: 66604 RVA: 0x003A2600 File Offset: 0x003A0800
		public override IDictionary<string, object> Serialize(object obj, JavaScriptSerializer serializer)
		{
			ResourceType resourceType = obj as ResourceType;
			if (resourceType == null)
			{
				throw new ArgumentException("Can serialize only ResourceType objects.");
			}
			IDictionary<string, object> dictionary = new Dictionary<string, object>();
			dictionary["name"] = resourceType.Name;
			ExplicitJavaScriptConverter.AddProperty(dictionary, "allowMultipleValues", resourceType.AllowMultipleValues, false);
			return dictionary;
		}

		// Token: 0x17004EC5 RID: 20165
		// (get) Token: 0x0601042D RID: 66605 RVA: 0x003A265C File Offset: 0x003A085C
		public override IEnumerable<Type> SupportedTypes
		{
			get
			{
				return new Type[]
				{
					typeof(ResourceType)
				};
			}
		}
	}
}
