using System;
using System.Collections.Generic;
using System.Threading;

namespace System.Linq.Parallel
{
	// Token: 0x020001BD RID: 445
	internal sealed class NullableIntMinMaxAggregationOperator : InlinedAggregationOperator<int?, int?, int?>
	{
		// Token: 0x06000EDC RID: 3804 RVA: 0x0003504F File Offset: 0x0003324F
		internal NullableIntMinMaxAggregationOperator(IEnumerable<int?> child, int sign) : base(child)
		{
			this.m_sign = sign;
		}

		// Token: 0x06000EDD RID: 3805 RVA: 0x00035060 File Offset: 0x00033260
		protected override int? InternalAggregate(ref Exception singularExceptionToThrow)
		{
			int? num;
			using (IEnumerator<int?> enumerator = this.GetEnumerator(new ParallelMergeOptions?(ParallelMergeOptions.FullyBuffered), true))
			{
				if (!enumerator.MoveNext())
				{
					num = null;
					num = num;
				}
				else
				{
					int? num2 = enumerator.Current;
					if (this.m_sign == -1)
					{
						while (enumerator.MoveNext())
						{
							int? num3 = enumerator.Current;
							if (num2 != null)
							{
								int? num4 = num3;
								int? num5 = num2;
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
							int? num6 = enumerator.Current;
							if (num2 != null)
							{
								int? num5 = num6;
								int? num4 = num2;
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

		// Token: 0x06000EDE RID: 3806 RVA: 0x00035150 File Offset: 0x00033350
		protected override QueryOperatorEnumerator<int?, int> CreateEnumerator<TKey>(int index, int count, QueryOperatorEnumerator<int?, TKey> source, object sharedData, CancellationToken cancellationToken)
		{
			return new NullableIntMinMaxAggregationOperator.NullableIntMinMaxAggregationOperatorEnumerator<TKey>(source, index, this.m_sign, cancellationToken);
		}

		// Token: 0x040008A2 RID: 2210
		private readonly int m_sign;

		// Token: 0x020003E3 RID: 995
		private class NullableIntMinMaxAggregationOperatorEnumerator<TKey> : InlinedAggregationOperatorEnumerator<int?>
		{
			// Token: 0x06001DE9 RID: 7657 RVA: 0x0006B11A File Offset: 0x0006931A
			internal NullableIntMinMaxAggregationOperatorEnumerator(QueryOperatorEnumerator<int?, TKey> source, int partitionIndex, int sign, CancellationToken cancellationToken) : base(partitionIndex, cancellationToken)
			{
				this.m_source = source;
				this.m_sign = sign;
			}

			// Token: 0x06001DEA RID: 7658 RVA: 0x0006B134 File Offset: 0x00069334
			protected override bool MoveNextCore(ref int? currentElement)
			{
				QueryOperatorEnumerator<int?, TKey> source = this.m_source;
				TKey tkey = default(TKey);
				if (source.MoveNext(ref currentElement, ref tkey))
				{
					int num = 0;
					if (this.m_sign == -1)
					{
						int? num2 = null;
						while (source.MoveNext(ref num2, ref tkey))
						{
							if ((num++ & 63) == 0)
							{
								CancellationState.ThrowIfCanceled(this.m_cancellationToken);
							}
							if (currentElement != null)
							{
								int? num3 = num2;
								int? num4 = currentElement;
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
						int? num5 = null;
						while (source.MoveNext(ref num5, ref tkey))
						{
							if ((num++ & 63) == 0)
							{
								CancellationState.ThrowIfCanceled(this.m_cancellationToken);
							}
							if (currentElement != null)
							{
								int? num4 = num5;
								int? num3 = currentElement;
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

			// Token: 0x06001DEB RID: 7659 RVA: 0x0006B23E File Offset: 0x0006943E
			protected override void Dispose(bool disposing)
			{
				this.m_source.Dispose();
			}

			// Token: 0x040011A2 RID: 4514
			private QueryOperatorEnumerator<int?, TKey> m_source;

			// Token: 0x040011A3 RID: 4515
			private int m_sign;
		}
	}
}
