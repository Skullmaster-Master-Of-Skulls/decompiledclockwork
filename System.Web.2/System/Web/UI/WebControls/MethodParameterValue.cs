using System;

namespace System.Web.UI.WebControls
{
	// Token: 0x02000479 RID: 1145
	internal sealed class MethodParameterValue : IStateManager
	{
		// Token: 0x0600388C RID: 14476 RVA: 0x000B81A7 File Offset: 0x000B63A7
		internal void SetOwner(MethodParametersDictionary owner)
		{
			this._owner = owner;
		}

		// Token: 0x0600388D RID: 14477 RVA: 0x000B81B0 File Offset: 0x000B63B0
		private void OnParameterChanged()
		{
			if (this._owner != null)
			{
				this._owner.CallOnParametersChanged();
			}
		}

		// Token: 0x0600388E RID: 14478 RVA: 0x000B81C8 File Offset: 0x000B63C8
		internal void UpdateValue(object newValue)
		{
			object obj = this.ViewState[MethodParameterValue.s_valueViewStateKey];
			this.ViewState[MethodParameterValue.s_valueViewStateKey] = newValue;
			if ((newValue == null && obj != null) || (newValue != null && !newValue.Equals(obj)))
			{
				this.OnParameterChanged();
			}
		}

		// Token: 0x1700108F RID: 4239
		// (get) Token: 0x0600388F RID: 14479 RVA: 0x000B820F File Offset: 0x000B640F
		private bool IsTrackingViewState
		{
			get
			{
				return this._tracking;
			}
		}

		// Token: 0x17001090 RID: 4240
		// (get) Token: 0x06003890 RID: 14480 RVA: 0x000B8217 File Offset: 0x000B6417
		private StateBag ViewState
		{
			get
			{
				if (this._viewState == null)
				{
					this._viewState = new StateBag();
					if (this._tracking)
					{
						this._viewState.TrackViewState();
					}
				}
				return this._viewState;
			}
		}

		// Token: 0x06003891 RID: 14481 RVA: 0x000B8245 File Offset: 0x000B6445
		private void LoadViewState(object savedState)
		{
			if (savedState != null)
			{
				this.ViewState.LoadViewState(savedState);
			}
		}

		// Token: 0x06003892 RID: 14482 RVA: 0x000B8256 File Offset: 0x000B6456
		private object SaveViewState()
		{
			if (this._viewState == null)
			{
				return null;
			}
			return this._viewState.SaveViewState();
		}

		// Token: 0x06003893 RID: 14483 RVA: 0x000B826D File Offset: 0x000B646D
		private void TrackViewState()
		{
			this._tracking = true;
			if (this._viewState != null)
			{
				this._viewState.TrackViewState();
			}
		}

		// Token: 0x17001091 RID: 4241
		// (get) Token: 0x06003894 RID: 14484 RVA: 0x000B8289 File Offset: 0x000B6489
		bool IStateManager.IsTrackingViewState
		{
			get
			{
				return this.IsTrackingViewState;
			}
		}

		// Token: 0x06003895 RID: 14485 RVA: 0x000B8291 File Offset: 0x000B6491
		void IStateManager.LoadViewState(object savedState)
		{
			this.LoadViewState(savedState);
		}

		// Token: 0x06003896 RID: 14486 RVA: 0x000B829A File Offset: 0x000B649A
		object IStateManager.SaveViewState()
		{
			return this.SaveViewState();
		}

		// Token: 0x06003897 RID: 14487 RVA: 0x000B82A2 File Offset: 0x000B64A2
		void IStateManager.TrackViewState()
		{
			this.TrackViewState();
		}

		// Token: 0x0400228B RID: 8843
		private MethodParametersDictionary _owner;

		// Token: 0x0400228C RID: 8844
		private bool _tracking;

		// Token: 0x0400228D RID: 8845
		private StateBag _viewState;

		// Token: 0x0400228E RID: 8846
		private static readonly string s_valueViewStateKey = "ParameterValue";
	}
}
