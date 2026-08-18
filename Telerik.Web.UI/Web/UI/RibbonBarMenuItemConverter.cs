using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;

namespace Telerik.Web.UI
{
	// Token: 0x02000EC3 RID: 3779
	internal class RibbonBarMenuItemConverter : JavaScriptConverter
	{
		// Token: 0x0600903C RID: 36924 RVA: 0x002076BC File Offset: 0x002058BC
		public override object Deserialize(IDictionary<string, object> dictionary, Type type, JavaScriptSerializer serializer)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600903D RID: 36925 RVA: 0x002076C4 File Offset: 0x002058C4
		public override IDictionary<string, object> Serialize(object obj, JavaScriptSerializer serializer)
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			RibbonBarMenuItem ribbonBarMenuItem = obj as RibbonBarMenuItem;
			if (!string.IsNullOrEmpty(ribbonBarMenuItem.Value))
			{
				dictionary["value"] = ribbonBarMenuItem.Value;
			}
			IList<RibbonBarMenuItem> visibleItems = ribbonBarMenuItem.GetVisibleItems();
			if (visibleItems.Count > 0)
			{
				dictionary["menuItemData"] = visibleItems;
			}
			if (this.ShouldSerializeImageUrl(ribbonBarMenuItem))
			{
				dictionary["imageUrl"] = ribbonBarMenuItem.ResolveUrl(ribbonBarMenuItem.ImageUrl);
			}
			if (this.ShouldSerializeDisabledImageUrl(ribbonBarMenuItem))
			{
				dictionary["disabledImageUrl"] = ribbonBarMenuItem.ResolveUrl(ribbonBarMenuItem.DisabledImageUrl);
			}
			if (!string.IsNullOrEmpty(ribbonBarMenuItem.CommandName))
			{
				dictionary["commandName"] = ribbonBarMenuItem.CommandName;
			}
			if (!string.IsNullOrEmpty(ribbonBarMenuItem.CommandArgument))
			{
				dictionary["commandArgument"] = ribbonBarMenuItem.CommandArgument;
			}
			if (!string.IsNullOrEmpty(ribbonBarMenuItem.ToolTip))
			{
				dictionary["toolTip"] = ribbonBarMenuItem.ToolTip;
			}
			return dictionary;
		}

		// Token: 0x17002DAF RID: 11695
		// (get) Token: 0x0600903E RID: 36926 RVA: 0x00207880 File Offset: 0x00205A80
		public override IEnumerable<Type> SupportedTypes
		{
			get
			{
				yield return typeof(RibbonBarMenuItem);
				yield break;
			}
		}

		// Token: 0x0600903F RID: 36927 RVA: 0x0020789D File Offset: 0x00205A9D
		private bool ShouldSerializeImageUrl(RibbonBarMenuItem menuItem)
		{
			return !string.IsNullOrEmpty(menuItem.ImageUrl);
		}

		// Token: 0x06009040 RID: 36928 RVA: 0x002078AD File Offset: 0x00205AAD
		private bool ShouldSerializeDisabledImageUrl(RibbonBarMenuItem menuItem)
		{
			return !string.IsNullOrEmpty(menuItem.DisabledImageUrl);
		}
	}
}
