using System;
using System.Collections.Generic;
using System.Drawing;
using System.Web.Script.Serialization;

namespace Telerik.Web.UI
{
	// Token: 0x02000973 RID: 2419
	internal class TreeMapItemJavaScriptConverter : JavaScriptConverter
	{
		// Token: 0x06005BFB RID: 23547 RVA: 0x0011873B File Offset: 0x0011693B
		public override object Deserialize(IDictionary<string, object> dictionary, Type type, JavaScriptSerializer serializer)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06005BFC RID: 23548 RVA: 0x00118744 File Offset: 0x00116944
		public override IDictionary<string, object> Serialize(object obj, JavaScriptSerializer serializer)
		{
			TreeMapItem treeMapItem = (TreeMapItem)obj;
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			if (!string.IsNullOrEmpty(treeMapItem.Text))
			{
				dictionary.Add("text", treeMapItem.Text);
			}
			if (!string.IsNullOrEmpty(treeMapItem.Value))
			{
				dictionary.Add("value", treeMapItem.Value);
			}
			if (treeMapItem.Color != Color.Empty)
			{
				dictionary.Add("color", ColorTranslator.ToHtml(treeMapItem.Color));
			}
			if (treeMapItem.TemplateData != null)
			{
				foreach (KeyValuePair<string, string> keyValuePair in treeMapItem.TemplateData)
				{
					dictionary.Add(keyValuePair.Key, keyValuePair.Value);
				}
			}
			if (treeMapItem.Items.Count > 0)
			{
				dictionary.Add("items", treeMapItem.Items);
			}
			return dictionary;
		}

		// Token: 0x17001E50 RID: 7760
		// (get) Token: 0x06005BFD RID: 23549 RVA: 0x0011890C File Offset: 0x00116B0C
		public override IEnumerable<Type> SupportedTypes
		{
			get
			{
				yield return typeof(TreeMapItem);
				yield break;
			}
		}
	}
}
