using System;
using Org.BouncyCastle.Asn1.X509;

namespace Org.BouncyCastle.Asn1.Ess
{
	// Token: 0x020002BB RID: 699
	public class SigningCertificateV2 : Asn1Encodable
	{
		// Token: 0x06001A57 RID: 6743 RVA: 0x0009BC9C File Offset: 0x0009AC9C
		public static SigningCertificateV2 GetInstance(object o)
		{
			if (o == null || o is SigningCertificateV2)
			{
				return (SigningCertificateV2)o;
			}
			if (o is Asn1Sequence)
			{
				return new SigningCertificateV2((Asn1Sequence)o);
			}
			throw new ArgumentException("unknown object in 'SigningCertificateV2' factory : " + o.GetType().Name + ".");
		}

		// Token: 0x06001A58 RID: 6744 RVA: 0x0009BCF0 File Offset: 0x0009ACF0
		private SigningCertificateV2(Asn1Sequence seq)
		{
			if (seq.Count < 1 || seq.Count > 2)
			{
				throw new ArgumentException("Bad sequence size: " + seq.Count, "seq");
			}
			this.certs = Asn1Sequence.GetInstance(seq[0].ToAsn1Object());
			if (seq.Count > 1)
			{
				this.policies = Asn1Sequence.GetInstance(seq[1].ToAsn1Object());
			}
		}

		// Token: 0x06001A59 RID: 6745 RVA: 0x0009BD6C File Offset: 0x0009AD6C
		public SigningCertificateV2(EssCertIDv2[] certs)
		{
			this.certs = new DerSequence(certs);
		}

		// Token: 0x06001A5A RID: 6746 RVA: 0x0009BD80 File Offset: 0x0009AD80
		public SigningCertificateV2(EssCertIDv2[] certs, PolicyInformation[] policies)
		{
			this.certs = new DerSequence(certs);
			if (policies != null)
			{
				this.policies = new DerSequence(policies);
			}
		}

		// Token: 0x06001A5B RID: 6747 RVA: 0x0009BDA4 File Offset: 0x0009ADA4
		public EssCertIDv2[] GetCerts()
		{
			EssCertIDv2[] array = new EssCertIDv2[this.certs.Count];
			for (int num = 0; num != this.certs.Count; num++)
			{
				array[num] = EssCertIDv2.GetInstance(this.certs[num]);
			}
			return array;
		}

		// Token: 0x06001A5C RID: 6748 RVA: 0x0009BDF0 File Offset: 0x0009ADF0
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

		// Token: 0x06001A5D RID: 6749 RVA: 0x0009BE44 File Offset: 0x0009AE44
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

		// Token: 0x0400119F RID: 4511
		private readonly Asn1Sequence certs;

		// Token: 0x040011A0 RID: 4512
		private readonly Asn1Sequence policies;
	}
}
