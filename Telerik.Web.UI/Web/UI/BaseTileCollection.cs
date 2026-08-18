using System;

namespace Telerik.Web.UI
{
	// Token: 0x02000916 RID: 2326
	public sealed class BaseTileCollection : StronglyTypedStateManagedCollection<RadBaseTile>
	{
		// Token: 0x06005830 RID: 22576 RVA: 0x0010D7AF File Offset: 0x0010B9AF
		internal BaseTileCollection()
		{
		}

		// Token: 0x06005831 RID: 22577 RVA: 0x0010D7B8 File Offset: 0x0010B9B8
		protected override void SetDirtyObject(object o)
		{
			StateManager stateManager = o as StateManager;
			if (stateManager != null)
			{
				stateManager.SetDirty();
			}
		}

		// Token: 0x06005832 RID: 22578 RVA: 0x0010D7D5 File Offset: 0x0010B9D5
		public override void Add(RadBaseTile item)
		{
			base.Add(item);
			this.InitilizeTileProperties(item);
			this.RegisterTileEventHandlers(item);
		}

		// Token: 0x06005833 RID: 22579 RVA: 0x0010D7EC File Offset: 0x0010B9EC
		private void InitilizeTileProperties(RadBaseTile tile)
		{
			if (this._tileList != null)
			{
				tile.EnableSelection = (this._tileList.SelectionMode != TileListSelectionMode.None);
			}
		}

		// Token: 0x17001D22 RID: 7458
		// (get) Token: 0x06005834 RID: 22580 RVA: 0x0010D80D File Offset: 0x0010BA0D
		// (set) Token: 0x06005835 RID: 22581 RVA: 0x0010D815 File Offset: 0x0010BA15
		internal RadTileList TileList
		{
			get
			{
				return this._tileList;
			}
			set
			{
				this.UnRegisterTileListEventHandlers();
				this._tileList = value;
				this.InitilezeExistingTilsePropertis();
				this._clickEventHandler = new EventHandler(this._tileList.TileClickHandler);
				this.RegisterTileListEventHandlers();
			}
		}

		// Token: 0x06005836 RID: 22582 RVA: 0x0010D848 File Offset: 0x0010BA48
		private void UnRegisterTileListEventHandlers()
		{
			foreach (object obj in base.List)
			{
				RadBaseTile tile = (RadBaseTile)obj;
				this.UnRegisterTileEventHandlers(tile);
			}
		}

		// Token: 0x06005837 RID: 22583 RVA: 0x0010D8A4 File Offset: 0x0010BAA4
		private void UnRegisterTileEventHandlers(RadBaseTile tile)
		{
			if (this._clickEventHandler != null)
			{
				tile.Click -= this._clickEventHandler;
			}
		}

		// Token: 0x06005838 RID: 22584 RVA: 0x0010D8BC File Offset: 0x0010BABC
		private void RegisterTileListEventHandlers()
		{
			if (this._tileList != null)
			{
				foreach (object obj in base.List)
				{
					RadBaseTile tile = (RadBaseTile)obj;
					this.RegisterTileEventHandlers(tile);
				}
			}
		}

		// Token: 0x06005839 RID: 22585 RVA: 0x0010D920 File Offset: 0x0010BB20
		private void RegisterTileEventHandlers(RadBaseTile tile)
		{
			if (this._tileList != null)
			{
				this.UnRegisterTileEventHandlers(tile);
				tile.Click += this._clickEventHandler;
			}
		}

		// Token: 0x0600583A RID: 22586 RVA: 0x0010D940 File Offset: 0x0010BB40
		private void InitilezeExistingTilsePropertis()
		{
			if (this._tileList != null)
			{
				foreach (object obj in base.List)
				{
					RadBaseTile tile = (RadBaseTile)obj;
					this.InitilizeTileProperties(tile);
				}
				this._tileList.EnsureOnlyLastTileIsSelected();
			}
		}

		// Token: 0x0400157E RID: 5502
		private RadTileList _tileList;

		// Token: 0x0400157F RID: 5503
		private EventHandler _clickEventHandler;
	}
}
