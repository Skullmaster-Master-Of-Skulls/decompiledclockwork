using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;

namespace Telerik.Web.UI
{
	// Token: 0x02000E37 RID: 3639
	internal class RibbonBarDropDownConverter : JavaScriptConverter
	{
		// Token: 0x0600899C RID: 35228 RVA: 0x001F61E9 File Offset: 0x001F43E9
		public override object Deserialize(IDictionary<string, object> dictionary, Type type, JavaScriptSerializer serializer)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600899D RID: 35229 RVA: 0x001F61F0 File Offset: 0x001F43F0
		public override IDictionary<string, object> Serialize(object obj, JavaScriptSerializer serializer)
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			RibbonBarDropDown ribbonBarDropDown = (RibbonBarDropDown)obj;
			if (!string.IsNullOrEmpty(ribbonBarDropDown.ToolTip))
			{
				dictionary["toolTip"] = ribbonBarDropDown.ToolTip;
			}
			IList<RibbonBarListItem> visibleItems = ribbonBarDropDown.GetVisibleItems();
			if (visibleItems.Count > 0)
			{
				dictionary["dropDownItemData"] = visibleItems;
			}
			return dictionary;
		}

		// Token: 0x17002B8D RID: 11149
		// (get) Token: 0x0600899E RID: 35230 RVA: 0x001F6314 File Offset: 0x001F4514
		public override IEnumerable<Type> SupportedTypes
		{
			get
			{
				yield return typeof(RibbonBarDropDown);
				yield break;
			}
		}
	}
}
