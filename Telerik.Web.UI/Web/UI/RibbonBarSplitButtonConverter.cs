using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;

namespace Telerik.Web.UI
{
	// Token: 0x02000F2B RID: 3883
	internal class RibbonBarSplitButtonConverter : JavaScriptConverter
	{
		// Token: 0x0600940D RID: 37901 RVA: 0x002132E0 File Offset: 0x002114E0
		public override object Deserialize(IDictionary<string, object> dictionary, Type type, JavaScriptSerializer serializer)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600940E RID: 37902 RVA: 0x002132E8 File Offset: 0x002114E8
		public override IDictionary<string, object> Serialize(object obj, JavaScriptSerializer serializer)
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			RibbonBarSplitButton ribbonBarSplitButton = (RibbonBarSplitButton)obj;
			if (this.ShouldSerializeImageUrl(ribbonBarSplitButton))
			{
				dictionary["imageUrl"] = ribbonBarSplitButton.ResolveUrl(ribbonBarSplitButton.ImageUrl);
			}
			if (this.ShouldSerializeImageUrlLarge(ribbonBarSplitButton))
			{
				dictionary["imageUrlLarge"] = ribbonBarSplitButton.ResolveUrl(ribbonBarSplitButton.ImageUrlLarge);
			}
			if (this.ShouldSerializeDisabledImageUrl(ribbonBarSplitButton))
			{
				dictionary["disabledImageUrl"] = ribbonBarSplitButton.ResolveUrl(ribbonBarSplitButton.DisabledImageUrl);
			}
			if (this.ShouldSerializeDisabledImageUrlLarge(ribbonBarSplitButton))
			{
				dictionary["disabledImageUrlLarge"] = ribbonBarSplitButton.ResolveUrl(ribbonBarSplitButton.DisabledImageUrlLarge);
			}
			if (this.ShouldSerializeText(ribbonBarSplitButton))
			{
				dictionary["text"] = ribbonBarSplitButton.Text;
			}
			if (this.ShouldSerializeEnableButtonSelection(ribbonBarSplitButton))
			{
				dictionary["enableButtonSelection"] = ribbonBarSplitButton.EnableButtonSelection;
			}
			if (this.ShouldSerializeSelectedButtonIndex(ribbonBarSplitButton))
			{
				dictionary["selectedButtonIndex"] = ribbonBarSplitButton.ResolvedSelectedButtonIndex;
			}
			IList<RibbonBarButton> visibleButtons = ribbonBarSplitButton.GetVisibleButtons();
			if (visibleButtons.Count > 0)
			{
				dictionary["buttonData"] = visibleButtons;
			}
			if (!string.IsNullOrEmpty(ribbonBarSplitButton.ToolTip))
			{
				dictionary["toolTip"] = ribbonBarSplitButton.ToolTip;
			}
			return dictionary;
		}

		// Token: 0x17002ED4 RID: 11988
		// (get) Token: 0x0600940F RID: 37903 RVA: 0x002134E4 File Offset: 0x002116E4
		public override IEnumerable<Type> SupportedTypes
		{
			get
			{
				yield return typeof(RibbonBarSplitButton);
				yield break;
			}
		}

		// Token: 0x06009410 RID: 37904 RVA: 0x00213501 File Offset: 0x00211701
		private bool ShouldSerializeImageUrl(RibbonBarSplitButton splitButton)
		{
			return !string.IsNullOrEmpty(splitButton.ImageUrl);
		}

		// Token: 0x06009411 RID: 37905 RVA: 0x00213511 File Offset: 0x00211711
		private bool ShouldSerializeImageUrlLarge(RibbonBarSplitButton splitButton)
		{
			return splitButton.Size == RibbonBarItemSize.Large && !string.IsNullOrEmpty(splitButton.ImageUrlLarge);
		}

		// Token: 0x06009412 RID: 37906 RVA: 0x0021352C File Offset: 0x0021172C
		private bool ShouldSerializeDisabledImageUrl(RibbonBarSplitButton splitButton)
		{
			return !string.IsNullOrEmpty(splitButton.DisabledImageUrl);
		}

		// Token: 0x06009413 RID: 37907 RVA: 0x0021353C File Offset: 0x0021173C
		private bool ShouldSerializeDisabledImageUrlLarge(RibbonBarSplitButton splitButton)
		{
			return splitButton.Size == RibbonBarItemSize.Large && !string.IsNullOrEmpty(splitButton.DisabledImageUrlLarge);
		}

		// Token: 0x06009414 RID: 37908 RVA: 0x00213557 File Offset: 0x00211757
		private bool ShouldSerializeText(RibbonBarSplitButton splitButton)
		{
			return !string.IsNullOrEmpty(splitButton.Text);
		}

		// Token: 0x06009415 RID: 37909 RVA: 0x00213567 File Offset: 0x00211767
		private bool ShouldSerializeEnableButtonSelection(RibbonBarSplitButton splitButton)
		{
			return splitButton.EnableButtonSelection;
		}

		// Token: 0x06009416 RID: 37910 RVA: 0x0021356F File Offset: 0x0021176F
		private bool ShouldSerializeSelectedButtonIndex(RibbonBarSplitButton splitButton)
		{
			return splitButton.IsValidButtonIndex(splitButton.ResolvedSelectedButtonIndex);
		}
	}
}
