using System;
using System.ComponentModel;

namespace Telerik.Web.UI
{
	// Token: 0x02000E14 RID: 3604
	[TypeConverter(typeof(ExpandableObjectConverter))]
	public class PivotGridTotalsSettings : StateManager
	{
		// Token: 0x060086EF RID: 34543 RVA: 0x001E909A File Offset: 0x001E729A
		public PivotGridTotalsSettings(RadPivotGrid owner)
		{
			this.owner = owner;
		}

		// Token: 0x17002AE3 RID: 10979
		// (get) Token: 0x060086F0 RID: 34544 RVA: 0x001E90AC File Offset: 0x001E72AC
		// (set) Token: 0x060086F1 RID: 34545 RVA: 0x001E90E4 File Offset: 0x001E72E4
		[Description("Gets or sets the grand total text value.")]
		[NotifyParentProperty(true)]
		[Category("Appearance")]
		[DefaultValue("Grand Total")]
		public virtual string GrandTotalText
		{
			get
			{
				object obj = base.ViewState["GrandTotalText"];
				if (obj != null)
				{
					return obj.ToString();
				}
				return this.owner.Localization.GrandTotalText;
			}
			set
			{
				base.ViewState["GrandTotalText"] = value;
			}
		}

		// Token: 0x17002AE4 RID: 10980
		// (get) Token: 0x060086F2 RID: 34546 RVA: 0x001E90F8 File Offset: 0x001E72F8
		// (set) Token: 0x060086F3 RID: 34547 RVA: 0x001E9130 File Offset: 0x001E7330
		[NotifyParentProperty(true)]
		[DefaultValue("Total {0}")]
		[Category("Appearance")]
		[Description("Gets or sets the column total group name format text.")]
		public virtual string TotalValueFormat
		{
			get
			{
				object obj = base.ViewState["TotalValueFormat"];
				if (obj != null)
				{
					return obj.ToString();
				}
				return this.owner.Localization.TotalValueFormat;
			}
			set
			{
				base.ViewState["TotalValueFormat"] = value;
			}
		}

		// Token: 0x17002AE5 RID: 10981
		// (get) Token: 0x060086F4 RID: 34548 RVA: 0x001E9144 File Offset: 0x001E7344
		// (set) Token: 0x060086F5 RID: 34549 RVA: 0x001E917C File Offset: 0x001E737C
		[NotifyParentProperty(true)]
		[DefaultValue("{0} Total")]
		[Category("Appearance")]
		[Description("Gets or sets the row total group name format.")]
		public virtual string ValueTotalFormat
		{
			get
			{
				object obj = base.ViewState["ValueTotalFormat"];
				if (obj != null)
				{
					return obj.ToString();
				}
				return this.owner.Localization.ValueTotalFormat;
			}
			set
			{
				base.ViewState["ValueTotalFormat"] = value;
			}
		}

		// Token: 0x17002AE6 RID: 10982
		// (get) Token: 0x060086F6 RID: 34550 RVA: 0x001E9190 File Offset: 0x001E7390
		// (set) Token: 0x060086F7 RID: 34551 RVA: 0x001E91C8 File Offset: 0x001E73C8
		[Category("Behavior")]
		[NotifyParentProperty(true)]
		[Description("Enables/diables the visibility of the row/column grand totals")]
		[DefaultValue(PivotGridGrandTotalsVisibility.RowsAndColumns)]
		public PivotGridGrandTotalsVisibility GrandTotalsVisibility
		{
			get
			{
				object obj = base.ViewState["GrandTotalsVisibility"];
				if (obj != null)
				{
					return (PivotGridGrandTotalsVisibility)base.ViewState["GrandTotalsVisibility"];
				}
				return PivotGridGrandTotalsVisibility.RowsAndColumns;
			}
			set
			{
				base.ViewState["GrandTotalsVisibility"] = value;
			}
		}

		// Token: 0x17002AE7 RID: 10983
		// (get) Token: 0x060086F8 RID: 34552 RVA: 0x001E91E0 File Offset: 0x001E73E0
		// (set) Token: 0x060086F9 RID: 34553 RVA: 0x001E9218 File Offset: 0x001E7418
		[Description("Gets or sets all rows subtotals items position")]
		[Category("Behavior")]
		[DefaultValue(TotalsPosition.Last)]
		[NotifyParentProperty(true)]
		public TotalsPosition RowsSubTotalsPosition
		{
			get
			{
				object obj = base.ViewState["RowsSubTotalsPosition"];
				if (obj != null)
				{
					return (TotalsPosition)base.ViewState["RowsSubTotalsPosition"];
				}
				return TotalsPosition.Last;
			}
			set
			{
				base.ViewState["RowsSubTotalsPosition"] = value;
			}
		}

		// Token: 0x17002AE8 RID: 10984
		// (get) Token: 0x060086FA RID: 34554 RVA: 0x001E9230 File Offset: 0x001E7430
		// (set) Token: 0x060086FB RID: 34555 RVA: 0x001E9268 File Offset: 0x001E7468
		[DefaultValue(TotalsPosition.Last)]
		[NotifyParentProperty(true)]
		[Description("Gets or sets row grandtotals item position")]
		[Category("Behavior")]
		public TotalsPosition RowGrandTotalsPosition
		{
			get
			{
				object obj = base.ViewState["RowGrandTotalsPosition"];
				if (obj != null)
				{
					return (TotalsPosition)base.ViewState["RowGrandTotalsPosition"];
				}
				return TotalsPosition.Last;
			}
			set
			{
				base.ViewState["RowGrandTotalsPosition"] = value;
			}
		}

		// Token: 0x17002AE9 RID: 10985
		// (get) Token: 0x060086FC RID: 34556 RVA: 0x001E9280 File Offset: 0x001E7480
		// (set) Token: 0x060086FD RID: 34557 RVA: 0x001E92B8 File Offset: 0x001E74B8
		[DefaultValue(TotalsPosition.Last)]
		[NotifyParentProperty(true)]
		[Category("Behavior")]
		[Description("Gets or sets all columns subtotals items position")]
		public TotalsPosition ColumnsSubTotalsPosition
		{
			get
			{
				object obj = base.ViewState["ColumnsSubTotalsPosition"];
				if (obj != null)
				{
					return (TotalsPosition)base.ViewState["ColumnsSubTotalsPosition"];
				}
				return TotalsPosition.Last;
			}
			set
			{
				base.ViewState["ColumnsSubTotalsPosition"] = value;
			}
		}

		// Token: 0x17002AEA RID: 10986
		// (get) Token: 0x060086FE RID: 34558 RVA: 0x001E92D0 File Offset: 0x001E74D0
		// (set) Token: 0x060086FF RID: 34559 RVA: 0x001E9308 File Offset: 0x001E7508
		[Category("Behavior")]
		[Description("Gets or sets column grandtotals item position")]
		[NotifyParentProperty(true)]
		[DefaultValue(TotalsPosition.Last)]
		public TotalsPosition ColumnGrandTotalsPosition
		{
			get
			{
				object obj = base.ViewState["ColumnGrandTotalsPosition"];
				if (obj != null)
				{
					return (TotalsPosition)base.ViewState["ColumnGrandTotalsPosition"];
				}
				return TotalsPosition.Last;
			}
			set
			{
				base.ViewState["ColumnGrandTotalsPosition"] = value;
			}
		}

		// Token: 0x0400254E RID: 9550
		private RadPivotGrid owner;
	}
}
