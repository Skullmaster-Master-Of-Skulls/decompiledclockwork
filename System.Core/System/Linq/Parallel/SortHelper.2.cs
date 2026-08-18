using System;
using System.Collections.Generic;
using System.Threading;

namespace System.Linq.Parallel
{
	// Token: 0x02000206 RID: 518
	internal class SortHelper<TInputOutput, TKey> : SortHelper<TInputOutput>, IDisposable
	{
		// Token: 0x0600105B RID: 4187 RVA: 0x00039828 File Offset: 0x00037A28
		private SortHelper(QueryOperatorEnumerator<TInputOutput, TKey> source, int partitionCount, int partitionIndex, QueryTaskGroupState groupState, int[][] sharedIndices, OrdinalIndexState indexState, IComparer<TKey> keyComparer, GrowingArray<TKey>[] sharedkeys, TInputOutput[][] sharedValues, Barrier[,] sharedBarriers)
		{
			this.m_source = source;
			this.m_partitionCount = partitionCount;
			this.m_partitionIndex = partitionIndex;
			this.m_groupState = groupState;
			this.m_sharedIndices = sharedIndices;
			this.m_indexState = indexState;
			this.m_keyComparer = keyComparer;
			this.m_sharedKeys = sharedkeys;
			this.m_sharedValues = sharedValues;
			this.m_sharedBarriers = sharedBarriers;
		}

		// Token: 0x0600105C RID: 4188 RVA: 0x00039888 File Offset: 0x00037A88
		internal static SortHelper<TInputOutput, TKey>[] GenerateSortHelpers(PartitionedStream<TInputOutput, TKey> partitions, QueryTaskGroupState groupState)
		{
			int partitionCount = partitions.PartitionCount;
			SortHelper<TInputOutput, TKey>[] array = new SortHelper<TInputOutput, TKey>[partitionCount];
			int i = 1;
			int num = 0;
			while (i < partitionCount)
			{
				num++;
				i <<= 1;
			}
			int[][] sharedIndices = new int[partitionCount][];
			GrowingArray<TKey>[] sharedkeys = new GrowingArray<TKey>[partitionCount];
			TInputOutput[][] sharedValues = new TInputOutput[partitionCount][];
			Barrier[,] array2 = new Barrier[num, partitionCount];
			if (partitionCount > 1)
			{
				int num2 = 1;
				for (int j = 0; j < array2.GetLength(0); j++)
				{
					for (int k = 0; k < array2.GetLength(1); k++)
					{
						if (k % num2 == 0)
						{
							array2[j, k] = new Barrier(2);
						}
					}
					num2 *= 2;
				}
			}
			for (int l = 0; l < partitionCount; l++)
			{
				array[l] = new SortHelper<TInputOutput, TKey>(partitions[l], partitionCount, l, groupState, sharedIndices, partitions.OrdinalIndexState, partitions.KeyComparer, sharedkeys, sharedValues, array2);
			}
			return array;
		}

		// Token: 0x0600105D RID: 4189 RVA: 0x00039964 File Offset: 0x00037B64
		public void Dispose()
		{
			if (this.m_partitionIndex == 0)
			{
				for (int i = 0; i < this.m_sharedBarriers.GetLength(0); i++)
				{
					for (int j = 0; j < this.m_sharedBarriers.GetLength(1); j++)
					{
						Barrier barrier = this.m_sharedBarriers[i, j];
						if (barrier != null)
						{
							barrier.Dispose();
						}
					}
				}
			}
		}

		// Token: 0x0600105E RID: 4190 RVA: 0x000399C0 File Offset: 0x00037BC0
		internal override TInputOutput[] Sort()
		{
			GrowingArray<TKey> keys = null;
			List<TInputOutput> values = null;
			this.BuildKeysFromSource(ref keys, ref values);
			this.QuickSortIndicesInPlace(keys, values, this.m_indexState);
			if (this.m_partitionCount > 1)
			{
				this.MergeSortCooperatively();
			}
			return this.m_sharedValues[this.m_partitionIndex];
		}

		// Token: 0x0600105F RID: 4191 RVA: 0x00039A08 File Offset: 0x00037C08
		private void BuildKeysFromSource(ref GrowingArray<TKey> keys, ref List<TInputOutput> values)
		{
			values = new List<TInputOutput>();
			CancellationToken mergedCancellationToken = this.m_groupState.CancellationState.MergedCancellationToken;
			try
			{
				TInputOutput item = default(TInputOutput);
				TKey element = default(TKey);
				bool flag = this.m_source.MoveNext(ref item, ref element);
				if (keys == null)
				{
					keys = new GrowingArray<TKey>();
				}
				if (flag)
				{
					int num = 0;
					do
					{
						if ((num++ & 63) == 0)
						{
							CancellationState.ThrowIfCanceled(mergedCancellationToken);
						}
						keys.Add(element);
						values.Add(item);
					}
					while (this.m_source.MoveNext(ref item, ref element));
				}
			}
			finally
			{
				this.m_source.Dispose();
			}
		}

		// Token: 0x06001060 RID: 4192 RVA: 0x00039AB0 File Offset: 0x00037CB0
		private void QuickSortIndicesInPlace(GrowingArray<TKey> keys, List<TInputOutput> values, OrdinalIndexState ordinalIndexState)
		{
			int[] array = new int[values.Count];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = i;
			}
			if (array.Length > 1 && ordinalIndexState.IsWorseThan(OrdinalIndexState.Increasing))
			{
				this.QuickSort(0, array.Length - 1, keys.InternalArray, array, this.m_groupState.CancellationState.MergedCancellationToken);
			}
			if (this.m_partitionCount == 1)
			{
				TInputOutput[] array2 = new TInputOutput[values.Count];
				for (int j = 0; j < array.Length; j++)
				{
					array2[j] = values[array[j]];
				}
				this.m_sharedValues[this.m_partitionIndex] = array2;
				return;
			}
			this.m_sharedIndices[this.m_partitionIndex] = array;
			this.m_sharedKeys[this.m_partitionIndex] = keys;
			this.m_sharedValues[this.m_partitionIndex] = new TInputOutput[values.Count];
			values.CopyTo(this.m_sharedValues[this.m_partitionIndex]);
		}

		// Token: 0x06001061 RID: 4193 RVA: 0x00039B98 File Offset: 0x00037D98
		private void MergeSortCooperatively()
		{
			CancellationToken mergedCancellationToken = this.m_groupState.CancellationState.MergedCancellationToken;
			int length = this.m_sharedBarriers.GetLength(0);
			for (int i = 0; i < length; i++)
			{
				bool flag = i == length - 1;
				int num = this.ComputePartnerIndex(i);
				if (num < this.m_partitionCount)
				{
					int[] array = this.m_sharedIndices[this.m_partitionIndex];
					GrowingArray<TKey> growingArray = this.m_sharedKeys[this.m_partitionIndex];
					TKey[] internalArray = growingArray.InternalArray;
					TInputOutput[] array2 = this.m_sharedValues[this.m_partitionIndex];
					this.m_sharedBarriers[i, Math.Min(this.m_partitionIndex, num)].SignalAndWait(mergedCancellationToken);
					if (this.m_partitionIndex >= num)
					{
						this.m_sharedBarriers[i, num].SignalAndWait(mergedCancellationToken);
						int[] array3 = this.m_sharedIndices[this.m_partitionIndex];
						TKey[] internalArray2 = this.m_sharedKeys[this.m_partitionIndex].InternalArray;
						TInputOutput[] array4 = this.m_sharedValues[this.m_partitionIndex];
						int[] array5 = this.m_sharedIndices[num];
						GrowingArray<TKey> growingArray2 = this.m_sharedKeys[num];
						TInputOutput[] array6 = this.m_sharedValues[num];
						int num2 = array4.Length;
						int num3 = array2.Length;
						int num4 = num2 + num3;
						int num5 = (num4 + 1) / 2;
						int j = num4 - 1;
						int num6 = num2 - 1;
						int num7 = num3 - 1;
						while (j >= num5)
						{
							if ((j & 63) == 0)
							{
								CancellationState.ThrowIfCanceled(mergedCancellationToken);
							}
							if (num6 >= 0 && (num7 < 0 || this.m_keyComparer.Compare(internalArray2[array3[num6]], internalArray[array[num7]]) > 0))
							{
								if (flag)
								{
									array6[j] = array4[array3[num6]];
								}
								else
								{
									array5[j] = array3[num6];
								}
								num6--;
							}
							else
							{
								if (flag)
								{
									array6[j] = array2[array[num7]];
								}
								else
								{
									array5[j] = num2 + array[num7];
								}
								num7--;
							}
							j--;
						}
						if (!flag && array2.Length != 0)
						{
							growingArray2.CopyFrom(internalArray, array2.Length);
							Array.Copy(array2, 0, array6, num2, array2.Length);
						}
						this.m_sharedBarriers[i, num].SignalAndWait(mergedCancellationToken);
						return;
					}
					int[] array7 = this.m_sharedIndices[num];
					TKey[] internalArray3 = this.m_sharedKeys[num].InternalArray;
					TInputOutput[] array8 = this.m_sharedValues[num];
					this.m_sharedIndices[num] = array;
					this.m_sharedKeys[num] = growingArray;
					this.m_sharedValues[num] = array2;
					int num8 = array2.Length;
					int num9 = array8.Length;
					int num10 = num8 + num9;
					int[] array9 = null;
					TInputOutput[] array10 = new TInputOutput[num10];
					if (!flag)
					{
						array9 = new int[num10];
					}
					this.m_sharedIndices[this.m_partitionIndex] = array9;
					this.m_sharedKeys[this.m_partitionIndex] = growingArray;
					this.m_sharedValues[this.m_partitionIndex] = array10;
					this.m_sharedBarriers[i, this.m_partitionIndex].SignalAndWait(mergedCancellationToken);
					int num11 = (num10 + 1) / 2;
					int k = 0;
					int num12 = 0;
					int num13 = 0;
					while (k < num11)
					{
						if ((k & 63) == 0)
						{
							CancellationState.ThrowIfCanceled(mergedCancellationToken);
						}
						if (num12 < num8 && (num13 >= num9 || this.m_keyComparer.Compare(internalArray[array[num12]], internalArray3[array7[num13]]) <= 0))
						{
							if (flag)
							{
								array10[k] = array2[array[num12]];
							}
							else
							{
								array9[k] = array[num12];
							}
							num12++;
						}
						else
						{
							if (flag)
							{
								array10[k] = array8[array7[num13]];
							}
							else
							{
								array9[k] = num8 + array7[num13];
							}
							num13++;
						}
						k++;
					}
					if (!flag && num8 > 0)
					{
						Array.Copy(array2, 0, array10, 0, num8);
					}
					this.m_sharedBarriers[i, this.m_partitionIndex].SignalAndWait(mergedCancellationToken);
				}
			}
		}

		// Token: 0x06001062 RID: 4194 RVA: 0x00039F74 File Offset: 0x00038174
		private int ComputePartnerIndex(int phase)
		{
			int num = 1 << phase;
			return this.m_partitionIndex + ((this.m_partitionIndex % (num * 2) == 0) ? num : (-num));
		}

		// Token: 0x06001063 RID: 4195 RVA: 0x00039FA0 File Offset: 0x000381A0
		private void QuickSort(int left, int right, TKey[] keys, int[] indices, CancellationToken cancelToken)
		{
			if (right - left > 63)
			{
				CancellationState.ThrowIfCanceled(cancelToken);
			}
			do
			{
				int num = left;
				int num2 = right;
				int num3 = indices[num + (num2 - num >> 1)];
				TKey y = keys[num3];
				for (;;)
				{
					if (this.m_keyComparer.Compare(keys[indices[num]], y) >= 0)
					{
						while (this.m_keyComparer.Compare(keys[indices[num2]], y) > 0)
						{
							num2--;
						}
						if (num > num2)
						{
							break;
						}
						if (num < num2)
						{
							int num4 = indices[num];
							indices[num] = indices[num2];
							indices[num2] = num4;
						}
						num++;
						num2--;
						if (num > num2)
						{
							break;
						}
					}
					else
					{
						num++;
					}
				}
				if (num2 - left <= right - num)
				{
					if (left < num2)
					{
						this.QuickSort(left, num2, keys, indices, cancelToken);
					}
					left = num;
				}
				else
				{
					if (num < right)
					{
						this.QuickSort(num, right, keys, indices, cancelToken);
					}
					right = num2;
				}
			}
			while (left < right);
		}

		// Token: 0x04000946 RID: 2374
		private QueryOperatorEnumerator<TInputOutput, TKey> m_source;

		// Token: 0x04000947 RID: 2375
		private int m_partitionCount;

		// Token: 0x04000948 RID: 2376
		private int m_partitionIndex;

		// Token: 0x04000949 RID: 2377
		private QueryTaskGroupState m_groupState;

		// Token: 0x0400094A RID: 2378
		private int[][] m_sharedIndices;

		// Token: 0x0400094B RID: 2379
		private GrowingArray<TKey>[] m_sharedKeys;

		// Token: 0x0400094C RID: 2380
		private TInputOutput[][] m_sharedValues;

		// Token: 0x0400094D RID: 2381
		private Barrier[,] m_sharedBarriers;

		// Token: 0x0400094E RID: 2382
		private OrdinalIndexState m_indexState;

		// Token: 0x0400094F RID: 2383
		private IComparer<TKey> m_keyComparer;
	}
}
