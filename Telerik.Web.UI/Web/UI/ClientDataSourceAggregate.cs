using System;
using System.ComponentModel;

namespace Telerik.Web.UI
{
	// Token: 0x02000114 RID: 276
	public class ClientDataSourceAggregate : StateManager
	{
		// Token: 0x170003F6 RID: 1014
		// (get) Token: 0x06000B59 RID: 2905 RVA: 0x00027BEC File Offset: 0x00025DEC
		// (set) Token: 0x06000B5A RID: 2906 RVA: 0x00027C0C File Offset: 0x00025E0C
		[DefaultValue("")]
		[Description("Gets or sets the field name for the operation.")]
		[Category("Behavior")]
		public string Field
		{
			get
			{
				return (base.ViewState["Field"] as string) ?? string.Empty;
			}
			set
			{
				base.ViewState["Field"] = value;
			}
		}

		// Token: 0x170003F7 RID: 1015
		// (get) Token: 0x06000B5B RID: 2907 RVA: 0x00027C1F File Offset: 0x00025E1F
		// (set) Token: 0x06000B5C RID: 2908 RVA: 0x00027C4A File Offset: 0x00025E4A
		[Description("Gets or sets the aggregate function.")]
		[Category("Behavior")]
		[DefaultValue(ClientDataSourceAggregateFunction.Sum)]
		public ClientDataSourceAggregateFunction Aggregate
		{
			get
			{
				if (base.ViewState["Aggregate"] != null)
				{
					return (ClientDataSourceAggregateFunction)base.ViewState["Aggregate"];
				}
				return ClientDataSourceAggregateFunction.Sum;
			}
			set
			{
				base.ViewState["Aggregate"] = value;
			}
		}
	}
}
