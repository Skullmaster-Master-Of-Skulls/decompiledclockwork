using System;
using iTextSharp.text.error_messages;

namespace iTextSharp.text.pdf
{
	// Token: 0x0200052E RID: 1326
	public class IntHashtable
	{
		// Token: 0x06002D7D RID: 11645 RVA: 0x0011600C File Offset: 0x0011500C
		public IntHashtable(int initialCapacity, float loadFactor)
		{
			if (initialCapacity < 0)
			{
				throw new ArgumentException(MessageLocalization.GetComposedMessage("illegal.capacity.1", initialCapacity));
			}
			if (loadFactor <= 0f)
			{
				throw new ArgumentException(MessageLocalization.GetComposedMessage("illegal.load.1", loadFactor));
			}
			this.loadFactor = loadFactor;
			this.table = new IntHashtable.IntHashtableEntry[initialCapacity];
			this.threshold = (int)((float)initialCapacity * loadFactor);
		}

		// Token: 0x06002D7E RID: 11646 RVA: 0x00116075 File Offset: 0x00115075
		public IntHashtable(int initialCapacity) : this(initialCapacity, 0.75f)
		{
		}

		// Token: 0x06002D7F RID: 11647 RVA: 0x00116083 File Offset: 0x00115083
		public IntHashtable() : this(101, 0.75f)
		{
		}

		// Token: 0x170007DC RID: 2012
		// (get) Token: 0x06002D80 RID: 11648 RVA: 0x00116092 File Offset: 0x00115092
		public int Size
		{
			get
			{
				return this.count;
			}
		}

		// Token: 0x06002D81 RID: 11649 RVA: 0x0011609A File Offset: 0x0011509A
		public bool IsEmpty()
		{
			return this.count == 0;
		}

		// Token: 0x06002D82 RID: 11650 RVA: 0x001160A8 File Offset: 0x001150A8
		public bool Contains(int value)
		{
			IntHashtable.IntHashtableEntry[] array = this.table;
			int num = array.Length;
			while (num-- > 0)
			{
				for (IntHashtable.IntHashtableEntry intHashtableEntry = array[num]; intHashtableEntry != null; intHashtableEntry = intHashtableEntry.next)
				{
					if (intHashtableEntry.value == value)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06002D83 RID: 11651 RVA: 0x001160E8 File Offset: 0x001150E8
		public bool ContainsKey(int key)
		{
			IntHashtable.IntHashtableEntry[] array = this.table;
			int num = (key & int.MaxValue) % array.Length;
			for (IntHashtable.IntHashtableEntry intHashtableEntry = array[num]; intHashtableEntry != null; intHashtableEntry = intHashtableEntry.next)
			{
				if (intHashtableEntry.hash == key && intHashtableEntry.key == key)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x170007DD RID: 2013
		public int this[int key]
		{
			get
			{
				IntHashtable.IntHashtableEntry[] array = this.table;
				int num = (key & int.MaxValue) % array.Length;
				for (IntHashtable.IntHashtableEntry intHashtableEntry = array[num]; intHashtableEntry != null; intHashtableEntry = intHashtableEntry.next)
				{
					if (intHashtableEntry.hash == key && intHashtableEntry.key == key)
					{
						return intHashtableEntry.value;
					}
				}
				return 0;
			}
			set
			{
				IntHashtable.IntHashtableEntry[] array = this.table;
				int num = (key & int.MaxValue) % array.Length;
				for (IntHashtable.IntHashtableEntry intHashtableEntry = array[num]; intHashtableEntry != null; intHashtableEntry = intHashtableEntry.next)
				{
					if (intHashtableEntry.hash == key && intHashtableEntry.key == key)
					{
						intHashtableEntry.value = value;
						return;
					}
				}
				if (this.count >= this.threshold)
				{
					this.Rehash();
					this[key] = value;
					return;
				}
				array[num] = new IntHashtable.IntHashtableEntry
				{
					hash = key,
					key = key,
					value = value,
					next = array[num]
				};
				this.count++;
			}
		}

		// Token: 0x06002D86 RID: 11654 RVA: 0x00116224 File Offset: 0x00115224
		protected void Rehash()
		{
			int num = this.table.Length;
			IntHashtable.IntHashtableEntry[] array = this.table;
			int num2 = num * 2 + 1;
			IntHashtable.IntHashtableEntry[] array2 = new IntHashtable.IntHashtableEntry[num2];
			this.threshold = (int)((float)num2 * this.loadFactor);
			this.table = array2;
			int num3 = num;
			while (num3-- > 0)
			{
				IntHashtable.IntHashtableEntry intHashtableEntry = array[num3];
				while (intHashtableEntry != null)
				{
					IntHashtable.IntHashtableEntry intHashtableEntry2 = intHashtableEntry;
					intHashtableEntry = intHashtableEntry.next;
					int num4 = (intHashtableEntry2.hash & int.MaxValue) % num2;
					intHashtableEntry2.next = array2[num4];
					array2[num4] = intHashtableEntry2;
				}
			}
		}

		// Token: 0x06002D87 RID: 11655 RVA: 0x001162B0 File Offset: 0x001152B0
		public int Remove(int key)
		{
			IntHashtable.IntHashtableEntry[] array = this.table;
			int num = (key & int.MaxValue) % array.Length;
			IntHashtable.IntHashtableEntry intHashtableEntry = array[num];
			IntHashtable.IntHashtableEntry intHashtableEntry2 = null;
			while (intHashtableEntry != null)
			{
				if (intHashtableEntry.hash == key && intHashtableEntry.key == key)
				{
					if (intHashtableEntry2 != null)
					{
						intHashtableEntry2.next = intHashtableEntry.next;
					}
					else
					{
						array[num] = intHashtableEntry.next;
					}
					this.count--;
					return intHashtableEntry.value;
				}
				intHashtableEntry2 = intHashtableEntry;
				intHashtableEntry = intHashtableEntry.next;
			}
			return 0;
		}

		// Token: 0x06002D88 RID: 11656 RVA: 0x0011632C File Offset: 0x0011532C
		public void Clear()
		{
			IntHashtable.IntHashtableEntry[] array = this.table;
			int num = array.Length;
			while (--num >= 0)
			{
				array[num] = null;
			}
			this.count = 0;
		}

		// Token: 0x06002D89 RID: 11657 RVA: 0x0011635C File Offset: 0x0011535C
		public IntHashtable Clone()
		{
			IntHashtable intHashtable = new IntHashtable();
			intHashtable.count = this.count;
			intHashtable.loadFactor = this.loadFactor;
			intHashtable.threshold = this.threshold;
			intHashtable.table = new IntHashtable.IntHashtableEntry[this.table.Length];
			int num = this.table.Length;
			while (num-- > 0)
			{
				intHashtable.table[num] = ((this.table[num] != null) ? this.table[num].Clone() : null);
			}
			return intHashtable;
		}

		// Token: 0x06002D8A RID: 11658 RVA: 0x001163DC File Offset: 0x001153DC
		public int[] ToOrderedKeys()
		{
			int[] keys = this.GetKeys();
			Array.Sort<int>(keys);
			return keys;
		}

		// Token: 0x06002D8B RID: 11659 RVA: 0x001163F8 File Offset: 0x001153F8
		public int[] GetKeys()
		{
			int[] array = new int[this.count];
			int num = 0;
			int num2 = this.table.Length;
			IntHashtable.IntHashtableEntry intHashtableEntry = null;
			for (;;)
			{
				if (intHashtableEntry == null)
				{
					while (num2-- > 0 && (intHashtableEntry = this.table[num2]) == null)
					{
					}
				}
				if (intHashtableEntry == null)
				{
					break;
				}
				IntHashtable.IntHashtableEntry intHashtableEntry2 = intHashtableEntry;
				intHashtableEntry = intHashtableEntry2.next;
				array[num++] = intHashtableEntry2.key;
			}
			return array;
		}

		// Token: 0x06002D8C RID: 11660 RVA: 0x00116454 File Offset: 0x00115454
		public IntHashtable.IntHashtableIterator GetEntryIterator()
		{
			return new IntHashtable.IntHashtableIterator(this.table);
		}

		// Token: 0x04001F5E RID: 8030
		private IntHashtable.IntHashtableEntry[] table;

		// Token: 0x04001F5F RID: 8031
		private int count;

		// Token: 0x04001F60 RID: 8032
		private int threshold;

		// Token: 0x04001F61 RID: 8033
		private float loadFactor;

		// Token: 0x0200052F RID: 1327
		public class IntHashtableEntry
		{
			// Token: 0x170007DE RID: 2014
			// (get) Token: 0x06002D8D RID: 11661 RVA: 0x00116461 File Offset: 0x00115461
			public int Key
			{
				get
				{
					return this.key;
				}
			}

			// Token: 0x170007DF RID: 2015
			// (get) Token: 0x06002D8E RID: 11662 RVA: 0x00116469 File Offset: 0x00115469
			public int Value
			{
				get
				{
					return this.value;
				}
			}

			// Token: 0x06002D8F RID: 11663 RVA: 0x00116474 File Offset: 0x00115474
			protected internal IntHashtable.IntHashtableEntry Clone()
			{
				return new IntHashtable.IntHashtableEntry
				{
					hash = this.hash,
					key = this.key,
					value = this.value,
					next = ((this.next != null) ? this.next.Clone() : null)
				};
			}

			// Token: 0x04001F62 RID: 8034
			internal int hash;

			// Token: 0x04001F63 RID: 8035
			internal int key;

			// Token: 0x04001F64 RID: 8036
			internal int value;

			// Token: 0x04001F65 RID: 8037
			internal IntHashtable.IntHashtableEntry next;
		}

		// Token: 0x02000530 RID: 1328
		public class IntHashtableIterator
		{
			// Token: 0x06002D91 RID: 11665 RVA: 0x001164D0 File Offset: 0x001154D0
			internal IntHashtableIterator(IntHashtable.IntHashtableEntry[] table)
			{
				this.table = table;
				this.index = table.Length;
			}

			// Token: 0x06002D92 RID: 11666 RVA: 0x001164E8 File Offset: 0x001154E8
			public bool HasNext()
			{
				if (this.entry != null)
				{
					return true;
				}
				while (this.index-- > 0)
				{
					if ((this.entry = this.table[this.index]) != null)
					{
						return true;
					}
				}
				return false;
			}

			// Token: 0x06002D93 RID: 11667 RVA: 0x00116530 File Offset: 0x00115530
			public IntHashtable.IntHashtableEntry Next()
			{
				if (this.entry == null)
				{
					while (this.index-- > 0 && (this.entry = this.table[this.index]) == null)
					{
					}
				}
				if (this.entry != null)
				{
					IntHashtable.IntHashtableEntry intHashtableEntry = this.entry;
					this.entry = intHashtableEntry.next;
					return intHashtableEntry;
				}
				throw new InvalidOperationException(MessageLocalization.GetComposedMessage("inthashtableiterator"));
			}

			// Token: 0x04001F66 RID: 8038
			private int index;

			// Token: 0x04001F67 RID: 8039
			private IntHashtable.IntHashtableEntry[] table;

			// Token: 0x04001F68 RID: 8040
			private IntHashtable.IntHashtableEntry entry;
		}
	}
}
