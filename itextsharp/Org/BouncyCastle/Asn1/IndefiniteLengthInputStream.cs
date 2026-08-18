using System;
using System.IO;

namespace Org.BouncyCastle.Asn1
{
	// Token: 0x0200003E RID: 62
	internal class IndefiniteLengthInputStream : LimitedInputStream
	{
		// Token: 0x06000199 RID: 409 RVA: 0x00009580 File Offset: 0x00008580
		internal IndefiniteLengthInputStream(Stream inStream) : base(inStream)
		{
			this._b1 = inStream.ReadByte();
			this._b2 = inStream.ReadByte();
			if (this._b2 < 0)
			{
				throw new EndOfStreamException();
			}
			this.CheckForEof();
		}

		// Token: 0x0600019A RID: 410 RVA: 0x000095BE File Offset: 0x000085BE
		internal void SetEofOn00(bool eofOn00)
		{
			this._eofOn00 = eofOn00;
			this.CheckForEof();
		}

		// Token: 0x0600019B RID: 411 RVA: 0x000095CE File Offset: 0x000085CE
		private bool CheckForEof()
		{
			if (!this._eofReached && this._eofOn00 && this._b1 == 0 && this._b2 == 0)
			{
				this._eofReached = true;
				this.SetParentEofDetect(true);
			}
			return this._eofReached;
		}

		// Token: 0x0600019C RID: 412 RVA: 0x00009604 File Offset: 0x00008604
		public override int Read(byte[] buffer, int offset, int count)
		{
			if (this._eofOn00 || count < 3)
			{
				return base.Read(buffer, offset, count);
			}
			if (this._eofReached)
			{
				return 0;
			}
			int num = this._in.Read(buffer, offset + 2, count - 2);
			if (num <= 0)
			{
				throw new EndOfStreamException();
			}
			buffer[offset] = (byte)this._b1;
			buffer[offset + 1] = (byte)this._b2;
			this._b1 = this._in.ReadByte();
			this._b2 = this._in.ReadByte();
			if (this._b2 < 0)
			{
				throw new EndOfStreamException();
			}
			return num + 2;
		}

		// Token: 0x0600019D RID: 413 RVA: 0x00009698 File Offset: 0x00008698
		public override int ReadByte()
		{
			if (this.CheckForEof())
			{
				return -1;
			}
			int num = this._in.ReadByte();
			if (num < 0)
			{
				throw new EndOfStreamException();
			}
			int b = this._b1;
			this._b1 = this._b2;
			this._b2 = num;
			return b;
		}

		// Token: 0x040000BB RID: 187
		private int _b1;

		// Token: 0x040000BC RID: 188
		private int _b2;

		// Token: 0x040000BD RID: 189
		private bool _eofReached;

		// Token: 0x040000BE RID: 190
		private bool _eofOn00 = true;
	}
}
