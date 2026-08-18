using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;

namespace Telerik.Web.UI
{
	// Token: 0x02000B20 RID: 2848
	internal class DropDownListItemConverter : JavaScriptConverter
	{
		// Token: 0x06006A6B RID: 27243 RVA: 0x0018EBB1 File Offset: 0x0018CDB1
		public override object Deserialize(IDictionary<string, object> dictionary, Type type, JavaScriptSerializer serializer)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06006A6C RID: 27244 RVA: 0x0018EBB8 File Offset: 0x0018CDB8
		public override IDictionary<string, object> Serialize(object obj, JavaScriptSerializer serializer)
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			DropDownListItem dropDownListItem = (DropDownListItem)obj;
			if (dropDownListItem.Templated)
			{
				dictionary.Add("text", dropDownListItem.Text);
			}
			if (!string.IsNullOrEmpty(dropDownListItem.Value))
			{
				dictionary.Add("value", dropDownListItem.Value);
			}
			if (dropDownListItem.Selected)
			{
				dictionary.Add("selected", true);
			}
			if (!dropDownListItem.Enabled)
			{
				dictionary.Add("enabled", false);
			}
			if (!string.IsNullOrEmpty(dropDownListItem.ImageUrl))
			{
				dictionary.Add("imageUrl", dropDownListItem.ResolveClientUrl(dropDownListItem.ImageUrl));
			}
			AttributeCollectionConverter attributeCollectionConverter = new AttributeCollectionConverter();
			IDictionary<string, object> dictionary2 = attributeCollectionConverter.Serialize(dropDownListItem.Attributes, serializer);
			if (dictionary2.Count > 0)
			{
				dictionary.Add("attributes", dictionary2);
			}
			return dictionary;
		}

		// Token: 0x170022D2 RID: 8914
		// (get) Token: 0x06006A6D RID: 27245 RVA: 0x0018ED58 File Offset: 0x0018CF58
		public override IEnumerable<Type> SupportedTypes
		{
			get
			{
				yield return typeof(DropDownListItem);
				yield break;
			}
		}
	}
}
