using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;

namespace System.Linq.Parallel
{
	// Token: 0x02000194 RID: 404
	internal class PartitionerQueryOperator<TElement> : QueryOperator<TElement>
	{
		// Token: 0x06000E39 RID: 3641 RVA: 0x000329A3 File Offset: 0x00030BA3
		internal PartitionerQueryOperator(Partitioner<TElement> partitioner) : base(false, QuerySettings.Empty)
		{
			this.m_partitioner = partitioner;
		}

		// Token: 0x17000284 RID: 644
		// (get) Token: 0x06000E3A RID: 3642 RVA: 0x000329B8 File Offset: 0x00030BB8
		internal bool Orderable
		{
			get
			{
				return this.m_partitioner is OrderablePartitioner<TElement>;
			}
		}

		// Token: 0x06000E3B RID: 3643 RVA: 0x000329C8 File Offset: 0x00030BC8
		internal override QueryResults<TElement> Open(QuerySettings settings, bool preferStriping)
		{
			return new PartitionerQueryOperator<TElement>.PartitionerQueryOperatorResults(this.m_partitioner, settings);
		}

		// Token: 0x06000E3C RID: 3644 RVA: 0x000329D6 File Offset: 0x00030BD6
		internal override IEnumerable<TElement> AsSequentialQuery(CancellationToken token)
		{
			using (IEnumerator<TElement> enumerator = this.m_partitioner.GetPartitions(1)[0])
			{
				while (enumerator.MoveNext())
				{
					TElement telement = enumerator.Current;
					yield return telement;
				}
			}
			IEnumerator<TElement> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x17000285 RID: 645
		// (get) Token: 0x06000E3D RID: 3645 RVA: 0x000329E6 File Offset: 0x00030BE6
		internal override OrdinalIndexState OrdinalIndexState
		{
			get
			{
				return PartitionerQueryOperator<TElement>.GetOrdinalIndexState(this.m_partitioner);
			}
		}

		// Token: 0x06000E3E RID: 3646 RVA: 0x000329F4 File Offset: 0x00030BF4
		internal static OrdinalIndexState GetOrdinalIndexState(Partitioner<TElement> partitioner)
		{
			OrderablePartitioner<TElement> orderablePartitioner = partitioner as OrderablePartitioner<TElement>;
			if (orderablePartitioner == null)
			{
				return OrdinalIndexState.Shuffled;
			}
			if (!orderablePartitioner.KeysOrderedInEachPartition)
			{
				return OrdinalIndexState.Shuffled;
			}
			if (orderablePartitioner.KeysNormalized)
			{
				return OrdinalIndexState.Correct;
			}
			return OrdinalIndexState.Increasing;
		}

		// Token: 0x17000286 RID: 646
		// (get) Token: 0x06000E3F RID: 3647 RVA: 0x00032A22 File Offset: 0x00030C22
		internal override bool LimitsParallelism
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0400086A RID: 2154
		private Partitioner<TElement> m_partitioner;

		// Token: 0x020003B9 RID: 953
		private class PartitionerQueryOperatorResults : QueryResults<TElement>
		{
			// Token: 0x06001D61 RID: 7521 RVA: 0x00068D1C File Offset: 0x00066F1C
			internal PartitionerQueryOperatorResults(Partitioner<TElement> partitioner, QuerySettings settings)
			{
				this.m_partitioner = partitioner;
				this.m_settings = settings;
			}

			// Token: 0x06001D62 RID: 7522 RVA: 0x00068D34 File Offset: 0x00066F34
			internal override void GivePartitionedStream(IPartitionedStreamRecipient<TElement> recipient)
			{
				int value = this.m_settings.DegreeOfParallelism.Value;
				OrderablePartitioner<TElement> orderablePartitioner = this.m_partitioner as OrderablePartitioner<TElement>;
				OrdinalIndexState indexState = (orderablePartitioner != null) ? PartitionerQueryOperator<TElement>.GetOrdinalIndexState(orderablePartitioner) : OrdinalIndexState.Shuffled;
				PartitionedStream<TElement, int> partitionedStream = new PartitionedStream<TElement, int>(value, Util.GetDefaultComparer<int>(), indexState);
				if (orderablePartitioner != null)
				{
					IList<IEnumerator<KeyValuePair<long, TElement>>> orderablePartitions = orderablePartitioner.GetOrderablePartitions(value);
					if (orderablePartitions == null)
					{
						throw new InvalidOperationException(SR.GetString("PartitionerQueryOperator_NullPartitionList"));
					}
					if (orderablePartitions.Count != value)
					{
						throw new InvalidOperationException(SR.GetString("PartitionerQueryOperator_WrongNumberOfPartitions"));
					}
					for (int i = 0; i < value; i++)
					{
						IEnumerator<KeyValuePair<long, TElement>> enumerator = orderablePartitions[i];
						if (enumerator == null)
						{
							throw new InvalidOperationException(SR.GetString("PartitionerQueryOperator_NullPartition"));
						}
						partitionedStream[i] = new PartitionerQueryOperator<TElement>.OrderablePartitionerEnumerator(enumerator);
					}
				}
				else
				{
					IList<IEnumerator<TElement>> partitions = this.m_partitioner.GetPartitions(value);
					if (partitions == null)
					{
						throw new InvalidOperationException(SR.GetString("PartitionerQueryOperator_NullPartitionList"));
					}
					if (partitions.Count != value)
					{
						throw new InvalidOperationException(SR.GetString("PartitionerQueryOperator_WrongNumberOfPartitions"));
					}
					for (int j = 0; j < value; j++)
					{
						IEnumerator<TElement> enumerator2 = partitions[j];
						if (enumerator2 == null)
						{
							throw new InvalidOperationException(SR.GetString("PartitionerQueryOperator_NullPartition"));
						}
						partitionedStream[j] = new PartitionerQueryOperator<TElement>.PartitionerEnumerator(enumerator2);
					}
				}
				recipient.Receive<int>(partitionedStream);
			}

			// Token: 0x04001134 RID: 4404
			private Partitioner<TElement> m_partitioner;

			// Token: 0x04001135 RID: 4405
			private QuerySettings m_settings;
		}

		// Token: 0x020003BA RID: 954
		private class OrderablePartitionerEnumerator : QueryOperatorEnumerator<TElement, int>
		{
			// Token: 0x06001D63 RID: 7523 RVA: 0x00068E77 File Offset: 0x00067077
			internal OrderablePartitionerEnumerator(IEnumerator<KeyValuePair<long, TElement>> sourceEnumerator)
			{
				this.m_sourceEnumerator = sourceEnumerator;
			}

			// Token: 0x06001D64 RID: 7524 RVA: 0x00068E88 File Offset: 0x00067088
			internal override bool MoveNext(ref TElement currentElement, ref int currentKey)
			{
				if (!this.m_sourceEnumerator.MoveNext())
				{
					return false;
				}
				KeyValuePair<long, TElement> keyValuePair = this.m_sourceEnumerator.Current;
				currentElement = keyValuePair.Value;
				currentKey = checked((int)keyValuePair.Key);
				return true;
			}

			// Token: 0x06001D65 RID: 7525 RVA: 0x00068EC8 File Offset: 0x000670C8
			protected override void Dispose(bool disposing)
			{
				this.m_sourceEnumerator.Dispose();
			}

			// Token: 0x04001136 RID: 4406
			private IEnumerator<KeyValuePair<long, TElement>> m_sourceEnumerator;
		}

		// Token: 0x020003BB RID: 955
		private class PartitionerEnumerator : QueryOperatorEnumerator<TElement, int>
		{
			// Token: 0x06001D66 RID: 7526 RVA: 0x00068ED5 File Offset: 0x000670D5
			internal PartitionerEnumerator(IEnumerator<TElement> sourceEnumerator)
			{
				this.m_sourceEnumerator = sourceEnumerator;
			}

			// Token: 0x06001D67 RID: 7527 RVA: 0x00068EE4 File Offset: 0x000670E4
			internal override bool MoveNext(ref TElement currentElement, ref int currentKey)
			{
				if (!this.m_sourceEnumerator.MoveNext())
				{
					return false;
				}
				currentElement = this.m_sourceEnumerator.Current;
				currentKey = 0;
				return true;
			}

			// Token: 0x06001D68 RID: 7528 RVA: 0x00068F0A File Offset: 0x0006710A
			protected override void Dispose(bool disposing)
			{
				this.m_sourceEnumerator.Dispose();
			}

			// Token: 0x04001137 RID: 4407
			private IEnumerator<TElement> m_sourceEnumerator;
		}
	}
}
