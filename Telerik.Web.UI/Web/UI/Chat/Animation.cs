using System;
using System.Web.UI;

namespace Telerik.Web.UI.Chat
{
	// Token: 0x0200007C RID: 124
	public class Animation : StateManager, IDefaultCheck
	{
		// Token: 0x170001DE RID: 478
		// (get) Token: 0x0600050B RID: 1291 RVA: 0x0000CA9A File Offset: 0x0000AC9A
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public Collapse CollapseSettings
		{
			get
			{
				if (this._collapse == null)
				{
					this._collapse = new Collapse();
				}
				return this._collapse;
			}
		}

		// Token: 0x170001DF RID: 479
		// (get) Token: 0x0600050C RID: 1292 RVA: 0x0000CAB5 File Offset: 0x0000ACB5
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public Expand ExpandSettings
		{
			get
			{
				if (this._expand == null)
				{
					this._expand = new Expand();
				}
				return this._expand;
			}
		}

		// Token: 0x0600050D RID: 1293 RVA: 0x0000CAD0 File Offset: 0x0000ACD0
		internal override void SetDirty()
		{
			base.SetDirty();
			this.CollapseSettings.SetDirty();
			this.ExpandSettings.SetDirty();
		}

		// Token: 0x0600050E RID: 1294 RVA: 0x0000CAF0 File Offset: 0x0000ACF0
		protected override void LoadViewState(object state)
		{
			object[] array = (object[])state;
			int num = 0;
			base.LoadViewState(array[num++]);
			((IStateManager)this.CollapseSettings).LoadViewState(array[num++]);
			((IStateManager)this.ExpandSettings).LoadViewState(array[num++]);
		}

		// Token: 0x0600050F RID: 1295 RVA: 0x0000CB38 File Offset: 0x0000AD38
		protected override object SaveViewState()
		{
			return new object[]
			{
				base.SaveViewState(),
				((IStateManager)this.CollapseSettings).SaveViewState(),
				((IStateManager)this.ExpandSettings).SaveViewState()
			};
		}

		// Token: 0x06000510 RID: 1296 RVA: 0x0000CB74 File Offset: 0x0000AD74
		protected override void TrackViewState()
		{
			base.TrackViewState();
			((IStateManager)this.CollapseSettings).TrackViewState();
			((IStateManager)this.ExpandSettings).TrackViewState();
		}

		// Token: 0x170001E0 RID: 480
		// (get) Token: 0x06000511 RID: 1297 RVA: 0x0000CB92 File Offset: 0x0000AD92
		public bool IsDefault
		{
			get
			{
				return this.CollapseSettings.IsDefault && this.ExpandSettings.IsDefault;
			}
		}

		// Token: 0x040000B2 RID: 178
		private Collapse _collapse;

		// Token: 0x040000B3 RID: 179
		private Expand _expand;
	}
}
