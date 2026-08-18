using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x0200091B RID: 2331
	[ToolboxItem(false)]
	[ParseChildren(true, "Tiles")]
	public class TileGroup : StateManager
	{
		// Token: 0x17001D2D RID: 7469
		// (get) Token: 0x06005854 RID: 22612 RVA: 0x0010DC1C File Offset: 0x0010BE1C
		// (set) Token: 0x06005855 RID: 22613 RVA: 0x0010DC3C File Offset: 0x0010BE3C
		[DefaultValue("")]
		[Category("Behavior")]
		[Description("Gets or sets the Name property of a tile group.")]
		public string Name
		{
			get
			{
				return ((string)base.ViewState["Name"]) ?? string.Empty;
			}
			set
			{
				base.ViewState["Name"] = value;
			}
		}

		// Token: 0x17001D2E RID: 7470
		// (get) Token: 0x06005856 RID: 22614 RVA: 0x0010DC4F File Offset: 0x0010BE4F
		// (set) Token: 0x06005857 RID: 22615 RVA: 0x0010DC6F File Offset: 0x0010BE6F
		[Description("Gets or sets the Title property of a tile group.")]
		[DefaultValue("")]
		[Category("Behavior")]
		public string Title
		{
			get
			{
				return ((string)base.ViewState["Title"]) ?? string.Empty;
			}
			set
			{
				base.ViewState["Title"] = value;
			}
		}

		// Token: 0x06005858 RID: 22616 RVA: 0x0010DC84 File Offset: 0x0010BE84
		public List<RadBaseTile> GetAllTiles()
		{
			List<RadBaseTile> list = new List<RadBaseTile>();
			foreach (object obj in this.Tiles)
			{
				RadBaseTile item = (RadBaseTile)obj;
				list.Add(item);
			}
			return list;
		}

		// Token: 0x06005859 RID: 22617 RVA: 0x0010DCEC File Offset: 0x0010BEEC
		public List<RadBaseTile> GetSelectedTiles()
		{
			return (from t in this.GetAllTiles()
			where t.Selected
			select t).ToList<RadBaseTile>();
		}

		// Token: 0x17001D2F RID: 7471
		// (get) Token: 0x0600585A RID: 22618 RVA: 0x0010DD1B File Offset: 0x0010BF1B
		[PersistenceMode(PersistenceMode.InnerDefaultProperty)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public BaseTileCollection Tiles
		{
			get
			{
				if (this._tiles == null)
				{
					this._tiles = new BaseTileCollection();
					if (this.IsTrackingViewState)
					{
						((IStateManager)this._tiles).TrackViewState();
					}
				}
				return this._tiles;
			}
		}

		// Token: 0x17001D30 RID: 7472
		// (get) Token: 0x0600585B RID: 22619 RVA: 0x0010DD49 File Offset: 0x0010BF49
		// (set) Token: 0x0600585C RID: 22620 RVA: 0x0010DD51 File Offset: 0x0010BF51
		internal RadTileList TileList
		{
			get
			{
				return this._tileList;
			}
			set
			{
				this._tileList = value;
				this.Tiles.TileList = this._tileList;
			}
		}

		// Token: 0x0400158D RID: 5517
		private BaseTileCollection _tiles;

		// Token: 0x0400158E RID: 5518
		private RadTileList _tileList;
	}
}
