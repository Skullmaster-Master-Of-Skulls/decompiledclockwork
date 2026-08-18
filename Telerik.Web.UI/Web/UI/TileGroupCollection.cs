using System;

namespace Telerik.Web.UI
{
	// Token: 0x0200091C RID: 2332
	public sealed class TileGroupCollection : StronglyTypedStateManagedCollection<TileGroup>
	{
		// Token: 0x0600585E RID: 22622 RVA: 0x0010DD6B File Offset: 0x0010BF6B
		internal TileGroupCollection(RadTileList tileList)
		{
			this._tileList = tileList;
		}

		// Token: 0x0600585F RID: 22623 RVA: 0x0010DD7C File Offset: 0x0010BF7C
		protected override void SetDirtyObject(object o)
		{
			StateManager stateManager = o as StateManager;
			if (stateManager != null)
			{
				stateManager.SetDirty();
			}
		}

		// Token: 0x06005860 RID: 22624 RVA: 0x0010DD99 File Offset: 0x0010BF99
		public override void Add(TileGroup item)
		{
			base.Add(item);
			item.TileList = this._tileList;
		}

		// Token: 0x04001590 RID: 5520
		private readonly RadTileList _tileList;
	}
}
