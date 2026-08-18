using System;
using System.Collections;
using System.Collections.Generic;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x02000894 RID: 2196
	public abstract class SpreadsheetBaseCollection<T> : StateManagedCollection, IEnumerable<!0>, IEnumerable where T : class, IMarkableStateManager
	{
		// Token: 0x060051BE RID: 20926 RVA: 0x000FED9C File Offset: 0x000FCF9C
		public SpreadsheetBaseCollection()
		{
			this._list = this;
		}

		// Token: 0x060051BF RID: 20927 RVA: 0x000FEDAB File Offset: 0x000FCFAB
		public SpreadsheetBaseCollection(ISpreadsheet owner) : this()
		{
			this._owner = owner;
		}

		// Token: 0x17001ACA RID: 6858
		public T this[int index]
		{
			get
			{
				return (T)((object)this._list[index]);
			}
			set
			{
				this._list[index] = value;
			}
		}

		// Token: 0x17001ACB RID: 6859
		// (get) Token: 0x060051C2 RID: 20930 RVA: 0x000FEDE1 File Offset: 0x000FCFE1
		public ISpreadsheet Owner
		{
			get
			{
				return this._owner;
			}
		}

		// Token: 0x060051C3 RID: 20931 RVA: 0x000FEF3C File Offset: 0x000FD13C
		public new IEnumerator<T> GetEnumerator()
		{
			foreach (object obj in this._list)
			{
				T entity = (T)((object)obj);
				yield return entity;
			}
			yield break;
		}

		// Token: 0x060051C4 RID: 20932 RVA: 0x000FEF58 File Offset: 0x000FD158
		protected override void SetDirtyObject(object o)
		{
			T t = o as T;
			if (t != null)
			{
				t.SetDirty();
			}
		}

		// Token: 0x060051C5 RID: 20933 RVA: 0x000FEF86 File Offset: 0x000FD186
		public virtual void Add(T entity)
		{
			this._list.Add(entity);
		}

		// Token: 0x060051C6 RID: 20934 RVA: 0x000FEF9C File Offset: 0x000FD19C
		public virtual void AddRange(IEnumerable<T> entities)
		{
			foreach (T t in entities)
			{
				this._list.Add(t);
			}
		}

		// Token: 0x040013FE RID: 5118
		private readonly IList _list;

		// Token: 0x040013FF RID: 5119
		private readonly ISpreadsheet _owner;
	}
}
