using System;
using System.ComponentModel;
using System.Web.UI;

namespace Telerik.Web.UI.Diagram
{
	// Token: 0x0200044F RID: 1103
	public class ShapeHover : StateManager, IDefaultCheck
	{
		// Token: 0x17000CE7 RID: 3303
		// (get) Token: 0x060027D5 RID: 10197 RVA: 0x0008172A File Offset: 0x0007F92A
		// (set) Token: 0x060027D6 RID: 10198 RVA: 0x0008174A File Offset: 0x0007F94A
		[DefaultValue("")]
		public string Fill
		{
			get
			{
				return (string)(base.ViewState["Fill"] ?? "");
			}
			set
			{
				base.ViewState["Fill"] = value;
			}
		}

		// Token: 0x17000CE8 RID: 3304
		// (get) Token: 0x060027D7 RID: 10199 RVA: 0x0008175D File Offset: 0x0007F95D
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public Fill FillSettings
		{
			get
			{
				if (this._fill == null)
				{
					this._fill = new Fill();
				}
				return this._fill;
			}
		}

		// Token: 0x060027D8 RID: 10200 RVA: 0x00081778 File Offset: 0x0007F978
		internal override void SetDirty()
		{
			base.SetDirty();
			this.FillSettings.SetDirty();
		}

		// Token: 0x060027D9 RID: 10201 RVA: 0x0008178C File Offset: 0x0007F98C
		protected override void LoadViewState(object state)
		{
			object[] array = (object[])state;
			int num = 0;
			base.LoadViewState(array[num++]);
			((IStateManager)this.FillSettings).LoadViewState(array[num++]);
		}

		// Token: 0x060027DA RID: 10202 RVA: 0x000817C4 File Offset: 0x0007F9C4
		protected override object SaveViewState()
		{
			return new object[]
			{
				base.SaveViewState(),
				((IStateManager)this.FillSettings).SaveViewState()
			};
		}

		// Token: 0x060027DB RID: 10203 RVA: 0x000817F2 File Offset: 0x0007F9F2
		protected override void TrackViewState()
		{
			base.TrackViewState();
			((IStateManager)this.FillSettings).TrackViewState();
		}

		// Token: 0x17000CE9 RID: 3305
		// (get) Token: 0x060027DC RID: 10204 RVA: 0x00081805 File Offset: 0x0007FA05
		public bool IsDefault
		{
			get
			{
				return this.Fill == "" && this.FillSettings.IsDefault;
			}
		}

		// Token: 0x04000A1F RID: 2591
		private Fill _fill;
	}
}
