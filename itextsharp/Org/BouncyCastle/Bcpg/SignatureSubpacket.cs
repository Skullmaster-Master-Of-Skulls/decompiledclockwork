using System;
using System.IO;

namespace Org.BouncyCastle.Bcpg
{
	// Token: 0x02000030 RID: 48
	public class SignatureSubpacket
	{
		// Token: 0x0600014B RID: 331 RVA: 0x00008B59 File Offset: 0x00007B59
		protected internal SignatureSubpacket(SignatureSubpacketTag type, bool critical, byte[] data)
		{
			this.type = type;
			this.critical = critical;
			this.data = data;
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x0600014C RID: 332 RVA: 0x00008B76 File Offset: 0x00007B76
		public SignatureSubpacketTag SubpacketType
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x0600014D RID: 333 RVA: 0x00008B7E File Offset: 0x00007B7E
		public bool IsCritical()
		{
			return this.critical;
		}

		// Token: 0x0600014E RID: 334 RVA: 0x00008B86 File Offset: 0x00007B86
		public byte[] GetData()
		{
			return (byte[])this.data.Clone();
		}

		// Token: 0x0600014F RID: 335 RVA: 0x00008B98 File Offset: 0x00007B98
		public void Encode(Stream os)
		{
			int num = this.data.Length + 1;
			if (num < 192)
			{
				os.WriteByte((byte)num);
			}
			else if (num <= 8383)
			{
				num -= 192;
				os.WriteByte((byte)((num >> 8 & 255) + 192));
				os.WriteByte((byte)num);
			}
			else
			{
				os.WriteByte(byte.MaxValue);
				os.WriteByte((byte)(num >> 24));
				os.WriteByte((byte)(num >> 16));
				os.WriteByte((byte)(num >> 8));
				os.WriteByte((byte)num);
			}
			if (this.critical)
			{
				os.WriteByte((byte)((SignatureSubpacketTag)128 | this.type));
			}
			else
			{
				os.WriteByte((byte)this.type);
			}
			os.Write(this.data, 0, this.data.Length);
		}

		// Token: 0x040000A4 RID: 164
		private readonly SignatureSubpacketTag type;

		// Token: 0x040000A5 RID: 165
		private readonly bool critical;

		// Token: 0x040000A6 RID: 166
		internal readonly byte[] data;
	}
}
