using System;

namespace ICSharpCode.SharpZipLib.Zip
{
	// Token: 0x02000070 RID: 112
	public class DescriptorData
	{
		// Token: 0x170000FA RID: 250
		// (get) Token: 0x0600045B RID: 1115 RVA: 0x00017078 File Offset: 0x00016078
		// (set) Token: 0x0600045C RID: 1116 RVA: 0x00017080 File Offset: 0x00016080
		public long CompressedSize
		{
			get
			{
				return this.compressedSize;
			}
			set
			{
				this.compressedSize = value;
			}
		}

		// Token: 0x170000FB RID: 251
		// (get) Token: 0x0600045D RID: 1117 RVA: 0x00017089 File Offset: 0x00016089
		// (set) Token: 0x0600045E RID: 1118 RVA: 0x00017091 File Offset: 0x00016091
		public long Size
		{
			get
			{
				return this.size;
			}
			set
			{
				this.size = value;
			}
		}

		// Token: 0x170000FC RID: 252
		// (get) Token: 0x0600045F RID: 1119 RVA: 0x0001709A File Offset: 0x0001609A
		// (set) Token: 0x06000460 RID: 1120 RVA: 0x000170A2 File Offset: 0x000160A2
		public long Crc
		{
			get
			{
				return this.crc;
			}
			set
			{
				this.crc = (value & (long)((ulong)-1));
			}
		}

		// Token: 0x040002E4 RID: 740
		private long size;

		// Token: 0x040002E5 RID: 741
		private long compressedSize;

		// Token: 0x040002E6 RID: 742
		private long crc;
	}
}
