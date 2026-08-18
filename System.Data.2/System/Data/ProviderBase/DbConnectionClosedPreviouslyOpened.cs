using System;
using System.Data.Common;
using System.Threading.Tasks;

namespace System.Data.ProviderBase
{
	// Token: 0x020002BE RID: 702
	internal sealed class DbConnectionClosedPreviouslyOpened : DbConnectionClosed
	{
		// Token: 0x06002A4C RID: 10828 RVA: 0x00116F70 File Offset: 0x00116370
		private DbConnectionClosedPreviouslyOpened() : base(ConnectionState.Closed, true, true)
		{
		}

		// Token: 0x06002A4D RID: 10829 RVA: 0x00116F88 File Offset: 0x00116388
		internal override bool TryReplaceConnection(DbConnection outerConnection, DbConnectionFactory connectionFactory, TaskCompletionSource<DbConnectionInternal> retry, DbConnectionOptions userOptions)
		{
			return this.TryOpenConnection(outerConnection, connectionFactory, retry, userOptions);
		}

		// Token: 0x04001B1B RID: 6939
		internal static readonly DbConnectionInternal SingletonInstance = new DbConnectionClosedPreviouslyOpened();
	}
}
