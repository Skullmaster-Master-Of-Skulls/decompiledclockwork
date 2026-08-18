using System;
using System.ComponentModel;

namespace Telerik.Web.UI.Gantt
{
	// Token: 0x02000352 RID: 850
	[TypeConverter(typeof(ExpandableObjectConverter))]
	public class DependenciesDataBindings : BaseDataBindings, IDependenciesDataBinding
	{
		// Token: 0x17000A00 RID: 2560
		// (get) Token: 0x06001D67 RID: 7527 RVA: 0x0005CB12 File Offset: 0x0005AD12
		// (set) Token: 0x06001D68 RID: 7528 RVA: 0x0005CB32 File Offset: 0x0005AD32
		[DefaultValue("")]
		[RequiredProperty]
		public string IdField
		{
			get
			{
				return (string)(base.ViewState["FieldId"] ?? string.Empty);
			}
			set
			{
				base.ViewState["FieldId"] = value;
			}
		}

		// Token: 0x17000A01 RID: 2561
		// (get) Token: 0x06001D69 RID: 7529 RVA: 0x0005CB45 File Offset: 0x0005AD45
		// (set) Token: 0x06001D6A RID: 7530 RVA: 0x0005CB65 File Offset: 0x0005AD65
		[RequiredProperty]
		[DefaultValue("")]
		public string SuccessorIdField
		{
			get
			{
				return (string)(base.ViewState["FieldSuccessorId"] ?? string.Empty);
			}
			set
			{
				base.ViewState["FieldSuccessorId"] = value;
			}
		}

		// Token: 0x17000A02 RID: 2562
		// (get) Token: 0x06001D6B RID: 7531 RVA: 0x0005CB78 File Offset: 0x0005AD78
		// (set) Token: 0x06001D6C RID: 7532 RVA: 0x0005CB98 File Offset: 0x0005AD98
		[RequiredProperty]
		[DefaultValue("")]
		public string PredecessorIdField
		{
			get
			{
				return (string)(base.ViewState["FieldPredecessorId"] ?? string.Empty);
			}
			set
			{
				base.ViewState["FieldPredecessorId"] = value;
			}
		}

		// Token: 0x17000A03 RID: 2563
		// (get) Token: 0x06001D6D RID: 7533 RVA: 0x0005CBAB File Offset: 0x0005ADAB
		// (set) Token: 0x06001D6E RID: 7534 RVA: 0x0005CBCB File Offset: 0x0005ADCB
		[DefaultValue("")]
		[RequiredProperty]
		public string TypeField
		{
			get
			{
				return (string)(base.ViewState["FieldDependectyType"] ?? string.Empty);
			}
			set
			{
				base.ViewState["FieldDependectyType"] = value;
			}
		}
	}
}
