using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;

namespace System.Data.Entity.Infrastructure
{
	// Token: 0x02000287 RID: 647
	public abstract class DbExecutionStrategy : IDbExecutionStrategy
	{
		// Token: 0x060016B1 RID: 5809 RVA: 0x0006EE9D File Offset: 0x0006D09D
		protected DbExecutionStrategy() : this(5, DbExecutionStrategy.DefaultMaxDelay)
		{
		}

		// Token: 0x060016B2 RID: 5810 RVA: 0x0006EEAC File Offset: 0x0006D0AC
		protected DbExecutionStrategy(int maxRetryCount, TimeSpan maxDelay)
		{
			if (maxRetryCount < 0)
			{
				throw new ArgumentOutOfRangeException("maxRetryCount");
			}
			if (maxDelay.TotalMilliseconds < 0.0)
			{
				throw new ArgumentOutOfRangeException("maxDelay");
			}
			this._maxRetryCount = maxRetryCount;
			this._maxDelay = maxDelay;
		}

		// Token: 0x17000293 RID: 659
		// (get) Token: 0x060016B3 RID: 5811 RVA: 0x0006EF0F File Offset: 0x0006D10F
		public bool RetriesOnFailure
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060016B4 RID: 5812 RVA: 0x0006EF28 File Offset: 0x0006D128
		public void Execute(Action operation)
		{
			Check.NotNull<Action>(operation, "operation");
			this.Execute<object>(delegate()
			{
				operation();
				return null;
			});
		}

		// Token: 0x060016B5 RID: 5813 RVA: 0x0006EF68 File Offset: 0x0006D168
		public TResult Execute<TResult>(Func<TResult> operation)
		{
			Check.NotNull<Func<TResult>>(operation, "operation");
			this.EnsurePreexecutionState();
			TimeSpan? nextDelay;
			for (;;)
			{
				try
				{
					return operation();
				}
				catch (Exception ex)
				{
					if (!DbExecutionStrategy.UnwrapAndHandleException<bool>(ex, new Func<Exception, bool>(this.ShouldRetryOn)))
					{
						throw;
					}
					nextDelay = this.GetNextDelay(ex);
					if (nextDelay == null)
					{
						throw new RetryLimitExceededException(Strings.ExecutionStrategy_RetryLimitExceeded(this._maxRetryCount, base.GetType().Name), ex);
					}
				}
				if (nextDelay < TimeSpan.Zero)
				{
					break;
				}
				Thread.Sleep(nextDelay.Value);
			}
			throw new InvalidOperationException(Strings.ExecutionStrategy_NegativeDelay(nextDelay));
		}

		// Token: 0x060016B6 RID: 5814 RVA: 0x0006F16C File Offset: 0x0006D36C
		public Task ExecuteAsync(Func<Task> operation, CancellationToken cancellationToken)
		{
			Check.NotNull<Func<Task>>(operation, "operation");
			this.EnsurePreexecutionState();
			cancellationToken.ThrowIfCancellationRequested();
			return this.ProtectedExecuteAsync<bool>(async delegate
			{
				await operation().WithCurrentCulture();
				return true;
			}, cancellationToken);
		}

		// Token: 0x060016B7 RID: 5815 RVA: 0x0006F1B7 File Offset: 0x0006D3B7
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		public Task<TResult> ExecuteAsync<TResult>(Func<Task<TResult>> operation, CancellationToken cancellationToken)
		{
			Check.NotNull<Func<Task<TResult>>>(operation, "operation");
			this.EnsurePreexecutionState();
			cancellationToken.ThrowIfCancellationRequested();
			return this.ProtectedExecuteAsync<TResult>(operation, cancellationToken);
		}

		// Token: 0x060016B8 RID: 5816 RVA: 0x0006F428 File Offset: 0x0006D628
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		private async Task<TResult> ProtectedExecuteAsync<TResult>(Func<Task<TResult>> operation, CancellationToken cancellationToken)
		{
			TimeSpan? delay;
			for (;;)
			{
				try
				{
					return await operation().WithCurrentCulture<TResult>();
				}
				catch (Exception ex)
				{
					if (!DbExecutionStrategy.UnwrapAndHandleException<bool>(ex, new Func<Exception, bool>(this.ShouldRetryOn)))
					{
						throw;
					}
					delay = this.GetNextDelay(ex);
					if (delay == null)
					{
						throw new RetryLimitExceededException(Strings.ExecutionStrategy_RetryLimitExceeded(this._maxRetryCount, base.GetType().Name), ex);
					}
				}
				if (delay < TimeSpan.Zero)
				{
					break;
				}
				await Task.Delay(delay.Value, cancellationToken).WithCurrentCulture();
			}
			throw new InvalidOperationException(Strings.ExecutionStrategy_NegativeDelay(delay));
		}

		// Token: 0x060016B9 RID: 5817 RVA: 0x0006F47E File Offset: 0x0006D67E
		private void EnsurePreexecutionState()
		{
			if (Transaction.Current != null)
			{
				throw new InvalidOperationException(Strings.ExecutionStrategy_ExistingTransaction(base.GetType().Name));
			}
			this._exceptionsEncountered.Clear();
		}

		// Token: 0x060016BA RID: 5818 RVA: 0x0006F4B0 File Offset: 0x0006D6B0
		protected internal virtual TimeSpan? GetNextDelay(Exception lastException)
		{
			this._exceptionsEncountered.Add(lastException);
			int num = this._exceptionsEncountered.Count - 1;
			if (num < this._maxRetryCount)
			{
				double num2 = (Math.Pow(2.0, (double)num) - 1.0) * (1.0 + this._random.NextDouble() * 0.10000000000000009);
				double value = Math.Min(DbExecutionStrategy.DefaultCoefficient.TotalMilliseconds * num2, this._maxDelay.TotalMilliseconds);
				return new TimeSpan?(TimeSpan.FromMilliseconds(value));
			}
			return null;
		}

		// Token: 0x060016BB RID: 5819 RVA: 0x0006F558 File Offset: 0x0006D758
		public static T UnwrapAndHandleException<T>(Exception exception, Func<Exception, T> exceptionHandler)
		{
			EntityException ex = exception as EntityException;
			if (ex != null)
			{
				return DbExecutionStrategy.UnwrapAndHandleException<T>(ex.InnerException, exceptionHandler);
			}
			DbUpdateException ex2 = exception as DbUpdateException;
			if (ex2 != null)
			{
				return DbExecutionStrategy.UnwrapAndHandleException<T>(ex2.InnerException, exceptionHandler);
			}
			UpdateException ex3 = exception as UpdateException;
			if (ex3 != null)
			{
				return DbExecutionStrategy.UnwrapAndHandleException<T>(ex3.InnerException, exceptionHandler);
			}
			return exceptionHandler(exception);
		}

		// Token: 0x060016BC RID: 5820
		protected internal abstract bool ShouldRetryOn(Exception exception);

		// Token: 0x04000810 RID: 2064
		private const int DefaultMaxRetryCount = 5;

		// Token: 0x04000811 RID: 2065
		private const double DefaultRandomFactor = 1.1;

		// Token: 0x04000812 RID: 2066
		private const double DefaultExponentialBase = 2.0;

		// Token: 0x04000813 RID: 2067
		private readonly List<Exception> _exceptionsEncountered = new List<Exception>();

		// Token: 0x04000814 RID: 2068
		private readonly Random _random = new Random();

		// Token: 0x04000815 RID: 2069
		private readonly int _maxRetryCount;

		// Token: 0x04000816 RID: 2070
		private readonly TimeSpan _maxDelay;

		// Token: 0x04000817 RID: 2071
		private static readonly TimeSpan DefaultCoefficient = TimeSpan.FromSeconds(1.0);

		// Token: 0x04000818 RID: 2072
		private static readonly TimeSpan DefaultMaxDelay = TimeSpan.FromSeconds(30.0);
	}
}
