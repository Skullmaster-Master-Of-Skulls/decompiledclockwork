using System;
using System.Collections;
using log4net.Util;

namespace log4net.Appender
{
	// Token: 0x0200000F RID: 15
	public class AppenderCollection : IList, ICollection, IEnumerable, ICloneable
	{
		// Token: 0x0600007E RID: 126 RVA: 0x000034B7 File Offset: 0x000016B7
		public static AppenderCollection ReadOnly(AppenderCollection list)
		{
			if (list == null)
			{
				throw new ArgumentNullException("list");
			}
			return new AppenderCollection.ReadOnlyAppenderCollection(list);
		}

		// Token: 0x0600007F RID: 127 RVA: 0x000034CD File Offset: 0x000016CD
		public AppenderCollection()
		{
			this.m_array = new IAppender[16];
		}

		// Token: 0x06000080 RID: 128 RVA: 0x000034E2 File Offset: 0x000016E2
		public AppenderCollection(int capacity)
		{
			this.m_array = new IAppender[capacity];
		}

		// Token: 0x06000081 RID: 129 RVA: 0x000034F6 File Offset: 0x000016F6
		public AppenderCollection(AppenderCollection c)
		{
			this.m_array = new IAppender[c.Count];
			this.AddRange(c);
		}

		// Token: 0x06000082 RID: 130 RVA: 0x00003517 File Offset: 0x00001717
		public AppenderCollection(IAppender[] a)
		{
			this.m_array = new IAppender[a.Length];
			this.AddRange(a);
		}

		// Token: 0x06000083 RID: 131 RVA: 0x00003535 File Offset: 0x00001735
		public AppenderCollection(ICollection col)
		{
			this.m_array = new IAppender[col.Count];
			this.AddRange(col);
		}

		// Token: 0x06000084 RID: 132 RVA: 0x00003556 File Offset: 0x00001756
		protected internal AppenderCollection(AppenderCollection.Tag tag)
		{
			this.m_array = null;
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000085 RID: 133 RVA: 0x00003565 File Offset: 0x00001765
		public virtual int Count
		{
			get
			{
				return this.m_count;
			}
		}

		// Token: 0x06000086 RID: 134 RVA: 0x0000356D File Offset: 0x0000176D
		public virtual void CopyTo(IAppender[] array)
		{
			this.CopyTo(array, 0);
		}

		// Token: 0x06000087 RID: 135 RVA: 0x00003577 File Offset: 0x00001777
		public virtual void CopyTo(IAppender[] array, int start)
		{
			if (this.m_count > array.GetUpperBound(0) + 1 - start)
			{
				throw new ArgumentException("Destination array was not long enough.");
			}
			Array.Copy(this.m_array, 0, array, start, this.m_count);
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x06000088 RID: 136 RVA: 0x000035AB File Offset: 0x000017AB
		public virtual bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x06000089 RID: 137 RVA: 0x000035AE File Offset: 0x000017AE
		public virtual object SyncRoot
		{
			get
			{
				return this.m_array;
			}
		}

		// Token: 0x17000028 RID: 40
		public virtual IAppender this[int index]
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

		// Token: 0x0600008C RID: 140 RVA: 0x000035E8 File Offset: 0x000017E8
		public virtual int Add(IAppender item)
		{
			if (this.m_count == this.m_array.Length)
			{
				this.EnsureCapacity(this.m_count + 1);
			}
			this.m_array[this.m_count] = item;
			this.m_version++;
			return this.m_count++;
		}

		// Token: 0x0600008D RID: 141 RVA: 0x00003640 File Offset: 0x00001840
		public virtual void Clear()
		{
			this.m_version++;
			this.m_array = new IAppender[16];
			this.m_count = 0;
		}

		// Token: 0x0600008E RID: 142 RVA: 0x00003664 File Offset: 0x00001864
		public virtual object Clone()
		{
			AppenderCollection appenderCollection = new AppenderCollection(this.m_count);
			Array.Copy(this.m_array, 0, appenderCollection.m_array, 0, this.m_count);
			appenderCollection.m_count = this.m_count;
			appenderCollection.m_version = this.m_version;
			return appenderCollection;
		}

		// Token: 0x0600008F RID: 143 RVA: 0x000036B0 File Offset: 0x000018B0
		public virtual bool Contains(IAppender item)
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

		// Token: 0x06000090 RID: 144 RVA: 0x000036E4 File Offset: 0x000018E4
		public virtual int IndexOf(IAppender item)
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

		// Token: 0x06000091 RID: 145 RVA: 0x00003718 File Offset: 0x00001918
		public virtual void Insert(int index, IAppender item)
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

		// Token: 0x06000092 RID: 146 RVA: 0x00003798 File Offset: 0x00001998
		public virtual void Remove(IAppender item)
		{
			int num = this.IndexOf(item);
			if (num < 0)
			{
				throw new ArgumentException("Cannot remove the specified item because it was not found in the specified Collection.");
			}
			this.m_version++;
			this.RemoveAt(num);
		}

		// Token: 0x06000093 RID: 147 RVA: 0x000037D4 File Offset: 0x000019D4
		public virtual void RemoveAt(int index)
		{
			this.ValidateIndex(index);
			this.m_count--;
			if (index < this.m_count)
			{
				Array.Copy(this.m_array, index + 1, this.m_array, index, this.m_count - index);
			}
			IAppender[] sourceArray = new IAppender[1];
			Array.Copy(sourceArray, 0, this.m_array, this.m_count, 1);
			this.m_version++;
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x06000094 RID: 148 RVA: 0x00003845 File Offset: 0x00001A45
		public virtual bool IsFixedSize
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x06000095 RID: 149 RVA: 0x00003848 File Offset: 0x00001A48
		public virtual bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000096 RID: 150 RVA: 0x0000384B File Offset: 0x00001A4B
		public virtual AppenderCollection.IAppenderCollectionEnumerator GetEnumerator()
		{
			return new AppenderCollection.Enumerator(this);
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x06000097 RID: 151 RVA: 0x00003853 File Offset: 0x00001A53
		// (set) Token: 0x06000098 RID: 152 RVA: 0x00003860 File Offset: 0x00001A60
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
						IAppender[] array = new IAppender[value];
						Array.Copy(this.m_array, 0, array, 0, this.m_count);
						this.m_array = array;
						return;
					}
					this.m_array = new IAppender[16];
				}
			}
		}

		// Token: 0x06000099 RID: 153 RVA: 0x000038C0 File Offset: 0x00001AC0
		public virtual int AddRange(AppenderCollection x)
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

		// Token: 0x0600009A RID: 154 RVA: 0x0000393C File Offset: 0x00001B3C
		public virtual int AddRange(IAppender[] x)
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

		// Token: 0x0600009B RID: 155 RVA: 0x000039A8 File Offset: 0x00001BA8
		public virtual int AddRange(ICollection col)
		{
			if (this.m_count + col.Count >= this.m_array.Length)
			{
				this.EnsureCapacity(this.m_count + col.Count);
			}
			foreach (object obj in col)
			{
				this.Add((IAppender)obj);
			}
			return this.m_count;
		}

		// Token: 0x0600009C RID: 156 RVA: 0x00003A30 File Offset: 0x00001C30
		public virtual void TrimToSize()
		{
			this.Capacity = this.m_count;
		}

		// Token: 0x0600009D RID: 157 RVA: 0x00003A40 File Offset: 0x00001C40
		public virtual IAppender[] ToArray()
		{
			IAppender[] array = new IAppender[this.m_count];
			if (this.m_count > 0)
			{
				Array.Copy(this.m_array, 0, array, 0, this.m_count);
			}
			return array;
		}

		// Token: 0x0600009E RID: 158 RVA: 0x00003A77 File Offset: 0x00001C77
		private void ValidateIndex(int i)
		{
			this.ValidateIndex(i, false);
		}

		// Token: 0x0600009F RID: 159 RVA: 0x00003A84 File Offset: 0x00001C84
		private void ValidateIndex(int i, bool allowEqualEnd)
		{
			int num = allowEqualEnd ? this.m_count : (this.m_count - 1);
			if (i < 0 || i > num)
			{
				throw SystemInfo.CreateArgumentOutOfRangeException("i", i, "Index was out of range. Must be non-negative and less than the size of the collection. [" + i + "] Specified argument was out of the range of valid values.");
			}
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x00003AD4 File Offset: 0x00001CD4
		private void EnsureCapacity(int min)
		{
			int num = (this.m_array.Length == 0) ? 16 : (this.m_array.Length * 2);
			if (num < min)
			{
				num = min;
			}
			this.Capacity = num;
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x00003B07 File Offset: 0x00001D07
		void ICollection.CopyTo(Array array, int start)
		{
			if (this.m_count > 0)
			{
				Array.Copy(this.m_array, 0, array, start, this.m_count);
			}
		}

		// Token: 0x1700002C RID: 44
		object IList.this[int i]
		{
			get
			{
				return this[i];
			}
			set
			{
				this[i] = (IAppender)value;
			}
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x00003B3E File Offset: 0x00001D3E
		int IList.Add(object x)
		{
			return this.Add((IAppender)x);
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x00003B4C File Offset: 0x00001D4C
		bool IList.Contains(object x)
		{
			return this.Contains((IAppender)x);
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x00003B5A File Offset: 0x00001D5A
		int IList.IndexOf(object x)
		{
			return this.IndexOf((IAppender)x);
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x00003B68 File Offset: 0x00001D68
		void IList.Insert(int pos, object x)
		{
			this.Insert(pos, (IAppender)x);
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x00003B77 File Offset: 0x00001D77
		void IList.Remove(object x)
		{
			this.Remove((IAppender)x);
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x00003B85 File Offset: 0x00001D85
		void IList.RemoveAt(int pos)
		{
			this.RemoveAt(pos);
		}

		// Token: 0x060000AA RID: 170 RVA: 0x00003B8E File Offset: 0x00001D8E
		IEnumerator IEnumerable.GetEnumerator()
		{
			return (IEnumerator)this.GetEnumerator();
		}

		// Token: 0x04000044 RID: 68
		private const int DEFAULT_CAPACITY = 16;

		// Token: 0x04000045 RID: 69
		private IAppender[] m_array;

		// Token: 0x04000046 RID: 70
		private int m_count;

		// Token: 0x04000047 RID: 71
		private int m_version;

		// Token: 0x04000048 RID: 72
		public static readonly AppenderCollection EmptyCollection = AppenderCollection.ReadOnly(new AppenderCollection(0));

		// Token: 0x02000010 RID: 16
		public interface IAppenderCollectionEnumerator
		{
			// Token: 0x1700002D RID: 45
			// (get) Token: 0x060000AC RID: 172
			IAppender Current { get; }

			// Token: 0x060000AD RID: 173
			bool MoveNext();

			// Token: 0x060000AE RID: 174
			void Reset();
		}

		// Token: 0x02000011 RID: 17
		protected internal enum Tag
		{
			// Token: 0x0400004A RID: 74
			Default
		}

		// Token: 0x02000012 RID: 18
		private sealed class Enumerator : IEnumerator, AppenderCollection.IAppenderCollectionEnumerator
		{
			// Token: 0x060000AF RID: 175 RVA: 0x00003BAD File Offset: 0x00001DAD
			internal Enumerator(AppenderCollection tc)
			{
				this.m_collection = tc;
				this.m_index = -1;
				this.m_version = tc.m_version;
			}

			// Token: 0x1700002E RID: 46
			// (get) Token: 0x060000B0 RID: 176 RVA: 0x00003BCF File Offset: 0x00001DCF
			public IAppender Current
			{
				get
				{
					return this.m_collection[this.m_index];
				}
			}

			// Token: 0x060000B1 RID: 177 RVA: 0x00003BE4 File Offset: 0x00001DE4
			public bool MoveNext()
			{
				if (this.m_version != this.m_collection.m_version)
				{
					throw new InvalidOperationException("Collection was modified; enumeration operation may not execute.");
				}
				this.m_index++;
				return this.m_index < this.m_collection.Count;
			}

			// Token: 0x060000B2 RID: 178 RVA: 0x00003C30 File Offset: 0x00001E30
			public void Reset()
			{
				this.m_index = -1;
			}

			// Token: 0x1700002F RID: 47
			// (get) Token: 0x060000B3 RID: 179 RVA: 0x00003C39 File Offset: 0x00001E39
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x0400004B RID: 75
			private readonly AppenderCollection m_collection;

			// Token: 0x0400004C RID: 76
			private int m_index;

			// Token: 0x0400004D RID: 77
			private int m_version;
		}

		// Token: 0x02000013 RID: 19
		private sealed class ReadOnlyAppenderCollection : AppenderCollection, ICollection, IEnumerable
		{
			// Token: 0x060000B4 RID: 180 RVA: 0x00003C41 File Offset: 0x00001E41
			internal ReadOnlyAppenderCollection(AppenderCollection list) : base(AppenderCollection.Tag.Default)
			{
				this.m_collection = list;
			}

			// Token: 0x060000B5 RID: 181 RVA: 0x00003C51 File Offset: 0x00001E51
			public override void CopyTo(IAppender[] array)
			{
				this.m_collection.CopyTo(array);
			}

			// Token: 0x060000B6 RID: 182 RVA: 0x00003C5F File Offset: 0x00001E5F
			public override void CopyTo(IAppender[] array, int start)
			{
				this.m_collection.CopyTo(array, start);
			}

			// Token: 0x060000B7 RID: 183 RVA: 0x00003C6E File Offset: 0x00001E6E
			void ICollection.CopyTo(Array array, int start)
			{
				((ICollection)this.m_collection).CopyTo(array, start);
			}

			// Token: 0x17000030 RID: 48
			// (get) Token: 0x060000B8 RID: 184 RVA: 0x00003C7D File Offset: 0x00001E7D
			public override int Count
			{
				get
				{
					return this.m_collection.Count;
				}
			}

			// Token: 0x17000031 RID: 49
			// (get) Token: 0x060000B9 RID: 185 RVA: 0x00003C8A File Offset: 0x00001E8A
			public override bool IsSynchronized
			{
				get
				{
					return this.m_collection.IsSynchronized;
				}
			}

			// Token: 0x17000032 RID: 50
			// (get) Token: 0x060000BA RID: 186 RVA: 0x00003C97 File Offset: 0x00001E97
			public override object SyncRoot
			{
				get
				{
					return this.m_collection.SyncRoot;
				}
			}

			// Token: 0x17000033 RID: 51
			public override IAppender this[int i]
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

			// Token: 0x060000BD RID: 189 RVA: 0x00003CBE File Offset: 0x00001EBE
			public override int Add(IAppender x)
			{
				throw new NotSupportedException("This is a Read Only Collection and can not be modified");
			}

			// Token: 0x060000BE RID: 190 RVA: 0x00003CCA File Offset: 0x00001ECA
			public override void Clear()
			{
				throw new NotSupportedException("This is a Read Only Collection and can not be modified");
			}

			// Token: 0x060000BF RID: 191 RVA: 0x00003CD6 File Offset: 0x00001ED6
			public override bool Contains(IAppender x)
			{
				return this.m_collection.Contains(x);
			}

			// Token: 0x060000C0 RID: 192 RVA: 0x00003CE4 File Offset: 0x00001EE4
			public override int IndexOf(IAppender x)
			{
				return this.m_collection.IndexOf(x);
			}

			// Token: 0x060000C1 RID: 193 RVA: 0x00003CF2 File Offset: 0x00001EF2
			public override void Insert(int pos, IAppender x)
			{
				throw new NotSupportedException("This is a Read Only Collection and can not be modified");
			}

			// Token: 0x060000C2 RID: 194 RVA: 0x00003CFE File Offset: 0x00001EFE
			public override void Remove(IAppender x)
			{
				throw new NotSupportedException("This is a Read Only Collection and can not be modified");
			}

			// Token: 0x060000C3 RID: 195 RVA: 0x00003D0A File Offset: 0x00001F0A
			public override void RemoveAt(int pos)
			{
				throw new NotSupportedException("This is a Read Only Collection and can not be modified");
			}

			// Token: 0x17000034 RID: 52
			// (get) Token: 0x060000C4 RID: 196 RVA: 0x00003D16 File Offset: 0x00001F16
			public override bool IsFixedSize
			{
				get
				{
					return true;
				}
			}

			// Token: 0x17000035 RID: 53
			// (get) Token: 0x060000C5 RID: 197 RVA: 0x00003D19 File Offset: 0x00001F19
			public override bool IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x060000C6 RID: 198 RVA: 0x00003D1C File Offset: 0x00001F1C
			public override AppenderCollection.IAppenderCollectionEnumerator GetEnumerator()
			{
				return this.m_collection.GetEnumerator();
			}

			// Token: 0x17000036 RID: 54
			// (get) Token: 0x060000C7 RID: 199 RVA: 0x00003D29 File Offset: 0x00001F29
			// (set) Token: 0x060000C8 RID: 200 RVA: 0x00003D36 File Offset: 0x00001F36
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

			// Token: 0x060000C9 RID: 201 RVA: 0x00003D42 File Offset: 0x00001F42
			public override int AddRange(AppenderCollection x)
			{
				throw new NotSupportedException("This is a Read Only Collection and can not be modified");
			}

			// Token: 0x060000CA RID: 202 RVA: 0x00003D4E File Offset: 0x00001F4E
			public override int AddRange(IAppender[] x)
			{
				throw new NotSupportedException("This is a Read Only Collection and can not be modified");
			}

			// Token: 0x060000CB RID: 203 RVA: 0x00003D5A File Offset: 0x00001F5A
			public override IAppender[] ToArray()
			{
				return this.m_collection.ToArray();
			}

			// Token: 0x060000CC RID: 204 RVA: 0x00003D67 File Offset: 0x00001F67
			public override void TrimToSize()
			{
				throw new NotSupportedException("This is a Read Only Collection and can not be modified");
			}

			// Token: 0x0400004E RID: 78
			private readonly AppenderCollection m_collection;
		}
	}
}
