using System;
using System.Collections.Generic;

namespace System.Linq.Parallel
{
	// Token: 0x020001FC RID: 508
	internal class HashLookup<TKey, TValue>
	{
		// Token: 0x06001029 RID: 4137 RVA: 0x00038FE7 File Offset: 0x000371E7
		internal HashLookup() : this(null)
		{
		}

		// Token: 0x0600102A RID: 4138 RVA: 0x00038FF0 File Offset: 0x000371F0
		internal HashLookup(IEqualityComparer<TKey> comparer)
		{
			this.comparer = comparer;
			this.buckets = new int[7];
			this.slots = new HashLookup<TKey, TValue>.Slot[7];
			this.freeList = -1;
		}

		// Token: 0x0600102B RID: 4139 RVA: 0x0003901E File Offset: 0x0003721E
		internal bool Add(TKey key, TValue value)
		{
			return !this.Find(key, true, false, ref value);
		}

		// Token: 0x0600102C RID: 4140 RVA: 0x0003902E File Offset: 0x0003722E
		internal bool TryGetValue(TKey key, ref TValue value)
		{
			return this.Find(key, false, false, ref value);
		}

		// Token: 0x170002D2 RID: 722
		internal TValue this[TKey key]
		{
			set
			{
				TValue tvalue = value;
				this.Find(key, false, true, ref tvalue);
			}
		}

		// Token: 0x0600102E RID: 4142 RVA: 0x00039057 File Offset: 0x00037257
		private int GetKeyHashCode(TKey key)
		{
			return int.MaxValue & ((this.comparer == null) ? ((key == null) ? 0 : key.GetHashCode()) : this.comparer.GetHashCode(key));
		}

		// Token: 0x0600102F RID: 4143 RVA: 0x00039090 File Offset: 0x00037290
		private bool AreKeysEqual(TKey key1, TKey key2)
		{
			if (this.comparer != null)
			{
				return this.comparer.Equals(key1, key2);
			}
			return (key1 == null && key2 == null) || (key1 != null && key1.Equals(key2));
		}

		// Token: 0x06001030 RID: 4144 RVA: 0x000390E4 File Offset: 0x000372E4
		internal bool Remove(TKey key)
		{
			int keyHashCode = this.GetKeyHashCode(key);
			int num = keyHashCode % this.buckets.Length;
			int num2 = -1;
			for (int i = this.buckets[num] - 1; i >= 0; i = this.slots[i].next)
			{
				if (this.slots[i].hashCode == keyHashCode && this.AreKeysEqual(this.slots[i].key, key))
				{
					if (num2 < 0)
					{
						this.buckets[num] = this.slots[i].next + 1;
					}
					else
					{
						this.slots[num2].next = this.slots[i].next;
					}
					this.slots[i].hashCode = -1;
					this.slots[i].key = default(TKey);
					this.slots[i].value = default(TValue);
					this.slots[i].next = this.freeList;
					this.freeList = i;
					return true;
				}
				num2 = i;
			}
			return false;
		}

		// Token: 0x06001031 RID: 4145 RVA: 0x0003920C File Offset: 0x0003740C
		private bool Find(TKey key, bool add, bool set, ref TValue value)
		{
			int keyHashCode = this.GetKeyHashCode(key);
			int i = this.buckets[keyHashCode % this.buckets.Length] - 1;
			while (i >= 0)
			{
				if (this.slots[i].hashCode == keyHashCode && this.AreKeysEqual(this.slots[i].key, key))
				{
					if (set)
					{
						this.slots[i].value = value;
						return true;
					}
					value = this.slots[i].value;
					return true;
				}
				else
				{
					i = this.slots[i].next;
				}
			}
			if (add)
			{
				int num;
				if (this.freeList >= 0)
				{
					num = this.freeList;
					this.freeList = this.slots[num].next;
				}
				else
				{
					if (this.count == this.slots.Length)
					{
						this.Resize();
					}
					num = this.count;
					this.count++;
				}
				int num2 = keyHashCode % this.buckets.Length;
				this.slots[num].hashCode = keyHashCode;
				this.slots[num].key = key;
				this.slots[num].value = value;
				this.slots[num].next = this.buckets[num2] - 1;
				this.buckets[num2] = num + 1;
			}
			return false;
		}

		// Token: 0x06001032 RID: 4146 RVA: 0x0003937C File Offset: 0x0003757C
		private void Resize()
		{
			int num = checked(this.count * 2 + 1);
			int[] array = new int[num];
			HashLookup<TKey, TValue>.Slot[] array2 = new HashLookup<TKey, TValue>.Slot[num];
			Array.Copy(this.slots, 0, array2, 0, this.count);
			for (int i = 0; i < this.count; i++)
			{
				int num2 = array2[i].hashCode % num;
				array2[i].next = array[num2] - 1;
				array[num2] = i + 1;
			}
			this.buckets = array;
			this.slots = array2;
		}

		// Token: 0x170002D3 RID: 723
		// (get) Token: 0x06001033 RID: 4147 RVA: 0x000393FE File Offset: 0x000375FE
		internal int Count
		{
			get
			{
				return this.count;
			}
		}

		// Token: 0x170002D4 RID: 724
		internal KeyValuePair<TKey, TValue> this[int index]
		{
			get
			{
				return new KeyValuePair<TKey, TValue>(this.slots[index].key, this.slots[index].value);
			}
		}

		// Token: 0x0400092B RID: 2347
		private int[] buckets;

		// Token: 0x0400092C RID: 2348
		private HashLookup<TKey, TValue>.Slot[] slots;

		// Token: 0x0400092D RID: 2349
		private int count;

		// Token: 0x0400092E RID: 2350
		private int freeList;

		// Token: 0x0400092F RID: 2351
		private IEqualityComparer<TKey> comparer;

		// Token: 0x02000418 RID: 1048
		internal struct Slot
		{
			// Token: 0x0400127A RID: 4730
			internal int hashCode;

			// Token: 0x0400127B RID: 4731
			internal TKey key;

			// Token: 0x0400127C RID: 4732
			internal TValue value;

			// Token: 0x0400127D RID: 4733
			internal int next;
		}
	}
}
