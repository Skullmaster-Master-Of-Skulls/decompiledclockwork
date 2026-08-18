using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing.Design;
using System.Security.Permissions;

namespace System.Web.UI.WebControls
{
	// Token: 0x02000657 RID: 1623
	[Editor("System.Web.UI.Design.WebControls.TableCellsCollectionEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public sealed class TableCellCollection : IList, ICollection, IEnumerable
	{
		// Token: 0x06004F79 RID: 20345 RVA: 0x0013F8EF File Offset: 0x0013E8EF
		internal TableCellCollection(TableRow owner)
		{
			this.owner = owner;
		}

		// Token: 0x17001416 RID: 5142
		// (get) Token: 0x06004F7A RID: 20346 RVA: 0x0013F8FE File Offset: 0x0013E8FE
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

		// Token: 0x17001417 RID: 5143
		public TableCell this[int index]
		{
			get
			{
				return (TableCell)this.owner.Controls[index];
			}
		}

		// Token: 0x06004F7C RID: 20348 RVA: 0x0013F937 File Offset: 0x0013E937
		public int Add(TableCell cell)
		{
			this.AddAt(-1, cell);
			return this.owner.Controls.Count - 1;
		}

		// Token: 0x06004F7D RID: 20349 RVA: 0x0013F953 File Offset: 0x0013E953
		public void AddAt(int index, TableCell cell)
		{
			this.owner.Controls.AddAt(index, cell);
		}

		// Token: 0x06004F7E RID: 20350 RVA: 0x0013F968 File Offset: 0x0013E968
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

		// Token: 0x06004F7F RID: 20351 RVA: 0x0013F99F File Offset: 0x0013E99F
		public void Clear()
		{
			if (this.owner.HasControls())
			{
				this.owner.Controls.Clear();
			}
		}

		// Token: 0x06004F80 RID: 20352 RVA: 0x0013F9BE File Offset: 0x0013E9BE
		public int GetCellIndex(TableCell cell)
		{
			if (this.owner.HasControls())
			{
				return this.owner.Controls.IndexOf(cell);
			}
			return -1;
		}

		// Token: 0x06004F81 RID: 20353 RVA: 0x0013F9E0 File Offset: 0x0013E9E0
		public IEnumerator GetEnumerator()
		{
			return this.owner.Controls.GetEnumerator();
		}

		// Token: 0x06004F82 RID: 20354 RVA: 0x0013F9F4 File Offset: 0x0013E9F4
		public void CopyTo(Array array, int index)
		{
			foreach (object value in this)
			{
				array.SetValue(value, index++);
			}
		}

		// Token: 0x17001418 RID: 5144
		// (get) Token: 0x06004F83 RID: 20355 RVA: 0x0013FA24 File Offset: 0x0013EA24
		public object SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x17001419 RID: 5145
		// (get) Token: 0x06004F84 RID: 20356 RVA: 0x0013FA27 File Offset: 0x0013EA27
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700141A RID: 5146
		// (get) Token: 0x06004F85 RID: 20357 RVA: 0x0013FA2A File Offset: 0x0013EA2A
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06004F86 RID: 20358 RVA: 0x0013FA2D File Offset: 0x0013EA2D
		public void Remove(TableCell cell)
		{
			this.owner.Controls.Remove(cell);
		}

		// Token: 0x06004F87 RID: 20359 RVA: 0x0013FA40 File Offset: 0x0013EA40
		public void RemoveAt(int index)
		{
			this.owner.Controls.RemoveAt(index);
		}

		// Token: 0x1700141B RID: 5147
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

		// Token: 0x1700141C RID: 5148
		// (get) Token: 0x06004F8A RID: 20362 RVA: 0x0013FA7C File Offset: 0x0013EA7C
		bool IList.IsFixedSize
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06004F8B RID: 20363 RVA: 0x0013FA7F File Offset: 0x0013EA7F
		int IList.Add(object o)
		{
			return this.Add((TableCell)o);
		}

		// Token: 0x06004F8C RID: 20364 RVA: 0x0013FA8D File Offset: 0x0013EA8D
		bool IList.Contains(object o)
		{
			return this.owner.Controls.Contains((TableCell)o);
		}

		// Token: 0x06004F8D RID: 20365 RVA: 0x0013FAA5 File Offset: 0x0013EAA5
		int IList.IndexOf(object o)
		{
			return this.owner.Controls.IndexOf((TableCell)o);
		}

		// Token: 0x06004F8E RID: 20366 RVA: 0x0013FABD File Offset: 0x0013EABD
		void IList.Insert(int index, object o)
		{
			this.owner.Controls.AddAt(index, (TableCell)o);
		}

		// Token: 0x06004F8F RID: 20367 RVA: 0x0013FAD6 File Offset: 0x0013EAD6
		void IList.Remove(object o)
		{
			this.owner.Controls.Remove((TableCell)o);
		}

		// Token: 0x04002CE9 RID: 11497
		private TableRow owner;
	}
}
