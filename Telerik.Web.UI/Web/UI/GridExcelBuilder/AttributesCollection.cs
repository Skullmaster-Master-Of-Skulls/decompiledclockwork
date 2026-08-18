using System;
using System.Collections;
using Telerik.Web.UI.GridExcelBuilder.Abstract;

namespace Telerik.Web.UI.GridExcelBuilder
{
	// Token: 0x02001B07 RID: 6919
	public class AttributesCollection : IAttributesCollection, IDictionary, ICollection, IEnumerable
	{
		// Token: 0x06010BB6 RID: 68534 RVA: 0x003B89C4 File Offset: 0x003B6BC4
		public AttributesCollection(IDictionary dictionary)
		{
			this._table = new Hashtable(dictionary);
		}

		// Token: 0x06010BB7 RID: 68535 RVA: 0x003B89D8 File Offset: 0x003B6BD8
		public AttributesCollection() : this(new Hashtable())
		{
		}

		// Token: 0x06010BB8 RID: 68536 RVA: 0x003B89E5 File Offset: 0x003B6BE5
		public void Add(string key, string value)
		{
			if (this._table.Contains(key))
			{
				this._table[key] = value;
				return;
			}
			this._table.Add(key, value);
		}

		// Token: 0x06010BB9 RID: 68537 RVA: 0x003B8A10 File Offset: 0x003B6C10
		public void Clear()
		{
			this._table.Clear();
		}

		// Token: 0x06010BBA RID: 68538 RVA: 0x003B8A1D File Offset: 0x003B6C1D
		public bool Contains(string key)
		{
			return this._table.Contains(key);
		}

		// Token: 0x06010BBB RID: 68539 RVA: 0x003B8A2B File Offset: 0x003B6C2B
		public IDictionaryEnumerator GetEnumerator()
		{
			return this._table.GetEnumerator();
		}

		// Token: 0x17005160 RID: 20832
		// (get) Token: 0x06010BBC RID: 68540 RVA: 0x003B8A38 File Offset: 0x003B6C38
		public bool IsFixedSize
		{
			get
			{
				return this._table.IsFixedSize;
			}
		}

		// Token: 0x17005161 RID: 20833
		// (get) Token: 0x06010BBD RID: 68541 RVA: 0x003B8A45 File Offset: 0x003B6C45
		public bool IsReadOnly
		{
			get
			{
				return this._table.IsReadOnly;
			}
		}

		// Token: 0x17005162 RID: 20834
		// (get) Token: 0x06010BBE RID: 68542 RVA: 0x003B8A52 File Offset: 0x003B6C52
		public ICollection Keys
		{
			get
			{
				return this._table.Keys;
			}
		}

		// Token: 0x06010BBF RID: 68543 RVA: 0x003B8A5F File Offset: 0x003B6C5F
		public void Remove(string key)
		{
			this._table.Remove(key);
		}

		// Token: 0x17005163 RID: 20835
		// (get) Token: 0x06010BC0 RID: 68544 RVA: 0x003B8A6D File Offset: 0x003B6C6D
		public ICollection Values
		{
			get
			{
				return this._table.Values;
			}
		}

		// Token: 0x17005164 RID: 20836
		public string this[string key]
		{
			get
			{
				return this._table[key] as string;
			}
			set
			{
				this._table[key] = value;
			}
		}

		// Token: 0x06010BC3 RID: 68547 RVA: 0x003B8A9C File Offset: 0x003B6C9C
		void IDictionary.Add(object key, object value)
		{
			this.Add((string)key, (string)value);
		}

		// Token: 0x06010BC4 RID: 68548 RVA: 0x003B8AB0 File Offset: 0x003B6CB0
		void IDictionary.Clear()
		{
			this.Clear();
		}

		// Token: 0x06010BC5 RID: 68549 RVA: 0x003B8AB8 File Offset: 0x003B6CB8
		bool IDictionary.Contains(object key)
		{
			return this.Contains((string)key);
		}

		// Token: 0x06010BC6 RID: 68550 RVA: 0x003B8AC6 File Offset: 0x003B6CC6
		IDictionaryEnumerator IDictionary.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x17005165 RID: 20837
		// (get) Token: 0x06010BC7 RID: 68551 RVA: 0x003B8ACE File Offset: 0x003B6CCE
		bool IDictionary.IsFixedSize
		{
			get
			{
				return this.IsFixedSize;
			}
		}

		// Token: 0x17005166 RID: 20838
		// (get) Token: 0x06010BC8 RID: 68552 RVA: 0x003B8AD6 File Offset: 0x003B6CD6
		bool IDictionary.IsReadOnly
		{
			get
			{
				return this.IsReadOnly;
			}
		}

		// Token: 0x17005167 RID: 20839
		// (get) Token: 0x06010BC9 RID: 68553 RVA: 0x003B8ADE File Offset: 0x003B6CDE
		ICollection IDictionary.Keys
		{
			get
			{
				return this.Keys;
			}
		}

		// Token: 0x06010BCA RID: 68554 RVA: 0x003B8AE6 File Offset: 0x003B6CE6
		void IDictionary.Remove(object key)
		{
			this.Remove((string)key);
		}

		// Token: 0x17005168 RID: 20840
		// (get) Token: 0x06010BCB RID: 68555 RVA: 0x003B8AF4 File Offset: 0x003B6CF4
		ICollection IDictionary.Values
		{
			get
			{
				return this.Values;
			}
		}

		// Token: 0x17005169 RID: 20841
		object IDictionary.this[object key]
		{
			get
			{
				return this[(string)key];
			}
			set
			{
				this[(string)key] = (string)value;
			}
		}

		// Token: 0x06010BCE RID: 68558 RVA: 0x003B8B1E File Offset: 0x003B6D1E
		public void CopyTo(string[] array, int index)
		{
			this._table.CopyTo(array, index);
		}

		// Token: 0x1700516A RID: 20842
		// (get) Token: 0x06010BCF RID: 68559 RVA: 0x003B8B2D File Offset: 0x003B6D2D
		public int Count
		{
			get
			{
				return this._table.Count;
			}
		}

		// Token: 0x1700516B RID: 20843
		// (get) Token: 0x06010BD0 RID: 68560 RVA: 0x003B8B3A File Offset: 0x003B6D3A
		public bool IsSynchronized
		{
			get
			{
				return this._table.IsSynchronized;
			}
		}

		// Token: 0x1700516C RID: 20844
		// (get) Token: 0x06010BD1 RID: 68561 RVA: 0x003B8B47 File Offset: 0x003B6D47
		public object SyncRoot
		{
			get
			{
				return this._table.SyncRoot;
			}
		}

		// Token: 0x06010BD2 RID: 68562 RVA: 0x003B8B54 File Offset: 0x003B6D54
		void ICollection.CopyTo(Array array, int index)
		{
			this.CopyTo((string[])array, index);
		}

		// Token: 0x1700516D RID: 20845
		// (get) Token: 0x06010BD3 RID: 68563 RVA: 0x003B8B63 File Offset: 0x003B6D63
		int ICollection.Count
		{
			get
			{
				return this.Count;
			}
		}

		// Token: 0x1700516E RID: 20846
		// (get) Token: 0x06010BD4 RID: 68564 RVA: 0x003B8B6B File Offset: 0x003B6D6B
		bool ICollection.IsSynchronized
		{
			get
			{
				return this.IsSynchronized;
			}
		}

		// Token: 0x1700516F RID: 20847
		// (get) Token: 0x06010BD5 RID: 68565 RVA: 0x003B8B73 File Offset: 0x003B6D73
		object ICollection.SyncRoot
		{
			get
			{
				return this.SyncRoot;
			}
		}

		// Token: 0x06010BD6 RID: 68566 RVA: 0x003B8B7B File Offset: 0x003B6D7B
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this._table.GetEnumerator();
		}

		// Token: 0x04004AA7 RID: 19111
		private IDictionary _table;
	}
}
