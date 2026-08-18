using System;
using System.Collections;
using System.Collections.Generic;
using System.Web.UI;

namespace Telerik.Web.UI.Timeline
{
	// Token: 0x02000922 RID: 2338
	public abstract class BaseCollection<T, TOwner> : StateManagedCollection, IEnumerable<!0>, IEnumerable where T : class, IMarkableStateManager
	{
		// Token: 0x060058C1 RID: 22721 RVA: 0x0010EC10 File Offset: 0x0010CE10
		public BaseCollection()
		{
			this._list = this;
		}

		// Token: 0x060058C2 RID: 22722 RVA: 0x0010EC1F File Offset: 0x0010CE1F
		public BaseCollection(TOwner owner) : this()
		{
			this._owner = owner;
		}

		// Token: 0x17001D55 RID: 7509
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

		// Token: 0x17001D56 RID: 7510
		// (get) Token: 0x060058C5 RID: 22725 RVA: 0x0010EC55 File Offset: 0x0010CE55
		public TOwner Owner
		{
			get
			{
				return this._owner;
			}
		}

		// Token: 0x060058C6 RID: 22726 RVA: 0x0010EDB0 File Offset: 0x0010CFB0
		public new IEnumerator<T> GetEnumerator()
		{
			foreach (object obj in this._list)
			{
				T entity = (T)((object)obj);
				yield return entity;
			}
			yield break;
		}

		// Token: 0x060058C7 RID: 22727 RVA: 0x0010EDCC File Offset: 0x0010CFCC
		protected override void SetDirtyObject(object o)
		{
			T t = o as T;
			if (t != null)
			{
				t.SetDirty();
			}
		}

		// Token: 0x060058C8 RID: 22728 RVA: 0x0010EDFA File Offset: 0x0010CFFA
		public virtual void Add(T entity)
		{
			this._list.Add(entity);
		}

		// Token: 0x060058C9 RID: 22729 RVA: 0x0010EE10 File Offset: 0x0010D010
		public virtual void AddRange(IEnumerable<T> entities)
		{
			foreach (T t in entities)
			{
				this._list.Add(t);
			}
		}

		// Token: 0x060058CA RID: 22730 RVA: 0x0010EE64 File Offset: 0x0010D064
		protected internal virtual void Remove(T item)
		{
			this._list.Remove(item);
		}

		// Token: 0x060058CB RID: 22731 RVA: 0x0010EE77 File Offset: 0x0010D077
		protected internal virtual void Insert(int index, T item)
		{
			this._list.Insert(index, item);
		}

		// Token: 0x060058CC RID: 22732 RVA: 0x0010EE8B File Offset: 0x0010D08B
		protected internal virtual bool Contains(T item)
		{
			return this._list.Contains(item);
		}

		// Token: 0x060058CD RID: 22733 RVA: 0x0010EE9E File Offset: 0x0010D09E
		protected internal virtual int IndexOf(T item)
		{
			return this._list.IndexOf(item);
		}

		// Token: 0x0400159C RID: 5532
		private readonly IList _list;

		// Token: 0x0400159D RID: 5533
		private readonly TOwner _owner;
	}
}
