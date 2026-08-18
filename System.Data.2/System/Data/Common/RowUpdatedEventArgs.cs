using System;

namespace System.Data.Common
{
	// Token: 0x0200030C RID: 780
	public class RowUpdatedEventArgs : EventArgs
	{
		// Token: 0x0600314A RID: 12618 RVA: 0x00132C84 File Offset: 0x00132084
		public RowUpdatedEventArgs(DataRow dataRow, IDbCommand command, StatementType statementType, DataTableMapping tableMapping)
		{
			if (statementType > StatementType.Batch)
			{
				throw ADP.InvalidStatementType(statementType);
			}
			this._dataRow = dataRow;
			this._command = command;
			this._statementType = statementType;
			this._tableMapping = tableMapping;
		}

		// Token: 0x1700081A RID: 2074
		// (get) Token: 0x0600314B RID: 12619 RVA: 0x00132CC0 File Offset: 0x001320C0
		public IDbCommand Command
		{
			get
			{
				return this._command;
			}
		}

		// Token: 0x1700081B RID: 2075
		// (get) Token: 0x0600314C RID: 12620 RVA: 0x00132CD4 File Offset: 0x001320D4
		// (set) Token: 0x0600314D RID: 12621 RVA: 0x00132CE8 File Offset: 0x001320E8
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

		// Token: 0x1700081C RID: 2076
		// (get) Token: 0x0600314E RID: 12622 RVA: 0x00132CFC File Offset: 0x001320FC
		public int RecordsAffected
		{
			get
			{
				return this._recordsAffected;
			}
		}

		// Token: 0x1700081D RID: 2077
		// (get) Token: 0x0600314F RID: 12623 RVA: 0x00132D10 File Offset: 0x00132110
		public DataRow Row
		{
			get
			{
				return this._dataRow;
			}
		}

		// Token: 0x1700081E RID: 2078
		// (get) Token: 0x06003150 RID: 12624 RVA: 0x00132D24 File Offset: 0x00132124
		internal DataRow[] Rows
		{
			get
			{
				return this._dataRows;
			}
		}

		// Token: 0x1700081F RID: 2079
		// (get) Token: 0x06003151 RID: 12625 RVA: 0x00132D38 File Offset: 0x00132138
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

		// Token: 0x17000820 RID: 2080
		// (get) Token: 0x06003152 RID: 12626 RVA: 0x00132D60 File Offset: 0x00132160
		public StatementType StatementType
		{
			get
			{
				return this._statementType;
			}
		}

		// Token: 0x17000821 RID: 2081
		// (get) Token: 0x06003153 RID: 12627 RVA: 0x00132D74 File Offset: 0x00132174
		// (set) Token: 0x06003154 RID: 12628 RVA: 0x00132D88 File Offset: 0x00132188
		public UpdateStatus Status
		{
			get
			{
				return this._status;
			}
			set
			{
				if (value <= UpdateStatus.SkipAllRemainingRows)
				{
					this._status = value;
					return;
				}
				throw ADP.InvalidUpdateStatus(value);
			}
		}

		// Token: 0x17000822 RID: 2082
		// (get) Token: 0x06003155 RID: 12629 RVA: 0x00132DA8 File Offset: 0x001321A8
		public DataTableMapping TableMapping
		{
			get
			{
				return this._tableMapping;
			}
		}

		// Token: 0x06003156 RID: 12630 RVA: 0x00132DBC File Offset: 0x001321BC
		internal void AdapterInit(DataRow[] dataRows)
		{
			this._statementType = StatementType.Batch;
			this._dataRows = dataRows;
			if (dataRows != null && 1 == dataRows.Length)
			{
				this._dataRow = dataRows[0];
			}
		}

		// Token: 0x06003157 RID: 12631 RVA: 0x00132DEC File Offset: 0x001321EC
		internal void AdapterInit(int recordsAffected)
		{
			this._recordsAffected = recordsAffected;
		}

		// Token: 0x06003158 RID: 12632 RVA: 0x00132E00 File Offset: 0x00132200
		public void CopyToRows(DataRow[] array)
		{
			this.CopyToRows(array, 0);
		}

		// Token: 0x06003159 RID: 12633 RVA: 0x00132E18 File Offset: 0x00132218
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

		// Token: 0x04001D7F RID: 7551
		private IDbCommand _command;

		// Token: 0x04001D80 RID: 7552
		private StatementType _statementType;

		// Token: 0x04001D81 RID: 7553
		private DataTableMapping _tableMapping;

		// Token: 0x04001D82 RID: 7554
		private Exception _errors;

		// Token: 0x04001D83 RID: 7555
		private DataRow _dataRow;

		// Token: 0x04001D84 RID: 7556
		private DataRow[] _dataRows;

		// Token: 0x04001D85 RID: 7557
		private UpdateStatus _status;

		// Token: 0x04001D86 RID: 7558
		private int _recordsAffected;
	}
}
