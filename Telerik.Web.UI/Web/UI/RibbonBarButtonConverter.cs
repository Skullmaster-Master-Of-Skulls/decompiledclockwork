using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;

namespace Telerik.Web.UI
{
	// Token: 0x02000F2C RID: 3884
	internal class RibbonBarButtonConverter : JavaScriptConverter
	{
		// Token: 0x06009418 RID: 37912 RVA: 0x00213585 File Offset: 0x00211785
		public override object Deserialize(IDictionary<string, object> dictionary, Type type, JavaScriptSerializer serializer)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06009419 RID: 37913 RVA: 0x0021358C File Offset: 0x0021178C
		public override IDictionary<string, object> Serialize(object obj, JavaScriptSerializer serializer)
		{
			RibbonBarButton ribbonBarButton = obj as RibbonBarButton;
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			if (!string.IsNullOrEmpty(ribbonBarButton.Value))
			{
				dictionary["value"] = ribbonBarButton.Value;
			}
			if (!string.IsNullOrEmpty(ribbonBarButton.Text) && ribbonBarButton.Size == RibbonBarItemSize.Small)
			{
				dictionary["text"] = ribbonBarButton.Text;
			}
			if (!string.IsNullOrEmpty(ribbonBarButton.CommandName))
			{
				dictionary["commandName"] = ribbonBarButton.CommandName;
			}
			if (!string.IsNullOrEmpty(ribbonBarButton.CommandArgument))
			{
				dictionary["commandArgument"] = ribbonBarButton.CommandArgument;
			}
			if (this.ShouldSerializeImageUrl(ribbonBarButton))
			{
				dictionary["imageUrl"] = ribbonBarButton.ResolveUrl(ribbonBarButton.ImageUrl);
			}
			if (this.ShouldSerializeImageUrlLarge(ribbonBarButton))
			{
				dictionary["imageUrlLarge"] = ribbonBarButton.ResolveUrl(ribbonBarButton.ImageUrlLarge);
			}
			if (this.ShouldSerializeDisabledImageUrl(ribbonBarButton))
			{
				dictionary["disabledImageUrl"] = ribbonBarButton.ResolveUrl(ribbonBarButton.DisabledImageUrl);
			}
			if (this.ShouldSerializeDisabledImageUrlLarge(ribbonBarButton))
			{
				dictionary["disabledImageUrlLarge"] = ribbonBarButton.ResolveUrl(ribbonBarButton.DisabledImageUrlLarge);
			}
			if (!string.IsNullOrEmpty(ribbonBarButton.ToolTip))
			{
				dictionary["toolTip"] = ribbonBarButton.ToolTip;
			}
			return dictionary;
		}

		// Token: 0x17002ED5 RID: 11989
		// (get) Token: 0x0600941A RID: 37914 RVA: 0x00213794 File Offset: 0x00211994
		public override IEnumerable<Type> SupportedTypes
		{
			get
			{
				yield return typeof(RibbonBarButton);
				yield break;
			}
		}

		// Token: 0x0600941B RID: 37915 RVA: 0x002137B1 File Offset: 0x002119B1
		private bool ShouldSerializeImageUrl(RibbonBarButton button)
		{
			return !string.IsNullOrEmpty(button.ImageUrl);
		}

		// Token: 0x0600941C RID: 37916 RVA: 0x002137C1 File Offset: 0x002119C1
		private bool ShouldSerializeImageUrlLarge(RibbonBarButton button)
		{
			return !string.IsNullOrEmpty(button.ImageUrlLarge);
		}

		// Token: 0x0600941D RID: 37917 RVA: 0x002137D1 File Offset: 0x002119D1
		private bool ShouldSerializeDisabledImageUrl(RibbonBarButton button)
		{
			return !string.IsNullOrEmpty(button.DisabledImageUrl);
		}

		// Token: 0x0600941E RID: 37918 RVA: 0x002137E1 File Offset: 0x002119E1
		private bool ShouldSerializeDisabledImageUrlLarge(RibbonBarButton button)
		{
			return !string.IsNullOrEmpty(button.DisabledImageUrlLarge);
		}
	}
}
