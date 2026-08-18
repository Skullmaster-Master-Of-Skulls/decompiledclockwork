using System;

namespace System.Net
{
	// Token: 0x02000196 RID: 406
	internal class BufferOffsetSize
	{
		// Token: 0x06000FBF RID: 4031 RVA: 0x00052388 File Offset: 0x00050588
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

		// Token: 0x06000FC0 RID: 4032 RVA: 0x000523CB File Offset: 0x000505CB
		internal BufferOffsetSize(byte[] buffer, bool copyBuffer) : this(buffer, 0, buffer.Length, copyBuffer)
		{
		}

		// Token: 0x040012E6 RID: 4838
		internal byte[] Buffer;

		// Token: 0x040012E7 RID: 4839
		internal int Offset;

		// Token: 0x040012E8 RID: 4840
		internal int Size;
	}
}
