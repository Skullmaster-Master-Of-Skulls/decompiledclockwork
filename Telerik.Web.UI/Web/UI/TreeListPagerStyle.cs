using System;
using System.ComponentModel;
using System.Web.UI.WebControls;
using Telerik.Web.UI.Functions;

namespace Telerik.Web.UI
{
	// Token: 0x0200125D RID: 4701
	public class TreeListPagerStyle : TreeListTableItemStyle
	{
		// Token: 0x0600C1A7 RID: 49575 RVA: 0x002B3F20 File Offset: 0x002B2120
		internal TreeListPagerStyle(RadTreeList owner)
		{
			this.OwnerTreeList = owner;
		}

		// Token: 0x17003E6A RID: 15978
		// (get) Token: 0x0600C1A8 RID: 49576 RVA: 0x002B3F2F File Offset: 0x002B212F
		// (set) Token: 0x0600C1A9 RID: 49577 RVA: 0x002B3F37 File Offset: 0x002B2137
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public RadTreeList OwnerTreeList { get; protected set; }

		// Token: 0x17003E6B RID: 15979
		// (get) Token: 0x0600C1AA RID: 49578 RVA: 0x002B3F40 File Offset: 0x002B2140
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override bool IsDefault
		{
			get
			{
				return base.IsDefault && base.ViewState["NextPageToolTip"] == null && base.ViewState["FirstPageToolTip"] == null && base.ViewState["LastPageToolTip"] == null && base.ViewState["PrevPageToolTip"] == null && base.ViewState["Mode"] == null && base.ViewState["Position"] == null && base.ViewState["PageButtonCount"] == null && base.ViewState["PageSliderIncreaseToolTip"] == null && base.ViewState["PageSliderDecreaseToolTip"] == null && base.ViewState["PageSliderDragToolTip"] == null && base.ViewState["PageSliderPagerLabel"] == null && base.ViewState["ChangePageSizeLabelText"] == null && base.ViewState["GoToPageLinkButtonText"] == null && base.ViewState["GoToPageLabelText"] == null && base.ViewState["ChangePageSizeLinkButtonText"] == null && base.ViewState["PageOfLabelText"] == null;
			}
		}

		// Token: 0x0600C1AB RID: 49579 RVA: 0x002B409C File Offset: 0x002B229C
		public override void CopyFrom(Style s)
		{
			if (s != null)
			{
				base.CopyFrom(s);
				TreeListPagerStyle treeListPagerStyle = s as TreeListPagerStyle;
				if (treeListPagerStyle != null)
				{
					if (treeListPagerStyle.ViewState["NextPageToolTip"] != null)
					{
						this.NextPageToolTip = treeListPagerStyle.NextPageToolTip;
					}
					if (treeListPagerStyle.ViewState["FirstPageToolTip"] != null)
					{
						this.FirstPageToolTip = treeListPagerStyle.FirstPageToolTip;
					}
					if (treeListPagerStyle.ViewState["LastPageToolTip"] != null)
					{
						this.LastPageToolTip = treeListPagerStyle.LastPageToolTip;
					}
					if (treeListPagerStyle.ViewState["PrevPageToolTip"] != null)
					{
						this.PrevPageToolTip = treeListPagerStyle.PrevPageToolTip;
					}
					if (treeListPagerStyle.ViewState["Mode"] != null)
					{
						this.Mode = treeListPagerStyle.Mode;
					}
					if (treeListPagerStyle.ViewState["Position"] != null)
					{
						this.Position = treeListPagerStyle.Position;
					}
					if (treeListPagerStyle.ViewState["PageButtonCount"] != null)
					{
						this.PageButtonCount = treeListPagerStyle.PageButtonCount;
					}
					if (treeListPagerStyle.ViewState["PageSliderIncreaseToolTip"] != null)
					{
						this.PageSliderIncreaseToolTip = treeListPagerStyle.PageSliderIncreaseToolTip;
					}
					if (treeListPagerStyle.ViewState["PageSliderDecreaseToolTip"] != null)
					{
						this.PageSliderDecreaseToolTip = treeListPagerStyle.PageSliderDecreaseToolTip;
					}
					if (treeListPagerStyle.ViewState["PageSliderDragToolTip"] != null)
					{
						this.PageSliderDragToolTip = treeListPagerStyle.PageSliderDragToolTip;
					}
					if (treeListPagerStyle.ViewState["PageSliderPagerLabel"] != null)
					{
						this.PageSliderPagerLabel = treeListPagerStyle.PageSliderPagerLabel;
					}
					if (treeListPagerStyle.ViewState["ChangePageSizeLabelText"] != null)
					{
						this.ChangePageSizeLabelText = treeListPagerStyle.ChangePageSizeLabelText;
					}
					if (treeListPagerStyle.ViewState["GoToPageLinkButtonText"] != null)
					{
						this.GoToPageLinkButtonText = treeListPagerStyle.GoToPageLinkButtonText;
					}
					if (treeListPagerStyle.ViewState["GoToPageLabelText"] != null)
					{
						this.GoToPageLabelText = treeListPagerStyle.GoToPageLabelText;
					}
					if (treeListPagerStyle.ViewState["ChangePageSizeLinkButtonText"] != null)
					{
						this.ChangePageSizeLinkButtonText = treeListPagerStyle.ChangePageSizeLinkButtonText;
					}
					if (treeListPagerStyle.ViewState["PageOfLabelText"] != null)
					{
						this.PageOfLabelText = treeListPagerStyle.PageOfLabelText;
					}
				}
			}
		}

		// Token: 0x0600C1AC RID: 49580 RVA: 0x002B42A4 File Offset: 0x002B24A4
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
				TreeListPagerStyle treeListPagerStyle = s as TreeListPagerStyle;
				if (treeListPagerStyle != null)
				{
					if (treeListPagerStyle.ViewState["NextPageToolTip"] != null && base.ViewState["NextPageToolTip"] == null)
					{
						this.NextPageToolTip = treeListPagerStyle.NextPageToolTip;
					}
					if (treeListPagerStyle.ViewState["FirstPageToolTip"] != null && base.ViewState["FirstPageToolTip"] == null)
					{
						this.FirstPageToolTip = treeListPagerStyle.FirstPageToolTip;
					}
					if (treeListPagerStyle.ViewState["LastPageToolTip"] != null && base.ViewState["LastPageToolTip"] == null)
					{
						this.LastPageToolTip = treeListPagerStyle.LastPageToolTip;
					}
					if (treeListPagerStyle.ViewState["PrevPageToolTip"] != null && base.ViewState["PrevPageToolTip"] == null)
					{
						this.PrevPageToolTip = treeListPagerStyle.PrevPageToolTip;
					}
					if (treeListPagerStyle.ViewState["Mode"] != null && base.ViewState["Mode"] == null)
					{
						this.Mode = treeListPagerStyle.Mode;
					}
					if (treeListPagerStyle.ViewState["Position"] != null && base.ViewState["Position"] == null)
					{
						this.Position = treeListPagerStyle.Position;
					}
					if (treeListPagerStyle.ViewState["PageButtonCount"] != null && base.ViewState["PageButtonCount"] == null)
					{
						this.PageButtonCount = treeListPagerStyle.PageButtonCount;
					}
					if (treeListPagerStyle.ViewState["PageSliderIncreaseToolTip"] != null && base.ViewState["PageSliderIncreaseToolTip"] == null)
					{
						this.PageSliderIncreaseToolTip = treeListPagerStyle.PageSliderIncreaseToolTip;
					}
					if (treeListPagerStyle.ViewState["PageSliderDecreaseToolTip"] != null && base.ViewState["PageSliderDecreaseToolTip"] == null)
					{
						this.PageSliderDecreaseToolTip = treeListPagerStyle.PageSliderDecreaseToolTip;
					}
					if (treeListPagerStyle.ViewState["PageSliderDragToolTip"] != null && base.ViewState["PageSliderDragToolTip"] == null)
					{
						this.PageSliderDragToolTip = treeListPagerStyle.PageSliderDragToolTip;
					}
					if (treeListPagerStyle.ViewState["PageSliderPagerLabel"] != null && base.ViewState["PageSliderPagerLabel"] == null)
					{
						this.PageSliderPagerLabel = treeListPagerStyle.PageSliderPagerLabel;
					}
					if (treeListPagerStyle.ViewState["ChangePageSizeLabelText"] != null && base.ViewState["ChangePageSizeLabelText"] == null)
					{
						this.ChangePageSizeLabelText = treeListPagerStyle.ChangePageSizeLabelText;
					}
					if (treeListPagerStyle.ViewState["GoToPageLinkButtonText"] != null && base.ViewState["GoToPageLinkButtonText"] == null)
					{
						this.GoToPageLinkButtonText = treeListPagerStyle.GoToPageLinkButtonText;
					}
					if (treeListPagerStyle.ViewState["GoToPageLabelText"] != null && base.ViewState["GoToPageLabelText"] == null)
					{
						this.GoToPageLabelText = treeListPagerStyle.GoToPageLabelText;
					}
					if (treeListPagerStyle.ViewState["ChangePageSizeLinkButtonText"] != null && base.ViewState["ChangePageSizeLinkButtonText"] == null)
					{
						this.ChangePageSizeLinkButtonText = treeListPagerStyle.ChangePageSizeLinkButtonText;
					}
					if (treeListPagerStyle.ViewState["PageOfLabelText"] != null && base.ViewState["PageOfLabelText"] == null)
					{
						this.PageOfLabelText = treeListPagerStyle.PageOfLabelText;
					}
				}
			}
		}

		// Token: 0x0600C1AD RID: 49581 RVA: 0x002B45DC File Offset: 0x002B27DC
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

		// Token: 0x0600C1AE RID: 49582 RVA: 0x002B46DD File Offset: 0x002B28DD
		private string GetLocalizationString(TFunc<TreeListLocalizationStrings, string> extractor)
		{
			return this.GetLocalizationString(extractor, string.Empty);
		}

		// Token: 0x0600C1AF RID: 49583 RVA: 0x002B46EB File Offset: 0x002B28EB
		private string GetLocalizationString(TFunc<TreeListLocalizationStrings, string> extractor, string defaultValue)
		{
			if (this.OwnerTreeList != null)
			{
				return extractor(this.OwnerTreeList.Localization);
			}
			return defaultValue;
		}

		// Token: 0x17003E6C RID: 15980
		// (get) Token: 0x0600C1B0 RID: 49584 RVA: 0x002B4708 File Offset: 0x002B2908
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public bool IsPagerOnBottom
		{
			get
			{
				TreeListPagerPosition position = this.Position;
				return position == TreeListPagerPosition.Bottom || position == TreeListPagerPosition.TopAndBottom;
			}
		}

		// Token: 0x17003E6D RID: 15981
		// (get) Token: 0x0600C1B1 RID: 49585 RVA: 0x002B4728 File Offset: 0x002B2928
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool IsPagerOnTop
		{
			get
			{
				TreeListPagerPosition position = this.Position;
				return position == TreeListPagerPosition.Top || position == TreeListPagerPosition.TopAndBottom;
			}
		}

		// Token: 0x17003E6E RID: 15982
		// (get) Token: 0x0600C1B2 RID: 49586 RVA: 0x002B4748 File Offset: 0x002B2948
		// (set) Token: 0x0600C1B3 RID: 49587 RVA: 0x002B4771 File Offset: 0x002B2971
		[Category("Appearance")]
		[NotifyParentProperty(true)]
		[Bindable(true)]
		[DefaultValue(typeof(TreeListPagerMode), "NextPrevAndNumeric")]
		public TreeListPagerMode Mode
		{
			get
			{
				object obj = base.ViewState["Mode"];
				if (obj == null)
				{
					return TreeListPagerMode.NextPrevAndNumeric;
				}
				return (TreeListPagerMode)obj;
			}
			set
			{
				if (value < TreeListPagerMode.NextPrev || value > TreeListPagerMode.Slider)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				base.ViewState["Mode"] = value;
			}
		}

		// Token: 0x17003E6F RID: 15983
		// (get) Token: 0x0600C1B4 RID: 49588 RVA: 0x002B479C File Offset: 0x002B299C
		// (set) Token: 0x0600C1B5 RID: 49589 RVA: 0x002B47D4 File Offset: 0x002B29D4
		[Category("Appearance")]
		[NotifyParentProperty(true)]
		[Localizable(true)]
		[Description("FirstPageToolTip")]
		[DefaultValue("First Page")]
		[Bindable(true)]
		public string FirstPageToolTip
		{
			get
			{
				object obj = base.ViewState["FirstPageToolTip"];
				if (obj == null)
				{
					return this.OwnerTreeList.Localization.FirstPageToolTip;
				}
				return (string)obj;
			}
			set
			{
				base.ViewState["FirstPageToolTip"] = value;
			}
		}

		// Token: 0x17003E70 RID: 15984
		// (get) Token: 0x0600C1B6 RID: 49590 RVA: 0x002B47E8 File Offset: 0x002B29E8
		// (set) Token: 0x0600C1B7 RID: 49591 RVA: 0x002B4820 File Offset: 0x002B2A20
		[Description("NextPageToolTip")]
		[Localizable(true)]
		[Bindable(true)]
		[Category("Appearance")]
		[DefaultValue("Next Page")]
		[NotifyParentProperty(true)]
		public string NextPageToolTip
		{
			get
			{
				object obj = base.ViewState["NextPageToolTip"];
				if (obj == null)
				{
					return this.OwnerTreeList.Localization.NextPageToolTip;
				}
				return (string)obj;
			}
			set
			{
				base.ViewState["NextPageToolTip"] = value;
			}
		}

		// Token: 0x17003E71 RID: 15985
		// (get) Token: 0x0600C1B8 RID: 49592 RVA: 0x002B4834 File Offset: 0x002B2A34
		// (set) Token: 0x0600C1B9 RID: 49593 RVA: 0x002B486C File Offset: 0x002B2A6C
		[Description("LastPageToolTip")]
		[Category("Appearance")]
		[Bindable(true)]
		[NotifyParentProperty(true)]
		[Localizable(true)]
		[DefaultValue("Last Page")]
		public string LastPageToolTip
		{
			get
			{
				object obj = base.ViewState["LastPageToolTip"];
				if (obj == null)
				{
					return this.OwnerTreeList.Localization.LastPageToolTip;
				}
				return (string)obj;
			}
			set
			{
				base.ViewState["LastPageToolTip"] = value;
			}
		}

		// Token: 0x17003E72 RID: 15986
		// (get) Token: 0x0600C1BA RID: 49594 RVA: 0x002B4880 File Offset: 0x002B2A80
		// (set) Token: 0x0600C1BB RID: 49595 RVA: 0x002B48B8 File Offset: 0x002B2AB8
		[DefaultValue("Previous Page")]
		[Bindable(true)]
		[NotifyParentProperty(true)]
		[Localizable(true)]
		[Description("PrevPageToolTip")]
		[Category("Appearance")]
		public string PrevPageToolTip
		{
			get
			{
				object obj = base.ViewState["PrevPageToolTip"];
				if (obj == null)
				{
					return this.OwnerTreeList.Localization.PrevPageToolTip;
				}
				return (string)obj;
			}
			set
			{
				base.ViewState["PrevPageToolTip"] = value;
			}
		}

		// Token: 0x17003E73 RID: 15987
		// (get) Token: 0x0600C1BC RID: 49596 RVA: 0x002B48D4 File Offset: 0x002B2AD4
		// (set) Token: 0x0600C1BD RID: 49597 RVA: 0x002B491F File Offset: 0x002B2B1F
		[DefaultValue("")]
		[Localizable(true)]
		[Description("The ToolTip that will be applied to the GoToPage TextBox control")]
		[Category("Appearance")]
		[Bindable(true)]
		[NotifyParentProperty(true)]
		public string GoToPageTextBoxToolTip
		{
			get
			{
				object obj = base.ViewState["GoToPageTextBoxToolTip"];
				if (obj == null)
				{
					return this.GetLocalizationString((TreeListLocalizationStrings loc) => loc.GoToPageTextBoxToolTip);
				}
				return (string)obj;
			}
			set
			{
				base.ViewState["GoToPageTextBoxToolTip"] = value;
			}
		}

		// Token: 0x17003E74 RID: 15988
		// (get) Token: 0x0600C1BE RID: 49598 RVA: 0x002B493C File Offset: 0x002B2B3C
		// (set) Token: 0x0600C1BF RID: 49599 RVA: 0x002B498C File Offset: 0x002B2B8C
		[Description("The ToolTip that will be applied to the GoToPage input element")]
		[Localizable(true)]
		[DefaultValue("Go to Page")]
		[Category("Appearance")]
		[Bindable(true)]
		[NotifyParentProperty(true)]
		public string GoToPageButtonToolTip
		{
			get
			{
				object obj = base.ViewState["GoToPageButtonToolTip"];
				if (obj == null)
				{
					return this.GetLocalizationString((TreeListLocalizationStrings loc) => loc.GoToPageButtonToolTip, "Go to Page");
				}
				return (string)obj;
			}
			set
			{
				base.ViewState["GoToPageButtonToolTip"] = value;
			}
		}

		// Token: 0x17003E75 RID: 15989
		// (get) Token: 0x0600C1C0 RID: 49600 RVA: 0x002B49A8 File Offset: 0x002B2BA8
		// (set) Token: 0x0600C1C1 RID: 49601 RVA: 0x002B49F3 File Offset: 0x002B2BF3
		[Category("Appearance")]
		[Bindable(true)]
		[NotifyParentProperty(true)]
		[Localizable(true)]
		[Description("The ToolTip that will be applied to the ChangePageSize TextBox control")]
		[DefaultValue("")]
		public string ChangePageSizeTextBoxToolTip
		{
			get
			{
				object obj = base.ViewState["ChangePageSizeTextBoxToolTip"];
				if (obj == null)
				{
					return this.GetLocalizationString((TreeListLocalizationStrings loc) => loc.ChangePageSizeTextBoxToolTip);
				}
				return (string)obj;
			}
			set
			{
				base.ViewState["ChangePageSizeTextBoxToolTip"] = value;
			}
		}

		// Token: 0x17003E76 RID: 15990
		// (get) Token: 0x0600C1C2 RID: 49602 RVA: 0x002B4A10 File Offset: 0x002B2C10
		// (set) Token: 0x0600C1C3 RID: 49603 RVA: 0x002B4A60 File Offset: 0x002B2C60
		[Localizable(true)]
		[DefaultValue("Change Page Size")]
		[NotifyParentProperty(true)]
		[Bindable(true)]
		[Description("The ToolTip that will be applied to the ChangePageSize Button control")]
		[Category("Appearance")]
		public string ChangePageSizeButtonToolTip
		{
			get
			{
				object obj = base.ViewState["ChangePageSizeButtonToolTip"];
				if (obj == null)
				{
					return this.GetLocalizationString((TreeListLocalizationStrings loc) => loc.ChangePageSizeButtonToolTip, "Change Page Size");
				}
				return (string)obj;
			}
			set
			{
				base.ViewState["ChangePageSizeButtonToolTip"] = value;
			}
		}

		// Token: 0x17003E77 RID: 15991
		// (get) Token: 0x0600C1C4 RID: 49604 RVA: 0x002B4A7C File Offset: 0x002B2C7C
		// (set) Token: 0x0600C1C5 RID: 49605 RVA: 0x002B4ACC File Offset: 0x002B2CCC
		[Bindable(true)]
		[DefaultValue("")]
		[NotifyParentProperty(true)]
		[Localizable(true)]
		[Description("The summary attribute that will be applied to the table which holds the ChangePageSize RadComboBox control")]
		[Category("Appearance")]
		public string ChangePageSizeComboBoxTableSummary
		{
			get
			{
				object obj = base.ViewState["ChangePageSizeComboBoxTableSummary"];
				if (obj == null)
				{
					return this.GetLocalizationString((TreeListLocalizationStrings loc) => loc.ChangePageSizeComboBoxTableSummary, "");
				}
				return (string)obj;
			}
			set
			{
				base.ViewState["ChangePageSizeComboBoxTableSummary"] = value;
			}
		}

		// Token: 0x17003E78 RID: 15992
		// (get) Token: 0x0600C1C6 RID: 49606 RVA: 0x002B4AE8 File Offset: 0x002B2CE8
		// (set) Token: 0x0600C1C7 RID: 49607 RVA: 0x002B4B38 File Offset: 0x002B2D38
		[NotifyParentProperty(true)]
		[Category("Appearance")]
		[Bindable(true)]
		[Localizable(true)]
		[Description("The ToolTip that will be applied to the input element in the ChangePageSize RadComboBox control")]
		[DefaultValue("")]
		public string ChangePageSizeComboBoxToolTip
		{
			get
			{
				object obj = base.ViewState["ChangePageSizeComboBoxToolTip"];
				if (obj == null)
				{
					return this.GetLocalizationString((TreeListLocalizationStrings loc) => loc.ChangePageSizeComboBoxToolTip, "");
				}
				return (string)obj;
			}
			set
			{
				base.ViewState["ChangePageSizeComboBoxToolTip"] = value;
			}
		}

		// Token: 0x17003E79 RID: 15993
		// (get) Token: 0x0600C1C8 RID: 49608 RVA: 0x002B4B4C File Offset: 0x002B2D4C
		// (set) Token: 0x0600C1C9 RID: 49609 RVA: 0x002B4B76 File Offset: 0x002B2D76
		[DefaultValue(10)]
		[Category("Behavior")]
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

		// Token: 0x17003E7A RID: 15994
		// (get) Token: 0x0600C1CA RID: 49610 RVA: 0x002B4BA0 File Offset: 0x002B2DA0
		// (set) Token: 0x0600C1CB RID: 49611 RVA: 0x002B4BC9 File Offset: 0x002B2DC9
		[Category("Layout")]
		[DefaultValue(typeof(TreeListPagerPosition), "Bottom")]
		[NotifyParentProperty(true)]
		[Bindable(true)]
		public TreeListPagerPosition Position
		{
			get
			{
				object obj = base.ViewState["Position"];
				if (obj == null)
				{
					return TreeListPagerPosition.Bottom;
				}
				return (TreeListPagerPosition)obj;
			}
			set
			{
				if (value < TreeListPagerPosition.Bottom || value > TreeListPagerPosition.TopAndBottom)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				base.ViewState["Position"] = value;
			}
		}

		// Token: 0x17003E7B RID: 15995
		// (get) Token: 0x0600C1CC RID: 49612 RVA: 0x002B4BF4 File Offset: 0x002B2DF4
		// (set) Token: 0x0600C1CD RID: 49613 RVA: 0x002B4C21 File Offset: 0x002B2E21
		[Bindable(true)]
		[DefaultValue(false)]
		[Description("RadTreeListPagerStyle_Visible")]
		[NotifyParentProperty(true)]
		[Category("Appearance")]
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

		// Token: 0x17003E7C RID: 15996
		// (get) Token: 0x0600C1CE RID: 49614 RVA: 0x002B4C3C File Offset: 0x002B2E3C
		// (set) Token: 0x0600C1CF RID: 49615 RVA: 0x002B4C74 File Offset: 0x002B2E74
		[DefaultValue("Increase")]
		[Category("Appearance")]
		[Bindable(true)]
		[NotifyParentProperty(true)]
		[Localizable(true)]
		[Description("PageSliderIncreaseToolTip")]
		public string PageSliderIncreaseToolTip
		{
			get
			{
				object obj = base.ViewState["PageSliderIncreaseToolTip"];
				if (obj == null)
				{
					return this.OwnerTreeList.Localization.PageSliderIncreaseToolTip;
				}
				return (string)obj;
			}
			set
			{
				base.ViewState["PageSliderIncreaseToolTip"] = value;
			}
		}

		// Token: 0x17003E7D RID: 15997
		// (get) Token: 0x0600C1D0 RID: 49616 RVA: 0x002B4C88 File Offset: 0x002B2E88
		// (set) Token: 0x0600C1D1 RID: 49617 RVA: 0x002B4CC0 File Offset: 0x002B2EC0
		[DefaultValue("Decrease")]
		[Localizable(true)]
		[NotifyParentProperty(true)]
		[Description("PageSliderDecreaseToolTip")]
		[Category("Appearance")]
		[Bindable(true)]
		public string PageSliderDecreaseToolTip
		{
			get
			{
				object obj = base.ViewState["PageSliderDecreaseToolTip"];
				if (obj == null)
				{
					return this.OwnerTreeList.Localization.PageSliderDecreaseToolTip;
				}
				return (string)obj;
			}
			set
			{
				base.ViewState["PageSliderDecreaseToolTip"] = value;
			}
		}

		// Token: 0x17003E7E RID: 15998
		// (get) Token: 0x0600C1D2 RID: 49618 RVA: 0x002B4CD4 File Offset: 0x002B2ED4
		// (set) Token: 0x0600C1D3 RID: 49619 RVA: 0x002B4D0C File Offset: 0x002B2F0C
		[Description("PageSliderDragToolTip")]
		[DefaultValue("Drag")]
		[Localizable(true)]
		[Category("Appearance")]
		[Bindable(true)]
		[NotifyParentProperty(true)]
		public string PageSliderDragToolTip
		{
			get
			{
				object obj = base.ViewState["PageSliderDragToolTip"];
				if (obj == null)
				{
					return this.OwnerTreeList.Localization.PageSliderDragToolTip;
				}
				return (string)obj;
			}
			set
			{
				base.ViewState["PageSliderDragToolTip"] = value;
			}
		}

		// Token: 0x17003E7F RID: 15999
		// (get) Token: 0x0600C1D4 RID: 49620 RVA: 0x002B4D20 File Offset: 0x002B2F20
		// (set) Token: 0x0600C1D5 RID: 49621 RVA: 0x002B4D58 File Offset: 0x002B2F58
		[DefaultValue("Page <strong>{0}</strong> of <strong>{1}</strong>")]
		[Localizable(true)]
		[Description("PageSliderPagerLabel")]
		[Category("Appearance")]
		[Bindable(true)]
		[NotifyParentProperty(true)]
		public string PageSliderPagerLabel
		{
			get
			{
				object obj = base.ViewState["PageSliderPagerLabel"];
				if (obj == null)
				{
					return this.OwnerTreeList.Localization.PageSliderPagerLabel;
				}
				return (string)obj;
			}
			set
			{
				base.ViewState["PageSliderPagerLabel"] = value;
			}
		}

		// Token: 0x17003E80 RID: 16000
		// (get) Token: 0x0600C1D6 RID: 49622 RVA: 0x002B4D6C File Offset: 0x002B2F6C
		// (set) Token: 0x0600C1D7 RID: 49623 RVA: 0x002B4DA4 File Offset: 0x002B2FA4
		[DefaultValue("Page size:")]
		[NotifyParentProperty(true)]
		[Description("ChangePageSizeLabelText")]
		[Category("Appearance")]
		[Bindable(true)]
		[Localizable(true)]
		public string ChangePageSizeLabelText
		{
			get
			{
				object obj = base.ViewState["ChangePageSizeLabelText"];
				if (obj == null)
				{
					return this.OwnerTreeList.Localization.ChangePageSizeLabelText;
				}
				return (string)obj;
			}
			set
			{
				base.ViewState["ChangePageSizeLabelText"] = value;
			}
		}

		// Token: 0x17003E81 RID: 16001
		// (get) Token: 0x0600C1D8 RID: 49624 RVA: 0x002B4DB8 File Offset: 0x002B2FB8
		// (set) Token: 0x0600C1D9 RID: 49625 RVA: 0x002B4DF0 File Offset: 0x002B2FF0
		[Category("Appearance")]
		[NotifyParentProperty(true)]
		[Description("ChangePageSizeLinkButtonText")]
		[DefaultValue("Change")]
		[Bindable(true)]
		[Localizable(true)]
		public string ChangePageSizeLinkButtonText
		{
			get
			{
				object obj = base.ViewState["ChangePageSizeLinkButtonText"];
				if (obj == null)
				{
					return this.OwnerTreeList.Localization.ChangePageSizeLinkButtonText;
				}
				return (string)obj;
			}
			set
			{
				base.ViewState["ChangePageSizeLinkButtonText"] = value;
			}
		}

		// Token: 0x17003E82 RID: 16002
		// (get) Token: 0x0600C1DA RID: 49626 RVA: 0x002B4E04 File Offset: 0x002B3004
		// (set) Token: 0x0600C1DB RID: 49627 RVA: 0x002B4E3C File Offset: 0x002B303C
		[NotifyParentProperty(true)]
		[Description("GoToPageLinkButtonText")]
		[Category("Appearance")]
		[Bindable(true)]
		[DefaultValue("Go")]
		[Localizable(true)]
		public string GoToPageLinkButtonText
		{
			get
			{
				object obj = base.ViewState["GoToPageLinkButtonText"];
				if (obj == null)
				{
					return this.OwnerTreeList.Localization.GoToPageLinkButtonText;
				}
				return (string)obj;
			}
			set
			{
				base.ViewState["GoToPageLinkButtonText"] = value;
			}
		}

		// Token: 0x17003E83 RID: 16003
		// (get) Token: 0x0600C1DC RID: 49628 RVA: 0x002B4E50 File Offset: 0x002B3050
		// (set) Token: 0x0600C1DD RID: 49629 RVA: 0x002B4E88 File Offset: 0x002B3088
		[DefaultValue("Page:")]
		[Localizable(true)]
		[Category("Appearance")]
		[Bindable(true)]
		[NotifyParentProperty(true)]
		[Description("GoToPageLabelText")]
		public string GoToPageLabelText
		{
			get
			{
				object obj = base.ViewState["GoToPageLabelText"];
				if (obj == null)
				{
					return this.OwnerTreeList.Localization.GoToPageLabelText;
				}
				return (string)obj;
			}
			set
			{
				base.ViewState["GoToPageLabelText"] = value;
			}
		}

		// Token: 0x17003E84 RID: 16004
		// (get) Token: 0x0600C1DE RID: 49630 RVA: 0x002B4E9C File Offset: 0x002B309C
		// (set) Token: 0x0600C1DF RID: 49631 RVA: 0x002B4ED4 File Offset: 0x002B30D4
		[Bindable(true)]
		[Localizable(true)]
		[Category("Appearance")]
		[Description("PageOfLabelText")]
		[DefaultValue("of {0}")]
		[NotifyParentProperty(true)]
		public string PageOfLabelText
		{
			get
			{
				object obj = base.ViewState["PageOfLabelText"];
				if (obj == null)
				{
					return this.OwnerTreeList.Localization.PageOfLabelText;
				}
				return (string)obj;
			}
			set
			{
				base.ViewState["PageOfLabelText"] = value;
			}
		}

		// Token: 0x17003E85 RID: 16005
		// (get) Token: 0x0600C1E0 RID: 49632 RVA: 0x002B4EE7 File Offset: 0x002B30E7
		// (set) Token: 0x0600C1E1 RID: 49633 RVA: 0x002B4F12 File Offset: 0x002B3112
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
