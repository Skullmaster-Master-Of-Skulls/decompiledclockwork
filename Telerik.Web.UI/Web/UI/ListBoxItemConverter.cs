using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;

namespace Telerik.Web.UI
{
	// Token: 0x0200193C RID: 6460
	internal class ListBoxItemConverter : JavaScriptConverter
	{
		// Token: 0x0600F9E3 RID: 63971 RVA: 0x003854C1 File Offset: 0x003836C1
		public override object Deserialize(IDictionary<string, object> dictionary, Type type, JavaScriptSerializer serializer)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600F9E4 RID: 63972 RVA: 0x003854C8 File Offset: 0x003836C8
		public override IDictionary<string, object> Serialize(object obj, JavaScriptSerializer serializer)
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			RadListBoxItem radListBoxItem = (RadListBoxItem)obj;
			if (!string.IsNullOrEmpty(radListBoxItem.Value))
			{
				dictionary.Add("value", radListBoxItem.Value);
			}
			if (radListBoxItem.Templated)
			{
				dictionary.Add("text", radListBoxItem.Text);
			}
			if (!radListBoxItem.Enabled)
			{
				dictionary.Add("enabled", false);
			}
			if (radListBoxItem.Selected)
			{
				dictionary.Add("selected", true);
			}
			if (!string.IsNullOrEmpty(radListBoxItem.ImageUrl))
			{
				dictionary.Add("imageUrl", radListBoxItem.ResolveClientUrl(radListBoxItem.ImageUrl));
			}
			if (!radListBoxItem.Checkable)
			{
				dictionary.Add("checkable", false);
			}
			if (radListBoxItem.Checked)
			{
				dictionary.Add("checked", true);
			}
			if (!radListBoxItem.AllowDrag)
			{
				dictionary.Add("allowDrag", false);
			}
			AttributeCollectionConverter attributeCollectionConverter = new AttributeCollectionConverter();
			IDictionary<string, object> dictionary2 = attributeCollectionConverter.Serialize(radListBoxItem.Attributes, serializer);
			if (dictionary2.Count > 0)
			{
				dictionary.Add("attributes", dictionary2);
			}
			return dictionary;
		}

		// Token: 0x17004B79 RID: 19321
		// (get) Token: 0x0600F9E5 RID: 63973 RVA: 0x003856B0 File Offset: 0x003838B0
		public override IEnumerable<Type> SupportedTypes
		{
			get
			{
				yield return typeof(RadListBoxItem);
				yield break;
			}
		}
	}
}
