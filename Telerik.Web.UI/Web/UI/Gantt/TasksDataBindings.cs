using System;
using System.ComponentModel;

namespace Telerik.Web.UI.Gantt
{
	// Token: 0x02000355 RID: 853
	[TypeConverter(typeof(ExpandableObjectConverter))]
	public class TasksDataBindings : BaseDataBindings, ITasksDataBindings
	{
		// Token: 0x17000A14 RID: 2580
		// (get) Token: 0x06001D91 RID: 7569 RVA: 0x0005CC93 File Offset: 0x0005AE93
		// (set) Token: 0x06001D92 RID: 7570 RVA: 0x0005CCB3 File Offset: 0x0005AEB3
		[RequiredProperty]
		[DefaultValue("")]
		public string IdField
		{
			get
			{
				return (string)(base.ViewState["IdField"] ?? string.Empty);
			}
			set
			{
				base.ViewState["IdField"] = value;
			}
		}

		// Token: 0x17000A15 RID: 2581
		// (get) Token: 0x06001D93 RID: 7571 RVA: 0x0005CCC6 File Offset: 0x0005AEC6
		// (set) Token: 0x06001D94 RID: 7572 RVA: 0x0005CCE6 File Offset: 0x0005AEE6
		[DefaultValue("")]
		public string ParentIdField
		{
			get
			{
				return (string)(base.ViewState["ParentIdField"] ?? string.Empty);
			}
			set
			{
				base.ViewState["ParentIdField"] = value;
			}
		}

		// Token: 0x17000A16 RID: 2582
		// (get) Token: 0x06001D95 RID: 7573 RVA: 0x0005CCF9 File Offset: 0x0005AEF9
		// (set) Token: 0x06001D96 RID: 7574 RVA: 0x0005CD19 File Offset: 0x0005AF19
		[DefaultValue("")]
		public string OrderIdField
		{
			get
			{
				return (string)(base.ViewState["OrderIdField"] ?? string.Empty);
			}
			set
			{
				base.ViewState["OrderIdField"] = value;
			}
		}

		// Token: 0x17000A17 RID: 2583
		// (get) Token: 0x06001D97 RID: 7575 RVA: 0x0005CD2C File Offset: 0x0005AF2C
		// (set) Token: 0x06001D98 RID: 7576 RVA: 0x0005CD4C File Offset: 0x0005AF4C
		[RequiredProperty]
		[DefaultValue("")]
		public string StartField
		{
			get
			{
				return (string)(base.ViewState["StartField"] ?? string.Empty);
			}
			set
			{
				base.ViewState["StartField"] = value;
			}
		}

		// Token: 0x17000A18 RID: 2584
		// (get) Token: 0x06001D99 RID: 7577 RVA: 0x0005CD5F File Offset: 0x0005AF5F
		// (set) Token: 0x06001D9A RID: 7578 RVA: 0x0005CD7F File Offset: 0x0005AF7F
		[DefaultValue("")]
		public string PlannedStartField
		{
			get
			{
				return (string)(base.ViewState["PlannedStartField"] ?? string.Empty);
			}
			set
			{
				base.ViewState["PlannedStartField"] = value;
			}
		}

		// Token: 0x17000A19 RID: 2585
		// (get) Token: 0x06001D9B RID: 7579 RVA: 0x0005CD92 File Offset: 0x0005AF92
		// (set) Token: 0x06001D9C RID: 7580 RVA: 0x0005CDB2 File Offset: 0x0005AFB2
		[RequiredProperty]
		[DefaultValue("")]
		public string EndField
		{
			get
			{
				return (string)(base.ViewState["EndField"] ?? string.Empty);
			}
			set
			{
				base.ViewState["EndField"] = value;
			}
		}

		// Token: 0x17000A1A RID: 2586
		// (get) Token: 0x06001D9D RID: 7581 RVA: 0x0005CDC5 File Offset: 0x0005AFC5
		// (set) Token: 0x06001D9E RID: 7582 RVA: 0x0005CDE5 File Offset: 0x0005AFE5
		[DefaultValue("")]
		public string PlannedEndField
		{
			get
			{
				return (string)(base.ViewState["PlannedEndField"] ?? string.Empty);
			}
			set
			{
				base.ViewState["PlannedEndField"] = value;
			}
		}

		// Token: 0x17000A1B RID: 2587
		// (get) Token: 0x06001D9F RID: 7583 RVA: 0x0005CDF8 File Offset: 0x0005AFF8
		// (set) Token: 0x06001DA0 RID: 7584 RVA: 0x0005CE18 File Offset: 0x0005B018
		[DefaultValue("")]
		public string PercentCompleteField
		{
			get
			{
				return (string)(base.ViewState["PercentCompleteField"] ?? string.Empty);
			}
			set
			{
				base.ViewState["PercentCompleteField"] = value;
			}
		}

		// Token: 0x17000A1C RID: 2588
		// (get) Token: 0x06001DA1 RID: 7585 RVA: 0x0005CE2B File Offset: 0x0005B02B
		// (set) Token: 0x06001DA2 RID: 7586 RVA: 0x0005CE4B File Offset: 0x0005B04B
		[DefaultValue("")]
		public string SummaryField
		{
			get
			{
				return (string)(base.ViewState["SummaryField"] ?? string.Empty);
			}
			set
			{
				base.ViewState["SummaryField"] = value;
			}
		}

		// Token: 0x17000A1D RID: 2589
		// (get) Token: 0x06001DA3 RID: 7587 RVA: 0x0005CE5E File Offset: 0x0005B05E
		// (set) Token: 0x06001DA4 RID: 7588 RVA: 0x0005CE7E File Offset: 0x0005B07E
		[DefaultValue("")]
		[RequiredProperty]
		public string TitleField
		{
			get
			{
				return (string)(base.ViewState["TitleField"] ?? string.Empty);
			}
			set
			{
				base.ViewState["TitleField"] = value;
			}
		}

		// Token: 0x17000A1E RID: 2590
		// (get) Token: 0x06001DA5 RID: 7589 RVA: 0x0005CE91 File Offset: 0x0005B091
		// (set) Token: 0x06001DA6 RID: 7590 RVA: 0x0005CEB1 File Offset: 0x0005B0B1
		[DefaultValue("")]
		public string ExpandedField
		{
			get
			{
				return (string)(base.ViewState["ExpandedField"] ?? string.Empty);
			}
			set
			{
				base.ViewState["ExpandedField"] = value;
			}
		}

		// Token: 0x06001DA7 RID: 7591 RVA: 0x0005CEC4 File Offset: 0x0005B0C4
		public override void EnsureDataFields()
		{
			base.EnsureDataFields();
			if ((!string.IsNullOrEmpty(this.ParentIdField) && string.IsNullOrEmpty(this.SummaryField)) || (string.IsNullOrEmpty(this.ParentIdField) && !string.IsNullOrEmpty(this.SummaryField)))
			{
				throw new ArgumentException("ParentIdField and SummaryField must be set simultaneously.");
			}
		}
	}
}
