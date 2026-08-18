using System;
using System.Collections.Generic;
using System.Threading;

namespace System.Linq.Parallel
{
	// Token: 0x020001F7 RID: 503
	internal static class ExceptionAggregator
	{
		// Token: 0x0600100D RID: 4109 RVA: 0x00038AA5 File Offset: 0x00036CA5
		internal static IEnumerable<TElement> WrapEnumerable<TElement>(IEnumerable<TElement> source, CancellationState cancellationState)
		{
			using (IEnumerator<TElement> enumerator = source.GetEnumerator())
			{
				for (;;)
				{
					TElement telement = default(TElement);
					try
					{
						if (!enumerator.MoveNext())
						{
							yield break;
						}
						telement = enumerator.Current;
					}
					catch (ThreadAbortException)
					{
						throw;
					}
					catch (Exception ex)
					{
						ExceptionAggregator.ThrowOCEorAggregateException(ex, cancellationState);
					}
					yield return telement;
				}
			}
			yield break;
			yield break;
		}

		// Token: 0x0600100E RID: 4110 RVA: 0x00038ABC File Offset: 0x00036CBC
		internal static IEnumerable<TElement> WrapQueryEnumerator<TElement, TIgnoreKey>(QueryOperatorEnumerator<TElement, TIgnoreKey> source, CancellationState cancellationState)
		{
			TElement elem = default(TElement);
			TIgnoreKey ignoreKey = default(TIgnoreKey);
			try
			{
				for (;;)
				{
					try
					{
						if (!source.MoveNext(ref elem, ref ignoreKey))
						{
							yield break;
						}
					}
					catch (ThreadAbortException)
					{
						throw;
					}
					catch (Exception ex)
					{
						ExceptionAggregator.ThrowOCEorAggregateException(ex, cancellationState);
					}
					yield return elem;
				}
			}
			finally
			{
				source.Dispose();
			}
			yield break;
			yield break;
		}

		// Token: 0x0600100F RID: 4111 RVA: 0x00038AD3 File Offset: 0x00036CD3
		internal static void ThrowOCEorAggregateException(Exception ex, CancellationState cancellationState)
		{
			if (ExceptionAggregator.ThrowAnOCE(ex, cancellationState))
			{
				CancellationState.ThrowWithStandardMessageIfCanceled(cancellationState.ExternalCancellationToken);
				return;
			}
			throw new AggregateException(new Exception[]
			{
				ex
			});
		}

		// Token: 0x06001010 RID: 4112 RVA: 0x00038AFC File Offset: 0x00036CFC
		internal static Func<T, U> WrapFunc<T, U>(Func<T, U> f, CancellationState cancellationState)
		{
			return delegate(T t)
			{
				U result = default(U);
				try
				{
					result = f(t);
				}
				catch (ThreadAbortException)
				{
					throw;
				}
				catch (Exception ex)
				{
					ExceptionAggregator.ThrowOCEorAggregateException(ex, cancellationState);
				}
				return result;
			};
		}

		// Token: 0x06001011 RID: 4113 RVA: 0x00038B2C File Offset: 0x00036D2C
		private static bool ThrowAnOCE(Exception ex, CancellationState cancellationState)
		{
			OperationCanceledException ex2 = ex as OperationCanceledException;
			return (ex2 != null && ex2.CancellationToken == cancellationState.ExternalCancellationToken && cancellationState.ExternalCancellationToken.IsCancellationRequested) || (ex2 != null && ex2.CancellationToken == cancellationState.MergedCancellationToken && cancellationState.MergedCancellationToken.IsCancellationRequested && cancellationState.ExternalCancellationToken.IsCancellationRequested);
		}
	}
}
