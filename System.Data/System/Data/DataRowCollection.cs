using System;
using System.Collections;

namespace System.Data
{
	// Token: 0x02000086 RID: 134
	public sealed class DataRowCollection : InternalDataCollectionBase
	{
		// Token: 0x060007CA RID: 1994 RVA: 0x001F5598 File Offset: 0x001F4998
		internal DataRowCollection(DataTable table)
		{
			this.table = table;
		}

		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x060007CB RID: 1995 RVA: 0x001F55C8 File Offset: 0x001F49C8
		public override int Count
		{
			get
			{
				return this.list.Count;
			}
		}

		// Token: 0x170000F7 RID: 247
		public DataRow this[int index]
		{
			get
			{
				return this.list[index];
			}
		}

		// Token: 0x060007CD RID: 1997 RVA: 0x001F5608 File Offset: 0x001F4A08
		public void Add(DataRow row)
		{
			this.table.AddRow(row, -1);
		}

		// Token: 0x060007CE RID: 1998 RVA: 0x001F5628 File Offset: 0x001F4A28
		public void InsertAt(DataRow row, int pos)
		{
			if (pos < 0)
			{
				throw ExceptionBuilder.RowInsertOutOfRange(pos);
			}
			if (pos >= this.list.Count)
			{
				this.table.AddRow(row, -1);
				return;
			}
			this.table.InsertRow(row, -1, pos);
		}

		// Token: 0x060007CF RID: 1999 RVA: 0x001F5678 File Offset: 0x001F4A78
		internal void DiffInsertAt(DataRow row, int pos)
		{
			if (pos < 0 || pos == this.list.Count)
			{
				this.table.AddRow(row, (pos > -1) ? (pos + 1) : -1);
				return;
			}
			if (this.table.NestedParentRelations.Length <= 0)
			{
				this.table.InsertRow(row, pos + 1, (pos > this.list.Count) ? -1 : pos);
				return;
			}
			if (pos >= this.list.Count)
			{
				while (pos > this.list.Count)
				{
					this.list.Add(null);
					this.nullInList++;
				}
				this.table.AddRow(row, pos + 1);
				return;
			}
			if (this.list[pos] != null)
			{
				throw ExceptionBuilder.RowInsertTwice(pos, this.table.TableName);
			}
			this.list.RemoveAt(pos);
			this.nullInList--;
			this.table.InsertRow(row, pos + 1, pos);
		}

		// Token: 0x060007D0 RID: 2000 RVA: 0x001F5778 File Offset: 0x001F4B78
		public int IndexOf(DataRow row)
		{
			if (row == null || row.Table != this.table || (row.RBTreeNodeId == 0 && row.RowState == DataRowState.Detached))
			{
				return -1;
			}
			return this.list.IndexOf(row.RBTreeNodeId, row);
		}

		// Token: 0x060007D1 RID: 2001 RVA: 0x001F57C8 File Offset: 0x001F4BC8
		internal DataRow AddWithColumnEvents(params object[] values)
		{
			DataRow dataRow = this.table.NewRow(-1);
			dataRow.ItemArray = values;
			this.table.AddRow(dataRow, -1);
			return dataRow;
		}

		// Token: 0x060007D2 RID: 2002 RVA: 0x001F57F8 File Offset: 0x001F4BF8
		public DataRow Add(params object[] values)
		{
			int record = this.table.NewRecordFromArray(values);
			DataRow dataRow = this.table.NewRow(record);
			this.table.AddRow(dataRow, -1);
			return dataRow;
		}

		// Token: 0x060007D3 RID: 2003 RVA: 0x001F5838 File Offset: 0x001F4C38
		internal void ArrayAdd(DataRow row)
		{
			row.RBTreeNodeId = this.list.Add(row);
		}

		// Token: 0x060007D4 RID: 2004 RVA: 0x001F5858 File Offset: 0x001F4C58
		internal void ArrayInsert(DataRow row, int pos)
		{
			row.RBTreeNodeId = this.list.Insert(pos, row);
		}

		// Token: 0x060007D5 RID: 2005 RVA: 0x001F5878 File Offset: 0x001F4C78
		internal void ArrayClear()
		{
			this.list.Clear();
		}

		// Token: 0x060007D6 RID: 2006 RVA: 0x001F5898 File Offset: 0x001F4C98
		internal void ArrayRemove(DataRow row)
		{
			if (row.RBTreeNodeId == 0)
			{
				throw ExceptionBuilder.InternalRBTreeError(RBTreeError.AttachedNodeWithZerorbTreeNodeId);
			}
			this.list.RBDelete(row.RBTreeNodeId);
			row.RBTreeNodeId = 0;
		}

		// Token: 0x060007D7 RID: 2007 RVA: 0x001F58D8 File Offset: 0x001F4CD8
		public DataRow Find(object key)
		{
			return this.table.FindByPrimaryKey(key);
		}

		// Token: 0x060007D8 RID: 2008 RVA: 0x001F58F8 File Offset: 0x001F4CF8
		public DataRow Find(object[] keys)
		{
			return this.table.FindByPrimaryKey(keys);
		}

		// Token: 0x060007D9 RID: 2009 RVA: 0x001F5918 File Offset: 0x001F4D18
		public void Clear()
		{
			this.table.Clear(false);
		}

		// Token: 0x060007DA RID: 2010 RVA: 0x001F5938 File Offset: 0x001F4D38
		public bool Contains(object key)
		{
			return this.table.FindByPrimaryKey(key) != null;
		}

		// Token: 0x060007DB RID: 2011 RVA: 0x001F5958 File Offset: 0x001F4D58
		public bool Contains(object[] keys)
		{
			return this.table.FindByPrimaryKey(keys) != null;
		}

		// Token: 0x060007DC RID: 2012 RVA: 0x001F5978 File Offset: 0x001F4D78
		public override void CopyTo(Array ar, int index)
		{
			this.list.CopyTo(ar, index);
		}

		// Token: 0x060007DD RID: 2013 RVA: 0x001F5998 File Offset: 0x001F4D98
		public void CopyTo(DataRow[] array, int index)
		{
			this.list.CopyTo(array, index);
		}

		// Token: 0x060007DE RID: 2014 RVA: 0x001F59B8 File Offset: 0x001F4DB8
		public override IEnumerator GetEnumerator()
		{
			return this.list.GetEnumerator();
		}

		// Token: 0x060007DF RID: 2015 RVA: 0x001F59D8 File Offset: 0x001F4DD8
		public void Remove(DataRow row)
		{
			if (row == null || row.Table != this.table || -1L == row.rowID)
			{
				throw ExceptionBuilder.RowOutOfRange();
			}
			if (row.RowState != DataRowState.Deleted && row.RowState != DataRowState.Detached)
			{
				row.Delete();
			}
			if (row.RowState != DataRowState.Detached)
			{
				row.AcceptChanges();
			}
		}

		// Token: 0x060007E0 RID: 2016 RVA: 0x001F5A38 File Offset: 0x001F4E38
		public void RemoveAt(int index)
		{
			this.Remove(this[index]);
		}

		// Token: 0x04000762 RID: 1890
		private readonly DataTable table;

		// Token: 0x04000763 RID: 1891
		private readonly DataRowCollection.DataRowTree list = new DataRowCollection.DataRowTree();

		// Token: 0x04000764 RID: 1892
		internal int nullInList;

		// Token: 0x0200008D RID: 141
		private sealed class DataRowTree : RBTree<DataRow>
		{
			// Token: 0x06000830 RID: 2096 RVA: 0x001F7E08 File Offset: 0x001F7208
			internal DataRowTree() : base(TreeAccessMethod.INDEX_ONLY)
			{
			}

			// Token: 0x06000831 RID: 2097 RVA: 0x001F7E28 File Offset: 0x001F7228
			protected override int CompareNode(DataRow record1, DataRow record2)
			{
				throw ExceptionBuilder.InternalRBTreeError(RBTreeError.CompareNodeInDataRowTree);
			}

			// Token: 0x06000832 RID: 2098 RVA: 0x001F7E48 File Offset: 0x001F7248
			protected override int CompareSateliteTreeNode(DataRow record1, DataRow record2)
			{
				throw ExceptionBuilder.InternalRBTreeError(RBTreeError.CompareSateliteTreeNodeInDataRowTree);
			}
		}
	}
}
