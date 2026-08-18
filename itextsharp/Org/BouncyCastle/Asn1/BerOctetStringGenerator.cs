using System;
using System.IO;
using Org.BouncyCastle.Utilities.IO;

namespace Org.BouncyCastle.Asn1
{
	// Token: 0x020002C1 RID: 705
	public class BerOctetStringGenerator : BerGenerator
	{
		// Token: 0x06001A7D RID: 6781 RVA: 0x0009C1D0 File Offset: 0x0009B1D0
		public BerOctetStringGenerator(Stream outStream) : base(outStream)
		{
			base.WriteBerHeader(36);
		}

		// Token: 0x06001A7E RID: 6782 RVA: 0x0009C1E1 File Offset: 0x0009B1E1
		public BerOctetStringGenerator(Stream outStream, int tagNo, bool isExplicit) : base(outStream, tagNo, isExplicit)
		{
			base.WriteBerHeader(36);
		}

		// Token: 0x06001A7F RID: 6783 RVA: 0x0009C1F4 File Offset: 0x0009B1F4
		public Stream GetOctetOutputStream()
		{
			return this.GetOctetOutputStream(new byte[1000]);
		}

		// Token: 0x06001A80 RID: 6784 RVA: 0x0009C206 File Offset: 0x0009B206
		public Stream GetOctetOutputStream(int bufSize)
		{
			if (bufSize >= 1)
			{
				return this.GetOctetOutputStream(new byte[bufSize]);
			}
			return this.GetOctetOutputStream();
		}

		// Token: 0x06001A81 RID: 6785 RVA: 0x0009C21F File Offset: 0x0009B21F
		public Stream GetOctetOutputStream(byte[] buf)
		{
			return new BerOctetStringGenerator.BufferedBerOctetStream(this, buf);
		}

		// Token: 0x020002C2 RID: 706
		private class BufferedBerOctetStream : BaseOutputStream
		{
			// Token: 0x06001A82 RID: 6786 RVA: 0x0009C228 File Offset: 0x0009B228
			internal BufferedBerOctetStream(BerOctetStringGenerator gen, byte[] buf)
			{
				this._gen = gen;
				this._buf = buf;
				this._off = 0;
				this._derOut = new DerOutputStream(this._gen.Out);
			}

			// Token: 0x06001A83 RID: 6787 RVA: 0x0009C25C File Offset: 0x0009B25C
			public override void WriteByte(byte b)
			{
				this._buf[this._off++] = b;
				if (this._off == this._buf.Length)
				{
					DerOctetString.Encode(this._derOut, this._buf, 0, this._off);
					this._off = 0;
				}
			}

			// Token: 0x06001A84 RID: 6788 RVA: 0x0009C2B4 File Offset: 0x0009B2B4
			public override void Write(byte[] buf, int offset, int len)
			{
				while (len > 0)
				{
					int num = Math.Min(len, this._buf.Length - this._off);
					if (num == this._buf.Length)
					{
						DerOctetString.Encode(this._derOut, buf, offset, num);
					}
					else
					{
						Array.Copy(buf, offset, this._buf, this._off, num);
						this._off += num;
						if (this._off < this._buf.Length)
						{
							return;
						}
						DerOctetString.Encode(this._derOut, this._buf, 0, this._off);
						this._off = 0;
					}
					offset += num;
					len -= num;
				}
			}

			// Token: 0x06001A85 RID: 6789 RVA: 0x0009C35A File Offset: 0x0009B35A
			public override void Close()
			{
				if (this._off != 0)
				{
					DerOctetString.Encode(this._derOut, this._buf, 0, this._off);
				}
				this._gen.WriteBerEnd();
				base.Close();
			}

			// Token: 0x040011A9 RID: 4521
			private byte[] _buf;

			// Token: 0x040011AA RID: 4522
			private int _off;

			// Token: 0x040011AB RID: 4523
			private readonly BerOctetStringGenerator _gen;

			// Token: 0x040011AC RID: 4524
			private readonly DerOutputStream _derOut;
		}
	}
}
