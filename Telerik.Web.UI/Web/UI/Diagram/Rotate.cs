using System;
using System.Web.UI;

namespace Telerik.Web.UI.Diagram
{
	// Token: 0x020002AB RID: 683
	public class Rotate : StateManager, IDefaultCheck
	{
		// Token: 0x1700083B RID: 2107
		// (get) Token: 0x0600181C RID: 6172 RVA: 0x0004FD62 File Offset: 0x0004DF62
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

		// Token: 0x1700083C RID: 2108
		// (get) Token: 0x0600181D RID: 6173 RVA: 0x0004FD7D File Offset: 0x0004DF7D
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public Stroke StrokeSettings
		{
			get
			{
				if (this._stroke == null)
				{
					this._stroke = new Stroke();
				}
				return this._stroke;
			}
		}

		// Token: 0x0600181E RID: 6174 RVA: 0x0004FD98 File Offset: 0x0004DF98
		internal override void SetDirty()
		{
			base.SetDirty();
			this.FillSettings.SetDirty();
			this.StrokeSettings.SetDirty();
		}

		// Token: 0x0600181F RID: 6175 RVA: 0x0004FDB8 File Offset: 0x0004DFB8
		protected override void LoadViewState(object state)
		{
			object[] array = (object[])state;
			int num = 0;
			base.LoadViewState(array[num++]);
			((IStateManager)this.FillSettings).LoadViewState(array[num++]);
			((IStateManager)this.StrokeSettings).LoadViewState(array[num++]);
		}

		// Token: 0x06001820 RID: 6176 RVA: 0x0004FE00 File Offset: 0x0004E000
		protected override object SaveViewState()
		{
			return new object[]
			{
				base.SaveViewState(),
				((IStateManager)this.FillSettings).SaveViewState(),
				((IStateManager)this.StrokeSettings).SaveViewState()
			};
		}

		// Token: 0x06001821 RID: 6177 RVA: 0x0004FE3C File Offset: 0x0004E03C
		protected override void TrackViewState()
		{
			base.TrackViewState();
			((IStateManager)this.FillSettings).TrackViewState();
			((IStateManager)this.StrokeSettings).TrackViewState();
		}

		// Token: 0x1700083D RID: 2109
		// (get) Token: 0x06001822 RID: 6178 RVA: 0x0004FE5A File Offset: 0x0004E05A
		public bool IsDefault
		{
			get
			{
				return this.FillSettings.IsDefault && this.StrokeSettings.IsDefault;
			}
		}

		// Token: 0x04000670 RID: 1648
		private Fill _fill;

		// Token: 0x04000671 RID: 1649
		private Stroke _stroke;
	}
}
