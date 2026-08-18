using System;
using System.Collections.Generic;
using System.Threading;

namespace System.Linq.Parallel
{
	// Token: 0x020001B1 RID: 433
	internal sealed class LongMinMaxAggregationOperator : InlinedAggregationOperator<long, long, long>
	{
		// Token: 0x06000EB8 RID: 3768 RVA: 0x00034707 File Offset: 0x00032907
		internal LongMinMaxAggregationOperator(IEnumerable<long> child, int sign) : base(child)
		{
			this.m_sign = sign;
		}

		// Token: 0x06000EB9 RID: 3769 RVA: 0x00034718 File Offset: 0x00032918
		protected override long InternalAggregate(ref Exception singularExceptionToThrow)
		{
			long result;
			using (IEnumerator<long> enumerator = this.GetEnumerator(new ParallelMergeOptions?(ParallelMergeOptions.FullyBuffered), true))
			{
				if (!enumerator.MoveNext())
				{
					singularExceptionToThrow = new InvalidOperationException(SR.GetString("NoElements"));
					result = 0L;
				}
				else
				{
					long num = enumerator.Current;
					if (this.m_sign == -1)
					{
						while (enumerator.MoveNext())
						{
							long num2 = enumerator.Current;
							if (num2 < num)
							{
								num = num2;
							}
						}
					}
					else
					{
						while (enumerator.MoveNext())
						{
							long num3 = enumerator.Current;
							if (num3 > num)
							{
								num = num3;
							}
						}
					}
					result = num;
				}
			}
			return result;
		}

		// Token: 0x06000EBA RID: 3770 RVA: 0x000347B4 File Offset: 0x000329B4
		protected override QueryOperatorEnumerator<long, int> CreateEnumerator<TKey>(int index, int count, QueryOperatorEnumerator<long, TKey> source, object sharedData, CancellationToken cancellationToken)
		{
			return new LongMinMaxAggregationOperator.LongMinMaxAggregationOperatorEnumerator<TKey>(source, index, this.m_sign, cancellationToken);
		}

		// Token: 0x0400089E RID: 2206
		private readonly int m_sign;

		// Token: 0x020003D7 RID: 983
		private class LongMinMaxAggregationOperatorEnumerator<TKey> : InlinedAggregationOperatorEnumerator<long>
		{
			// Token: 0x06001DC5 RID: 7621 RVA: 0x0006A791 File Offset: 0x00068991
			internal LongMinMaxAggregationOperatorEnumerator(QueryOperatorEnumerator<long, TKey> source, int partitionIndex, int sign, CancellationToken cancellationToken) : base(partitionIndex, cancellationToken)
			{
				this.m_source = source;
				this.m_sign = sign;
			}

			// Token: 0x06001DC6 RID: 7622 RVA: 0x0006A7AC File Offset: 0x000689AC
			protected override bool MoveNextCore(ref long currentElement)
			{
				QueryOperatorEnumerator<long, TKey> source = this.m_source;
				TKey tkey = default(TKey);
				if (source.MoveNext(ref currentElement, ref tkey))
				{
					int num = 0;
					if (this.m_sign == -1)
					{
						long num2 = 0L;
						while (source.MoveNext(ref num2, ref tkey))
						{
							if ((num++ & 63) == 0)
							{
								CancellationState.ThrowIfCanceled(this.m_cancellationToken);
							}
							if (num2 < currentElement)
							{
								currentElement = num2;
							}
						}
					}
					else
					{
						long num3 = 0L;
						while (source.MoveNext(ref num3, ref tkey))
						{
							if ((num++ & 63) == 0)
							{
								CancellationState.ThrowIfCanceled(this.m_cancellationToken);
							}
							if (num3 > currentElement)
							{
								currentElement = num3;
							}
						}
					}
					return true;
				}
				return false;
			}

			// Token: 0x06001DC7 RID: 7623 RVA: 0x0006A842 File Offset: 0x00068A42
			protected override void Dispose(bool disposing)
			{
				this.m_source.Dispose();
			}

			// Token: 0x04001192 RID: 4498
			private QueryOperatorEnumerator<long, TKey> m_source;

			// Token: 0x04001193 RID: 4499
			private int m_sign;
		}
	}
}
