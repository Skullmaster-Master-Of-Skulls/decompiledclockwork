using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Web.Script.Serialization;

namespace Telerik.Web.UI.Gantt
{
	// Token: 0x02000309 RID: 777
	internal class LocalizationConverter : JavaScriptConverter
	{
		// Token: 0x06001A62 RID: 6754 RVA: 0x00055F62 File Offset: 0x00054162
		public override object Deserialize(IDictionary<string, object> dictionary, Type type, JavaScriptSerializer serializer)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001A63 RID: 6755 RVA: 0x00055F6C File Offset: 0x0005416C
		public override IDictionary<string, object> Serialize(object obj, JavaScriptSerializer serializer)
		{
			GanttStrings ganttStrings = (GanttStrings)obj;
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			Dictionary<string, string> dictionary2 = new Dictionary<string, string>();
			Dictionary<string, string> dictionary3 = new Dictionary<string, string>();
			Dictionary<string, string> dictionary4 = new Dictionary<string, string>();
			foreach (PropertyInfo propertyInfo in ganttStrings.GetType().GetProperties())
			{
				string text = (string)propertyInfo.GetValue(ganttStrings, null);
				DefaultValueAttribute defaultValueAttribute = propertyInfo.GetCustomAttributes(typeof(DefaultValueAttribute), false).First<object>() as DefaultValueAttribute;
				ClientPropertyNameAttribute clientPropertyNameAttribute = propertyInfo.GetCustomAttributes(typeof(ClientPropertyNameAttribute), false).First<object>() as ClientPropertyNameAttribute;
				CategoryAttribute categoryAttribute = propertyInfo.GetCustomAttributes(typeof(CategoryAttribute), false).First<object>() as CategoryAttribute;
				if (!string.Equals(defaultValueAttribute.Value.ToString(), text, StringComparison.InvariantCulture))
				{
					if (categoryAttribute.Category == "Views")
					{
						dictionary2.Add(clientPropertyNameAttribute.PropertyName, text);
					}
					else if (categoryAttribute.Category == "Actions")
					{
						dictionary3.Add(clientPropertyNameAttribute.PropertyName, text);
					}
					else if (categoryAttribute.Category == "Editor")
					{
						dictionary4.Add(clientPropertyNameAttribute.PropertyName, text);
					}
					else
					{
						dictionary.Add(clientPropertyNameAttribute.PropertyName, text);
					}
				}
			}
			if (dictionary2.Count > 0)
			{
				dictionary.Add("views", dictionary2);
			}
			if (dictionary3.Count > 0)
			{
				dictionary.Add("actions", dictionary3);
			}
			if (dictionary4.Count > 0)
			{
				dictionary.Add("editor", dictionary4);
			}
			return dictionary;
		}

		// Token: 0x170008DD RID: 2269
		// (get) Token: 0x06001A64 RID: 6756 RVA: 0x00056104 File Offset: 0x00054304
		public override IEnumerable<Type> SupportedTypes
		{
			get
			{
				return new Type[]
				{
					typeof(GanttStrings)
				};
			}
		}
	}
}
