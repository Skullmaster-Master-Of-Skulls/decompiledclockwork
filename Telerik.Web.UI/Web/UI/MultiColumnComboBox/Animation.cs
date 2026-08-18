using System;
using System.ComponentModel;
using System.Web.UI;

namespace Telerik.Web.UI.MultiColumnComboBox
{
	// Token: 0x020005E5 RID: 1509
	public class Animation : StateManager, IDefaultCheck
	{
		// Token: 0x170011EF RID: 4591
		// (get) Token: 0x060036AE RID: 13998 RVA: 0x000B54C1 File Offset: 0x000B36C1
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
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

		// Token: 0x170011F0 RID: 4592
		// (get) Token: 0x060036AF RID: 13999 RVA: 0x000B54DC File Offset: 0x000B36DC
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
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

		// Token: 0x060036B0 RID: 14000 RVA: 0x000B54F7 File Offset: 0x000B36F7
		internal override void SetDirty()
		{
			base.SetDirty();
			this.CloseSettings.SetDirty();
			this.OpenSettings.SetDirty();
		}

		// Token: 0x060036B1 RID: 14001 RVA: 0x000B5518 File Offset: 0x000B3718
		protected override void LoadViewState(object state)
		{
			object[] array = (object[])state;
			int num = 0;
			base.LoadViewState(array[num++]);
			((IStateManager)this.CloseSettings).LoadViewState(array[num++]);
			((IStateManager)this.OpenSettings).LoadViewState(array[num++]);
		}

		// Token: 0x060036B2 RID: 14002 RVA: 0x000B5560 File Offset: 0x000B3760
		protected override object SaveViewState()
		{
			return new object[]
			{
				base.SaveViewState(),
				((IStateManager)this.CloseSettings).SaveViewState(),
				((IStateManager)this.OpenSettings).SaveViewState()
			};
		}

		// Token: 0x060036B3 RID: 14003 RVA: 0x000B559C File Offset: 0x000B379C
		protected override void TrackViewState()
		{
			base.TrackViewState();
			((IStateManager)this.CloseSettings).TrackViewState();
			((IStateManager)this.OpenSettings).TrackViewState();
		}

		// Token: 0x170011F1 RID: 4593
		// (get) Token: 0x060036B4 RID: 14004 RVA: 0x000B55BA File Offset: 0x000B37BA
		public bool IsDefault
		{
			get
			{
				return this.CloseSettings.IsDefault && this.OpenSettings.IsDefault;
			}
		}

		// Token: 0x04000EC2 RID: 3778
		private Close _close;

		// Token: 0x04000EC3 RID: 3779
		private Open _open;
	}
}
