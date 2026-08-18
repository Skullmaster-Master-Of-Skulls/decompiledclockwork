using System;
using System.Collections.Generic;
using System.Threading;

namespace System.Linq.Parallel
{
	// Token: 0x020001B7 RID: 439
	internal sealed class NullableDoubleMinMaxAggregationOperator : InlinedAggregationOperator<double?, double?, double?>
	{
		// Token: 0x06000ECA RID: 3786 RVA: 0x00034B53 File Offset: 0x00032D53
		internal NullableDoubleMinMaxAggregationOperator(IEnumerable<double?> child, int sign) : base(child)
		{
			this.m_sign = sign;
		}

		// Token: 0x06000ECB RID: 3787 RVA: 0x00034B64 File Offset: 0x00032D64
		protected override double? InternalAggregate(ref Exception singularExceptionToThrow)
		{
			double? num;
			using (IEnumerator<double?> enumerator = this.GetEnumerator(new ParallelMergeOptions?(ParallelMergeOptions.FullyBuffered), true))
			{
				if (!enumerator.MoveNext())
				{
					num = null;
					num = num;
				}
				else
				{
					double? num2 = enumerator.Current;
					if (this.m_sign == -1)
					{
						while (enumerator.MoveNext())
						{
							double? num3 = enumerator.Current;
							if (num3 != null)
							{
								if (num2 != null)
								{
									double? num4 = num3;
									double? num5 = num2;
									if (!(num4.GetValueOrDefault() < num5.GetValueOrDefault() & (num4 != null & num5 != null)) && !double.IsNaN(num3.GetValueOrDefault()))
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
							double? num6 = enumerator.Current;
							if (num6 != null)
							{
								if (num2 != null)
								{
									double? num5 = num6;
									double? num4 = num2;
									if (!(num5.GetValueOrDefault() > num4.GetValueOrDefault() & (num5 != null & num4 != null)) && !double.IsNaN(num2.GetValueOrDefault()))
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

		// Token: 0x06000ECC RID: 3788 RVA: 0x00034C84 File Offset: 0x00032E84
		protected override QueryOperatorEnumerator<double?, int> CreateEnumerator<TKey>(int index, int count, QueryOperatorEnumerator<double?, TKey> source, object sharedData, CancellationToken cancellationToken)
		{
			return new NullableDoubleMinMaxAggregationOperator.NullableDoubleMinMaxAggregationOperatorEnumerator<TKey>(source, index, this.m_sign, cancellationToken);
		}

		// Token: 0x040008A0 RID: 2208
		private readonly int m_sign;

		// Token: 0x020003DD RID: 989
		private class NullableDoubleMinMaxAggregationOperatorEnumerator<TKey> : InlinedAggregationOperatorEnumerator<double?>
		{
			// Token: 0x06001DD7 RID: 7639 RVA: 0x0006ABEC File Offset: 0x00068DEC
			internal NullableDoubleMinMaxAggregationOperatorEnumerator(QueryOperatorEnumerator<double?, TKey> source, int partitionIndex, int sign, CancellationToken cancellationToken) : base(partitionIndex, cancellationToken)
			{
				this.m_source = source;
				this.m_sign = sign;
			}

			// Token: 0x06001DD8 RID: 7640 RVA: 0x0006AC08 File Offset: 0x00068E08
			protected override bool MoveNextCore(ref double? currentElement)
			{
				QueryOperatorEnumerator<double?, TKey> source = this.m_source;
				TKey tkey = default(TKey);
				if (source.MoveNext(ref currentElement, ref tkey))
				{
					int num = 0;
					if (this.m_sign == -1)
					{
						double? num2 = null;
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
									double? num3 = num2;
									double? num4 = currentElement;
									if (!(num3.GetValueOrDefault() < num4.GetValueOrDefault() & (num3 != null & num4 != null)) && !double.IsNaN(num2.GetValueOrDefault()))
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
						double? num5 = null;
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
									double? num4 = num5;
									double? num3 = currentElement;
									if (!(num4.GetValueOrDefault() > num3.GetValueOrDefault() & (num4 != null & num3 != null)) && !double.IsNaN(currentElement.GetValueOrDefault()))
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

			// Token: 0x06001DD9 RID: 7641 RVA: 0x0006AD42 File Offset: 0x00068F42
			protected override void Dispose(bool disposing)
			{
				this.m_source.Dispose();
			}

			// Token: 0x0400119A RID: 4506
			private QueryOperatorEnumerator<double?, TKey> m_source;

			// Token: 0x0400119B RID: 4507
			private int m_sign;
		}
	}
}
