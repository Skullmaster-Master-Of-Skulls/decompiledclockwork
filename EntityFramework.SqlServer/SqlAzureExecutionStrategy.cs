using System;
using System.Data.Entity.Infrastructure;

namespace System.Data.Entity.SqlServer
{
	// Token: 0x02000018 RID: 24
	public class SqlAzureExecutionStrategy : DbExecutionStrategy
	{
		// Token: 0x060000F9 RID: 249 RVA: 0x00004C61 File Offset: 0x00002E61
		public SqlAzureExecutionStrategy()
		{
		}

		// Token: 0x060000FA RID: 250 RVA: 0x00004C69 File Offset: 0x00002E69
		public SqlAzureExecutionStrategy(int maxRetryCount, TimeSpan maxDelay) : base(maxRetryCount, maxDelay)
		{
		}

		// Token: 0x060000FB RID: 251 RVA: 0x00004C73 File Offset: 0x00002E73
		protected override bool ShouldRetryOn(Exception exception)
		{
			return SqlAzureRetriableExceptionDetector.ShouldRetryOn(exception);
		}
	}
}
