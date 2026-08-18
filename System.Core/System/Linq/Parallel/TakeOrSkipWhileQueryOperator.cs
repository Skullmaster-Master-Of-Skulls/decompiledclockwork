using System;
using System.Collections.Generic;
using System.Threading;

namespace System.Linq.Parallel
{
	// Token: 0x020001E7 RID: 487
	internal sealed class TakeOrSkipWhileQueryOperator<TResult> : UnaryQueryOperator<TResult, TResult>
	{
		// Token: 0x06000FCA RID: 4042 RVA: 0x00037C74 File Offset: 0x00035E74
		internal TakeOrSkipWhileQueryOperator(IEnumerable<TResult> child, Func<TResult, bool> predicate, Func<TResult, int, bool> indexedPredicate, bool take) : base(child)
		{
			this.m_predicate = predicate;
			this.m_indexedPredicate = indexedPredicate;
			this.m_take = take;
			this.InitOrderIndexState();
		}

		// Token: 0x06000FCB RID: 4043 RVA: 0x00037C9C File Offset: 0x00035E9C
		private void InitOrderIndexState()
		{
			OrdinalIndexState state = OrdinalIndexState.Increasing;
			OrdinalIndexState ordinalIndexState = base.Child.OrdinalIndexState;
			if (this.m_indexedPredicate != null)
			{
				state = OrdinalIndexState.Correct;
				this.m_limitsParallelism = (ordinalIndexState == OrdinalIndexState.Increasing);
			}
			OrdinalIndexState ordinalIndexState2 = ordinalIndexState.Worse(OrdinalIndexState.Correct);
			if (ordinalIndexState2.IsWorseThan(state))
			{
				this.m_prematureMerge = true;
			}
			if (!this.m_take)
			{
				ordinalIndexState2 = ordinalIndexState2.Worse(OrdinalIndexState.Increasing);
			}
			base.SetOrdinalIndexState(ordinalIndexState2);
		}

		// Token: 0x06000FCC RID: 4044 RVA: 0x00037CFC File Offset: 0x00035EFC
		internal override void WrapPartitionedStream<TKey>(PartitionedStream<TResult, TKey> inputStream, IPartitionedStreamRecipient<TResult> recipient, bool preferStriping, QuerySettings settings)
		{
			if (this.m_prematureMerge)
			{
				ListQueryResults<TResult> listQueryResults = QueryOperator<TResult>.ExecuteAndCollectResults<TKey>(inputStream, inputStream.PartitionCount, base.Child.OutputOrdered, preferStriping, settings);
				PartitionedStream<TResult, int> partitionedStream = listQueryResults.GetPartitionedStream();
				this.WrapHelper<int>(partitionedStream, recipient, settings);
				return;
			}
			this.WrapHelper<TKey>(inputStream, recipient, settings);
		}

		// Token: 0x06000FCD RID: 4045 RVA: 0x00037D48 File Offset: 0x00035F48
		private void WrapHelper<TKey>(PartitionedStream<TResult, TKey> inputStream, IPartitionedStreamRecipient<TResult> recipient, QuerySettings settings)
		{
			int partitionCount = inputStream.PartitionCount;
			TakeOrSkipWhileQueryOperator<TResult>.OperatorState<TKey> operatorState = new TakeOrSkipWhileQueryOperator<TResult>.OperatorState<TKey>();
			CountdownEvent sharedBarrier = new CountdownEvent(partitionCount);
			Func<TResult, TKey, bool> indexedPredicate = (Func<TResult, TKey, bool>)this.m_indexedPredicate;
			PartitionedStream<TResult, TKey> partitionedStream = new PartitionedStream<TResult, TKey>(partitionCount, inputStream.KeyComparer, this.OrdinalIndexState);
			for (int i = 0; i < partitionCount; i++)
			{
				partitionedStream[i] = new TakeOrSkipWhileQueryOperator<TResult>.TakeOrSkipWhileQueryOperatorEnumerator<TKey>(inputStream[i], this.m_predicate, indexedPredicate, this.m_take, operatorState, sharedBarrier, settings.CancellationState.MergedCancellationToken, inputStream.KeyComparer);
			}
			recipient.Receive<TKey>(partitionedStream);
		}

		// Token: 0x06000FCE RID: 4046 RVA: 0x00037DD8 File Offset: 0x00035FD8
		internal override QueryResults<TResult> Open(QuerySettings settings, bool preferStriping)
		{
			QueryResults<TResult> childQueryResults = base.Child.Open(settings, true);
			return new UnaryQueryOperator<TResult, TResult>.UnaryQueryOperatorResults(childQueryResults, this, settings, preferStriping);
		}

		// Token: 0x06000FCF RID: 4047 RVA: 0x00037DFC File Offset: 0x00035FFC
		internal override IEnumerable<TResult> AsSequentialQuery(CancellationToken token)
		{
			if (this.m_take)
			{
				if (this.m_indexedPredicate != null)
				{
					return base.Child.AsSequentialQuery(token).TakeWhile(this.m_indexedPredicate);
				}
				return base.Child.AsSequentialQuery(token).TakeWhile(this.m_predicate);
			}
			else
			{
				if (this.m_indexedPredicate != null)
				{
					IEnumerable<TResult> source = CancellableEnumerable.Wrap<TResult>(base.Child.AsSequentialQuery(token), token);
					return source.SkipWhile(this.m_indexedPredicate);
				}
				IEnumerable<TResult> source2 = CancellableEnumerable.Wrap<TResult>(base.Child.AsSequentialQuery(token), token);
				return source2.SkipWhile(this.m_predicate);
			}
		}

		// Token: 0x170002C5 RID: 709
		// (get) Token: 0x06000FD0 RID: 4048 RVA: 0x00037E90 File Offset: 0x00036090
		internal override bool LimitsParallelism
		{
			get
			{
				return this.m_limitsParallelism;
			}
		}

		// Token: 0x040008F8 RID: 2296
		private Func<TResult, bool> m_predicate;

		// Token: 0x040008F9 RID: 2297
		private Func<TResult, int, bool> m_indexedPredicate;

		// Token: 0x040008FA RID: 2298
		private readonly bool m_take;

		// Token: 0x040008FB RID: 2299
		private bool m_prematureMerge;

		// Token: 0x040008FC RID: 2300
		private bool m_limitsParallelism;

		// Token: 0x0200040A RID: 1034
		private class TakeOrSkipWhileQueryOperatorEnumerator<TKey> : QueryOperatorEnumerator<TResult, TKey>
		{
			// Token: 0x06001E64 RID: 7780 RVA: 0x0006CE7C File Offset: 0x0006B07C
			internal TakeOrSkipWhileQueryOperatorEnumerator(QueryOperatorEnumerator<TResult, TKey> source, Func<TResult, bool> predicate, Func<TResult, TKey, bool> indexedPredicate, bool take, TakeOrSkipWhileQueryOperator<TResult>.OperatorState<TKey> operatorState, CountdownEvent sharedBarrier, CancellationToken cancelToken, IComparer<TKey> keyComparer)
			{
				this.m_source = source;
				this.m_predicate = predicate;
				this.m_indexedPredicate = indexedPredicate;
				this.m_take = take;
				this.m_operatorState = operatorState;
				this.m_sharedBarrier = sharedBarrier;
				this.m_cancellationToken = cancelToken;
				this.m_keyComparer = keyComparer;
			}

			// Token: 0x06001E65 RID: 7781 RVA: 0x0006CECC File Offset: 0x0006B0CC
			internal override bool MoveNext(ref TResult currentElement, ref TKey currentKey)
			{
				if (this.m_buffer == null)
				{
					List<Pair<TResult, TKey>> list = new List<Pair<TResult, TKey>>();
					try
					{
						TResult tresult = default(TResult);
						TKey tkey = default(TKey);
						int num = 0;
						while (this.m_source.MoveNext(ref tresult, ref tkey))
						{
							if ((num++ & 63) == 0)
							{
								CancellationState.ThrowIfCanceled(this.m_cancellationToken);
							}
							list.Add(new Pair<TResult, TKey>(tresult, tkey));
							if (this.m_updatesSeen != this.m_operatorState.m_updatesDone)
							{
								TakeOrSkipWhileQueryOperator<TResult>.OperatorState<TKey> operatorState = this.m_operatorState;
								lock (operatorState)
								{
									this.m_currentLowKey = this.m_operatorState.m_currentLowKey;
									this.m_updatesSeen = this.m_operatorState.m_updatesDone;
								}
							}
							if (this.m_updatesSeen > 0 && this.m_keyComparer.Compare(tkey, this.m_currentLowKey) > 0)
							{
								break;
							}
							bool flag2;
							if (this.m_predicate != null)
							{
								flag2 = this.m_predicate(tresult);
							}
							else
							{
								flag2 = this.m_indexedPredicate(tresult, tkey);
							}
							if (!flag2)
							{
								TakeOrSkipWhileQueryOperator<TResult>.OperatorState<TKey> operatorState2 = this.m_operatorState;
								lock (operatorState2)
								{
									if (this.m_operatorState.m_updatesDone == 0 || this.m_keyComparer.Compare(this.m_operatorState.m_currentLowKey, tkey) > 0)
									{
										this.m_currentLowKey = (this.m_operatorState.m_currentLowKey = tkey);
										TakeOrSkipWhileQueryOperator<TResult>.OperatorState<TKey> operatorState3 = this.m_operatorState;
										int num2 = operatorState3.m_updatesDone + 1;
										operatorState3.m_updatesDone = num2;
										this.m_updatesSeen = num2;
									}
									break;
								}
							}
						}
					}
					finally
					{
						this.m_sharedBarrier.Signal();
					}
					this.m_sharedBarrier.Wait(this.m_cancellationToken);
					this.m_buffer = list;
					this.m_bufferIndex = new Shared<int>(-1);
				}
				if (this.m_take)
				{
					if (this.m_bufferIndex.Value >= this.m_buffer.Count - 1)
					{
						return false;
					}
					this.m_bufferIndex.Value++;
					currentElement = this.m_buffer[this.m_bufferIndex.Value].First;
					currentKey = this.m_buffer[this.m_bufferIndex.Value].Second;
					return this.m_operatorState.m_updatesDone == 0 || this.m_keyComparer.Compare(this.m_operatorState.m_currentLowKey, currentKey) > 0;
				}
				else
				{
					if (this.m_operatorState.m_updatesDone == 0)
					{
						return false;
					}
					if (this.m_bufferIndex.Value < this.m_buffer.Count - 1)
					{
						this.m_bufferIndex.Value++;
						while (this.m_bufferIndex.Value < this.m_buffer.Count)
						{
							if (this.m_keyComparer.Compare(this.m_buffer[this.m_bufferIndex.Value].Second, this.m_operatorState.m_currentLowKey) >= 0)
							{
								currentElement = this.m_buffer[this.m_bufferIndex.Value].First;
								currentKey = this.m_buffer[this.m_bufferIndex.Value].Second;
								return true;
							}
							this.m_bufferIndex.Value++;
						}
					}
					return this.m_source.MoveNext(ref currentElement, ref currentKey);
				}
			}

			// Token: 0x06001E66 RID: 7782 RVA: 0x0006D2A8 File Offset: 0x0006B4A8
			protected override void Dispose(bool disposing)
			{
				this.m_source.Dispose();
			}

			// Token: 0x0400122D RID: 4653
			private readonly QueryOperatorEnumerator<TResult, TKey> m_source;

			// Token: 0x0400122E RID: 4654
			private readonly Func<TResult, bool> m_predicate;

			// Token: 0x0400122F RID: 4655
			private readonly Func<TResult, TKey, bool> m_indexedPredicate;

			// Token: 0x04001230 RID: 4656
			private readonly bool m_take;

			// Token: 0x04001231 RID: 4657
			private readonly IComparer<TKey> m_keyComparer;

			// Token: 0x04001232 RID: 4658
			private readonly TakeOrSkipWhileQueryOperator<TResult>.OperatorState<TKey> m_operatorState;

			// Token: 0x04001233 RID: 4659
			private readonly CountdownEvent m_sharedBarrier;

			// Token: 0x04001234 RID: 4660
			private readonly CancellationToken m_cancellationToken;

			// Token: 0x04001235 RID: 4661
			private List<Pair<TResult, TKey>> m_buffer;

			// Token: 0x04001236 RID: 4662
			private Shared<int> m_bufferIndex;

			// Token: 0x04001237 RID: 4663
			private int m_updatesSeen;

			// Token: 0x04001238 RID: 4664
			private TKey m_currentLowKey;
		}

		// Token: 0x0200040B RID: 1035
		private class OperatorState<TKey>
		{
			// Token: 0x04001239 RID: 4665
			internal volatile int m_updatesDone;

			// Token: 0x0400123A RID: 4666
			internal TKey m_currentLowKey;
		}
	}
}
