using System;

namespace TechnoPro.Common.DataStructure
{
	// Token: 0x02000002 RID: 2
	public class Chunk
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		public Chunk()
		{
		}

		// Token: 0x06000002 RID: 2 RVA: 0x00002058 File Offset: 0x00000258
		public Chunk(int start, int end)
		{
			this.Start = start;
			this.End = end;
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000003 RID: 3 RVA: 0x0000206E File Offset: 0x0000026E
		// (set) Token: 0x06000004 RID: 4 RVA: 0x00002076 File Offset: 0x00000276
		public int Start { get; set; }

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000005 RID: 5 RVA: 0x0000207F File Offset: 0x0000027F
		// (set) Token: 0x06000006 RID: 6 RVA: 0x00002087 File Offset: 0x00000287
		public int End { get; set; }
	}
}
