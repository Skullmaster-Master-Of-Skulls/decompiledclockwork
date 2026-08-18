using System;
using System.Web.UI;

namespace Telerik.Web.UI.Diagram
{
	// Token: 0x020002AF RID: 687
	public class Selection : StateManager, IDefaultCheck
	{
		// Token: 0x17000844 RID: 2116
		// (get) Token: 0x06001835 RID: 6197 RVA: 0x000500C6 File Offset: 0x0004E2C6
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

		// Token: 0x06001836 RID: 6198 RVA: 0x000500E1 File Offset: 0x0004E2E1
		internal override void SetDirty()
		{
			base.SetDirty();
			this.HandlesSettings.SetDirty();
		}

		// Token: 0x06001837 RID: 6199 RVA: 0x000500F4 File Offset: 0x0004E2F4
		protected override void LoadViewState(object state)
		{
			object[] array = (object[])state;
			int num = 0;
			base.LoadViewState(array[num++]);
			((IStateManager)this.HandlesSettings).LoadViewState(array[num++]);
		}

		// Token: 0x06001838 RID: 6200 RVA: 0x0005012C File Offset: 0x0004E32C
		protected override object SaveViewState()
		{
			return new object[]
			{
				base.SaveViewState(),
				((IStateManager)this.HandlesSettings).SaveViewState()
			};
		}

		// Token: 0x06001839 RID: 6201 RVA: 0x0005015A File Offset: 0x0004E35A
		protected override void TrackViewState()
		{
			base.TrackViewState();
			((IStateManager)this.HandlesSettings).TrackViewState();
		}

		// Token: 0x17000845 RID: 2117
		// (get) Token: 0x0600183A RID: 6202 RVA: 0x0005016D File Offset: 0x0004E36D
		public bool IsDefault
		{
			get
			{
				return this.HandlesSettings.IsDefault;
			}
		}

		// Token: 0x04000673 RID: 1651
		private Handles _handles;
	}
}
