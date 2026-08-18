using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing.Design;
using System.Security.Permissions;

namespace System.Web.UI.WebControls
{
	// Token: 0x0200065C RID: 1628
	[Editor("System.Web.UI.Design.WebControls.TableRowsCollectionEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public sealed class TableRowCollection : IList, ICollection, IEnumerable
	{
		// Token: 0x06004F9A RID: 20378 RVA: 0x0013FCB3 File Offset: 0x0013ECB3
		internal TableRowCollection(Table owner)
		{
			this.owner = owner;
		}

		// Token: 0x17001420 RID: 5152
		// (get) Token: 0x06004F9B RID: 20379 RVA: 0x0013FCC2 File Offset: 0x0013ECC2
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

		// Token: 0x17001421 RID: 5153
		public TableRow this[int index]
		{
			get
			{
				return (TableRow)this.owner.Controls[index];
			}
		}

		// Token: 0x06004F9D RID: 20381 RVA: 0x0013FCFB File Offset: 0x0013ECFB
		public int Add(TableRow row)
		{
			this.AddAt(-1, row);
			return this.owner.Controls.Count - 1;
		}

		// Token: 0x06004F9E RID: 20382 RVA: 0x0013FD17 File Offset: 0x0013ED17
		public void AddAt(int index, TableRow row)
		{
			this.owner.Controls.AddAt(index, row);
			if (row.TableSection != TableRowSection.TableBody)
			{
				this.owner.HasRowSections = true;
			}
		}

		// Token: 0x06004F9F RID: 20383 RVA: 0x0013FD40 File Offset: 0x0013ED40
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

		// Token: 0x06004FA0 RID: 20384 RVA: 0x0013FD77 File Offset: 0x0013ED77
		public void Clear()
		{
			if (this.owner.HasControls())
			{
				this.owner.Controls.Clear();
				this.owner.HasRowSections = false;
			}
		}

		// Token: 0x06004FA1 RID: 20385 RVA: 0x0013FDA2 File Offset: 0x0013EDA2
		public int GetRowIndex(TableRow row)
		{
			if (this.owner.HasControls())
			{
				return this.owner.Controls.IndexOf(row);
			}
			return -1;
		}

		// Token: 0x06004FA2 RID: 20386 RVA: 0x0013FDC4 File Offset: 0x0013EDC4
		public IEnumerator GetEnumerator()
		{
			return this.owner.Controls.GetEnumerator();
		}

		// Token: 0x06004FA3 RID: 20387 RVA: 0x0013FDD8 File Offset: 0x0013EDD8
		public void CopyTo(Array array, int index)
		{
			foreach (object value in this)
			{
				array.SetValue(value, index++);
			}
		}

		// Token: 0x17001422 RID: 5154
		// (get) Token: 0x06004FA4 RID: 20388 RVA: 0x0013FE08 File Offset: 0x0013EE08
		public object SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x17001423 RID: 5155
		// (get) Token: 0x06004FA5 RID: 20389 RVA: 0x0013FE0B File Offset: 0x0013EE0B
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001424 RID: 5156
		// (get) Token: 0x06004FA6 RID: 20390 RVA: 0x0013FE0E File Offset: 0x0013EE0E
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06004FA7 RID: 20391 RVA: 0x0013FE11 File Offset: 0x0013EE11
		public void Remove(TableRow row)
		{
			this.owner.Controls.Remove(row);
		}

		// Token: 0x06004FA8 RID: 20392 RVA: 0x0013FE24 File Offset: 0x0013EE24
		public void RemoveAt(int index)
		{
			this.owner.Controls.RemoveAt(index);
		}

		// Token: 0x17001425 RID: 5157
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

		// Token: 0x17001426 RID: 5158
		// (get) Token: 0x06004FAB RID: 20395 RVA: 0x0013FE60 File Offset: 0x0013EE60
		bool IList.IsFixedSize
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06004FAC RID: 20396 RVA: 0x0013FE63 File Offset: 0x0013EE63
		int IList.Add(object o)
		{
			return this.Add((TableRow)o);
		}

		// Token: 0x06004FAD RID: 20397 RVA: 0x0013FE71 File Offset: 0x0013EE71
		bool IList.Contains(object o)
		{
			return this.owner.Controls.Contains((TableRow)o);
		}

		// Token: 0x06004FAE RID: 20398 RVA: 0x0013FE89 File Offset: 0x0013EE89
		int IList.IndexOf(object o)
		{
			return this.owner.Controls.IndexOf((TableRow)o);
		}

		// Token: 0x06004FAF RID: 20399 RVA: 0x0013FEA1 File Offset: 0x0013EEA1
		void IList.Insert(int index, object o)
		{
			this.AddAt(index, (TableRow)o);
		}

		// Token: 0x06004FB0 RID: 20400 RVA: 0x0013FEB0 File Offset: 0x0013EEB0
		void IList.Remove(object o)
		{
			this.Remove((TableRow)o);
		}

		// Token: 0x04002CEE RID: 11502
		private Table owner;
	}
}
