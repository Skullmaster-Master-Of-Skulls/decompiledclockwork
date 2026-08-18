using System;
using Org.BouncyCastle.Asn1.X509;

namespace Org.BouncyCastle.Asn1.Ess
{
	// Token: 0x02000389 RID: 905
	[Obsolete("Use version in Asn1.Esf instead")]
	public class OtherSigningCertificate : Asn1Encodable
	{
		// Token: 0x06001F84 RID: 8068 RVA: 0x000BC2AC File Offset: 0x000BB2AC
		public static OtherSigningCertificate GetInstance(object o)
		{
			if (o == null || o is OtherSigningCertificate)
			{
				return (OtherSigningCertificate)o;
			}
			if (o is Asn1Sequence)
			{
				return new OtherSigningCertificate((Asn1Sequence)o);
			}
			throw new ArgumentException("unknown object in 'OtherSigningCertificate' factory : " + o.GetType().Name + ".");
		}

		// Token: 0x06001F85 RID: 8069 RVA: 0x000BC300 File Offset: 0x000BB300
		public OtherSigningCertificate(Asn1Sequence seq)
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

		// Token: 0x06001F86 RID: 8070 RVA: 0x000BC36D File Offset: 0x000BB36D
		public OtherSigningCertificate(OtherCertID otherCertID)
		{
			this.certs = new DerSequence(otherCertID);
		}

		// Token: 0x06001F87 RID: 8071 RVA: 0x000BC384 File Offset: 0x000BB384
		public OtherCertID[] GetCerts()
		{
			OtherCertID[] array = new OtherCertID[this.certs.Count];
			for (int num = 0; num != this.certs.Count; num++)
			{
				array[num] = OtherCertID.GetInstance(this.certs[num]);
			}
			return array;
		}

		// Token: 0x06001F88 RID: 8072 RVA: 0x000BC3D0 File Offset: 0x000BB3D0
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

		// Token: 0x06001F89 RID: 8073 RVA: 0x000BC424 File Offset: 0x000BB424
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

		// Token: 0x040015D7 RID: 5591
		private Asn1Sequence certs;

		// Token: 0x040015D8 RID: 5592
		private Asn1Sequence policies;
	}
}
