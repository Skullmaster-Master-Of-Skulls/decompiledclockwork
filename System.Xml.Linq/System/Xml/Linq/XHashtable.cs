using System;
using System.Threading;

namespace System.Xml.Linq
{
	// Token: 0x0200000F RID: 15
	internal sealed class XHashtable<TValue>
	{
		// Token: 0x06000080 RID: 128 RVA: 0x0000407A File Offset: 0x0000227A
		public XHashtable(XHashtable<TValue>.ExtractKeyDelegate extractKey, int capacity)
		{
			this.state = new XHashtable<TValue>.XHashtableState(extractKey, capacity);
		}

		// Token: 0x06000081 RID: 129 RVA: 0x0000408F File Offset: 0x0000228F
		public bool TryGetValue(string key, int index, int count, out TValue value)
		{
			return this.state.TryGetValue(key, index, count, out value);
		}

		// Token: 0x06000082 RID: 130 RVA: 0x000040A4 File Offset: 0x000022A4
		public TValue Add(TValue value)
		{
			TValue result;
			while (!this.state.TryAdd(value, out result))
			{
				lock (this)
				{
					XHashtable<TValue>.XHashtableState xhashtableState = this.state.Resize();
					Thread.MemoryBarrier();
					this.state = xhashtableState;
				}
			}
			return result;
		}

		// Token: 0x04000070 RID: 112
		private XHashtable<TValue>.XHashtableState state;

		// Token: 0x04000071 RID: 113
		private const int StartingHash = 352654597;

		// Token: 0x02000042 RID: 66
		// (Invoke) Token: 0x060002ED RID: 749
		public delegate string ExtractKeyDelegate(TValue value);

		// Token: 0x02000043 RID: 67
		private sealed class XHashtableState
		{
			// Token: 0x060002F0 RID: 752 RVA: 0x0000C7B3 File Offset: 0x0000A9B3
			public XHashtableState(XHashtable<TValue>.ExtractKeyDelegate extractKey, int capacity)
			{
				this.buckets = new int[capacity];
				this.entries = new XHashtable<TValue>.XHashtableState.Entry[capacity];
				this.extractKey = extractKey;
			}

			// Token: 0x060002F1 RID: 753 RVA: 0x0000C7DC File Offset: 0x0000A9DC
			public XHashtable<TValue>.XHashtableState Resize()
			{
				if (this.numEntries < this.buckets.Length)
				{
					return this;
				}
				int num = 0;
				for (int i = 0; i < this.buckets.Length; i++)
				{
					int j = this.buckets[i];
					if (j == 0)
					{
						j = Interlocked.CompareExchange(ref this.buckets[i], -1, 0);
					}
					while (j > 0)
					{
						if (this.extractKey(this.entries[j].Value) != null)
						{
							num++;
						}
						if (this.entries[j].Next == 0)
						{
							j = Interlocked.CompareExchange(ref this.entries[j].Next, -1, 0);
						}
						else
						{
							j = this.entries[j].Next;
						}
					}
				}
				if (num < this.buckets.Length / 2)
				{
					num = this.buckets.Length;
				}
				else
				{
					num = this.buckets.Length * 2;
					if (num < 0)
					{
						throw new OverflowException();
					}
				}
				XHashtable<TValue>.XHashtableState xhashtableState = new XHashtable<TValue>.XHashtableState(this.extractKey, num);
				for (int k = 0; k < this.buckets.Length; k++)
				{
					for (int l = this.buckets[k]; l > 0; l = this.entries[l].Next)
					{
						TValue tvalue;
						xhashtableState.TryAdd(this.entries[l].Value, out tvalue);
					}
				}
				return xhashtableState;
			}

			// Token: 0x060002F2 RID: 754 RVA: 0x0000C934 File Offset: 0x0000AB34
			public bool TryGetValue(string key, int index, int count, out TValue value)
			{
				int hashCode = XHashtable<TValue>.XHashtableState.ComputeHashCode(key, index, count);
				int num = 0;
				if (this.FindEntry(hashCode, key, index, count, ref num))
				{
					value = this.entries[num].Value;
					return true;
				}
				value = default(TValue);
				return false;
			}

			// Token: 0x060002F3 RID: 755 RVA: 0x0000C980 File Offset: 0x0000AB80
			public bool TryAdd(TValue value, out TValue newValue)
			{
				newValue = value;
				string text = this.extractKey(value);
				if (text == null)
				{
					return true;
				}
				int num = XHashtable<TValue>.XHashtableState.ComputeHashCode(text, 0, text.Length);
				int num2 = Interlocked.Increment(ref this.numEntries);
				if (num2 < 0 || num2 >= this.buckets.Length)
				{
					return false;
				}
				this.entries[num2].Value = value;
				this.entries[num2].HashCode = num;
				Thread.MemoryBarrier();
				int num3 = 0;
				while (!this.FindEntry(num, text, 0, text.Length, ref num3))
				{
					if (num3 == 0)
					{
						num3 = Interlocked.CompareExchange(ref this.buckets[num & this.buckets.Length - 1], num2, 0);
					}
					else
					{
						num3 = Interlocked.CompareExchange(ref this.entries[num3].Next, num2, 0);
					}
					if (num3 <= 0)
					{
						return num3 == 0;
					}
				}
				newValue = this.entries[num3].Value;
				return true;
			}

			// Token: 0x060002F4 RID: 756 RVA: 0x0000CA70 File Offset: 0x0000AC70
			private bool FindEntry(int hashCode, string key, int index, int count, ref int entryIndex)
			{
				int num = entryIndex;
				int i;
				if (num == 0)
				{
					i = this.buckets[hashCode & this.buckets.Length - 1];
				}
				else
				{
					i = num;
				}
				while (i > 0)
				{
					if (this.entries[i].HashCode == hashCode)
					{
						string text = this.extractKey(this.entries[i].Value);
						if (text == null)
						{
							if (this.entries[i].Next > 0)
							{
								this.entries[i].Value = default(TValue);
								i = this.entries[i].Next;
								if (num == 0)
								{
									this.buckets[hashCode & this.buckets.Length - 1] = i;
									continue;
								}
								this.entries[num].Next = i;
								continue;
							}
						}
						else if (count == text.Length && string.CompareOrdinal(key, index, text, 0, count) == 0)
						{
							entryIndex = i;
							return true;
						}
					}
					num = i;
					i = this.entries[i].Next;
				}
				entryIndex = num;
				return false;
			}

			// Token: 0x060002F5 RID: 757 RVA: 0x0000CB84 File Offset: 0x0000AD84
			private static int ComputeHashCode(string key, int index, int count)
			{
				int num = 352654597;
				int num2 = index + count;
				for (int i = index; i < num2; i++)
				{
					num += (num << 7 ^ (int)key[i]);
				}
				num -= num >> 17;
				num -= num >> 11;
				num -= num >> 5;
				return num & int.MaxValue;
			}

			// Token: 0x04000105 RID: 261
			private int[] buckets;

			// Token: 0x04000106 RID: 262
			private XHashtable<TValue>.XHashtableState.Entry[] entries;

			// Token: 0x04000107 RID: 263
			private int numEntries;

			// Token: 0x04000108 RID: 264
			private XHashtable<TValue>.ExtractKeyDelegate extractKey;

			// Token: 0x04000109 RID: 265
			private const int EndOfList = 0;

			// Token: 0x0400010A RID: 266
			private const int FullList = -1;

			// Token: 0x0200005D RID: 93
			private struct Entry
			{
				// Token: 0x040001AA RID: 426
				public TValue Value;

				// Token: 0x040001AB RID: 427
				public int HashCode;

				// Token: 0x040001AC RID: 428
				public int Next;
			}
		}
	}
}
