using System;
using System.Collections;
using System.Collections.Generic;
using System.Web.UI;

namespace Telerik.Web.UI.MultiColumnComboBox
{
	// Token: 0x020005E8 RID: 1512
	public abstract class BaseCollection<T> : StateManagedCollection, IEnumerable<!0>, IEnumerable where T : class, IMarkableStateManager
	{
		// Token: 0x060036BB RID: 14011 RVA: 0x000B56B6 File Offset: 0x000B38B6
		public BaseCollection()
		{
			this._list = this;
		}

		// Token: 0x060036BC RID: 14012 RVA: 0x000B56C5 File Offset: 0x000B38C5
		public BaseCollection(RadMultiColumnComboBox owner) : this()
		{
			this._owner = owner;
		}

		// Token: 0x170011F3 RID: 4595
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

		// Token: 0x170011F4 RID: 4596
		// (get) Token: 0x060036BF RID: 14015 RVA: 0x000B56FB File Offset: 0x000B38FB
		public RadMultiColumnComboBox Owner
		{
			get
			{
				return this._owner;
			}
		}

		// Token: 0x060036C0 RID: 14016 RVA: 0x000B5854 File Offset: 0x000B3A54
		public new IEnumerator<T> GetEnumerator()
		{
			foreach (object obj in this._list)
			{
				T entity = (T)((object)obj);
				yield return entity;
			}
			yield break;
		}

		// Token: 0x060036C1 RID: 14017 RVA: 0x000B5870 File Offset: 0x000B3A70
		protected override void SetDirtyObject(object o)
		{
			T t = o as T;
			if (t != null)
			{
				t.SetDirty();
			}
		}

		// Token: 0x060036C2 RID: 14018 RVA: 0x000B589E File Offset: 0x000B3A9E
		public virtual void Add(T entity)
		{
			this._list.Add(entity);
		}

		// Token: 0x060036C3 RID: 14019 RVA: 0x000B58B4 File Offset: 0x000B3AB4
		public virtual void AddRange(IEnumerable<T> entities)
		{
			foreach (T t in entities)
			{
				this._list.Add(t);
			}
		}

		// Token: 0x060036C4 RID: 14020 RVA: 0x000B5908 File Offset: 0x000B3B08
		protected internal virtual void Remove(T item)
		{
			this._list.Remove(item);
		}

		// Token: 0x060036C5 RID: 14021 RVA: 0x000B591B File Offset: 0x000B3B1B
		protected internal virtual void Insert(int index, T item)
		{
			this._list.Insert(index, item);
		}

		// Token: 0x060036C6 RID: 14022 RVA: 0x000B592F File Offset: 0x000B3B2F
		protected internal virtual bool Contains(T item)
		{
			return this._list.Contains(item);
		}

		// Token: 0x060036C7 RID: 14023 RVA: 0x000B5942 File Offset: 0x000B3B42
		protected internal virtual int IndexOf(T item)
		{
			return this._list.IndexOf(item);
		}

		// Token: 0x04000EC4 RID: 3780
		private readonly IList _list;

		// Token: 0x04000EC5 RID: 3781
		private readonly RadMultiColumnComboBox _owner;
	}
}
