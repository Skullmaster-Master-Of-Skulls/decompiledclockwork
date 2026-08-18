using System;
using System.Collections;
using System.Design;
using System.Globalization;

namespace System.Web.UI.Design
{
	// Token: 0x02000035 RID: 53
	public class DesignerRegionCollection : IList, ICollection, IEnumerable
	{
		// Token: 0x060001C3 RID: 451 RVA: 0x0000362F File Offset: 0x0000182F
		public DesignerRegionCollection()
		{
		}

		// Token: 0x060001C4 RID: 452 RVA: 0x0000CDB1 File Offset: 0x0000AFB1
		public DesignerRegionCollection(ControlDesigner owner)
		{
			this._owner = owner;
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x060001C5 RID: 453 RVA: 0x0000CDC0 File Offset: 0x0000AFC0
		public int Count
		{
			get
			{
				return this.InternalList.Count;
			}
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x060001C6 RID: 454 RVA: 0x0000CDCD File Offset: 0x0000AFCD
		private ArrayList InternalList
		{
			get
			{
				if (this._list == null)
				{
					this._list = new ArrayList();
				}
				return this._list;
			}
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x060001C7 RID: 455 RVA: 0x0000CDE8 File Offset: 0x0000AFE8
		public bool IsFixedSize
		{
			get
			{
				return this.InternalList.IsFixedSize;
			}
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x060001C8 RID: 456 RVA: 0x0000CDF5 File Offset: 0x0000AFF5
		public bool IsReadOnly
		{
			get
			{
				return this.InternalList.IsReadOnly;
			}
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x060001C9 RID: 457 RVA: 0x0000CE02 File Offset: 0x0000B002
		public bool IsSynchronized
		{
			get
			{
				return this.InternalList.IsSynchronized;
			}
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x060001CA RID: 458 RVA: 0x0000CE0F File Offset: 0x0000B00F
		public ControlDesigner Owner
		{
			get
			{
				return this._owner;
			}
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x060001CB RID: 459 RVA: 0x0000CE17 File Offset: 0x0000B017
		public object SyncRoot
		{
			get
			{
				return this.InternalList.SyncRoot;
			}
		}

		// Token: 0x17000068 RID: 104
		public DesignerRegion this[int index]
		{
			get
			{
				return (DesignerRegion)this.InternalList[index];
			}
			set
			{
				this.InternalList[index] = value;
			}
		}

		// Token: 0x060001CE RID: 462 RVA: 0x0000CE46 File Offset: 0x0000B046
		public int Add(DesignerRegion region)
		{
			return this.InternalList.Add(region);
		}

		// Token: 0x060001CF RID: 463 RVA: 0x0000CE54 File Offset: 0x0000B054
		public void Clear()
		{
			this.InternalList.Clear();
		}

		// Token: 0x060001D0 RID: 464 RVA: 0x0000CE61 File Offset: 0x0000B061
		public void CopyTo(Array array, int index)
		{
			this.InternalList.CopyTo(array, index);
		}

		// Token: 0x060001D1 RID: 465 RVA: 0x0000CE70 File Offset: 0x0000B070
		public IEnumerator GetEnumerator()
		{
			return this.InternalList.GetEnumerator();
		}

		// Token: 0x060001D2 RID: 466 RVA: 0x0000CE7D File Offset: 0x0000B07D
		public bool Contains(DesignerRegion region)
		{
			return this.InternalList.Contains(region);
		}

		// Token: 0x060001D3 RID: 467 RVA: 0x0000CE8B File Offset: 0x0000B08B
		public int IndexOf(DesignerRegion region)
		{
			return this.InternalList.IndexOf(region);
		}

		// Token: 0x060001D4 RID: 468 RVA: 0x0000CE99 File Offset: 0x0000B099
		public void Insert(int index, DesignerRegion region)
		{
			this.InternalList.Insert(index, region);
		}

		// Token: 0x060001D5 RID: 469 RVA: 0x0000CEA8 File Offset: 0x0000B0A8
		public void Remove(DesignerRegion region)
		{
			this.InternalList.Remove(region);
		}

		// Token: 0x060001D6 RID: 470 RVA: 0x0000CEB6 File Offset: 0x0000B0B6
		public void RemoveAt(int index)
		{
			this.InternalList.RemoveAt(index);
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x060001D7 RID: 471 RVA: 0x0000CEC4 File Offset: 0x0000B0C4
		int ICollection.Count
		{
			get
			{
				return this.Count;
			}
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x060001D8 RID: 472 RVA: 0x0000CECC File Offset: 0x0000B0CC
		bool IList.IsFixedSize
		{
			get
			{
				return this.IsFixedSize;
			}
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x060001D9 RID: 473 RVA: 0x0000CED4 File Offset: 0x0000B0D4
		bool IList.IsReadOnly
		{
			get
			{
				return this.IsReadOnly;
			}
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x060001DA RID: 474 RVA: 0x0000CEDC File Offset: 0x0000B0DC
		bool ICollection.IsSynchronized
		{
			get
			{
				return this.IsSynchronized;
			}
		}

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x060001DB RID: 475 RVA: 0x0000CEE4 File Offset: 0x0000B0E4
		object ICollection.SyncRoot
		{
			get
			{
				return this.SyncRoot;
			}
		}

		// Token: 0x1700006E RID: 110
		object IList.this[int index]
		{
			get
			{
				return this[index];
			}
			set
			{
				if (!(value is DesignerRegion))
				{
					throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, SR.GetString("WrongType"), new object[]
					{
						"DesignerRegion"
					}), "value");
				}
				this[index] = (DesignerRegion)value;
			}
		}

		// Token: 0x060001DE RID: 478 RVA: 0x0000CF48 File Offset: 0x0000B148
		int IList.Add(object o)
		{
			if (!(o is DesignerRegion))
			{
				throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, SR.GetString("WrongType"), new object[]
				{
					"DesignerRegion"
				}), "o");
			}
			return this.Add((DesignerRegion)o);
		}

		// Token: 0x060001DF RID: 479 RVA: 0x0000CF96 File Offset: 0x0000B196
		void IList.Clear()
		{
			this.Clear();
		}

		// Token: 0x060001E0 RID: 480 RVA: 0x0000CFA0 File Offset: 0x0000B1A0
		bool IList.Contains(object o)
		{
			if (!(o is DesignerRegion))
			{
				throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, SR.GetString("WrongType"), new object[]
				{
					"DesignerRegion"
				}), "o");
			}
			return this.Contains((DesignerRegion)o);
		}

		// Token: 0x060001E1 RID: 481 RVA: 0x0000CFEE File Offset: 0x0000B1EE
		void ICollection.CopyTo(Array array, int index)
		{
			this.CopyTo(array, index);
		}

		// Token: 0x060001E2 RID: 482 RVA: 0x0000CFF8 File Offset: 0x0000B1F8
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x060001E3 RID: 483 RVA: 0x0000D000 File Offset: 0x0000B200
		int IList.IndexOf(object o)
		{
			if (!(o is DesignerRegion))
			{
				throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, SR.GetString("WrongType"), new object[]
				{
					"DesignerRegion"
				}), "o");
			}
			return this.IndexOf((DesignerRegion)o);
		}

		// Token: 0x060001E4 RID: 484 RVA: 0x0000D050 File Offset: 0x0000B250
		void IList.Insert(int index, object o)
		{
			if (!(o is DesignerRegion))
			{
				throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, SR.GetString("WrongType"), new object[]
				{
					"DesignerRegion"
				}), "o");
			}
			this.Insert(index, (DesignerRegion)o);
		}

		// Token: 0x060001E5 RID: 485 RVA: 0x0000D0A0 File Offset: 0x0000B2A0
		void IList.Remove(object o)
		{
			if (!(o is DesignerRegion))
			{
				throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, SR.GetString("WrongType"), new object[]
				{
					"DesignerRegion"
				}), "o");
			}
			this.Remove((DesignerRegion)o);
		}

		// Token: 0x060001E6 RID: 486 RVA: 0x0000D0EE File Offset: 0x0000B2EE
		void IList.RemoveAt(int index)
		{
			this.RemoveAt(index);
		}

		// Token: 0x0400012B RID: 299
		private ArrayList _list;

		// Token: 0x0400012C RID: 300
		private ControlDesigner _owner;
	}
}
