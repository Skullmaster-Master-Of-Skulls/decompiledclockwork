using System;
using Org.BouncyCastle.Asn1.X509;

namespace Org.BouncyCastle.Asn1.Mozilla
{
	// Token: 0x02000310 RID: 784
	public class PublicKeyAndChallenge : Asn1Encodable
	{
		// Token: 0x06001CA4 RID: 7332 RVA: 0x000AB394 File Offset: 0x000AA394
		public static PublicKeyAndChallenge GetInstance(object obj)
		{
			if (obj is PublicKeyAndChallenge)
			{
				return (PublicKeyAndChallenge)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new PublicKeyAndChallenge((Asn1Sequence)obj);
			}
			throw new ArgumentException("unknown object in 'PublicKeyAndChallenge' factory : " + obj.GetType().Name + ".");
		}

		// Token: 0x06001CA5 RID: 7333 RVA: 0x000AB3E3 File Offset: 0x000AA3E3
		public PublicKeyAndChallenge(Asn1Sequence seq)
		{
			this.pkacSeq = seq;
			this.spki = SubjectPublicKeyInfo.GetInstance(seq[0]);
			this.challenge = DerIA5String.GetInstance(seq[1]);
		}

		// Token: 0x06001CA6 RID: 7334 RVA: 0x000AB416 File Offset: 0x000AA416
		public override Asn1Object ToAsn1Object()
		{
			return this.pkacSeq;
		}

		// Token: 0x17000512 RID: 1298
		// (get) Token: 0x06001CA7 RID: 7335 RVA: 0x000AB41E File Offset: 0x000AA41E
		public SubjectPublicKeyInfo SubjectPublicKeyInfo
		{
			get
			{
				return this.spki;
			}
		}

		// Token: 0x17000513 RID: 1299
		// (get) Token: 0x06001CA8 RID: 7336 RVA: 0x000AB426 File Offset: 0x000AA426
		public DerIA5String Challenge
		{
			get
			{
				return this.challenge;
			}
		}

		// Token: 0x040013B3 RID: 5043
		private Asn1Sequence pkacSeq;

		// Token: 0x040013B4 RID: 5044
		private SubjectPublicKeyInfo spki;

		// Token: 0x040013B5 RID: 5045
		private DerIA5String challenge;
	}
}
