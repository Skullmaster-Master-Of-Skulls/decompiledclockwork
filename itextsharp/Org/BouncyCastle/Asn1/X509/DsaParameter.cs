using System;
using Org.BouncyCastle.Math;

namespace Org.BouncyCastle.Asn1.X509
{
	// Token: 0x020005B7 RID: 1463
	public class DsaParameter : Asn1Encodable
	{
		// Token: 0x0600325C RID: 12892 RVA: 0x00138AC8 File Offset: 0x00137AC8
		public static DsaParameter GetInstance(Asn1TaggedObject obj, bool explicitly)
		{
			return DsaParameter.GetInstance(Asn1Sequence.GetInstance(obj, explicitly));
		}

		// Token: 0x0600325D RID: 12893 RVA: 0x00138AD8 File Offset: 0x00137AD8
		public static DsaParameter GetInstance(object obj)
		{
			if (obj == null || obj is DsaParameter)
			{
				return (DsaParameter)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new DsaParameter((Asn1Sequence)obj);
			}
			throw new ArgumentException("Invalid DsaParameter: " + obj.GetType().Name);
		}

		// Token: 0x0600325E RID: 12894 RVA: 0x00138B25 File Offset: 0x00137B25
		public DsaParameter(BigInteger p, BigInteger q, BigInteger g)
		{
			this.p = new DerInteger(p);
			this.q = new DerInteger(q);
			this.g = new DerInteger(g);
		}

		// Token: 0x0600325F RID: 12895 RVA: 0x00138B54 File Offset: 0x00137B54
		private DsaParameter(Asn1Sequence seq)
		{
			if (seq.Count != 3)
			{
				throw new ArgumentException("Bad sequence size: " + seq.Count, "seq");
			}
			this.p = DerInteger.GetInstance(seq[0]);
			this.q = DerInteger.GetInstance(seq[1]);
			this.g = DerInteger.GetInstance(seq[2]);
		}

		// Token: 0x1700089B RID: 2203
		// (get) Token: 0x06003260 RID: 12896 RVA: 0x00138BC6 File Offset: 0x00137BC6
		public BigInteger P
		{
			get
			{
				return this.p.PositiveValue;
			}
		}

		// Token: 0x1700089C RID: 2204
		// (get) Token: 0x06003261 RID: 12897 RVA: 0x00138BD3 File Offset: 0x00137BD3
		public BigInteger Q
		{
			get
			{
				return this.q.PositiveValue;
			}
		}

		// Token: 0x1700089D RID: 2205
		// (get) Token: 0x06003262 RID: 12898 RVA: 0x00138BE0 File Offset: 0x00137BE0
		public BigInteger G
		{
			get
			{
				return this.g.PositiveValue;
			}
		}

		// Token: 0x06003263 RID: 12899 RVA: 0x00138BF0 File Offset: 0x00137BF0
		public override Asn1Object ToAsn1Object()
		{
			return new DerSequence(new Asn1Encodable[]
			{
				this.p,
				this.q,
				this.g
			});
		}

		// Token: 0x0400227D RID: 8829
		internal readonly DerInteger p;

		// Token: 0x0400227E RID: 8830
		internal readonly DerInteger q;

		// Token: 0x0400227F RID: 8831
		internal readonly DerInteger g;
	}
}
