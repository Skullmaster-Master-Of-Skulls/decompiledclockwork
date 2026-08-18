using System;
using System.IO;

namespace Org.BouncyCastle.Crypto.IO
{
	// Token: 0x020004C2 RID: 1218
	public class DigestStream : Stream
	{
		// Token: 0x0600297F RID: 10623 RVA: 0x000FCC83 File Offset: 0x000FBC83
		public DigestStream(Stream stream, IDigest readDigest, IDigest writeDigest)
		{
			this.stream = stream;
			this.inDigest = readDigest;
			this.outDigest = writeDigest;
		}

		// Token: 0x06002980 RID: 10624 RVA: 0x000FCCA0 File Offset: 0x000FBCA0
		public virtual IDigest ReadDigest()
		{
			return this.inDigest;
		}

		// Token: 0x06002981 RID: 10625 RVA: 0x000FCCA8 File Offset: 0x000FBCA8
		public virtual IDigest WriteDigest()
		{
			return this.outDigest;
		}

		// Token: 0x06002982 RID: 10626 RVA: 0x000FCCB0 File Offset: 0x000FBCB0
		public override int Read(byte[] buffer, int offset, int count)
		{
			int num = this.stream.Read(buffer, offset, count);
			if (this.inDigest != null && num > 0)
			{
				this.inDigest.BlockUpdate(buffer, offset, num);
			}
			return num;
		}

		// Token: 0x06002983 RID: 10627 RVA: 0x000FCCE8 File Offset: 0x000FBCE8
		public override int ReadByte()
		{
			int num = this.stream.ReadByte();
			if (this.inDigest != null && num >= 0)
			{
				this.inDigest.Update((byte)num);
			}
			return num;
		}

		// Token: 0x06002984 RID: 10628 RVA: 0x000FCD1B File Offset: 0x000FBD1B
		public override void Write(byte[] buffer, int offset, int count)
		{
			if (this.outDigest != null && count > 0)
			{
				this.outDigest.BlockUpdate(buffer, offset, count);
			}
			this.stream.Write(buffer, offset, count);
		}

		// Token: 0x06002985 RID: 10629 RVA: 0x000FCD45 File Offset: 0x000FBD45
		public override void WriteByte(byte b)
		{
			if (this.outDigest != null)
			{
				this.outDigest.Update(b);
			}
			this.stream.WriteByte(b);
		}

		// Token: 0x1700072C RID: 1836
		// (get) Token: 0x06002986 RID: 10630 RVA: 0x000FCD67 File Offset: 0x000FBD67
		public override bool CanRead
		{
			get
			{
				return this.stream.CanRead;
			}
		}

		// Token: 0x1700072D RID: 1837
		// (get) Token: 0x06002987 RID: 10631 RVA: 0x000FCD74 File Offset: 0x000FBD74
		public override bool CanWrite
		{
			get
			{
				return this.stream.CanWrite;
			}
		}

		// Token: 0x1700072E RID: 1838
		// (get) Token: 0x06002988 RID: 10632 RVA: 0x000FCD81 File Offset: 0x000FBD81
		public override bool CanSeek
		{
			get
			{
				return this.stream.CanSeek;
			}
		}

		// Token: 0x1700072F RID: 1839
		// (get) Token: 0x06002989 RID: 10633 RVA: 0x000FCD8E File Offset: 0x000FBD8E
		public override long Length
		{
			get
			{
				return this.stream.Length;
			}
		}

		// Token: 0x17000730 RID: 1840
		// (get) Token: 0x0600298A RID: 10634 RVA: 0x000FCD9B File Offset: 0x000FBD9B
		// (set) Token: 0x0600298B RID: 10635 RVA: 0x000FCDA8 File Offset: 0x000FBDA8
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

		// Token: 0x0600298C RID: 10636 RVA: 0x000FCDB6 File Offset: 0x000FBDB6
		public override void Close()
		{
			this.stream.Close();
		}

		// Token: 0x0600298D RID: 10637 RVA: 0x000FCDC3 File Offset: 0x000FBDC3
		public override void Flush()
		{
			this.stream.Flush();
		}

		// Token: 0x0600298E RID: 10638 RVA: 0x000FCDD0 File Offset: 0x000FBDD0
		public override long Seek(long offset, SeekOrigin origin)
		{
			return this.stream.Seek(offset, origin);
		}

		// Token: 0x0600298F RID: 10639 RVA: 0x000FCDDF File Offset: 0x000FBDDF
		public override void SetLength(long length)
		{
			this.stream.SetLength(length);
		}

		// Token: 0x04001CFF RID: 7423
		protected readonly Stream stream;

		// Token: 0x04001D00 RID: 7424
		protected readonly IDigest inDigest;

		// Token: 0x04001D01 RID: 7425
		protected readonly IDigest outDigest;
	}
}
