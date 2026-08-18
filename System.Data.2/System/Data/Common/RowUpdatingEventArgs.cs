using System;

namespace System.Data.Common
{
	// Token: 0x0200030D RID: 781
	public class RowUpdatingEventArgs : EventArgs
	{
		// Token: 0x0600315A RID: 12634 RVA: 0x00132E50 File Offset: 0x00132250
		public RowUpdatingEventArgs(DataRow dataRow, IDbCommand command, StatementType statementType, DataTableMapping tableMapping)
		{
			ADP.CheckArgumentNull(dataRow, "dataRow");
			ADP.CheckArgumentNull(tableMapping, "tableMapping");
			if (statementType <= StatementType.Delete)
			{
				this._dataRow = dataRow;
				this._command = command;
				this._statementType = statementType;
				this._tableMapping = tableMapping;
				return;
			}
			if (statementType == StatementType.Batch)
			{
				throw ADP.NotSupportedStatementType(statementType, "RowUpdatingEventArgs");
			}
			throw ADP.InvalidStatementType(statementType);
		}

		// Token: 0x17000823 RID: 2083
		// (get) Token: 0x0600315B RID: 12635 RVA: 0x00132EB4 File Offset: 0x001322B4
		// (set) Token: 0x0600315C RID: 12636 RVA: 0x00132EC8 File Offset: 0x001322C8
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

		// Token: 0x17000824 RID: 2084
		// (get) Token: 0x0600315D RID: 12637 RVA: 0x00132EDC File Offset: 0x001322DC
		// (set) Token: 0x0600315E RID: 12638 RVA: 0x00132EF0 File Offset: 0x001322F0
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

		// Token: 0x17000825 RID: 2085
		// (get) Token: 0x0600315F RID: 12639 RVA: 0x00132F04 File Offset: 0x00132304
		// (set) Token: 0x06003160 RID: 12640 RVA: 0x00132F18 File Offset: 0x00132318
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

		// Token: 0x17000826 RID: 2086
		// (get) Token: 0x06003161 RID: 12641 RVA: 0x00132F2C File Offset: 0x0013232C
		public DataRow Row
		{
			get
			{
				return this._dataRow;
			}
		}

		// Token: 0x17000827 RID: 2087
		// (get) Token: 0x06003162 RID: 12642 RVA: 0x00132F40 File Offset: 0x00132340
		public StatementType StatementType
		{
			get
			{
				return this._statementType;
			}
		}

		// Token: 0x17000828 RID: 2088
		// (get) Token: 0x06003163 RID: 12643 RVA: 0x00132F54 File Offset: 0x00132354
		// (set) Token: 0x06003164 RID: 12644 RVA: 0x00132F68 File Offset: 0x00132368
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

		// Token: 0x17000829 RID: 2089
		// (get) Token: 0x06003165 RID: 12645 RVA: 0x00132F88 File Offset: 0x00132388
		public DataTableMapping TableMapping
		{
			get
			{
				return this._tableMapping;
			}
		}

		// Token: 0x04001D87 RID: 7559
		private IDbCommand _command;

		// Token: 0x04001D88 RID: 7560
		private StatementType _statementType;

		// Token: 0x04001D89 RID: 7561
		private DataTableMapping _tableMapping;

		// Token: 0x04001D8A RID: 7562
		private Exception _errors;

		// Token: 0x04001D8B RID: 7563
		private DataRow _dataRow;

		// Token: 0x04001D8C RID: 7564
		private UpdateStatus _status;
	}
}
