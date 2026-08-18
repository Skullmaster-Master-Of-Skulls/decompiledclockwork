using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;

namespace Telerik.Web.UI
{
	// Token: 0x02000F2A RID: 3882
	internal class RibbonBarMenuConverter : JavaScriptConverter
	{
		// Token: 0x06009405 RID: 37893 RVA: 0x002130B5 File Offset: 0x002112B5
		public override object Deserialize(IDictionary<string, object> dictionary, Type type, JavaScriptSerializer serializer)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06009406 RID: 37894 RVA: 0x002130BC File Offset: 0x002112BC
		public override IDictionary<string, object> Serialize(object obj, JavaScriptSerializer serializer)
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			RibbonBarMenu ribbonBarMenu = (RibbonBarMenu)obj;
			if (this.ShouldSerializeImageUrl(ribbonBarMenu))
			{
				dictionary["imageUrl"] = ribbonBarMenu.ResolveUrl(ribbonBarMenu.ImageUrl);
			}
			if (this.ShouldSerializeImageUrlLarge(ribbonBarMenu))
			{
				dictionary["imageUrlLarge"] = ribbonBarMenu.ResolveUrl(ribbonBarMenu.ImageUrlLarge);
			}
			if (this.ShouldSerializeDisabledImageUrl(ribbonBarMenu))
			{
				dictionary["disabledImageUrl"] = ribbonBarMenu.ResolveUrl(ribbonBarMenu.DisabledImageUrl);
			}
			if (this.ShouldSerializeDisabledImageUrlLarge(ribbonBarMenu))
			{
				dictionary["disabledImageUrlLarge"] = ribbonBarMenu.ResolveUrl(ribbonBarMenu.DisabledImageUrlLarge);
			}
			IList<RibbonBarMenuItem> visibleItems = ribbonBarMenu.GetVisibleItems();
			if (visibleItems.Count > 0)
			{
				dictionary["menuItemData"] = visibleItems;
			}
			if (!string.IsNullOrEmpty(ribbonBarMenu.ToolTip))
			{
				dictionary["toolTip"] = ribbonBarMenu.ToolTip;
			}
			return dictionary;
		}

		// Token: 0x17002ED3 RID: 11987
		// (get) Token: 0x06009407 RID: 37895 RVA: 0x00213260 File Offset: 0x00211460
		public override IEnumerable<Type> SupportedTypes
		{
			get
			{
				yield return typeof(RibbonBarMenu);
				yield break;
			}
		}

		// Token: 0x06009408 RID: 37896 RVA: 0x0021327D File Offset: 0x0021147D
		private bool ShouldSerializeImageUrl(RibbonBarMenu menu)
		{
			return !string.IsNullOrEmpty(menu.ImageUrl);
		}

		// Token: 0x06009409 RID: 37897 RVA: 0x0021328D File Offset: 0x0021148D
		private bool ShouldSerializeImageUrlLarge(RibbonBarMenu menu)
		{
			return menu.Size == RibbonBarItemSize.Large && !string.IsNullOrEmpty(menu.ImageUrlLarge);
		}

		// Token: 0x0600940A RID: 37898 RVA: 0x002132A8 File Offset: 0x002114A8
		private bool ShouldSerializeDisabledImageUrl(RibbonBarMenu menu)
		{
			return !string.IsNullOrEmpty(menu.DisabledImageUrl);
		}

		// Token: 0x0600940B RID: 37899 RVA: 0x002132B8 File Offset: 0x002114B8
		private bool ShouldSerializeDisabledImageUrlLarge(RibbonBarMenu menu)
		{
			return menu.Size == RibbonBarItemSize.Large && !string.IsNullOrEmpty(menu.DisabledImageUrlLarge) && menu.Enabled;
		}
	}
}
