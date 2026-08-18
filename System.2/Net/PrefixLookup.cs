using System;
using System.Collections.Generic;

namespace System.Net
{
	// Token: 0x020001DD RID: 477
	internal class PrefixLookup
	{
		// Token: 0x060012BD RID: 4797 RVA: 0x0006358A File Offset: 0x0006178A
		public PrefixLookup() : this(100)
		{
		}

		// Token: 0x060012BE RID: 4798 RVA: 0x00063594 File Offset: 0x00061794
		public PrefixLookup(int capacity)
		{
			this.capacity = capacity;
		}

		// Token: 0x060012BF RID: 4799 RVA: 0x000635B0 File Offset: 0x000617B0
		public void Add(string prefix, object value)
		{
			if (this.capacity == 0 || prefix == null || prefix.Length == 0 || value == null)
			{
				return;
			}
			LinkedList<PrefixLookup.PrefixValuePair> obj = this.lruList;
			lock (obj)
			{
				if (this.lruList.First != null && this.lruList.First.Value.prefix.Equals(prefix))
				{
					this.lruList.First.Value.value = value;
				}
				else
				{
					this.lruList.AddFirst(new PrefixLookup.PrefixValuePair(prefix, value));
					while (this.lruList.Count > this.capacity)
					{
						this.lruList.RemoveLast();
					}
				}
			}
		}

		// Token: 0x060012C0 RID: 4800 RVA: 0x0006367C File Offset: 0x0006187C
		public object Lookup(string lookupKey)
		{
			if (lookupKey == null || lookupKey.Length == 0 || this.lruList.Count == 0)
			{
				return null;
			}
			LinkedListNode<PrefixLookup.PrefixValuePair> linkedListNode = null;
			LinkedList<PrefixLookup.PrefixValuePair> obj = this.lruList;
			lock (obj)
			{
				int num = 0;
				for (LinkedListNode<PrefixLookup.PrefixValuePair> linkedListNode2 = this.lruList.First; linkedListNode2 != null; linkedListNode2 = linkedListNode2.Next)
				{
					string prefix = linkedListNode2.Value.prefix;
					if (prefix.Length > num && lookupKey.StartsWith(prefix))
					{
						num = prefix.Length;
						linkedListNode = linkedListNode2;
						if (num == lookupKey.Length)
						{
							break;
						}
					}
				}
				if (linkedListNode != null && linkedListNode != this.lruList.First)
				{
					this.lruList.Remove(linkedListNode);
					this.lruList.AddFirst(linkedListNode);
				}
			}
			if (linkedListNode == null)
			{
				return null;
			}
			return linkedListNode.Value.value;
		}

		// Token: 0x04001518 RID: 5400
		private const int defaultCapacity = 100;

		// Token: 0x04001519 RID: 5401
		private volatile int capacity;

		// Token: 0x0400151A RID: 5402
		private readonly LinkedList<PrefixLookup.PrefixValuePair> lruList = new LinkedList<PrefixLookup.PrefixValuePair>();

		// Token: 0x02000754 RID: 1876
		private class PrefixValuePair
		{
			// Token: 0x06004205 RID: 16901 RVA: 0x00112583 File Offset: 0x00110783
			public PrefixValuePair(string pre, object val)
			{
				this.prefix = pre;
				this.value = val;
			}

			// Token: 0x04003216 RID: 12822
			public string prefix;

			// Token: 0x04003217 RID: 12823
			public object value;
		}
	}
}
