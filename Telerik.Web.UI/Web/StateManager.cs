using System;
using System.Web.UI;

namespace Telerik.Web
{
	// Token: 0x02000003 RID: 3
	public abstract class StateManager : IMarkableStateManager, IStateManager
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000002 RID: 2 RVA: 0x000020D0 File Offset: 0x000002D0
		bool IStateManager.IsTrackingViewState
		{
			get
			{
				return this.IsTrackingViewState;
			}
		}

		// Token: 0x06000003 RID: 3 RVA: 0x000020D8 File Offset: 0x000002D8
		void IStateManager.LoadViewState(object state)
		{
			this.LoadViewState(state);
		}

		// Token: 0x06000004 RID: 4 RVA: 0x000020E1 File Offset: 0x000002E1
		object IStateManager.SaveViewState()
		{
			return this.SaveViewState();
		}

		// Token: 0x06000005 RID: 5 RVA: 0x000020E9 File Offset: 0x000002E9
		void IStateManager.TrackViewState()
		{
			this.TrackViewState();
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000006 RID: 6 RVA: 0x000020F1 File Offset: 0x000002F1
		protected virtual bool IsTrackingViewState
		{
			get
			{
				return this._isTrackingViewState;
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000007 RID: 7 RVA: 0x000020F9 File Offset: 0x000002F9
		protected StateBag ViewState
		{
			get
			{
				if (this._viewState == null)
				{
					this._viewState = new StateBag();
					if (this.IsTrackingViewState)
					{
						((IStateManager)this._viewState).TrackViewState();
					}
				}
				return this._viewState;
			}
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002127 File Offset: 0x00000327
		protected virtual void TrackViewState()
		{
			this._isTrackingViewState = true;
			if (this._viewState != null)
			{
				((IStateManager)this._viewState).TrackViewState();
			}
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002143 File Offset: 0x00000343
		protected virtual void LoadViewState(object state)
		{
			if (state != null)
			{
				((IStateManager)this.ViewState).LoadViewState(state);
			}
		}

		// Token: 0x0600000A RID: 10 RVA: 0x00002154 File Offset: 0x00000354
		protected virtual object SaveViewState()
		{
			return ((IStateManager)this.ViewState).SaveViewState();
		}

		// Token: 0x0600000B RID: 11 RVA: 0x00002161 File Offset: 0x00000361
		internal virtual void SetDirty()
		{
			this.ViewState.SetDirty(true);
		}

		// Token: 0x0600000C RID: 12 RVA: 0x0000216F File Offset: 0x0000036F
		void IMarkableStateManager.SetDirty()
		{
			this.SetDirty();
		}

		// Token: 0x0600000D RID: 13 RVA: 0x00002178 File Offset: 0x00000378
		protected T GetViewStateValue<T>(string key, T defaultValue)
		{
			object obj = this.ViewState[key];
			if (obj == null)
			{
				return defaultValue;
			}
			return (T)((object)obj);
		}

		// Token: 0x04000001 RID: 1
		private bool _isTrackingViewState;

		// Token: 0x04000002 RID: 2
		private StateBag _viewState;
	}
}
