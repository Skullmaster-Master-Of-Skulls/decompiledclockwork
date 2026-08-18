using System;

namespace Telerik.Web.UI
{
	// Token: 0x02000915 RID: 2325
	public class TileListEventArgs : EventArgs
	{
		// Token: 0x17001D21 RID: 7457
		// (get) Token: 0x0600582E RID: 22574 RVA: 0x0010D798 File Offset: 0x0010B998
		public RadBaseTile Tile
		{
			get
			{
				return this._tile;
			}
		}

		// Token: 0x0600582F RID: 22575 RVA: 0x0010D7A0 File Offset: 0x0010B9A0
		public TileListEventArgs(RadBaseTile tile)
		{
			this._tile = tile;
		}

		// Token: 0x0400157D RID: 5501
		private readonly RadBaseTile _tile;
	}
}
