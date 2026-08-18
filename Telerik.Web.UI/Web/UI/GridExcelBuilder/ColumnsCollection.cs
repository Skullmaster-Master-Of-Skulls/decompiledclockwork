using System;
using System.Collections;

namespace Telerik.Web.UI.GridExcelBuilder
{
	// Token: 0x02001B16 RID: 6934
	public class ColumnsCollection : IColumnsCollection, IList, ICollection, IEnumerable
	{
		// Token: 0x06010C6D RID: 68717 RVA: 0x003B9891 File Offset: 0x003B7A91
		public ColumnsCollection() : this(new ArrayList())
		{
		}

		// Token: 0x06010C6E RID: 68718 RVA: 0x003B989E File Offset: 0x003B7A9E
		public ColumnsCollection(IList list)
		{
			this._list = list;
		}

		// Token: 0x06010C6F RID: 68719 RVA: 0x003B98AD File Offset: 0x003B7AAD
		public virtual int Add(ColumnElement value)
		{
			return this._list.Add(value);
		}

		// Token: 0x06010C70 RID: 68720 RVA: 0x003B98BB File Offset: 0x003B7ABB
		public virtual void Clear()
		{
			this._list.Clear();
		}

		// Token: 0x06010C71 RID: 68721 RVA: 0x003B98C8 File Offset: 0x003B7AC8
		public virtual bool Contains(ColumnElement value)
		{
			return this._list.Contains(value);
		}

		// Token: 0x06010C72 RID: 68722 RVA: 0x003B98D6 File Offset: 0x003B7AD6
		public virtual int IndexOf(ColumnElement value)
		{
			return this._list.IndexOf(value);
		}

		// Token: 0x06010C73 RID: 68723 RVA: 0x003B98E4 File Offset: 0x003B7AE4
		public virtual void Insert(int index, ColumnElement value)
		{
			this._list.Insert(index, value);
		}

		// Token: 0x170051B4 RID: 20916
		// (get) Token: 0x06010C74 RID: 68724 RVA: 0x003B98F3 File Offset: 0x003B7AF3
		public virtual bool IsFixedSize
		{
			get
			{
				return this._list.IsFixedSize;
			}
		}

		// Token: 0x170051B5 RID: 20917
		// (get) Token: 0x06010C75 RID: 68725 RVA: 0x003B9900 File Offset: 0x003B7B00
		public virtual bool IsReadOnly
		{
			get
			{
				return this._list.IsReadOnly;
			}
		}

		// Token: 0x06010C76 RID: 68726 RVA: 0x003B990D File Offset: 0x003B7B0D
		public virtual void Remove(ColumnElement value)
		{
			this._list.Remove(value);
		}

		// Token: 0x06010C77 RID: 68727 RVA: 0x003B991B File Offset: 0x003B7B1B
		public virtual void RemoveAt(int index)
		{
			this._list.Remove(index);
		}

		// Token: 0x170051B6 RID: 20918
		public virtual ColumnElement this[int index]
		{
			get
			{
				if (index > this._list.Count)
				{
					throw new IndexOutOfRangeException();
				}
				return this._list[index] as ColumnElement;
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

		// Token: 0x06010C7A RID: 68730 RVA: 0x003B9978 File Offset: 0x003B7B78
		int IList.Add(object value)
		{
			return this.Add((ColumnElement)value);
		}

		// Token: 0x06010C7B RID: 68731 RVA: 0x003B9986 File Offset: 0x003B7B86
		void IList.Clear()
		{
			this.Clear();
		}

		// Token: 0x06010C7C RID: 68732 RVA: 0x003B998E File Offset: 0x003B7B8E
		bool IList.Contains(object value)
		{
			return this.Contains((ColumnElement)value);
		}

		// Token: 0x06010C7D RID: 68733 RVA: 0x003B999C File Offset: 0x003B7B9C
		int IList.IndexOf(object value)
		{
			return this.IndexOf((ColumnElement)value);
		}

		// Token: 0x06010C7E RID: 68734 RVA: 0x003B99AA File Offset: 0x003B7BAA
		void IList.Insert(int index, object value)
		{
			this.Insert(index, (ColumnElement)value);
		}

		// Token: 0x170051B7 RID: 20919
		// (get) Token: 0x06010C7F RID: 68735 RVA: 0x003B99B9 File Offset: 0x003B7BB9
		bool IList.IsFixedSize
		{
			get
			{
				return this.IsFixedSize;
			}
		}

		// Token: 0x170051B8 RID: 20920
		// (get) Token: 0x06010C80 RID: 68736 RVA: 0x003B99C1 File Offset: 0x003B7BC1
		bool IList.IsReadOnly
		{
			get
			{
				return this.IsReadOnly;
			}
		}

		// Token: 0x06010C81 RID: 68737 RVA: 0x003B99C9 File Offset: 0x003B7BC9
		void IList.Remove(object value)
		{
			this.Remove((ColumnElement)value);
		}

		// Token: 0x06010C82 RID: 68738 RVA: 0x003B99D7 File Offset: 0x003B7BD7
		void IList.RemoveAt(int index)
		{
			this.RemoveAt(index);
		}

		// Token: 0x170051B9 RID: 20921
		object IList.this[int index]
		{
			get
			{
				return this[index];
			}
			set
			{
				this[index] = (ColumnElement)value;
			}
		}

		// Token: 0x06010C85 RID: 68741 RVA: 0x003B99F8 File Offset: 0x003B7BF8
		public void CopyTo(ColumnElement[] array, int index)
		{
			this._list.CopyTo(array, index);
		}

		// Token: 0x170051BA RID: 20922
		// (get) Token: 0x06010C86 RID: 68742 RVA: 0x003B9A07 File Offset: 0x003B7C07
		public int Count
		{
			get
			{
				return this._list.Count;
			}
		}

		// Token: 0x170051BB RID: 20923
		// (get) Token: 0x06010C87 RID: 68743 RVA: 0x003B9A14 File Offset: 0x003B7C14
		public bool IsSynchronized
		{
			get
			{
				return this._list.IsSynchronized;
			}
		}

		// Token: 0x170051BC RID: 20924
		// (get) Token: 0x06010C88 RID: 68744 RVA: 0x003B9A21 File Offset: 0x003B7C21
		public object SyncRoot
		{
			get
			{
				return this._list.SyncRoot;
			}
		}

		// Token: 0x06010C89 RID: 68745 RVA: 0x003B9A2E File Offset: 0x003B7C2E
		void ICollection.CopyTo(Array array, int index)
		{
			this.CopyTo((ColumnElement[])array, index);
		}

		// Token: 0x170051BD RID: 20925
		// (get) Token: 0x06010C8A RID: 68746 RVA: 0x003B9A3D File Offset: 0x003B7C3D
		int ICollection.Count
		{
			get
			{
				return this.Count;
			}
		}

		// Token: 0x170051BE RID: 20926
		// (get) Token: 0x06010C8B RID: 68747 RVA: 0x003B9A45 File Offset: 0x003B7C45
		bool ICollection.IsSynchronized
		{
			get
			{
				return this.IsSynchronized;
			}
		}

		// Token: 0x170051BF RID: 20927
		// (get) Token: 0x06010C8C RID: 68748 RVA: 0x003B9A4D File Offset: 0x003B7C4D
		object ICollection.SyncRoot
		{
			get
			{
				return this.SyncRoot;
			}
		}

		// Token: 0x06010C8D RID: 68749 RVA: 0x003B9A55 File Offset: 0x003B7C55
		public virtual IEnumerator GetEnumerator()
		{
			return this._list.GetEnumerator();
		}

		// Token: 0x06010C8E RID: 68750 RVA: 0x003B9A62 File Offset: 0x003B7C62
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x04004AE2 RID: 19170
		private IList _list;
	}
}
