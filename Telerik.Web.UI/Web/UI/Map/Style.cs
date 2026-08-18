using System;
using System.Web.UI;

namespace Telerik.Web.UI.Map
{
	// Token: 0x020005B3 RID: 1459
	public class Style : StateManager, IDefaultCheck
	{
		// Token: 0x170010FB RID: 4347
		// (get) Token: 0x06003417 RID: 13335 RVA: 0x000ACEC6 File Offset: 0x000AB0C6
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

		// Token: 0x170010FC RID: 4348
		// (get) Token: 0x06003418 RID: 13336 RVA: 0x000ACEE1 File Offset: 0x000AB0E1
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

		// Token: 0x06003419 RID: 13337 RVA: 0x000ACEFC File Offset: 0x000AB0FC
		internal override void SetDirty()
		{
			base.SetDirty();
			this.FillSettings.SetDirty();
			this.StrokeSettings.SetDirty();
		}

		// Token: 0x0600341A RID: 13338 RVA: 0x000ACF1C File Offset: 0x000AB11C
		protected override void LoadViewState(object state)
		{
			object[] array = (object[])state;
			int num = 0;
			base.LoadViewState(array[num++]);
			((IStateManager)this.FillSettings).LoadViewState(array[num++]);
			((IStateManager)this.StrokeSettings).LoadViewState(array[num++]);
		}

		// Token: 0x0600341B RID: 13339 RVA: 0x000ACF64 File Offset: 0x000AB164
		protected override object SaveViewState()
		{
			return new object[]
			{
				base.SaveViewState(),
				((IStateManager)this.FillSettings).SaveViewState(),
				((IStateManager)this.StrokeSettings).SaveViewState()
			};
		}

		// Token: 0x0600341C RID: 13340 RVA: 0x000ACFA0 File Offset: 0x000AB1A0
		protected override void TrackViewState()
		{
			base.TrackViewState();
			((IStateManager)this.FillSettings).TrackViewState();
			((IStateManager)this.StrokeSettings).TrackViewState();
		}

		// Token: 0x170010FD RID: 4349
		// (get) Token: 0x0600341D RID: 13341 RVA: 0x000ACFBE File Offset: 0x000AB1BE
		public bool IsDefault
		{
			get
			{
				return this.FillSettings.IsDefault && this.StrokeSettings.IsDefault;
			}
		}

		// Token: 0x04000E2C RID: 3628
		private Fill _fill;

		// Token: 0x04000E2D RID: 3629
		private Stroke _stroke;
	}
}
