using System;
using System.Collections.Generic;
using System.Threading;

namespace System.Linq.Parallel
{
	// Token: 0x02000190 RID: 400
	internal class PartitionedDataSource<T> : PartitionedStream<T, int>
	{
		// Token: 0x06000E29 RID: 3625 RVA: 0x000325B7 File Offset: 0x000307B7
		internal PartitionedDataSource(IEnumerable<T> source, int partitionCount, bool useStriping) : base(partitionCount, Util.GetDefaultComparer<int>(), (source is IList<T>) ? OrdinalIndexState.Indexible : OrdinalIndexState.Correct)
		{
			this.InitializePartitions(source, partitionCount, useStriping);
		}

		// Token: 0x06000E2A RID: 3626 RVA: 0x000325DC File Offset: 0x000307DC
		private void InitializePartitions(IEnumerable<T> source, int partitionCount, bool useStriping)
		{
			ParallelEnumerableWrapper<T> parallelEnumerableWrapper = source as ParallelEnumerableWrapper<T>;
			if (parallelEnumerableWrapper != null)
			{
				source = parallelEnumerableWrapper.WrappedEnumerable;
			}
			IList<T> list = source as IList<T>;
			if (list != null)
			{
				QueryOperatorEnumerator<T, int>[] array = new QueryOperatorEnumerator<T, int>[partitionCount];
				int count = list.Count;
				T[] array2 = source as T[];
				int num = -1;
				if (useStriping)
				{
					num = Scheduling.GetDefaultChunkSize<T>();
					if (num < 1)
					{
						num = 1;
					}
				}
				for (int i = 0; i < partitionCount; i++)
				{
					if (array2 != null)
					{
						if (useStriping)
						{
							array[i] = new PartitionedDataSource<T>.ArrayIndexRangeEnumerator(array2, partitionCount, i, num);
						}
						else
						{
							array[i] = new PartitionedDataSource<T>.ArrayContiguousIndexRangeEnumerator(array2, partitionCount, i);
						}
					}
					else if (useStriping)
					{
						array[i] = new PartitionedDataSource<T>.ListIndexRangeEnumerator(list, partitionCount, i, num);
					}
					else
					{
						array[i] = new PartitionedDataSource<T>.ListContiguousIndexRangeEnumerator(list, partitionCount, i);
					}
				}
				this.m_partitions = array;
				return;
			}
			this.m_partitions = PartitionedDataSource<T>.MakePartitions(source.GetEnumerator(), partitionCount);
		}

		// Token: 0x06000E2B RID: 3627 RVA: 0x000326A8 File Offset: 0x000308A8
		private static QueryOperatorEnumerator<T, int>[] MakePartitions(IEnumerator<T> source, int partitionCount)
		{
			QueryOperatorEnumerator<T, int>[] array = new QueryOperatorEnumerator<T, int>[partitionCount];
			object sourceSyncLock = new object();
			Shared<int> currentIndex = new Shared<int>(0);
			Shared<int> degreeOfParallelism = new Shared<int>(partitionCount);
			Shared<bool> exceptionTracker = new Shared<bool>(false);
			for (int i = 0; i < partitionCount; i++)
			{
				array[i] = new PartitionedDataSource<T>.ContiguousChunkLazyEnumerator(source, exceptionTracker, sourceSyncLock, currentIndex, degreeOfParallelism);
			}
			return array;
		}

		// Token: 0x020003B3 RID: 947
		internal sealed class ArrayIndexRangeEnumerator : QueryOperatorEnumerator<T, int>
		{
			// Token: 0x06001D51 RID: 7505 RVA: 0x00068550 File Offset: 0x00066750
			internal ArrayIndexRangeEnumerator(T[] data, int partitionCount, int partitionIndex, int maxChunkSize)
			{
				this.m_data = data;
				this.m_elementCount = data.Length;
				this.m_partitionCount = partitionCount;
				this.m_partitionIndex = partitionIndex;
				this.m_maxChunkSize = maxChunkSize;
				int num = maxChunkSize * partitionCount;
				this.m_sectionCount = this.m_elementCount / num + ((this.m_elementCount % num == 0) ? 0 : 1);
			}

			// Token: 0x06001D52 RID: 7506 RVA: 0x000685AC File Offset: 0x000667AC
			internal override bool MoveNext(ref T currentElement, ref int currentKey)
			{
				PartitionedDataSource<T>.ArrayIndexRangeEnumerator.Mutables mutables = this.m_mutables;
				if (mutables == null)
				{
					mutables = (this.m_mutables = new PartitionedDataSource<T>.ArrayIndexRangeEnumerator.Mutables());
				}
				PartitionedDataSource<T>.ArrayIndexRangeEnumerator.Mutables mutables2 = mutables;
				int num = mutables2.m_currentPositionInChunk + 1;
				mutables2.m_currentPositionInChunk = num;
				if (num < mutables.m_currentChunkSize || this.MoveNextSlowPath())
				{
					currentKey = mutables.m_currentChunkOffset + mutables.m_currentPositionInChunk;
					currentElement = this.m_data[currentKey];
					return true;
				}
				return false;
			}

			// Token: 0x06001D53 RID: 7507 RVA: 0x00068618 File Offset: 0x00066818
			private bool MoveNextSlowPath()
			{
				PartitionedDataSource<T>.ArrayIndexRangeEnumerator.Mutables mutables = this.m_mutables;
				PartitionedDataSource<T>.ArrayIndexRangeEnumerator.Mutables mutables2 = mutables;
				int num = mutables2.m_currentSection + 1;
				mutables2.m_currentSection = num;
				int num2 = num;
				int num3 = this.m_sectionCount - num2;
				if (num3 <= 0)
				{
					return false;
				}
				int num4 = num2 * this.m_partitionCount * this.m_maxChunkSize;
				mutables.m_currentPositionInChunk = 0;
				if (num3 > 1)
				{
					mutables.m_currentChunkSize = this.m_maxChunkSize;
					mutables.m_currentChunkOffset = num4 + this.m_partitionIndex * this.m_maxChunkSize;
				}
				else
				{
					int num5 = this.m_elementCount - num4;
					int num6 = num5 / this.m_partitionCount;
					int num7 = num5 % this.m_partitionCount;
					mutables.m_currentChunkSize = num6;
					if (this.m_partitionIndex < num7)
					{
						mutables.m_currentChunkSize++;
					}
					if (mutables.m_currentChunkSize == 0)
					{
						return false;
					}
					mutables.m_currentChunkOffset = num4 + this.m_partitionIndex * num6 + ((this.m_partitionIndex < num7) ? this.m_partitionIndex : num7);
				}
				return true;
			}

			// Token: 0x04001112 RID: 4370
			private readonly T[] m_data;

			// Token: 0x04001113 RID: 4371
			private readonly int m_elementCount;

			// Token: 0x04001114 RID: 4372
			private readonly int m_partitionCount;

			// Token: 0x04001115 RID: 4373
			private readonly int m_partitionIndex;

			// Token: 0x04001116 RID: 4374
			private readonly int m_maxChunkSize;

			// Token: 0x04001117 RID: 4375
			private readonly int m_sectionCount;

			// Token: 0x04001118 RID: 4376
			private PartitionedDataSource<T>.ArrayIndexRangeEnumerator.Mutables m_mutables;

			// Token: 0x02000490 RID: 1168
			private class Mutables
			{
				// Token: 0x0600205A RID: 8282 RVA: 0x000707E6 File Offset: 0x0006E9E6
				internal Mutables()
				{
					this.m_currentSection = -1;
				}

				// Token: 0x040013DE RID: 5086
				internal int m_currentSection;

				// Token: 0x040013DF RID: 5087
				internal int m_currentChunkSize;

				// Token: 0x040013E0 RID: 5088
				internal int m_currentPositionInChunk;

				// Token: 0x040013E1 RID: 5089
				internal int m_currentChunkOffset;
			}
		}

		// Token: 0x020003B4 RID: 948
		internal sealed class ArrayContiguousIndexRangeEnumerator : QueryOperatorEnumerator<T, int>
		{
			// Token: 0x06001D54 RID: 7508 RVA: 0x00068700 File Offset: 0x00066900
			internal ArrayContiguousIndexRangeEnumerator(T[] data, int partitionCount, int partitionIndex)
			{
				this.m_data = data;
				int num = data.Length / partitionCount;
				int num2 = data.Length % partitionCount;
				int num3 = partitionIndex * num + ((partitionIndex < num2) ? partitionIndex : num2);
				this.m_startIndex = num3 - 1;
				this.m_maximumIndex = num3 + num + ((partitionIndex < num2) ? 1 : 0);
			}

			// Token: 0x06001D55 RID: 7509 RVA: 0x00068750 File Offset: 0x00066950
			internal override bool MoveNext(ref T currentElement, ref int currentKey)
			{
				if (this.m_currentIndex == null)
				{
					this.m_currentIndex = new Shared<int>(this.m_startIndex);
				}
				Shared<int> currentIndex = this.m_currentIndex;
				int num = currentIndex.Value + 1;
				currentIndex.Value = num;
				int num2 = num;
				if (num2 < this.m_maximumIndex)
				{
					currentKey = num2;
					currentElement = this.m_data[num2];
					return true;
				}
				return false;
			}

			// Token: 0x04001119 RID: 4377
			private readonly T[] m_data;

			// Token: 0x0400111A RID: 4378
			private readonly int m_startIndex;

			// Token: 0x0400111B RID: 4379
			private readonly int m_maximumIndex;

			// Token: 0x0400111C RID: 4380
			private Shared<int> m_currentIndex;
		}

		// Token: 0x020003B5 RID: 949
		internal sealed class ListIndexRangeEnumerator : QueryOperatorEnumerator<T, int>
		{
			// Token: 0x06001D56 RID: 7510 RVA: 0x000687B0 File Offset: 0x000669B0
			internal ListIndexRangeEnumerator(IList<T> data, int partitionCount, int partitionIndex, int maxChunkSize)
			{
				this.m_data = data;
				this.m_elementCount = data.Count;
				this.m_partitionCount = partitionCount;
				this.m_partitionIndex = partitionIndex;
				this.m_maxChunkSize = maxChunkSize;
				int num = maxChunkSize * partitionCount;
				this.m_sectionCount = this.m_elementCount / num + ((this.m_elementCount % num == 0) ? 0 : 1);
			}

			// Token: 0x06001D57 RID: 7511 RVA: 0x00068810 File Offset: 0x00066A10
			internal override bool MoveNext(ref T currentElement, ref int currentKey)
			{
				PartitionedDataSource<T>.ListIndexRangeEnumerator.Mutables mutables = this.m_mutables;
				if (mutables == null)
				{
					mutables = (this.m_mutables = new PartitionedDataSource<T>.ListIndexRangeEnumerator.Mutables());
				}
				PartitionedDataSource<T>.ListIndexRangeEnumerator.Mutables mutables2 = mutables;
				int num = mutables2.m_currentPositionInChunk + 1;
				mutables2.m_currentPositionInChunk = num;
				if (num < mutables.m_currentChunkSize || this.MoveNextSlowPath())
				{
					currentKey = mutables.m_currentChunkOffset + mutables.m_currentPositionInChunk;
					currentElement = this.m_data[currentKey];
					return true;
				}
				return false;
			}

			// Token: 0x06001D58 RID: 7512 RVA: 0x0006887C File Offset: 0x00066A7C
			private bool MoveNextSlowPath()
			{
				PartitionedDataSource<T>.ListIndexRangeEnumerator.Mutables mutables = this.m_mutables;
				PartitionedDataSource<T>.ListIndexRangeEnumerator.Mutables mutables2 = mutables;
				int num = mutables2.m_currentSection + 1;
				mutables2.m_currentSection = num;
				int num2 = num;
				int num3 = this.m_sectionCount - num2;
				if (num3 <= 0)
				{
					return false;
				}
				int num4 = num2 * this.m_partitionCount * this.m_maxChunkSize;
				mutables.m_currentPositionInChunk = 0;
				if (num3 > 1)
				{
					mutables.m_currentChunkSize = this.m_maxChunkSize;
					mutables.m_currentChunkOffset = num4 + this.m_partitionIndex * this.m_maxChunkSize;
				}
				else
				{
					int num5 = this.m_elementCount - num4;
					int num6 = num5 / this.m_partitionCount;
					int num7 = num5 % this.m_partitionCount;
					mutables.m_currentChunkSize = num6;
					if (this.m_partitionIndex < num7)
					{
						mutables.m_currentChunkSize++;
					}
					if (mutables.m_currentChunkSize == 0)
					{
						return false;
					}
					mutables.m_currentChunkOffset = num4 + this.m_partitionIndex * num6 + ((this.m_partitionIndex < num7) ? this.m_partitionIndex : num7);
				}
				return true;
			}

			// Token: 0x0400111D RID: 4381
			private readonly IList<T> m_data;

			// Token: 0x0400111E RID: 4382
			private readonly int m_elementCount;

			// Token: 0x0400111F RID: 4383
			private readonly int m_partitionCount;

			// Token: 0x04001120 RID: 4384
			private readonly int m_partitionIndex;

			// Token: 0x04001121 RID: 4385
			private readonly int m_maxChunkSize;

			// Token: 0x04001122 RID: 4386
			private readonly int m_sectionCount;

			// Token: 0x04001123 RID: 4387
			private PartitionedDataSource<T>.ListIndexRangeEnumerator.Mutables m_mutables;

			// Token: 0x02000491 RID: 1169
			private class Mutables
			{
				// Token: 0x0600205B RID: 8283 RVA: 0x000707F5 File Offset: 0x0006E9F5
				internal Mutables()
				{
					this.m_currentSection = -1;
				}

				// Token: 0x040013E2 RID: 5090
				internal int m_currentSection;

				// Token: 0x040013E3 RID: 5091
				internal int m_currentChunkSize;

				// Token: 0x040013E4 RID: 5092
				internal int m_currentPositionInChunk;

				// Token: 0x040013E5 RID: 5093
				internal int m_currentChunkOffset;
			}
		}

		// Token: 0x020003B6 RID: 950
		internal sealed class ListContiguousIndexRangeEnumerator : QueryOperatorEnumerator<T, int>
		{
			// Token: 0x06001D59 RID: 7513 RVA: 0x00068964 File Offset: 0x00066B64
			internal ListContiguousIndexRangeEnumerator(IList<T> data, int partitionCount, int partitionIndex)
			{
				this.m_data = data;
				int num = data.Count / partitionCount;
				int num2 = data.Count % partitionCount;
				int num3 = partitionIndex * num + ((partitionIndex < num2) ? partitionIndex : num2);
				this.m_startIndex = num3 - 1;
				this.m_maximumIndex = num3 + num + ((partitionIndex < num2) ? 1 : 0);
			}

			// Token: 0x06001D5A RID: 7514 RVA: 0x000689B8 File Offset: 0x00066BB8
			internal override bool MoveNext(ref T currentElement, ref int currentKey)
			{
				if (this.m_currentIndex == null)
				{
					this.m_currentIndex = new Shared<int>(this.m_startIndex);
				}
				Shared<int> currentIndex = this.m_currentIndex;
				int num = currentIndex.Value + 1;
				currentIndex.Value = num;
				int num2 = num;
				if (num2 < this.m_maximumIndex)
				{
					currentKey = num2;
					currentElement = this.m_data[num2];
					return true;
				}
				return false;
			}

			// Token: 0x04001124 RID: 4388
			private readonly IList<T> m_data;

			// Token: 0x04001125 RID: 4389
			private readonly int m_startIndex;

			// Token: 0x04001126 RID: 4390
			private readonly int m_maximumIndex;

			// Token: 0x04001127 RID: 4391
			private Shared<int> m_currentIndex;
		}

		// Token: 0x020003B7 RID: 951
		private class ContiguousChunkLazyEnumerator : QueryOperatorEnumerator<T, int>
		{
			// Token: 0x06001D5B RID: 7515 RVA: 0x00068A16 File Offset: 0x00066C16
			internal ContiguousChunkLazyEnumerator(IEnumerator<T> source, Shared<bool> exceptionTracker, object sourceSyncLock, Shared<int> currentIndex, Shared<int> degreeOfParallelism)
			{
				this.m_source = source;
				this.m_sourceSyncLock = sourceSyncLock;
				this.m_currentIndex = currentIndex;
				this.m_activeEnumeratorsCount = degreeOfParallelism;
				this.m_exceptionTracker = exceptionTracker;
			}

			// Token: 0x06001D5C RID: 7516 RVA: 0x00068A44 File Offset: 0x00066C44
			internal override bool MoveNext(ref T currentElement, ref int currentKey)
			{
				PartitionedDataSource<T>.ContiguousChunkLazyEnumerator.Mutables mutables = this.m_mutables;
				if (mutables == null)
				{
					mutables = (this.m_mutables = new PartitionedDataSource<T>.ContiguousChunkLazyEnumerator.Mutables());
				}
				T[] chunkBuffer;
				int num2;
				for (;;)
				{
					chunkBuffer = mutables.m_chunkBuffer;
					PartitionedDataSource<T>.ContiguousChunkLazyEnumerator.Mutables mutables2 = mutables;
					int num = mutables2.m_currentChunkIndex + 1;
					mutables2.m_currentChunkIndex = num;
					num2 = num;
					if (num2 < mutables.m_currentChunkSize)
					{
						break;
					}
					object sourceSyncLock = this.m_sourceSyncLock;
					lock (sourceSyncLock)
					{
						int num3 = 0;
						if (this.m_exceptionTracker.Value)
						{
							return false;
						}
						try
						{
							while (num3 < mutables.m_nextChunkMaxSize && this.m_source.MoveNext())
							{
								chunkBuffer[num3] = this.m_source.Current;
								num3++;
							}
						}
						catch
						{
							this.m_exceptionTracker.Value = true;
							throw;
						}
						mutables.m_currentChunkSize = num3;
						if (num3 == 0)
						{
							return false;
						}
						mutables.m_chunkBaseIndex = this.m_currentIndex.Value;
						checked
						{
							this.m_currentIndex.Value += num3;
						}
					}
					if (mutables.m_nextChunkMaxSize < chunkBuffer.Length)
					{
						PartitionedDataSource<T>.ContiguousChunkLazyEnumerator.Mutables mutables3 = mutables;
						num = mutables3.m_chunkCounter;
						mutables3.m_chunkCounter = num + 1;
						if ((num & 7) == 7)
						{
							mutables.m_nextChunkMaxSize *= 2;
							if (mutables.m_nextChunkMaxSize > chunkBuffer.Length)
							{
								mutables.m_nextChunkMaxSize = chunkBuffer.Length;
							}
						}
					}
					mutables.m_currentChunkIndex = -1;
				}
				currentElement = chunkBuffer[num2];
				currentKey = mutables.m_chunkBaseIndex + num2;
				return true;
			}

			// Token: 0x06001D5D RID: 7517 RVA: 0x00068BD0 File Offset: 0x00066DD0
			protected override void Dispose(bool disposing)
			{
				if (Interlocked.Decrement(ref this.m_activeEnumeratorsCount.Value) == 0)
				{
					this.m_source.Dispose();
				}
			}

			// Token: 0x04001128 RID: 4392
			private const int chunksPerChunkSize = 7;

			// Token: 0x04001129 RID: 4393
			private readonly IEnumerator<T> m_source;

			// Token: 0x0400112A RID: 4394
			private readonly object m_sourceSyncLock;

			// Token: 0x0400112B RID: 4395
			private readonly Shared<int> m_currentIndex;

			// Token: 0x0400112C RID: 4396
			private readonly Shared<int> m_activeEnumeratorsCount;

			// Token: 0x0400112D RID: 4397
			private readonly Shared<bool> m_exceptionTracker;

			// Token: 0x0400112E RID: 4398
			private PartitionedDataSource<T>.ContiguousChunkLazyEnumerator.Mutables m_mutables;

			// Token: 0x02000492 RID: 1170
			private class Mutables
			{
				// Token: 0x0600205C RID: 8284 RVA: 0x00070804 File Offset: 0x0006EA04
				internal Mutables()
				{
					this.m_nextChunkMaxSize = 1;
					this.m_chunkBuffer = new T[Scheduling.GetDefaultChunkSize<T>()];
					this.m_currentChunkSize = 0;
					this.m_currentChunkIndex = -1;
					this.m_chunkBaseIndex = 0;
					this.m_chunkCounter = 0;
				}

				// Token: 0x040013E6 RID: 5094
				internal readonly T[] m_chunkBuffer;

				// Token: 0x040013E7 RID: 5095
				internal int m_nextChunkMaxSize;

				// Token: 0x040013E8 RID: 5096
				internal int m_currentChunkSize;

				// Token: 0x040013E9 RID: 5097
				internal int m_currentChunkIndex;

				// Token: 0x040013EA RID: 5098
				internal int m_chunkBaseIndex;

				// Token: 0x040013EB RID: 5099
				internal int m_chunkCounter;
			}
		}
	}
}
