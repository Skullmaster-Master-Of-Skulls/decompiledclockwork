using System;
using System.Collections.Generic;

namespace System.Web.UI.WebControls.Expressions
{
	// Token: 0x020000D1 RID: 209
	[PersistChildren(false)]
	[ParseChildren(true, "Parameters")]
	public abstract class ParameterDataSourceExpression : DataSourceExpression
	{
		// Token: 0x170002FA RID: 762
		// (get) Token: 0x06000A58 RID: 2648 RVA: 0x00026B67 File Offset: 0x00024D67
		[PersistenceMode(PersistenceMode.InnerDefaultProperty)]
		public ParameterCollection Parameters
		{
			get
			{
				if (this._parameters == null)
				{
					this._parameters = new ParameterCollection();
					this._parameters.ParametersChanged += this.OnParametersChanged;
				}
				return this._parameters;
			}
		}

		// Token: 0x06000A59 RID: 2649 RVA: 0x00026B99 File Offset: 0x00024D99
		internal virtual IDictionary<string, object> GetValues()
		{
			return this.Parameters.ToDictionary(base.Context, base.Owner);
		}

		// Token: 0x06000A5A RID: 2650 RVA: 0x00026BB2 File Offset: 0x00024DB2
		public override void SetContext(Control owner, HttpContext context, IQueryableDataSource dataSource)
		{
			base.SetContext(owner, context, dataSource);
			owner.Page.LoadComplete += this.OnPageLoadComplete;
		}

		// Token: 0x06000A5B RID: 2651 RVA: 0x00026BD4 File Offset: 0x00024DD4
		private void OnParametersChanged(object sender, EventArgs e)
		{
			if (base.DataSource != null)
			{
				base.DataSource.RaiseViewChanged();
			}
		}

		// Token: 0x06000A5C RID: 2652 RVA: 0x00026BE9 File Offset: 0x00024DE9
		private void OnPageLoadComplete(object sender, EventArgs e)
		{
			this.Parameters.UpdateValues(base.Context, base.Owner);
		}

		// Token: 0x06000A5D RID: 2653 RVA: 0x00026C04 File Offset: 0x00024E04
		protected override object SaveViewState()
		{
			return new Pair
			{
				First = base.SaveViewState(),
				Second = DataSourceHelper.SaveViewState(this._parameters)
			};
		}

		// Token: 0x06000A5E RID: 2654 RVA: 0x00026C38 File Offset: 0x00024E38
		protected override void LoadViewState(object savedState)
		{
			Pair pair = (Pair)savedState;
			base.LoadViewState(pair.First);
			if (pair.Second != null)
			{
				((IStateManager)this.Parameters).LoadViewState(pair.Second);
			}
		}

		// Token: 0x06000A5F RID: 2655 RVA: 0x00026C71 File Offset: 0x00024E71
		protected override void TrackViewState()
		{
			base.TrackViewState();
			DataSourceHelper.TrackViewState(this._parameters);
		}

		// Token: 0x04000357 RID: 855
		private ParameterCollection _parameters;
	}
}
