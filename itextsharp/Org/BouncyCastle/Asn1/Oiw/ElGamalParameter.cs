using System;
using Org.BouncyCastle.Math;

namespace Org.BouncyCastle.Asn1.Oiw
{
	// Token: 0x020001B1 RID: 433
	public class ElGamalParameter : Asn1Encodable
	{
		// Token: 0x06001069 RID: 4201 RVA: 0x0005E5C4 File Offset: 0x0005D5C4
		public ElGamalParameter(BigInteger p, BigInteger g)
		{
			this.p = new DerInteger(p);
			this.g = new DerInteger(g);
		}

		// Token: 0x0600106A RID: 4202 RVA: 0x0005E5E4 File Offset: 0x0005D5E4
		public ElGamalParameter(Asn1Sequence seq)
		{
			if (seq.Count != 2)
			{
				throw new ArgumentException("Wrong number of elements in sequence", "seq");
			}
			this.p = DerInteger.GetInstance(seq[0]);
			this.g = DerInteger.GetInstance(seq[1]);
		}

		// Token: 0x17000316 RID: 790
		// (get) Token: 0x0600106B RID: 4203 RVA: 0x0005E634 File Offset: 0x0005D634
		public BigInteger P
		{
			get
			{
				return this.p.PositiveValue;
			}
		}

		// Token: 0x17000317 RID: 791
		// (get) Token: 0x0600106C RID: 4204 RVA: 0x0005E641 File Offset: 0x0005D641
		public BigInteger G
		{
			get
			{
				return this.g.PositiveValue;
			}
		}

		// Token: 0x0600106D RID: 4205 RVA: 0x0005E650 File Offset: 0x0005D650
		public override Asn1Object ToAsn1Object()
		{
			return new DerSequence(new Asn1Encodable[]
			{
				this.p,
				this.g
			});
		}

		// Token: 0x04000C11 RID: 3089
		internal DerInteger p;

		// Token: 0x04000C12 RID: 3090
		internal DerInteger g;
	}
}
