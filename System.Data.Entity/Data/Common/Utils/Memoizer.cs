using System;
using System.Collections.Generic;
using System.Threading;

namespace System.Data.Common.Utils
{
	// Token: 0x0200038E RID: 910
	internal sealed class Memoizer<TArg, TResult>
	{
		// Token: 0x0600327E RID: 12926 RVA: 0x000C5479 File Offset: 0x000C3679
		internal Memoizer(Func<TArg, TResult> function, IEqualityComparer<TArg> argComparer)
		{
			EntityUtil.CheckArgumentNull<Func<TArg, TResult>>(function, "function");
			this._function = function;
			this._resultCache = new Dictionary<TArg, Memoizer<TArg, TResult>.Result>(argComparer);
			this._lock = new ReaderWriterLockSlim();
		}

		// Token: 0x0600327F RID: 12927 RVA: 0x000C54AC File Offset: 0x000C36AC
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

		// Token: 0x06003280 RID: 12928 RVA: 0x000C5548 File Offset: 0x000C3748
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

		// Token: 0x06003281 RID: 12929 RVA: 0x000C5578 File Offset: 0x000C3778
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

		// Token: 0x04001655 RID: 5717
		private readonly Func<TArg, TResult> _function;

		// Token: 0x04001656 RID: 5718
		private readonly Dictionary<TArg, Memoizer<TArg, TResult>.Result> _resultCache;

		// Token: 0x04001657 RID: 5719
		private readonly ReaderWriterLockSlim _lock;

		// Token: 0x0200066C RID: 1644
		private class Result
		{
			// Token: 0x06004470 RID: 17520 RVA: 0x000F78D8 File Offset: 0x000F5AD8
			internal Result(Func<TResult> createValueDelegate)
			{
				this._delegate = createValueDelegate;
			}

			// Token: 0x06004471 RID: 17521 RVA: 0x000F78E8 File Offset: 0x000F5AE8
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

			// Token: 0x04001F6D RID: 8045
			private TResult _value;

			// Token: 0x04001F6E RID: 8046
			private Func<TResult> _delegate;
		}
	}
}
