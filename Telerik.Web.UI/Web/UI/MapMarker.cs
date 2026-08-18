using System;
using System.ComponentModel;
using System.Web.UI;
using Telerik.Web.UI.Map;

namespace Telerik.Web.UI
{
	// Token: 0x020005A1 RID: 1441
	public class MapMarker : StateManager
	{
		// Token: 0x170010D6 RID: 4310
		// (get) Token: 0x060033B8 RID: 13240 RVA: 0x000AC046 File Offset: 0x000AA246
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public Location LocationSettings
		{
			get
			{
				if (this._location == null)
				{
					this._location = new Location();
				}
				return this._location;
			}
		}

		// Token: 0x170010D7 RID: 4311
		// (get) Token: 0x060033B9 RID: 13241 RVA: 0x000AC061 File Offset: 0x000AA261
		// (set) Token: 0x060033BA RID: 13242 RVA: 0x000AC081 File Offset: 0x000AA281
		[TypeConverter(typeof(MarkerShapeStringConverter))]
		[DefaultValue("pinTarget")]
		public string Shape
		{
			get
			{
				return (string)(base.ViewState["Shape"] ?? "pinTarget");
			}
			set
			{
				base.ViewState["Shape"] = value;
			}
		}

		// Token: 0x170010D8 RID: 4312
		// (get) Token: 0x060033BB RID: 13243 RVA: 0x000AC094 File Offset: 0x000AA294
		// (set) Token: 0x060033BC RID: 13244 RVA: 0x000AC0B4 File Offset: 0x000AA2B4
		[DefaultValue("pinTarget")]
		public string Title
		{
			get
			{
				return (string)(base.ViewState["Title"] ?? "pinTarget");
			}
			set
			{
				base.ViewState["Title"] = value;
			}
		}

		// Token: 0x170010D9 RID: 4313
		// (get) Token: 0x060033BD RID: 13245 RVA: 0x000AC0C7 File Offset: 0x000AA2C7
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public Tooltip TooltipSettings
		{
			get
			{
				if (this._tooltip == null)
				{
					this._tooltip = new Tooltip();
				}
				return this._tooltip;
			}
		}

		// Token: 0x060033BE RID: 13246 RVA: 0x000AC0E2 File Offset: 0x000AA2E2
		internal override void SetDirty()
		{
			base.SetDirty();
			this.LocationSettings.SetDirty();
			this.TooltipSettings.SetDirty();
		}

		// Token: 0x060033BF RID: 13247 RVA: 0x000AC100 File Offset: 0x000AA300
		protected override void LoadViewState(object state)
		{
			object[] array = (object[])state;
			int num = 0;
			base.LoadViewState(array[num++]);
			((IStateManager)this.LocationSettings).LoadViewState(array[num++]);
			((IStateManager)this.TooltipSettings).LoadViewState(array[num++]);
		}

		// Token: 0x060033C0 RID: 13248 RVA: 0x000AC148 File Offset: 0x000AA348
		protected override object SaveViewState()
		{
			return new object[]
			{
				base.SaveViewState(),
				((IStateManager)this.LocationSettings).SaveViewState(),
				((IStateManager)this.TooltipSettings).SaveViewState()
			};
		}

		// Token: 0x060033C1 RID: 13249 RVA: 0x000AC184 File Offset: 0x000AA384
		protected override void TrackViewState()
		{
			base.TrackViewState();
			((IStateManager)this.LocationSettings).TrackViewState();
			((IStateManager)this.TooltipSettings).TrackViewState();
		}

		// Token: 0x04000E1F RID: 3615
		private Location _location;

		// Token: 0x04000E20 RID: 3616
		private Tooltip _tooltip;
	}
}
