using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace System.Linq.Parallel
{
	// Token: 0x02000187 RID: 391
	internal class OrderPreservingPipeliningMergeHelper<TOutput, TKey> : IMergeHelper<!0>
	{
		// Token: 0x06000E12 RID: 3602 RVA: 0x00031B4C File Offset: 0x0002FD4C
		internal OrderPreservingPipeliningMergeHelper(PartitionedStream<TOutput, TKey> partitions, TaskScheduler taskScheduler, CancellationState cancellationState, bool autoBuffered, int queryId, IComparer<TKey> keyComparer)
		{
			this.m_taskGroupState = new QueryTaskGroupState(cancellationState, queryId);
			this.m_partitions = partitions;
			this.m_taskScheduler = taskScheduler;
			this.m_autoBuffered = autoBuffered;
			int partitionCount = this.m_partitions.PartitionCount;
			this.m_buffers = new Queue<Pair<TKey, TOutput>>[partitionCount];
			this.m_producerDone = new bool[partitionCount];
			this.m_consumerWaiting = new bool[partitionCount];
			this.m_producerWaiting = new bool[partitionCount];
			this.m_bufferLocks = new object[partitionCount];
			if (keyComparer == Util.GetDefaultComparer<int>())
			{
				this.m_producerComparer = (IComparer<Producer<TKey>>)new ProducerComparerInt();
				return;
			}
			this.m_producerComparer = new OrderPreservingPipeliningMergeHelper<TOutput, TKey>.ProducerComparer(keyComparer);
		}

		// Token: 0x06000E13 RID: 3603 RVA: 0x00031BF4 File Offset: 0x0002FDF4
		void IMergeHelper<!0>.Execute()
		{
			OrderPreservingPipeliningSpoolingTask<TOutput, TKey>.Spool(this.m_taskGroupState, this.m_partitions, this.m_consumerWaiting, this.m_producerWaiting, this.m_producerDone, this.m_buffers, this.m_bufferLocks, this.m_taskScheduler, this.m_autoBuffered);
		}

		// Token: 0x06000E14 RID: 3604 RVA: 0x00031C3C File Offset: 0x0002FE3C
		IEnumerator<TOutput> IMergeHelper<!0>.GetEnumerator()
		{
			return new OrderPreservingPipeliningMergeHelper<TOutput, TKey>.OrderedPipeliningMergeEnumerator(this, this.m_producerComparer);
		}

		// Token: 0x06000E15 RID: 3605 RVA: 0x00031C4A File Offset: 0x0002FE4A
		public TOutput[] GetResultsAsArray()
		{
			throw new InvalidOperationException();
		}

		// Token: 0x04000835 RID: 2101
		private readonly QueryTaskGroupState m_taskGroupState;

		// Token: 0x04000836 RID: 2102
		private readonly PartitionedStream<TOutput, TKey> m_partitions;

		// Token: 0x04000837 RID: 2103
		private readonly TaskScheduler m_taskScheduler;

		// Token: 0x04000838 RID: 2104
		private readonly bool m_autoBuffered;

		// Token: 0x04000839 RID: 2105
		private readonly Queue<Pair<TKey, TOutput>>[] m_buffers;

		// Token: 0x0400083A RID: 2106
		private readonly bool[] m_producerDone;

		// Token: 0x0400083B RID: 2107
		private readonly bool[] m_producerWaiting;

		// Token: 0x0400083C RID: 2108
		private readonly bool[] m_consumerWaiting;

		// Token: 0x0400083D RID: 2109
		private readonly object[] m_bufferLocks;

		// Token: 0x0400083E RID: 2110
		private IComparer<Producer<TKey>> m_producerComparer;

		// Token: 0x0400083F RID: 2111
		internal const int INITIAL_BUFFER_SIZE = 128;

		// Token: 0x04000840 RID: 2112
		internal const int STEAL_BUFFER_SIZE = 1024;

		// Token: 0x04000841 RID: 2113
		internal const int MAX_BUFFER_SIZE = 8192;

		// Token: 0x020003AF RID: 943
		private class ProducerComparer : IComparer<Producer<TKey>>
		{
			// Token: 0x06001D46 RID: 7494 RVA: 0x00068101 File Offset: 0x00066301
			internal ProducerComparer(IComparer<TKey> keyComparer)
			{
				this._keyComparer = keyComparer;
			}

			// Token: 0x06001D47 RID: 7495 RVA: 0x00068110 File Offset: 0x00066310
			public int Compare(Producer<TKey> x, Producer<TKey> y)
			{
				return this._keyComparer.Compare(y.MaxKey, x.MaxKey);
			}

			// Token: 0x04001105 RID: 4357
			private IComparer<TKey> _keyComparer;
		}

		// Token: 0x020003B0 RID: 944
		private class OrderedPipeliningMergeEnumerator : MergeEnumerator<TOutput>
		{
			// Token: 0x06001D48 RID: 7496 RVA: 0x0006812C File Offset: 0x0006632C
			internal OrderedPipeliningMergeEnumerator(OrderPreservingPipeliningMergeHelper<TOutput, TKey> mergeHelper, IComparer<Producer<TKey>> producerComparer) : base(mergeHelper.m_taskGroupState)
			{
				int partitionCount = mergeHelper.m_partitions.PartitionCount;
				this.m_mergeHelper = mergeHelper;
				this.m_producerHeap = new FixedMaxHeap<Producer<TKey>>(partitionCount, producerComparer);
				this.m_privateBuffer = new Queue<Pair<TKey, TOutput>>[partitionCount];
				this.m_producerNextElement = new TOutput[partitionCount];
			}

			// Token: 0x17000561 RID: 1377
			// (get) Token: 0x06001D49 RID: 7497 RVA: 0x00068180 File Offset: 0x00066380
			public override TOutput Current
			{
				get
				{
					int producerIndex = this.m_producerHeap.MaxValue.ProducerIndex;
					return this.m_producerNextElement[producerIndex];
				}
			}

			// Token: 0x06001D4A RID: 7498 RVA: 0x000681AC File Offset: 0x000663AC
			public override bool MoveNext()
			{
				if (!this.m_initialized)
				{
					this.m_initialized = true;
					for (int i = 0; i < this.m_mergeHelper.m_partitions.PartitionCount; i++)
					{
						Pair<TKey, TOutput> pair = default(Pair<TKey, TOutput>);
						if (this.TryWaitForElement(i, ref pair))
						{
							this.m_producerHeap.Insert(new Producer<TKey>(pair.First, i));
							this.m_producerNextElement[i] = pair.Second;
						}
						else
						{
							this.ThrowIfInTearDown();
						}
					}
				}
				else
				{
					if (this.m_producerHeap.Count == 0)
					{
						return false;
					}
					int producerIndex = this.m_producerHeap.MaxValue.ProducerIndex;
					Pair<TKey, TOutput> pair2 = default(Pair<TKey, TOutput>);
					if (this.TryGetPrivateElement(producerIndex, ref pair2) || this.TryWaitForElement(producerIndex, ref pair2))
					{
						this.m_producerHeap.ReplaceMax(new Producer<TKey>(pair2.First, producerIndex));
						this.m_producerNextElement[producerIndex] = pair2.Second;
					}
					else
					{
						this.ThrowIfInTearDown();
						this.m_producerHeap.RemoveMax();
					}
				}
				return this.m_producerHeap.Count > 0;
			}

			// Token: 0x06001D4B RID: 7499 RVA: 0x000682B8 File Offset: 0x000664B8
			private void ThrowIfInTearDown()
			{
				if (this.m_mergeHelper.m_taskGroupState.CancellationState.MergedCancellationToken.IsCancellationRequested)
				{
					try
					{
						object[] bufferLocks = this.m_mergeHelper.m_bufferLocks;
						for (int i = 0; i < bufferLocks.Length; i++)
						{
							object obj = bufferLocks[i];
							lock (obj)
							{
								Monitor.Pulse(bufferLocks[i]);
							}
						}
						this.m_taskGroupState.QueryEnd(false);
					}
					finally
					{
						this.m_producerHeap.Clear();
					}
				}
			}

			// Token: 0x06001D4C RID: 7500 RVA: 0x00068358 File Offset: 0x00066558
			private bool TryWaitForElement(int producer, ref Pair<TKey, TOutput> element)
			{
				Queue<Pair<TKey, TOutput>> queue = this.m_mergeHelper.m_buffers[producer];
				object obj = this.m_mergeHelper.m_bufferLocks[producer];
				object obj2 = obj;
				lock (obj2)
				{
					if (queue.Count == 0)
					{
						if (this.m_mergeHelper.m_producerDone[producer])
						{
							element = default(Pair<TKey, TOutput>);
							return false;
						}
						this.m_mergeHelper.m_consumerWaiting[producer] = true;
						Monitor.Wait(obj);
						if (queue.Count == 0)
						{
							element = default(Pair<TKey, TOutput>);
							return false;
						}
					}
					if (this.m_mergeHelper.m_producerWaiting[producer])
					{
						Monitor.Pulse(obj);
						this.m_mergeHelper.m_producerWaiting[producer] = false;
					}
					if (queue.Count < 1024)
					{
						element = queue.Dequeue();
						return true;
					}
					this.m_privateBuffer[producer] = this.m_mergeHelper.m_buffers[producer];
					this.m_mergeHelper.m_buffers[producer] = new Queue<Pair<TKey, TOutput>>(128);
				}
				bool flag2 = this.TryGetPrivateElement(producer, ref element);
				return true;
			}

			// Token: 0x06001D4D RID: 7501 RVA: 0x00068478 File Offset: 0x00066678
			private bool TryGetPrivateElement(int producer, ref Pair<TKey, TOutput> element)
			{
				Queue<Pair<TKey, TOutput>> queue = this.m_privateBuffer[producer];
				if (queue != null)
				{
					if (queue.Count > 0)
					{
						element = queue.Dequeue();
						return true;
					}
					this.m_privateBuffer[producer] = null;
				}
				return false;
			}

			// Token: 0x06001D4E RID: 7502 RVA: 0x000684B4 File Offset: 0x000666B4
			public override void Dispose()
			{
				int num = this.m_mergeHelper.m_buffers.Length;
				for (int i = 0; i < num; i++)
				{
					object obj = this.m_mergeHelper.m_bufferLocks[i];
					object obj2 = obj;
					lock (obj2)
					{
						if (this.m_mergeHelper.m_producerWaiting[i])
						{
							Monitor.Pulse(obj);
						}
					}
				}
				base.Dispose();
			}

			// Token: 0x04001106 RID: 4358
			private OrderPreservingPipeliningMergeHelper<TOutput, TKey> m_mergeHelper;

			// Token: 0x04001107 RID: 4359
			private readonly FixedMaxHeap<Producer<TKey>> m_producerHeap;

			// Token: 0x04001108 RID: 4360
			private readonly TOutput[] m_producerNextElement;

			// Token: 0x04001109 RID: 4361
			private readonly Queue<Pair<TKey, TOutput>>[] m_privateBuffer;

			// Token: 0x0400110A RID: 4362
			private bool m_initialized;
		}
	}
}
