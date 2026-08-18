using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x0200125A RID: 4698
	public class TreeListPagerItem : TreeListItem
	{
		// Token: 0x0600C197 RID: 49559 RVA: 0x002B3B6A File Offset: 0x002B1D6A
		public TreeListPagerItem(RadTreeList ownerTreeList, TreeListItemType itemType, bool isDataBinding) : base(ownerTreeList, itemType, isDataBinding)
		{
		}

		// Token: 0x17003E63 RID: 15971
		// (get) Token: 0x0600C198 RID: 49560 RVA: 0x002B3B75 File Offset: 0x002B1D75
		// (set) Token: 0x0600C199 RID: 49561 RVA: 0x002B3B82 File Offset: 0x002B1D82
		public override TableRowSection TableSection
		{
			get
			{
				if (!this.IsTopItem)
				{
					return TableRowSection.TableFooter;
				}
				return TableRowSection.TableHeader;
			}
			set
			{
			}
		}

		// Token: 0x0600C19A RID: 49562 RVA: 0x002B3B84 File Offset: 0x002B1D84
		public override void Initialize(IList<TreeListColumn> columns)
		{
			this.PagerContentCell = this.CreateCellObject();
			this.Cells.Add(this.PagerContentCell);
			if (base.OwnerTreeList.PagerTemplate == null)
			{
				this.InitializePagerItem();
			}
			else
			{
				base.OwnerTreeList.PagerTemplate.InstantiateIn(this.PagerContentCell);
			}
			this.CallOnItemCreated();
			if (this.IsDataBinding)
			{
				this.DataBind();
				this.CallOnItemDataBound();
			}
		}

		// Token: 0x17003E64 RID: 15972
		// (get) Token: 0x0600C19B RID: 49563 RVA: 0x002B3BF4 File Offset: 0x002B1DF4
		internal override bool IsExportable
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600C19C RID: 49564 RVA: 0x002B3BF8 File Offset: 0x002B1DF8
		private void InitializePagerItem()
		{
			ControlCollection controls;
			if (base.OwnerTreeList.ResolvedRenderMode == RenderMode.Mobile)
			{
				Panel child = new Panel();
				this.PagerContentCell.Controls.Add(child);
				controls = this.PagerContentCell.Controls[0].Controls;
			}
			else
			{
				controls = this.PagerContentCell.Controls;
			}
			switch (base.OwnerTreeList.PagerStyle.Mode)
			{
			case TreeListPagerMode.NextPrev:
			{
				Control control = this.Builder.BuildContainer("rtlArrPart1");
				control.Controls.Add(this.Builder.CreateFirstButton());
				control.Controls.Add(this.Builder.CreatePrevButton());
				control.Controls.Add(this.Builder.CreateNextButton());
				control.Controls.Add(this.Builder.CreateLastButton());
				controls.Add(control);
				return;
			}
			case TreeListPagerMode.NumericPages:
				controls.Add(this.NumericPager);
				return;
			case TreeListPagerMode.NextPrevAndNumeric:
				this.CreateNextPrevAndNumeric(controls);
				if (base.OwnerTreeList.PagerStyle.PageSizeControlType != PagerDropDownControlType.None)
				{
					controls.Add(this.Builder.CreatePageSize());
					return;
				}
				break;
			case TreeListPagerMode.NextPrevNumericAndAdvanced:
				this.CreateNextPrevAndNumeric(controls);
				controls.Add(this.Builder.CreateAdvancedPager());
				return;
			case TreeListPagerMode.Advanced:
				controls.Add(this.Builder.CreateAdvancedPager());
				return;
			case TreeListPagerMode.Slider:
				controls.Add(this.Builder.CreateSliderPager());
				break;
			default:
				return;
			}
		}

		// Token: 0x0600C19D RID: 49565 RVA: 0x002B3D68 File Offset: 0x002B1F68
		private void CreateNextPrevAndNumeric(ControlCollection pagerContainer)
		{
			Control control = this.Builder.BuildContainer("rtlArrPart1");
			control.Controls.Add(this.Builder.CreateFirstButton());
			control.Controls.Add(this.Builder.CreatePrevButton());
			pagerContainer.Add(control);
			pagerContainer.Add(this.NumericPager);
			control = this.Builder.BuildContainer("rtlArrPart2");
			control.Controls.Add(this.Builder.CreateNextButton());
			control.Controls.Add(this.Builder.CreateLastButton());
			pagerContainer.Add(control);
		}

		// Token: 0x17003E65 RID: 15973
		// (get) Token: 0x0600C19E RID: 49566 RVA: 0x002B3E09 File Offset: 0x002B2009
		// (set) Token: 0x0600C19F RID: 49567 RVA: 0x002B3E11 File Offset: 0x002B2011
		public TableCell PagerContentCell { get; protected set; }

		// Token: 0x17003E66 RID: 15974
		// (get) Token: 0x0600C1A0 RID: 49568 RVA: 0x002B3E1A File Offset: 0x002B201A
		public TreeListPagingManager Paging
		{
			get
			{
				return base.OwnerTreeList.ResolvedDataSource.PagingManager;
			}
		}

		// Token: 0x0600C1A1 RID: 49569 RVA: 0x002B3E2C File Offset: 0x002B202C
		public override void PrepareItemStyle()
		{
			base.PrepareItemStyle();
			if (this.IsTopItem)
			{
				this.CssClass = string.Format("{0} {1}", this.CssClass, "rtlPagerTop");
			}
		}

		// Token: 0x17003E67 RID: 15975
		// (get) Token: 0x0600C1A2 RID: 49570 RVA: 0x002B3E57 File Offset: 0x002B2057
		// (set) Token: 0x0600C1A3 RID: 49571 RVA: 0x002B3E5F File Offset: 0x002B205F
		public bool IsTopItem { get; internal set; }

		// Token: 0x17003E68 RID: 15976
		// (get) Token: 0x0600C1A4 RID: 49572 RVA: 0x002B3E68 File Offset: 0x002B2068
		internal TreeListPagerButtonBuilder Builder
		{
			get
			{
				if (this._builder == null)
				{
					this._builder = new TreeListPagerButtonBuilder(this);
				}
				return this._builder;
			}
		}

		// Token: 0x17003E69 RID: 15977
		// (get) Token: 0x0600C1A5 RID: 49573 RVA: 0x002B3E84 File Offset: 0x002B2084
		public Control NumericPager
		{
			get
			{
				return this.Builder.CreateNumericPager();
			}
		}

		// Token: 0x0600C1A6 RID: 49574 RVA: 0x002B3E94 File Offset: 0x002B2094
		public Control GetButtonForArgument(string commandArgument)
		{
			if (commandArgument != null)
			{
				Control result;
				if (!(commandArgument == "First"))
				{
					if (!(commandArgument == "Next"))
					{
						if (!(commandArgument == "Prev"))
						{
							if (!(commandArgument == "Last"))
							{
								goto IL_73;
							}
							result = this.Builder.CreateLastButton();
						}
						else
						{
							result = this.Builder.CreatePrevButton();
						}
					}
					else
					{
						result = this.Builder.CreateNextButton();
					}
				}
				else
				{
					result = this.Builder.CreateFirstButton();
				}
				return result;
			}
			IL_73:
			throw new ArgumentOutOfRangeException("commandArgument");
		}

		// Token: 0x040032FC RID: 13052
		private TreeListPagerButtonBuilder _builder;
	}
}
