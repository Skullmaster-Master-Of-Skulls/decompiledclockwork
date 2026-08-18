using System;
using System.Collections;

namespace Telerik.Web.UI.GridExcelBuilder
{
	// Token: 0x02001B14 RID: 6932
	public class CellsCollection : IList, ICollection, IEnumerable
	{
		// Token: 0x06010C43 RID: 68675 RVA: 0x003B9573 File Offset: 0x003B7773
		public CellsCollection()
		{
			this._list = new ArrayList();
		}

		// Token: 0x06010C44 RID: 68676 RVA: 0x003B9586 File Offset: 0x003B7786
		public virtual int Add(CellElement value)
		{
			return this._list.Add(value);
		}

		// Token: 0x06010C45 RID: 68677 RVA: 0x003B9594 File Offset: 0x003B7794
		public virtual void Clear()
		{
			this._list.Clear();
		}

		// Token: 0x06010C46 RID: 68678 RVA: 0x003B95A4 File Offset: 0x003B77A4
		public virtual CellElement GetCellByName(string uniqueName)
		{
			foreach (object obj in this._list)
			{
				try
				{
					CellElement cellElement = (CellElement)obj;
					if (cellElement.ColumnName.Equals(uniqueName, StringComparison.InvariantCultureIgnoreCase))
					{
						return cellElement;
					}
				}
				catch (InvalidCastException)
				{
				}
			}
			return null;
		}

		// Token: 0x06010C47 RID: 68679 RVA: 0x003B9624 File Offset: 0x003B7824
		public virtual bool Contains(CellElement value)
		{
			return this._list.Contains(value);
		}

		// Token: 0x06010C48 RID: 68680 RVA: 0x003B9632 File Offset: 0x003B7832
		public virtual int IndexOf(CellElement value)
		{
			return this._list.IndexOf(value);
		}

		// Token: 0x06010C49 RID: 68681 RVA: 0x003B9640 File Offset: 0x003B7840
		public virtual void Insert(int index, CellElement value)
		{
			this._list.Insert(index, value);
		}

		// Token: 0x170051A4 RID: 20900
		// (get) Token: 0x06010C4A RID: 68682 RVA: 0x003B964F File Offset: 0x003B784F
		public virtual bool IsFixedSize
		{
			get
			{
				return this._list.IsFixedSize;
			}
		}

		// Token: 0x170051A5 RID: 20901
		// (get) Token: 0x06010C4B RID: 68683 RVA: 0x003B965C File Offset: 0x003B785C
		public virtual bool IsReadOnly
		{
			get
			{
				return this._list.IsReadOnly;
			}
		}

		// Token: 0x06010C4C RID: 68684 RVA: 0x003B9669 File Offset: 0x003B7869
		public virtual void Remove(CellElement value)
		{
			this._list.Remove(value);
		}

		// Token: 0x06010C4D RID: 68685 RVA: 0x003B9677 File Offset: 0x003B7877
		public virtual void RemoveAt(int index)
		{
			this._list.Remove(index);
		}

		// Token: 0x170051A6 RID: 20902
		public virtual CellElement this[int index]
		{
			get
			{
				if (index > this._list.Count)
				{
					throw new IndexOutOfRangeException();
				}
				return this._list[index] as CellElement;
			}
			set
			{
				if (index > this._list.Count)
				{
					throw new IndexOutOfRangeException();
				}
				this._list[index] = value;
			}
		}

		// Token: 0x06010C50 RID: 68688 RVA: 0x003B96D4 File Offset: 0x003B78D4
		int IList.Add(object value)
		{
			return this.Add((CellElement)value);
		}

		// Token: 0x06010C51 RID: 68689 RVA: 0x003B96E2 File Offset: 0x003B78E2
		void IList.Clear()
		{
			this.Clear();
		}

		// Token: 0x06010C52 RID: 68690 RVA: 0x003B96EA File Offset: 0x003B78EA
		bool IList.Contains(object value)
		{
			return this.Contains((CellElement)value);
		}

		// Token: 0x06010C53 RID: 68691 RVA: 0x003B96F8 File Offset: 0x003B78F8
		int IList.IndexOf(object value)
		{
			return this.IndexOf((CellElement)value);
		}

		// Token: 0x06010C54 RID: 68692 RVA: 0x003B9706 File Offset: 0x003B7906
		void IList.Insert(int index, object value)
		{
			this.Insert(index, (CellElement)value);
		}

		// Token: 0x170051A7 RID: 20903
		// (get) Token: 0x06010C55 RID: 68693 RVA: 0x003B9715 File Offset: 0x003B7915
		bool IList.IsFixedSize
		{
			get
			{
				return this.IsFixedSize;
			}
		}

		// Token: 0x170051A8 RID: 20904
		// (get) Token: 0x06010C56 RID: 68694 RVA: 0x003B971D File Offset: 0x003B791D
		bool IList.IsReadOnly
		{
			get
			{
				return this.IsReadOnly;
			}
		}

		// Token: 0x06010C57 RID: 68695 RVA: 0x003B9725 File Offset: 0x003B7925
		void IList.Remove(object value)
		{
			this.Remove((CellElement)value);
		}

		// Token: 0x06010C58 RID: 68696 RVA: 0x003B9733 File Offset: 0x003B7933
		void IList.RemoveAt(int index)
		{
			this.RemoveAt(index);
		}

		// Token: 0x170051A9 RID: 20905
		object IList.this[int index]
		{
			get
			{
				return this[index];
			}
			set
			{
				this[index] = (CellElement)value;
			}
		}

		// Token: 0x06010C5B RID: 68699 RVA: 0x003B9754 File Offset: 0x003B7954
		public void CopyTo(CellElement[] array, int index)
		{
			this._list.CopyTo(array, index);
		}

		// Token: 0x170051AA RID: 20906
		// (get) Token: 0x06010C5C RID: 68700 RVA: 0x003B9763 File Offset: 0x003B7963
		public int Count
		{
			get
			{
				return this._list.Count;
			}
		}

		// Token: 0x170051AB RID: 20907
		// (get) Token: 0x06010C5D RID: 68701 RVA: 0x003B9770 File Offset: 0x003B7970
		public bool IsSynchronized
		{
			get
			{
				return this._list.IsSynchronized;
			}
		}

		// Token: 0x170051AC RID: 20908
		// (get) Token: 0x06010C5E RID: 68702 RVA: 0x003B977D File Offset: 0x003B797D
		public object SyncRoot
		{
			get
			{
				return this._list.SyncRoot;
			}
		}

		// Token: 0x06010C5F RID: 68703 RVA: 0x003B978A File Offset: 0x003B798A
		void ICollection.CopyTo(Array array, int index)
		{
			this.CopyTo((CellElement[])array, index);
		}

		// Token: 0x170051AD RID: 20909
		// (get) Token: 0x06010C60 RID: 68704 RVA: 0x003B9799 File Offset: 0x003B7999
		int ICollection.Count
		{
			get
			{
				return this.Count;
			}
		}

		// Token: 0x170051AE RID: 20910
		// (get) Token: 0x06010C61 RID: 68705 RVA: 0x003B97A1 File Offset: 0x003B79A1
		bool ICollection.IsSynchronized
		{
			get
			{
				return this.IsSynchronized;
			}
		}

		// Token: 0x170051AF RID: 20911
		// (get) Token: 0x06010C62 RID: 68706 RVA: 0x003B97A9 File Offset: 0x003B79A9
		object ICollection.SyncRoot
		{
			get
			{
				return this.SyncRoot;
			}
		}

		// Token: 0x06010C63 RID: 68707 RVA: 0x003B97B1 File Offset: 0x003B79B1
		public virtual IEnumerator GetEnumerator()
		{
			return this._list.GetEnumerator();
		}

		// Token: 0x06010C64 RID: 68708 RVA: 0x003B97BE File Offset: 0x003B79BE
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x04004ADF RID: 19167
		private IList _list;
	}
}
