using System;
using System.Collections.Generic;
using System.Threading;

namespace System.Linq.Parallel
{
	// Token: 0x02000193 RID: 403
	internal sealed class AssociativeAggregationOperator<TInput, TIntermediate, TOutput> : UnaryQueryOperator<TInput, TIntermediate>
	{
		// Token: 0x06000E33 RID: 3635 RVA: 0x000327D0 File Offset: 0x000309D0
		internal AssociativeAggregationOperator(IEnumerable<TInput> child, TIntermediate seed, Func<TIntermediate> seedFactory, bool seedIsSpecified, Func<TIntermediate, TInput, TIntermediate> intermediateReduce, Func<TIntermediate, TIntermediate, TIntermediate> finalReduce, Func<TIntermediate, TOutput> resultSelector, bool throwIfEmpty, QueryAggregationOptions options) : base(child)
		{
			this.m_seed = seed;
			this.m_seedFactory = seedFactory;
			this.m_seedIsSpecified = seedIsSpecified;
			this.m_intermediateReduce = intermediateReduce;
			this.m_finalReduce = finalReduce;
			this.m_resultSelector = resultSelector;
			this.m_throwIfEmpty = throwIfEmpty;
		}

		// Token: 0x06000E34 RID: 3636 RVA: 0x00032810 File Offset: 0x00030A10
		internal TOutput Aggregate()
		{
			TIntermediate tintermediate = default(TIntermediate);
			bool flag = false;
			using (IEnumerator<TIntermediate> enumerator = this.GetEnumerator(new ParallelMergeOptions?(ParallelMergeOptions.FullyBuffered), true))
			{
				while (enumerator.MoveNext())
				{
					if (flag)
					{
						try
						{
							tintermediate = this.m_finalReduce(tintermediate, enumerator.Current);
							continue;
						}
						catch (ThreadAbortException)
						{
							throw;
						}
						catch (Exception ex)
						{
							throw new AggregateException(new Exception[]
							{
								ex
							});
						}
					}
					tintermediate = enumerator.Current;
					flag = true;
				}
				if (!flag)
				{
					if (this.m_throwIfEmpty)
					{
						throw new InvalidOperationException(SR.GetString("NoElements"));
					}
					tintermediate = ((this.m_seedFactory == null) ? this.m_seed : this.m_seedFactory());
				}
			}
			TOutput result;
			try
			{
				result = this.m_resultSelector(tintermediate);
			}
			catch (ThreadAbortException)
			{
				throw;
			}
			catch (Exception ex2)
			{
				throw new AggregateException(new Exception[]
				{
					ex2
				});
			}
			return result;
		}

		// Token: 0x06000E35 RID: 3637 RVA: 0x00032920 File Offset: 0x00030B20
		internal override QueryResults<TIntermediate> Open(QuerySettings settings, bool preferStriping)
		{
			QueryResults<TInput> childQueryResults = base.Child.Open(settings, preferStriping);
			return new UnaryQueryOperator<TInput, TIntermediate>.UnaryQueryOperatorResults(childQueryResults, this, settings, preferStriping);
		}

		// Token: 0x06000E36 RID: 3638 RVA: 0x00032944 File Offset: 0x00030B44
		internal override void WrapPartitionedStream<TKey>(PartitionedStream<TInput, TKey> inputStream, IPartitionedStreamRecipient<TIntermediate> recipient, bool preferStriping, QuerySettings settings)
		{
			int partitionCount = inputStream.PartitionCount;
			PartitionedStream<TIntermediate, int> partitionedStream = new PartitionedStream<TIntermediate, int>(partitionCount, Util.GetDefaultComparer<int>(), OrdinalIndexState.Correct);
			for (int i = 0; i < partitionCount; i++)
			{
				partitionedStream[i] = new AssociativeAggregationOperator<TInput, TIntermediate, TOutput>.AssociativeAggregationOperatorEnumerator<TKey>(inputStream[i], this, i, settings.CancellationState.MergedCancellationToken);
			}
			recipient.Receive<int>(partitionedStream);
		}

		// Token: 0x06000E37 RID: 3639 RVA: 0x00032999 File Offset: 0x00030B99
		internal override IEnumerable<TIntermediate> AsSequentialQuery(CancellationToken token)
		{
			throw new NotSupportedException();
		}

		// Token: 0x17000283 RID: 643
		// (get) Token: 0x06000E38 RID: 3640 RVA: 0x000329A0 File Offset: 0x00030BA0
		internal override bool LimitsParallelism
		{
			get
			{
				return false;
			}
		}

		// Token: 0x04000863 RID: 2147
		private readonly TIntermediate m_seed;

		// Token: 0x04000864 RID: 2148
		private readonly bool m_seedIsSpecified;

		// Token: 0x04000865 RID: 2149
		private readonly bool m_throwIfEmpty;

		// Token: 0x04000866 RID: 2150
		private Func<TIntermediate, TInput, TIntermediate> m_intermediateReduce;

		// Token: 0x04000867 RID: 2151
		private Func<TIntermediate, TIntermediate, TIntermediate> m_finalReduce;

		// Token: 0x04000868 RID: 2152
		private Func<TIntermediate, TOutput> m_resultSelector;

		// Token: 0x04000869 RID: 2153
		private Func<TIntermediate> m_seedFactory;

		// Token: 0x020003B8 RID: 952
		private class AssociativeAggregationOperatorEnumerator<TKey> : QueryOperatorEnumerator<TIntermediate, int>
		{
			// Token: 0x06001D5E RID: 7518 RVA: 0x00068BEF File Offset: 0x00066DEF
			internal AssociativeAggregationOperatorEnumerator(QueryOperatorEnumerator<TInput, TKey> source, AssociativeAggregationOperator<TInput, TIntermediate, TOutput> reduceOperator, int partitionIndex, CancellationToken cancellationToken)
			{
				this.m_source = source;
				this.m_reduceOperator = reduceOperator;
				this.m_partitionIndex = partitionIndex;
				this.m_cancellationToken = cancellationToken;
			}

			// Token: 0x06001D5F RID: 7519 RVA: 0x00068C14 File Offset: 0x00066E14
			internal override bool MoveNext(ref TIntermediate currentElement, ref int currentKey)
			{
				if (this.m_accumulated)
				{
					return false;
				}
				this.m_accumulated = true;
				bool flag = false;
				TIntermediate tintermediate = default(TIntermediate);
				if (this.m_reduceOperator.m_seedIsSpecified)
				{
					tintermediate = ((this.m_reduceOperator.m_seedFactory == null) ? this.m_reduceOperator.m_seed : this.m_reduceOperator.m_seedFactory());
				}
				else
				{
					TInput tinput = default(TInput);
					TKey tkey = default(TKey);
					if (!this.m_source.MoveNext(ref tinput, ref tkey))
					{
						return false;
					}
					flag = true;
					tintermediate = (TIntermediate)((object)tinput);
				}
				TInput arg = default(TInput);
				TKey tkey2 = default(TKey);
				int num = 0;
				while (this.m_source.MoveNext(ref arg, ref tkey2))
				{
					if ((num++ & 63) == 0)
					{
						CancellationState.ThrowIfCanceled(this.m_cancellationToken);
					}
					flag = true;
					tintermediate = this.m_reduceOperator.m_intermediateReduce(tintermediate, arg);
				}
				if (flag)
				{
					currentElement = tintermediate;
					currentKey = this.m_partitionIndex;
					return true;
				}
				return false;
			}

			// Token: 0x06001D60 RID: 7520 RVA: 0x00068D0F File Offset: 0x00066F0F
			protected override void Dispose(bool disposing)
			{
				this.m_source.Dispose();
			}

			// Token: 0x0400112F RID: 4399
			private readonly QueryOperatorEnumerator<TInput, TKey> m_source;

			// Token: 0x04001130 RID: 4400
			private readonly AssociativeAggregationOperator<TInput, TIntermediate, TOutput> m_reduceOperator;

			// Token: 0x04001131 RID: 4401
			private readonly int m_partitionIndex;

			// Token: 0x04001132 RID: 4402
			private readonly CancellationToken m_cancellationToken;

			// Token: 0x04001133 RID: 4403
			private bool m_accumulated;
		}
	}
}
