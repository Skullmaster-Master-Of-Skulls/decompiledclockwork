using System;
using System.Collections.Generic;

namespace System.Runtime
{
	// Token: 0x02000015 RID: 21
	internal class DuplicateDetector<T> where T : class
	{
		// Token: 0x0600007F RID: 127 RVA: 0x000036DE File Offset: 0x000018DE
		public DuplicateDetector(int capacity)
		{
			this.capacity = capacity;
			this.items = new Dictionary<T, LinkedListNode<T>>();
			this.fifoList = new LinkedList<T>();
			this.thisLock = new object();
		}

		// Token: 0x06000080 RID: 128 RVA: 0x00003710 File Offset: 0x00001910
		public bool AddIfNotDuplicate(T value)
		{
			bool result = false;
			object obj = this.thisLock;
			lock (obj)
			{
				if (!this.items.ContainsKey(value))
				{
					this.Add(value);
					result = true;
				}
			}
			return result;
		}

		// Token: 0x06000081 RID: 129 RVA: 0x00003764 File Offset: 0x00001964
		private void Add(T value)
		{
			if (this.items.Count == this.capacity)
			{
				LinkedListNode<T> last = this.fifoList.Last;
				this.items.Remove(last.Value);
				this.fifoList.Remove(last);
			}
			this.items.Add(value, this.fifoList.AddFirst(value));
		}

		// Token: 0x06000082 RID: 130 RVA: 0x000037C8 File Offset: 0x000019C8
		public bool Remove(T value)
		{
			bool result = false;
			object obj = this.thisLock;
			lock (obj)
			{
				LinkedListNode<T> node;
				if (this.items.TryGetValue(value, out node))
				{
					this.items.Remove(value);
					this.fifoList.Remove(node);
					result = true;
				}
			}
			return result;
		}

		// Token: 0x06000083 RID: 131 RVA: 0x00003830 File Offset: 0x00001A30
		public void Clear()
		{
			object obj = this.thisLock;
			lock (obj)
			{
				this.fifoList.Clear();
				this.items.Clear();
			}
		}

		// Token: 0x04000060 RID: 96
		private LinkedList<T> fifoList;

		// Token: 0x04000061 RID: 97
		private Dictionary<T, LinkedListNode<T>> items;

		// Token: 0x04000062 RID: 98
		private int capacity;

		// Token: 0x04000063 RID: 99
		private object thisLock;
	}
}
