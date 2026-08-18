using System;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace System.Reflection.Metadata.Ecma335
{
	// Token: 0x02000118 RID: 280
	public sealed class MetadataAggregator
	{
		// Token: 0x0600095E RID: 2398 RVA: 0x0001B360 File Offset: 0x00019560
		public MetadataAggregator(MetadataReader baseReader, IReadOnlyList<MetadataReader> deltaReaders) : this(baseReader, null, null, deltaReaders)
		{
		}

		// Token: 0x0600095F RID: 2399 RVA: 0x0001B36C File Offset: 0x0001956C
		public MetadataAggregator(IReadOnlyList<int> baseTableRowCounts, IReadOnlyList<int> baseHeapSizes, IReadOnlyList<MetadataReader> deltaReaders) : this(null, baseTableRowCounts, baseHeapSizes, deltaReaders)
		{
		}

		// Token: 0x06000960 RID: 2400 RVA: 0x0001B378 File Offset: 0x00019578
		private MetadataAggregator(MetadataReader baseReader, IReadOnlyList<int> baseTableRowCounts, IReadOnlyList<int> baseHeapSizes, IReadOnlyList<MetadataReader> deltaReaders)
		{
			if (baseTableRowCounts == null)
			{
				if (baseReader == null)
				{
					throw new ArgumentNullException("baseReader");
				}
				if (baseReader.GetTableRowCount(TableIndex.EncMap) != 0)
				{
					throw new ArgumentException("Base reader must be a full metadata reader.", "baseReader");
				}
				MetadataAggregator.CalculateBaseCounts(baseReader, out baseTableRowCounts, out baseHeapSizes);
			}
			else
			{
				if (baseTableRowCounts.Count != MetadataTokens.TableCount)
				{
					throw new ArgumentException("Must have " + MetadataTokens.TableCount + " elements", "baseTableRowCounts");
				}
				if (baseHeapSizes == null)
				{
					throw new ArgumentNullException("baseHeapSizes");
				}
				if (baseHeapSizes.Count != MetadataTokens.HeapCount)
				{
					throw new ArgumentException("Must have " + MetadataTokens.HeapCount + " elements", "baseTableRowCounts");
				}
			}
			if (deltaReaders == null || deltaReaders.Count == 0)
			{
				throw new ArgumentException("Must not be empty.", "deltaReaders");
			}
			for (int i = 0; i < deltaReaders.Count; i++)
			{
				if (deltaReaders[i].GetTableRowCount(TableIndex.EncMap) == 0 || !deltaReaders[i].IsMinimalDelta)
				{
					throw new ArgumentException("All delta readers must be minimal delta metadata readers.", "deltaReaders");
				}
			}
			this._heapSizes = MetadataAggregator.CalculateHeapSizes(baseHeapSizes, deltaReaders);
			this._rowCounts = MetadataAggregator.CalculateRowCounts(baseTableRowCounts, deltaReaders);
		}

		// Token: 0x06000961 RID: 2401 RVA: 0x0001B4AD File Offset: 0x000196AD
		internal MetadataAggregator(MetadataAggregator.RowCounts[][] rowCounts, int[][] heapSizes)
		{
			this._rowCounts = MetadataAggregator.ToImmutable<MetadataAggregator.RowCounts>(rowCounts);
			this._heapSizes = MetadataAggregator.ToImmutable<int>(heapSizes);
		}

		// Token: 0x06000962 RID: 2402 RVA: 0x0001B4D0 File Offset: 0x000196D0
		private static void CalculateBaseCounts(MetadataReader baseReader, out IReadOnlyList<int> baseTableRowCounts, out IReadOnlyList<int> baseHeapSizes)
		{
			int[] array = new int[MetadataTokens.TableCount];
			int[] array2 = new int[MetadataTokens.HeapCount];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = baseReader.GetTableRowCount((TableIndex)i);
			}
			for (int j = 0; j < array2.Length; j++)
			{
				array2[j] = baseReader.GetHeapSize((HeapIndex)j);
			}
			baseTableRowCounts = array;
			baseHeapSizes = array2;
		}

		// Token: 0x06000963 RID: 2403 RVA: 0x0001B52C File Offset: 0x0001972C
		private static ImmutableArray<ImmutableArray<int>> CalculateHeapSizes(IReadOnlyList<int> baseSizes, IReadOnlyList<MetadataReader> deltaReaders)
		{
			int num = 1 + deltaReaders.Count;
			int[] array = new int[num];
			int[] array2 = new int[num];
			int[] array3 = new int[num];
			int[] array4 = new int[num];
			array[0] = baseSizes[0];
			array2[0] = baseSizes[1];
			array3[0] = baseSizes[2];
			array4[0] = baseSizes[3] / 16;
			for (int i = 0; i < deltaReaders.Count; i++)
			{
				array[i + 1] = array[i] + deltaReaders[i].GetHeapSize(HeapIndex.UserString);
				array2[i + 1] = array2[i] + deltaReaders[i].GetHeapSize(HeapIndex.String);
				array3[i + 1] = array3[i] + deltaReaders[i].GetHeapSize(HeapIndex.Blob);
				array4[i + 1] = array4[i] + deltaReaders[i].GetHeapSize(HeapIndex.Guid) / 16;
			}
			return ImmutableArray.Create<ImmutableArray<int>>(array.ToImmutableArray<int>(), array2.ToImmutableArray<int>(), array3.ToImmutableArray<int>(), array4.ToImmutableArray<int>());
		}

		// Token: 0x06000964 RID: 2404 RVA: 0x0001B620 File Offset: 0x00019820
		private static ImmutableArray<ImmutableArray<MetadataAggregator.RowCounts>> CalculateRowCounts(IReadOnlyList<int> baseRowCounts, IReadOnlyList<MetadataReader> deltaReaders)
		{
			MetadataAggregator.RowCounts[][] baseRowCounts2 = MetadataAggregator.GetBaseRowCounts(baseRowCounts, 1 + deltaReaders.Count);
			for (int i = 1; i <= deltaReaders.Count; i++)
			{
				MetadataAggregator.CalculateDeltaRowCountsForGeneration(baseRowCounts2, i, ref deltaReaders[i - 1].EncMapTable);
			}
			return MetadataAggregator.ToImmutable<MetadataAggregator.RowCounts>(baseRowCounts2);
		}

		// Token: 0x06000965 RID: 2405 RVA: 0x0001B668 File Offset: 0x00019868
		private static ImmutableArray<ImmutableArray<T>> ToImmutable<T>(T[][] array)
		{
			ImmutableArray<T>[] array2 = new ImmutableArray<T>[array.Length];
			for (int i = 0; i < array.Length; i++)
			{
				array2[i] = array[i].ToImmutableArray<T>();
			}
			return array2.ToImmutableArray<ImmutableArray<T>>();
		}

		// Token: 0x06000966 RID: 2406 RVA: 0x0001B6A4 File Offset: 0x000198A4
		internal static MetadataAggregator.RowCounts[][] GetBaseRowCounts(IReadOnlyList<int> baseRowCounts, int generations)
		{
			MetadataAggregator.RowCounts[][] array = new MetadataAggregator.RowCounts[56][];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = new MetadataAggregator.RowCounts[generations];
				array[i][0].AggregateInserts = baseRowCounts[i];
			}
			return array;
		}

		// Token: 0x06000967 RID: 2407 RVA: 0x0001B6E8 File Offset: 0x000198E8
		internal static void CalculateDeltaRowCountsForGeneration(MetadataAggregator.RowCounts[][] rowCounts, int generation, ref EnCMapTableReader encMapTable)
		{
			foreach (MetadataAggregator.RowCounts[] array in rowCounts)
			{
				array[generation].AggregateInserts = array[generation - 1].AggregateInserts;
			}
			int numberOfRows = encMapTable.NumberOfRows;
			for (int j = 1; j <= numberOfRows; j++)
			{
				uint token = encMapTable.GetToken(j);
				int num = (int)(token & 16777215U);
				MetadataAggregator.RowCounts[] array2 = rowCounts[(int)(token >> 24)];
				if (num > array2[generation].AggregateInserts)
				{
					if (num != array2[generation].AggregateInserts + 1)
					{
						throw new BadImageFormatException(SR.EnCMapNotSorted);
					}
					array2[generation].AggregateInserts = num;
				}
				else
				{
					MetadataAggregator.RowCounts[] array3 = array2;
					array3[generation].Updates = array3[generation].Updates + 1;
				}
			}
		}

		// Token: 0x06000968 RID: 2408 RVA: 0x0001B7AC File Offset: 0x000199AC
		public Handle GetGenerationHandle(Handle handle, out int generation)
		{
			if (handle.IsVirtual)
			{
				throw new NotSupportedException();
			}
			if (!handle.IsHeapHandle)
			{
				int rowId = handle.RowId;
				ImmutableArray<MetadataAggregator.RowCounts> array = this._rowCounts[(int)handle.Type];
				generation = array.BinarySearch(new MetadataAggregator.RowCounts
				{
					AggregateInserts = rowId
				});
				if (generation >= 0)
				{
					while (generation > 0)
					{
						if (array[generation - 1].AggregateInserts != rowId)
						{
							break;
						}
						generation--;
					}
				}
				else
				{
					generation = ~generation;
					if (generation >= array.Length)
					{
						throw new ArgumentException(SR.HandleBelongsToFutureGeneration, "handle");
					}
				}
				int value = (generation == 0) ? rowId : (rowId - array[generation - 1].AggregateInserts + array[generation].Updates);
				return new Handle((byte)handle.Type, value);
			}
			int offset = handle.Offset;
			HeapIndex index;
			MetadataTokens.TryGetHeapIndex(handle.Kind, out index);
			ImmutableArray<int> array2 = this._heapSizes[(int)index];
			generation = array2.BinarySearch(offset);
			if (generation >= 0)
			{
				do
				{
					generation++;
					if (generation >= array2.Length)
					{
						break;
					}
				}
				while (array2[generation] == offset);
			}
			else
			{
				generation = ~generation;
			}
			if (generation >= array2.Length)
			{
				throw new ArgumentException(SR.HandleBelongsToFutureGeneration, "handle");
			}
			int value2 = (handle.Type == 114U || generation == 0) ? offset : (offset - array2[generation - 1]);
			return new Handle((byte)handle.Type, value2);
		}

		// Token: 0x0400082D RID: 2093
		private readonly ImmutableArray<ImmutableArray<int>> _heapSizes;

		// Token: 0x0400082E RID: 2094
		private readonly ImmutableArray<ImmutableArray<MetadataAggregator.RowCounts>> _rowCounts;

		// Token: 0x020001D6 RID: 470
		internal struct RowCounts : IComparable<MetadataAggregator.RowCounts>
		{
			// Token: 0x06000C57 RID: 3159 RVA: 0x000226D4 File Offset: 0x000208D4
			public int CompareTo(MetadataAggregator.RowCounts other)
			{
				return this.AggregateInserts - other.AggregateInserts;
			}

			// Token: 0x06000C58 RID: 3160 RVA: 0x000226E3 File Offset: 0x000208E3
			public override string ToString()
			{
				return string.Format("+0x{0:x} ~0x{1:x}", new object[]
				{
					this.AggregateInserts,
					this.Updates
				});
			}

			// Token: 0x04000B47 RID: 2887
			public int AggregateInserts;

			// Token: 0x04000B48 RID: 2888
			public int Updates;
		}
	}
}
