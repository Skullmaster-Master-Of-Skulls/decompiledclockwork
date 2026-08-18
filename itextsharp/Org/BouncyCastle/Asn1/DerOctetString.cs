using System;

namespace Org.BouncyCastle.Asn1
{
	// Token: 0x0200031E RID: 798
	public class DerOctetString : Asn1OctetString
	{
		// Token: 0x06001CFB RID: 7419 RVA: 0x000AC44D File Offset: 0x000AB44D
		public DerOctetString(byte[] str) : base(str)
		{
		}

		// Token: 0x06001CFC RID: 7420 RVA: 0x000AC456 File Offset: 0x000AB456
		public DerOctetString(Asn1Encodable obj) : base(obj)
		{
		}

		// Token: 0x06001CFD RID: 7421 RVA: 0x000AC45F File Offset: 0x000AB45F
		internal override void Encode(DerOutputStream derOut)
		{
			derOut.WriteEncoded(4, this.str);
		}

		// Token: 0x06001CFE RID: 7422 RVA: 0x000AC46E File Offset: 0x000AB46E
		internal static void Encode(DerOutputStream derOut, byte[] bytes, int offset, int length)
		{
			derOut.WriteEncoded(4, bytes, offset, length);
		}
	}
}
