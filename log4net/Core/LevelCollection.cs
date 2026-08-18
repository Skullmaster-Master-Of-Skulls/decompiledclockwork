using System;
using System.Collections;
using log4net.Util;

namespace log4net.Core
{
	// Token: 0x02000064 RID: 100
	public class LevelCollection : IList, ICollection, IEnumerable, ICloneable
	{
		// Token: 0x0600032F RID: 815 RVA: 0x0000B6B9 File Offset: 0x000098B9
		public static LevelCollection ReadOnly(LevelCollection list)
		{
			if (list == null)
			{
				throw new ArgumentNullException("list");
			}
			return new LevelCollection.ReadOnlyLevelCollection(list);
		}

		// Token: 0x06000330 RID: 816 RVA: 0x0000B6CF File Offset: 0x000098CF
		public LevelCollection()
		{
			this.m_array = new Level[16];
		}

		// Token: 0x06000331 RID: 817 RVA: 0x0000B6E4 File Offset: 0x000098E4
		public LevelCollection(int capacity)
		{
			this.m_array = new Level[capacity];
		}

		// Token: 0x06000332 RID: 818 RVA: 0x0000B6F8 File Offset: 0x000098F8
		public LevelCollection(LevelCollection c)
		{
			this.m_array = new Level[c.Count];
			this.AddRange(c);
		}

		// Token: 0x06000333 RID: 819 RVA: 0x0000B719 File Offset: 0x00009919
		public LevelCollection(Level[] a)
		{
			this.m_array = new Level[a.Length];
			this.AddRange(a);
		}

		// Token: 0x06000334 RID: 820 RVA: 0x0000B737 File Offset: 0x00009937
		public LevelCollection(ICollection col)
		{
			this.m_array = new Level[col.Count];
			this.AddRange(col);
		}

		// Token: 0x06000335 RID: 821 RVA: 0x0000B758 File Offset: 0x00009958
		protected internal LevelCollection(LevelCollection.Tag tag)
		{
			this.m_array = null;
		}

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x06000336 RID: 822 RVA: 0x0000B767 File Offset: 0x00009967
		public virtual int Count
		{
			get
			{
				return this.m_count;
			}
		}

		// Token: 0x06000337 RID: 823 RVA: 0x0000B76F File Offset: 0x0000996F
		public virtual void CopyTo(Level[] array)
		{
			this.CopyTo(array, 0);
		}

		// Token: 0x06000338 RID: 824 RVA: 0x0000B779 File Offset: 0x00009979
		public virtual void CopyTo(Level[] array, int start)
		{
			if (this.m_count > array.GetUpperBound(0) + 1 - start)
			{
				throw new ArgumentException("Destination array was not long enough.");
			}
			Array.Copy(this.m_array, 0, array, start, this.m_count);
		}

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x06000339 RID: 825 RVA: 0x0000B7AD File Offset: 0x000099AD
		public virtual bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x0600033A RID: 826 RVA: 0x0000B7B0 File Offset: 0x000099B0
		public virtual object SyncRoot
		{
			get
			{
				return this.m_array;
			}
		}

		// Token: 0x170000BA RID: 186
		public virtual Level this[int index]
		{
			get
			{
				this.ValidateIndex(index);
				return this.m_array[index];
			}
			set
			{
				this.ValidateIndex(index);
				this.m_version++;
				this.m_array[index] = value;
			}
		}

		// Token: 0x0600033D RID: 829 RVA: 0x0000B7EC File Offset: 0x000099EC
		public virtual int Add(Level item)
		{
			if (this.m_count == this.m_array.Length)
			{
				this.EnsureCapacity(this.m_count + 1);
			}
			this.m_array[this.m_count] = item;
			this.m_version++;
			return this.m_count++;
		}

		// Token: 0x0600033E RID: 830 RVA: 0x0000B844 File Offset: 0x00009A44
		public virtual void Clear()
		{
			this.m_version++;
			this.m_array = new Level[16];
			this.m_count = 0;
		}

		// Token: 0x0600033F RID: 831 RVA: 0x0000B868 File Offset: 0x00009A68
		public virtual object Clone()
		{
			LevelCollection levelCollection = new LevelCollection(this.m_count);
			Array.Copy(this.m_array, 0, levelCollection.m_array, 0, this.m_count);
			levelCollection.m_count = this.m_count;
			levelCollection.m_version = this.m_version;
			return levelCollection;
		}

		// Token: 0x06000340 RID: 832 RVA: 0x0000B8B4 File Offset: 0x00009AB4
		public virtual bool Contains(Level item)
		{
			for (int num = 0; num != this.m_count; num++)
			{
				if (this.m_array[num].Equals(item))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000341 RID: 833 RVA: 0x0000B8E8 File Offset: 0x00009AE8
		public virtual int IndexOf(Level item)
		{
			for (int num = 0; num != this.m_count; num++)
			{
				if (this.m_array[num].Equals(item))
				{
					return num;
				}
			}
			return -1;
		}

		// Token: 0x06000342 RID: 834 RVA: 0x0000B91C File Offset: 0x00009B1C
		public virtual void Insert(int index, Level item)
		{
			this.ValidateIndex(index, true);
			if (this.m_count == this.m_array.Length)
			{
				this.EnsureCapacity(this.m_count + 1);
			}
			if (index < this.m_count)
			{
				Array.Copy(this.m_array, index, this.m_array, index + 1, this.m_count - index);
			}
			this.m_array[index] = item;
			this.m_count++;
			this.m_version++;
		}

		// Token: 0x06000343 RID: 835 RVA: 0x0000B99C File Offset: 0x00009B9C
		public virtual void Remove(Level item)
		{
			int num = this.IndexOf(item);
			if (num < 0)
			{
				throw new ArgumentException("Cannot remove the specified item because it was not found in the specified Collection.");
			}
			this.m_version++;
			this.RemoveAt(num);
		}

		// Token: 0x06000344 RID: 836 RVA: 0x0000B9D8 File Offset: 0x00009BD8
		public virtual void RemoveAt(int index)
		{
			this.ValidateIndex(index);
			this.m_count--;
			if (index < this.m_count)
			{
				Array.Copy(this.m_array, index + 1, this.m_array, index, this.m_count - index);
			}
			Level[] sourceArray = new Level[1];
			Array.Copy(sourceArray, 0, this.m_array, this.m_count, 1);
			this.m_version++;
		}

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x06000345 RID: 837 RVA: 0x0000BA49 File Offset: 0x00009C49
		public virtual bool IsFixedSize
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x06000346 RID: 838 RVA: 0x0000BA4C File Offset: 0x00009C4C
		public virtual bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000347 RID: 839 RVA: 0x0000BA4F File Offset: 0x00009C4F
		public virtual LevelCollection.ILevelCollectionEnumerator GetEnumerator()
		{
			return new LevelCollection.Enumerator(this);
		}

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x06000348 RID: 840 RVA: 0x0000BA57 File Offset: 0x00009C57
		// (set) Token: 0x06000349 RID: 841 RVA: 0x0000BA64 File Offset: 0x00009C64
		public virtual int Capacity
		{
			get
			{
				return this.m_array.Length;
			}
			set
			{
				if (value < this.m_count)
				{
					value = this.m_count;
				}
				if (value != this.m_array.Length)
				{
					if (value > 0)
					{
						Level[] array = new Level[value];
						Array.Copy(this.m_array, 0, array, 0, this.m_count);
						this.m_array = array;
						return;
					}
					this.m_array = new Level[16];
				}
			}
		}

		// Token: 0x0600034A RID: 842 RVA: 0x0000BAC4 File Offset: 0x00009CC4
		public virtual int AddRange(LevelCollection x)
		{
			if (this.m_count + x.Count >= this.m_array.Length)
			{
				this.EnsureCapacity(this.m_count + x.Count);
			}
			Array.Copy(x.m_array, 0, this.m_array, this.m_count, x.Count);
			this.m_count += x.Count;
			this.m_version++;
			return this.m_count;
		}

		// Token: 0x0600034B RID: 843 RVA: 0x0000BB40 File Offset: 0x00009D40
		public virtual int AddRange(Level[] x)
		{
			if (this.m_count + x.Length >= this.m_array.Length)
			{
				this.EnsureCapacity(this.m_count + x.Length);
			}
			Array.Copy(x, 0, this.m_array, this.m_count, x.Length);
			this.m_count += x.Length;
			this.m_version++;
			return this.m_count;
		}

		// Token: 0x0600034C RID: 844 RVA: 0x0000BBAC File Offset: 0x00009DAC
		public virtual int AddRange(ICollection col)
		{
			if (this.m_count + col.Count >= this.m_array.Length)
			{
				this.EnsureCapacity(this.m_count + col.Count);
			}
			foreach (object obj in col)
			{
				this.Add((Level)obj);
			}
			return this.m_count;
		}

		// Token: 0x0600034D RID: 845 RVA: 0x0000BC34 File Offset: 0x00009E34
		public virtual void TrimToSize()
		{
			this.Capacity = this.m_count;
		}

		// Token: 0x0600034E RID: 846 RVA: 0x0000BC42 File Offset: 0x00009E42
		private void ValidateIndex(int i)
		{
			this.ValidateIndex(i, false);
		}

		// Token: 0x0600034F RID: 847 RVA: 0x0000BC4C File Offset: 0x00009E4C
		private void ValidateIndex(int i, bool allowEqualEnd)
		{
			int num = allowEqualEnd ? this.m_count : (this.m_count - 1);
			if (i < 0 || i > num)
			{
				throw SystemInfo.CreateArgumentOutOfRangeException("i", i, "Index was out of range. Must be non-negative and less than the size of the collection. [" + i + "] Specified argument was out of the range of valid values.");
			}
		}

		// Token: 0x06000350 RID: 848 RVA: 0x0000BC9C File Offset: 0x00009E9C
		private void EnsureCapacity(int min)
		{
			int num = (this.m_array.Length == 0) ? 16 : (this.m_array.Length * 2);
			if (num < min)
			{
				num = min;
			}
			this.Capacity = num;
		}

		// Token: 0x06000351 RID: 849 RVA: 0x0000BCCF File Offset: 0x00009ECF
		void ICollection.CopyTo(Array array, int start)
		{
			Array.Copy(this.m_array, 0, array, start, this.m_count);
		}

		// Token: 0x170000BE RID: 190
		object IList.this[int i]
		{
			get
			{
				return this[i];
			}
			set
			{
				this[i] = (Level)value;
			}
		}

		// Token: 0x06000354 RID: 852 RVA: 0x0000BCFD File Offset: 0x00009EFD
		int IList.Add(object x)
		{
			return this.Add((Level)x);
		}

		// Token: 0x06000355 RID: 853 RVA: 0x0000BD0B File Offset: 0x00009F0B
		bool IList.Contains(object x)
		{
			return this.Contains((Level)x);
		}

		// Token: 0x06000356 RID: 854 RVA: 0x0000BD19 File Offset: 0x00009F19
		int IList.IndexOf(object x)
		{
			return this.IndexOf((Level)x);
		}

		// Token: 0x06000357 RID: 855 RVA: 0x0000BD27 File Offset: 0x00009F27
		void IList.Insert(int pos, object x)
		{
			this.Insert(pos, (Level)x);
		}

		// Token: 0x06000358 RID: 856 RVA: 0x0000BD36 File Offset: 0x00009F36
		void IList.Remove(object x)
		{
			this.Remove((Level)x);
		}

		// Token: 0x06000359 RID: 857 RVA: 0x0000BD44 File Offset: 0x00009F44
		void IList.RemoveAt(int pos)
		{
			this.RemoveAt(pos);
		}

		// Token: 0x0600035A RID: 858 RVA: 0x0000BD4D File Offset: 0x00009F4D
		IEnumerator IEnumerable.GetEnumerator()
		{
			return (IEnumerator)this.GetEnumerator();
		}

		// Token: 0x04000184 RID: 388
		private const int DEFAULT_CAPACITY = 16;

		// Token: 0x04000185 RID: 389
		private Level[] m_array;

		// Token: 0x04000186 RID: 390
		private int m_count;

		// Token: 0x04000187 RID: 391
		private int m_version;

		// Token: 0x02000065 RID: 101
		public interface ILevelCollectionEnumerator
		{
			// Token: 0x170000BF RID: 191
			// (get) Token: 0x0600035B RID: 859
			Level Current { get; }

			// Token: 0x0600035C RID: 860
			bool MoveNext();

			// Token: 0x0600035D RID: 861
			void Reset();
		}

		// Token: 0x02000066 RID: 102
		protected internal enum Tag
		{
			// Token: 0x04000189 RID: 393
			Default
		}

		// Token: 0x02000067 RID: 103
		private sealed class Enumerator : IEnumerator, LevelCollection.ILevelCollectionEnumerator
		{
			// Token: 0x0600035E RID: 862 RVA: 0x0000BD5A File Offset: 0x00009F5A
			internal Enumerator(LevelCollection tc)
			{
				this.m_collection = tc;
				this.m_index = -1;
				this.m_version = tc.m_version;
			}

			// Token: 0x170000C0 RID: 192
			// (get) Token: 0x0600035F RID: 863 RVA: 0x0000BD7C File Offset: 0x00009F7C
			public Level Current
			{
				get
				{
					return this.m_collection[this.m_index];
				}
			}

			// Token: 0x06000360 RID: 864 RVA: 0x0000BD90 File Offset: 0x00009F90
			public bool MoveNext()
			{
				if (this.m_version != this.m_collection.m_version)
				{
					throw new InvalidOperationException("Collection was modified; enumeration operation may not execute.");
				}
				this.m_index++;
				return this.m_index < this.m_collection.Count;
			}

			// Token: 0x06000361 RID: 865 RVA: 0x0000BDDC File Offset: 0x00009FDC
			public void Reset()
			{
				this.m_index = -1;
			}

			// Token: 0x170000C1 RID: 193
			// (get) Token: 0x06000362 RID: 866 RVA: 0x0000BDE5 File Offset: 0x00009FE5
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x0400018A RID: 394
			private readonly LevelCollection m_collection;

			// Token: 0x0400018B RID: 395
			private int m_index;

			// Token: 0x0400018C RID: 396
			private int m_version;
		}

		// Token: 0x02000068 RID: 104
		private sealed class ReadOnlyLevelCollection : LevelCollection
		{
			// Token: 0x06000363 RID: 867 RVA: 0x0000BDED File Offset: 0x00009FED
			internal ReadOnlyLevelCollection(LevelCollection list) : base(LevelCollection.Tag.Default)
			{
				this.m_collection = list;
			}

			// Token: 0x06000364 RID: 868 RVA: 0x0000BDFD File Offset: 0x00009FFD
			public override void CopyTo(Level[] array)
			{
				this.m_collection.CopyTo(array);
			}

			// Token: 0x06000365 RID: 869 RVA: 0x0000BE0B File Offset: 0x0000A00B
			public override void CopyTo(Level[] array, int start)
			{
				this.m_collection.CopyTo(array, start);
			}

			// Token: 0x170000C2 RID: 194
			// (get) Token: 0x06000366 RID: 870 RVA: 0x0000BE1A File Offset: 0x0000A01A
			public override int Count
			{
				get
				{
					return this.m_collection.Count;
				}
			}

			// Token: 0x170000C3 RID: 195
			// (get) Token: 0x06000367 RID: 871 RVA: 0x0000BE27 File Offset: 0x0000A027
			public override bool IsSynchronized
			{
				get
				{
					return this.m_collection.IsSynchronized;
				}
			}

			// Token: 0x170000C4 RID: 196
			// (get) Token: 0x06000368 RID: 872 RVA: 0x0000BE34 File Offset: 0x0000A034
			public override object SyncRoot
			{
				get
				{
					return this.m_collection.SyncRoot;
				}
			}

			// Token: 0x170000C5 RID: 197
			public override Level this[int i]
			{
				get
				{
					return this.m_collection[i];
				}
				set
				{
					throw new NotSupportedException("This is a Read Only Collection and can not be modified");
				}
			}

			// Token: 0x0600036B RID: 875 RVA: 0x0000BE5B File Offset: 0x0000A05B
			public override int Add(Level x)
			{
				throw new NotSupportedException("This is a Read Only Collection and can not be modified");
			}

			// Token: 0x0600036C RID: 876 RVA: 0x0000BE67 File Offset: 0x0000A067
			public override void Clear()
			{
				throw new NotSupportedException("This is a Read Only Collection and can not be modified");
			}

			// Token: 0x0600036D RID: 877 RVA: 0x0000BE73 File Offset: 0x0000A073
			public override bool Contains(Level x)
			{
				return this.m_collection.Contains(x);
			}

			// Token: 0x0600036E RID: 878 RVA: 0x0000BE81 File Offset: 0x0000A081
			public override int IndexOf(Level x)
			{
				return this.m_collection.IndexOf(x);
			}

			// Token: 0x0600036F RID: 879 RVA: 0x0000BE8F File Offset: 0x0000A08F
			public override void Insert(int pos, Level x)
			{
				throw new NotSupportedException("This is a Read Only Collection and can not be modified");
			}

			// Token: 0x06000370 RID: 880 RVA: 0x0000BE9B File Offset: 0x0000A09B
			public override void Remove(Level x)
			{
				throw new NotSupportedException("This is a Read Only Collection and can not be modified");
			}

			// Token: 0x06000371 RID: 881 RVA: 0x0000BEA7 File Offset: 0x0000A0A7
			public override void RemoveAt(int pos)
			{
				throw new NotSupportedException("This is a Read Only Collection and can not be modified");
			}

			// Token: 0x170000C6 RID: 198
			// (get) Token: 0x06000372 RID: 882 RVA: 0x0000BEB3 File Offset: 0x0000A0B3
			public override bool IsFixedSize
			{
				get
				{
					return true;
				}
			}

			// Token: 0x170000C7 RID: 199
			// (get) Token: 0x06000373 RID: 883 RVA: 0x0000BEB6 File Offset: 0x0000A0B6
			public override bool IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x06000374 RID: 884 RVA: 0x0000BEB9 File Offset: 0x0000A0B9
			public override LevelCollection.ILevelCollectionEnumerator GetEnumerator()
			{
				return this.m_collection.GetEnumerator();
			}

			// Token: 0x170000C8 RID: 200
			// (get) Token: 0x06000375 RID: 885 RVA: 0x0000BEC6 File Offset: 0x0000A0C6
			// (set) Token: 0x06000376 RID: 886 RVA: 0x0000BED3 File Offset: 0x0000A0D3
			public override int Capacity
			{
				get
				{
					return this.m_collection.Capacity;
				}
				set
				{
					throw new NotSupportedException("This is a Read Only Collection and can not be modified");
				}
			}

			// Token: 0x06000377 RID: 887 RVA: 0x0000BEDF File Offset: 0x0000A0DF
			public override int AddRange(LevelCollection x)
			{
				throw new NotSupportedException("This is a Read Only Collection and can not be modified");
			}

			// Token: 0x06000378 RID: 888 RVA: 0x0000BEEB File Offset: 0x0000A0EB
			public override int AddRange(Level[] x)
			{
				throw new NotSupportedException("This is a Read Only Collection and can not be modified");
			}

			// Token: 0x0400018D RID: 397
			private readonly LevelCollection m_collection;
		}
	}
}
