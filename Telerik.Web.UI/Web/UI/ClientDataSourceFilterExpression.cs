using System;
using System.ComponentModel;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x0200011A RID: 282
	[ParseChildren(true, "Filters")]
	[PersistChildren(true)]
	[TypeConverter(typeof(ExpandableObjectConverter))]
	[DefaultProperty("Filters")]
	public class ClientDataSourceFilterExpression : ClientDataSourceFilterBase
	{
		// Token: 0x06000B6E RID: 2926 RVA: 0x000283C1 File Offset: 0x000265C1
		public ClientDataSourceFilterExpression()
		{
			this._filters = null;
		}

		// Token: 0x170003FC RID: 1020
		// (get) Token: 0x06000B6F RID: 2927 RVA: 0x000283D0 File Offset: 0x000265D0
		internal bool ShouldSerializeLogicOperator
		{
			get
			{
				return this.LogicOperator != ClientDataSourceFilterLogicOperator.And;
			}
		}

		// Token: 0x170003FD RID: 1021
		// (get) Token: 0x06000B70 RID: 2928 RVA: 0x000283DE File Offset: 0x000265DE
		internal bool ShouldSerializeFilters
		{
			get
			{
				return this.Filters != null && this.Filters.Count > 0;
			}
		}

		// Token: 0x170003FE RID: 1022
		// (get) Token: 0x06000B71 RID: 2929 RVA: 0x000283F8 File Offset: 0x000265F8
		// (set) Token: 0x06000B72 RID: 2930 RVA: 0x00028423 File Offset: 0x00026623
		[PersistenceMode(PersistenceMode.Attribute)]
		[Description("Gets or sets the filter logic, AND or OR.")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[DefaultValue(ClientDataSourceFilterLogicOperator.And)]
		[Category("Behavior")]
		public ClientDataSourceFilterLogicOperator LogicOperator
		{
			get
			{
				if (base.ViewState["LogicOperator"] != null)
				{
					return (ClientDataSourceFilterLogicOperator)base.ViewState["LogicOperator"];
				}
				return ClientDataSourceFilterLogicOperator.And;
			}
			set
			{
				base.ViewState["LogicOperator"] = value;
			}
		}

		// Token: 0x170003FF RID: 1023
		// (get) Token: 0x06000B73 RID: 2931 RVA: 0x0002843B File Offset: 0x0002663B
		[NotifyParentProperty(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[PersistenceMode(PersistenceMode.InnerDefaultProperty)]
		public ClientDataSourceFilterBaseCollection Filters
		{
			get
			{
				if (this._filters == null)
				{
					this._filters = new ClientDataSourceFilterBaseCollection();
				}
				if (base.IsTrackingViewState)
				{
					((IStateManager)this._filters).TrackViewState();
				}
				return this._filters;
			}
		}

		// Token: 0x040002D5 RID: 725
		private ClientDataSourceFilterBaseCollection _filters;
	}
}
