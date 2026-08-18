using System;
using System.Data.Common;
using System.Data.Entity.Infrastructure.Interception;
using System.Data.Entity.Resources;

namespace System.Data.Entity.Infrastructure
{
	// Token: 0x02000196 RID: 406
	internal class DefaultTransactionHandler : TransactionHandler
	{
		// Token: 0x06000DC7 RID: 3527 RVA: 0x0003D51B File Offset: 0x0003B71B
		public override string BuildDatabaseInitializationScript()
		{
			return string.Empty;
		}

		// Token: 0x06000DC8 RID: 3528 RVA: 0x0003D522 File Offset: 0x0003B722
		public override void Committed(DbTransaction transaction, DbTransactionInterceptionContext interceptionContext)
		{
			if (interceptionContext.Exception != null && interceptionContext.Connection != null && this.MatchesParentContext(interceptionContext.Connection, interceptionContext))
			{
				interceptionContext.Exception = new CommitFailedException(Strings.CommitFailed, interceptionContext.Exception);
			}
		}
	}
}
