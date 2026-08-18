using System;
using System.ComponentModel;

namespace Telerik.Web.UI
{
	// Token: 0x02000888 RID: 2184
	[TypeConverter(typeof(ExpandableObjectConverter))]
	public class PivotGridAccessibilitySettings : StateManager
	{
		// Token: 0x060050C5 RID: 20677 RVA: 0x000FBE04 File Offset: 0x000FA004
		public PivotGridAccessibilitySettings(RadPivotGrid owner)
		{
			this.owner = owner;
		}

		// Token: 0x17001A73 RID: 6771
		// (get) Token: 0x060050C6 RID: 20678 RVA: 0x000FBE14 File Offset: 0x000FA014
		// (set) Token: 0x060050C7 RID: 20679 RVA: 0x000FBE4C File Offset: 0x000FA04C
		[DefaultValue("")]
		[Description("")]
		[Category("Accessibility")]
		[NotifyParentProperty(true)]
		[Localizable(true)]
		public virtual string OuterTableSummary
		{
			get
			{
				string text = (string)base.ViewState["rpg_OTsumm"];
				if (text == null)
				{
					return this.owner.Localization.OuterTableSummary;
				}
				return text;
			}
			set
			{
				base.ViewState["rpg_OTsumm"] = value;
			}
		}

		// Token: 0x17001A74 RID: 6772
		// (get) Token: 0x060050C8 RID: 20680 RVA: 0x000FBE60 File Offset: 0x000FA060
		// (set) Token: 0x060050C9 RID: 20681 RVA: 0x000FBE98 File Offset: 0x000FA098
		[DefaultValue("")]
		[Description("")]
		[Category("Accessibility")]
		[NotifyParentProperty(true)]
		[Localizable(true)]
		public virtual string OuterTableCaption
		{
			get
			{
				string text = (string)base.ViewState["rpg_OTcapt"];
				if (text == null)
				{
					return this.owner.Localization.OuterTableCaption;
				}
				return text;
			}
			set
			{
				base.ViewState["rpg_OTcapt"] = value;
			}
		}

		// Token: 0x17001A75 RID: 6773
		// (get) Token: 0x060050CA RID: 20682 RVA: 0x000FBEAC File Offset: 0x000FA0AC
		// (set) Token: 0x060050CB RID: 20683 RVA: 0x000FBEE4 File Offset: 0x000FA0E4
		[DefaultValue("")]
		[Localizable(true)]
		[Description("")]
		[Category("Accessibility")]
		[NotifyParentProperty(true)]
		public virtual string ColumnHeaderTableSummary
		{
			get
			{
				string text = (string)base.ViewState["rpg_CHTsumm"];
				if (text == null)
				{
					return this.owner.Localization.ColumnHeaderTableSummary;
				}
				return text;
			}
			set
			{
				base.ViewState["rpg_CHTsumm"] = value;
			}
		}

		// Token: 0x17001A76 RID: 6774
		// (get) Token: 0x060050CC RID: 20684 RVA: 0x000FBEF8 File Offset: 0x000FA0F8
		// (set) Token: 0x060050CD RID: 20685 RVA: 0x000FBF30 File Offset: 0x000FA130
		[Description("")]
		[DefaultValue("")]
		[Category("Accessibility")]
		[NotifyParentProperty(true)]
		[Localizable(true)]
		public virtual string ColumnHeaderTableCaption
		{
			get
			{
				string text = (string)base.ViewState["rpg_CHTcapt"];
				if (text == null)
				{
					return this.owner.Localization.ColumnHeaderTableCaption;
				}
				return text;
			}
			set
			{
				base.ViewState["rpg_CHTcapt"] = value;
			}
		}

		// Token: 0x17001A77 RID: 6775
		// (get) Token: 0x060050CE RID: 20686 RVA: 0x000FBF44 File Offset: 0x000FA144
		// (set) Token: 0x060050CF RID: 20687 RVA: 0x000FBF7C File Offset: 0x000FA17C
		[Localizable(true)]
		[Description("")]
		[DefaultValue("")]
		[Category("Accessibility")]
		[NotifyParentProperty(true)]
		public virtual string RowHeaderTableSummary
		{
			get
			{
				string text = (string)base.ViewState["rpg_RHTsumm"];
				if (text == null)
				{
					return this.owner.Localization.RowHeaderTableSummary;
				}
				return text;
			}
			set
			{
				base.ViewState["rpg_RHTsumm"] = value;
			}
		}

		// Token: 0x17001A78 RID: 6776
		// (get) Token: 0x060050D0 RID: 20688 RVA: 0x000FBF90 File Offset: 0x000FA190
		// (set) Token: 0x060050D1 RID: 20689 RVA: 0x000FBFC8 File Offset: 0x000FA1C8
		[Description("")]
		[Localizable(true)]
		[DefaultValue("")]
		[Category("Accessibility")]
		[NotifyParentProperty(true)]
		public virtual string RowHeaderTableCaption
		{
			get
			{
				string text = (string)base.ViewState["rpg_RHTcapt"];
				if (text == null)
				{
					return this.owner.Localization.RowHeaderTableCaption;
				}
				return text;
			}
			set
			{
				base.ViewState["rpg_RHTcapt"] = value;
			}
		}

		// Token: 0x17001A79 RID: 6777
		// (get) Token: 0x060050D2 RID: 20690 RVA: 0x000FBFDC File Offset: 0x000FA1DC
		// (set) Token: 0x060050D3 RID: 20691 RVA: 0x000FC014 File Offset: 0x000FA214
		[Category("Accessibility")]
		[Description("")]
		[DefaultValue("")]
		[NotifyParentProperty(true)]
		[Localizable(true)]
		public virtual string DataTableSummary
		{
			get
			{
				string text = (string)base.ViewState["rpg_DTsumm"];
				if (text == null)
				{
					return this.owner.Localization.DataTableSummary;
				}
				return text;
			}
			set
			{
				base.ViewState["rpg_DTsumm"] = value;
			}
		}

		// Token: 0x17001A7A RID: 6778
		// (get) Token: 0x060050D4 RID: 20692 RVA: 0x000FC028 File Offset: 0x000FA228
		// (set) Token: 0x060050D5 RID: 20693 RVA: 0x000FC060 File Offset: 0x000FA260
		[Description("")]
		[DefaultValue("")]
		[Category("Accessibility")]
		[NotifyParentProperty(true)]
		[Localizable(true)]
		public virtual string DataTableCaption
		{
			get
			{
				string text = (string)base.ViewState["rpg_DTcapt"];
				if (text == null)
				{
					return this.owner.Localization.DataTableCaption;
				}
				return text;
			}
			set
			{
				base.ViewState["rpg_DTcapt"] = value;
			}
		}

		// Token: 0x17001A7B RID: 6779
		// (get) Token: 0x060050D6 RID: 20694 RVA: 0x000FC074 File Offset: 0x000FA274
		// (set) Token: 0x060050D7 RID: 20695 RVA: 0x000FC0AC File Offset: 0x000FA2AC
		[Localizable(true)]
		[Description("")]
		[DefaultValue("")]
		[Category("Accessibility")]
		[NotifyParentProperty(true)]
		public virtual string WrapperTableSummary
		{
			get
			{
				string text = (string)base.ViewState["rpg_WTsumm"];
				if (text == null)
				{
					return this.owner.Localization.WrapperTableSummary;
				}
				return text;
			}
			set
			{
				base.ViewState["rpg_WTsumm"] = value;
			}
		}

		// Token: 0x17001A7C RID: 6780
		// (get) Token: 0x060050D8 RID: 20696 RVA: 0x000FC0C0 File Offset: 0x000FA2C0
		// (set) Token: 0x060050D9 RID: 20697 RVA: 0x000FC0F8 File Offset: 0x000FA2F8
		[Category("Accessibility")]
		[DefaultValue("")]
		[Description("")]
		[NotifyParentProperty(true)]
		[Localizable(true)]
		public virtual string WrapperTableCaption
		{
			get
			{
				string text = (string)base.ViewState["rpg_WTcapt"];
				if (text == null)
				{
					return this.owner.Localization.WrapperTableCaption;
				}
				return text;
			}
			set
			{
				base.ViewState["rpg_WTcapt"] = value;
			}
		}

		// Token: 0x040013EE RID: 5102
		private readonly RadPivotGrid owner;
	}
}
