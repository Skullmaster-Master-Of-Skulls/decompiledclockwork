using System;
using System.Collections.Generic;
using System.Threading;

namespace System.Linq.Parallel
{
	// Token: 0x020001A3 RID: 419
	internal sealed class DecimalMinMaxAggregationOperator : InlinedAggregationOperator<decimal, decimal, decimal>
	{
		// Token: 0x06000E89 RID: 3721 RVA: 0x00033D8F File Offset: 0x00031F8F
		internal DecimalMinMaxAggregationOperator(IEnumerable<decimal> child, int sign) : base(child)
		{
			this.m_sign = sign;
		}

		// Token: 0x06000E8A RID: 3722 RVA: 0x00033DA0 File Offset: 0x00031FA0
		protected override decimal InternalAggregate(ref Exception singularExceptionToThrow)
		{
			decimal result;
			using (IEnumerator<decimal> enumerator = this.GetEnumerator(new ParallelMergeOptions?(ParallelMergeOptions.FullyBuffered), true))
			{
				if (!enumerator.MoveNext())
				{
					singularExceptionToThrow = new InvalidOperationException(SR.GetString("NoElements"));
					result = 0m;
				}
				else
				{
					decimal num = enumerator.Current;
					if (this.m_sign == -1)
					{
						while (enumerator.MoveNext())
						{
							decimal num2 = enumerator.Current;
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
							decimal num3 = enumerator.Current;
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

		// Token: 0x06000E8B RID: 3723 RVA: 0x00033E48 File Offset: 0x00032048
		protected override QueryOperatorEnumerator<decimal, int> CreateEnumerator<TKey>(int index, int count, QueryOperatorEnumerator<decimal, TKey> source, object sharedData, CancellationToken cancellationToken)
		{
			return new DecimalMinMaxAggregationOperator.DecimalMinMaxAggregationOperatorEnumerator<TKey>(source, index, this.m_sign, cancellationToken);
		}

		// Token: 0x04000897 RID: 2199
		private readonly int m_sign;

		// Token: 0x020003CB RID: 971
		private class DecimalMinMaxAggregationOperatorEnumerator<TKey> : InlinedAggregationOperatorEnumerator<decimal>
		{
			// Token: 0x06001DA1 RID: 7585 RVA: 0x00069FBF File Offset: 0x000681BF
			internal DecimalMinMaxAggregationOperatorEnumerator(QueryOperatorEnumerator<decimal, TKey> source, int partitionIndex, int sign, CancellationToken cancellationToken) : base(partitionIndex, cancellationToken)
			{
				this.m_source = source;
				this.m_sign = sign;
			}

			// Token: 0x06001DA2 RID: 7586 RVA: 0x00069FD8 File Offset: 0x000681D8
			protected override bool MoveNextCore(ref decimal currentElement)
			{
				QueryOperatorEnumerator<decimal, TKey> source = this.m_source;
				TKey tkey = default(TKey);
				if (source.MoveNext(ref currentElement, ref tkey))
				{
					int num = 0;
					if (this.m_sign == -1)
					{
						decimal num2 = 0m;
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
						decimal num3 = 0m;
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

			// Token: 0x06001DA3 RID: 7587 RVA: 0x0006A094 File Offset: 0x00068294
			protected override void Dispose(bool disposing)
			{
				this.m_source.Dispose();
			}

			// Token: 0x04001182 RID: 4482
			private QueryOperatorEnumerator<decimal, TKey> m_source;

			// Token: 0x04001183 RID: 4483
			private int m_sign;
		}
	}
}
