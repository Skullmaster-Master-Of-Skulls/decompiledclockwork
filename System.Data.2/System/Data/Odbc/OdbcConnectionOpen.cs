using System;
using System.Data.Common;
using System.Data.ProviderBase;
using System.Transactions;

namespace System.Data.Odbc
{
	// Token: 0x02000293 RID: 659
	internal sealed class OdbcConnectionOpen : DbConnectionInternal
	{
		// Token: 0x06002812 RID: 10258 RVA: 0x0010CACC File Offset: 0x0010BECC
		internal OdbcConnectionOpen(OdbcConnection outerConnection, OdbcConnectionString connectionOptions)
		{
			OdbcEnvironmentHandle globalEnvironmentHandle = OdbcEnvironment.GetGlobalEnvironmentHandle();
			outerConnection.ConnectionHandle = new OdbcConnectionHandle(outerConnection, connectionOptions, globalEnvironmentHandle);
		}

		// Token: 0x17000678 RID: 1656
		// (get) Token: 0x06002813 RID: 10259 RVA: 0x0010CAF4 File Offset: 0x0010BEF4
		internal OdbcConnection OuterConnection
		{
			get
			{
				OdbcConnection odbcConnection = (OdbcConnection)base.Owner;
				if (odbcConnection == null)
				{
					throw ODBC.OpenConnectionNoOwner();
				}
				return odbcConnection;
			}
		}

		// Token: 0x17000679 RID: 1657
		// (get) Token: 0x06002814 RID: 10260 RVA: 0x0010CB18 File Offset: 0x0010BF18
		public override string ServerVersion
		{
			get
			{
				return this.OuterConnection.Open_GetServerVersion();
			}
		}

		// Token: 0x06002815 RID: 10261 RVA: 0x0010CB30 File Offset: 0x0010BF30
		protected override void Activate(Transaction transaction)
		{
			OdbcConnection.ExecutePermission.Demand();
		}

		// Token: 0x06002816 RID: 10262 RVA: 0x0010CB48 File Offset: 0x0010BF48
		public override DbTransaction BeginTransaction(IsolationLevel isolevel)
		{
			return this.BeginOdbcTransaction(isolevel);
		}

		// Token: 0x06002817 RID: 10263 RVA: 0x0010CB5C File Offset: 0x0010BF5C
		internal OdbcTransaction BeginOdbcTransaction(IsolationLevel isolevel)
		{
			return this.OuterConnection.Open_BeginTransaction(isolevel);
		}

		// Token: 0x06002818 RID: 10264 RVA: 0x0010CB78 File Offset: 0x0010BF78
		public override void ChangeDatabase(string value)
		{
			this.OuterConnection.Open_ChangeDatabase(value);
		}

		// Token: 0x06002819 RID: 10265 RVA: 0x0010CB94 File Offset: 0x0010BF94
		protected override DbReferenceCollection CreateReferenceCollection()
		{
			return new OdbcReferenceCollection();
		}

		// Token: 0x0600281A RID: 10266 RVA: 0x0010CBA8 File Offset: 0x0010BFA8
		protected override void Deactivate()
		{
			base.NotifyWeakReference(0);
		}

		// Token: 0x0600281B RID: 10267 RVA: 0x0010CBBC File Offset: 0x0010BFBC
		public override void EnlistTransaction(Transaction transaction)
		{
			this.OuterConnection.Open_EnlistTransaction(transaction);
		}
	}
}
