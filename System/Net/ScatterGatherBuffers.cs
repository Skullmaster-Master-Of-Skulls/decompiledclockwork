using System;

namespace System.Net
{
	// Token: 0x02000532 RID: 1330
	internal class ScatterGatherBuffers
	{
		// Token: 0x060028B1 RID: 10417 RVA: 0x000A81EA File Offset: 0x000A71EA
		internal ScatterGatherBuffers()
		{
		}

		// Token: 0x060028B2 RID: 10418 RVA: 0x000A81FD File Offset: 0x000A71FD
		internal ScatterGatherBuffers(long totalSize)
		{
			if (totalSize > 0L)
			{
				this.currentChunk = this.AllocateMemoryChunk((totalSize > 2147483647L) ? int.MaxValue : ((int)totalSize));
			}
		}

		// Token: 0x060028B3 RID: 10419 RVA: 0x000A8234 File Offset: 0x000A7234
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

		// Token: 0x1700084C RID: 2124
		// (get) Token: 0x060028B4 RID: 10420 RVA: 0x000A8287 File Offset: 0x000A7287
		private bool Empty
		{
			get
			{
				return this.headChunk == null || this.chunkCount == 0;
			}
		}

		// Token: 0x1700084D RID: 2125
		// (get) Token: 0x060028B5 RID: 10421 RVA: 0x000A829C File Offset: 0x000A729C
		internal int Length
		{
			get
			{
				return this.totalLength;
			}
		}

		// Token: 0x060028B6 RID: 10422 RVA: 0x000A82A4 File Offset: 0x000A72A4
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

		// Token: 0x060028B7 RID: 10423 RVA: 0x000A835C File Offset: 0x000A735C
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

		// Token: 0x0400279B RID: 10139
		private ScatterGatherBuffers.MemoryChunk headChunk;

		// Token: 0x0400279C RID: 10140
		private ScatterGatherBuffers.MemoryChunk currentChunk;

		// Token: 0x0400279D RID: 10141
		private int nextChunkLength = 1024;

		// Token: 0x0400279E RID: 10142
		private int totalLength;

		// Token: 0x0400279F RID: 10143
		private int chunkCount;

		// Token: 0x02000533 RID: 1331
		private class MemoryChunk
		{
			// Token: 0x060028B8 RID: 10424 RVA: 0x000A83B1 File Offset: 0x000A73B1
			internal MemoryChunk(int bufferSize)
			{
				this.Buffer = new byte[bufferSize];
			}

			// Token: 0x040027A0 RID: 10144
			internal byte[] Buffer;

			// Token: 0x040027A1 RID: 10145
			internal int FreeOffset;

			// Token: 0x040027A2 RID: 10146
			internal ScatterGatherBuffers.MemoryChunk Next;
		}
	}
}
