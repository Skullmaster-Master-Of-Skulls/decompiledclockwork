using System;

namespace System.IO.Compression
{
	// Token: 0x02000436 RID: 1078
	internal class OutputBuffer
	{
		// Token: 0x0600286A RID: 10346 RVA: 0x000B9D86 File Offset: 0x000B7F86
		internal void UpdateBuffer(byte[] output)
		{
			this.byteBuffer = output;
			this.pos = 0;
		}

		// Token: 0x170009F1 RID: 2545
		// (get) Token: 0x0600286B RID: 10347 RVA: 0x000B9D96 File Offset: 0x000B7F96
		internal int BytesWritten
		{
			get
			{
				return this.pos;
			}
		}

		// Token: 0x170009F2 RID: 2546
		// (get) Token: 0x0600286C RID: 10348 RVA: 0x000B9D9E File Offset: 0x000B7F9E
		internal int FreeBytes
		{
			get
			{
				return this.byteBuffer.Length - this.pos;
			}
		}

		// Token: 0x0600286D RID: 10349 RVA: 0x000B9DB0 File Offset: 0x000B7FB0
		internal void WriteUInt16(ushort value)
		{
			byte[] array = this.byteBuffer;
			int num = this.pos;
			this.pos = num + 1;
			array[num] = (byte)value;
			byte[] array2 = this.byteBuffer;
			num = this.pos;
			this.pos = num + 1;
			array2[num] = (byte)(value >> 8);
		}

		// Token: 0x0600286E RID: 10350 RVA: 0x000B9DF4 File Offset: 0x000B7FF4
		internal void WriteBits(int n, uint bits)
		{
			this.bitBuf |= bits << this.bitCount;
			this.bitCount += n;
			if (this.bitCount >= 16)
			{
				byte[] array = this.byteBuffer;
				int num = this.pos;
				this.pos = num + 1;
				array[num] = (byte)this.bitBuf;
				byte[] array2 = this.byteBuffer;
				num = this.pos;
				this.pos = num + 1;
				array2[num] = (byte)(this.bitBuf >> 8);
				this.bitCount -= 16;
				this.bitBuf >>= 16;
			}
		}

		// Token: 0x0600286F RID: 10351 RVA: 0x000B9E90 File Offset: 0x000B8090
		internal void FlushBits()
		{
			while (this.bitCount >= 8)
			{
				byte[] array = this.byteBuffer;
				int num = this.pos;
				this.pos = num + 1;
				array[num] = (byte)this.bitBuf;
				this.bitCount -= 8;
				this.bitBuf >>= 8;
			}
			if (this.bitCount > 0)
			{
				byte[] array2 = this.byteBuffer;
				int num = this.pos;
				this.pos = num + 1;
				array2[num] = (byte)this.bitBuf;
				this.bitBuf = 0U;
				this.bitCount = 0;
			}
		}

		// Token: 0x06002870 RID: 10352 RVA: 0x000B9F19 File Offset: 0x000B8119
		internal void WriteBytes(byte[] byteArray, int offset, int count)
		{
			if (this.bitCount == 0)
			{
				Array.Copy(byteArray, offset, this.byteBuffer, this.pos, count);
				this.pos += count;
				return;
			}
			this.WriteBytesUnaligned(byteArray, offset, count);
		}

		// Token: 0x06002871 RID: 10353 RVA: 0x000B9F50 File Offset: 0x000B8150
		private void WriteBytesUnaligned(byte[] byteArray, int offset, int count)
		{
			for (int i = 0; i < count; i++)
			{
				byte b = byteArray[offset + i];
				this.WriteByteUnaligned(b);
			}
		}

		// Token: 0x06002872 RID: 10354 RVA: 0x000B9F76 File Offset: 0x000B8176
		private void WriteByteUnaligned(byte b)
		{
			this.WriteBits(8, (uint)b);
		}

		// Token: 0x170009F3 RID: 2547
		// (get) Token: 0x06002873 RID: 10355 RVA: 0x000B9F80 File Offset: 0x000B8180
		internal int BitsInBuffer
		{
			get
			{
				return this.bitCount / 8 + 1;
			}
		}

		// Token: 0x06002874 RID: 10356 RVA: 0x000B9F8C File Offset: 0x000B818C
		internal OutputBuffer.BufferState DumpState()
		{
			OutputBuffer.BufferState result;
			result.pos = this.pos;
			result.bitBuf = this.bitBuf;
			result.bitCount = this.bitCount;
			return result;
		}

		// Token: 0x06002875 RID: 10357 RVA: 0x000B9FC1 File Offset: 0x000B81C1
		internal void RestoreState(OutputBuffer.BufferState state)
		{
			this.pos = state.pos;
			this.bitBuf = state.bitBuf;
			this.bitCount = state.bitCount;
		}

		// Token: 0x04002236 RID: 8758
		private byte[] byteBuffer;

		// Token: 0x04002237 RID: 8759
		private int pos;

		// Token: 0x04002238 RID: 8760
		private uint bitBuf;

		// Token: 0x04002239 RID: 8761
		private int bitCount;

		// Token: 0x0200082C RID: 2092
		internal struct BufferState
		{
			// Token: 0x040035EE RID: 13806
			internal int pos;

			// Token: 0x040035EF RID: 13807
			internal uint bitBuf;

			// Token: 0x040035F0 RID: 13808
			internal int bitCount;
		}
	}
}
