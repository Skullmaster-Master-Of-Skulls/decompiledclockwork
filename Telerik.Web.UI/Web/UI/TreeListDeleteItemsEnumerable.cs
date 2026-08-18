using System;
using System.Collections;
using System.Collections.Generic;

namespace Telerik.Web.UI
{
	// Token: 0x02001239 RID: 4665
	internal class TreeListDeleteItemsEnumerable : IEnumerable<TreeListSourceItem>, IEnumerable
	{
		// Token: 0x0600C06A RID: 49258 RVA: 0x002AB332 File Offset: 0x002A9532
		public TreeListDeleteItemsEnumerable()
		{
			this._itemsToDelete = new List<TreeListSourceItem>();
		}

		// Token: 0x0600C06B RID: 49259 RVA: 0x002AB345 File Offset: 0x002A9545
		public void Add(TreeListSourceItem item)
		{
			this._itemsToDelete.Add(item);
		}

		// Token: 0x0600C06C RID: 49260 RVA: 0x002AB408 File Offset: 0x002A9608
		public IEnumerator<TreeListSourceItem> GetEnumerator()
		{
			for (int i = this._itemsToDelete.Count - 1; i >= 0; i--)
			{
				yield return this._itemsToDelete[i];
			}
			yield break;
		}

		// Token: 0x0600C06D RID: 49261 RVA: 0x002AB424 File Offset: 0x002A9624
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x040032AD RID: 12973
		private List<TreeListSourceItem> _itemsToDelete;
	}
}
