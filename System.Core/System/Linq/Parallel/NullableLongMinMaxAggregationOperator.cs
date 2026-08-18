using System;
using System.Collections.Generic;
using System.Threading;

namespace System.Linq.Parallel
{
	// Token: 0x020001C0 RID: 448
	internal sealed class NullableLongMinMaxAggregationOperator : InlinedAggregationOperator<long?, long?, long?>
	{
		// Token: 0x06000EE5 RID: 3813 RVA: 0x00035293 File Offset: 0x00033493
		internal NullableLongMinMaxAggregationOperator(IEnumerable<long?> child, int sign) : base(child)
		{
			this.m_sign = sign;
		}

		// Token: 0x06000EE6 RID: 3814 RVA: 0x000352A4 File Offset: 0x000334A4
		protected override long? InternalAggregate(ref Exception singularExceptionToThrow)
		{
			long? num;
			using (IEnumerator<long?> enumerator = this.GetEnumerator(new ParallelMergeOptions?(ParallelMergeOptions.FullyBuffered), true))
			{
				if (!enumerator.MoveNext())
				{
					num = null;
					num = num;
				}
				else
				{
					long? num2 = enumerator.Current;
					if (this.m_sign == -1)
					{
						while (enumerator.MoveNext())
						{
							long? num3 = enumerator.Current;
							if (num2 != null)
							{
								long? num4 = num3;
								long? num5 = num2;
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
							long? num6 = enumerator.Current;
							if (num2 != null)
							{
								long? num5 = num6;
								long? num4 = num2;
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

		// Token: 0x06000EE7 RID: 3815 RVA: 0x00035394 File Offset: 0x00033594
		protected override QueryOperatorEnumerator<long?, int> CreateEnumerator<TKey>(int index, int count, QueryOperatorEnumerator<long?, TKey> source, object sharedData, CancellationToken cancellationToken)
		{
			return new NullableLongMinMaxAggregationOperator.NullableLongMinMaxAggregationOperatorEnumerator<TKey>(source, index, this.m_sign, cancellationToken);
		}

		// Token: 0x040008A3 RID: 2211
		private readonly int m_sign;

		// Token: 0x020003E6 RID: 998
		private class NullableLongMinMaxAggregationOperatorEnumerator<TKey> : InlinedAggregationOperatorEnumerator<long?>
		{
			// Token: 0x06001DF2 RID: 7666 RVA: 0x0006B375 File Offset: 0x00069575
			internal NullableLongMinMaxAggregationOperatorEnumerator(QueryOperatorEnumerator<long?, TKey> source, int partitionIndex, int sign, CancellationToken cancellationToken) : base(partitionIndex, cancellationToken)
			{
				this.m_source = source;
				this.m_sign = sign;
			}

			// Token: 0x06001DF3 RID: 7667 RVA: 0x0006B390 File Offset: 0x00069590
			protected override bool MoveNextCore(ref long? currentElement)
			{
				QueryOperatorEnumerator<long?, TKey> source = this.m_source;
				TKey tkey = default(TKey);
				if (source.MoveNext(ref currentElement, ref tkey))
				{
					int num = 0;
					if (this.m_sign == -1)
					{
						long? num2 = null;
						while (source.MoveNext(ref num2, ref tkey))
						{
							if ((num++ & 63) == 0)
							{
								CancellationState.ThrowIfCanceled(this.m_cancellationToken);
							}
							if (currentElement != null)
							{
								long? num3 = num2;
								long? num4 = currentElement;
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
						long? num5 = null;
						while (source.MoveNext(ref num5, ref tkey))
						{
							if ((num++ & 63) == 0)
							{
								CancellationState.ThrowIfCanceled(this.m_cancellationToken);
							}
							if (currentElement != null)
							{
								long? num4 = num5;
								long? num3 = currentElement;
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

			// Token: 0x06001DF4 RID: 7668 RVA: 0x0006B49A File Offset: 0x0006969A
			protected override void Dispose(bool disposing)
			{
				this.m_source.Dispose();
			}

			// Token: 0x040011A6 RID: 4518
			private QueryOperatorEnumerator<long?, TKey> m_source;

			// Token: 0x040011A7 RID: 4519
			private int m_sign;
		}
	}
}
