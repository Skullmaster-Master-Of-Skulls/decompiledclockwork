using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading;

namespace System.Data.Entity.Core.Common.Utils
{
	// Token: 0x0200032A RID: 810
	[SuppressMessage("Microsoft.Design", "CA1001:TypesThatOwnDisposableFieldsShouldBeDisposable")]
	internal sealed class Memoizer<TArg, TResult>
	{
		// Token: 0x06001BE5 RID: 7141 RVA: 0x000894E0 File Offset: 0x000876E0
		internal Memoizer(Func<TArg, TResult> function, IEqualityComparer<TArg> argComparer)
		{
			this._function = function;
			this._resultCache = new Dictionary<TArg, Memoizer<TArg, TResult>.Result>(argComparer);
			this._lock = new ReaderWriterLockSlim();
		}

		// Token: 0x06001BE6 RID: 7142 RVA: 0x00089528 File Offset: 0x00087728
		internal TResult Evaluate(TArg arg)
		{
			Memoizer<TArg, TResult>.Result result;
			if (!this.TryGetResult(arg, out result))
			{
				this._lock.EnterWriteLock();
				try
				{
					if (!this._resultCache.TryGetValue(arg, out result))
					{
						result = new Memoizer<TArg, TResult>.Result(() => this._function(arg));
						this._resultCache.Add(arg, result);
					}
				}
				finally
				{
					this._lock.ExitWriteLock();
				}
			}
			return result.GetValue();
		}

		// Token: 0x06001BE7 RID: 7143 RVA: 0x000895C8 File Offset: 0x000877C8
		internal bool TryGetValue(TArg arg, out TResult value)
		{
			Memoizer<TArg, TResult>.Result result;
			if (this.TryGetResult(arg, out result))
			{
				value = result.GetValue();
				return true;
			}
			value = default(TResult);
			return false;
		}

		// Token: 0x06001BE8 RID: 7144 RVA: 0x000895F8 File Offset: 0x000877F8
		private bool TryGetResult(TArg arg, out Memoizer<TArg, TResult>.Result result)
		{
			this._lock.EnterReadLock();
			bool result2;
			try
			{
				result2 = this._resultCache.TryGetValue(arg, out result);
			}
			finally
			{
				this._lock.ExitReadLock();
			}
			return result2;
		}

		// Token: 0x040009BB RID: 2491
		private readonly Func<TArg, TResult> _function;

		// Token: 0x040009BC RID: 2492
		private readonly Dictionary<TArg, Memoizer<TArg, TResult>.Result> _resultCache;

		// Token: 0x040009BD RID: 2493
		private readonly ReaderWriterLockSlim _lock;

		// Token: 0x0200032B RID: 811
		private class Result
		{
			// Token: 0x06001BE9 RID: 7145 RVA: 0x00089640 File Offset: 0x00087840
			internal Result(Func<TResult> createValueDelegate)
			{
				this._delegate = createValueDelegate;
			}

			// Token: 0x06001BEA RID: 7146 RVA: 0x00089650 File Offset: 0x00087850
			internal TResult GetValue()
			{
				if (this._delegate == null)
				{
					return this._value;
				}
				TResult value;
				lock (this)
				{
					if (this._delegate == null)
					{
						value = this._value;
					}
					else
					{
						this._value = this._delegate();
						this._delegate = null;
						value = this._value;
					}
				}
				return value;
			}

			// Token: 0x040009BE RID: 2494
			private TResult _value;

			// Token: 0x040009BF RID: 2495
			private Func<TResult> _delegate;
		}
	}
}
