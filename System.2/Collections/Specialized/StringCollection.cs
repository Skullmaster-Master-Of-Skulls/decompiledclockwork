using System;

namespace System.Collections.Specialized
{
	// Token: 0x020003B5 RID: 949
	[Serializable]
	public class StringCollection : IList, ICollection, IEnumerable
	{
		// Token: 0x1700090D RID: 2317
		public string this[int index]
		{
			get
			{
				return (string)this.data[index];
			}
			set
			{
				this.data[index] = value;
			}
		}

		// Token: 0x1700090E RID: 2318
		// (get) Token: 0x060023B9 RID: 9145 RVA: 0x000A8BF3 File Offset: 0x000A6DF3
		public int Count
		{
			get
			{
				return this.data.Count;
			}
		}

		// Token: 0x1700090F RID: 2319
		// (get) Token: 0x060023BA RID: 9146 RVA: 0x000A8C00 File Offset: 0x000A6E00
		bool IList.IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000910 RID: 2320
		// (get) Token: 0x060023BB RID: 9147 RVA: 0x000A8C03 File Offset: 0x000A6E03
		bool IList.IsFixedSize
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060023BC RID: 9148 RVA: 0x000A8C06 File Offset: 0x000A6E06
		public int Add(string value)
		{
			return this.data.Add(value);
		}

		// Token: 0x060023BD RID: 9149 RVA: 0x000A8C14 File Offset: 0x000A6E14
		public void AddRange(string[] value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			this.data.AddRange(value);
		}

		// Token: 0x060023BE RID: 9150 RVA: 0x000A8C30 File Offset: 0x000A6E30
		public void Clear()
		{
			this.data.Clear();
		}

		// Token: 0x060023BF RID: 9151 RVA: 0x000A8C3D File Offset: 0x000A6E3D
		public bool Contains(string value)
		{
			return this.data.Contains(value);
		}

		// Token: 0x060023C0 RID: 9152 RVA: 0x000A8C4B File Offset: 0x000A6E4B
		public void CopyTo(string[] array, int index)
		{
			this.data.CopyTo(array, index);
		}

		// Token: 0x060023C1 RID: 9153 RVA: 0x000A8C5A File Offset: 0x000A6E5A
		public StringEnumerator GetEnumerator()
		{
			return new StringEnumerator(this);
		}

		// Token: 0x060023C2 RID: 9154 RVA: 0x000A8C62 File Offset: 0x000A6E62
		public int IndexOf(string value)
		{
			return this.data.IndexOf(value);
		}

		// Token: 0x060023C3 RID: 9155 RVA: 0x000A8C70 File Offset: 0x000A6E70
		public void Insert(int index, string value)
		{
			this.data.Insert(index, value);
		}

		// Token: 0x17000911 RID: 2321
		// (get) Token: 0x060023C4 RID: 9156 RVA: 0x000A8C7F File Offset: 0x000A6E7F
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000912 RID: 2322
		// (get) Token: 0x060023C5 RID: 9157 RVA: 0x000A8C82 File Offset: 0x000A6E82
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060023C6 RID: 9158 RVA: 0x000A8C85 File Offset: 0x000A6E85
		public void Remove(string value)
		{
			this.data.Remove(value);
		}

		// Token: 0x060023C7 RID: 9159 RVA: 0x000A8C93 File Offset: 0x000A6E93
		public void RemoveAt(int index)
		{
			this.data.RemoveAt(index);
		}

		// Token: 0x17000913 RID: 2323
		// (get) Token: 0x060023C8 RID: 9160 RVA: 0x000A8CA1 File Offset: 0x000A6EA1
		public object SyncRoot
		{
			get
			{
				return this.data.SyncRoot;
			}
		}

		// Token: 0x17000914 RID: 2324
		object IList.this[int index]
		{
			get
			{
				return this[index];
			}
			set
			{
				this[index] = (string)value;
			}
		}

		// Token: 0x060023CB RID: 9163 RVA: 0x000A8CC6 File Offset: 0x000A6EC6
		int IList.Add(object value)
		{
			return this.Add((string)value);
		}

		// Token: 0x060023CC RID: 9164 RVA: 0x000A8CD4 File Offset: 0x000A6ED4
		bool IList.Contains(object value)
		{
			return this.Contains((string)value);
		}

		// Token: 0x060023CD RID: 9165 RVA: 0x000A8CE2 File Offset: 0x000A6EE2
		int IList.IndexOf(object value)
		{
			return this.IndexOf((string)value);
		}

		// Token: 0x060023CE RID: 9166 RVA: 0x000A8CF0 File Offset: 0x000A6EF0
		void IList.Insert(int index, object value)
		{
			this.Insert(index, (string)value);
		}

		// Token: 0x060023CF RID: 9167 RVA: 0x000A8CFF File Offset: 0x000A6EFF
		void IList.Remove(object value)
		{
			this.Remove((string)value);
		}

		// Token: 0x060023D0 RID: 9168 RVA: 0x000A8D0D File Offset: 0x000A6F0D
		void ICollection.CopyTo(Array array, int index)
		{
			this.data.CopyTo(array, index);
		}

		// Token: 0x060023D1 RID: 9169 RVA: 0x000A8D1C File Offset: 0x000A6F1C
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.data.GetEnumerator();
		}

		// Token: 0x04001FF5 RID: 8181
		private ArrayList data = new ArrayList();
	}
}
