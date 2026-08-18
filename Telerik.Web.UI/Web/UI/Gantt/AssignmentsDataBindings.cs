using System;
using System.ComponentModel;

namespace Telerik.Web.UI.Gantt
{
	// Token: 0x02000310 RID: 784
	public class AssignmentsDataBindings : BaseDataBindings, IAssignmentsDataBindings
	{
		// Token: 0x170008E7 RID: 2279
		// (get) Token: 0x06001A84 RID: 6788 RVA: 0x0005694C File Offset: 0x00054B4C
		// (set) Token: 0x06001A85 RID: 6789 RVA: 0x0005696C File Offset: 0x00054B6C
		[DefaultValue("")]
		[RequiredProperty]
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

		// Token: 0x170008E8 RID: 2280
		// (get) Token: 0x06001A86 RID: 6790 RVA: 0x0005697F File Offset: 0x00054B7F
		// (set) Token: 0x06001A87 RID: 6791 RVA: 0x0005699F File Offset: 0x00054B9F
		[DefaultValue("")]
		[RequiredProperty]
		public string TaskIdField
		{
			get
			{
				return (string)(base.ViewState["TaskIdField"] ?? string.Empty);
			}
			set
			{
				base.ViewState["TaskIdField"] = value;
			}
		}

		// Token: 0x170008E9 RID: 2281
		// (get) Token: 0x06001A88 RID: 6792 RVA: 0x000569B2 File Offset: 0x00054BB2
		// (set) Token: 0x06001A89 RID: 6793 RVA: 0x000569D2 File Offset: 0x00054BD2
		[RequiredProperty]
		[DefaultValue("")]
		public string ResourceIdField
		{
			get
			{
				return (string)(base.ViewState["ResourceIdField"] ?? string.Empty);
			}
			set
			{
				base.ViewState["ResourceIdField"] = value;
			}
		}

		// Token: 0x170008EA RID: 2282
		// (get) Token: 0x06001A8A RID: 6794 RVA: 0x000569E5 File Offset: 0x00054BE5
		// (set) Token: 0x06001A8B RID: 6795 RVA: 0x00056A05 File Offset: 0x00054C05
		[DefaultValue("")]
		[RequiredProperty]
		public string UnitsField
		{
			get
			{
				return (string)(base.ViewState["UnitsField"] ?? string.Empty);
			}
			set
			{
				base.ViewState["UnitsField"] = value;
			}
		}
	}
}
