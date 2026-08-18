using System;
using System.Threading;
using System.Threading.Tasks;

namespace System.Data.Entity.Infrastructure
{
	// Token: 0x02000292 RID: 658
	public class DefaultExecutionStrategy : IDbExecutionStrategy
	{
		// Token: 0x1700029B RID: 667
		// (get) Token: 0x06001708 RID: 5896 RVA: 0x00072C85 File Offset: 0x00070E85
		public bool RetriesOnFailure
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06001709 RID: 5897 RVA: 0x00072C88 File Offset: 0x00070E88
		public void Execute(Action operation)
		{
			operation();
		}

		// Token: 0x0600170A RID: 5898 RVA: 0x00072C90 File Offset: 0x00070E90
		public TResult Execute<TResult>(Func<TResult> operation)
		{
			return operation();
		}

		// Token: 0x0600170B RID: 5899 RVA: 0x00072C98 File Offset: 0x00070E98
		public Task ExecuteAsync(Func<Task> operation, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();
			return operation();
		}

		// Token: 0x0600170C RID: 5900 RVA: 0x00072CA7 File Offset: 0x00070EA7
		public Task<TResult> ExecuteAsync<TResult>(Func<Task<TResult>> operation, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();
			return operation();
		}
	}
}
