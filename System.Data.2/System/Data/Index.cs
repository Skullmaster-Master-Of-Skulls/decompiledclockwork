using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;

namespace System.Data
{
	// Token: 0x02000127 RID: 295
	internal sealed class Index
	{
		// Token: 0x06001181 RID: 4481 RVA: 0x00087530 File Offset: 0x00086930
		public Index(DataTable table, IndexField[] indexFields, DataViewRowState recordStates, IFilter rowFilter) : this(table, indexFields, null, recordStates, rowFilter)
		{
		}

		// Token: 0x06001182 RID: 4482 RVA: 0x0008754C File Offset: 0x0008694C
		public Index(DataTable table, Comparison<DataRow> comparison, DataViewRowState recordStates, IFilter rowFilter) : this(table, Index.GetAllFields(table.Columns), comparison, recordStates, rowFilter)
		{
		}

		// Token: 0x06001183 RID: 4483 RVA: 0x00087570 File Offset: 0x00086970
		private static IndexField[] GetAllFields(DataColumnCollection columns)
		{
			IndexField[] array = new IndexField[columns.Count];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = new IndexField(columns[i], false);
			}
			return array;
		}

		// Token: 0x06001184 RID: 4484 RVA: 0x000875AC File Offset: 0x000869AC
		private Index(DataTable table, IndexField[] indexFields, Comparison<DataRow> comparison, DataViewRowState recordStates, IFilter rowFilter)
		{
			Bid.Trace("<ds.Index.Index|API> %d#, table=%d, recordStates=%d{ds.DataViewRowState}\n", this.ObjectID, (table != null) ? table.ObjectID : 0, (int)recordStates);
			if ((recordStates & ~(DataViewRowState.Unchanged | DataViewRowState.Added | DataViewRowState.Deleted | DataViewRowState.ModifiedCurrent | DataViewRowState.ModifiedOriginal)) != DataViewRowState.None)
			{
				throw ExceptionBuilder.RecordStateRange();
			}
			this.table = table;
			this._listeners = new Listeners<DataViewListener>(this.ObjectID, (DataViewListener listener) => listener != null);
			this.IndexFields = indexFields;
			this.recordStates = recordStates;
			this._comparison = comparison;
			DataColumnCollection columns = table.Columns;
			this.isSharable = (rowFilter == null && comparison == null);
			if (rowFilter != null)
			{
				this.rowFilter = new WeakReference(rowFilter);
				DataExpression dataExpression = rowFilter as DataExpression;
				if (dataExpression != null)
				{
					this._hasRemoteAggregate = dataExpression.HasRemoteAggregate();
				}
			}
			this.InitRecords(rowFilter);
		}

		// Token: 0x06001185 RID: 4485 RVA: 0x00087690 File Offset: 0x00086A90
		public bool Equal(IndexField[] indexDesc, DataViewRowState recordStates, IFilter rowFilter)
		{
			if (!this.isSharable || this.IndexFields.Length != indexDesc.Length || this.recordStates != recordStates || rowFilter != null)
			{
				return false;
			}
			for (int i = 0; i < this.IndexFields.Length; i++)
			{
				if (this.IndexFields[i].Column != indexDesc[i].Column || this.IndexFields[i].IsDescending != indexDesc[i].IsDescending)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x1700029A RID: 666
		// (get) Token: 0x06001186 RID: 4486 RVA: 0x00087714 File Offset: 0x00086B14
		internal bool HasRemoteAggregate
		{
			get
			{
				return this._hasRemoteAggregate;
			}
		}

		// Token: 0x1700029B RID: 667
		// (get) Token: 0x06001187 RID: 4487 RVA: 0x00087728 File Offset: 0x00086B28
		internal int ObjectID
		{
			get
			{
				return this._objectID;
			}
		}

		// Token: 0x1700029C RID: 668
		// (get) Token: 0x06001188 RID: 4488 RVA: 0x0008773C File Offset: 0x00086B3C
		public DataViewRowState RecordStates
		{
			get
			{
				return this.recordStates;
			}
		}

		// Token: 0x1700029D RID: 669
		// (get) Token: 0x06001189 RID: 4489 RVA: 0x00087750 File Offset: 0x00086B50
		public IFilter RowFilter
		{
			get
			{
				return (IFilter)((this.rowFilter != null) ? this.rowFilter.Target : null);
			}
		}

		// Token: 0x0600118A RID: 4490 RVA: 0x00087778 File Offset: 0x00086B78
		public int GetRecord(int recordIndex)
		{
			return this.records[recordIndex];
		}

		// Token: 0x1700029E RID: 670
		// (get) Token: 0x0600118B RID: 4491 RVA: 0x00087794 File Offset: 0x00086B94
		public bool HasDuplicates
		{
			get
			{
				return this.records.HasDuplicates;
			}
		}

		// Token: 0x1700029F RID: 671
		// (get) Token: 0x0600118C RID: 4492 RVA: 0x000877AC File Offset: 0x00086BAC
		public int RecordCount
		{
			get
			{
				return this.recordCount;
			}
		}

		// Token: 0x170002A0 RID: 672
		// (get) Token: 0x0600118D RID: 4493 RVA: 0x000877C0 File Offset: 0x00086BC0
		public bool IsSharable
		{
			get
			{
				return this.isSharable;
			}
		}

		// Token: 0x0600118E RID: 4494 RVA: 0x000877D4 File Offset: 0x00086BD4
		private bool AcceptRecord(int record)
		{
			return this.AcceptRecord(record, this.RowFilter);
		}

		// Token: 0x0600118F RID: 4495 RVA: 0x000877F0 File Offset: 0x00086BF0
		private bool AcceptRecord(int record, IFilter filter)
		{
			Bid.Trace("<ds.Index.AcceptRecord|API> %d#, record=%d\n", this.ObjectID, record);
			if (filter == null)
			{
				return true;
			}
			DataRow dataRow = this.table.recordManager[record];
			if (dataRow == null)
			{
				return true;
			}
			DataRowVersion version = DataRowVersion.Default;
			if (dataRow.oldRecord == record)
			{
				version = DataRowVersion.Original;
			}
			else if (dataRow.newRecord == record)
			{
				version = DataRowVersion.Current;
			}
			else if (dataRow.tempRecord == record)
			{
				version = DataRowVersion.Proposed;
			}
			return filter.Invoke(dataRow, version);
		}

		// Token: 0x06001190 RID: 4496 RVA: 0x0008786C File Offset: 0x00086C6C
		internal void ListChangedAdd(DataViewListener listener)
		{
			this._listeners.Add(listener);
		}

		// Token: 0x06001191 RID: 4497 RVA: 0x00087888 File Offset: 0x00086C88
		internal void ListChangedRemove(DataViewListener listener)
		{
			this._listeners.Remove(listener);
		}

		// Token: 0x170002A1 RID: 673
		// (get) Token: 0x06001192 RID: 4498 RVA: 0x000878A4 File Offset: 0x00086CA4
		public int RefCount
		{
			get
			{
				return this.refCount;
			}
		}

		// Token: 0x06001193 RID: 4499 RVA: 0x000878B8 File Offset: 0x00086CB8
		public void AddRef()
		{
			Bid.Trace("<ds.Index.AddRef|API> %d#\n", this.ObjectID);
			LockCookie lockCookie = this.table.indexesLock.UpgradeToWriterLock(-1);
			try
			{
				if (this.refCount == 0)
				{
					this.table.ShadowIndexCopy();
					this.table.indexes.Add(this);
				}
				this.refCount++;
			}
			finally
			{
				this.table.indexesLock.DowngradeFromWriterLock(ref lockCookie);
			}
		}

		// Token: 0x06001194 RID: 4500 RVA: 0x0008794C File Offset: 0x00086D4C
		public int RemoveRef()
		{
			Bid.Trace("<ds.Index.RemoveRef|API> %d#\n", this.ObjectID);
			LockCookie lockCookie = this.table.indexesLock.UpgradeToWriterLock(-1);
			int result;
			try
			{
				int num = this.refCount - 1;
				this.refCount = num;
				result = num;
				if (this.refCount <= 0)
				{
					this.table.ShadowIndexCopy();
					this.table.indexes.Remove(this);
				}
			}
			finally
			{
				this.table.indexesLock.DowngradeFromWriterLock(ref lockCookie);
			}
			return result;
		}

		// Token: 0x06001195 RID: 4501 RVA: 0x000879E8 File Offset: 0x00086DE8
		private void ApplyChangeAction(int record, int action, int changeRecord)
		{
			if (action != 0)
			{
				if (action > 0)
				{
					if (this.AcceptRecord(record))
					{
						this.InsertRecord(record, true);
						return;
					}
				}
				else
				{
					if (this._comparison != null && -1 != record)
					{
						this.DeleteRecord(this.GetIndex(record, changeRecord));
						return;
					}
					this.DeleteRecord(this.GetIndex(record));
				}
			}
		}

		// Token: 0x06001196 RID: 4502 RVA: 0x00087A38 File Offset: 0x00086E38
		public bool CheckUnique()
		{
			return !this.HasDuplicates;
		}

		// Token: 0x06001197 RID: 4503 RVA: 0x00087A50 File Offset: 0x00086E50
		private int CompareRecords(int record1, int record2)
		{
			if (this._comparison != null)
			{
				return this.CompareDataRows(record1, record2);
			}
			if (this.IndexFields.Length != 0)
			{
				int i = 0;
				while (i < this.IndexFields.Length)
				{
					int num = this.IndexFields[i].Column.Compare(record1, record2);
					if (num != 0)
					{
						if (!this.IndexFields[i].IsDescending)
						{
							return num;
						}
						return -num;
					}
					else
					{
						i++;
					}
				}
				return 0;
			}
			return this.table.Rows.IndexOf(this.table.recordManager[record1]).CompareTo(this.table.Rows.IndexOf(this.table.recordManager[record2]));
		}

		// Token: 0x06001198 RID: 4504 RVA: 0x00087B0C File Offset: 0x00086F0C
		private int CompareDataRows(int record1, int record2)
		{
			return this._comparison(this.table.recordManager[record1], this.table.recordManager[record2]);
		}

		// Token: 0x06001199 RID: 4505 RVA: 0x00087B48 File Offset: 0x00086F48
		private int CompareDuplicateRecords(int record1, int record2)
		{
			if (this.table.recordManager[record1] == null)
			{
				if (this.table.recordManager[record2] != null)
				{
					return -1;
				}
				return 0;
			}
			else
			{
				if (this.table.recordManager[record2] == null)
				{
					return 1;
				}
				int num = this.table.recordManager[record1].rowID.CompareTo(this.table.recordManager[record2].rowID);
				if (num == 0 && record1 != record2)
				{
					num = ((int)this.table.recordManager[record1].GetRecordState(record1)).CompareTo((int)this.table.recordManager[record2].GetRecordState(record2));
				}
				return num;
			}
		}

		// Token: 0x0600119A RID: 4506 RVA: 0x00087C08 File Offset: 0x00087008
		private int CompareRecordToKey(int record1, object[] vals)
		{
			int i = 0;
			while (i < this.IndexFields.Length)
			{
				int num = this.IndexFields[i].Column.CompareValueTo(record1, vals[i]);
				if (num != 0)
				{
					if (!this.IndexFields[i].IsDescending)
					{
						return num;
					}
					return -num;
				}
				else
				{
					i++;
				}
			}
			return 0;
		}

		// Token: 0x0600119B RID: 4507 RVA: 0x00087C60 File Offset: 0x00087060
		public void DeleteRecordFromIndex(int recordIndex)
		{
			this.DeleteRecord(recordIndex, false);
		}

		// Token: 0x0600119C RID: 4508 RVA: 0x00087C78 File Offset: 0x00087078
		private void DeleteRecord(int recordIndex)
		{
			this.DeleteRecord(recordIndex, true);
		}

		// Token: 0x0600119D RID: 4509 RVA: 0x00087C90 File Offset: 0x00087090
		private void DeleteRecord(int recordIndex, bool fireEvent)
		{
			Bid.Trace("<ds.Index.DeleteRecord|INFO> %d#, recordIndex=%d, fireEvent=%d{bool}\n", this.ObjectID, recordIndex, fireEvent);
			if (recordIndex >= 0)
			{
				this.recordCount--;
				int record = this.records.DeleteByIndex(recordIndex);
				this.MaintainDataView(ListChangedType.ItemDeleted, record, !fireEvent);
				if (fireEvent)
				{
					this.OnListChanged(ListChangedType.ItemDeleted, recordIndex);
				}
			}
		}

		// Token: 0x0600119E RID: 4510 RVA: 0x00087CE8 File Offset: 0x000870E8
		public RBTree<int>.RBTreeEnumerator GetEnumerator(int startIndex)
		{
			return new RBTree<int>.RBTreeEnumerator(this.records, startIndex);
		}

		// Token: 0x0600119F RID: 4511 RVA: 0x00087D04 File Offset: 0x00087104
		public int GetIndex(int record)
		{
			return this.records.GetIndexByKey(record);
		}

		// Token: 0x060011A0 RID: 4512 RVA: 0x00087D20 File Offset: 0x00087120
		private int GetIndex(int record, int changeRecord)
		{
			DataRow dataRow = this.table.recordManager[record];
			int newRecord = dataRow.newRecord;
			int oldRecord = dataRow.oldRecord;
			int indexByKey;
			try
			{
				if (changeRecord != 1)
				{
					if (changeRecord == 2)
					{
						dataRow.oldRecord = record;
					}
				}
				else
				{
					dataRow.newRecord = record;
				}
				indexByKey = this.records.GetIndexByKey(record);
			}
			finally
			{
				if (changeRecord != 1)
				{
					if (changeRecord == 2)
					{
						dataRow.oldRecord = oldRecord;
					}
				}
				else
				{
					dataRow.newRecord = newRecord;
				}
			}
			return indexByKey;
		}

		// Token: 0x060011A1 RID: 4513 RVA: 0x00087DB0 File Offset: 0x000871B0
		public object[] GetUniqueKeyValues()
		{
			if (this.IndexFields == null || this.IndexFields.Length == 0)
			{
				return Index.zeroObjects;
			}
			List<object[]> list = new List<object[]>();
			this.GetUniqueKeyValues(list, this.records.root);
			return list.ToArray();
		}

		// Token: 0x060011A2 RID: 4514 RVA: 0x00087DF4 File Offset: 0x000871F4
		public int FindRecord(int record)
		{
			int num = this.records.Search(record);
			if (num != 0)
			{
				return this.records.GetIndexByNode(num);
			}
			return -1;
		}

		// Token: 0x060011A3 RID: 4515 RVA: 0x00087E20 File Offset: 0x00087220
		public int FindRecordByKey(object key)
		{
			int num = this.FindNodeByKey(key);
			if (num != 0)
			{
				return this.records.GetIndexByNode(num);
			}
			return -1;
		}

		// Token: 0x060011A4 RID: 4516 RVA: 0x00087E48 File Offset: 0x00087248
		public int FindRecordByKey(object[] key)
		{
			int num = this.FindNodeByKeys(key);
			if (num != 0)
			{
				return this.records.GetIndexByNode(num);
			}
			return -1;
		}

		// Token: 0x060011A5 RID: 4517 RVA: 0x00087E70 File Offset: 0x00087270
		private int FindNodeByKey(object originalKey)
		{
			if (this.IndexFields.Length != 1)
			{
				throw ExceptionBuilder.IndexKeyLength(this.IndexFields.Length, 1);
			}
			int num = this.records.root;
			if (num != 0)
			{
				DataColumn column = this.IndexFields[0].Column;
				object value = column.ConvertValue(originalKey);
				num = this.records.root;
				if (this.IndexFields[0].IsDescending)
				{
					while (num != 0)
					{
						int num2 = column.CompareValueTo(this.records.Key(num), value);
						if (num2 == 0)
						{
							break;
						}
						if (num2 < 0)
						{
							num = this.records.Left(num);
						}
						else
						{
							num = this.records.Right(num);
						}
					}
				}
				else
				{
					while (num != 0)
					{
						int num2 = column.CompareValueTo(this.records.Key(num), value);
						if (num2 == 0)
						{
							break;
						}
						if (num2 > 0)
						{
							num = this.records.Left(num);
						}
						else
						{
							num = this.records.Right(num);
						}
					}
				}
			}
			return num;
		}

		// Token: 0x060011A6 RID: 4518 RVA: 0x00087F60 File Offset: 0x00087360
		private int FindNodeByKeys(object[] originalKey)
		{
			int num = (originalKey != null) ? originalKey.Length : 0;
			if (num == 0 || this.IndexFields.Length != num)
			{
				throw ExceptionBuilder.IndexKeyLength(this.IndexFields.Length, num);
			}
			int num2 = this.records.root;
			if (num2 != 0)
			{
				object[] array = new object[originalKey.Length];
				for (int i = 0; i < originalKey.Length; i++)
				{
					array[i] = this.IndexFields[i].Column.ConvertValue(originalKey[i]);
				}
				num2 = this.records.root;
				while (num2 != 0)
				{
					num = this.CompareRecordToKey(this.records.Key(num2), array);
					if (num == 0)
					{
						break;
					}
					if (num > 0)
					{
						num2 = this.records.Left(num2);
					}
					else
					{
						num2 = this.records.Right(num2);
					}
				}
			}
			return num2;
		}

		// Token: 0x060011A7 RID: 4519 RVA: 0x00088024 File Offset: 0x00087424
		private int FindNodeByKeyRecord(int record)
		{
			int num = this.records.root;
			if (num != 0)
			{
				num = this.records.root;
				while (num != 0)
				{
					int num2 = this.CompareRecords(this.records.Key(num), record);
					if (num2 == 0)
					{
						break;
					}
					if (num2 > 0)
					{
						num = this.records.Left(num);
					}
					else
					{
						num = this.records.Right(num);
					}
				}
			}
			return num;
		}

		// Token: 0x060011A8 RID: 4520 RVA: 0x0008808C File Offset: 0x0008748C
		internal Range FindRecords<TKey, TRow>(Index.ComparisonBySelector<TKey, TRow> comparison, TKey key) where TRow : DataRow
		{
			int nodeId = this.records.root;
			while (nodeId != 0)
			{
				int num = comparison(key, (TRow)((object)this.table.recordManager[this.records.Key(nodeId)]));
				if (num == 0)
				{
					break;
				}
				if (num < 0)
				{
					nodeId = this.records.Left(nodeId);
				}
				else
				{
					nodeId = this.records.Right(nodeId);
				}
			}
			return this.GetRangeFromNode(nodeId);
		}

		// Token: 0x060011A9 RID: 4521 RVA: 0x00088100 File Offset: 0x00087500
		private Range GetRangeFromNode(int nodeId)
		{
			if (nodeId == 0)
			{
				return default(Range);
			}
			int indexByNode = this.records.GetIndexByNode(nodeId);
			if (this.records.Next(nodeId) == 0)
			{
				return new Range(indexByNode, indexByNode);
			}
			int num = this.records.SubTreeSize(this.records.Next(nodeId));
			return new Range(indexByNode, indexByNode + num - 1);
		}

		// Token: 0x060011AA RID: 4522 RVA: 0x00088160 File Offset: 0x00087560
		public Range FindRecords(object key)
		{
			int nodeId = this.FindNodeByKey(key);
			return this.GetRangeFromNode(nodeId);
		}

		// Token: 0x060011AB RID: 4523 RVA: 0x0008817C File Offset: 0x0008757C
		public Range FindRecords(object[] key)
		{
			int nodeId = this.FindNodeByKeys(key);
			return this.GetRangeFromNode(nodeId);
		}

		// Token: 0x060011AC RID: 4524 RVA: 0x00088198 File Offset: 0x00087598
		internal void FireResetEvent()
		{
			Bid.Trace("<ds.Index.FireResetEvent|API> %d#\n", this.ObjectID);
			if (this.DoListChanged)
			{
				this.OnListChanged(DataView.ResetEventArgs);
			}
		}

		// Token: 0x060011AD RID: 4525 RVA: 0x000881C8 File Offset: 0x000875C8
		private int GetChangeAction(DataViewRowState oldState, DataViewRowState newState)
		{
			int num = ((this.recordStates & oldState) == DataViewRowState.None) ? 0 : 1;
			int num2 = ((this.recordStates & newState) == DataViewRowState.None) ? 0 : 1;
			return num2 - num;
		}

		// Token: 0x060011AE RID: 4526 RVA: 0x000881F8 File Offset: 0x000875F8
		private static int GetReplaceAction(DataViewRowState oldState)
		{
			if ((DataViewRowState.CurrentRows & oldState) != DataViewRowState.None)
			{
				return 1;
			}
			if ((DataViewRowState.OriginalRows & oldState) == DataViewRowState.None)
			{
				return 0;
			}
			return 2;
		}

		// Token: 0x060011AF RID: 4527 RVA: 0x00088218 File Offset: 0x00087618
		public DataRow GetRow(int i)
		{
			return this.table.recordManager[this.GetRecord(i)];
		}

		// Token: 0x060011B0 RID: 4528 RVA: 0x0008823C File Offset: 0x0008763C
		public DataRow[] GetRows(object[] values)
		{
			return this.GetRows(this.FindRecords(values));
		}

		// Token: 0x060011B1 RID: 4529 RVA: 0x00088258 File Offset: 0x00087658
		public DataRow[] GetRows(Range range)
		{
			DataRow[] array = this.table.NewRowArray(range.Count);
			if (array.Length != 0)
			{
				RBTree<int>.RBTreeEnumerator enumerator = this.GetEnumerator(range.Min);
				int num = 0;
				while (num < array.Length && enumerator.MoveNext())
				{
					array[num] = this.table.recordManager[enumerator.Current];
					num++;
				}
			}
			return array;
		}

		// Token: 0x060011B2 RID: 4530 RVA: 0x000882BC File Offset: 0x000876BC
		private void InitRecords(IFilter filter)
		{
			DataViewRowState dataViewRowState = this.recordStates;
			bool append = this.IndexFields.Length == 0;
			this.records = new Index.IndexTree(this);
			this.recordCount = 0;
			foreach (object obj in this.table.Rows)
			{
				DataRow dataRow = (DataRow)obj;
				int num = -1;
				if (dataRow.oldRecord == dataRow.newRecord)
				{
					if ((dataViewRowState & DataViewRowState.Unchanged) != DataViewRowState.None)
					{
						num = dataRow.oldRecord;
					}
				}
				else if (dataRow.oldRecord == -1)
				{
					if ((dataViewRowState & DataViewRowState.Added) != DataViewRowState.None)
					{
						num = dataRow.newRecord;
					}
				}
				else if (dataRow.newRecord == -1)
				{
					if ((dataViewRowState & DataViewRowState.Deleted) != DataViewRowState.None)
					{
						num = dataRow.oldRecord;
					}
				}
				else if ((dataViewRowState & DataViewRowState.ModifiedCurrent) != DataViewRowState.None)
				{
					num = dataRow.newRecord;
				}
				else if ((dataViewRowState & DataViewRowState.ModifiedOriginal) != DataViewRowState.None)
				{
					num = dataRow.oldRecord;
				}
				if (num != -1 && this.AcceptRecord(num, filter))
				{
					this.records.InsertAt(-1, num, append);
					this.recordCount++;
				}
			}
		}

		// Token: 0x060011B3 RID: 4531 RVA: 0x000883E4 File Offset: 0x000877E4
		public int InsertRecordToIndex(int record)
		{
			int result = -1;
			if (this.AcceptRecord(record))
			{
				result = this.InsertRecord(record, false);
			}
			return result;
		}

		// Token: 0x060011B4 RID: 4532 RVA: 0x00088408 File Offset: 0x00087808
		private int InsertRecord(int record, bool fireEvent)
		{
			Bid.Trace("<ds.Index.InsertRecord|INFO> %d#, record=%d, fireEvent=%d{bool}\n", this.ObjectID, record, fireEvent);
			bool append = false;
			if (this.IndexFields.Length == 0 && this.table != null)
			{
				DataRow row = this.table.recordManager[record];
				append = (this.table.Rows.IndexOf(row) + 1 == this.table.Rows.Count);
			}
			int node = this.records.InsertAt(-1, record, append);
			this.recordCount++;
			this.MaintainDataView(ListChangedType.ItemAdded, record, !fireEvent);
			if (fireEvent)
			{
				if (this.DoListChanged)
				{
					this.OnListChanged(ListChangedType.ItemAdded, this.records.GetIndexByNode(node));
				}
				return 0;
			}
			return this.records.GetIndexByNode(node);
		}

		// Token: 0x060011B5 RID: 4533 RVA: 0x000884C8 File Offset: 0x000878C8
		public bool IsKeyInIndex(object key)
		{
			int num = this.FindNodeByKey(key);
			return num != 0;
		}

		// Token: 0x060011B6 RID: 4534 RVA: 0x000884E4 File Offset: 0x000878E4
		public bool IsKeyInIndex(object[] key)
		{
			int num = this.FindNodeByKeys(key);
			return num != 0;
		}

		// Token: 0x060011B7 RID: 4535 RVA: 0x00088500 File Offset: 0x00087900
		public bool IsKeyRecordInIndex(int record)
		{
			int num = this.FindNodeByKeyRecord(record);
			return num != 0;
		}

		// Token: 0x170002A2 RID: 674
		// (get) Token: 0x060011B8 RID: 4536 RVA: 0x0008851C File Offset: 0x0008791C
		private bool DoListChanged
		{
			get
			{
				return !this.suspendEvents && this._listeners.HasListeners && !this.table.AreIndexEventsSuspended;
			}
		}

		// Token: 0x060011B9 RID: 4537 RVA: 0x00088550 File Offset: 0x00087950
		private void OnListChanged(ListChangedType changedType, int newIndex, int oldIndex)
		{
			if (this.DoListChanged)
			{
				this.OnListChanged(new ListChangedEventArgs(changedType, newIndex, oldIndex));
			}
		}

		// Token: 0x060011BA RID: 4538 RVA: 0x00088574 File Offset: 0x00087974
		private void OnListChanged(ListChangedType changedType, int index)
		{
			if (this.DoListChanged)
			{
				this.OnListChanged(new ListChangedEventArgs(changedType, index));
			}
		}

		// Token: 0x060011BB RID: 4539 RVA: 0x00088598 File Offset: 0x00087998
		private void OnListChanged(ListChangedEventArgs e)
		{
			Bid.Trace("<ds.Index.OnListChanged|INFO> %d#\n", this.ObjectID);
			this._listeners.Notify<ListChangedEventArgs, bool, bool>(e, false, false, delegate(DataViewListener listener, ListChangedEventArgs args, bool arg2, bool arg3)
			{
				listener.IndexListChanged(args);
			});
		}

		// Token: 0x060011BC RID: 4540 RVA: 0x000885E4 File Offset: 0x000879E4
		private void MaintainDataView(ListChangedType changedType, int record, bool trackAddRemove)
		{
			this._listeners.Notify<ListChangedType, DataRow, bool>(changedType, (0 <= record) ? this.table.recordManager[record] : null, trackAddRemove, delegate(DataViewListener listener, ListChangedType type, DataRow row, bool track)
			{
				listener.MaintainDataView(changedType, row, track);
			});
		}

		// Token: 0x060011BD RID: 4541 RVA: 0x00088634 File Offset: 0x00087A34
		public void Reset()
		{
			Bid.Trace("<ds.Index.Reset|API> %d#\n", this.ObjectID);
			this.InitRecords(this.RowFilter);
			this.MaintainDataView(ListChangedType.Reset, -1, false);
			this.FireResetEvent();
		}

		// Token: 0x060011BE RID: 4542 RVA: 0x0008866C File Offset: 0x00087A6C
		public void RecordChanged(int record)
		{
			Bid.Trace("<ds.Index.RecordChanged|API> %d#, record=%d\n", this.ObjectID, record);
			if (this.DoListChanged)
			{
				int index = this.GetIndex(record);
				if (index >= 0)
				{
					this.OnListChanged(ListChangedType.ItemChanged, index);
				}
			}
		}

		// Token: 0x060011BF RID: 4543 RVA: 0x000886A8 File Offset: 0x00087AA8
		public void RecordChanged(int oldIndex, int newIndex)
		{
			Bid.Trace("<ds.Index.RecordChanged|API> %d#, oldIndex=%d, newIndex=%d\n", this.ObjectID, oldIndex, newIndex);
			if (oldIndex > -1 || newIndex > -1)
			{
				if (oldIndex == newIndex)
				{
					this.OnListChanged(ListChangedType.ItemChanged, newIndex, oldIndex);
					return;
				}
				if (oldIndex == -1)
				{
					this.OnListChanged(ListChangedType.ItemAdded, newIndex, oldIndex);
					return;
				}
				if (newIndex == -1)
				{
					this.OnListChanged(ListChangedType.ItemDeleted, oldIndex);
					return;
				}
				this.OnListChanged(ListChangedType.ItemMoved, newIndex, oldIndex);
			}
		}

		// Token: 0x060011C0 RID: 4544 RVA: 0x00088704 File Offset: 0x00087B04
		public void RecordStateChanged(int record, DataViewRowState oldState, DataViewRowState newState)
		{
			Bid.Trace("<ds.Index.RecordStateChanged|API> %d#, record=%d, oldState=%d{ds.DataViewRowState}, newState=%d{ds.DataViewRowState}\n", this.ObjectID, record, (int)oldState, (int)newState);
			int changeAction = this.GetChangeAction(oldState, newState);
			this.ApplyChangeAction(record, changeAction, Index.GetReplaceAction(oldState));
		}

		// Token: 0x060011C1 RID: 4545 RVA: 0x0008873C File Offset: 0x00087B3C
		public void RecordStateChanged(int oldRecord, DataViewRowState oldOldState, DataViewRowState oldNewState, int newRecord, DataViewRowState newOldState, DataViewRowState newNewState)
		{
			Bid.Trace("<ds.Index.RecordStateChanged|API> %d#, oldRecord=%d, oldOldState=%d{ds.DataViewRowState}, oldNewState=%d{ds.DataViewRowState}, newRecord=%d, newOldState=%d{ds.DataViewRowState}, newNewState=%d{ds.DataViewRowState}\n", this.ObjectID, oldRecord, (int)oldOldState, (int)oldNewState, newRecord, (int)newOldState, (int)newNewState);
			int changeAction = this.GetChangeAction(oldOldState, oldNewState);
			int changeAction2 = this.GetChangeAction(newOldState, newNewState);
			if (changeAction != -1 || changeAction2 != 1 || !this.AcceptRecord(newRecord))
			{
				this.ApplyChangeAction(oldRecord, changeAction, Index.GetReplaceAction(oldOldState));
				this.ApplyChangeAction(newRecord, changeAction2, Index.GetReplaceAction(newOldState));
				return;
			}
			int index;
			if (this._comparison != null && changeAction < 0)
			{
				index = this.GetIndex(oldRecord, Index.GetReplaceAction(oldOldState));
			}
			else
			{
				index = this.GetIndex(oldRecord);
			}
			if (this._comparison == null && index != -1 && this.CompareRecords(oldRecord, newRecord) == 0)
			{
				this.records.UpdateNodeKey(oldRecord, newRecord);
				int index2 = this.GetIndex(newRecord);
				this.OnListChanged(ListChangedType.ItemChanged, index2, index2);
				return;
			}
			this.suspendEvents = true;
			if (index != -1)
			{
				this.records.DeleteByIndex(index);
				this.recordCount--;
			}
			this.records.Insert(newRecord);
			this.recordCount++;
			this.suspendEvents = false;
			int index3 = this.GetIndex(newRecord);
			if (index == index3)
			{
				this.OnListChanged(ListChangedType.ItemChanged, index3, index);
				return;
			}
			if (index == -1)
			{
				this.MaintainDataView(ListChangedType.ItemAdded, newRecord, false);
				this.OnListChanged(ListChangedType.ItemAdded, this.GetIndex(newRecord));
				return;
			}
			this.OnListChanged(ListChangedType.ItemMoved, index3, index);
		}

		// Token: 0x170002A3 RID: 675
		// (get) Token: 0x060011C2 RID: 4546 RVA: 0x00088898 File Offset: 0x00087C98
		internal DataTable Table
		{
			get
			{
				return this.table;
			}
		}

		// Token: 0x060011C3 RID: 4547 RVA: 0x000888AC File Offset: 0x00087CAC
		private void GetUniqueKeyValues(List<object[]> list, int curNodeId)
		{
			if (curNodeId != 0)
			{
				this.GetUniqueKeyValues(list, this.records.Left(curNodeId));
				int record = this.records.Key(curNodeId);
				object[] array = new object[this.IndexFields.Length];
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = this.IndexFields[i].Column[record];
				}
				list.Add(array);
				this.GetUniqueKeyValues(list, this.records.Right(curNodeId));
			}
		}

		// Token: 0x060011C4 RID: 4548 RVA: 0x0008892C File Offset: 0x00087D2C
		internal static int IndexOfReference<T>(List<T> list, T item) where T : class
		{
			if (list != null)
			{
				for (int i = 0; i < list.Count; i++)
				{
					if (list[i] == item)
					{
						return i;
					}
				}
			}
			return -1;
		}

		// Token: 0x060011C5 RID: 4549 RVA: 0x00088964 File Offset: 0x00087D64
		internal static bool ContainsReference<T>(List<T> list, T item) where T : class
		{
			return 0 <= Index.IndexOfReference<T>(list, item);
		}

		// Token: 0x040005EA RID: 1514
		private const int DoNotReplaceCompareRecord = 0;

		// Token: 0x040005EB RID: 1515
		private const int ReplaceNewRecordForCompare = 1;

		// Token: 0x040005EC RID: 1516
		private const int ReplaceOldRecordForCompare = 2;

		// Token: 0x040005ED RID: 1517
		private readonly DataTable table;

		// Token: 0x040005EE RID: 1518
		internal readonly IndexField[] IndexFields;

		// Token: 0x040005EF RID: 1519
		private readonly Comparison<DataRow> _comparison;

		// Token: 0x040005F0 RID: 1520
		private readonly DataViewRowState recordStates;

		// Token: 0x040005F1 RID: 1521
		private WeakReference rowFilter;

		// Token: 0x040005F2 RID: 1522
		private Index.IndexTree records;

		// Token: 0x040005F3 RID: 1523
		private int recordCount;

		// Token: 0x040005F4 RID: 1524
		private int refCount;

		// Token: 0x040005F5 RID: 1525
		private Listeners<DataViewListener> _listeners;

		// Token: 0x040005F6 RID: 1526
		private bool suspendEvents;

		// Token: 0x040005F7 RID: 1527
		private static readonly object[] zeroObjects = new object[0];

		// Token: 0x040005F8 RID: 1528
		private readonly bool isSharable;

		// Token: 0x040005F9 RID: 1529
		private readonly bool _hasRemoteAggregate;

		// Token: 0x040005FA RID: 1530
		internal const int MaskBits = 2147483647;

		// Token: 0x040005FB RID: 1531
		private static int _objectTypeCount;

		// Token: 0x040005FC RID: 1532
		private readonly int _objectID = Interlocked.Increment(ref Index._objectTypeCount);

		// Token: 0x02000358 RID: 856
		private sealed class IndexTree : RBTree<int>
		{
			// Token: 0x0600341E RID: 13342 RVA: 0x0014034C File Offset: 0x0013F74C
			internal IndexTree(Index index) : base(TreeAccessMethod.KEY_SEARCH_AND_INDEX)
			{
				this._index = index;
			}

			// Token: 0x0600341F RID: 13343 RVA: 0x00140368 File Offset: 0x0013F768
			protected override int CompareNode(int record1, int record2)
			{
				return this._index.CompareRecords(record1, record2);
			}

			// Token: 0x06003420 RID: 13344 RVA: 0x00140384 File Offset: 0x0013F784
			protected override int CompareSateliteTreeNode(int record1, int record2)
			{
				return this._index.CompareDuplicateRecords(record1, record2);
			}

			// Token: 0x04001F00 RID: 7936
			private readonly Index _index;
		}

		// Token: 0x02000359 RID: 857
		// (Invoke) Token: 0x06003422 RID: 13346
		internal delegate int ComparisonBySelector<TKey, TRow>(TKey key, TRow row) where TRow : DataRow;
	}
}
