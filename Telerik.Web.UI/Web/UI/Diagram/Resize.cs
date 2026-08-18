using System;
using System.Web.UI;

namespace Telerik.Web.UI.Diagram
{
	// Token: 0x020002A9 RID: 681
	public class Resize : StateManager, IDefaultCheck
	{
		// Token: 0x17000838 RID: 2104
		// (get) Token: 0x06001812 RID: 6162 RVA: 0x0004FC51 File Offset: 0x0004DE51
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public Handles HandlesSettings
		{
			get
			{
				if (this._handles == null)
				{
					this._handles = new Handles();
				}
				return this._handles;
			}
		}

		// Token: 0x06001813 RID: 6163 RVA: 0x0004FC6C File Offset: 0x0004DE6C
		internal override void SetDirty()
		{
			base.SetDirty();
			this.HandlesSettings.SetDirty();
		}

		// Token: 0x06001814 RID: 6164 RVA: 0x0004FC80 File Offset: 0x0004DE80
		protected override void LoadViewState(object state)
		{
			object[] array = (object[])state;
			int num = 0;
			base.LoadViewState(array[num++]);
			((IStateManager)this.HandlesSettings).LoadViewState(array[num++]);
		}

		// Token: 0x06001815 RID: 6165 RVA: 0x0004FCB8 File Offset: 0x0004DEB8
		protected override object SaveViewState()
		{
			return new object[]
			{
				base.SaveViewState(),
				((IStateManager)this.HandlesSettings).SaveViewState()
			};
		}

		// Token: 0x06001816 RID: 6166 RVA: 0x0004FCE6 File Offset: 0x0004DEE6
		protected override void TrackViewState()
		{
			base.TrackViewState();
			((IStateManager)this.HandlesSettings).TrackViewState();
		}

		// Token: 0x17000839 RID: 2105
		// (get) Token: 0x06001817 RID: 6167 RVA: 0x0004FCF9 File Offset: 0x0004DEF9
		public bool IsDefault
		{
			get
			{
				return this.HandlesSettings.IsDefault;
			}
		}

		// Token: 0x0400066F RID: 1647
		private Handles _handles;
	}
}
