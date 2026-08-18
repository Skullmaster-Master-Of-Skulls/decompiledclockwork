using System;
using System.Collections.Generic;

namespace System.Linq.Expressions.Compiler
{
	// Token: 0x0200027F RID: 639
	internal sealed class KeyedQueue<K, V>
	{
		// Token: 0x060016E6 RID: 5862 RVA: 0x0004CEFF File Offset: 0x0004B0FF
		internal KeyedQueue()
		{
			this._data = new Dictionary<K, Queue<V>>();
		}

		// Token: 0x060016E7 RID: 5863 RVA: 0x0004CF14 File Offset: 0x0004B114
		internal void Enqueue(K key, V value)
		{
			Queue<V> queue;
			if (!this._data.TryGetValue(key, out queue))
			{
				this._data.Add(key, queue = new Queue<V>());
			}
			queue.Enqueue(value);
		}

		// Token: 0x060016E8 RID: 5864 RVA: 0x0004CF4C File Offset: 0x0004B14C
		internal V Dequeue(K key)
		{
			Queue<V> queue;
			if (!this._data.TryGetValue(key, out queue))
			{
				throw Error.QueueEmpty();
			}
			V result = queue.Dequeue();
			if (queue.Count == 0)
			{
				this._data.Remove(key);
			}
			return result;
		}

		// Token: 0x060016E9 RID: 5865 RVA: 0x0004CF8C File Offset: 0x0004B18C
		internal bool TryDequeue(K key, out V value)
		{
			Queue<V> queue;
			if (this._data.TryGetValue(key, out queue) && queue.Count > 0)
			{
				value = queue.Dequeue();
				if (queue.Count == 0)
				{
					this._data.Remove(key);
				}
				return true;
			}
			value = default(V);
			return false;
		}

		// Token: 0x060016EA RID: 5866 RVA: 0x0004CFE0 File Offset: 0x0004B1E0
		internal V Peek(K key)
		{
			Queue<V> queue;
			if (!this._data.TryGetValue(key, out queue))
			{
				throw Error.QueueEmpty();
			}
			return queue.Peek();
		}

		// Token: 0x060016EB RID: 5867 RVA: 0x0004D00C File Offset: 0x0004B20C
		internal int GetCount(K key)
		{
			Queue<V> queue;
			if (!this._data.TryGetValue(key, out queue))
			{
				return 0;
			}
			return queue.Count;
		}

		// Token: 0x060016EC RID: 5868 RVA: 0x0004D031 File Offset: 0x0004B231
		internal void Clear()
		{
			this._data.Clear();
		}

		// Token: 0x04000B4B RID: 2891
		private readonly Dictionary<K, Queue<V>> _data;
	}
}
