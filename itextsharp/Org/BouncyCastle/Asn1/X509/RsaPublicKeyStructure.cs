using System;
using Org.BouncyCastle.Math;

namespace Org.BouncyCastle.Asn1.X509
{
	// Token: 0x02000203 RID: 515
	public class RsaPublicKeyStructure : Asn1Encodable
	{
		// Token: 0x060013D2 RID: 5074 RVA: 0x00072460 File Offset: 0x00071460
		public static RsaPublicKeyStructure GetInstance(Asn1TaggedObject obj, bool explicitly)
		{
			return RsaPublicKeyStructure.GetInstance(Asn1Sequence.GetInstance(obj, explicitly));
		}

		// Token: 0x060013D3 RID: 5075 RVA: 0x00072470 File Offset: 0x00071470
		public static RsaPublicKeyStructure GetInstance(object obj)
		{
			if (obj == null || obj is RsaPublicKeyStructure)
			{
				return (RsaPublicKeyStructure)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new RsaPublicKeyStructure((Asn1Sequence)obj);
			}
			throw new ArgumentException("Invalid RsaPublicKeyStructure: " + obj.GetType().Name);
		}

		// Token: 0x060013D4 RID: 5076 RVA: 0x000724C0 File Offset: 0x000714C0
		public RsaPublicKeyStructure(BigInteger modulus, BigInteger publicExponent)
		{
			if (modulus == null)
			{
				throw new ArgumentNullException("modulus");
			}
			if (publicExponent == null)
			{
				throw new ArgumentNullException("publicExponent");
			}
			if (modulus.SignValue <= 0)
			{
				throw new ArgumentException("Not a valid RSA modulus", "modulus");
			}
			if (publicExponent.SignValue <= 0)
			{
				throw new ArgumentException("Not a valid RSA public exponent", "publicExponent");
			}
			this.modulus = modulus;
			this.publicExponent = publicExponent;
		}

		// Token: 0x060013D5 RID: 5077 RVA: 0x00072530 File Offset: 0x00071530
		private RsaPublicKeyStructure(Asn1Sequence seq)
		{
			if (seq.Count != 2)
			{
				throw new ArgumentException("Bad sequence size: " + seq.Count);
			}
			this.modulus = DerInteger.GetInstance(seq[0]).PositiveValue;
			this.publicExponent = DerInteger.GetInstance(seq[1]).PositiveValue;
		}

		// Token: 0x1700039E RID: 926
		// (get) Token: 0x060013D6 RID: 5078 RVA: 0x00072595 File Offset: 0x00071595
		public BigInteger Modulus
		{
			get
			{
				return this.modulus;
			}
		}

		// Token: 0x1700039F RID: 927
		// (get) Token: 0x060013D7 RID: 5079 RVA: 0x0007259D File Offset: 0x0007159D
		public BigInteger PublicExponent
		{
			get
			{
				return this.publicExponent;
			}
		}

		// Token: 0x060013D8 RID: 5080 RVA: 0x000725A8 File Offset: 0x000715A8
		public override Asn1Object ToAsn1Object()
		{
			return new DerSequence(new Asn1Encodable[]
			{
				new DerInteger(this.Modulus),
				new DerInteger(this.PublicExponent)
			});
		}

		// Token: 0x04000DBE RID: 3518
		private BigInteger modulus;

		// Token: 0x04000DBF RID: 3519
		private BigInteger publicExponent;
	}
}
