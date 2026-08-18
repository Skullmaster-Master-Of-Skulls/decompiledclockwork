using System;
using System.Collections.Generic;
using System.Linq;
using TechnoPro.Common.Public.Entities;

namespace TechnoPro.Common.UI.Web.Entity.Web
{
	// Token: 0x02000013 RID: 19
	public class ShoppingCart<TU, TV> where TV : BusinessBase<TU>
	{
		// Token: 0x1700001B RID: 27
		// (get) Token: 0x0600004B RID: 75 RVA: 0x00002508 File Offset: 0x00000708
		public IList<TV> Items
		{
			get
			{
				return this._items;
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x0600004C RID: 76 RVA: 0x00002520 File Offset: 0x00000720
		public int Count
		{
			get
			{
				return this._items.Count;
			}
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00002540 File Offset: 0x00000740
		public void Remove(TU id)
		{
			TV tv = this._items.FirstOrDefault(delegate(TV c)
			{
				TU id2 = c.Id;
				return id2.Equals(id);
			});
			bool flag = tv != null;
			if (flag)
			{
				this._items.Remove(tv);
			}
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00002590 File Offset: 0x00000790
		public void RemoveAll(IList<TU> idList)
		{
			foreach (TU id in idList)
			{
				this.Remove(id);
			}
		}

		// Token: 0x1700001D RID: 29
		public TV this[TU id]
		{
			get
			{
				return this._items.FirstOrDefault(delegate(TV c)
				{
					TU id2 = c.Id;
					return id2.Equals(id);
				});
			}
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00002614 File Offset: 0x00000814
		public bool Contains(TU id)
		{
			return this._items.Any(delegate(TV c)
			{
				TU id2 = c.Id;
				return id2.Equals(id);
			});
		}

		// Token: 0x06000051 RID: 81 RVA: 0x0000264A File Offset: 0x0000084A
		public void Add(TV item)
		{
			this._items.Add(item);
		}

		// Token: 0x06000052 RID: 82 RVA: 0x0000265A File Offset: 0x0000085A
		public void Clear()
		{
			this._items.Clear();
		}

		// Token: 0x04000077 RID: 119
		private IList<TV> _items = new List<TV>();
	}
}
