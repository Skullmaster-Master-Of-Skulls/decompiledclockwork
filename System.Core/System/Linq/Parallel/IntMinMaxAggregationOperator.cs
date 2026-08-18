using System;
using System.Collections.Generic;
using System.Threading;

namespace System.Linq.Parallel
{
	// Token: 0x020001AE RID: 430
	internal sealed class IntMinMaxAggregationOperator : InlinedAggregationOperator<int, int, int>
	{
		// Token: 0x06000EAF RID: 3759 RVA: 0x0003451B File Offset: 0x0003271B
		internal IntMinMaxAggregationOperator(IEnumerable<int> child, int sign) : base(child)
		{
			this.m_sign = sign;
		}

		// Token: 0x06000EB0 RID: 3760 RVA: 0x0003452C File Offset: 0x0003272C
		protected override int InternalAggregate(ref Exception singularExceptionToThrow)
		{
			int result;
			using (IEnumerator<int> enumerator = this.GetEnumerator(new ParallelMergeOptions?(ParallelMergeOptions.FullyBuffered), true))
			{
				if (!enumerator.MoveNext())
				{
					singularExceptionToThrow = new InvalidOperationException(SR.GetString("NoElements"));
					result = 0;
				}
				else
				{
					int num = enumerator.Current;
					if (this.m_sign == -1)
					{
						while (enumerator.MoveNext())
						{
							int num2 = enumerator.Current;
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
							int num3 = enumerator.Current;
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

		// Token: 0x06000EB1 RID: 3761 RVA: 0x000345C4 File Offset: 0x000327C4
		protected override QueryOperatorEnumerator<int, int> CreateEnumerator<TKey>(int index, int count, QueryOperatorEnumerator<int, TKey> source, object sharedData, CancellationToken cancellationToken)
		{
			return new IntMinMaxAggregationOperator.IntMinMaxAggregationOperatorEnumerator<TKey>(source, index, this.m_sign, cancellationToken);
		}

		// Token: 0x0400089D RID: 2205
		private readonly int m_sign;

		// Token: 0x020003D4 RID: 980
		private class IntMinMaxAggregationOperatorEnumerator<TKey> : InlinedAggregationOperatorEnumerator<int>
		{
			// Token: 0x06001DBC RID: 7612 RVA: 0x0006A5C9 File Offset: 0x000687C9
			internal IntMinMaxAggregationOperatorEnumerator(QueryOperatorEnumerator<int, TKey> source, int partitionIndex, int sign, CancellationToken cancellationToken) : base(partitionIndex, cancellationToken)
			{
				this.m_source = source;
				this.m_sign = sign;
			}

			// Token: 0x06001DBD RID: 7613 RVA: 0x0006A5E4 File Offset: 0x000687E4
			protected override bool MoveNextCore(ref int currentElement)
			{
				QueryOperatorEnumerator<int, TKey> source = this.m_source;
				TKey tkey = default(TKey);
				if (source.MoveNext(ref currentElement, ref tkey))
				{
					int num = 0;
					if (this.m_sign == -1)
					{
						int num2 = 0;
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
						int num3 = 0;
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

			// Token: 0x06001DBE RID: 7614 RVA: 0x0006A678 File Offset: 0x00068878
			protected override void Dispose(bool disposing)
			{
				this.m_source.Dispose();
			}

			// Token: 0x0400118E RID: 4494
			private readonly QueryOperatorEnumerator<int, TKey> m_source;

			// Token: 0x0400118F RID: 4495
			private readonly int m_sign;
		}
	}
}
