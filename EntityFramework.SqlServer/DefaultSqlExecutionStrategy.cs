using System;
using System.Data.Entity.Core;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.SqlServer.Resources;
using System.Data.Entity.SqlServer.Utilities;
using System.Threading;
using System.Threading.Tasks;

namespace System.Data.Entity.SqlServer
{
	// Token: 0x02000010 RID: 16
	internal sealed class DefaultSqlExecutionStrategy : IDbExecutionStrategy
	{
		// Token: 0x17000017 RID: 23
		// (get) Token: 0x060000A9 RID: 169 RVA: 0x000041AA File Offset: 0x000023AA
		public bool RetriesOnFailure
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060000AA RID: 170 RVA: 0x000041C4 File Offset: 0x000023C4
		public void Execute(Action operation)
		{
			if (operation == null)
			{
				throw new ArgumentNullException("operation");
			}
			this.Execute<object>(delegate()
			{
				operation();
				return null;
			});
		}

		// Token: 0x060000AB RID: 171 RVA: 0x00004204 File Offset: 0x00002404
		public TResult Execute<TResult>(Func<TResult> operation)
		{
			Check.NotNull<Func<TResult>>(operation, "operation");
			TResult result;
			try
			{
				result = operation();
			}
			catch (Exception ex)
			{
				if (DbExecutionStrategy.UnwrapAndHandleException<bool>(ex, new Func<Exception, bool>(SqlAzureRetriableExceptionDetector.ShouldRetryOn)))
				{
					throw new EntityException(Strings.TransientExceptionDetected, ex);
				}
				throw;
			}
			return result;
		}

		// Token: 0x060000AC RID: 172 RVA: 0x00004390 File Offset: 0x00002590
		public Task ExecuteAsync(Func<Task> operation, CancellationToken cancellationToken)
		{
			Check.NotNull<Func<Task>>(operation, "operation");
			cancellationToken.ThrowIfCancellationRequested();
			return DefaultSqlExecutionStrategy.ExecuteAsyncImplementation<bool>(async delegate
			{
				await operation().ConfigureAwait(false);
				return true;
			});
		}

		// Token: 0x060000AD RID: 173 RVA: 0x000043D3 File Offset: 0x000025D3
		public Task<TResult> ExecuteAsync<TResult>(Func<Task<TResult>> operation, CancellationToken cancellationToken)
		{
			Check.NotNull<Func<Task<TResult>>>(operation, "operation");
			cancellationToken.ThrowIfCancellationRequested();
			return DefaultSqlExecutionStrategy.ExecuteAsyncImplementation<TResult>(operation);
		}

		// Token: 0x060000AE RID: 174 RVA: 0x0000450C File Offset: 0x0000270C
		private static async Task<TResult> ExecuteAsyncImplementation<TResult>(Func<Task<TResult>> func)
		{
			TResult result;
			try
			{
				result = await func().ConfigureAwait(false);
			}
			catch (Exception ex)
			{
				if (DbExecutionStrategy.UnwrapAndHandleException<bool>(ex, new Func<Exception, bool>(SqlAzureRetriableExceptionDetector.ShouldRetryOn)))
				{
					throw new EntityException(Strings.TransientExceptionDetected, ex);
				}
				throw;
			}
			return result;
		}
	}
}
