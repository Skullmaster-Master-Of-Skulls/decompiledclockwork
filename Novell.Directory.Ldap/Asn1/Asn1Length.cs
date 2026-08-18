using System;
using System.IO;

namespace Novell.Directory.Ldap.Asn1
{
	// Token: 0x0200005A RID: 90
	[CLSCompliant(true)]
	public class Asn1Length
	{
		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x06000355 RID: 853 RVA: 0x00010D8C File Offset: 0x0000FD8C
		public virtual int Length
		{
			get
			{
				return this.length;
			}
		}

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x06000356 RID: 854 RVA: 0x00010DA4 File Offset: 0x0000FDA4
		public virtual int EncodedLength
		{
			get
			{
				return this.encodedLength;
			}
		}

		// Token: 0x06000357 RID: 855 RVA: 0x00010DBC File Offset: 0x0000FDBC
		public Asn1Length()
		{
		}

		// Token: 0x06000358 RID: 856 RVA: 0x00010DD0 File Offset: 0x0000FDD0
		public Asn1Length(int length)
		{
			this.length = length;
		}

		// Token: 0x06000359 RID: 857 RVA: 0x00010DEC File Offset: 0x0000FDEC
		public Asn1Length(Stream in_Renamed)
		{
			int i = in_Renamed.ReadByte();
			this.encodedLength++;
			if (i == 128)
			{
				this.length = -1;
			}
			else if (i < 128)
			{
				this.length = i;
			}
			else
			{
				this.length = 0;
				for (i &= 127; i > 0; i--)
				{
					int num = in_Renamed.ReadByte();
					this.encodedLength++;
					if (num < 0)
					{
						throw new EndOfStreamException("BERDecoder: decode: EOF in Asn1Length");
					}
					this.length = (this.length << 8) + num;
				}
			}
		}

		// Token: 0x0600035A RID: 858 RVA: 0x00010E80 File Offset: 0x0000FE80
		public void reset(Stream in_Renamed)
		{
			this.encodedLength = 0;
			int i = in_Renamed.ReadByte();
			this.encodedLength++;
			if (i == 128)
			{
				this.length = -1;
			}
			else if (i < 128)
			{
				this.length = i;
			}
			else
			{
				this.length = 0;
				for (i &= 127; i > 0; i--)
				{
					int num = in_Renamed.ReadByte();
					this.encodedLength++;
					if (num < 0)
					{
						throw new EndOfStreamException("BERDecoder: decode: EOF in Asn1Length");
					}
					this.length = (this.length << 8) + num;
				}
			}
		}

		// Token: 0x04000191 RID: 401
		private int length;

		// Token: 0x04000192 RID: 402
		private int encodedLength;
	}
}
