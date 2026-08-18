using System;

namespace System.Data.Common
{
	// Token: 0x0200015C RID: 348
	public class RowUpdatedEventArgs : EventArgs
	{
		// Token: 0x060015B8 RID: 5560 RVA: 0x00246208 File Offset: 0x00245608
		public RowUpdatedEventArgs(DataRow dataRow, IDbCommand command, StatementType statementType, DataTableMapping tableMapping)
		{
			switch (statementType)
			{
			case StatementType.Select:
			case StatementType.Insert:
			case StatementType.Update:
			case StatementType.Delete:
			case StatementType.Batch:
				this._dataRow = dataRow;
				this._command = command;
				this._statementType = statementType;
				this._tableMapping = tableMapping;
				return;
			default:
				throw ADP.InvalidStatementType(statementType);
			}
		}

		// Token: 0x1700031A RID: 794
		// (get) Token: 0x060015B9 RID: 5561 RVA: 0x00246268 File Offset: 0x00245668
		public IDbCommand Command
		{
			get
			{
				return this._command;
			}
		}

		// Token: 0x1700031B RID: 795
		// (get) Token: 0x060015BA RID: 5562 RVA: 0x00246288 File Offset: 0x00245688
		// (set) Token: 0x060015BB RID: 5563 RVA: 0x002462A8 File Offset: 0x002456A8
		public Exception Errors
		{
			get
			{
				return this._errors;
			}
			set
			{
				this._errors = value;
			}
		}

		// Token: 0x1700031C RID: 796
		// (get) Token: 0x060015BC RID: 5564 RVA: 0x002462C8 File Offset: 0x002456C8
		public int RecordsAffected
		{
			get
			{
				return this._recordsAffected;
			}
		}

		// Token: 0x1700031D RID: 797
		// (get) Token: 0x060015BD RID: 5565 RVA: 0x002462E8 File Offset: 0x002456E8
		public DataRow Row
		{
			get
			{
				return this._dataRow;
			}
		}

		// Token: 0x1700031E RID: 798
		// (get) Token: 0x060015BE RID: 5566 RVA: 0x00246308 File Offset: 0x00245708
		internal DataRow[] Rows
		{
			get
			{
				return this._dataRows;
			}
		}

		// Token: 0x1700031F RID: 799
		// (get) Token: 0x060015BF RID: 5567 RVA: 0x00246328 File Offset: 0x00245728
		public int RowCount
		{
			get
			{
				DataRow[] dataRows = this._dataRows;
				if (dataRows != null)
				{
					return dataRows.Length;
				}
				if (this._dataRow == null)
				{
					return 0;
				}
				return 1;
			}
		}

		// Token: 0x17000320 RID: 800
		// (get) Token: 0x060015C0 RID: 5568 RVA: 0x00246358 File Offset: 0x00245758
		public StatementType StatementType
		{
			get
			{
				return this._statementType;
			}
		}

		// Token: 0x17000321 RID: 801
		// (get) Token: 0x060015C1 RID: 5569 RVA: 0x00246378 File Offset: 0x00245778
		// (set) Token: 0x060015C2 RID: 5570 RVA: 0x00246398 File Offset: 0x00245798
		public UpdateStatus Status
		{
			get
			{
				return this._status;
			}
			set
			{
				switch (value)
				{
				case UpdateStatus.Continue:
				case UpdateStatus.ErrorsOccurred:
				case UpdateStatus.SkipCurrentRow:
				case UpdateStatus.SkipAllRemainingRows:
					this._status = value;
					return;
				default:
					throw ADP.InvalidUpdateStatus(value);
				}
			}
		}

		// Token: 0x17000322 RID: 802
		// (get) Token: 0x060015C3 RID: 5571 RVA: 0x002463D8 File Offset: 0x002457D8
		public DataTableMapping TableMapping
		{
			get
			{
				return this._tableMapping;
			}
		}

		// Token: 0x060015C4 RID: 5572 RVA: 0x002463F8 File Offset: 0x002457F8
		internal void AdapterInit(DataRow[] dataRows)
		{
			this._statementType = StatementType.Batch;
			this._dataRows = dataRows;
			if (dataRows != null && 1 == dataRows.Length)
			{
				this._dataRow = dataRows[0];
			}
		}

		// Token: 0x060015C5 RID: 5573 RVA: 0x00246428 File Offset: 0x00245828
		internal void AdapterInit(int recordsAffected)
		{
			this._recordsAffected = recordsAffected;
		}

		// Token: 0x060015C6 RID: 5574 RVA: 0x00246448 File Offset: 0x00245848
		public void CopyToRows(DataRow[] array)
		{
			this.CopyToRows(array, 0);
		}

		// Token: 0x060015C7 RID: 5575 RVA: 0x00246468 File Offset: 0x00245868
		public void CopyToRows(DataRow[] array, int arrayIndex)
		{
			DataRow[] dataRows = this._dataRows;
			if (dataRows != null)
			{
				dataRows.CopyTo(array, arrayIndex);
				return;
			}
			if (array == null)
			{
				throw ADP.ArgumentNull("array");
			}
			array[arrayIndex] = this.Row;
		}

		// Token: 0x04000CC5 RID: 3269
		private IDbCommand _command;

		// Token: 0x04000CC6 RID: 3270
		private StatementType _statementType;

		// Token: 0x04000CC7 RID: 3271
		private DataTableMapping _tableMapping;

		// Token: 0x04000CC8 RID: 3272
		private Exception _errors;

		// Token: 0x04000CC9 RID: 3273
		private DataRow _dataRow;

		// Token: 0x04000CCA RID: 3274
		private DataRow[] _dataRows;

		// Token: 0x04000CCB RID: 3275
		private UpdateStatus _status;

		// Token: 0x04000CCC RID: 3276
		private int _recordsAffected;
	}
}
