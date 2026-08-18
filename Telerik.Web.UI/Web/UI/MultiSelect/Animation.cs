using System;
using System.Web.UI;

namespace Telerik.Web.UI.MultiSelect
{
	// Token: 0x02000609 RID: 1545
	public class Animation : StateManager, IDefaultCheck
	{
		// Token: 0x17001276 RID: 4726
		// (get) Token: 0x06003847 RID: 14407 RVA: 0x000B9500 File Offset: 0x000B7700
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

		// Token: 0x17001277 RID: 4727
		// (get) Token: 0x06003848 RID: 14408 RVA: 0x000B951B File Offset: 0x000B771B
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

		// Token: 0x06003849 RID: 14409 RVA: 0x000B9536 File Offset: 0x000B7736
		internal override void SetDirty()
		{
			base.SetDirty();
			this.CloseSettings.SetDirty();
			this.OpenSettings.SetDirty();
		}

		// Token: 0x0600384A RID: 14410 RVA: 0x000B9554 File Offset: 0x000B7754
		protected override void LoadViewState(object state)
		{
			object[] array = (object[])state;
			int num = 0;
			base.LoadViewState(array[num++]);
			((IStateManager)this.CloseSettings).LoadViewState(array[num++]);
			((IStateManager)this.OpenSettings).LoadViewState(array[num++]);
		}

		// Token: 0x0600384B RID: 14411 RVA: 0x000B959C File Offset: 0x000B779C
		protected override object SaveViewState()
		{
			return new object[]
			{
				base.SaveViewState(),
				((IStateManager)this.CloseSettings).SaveViewState(),
				((IStateManager)this.OpenSettings).SaveViewState()
			};
		}

		// Token: 0x0600384C RID: 14412 RVA: 0x000B95D8 File Offset: 0x000B77D8
		protected override void TrackViewState()
		{
			base.TrackViewState();
			((IStateManager)this.CloseSettings).TrackViewState();
			((IStateManager)this.OpenSettings).TrackViewState();
		}

		// Token: 0x17001278 RID: 4728
		// (get) Token: 0x0600384D RID: 14413 RVA: 0x000B95F6 File Offset: 0x000B77F6
		public bool IsDefault
		{
			get
			{
				return this.CloseSettings.IsDefault && this.OpenSettings.IsDefault;
			}
		}

		// Token: 0x04000F00 RID: 3840
		private Close _close;

		// Token: 0x04000F01 RID: 3841
		private Open _open;
	}
}
