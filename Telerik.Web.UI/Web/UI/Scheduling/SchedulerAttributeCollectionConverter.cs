using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;
using System.Web.UI;

namespace Telerik.Web.UI.Scheduling
{
	// Token: 0x02001B44 RID: 6980
	internal class SchedulerAttributeCollectionConverter : JavaScriptConverter
	{
		// Token: 0x06010E19 RID: 69145 RVA: 0x003BE277 File Offset: 0x003BC477
		public override object Deserialize(IDictionary<string, object> dictionary, Type type, JavaScriptSerializer serializer)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06010E1A RID: 69146 RVA: 0x003BE280 File Offset: 0x003BC480
		public override IDictionary<string, object> Serialize(object obj, JavaScriptSerializer serializer)
		{
			AttributeCollection attributeCollection = obj as AttributeCollection;
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			foreach (object obj2 in attributeCollection.Keys)
			{
				string text = (string)obj2;
				string value = attributeCollection[text];
				if (!HtmlAttributes.IsHtmlAttribute(text) && !string.IsNullOrEmpty(value))
				{
					dictionary.Add(text, value);
				}
			}
			return dictionary;
		}

		// Token: 0x17005257 RID: 21079
		// (get) Token: 0x06010E1B RID: 69147 RVA: 0x003BE3D4 File Offset: 0x003BC5D4
		public override IEnumerable<Type> SupportedTypes
		{
			get
			{
				yield return typeof(AttributeCollection);
				yield break;
			}
		}
	}
}
