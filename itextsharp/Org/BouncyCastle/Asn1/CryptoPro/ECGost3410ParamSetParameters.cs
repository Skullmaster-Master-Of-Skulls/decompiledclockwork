using System;
using Org.BouncyCastle.Math;

namespace Org.BouncyCastle.Asn1.CryptoPro
{
	// Token: 0x02000316 RID: 790
	public class ECGost3410ParamSetParameters : Asn1Encodable
	{
		// Token: 0x06001CC0 RID: 7360 RVA: 0x000ABA04 File Offset: 0x000AAA04
		public static ECGost3410ParamSetParameters GetInstance(Asn1TaggedObject obj, bool explicitly)
		{
			return ECGost3410ParamSetParameters.GetInstance(Asn1Sequence.GetInstance(obj, explicitly));
		}

		// Token: 0x06001CC1 RID: 7361 RVA: 0x000ABA14 File Offset: 0x000AAA14
		public static ECGost3410ParamSetParameters GetInstance(object obj)
		{
			if (obj == null || obj is ECGost3410ParamSetParameters)
			{
				return (ECGost3410ParamSetParameters)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new ECGost3410ParamSetParameters((Asn1Sequence)obj);
			}
			throw new ArgumentException("Invalid GOST3410Parameter: " + obj.GetType().Name);
		}

		// Token: 0x06001CC2 RID: 7362 RVA: 0x000ABA64 File Offset: 0x000AAA64
		public ECGost3410ParamSetParameters(BigInteger a, BigInteger b, BigInteger p, BigInteger q, int x, BigInteger y)
		{
			this.a = new DerInteger(a);
			this.b = new DerInteger(b);
			this.p = new DerInteger(p);
			this.q = new DerInteger(q);
			this.x = new DerInteger(x);
			this.y = new DerInteger(y);
		}

		// Token: 0x06001CC3 RID: 7363 RVA: 0x000ABAC4 File Offset: 0x000AAAC4
		public ECGost3410ParamSetParameters(Asn1Sequence seq)
		{
			if (seq.Count != 6)
			{
				throw new ArgumentException("Wrong number of elements in sequence", "seq");
			}
			this.a = DerInteger.GetInstance(seq[0]);
			this.b = DerInteger.GetInstance(seq[1]);
			this.p = DerInteger.GetInstance(seq[2]);
			this.q = DerInteger.GetInstance(seq[3]);
			this.x = DerInteger.GetInstance(seq[4]);
			this.y = DerInteger.GetInstance(seq[5]);
		}

		// Token: 0x17000517 RID: 1303
		// (get) Token: 0x06001CC4 RID: 7364 RVA: 0x000ABB5C File Offset: 0x000AAB5C
		public BigInteger P
		{
			get
			{
				return this.p.PositiveValue;
			}
		}

		// Token: 0x17000518 RID: 1304
		// (get) Token: 0x06001CC5 RID: 7365 RVA: 0x000ABB69 File Offset: 0x000AAB69
		public BigInteger Q
		{
			get
			{
				return this.q.PositiveValue;
			}
		}

		// Token: 0x17000519 RID: 1305
		// (get) Token: 0x06001CC6 RID: 7366 RVA: 0x000ABB76 File Offset: 0x000AAB76
		public BigInteger A
		{
			get
			{
				return this.a.PositiveValue;
			}
		}

		// Token: 0x06001CC7 RID: 7367 RVA: 0x000ABB84 File Offset: 0x000AAB84
		public override Asn1Object ToAsn1Object()
		{
			return new DerSequence(new Asn1Encodable[]
			{
				this.a,
				this.b,
				this.p,
				this.q,
				this.x,
				this.y
			});
		}

		// Token: 0x040013D2 RID: 5074
		internal readonly DerInteger p;

		// Token: 0x040013D3 RID: 5075
		internal readonly DerInteger q;

		// Token: 0x040013D4 RID: 5076
		internal readonly DerInteger a;

		// Token: 0x040013D5 RID: 5077
		internal readonly DerInteger b;

		// Token: 0x040013D6 RID: 5078
		internal readonly DerInteger x;

		// Token: 0x040013D7 RID: 5079
		internal readonly DerInteger y;
	}
}
