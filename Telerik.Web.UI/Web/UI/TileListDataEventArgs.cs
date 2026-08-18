using System;
using System.Collections.Generic;

namespace Telerik.Web.UI
{
	// Token: 0x020008FC RID: 2300
	public class TileListDataEventArgs : EventArgs
	{
		// Token: 0x17001CB6 RID: 7350
		// (get) Token: 0x060056CF RID: 22223 RVA: 0x00109DA9 File Offset: 0x00107FA9
		public List<RadBaseTile> Tiles
		{
			get
			{
				return this._tiles;
			}
		}

		// Token: 0x060056D0 RID: 22224 RVA: 0x00109DB1 File Offset: 0x00107FB1
		public TileListDataEventArgs(List<RadBaseTile> tiles)
		{
			this._tiles = tiles;
		}

		// Token: 0x04001538 RID: 5432
		private readonly List<RadBaseTile> _tiles;
	}
}
