using System;
using System.Linq;

namespace AutoMapper.Mappers
{
	// Token: 0x0200008B RID: 139
	internal static class ArrayExtensions
	{
		// Token: 0x06000444 RID: 1092 RVA: 0x000118A0 File Offset: 0x0000FAA0
		public static int[] GetLengths(this Array array)
		{
			return (from dimension in Enumerable.Range(0, array.Rank)
			select array.GetLength(dimension)).ToArray<int>();
		}
	}
}
