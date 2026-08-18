using System;
using System.Collections.Generic;
using System.Threading;

namespace System.Linq.Parallel
{
	// Token: 0x020001B4 RID: 436
	internal sealed class NullableDecimalMinMaxAggregationOperator : InlinedAggregationOperator<decimal?, decimal?, decimal?>
	{
		// Token: 0x06000EC1 RID: 3777 RVA: 0x000348F7 File Offset: 0x00032AF7
		internal NullableDecimalMinMaxAggregationOperator(IEnumerable<decimal?> child, int sign) : base(child)
		{
			this.m_sign = sign;
		}

		// Token: 0x06000EC2 RID: 3778 RVA: 0x00034908 File Offset: 0x00032B08
		protected override decimal? InternalAggregate(ref Exception singularExceptionToThrow)
		{
			decimal? num;
			using (IEnumerator<decimal?> enumerator = this.GetEnumerator(new ParallelMergeOptions?(ParallelMergeOptions.FullyBuffered), true))
			{
				if (!enumerator.MoveNext())
				{
					num = null;
					num = num;
				}
				else
				{
					decimal? num2 = enumerator.Current;
					if (this.m_sign == -1)
					{
						while (enumerator.MoveNext())
						{
							decimal? num3 = enumerator.Current;
							if (num2 != null)
							{
								decimal? num4 = num3;
								decimal? num5 = num2;
								if (!(num4.GetValueOrDefault() < num5.GetValueOrDefault() & (num4 != null & num5 != null)))
								{
									continue;
								}
							}
							num2 = num3;
						}
					}
					else
					{
						while (enumerator.MoveNext())
						{
							decimal? num6 = enumerator.Current;
							if (num2 != null)
							{
								decimal? num5 = num6;
								decimal? num4 = num2;
								if (!(num5.GetValueOrDefault() > num4.GetValueOrDefault() & (num5 != null & num4 != null)))
								{
									continue;
								}
							}
							num2 = num6;
						}
					}
					num = num2;
				}
			}
			return num;
		}

		// Token: 0x06000EC3 RID: 3779 RVA: 0x00034A00 File Offset: 0x00032C00
		protected override QueryOperatorEnumerator<decimal?, int> CreateEnumerator<TKey>(int index, int count, QueryOperatorEnumerator<decimal?, TKey> source, object sharedData, CancellationToken cancellationToken)
		{
			return new NullableDecimalMinMaxAggregationOperator.NullableDecimalMinMaxAggregationOperatorEnumerator<TKey>(source, index, this.m_sign, cancellationToken);
		}

		// Token: 0x0400089F RID: 2207
		private readonly int m_sign;

		// Token: 0x020003DA RID: 986
		private class NullableDecimalMinMaxAggregationOperatorEnumerator<TKey> : InlinedAggregationOperatorEnumerator<decimal?>
		{
			// Token: 0x06001DCE RID: 7630 RVA: 0x0006A972 File Offset: 0x00068B72
			internal NullableDecimalMinMaxAggregationOperatorEnumerator(QueryOperatorEnumerator<decimal?, TKey> source, int partitionIndex, int sign, CancellationToken cancellationToken) : base(partitionIndex, cancellationToken)
			{
				this.m_source = source;
				this.m_sign = sign;
			}

			// Token: 0x06001DCF RID: 7631 RVA: 0x0006A98C File Offset: 0x00068B8C
			protected override bool MoveNextCore(ref decimal? currentElement)
			{
				QueryOperatorEnumerator<decimal?, TKey> source = this.m_source;
				TKey tkey = default(TKey);
				if (source.MoveNext(ref currentElement, ref tkey))
				{
					int num = 0;
					if (this.m_sign == -1)
					{
						decimal? num2 = null;
						while (source.MoveNext(ref num2, ref tkey))
						{
							if ((num++ & 63) == 0)
							{
								CancellationState.ThrowIfCanceled(this.m_cancellationToken);
							}
							if (currentElement != null)
							{
								decimal? num3 = num2;
								decimal? num4 = currentElement;
								if (!(num3.GetValueOrDefault() < num4.GetValueOrDefault() & (num3 != null & num4 != null)))
								{
									continue;
								}
							}
							currentElement = num2;
						}
					}
					else
					{
						decimal? num5 = null;
						while (source.MoveNext(ref num5, ref tkey))
						{
							if ((num++ & 63) == 0)
							{
								CancellationState.ThrowIfCanceled(this.m_cancellationToken);
							}
							if (currentElement != null)
							{
								decimal? num4 = num5;
								decimal? num3 = currentElement;
								if (!(num4.GetValueOrDefault() > num3.GetValueOrDefault() & (num4 != null & num3 != null)))
								{
									continue;
								}
							}
							currentElement = num5;
						}
					}
					return true;
				}
				return false;
			}

			// Token: 0x06001DD0 RID: 7632 RVA: 0x0006AA9C File Offset: 0x00068C9C
			protected override void Dispose(bool disposing)
			{
				this.m_source.Dispose();
			}

			// Token: 0x04001196 RID: 4502
			private QueryOperatorEnumerator<decimal?, TKey> m_source;

			// Token: 0x04001197 RID: 4503
			private int m_sign;
		}
	}
}
