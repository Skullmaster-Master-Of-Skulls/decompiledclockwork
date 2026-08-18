using System;
using System.IO;
using Org.BouncyCastle.Asn1.Utilities;

namespace Org.BouncyCastle.Utilities.IO
{
	// Token: 0x020004FB RID: 1275
	public class PushbackStream : FilterStream
	{
		// Token: 0x06002B98 RID: 11160 RVA: 0x00108440 File Offset: 0x00107440
		public PushbackStream(Stream s) : base(s)
		{
		}

		// Token: 0x06002B99 RID: 11161 RVA: 0x00108450 File Offset: 0x00107450
		public override int ReadByte()
		{
			if (this.buf != -1)
			{
				int result = this.buf;
				this.buf = -1;
				return result;
			}
			return base.ReadByte();
		}

		// Token: 0x06002B9A RID: 11162 RVA: 0x0010847C File Offset: 0x0010747C
		public override int Read(byte[] buffer, int offset, int count)
		{
			if (this.buf != -1 && count > 0)
			{
				buffer[offset] = (byte)this.buf;
				this.buf = -1;
				return 1;
			}
			return base.Read(buffer, offset, count);
		}

		// Token: 0x06002B9B RID: 11163 RVA: 0x001084A7 File Offset: 0x001074A7
		public virtual void Unread(int b)
		{
			if (this.buf != -1)
			{
				throw new InvalidOperationException("Can only push back one byte");
			}
			this.buf = (b & 255);
		}

		// Token: 0x04001E2F RID: 7727
		private int buf = -1;
	}
}
