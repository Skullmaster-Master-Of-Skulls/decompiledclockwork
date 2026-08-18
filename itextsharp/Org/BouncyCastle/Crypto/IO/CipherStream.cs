using System;
using System.IO;

namespace Org.BouncyCastle.Crypto.IO
{
	// Token: 0x0200008A RID: 138
	public class CipherStream : Stream
	{
		// Token: 0x0600044B RID: 1099 RVA: 0x00016ACB File Offset: 0x00015ACB
		public CipherStream(Stream stream, IBufferedCipher readCipher, IBufferedCipher writeCipher)
		{
			this.stream = stream;
			if (readCipher != null)
			{
				this.inCipher = readCipher;
				this.mInBuf = null;
			}
			if (writeCipher != null)
			{
				this.outCipher = writeCipher;
			}
		}

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x0600044C RID: 1100 RVA: 0x00016AF5 File Offset: 0x00015AF5
		public IBufferedCipher ReadCipher
		{
			get
			{
				return this.inCipher;
			}
		}

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x0600044D RID: 1101 RVA: 0x00016AFD File Offset: 0x00015AFD
		public IBufferedCipher WriteCipher
		{
			get
			{
				return this.outCipher;
			}
		}

		// Token: 0x0600044E RID: 1102 RVA: 0x00016B08 File Offset: 0x00015B08
		public override int ReadByte()
		{
			if (this.inCipher == null)
			{
				return this.stream.ReadByte();
			}
			if ((this.mInBuf == null || this.mInPos >= this.mInBuf.Length) && !this.FillInBuf())
			{
				return -1;
			}
			return (int)this.mInBuf[this.mInPos++];
		}

		// Token: 0x0600044F RID: 1103 RVA: 0x00016B64 File Offset: 0x00015B64
		public override int Read(byte[] buffer, int offset, int count)
		{
			if (this.inCipher == null)
			{
				return this.stream.Read(buffer, offset, count);
			}
			int num = 0;
			while (num < count && ((this.mInBuf != null && this.mInPos < this.mInBuf.Length) || this.FillInBuf()))
			{
				int num2 = Math.Min(count - num, this.mInBuf.Length - this.mInPos);
				Array.Copy(this.mInBuf, this.mInPos, buffer, offset + num, num2);
				this.mInPos += num2;
				num += num2;
			}
			return num;
		}

		// Token: 0x06000450 RID: 1104 RVA: 0x00016BF1 File Offset: 0x00015BF1
		private bool FillInBuf()
		{
			if (this.inStreamEnded)
			{
				return false;
			}
			this.mInPos = 0;
			do
			{
				this.mInBuf = this.ReadAndProcessBlock();
			}
			while (!this.inStreamEnded && this.mInBuf == null);
			return this.mInBuf != null;
		}

		// Token: 0x06000451 RID: 1105 RVA: 0x00016C2C File Offset: 0x00015C2C
		private byte[] ReadAndProcessBlock()
		{
			int blockSize = this.inCipher.GetBlockSize();
			int num = (blockSize == 0) ? 256 : blockSize;
			byte[] array = new byte[num];
			int num2 = 0;
			for (;;)
			{
				int num3 = this.stream.Read(array, num2, array.Length - num2);
				if (num3 < 1)
				{
					break;
				}
				num2 += num3;
				if (num2 >= array.Length)
				{
					goto IL_4E;
				}
			}
			this.inStreamEnded = true;
			IL_4E:
			byte[] array2 = this.inStreamEnded ? this.inCipher.DoFinal(array, 0, num2) : this.inCipher.ProcessBytes(array);
			if (array2 != null && array2.Length == 0)
			{
				array2 = null;
			}
			return array2;
		}

		// Token: 0x06000452 RID: 1106 RVA: 0x00016CBC File Offset: 0x00015CBC
		public override void Write(byte[] buffer, int offset, int count)
		{
			if (this.outCipher == null)
			{
				this.stream.Write(buffer, offset, count);
				return;
			}
			byte[] array = this.outCipher.ProcessBytes(buffer, offset, count);
			if (array != null)
			{
				this.stream.Write(array, 0, array.Length);
			}
		}

		// Token: 0x06000453 RID: 1107 RVA: 0x00016D04 File Offset: 0x00015D04
		public override void WriteByte(byte b)
		{
			if (this.outCipher == null)
			{
				this.stream.WriteByte(b);
				return;
			}
			byte[] array = this.outCipher.ProcessByte(b);
			if (array != null)
			{
				this.stream.Write(array, 0, array.Length);
			}
		}

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x06000454 RID: 1108 RVA: 0x00016D46 File Offset: 0x00015D46
		public override bool CanRead
		{
			get
			{
				return this.stream.CanRead && this.inCipher != null;
			}
		}

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x06000455 RID: 1109 RVA: 0x00016D63 File Offset: 0x00015D63
		public override bool CanWrite
		{
			get
			{
				return this.stream.CanWrite && this.outCipher != null;
			}
		}

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x06000456 RID: 1110 RVA: 0x00016D80 File Offset: 0x00015D80
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x06000457 RID: 1111 RVA: 0x00016D83 File Offset: 0x00015D83
		public sealed override long Length
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x06000458 RID: 1112 RVA: 0x00016D8A File Offset: 0x00015D8A
		// (set) Token: 0x06000459 RID: 1113 RVA: 0x00016D91 File Offset: 0x00015D91
		public sealed override long Position
		{
			get
			{
				throw new NotSupportedException();
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x0600045A RID: 1114 RVA: 0x00016D98 File Offset: 0x00015D98
		public override void Close()
		{
			if (this.outCipher != null)
			{
				byte[] array = this.outCipher.DoFinal();
				this.stream.Write(array, 0, array.Length);
				this.stream.Flush();
			}
			this.stream.Close();
		}

		// Token: 0x0600045B RID: 1115 RVA: 0x00016DDF File Offset: 0x00015DDF
		public override void Flush()
		{
			this.stream.Flush();
		}

		// Token: 0x0600045C RID: 1116 RVA: 0x00016DEC File Offset: 0x00015DEC
		public sealed override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600045D RID: 1117 RVA: 0x00016DF3 File Offset: 0x00015DF3
		public sealed override void SetLength(long length)
		{
			throw new NotSupportedException();
		}

		// Token: 0x04000229 RID: 553
		internal Stream stream;

		// Token: 0x0400022A RID: 554
		internal IBufferedCipher inCipher;

		// Token: 0x0400022B RID: 555
		internal IBufferedCipher outCipher;

		// Token: 0x0400022C RID: 556
		private byte[] mInBuf;

		// Token: 0x0400022D RID: 557
		private int mInPos;

		// Token: 0x0400022E RID: 558
		private bool inStreamEnded;
	}
}
