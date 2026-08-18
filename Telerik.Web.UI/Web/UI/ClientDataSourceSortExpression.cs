using System;
using System.ComponentModel;

namespace Telerik.Web.UI
{
	// Token: 0x0200011D RID: 285
	public class ClientDataSourceSortExpression : StateManager
	{
		// Token: 0x17000403 RID: 1027
		// (get) Token: 0x06000B7B RID: 2939 RVA: 0x00028524 File Offset: 0x00026724
		// (set) Token: 0x06000B7C RID: 2940 RVA: 0x00028544 File Offset: 0x00026744
		[Description("Gets or sets the field name for the sort operation.")]
		[Category("Behavior")]
		[DefaultValue("")]
		public string FieldName
		{
			get
			{
				return (base.ViewState["FieldName"] as string) ?? string.Empty;
			}
			set
			{
				base.ViewState["FieldName"] = value;
			}
		}

		// Token: 0x17000404 RID: 1028
		// (get) Token: 0x06000B7D RID: 2941 RVA: 0x00028557 File Offset: 0x00026757
		// (set) Token: 0x06000B7E RID: 2942 RVA: 0x00028582 File Offset: 0x00026782
		[DefaultValue(ClientDataSourceSortOrder.Asc)]
		[Category("Behavior")]
		[Description("Gets or sets the sort order.")]
		public ClientDataSourceSortOrder SortOrder
		{
			get
			{
				if (base.ViewState["SortOrder"] != null)
				{
					return (ClientDataSourceSortOrder)base.ViewState["SortOrder"];
				}
				return ClientDataSourceSortOrder.Asc;
			}
			set
			{
				base.ViewState["SortOrder"] = value;
			}
		}
	}
}
