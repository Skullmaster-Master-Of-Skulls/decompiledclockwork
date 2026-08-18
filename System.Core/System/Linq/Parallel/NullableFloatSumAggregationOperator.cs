using System;
using System.Collections.Generic;
using System.Threading;

namespace System.Linq.Parallel
{
	// Token: 0x020001BB RID: 443
	internal sealed class NullableFloatSumAggregationOperator : InlinedAggregationOperator<float?, double?, float?>
	{
		// Token: 0x06000ED6 RID: 3798 RVA: 0x00034F11 File Offset: 0x00033111
		internal NullableFloatSumAggregationOperator(IEnumerable<float?> child) : base(child)
		{
		}

		// Token: 0x06000ED7 RID: 3799 RVA: 0x00034F1C File Offset: 0x0003311C
		protected override float? InternalAggregate(ref Exception singularExceptionToThrow)
		{
			float? result;
			using (IEnumerator<double?> enumerator = this.GetEnumerator(new ParallelMergeOptions?(ParallelMergeOptions.FullyBuffered), true))
			{
				double num = 0.0;
				while (enumerator.MoveNext())
				{
					double num2 = num;
					double? num3 = enumerator.Current;
					num = num2 + num3.GetValueOrDefault();
				}
				result = new float?((float)num);
			}
			return result;
		}

		// Token: 0x06000ED8 RID: 3800 RVA: 0x00034F84 File Offset: 0x00033184
		protected override QueryOperatorEnumerator<double?, int> CreateEnumerator<TKey>(int index, int count, QueryOperatorEnumerator<float?, TKey> source, object sharedData, CancellationToken cancellationToken)
		{
			return new NullableFloatSumAggregationOperator.NullableFloatSumAggregationOperatorEnumerator<TKey>(source, index, cancellationToken);
		}

		// Token: 0x020003E1 RID: 993
		private class NullableFloatSumAggregationOperatorEnumerator<TKey> : InlinedAggregationOperatorEnumerator<double?>
		{
			// Token: 0x06001DE3 RID: 7651 RVA: 0x0006AFEB File Offset: 0x000691EB
			internal NullableFloatSumAggregationOperatorEnumerator(QueryOperatorEnumerator<float?, TKey> source, int partitionIndex, CancellationToken cancellationToken) : base(partitionIndex, cancellationToken)
			{
				this.m_source = source;
			}

			// Token: 0x06001DE4 RID: 7652 RVA: 0x0006AFFC File Offset: 0x000691FC
			protected override bool MoveNextCore(ref double? currentElement)
			{
				float? num = null;
				TKey tkey = default(TKey);
				QueryOperatorEnumerator<float?, TKey> source = this.m_source;
				if (source.MoveNext(ref num, ref tkey))
				{
					float num2 = 0f;
					int num3 = 0;
					do
					{
						if ((num3++ & 63) == 0)
						{
							CancellationState.ThrowIfCanceled(this.m_cancellationToken);
						}
						num2 += num.GetValueOrDefault();
					}
					while (source.MoveNext(ref num, ref tkey));
					currentElement = new double?((double)num2);
					return true;
				}
				return false;
			}

			// Token: 0x06001DE5 RID: 7653 RVA: 0x0006B072 File Offset: 0x00069272
			protected override void Dispose(bool disposing)
			{
				this.m_source.Dispose();
			}

			// Token: 0x040011A0 RID: 4512
			private readonly QueryOperatorEnumerator<float?, TKey> m_source;
		}
	}
}
