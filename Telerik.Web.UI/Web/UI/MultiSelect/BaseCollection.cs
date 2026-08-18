using System;
using System.Collections;
using System.Collections.Generic;
using System.Web.UI;

namespace Telerik.Web.UI.MultiSelect
{
	// Token: 0x020005F8 RID: 1528
	public abstract class BaseCollection<T> : StateManagedCollection, IEnumerable<!0>, IEnumerable where T : class, IMarkableStateManager
	{
		// Token: 0x06003737 RID: 14135 RVA: 0x000B6D02 File Offset: 0x000B4F02
		public BaseCollection()
		{
			this._list = this;
		}

		// Token: 0x06003738 RID: 14136 RVA: 0x000B6D11 File Offset: 0x000B4F11
		public BaseCollection(RadMultiSelect owner) : this()
		{
			this._owner = owner;
		}

		// Token: 0x1700121E RID: 4638
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

		// Token: 0x1700121F RID: 4639
		// (get) Token: 0x0600373B RID: 14139 RVA: 0x000B6D47 File Offset: 0x000B4F47
		public RadMultiSelect Owner
		{
			get
			{
				return this._owner;
			}
		}

		// Token: 0x0600373C RID: 14140 RVA: 0x000B6EA0 File Offset: 0x000B50A0
		public new IEnumerator<T> GetEnumerator()
		{
			foreach (object obj in this._list)
			{
				T entity = (T)((object)obj);
				yield return entity;
			}
			yield break;
		}

		// Token: 0x0600373D RID: 14141 RVA: 0x000B6EBC File Offset: 0x000B50BC
		protected override void SetDirtyObject(object o)
		{
			T t = o as T;
			if (t != null)
			{
				t.SetDirty();
			}
		}

		// Token: 0x0600373E RID: 14142 RVA: 0x000B6EEA File Offset: 0x000B50EA
		public virtual void Add(T entity)
		{
			this._list.Add(entity);
		}

		// Token: 0x0600373F RID: 14143 RVA: 0x000B6F00 File Offset: 0x000B5100
		public virtual void AddRange(IEnumerable<T> entities)
		{
			foreach (T t in entities)
			{
				this._list.Add(t);
			}
		}

		// Token: 0x06003740 RID: 14144 RVA: 0x000B6F54 File Offset: 0x000B5154
		protected internal virtual void Remove(T item)
		{
			this._list.Remove(item);
		}

		// Token: 0x06003741 RID: 14145 RVA: 0x000B6F67 File Offset: 0x000B5167
		protected internal virtual void Insert(int index, T item)
		{
			this._list.Insert(index, item);
		}

		// Token: 0x06003742 RID: 14146 RVA: 0x000B6F7B File Offset: 0x000B517B
		protected internal virtual bool Contains(T item)
		{
			return this._list.Contains(item);
		}

		// Token: 0x06003743 RID: 14147 RVA: 0x000B6F8E File Offset: 0x000B518E
		protected internal virtual int IndexOf(T item)
		{
			return this._list.IndexOf(item);
		}

		// Token: 0x04000ECA RID: 3786
		private readonly IList _list;

		// Token: 0x04000ECB RID: 3787
		private readonly RadMultiSelect _owner;
	}
}
