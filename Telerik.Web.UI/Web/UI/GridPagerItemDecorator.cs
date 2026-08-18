using System;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x02001122 RID: 4386
	internal class GridPagerItemDecorator : GridItemDecorator
	{
		// Token: 0x0600B349 RID: 45897 RVA: 0x00270C03 File Offset: 0x0026EE03
		public GridPagerItemDecorator(GridItem item) : base(item)
		{
		}

		// Token: 0x0600B34A RID: 45898 RVA: 0x00270C0C File Offset: 0x0026EE0C
		public override void SetItemVisibility(GridTableView owner, GridColumn[] columnArray)
		{
			if (!owner.RenderPagerStyle.Visible)
			{
				base.Item.Visible = false;
				return;
			}
			if (owner.PageCount <= 1)
			{
				if (!owner.RenderPagerStyle.AlwaysVisible && owner.PageSize < 2147483647)
				{
					base.Item.Visible = false;
				}
				this.SetItemStyle(owner);
				return;
			}
			base.Item.Visible = true;
			if (((GridPagerItem)base.Item).IsTopPager && !owner.RenderPagerStyle.IsPagerOnTop)
			{
				base.Item.Visible = false;
				return;
			}
			if (!((GridPagerItem)base.Item).IsTopPager && !owner.RenderPagerStyle.IsPagerOnBottom)
			{
				base.Item.Visible = false;
			}
		}

		// Token: 0x0600B34B RID: 45899 RVA: 0x00270CCD File Offset: 0x0026EECD
		public override void DecorateItem(GridTableView owner, GridColumn[] columnArray)
		{
			this.SetItemStyle(owner);
			if (base.Item != null && !base.Item.Display)
			{
				base.Item.Style["display"] = "none";
			}
		}

		// Token: 0x0600B34C RID: 45900 RVA: 0x00270D08 File Offset: 0x0026EF08
		public override void SetItemStyle(GridTableView owner)
		{
			base.Item.MergeStyle(owner.RenderPagerStyle);
			if (!base.Item.CssClass.Contains("rgPager"))
			{
				GridItem item = base.Item;
				item.CssClass += " rgPager";
			}
			ControlItemContainer controlItemContainer;
			if (owner.PagerStyle.PageSizeControlType == PagerDropDownControlType.RadDropDownList)
			{
				controlItemContainer = (RadDropDownList)base.Item.FindControl("PageSizeDropDownList");
			}
			else
			{
				controlItemContainer = (RadComboBox)base.Item.FindControl("PageSizeComboBox");
			}
			int num = (owner.PageSize == int.MaxValue) ? 3 : owner.PageSize.ToString().Length;
			if (owner.OwnerGrid.ResolvedRenderMode == RenderMode.Classic)
			{
				int num2 = 34;
				if (owner.OwnerGrid.RuntimeSkin == "MetroTouch" || owner.OwnerGrid.RuntimeSkin == "Glow" || owner.OwnerGrid.RuntimeSkin == "Silk" || owner.OwnerGrid.RuntimeSkin == "BlackMetroTouch" || owner.OwnerGrid.RuntimeSkin == "Bootstrap")
				{
					num2 = 55;
					if (!owner.IsDesignMode && owner.Page.Request.Browser.IsBrowser("IE") && owner.Page.Request.Browser.MajorVersion == 7)
					{
						num2 = 60;
					}
				}
				if (controlItemContainer != null)
				{
					controlItemContainer.Width = Unit.Pixel(num * 6 + num2);
					return;
				}
			}
			else if (controlItemContainer != null)
			{
				controlItemContainer.Width = new Unit((double)num * 2.1, UnitType.Em);
			}
		}
	}
}
