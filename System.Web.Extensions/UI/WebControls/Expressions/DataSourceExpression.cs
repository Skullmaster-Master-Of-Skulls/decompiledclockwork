using System;
using System.ComponentModel;
using System.Linq;

namespace System.Web.UI.WebControls.Expressions
{
	// Token: 0x020000CB RID: 203
	public abstract class DataSourceExpression : IStateManager
	{
		// Token: 0x170002E9 RID: 745
		// (get) Token: 0x06000A06 RID: 2566 RVA: 0x00025F42 File Offset: 0x00024142
		// (set) Token: 0x06000A07 RID: 2567 RVA: 0x00025F4A File Offset: 0x0002414A
		private protected HttpContext Context { protected get; private set; }

		// Token: 0x170002EA RID: 746
		// (get) Token: 0x06000A08 RID: 2568 RVA: 0x00025F53 File Offset: 0x00024153
		// (set) Token: 0x06000A09 RID: 2569 RVA: 0x00025F5B File Offset: 0x0002415B
		private protected Control Owner { protected get; private set; }

		// Token: 0x170002EB RID: 747
		// (get) Token: 0x06000A0A RID: 2570 RVA: 0x00025F64 File Offset: 0x00024164
		// (set) Token: 0x06000A0B RID: 2571 RVA: 0x00025F6C File Offset: 0x0002416C
		public IQueryableDataSource DataSource { get; internal set; }

		// Token: 0x170002EC RID: 748
		// (get) Token: 0x06000A0C RID: 2572 RVA: 0x00025F75 File Offset: 0x00024175
		protected bool IsTrackingViewState
		{
			get
			{
				return this._tracking;
			}
		}

		// Token: 0x170002ED RID: 749
		// (get) Token: 0x06000A0D RID: 2573 RVA: 0x00025F7D File Offset: 0x0002417D
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		protected StateBag ViewState
		{
			get
			{
				if (this._viewState == null)
				{
					this._viewState = new StateBag();
					if (this._tracking)
					{
						((IStateManager)this._viewState).TrackViewState();
					}
				}
				return this._viewState;
			}
		}

		// Token: 0x06000A0E RID: 2574 RVA: 0x00002050 File Offset: 0x00000250
		protected DataSourceExpression()
		{
		}

		// Token: 0x06000A0F RID: 2575 RVA: 0x00025FAB File Offset: 0x000241AB
		internal DataSourceExpression(Control owner)
		{
			this.Owner = owner;
		}

		// Token: 0x06000A10 RID: 2576 RVA: 0x00025FBA File Offset: 0x000241BA
		public void SetDirty()
		{
			this.ViewState.SetDirty(true);
		}

		// Token: 0x06000A11 RID: 2577 RVA: 0x00025FC8 File Offset: 0x000241C8
		protected virtual void LoadViewState(object savedState)
		{
			if (savedState != null)
			{
				((IStateManager)this.ViewState).LoadViewState(savedState);
			}
		}

		// Token: 0x06000A12 RID: 2578 RVA: 0x00025FD9 File Offset: 0x000241D9
		protected virtual object SaveViewState()
		{
			if (this._viewState == null)
			{
				return null;
			}
			return ((IStateManager)this._viewState).SaveViewState();
		}

		// Token: 0x06000A13 RID: 2579 RVA: 0x00025FF0 File Offset: 0x000241F0
		protected virtual void TrackViewState()
		{
			this._tracking = true;
			if (this._viewState != null)
			{
				((IStateManager)this._viewState).TrackViewState();
			}
		}

		// Token: 0x06000A14 RID: 2580
		public abstract IQueryable GetQueryable(IQueryable source);

		// Token: 0x06000A15 RID: 2581 RVA: 0x0002600C File Offset: 0x0002420C
		public virtual void SetContext(Control owner, HttpContext context, IQueryableDataSource dataSource)
		{
			if (owner == null)
			{
				throw new ArgumentNullException("owner");
			}
			if (context == null)
			{
				throw new ArgumentNullException("context");
			}
			if (dataSource == null)
			{
				throw new ArgumentNullException("dataSource");
			}
			this.Owner = owner;
			this.Context = context;
			this.DataSource = dataSource;
		}

		// Token: 0x170002EE RID: 750
		// (get) Token: 0x06000A16 RID: 2582 RVA: 0x00026058 File Offset: 0x00024258
		bool IStateManager.IsTrackingViewState
		{
			get
			{
				return this.IsTrackingViewState;
			}
		}

		// Token: 0x06000A17 RID: 2583 RVA: 0x00026060 File Offset: 0x00024260
		void IStateManager.LoadViewState(object state)
		{
			this.LoadViewState(state);
		}

		// Token: 0x06000A18 RID: 2584 RVA: 0x00026069 File Offset: 0x00024269
		object IStateManager.SaveViewState()
		{
			return this.SaveViewState();
		}

		// Token: 0x06000A19 RID: 2585 RVA: 0x00026071 File Offset: 0x00024271
		void IStateManager.TrackViewState()
		{
			this.TrackViewState();
		}

		// Token: 0x04000345 RID: 837
		private bool _tracking;

		// Token: 0x04000346 RID: 838
		private StateBag _viewState;
	}
}
