using System;
using Org.BouncyCastle.Math;

namespace Org.BouncyCastle.Asn1.CryptoPro
{
	// Token: 0x020004D8 RID: 1240
	public class Gost3410ParamSetParameters : Asn1Encodable
	{
		// Token: 0x06002A36 RID: 10806 RVA: 0x0010070D File Offset: 0x000FF70D
		public static Gost3410ParamSetParameters GetInstance(Asn1TaggedObject obj, bool explicitly)
		{
			return Gost3410ParamSetParameters.GetInstance(Asn1Sequence.GetInstance(obj, explicitly));
		}

		// Token: 0x06002A37 RID: 10807 RVA: 0x0010071C File Offset: 0x000FF71C
		public static Gost3410ParamSetParameters GetInstance(object obj)
		{
			if (obj == null || obj is Gost3410ParamSetParameters)
			{
				return (Gost3410ParamSetParameters)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new Gost3410ParamSetParameters((Asn1Sequence)obj);
			}
			throw new ArgumentException("Invalid GOST3410Parameter: " + obj.GetType().Name);
		}

		// Token: 0x06002A38 RID: 10808 RVA: 0x00100769 File Offset: 0x000FF769
		public Gost3410ParamSetParameters(int keySize, BigInteger p, BigInteger q, BigInteger a)
		{
			this.keySize = keySize;
			this.p = new DerInteger(p);
			this.q = new DerInteger(q);
			this.a = new DerInteger(a);
		}

		// Token: 0x06002A39 RID: 10809 RVA: 0x001007A0 File Offset: 0x000FF7A0
		private Gost3410ParamSetParameters(Asn1Sequence seq)
		{
			if (seq.Count != 4)
			{
				throw new ArgumentException("Wrong number of elements in sequence", "seq");
			}
			this.keySize = DerInteger.GetInstance(seq[0]).Value.IntValue;
			this.p = DerInteger.GetInstance(seq[1]);
			this.q = DerInteger.GetInstance(seq[2]);
			this.a = DerInteger.GetInstance(seq[3]);
		}

		// Token: 0x17000756 RID: 1878
		// (get) Token: 0x06002A3A RID: 10810 RVA: 0x0010081E File Offset: 0x000FF81E
		public int KeySize
		{
			get
			{
				return this.keySize;
			}
		}

		// Token: 0x17000757 RID: 1879
		// (get) Token: 0x06002A3B RID: 10811 RVA: 0x00100826 File Offset: 0x000FF826
		public BigInteger P
		{
			get
			{
				return this.p.PositiveValue;
			}
		}

		// Token: 0x17000758 RID: 1880
		// (get) Token: 0x06002A3C RID: 10812 RVA: 0x00100833 File Offset: 0x000FF833
		public BigInteger Q
		{
			get
			{
				return this.q.PositiveValue;
			}
		}

		// Token: 0x17000759 RID: 1881
		// (get) Token: 0x06002A3D RID: 10813 RVA: 0x00100840 File Offset: 0x000FF840
		public BigInteger A
		{
			get
			{
				return this.a.PositiveValue;
			}
		}

		// Token: 0x06002A3E RID: 10814 RVA: 0x00100850 File Offset: 0x000FF850
		public override Asn1Object ToAsn1Object()
		{
			return new DerSequence(new Asn1Encodable[]
			{
				new DerInteger(this.keySize),
				this.p,
				this.q,
				this.a
			});
		}

		// Token: 0x04001D72 RID: 7538
		private readonly int keySize;

		// Token: 0x04001D73 RID: 7539
		private readonly DerInteger p;

		// Token: 0x04001D74 RID: 7540
		private readonly DerInteger q;

		// Token: 0x04001D75 RID: 7541
		private readonly DerInteger a;
	}
}
