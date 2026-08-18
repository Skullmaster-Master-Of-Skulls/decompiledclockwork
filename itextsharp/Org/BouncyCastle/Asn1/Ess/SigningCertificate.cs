using System;
using Org.BouncyCastle.Asn1.X509;

namespace Org.BouncyCastle.Asn1.Ess
{
	// Token: 0x0200057D RID: 1405
	public class SigningCertificate : Asn1Encodable
	{
		// Token: 0x06002FDD RID: 12253 RVA: 0x001278A8 File Offset: 0x001268A8
		public static SigningCertificate GetInstance(object o)
		{
			if (o == null || o is SigningCertificate)
			{
				return (SigningCertificate)o;
			}
			if (o is Asn1Sequence)
			{
				return new SigningCertificate((Asn1Sequence)o);
			}
			throw new ArgumentException("unknown object in 'SigningCertificate' factory : " + o.GetType().Name + ".");
		}

		// Token: 0x06002FDE RID: 12254 RVA: 0x001278FC File Offset: 0x001268FC
		public SigningCertificate(Asn1Sequence seq)
		{
			if (seq.Count < 1 || seq.Count > 2)
			{
				throw new ArgumentException("Bad sequence size: " + seq.Count);
			}
			this.certs = Asn1Sequence.GetInstance(seq[0]);
			if (seq.Count > 1)
			{
				this.policies = Asn1Sequence.GetInstance(seq[1]);
			}
		}

		// Token: 0x06002FDF RID: 12255 RVA: 0x00127969 File Offset: 0x00126969
		public SigningCertificate(EssCertID essCertID)
		{
			this.certs = new DerSequence(essCertID);
		}

		// Token: 0x06002FE0 RID: 12256 RVA: 0x00127980 File Offset: 0x00126980
		public EssCertID[] GetCerts()
		{
			EssCertID[] array = new EssCertID[this.certs.Count];
			for (int num = 0; num != this.certs.Count; num++)
			{
				array[num] = EssCertID.GetInstance(this.certs[num]);
			}
			return array;
		}

		// Token: 0x06002FE1 RID: 12257 RVA: 0x001279CC File Offset: 0x001269CC
		public PolicyInformation[] GetPolicies()
		{
			if (this.policies == null)
			{
				return null;
			}
			PolicyInformation[] array = new PolicyInformation[this.policies.Count];
			for (int num = 0; num != this.policies.Count; num++)
			{
				array[num] = PolicyInformation.GetInstance(this.policies[num]);
			}
			return array;
		}

		// Token: 0x06002FE2 RID: 12258 RVA: 0x00127A20 File Offset: 0x00126A20
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(new Asn1Encodable[]
			{
				this.certs
			});
			if (this.policies != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					this.policies
				});
			}
			return new DerSequence(asn1EncodableVector);
		}

		// Token: 0x040020E1 RID: 8417
		private Asn1Sequence certs;

		// Token: 0x040020E2 RID: 8418
		private Asn1Sequence policies;
	}
}
