using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;

namespace Telerik.Web.UI
{
	// Token: 0x02000E36 RID: 3638
	internal class RibbonBarListItemConverter : JavaScriptConverter
	{
		// Token: 0x06008998 RID: 35224 RVA: 0x001F609D File Offset: 0x001F429D
		public override object Deserialize(IDictionary<string, object> dictionary, Type type, JavaScriptSerializer serializer)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06008999 RID: 35225 RVA: 0x001F60A4 File Offset: 0x001F42A4
		public override IDictionary<string, object> Serialize(object obj, JavaScriptSerializer serializer)
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			RibbonBarListItem ribbonBarListItem = obj as RibbonBarListItem;
			if (!string.IsNullOrEmpty(ribbonBarListItem.Text))
			{
				dictionary["text"] = ribbonBarListItem.Text;
			}
			if (ribbonBarListItem.Selected)
			{
				dictionary["selected"] = true;
			}
			return dictionary;
		}

		// Token: 0x17002B8C RID: 11148
		// (get) Token: 0x0600899A RID: 35226 RVA: 0x001F61C4 File Offset: 0x001F43C4
		public override IEnumerable<Type> SupportedTypes
		{
			get
			{
				yield return typeof(RibbonBarListItem);
				yield break;
			}
		}
	}
}
