using System;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Math.EC;

namespace Org.BouncyCastle.Asn1.X9
{
	// Token: 0x020002B2 RID: 690
	public class X9FieldElement : Asn1Encodable
	{
		// Token: 0x06001A1A RID: 6682 RVA: 0x0009ADCB File Offset: 0x00099DCB
		public X9FieldElement(ECFieldElement f)
		{
			this.f = f;
		}

		// Token: 0x06001A1B RID: 6683 RVA: 0x0009ADDA File Offset: 0x00099DDA
		public X9FieldElement(BigInteger p, Asn1OctetString s) : this(new FpFieldElement(p, new BigInteger(1, s.GetOctets())))
		{
		}

		// Token: 0x06001A1C RID: 6684 RVA: 0x0009ADF4 File Offset: 0x00099DF4
		public X9FieldElement(int m, int k1, int k2, int k3, Asn1OctetString s) : this(new F2mFieldElement(m, k1, k2, k3, new BigInteger(1, s.GetOctets())))
		{
		}

		// Token: 0x170004B5 RID: 1205
		// (get) Token: 0x06001A1D RID: 6685 RVA: 0x0009AE13 File Offset: 0x00099E13
		public ECFieldElement Value
		{
			get
			{
				return this.f;
			}
		}

		// Token: 0x06001A1E RID: 6686 RVA: 0x0009AE1C File Offset: 0x00099E1C
		public override Asn1Object ToAsn1Object()
		{
			int byteLength = X9IntegerConverter.GetByteLength(this.f);
			byte[] str = X9IntegerConverter.IntegerToBytes(this.f.ToBigInteger(), byteLength);
			return new DerOctetString(str);
		}

		// Token: 0x04001165 RID: 4453
		private ECFieldElement f;
	}
}
