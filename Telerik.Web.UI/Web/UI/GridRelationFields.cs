using System;
using System.ComponentModel;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x02001199 RID: 4505
	public class GridRelationFields : IStateManager
	{
		// Token: 0x17003BD0 RID: 15312
		// (get) Token: 0x0600B901 RID: 47361 RVA: 0x0028F158 File Offset: 0x0028D358
		// (set) Token: 0x0600B902 RID: 47362 RVA: 0x0028F185 File Offset: 0x0028D385
		[NotifyParentProperty(true)]
		[DefaultValue("")]
		public string MasterKeyField
		{
			get
			{
				object obj = this.ViewState["_mkf"];
				if (obj == null)
				{
					obj = "";
				}
				return (string)obj;
			}
			set
			{
				this.ViewState["_mkf"] = value;
			}
		}

		// Token: 0x17003BD1 RID: 15313
		// (get) Token: 0x0600B903 RID: 47363 RVA: 0x0028F198 File Offset: 0x0028D398
		// (set) Token: 0x0600B904 RID: 47364 RVA: 0x0028F1C5 File Offset: 0x0028D3C5
		[DefaultValue("")]
		[NotifyParentProperty(true)]
		public string DetailKeyField
		{
			get
			{
				object obj = this.ViewState["_dkf"];
				if (obj == null)
				{
					obj = "";
				}
				return (string)obj;
			}
			set
			{
				this.ViewState["_dkf"] = value;
			}
		}

		// Token: 0x0600B905 RID: 47365 RVA: 0x0028F1D8 File Offset: 0x0028D3D8
		void IStateManager.LoadViewState(object state)
		{
			((IStateManager)this.ViewState).LoadViewState(state);
		}

		// Token: 0x0600B906 RID: 47366 RVA: 0x0028F1E6 File Offset: 0x0028D3E6
		object IStateManager.SaveViewState()
		{
			return ((IStateManager)this.ViewState).SaveViewState();
		}

		// Token: 0x0600B907 RID: 47367 RVA: 0x0028F1F3 File Offset: 0x0028D3F3
		void IStateManager.TrackViewState()
		{
			((IStateManager)this.ViewState).TrackViewState();
		}

		// Token: 0x17003BD2 RID: 15314
		// (get) Token: 0x0600B908 RID: 47368 RVA: 0x0028F200 File Offset: 0x0028D400
		bool IStateManager.IsTrackingViewState
		{
			get
			{
				return ((IStateManager)this.ViewState).IsTrackingViewState;
			}
		}

		// Token: 0x040030F0 RID: 12528
		private StateBag ViewState = new StateBag();
	}
}
