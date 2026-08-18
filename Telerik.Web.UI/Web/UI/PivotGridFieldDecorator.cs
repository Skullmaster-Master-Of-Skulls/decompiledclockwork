using System;

namespace Telerik.Web.UI
{
	// Token: 0x02000E23 RID: 3619
	public class PivotGridFieldDecorator
	{
		// Token: 0x17002B6D RID: 11117
		// (get) Token: 0x06008930 RID: 35120 RVA: 0x001F480B File Offset: 0x001F2A0B
		// (set) Token: 0x06008931 RID: 35121 RVA: 0x001F4813 File Offset: 0x001F2A13
		public PivotGridFieldRenderingControl RenderingControl { get; internal set; }

		// Token: 0x06008932 RID: 35122 RVA: 0x001F481C File Offset: 0x001F2A1C
		public PivotGridFieldDecorator(PivotGridFieldRenderingControl renderingControl)
		{
			this.RenderingControl = renderingControl;
		}

		// Token: 0x06008933 RID: 35123 RVA: 0x001F482C File Offset: 0x001F2A2C
		public void DecorateControl()
		{
			bool flag = this.RenderingControl.OwnerField.FlatChildOlapInfoNames.Count == 0;
			this.RenderingControl.CssClass = this.RenderingControl.OwnerField.Owner.FormatCssClass(this.RenderingControl.CssClass, (this.RenderingControl.ChildIndex == -1) ? "rpgFieldItem" : "rpgFieldItem rpgSubFieldItem");
			if (this.RenderingControl.SortIcon != null)
			{
				if (this.RenderingControl.OwnerField.SortOrder == PivotGridSortOrder.Ascending && flag)
				{
					this.RenderingControl.SortIcon.CssClass = "rpgSortAsc";
					this.RenderingControl.SortIcon.ToolTip = this.RenderingControl.OwnerField.Owner.Localization.SortIconAscTooltip;
					this.RenderingControl.SortIcon.Text = this.RenderingControl.OwnerField.Owner.Localization.SortIconAscText;
					this.RenderingControl.SortIcon.Visible = true;
					if (this.RenderingControl.OwnerField.Owner.EnableAriaSupport)
					{
						this.RenderingControl.SortIcon.Attributes.Add("aria-label", this.RenderingControl.SortIcon.ToolTip);
					}
				}
				else if (this.RenderingControl.OwnerField.SortOrder == PivotGridSortOrder.Descending && flag)
				{
					this.RenderingControl.SortIcon.CssClass = "rpgSortDesc";
					this.RenderingControl.SortIcon.ToolTip = this.RenderingControl.OwnerField.Owner.Localization.SortIconDescTooltip;
					this.RenderingControl.SortIcon.Text = this.RenderingControl.OwnerField.Owner.Localization.SortIconDescText;
					this.RenderingControl.SortIcon.Visible = true;
					if (this.RenderingControl.OwnerField.Owner.EnableAriaSupport)
					{
						this.RenderingControl.SortIcon.Attributes.Add("aria-label", this.RenderingControl.SortIcon.ToolTip);
					}
				}
				else
				{
					this.RenderingControl.SortIcon.Visible = false;
				}
			}
			if (this.RenderingControl.OwnerField.Owner.ResolvedRenderMode == RenderMode.Lightweight && this.RenderingControl.SortLinkIcon != null)
			{
				if (this.RenderingControl.OwnerField.SortOrder == PivotGridSortOrder.Ascending && flag)
				{
					this.RenderingControl.SortLinkIcon.CssClass = "rpgIcon rpgSortAscIcon";
					this.RenderingControl.SortLinkIcon.ToolTip = this.RenderingControl.OwnerField.Owner.Localization.SortIconAscTooltip;
					this.RenderingControl.SortLinkIcon.Visible = true;
					if (this.RenderingControl.OwnerField.Owner.EnableAriaSupport)
					{
						this.RenderingControl.SortLinkIcon.Attributes.Add("aria-label", this.RenderingControl.SortLinkIcon.ToolTip);
						return;
					}
				}
				else if (this.RenderingControl.OwnerField.SortOrder == PivotGridSortOrder.Descending && flag)
				{
					this.RenderingControl.SortLinkIcon.CssClass = "rpgIcon rpgSortDescIcon";
					this.RenderingControl.SortLinkIcon.ToolTip = this.RenderingControl.OwnerField.Owner.Localization.SortIconDescTooltip;
					this.RenderingControl.SortLinkIcon.Visible = true;
					if (this.RenderingControl.OwnerField.Owner.EnableAriaSupport)
					{
						this.RenderingControl.SortLinkIcon.Attributes.Add("aria-label", this.RenderingControl.SortLinkIcon.ToolTip);
						return;
					}
				}
				else
				{
					this.RenderingControl.SortLinkIcon.Visible = false;
				}
			}
		}
	}
}
