using System;
using System.Reflection.Internal;

namespace System.Reflection.Metadata.Ecma335
{
	// Token: 0x020000CE RID: 206
	internal struct GuidStreamReader
	{
		// Token: 0x06000872 RID: 2162 RVA: 0x0001717E File Offset: 0x0001537E
		public GuidStreamReader(MemoryBlock block)
		{
			this.Block = block;
		}

		// Token: 0x06000873 RID: 2163 RVA: 0x00017188 File Offset: 0x00015388
		internal Guid GetGuid(GuidHandle handle)
		{
			if (handle.IsNil)
			{
				return default(Guid);
			}
			return this.Block.PeekGuid((handle.Index - 1) * 16);
		}

		// Token: 0x040005B2 RID: 1458
		internal readonly MemoryBlock Block;

		// Token: 0x040005B3 RID: 1459
		internal const int GuidSize = 16;
	}
}
