using System;
using System.Collections;

namespace Telerik.Web.UI.GridExcelBuilder
{
	// Token: 0x02001B2A RID: 6954
	public class StylesCollection : IStylesCollection, IList, ICollection, IEnumerable
	{
		// Token: 0x06010D23 RID: 68899 RVA: 0x003BBD18 File Offset: 0x003B9F18
		public StylesCollection(IList list)
		{
			this._list = list;
		}

		// Token: 0x06010D24 RID: 68900 RVA: 0x003BBD27 File Offset: 0x003B9F27
		public StylesCollection() : this(new ArrayList())
		{
		}

		// Token: 0x06010D25 RID: 68901 RVA: 0x003BBD34 File Offset: 0x003B9F34
		public virtual int Add(StyleElement value)
		{
			return this._list.Add(value);
		}

		// Token: 0x06010D26 RID: 68902 RVA: 0x003BBD42 File Offset: 0x003B9F42
		public virtual void Clear()
		{
			this._list.Clear();
		}

		// Token: 0x06010D27 RID: 68903 RVA: 0x003BBD4F File Offset: 0x003B9F4F
		public virtual bool Contains(StyleElement value)
		{
			return this._list.Contains(value);
		}

		// Token: 0x06010D28 RID: 68904 RVA: 0x003BBD5D File Offset: 0x003B9F5D
		public virtual int IndexOf(StyleElement value)
		{
			return this._list.IndexOf(value);
		}

		// Token: 0x06010D29 RID: 68905 RVA: 0x003BBD6B File Offset: 0x003B9F6B
		public virtual void Insert(int index, StyleElement value)
		{
			this._list.Insert(index, value);
		}

		// Token: 0x170051F4 RID: 20980
		// (get) Token: 0x06010D2A RID: 68906 RVA: 0x003BBD7A File Offset: 0x003B9F7A
		public virtual bool IsFixedSize
		{
			get
			{
				return this._list.IsFixedSize;
			}
		}

		// Token: 0x170051F5 RID: 20981
		// (get) Token: 0x06010D2B RID: 68907 RVA: 0x003BBD87 File Offset: 0x003B9F87
		public virtual bool IsReadOnly
		{
			get
			{
				return this._list.IsReadOnly;
			}
		}

		// Token: 0x06010D2C RID: 68908 RVA: 0x003BBD94 File Offset: 0x003B9F94
		public virtual void Remove(StyleElement value)
		{
			this._list.Remove(value);
		}

		// Token: 0x06010D2D RID: 68909 RVA: 0x003BBDA2 File Offset: 0x003B9FA2
		public virtual void RemoveAt(int index)
		{
			this._list.RemoveAt(index);
		}

		// Token: 0x170051F6 RID: 20982
		public virtual StyleElement this[int index]
		{
			get
			{
				if (index > this._list.Count)
				{
					throw new IndexOutOfRangeException();
				}
				return (StyleElement)this._list[index];
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

		// Token: 0x06010D30 RID: 68912 RVA: 0x003BBDFA File Offset: 0x003B9FFA
		int IList.Add(object value)
		{
			return this.Add((StyleElement)value);
		}

		// Token: 0x06010D31 RID: 68913 RVA: 0x003BBE08 File Offset: 0x003BA008
		void IList.Clear()
		{
			this.Clear();
		}

		// Token: 0x06010D32 RID: 68914 RVA: 0x003BBE10 File Offset: 0x003BA010
		bool IList.Contains(object value)
		{
			return this.Contains((StyleElement)value);
		}

		// Token: 0x06010D33 RID: 68915 RVA: 0x003BBE1E File Offset: 0x003BA01E
		int IList.IndexOf(object value)
		{
			return this.IndexOf((StyleElement)value);
		}

		// Token: 0x06010D34 RID: 68916 RVA: 0x003BBE2C File Offset: 0x003BA02C
		void IList.Insert(int index, object value)
		{
			this.Insert(index, (StyleElement)value);
		}

		// Token: 0x170051F7 RID: 20983
		// (get) Token: 0x06010D35 RID: 68917 RVA: 0x003BBE3B File Offset: 0x003BA03B
		bool IList.IsFixedSize
		{
			get
			{
				return this.IsFixedSize;
			}
		}

		// Token: 0x170051F8 RID: 20984
		// (get) Token: 0x06010D36 RID: 68918 RVA: 0x003BBE43 File Offset: 0x003BA043
		bool IList.IsReadOnly
		{
			get
			{
				return this.IsReadOnly;
			}
		}

		// Token: 0x06010D37 RID: 68919 RVA: 0x003BBE4B File Offset: 0x003BA04B
		void IList.Remove(object value)
		{
			this.Remove((StyleElement)value);
		}

		// Token: 0x06010D38 RID: 68920 RVA: 0x003BBE59 File Offset: 0x003BA059
		void IList.RemoveAt(int index)
		{
			this.RemoveAt(index);
		}

		// Token: 0x170051F9 RID: 20985
		object IList.this[int index]
		{
			get
			{
				return this[index];
			}
			set
			{
				this[index] = (StyleElement)value;
			}
		}

		// Token: 0x06010D3B RID: 68923 RVA: 0x003BBE7A File Offset: 0x003BA07A
		public virtual void CopyTo(StyleElement[] array, int index)
		{
			this._list.CopyTo(array, index);
		}

		// Token: 0x170051FA RID: 20986
		// (get) Token: 0x06010D3C RID: 68924 RVA: 0x003BBE89 File Offset: 0x003BA089
		public virtual int Count
		{
			get
			{
				return this._list.Count;
			}
		}

		// Token: 0x170051FB RID: 20987
		// (get) Token: 0x06010D3D RID: 68925 RVA: 0x003BBE96 File Offset: 0x003BA096
		public virtual bool IsSynchronized
		{
			get
			{
				return this._list.IsSynchronized;
			}
		}

		// Token: 0x170051FC RID: 20988
		// (get) Token: 0x06010D3E RID: 68926 RVA: 0x003BBEA3 File Offset: 0x003BA0A3
		public virtual object SyncRoot
		{
			get
			{
				return this._list.SyncRoot;
			}
		}

		// Token: 0x06010D3F RID: 68927 RVA: 0x003BBEB0 File Offset: 0x003BA0B0
		void ICollection.CopyTo(Array array, int index)
		{
			this.CopyTo((StyleElement[])array, index);
		}

		// Token: 0x170051FD RID: 20989
		// (get) Token: 0x06010D40 RID: 68928 RVA: 0x003BBEBF File Offset: 0x003BA0BF
		int ICollection.Count
		{
			get
			{
				return this.Count;
			}
		}

		// Token: 0x170051FE RID: 20990
		// (get) Token: 0x06010D41 RID: 68929 RVA: 0x003BBEC7 File Offset: 0x003BA0C7
		bool ICollection.IsSynchronized
		{
			get
			{
				return this.IsSynchronized;
			}
		}

		// Token: 0x170051FF RID: 20991
		// (get) Token: 0x06010D42 RID: 68930 RVA: 0x003BBECF File Offset: 0x003BA0CF
		object ICollection.SyncRoot
		{
			get
			{
				return this.SyncRoot;
			}
		}

		// Token: 0x06010D43 RID: 68931 RVA: 0x003BBED7 File Offset: 0x003BA0D7
		public virtual IEnumerator GetEnumerator()
		{
			return this._list.GetEnumerator();
		}

		// Token: 0x06010D44 RID: 68932 RVA: 0x003BBEE4 File Offset: 0x003BA0E4
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x04004B41 RID: 19265
		private IList _list;
	}
}
