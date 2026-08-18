using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Diagnostics;

namespace System.Data
{
	// Token: 0x0200011D RID: 285
	internal sealed class RecordManager
	{
		// Token: 0x0600113F RID: 4415 RVA: 0x00085304 File Offset: 0x00084704
		internal RecordManager(DataTable table)
		{
			if (table == null)
			{
				throw ExceptionBuilder.ArgumentNull("table");
			}
			this.table = table;
		}

		// Token: 0x06001140 RID: 4416 RVA: 0x00085340 File Offset: 0x00084740
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

		// Token: 0x17000295 RID: 661
		// (get) Token: 0x06001141 RID: 4417 RVA: 0x000853D0 File Offset: 0x000847D0
		internal int LastFreeRecord
		{
			get
			{
				return this.lastFreeRecord;
			}
		}

		// Token: 0x17000296 RID: 662
		// (get) Token: 0x06001142 RID: 4418 RVA: 0x000853E4 File Offset: 0x000847E4
		// (set) Token: 0x06001143 RID: 4419 RVA: 0x000853F8 File Offset: 0x000847F8
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

		// Token: 0x17000297 RID: 663
		// (get) Token: 0x06001144 RID: 4420 RVA: 0x00085420 File Offset: 0x00084820
		// (set) Token: 0x06001145 RID: 4421 RVA: 0x00085434 File Offset: 0x00084834
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

		// Token: 0x06001146 RID: 4422 RVA: 0x00085484 File Offset: 0x00084884
		internal static int NewCapacity(int capacity)
		{
			if (capacity >= 128)
			{
				return capacity + capacity;
			}
			return 128;
		}

		// Token: 0x06001147 RID: 4423 RVA: 0x000854A4 File Offset: 0x000848A4
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

		// Token: 0x06001148 RID: 4424 RVA: 0x000854E4 File Offset: 0x000848E4
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

		// Token: 0x06001149 RID: 4425 RVA: 0x0008555C File Offset: 0x0008495C
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

		// Token: 0x0600114A RID: 4426 RVA: 0x000855E8 File Offset: 0x000849E8
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

		// Token: 0x17000298 RID: 664
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

		// Token: 0x0600114D RID: 4429 RVA: 0x00085704 File Offset: 0x00084B04
		internal void SetKeyValues(int record, DataKey key, object[] keyValues)
		{
			for (int i = 0; i < keyValues.Length; i++)
			{
				key.ColumnsReference[i][record] = keyValues[i];
			}
		}

		// Token: 0x0600114E RID: 4430 RVA: 0x00085734 File Offset: 0x00084B34
		internal int ImportRecord(DataTable src, int record)
		{
			return this.CopyRecord(src, record, -1);
		}

		// Token: 0x0600114F RID: 4431 RVA: 0x0008574C File Offset: 0x00084B4C
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

		// Token: 0x06001150 RID: 4432 RVA: 0x00085834 File Offset: 0x00084C34
		internal void SetRowCache(DataRow[] newRows)
		{
			this.rows = newRows;
			this.lastFreeRecord = this.rows.Length;
			this.recordCapacity = this.lastFreeRecord;
		}

		// Token: 0x06001151 RID: 4433 RVA: 0x00085864 File Offset: 0x00084C64
		[Conditional("DEBUG")]
		internal void VerifyRecord(int record)
		{
		}

		// Token: 0x06001152 RID: 4434 RVA: 0x00085874 File Offset: 0x00084C74
		[Conditional("DEBUG")]
		internal void VerifyRecord(int record, DataRow row)
		{
		}

		// Token: 0x040005C5 RID: 1477
		private readonly DataTable table;

		// Token: 0x040005C6 RID: 1478
		private int lastFreeRecord;

		// Token: 0x040005C7 RID: 1479
		private int minimumCapacity = 50;

		// Token: 0x040005C8 RID: 1480
		private int recordCapacity;

		// Token: 0x040005C9 RID: 1481
		private readonly List<int> freeRecordList = new List<int>();

		// Token: 0x040005CA RID: 1482
		private DataRow[] rows;
	}
}
