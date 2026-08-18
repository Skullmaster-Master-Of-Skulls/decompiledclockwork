using System;
using System.Web.UI;

namespace Telerik.Web.UI.Map
{
	// Token: 0x0200058B RID: 1419
	public class Animation : StateManager, IDefaultCheck
	{
		// Token: 0x17001091 RID: 4241
		// (get) Token: 0x06003320 RID: 13088 RVA: 0x000AA8E0 File Offset: 0x000A8AE0
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public Close CloseSettings
		{
			get
			{
				if (this._close == null)
				{
					this._close = new Close();
				}
				return this._close;
			}
		}

		// Token: 0x17001092 RID: 4242
		// (get) Token: 0x06003321 RID: 13089 RVA: 0x000AA8FB File Offset: 0x000A8AFB
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public Open OpenSettings
		{
			get
			{
				if (this._open == null)
				{
					this._open = new Open();
				}
				return this._open;
			}
		}

		// Token: 0x06003322 RID: 13090 RVA: 0x000AA916 File Offset: 0x000A8B16
		internal override void SetDirty()
		{
			base.SetDirty();
			this.CloseSettings.SetDirty();
			this.OpenSettings.SetDirty();
		}

		// Token: 0x06003323 RID: 13091 RVA: 0x000AA934 File Offset: 0x000A8B34
		protected override void LoadViewState(object state)
		{
			object[] array = (object[])state;
			int num = 0;
			base.LoadViewState(array[num++]);
			((IStateManager)this.CloseSettings).LoadViewState(array[num++]);
			((IStateManager)this.OpenSettings).LoadViewState(array[num++]);
		}

		// Token: 0x06003324 RID: 13092 RVA: 0x000AA97C File Offset: 0x000A8B7C
		protected override object SaveViewState()
		{
			return new object[]
			{
				base.SaveViewState(),
				((IStateManager)this.CloseSettings).SaveViewState(),
				((IStateManager)this.OpenSettings).SaveViewState()
			};
		}

		// Token: 0x06003325 RID: 13093 RVA: 0x000AA9B8 File Offset: 0x000A8BB8
		protected override void TrackViewState()
		{
			base.TrackViewState();
			((IStateManager)this.CloseSettings).TrackViewState();
			((IStateManager)this.OpenSettings).TrackViewState();
		}

		// Token: 0x17001093 RID: 4243
		// (get) Token: 0x06003326 RID: 13094 RVA: 0x000AA9D6 File Offset: 0x000A8BD6
		public bool IsDefault
		{
			get
			{
				return this.CloseSettings.IsDefault && this.OpenSettings.IsDefault;
			}
		}

		// Token: 0x04000E07 RID: 3591
		private Close _close;

		// Token: 0x04000E08 RID: 3592
		private Open _open;
	}
}
