using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing.Design;

namespace System.Web.UI.WebControls
{
	// Token: 0x020004E6 RID: 1254
	[Editor("System.Web.UI.Design.WebControls.TableCellsCollectionEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
	public sealed class TableCellCollection : IList, ICollection, IEnumerable
	{
		// Token: 0x06003E9E RID: 16030 RVA: 0x000C9B10 File Offset: 0x000C7D10
		internal TableCellCollection(TableRow owner)
		{
			this.owner = owner;
		}

		// Token: 0x17001242 RID: 4674
		// (get) Token: 0x06003E9F RID: 16031 RVA: 0x000C9B1F File Offset: 0x000C7D1F
		public int Count
		{
			get
			{
				if (this.owner.HasControls())
				{
					return this.owner.Controls.Count;
				}
				return 0;
			}
		}

		// Token: 0x17001243 RID: 4675
		public TableCell this[int index]
		{
			get
			{
				return (TableCell)this.owner.Controls[index];
			}
		}

		// Token: 0x06003EA1 RID: 16033 RVA: 0x000C9B58 File Offset: 0x000C7D58
		public int Add(TableCell cell)
		{
			this.AddAt(-1, cell);
			return this.owner.Controls.Count - 1;
		}

		// Token: 0x06003EA2 RID: 16034 RVA: 0x000C9B74 File Offset: 0x000C7D74
		public void AddAt(int index, TableCell cell)
		{
			this.owner.Controls.AddAt(index, cell);
		}

		// Token: 0x06003EA3 RID: 16035 RVA: 0x000C9B88 File Offset: 0x000C7D88
		public void AddRange(TableCell[] cells)
		{
			if (cells == null)
			{
				throw new ArgumentNullException("cells");
			}
			foreach (TableCell cell in cells)
			{
				this.Add(cell);
			}
		}

		// Token: 0x06003EA4 RID: 16036 RVA: 0x000C9BBF File Offset: 0x000C7DBF
		public void Clear()
		{
			if (this.owner.HasControls())
			{
				this.owner.Controls.Clear();
			}
		}

		// Token: 0x06003EA5 RID: 16037 RVA: 0x000C9BDE File Offset: 0x000C7DDE
		public int GetCellIndex(TableCell cell)
		{
			if (this.owner.HasControls())
			{
				return this.owner.Controls.IndexOf(cell);
			}
			return -1;
		}

		// Token: 0x06003EA6 RID: 16038 RVA: 0x000C9C00 File Offset: 0x000C7E00
		public IEnumerator GetEnumerator()
		{
			return this.owner.Controls.GetEnumerator();
		}

		// Token: 0x06003EA7 RID: 16039 RVA: 0x000C9C14 File Offset: 0x000C7E14
		public void CopyTo(Array array, int index)
		{
			foreach (object value in this)
			{
				array.SetValue(value, index++);
			}
		}

		// Token: 0x17001244 RID: 4676
		// (get) Token: 0x06003EA8 RID: 16040 RVA: 0x00004335 File Offset: 0x00002535
		public object SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x17001245 RID: 4677
		// (get) Token: 0x06003EA9 RID: 16041 RVA: 0x00007722 File Offset: 0x00005922
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001246 RID: 4678
		// (get) Token: 0x06003EAA RID: 16042 RVA: 0x00007722 File Offset: 0x00005922
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06003EAB RID: 16043 RVA: 0x000C9C44 File Offset: 0x000C7E44
		public void Remove(TableCell cell)
		{
			this.owner.Controls.Remove(cell);
		}

		// Token: 0x06003EAC RID: 16044 RVA: 0x000C9C57 File Offset: 0x000C7E57
		public void RemoveAt(int index)
		{
			this.owner.Controls.RemoveAt(index);
		}

		// Token: 0x17001247 RID: 4679
		object IList.this[int index]
		{
			get
			{
				return this.owner.Controls[index];
			}
			set
			{
				this.RemoveAt(index);
				this.AddAt(index, (TableCell)value);
			}
		}

		// Token: 0x17001248 RID: 4680
		// (get) Token: 0x06003EAF RID: 16047 RVA: 0x00007722 File Offset: 0x00005922
		bool IList.IsFixedSize
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06003EB0 RID: 16048 RVA: 0x000C9C93 File Offset: 0x000C7E93
		int IList.Add(object o)
		{
			return this.Add((TableCell)o);
		}

		// Token: 0x06003EB1 RID: 16049 RVA: 0x000C9CA1 File Offset: 0x000C7EA1
		bool IList.Contains(object o)
		{
			return this.owner.Controls.Contains((TableCell)o);
		}

		// Token: 0x06003EB2 RID: 16050 RVA: 0x000C9CB9 File Offset: 0x000C7EB9
		int IList.IndexOf(object o)
		{
			return this.owner.Controls.IndexOf((TableCell)o);
		}

		// Token: 0x06003EB3 RID: 16051 RVA: 0x000C9CD1 File Offset: 0x000C7ED1
		void IList.Insert(int index, object o)
		{
			this.owner.Controls.AddAt(index, (TableCell)o);
		}

		// Token: 0x06003EB4 RID: 16052 RVA: 0x000C9CEA File Offset: 0x000C7EEA
		void IList.Remove(object o)
		{
			this.owner.Controls.Remove((TableCell)o);
		}

		// Token: 0x04002414 RID: 9236
		private TableRow owner;
	}
}
