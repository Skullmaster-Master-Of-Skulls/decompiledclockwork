using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x020004C3 RID: 1219
	internal class GridPagerButtonBuilder
	{
		// Token: 0x06002C39 RID: 11321 RVA: 0x00090EE4 File Offset: 0x0008F0E4
		public GridPagerButtonBuilder(GridPagerItem pagerItem)
		{
			this.PagerItem = pagerItem;
			this._pagingManager = pagerItem.OwnerTableView.ResolvedDataSource.Paging;
		}

		// Token: 0x17000E47 RID: 3655
		// (get) Token: 0x06002C3B RID: 11323 RVA: 0x00090F12 File Offset: 0x0008F112
		// (set) Token: 0x06002C3A RID: 11322 RVA: 0x00090F09 File Offset: 0x0008F109
		public GridPagerItem PagerItem { get; internal set; }

		// Token: 0x17000E48 RID: 3656
		// (get) Token: 0x06002C3C RID: 11324 RVA: 0x00090F1A File Offset: 0x0008F11A
		public RadGrid OwnerGrid
		{
			get
			{
				return this.OwnerTableView.OwnerGrid;
			}
		}

		// Token: 0x17000E49 RID: 3657
		// (get) Token: 0x06002C3D RID: 11325 RVA: 0x00090F27 File Offset: 0x0008F127
		public GridTableView OwnerTableView
		{
			get
			{
				return this.PagerItem.OwnerTableView;
			}
		}

		// Token: 0x17000E4A RID: 3658
		// (get) Token: 0x06002C3E RID: 11326 RVA: 0x00090F34 File Offset: 0x0008F134
		public GridPagingManager PagingManager
		{
			get
			{
				return this._pagingManager;
			}
		}

		// Token: 0x17000E4B RID: 3659
		// (get) Token: 0x06002C3F RID: 11327 RVA: 0x00090F3C File Offset: 0x0008F13C
		protected GridPagerStyle RenderPagerStyle
		{
			get
			{
				return this.OwnerTableView.RenderPagerStyle;
			}
		}

		// Token: 0x06002C40 RID: 11328 RVA: 0x00090F49 File Offset: 0x0008F149
		public void CreatePageSize(Panel container)
		{
			this.CreatePageSizeLabel(container);
			this.CreatePageSizeDropDown(container);
		}

		// Token: 0x06002C41 RID: 11329 RVA: 0x00090F5C File Offset: 0x0008F15C
		public void CreatePageSizeLabel(Panel container)
		{
			Label label = new Label();
			label.Text = this.OwnerTableView.RenderPagerStyle.PageSizeLabelText;
			if (this.OwnerGrid.EnableAriaSupport)
			{
				label.ToolTip = label.Text;
			}
			label.ID = "ChangePageSizeLabel";
			if (!this.OwnerGrid.EmptySkin())
			{
				label.CssClass = "rgPagerLabel";
			}
			container.Controls.Add(label);
		}

		// Token: 0x06002C42 RID: 11330 RVA: 0x00090FD0 File Offset: 0x0008F1D0
		public void CreatePageSizeDropDown(Panel container)
		{
			ControlItemContainer controlItemContainer;
			if (this.OwnerTableView.PagerStyle.PageSizeControlType == PagerDropDownControlType.RadComboBox)
			{
				controlItemContainer = new RadComboBox
				{
					ID = "PageSizeComboBox"
				};
			}
			else
			{
				controlItemContainer = new RadDropDownList
				{
					ID = "PageSizeDropDownList"
				};
			}
			controlItemContainer.RenderMode = this.OwnerGrid.RenderMode;
			container.Controls.Add(controlItemContainer);
			this.PrepareClientIDs(controlItemContainer);
			this.PrepareSkinnableControlProperties(controlItemContainer);
			IList<int> defaultPageSizes = new List<int>(this.OwnerTableView.GetPageSizes());
			if (!this.InitializePageSizeCombo(controlItemContainer as RadComboBox, defaultPageSizes))
			{
				this.InitializePageSizeDropDownList(controlItemContainer as RadDropDownList, defaultPageSizes);
			}
			if (this.OwnerGrid.EnableAriaSupport)
			{
				if (this.OwnerTableView.PagerStyle.PageSizeControlType == PagerDropDownControlType.RadComboBox)
				{
					controlItemContainer.ToolTip = (string.IsNullOrEmpty(this.RenderPagerStyle.ChangePageSizeComboBoxToolTip) ? "Change page size control" : this.RenderPagerStyle.ChangePageSizeComboBoxToolTip);
					return;
				}
				controlItemContainer.ToolTip = "Change page size control";
			}
		}

		// Token: 0x06002C43 RID: 11331 RVA: 0x000910C4 File Offset: 0x0008F2C4
		private bool InitializePageSizeCombo(RadComboBox pageSizeCombo, IList<int> defaultPageSizes)
		{
			bool result = false;
			if (pageSizeCombo != null)
			{
				bool flag = this.OwnerTableView.PagerStyle.EnableSEOPaging || this.OwnerGrid.PagerStyle.EnableSEOPaging;
				if (string.IsNullOrEmpty(this.RenderPagerStyle.ChangePageSizeComboBoxTableSummary))
				{
					pageSizeCombo.TableSummary = "Page Size Drop Down Control";
				}
				else
				{
					pageSizeCombo.TableSummary = this.RenderPagerStyle.ChangePageSizeComboBoxTableSummary;
				}
				pageSizeCombo.InputTitle = this.RenderPagerStyle.ChangePageSizeComboBoxToolTip;
				if (string.IsNullOrEmpty(pageSizeCombo.InputTitle) && this.OwnerGrid.EnableAriaSupport)
				{
					pageSizeCombo.InputTitle = (string.IsNullOrEmpty(this.RenderPagerStyle.ChangePageSizeComboBoxToolTip) ? "Page size drop-down list" : this.RenderPagerStyle.ChangePageSizeComboBoxToolTip);
				}
				pageSizeCombo.TableCaption = "PageSizeComboBox";
				if (!string.IsNullOrEmpty(this.RenderPagerStyle.ChangePageSizeComboBoxTableSummary) && !string.IsNullOrEmpty(this.RenderPagerStyle.ChangePageSizeComboBoxToolTip))
				{
					pageSizeCombo.EnableTableHeaders = true;
				}
				pageSizeCombo.EnableAriaSupport = this.OwnerGrid.EnableAriaSupport;
				pageSizeCombo.ClearSelection();
				if (!defaultPageSizes.Contains(this.PagingManager.PageSize) || this.OwnerTableView.CustomPageSize != null)
				{
					if (this.OwnerTableView.CustomPageSize == null)
					{
						this.OwnerTableView.CustomPageSize = new int?(this.PagingManager.PageSize);
					}
					int? customPageSize = this.OwnerTableView.CustomPageSize;
					RadComboBoxItem radComboBoxItem = new RadComboBoxItem(customPageSize.ToString(), customPageSize.ToString());
					radComboBoxItem.Attributes.Add("ownerTableViewId", this.OwnerTableView.ClientID);
					if (flag)
					{
						int num = this.OwnerGrid.GetSEOPageSizeFromUrl();
						if (num < 0 || defaultPageSizes.Contains(num))
						{
							num = customPageSize.Value;
							if (num < 0)
							{
								num = this.OwnerGrid._defaultPageSize;
							}
						}
						if (!defaultPageSizes.Contains(num))
						{
							radComboBoxItem.Attributes.Add("seoRedirectUrl", this.PagerItem.GeneratePageSizeAttributeLink(num, false));
							radComboBoxItem.Text = (radComboBoxItem.Value = num.ToString());
							pageSizeCombo.Items.Add(radComboBoxItem);
						}
					}
					else
					{
						pageSizeCombo.Items.Add(radComboBoxItem);
					}
				}
				foreach (int num2 in defaultPageSizes)
				{
					RadComboBoxItem radComboBoxItem2 = new RadComboBoxItem((num2 == int.MaxValue) ? "All" : num2.ToString(), num2.ToString());
					radComboBoxItem2.Attributes.Add("ownerTableViewId", this.OwnerTableView.ClientID);
					if (flag && !this.OwnerTableView.IsDesignMode)
					{
						radComboBoxItem2.Attributes.Add("seoRedirectUrl", this.PagerItem.GeneratePageSizeAttributeLink(num2, false));
					}
					pageSizeCombo.Items.Add(radComboBoxItem2);
				}
				pageSizeCombo.Items.Sort(new PageSizeItemsComparer());
				RadComboBoxItem radComboBoxItem3 = pageSizeCombo.Items.FindItemByValue(this.PagingManager.PageSize.ToString());
				if (radComboBoxItem3 != null)
				{
					radComboBoxItem3.Selected = true;
				}
				pageSizeCombo.OnClientSelectedIndexChanging = "Telerik.Web.UI.Grid.ChangingPageSizeComboHandler";
				pageSizeCombo.OnClientSelectedIndexChanged = "Telerik.Web.UI.Grid.ChangePageSizeComboHandler";
				result = true;
			}
			return result;
		}

		// Token: 0x06002C44 RID: 11332 RVA: 0x00091420 File Offset: 0x0008F620
		private void InitializePageSizeDropDownList(RadDropDownList ddl, IList<int> defaultPageSizes)
		{
			if (ddl != null)
			{
				bool flag = this.OwnerTableView.PagerStyle.EnableSEOPaging || this.OwnerGrid.PagerStyle.EnableSEOPaging;
				ddl.ClearSelection();
				ddl.EnableAriaSupport = this.OwnerGrid.EnableAriaSupport;
				if (!defaultPageSizes.Contains(this.PagingManager.PageSize) || this.OwnerTableView.CustomPageSize != null)
				{
					if (this.OwnerTableView.CustomPageSize == null)
					{
						this.OwnerTableView.CustomPageSize = new int?(this.PagingManager.PageSize);
					}
					int? customPageSize = this.OwnerTableView.CustomPageSize;
					DropDownListItem dropDownListItem = new DropDownListItem(customPageSize.ToString(), customPageSize.ToString());
					dropDownListItem.Attributes.Add("ownerTableViewId", this.OwnerTableView.ClientID);
					if (flag)
					{
						int num = this.OwnerGrid.GetSEOPageSizeFromUrl();
						if (num < 0 || defaultPageSizes.Contains(num))
						{
							num = customPageSize.Value;
							if (num < 0)
							{
								num = this.OwnerGrid._defaultPageSize;
							}
						}
						if (!defaultPageSizes.Contains(num))
						{
							dropDownListItem.Attributes.Add("seoRedirectUrl", this.PagerItem.GeneratePageSizeAttributeLink(num, false));
							dropDownListItem.Text = (dropDownListItem.Value = num.ToString());
							ddl.Items.Add(dropDownListItem);
						}
					}
					else
					{
						ddl.Items.Add(dropDownListItem);
					}
				}
				foreach (int num2 in defaultPageSizes)
				{
					DropDownListItem dropDownListItem2 = new DropDownListItem((num2 == int.MaxValue) ? "All" : num2.ToString(), num2.ToString());
					dropDownListItem2.Attributes.Add("ownerTableViewId", this.OwnerTableView.ClientID);
					if (flag && !this.OwnerTableView.IsDesignMode)
					{
						dropDownListItem2.Attributes.Add("seoRedirectUrl", this.PagerItem.GeneratePageSizeAttributeLink(num2, false));
					}
					ddl.Items.Add(dropDownListItem2);
				}
				ddl.Items.Sort(new PageSizeItemsComparer());
				DropDownListItem dropDownListItem3 = ddl.FindChildByValue<DropDownListItem>(this.PagingManager.PageSize.ToString());
				if (dropDownListItem3 != null)
				{
					dropDownListItem3.Selected = true;
				}
				ddl.OnClientSelectedIndexChanged = "Telerik.Web.UI.Grid.ChangePageSizeComboHandler";
			}
		}

		// Token: 0x06002C45 RID: 11333 RVA: 0x000916C8 File Offset: 0x0008F8C8
		private void PrepareSkinnableControlProperties(ISkinnableControl control)
		{
			(control as Control).PreRender += delegate(object sender, EventArgs args)
			{
				control.Skin = this.OwnerGrid.RuntimeSkin;
			};
			control.EnableEmbeddedSkins = this.OwnerGrid.EnableEmbeddedSkins;
			control.EnableEmbeddedScripts = this.OwnerGrid.EnableEmbeddedScripts;
			control.EnableEmbeddedBaseStylesheet = this.OwnerGrid.EnableEmbeddedBaseStylesheet;
			control.RegisterWithScriptManager = this.OwnerGrid.RegisterWithScriptManager;
		}

		// Token: 0x06002C46 RID: 11334 RVA: 0x0009175D File Offset: 0x0008F95D
		private void PrepareClientIDs(ControlItemContainer ddlControl)
		{
			if (this.OwnerGrid.IsClientCommandAssigned)
			{
				if (this.PagerItem.IsTopPager)
				{
					this.OwnerTableView.changePageSizeComboBoxTopClientID = ddlControl.ClientID;
					return;
				}
				this.OwnerTableView.changePageSizeComboBoxClientID = ddlControl.ClientID;
			}
		}

		// Token: 0x06002C47 RID: 11335 RVA: 0x000917B4 File Offset: 0x0008F9B4
		private void InitalizePageSizeDropDownList(RadDropDownList ddl, IList<int> defaultPageSizes)
		{
			if (ddl != null)
			{
				ddl.ClearSelection();
				foreach (int num in defaultPageSizes)
				{
					DropDownListItem item = new DropDownListItem(num.ToString(), num.ToString());
					ddl.Items.Add(item);
				}
				ddl.Items.Sort(new PageSizeItemsComparer());
				DropDownListItem dropDownListItem = ddl.FindChildByValue<DropDownListItem>(this.OwnerGrid.PageSize.ToString());
				if (dropDownListItem != null)
				{
					dropDownListItem.Selected = true;
				}
				ddl.AutoPostBack = true;
				ddl.SelectedIndexChanged += delegate(object sender, DropDownListEventArgs args)
				{
					this.OwnerGrid.PageSize = int.Parse(args.Value);
				};
			}
		}

		// Token: 0x04000B6D RID: 2925
		private GridPagingManager _pagingManager;
	}
}
