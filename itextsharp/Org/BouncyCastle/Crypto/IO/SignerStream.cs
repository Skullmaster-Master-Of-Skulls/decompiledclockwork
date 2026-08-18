using System;
using System.IO;

namespace Org.BouncyCastle.Crypto.IO
{
	// Token: 0x0200042F RID: 1071
	public class SignerStream : Stream
	{
		// Token: 0x06002475 RID: 9333 RVA: 0x000DE6E2 File Offset: 0x000DD6E2
		public SignerStream(Stream stream, ISigner readSigner, ISigner writeSigner)
		{
			this.stream = stream;
			this.inSigner = readSigner;
			this.outSigner = writeSigner;
		}

		// Token: 0x06002476 RID: 9334 RVA: 0x000DE6FF File Offset: 0x000DD6FF
		public virtual ISigner ReadSigner()
		{
			return this.inSigner;
		}

		// Token: 0x06002477 RID: 9335 RVA: 0x000DE707 File Offset: 0x000DD707
		public virtual ISigner WriteSigner()
		{
			return this.outSigner;
		}

		// Token: 0x06002478 RID: 9336 RVA: 0x000DE710 File Offset: 0x000DD710
		public override int Read(byte[] buffer, int offset, int count)
		{
			int num = this.stream.Read(buffer, offset, count);
			if (this.inSigner != null && num > 0)
			{
				this.inSigner.BlockUpdate(buffer, offset, num);
			}
			return num;
		}

		// Token: 0x06002479 RID: 9337 RVA: 0x000DE748 File Offset: 0x000DD748
		public override int ReadByte()
		{
			int num = this.stream.ReadByte();
			if (this.inSigner != null && num >= 0)
			{
				this.inSigner.Update((byte)num);
			}
			return num;
		}

		// Token: 0x0600247A RID: 9338 RVA: 0x000DE77B File Offset: 0x000DD77B
		public override void Write(byte[] buffer, int offset, int count)
		{
			if (this.outSigner != null && count > 0)
			{
				this.outSigner.BlockUpdate(buffer, offset, count);
			}
			this.stream.Write(buffer, offset, count);
		}

		// Token: 0x0600247B RID: 9339 RVA: 0x000DE7A5 File Offset: 0x000DD7A5
		public override void WriteByte(byte b)
		{
			if (this.outSigner != null)
			{
				this.outSigner.Update(b);
			}
			this.stream.WriteByte(b);
		}

		// Token: 0x17000648 RID: 1608
		// (get) Token: 0x0600247C RID: 9340 RVA: 0x000DE7C7 File Offset: 0x000DD7C7
		public override bool CanRead
		{
			get
			{
				return this.stream.CanRead;
			}
		}

		// Token: 0x17000649 RID: 1609
		// (get) Token: 0x0600247D RID: 9341 RVA: 0x000DE7D4 File Offset: 0x000DD7D4
		public override bool CanWrite
		{
			get
			{
				return this.stream.CanWrite;
			}
		}

		// Token: 0x1700064A RID: 1610
		// (get) Token: 0x0600247E RID: 9342 RVA: 0x000DE7E1 File Offset: 0x000DD7E1
		public override bool CanSeek
		{
			get
			{
				return this.stream.CanSeek;
			}
		}

		// Token: 0x1700064B RID: 1611
		// (get) Token: 0x0600247F RID: 9343 RVA: 0x000DE7EE File Offset: 0x000DD7EE
		public override long Length
		{
			get
			{
				return this.stream.Length;
			}
		}

		// Token: 0x1700064C RID: 1612
		// (get) Token: 0x06002480 RID: 9344 RVA: 0x000DE7FB File Offset: 0x000DD7FB
		// (set) Token: 0x06002481 RID: 9345 RVA: 0x000DE808 File Offset: 0x000DD808
		public override long Position
		{
			get
			{
				return this.stream.Position;
			}
			set
			{
				this.stream.Position = value;
			}
		}

		// Token: 0x06002482 RID: 9346 RVA: 0x000DE816 File Offset: 0x000DD816
		public override void Close()
		{
			this.stream.Close();
		}

		// Token: 0x06002483 RID: 9347 RVA: 0x000DE823 File Offset: 0x000DD823
		public override void Flush()
		{
			this.stream.Flush();
		}

		// Token: 0x06002484 RID: 9348 RVA: 0x000DE830 File Offset: 0x000DD830
		public override long Seek(long offset, SeekOrigin origin)
		{
			return this.stream.Seek(offset, origin);
		}

		// Token: 0x06002485 RID: 9349 RVA: 0x000DE83F File Offset: 0x000DD83F
		public override void SetLength(long length)
		{
			this.stream.SetLength(length);
		}

		// Token: 0x04001982 RID: 6530
		protected readonly Stream stream;

		// Token: 0x04001983 RID: 6531
		protected readonly ISigner inSigner;

		// Token: 0x04001984 RID: 6532
		protected readonly ISigner outSigner;
	}
}
