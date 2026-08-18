using System;
using System.ComponentModel;
using System.Web.UI;

namespace Telerik.Web.UI.Diagram
{
	// Token: 0x0200024D RID: 589
	public class Drag : StateManager, IDefaultCheck
	{
		// Token: 0x17000755 RID: 1877
		// (get) Token: 0x06001587 RID: 5511 RVA: 0x00049BDE File Offset: 0x00047DDE
		// (set) Token: 0x06001588 RID: 5512 RVA: 0x00049BFF File Offset: 0x00047DFF
		[DefaultValue(true)]
		public bool Snap
		{
			get
			{
				return (bool)(base.ViewState["Snap"] ?? true);
			}
			set
			{
				base.ViewState["Snap"] = value;
			}
		}

		// Token: 0x17000756 RID: 1878
		// (get) Token: 0x06001589 RID: 5513 RVA: 0x00049C17 File Offset: 0x00047E17
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public Snap SnapSettings
		{
			get
			{
				if (this._snap == null)
				{
					this._snap = new Snap();
				}
				return this._snap;
			}
		}

		// Token: 0x0600158A RID: 5514 RVA: 0x00049C32 File Offset: 0x00047E32
		internal override void SetDirty()
		{
			base.SetDirty();
			this.SnapSettings.SetDirty();
		}

		// Token: 0x0600158B RID: 5515 RVA: 0x00049C48 File Offset: 0x00047E48
		protected override void LoadViewState(object state)
		{
			object[] array = (object[])state;
			int num = 0;
			base.LoadViewState(array[num++]);
			((IStateManager)this.SnapSettings).LoadViewState(array[num++]);
		}

		// Token: 0x0600158C RID: 5516 RVA: 0x00049C80 File Offset: 0x00047E80
		protected override object SaveViewState()
		{
			return new object[]
			{
				base.SaveViewState(),
				((IStateManager)this.SnapSettings).SaveViewState()
			};
		}

		// Token: 0x0600158D RID: 5517 RVA: 0x00049CAE File Offset: 0x00047EAE
		protected override void TrackViewState()
		{
			base.TrackViewState();
			((IStateManager)this.SnapSettings).TrackViewState();
		}

		// Token: 0x17000757 RID: 1879
		// (get) Token: 0x0600158E RID: 5518 RVA: 0x00049CC1 File Offset: 0x00047EC1
		public bool IsDefault
		{
			get
			{
				return this.Snap && this.SnapSettings.IsDefault;
			}
		}

		// Token: 0x040005B9 RID: 1465
		private Snap _snap;
	}
}
