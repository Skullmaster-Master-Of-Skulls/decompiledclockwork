using System;
using System.Collections.Generic;

namespace System.Web.Util
{
	// Token: 0x020001D9 RID: 473
	internal struct SubscriptionQueue<T>
	{
		// Token: 0x1700070A RID: 1802
		// (get) Token: 0x06001798 RID: 6040 RVA: 0x0004A03F File Offset: 0x0004823F
		public bool IsEmpty
		{
			get
			{
				return this._list == null || this._list.Count == 0;
			}
		}

		// Token: 0x06001799 RID: 6041 RVA: 0x0004A05C File Offset: 0x0004825C
		public ISubscriptionToken Enqueue(T value)
		{
			if (this._list == null)
			{
				this._list = new LinkedList<T>();
			}
			LinkedListNode<T> node = this._list.AddLast(value);
			return new SubscriptionQueue<T>.SubscriptionToken(node);
		}

		// Token: 0x0600179A RID: 6042 RVA: 0x0004A090 File Offset: 0x00048290
		public void FireAndComplete(Action<T> action)
		{
			try
			{
				T obj;
				while (this.TryDequeue(out obj))
				{
					action(obj);
				}
			}
			finally
			{
				this._list = null;
			}
		}

		// Token: 0x0600179B RID: 6043 RVA: 0x0004A0CC File Offset: 0x000482CC
		private bool TryDequeue(out T result)
		{
			if (this._list != null && this._list.First != null)
			{
				LinkedListNode<T> first = this._list.First;
				this._list.RemoveFirst();
				result = first.Value;
				first.Value = default(T);
				return true;
			}
			result = default(T);
			return false;
		}

		// Token: 0x0400171D RID: 5917
		private LinkedList<T> _list;

		// Token: 0x0200093A RID: 2362
		private sealed class SubscriptionToken : ISubscriptionToken
		{
			// Token: 0x06006968 RID: 26984 RVA: 0x00177450 File Offset: 0x00175650
			public SubscriptionToken(LinkedListNode<T> node)
			{
				this._node = node;
			}

			// Token: 0x17001D24 RID: 7460
			// (get) Token: 0x06006969 RID: 26985 RVA: 0x0017745F File Offset: 0x0017565F
			public bool IsActive
			{
				get
				{
					return this._node.List != null;
				}
			}

			// Token: 0x0600696A RID: 26986 RVA: 0x00177470 File Offset: 0x00175670
			public void Unsubscribe()
			{
				if (this.IsActive)
				{
					this._node.List.Remove(this._node);
					this._node.Value = default(T);
				}
			}

			// Token: 0x0400379C RID: 14236
			private readonly LinkedListNode<T> _node;
		}
	}
}
