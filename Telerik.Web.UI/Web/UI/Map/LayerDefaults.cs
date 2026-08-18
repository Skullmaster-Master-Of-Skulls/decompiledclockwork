using System;
using System.ComponentModel;
using System.Web.UI;

namespace Telerik.Web.UI.Map
{
	// Token: 0x0200059A RID: 1434
	public class LayerDefaults : StateManager, IDefaultCheck
	{
		// Token: 0x170010B2 RID: 4274
		// (get) Token: 0x0600336C RID: 13164 RVA: 0x000AB362 File Offset: 0x000A9562
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public Marker MarkerSettings
		{
			get
			{
				if (this._marker == null)
				{
					this._marker = new Marker();
				}
				return this._marker;
			}
		}

		// Token: 0x170010B3 RID: 4275
		// (get) Token: 0x0600336D RID: 13165 RVA: 0x000AB37D File Offset: 0x000A957D
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public Shape ShapeSettings
		{
			get
			{
				if (this._shape == null)
				{
					this._shape = new Shape();
				}
				return this._shape;
			}
		}

		// Token: 0x170010B4 RID: 4276
		// (get) Token: 0x0600336E RID: 13166 RVA: 0x000AB398 File Offset: 0x000A9598
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public Bubble BubbleSettings
		{
			get
			{
				if (this._bubble == null)
				{
					this._bubble = new Bubble();
				}
				return this._bubble;
			}
		}

		// Token: 0x170010B5 RID: 4277
		// (get) Token: 0x0600336F RID: 13167 RVA: 0x000AB3B3 File Offset: 0x000A95B3
		// (set) Token: 0x06003370 RID: 13168 RVA: 0x000AB3DC File Offset: 0x000A95DC
		[DefaultValue(256.0)]
		public double TileSize
		{
			get
			{
				return (double)(base.ViewState["TileSize"] ?? 256.0);
			}
			set
			{
				base.ViewState["TileSize"] = value;
			}
		}

		// Token: 0x170010B6 RID: 4278
		// (get) Token: 0x06003371 RID: 13169 RVA: 0x000AB3F4 File Offset: 0x000A95F4
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public Tile TileSettings
		{
			get
			{
				if (this._tile == null)
				{
					this._tile = new Tile();
				}
				return this._tile;
			}
		}

		// Token: 0x170010B7 RID: 4279
		// (get) Token: 0x06003372 RID: 13170 RVA: 0x000AB40F File Offset: 0x000A960F
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public Bing BingSettings
		{
			get
			{
				if (this._bing == null)
				{
					this._bing = new Bing();
				}
				return this._bing;
			}
		}

		// Token: 0x06003373 RID: 13171 RVA: 0x000AB42A File Offset: 0x000A962A
		internal override void SetDirty()
		{
			base.SetDirty();
			this.BingSettings.SetDirty();
			this.BubbleSettings.SetDirty();
			this.MarkerSettings.SetDirty();
			this.ShapeSettings.SetDirty();
			this.TileSettings.SetDirty();
		}

		// Token: 0x06003374 RID: 13172 RVA: 0x000AB46C File Offset: 0x000A966C
		protected override void LoadViewState(object state)
		{
			object[] array = (object[])state;
			int num = 0;
			base.LoadViewState(array[num++]);
			((IStateManager)this.BingSettings).LoadViewState(array[num++]);
			((IStateManager)this.BubbleSettings).LoadViewState(array[num++]);
			((IStateManager)this.MarkerSettings).LoadViewState(array[num++]);
			((IStateManager)this.ShapeSettings).LoadViewState(array[num++]);
			((IStateManager)this.TileSettings).LoadViewState(array[num++]);
		}

		// Token: 0x06003375 RID: 13173 RVA: 0x000AB4EC File Offset: 0x000A96EC
		protected override object SaveViewState()
		{
			return new object[]
			{
				base.SaveViewState(),
				((IStateManager)this.BingSettings).SaveViewState(),
				((IStateManager)this.BubbleSettings).SaveViewState(),
				((IStateManager)this.MarkerSettings).SaveViewState(),
				((IStateManager)this.ShapeSettings).SaveViewState(),
				((IStateManager)this.TileSettings).SaveViewState()
			};
		}

		// Token: 0x06003376 RID: 13174 RVA: 0x000AB552 File Offset: 0x000A9752
		protected override void TrackViewState()
		{
			base.TrackViewState();
			((IStateManager)this.BingSettings).TrackViewState();
			((IStateManager)this.BubbleSettings).TrackViewState();
			((IStateManager)this.MarkerSettings).TrackViewState();
			((IStateManager)this.ShapeSettings).TrackViewState();
			((IStateManager)this.TileSettings).TrackViewState();
		}

		// Token: 0x170010B8 RID: 4280
		// (get) Token: 0x06003377 RID: 13175 RVA: 0x000AB594 File Offset: 0x000A9794
		public bool IsDefault
		{
			get
			{
				return this.MarkerSettings.IsDefault && this.ShapeSettings.IsDefault && this.BubbleSettings.IsDefault && this.TileSize == 256.0 && this.TileSettings.IsDefault && this.BingSettings.IsDefault;
			}
		}

		// Token: 0x04000E11 RID: 3601
		private Marker _marker;

		// Token: 0x04000E12 RID: 3602
		private Shape _shape;

		// Token: 0x04000E13 RID: 3603
		private Bubble _bubble;

		// Token: 0x04000E14 RID: 3604
		private Tile _tile;

		// Token: 0x04000E15 RID: 3605
		private Bing _bing;
	}
}
