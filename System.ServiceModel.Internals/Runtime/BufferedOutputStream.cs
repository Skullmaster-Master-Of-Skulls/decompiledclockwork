using System;
using System.IO;

namespace System.Runtime
{
	// Token: 0x0200000E RID: 14
	internal class BufferedOutputStream : Stream
	{
		// Token: 0x06000058 RID: 88 RVA: 0x000031BA File Offset: 0x000013BA
		public BufferedOutputStream()
		{
			this.chunks = new byte[4][];
		}

		// Token: 0x06000059 RID: 89 RVA: 0x000031CE File Offset: 0x000013CE
		public BufferedOutputStream(int initialSize, int maxSize, InternalBufferManager bufferManager) : this()
		{
			this.Reinitialize(initialSize, maxSize, bufferManager);
		}

		// Token: 0x0600005A RID: 90 RVA: 0x000031DF File Offset: 0x000013DF
		public BufferedOutputStream(int maxSize) : this(0, maxSize, InternalBufferManager.Create(0L, int.MaxValue))
		{
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x0600005B RID: 91 RVA: 0x000031F5 File Offset: 0x000013F5
		public override bool CanRead
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x0600005C RID: 92 RVA: 0x000031F5 File Offset: 0x000013F5
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x0600005D RID: 93 RVA: 0x00002940 File Offset: 0x00000B40
		public override bool CanWrite
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600005E RID: 94 RVA: 0x000031F8 File Offset: 0x000013F8
		public override long Length
		{
			get
			{
				return (long)this.totalSize;
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x0600005F RID: 95 RVA: 0x00003201 File Offset: 0x00001401
		// (set) Token: 0x06000060 RID: 96 RVA: 0x00003201 File Offset: 0x00001401
		public override long Position
		{
			get
			{
				throw Fx.Exception.AsError(new NotSupportedException(InternalSR.SeekNotSupported));
			}
			set
			{
				throw Fx.Exception.AsError(new NotSupportedException(InternalSR.SeekNotSupported));
			}
		}

		// Token: 0x06000061 RID: 97 RVA: 0x00003217 File Offset: 0x00001417
		public void Reinitialize(int initialSize, int maxSizeQuota, InternalBufferManager bufferManager)
		{
			this.Reinitialize(initialSize, maxSizeQuota, maxSizeQuota, bufferManager);
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00003224 File Offset: 0x00001424
		public void Reinitialize(int initialSize, int maxSizeQuota, int effectiveMaxSize, InternalBufferManager bufferManager)
		{
			this.maxSizeQuota = maxSizeQuota;
			this.maxSize = effectiveMaxSize;
			this.bufferManager = bufferManager;
			this.currentChunk = bufferManager.TakeBuffer(initialSize);
			this.currentChunkSize = 0;
			this.totalSize = 0;
			this.chunkCount = 1;
			this.chunks[0] = this.currentChunk;
			this.initialized = true;
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00003280 File Offset: 0x00001480
		private void AllocNextChunk(int minimumChunkSize)
		{
			int num;
			if (this.currentChunk.Length > 1073741823)
			{
				num = int.MaxValue;
			}
			else
			{
				num = this.currentChunk.Length * 2;
			}
			if (minimumChunkSize > num)
			{
				num = minimumChunkSize;
			}
			byte[] array = this.bufferManager.TakeBuffer(num);
			if (this.chunkCount == this.chunks.Length)
			{
				byte[][] destinationArray = new byte[this.chunks.Length * 2][];
				Array.Copy(this.chunks, destinationArray, this.chunks.Length);
				this.chunks = destinationArray;
			}
			byte[][] array2 = this.chunks;
			int num2 = this.chunkCount;
			this.chunkCount = num2 + 1;
			array2[num2] = array;
			this.currentChunk = array;
			this.currentChunkSize = 0;
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00003324 File Offset: 0x00001524
		public override IAsyncResult BeginRead(byte[] buffer, int offset, int size, AsyncCallback callback, object state)
		{
			throw Fx.Exception.AsError(new NotSupportedException(InternalSR.ReadNotSupported));
		}

		// Token: 0x06000065 RID: 101 RVA: 0x00003324 File Offset: 0x00001524
		public override int EndRead(IAsyncResult result)
		{
			throw Fx.Exception.AsError(new NotSupportedException(InternalSR.ReadNotSupported));
		}

		// Token: 0x06000066 RID: 102 RVA: 0x0000333A File Offset: 0x0000153A
		public override IAsyncResult BeginWrite(byte[] buffer, int offset, int size, AsyncCallback callback, object state)
		{
			this.Write(buffer, offset, size);
			return new CompletedAsyncResult(callback, state);
		}

		// Token: 0x06000067 RID: 103 RVA: 0x0000334E File Offset: 0x0000154E
		public override void EndWrite(IAsyncResult result)
		{
			CompletedAsyncResult.End(result);
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00003358 File Offset: 0x00001558
		public void Clear()
		{
			if (!this.callerReturnsBuffer)
			{
				for (int i = 0; i < this.chunkCount; i++)
				{
					this.bufferManager.ReturnBuffer(this.chunks[i]);
					this.chunks[i] = null;
				}
			}
			this.callerReturnsBuffer = false;
			this.initialized = false;
			this.bufferReturned = false;
			this.chunkCount = 0;
			this.currentChunk = null;
		}

		// Token: 0x06000069 RID: 105 RVA: 0x000033BD File Offset: 0x000015BD
		public override void Close()
		{
		}

		// Token: 0x0600006A RID: 106 RVA: 0x000033BD File Offset: 0x000015BD
		public override void Flush()
		{
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00003324 File Offset: 0x00001524
		public override int Read(byte[] buffer, int offset, int size)
		{
			throw Fx.Exception.AsError(new NotSupportedException(InternalSR.ReadNotSupported));
		}

		// Token: 0x0600006C RID: 108 RVA: 0x00003324 File Offset: 0x00001524
		public override int ReadByte()
		{
			throw Fx.Exception.AsError(new NotSupportedException(InternalSR.ReadNotSupported));
		}

		// Token: 0x0600006D RID: 109 RVA: 0x00003201 File Offset: 0x00001401
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw Fx.Exception.AsError(new NotSupportedException(InternalSR.SeekNotSupported));
		}

		// Token: 0x0600006E RID: 110 RVA: 0x00003201 File Offset: 0x00001401
		public override void SetLength(long value)
		{
			throw Fx.Exception.AsError(new NotSupportedException(InternalSR.SeekNotSupported));
		}

		// Token: 0x0600006F RID: 111 RVA: 0x000033C0 File Offset: 0x000015C0
		public MemoryStream ToMemoryStream()
		{
			int count;
			byte[] buffer = this.ToArray(out count);
			return new MemoryStream(buffer, 0, count);
		}

		// Token: 0x06000070 RID: 112 RVA: 0x000033E0 File Offset: 0x000015E0
		public byte[] ToArray(out int bufferSize)
		{
			byte[] array;
			if (this.chunkCount == 1)
			{
				array = this.currentChunk;
				bufferSize = this.currentChunkSize;
				this.callerReturnsBuffer = true;
			}
			else
			{
				array = this.bufferManager.TakeBuffer(this.totalSize);
				int num = 0;
				int num2 = this.chunkCount - 1;
				for (int i = 0; i < num2; i++)
				{
					byte[] array2 = this.chunks[i];
					Buffer.BlockCopy(array2, 0, array, num, array2.Length);
					num += array2.Length;
				}
				Buffer.BlockCopy(this.currentChunk, 0, array, num, this.currentChunkSize);
				bufferSize = this.totalSize;
			}
			this.bufferReturned = true;
			return array;
		}

		// Token: 0x06000071 RID: 113 RVA: 0x0000347A File Offset: 0x0000167A
		public void Skip(int size)
		{
			this.WriteCore(null, 0, size);
		}

		// Token: 0x06000072 RID: 114 RVA: 0x00003485 File Offset: 0x00001685
		public override void Write(byte[] buffer, int offset, int size)
		{
			this.WriteCore(buffer, offset, size);
		}

		// Token: 0x06000073 RID: 115 RVA: 0x00003490 File Offset: 0x00001690
		protected virtual Exception CreateQuotaExceededException(int maxSizeQuota)
		{
			return new InvalidOperationException(InternalSR.BufferedOutputStreamQuotaExceeded(maxSizeQuota));
		}

		// Token: 0x06000074 RID: 116 RVA: 0x000034A4 File Offset: 0x000016A4
		private void WriteCore(byte[] buffer, int offset, int size)
		{
			if (size < 0)
			{
				throw Fx.Exception.ArgumentOutOfRange("size", size, InternalSR.ValueMustBeNonNegative);
			}
			if (2147483647 - size < this.totalSize)
			{
				throw Fx.Exception.AsError(this.CreateQuotaExceededException(this.maxSizeQuota));
			}
			int num = this.totalSize + size;
			if (num > this.maxSize)
			{
				throw Fx.Exception.AsError(this.CreateQuotaExceededException(this.maxSizeQuota));
			}
			int num2 = this.currentChunk.Length - this.currentChunkSize;
			if (size > num2)
			{
				if (num2 > 0)
				{
					if (buffer != null)
					{
						Buffer.BlockCopy(buffer, offset, this.currentChunk, this.currentChunkSize, num2);
					}
					this.currentChunkSize = this.currentChunk.Length;
					offset += num2;
					size -= num2;
				}
				this.AllocNextChunk(size);
			}
			if (buffer != null)
			{
				Buffer.BlockCopy(buffer, offset, this.currentChunk, this.currentChunkSize, size);
			}
			this.totalSize = num;
			this.currentChunkSize += size;
		}

		// Token: 0x06000075 RID: 117 RVA: 0x0000359C File Offset: 0x0000179C
		public override void WriteByte(byte value)
		{
			if (this.totalSize == this.maxSize)
			{
				throw Fx.Exception.AsError(this.CreateQuotaExceededException(this.maxSize));
			}
			if (this.currentChunkSize == this.currentChunk.Length)
			{
				this.AllocNextChunk(1);
			}
			byte[] array = this.currentChunk;
			int num = this.currentChunkSize;
			this.currentChunkSize = num + 1;
			array[num] = value;
		}

		// Token: 0x04000032 RID: 50
		private InternalBufferManager bufferManager;

		// Token: 0x04000033 RID: 51
		private byte[][] chunks;

		// Token: 0x04000034 RID: 52
		private int chunkCount;

		// Token: 0x04000035 RID: 53
		private byte[] currentChunk;

		// Token: 0x04000036 RID: 54
		private int currentChunkSize;

		// Token: 0x04000037 RID: 55
		private int maxSize;

		// Token: 0x04000038 RID: 56
		private int maxSizeQuota;

		// Token: 0x04000039 RID: 57
		private int totalSize;

		// Token: 0x0400003A RID: 58
		private bool callerReturnsBuffer;

		// Token: 0x0400003B RID: 59
		private bool bufferReturned;

		// Token: 0x0400003C RID: 60
		private bool initialized;
	}
}
