using System;
using System.Collections;
using System.Collections.Generic;
using System.Web.UI;

namespace Telerik.Web.UI.Gantt
{
	// Token: 0x02000045 RID: 69
	public abstract class BaseCollection<T> : StateManagedCollection, IEnumerable<T>, IEnumerable where T : class, IMarkableStateManager
	{
		// Token: 0x0600023D RID: 573 RVA: 0x00006296 File Offset: 0x00004496
		public BaseCollection()
		{
			this._list = this;
		}

		// Token: 0x0600023E RID: 574 RVA: 0x000062A5 File Offset: 0x000044A5
		public BaseCollection(IGantt owner) : this()
		{
			this._owner = owner;
		}

		// Token: 0x170000D6 RID: 214
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

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x06000241 RID: 577 RVA: 0x000062DB File Offset: 0x000044DB
		public IGantt Owner
		{
			get
			{
				return this._owner;
			}
		}

		// Token: 0x06000242 RID: 578 RVA: 0x00006434 File Offset: 0x00004634
		public new IEnumerator<T> GetEnumerator()
		{
			foreach (object obj in this._list)
			{
				T entity = (T)((object)obj);
				yield return entity;
			}
			yield break;
		}

		// Token: 0x06000243 RID: 579 RVA: 0x00006450 File Offset: 0x00004650
		protected override void SetDirtyObject(object o)
		{
			T t = o as T;
			if (t != null)
			{
				t.SetDirty();
			}
		}

		// Token: 0x06000244 RID: 580 RVA: 0x0000647E File Offset: 0x0000467E
		public virtual void Add(T entity)
		{
			this._list.Add(entity);
		}

		// Token: 0x06000245 RID: 581 RVA: 0x00006494 File Offset: 0x00004694
		public virtual void AddRange(IEnumerable<T> entities)
		{
			foreach (T t in entities)
			{
				this._list.Add(t);
			}
		}

		// Token: 0x0400004F RID: 79
		private readonly IList _list;

		// Token: 0x04000050 RID: 80
		private readonly IGantt _owner;
	}
}
