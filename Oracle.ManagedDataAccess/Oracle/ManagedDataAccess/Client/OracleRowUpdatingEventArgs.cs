using System;
using System.Data;
using System.Data.Common;
using OracleInternal.Common;

namespace Oracle.ManagedDataAccess.Client
{
	// Token: 0x0200007C RID: 124
	public sealed class OracleRowUpdatingEventArgs : RowUpdatingEventArgs
	{
		// Token: 0x0600064C RID: 1612 RVA: 0x0003914C File Offset: 0x0003734C
		public OracleRowUpdatingEventArgs(DataRow row, IDbCommand command, StatementType statementType, DataTableMapping tableMapping) : base(row, command, statementType, tableMapping)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Exit, new string[0]);
			}
		}

		// Token: 0x1700019C RID: 412
		// (get) Token: 0x0600064D RID: 1613 RVA: 0x0003918C File Offset: 0x0003738C
		// (set) Token: 0x0600064E RID: 1614 RVA: 0x0003919C File Offset: 0x0003739C
		public new OracleCommand Command
		{
			get
			{
				return (OracleCommand)base.Command;
			}
			set
			{
				base.Command = value;
			}
		}

		// Token: 0x1700019D RID: 413
		// (get) Token: 0x0600064F RID: 1615 RVA: 0x000391A8 File Offset: 0x000373A8
		// (set) Token: 0x06000650 RID: 1616 RVA: 0x000391B0 File Offset: 0x000373B0
		protected override IDbCommand BaseCommand
		{
			get
			{
				return base.BaseCommand;
			}
			set
			{
				base.BaseCommand = (value as OracleCommand);
			}
		}
	}
}
