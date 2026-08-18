using System;
using System.ComponentModel;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x02000DE2 RID: 3554
	public class PivotGridPagerStyle : PivotGridTableItemStyle
	{
		// Token: 0x060083D1 RID: 33745 RVA: 0x001E0B1C File Offset: 0x001DED1C
		internal PivotGridPagerStyle(RadPivotGrid owner)
		{
			this.OwnerPivotGrid = owner;
		}

		// Token: 0x1700299E RID: 10654
		// (get) Token: 0x060083D2 RID: 33746 RVA: 0x001E0B2B File Offset: 0x001DED2B
		// (set) Token: 0x060083D3 RID: 33747 RVA: 0x001E0B33 File Offset: 0x001DED33
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public RadPivotGrid OwnerPivotGrid { get; protected set; }

		// Token: 0x1700299F RID: 10655
		// (get) Token: 0x060083D4 RID: 33748 RVA: 0x001E0B3C File Offset: 0x001DED3C
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public override bool IsDefault
		{
			get
			{
				return base.IsDefault && base.ViewState["NextPageToolTip"] == null && base.ViewState["FirstPageToolTip"] == null && base.ViewState["LastPageToolTip"] == null && base.ViewState["PrevPageToolTip"] == null && base.ViewState["Mode"] == null && base.ViewState["Position"] == null && base.ViewState["PageButtonCount"] == null && base.ViewState["PageSliderIncreaseToolTip"] == null && base.ViewState["PageSliderDecreaseToolTip"] == null && base.ViewState["PageSliderDragToolTip"] == null && base.ViewState["PageSliderPagerLabel"] == null && base.ViewState["ChangePageSizeLabelText"] == null && base.ViewState["GoToPageLinkButtonText"] == null && base.ViewState["GoToPageLabelText"] == null && base.ViewState["ChangePageSizeLinkButtonText"] == null && base.ViewState["PageOfLabelText"] == null;
			}
		}

		// Token: 0x060083D5 RID: 33749 RVA: 0x001E0C98 File Offset: 0x001DEE98
		public override void CopyFrom(Style s)
		{
			if (s != null)
			{
				base.CopyFrom(s);
				PivotGridPagerStyle pivotGridPagerStyle = s as PivotGridPagerStyle;
				if (pivotGridPagerStyle != null)
				{
					if (pivotGridPagerStyle.ViewState["NextPageToolTip"] != null)
					{
						this.NextPageToolTip = pivotGridPagerStyle.NextPageToolTip;
					}
					if (pivotGridPagerStyle.ViewState["FirstPageToolTip"] != null)
					{
						this.FirstPageToolTip = pivotGridPagerStyle.FirstPageToolTip;
					}
					if (pivotGridPagerStyle.ViewState["LastPageToolTip"] != null)
					{
						this.LastPageToolTip = pivotGridPagerStyle.LastPageToolTip;
					}
					if (pivotGridPagerStyle.ViewState["PrevPageToolTip"] != null)
					{
						this.PrevPageToolTip = pivotGridPagerStyle.PrevPageToolTip;
					}
					if (pivotGridPagerStyle.ViewState["Mode"] != null)
					{
						this.Mode = pivotGridPagerStyle.Mode;
					}
					if (pivotGridPagerStyle.ViewState["Position"] != null)
					{
						this.Position = pivotGridPagerStyle.Position;
					}
					if (pivotGridPagerStyle.ViewState["PageButtonCount"] != null)
					{
						this.PageButtonCount = pivotGridPagerStyle.PageButtonCount;
					}
					if (pivotGridPagerStyle.ViewState["PageSliderIncreaseToolTip"] != null)
					{
						this.PageSliderIncreaseToolTip = pivotGridPagerStyle.PageSliderIncreaseToolTip;
					}
					if (pivotGridPagerStyle.ViewState["PageSliderDecreaseToolTip"] != null)
					{
						this.PageSliderDecreaseToolTip = pivotGridPagerStyle.PageSliderDecreaseToolTip;
					}
					if (pivotGridPagerStyle.ViewState["PageSliderDragToolTip"] != null)
					{
						this.PageSliderDragToolTip = pivotGridPagerStyle.PageSliderDragToolTip;
					}
					if (pivotGridPagerStyle.ViewState["PageSliderPagerLabel"] != null)
					{
						this.PageSliderPagerLabel = pivotGridPagerStyle.PageSliderPagerLabel;
					}
					if (pivotGridPagerStyle.ViewState["ChangePageSizeLabelText"] != null)
					{
						this.ChangePageSizeLabelText = pivotGridPagerStyle.ChangePageSizeLabelText;
					}
					if (pivotGridPagerStyle.ViewState["GoToPageLinkButtonText"] != null)
					{
						this.GoToPageLinkButtonText = pivotGridPagerStyle.GoToPageLinkButtonText;
					}
					if (pivotGridPagerStyle.ViewState["GoToPageLabelText"] != null)
					{
						this.GoToPageLabelText = pivotGridPagerStyle.GoToPageLabelText;
					}
					if (pivotGridPagerStyle.ViewState["ChangePageSizeLinkButtonText"] != null)
					{
						this.ChangePageSizeLinkButtonText = pivotGridPagerStyle.ChangePageSizeLinkButtonText;
					}
					if (pivotGridPagerStyle.ViewState["PageOfLabelText"] != null)
					{
						this.PageOfLabelText = pivotGridPagerStyle.PageOfLabelText;
					}
				}
			}
		}

		// Token: 0x060083D6 RID: 33750 RVA: 0x001E0EA0 File Offset: 0x001DF0A0
		public override void MergeWith(Style s)
		{
			if (s != null)
			{
				if (this.IsEmpty)
				{
					this.CopyFrom(s);
					return;
				}
				base.MergeWith(s);
				PivotGridPagerStyle pivotGridPagerStyle = s as PivotGridPagerStyle;
				if (pivotGridPagerStyle != null)
				{
					if (pivotGridPagerStyle.ViewState["NextPageToolTip"] != null && base.ViewState["NextPageToolTip"] == null)
					{
						this.NextPageToolTip = pivotGridPagerStyle.NextPageToolTip;
					}
					if (pivotGridPagerStyle.ViewState["FirstPageToolTip"] != null && base.ViewState["FirstPageToolTip"] == null)
					{
						this.FirstPageToolTip = pivotGridPagerStyle.FirstPageToolTip;
					}
					if (pivotGridPagerStyle.ViewState["LastPageToolTip"] != null && base.ViewState["LastPageToolTip"] == null)
					{
						this.LastPageToolTip = pivotGridPagerStyle.LastPageToolTip;
					}
					if (pivotGridPagerStyle.ViewState["PrevPageToolTip"] != null && base.ViewState["PrevPageToolTip"] == null)
					{
						this.PrevPageToolTip = pivotGridPagerStyle.PrevPageToolTip;
					}
					if (pivotGridPagerStyle.ViewState["Mode"] != null && base.ViewState["Mode"] == null)
					{
						this.Mode = pivotGridPagerStyle.Mode;
					}
					if (pivotGridPagerStyle.ViewState["Position"] != null && base.ViewState["Position"] == null)
					{
						this.Position = pivotGridPagerStyle.Position;
					}
					if (pivotGridPagerStyle.ViewState["PageButtonCount"] != null && base.ViewState["PageButtonCount"] == null)
					{
						this.PageButtonCount = pivotGridPagerStyle.PageButtonCount;
					}
					if (pivotGridPagerStyle.ViewState["PageSliderIncreaseToolTip"] != null && base.ViewState["PageSliderIncreaseToolTip"] == null)
					{
						this.PageSliderIncreaseToolTip = pivotGridPagerStyle.PageSliderIncreaseToolTip;
					}
					if (pivotGridPagerStyle.ViewState["PageSliderDecreaseToolTip"] != null && base.ViewState["PageSliderDecreaseToolTip"] == null)
					{
						this.PageSliderDecreaseToolTip = pivotGridPagerStyle.PageSliderDecreaseToolTip;
					}
					if (pivotGridPagerStyle.ViewState["PageSliderDragToolTip"] != null && base.ViewState["PageSliderDragToolTip"] == null)
					{
						this.PageSliderDragToolTip = pivotGridPagerStyle.PageSliderDragToolTip;
					}
					if (pivotGridPagerStyle.ViewState["PageSliderPagerLabel"] != null && base.ViewState["PageSliderPagerLabel"] == null)
					{
						this.PageSliderPagerLabel = pivotGridPagerStyle.PageSliderPagerLabel;
					}
					if (pivotGridPagerStyle.ViewState["ChangePageSizeLabelText"] != null && base.ViewState["ChangePageSizeLabelText"] == null)
					{
						this.ChangePageSizeLabelText = pivotGridPagerStyle.ChangePageSizeLabelText;
					}
					if (pivotGridPagerStyle.ViewState["GoToPageLinkButtonText"] != null && base.ViewState["GoToPageLinkButtonText"] == null)
					{
						this.GoToPageLinkButtonText = pivotGridPagerStyle.GoToPageLinkButtonText;
					}
					if (pivotGridPagerStyle.ViewState["GoToPageLabelText"] != null && base.ViewState["GoToPageLabelText"] == null)
					{
						this.GoToPageLabelText = pivotGridPagerStyle.GoToPageLabelText;
					}
					if (pivotGridPagerStyle.ViewState["ChangePageSizeLinkButtonText"] != null && base.ViewState["ChangePageSizeLinkButtonText"] == null)
					{
						this.ChangePageSizeLinkButtonText = pivotGridPagerStyle.ChangePageSizeLinkButtonText;
					}
					if (pivotGridPagerStyle.ViewState["PageOfLabelText"] != null && base.ViewState["PageOfLabelText"] == null)
					{
						this.PageOfLabelText = pivotGridPagerStyle.PageOfLabelText;
					}
				}
			}
		}

		// Token: 0x060083D7 RID: 33751 RVA: 0x001E11D8 File Offset: 0x001DF3D8
		public override void Reset()
		{
			if (base.ViewState["NextPageToolTip"] != null)
			{
				base.ViewState.Remove("NextPageToolTip");
			}
			if (base.ViewState["PrevPageToolTip"] != null)
			{
				base.ViewState.Remove("PrevPageToolTip");
			}
			if (base.ViewState["FirstPageToolTip"] != null)
			{
				base.ViewState.Remove("FirstPageToolTip");
			}
			if (base.ViewState["LastPageToolTip"] != null)
			{
				base.ViewState.Remove("LastPageToolTip");
			}
			if (base.ViewState["Mode"] != null)
			{
				base.ViewState.Remove("Mode");
			}
			if (base.ViewState["Position"] != null)
			{
				base.ViewState.Remove("Position");
			}
			if (base.ViewState["PageButtonCount"] != null)
			{
				base.ViewState.Remove("PageButtonCount");
			}
			base.Reset();
		}

		// Token: 0x170029A0 RID: 10656
		// (get) Token: 0x060083D8 RID: 33752 RVA: 0x001E12DC File Offset: 0x001DF4DC
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public bool IsPagerOnBottom
		{
			get
			{
				PivotGridPagerPosition position = this.Position;
				return position == PivotGridPagerPosition.Bottom || position == PivotGridPagerPosition.TopAndBottom;
			}
		}

		// Token: 0x170029A1 RID: 10657
		// (get) Token: 0x060083D9 RID: 33753 RVA: 0x001E12FC File Offset: 0x001DF4FC
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool IsPagerOnTop
		{
			get
			{
				PivotGridPagerPosition position = this.Position;
				return position == PivotGridPagerPosition.Top || position == PivotGridPagerPosition.TopAndBottom;
			}
		}

		// Token: 0x170029A2 RID: 10658
		// (get) Token: 0x060083DA RID: 33754 RVA: 0x001E131C File Offset: 0x001DF51C
		// (set) Token: 0x060083DB RID: 33755 RVA: 0x001E1345 File Offset: 0x001DF545
		[Bindable(true)]
		[Category("Appearance")]
		[DefaultValue(typeof(PivotGridPagerMode), "NextPrevAndNumeric")]
		[NotifyParentProperty(true)]
		public PivotGridPagerMode Mode
		{
			get
			{
				object obj = base.ViewState["Mode"];
				if (obj == null)
				{
					return PivotGridPagerMode.NextPrevAndNumeric;
				}
				return (PivotGridPagerMode)obj;
			}
			set
			{
				if (value < PivotGridPagerMode.NextPrev || value > PivotGridPagerMode.Slider)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				base.ViewState["Mode"] = value;
			}
		}

		// Token: 0x170029A3 RID: 10659
		// (get) Token: 0x060083DC RID: 33756 RVA: 0x001E1370 File Offset: 0x001DF570
		// (set) Token: 0x060083DD RID: 33757 RVA: 0x001E13A8 File Offset: 0x001DF5A8
		[Bindable(true)]
		[NotifyParentProperty(true)]
		[DefaultValue("First Page")]
		[Localizable(true)]
		[Description("FirstPageToolTip")]
		[Category("Appearance")]
		public string FirstPageToolTip
		{
			get
			{
				object obj = base.ViewState["FirstPageToolTip"];
				if (obj == null)
				{
					return this.OwnerPivotGrid.Localization.FirstPageToolTip;
				}
				return (string)obj;
			}
			set
			{
				base.ViewState["FirstPageToolTip"] = value;
			}
		}

		// Token: 0x170029A4 RID: 10660
		// (get) Token: 0x060083DE RID: 33758 RVA: 0x001E13BC File Offset: 0x001DF5BC
		// (set) Token: 0x060083DF RID: 33759 RVA: 0x001E13F4 File Offset: 0x001DF5F4
		[Category("Appearance")]
		[DefaultValue("Next Page")]
		[NotifyParentProperty(true)]
		[Localizable(true)]
		[Description("NextPageToolTip")]
		[Bindable(true)]
		public string NextPageToolTip
		{
			get
			{
				object obj = base.ViewState["NextPageToolTip"];
				if (obj == null)
				{
					return this.OwnerPivotGrid.Localization.NextPageToolTip;
				}
				return (string)obj;
			}
			set
			{
				base.ViewState["NextPageToolTip"] = value;
			}
		}

		// Token: 0x170029A5 RID: 10661
		// (get) Token: 0x060083E0 RID: 33760 RVA: 0x001E1408 File Offset: 0x001DF608
		// (set) Token: 0x060083E1 RID: 33761 RVA: 0x001E1440 File Offset: 0x001DF640
		[Localizable(true)]
		[DefaultValue("")]
		[NotifyParentProperty(true)]
		[Bindable(true)]
		[Description("GoToPageTextBoxToolTip")]
		[Category("Appearance")]
		public string GoToPageTextBoxToolTip
		{
			get
			{
				object obj = base.ViewState["GoToPageTextBoxToolTip"];
				if (obj == null)
				{
					return this.OwnerPivotGrid.Localization.GoToPageTextBoxToolTip;
				}
				return (string)obj;
			}
			set
			{
				base.ViewState["GoToPageTextBoxToolTip"] = value;
			}
		}

		// Token: 0x170029A6 RID: 10662
		// (get) Token: 0x060083E2 RID: 33762 RVA: 0x001E1454 File Offset: 0x001DF654
		// (set) Token: 0x060083E3 RID: 33763 RVA: 0x001E148C File Offset: 0x001DF68C
		[Bindable(true)]
		[DefaultValue("")]
		[NotifyParentProperty(true)]
		[Localizable(true)]
		[Description("ChangePageSizeTextBoxToolTip")]
		[Category("Appearance")]
		public string ChangePageSizeTextBoxToolTip
		{
			get
			{
				object obj = base.ViewState["ChangePageSizeTextBoxToolTip"];
				if (obj == null)
				{
					return this.OwnerPivotGrid.Localization.ChangePageSizeTextBoxToolTip;
				}
				return (string)obj;
			}
			set
			{
				base.ViewState["ChangePageSizeTextBoxToolTip"] = value;
			}
		}

		// Token: 0x170029A7 RID: 10663
		// (get) Token: 0x060083E4 RID: 33764 RVA: 0x001E14A0 File Offset: 0x001DF6A0
		// (set) Token: 0x060083E5 RID: 33765 RVA: 0x001E14D8 File Offset: 0x001DF6D8
		[Description("GoToPageButtonToolTip")]
		[DefaultValue("Go to Page")]
		[NotifyParentProperty(true)]
		[Localizable(true)]
		[Category("Appearance")]
		[Bindable(true)]
		public string GoToPageButtonToolTip
		{
			get
			{
				object obj = base.ViewState["GoToPageButtonToolTip"];
				if (obj == null)
				{
					return this.OwnerPivotGrid.Localization.GoToPageButtonToolTip;
				}
				return (string)obj;
			}
			set
			{
				base.ViewState["GoToPageButtonToolTip"] = value;
			}
		}

		// Token: 0x170029A8 RID: 10664
		// (get) Token: 0x060083E6 RID: 33766 RVA: 0x001E14EC File Offset: 0x001DF6EC
		// (set) Token: 0x060083E7 RID: 33767 RVA: 0x001E1524 File Offset: 0x001DF724
		[Localizable(true)]
		[NotifyParentProperty(true)]
		[DefaultValue("Go to Page")]
		[Description("GoToPageButtonToolTip")]
		[Category("Appearance")]
		[Bindable(true)]
		public string ChangePageSizeButtonToolTip
		{
			get
			{
				object obj = base.ViewState["ChangePageSizeButtonToolTip"];
				if (obj == null)
				{
					return this.OwnerPivotGrid.Localization.ChangePageSizeButtonToolTip;
				}
				return (string)obj;
			}
			set
			{
				base.ViewState["ChangePageSizeButtonToolTip"] = value;
			}
		}

		// Token: 0x170029A9 RID: 10665
		// (get) Token: 0x060083E8 RID: 33768 RVA: 0x001E1538 File Offset: 0x001DF738
		// (set) Token: 0x060083E9 RID: 33769 RVA: 0x001E1570 File Offset: 0x001DF770
		[DefaultValue("")]
		[NotifyParentProperty(true)]
		[Localizable(true)]
		[Category("Appearance")]
		[Bindable(true)]
		public string ChangePageSizeComboBoxTableSummary
		{
			get
			{
				object obj = base.ViewState["ChangePageSizeComboBoxTableSummary"];
				if (obj == null)
				{
					return this.OwnerPivotGrid.Localization.ChangePageSizeComboBoxTableSummary;
				}
				return (string)obj;
			}
			set
			{
				base.ViewState["ChangePageSizeComboBoxTableSummary"] = value;
			}
		}

		// Token: 0x170029AA RID: 10666
		// (get) Token: 0x060083EA RID: 33770 RVA: 0x001E1584 File Offset: 0x001DF784
		// (set) Token: 0x060083EB RID: 33771 RVA: 0x001E15BC File Offset: 0x001DF7BC
		[DefaultValue("")]
		[NotifyParentProperty(true)]
		[Localizable(true)]
		[Category("Appearance")]
		[Bindable(true)]
		public string ChangePageSizeComboBoxToolTip
		{
			get
			{
				object obj = base.ViewState["ChangePageSizeComboBoxToolTip"];
				if (obj == null)
				{
					return this.OwnerPivotGrid.Localization.ChangePageSizeComboBoxToolTip;
				}
				return (string)obj;
			}
			set
			{
				base.ViewState["ChangePageSizeComboBoxToolTip"] = value;
			}
		}

		// Token: 0x170029AB RID: 10667
		// (get) Token: 0x060083EC RID: 33772 RVA: 0x001E15D0 File Offset: 0x001DF7D0
		// (set) Token: 0x060083ED RID: 33773 RVA: 0x001E1608 File Offset: 0x001DF808
		[Description("LastPageToolTip")]
		[NotifyParentProperty(true)]
		[Localizable(true)]
		[DefaultValue("Last Page")]
		[Category("Appearance")]
		[Bindable(true)]
		public string LastPageToolTip
		{
			get
			{
				object obj = base.ViewState["LastPageToolTip"];
				if (obj == null)
				{
					return this.OwnerPivotGrid.Localization.LastPageToolTip;
				}
				return (string)obj;
			}
			set
			{
				base.ViewState["LastPageToolTip"] = value;
			}
		}

		// Token: 0x170029AC RID: 10668
		// (get) Token: 0x060083EE RID: 33774 RVA: 0x001E161C File Offset: 0x001DF81C
		// (set) Token: 0x060083EF RID: 33775 RVA: 0x001E1654 File Offset: 0x001DF854
		[DefaultValue("Previous Page")]
		[NotifyParentProperty(true)]
		[Localizable(true)]
		[Description("PrevPageToolTip")]
		[Category("Appearance")]
		[Bindable(true)]
		public string PrevPageToolTip
		{
			get
			{
				object obj = base.ViewState["PrevPageToolTip"];
				if (obj == null)
				{
					return this.OwnerPivotGrid.Localization.PrevPageToolTip;
				}
				return (string)obj;
			}
			set
			{
				base.ViewState["PrevPageToolTip"] = value;
			}
		}

		// Token: 0x170029AD RID: 10669
		// (get) Token: 0x060083F0 RID: 33776 RVA: 0x001E1668 File Offset: 0x001DF868
		// (set) Token: 0x060083F1 RID: 33777 RVA: 0x001E1692 File Offset: 0x001DF892
		[Category("Behavior")]
		[DefaultValue(10)]
		[NotifyParentProperty(true)]
		[Bindable(true)]
		public int PageButtonCount
		{
			get
			{
				object obj = base.ViewState["PageButtonCount"];
				if (obj == null)
				{
					return 10;
				}
				return (int)obj;
			}
			set
			{
				if (value < 1)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				base.ViewState["PageButtonCount"] = value;
			}
		}

		// Token: 0x170029AE RID: 10670
		// (get) Token: 0x060083F2 RID: 33778 RVA: 0x001E16BC File Offset: 0x001DF8BC
		// (set) Token: 0x060083F3 RID: 33779 RVA: 0x001E16E5 File Offset: 0x001DF8E5
		[NotifyParentProperty(true)]
		[Category("Layout")]
		[DefaultValue(typeof(PivotGridPagerPosition), "Bottom")]
		[Bindable(true)]
		public PivotGridPagerPosition Position
		{
			get
			{
				object obj = base.ViewState["Position"];
				if (obj == null)
				{
					return PivotGridPagerPosition.Bottom;
				}
				return (PivotGridPagerPosition)obj;
			}
			set
			{
				if (value < PivotGridPagerPosition.Bottom || value > PivotGridPagerPosition.TopAndBottom)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				base.ViewState["Position"] = value;
			}
		}

		// Token: 0x170029AF RID: 10671
		// (get) Token: 0x060083F4 RID: 33780 RVA: 0x001E1710 File Offset: 0x001DF910
		// (set) Token: 0x060083F5 RID: 33781 RVA: 0x001E173D File Offset: 0x001DF93D
		[NotifyParentProperty(true)]
		[Description("RadPivotGridPagerStyle_Visible")]
		[Bindable(true)]
		[Category("Appearance")]
		[DefaultValue(false)]
		public bool AlwaysVisible
		{
			get
			{
				object obj = base.ViewState["PagerAlwaysVisible"];
				return obj != null && (bool)obj;
			}
			set
			{
				base.ViewState["PagerAlwaysVisible"] = value;
			}
		}

		// Token: 0x170029B0 RID: 10672
		// (get) Token: 0x060083F6 RID: 33782 RVA: 0x001E1758 File Offset: 0x001DF958
		// (set) Token: 0x060083F7 RID: 33783 RVA: 0x001E1790 File Offset: 0x001DF990
		[Category("Appearance")]
		[DefaultValue("Increase")]
		[NotifyParentProperty(true)]
		[Localizable(true)]
		[Description("PageSliderIncreaseToolTip")]
		[Bindable(true)]
		public string PageSliderIncreaseToolTip
		{
			get
			{
				object obj = base.ViewState["PageSliderIncreaseToolTip"];
				if (obj == null)
				{
					return this.OwnerPivotGrid.Localization.PageSliderIncreaseToolTip;
				}
				return (string)obj;
			}
			set
			{
				base.ViewState["PageSliderIncreaseToolTip"] = value;
			}
		}

		// Token: 0x170029B1 RID: 10673
		// (get) Token: 0x060083F8 RID: 33784 RVA: 0x001E17A4 File Offset: 0x001DF9A4
		// (set) Token: 0x060083F9 RID: 33785 RVA: 0x001E17DC File Offset: 0x001DF9DC
		[Bindable(true)]
		[DefaultValue("Decrease")]
		[NotifyParentProperty(true)]
		[Localizable(true)]
		[Description("PageSliderDecreaseToolTip")]
		[Category("Appearance")]
		public string PageSliderDecreaseToolTip
		{
			get
			{
				object obj = base.ViewState["PageSliderDecreaseToolTip"];
				if (obj == null)
				{
					return this.OwnerPivotGrid.Localization.PageSliderDecreaseToolTip;
				}
				return (string)obj;
			}
			set
			{
				base.ViewState["PageSliderDecreaseToolTip"] = value;
			}
		}

		// Token: 0x170029B2 RID: 10674
		// (get) Token: 0x060083FA RID: 33786 RVA: 0x001E17F0 File Offset: 0x001DF9F0
		// (set) Token: 0x060083FB RID: 33787 RVA: 0x001E1828 File Offset: 0x001DFA28
		[Localizable(true)]
		[DefaultValue("Drag")]
		[NotifyParentProperty(true)]
		[Bindable(true)]
		[Description("PageSliderDragToolTip")]
		[Category("Appearance")]
		public string PageSliderDragToolTip
		{
			get
			{
				object obj = base.ViewState["PageSliderDragToolTip"];
				if (obj == null)
				{
					return this.OwnerPivotGrid.Localization.PageSliderDragToolTip;
				}
				return (string)obj;
			}
			set
			{
				base.ViewState["PageSliderDragToolTip"] = value;
			}
		}

		// Token: 0x170029B3 RID: 10675
		// (get) Token: 0x060083FC RID: 33788 RVA: 0x001E183C File Offset: 0x001DFA3C
		// (set) Token: 0x060083FD RID: 33789 RVA: 0x001E1874 File Offset: 0x001DFA74
		[Bindable(true)]
		[DefaultValue("Page <strong>{0}</strong> of <strong>{1}</strong>")]
		[NotifyParentProperty(true)]
		[Localizable(true)]
		[Description("PageSliderPagerLabel")]
		[Category("Appearance")]
		public string PageSliderPagerLabel
		{
			get
			{
				object obj = base.ViewState["PageSliderPagerLabel"];
				if (obj == null)
				{
					return this.OwnerPivotGrid.Localization.PageSliderPagerLabel;
				}
				return (string)obj;
			}
			set
			{
				base.ViewState["PageSliderPagerLabel"] = value;
			}
		}

		// Token: 0x170029B4 RID: 10676
		// (get) Token: 0x060083FE RID: 33790 RVA: 0x001E1888 File Offset: 0x001DFA88
		// (set) Token: 0x060083FF RID: 33791 RVA: 0x001E18C0 File Offset: 0x001DFAC0
		[Category("Appearance")]
		[Bindable(true)]
		[DefaultValue("Page size:")]
		[NotifyParentProperty(true)]
		[Localizable(true)]
		[Description("ChangePageSizeLabelText")]
		public string ChangePageSizeLabelText
		{
			get
			{
				object obj = base.ViewState["ChangePageSizeLabelText"];
				if (obj == null)
				{
					return this.OwnerPivotGrid.Localization.ChangePageSizeLabelText;
				}
				return (string)obj;
			}
			set
			{
				base.ViewState["ChangePageSizeLabelText"] = value;
			}
		}

		// Token: 0x170029B5 RID: 10677
		// (get) Token: 0x06008400 RID: 33792 RVA: 0x001E18D4 File Offset: 0x001DFAD4
		// (set) Token: 0x06008401 RID: 33793 RVA: 0x001E190C File Offset: 0x001DFB0C
		[Bindable(true)]
		[DefaultValue("Change")]
		[NotifyParentProperty(true)]
		[Localizable(true)]
		[Description("ChangePageSizeLinkButtonText")]
		[Category("Appearance")]
		public string ChangePageSizeLinkButtonText
		{
			get
			{
				object obj = base.ViewState["ChangePageSizeLinkButtonText"];
				if (obj == null)
				{
					return this.OwnerPivotGrid.Localization.ChangePageSizeLinkButtonText;
				}
				return (string)obj;
			}
			set
			{
				base.ViewState["ChangePageSizeLinkButtonText"] = value;
			}
		}

		// Token: 0x170029B6 RID: 10678
		// (get) Token: 0x06008402 RID: 33794 RVA: 0x001E1920 File Offset: 0x001DFB20
		// (set) Token: 0x06008403 RID: 33795 RVA: 0x001E1958 File Offset: 0x001DFB58
		[Localizable(true)]
		[DefaultValue("Go")]
		[NotifyParentProperty(true)]
		[Description("GoToPageLinkButtonText")]
		[Category("Appearance")]
		[Bindable(true)]
		public string GoToPageLinkButtonText
		{
			get
			{
				object obj = base.ViewState["GoToPageLinkButtonText"];
				if (obj == null)
				{
					return this.OwnerPivotGrid.Localization.GoToPageLinkButtonText;
				}
				return (string)obj;
			}
			set
			{
				base.ViewState["GoToPageLinkButtonText"] = value;
			}
		}

		// Token: 0x170029B7 RID: 10679
		// (get) Token: 0x06008404 RID: 33796 RVA: 0x001E196C File Offset: 0x001DFB6C
		// (set) Token: 0x06008405 RID: 33797 RVA: 0x001E19A4 File Offset: 0x001DFBA4
		[Localizable(true)]
		[DefaultValue("Page:")]
		[NotifyParentProperty(true)]
		[Bindable(true)]
		[Description("GoToPageLabelText")]
		[Category("Appearance")]
		public string GoToPageLabelText
		{
			get
			{
				object obj = base.ViewState["GoToPageLabelText"];
				if (obj == null)
				{
					return this.OwnerPivotGrid.Localization.GoToPageLabelText;
				}
				return (string)obj;
			}
			set
			{
				base.ViewState["GoToPageLabelText"] = value;
			}
		}

		// Token: 0x170029B8 RID: 10680
		// (get) Token: 0x06008406 RID: 33798 RVA: 0x001E19B8 File Offset: 0x001DFBB8
		// (set) Token: 0x06008407 RID: 33799 RVA: 0x001E19F0 File Offset: 0x001DFBF0
		[Bindable(true)]
		[DefaultValue("of {0}")]
		[NotifyParentProperty(true)]
		[Localizable(true)]
		[Description("PageOfLabelText")]
		[Category("Appearance")]
		public string PageOfLabelText
		{
			get
			{
				object obj = base.ViewState["PageOfLabelText"];
				if (obj == null)
				{
					return this.OwnerPivotGrid.Localization.PageOfLabelText;
				}
				return (string)obj;
			}
			set
			{
				base.ViewState["PageOfLabelText"] = value;
			}
		}

		// Token: 0x170029B9 RID: 10681
		// (get) Token: 0x06008408 RID: 33800 RVA: 0x001E1A03 File Offset: 0x001DFC03
		// (set) Token: 0x06008409 RID: 33801 RVA: 0x001E1A2E File Offset: 0x001DFC2E
		[NotifyParentProperty(true)]
		[Description("Gets or sets the type of the page size drop down control")]
		[Category("Behavior")]
		public PagerDropDownControlType PageSizeControlType
		{
			get
			{
				if (base.ViewState["PageSizeControlType"] == null)
				{
					return PagerDropDownControlType.RadComboBox;
				}
				return (PagerDropDownControlType)base.ViewState["PageSizeControlType"];
			}
			set
			{
				base.ViewState["PageSizeControlType"] = value;
			}
		}
	}
}
