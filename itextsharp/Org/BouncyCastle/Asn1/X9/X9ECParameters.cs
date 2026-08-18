using System;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Math.EC;

namespace Org.BouncyCastle.Asn1.X9
{
	// Token: 0x0200055B RID: 1371
	public class X9ECParameters : Asn1Encodable
	{
		// Token: 0x06002F3B RID: 12091 RVA: 0x001258C0 File Offset: 0x001248C0
		public X9ECParameters(Asn1Sequence seq)
		{
			if (!(seq[0] is DerInteger) || !((DerInteger)seq[0]).Value.Equals(BigInteger.One))
			{
				throw new ArgumentException("bad version in X9ECParameters");
			}
			X9Curve x9Curve;
			if (seq[2] is X9Curve)
			{
				x9Curve = (X9Curve)seq[2];
			}
			else
			{
				x9Curve = new X9Curve(new X9FieldID((Asn1Sequence)seq[1]), (Asn1Sequence)seq[2]);
			}
			this.curve = x9Curve.Curve;
			if (seq[3] is X9ECPoint)
			{
				this.g = ((X9ECPoint)seq[3]).Point;
			}
			else
			{
				this.g = new X9ECPoint(this.curve, (Asn1OctetString)seq[3]).Point;
			}
			this.n = ((DerInteger)seq[4]).Value;
			this.seed = x9Curve.GetSeed();
			if (seq.Count == 6)
			{
				this.h = ((DerInteger)seq[5]).Value;
			}
		}

		// Token: 0x06002F3C RID: 12092 RVA: 0x001259E3 File Offset: 0x001249E3
		public X9ECParameters(ECCurve curve, ECPoint g, BigInteger n) : this(curve, g, n, BigInteger.One, null)
		{
		}

		// Token: 0x06002F3D RID: 12093 RVA: 0x001259F4 File Offset: 0x001249F4
		public X9ECParameters(ECCurve curve, ECPoint g, BigInteger n, BigInteger h) : this(curve, g, n, h, null)
		{
		}

		// Token: 0x06002F3E RID: 12094 RVA: 0x00125A04 File Offset: 0x00124A04
		public X9ECParameters(ECCurve curve, ECPoint g, BigInteger n, BigInteger h, byte[] seed)
		{
			this.curve = curve;
			this.g = g;
			this.n = n;
			this.h = h;
			this.seed = seed;
			if (curve is FpCurve)
			{
				this.fieldID = new X9FieldID(((FpCurve)curve).Q);
				return;
			}
			if (curve is F2mCurve)
			{
				F2mCurve f2mCurve = (F2mCurve)curve;
				this.fieldID = new X9FieldID(f2mCurve.M, f2mCurve.K1, f2mCurve.K2, f2mCurve.K3);
			}
		}

		// Token: 0x17000813 RID: 2067
		// (get) Token: 0x06002F3F RID: 12095 RVA: 0x00125A8D File Offset: 0x00124A8D
		public ECCurve Curve
		{
			get
			{
				return this.curve;
			}
		}

		// Token: 0x17000814 RID: 2068
		// (get) Token: 0x06002F40 RID: 12096 RVA: 0x00125A95 File Offset: 0x00124A95
		public ECPoint G
		{
			get
			{
				return this.g;
			}
		}

		// Token: 0x17000815 RID: 2069
		// (get) Token: 0x06002F41 RID: 12097 RVA: 0x00125A9D File Offset: 0x00124A9D
		public BigInteger N
		{
			get
			{
				return this.n;
			}
		}

		// Token: 0x17000816 RID: 2070
		// (get) Token: 0x06002F42 RID: 12098 RVA: 0x00125AA5 File Offset: 0x00124AA5
		public BigInteger H
		{
			get
			{
				if (this.h == null)
				{
					return BigInteger.One;
				}
				return this.h;
			}
		}

		// Token: 0x06002F43 RID: 12099 RVA: 0x00125ABB File Offset: 0x00124ABB
		public byte[] GetSeed()
		{
			return this.seed;
		}

		// Token: 0x06002F44 RID: 12100 RVA: 0x00125AC4 File Offset: 0x00124AC4
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(new Asn1Encodable[]
			{
				new DerInteger(1),
				this.fieldID,
				new X9Curve(this.curve, this.seed),
				new X9ECPoint(this.g),
				new DerInteger(this.n)
			});
			if (this.h != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					new DerInteger(this.h)
				});
			}
			return new DerSequence(asn1EncodableVector);
		}

		// Token: 0x04002082 RID: 8322
		private X9FieldID fieldID;

		// Token: 0x04002083 RID: 8323
		private ECCurve curve;

		// Token: 0x04002084 RID: 8324
		private ECPoint g;

		// Token: 0x04002085 RID: 8325
		private BigInteger n;

		// Token: 0x04002086 RID: 8326
		private BigInteger h;

		// Token: 0x04002087 RID: 8327
		private byte[] seed;
	}
}
