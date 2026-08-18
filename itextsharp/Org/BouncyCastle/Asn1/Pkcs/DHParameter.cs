using System;
using System.Collections;
using Org.BouncyCastle.Math;

namespace Org.BouncyCastle.Asn1.Pkcs
{
	// Token: 0x020004D2 RID: 1234
	public class DHParameter : Asn1Encodable
	{
		// Token: 0x06002A18 RID: 10776 RVA: 0x001000DE File Offset: 0x000FF0DE
		public DHParameter(BigInteger p, BigInteger g, int l)
		{
			this.p = new DerInteger(p);
			this.g = new DerInteger(g);
			if (l != 0)
			{
				this.l = new DerInteger(l);
			}
		}

		// Token: 0x06002A19 RID: 10777 RVA: 0x00100110 File Offset: 0x000FF110
		public DHParameter(Asn1Sequence seq)
		{
			IEnumerator enumerator = seq.GetEnumerator();
			enumerator.MoveNext();
			this.p = (DerInteger)enumerator.Current;
			enumerator.MoveNext();
			this.g = (DerInteger)enumerator.Current;
			if (enumerator.MoveNext())
			{
				this.l = (DerInteger)enumerator.Current;
			}
		}

		// Token: 0x17000750 RID: 1872
		// (get) Token: 0x06002A1A RID: 10778 RVA: 0x00100173 File Offset: 0x000FF173
		public BigInteger P
		{
			get
			{
				return this.p.PositiveValue;
			}
		}

		// Token: 0x17000751 RID: 1873
		// (get) Token: 0x06002A1B RID: 10779 RVA: 0x00100180 File Offset: 0x000FF180
		public BigInteger G
		{
			get
			{
				return this.g.PositiveValue;
			}
		}

		// Token: 0x17000752 RID: 1874
		// (get) Token: 0x06002A1C RID: 10780 RVA: 0x0010018D File Offset: 0x000FF18D
		public BigInteger L
		{
			get
			{
				if (this.l != null)
				{
					return this.l.PositiveValue;
				}
				return null;
			}
		}

		// Token: 0x06002A1D RID: 10781 RVA: 0x001001A4 File Offset: 0x000FF1A4
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(new Asn1Encodable[]
			{
				this.p,
				this.g
			});
			if (this.l != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					this.l
				});
			}
			return new DerSequence(asn1EncodableVector);
		}

		// Token: 0x04001D4E RID: 7502
		internal DerInteger p;

		// Token: 0x04001D4F RID: 7503
		internal DerInteger g;

		// Token: 0x04001D50 RID: 7504
		internal DerInteger l;
	}
}
