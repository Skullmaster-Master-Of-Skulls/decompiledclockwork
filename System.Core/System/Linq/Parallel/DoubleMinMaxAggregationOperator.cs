using System;
using System.Collections.Generic;
using System.Threading;

namespace System.Linq.Parallel
{
	// Token: 0x020001A6 RID: 422
	internal sealed class DoubleMinMaxAggregationOperator : InlinedAggregationOperator<double, double, double>
	{
		// Token: 0x06000E92 RID: 3730 RVA: 0x00033F9B File Offset: 0x0003219B
		internal DoubleMinMaxAggregationOperator(IEnumerable<double> child, int sign) : base(child)
		{
			this.m_sign = sign;
		}

		// Token: 0x06000E93 RID: 3731 RVA: 0x00033FAC File Offset: 0x000321AC
		protected override double InternalAggregate(ref Exception singularExceptionToThrow)
		{
			double result;
			using (IEnumerator<double> enumerator = this.GetEnumerator(new ParallelMergeOptions?(ParallelMergeOptions.FullyBuffered), true))
			{
				if (!enumerator.MoveNext())
				{
					singularExceptionToThrow = new InvalidOperationException(SR.GetString("NoElements"));
					result = 0.0;
				}
				else
				{
					double num = enumerator.Current;
					if (this.m_sign == -1)
					{
						while (enumerator.MoveNext())
						{
							double num2 = enumerator.Current;
							if (num2 < num || double.IsNaN(num2))
							{
								num = num2;
							}
						}
					}
					else
					{
						while (enumerator.MoveNext())
						{
							double num3 = enumerator.Current;
							if (num3 > num || double.IsNaN(num))
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

		// Token: 0x06000E94 RID: 3732 RVA: 0x0003405C File Offset: 0x0003225C
		protected override QueryOperatorEnumerator<double, int> CreateEnumerator<TKey>(int index, int count, QueryOperatorEnumerator<double, TKey> source, object sharedData, CancellationToken cancellationToken)
		{
			return new DoubleMinMaxAggregationOperator.DoubleMinMaxAggregationOperatorEnumerator<TKey>(source, index, this.m_sign, cancellationToken);
		}

		// Token: 0x04000898 RID: 2200
		private readonly int m_sign;

		// Token: 0x020003CE RID: 974
		private class DoubleMinMaxAggregationOperatorEnumerator<TKey> : InlinedAggregationOperatorEnumerator<double>
		{
			// Token: 0x06001DAA RID: 7594 RVA: 0x0006A1D3 File Offset: 0x000683D3
			internal DoubleMinMaxAggregationOperatorEnumerator(QueryOperatorEnumerator<double, TKey> source, int partitionIndex, int sign, CancellationToken cancellationToken) : base(partitionIndex, cancellationToken)
			{
				this.m_source = source;
				this.m_sign = sign;
			}

			// Token: 0x06001DAB RID: 7595 RVA: 0x0006A1EC File Offset: 0x000683EC
			protected override bool MoveNextCore(ref double currentElement)
			{
				QueryOperatorEnumerator<double, TKey> source = this.m_source;
				TKey tkey = default(TKey);
				if (source.MoveNext(ref currentElement, ref tkey))
				{
					int num = 0;
					if (this.m_sign == -1)
					{
						double num2 = 0.0;
						while (source.MoveNext(ref num2, ref tkey))
						{
							if ((num++ & 63) == 0)
							{
								CancellationState.ThrowIfCanceled(this.m_cancellationToken);
							}
							if (num2 < currentElement || double.IsNaN(num2))
							{
								currentElement = num2;
							}
						}
					}
					else
					{
						double num3 = 0.0;
						while (source.MoveNext(ref num3, ref tkey))
						{
							if ((num++ & 63) == 0)
							{
								CancellationState.ThrowIfCanceled(this.m_cancellationToken);
							}
							if (num3 > currentElement || double.IsNaN(currentElement))
							{
								currentElement = num3;
							}
						}
					}
					return true;
				}
				return false;
			}

			// Token: 0x06001DAC RID: 7596 RVA: 0x0006A2A4 File Offset: 0x000684A4
			protected override void Dispose(bool disposing)
			{
				this.m_source.Dispose();
			}

			// Token: 0x04001186 RID: 4486
			private QueryOperatorEnumerator<double, TKey> m_source;

			// Token: 0x04001187 RID: 4487
			private int m_sign;
		}
	}
}
