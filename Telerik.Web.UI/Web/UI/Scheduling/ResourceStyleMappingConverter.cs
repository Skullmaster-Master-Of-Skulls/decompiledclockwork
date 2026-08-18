using System;
using System.Collections.Generic;
using System.Drawing;
using System.Web.Script.Serialization;

namespace Telerik.Web.UI.Scheduling
{
	// Token: 0x02001A2C RID: 6700
	internal class ResourceStyleMappingConverter : ExplicitJavaScriptConverter
	{
		// Token: 0x0601042F RID: 66607 RVA: 0x003A2688 File Offset: 0x003A0888
		public override IDictionary<string, object> Serialize(object obj, JavaScriptSerializer serializer)
		{
			ResourceStyleMapping resourceStyleMapping = obj as ResourceStyleMapping;
			if (resourceStyleMapping == null)
			{
				throw new ArgumentException("Can serialize only ResourceStyleMapping objects.");
			}
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			ExplicitJavaScriptConverter.AddProperty(dictionary, "key", resourceStyleMapping.Key, string.Empty);
			ExplicitJavaScriptConverter.AddProperty(dictionary, "text", resourceStyleMapping.Text, string.Empty);
			ExplicitJavaScriptConverter.AddProperty(dictionary, "type", resourceStyleMapping.Type, string.Empty);
			ExplicitJavaScriptConverter.AddProperty(dictionary, "applyCssClass", resourceStyleMapping.ApplyCssClass, string.Empty);
			if (resourceStyleMapping.BackColor != Color.Empty)
			{
				dictionary["backColor"] = ResourceStyleMappingConverter.FormatColor(resourceStyleMapping.BackColor);
			}
			if (resourceStyleMapping.BorderColor != Color.Empty)
			{
				dictionary["borderColor"] = ResourceStyleMappingConverter.FormatColor(resourceStyleMapping.BorderColor);
			}
			return dictionary;
		}

		// Token: 0x17004EC6 RID: 20166
		// (get) Token: 0x06010430 RID: 66608 RVA: 0x003A275C File Offset: 0x003A095C
		public override IEnumerable<Type> SupportedTypes
		{
			get
			{
				return new Type[]
				{
					typeof(ResourceStyleMapping)
				};
			}
		}

		// Token: 0x06010431 RID: 66609 RVA: 0x003A277E File Offset: 0x003A097E
		private static string FormatColor(Color c)
		{
			return string.Format("#{0:x2}{1:x2}{2:x2}", c.R, c.G, c.B);
		}
	}
}
