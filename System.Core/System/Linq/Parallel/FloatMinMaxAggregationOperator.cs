using System;
using System.Collections.Generic;
using System.Threading;

namespace System.Linq.Parallel
{
	// Token: 0x020001A9 RID: 425
	internal sealed class FloatMinMaxAggregationOperator : InlinedAggregationOperator<float, float, float>
	{
		// Token: 0x06000E9B RID: 3739 RVA: 0x000341A3 File Offset: 0x000323A3
		internal FloatMinMaxAggregationOperator(IEnumerable<float> child, int sign) : base(child)
		{
			this.m_sign = sign;
		}

		// Token: 0x06000E9C RID: 3740 RVA: 0x000341B4 File Offset: 0x000323B4
		protected override float InternalAggregate(ref Exception singularExceptionToThrow)
		{
			float result;
			using (IEnumerator<float> enumerator = this.GetEnumerator(new ParallelMergeOptions?(ParallelMergeOptions.FullyBuffered), true))
			{
				if (!enumerator.MoveNext())
				{
					singularExceptionToThrow = new InvalidOperationException(SR.GetString("NoElements"));
					result = 0f;
				}
				else
				{
					float num = enumerator.Current;
					if (this.m_sign == -1)
					{
						while (enumerator.MoveNext())
						{
							float num2 = enumerator.Current;
							if (num2 < num || float.IsNaN(num2))
							{
								num = num2;
							}
						}
					}
					else
					{
						while (enumerator.MoveNext())
						{
							float num3 = enumerator.Current;
							if (num3 > num || float.IsNaN(num))
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

		// Token: 0x06000E9D RID: 3741 RVA: 0x00034260 File Offset: 0x00032460
		protected override QueryOperatorEnumerator<float, int> CreateEnumerator<TKey>(int index, int count, QueryOperatorEnumerator<float, TKey> source, object sharedData, CancellationToken cancellationToken)
		{
			return new FloatMinMaxAggregationOperator.FloatMinMaxAggregationOperatorEnumerator<TKey>(source, index, this.m_sign, cancellationToken);
		}

		// Token: 0x04000899 RID: 2201
		private readonly int m_sign;

		// Token: 0x020003D1 RID: 977
		private class FloatMinMaxAggregationOperatorEnumerator<TKey> : InlinedAggregationOperatorEnumerator<float>
		{
			// Token: 0x06001DB3 RID: 7603 RVA: 0x0006A3D8 File Offset: 0x000685D8
			internal FloatMinMaxAggregationOperatorEnumerator(QueryOperatorEnumerator<float, TKey> source, int partitionIndex, int sign, CancellationToken cancellationToken) : base(partitionIndex, cancellationToken)
			{
				this.m_source = source;
				this.m_sign = sign;
			}

			// Token: 0x06001DB4 RID: 7604 RVA: 0x0006A3F4 File Offset: 0x000685F4
			protected override bool MoveNextCore(ref float currentElement)
			{
				QueryOperatorEnumerator<float, TKey> source = this.m_source;
				TKey tkey = default(TKey);
				if (source.MoveNext(ref currentElement, ref tkey))
				{
					int num = 0;
					if (this.m_sign == -1)
					{
						float num2 = 0f;
						while (source.MoveNext(ref num2, ref tkey))
						{
							if ((num++ & 63) == 0)
							{
								CancellationState.ThrowIfCanceled(this.m_cancellationToken);
							}
							if (num2 < currentElement || float.IsNaN(num2))
							{
								currentElement = num2;
							}
						}
					}
					else
					{
						float num3 = 0f;
						while (source.MoveNext(ref num3, ref tkey))
						{
							if ((num++ & 63) == 0)
							{
								CancellationState.ThrowIfCanceled(this.m_cancellationToken);
							}
							if (num3 > currentElement || float.IsNaN(currentElement))
							{
								currentElement = num3;
							}
						}
					}
					return true;
				}
				return false;
			}

			// Token: 0x06001DB5 RID: 7605 RVA: 0x0006A4A4 File Offset: 0x000686A4
			protected override void Dispose(bool disposing)
			{
				this.m_source.Dispose();
			}

			// Token: 0x0400118A RID: 4490
			private QueryOperatorEnumerator<float, TKey> m_source;

			// Token: 0x0400118B RID: 4491
			private int m_sign;
		}
	}
}
