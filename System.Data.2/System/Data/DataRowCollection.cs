using System;
using System.Collections;

namespace System.Data
{
	// Token: 0x020000C2 RID: 194
	public sealed class DataRowCollection : InternalDataCollectionBase
	{
		// Token: 0x06000B57 RID: 2903 RVA: 0x00062D80 File Offset: 0x00062180
		internal DataRowCollection(DataTable table)
		{
			this.table = table;
		}

		// Token: 0x1700019E RID: 414
		// (get) Token: 0x06000B58 RID: 2904 RVA: 0x00062DA8 File Offset: 0x000621A8
		public override int Count
		{
			get
			{
				return this.list.Count;
			}
		}

		// Token: 0x1700019F RID: 415
		public DataRow this[int index]
		{
			get
			{
				return this.list[index];
			}
		}

		// Token: 0x06000B5A RID: 2906 RVA: 0x00062DDC File Offset: 0x000621DC
		public void Add(DataRow row)
		{
			this.table.AddRow(row, -1);
		}

		// Token: 0x06000B5B RID: 2907 RVA: 0x00062DF8 File Offset: 0x000621F8
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

		// Token: 0x06000B5C RID: 2908 RVA: 0x00062E3C File Offset: 0x0006223C
		internal void DiffInsertAt(DataRow row, int pos)
		{
			if (pos < 0 || pos == this.list.Count)
			{
				this.table.AddRow(row, (pos > -1) ? (pos + 1) : -1);
				return;
			}
			if (this.table.NestedParentRelations.Length == 0)
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

		// Token: 0x06000B5D RID: 2909 RVA: 0x00062F38 File Offset: 0x00062338
		public int IndexOf(DataRow row)
		{
			if (row == null || row.Table != this.table || (row.RBTreeNodeId == 0 && row.RowState == DataRowState.Detached))
			{
				return -1;
			}
			return this.list.IndexOf(row.RBTreeNodeId, row);
		}

		// Token: 0x06000B5E RID: 2910 RVA: 0x00062F7C File Offset: 0x0006237C
		internal DataRow AddWithColumnEvents(params object[] values)
		{
			DataRow dataRow = this.table.NewRow(-1);
			dataRow.ItemArray = values;
			this.table.AddRow(dataRow, -1);
			return dataRow;
		}

		// Token: 0x06000B5F RID: 2911 RVA: 0x00062FAC File Offset: 0x000623AC
		public DataRow Add(params object[] values)
		{
			int record = this.table.NewRecordFromArray(values);
			DataRow dataRow = this.table.NewRow(record);
			this.table.AddRow(dataRow, -1);
			return dataRow;
		}

		// Token: 0x06000B60 RID: 2912 RVA: 0x00062FE4 File Offset: 0x000623E4
		internal void ArrayAdd(DataRow row)
		{
			row.RBTreeNodeId = this.list.Add(row);
		}

		// Token: 0x06000B61 RID: 2913 RVA: 0x00063004 File Offset: 0x00062404
		internal void ArrayInsert(DataRow row, int pos)
		{
			row.RBTreeNodeId = this.list.Insert(pos, row);
		}

		// Token: 0x06000B62 RID: 2914 RVA: 0x00063024 File Offset: 0x00062424
		internal void ArrayClear()
		{
			this.list.Clear();
		}

		// Token: 0x06000B63 RID: 2915 RVA: 0x0006303C File Offset: 0x0006243C
		internal void ArrayRemove(DataRow row)
		{
			if (row.RBTreeNodeId == 0)
			{
				throw ExceptionBuilder.InternalRBTreeError(RBTreeError.AttachedNodeWithZerorbTreeNodeId);
			}
			this.list.RBDelete(row.RBTreeNodeId);
			row.RBTreeNodeId = 0;
		}

		// Token: 0x06000B64 RID: 2916 RVA: 0x00063074 File Offset: 0x00062474
		public DataRow Find(object key)
		{
			return this.table.FindByPrimaryKey(key);
		}

		// Token: 0x06000B65 RID: 2917 RVA: 0x00063090 File Offset: 0x00062490
		public DataRow Find(object[] keys)
		{
			return this.table.FindByPrimaryKey(keys);
		}

		// Token: 0x06000B66 RID: 2918 RVA: 0x000630AC File Offset: 0x000624AC
		public void Clear()
		{
			this.table.Clear(false);
		}

		// Token: 0x06000B67 RID: 2919 RVA: 0x000630C8 File Offset: 0x000624C8
		public bool Contains(object key)
		{
			return this.table.FindByPrimaryKey(key) != null;
		}

		// Token: 0x06000B68 RID: 2920 RVA: 0x000630E4 File Offset: 0x000624E4
		public bool Contains(object[] keys)
		{
			return this.table.FindByPrimaryKey(keys) != null;
		}

		// Token: 0x06000B69 RID: 2921 RVA: 0x00063100 File Offset: 0x00062500
		public override void CopyTo(Array ar, int index)
		{
			this.list.CopyTo(ar, index);
		}

		// Token: 0x06000B6A RID: 2922 RVA: 0x0006311C File Offset: 0x0006251C
		public void CopyTo(DataRow[] array, int index)
		{
			this.list.CopyTo(array, index);
		}

		// Token: 0x06000B6B RID: 2923 RVA: 0x00063138 File Offset: 0x00062538
		public override IEnumerator GetEnumerator()
		{
			return this.list.GetEnumerator();
		}

		// Token: 0x06000B6C RID: 2924 RVA: 0x00063150 File Offset: 0x00062550
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

		// Token: 0x06000B6D RID: 2925 RVA: 0x000631A8 File Offset: 0x000625A8
		public void RemoveAt(int index)
		{
			this.Remove(this[index]);
		}

		// Token: 0x04000365 RID: 869
		private readonly DataTable table;

		// Token: 0x04000366 RID: 870
		private readonly DataRowCollection.DataRowTree list = new DataRowCollection.DataRowTree();

		// Token: 0x04000367 RID: 871
		internal int nullInList;

		// Token: 0x02000349 RID: 841
		private sealed class DataRowTree : RBTree<DataRow>
		{
			// Token: 0x060033F7 RID: 13303 RVA: 0x0013FD18 File Offset: 0x0013F118
			internal DataRowTree() : base(TreeAccessMethod.INDEX_ONLY)
			{
			}

			// Token: 0x060033F8 RID: 13304 RVA: 0x0013FD2C File Offset: 0x0013F12C
			protected override int CompareNode(DataRow record1, DataRow record2)
			{
				throw ExceptionBuilder.InternalRBTreeError(RBTreeError.CompareNodeInDataRowTree);
			}

			// Token: 0x060033F9 RID: 13305 RVA: 0x0013FD40 File Offset: 0x0013F140
			protected override int CompareSateliteTreeNode(DataRow record1, DataRow record2)
			{
				throw ExceptionBuilder.InternalRBTreeError(RBTreeError.CompareSateliteTreeNodeInDataRowTree);
			}
		}
	}
}
