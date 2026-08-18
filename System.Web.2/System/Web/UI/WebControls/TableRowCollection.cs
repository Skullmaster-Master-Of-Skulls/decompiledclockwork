using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing.Design;

namespace System.Web.UI.WebControls
{
	// Token: 0x020004ED RID: 1261
	[Editor("System.Web.UI.Design.WebControls.TableRowsCollectionEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
	public sealed class TableRowCollection : IList, ICollection, IEnumerable
	{
		// Token: 0x06003EDA RID: 16090 RVA: 0x000CA33D File Offset: 0x000C853D
		internal TableRowCollection(Table owner)
		{
			this.owner = owner;
		}

		// Token: 0x17001255 RID: 4693
		// (get) Token: 0x06003EDB RID: 16091 RVA: 0x000CA34C File Offset: 0x000C854C
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

		// Token: 0x17001256 RID: 4694
		public TableRow this[int index]
		{
			get
			{
				return (TableRow)this.owner.Controls[index];
			}
		}

		// Token: 0x06003EDD RID: 16093 RVA: 0x000CA385 File Offset: 0x000C8585
		public int Add(TableRow row)
		{
			this.AddAt(-1, row);
			return this.owner.Controls.Count - 1;
		}

		// Token: 0x06003EDE RID: 16094 RVA: 0x000CA3A1 File Offset: 0x000C85A1
		public void AddAt(int index, TableRow row)
		{
			this.owner.Controls.AddAt(index, row);
			if (row.TableSection != TableRowSection.TableBody)
			{
				this.owner.HasRowSections = true;
			}
		}

		// Token: 0x06003EDF RID: 16095 RVA: 0x000CA3CC File Offset: 0x000C85CC
		public void AddRange(TableRow[] rows)
		{
			if (rows == null)
			{
				throw new ArgumentNullException("rows");
			}
			foreach (TableRow row in rows)
			{
				this.Add(row);
			}
		}

		// Token: 0x06003EE0 RID: 16096 RVA: 0x000CA403 File Offset: 0x000C8603
		public void Clear()
		{
			if (this.owner.HasControls())
			{
				this.owner.Controls.Clear();
				this.owner.HasRowSections = false;
			}
		}

		// Token: 0x06003EE1 RID: 16097 RVA: 0x000CA42E File Offset: 0x000C862E
		public int GetRowIndex(TableRow row)
		{
			if (this.owner.HasControls())
			{
				return this.owner.Controls.IndexOf(row);
			}
			return -1;
		}

		// Token: 0x06003EE2 RID: 16098 RVA: 0x000CA450 File Offset: 0x000C8650
		public IEnumerator GetEnumerator()
		{
			return this.owner.Controls.GetEnumerator();
		}

		// Token: 0x06003EE3 RID: 16099 RVA: 0x000CA464 File Offset: 0x000C8664
		public void CopyTo(Array array, int index)
		{
			foreach (object value in this)
			{
				array.SetValue(value, index++);
			}
		}

		// Token: 0x17001257 RID: 4695
		// (get) Token: 0x06003EE4 RID: 16100 RVA: 0x00004335 File Offset: 0x00002535
		public object SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x17001258 RID: 4696
		// (get) Token: 0x06003EE5 RID: 16101 RVA: 0x00007722 File Offset: 0x00005922
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001259 RID: 4697
		// (get) Token: 0x06003EE6 RID: 16102 RVA: 0x00007722 File Offset: 0x00005922
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06003EE7 RID: 16103 RVA: 0x000CA494 File Offset: 0x000C8694
		public void Remove(TableRow row)
		{
			this.owner.Controls.Remove(row);
		}

		// Token: 0x06003EE8 RID: 16104 RVA: 0x000CA4A7 File Offset: 0x000C86A7
		public void RemoveAt(int index)
		{
			this.owner.Controls.RemoveAt(index);
		}

		// Token: 0x1700125A RID: 4698
		object IList.this[int index]
		{
			get
			{
				return this.owner.Controls[index];
			}
			set
			{
				this.RemoveAt(index);
				this.AddAt(index, (TableRow)value);
			}
		}

		// Token: 0x1700125B RID: 4699
		// (get) Token: 0x06003EEB RID: 16107 RVA: 0x00007722 File Offset: 0x00005922
		bool IList.IsFixedSize
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06003EEC RID: 16108 RVA: 0x000CA4E3 File Offset: 0x000C86E3
		int IList.Add(object o)
		{
			return this.Add((TableRow)o);
		}

		// Token: 0x06003EED RID: 16109 RVA: 0x000CA4F1 File Offset: 0x000C86F1
		bool IList.Contains(object o)
		{
			return this.owner.Controls.Contains((TableRow)o);
		}

		// Token: 0x06003EEE RID: 16110 RVA: 0x000CA509 File Offset: 0x000C8709
		int IList.IndexOf(object o)
		{
			return this.owner.Controls.IndexOf((TableRow)o);
		}

		// Token: 0x06003EEF RID: 16111 RVA: 0x000CA521 File Offset: 0x000C8721
		void IList.Insert(int index, object o)
		{
			this.AddAt(index, (TableRow)o);
		}

		// Token: 0x06003EF0 RID: 16112 RVA: 0x000CA530 File Offset: 0x000C8730
		void IList.Remove(object o)
		{
			this.Remove((TableRow)o);
		}

		// Token: 0x0400241D RID: 9245
		private Table owner;
	}
}
