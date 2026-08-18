using System;

namespace Ionic.Zlib
{
	// Token: 0x0200000E RID: 14
	internal class WorkItem
	{
		// Token: 0x06000094 RID: 148 RVA: 0x00008700 File Offset: 0x00006900
		public WorkItem(int size, CompressionLevel compressLevel, CompressionStrategy strategy, int ix)
		{
			this.buffer = new byte[size];
			int num = size + (size / 32768 + 1) * 5 * 2;
			this.compressed = new byte[num];
			this.compressor = new ZlibCodec();
			this.compressor.InitializeDeflate(compressLevel, false);
			this.compressor.OutputBuffer = this.compressed;
			this.compressor.InputBuffer = this.buffer;
			this.index = ix;
		}

		// Token: 0x040000C4 RID: 196
		public byte[] buffer;

		// Token: 0x040000C5 RID: 197
		public byte[] compressed;

		// Token: 0x040000C6 RID: 198
		public int crc;

		// Token: 0x040000C7 RID: 199
		public int index;

		// Token: 0x040000C8 RID: 200
		public int ordinal;

		// Token: 0x040000C9 RID: 201
		public int inputBytesAvailable;

		// Token: 0x040000CA RID: 202
		public int compressedBytesAvailable;

		// Token: 0x040000CB RID: 203
		public ZlibCodec compressor;
	}
}
