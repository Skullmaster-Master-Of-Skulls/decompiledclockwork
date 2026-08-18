using System;
using System.ComponentModel;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x0200011B RID: 283
	public class ClientDataSourceGroupExpression : StateManager
	{
		// Token: 0x06000B74 RID: 2932 RVA: 0x00028469 File Offset: 0x00026669
		public ClientDataSourceGroupExpression()
		{
			this._aggregates = null;
		}

		// Token: 0x17000400 RID: 1024
		// (get) Token: 0x06000B75 RID: 2933 RVA: 0x00028478 File Offset: 0x00026678
		// (set) Token: 0x06000B76 RID: 2934 RVA: 0x00028498 File Offset: 0x00026698
		[DefaultValue("")]
		[Category("Behavior")]
		[Description("Gets or sets the field name for the group operation.")]
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

		// Token: 0x17000401 RID: 1025
		// (get) Token: 0x06000B77 RID: 2935 RVA: 0x000284AB File Offset: 0x000266AB
		// (set) Token: 0x06000B78 RID: 2936 RVA: 0x000284D6 File Offset: 0x000266D6
		[DefaultValue(ClientDataSourceSortOrder.Asc)]
		[Description("Gets or sets the sort order.")]
		[Category("Behavior")]
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

		// Token: 0x17000402 RID: 1026
		// (get) Token: 0x06000B79 RID: 2937 RVA: 0x000284EE File Offset: 0x000266EE
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[NotifyParentProperty(true)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public virtual ClientDataSourceAggregatesCollection Aggregates
		{
			get
			{
				if (this._aggregates == null)
				{
					this._aggregates = new ClientDataSourceAggregatesCollection();
					if (base.IsTrackingViewState)
					{
						((IStateManager)this._aggregates).TrackViewState();
					}
				}
				return this._aggregates;
			}
		}

		// Token: 0x040002D6 RID: 726
		private ClientDataSourceAggregatesCollection _aggregates;
	}
}
