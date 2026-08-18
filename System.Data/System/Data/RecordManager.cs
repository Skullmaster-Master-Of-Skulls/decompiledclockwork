using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Diagnostics;

namespace System.Data
{
	// Token: 0x020000D2 RID: 210
	internal sealed class RecordManager
	{
		// Token: 0x06000CD2 RID: 3282 RVA: 0x00212408 File Offset: 0x00211808
		internal RecordManager(DataTable table)
		{
			if (table == null)
			{
				throw ExceptionBuilder.ArgumentNull("table");
			}
			this.table = table;
		}

		// Token: 0x06000CD3 RID: 3283 RVA: 0x00212448 File Offset: 0x00211848
		private void GrowRecordCapacity()
		{
			if (RecordManager.NewCapacity(this.recordCapacity) < this.NormalizedMinimumCapacity(this.minimumCapacity))
			{
				this.RecordCapacity = this.NormalizedMinimumCapacity(this.minimumCapacity);
			}
			else
			{
				this.RecordCapacity = RecordManager.NewCapacity(this.recordCapacity);
			}
			DataRow[] destinationArray = this.table.NewRowArray(this.recordCapacity);
			if (this.rows != null)
			{
				Array.Copy(this.rows, 0, destinationArray, 0, Math.Min(this.lastFreeRecord, this.rows.Length));
			}
			this.rows = destinationArray;
		}

		// Token: 0x170001F0 RID: 496
		// (get) Token: 0x06000CD4 RID: 3284 RVA: 0x002124D8 File Offset: 0x002118D8
		internal int LastFreeRecord
		{
			get
			{
				return this.lastFreeRecord;
			}
		}

		// Token: 0x170001F1 RID: 497
		// (get) Token: 0x06000CD5 RID: 3285 RVA: 0x002124F8 File Offset: 0x002118F8
		// (set) Token: 0x06000CD6 RID: 3286 RVA: 0x00212518 File Offset: 0x00211918
		internal int MinimumCapacity
		{
			get
			{
				return this.minimumCapacity;
			}
			set
			{
				if (this.minimumCapacity != value)
				{
					if (value < 0)
					{
						throw ExceptionBuilder.NegativeMinimumCapacity();
					}
					this.minimumCapacity = value;
				}
			}
		}

		// Token: 0x170001F2 RID: 498
		// (get) Token: 0x06000CD7 RID: 3287 RVA: 0x00212548 File Offset: 0x00211948
		// (set) Token: 0x06000CD8 RID: 3288 RVA: 0x00212568 File Offset: 0x00211968
		internal int RecordCapacity
		{
			get
			{
				return this.recordCapacity;
			}
			set
			{
				if (this.recordCapacity != value)
				{
					for (int i = 0; i < this.table.Columns.Count; i++)
					{
						this.table.Columns[i].SetCapacity(value);
					}
					this.recordCapacity = value;
				}
			}
		}

		// Token: 0x06000CD9 RID: 3289 RVA: 0x002125B8 File Offset: 0x002119B8
		internal static int NewCapacity(int capacity)
		{
			if (capacity >= 128)
			{
				return capacity + capacity;
			}
			return 128;
		}

		// Token: 0x06000CDA RID: 3290 RVA: 0x002125D8 File Offset: 0x002119D8
		private int NormalizedMinimumCapacity(int capacity)
		{
			if (capacity >= 1014)
			{
				return (capacity + 10 >> 10) + 1 << 10;
			}
			if (capacity >= 246)
			{
				return 1024;
			}
			if (capacity < 54)
			{
				return 64;
			}
			return 256;
		}

		// Token: 0x06000CDB RID: 3291 RVA: 0x00212618 File Offset: 0x00211A18
		internal int NewRecordBase()
		{
			int result;
			if (this.freeRecordList.Count != 0)
			{
				result = this.freeRecordList[this.freeRecordList.Count - 1];
				this.freeRecordList.RemoveAt(this.freeRecordList.Count - 1);
			}
			else
			{
				if (this.lastFreeRecord >= this.recordCapacity)
				{
					this.GrowRecordCapacity();
				}
				result = this.lastFreeRecord;
				this.lastFreeRecord++;
			}
			return result;
		}

		// Token: 0x06000CDC RID: 3292 RVA: 0x00212698 File Offset: 0x00211A98
		internal void FreeRecord(ref int record)
		{
			if (-1 != record)
			{
				this[record] = null;
				int count = this.table.columnCollection.Count;
				for (int i = 0; i < count; i++)
				{
					this.table.columnCollection[i].FreeRecord(record);
				}
				if (this.lastFreeRecord == record + 1)
				{
					this.lastFreeRecord--;
				}
				else if (record < this.lastFreeRecord)
				{
					this.freeRecordList.Add(record);
				}
				record = -1;
			}
		}

		// Token: 0x06000CDD RID: 3293 RVA: 0x00212728 File Offset: 0x00211B28
		internal void Clear(bool clearAll)
		{
			if (clearAll)
			{
				for (int i = 0; i < this.recordCapacity; i++)
				{
					this.rows[i] = null;
				}
				int count = this.table.columnCollection.Count;
				for (int j = 0; j < count; j++)
				{
					DataColumn dataColumn = this.table.columnCollection[j];
					for (int k = 0; k < this.recordCapacity; k++)
					{
						dataColumn.FreeRecord(k);
					}
				}
				this.lastFreeRecord = 0;
				this.freeRecordList.Clear();
				return;
			}
			this.freeRecordList.Capacity = this.freeRecordList.Count + this.table.Rows.Count;
			for (int l = 0; l < this.recordCapacity; l++)
			{
				if (this.rows[l] != null && this.rows[l].rowID != -1L)
				{
					int num = l;
					this.FreeRecord(ref num);
				}
			}
		}

		// Token: 0x170001F3 RID: 499
		internal DataRow this[int record]
		{
			get
			{
				return this.rows[record];
			}
			set
			{
				this.rows[record] = value;
			}
		}

		// Token: 0x06000CE0 RID: 3296 RVA: 0x00212858 File Offset: 0x00211C58
		internal void SetKeyValues(int record, DataKey key, object[] keyValues)
		{
			for (int i = 0; i < keyValues.Length; i++)
			{
				key.ColumnsReference[i][record] = keyValues[i];
			}
		}

		// Token: 0x06000CE1 RID: 3297 RVA: 0x00212888 File Offset: 0x00211C88
		internal int ImportRecord(DataTable src, int record)
		{
			return this.CopyRecord(src, record, -1);
		}

		// Token: 0x06000CE2 RID: 3298 RVA: 0x002128A8 File Offset: 0x00211CA8
		internal int CopyRecord(DataTable src, int record, int copy)
		{
			if (record == -1)
			{
				return copy;
			}
			int num = -1;
			try
			{
				if (copy == -1)
				{
					num = this.table.NewUninitializedRecord();
				}
				else
				{
					num = copy;
				}
				int count = this.table.Columns.Count;
				for (int i = 0; i < count; i++)
				{
					DataColumn dataColumn = this.table.Columns[i];
					DataColumn dataColumn2 = src.Columns[dataColumn.ColumnName];
					if (dataColumn2 != null)
					{
						object obj = dataColumn2[record];
						ICloneable cloneable = obj as ICloneable;
						if (cloneable != null)
						{
							dataColumn[num] = cloneable.Clone();
						}
						else
						{
							dataColumn[num] = obj;
						}
					}
					else if (-1 == copy)
					{
						dataColumn.Init(num);
					}
				}
			}
			catch (Exception e)
			{
				if (ADP.IsCatchableOrSecurityExceptionType(e) && -1 == copy)
				{
					this.FreeRecord(ref num);
				}
				throw;
			}
			return num;
		}

		// Token: 0x06000CE3 RID: 3299 RVA: 0x00212998 File Offset: 0x00211D98
		internal void SetRowCache(DataRow[] newRows)
		{
			this.rows = newRows;
			this.lastFreeRecord = this.rows.Length;
			this.recordCapacity = this.lastFreeRecord;
		}

		// Token: 0x06000CE4 RID: 3300 RVA: 0x002129C8 File Offset: 0x00211DC8
		[Conditional("DEBUG")]
		internal void VerifyRecord(int record)
		{
		}

		// Token: 0x06000CE5 RID: 3301 RVA: 0x002129D8 File Offset: 0x00211DD8
		[Conditional("DEBUG")]
		internal void VerifyRecord(int record, DataRow row)
		{
		}

		// Token: 0x040008EA RID: 2282
		private readonly DataTable table;

		// Token: 0x040008EB RID: 2283
		private int lastFreeRecord;

		// Token: 0x040008EC RID: 2284
		private int minimumCapacity = 50;

		// Token: 0x040008ED RID: 2285
		private int recordCapacity;

		// Token: 0x040008EE RID: 2286
		private readonly List<int> freeRecordList = new List<int>();

		// Token: 0x040008EF RID: 2287
		private DataRow[] rows;
	}
}
