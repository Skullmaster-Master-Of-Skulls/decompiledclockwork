using System;
using System.IO;

namespace Ionic.BZip2
{
	// Token: 0x0200004B RID: 75
	internal class WorkItem
	{
		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x060003A4 RID: 932 RVA: 0x00015911 File Offset: 0x00013B11
		// (set) Token: 0x060003A5 RID: 933 RVA: 0x00015919 File Offset: 0x00013B19
		public BZip2Compressor Compressor { get; private set; }

		// Token: 0x060003A6 RID: 934 RVA: 0x00015922 File Offset: 0x00013B22
		public WorkItem(int ix, int blockSize)
		{
			this.ms = new MemoryStream();
			this.bw = new BitWriter(this.ms);
			this.Compressor = new BZip2Compressor(this.bw, blockSize);
			this.index = ix;
		}

		// Token: 0x04000230 RID: 560
		public int index;

		// Token: 0x04000231 RID: 561
		public MemoryStream ms;

		// Token: 0x04000232 RID: 562
		public int ordinal;

		// Token: 0x04000233 RID: 563
		public BitWriter bw;
	}
}
