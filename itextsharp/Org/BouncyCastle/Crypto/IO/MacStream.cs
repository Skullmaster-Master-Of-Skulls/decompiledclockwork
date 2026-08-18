using System;
using System.IO;

namespace Org.BouncyCastle.Crypto.IO
{
	// Token: 0x020001F2 RID: 498
	public class MacStream : Stream
	{
		// Token: 0x06001363 RID: 4963 RVA: 0x0006EFDC File Offset: 0x0006DFDC
		public MacStream(Stream stream, IMac readMac, IMac writeMac)
		{
			this.stream = stream;
			this.inMac = readMac;
			this.outMac = writeMac;
		}

		// Token: 0x06001364 RID: 4964 RVA: 0x0006EFF9 File Offset: 0x0006DFF9
		public virtual IMac ReadMac()
		{
			return this.inMac;
		}

		// Token: 0x06001365 RID: 4965 RVA: 0x0006F001 File Offset: 0x0006E001
		public virtual IMac WriteMac()
		{
			return this.outMac;
		}

		// Token: 0x06001366 RID: 4966 RVA: 0x0006F00C File Offset: 0x0006E00C
		public override int Read(byte[] buffer, int offset, int count)
		{
			int num = this.stream.Read(buffer, offset, count);
			if (this.inMac != null && num > 0)
			{
				this.inMac.BlockUpdate(buffer, offset, num);
			}
			return num;
		}

		// Token: 0x06001367 RID: 4967 RVA: 0x0006F044 File Offset: 0x0006E044
		public override int ReadByte()
		{
			int num = this.stream.ReadByte();
			if (this.inMac != null && num >= 0)
			{
				this.inMac.Update((byte)num);
			}
			return num;
		}

		// Token: 0x06001368 RID: 4968 RVA: 0x0006F077 File Offset: 0x0006E077
		public override void Write(byte[] buffer, int offset, int count)
		{
			if (this.outMac != null && count > 0)
			{
				this.outMac.BlockUpdate(buffer, offset, count);
			}
			this.stream.Write(buffer, offset, count);
		}

		// Token: 0x06001369 RID: 4969 RVA: 0x0006F0A1 File Offset: 0x0006E0A1
		public override void WriteByte(byte b)
		{
			if (this.outMac != null)
			{
				this.outMac.Update(b);
			}
			this.stream.WriteByte(b);
		}

		// Token: 0x1700038B RID: 907
		// (get) Token: 0x0600136A RID: 4970 RVA: 0x0006F0C3 File Offset: 0x0006E0C3
		public override bool CanRead
		{
			get
			{
				return this.stream.CanRead;
			}
		}

		// Token: 0x1700038C RID: 908
		// (get) Token: 0x0600136B RID: 4971 RVA: 0x0006F0D0 File Offset: 0x0006E0D0
		public override bool CanWrite
		{
			get
			{
				return this.stream.CanWrite;
			}
		}

		// Token: 0x1700038D RID: 909
		// (get) Token: 0x0600136C RID: 4972 RVA: 0x0006F0DD File Offset: 0x0006E0DD
		public override bool CanSeek
		{
			get
			{
				return this.stream.CanSeek;
			}
		}

		// Token: 0x1700038E RID: 910
		// (get) Token: 0x0600136D RID: 4973 RVA: 0x0006F0EA File Offset: 0x0006E0EA
		public override long Length
		{
			get
			{
				return this.stream.Length;
			}
		}

		// Token: 0x1700038F RID: 911
		// (get) Token: 0x0600136E RID: 4974 RVA: 0x0006F0F7 File Offset: 0x0006E0F7
		// (set) Token: 0x0600136F RID: 4975 RVA: 0x0006F104 File Offset: 0x0006E104
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

		// Token: 0x06001370 RID: 4976 RVA: 0x0006F112 File Offset: 0x0006E112
		public override void Close()
		{
			this.stream.Close();
		}

		// Token: 0x06001371 RID: 4977 RVA: 0x0006F11F File Offset: 0x0006E11F
		public override void Flush()
		{
			this.stream.Flush();
		}

		// Token: 0x06001372 RID: 4978 RVA: 0x0006F12C File Offset: 0x0006E12C
		public override long Seek(long offset, SeekOrigin origin)
		{
			return this.stream.Seek(offset, origin);
		}

		// Token: 0x06001373 RID: 4979 RVA: 0x0006F13B File Offset: 0x0006E13B
		public override void SetLength(long length)
		{
			this.stream.SetLength(length);
		}

		// Token: 0x04000D90 RID: 3472
		protected readonly Stream stream;

		// Token: 0x04000D91 RID: 3473
		protected readonly IMac inMac;

		// Token: 0x04000D92 RID: 3474
		protected readonly IMac outMac;
	}
}
