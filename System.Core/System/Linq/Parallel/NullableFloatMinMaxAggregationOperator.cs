using System;
using System.Collections.Generic;
using System.Threading;

namespace System.Linq.Parallel
{
	// Token: 0x020001BA RID: 442
	internal sealed class NullableFloatMinMaxAggregationOperator : InlinedAggregationOperator<float?, float?, float?>
	{
		// Token: 0x06000ED3 RID: 3795 RVA: 0x00034DCF File Offset: 0x00032FCF
		internal NullableFloatMinMaxAggregationOperator(IEnumerable<float?> child, int sign) : base(child)
		{
			this.m_sign = sign;
		}

		// Token: 0x06000ED4 RID: 3796 RVA: 0x00034DE0 File Offset: 0x00032FE0
		protected override float? InternalAggregate(ref Exception singularExceptionToThrow)
		{
			float? num;
			using (IEnumerator<float?> enumerator = this.GetEnumerator(new ParallelMergeOptions?(ParallelMergeOptions.FullyBuffered), true))
			{
				if (!enumerator.MoveNext())
				{
					num = null;
					num = num;
				}
				else
				{
					float? num2 = enumerator.Current;
					if (this.m_sign == -1)
					{
						while (enumerator.MoveNext())
						{
							float? num3 = enumerator.Current;
							if (num3 != null)
							{
								if (num2 != null)
								{
									float? num4 = num3;
									float? num5 = num2;
									if (!(num4.GetValueOrDefault() < num5.GetValueOrDefault() & (num4 != null & num5 != null)) && !float.IsNaN(num3.GetValueOrDefault()))
									{
										continue;
									}
								}
								num2 = num3;
							}
						}
					}
					else
					{
						while (enumerator.MoveNext())
						{
							float? num6 = enumerator.Current;
							if (num6 != null)
							{
								if (num2 != null)
								{
									float? num5 = num6;
									float? num4 = num2;
									if (!(num5.GetValueOrDefault() > num4.GetValueOrDefault() & (num5 != null & num4 != null)) && !float.IsNaN(num2.GetValueOrDefault()))
									{
										continue;
									}
								}
								num2 = num6;
							}
						}
					}
					num = num2;
				}
			}
			return num;
		}

		// Token: 0x06000ED5 RID: 3797 RVA: 0x00034F00 File Offset: 0x00033100
		protected override QueryOperatorEnumerator<float?, int> CreateEnumerator<TKey>(int index, int count, QueryOperatorEnumerator<float?, TKey> source, object sharedData, CancellationToken cancellationToken)
		{
			return new NullableFloatMinMaxAggregationOperator.NullableFloatMinMaxAggregationOperatorEnumerator<TKey>(source, index, this.m_sign, cancellationToken);
		}

		// Token: 0x040008A1 RID: 2209
		private readonly int m_sign;

		// Token: 0x020003E0 RID: 992
		private class NullableFloatMinMaxAggregationOperatorEnumerator<TKey> : InlinedAggregationOperatorEnumerator<float?>
		{
			// Token: 0x06001DE0 RID: 7648 RVA: 0x0006AE89 File Offset: 0x00069089
			internal NullableFloatMinMaxAggregationOperatorEnumerator(QueryOperatorEnumerator<float?, TKey> source, int partitionIndex, int sign, CancellationToken cancellationToken) : base(partitionIndex, cancellationToken)
			{
				this.m_source = source;
				this.m_sign = sign;
			}

			// Token: 0x06001DE1 RID: 7649 RVA: 0x0006AEA4 File Offset: 0x000690A4
			protected override bool MoveNextCore(ref float? currentElement)
			{
				QueryOperatorEnumerator<float?, TKey> source = this.m_source;
				TKey tkey = default(TKey);
				if (source.MoveNext(ref currentElement, ref tkey))
				{
					int num = 0;
					if (this.m_sign == -1)
					{
						float? num2 = null;
						while (source.MoveNext(ref num2, ref tkey))
						{
							if ((num++ & 63) == 0)
							{
								CancellationState.ThrowIfCanceled(this.m_cancellationToken);
							}
							if (num2 != null)
							{
								if (currentElement != null)
								{
									float? num3 = num2;
									float? num4 = currentElement;
									if (!(num3.GetValueOrDefault() < num4.GetValueOrDefault() & (num3 != null & num4 != null)) && !float.IsNaN(num2.GetValueOrDefault()))
									{
										continue;
									}
								}
								currentElement = num2;
							}
						}
					}
					else
					{
						float? num5 = null;
						while (source.MoveNext(ref num5, ref tkey))
						{
							if ((num++ & 63) == 0)
							{
								CancellationState.ThrowIfCanceled(this.m_cancellationToken);
							}
							if (num5 != null)
							{
								if (currentElement != null)
								{
									float? num4 = num5;
									float? num3 = currentElement;
									if (!(num4.GetValueOrDefault() > num3.GetValueOrDefault() & (num4 != null & num3 != null)) && !float.IsNaN(currentElement.GetValueOrDefault()))
									{
										continue;
									}
								}
								currentElement = num5;
							}
						}
					}
					return true;
				}
				return false;
			}

			// Token: 0x06001DE2 RID: 7650 RVA: 0x0006AFDE File Offset: 0x000691DE
			protected override void Dispose(bool disposing)
			{
				this.m_source.Dispose();
			}

			// Token: 0x0400119E RID: 4510
			private QueryOperatorEnumerator<float?, TKey> m_source;

			// Token: 0x0400119F RID: 4511
			private int m_sign;
		}
	}
}
