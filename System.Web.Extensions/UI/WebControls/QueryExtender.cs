using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Web.Resources;
using System.Web.UI.WebControls.Expressions;

namespace System.Web.UI.WebControls
{
	// Token: 0x020000C7 RID: 199
	[TargetControlType(typeof(IQueryableDataSource))]
	[NonVisualControl]
	[DefaultProperty("TargetControlID")]
	[ToolboxBitmap(typeof(QueryExtender), "QueryExtender.bmp")]
	[Designer("System.Web.UI.Design.QueryExtenderDesigner, System.Web.Extensions.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35")]
	[ParseChildren(true, "Expressions")]
	[PersistChildren(false)]
	public class QueryExtender : Control
	{
		// Token: 0x060009E4 RID: 2532 RVA: 0x00011E41 File Offset: 0x00010041
		public QueryExtender()
		{
		}

		// Token: 0x060009E5 RID: 2533 RVA: 0x00025B5C File Offset: 0x00023D5C
		internal QueryExtender(IQueryableDataSource dataSource)
		{
			this._dataSource = dataSource;
		}

		// Token: 0x170002E1 RID: 737
		// (get) Token: 0x060009E6 RID: 2534 RVA: 0x00025B6C File Offset: 0x00023D6C
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public virtual IQueryableDataSource DataSource
		{
			get
			{
				if (this._dataSource == null)
				{
					if (string.IsNullOrEmpty(this.TargetControlID))
					{
						throw new InvalidOperationException(AtlasWeb.DataSourceControlExtender_TargetControlIDMustBeSpecified);
					}
					this._dataSource = (DataBoundControlHelper.FindControl(this, this.TargetControlID) as IQueryableDataSource);
					if (this._dataSource == null)
					{
						throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, AtlasWeb.QueryExtender_DataSourceMustBeIQueryableDataSource, new object[]
						{
							this.TargetControlID
						}));
					}
				}
				return this._dataSource;
			}
		}

		// Token: 0x170002E2 RID: 738
		// (get) Token: 0x060009E7 RID: 2535 RVA: 0x00025BE2 File Offset: 0x00023DE2
		[Category("Behavior")]
		[ResourceDescription("QueryExtender_Expressions")]
		[PersistenceMode(PersistenceMode.InnerDefaultProperty)]
		public DataSourceExpressionCollection Expressions
		{
			get
			{
				return this.Query.Expressions;
			}
		}

		// Token: 0x170002E3 RID: 739
		// (get) Token: 0x060009E8 RID: 2536 RVA: 0x00025BEF File Offset: 0x00023DEF
		// (set) Token: 0x060009E9 RID: 2537 RVA: 0x00025C00 File Offset: 0x00023E00
		[Category("Behavior")]
		[DefaultValue("")]
		[IDReferenceProperty]
		[ResourceDescription("ExtenderControl_TargetControlID")]
		public virtual string TargetControlID
		{
			get
			{
				return this._targetControlID ?? string.Empty;
			}
			set
			{
				if (this._targetControlID != value)
				{
					this._dataSource = null;
					this._targetControlID = value;
				}
			}
		}

		// Token: 0x170002E4 RID: 740
		// (get) Token: 0x060009EA RID: 2538 RVA: 0x00025C1E File Offset: 0x00023E1E
		private QueryExpression Query
		{
			get
			{
				if (this._query == null)
				{
					this._query = new QueryExpression();
				}
				return this._query;
			}
		}

		// Token: 0x060009EB RID: 2539 RVA: 0x00025C39 File Offset: 0x00023E39
		protected internal override void OnInit(EventArgs e)
		{
			if (!base.DesignMode)
			{
				this.DataSource.QueryCreated += this.OnDataSourceQueryCreated;
				this.Query.Initialize(this, this.Context, this.DataSource);
			}
			base.OnInit(e);
		}

		// Token: 0x060009EC RID: 2540 RVA: 0x00025C79 File Offset: 0x00023E79
		private void OnDataSourceQueryCreated(object sender, QueryCreatedEventArgs e)
		{
			e.Query = this.Query.GetQueryable(e.Query);
		}

		// Token: 0x060009ED RID: 2541 RVA: 0x00025C94 File Offset: 0x00023E94
		protected override object SaveViewState()
		{
			return new Pair
			{
				First = base.SaveViewState(),
				Second = ((this._query != null) ? ((IStateManager)this._query.Expressions).SaveViewState() : null)
			};
		}

		// Token: 0x060009EE RID: 2542 RVA: 0x00025CD8 File Offset: 0x00023ED8
		protected override void LoadViewState(object savedState)
		{
			Pair pair = (Pair)savedState;
			base.LoadViewState(pair.First);
			if (pair.Second != null)
			{
				((IStateManager)this.Query.Expressions).LoadViewState(pair.Second);
			}
		}

		// Token: 0x060009EF RID: 2543 RVA: 0x00025D16 File Offset: 0x00023F16
		protected override void TrackViewState()
		{
			base.TrackViewState();
			if (this._query != null)
			{
				((IStateManager)this._query.Expressions).TrackViewState();
			}
		}

		// Token: 0x04000339 RID: 825
		private QueryExpression _query;

		// Token: 0x0400033A RID: 826
		private string _targetControlID;

		// Token: 0x0400033B RID: 827
		private IQueryableDataSource _dataSource;
	}
}
