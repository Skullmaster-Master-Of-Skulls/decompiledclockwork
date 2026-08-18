using System;
using Org.BouncyCastle.Math;

namespace Org.BouncyCastle.Asn1.Pkcs
{
	// Token: 0x02000576 RID: 1398
	public class RsaPrivateKeyStructure : Asn1Encodable
	{
		// Token: 0x06002FB3 RID: 12211 RVA: 0x001271B8 File Offset: 0x001261B8
		public RsaPrivateKeyStructure(BigInteger modulus, BigInteger publicExponent, BigInteger privateExponent, BigInteger prime1, BigInteger prime2, BigInteger exponent1, BigInteger exponent2, BigInteger coefficient)
		{
			this.modulus = modulus;
			this.publicExponent = publicExponent;
			this.privateExponent = privateExponent;
			this.prime1 = prime1;
			this.prime2 = prime2;
			this.exponent1 = exponent1;
			this.exponent2 = exponent2;
			this.coefficient = coefficient;
		}

		// Token: 0x06002FB4 RID: 12212 RVA: 0x00127208 File Offset: 0x00126208
		public RsaPrivateKeyStructure(Asn1Sequence seq)
		{
			BigInteger value = ((DerInteger)seq[0]).Value;
			if (value.IntValue != 0)
			{
				throw new ArgumentException("wrong version for RSA private key");
			}
			this.modulus = ((DerInteger)seq[1]).Value;
			this.publicExponent = ((DerInteger)seq[2]).Value;
			this.privateExponent = ((DerInteger)seq[3]).Value;
			this.prime1 = ((DerInteger)seq[4]).Value;
			this.prime2 = ((DerInteger)seq[5]).Value;
			this.exponent1 = ((DerInteger)seq[6]).Value;
			this.exponent2 = ((DerInteger)seq[7]).Value;
			this.coefficient = ((DerInteger)seq[8]).Value;
		}

		// Token: 0x17000823 RID: 2083
		// (get) Token: 0x06002FB5 RID: 12213 RVA: 0x001272F8 File Offset: 0x001262F8
		public BigInteger Modulus
		{
			get
			{
				return this.modulus;
			}
		}

		// Token: 0x17000824 RID: 2084
		// (get) Token: 0x06002FB6 RID: 12214 RVA: 0x00127300 File Offset: 0x00126300
		public BigInteger PublicExponent
		{
			get
			{
				return this.publicExponent;
			}
		}

		// Token: 0x17000825 RID: 2085
		// (get) Token: 0x06002FB7 RID: 12215 RVA: 0x00127308 File Offset: 0x00126308
		public BigInteger PrivateExponent
		{
			get
			{
				return this.privateExponent;
			}
		}

		// Token: 0x17000826 RID: 2086
		// (get) Token: 0x06002FB8 RID: 12216 RVA: 0x00127310 File Offset: 0x00126310
		public BigInteger Prime1
		{
			get
			{
				return this.prime1;
			}
		}

		// Token: 0x17000827 RID: 2087
		// (get) Token: 0x06002FB9 RID: 12217 RVA: 0x00127318 File Offset: 0x00126318
		public BigInteger Prime2
		{
			get
			{
				return this.prime2;
			}
		}

		// Token: 0x17000828 RID: 2088
		// (get) Token: 0x06002FBA RID: 12218 RVA: 0x00127320 File Offset: 0x00126320
		public BigInteger Exponent1
		{
			get
			{
				return this.exponent1;
			}
		}

		// Token: 0x17000829 RID: 2089
		// (get) Token: 0x06002FBB RID: 12219 RVA: 0x00127328 File Offset: 0x00126328
		public BigInteger Exponent2
		{
			get
			{
				return this.exponent2;
			}
		}

		// Token: 0x1700082A RID: 2090
		// (get) Token: 0x06002FBC RID: 12220 RVA: 0x00127330 File Offset: 0x00126330
		public BigInteger Coefficient
		{
			get
			{
				return this.coefficient;
			}
		}

		// Token: 0x06002FBD RID: 12221 RVA: 0x00127338 File Offset: 0x00126338
		public override Asn1Object ToAsn1Object()
		{
			return new DerSequence(new Asn1Encodable[]
			{
				new DerInteger(0),
				new DerInteger(this.Modulus),
				new DerInteger(this.PublicExponent),
				new DerInteger(this.PrivateExponent),
				new DerInteger(this.Prime1),
				new DerInteger(this.Prime2),
				new DerInteger(this.Exponent1),
				new DerInteger(this.Exponent2),
				new DerInteger(this.Coefficient)
			});
		}

		// Token: 0x040020CB RID: 8395
		private readonly BigInteger modulus;

		// Token: 0x040020CC RID: 8396
		private readonly BigInteger publicExponent;

		// Token: 0x040020CD RID: 8397
		private readonly BigInteger privateExponent;

		// Token: 0x040020CE RID: 8398
		private readonly BigInteger prime1;

		// Token: 0x040020CF RID: 8399
		private readonly BigInteger prime2;

		// Token: 0x040020D0 RID: 8400
		private readonly BigInteger exponent1;

		// Token: 0x040020D1 RID: 8401
		private readonly BigInteger exponent2;

		// Token: 0x040020D2 RID: 8402
		private readonly BigInteger coefficient;
	}
}
