using System;
using System.IO;

namespace ICSharpCode.SharpZipLib.Zip.Compression.Streams
{
	// Token: 0x0200000D RID: 13
	public class InflaterInputStream : Stream
	{
		// Token: 0x06000072 RID: 114 RVA: 0x00003B64 File Offset: 0x00002B64
		public InflaterInputStream(Stream baseInputStream) : this(baseInputStream, new Inflater(), 4096)
		{
		}

		// Token: 0x06000073 RID: 115 RVA: 0x00003B77 File Offset: 0x00002B77
		public InflaterInputStream(Stream baseInputStream, Inflater inf) : this(baseInputStream, inf, 4096)
		{
		}

		// Token: 0x06000074 RID: 116 RVA: 0x00003B88 File Offset: 0x00002B88
		public InflaterInputStream(Stream baseInputStream, Inflater inflater, int bufferSize)
		{
			if (baseInputStream == null)
			{
				throw new ArgumentNullException("baseInputStream");
			}
			if (inflater == null)
			{
				throw new ArgumentNullException("inflater");
			}
			if (bufferSize <= 0)
			{
				throw new ArgumentOutOfRangeException("bufferSize");
			}
			this.baseInputStream = baseInputStream;
			this.inf = inflater;
			this.inputBuffer = new InflaterInputBuffer(baseInputStream, bufferSize);
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x06000075 RID: 117 RVA: 0x00003BE8 File Offset: 0x00002BE8
		// (set) Token: 0x06000076 RID: 118 RVA: 0x00003BF0 File Offset: 0x00002BF0
		public bool IsStreamOwner
		{
			get
			{
				return this.isStreamOwner;
			}
			set
			{
				this.isStreamOwner = value;
			}
		}

		// Token: 0x06000077 RID: 119 RVA: 0x00003BFC File Offset: 0x00002BFC
		public long Skip(long count)
		{
			if (count <= 0L)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			if (this.baseInputStream.CanSeek)
			{
				this.baseInputStream.Seek(count, SeekOrigin.Current);
				return count;
			}
			int num = 2048;
			if (count < (long)num)
			{
				num = (int)count;
			}
			byte[] buffer = new byte[num];
			int num2 = 1;
			long num3 = count;
			while (num3 > 0L && num2 > 0)
			{
				if (num3 < (long)num)
				{
					num = (int)num3;
				}
				num2 = this.baseInputStream.Read(buffer, 0, num);
				num3 -= (long)num2;
			}
			return count - num3;
		}

		// Token: 0x06000078 RID: 120 RVA: 0x00003C79 File Offset: 0x00002C79
		protected void StopDecrypting()
		{
			this.inputBuffer.CryptoTransform = null;
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x06000079 RID: 121 RVA: 0x00003C87 File Offset: 0x00002C87
		public virtual int Available
		{
			get
			{
				if (!this.inf.IsFinished)
				{
					return 1;
				}
				return 0;
			}
		}

		// Token: 0x0600007A RID: 122 RVA: 0x00003C9C File Offset: 0x00002C9C
		protected void Fill()
		{
			if (this.inputBuffer.Available <= 0)
			{
				this.inputBuffer.Fill();
				if (this.inputBuffer.Available <= 0)
				{
					throw new SharpZipBaseException("Unexpected EOF");
				}
			}
			this.inputBuffer.SetInflaterInput(this.inf);
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x0600007B RID: 123 RVA: 0x00003CEC File Offset: 0x00002CEC
		public override bool CanRead
		{
			get
			{
				return this.baseInputStream.CanRead;
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x0600007C RID: 124 RVA: 0x00003CF9 File Offset: 0x00002CF9
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x0600007D RID: 125 RVA: 0x00003CFC File Offset: 0x00002CFC
		public override bool CanWrite
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x0600007E RID: 126 RVA: 0x00003CFF File Offset: 0x00002CFF
		public override long Length
		{
			get
			{
				return (long)this.inputBuffer.RawLength;
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x0600007F RID: 127 RVA: 0x00003D0D File Offset: 0x00002D0D
		// (set) Token: 0x06000080 RID: 128 RVA: 0x00003D1A File Offset: 0x00002D1A
		public override long Position
		{
			get
			{
				return this.baseInputStream.Position;
			}
			set
			{
				throw new NotSupportedException("InflaterInputStream Position not supported");
			}
		}

		// Token: 0x06000081 RID: 129 RVA: 0x00003D26 File Offset: 0x00002D26
		public override void Flush()
		{
			this.baseInputStream.Flush();
		}

		// Token: 0x06000082 RID: 130 RVA: 0x00003D33 File Offset: 0x00002D33
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotSupportedException("Seek not supported");
		}

		// Token: 0x06000083 RID: 131 RVA: 0x00003D3F File Offset: 0x00002D3F
		public override void SetLength(long value)
		{
			throw new NotSupportedException("InflaterInputStream SetLength not supported");
		}

		// Token: 0x06000084 RID: 132 RVA: 0x00003D4B File Offset: 0x00002D4B
		public override void Write(byte[] buffer, int offset, int count)
		{
			throw new NotSupportedException("InflaterInputStream Write not supported");
		}

		// Token: 0x06000085 RID: 133 RVA: 0x00003D57 File Offset: 0x00002D57
		public override void WriteByte(byte value)
		{
			throw new NotSupportedException("InflaterInputStream WriteByte not supported");
		}

		// Token: 0x06000086 RID: 134 RVA: 0x00003D63 File Offset: 0x00002D63
		public override IAsyncResult BeginWrite(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
		{
			throw new NotSupportedException("InflaterInputStream BeginWrite not supported");
		}

		// Token: 0x06000087 RID: 135 RVA: 0x00003D6F File Offset: 0x00002D6F
		public override void Close()
		{
			if (!this.isClosed)
			{
				this.isClosed = true;
				if (this.isStreamOwner)
				{
					this.baseInputStream.Close();
				}
			}
		}

		// Token: 0x06000088 RID: 136 RVA: 0x00003D94 File Offset: 0x00002D94
		public override int Read(byte[] buffer, int offset, int count)
		{
			if (this.inf.IsNeedingDictionary)
			{
				throw new SharpZipBaseException("Need a dictionary");
			}
			int num = count;
			for (;;)
			{
				int num2 = this.inf.Inflate(buffer, offset, num);
				offset += num2;
				num -= num2;
				if (num == 0 || this.inf.IsFinished)
				{
					goto IL_65;
				}
				if (this.inf.IsNeedingInput)
				{
					this.Fill();
				}
				else if (num2 == 0)
				{
					break;
				}
			}
			throw new ZipException("Dont know what to do");
			IL_65:
			return count - num;
		}

		// Token: 0x04000050 RID: 80
		protected Inflater inf;

		// Token: 0x04000051 RID: 81
		protected InflaterInputBuffer inputBuffer;

		// Token: 0x04000052 RID: 82
		private Stream baseInputStream;

		// Token: 0x04000053 RID: 83
		protected long csize;

		// Token: 0x04000054 RID: 84
		private bool isClosed;

		// Token: 0x04000055 RID: 85
		private bool isStreamOwner = true;
	}
}
