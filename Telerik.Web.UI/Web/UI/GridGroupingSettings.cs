using System;
using System.ComponentModel;
using System.Web.UI;
using Telerik.Web.UI.Functions;

namespace Telerik.Web.UI
{
	// Token: 0x02001109 RID: 4361
	[TypeConverter(typeof(ExpandableObjectConverter))]
	public class GridGroupingSettings : ObjectWithState
	{
		// Token: 0x0600B26B RID: 45675 RVA: 0x0026D516 File Offset: 0x0026B716
		public GridGroupingSettings(StateBag ownerStateBag) : this(null, ownerStateBag)
		{
		}

		// Token: 0x0600B26C RID: 45676 RVA: 0x0026D520 File Offset: 0x0026B720
		public GridGroupingSettings(RadGrid ownerGrid, StateBag ownerStateBag) : base("ggs_", ownerStateBag)
		{
			this._ownerGrid = ownerGrid;
		}

		// Token: 0x0600B26D RID: 45677 RVA: 0x0026D535 File Offset: 0x0026B735
		private string GetLocalizationString(TFunc<GridStrings, string> extractor)
		{
			if (this._ownerGrid != null)
			{
				return extractor(this._ownerGrid.Localization);
			}
			return string.Empty;
		}

		// Token: 0x170039C5 RID: 14789
		// (get) Token: 0x0600B26E RID: 45678 RVA: 0x0026D560 File Offset: 0x0026B760
		// (set) Token: 0x0600B26F RID: 45679 RVA: 0x0026D5AB File Offset: 0x0026B7AB
		[Localizable(true)]
		[Description("String to format the information label that appears on each group hedea of a group that is split and continues on the next page")]
		[NotifyParentProperty(true)]
		[DefaultValue(" Group continues on the next page.")]
		public string GroupContinuesFormatString
		{
			get
			{
				object obj;
				if ((obj = base.ViewState["_gcfs1"]) == null)
				{
					obj = this.GetLocalizationString((GridStrings loc) => loc.GroupContinuesFormatString);
				}
				object obj2 = obj;
				return (string)obj2;
			}
			set
			{
				base.ViewState["_gcfs1"] = value;
			}
		}

		// Token: 0x170039C6 RID: 14790
		// (get) Token: 0x0600B270 RID: 45680 RVA: 0x0026D5C8 File Offset: 0x0026B7C8
		// (set) Token: 0x0600B271 RID: 45681 RVA: 0x0026D613 File Offset: 0x0026B813
		[DefaultValue("... group continued from the previous page. ")]
		[Localizable(true)]
		[Description("String to format the information label that appears on each group hedea of a groupthat is split and continued from the previous page.")]
		[NotifyParentProperty(true)]
		public string GroupContinuedFormatString
		{
			get
			{
				object obj;
				if ((obj = base.ViewState["_gcfs"]) == null)
				{
					obj = this.GetLocalizationString((GridStrings loc) => loc.GroupContinuedFormatString);
				}
				object obj2 = obj;
				return (string)obj2;
			}
			set
			{
				base.ViewState["_gcfs"] = value;
			}
		}

		// Token: 0x170039C7 RID: 14791
		// (get) Token: 0x0600B272 RID: 45682 RVA: 0x0026D630 File Offset: 0x0026B830
		// (set) Token: 0x0600B273 RID: 45683 RVA: 0x0026D67B File Offset: 0x0026B87B
		[Description("A part of the string that formats the information label that appears on each group heder of a groupthat is split onto several pagesparameter {0} will be replaced with the number of actual items displayed on the pageparameter {1} will be replaced with the number of all items in the group")]
		[DefaultValue("Showing {0} of {1} items.")]
		[NotifyParentProperty(true)]
		[Localizable(true)]
		public string GroupSplitDisplayFormat
		{
			get
			{
				object obj;
				if ((obj = base.ViewState["_gsdf"]) == null)
				{
					obj = this.GetLocalizationString((GridStrings loc) => loc.GroupSplitDisplayFormat);
				}
				object obj2 = obj;
				return (string)obj2;
			}
			set
			{
				base.ViewState["_gsdf"] = value;
			}
		}

		// Token: 0x170039C8 RID: 14792
		// (get) Token: 0x0600B274 RID: 45684 RVA: 0x0026D698 File Offset: 0x0026B898
		// (set) Token: 0x0600B275 RID: 45685 RVA: 0x0026D6E3 File Offset: 0x0026B8E3
		[NotifyParentProperty(true)]
		[DefaultValue(" ({0})")]
		[Localizable(true)]
		public string GroupSplitFormat
		{
			get
			{
				object obj;
				if ((obj = base.ViewState["_gsf"]) == null)
				{
					obj = this.GetLocalizationString((GridStrings loc) => loc.GroupSplitFormat);
				}
				object obj2 = obj;
				return (string)obj2;
			}
			set
			{
				base.ViewState["_gsf"] = value;
			}
		}

		// Token: 0x170039C9 RID: 14793
		// (get) Token: 0x0600B276 RID: 45686 RVA: 0x0026D700 File Offset: 0x0026B900
		// (set) Token: 0x0600B277 RID: 45687 RVA: 0x0026D74B File Offset: 0x0026B94B
		[NotifyParentProperty(true)]
		[DefaultValue("; ")]
		[Localizable(true)]
		public string GroupByFieldsSeparator
		{
			get
			{
				object obj;
				if ((obj = base.ViewState["_gbfs"]) == null)
				{
					obj = this.GetLocalizationString((GridStrings loc) => loc.GroupByFieldsSeparator);
				}
				object obj2 = obj;
				return (string)obj2;
			}
			set
			{
				base.ViewState["_gbfs"] = value;
			}
		}

		// Token: 0x170039CA RID: 14794
		// (get) Token: 0x0600B278 RID: 45688 RVA: 0x0026D760 File Offset: 0x0026B960
		// (set) Token: 0x0600B279 RID: 45689 RVA: 0x0026D78E File Offset: 0x0026B98E
		[NotifyParentProperty(true)]
		[DefaultValue(true)]
		public bool CaseSensitive
		{
			get
			{
				object obj = base.ViewState["_cs"];
				if (obj == null)
				{
					obj = true;
				}
				return (bool)obj;
			}
			set
			{
				base.ViewState["_cs"] = value;
			}
		}

		// Token: 0x170039CB RID: 14795
		// (get) Token: 0x0600B27A RID: 45690 RVA: 0x0026D7B0 File Offset: 0x0026B9B0
		// (set) Token: 0x0600B27B RID: 45691 RVA: 0x0026D7FB File Offset: 0x0026B9FB
		[Localizable(true)]
		[Description("Expand group tooltip")]
		[DefaultValue("Expand group")]
		[Category("Grouping")]
		[NotifyParentProperty(true)]
		public virtual string ExpandTooltip
		{
			get
			{
				object obj = base.ViewState["_extt"];
				if (obj != null)
				{
					return (string)obj;
				}
				return this.GetLocalizationString((GridStrings loc) => loc.ExpandTooltip);
			}
			set
			{
				base.ViewState["_extt"] = value;
			}
		}

		// Token: 0x170039CC RID: 14796
		// (get) Token: 0x0600B27C RID: 45692 RVA: 0x0026D818 File Offset: 0x0026BA18
		// (set) Token: 0x0600B27D RID: 45693 RVA: 0x0026D863 File Offset: 0x0026BA63
		[Description("Expand all groups tooltip")]
		[DefaultValue("Expand all groups")]
		[Category("Grouping")]
		[NotifyParentProperty(true)]
		[Localizable(true)]
		public virtual string ExpandAllTooltip
		{
			get
			{
				object obj = base.ViewState["_exatt"];
				if (obj != null)
				{
					return (string)obj;
				}
				return this.GetLocalizationString((GridStrings loc) => loc.ExpandAllTooltip);
			}
			set
			{
				base.ViewState["_exatt"] = value;
			}
		}

		// Token: 0x170039CD RID: 14797
		// (get) Token: 0x0600B27E RID: 45694 RVA: 0x0026D880 File Offset: 0x0026BA80
		// (set) Token: 0x0600B27F RID: 45695 RVA: 0x0026D8CB File Offset: 0x0026BACB
		[Description("Collapse group tooltip")]
		[DefaultValue("Collapse group")]
		[Category("Grouping")]
		[Localizable(true)]
		[NotifyParentProperty(true)]
		public virtual string CollapseTooltip
		{
			get
			{
				object obj = base.ViewState["_cltt"];
				if (obj != null)
				{
					return (string)obj;
				}
				return this.GetLocalizationString((GridStrings loc) => loc.CollapseTooltip);
			}
			set
			{
				base.ViewState["_cltt"] = value;
			}
		}

		// Token: 0x170039CE RID: 14798
		// (get) Token: 0x0600B280 RID: 45696 RVA: 0x0026D8E8 File Offset: 0x0026BAE8
		// (set) Token: 0x0600B281 RID: 45697 RVA: 0x0026D933 File Offset: 0x0026BB33
		[Description("Collapse all groups tooltip")]
		[DefaultValue("Collapse group")]
		[NotifyParentProperty(true)]
		[Localizable(true)]
		[Category("Grouping")]
		public virtual string CollapseAllTooltip
		{
			get
			{
				object obj = base.ViewState["_clatt"];
				if (obj != null)
				{
					return (string)obj;
				}
				return this.GetLocalizationString((GridStrings loc) => loc.CollapseAllTooltip);
			}
			set
			{
				base.ViewState["_clatt"] = value;
			}
		}

		// Token: 0x170039CF RID: 14799
		// (get) Token: 0x0600B282 RID: 45698 RVA: 0x0026D950 File Offset: 0x0026BB50
		// (set) Token: 0x0600B283 RID: 45699 RVA: 0x0026D99B File Offset: 0x0026BB9B
		[Category("Grouping")]
		[DefaultValue("Drag out of the bar to ungroup")]
		[NotifyParentProperty(true)]
		[Localizable(true)]
		[Description("Drag out of the bar to ungroup")]
		public virtual string UnGroupTooltip
		{
			get
			{
				object obj = base.ViewState["_ugtt"];
				if (obj != null)
				{
					return (string)obj;
				}
				return this.GetLocalizationString((GridStrings loc) => loc.UnGroupTooltip);
			}
			set
			{
				base.ViewState["_ugtt"] = value;
			}
		}

		// Token: 0x170039D0 RID: 14800
		// (get) Token: 0x0600B284 RID: 45700 RVA: 0x0026D9AE File Offset: 0x0026BBAE
		internal bool UnGroupTooltipSet
		{
			get
			{
				return base.ViewState["_ugtt"] != null;
			}
		}

		// Token: 0x170039D1 RID: 14801
		// (get) Token: 0x0600B285 RID: 45701 RVA: 0x0026D9D0 File Offset: 0x0026BBD0
		// (set) Token: 0x0600B286 RID: 45702 RVA: 0x0026DA1B File Offset: 0x0026BC1B
		[Description("Click here to ungroup")]
		[Category("Grouping")]
		[NotifyParentProperty(true)]
		[Localizable(true)]
		[DefaultValue("Click here to ungroup")]
		public virtual string UnGroupButtonTooltip
		{
			get
			{
				object obj = base.ViewState["UnGroupButtonTooltip"];
				if (obj != null)
				{
					return (string)obj;
				}
				return this.GetLocalizationString((GridStrings loc) => loc.UnGroupButtonTooltip);
			}
			set
			{
				base.ViewState["UnGroupButtonTooltip"] = value;
			}
		}

		// Token: 0x170039D2 RID: 14802
		// (get) Token: 0x0600B287 RID: 45703 RVA: 0x0026DA2E File Offset: 0x0026BC2E
		internal bool UnGroupButtonTooltipSet
		{
			get
			{
				return base.ViewState["UnGroupButtonTooltip"] != null;
			}
		}

		// Token: 0x170039D3 RID: 14803
		// (get) Token: 0x0600B288 RID: 45704 RVA: 0x0026DA48 File Offset: 0x0026BC48
		// (set) Token: 0x0600B289 RID: 45705 RVA: 0x0026DA71 File Offset: 0x0026BC71
		[DefaultValue(false)]
		[NotifyParentProperty(true)]
		[Description("Shows or hides ungroup button of group panel item")]
		[Category("Grouping")]
		public virtual bool ShowUnGroupButton
		{
			get
			{
				object obj = base.ViewState["ShowUnGroupButton"];
				return obj != null && (bool)obj;
			}
			set
			{
				base.ViewState["ShowUnGroupButton"] = value;
			}
		}

		// Token: 0x170039D4 RID: 14804
		// (get) Token: 0x0600B28A RID: 45706 RVA: 0x0026DA8C File Offset: 0x0026BC8C
		// (set) Token: 0x0600B28B RID: 45707 RVA: 0x0026DAB5 File Offset: 0x0026BCB5
		[DefaultValue(false)]
		[Description("Gets or sets value indicating if group aggregates should not depend on the current page.")]
		[Category("Grouping")]
		[NotifyParentProperty(true)]
		public virtual bool IgnorePagingForGroupAggregates
		{
			get
			{
				object obj = base.ViewState["_ipfga"];
				return obj != null && (bool)obj;
			}
			set
			{
				base.ViewState["_ipfga"] = value;
			}
		}

		// Token: 0x170039D5 RID: 14805
		// (get) Token: 0x0600B28C RID: 45708 RVA: 0x0026DAD0 File Offset: 0x0026BCD0
		// (set) Token: 0x0600B28D RID: 45709 RVA: 0x0026DAF9 File Offset: 0x0026BCF9
		[NotifyParentProperty(true)]
		[Category("Grouping")]
		[Description("Keep group footers visible/hidden on collapse.")]
		[DefaultValue(false)]
		public virtual bool RetainGroupFootersVisibility
		{
			get
			{
				object obj = base.ViewState["RetainGroupFootersVisibility"];
				return obj != null && (bool)obj;
			}
			set
			{
				base.ViewState["RetainGroupFootersVisibility"] = value;
			}
		}

		// Token: 0x170039D6 RID: 14806
		// (get) Token: 0x0600B28E RID: 45710 RVA: 0x0026DB11 File Offset: 0x0026BD11
		// (set) Token: 0x0600B28F RID: 45711 RVA: 0x0026DB31 File Offset: 0x0026BD31
		[Description("The summary attribute for the table that wraps the GridGroupPanel")]
		[DefaultValue("")]
		[Category("Grouping")]
		[NotifyParentProperty(true)]
		public virtual string MainTableSummary
		{
			get
			{
				return (base.ViewState["MainTableSummary"] as string) ?? string.Empty;
			}
			set
			{
				base.ViewState["MainTableSummary"] = value;
			}
		}

		// Token: 0x170039D7 RID: 14807
		// (get) Token: 0x0600B290 RID: 45712 RVA: 0x0026DB44 File Offset: 0x0026BD44
		// (set) Token: 0x0600B291 RID: 45713 RVA: 0x0026DB64 File Offset: 0x0026BD64
		[NotifyParentProperty(true)]
		[DefaultValue("")]
		[Description("The summary attribute for the table second level table in the GridGroupPanel")]
		[Category("Grouping")]
		public virtual string NestedTableSummary
		{
			get
			{
				return (base.ViewState["NestedTableSummary"] as string) ?? string.Empty;
			}
			set
			{
				base.ViewState["NestedTableSummary"] = value;
			}
		}

		// Token: 0x170039D8 RID: 14808
		// (get) Token: 0x0600B292 RID: 45714 RVA: 0x0026DB77 File Offset: 0x0026BD77
		// (set) Token: 0x0600B293 RID: 45715 RVA: 0x0026DB97 File Offset: 0x0026BD97
		[Description("The summary attribute for the table which holds all group items in the GridGroupPanel")]
		[DefaultValue("")]
		[Category("Grouping")]
		[NotifyParentProperty(true)]
		public virtual string GroupItemsWrapperTableSummary
		{
			get
			{
				return (base.ViewState["GroupItemsWrapperTableSummary"] as string) ?? string.Empty;
			}
			set
			{
				base.ViewState["GroupItemsWrapperTableSummary"] = value;
			}
		}

		// Token: 0x170039D9 RID: 14809
		// (get) Token: 0x0600B294 RID: 45716 RVA: 0x0026DBAA File Offset: 0x0026BDAA
		// (set) Token: 0x0600B295 RID: 45717 RVA: 0x0026DBCA File Offset: 0x0026BDCA
		[Category("Grouping")]
		[Description("The caption for the table that wraps the GridGroupPanel")]
		[NotifyParentProperty(true)]
		[DefaultValue("")]
		public virtual string MainTableCaption
		{
			get
			{
				return (base.ViewState["MainTableCaption"] as string) ?? string.Empty;
			}
			set
			{
				base.ViewState["MainTableCaption"] = value;
			}
		}

		// Token: 0x170039DA RID: 14810
		// (get) Token: 0x0600B296 RID: 45718 RVA: 0x0026DBDD File Offset: 0x0026BDDD
		// (set) Token: 0x0600B297 RID: 45719 RVA: 0x0026DBFD File Offset: 0x0026BDFD
		[NotifyParentProperty(true)]
		[Category("Grouping")]
		[Description("The caption for the table second level table in the GridGroupPanel")]
		[DefaultValue("")]
		public virtual string NestedTableCaption
		{
			get
			{
				return (base.ViewState["NestedTableCaption"] as string) ?? string.Empty;
			}
			set
			{
				base.ViewState["NestedTableCaption"] = value;
			}
		}

		// Token: 0x170039DB RID: 14811
		// (get) Token: 0x0600B298 RID: 45720 RVA: 0x0026DC10 File Offset: 0x0026BE10
		// (set) Token: 0x0600B299 RID: 45721 RVA: 0x0026DC30 File Offset: 0x0026BE30
		[Category("Grouping")]
		[Description("The caption for the table which holds all group items in the GridGroupPanel")]
		[NotifyParentProperty(true)]
		[DefaultValue("")]
		public virtual string GroupItemsWrapperTableCaption
		{
			get
			{
				return (base.ViewState["GroupItemsWrapperTableCaption"] as string) ?? string.Empty;
			}
			set
			{
				base.ViewState["GroupItemsWrapperTableCaption"] = value;
			}
		}

		// Token: 0x04002EFE RID: 12030
		private const string _groupContinuesFormatString = " Group continues on the next page.";

		// Token: 0x04002EFF RID: 12031
		private const string _groupContinuedFormatString = "... group continued from the previous page. ";

		// Token: 0x04002F00 RID: 12032
		private const string _groupSplitDisplayFormat = "Showing {0} of {1} items.";

		// Token: 0x04002F01 RID: 12033
		private readonly RadGrid _ownerGrid;
	}
}
