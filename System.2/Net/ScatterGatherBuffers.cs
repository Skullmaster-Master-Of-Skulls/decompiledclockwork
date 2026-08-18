using System;

namespace System.Net
{
	// Token: 0x02000208 RID: 520
	internal class ScatterGatherBuffers
	{
		// Token: 0x06001376 RID: 4982 RVA: 0x0006635F File Offset: 0x0006455F
		internal ScatterGatherBuffers()
		{
		}

		// Token: 0x06001377 RID: 4983 RVA: 0x00066372 File Offset: 0x00064572
		internal ScatterGatherBuffers(long totalSize)
		{
			if (totalSize > 0L)
			{
				this.currentChunk = this.AllocateMemoryChunk((totalSize > 2147483647L) ? int.MaxValue : ((int)totalSize));
			}
		}

		// Token: 0x06001378 RID: 4984 RVA: 0x000663A8 File Offset: 0x000645A8
		internal BufferOffsetSize[] GetBuffers()
		{
			if (this.Empty)
			{
				return null;
			}
			BufferOffsetSize[] array = new BufferOffsetSize[this.chunkCount];
			int num = 0;
			for (ScatterGatherBuffers.MemoryChunk next = this.headChunk; next != null; next = next.Next)
			{
				array[num] = new BufferOffsetSize(next.Buffer, 0, next.FreeOffset, false);
				num++;
			}
			return array;
		}

		// Token: 0x1700041F RID: 1055
		// (get) Token: 0x06001379 RID: 4985 RVA: 0x000663FB File Offset: 0x000645FB
		private bool Empty
		{
			get
			{
				return this.headChunk == null || this.chunkCount == 0;
			}
		}

		// Token: 0x17000420 RID: 1056
		// (get) Token: 0x0600137A RID: 4986 RVA: 0x00066410 File Offset: 0x00064610
		internal int Length
		{
			get
			{
				return this.totalLength;
			}
		}

		// Token: 0x0600137B RID: 4987 RVA: 0x00066418 File Offset: 0x00064618
		internal void Write(byte[] buffer, int offset, int count)
		{
			while (count > 0)
			{
				int num = this.Empty ? 0 : (this.currentChunk.Buffer.Length - this.currentChunk.FreeOffset);
				if (num == 0)
				{
					ScatterGatherBuffers.MemoryChunk next = this.AllocateMemoryChunk(count);
					if (this.currentChunk != null)
					{
						this.currentChunk.Next = next;
					}
					this.currentChunk = next;
				}
				int num2 = (count < num) ? count : num;
				Buffer.BlockCopy(buffer, offset, this.currentChunk.Buffer, this.currentChunk.FreeOffset, num2);
				offset += num2;
				count -= num2;
				this.totalLength += num2;
				this.currentChunk.FreeOffset += num2;
			}
		}

		// Token: 0x0600137C RID: 4988 RVA: 0x000664D0 File Offset: 0x000646D0
		private ScatterGatherBuffers.MemoryChunk AllocateMemoryChunk(int newSize)
		{
			if (newSize > this.nextChunkLength)
			{
				this.nextChunkLength = newSize;
			}
			ScatterGatherBuffers.MemoryChunk result = new ScatterGatherBuffers.MemoryChunk(this.nextChunkLength);
			if (this.Empty)
			{
				this.headChunk = result;
			}
			this.nextChunkLength *= 2;
			this.chunkCount++;
			return result;
		}

		// Token: 0x04001563 RID: 5475
		private ScatterGatherBuffers.MemoryChunk headChunk;

		// Token: 0x04001564 RID: 5476
		private ScatterGatherBuffers.MemoryChunk currentChunk;

		// Token: 0x04001565 RID: 5477
		private int nextChunkLength = 1024;

		// Token: 0x04001566 RID: 5478
		private int totalLength;

		// Token: 0x04001567 RID: 5479
		private int chunkCount;

		// Token: 0x02000758 RID: 1880
		private class MemoryChunk
		{
			// Token: 0x06004214 RID: 16916 RVA: 0x001128DB File Offset: 0x00110ADB
			internal MemoryChunk(int bufferSize)
			{
				this.Buffer = new byte[bufferSize];
			}

			// Token: 0x04003223 RID: 12835
			internal byte[] Buffer;

			// Token: 0x04003224 RID: 12836
			internal int FreeOffset;

			// Token: 0x04003225 RID: 12837
			internal ScatterGatherBuffers.MemoryChunk Next;
		}
	}
}
