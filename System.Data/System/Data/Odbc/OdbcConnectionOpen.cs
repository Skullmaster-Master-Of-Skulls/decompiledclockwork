using System;
using System.Data.Common;
using System.Data.ProviderBase;
using System.Transactions;

namespace System.Data.Odbc
{
	// Token: 0x020001DD RID: 477
	internal sealed class OdbcConnectionOpen : DbConnectionInternal
	{
		// Token: 0x06001AA1 RID: 6817 RVA: 0x0025EEF8 File Offset: 0x0025E2F8
		internal OdbcConnectionOpen(OdbcConnection outerConnection, OdbcConnectionString connectionOptions)
		{
			OdbcEnvironmentHandle globalEnvironmentHandle = OdbcEnvironment.GetGlobalEnvironmentHandle();
			outerConnection.ConnectionHandle = new OdbcConnectionHandle(outerConnection, connectionOptions, globalEnvironmentHandle);
		}

		// Token: 0x17000377 RID: 887
		// (get) Token: 0x06001AA2 RID: 6818 RVA: 0x0025EF28 File Offset: 0x0025E328
		internal OdbcConnection OuterConnection
		{
			get
			{
				OdbcConnection odbcConnection = (OdbcConnection)base.Owner;
				if (odbcConnection == null)
				{
					throw ADP.InvalidOperation("internal connection without an outer connection?");
				}
				return odbcConnection;
			}
		}

		// Token: 0x17000378 RID: 888
		// (get) Token: 0x06001AA3 RID: 6819 RVA: 0x0025EF58 File Offset: 0x0025E358
		public override string ServerVersion
		{
			get
			{
				return this.OuterConnection.Open_GetServerVersion();
			}
		}

		// Token: 0x06001AA4 RID: 6820 RVA: 0x0025EF78 File Offset: 0x0025E378
		protected override void Activate(Transaction transaction)
		{
			OdbcConnection.ExecutePermission.Demand();
		}

		// Token: 0x06001AA5 RID: 6821 RVA: 0x0025EF98 File Offset: 0x0025E398
		public override DbTransaction BeginTransaction(IsolationLevel isolevel)
		{
			return this.BeginOdbcTransaction(isolevel);
		}

		// Token: 0x06001AA6 RID: 6822 RVA: 0x0025EFB8 File Offset: 0x0025E3B8
		internal OdbcTransaction BeginOdbcTransaction(IsolationLevel isolevel)
		{
			return this.OuterConnection.Open_BeginTransaction(isolevel);
		}

		// Token: 0x06001AA7 RID: 6823 RVA: 0x0025EFD8 File Offset: 0x0025E3D8
		public override void ChangeDatabase(string value)
		{
			this.OuterConnection.Open_ChangeDatabase(value);
		}

		// Token: 0x06001AA8 RID: 6824 RVA: 0x0025EFF8 File Offset: 0x0025E3F8
		protected override DbReferenceCollection CreateReferenceCollection()
		{
			return new OdbcReferenceCollection();
		}

		// Token: 0x06001AA9 RID: 6825 RVA: 0x0025F018 File Offset: 0x0025E418
		protected override void Deactivate()
		{
			base.NotifyWeakReference(0);
		}

		// Token: 0x06001AAA RID: 6826 RVA: 0x0025F038 File Offset: 0x0025E438
		public override void EnlistTransaction(Transaction transaction)
		{
			this.OuterConnection.Open_EnlistTransaction(transaction);
		}
	}
}
