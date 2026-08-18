using System;

namespace System.Data.Common
{
	// Token: 0x0200015D RID: 349
	public class RowUpdatingEventArgs : EventArgs
	{
		// Token: 0x060015C8 RID: 5576 RVA: 0x002464A8 File Offset: 0x002458A8
		public RowUpdatingEventArgs(DataRow dataRow, IDbCommand command, StatementType statementType, DataTableMapping tableMapping)
		{
			ADP.CheckArgumentNull(dataRow, "dataRow");
			ADP.CheckArgumentNull(tableMapping, "tableMapping");
			switch (statementType)
			{
			case StatementType.Select:
			case StatementType.Insert:
			case StatementType.Update:
			case StatementType.Delete:
				this._dataRow = dataRow;
				this._command = command;
				this._statementType = statementType;
				this._tableMapping = tableMapping;
				return;
			case StatementType.Batch:
				throw ADP.NotSupportedStatementType(statementType, "RowUpdatingEventArgs");
			default:
				throw ADP.InvalidStatementType(statementType);
			}
		}

		// Token: 0x17000323 RID: 803
		// (get) Token: 0x060015C9 RID: 5577 RVA: 0x00246528 File Offset: 0x00245928
		// (set) Token: 0x060015CA RID: 5578 RVA: 0x00246548 File Offset: 0x00245948
		protected virtual IDbCommand BaseCommand
		{
			get
			{
				return this._command;
			}
			set
			{
				this._command = value;
			}
		}

		// Token: 0x17000324 RID: 804
		// (get) Token: 0x060015CB RID: 5579 RVA: 0x00246568 File Offset: 0x00245968
		// (set) Token: 0x060015CC RID: 5580 RVA: 0x00246588 File Offset: 0x00245988
		public IDbCommand Command
		{
			get
			{
				return this.BaseCommand;
			}
			set
			{
				this.BaseCommand = value;
			}
		}

		// Token: 0x17000325 RID: 805
		// (get) Token: 0x060015CD RID: 5581 RVA: 0x002465A8 File Offset: 0x002459A8
		// (set) Token: 0x060015CE RID: 5582 RVA: 0x002465C8 File Offset: 0x002459C8
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

		// Token: 0x17000326 RID: 806
		// (get) Token: 0x060015CF RID: 5583 RVA: 0x002465E8 File Offset: 0x002459E8
		public DataRow Row
		{
			get
			{
				return this._dataRow;
			}
		}

		// Token: 0x17000327 RID: 807
		// (get) Token: 0x060015D0 RID: 5584 RVA: 0x00246608 File Offset: 0x00245A08
		public StatementType StatementType
		{
			get
			{
				return this._statementType;
			}
		}

		// Token: 0x17000328 RID: 808
		// (get) Token: 0x060015D1 RID: 5585 RVA: 0x00246628 File Offset: 0x00245A28
		// (set) Token: 0x060015D2 RID: 5586 RVA: 0x00246648 File Offset: 0x00245A48
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

		// Token: 0x17000329 RID: 809
		// (get) Token: 0x060015D3 RID: 5587 RVA: 0x00246688 File Offset: 0x00245A88
		public DataTableMapping TableMapping
		{
			get
			{
				return this._tableMapping;
			}
		}

		// Token: 0x04000CCD RID: 3277
		private IDbCommand _command;

		// Token: 0x04000CCE RID: 3278
		private StatementType _statementType;

		// Token: 0x04000CCF RID: 3279
		private DataTableMapping _tableMapping;

		// Token: 0x04000CD0 RID: 3280
		private Exception _errors;

		// Token: 0x04000CD1 RID: 3281
		private DataRow _dataRow;

		// Token: 0x04000CD2 RID: 3282
		private UpdateStatus _status;
	}
}
