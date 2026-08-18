using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x02000DDE RID: 3550
	public class PivotGridPagerItem : PivotGridItem
	{
		// Token: 0x17002997 RID: 10647
		// (get) Token: 0x060083C1 RID: 33729 RVA: 0x001E076A File Offset: 0x001DE96A
		// (set) Token: 0x060083C2 RID: 33730 RVA: 0x001E0772 File Offset: 0x001DE972
		public int ColumnSpan { get; set; }

		// Token: 0x060083C3 RID: 33731 RVA: 0x001E077B File Offset: 0x001DE97B
		public PivotGridPagerItem(RadPivotGrid ownerPivotGrid, PivotGridItemType itemType, bool isDataBinding, int columnSpan) : base(ownerPivotGrid, itemType, isDataBinding)
		{
			this.ColumnSpan = columnSpan;
		}

		// Token: 0x060083C4 RID: 33732 RVA: 0x001E0790 File Offset: 0x001DE990
		internal override void Initialize()
		{
			this.PagerContentCell = this.CreateCellObject();
			this.PagerContentCell.ColumnSpan = this.ColumnSpan;
			this.Cells.Add(this.PagerContentCell);
			this.InitializePagerItem();
			this.CallOnItemCreated();
			base.OwnerPivotGrid.Items.Add(this);
			if (this.IsDataBinding)
			{
				this.DataBind();
				this.CallOnItemDataBound();
			}
		}

		// Token: 0x060083C5 RID: 33733 RVA: 0x001E0800 File Offset: 0x001DEA00
		private void InitializePagerItem()
		{
			ControlCollection controls;
			if (base.OwnerPivotGrid.ResolvedRenderMode == RenderMode.Lightweight)
			{
				Panel panel = new Panel
				{
					CssClass = base.OwnerPivotGrid.PagerStyle.Mode.ToString()
				};
				this.PagerContentCell.Controls.Add(panel);
				controls = panel.Controls;
			}
			else
			{
				controls = this.PagerContentCell.Controls;
			}
			switch (base.OwnerPivotGrid.PagerStyle.Mode)
			{
			case PivotGridPagerMode.NextPrev:
			{
				Control control = this.Builder.BuildContainer("rpgArrPart1");
				control.Controls.Add(this.Builder.CreateFirstButton());
				control.Controls.Add(this.Builder.CreatePrevButton());
				control.Controls.Add(this.Builder.CreateNextButton());
				control.Controls.Add(this.Builder.CreateLastButton());
				controls.Add(control);
				return;
			}
			case PivotGridPagerMode.NumericPages:
				controls.Add(this.NumericPager);
				return;
			case PivotGridPagerMode.NextPrevAndNumeric:
				this.CreateNextPrevAndNumeric(controls);
				if (base.OwnerPivotGrid.PagerStyle.PageSizeControlType != PagerDropDownControlType.None)
				{
					controls.Add(this.Builder.CreatePageSize());
					return;
				}
				break;
			case PivotGridPagerMode.NextPrevNumericAndAdvanced:
				this.CreateNextPrevAndNumeric(controls);
				controls.Add(this.Builder.CreateAdvancedPager());
				return;
			case PivotGridPagerMode.Advanced:
				controls.Add(this.Builder.CreateAdvancedPager());
				return;
			case PivotGridPagerMode.Slider:
				controls.Add(this.Builder.CreateSliderPager());
				break;
			default:
				return;
			}
		}

		// Token: 0x060083C6 RID: 33734 RVA: 0x001E0984 File Offset: 0x001DEB84
		private void CreateNextPrevAndNumeric(ControlCollection pagerContainer)
		{
			Control control = this.Builder.BuildContainer("rpgArrPart1");
			control.Controls.Add(this.Builder.CreateFirstButton());
			control.Controls.Add(this.Builder.CreatePrevButton());
			pagerContainer.Add(control);
			pagerContainer.Add(this.NumericPager);
			control = this.Builder.BuildContainer("rpgArrPart2");
			control.Controls.Add(this.Builder.CreateNextButton());
			control.Controls.Add(this.Builder.CreateLastButton());
			pagerContainer.Add(control);
		}

		// Token: 0x17002998 RID: 10648
		// (get) Token: 0x060083C7 RID: 33735 RVA: 0x001E0A25 File Offset: 0x001DEC25
		// (set) Token: 0x060083C8 RID: 33736 RVA: 0x001E0A2D File Offset: 0x001DEC2D
		public TableCell PagerContentCell { get; protected set; }

		// Token: 0x17002999 RID: 10649
		// (get) Token: 0x060083C9 RID: 33737 RVA: 0x001E0A36 File Offset: 0x001DEC36
		public PivotGridPagingManager Paging
		{
			get
			{
				return base.OwnerPivotGrid.pagingManager;
			}
		}

		// Token: 0x1700299A RID: 10650
		// (get) Token: 0x060083CA RID: 33738 RVA: 0x001E0A43 File Offset: 0x001DEC43
		// (set) Token: 0x060083CB RID: 33739 RVA: 0x001E0A4B File Offset: 0x001DEC4B
		public bool IsTopItem { get; internal set; }

		// Token: 0x1700299B RID: 10651
		// (get) Token: 0x060083CC RID: 33740 RVA: 0x001E0A54 File Offset: 0x001DEC54
		internal PivotGridPagerButtonBuilder Builder
		{
			get
			{
				if (this._builder == null)
				{
					this._builder = new PivotGridPagerButtonBuilder(this);
				}
				return this._builder;
			}
		}

		// Token: 0x1700299C RID: 10652
		// (get) Token: 0x060083CD RID: 33741 RVA: 0x001E0A70 File Offset: 0x001DEC70
		public Control NumericPager
		{
			get
			{
				return this.Builder.CreateNumericPager();
			}
		}

		// Token: 0x060083CE RID: 33742 RVA: 0x001E0A80 File Offset: 0x001DEC80
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

		// Token: 0x0400248E RID: 9358
		private PivotGridPagerButtonBuilder _builder;
	}
}
