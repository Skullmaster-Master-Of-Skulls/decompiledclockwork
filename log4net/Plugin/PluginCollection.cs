using System;
using System.Collections;
using log4net.Util;

namespace log4net.Plugin
{
	// Token: 0x020000BA RID: 186
	public class PluginCollection : IList, ICollection, IEnumerable, ICloneable
	{
		// Token: 0x06000542 RID: 1346 RVA: 0x00010D42 File Offset: 0x0000EF42
		public static PluginCollection ReadOnly(PluginCollection list)
		{
			if (list == null)
			{
				throw new ArgumentNullException("list");
			}
			return new PluginCollection.ReadOnlyPluginCollection(list);
		}

		// Token: 0x06000543 RID: 1347 RVA: 0x00010D58 File Offset: 0x0000EF58
		public PluginCollection()
		{
			this.m_array = new IPlugin[16];
		}

		// Token: 0x06000544 RID: 1348 RVA: 0x00010D6D File Offset: 0x0000EF6D
		public PluginCollection(int capacity)
		{
			this.m_array = new IPlugin[capacity];
		}

		// Token: 0x06000545 RID: 1349 RVA: 0x00010D81 File Offset: 0x0000EF81
		public PluginCollection(PluginCollection c)
		{
			this.m_array = new IPlugin[c.Count];
			this.AddRange(c);
		}

		// Token: 0x06000546 RID: 1350 RVA: 0x00010DA2 File Offset: 0x0000EFA2
		public PluginCollection(IPlugin[] a)
		{
			this.m_array = new IPlugin[a.Length];
			this.AddRange(a);
		}

		// Token: 0x06000547 RID: 1351 RVA: 0x00010DC0 File Offset: 0x0000EFC0
		public PluginCollection(ICollection col)
		{
			this.m_array = new IPlugin[col.Count];
			this.AddRange(col);
		}

		// Token: 0x06000548 RID: 1352 RVA: 0x00010DE1 File Offset: 0x0000EFE1
		protected internal PluginCollection(PluginCollection.Tag tag)
		{
			this.m_array = null;
		}

		// Token: 0x17000121 RID: 289
		// (get) Token: 0x06000549 RID: 1353 RVA: 0x00010DF0 File Offset: 0x0000EFF0
		public virtual int Count
		{
			get
			{
				return this.m_count;
			}
		}

		// Token: 0x0600054A RID: 1354 RVA: 0x00010DF8 File Offset: 0x0000EFF8
		public virtual void CopyTo(IPlugin[] array)
		{
			this.CopyTo(array, 0);
		}

		// Token: 0x0600054B RID: 1355 RVA: 0x00010E02 File Offset: 0x0000F002
		public virtual void CopyTo(IPlugin[] array, int start)
		{
			if (this.m_count > array.GetUpperBound(0) + 1 - start)
			{
				throw new ArgumentException("Destination array was not long enough.");
			}
			Array.Copy(this.m_array, 0, array, start, this.m_count);
		}

		// Token: 0x17000122 RID: 290
		// (get) Token: 0x0600054C RID: 1356 RVA: 0x00010E36 File Offset: 0x0000F036
		public virtual bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000123 RID: 291
		// (get) Token: 0x0600054D RID: 1357 RVA: 0x00010E39 File Offset: 0x0000F039
		public virtual object SyncRoot
		{
			get
			{
				return this.m_array;
			}
		}

		// Token: 0x17000124 RID: 292
		public virtual IPlugin this[int index]
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

		// Token: 0x06000550 RID: 1360 RVA: 0x00010E74 File Offset: 0x0000F074
		public virtual int Add(IPlugin item)
		{
			if (this.m_count == this.m_array.Length)
			{
				this.EnsureCapacity(this.m_count + 1);
			}
			this.m_array[this.m_count] = item;
			this.m_version++;
			return this.m_count++;
		}

		// Token: 0x06000551 RID: 1361 RVA: 0x00010ECC File Offset: 0x0000F0CC
		public virtual void Clear()
		{
			this.m_version++;
			this.m_array = new IPlugin[16];
			this.m_count = 0;
		}

		// Token: 0x06000552 RID: 1362 RVA: 0x00010EF0 File Offset: 0x0000F0F0
		public virtual object Clone()
		{
			PluginCollection pluginCollection = new PluginCollection(this.m_count);
			Array.Copy(this.m_array, 0, pluginCollection.m_array, 0, this.m_count);
			pluginCollection.m_count = this.m_count;
			pluginCollection.m_version = this.m_version;
			return pluginCollection;
		}

		// Token: 0x06000553 RID: 1363 RVA: 0x00010F3C File Offset: 0x0000F13C
		public virtual bool Contains(IPlugin item)
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

		// Token: 0x06000554 RID: 1364 RVA: 0x00010F70 File Offset: 0x0000F170
		public virtual int IndexOf(IPlugin item)
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

		// Token: 0x06000555 RID: 1365 RVA: 0x00010FA4 File Offset: 0x0000F1A4
		public virtual void Insert(int index, IPlugin item)
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

		// Token: 0x06000556 RID: 1366 RVA: 0x00011024 File Offset: 0x0000F224
		public virtual void Remove(IPlugin item)
		{
			int num = this.IndexOf(item);
			if (num < 0)
			{
				throw new ArgumentException("Cannot remove the specified item because it was not found in the specified Collection.");
			}
			this.m_version++;
			this.RemoveAt(num);
		}

		// Token: 0x06000557 RID: 1367 RVA: 0x00011060 File Offset: 0x0000F260
		public virtual void RemoveAt(int index)
		{
			this.ValidateIndex(index);
			this.m_count--;
			if (index < this.m_count)
			{
				Array.Copy(this.m_array, index + 1, this.m_array, index, this.m_count - index);
			}
			IPlugin[] sourceArray = new IPlugin[1];
			Array.Copy(sourceArray, 0, this.m_array, this.m_count, 1);
			this.m_version++;
		}

		// Token: 0x17000125 RID: 293
		// (get) Token: 0x06000558 RID: 1368 RVA: 0x000110D1 File Offset: 0x0000F2D1
		public virtual bool IsFixedSize
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000126 RID: 294
		// (get) Token: 0x06000559 RID: 1369 RVA: 0x000110D4 File Offset: 0x0000F2D4
		public virtual bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600055A RID: 1370 RVA: 0x000110D7 File Offset: 0x0000F2D7
		public virtual PluginCollection.IPluginCollectionEnumerator GetEnumerator()
		{
			return new PluginCollection.Enumerator(this);
		}

		// Token: 0x17000127 RID: 295
		// (get) Token: 0x0600055B RID: 1371 RVA: 0x000110DF File Offset: 0x0000F2DF
		// (set) Token: 0x0600055C RID: 1372 RVA: 0x000110EC File Offset: 0x0000F2EC
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
						IPlugin[] array = new IPlugin[value];
						Array.Copy(this.m_array, 0, array, 0, this.m_count);
						this.m_array = array;
						return;
					}
					this.m_array = new IPlugin[16];
				}
			}
		}

		// Token: 0x0600055D RID: 1373 RVA: 0x0001114C File Offset: 0x0000F34C
		public virtual int AddRange(PluginCollection x)
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

		// Token: 0x0600055E RID: 1374 RVA: 0x000111C8 File Offset: 0x0000F3C8
		public virtual int AddRange(IPlugin[] x)
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

		// Token: 0x0600055F RID: 1375 RVA: 0x00011234 File Offset: 0x0000F434
		public virtual int AddRange(ICollection col)
		{
			if (this.m_count + col.Count >= this.m_array.Length)
			{
				this.EnsureCapacity(this.m_count + col.Count);
			}
			foreach (object obj in col)
			{
				this.Add((IPlugin)obj);
			}
			return this.m_count;
		}

		// Token: 0x06000560 RID: 1376 RVA: 0x000112BC File Offset: 0x0000F4BC
		public virtual void TrimToSize()
		{
			this.Capacity = this.m_count;
		}

		// Token: 0x06000561 RID: 1377 RVA: 0x000112CA File Offset: 0x0000F4CA
		private void ValidateIndex(int i)
		{
			this.ValidateIndex(i, false);
		}

		// Token: 0x06000562 RID: 1378 RVA: 0x000112D4 File Offset: 0x0000F4D4
		private void ValidateIndex(int i, bool allowEqualEnd)
		{
			int num = allowEqualEnd ? this.m_count : (this.m_count - 1);
			if (i < 0 || i > num)
			{
				throw SystemInfo.CreateArgumentOutOfRangeException("i", i, "Index was out of range. Must be non-negative and less than the size of the collection. [" + i + "] Specified argument was out of the range of valid values.");
			}
		}

		// Token: 0x06000563 RID: 1379 RVA: 0x00011324 File Offset: 0x0000F524
		private void EnsureCapacity(int min)
		{
			int num = (this.m_array.Length == 0) ? 16 : (this.m_array.Length * 2);
			if (num < min)
			{
				num = min;
			}
			this.Capacity = num;
		}

		// Token: 0x06000564 RID: 1380 RVA: 0x00011357 File Offset: 0x0000F557
		void ICollection.CopyTo(Array array, int start)
		{
			Array.Copy(this.m_array, 0, array, start, this.m_count);
		}

		// Token: 0x17000128 RID: 296
		object IList.this[int i]
		{
			get
			{
				return this[i];
			}
			set
			{
				this[i] = (IPlugin)value;
			}
		}

		// Token: 0x06000567 RID: 1383 RVA: 0x00011385 File Offset: 0x0000F585
		int IList.Add(object x)
		{
			return this.Add((IPlugin)x);
		}

		// Token: 0x06000568 RID: 1384 RVA: 0x00011393 File Offset: 0x0000F593
		bool IList.Contains(object x)
		{
			return this.Contains((IPlugin)x);
		}

		// Token: 0x06000569 RID: 1385 RVA: 0x000113A1 File Offset: 0x0000F5A1
		int IList.IndexOf(object x)
		{
			return this.IndexOf((IPlugin)x);
		}

		// Token: 0x0600056A RID: 1386 RVA: 0x000113AF File Offset: 0x0000F5AF
		void IList.Insert(int pos, object x)
		{
			this.Insert(pos, (IPlugin)x);
		}

		// Token: 0x0600056B RID: 1387 RVA: 0x000113BE File Offset: 0x0000F5BE
		void IList.Remove(object x)
		{
			this.Remove((IPlugin)x);
		}

		// Token: 0x0600056C RID: 1388 RVA: 0x000113CC File Offset: 0x0000F5CC
		void IList.RemoveAt(int pos)
		{
			this.RemoveAt(pos);
		}

		// Token: 0x0600056D RID: 1389 RVA: 0x000113D5 File Offset: 0x0000F5D5
		IEnumerator IEnumerable.GetEnumerator()
		{
			return (IEnumerator)this.GetEnumerator();
		}

		// Token: 0x04000239 RID: 569
		private const int DEFAULT_CAPACITY = 16;

		// Token: 0x0400023A RID: 570
		private IPlugin[] m_array;

		// Token: 0x0400023B RID: 571
		private int m_count;

		// Token: 0x0400023C RID: 572
		private int m_version;

		// Token: 0x020000BB RID: 187
		public interface IPluginCollectionEnumerator
		{
			// Token: 0x17000129 RID: 297
			// (get) Token: 0x0600056E RID: 1390
			IPlugin Current { get; }

			// Token: 0x0600056F RID: 1391
			bool MoveNext();

			// Token: 0x06000570 RID: 1392
			void Reset();
		}

		// Token: 0x020000BC RID: 188
		protected internal enum Tag
		{
			// Token: 0x0400023E RID: 574
			Default
		}

		// Token: 0x020000BD RID: 189
		private sealed class Enumerator : IEnumerator, PluginCollection.IPluginCollectionEnumerator
		{
			// Token: 0x06000571 RID: 1393 RVA: 0x000113E2 File Offset: 0x0000F5E2
			internal Enumerator(PluginCollection tc)
			{
				this.m_collection = tc;
				this.m_index = -1;
				this.m_version = tc.m_version;
			}

			// Token: 0x1700012A RID: 298
			// (get) Token: 0x06000572 RID: 1394 RVA: 0x00011404 File Offset: 0x0000F604
			public IPlugin Current
			{
				get
				{
					return this.m_collection[this.m_index];
				}
			}

			// Token: 0x06000573 RID: 1395 RVA: 0x00011418 File Offset: 0x0000F618
			public bool MoveNext()
			{
				if (this.m_version != this.m_collection.m_version)
				{
					throw new InvalidOperationException("Collection was modified; enumeration operation may not execute.");
				}
				this.m_index++;
				return this.m_index < this.m_collection.Count;
			}

			// Token: 0x06000574 RID: 1396 RVA: 0x00011464 File Offset: 0x0000F664
			public void Reset()
			{
				this.m_index = -1;
			}

			// Token: 0x1700012B RID: 299
			// (get) Token: 0x06000575 RID: 1397 RVA: 0x0001146D File Offset: 0x0000F66D
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x0400023F RID: 575
			private readonly PluginCollection m_collection;

			// Token: 0x04000240 RID: 576
			private int m_index;

			// Token: 0x04000241 RID: 577
			private int m_version;
		}

		// Token: 0x020000BE RID: 190
		private sealed class ReadOnlyPluginCollection : PluginCollection
		{
			// Token: 0x06000576 RID: 1398 RVA: 0x00011475 File Offset: 0x0000F675
			internal ReadOnlyPluginCollection(PluginCollection list) : base(PluginCollection.Tag.Default)
			{
				this.m_collection = list;
			}

			// Token: 0x06000577 RID: 1399 RVA: 0x00011485 File Offset: 0x0000F685
			public override void CopyTo(IPlugin[] array)
			{
				this.m_collection.CopyTo(array);
			}

			// Token: 0x06000578 RID: 1400 RVA: 0x00011493 File Offset: 0x0000F693
			public override void CopyTo(IPlugin[] array, int start)
			{
				this.m_collection.CopyTo(array, start);
			}

			// Token: 0x1700012C RID: 300
			// (get) Token: 0x06000579 RID: 1401 RVA: 0x000114A2 File Offset: 0x0000F6A2
			public override int Count
			{
				get
				{
					return this.m_collection.Count;
				}
			}

			// Token: 0x1700012D RID: 301
			// (get) Token: 0x0600057A RID: 1402 RVA: 0x000114AF File Offset: 0x0000F6AF
			public override bool IsSynchronized
			{
				get
				{
					return this.m_collection.IsSynchronized;
				}
			}

			// Token: 0x1700012E RID: 302
			// (get) Token: 0x0600057B RID: 1403 RVA: 0x000114BC File Offset: 0x0000F6BC
			public override object SyncRoot
			{
				get
				{
					return this.m_collection.SyncRoot;
				}
			}

			// Token: 0x1700012F RID: 303
			public override IPlugin this[int i]
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

			// Token: 0x0600057E RID: 1406 RVA: 0x000114E3 File Offset: 0x0000F6E3
			public override int Add(IPlugin x)
			{
				throw new NotSupportedException("This is a Read Only Collection and can not be modified");
			}

			// Token: 0x0600057F RID: 1407 RVA: 0x000114EF File Offset: 0x0000F6EF
			public override void Clear()
			{
				throw new NotSupportedException("This is a Read Only Collection and can not be modified");
			}

			// Token: 0x06000580 RID: 1408 RVA: 0x000114FB File Offset: 0x0000F6FB
			public override bool Contains(IPlugin x)
			{
				return this.m_collection.Contains(x);
			}

			// Token: 0x06000581 RID: 1409 RVA: 0x00011509 File Offset: 0x0000F709
			public override int IndexOf(IPlugin x)
			{
				return this.m_collection.IndexOf(x);
			}

			// Token: 0x06000582 RID: 1410 RVA: 0x00011517 File Offset: 0x0000F717
			public override void Insert(int pos, IPlugin x)
			{
				throw new NotSupportedException("This is a Read Only Collection and can not be modified");
			}

			// Token: 0x06000583 RID: 1411 RVA: 0x00011523 File Offset: 0x0000F723
			public override void Remove(IPlugin x)
			{
				throw new NotSupportedException("This is a Read Only Collection and can not be modified");
			}

			// Token: 0x06000584 RID: 1412 RVA: 0x0001152F File Offset: 0x0000F72F
			public override void RemoveAt(int pos)
			{
				throw new NotSupportedException("This is a Read Only Collection and can not be modified");
			}

			// Token: 0x17000130 RID: 304
			// (get) Token: 0x06000585 RID: 1413 RVA: 0x0001153B File Offset: 0x0000F73B
			public override bool IsFixedSize
			{
				get
				{
					return true;
				}
			}

			// Token: 0x17000131 RID: 305
			// (get) Token: 0x06000586 RID: 1414 RVA: 0x0001153E File Offset: 0x0000F73E
			public override bool IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x06000587 RID: 1415 RVA: 0x00011541 File Offset: 0x0000F741
			public override PluginCollection.IPluginCollectionEnumerator GetEnumerator()
			{
				return this.m_collection.GetEnumerator();
			}

			// Token: 0x17000132 RID: 306
			// (get) Token: 0x06000588 RID: 1416 RVA: 0x0001154E File Offset: 0x0000F74E
			// (set) Token: 0x06000589 RID: 1417 RVA: 0x0001155B File Offset: 0x0000F75B
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

			// Token: 0x0600058A RID: 1418 RVA: 0x00011567 File Offset: 0x0000F767
			public override int AddRange(PluginCollection x)
			{
				throw new NotSupportedException("This is a Read Only Collection and can not be modified");
			}

			// Token: 0x0600058B RID: 1419 RVA: 0x00011573 File Offset: 0x0000F773
			public override int AddRange(IPlugin[] x)
			{
				throw new NotSupportedException("This is a Read Only Collection and can not be modified");
			}

			// Token: 0x04000242 RID: 578
			private readonly PluginCollection m_collection;
		}
	}
}
