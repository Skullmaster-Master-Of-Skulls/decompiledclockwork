using System;
using System.ComponentModel;
using System.Web.UI;

namespace Telerik.Web.UI.Map
{
	// Token: 0x02000596 RID: 1430
	public class Controls : StateManager, IDefaultCheck
	{
		// Token: 0x170010A6 RID: 4262
		// (get) Token: 0x06003351 RID: 13137 RVA: 0x000AAF16 File Offset: 0x000A9116
		// (set) Token: 0x06003352 RID: 13138 RVA: 0x000AAF37 File Offset: 0x000A9137
		[DefaultValue(true)]
		public bool Attribution
		{
			get
			{
				return (bool)(base.ViewState["Attribution"] ?? true);
			}
			set
			{
				base.ViewState["Attribution"] = value;
			}
		}

		// Token: 0x170010A7 RID: 4263
		// (get) Token: 0x06003353 RID: 13139 RVA: 0x000AAF4F File Offset: 0x000A914F
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public Attribution AttributionSettings
		{
			get
			{
				if (this._attribution == null)
				{
					this._attribution = new Attribution();
				}
				return this._attribution;
			}
		}

		// Token: 0x170010A8 RID: 4264
		// (get) Token: 0x06003354 RID: 13140 RVA: 0x000AAF6A File Offset: 0x000A916A
		// (set) Token: 0x06003355 RID: 13141 RVA: 0x000AAF8B File Offset: 0x000A918B
		[DefaultValue(true)]
		public bool Navigator
		{
			get
			{
				return (bool)(base.ViewState["Navigator"] ?? true);
			}
			set
			{
				base.ViewState["Navigator"] = value;
			}
		}

		// Token: 0x170010A9 RID: 4265
		// (get) Token: 0x06003356 RID: 13142 RVA: 0x000AAFA3 File Offset: 0x000A91A3
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public Navigator NavigatorSettings
		{
			get
			{
				if (this._navigator == null)
				{
					this._navigator = new Navigator();
				}
				return this._navigator;
			}
		}

		// Token: 0x170010AA RID: 4266
		// (get) Token: 0x06003357 RID: 13143 RVA: 0x000AAFBE File Offset: 0x000A91BE
		// (set) Token: 0x06003358 RID: 13144 RVA: 0x000AAFDF File Offset: 0x000A91DF
		[DefaultValue(true)]
		public bool Zoom
		{
			get
			{
				return (bool)(base.ViewState["Zoom"] ?? true);
			}
			set
			{
				base.ViewState["Zoom"] = value;
			}
		}

		// Token: 0x170010AB RID: 4267
		// (get) Token: 0x06003359 RID: 13145 RVA: 0x000AAFF7 File Offset: 0x000A91F7
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public Zoom ZoomSettings
		{
			get
			{
				if (this._zoom == null)
				{
					this._zoom = new Zoom();
				}
				return this._zoom;
			}
		}

		// Token: 0x0600335A RID: 13146 RVA: 0x000AB012 File Offset: 0x000A9212
		internal override void SetDirty()
		{
			base.SetDirty();
			this.AttributionSettings.SetDirty();
			this.NavigatorSettings.SetDirty();
			this.ZoomSettings.SetDirty();
		}

		// Token: 0x0600335B RID: 13147 RVA: 0x000AB03C File Offset: 0x000A923C
		protected override void LoadViewState(object state)
		{
			object[] array = (object[])state;
			int num = 0;
			base.LoadViewState(array[num++]);
			((IStateManager)this.AttributionSettings).LoadViewState(array[num++]);
			((IStateManager)this.NavigatorSettings).LoadViewState(array[num++]);
			((IStateManager)this.ZoomSettings).LoadViewState(array[num++]);
		}

		// Token: 0x0600335C RID: 13148 RVA: 0x000AB098 File Offset: 0x000A9298
		protected override object SaveViewState()
		{
			return new object[]
			{
				base.SaveViewState(),
				((IStateManager)this.AttributionSettings).SaveViewState(),
				((IStateManager)this.NavigatorSettings).SaveViewState(),
				((IStateManager)this.ZoomSettings).SaveViewState()
			};
		}

		// Token: 0x0600335D RID: 13149 RVA: 0x000AB0E2 File Offset: 0x000A92E2
		protected override void TrackViewState()
		{
			base.TrackViewState();
			((IStateManager)this.AttributionSettings).TrackViewState();
			((IStateManager)this.NavigatorSettings).TrackViewState();
			((IStateManager)this.ZoomSettings).TrackViewState();
		}

		// Token: 0x170010AC RID: 4268
		// (get) Token: 0x0600335E RID: 13150 RVA: 0x000AB10C File Offset: 0x000A930C
		public bool IsDefault
		{
			get
			{
				return this.Attribution && this.AttributionSettings.IsDefault && this.Navigator && this.NavigatorSettings.IsDefault && this.Zoom && this.ZoomSettings.IsDefault;
			}
		}

		// Token: 0x04000E0E RID: 3598
		private Attribution _attribution;

		// Token: 0x04000E0F RID: 3599
		private Navigator _navigator;

		// Token: 0x04000E10 RID: 3600
		private Zoom _zoom;
	}
}
