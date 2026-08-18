using System;

namespace System.Net
{
	// Token: 0x020004B7 RID: 1207
	internal class BufferOffsetSize
	{
		// Token: 0x0600255E RID: 9566 RVA: 0x00095028 File Offset: 0x00094028
		internal BufferOffsetSize(byte[] buffer, int offset, int size, bool copyBuffer)
		{
			if (copyBuffer)
			{
				byte[] array = new byte[size];
				System.Buffer.BlockCopy(buffer, offset, array, 0, size);
				offset = 0;
				buffer = array;
			}
			this.Buffer = buffer;
			this.Offset = offset;
			this.Size = size;
		}

		// Token: 0x0600255F RID: 9567 RVA: 0x0009506B File Offset: 0x0009406B
		internal BufferOffsetSize(byte[] buffer, bool copyBuffer) : this(buffer, 0, buffer.Length, copyBuffer)
		{
		}

		// Token: 0x04002524 RID: 9508
		internal byte[] Buffer;

		// Token: 0x04002525 RID: 9509
		internal int Offset;

		// Token: 0x04002526 RID: 9510
		internal int Size;
	}
}
