using System;
using System.Collections;

namespace Telerik.Web.UI.GridExcelBuilder
{
	// Token: 0x02001B28 RID: 6952
	public class RowsCollection : IRowsCollection, IList, ICollection, IEnumerable
	{
		// Token: 0x06010CF3 RID: 68851 RVA: 0x003BB8B7 File Offset: 0x003B9AB7
		public RowsCollection() : this(new ArrayList())
		{
		}

		// Token: 0x06010CF4 RID: 68852 RVA: 0x003BB8C4 File Offset: 0x003B9AC4
		public RowsCollection(IList list)
		{
			this._list = list;
		}

		// Token: 0x06010CF5 RID: 68853 RVA: 0x003BB8D3 File Offset: 0x003B9AD3
		public virtual int Add(RowElement value)
		{
			return this._list.Add(value);
		}

		// Token: 0x06010CF6 RID: 68854 RVA: 0x003BB8E1 File Offset: 0x003B9AE1
		public virtual void Clear()
		{
			this._list.Clear();
		}

		// Token: 0x06010CF7 RID: 68855 RVA: 0x003BB8EE File Offset: 0x003B9AEE
		public virtual bool Contains(RowElement value)
		{
			return this._list.Contains(value);
		}

		// Token: 0x06010CF8 RID: 68856 RVA: 0x003BB8FC File Offset: 0x003B9AFC
		public virtual int IndexOf(RowElement value)
		{
			return this._list.IndexOf(value);
		}

		// Token: 0x06010CF9 RID: 68857 RVA: 0x003BB90A File Offset: 0x003B9B0A
		public virtual void Insert(int index, RowElement value)
		{
			this._list.Insert(index, value);
		}

		// Token: 0x170051DF RID: 20959
		// (get) Token: 0x06010CFA RID: 68858 RVA: 0x003BB919 File Offset: 0x003B9B19
		public virtual bool IsFixedSize
		{
			get
			{
				return this._list.IsFixedSize;
			}
		}

		// Token: 0x170051E0 RID: 20960
		// (get) Token: 0x06010CFB RID: 68859 RVA: 0x003BB926 File Offset: 0x003B9B26
		public virtual bool IsReadOnly
		{
			get
			{
				return this._list.IsReadOnly;
			}
		}

		// Token: 0x06010CFC RID: 68860 RVA: 0x003BB933 File Offset: 0x003B9B33
		public virtual void Remove(RowElement value)
		{
			this._list.Remove(value);
		}

		// Token: 0x06010CFD RID: 68861 RVA: 0x003BB941 File Offset: 0x003B9B41
		public virtual void RemoveAt(int index)
		{
			this._list.Remove(index);
		}

		// Token: 0x170051E1 RID: 20961
		public virtual RowElement this[int index]
		{
			get
			{
				if (index > this._list.Count)
				{
					throw new IndexOutOfRangeException();
				}
				return this._list[index] as RowElement;
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

		// Token: 0x06010D00 RID: 68864 RVA: 0x003BB99E File Offset: 0x003B9B9E
		int IList.Add(object value)
		{
			return this.Add((RowElement)value);
		}

		// Token: 0x06010D01 RID: 68865 RVA: 0x003BB9AC File Offset: 0x003B9BAC
		void IList.Clear()
		{
			this.Clear();
		}

		// Token: 0x06010D02 RID: 68866 RVA: 0x003BB9B4 File Offset: 0x003B9BB4
		bool IList.Contains(object value)
		{
			return this.Contains((RowElement)value);
		}

		// Token: 0x06010D03 RID: 68867 RVA: 0x003BB9C2 File Offset: 0x003B9BC2
		int IList.IndexOf(object value)
		{
			return this.IndexOf((RowElement)value);
		}

		// Token: 0x06010D04 RID: 68868 RVA: 0x003BB9D0 File Offset: 0x003B9BD0
		void IList.Insert(int index, object value)
		{
			this.Insert(index, (RowElement)value);
		}

		// Token: 0x170051E2 RID: 20962
		// (get) Token: 0x06010D05 RID: 68869 RVA: 0x003BB9DF File Offset: 0x003B9BDF
		bool IList.IsFixedSize
		{
			get
			{
				return this.IsFixedSize;
			}
		}

		// Token: 0x170051E3 RID: 20963
		// (get) Token: 0x06010D06 RID: 68870 RVA: 0x003BB9E7 File Offset: 0x003B9BE7
		bool IList.IsReadOnly
		{
			get
			{
				return this.IsReadOnly;
			}
		}

		// Token: 0x06010D07 RID: 68871 RVA: 0x003BB9EF File Offset: 0x003B9BEF
		void IList.Remove(object value)
		{
			this.Remove((RowElement)value);
		}

		// Token: 0x06010D08 RID: 68872 RVA: 0x003BB9FD File Offset: 0x003B9BFD
		void IList.RemoveAt(int index)
		{
			this.RemoveAt(index);
		}

		// Token: 0x170051E4 RID: 20964
		object IList.this[int index]
		{
			get
			{
				return this[index];
			}
			set
			{
				this[index] = (RowElement)value;
			}
		}

		// Token: 0x06010D0B RID: 68875 RVA: 0x003BBA1E File Offset: 0x003B9C1E
		public void CopyTo(RowElement[] array, int index)
		{
			this._list.CopyTo(array, index);
		}

		// Token: 0x170051E5 RID: 20965
		// (get) Token: 0x06010D0C RID: 68876 RVA: 0x003BBA2D File Offset: 0x003B9C2D
		public int Count
		{
			get
			{
				return this._list.Count;
			}
		}

		// Token: 0x170051E6 RID: 20966
		// (get) Token: 0x06010D0D RID: 68877 RVA: 0x003BBA3A File Offset: 0x003B9C3A
		public bool IsSynchronized
		{
			get
			{
				return this._list.IsSynchronized;
			}
		}

		// Token: 0x170051E7 RID: 20967
		// (get) Token: 0x06010D0E RID: 68878 RVA: 0x003BBA47 File Offset: 0x003B9C47
		public object SyncRoot
		{
			get
			{
				return this._list.SyncRoot;
			}
		}

		// Token: 0x06010D0F RID: 68879 RVA: 0x003BBA54 File Offset: 0x003B9C54
		void ICollection.CopyTo(Array array, int index)
		{
			this.CopyTo((RowElement[])array, index);
		}

		// Token: 0x170051E8 RID: 20968
		// (get) Token: 0x06010D10 RID: 68880 RVA: 0x003BBA63 File Offset: 0x003B9C63
		int ICollection.Count
		{
			get
			{
				return this.Count;
			}
		}

		// Token: 0x170051E9 RID: 20969
		// (get) Token: 0x06010D11 RID: 68881 RVA: 0x003BBA6B File Offset: 0x003B9C6B
		bool ICollection.IsSynchronized
		{
			get
			{
				return this.IsSynchronized;
			}
		}

		// Token: 0x170051EA RID: 20970
		// (get) Token: 0x06010D12 RID: 68882 RVA: 0x003BBA73 File Offset: 0x003B9C73
		object ICollection.SyncRoot
		{
			get
			{
				return this.SyncRoot;
			}
		}

		// Token: 0x06010D13 RID: 68883 RVA: 0x003BBA7B File Offset: 0x003B9C7B
		public virtual IEnumerator GetEnumerator()
		{
			return this._list.GetEnumerator();
		}

		// Token: 0x06010D14 RID: 68884 RVA: 0x003BBA88 File Offset: 0x003B9C88
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x04004B39 RID: 19257
		private IList _list;
	}
}
