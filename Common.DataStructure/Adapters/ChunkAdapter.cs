using System;
using System.Collections.Generic;

namespace TechnoPro.Common.DataStructure.Adapters
{
	// Token: 0x0200001C RID: 28
	public static class ChunkAdapter
	{
		// Token: 0x060000D9 RID: 217 RVA: 0x00005501 File Offset: 0x00003701
		public static IList<Chunk> BreakdownItemsIntoChunks<T>(this IList<T> items, int chunkSize = 100000)
		{
			return ((items != null) ? items.Count : 0).BreakdownItemsIntoChunks(chunkSize);
		}

		// Token: 0x060000DA RID: 218 RVA: 0x00005515 File Offset: 0x00003715
		public static int GetCount(this Chunk chunk)
		{
			if (chunk == null || chunk.End < chunk.Start)
			{
				return 0;
			}
			return chunk.End - chunk.Start + 1;
		}

		// Token: 0x060000DB RID: 219 RVA: 0x0000553C File Offset: 0x0000373C
		public static IList<T> GetChunkRange<T>(this Chunk chunk, List<T> items)
		{
			int num = (chunk != null) ? chunk.GetCount() : 0;
			if (num <= 0 || items == null)
			{
				return new List<T>();
			}
			return items.GetRange(chunk.Start, num);
		}

		// Token: 0x060000DC RID: 220 RVA: 0x00005570 File Offset: 0x00003770
		public static IList<Chunk> BreakdownItemsIntoChunks(this int itemsCount, int chunkSize = 100000)
		{
			if (itemsCount < chunkSize)
			{
				return new List<Chunk>
				{
					new Chunk(0, itemsCount - 1)
				};
			}
			List<Chunk> list = new List<Chunk>();
			int num = (int)(Convert.ToDouble(itemsCount) / Convert.ToDouble(chunkSize));
			int num2 = itemsCount - num * chunkSize;
			for (int i = 0; i < num; i++)
			{
				int num3 = i * chunkSize;
				list.Add(new Chunk(num3, num3 + chunkSize - 1));
			}
			if (num2 < 1)
			{
				return list;
			}
			int num4 = num * chunkSize;
			list.Add(new Chunk(num4, num4 + num2 - 1));
			return list;
		}
	}
}
