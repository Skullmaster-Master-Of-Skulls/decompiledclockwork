using System;
using System.Collections;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Utilities.Collections;

namespace Org.BouncyCastle.Asn1.Esf
{
	// Token: 0x0200051E RID: 1310
	public class OtherSigningCertificate : Asn1Encodable
	{
		// Token: 0x06002CB1 RID: 11441 RVA: 0x0010F688 File Offset: 0x0010E688
		public static OtherSigningCertificate GetInstance(object obj)
		{
			if (obj == null || obj is OtherSigningCertificate)
			{
				return (OtherSigningCertificate)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new OtherSigningCertificate((Asn1Sequence)obj);
			}
			throw new ArgumentException("Unknown object in 'OtherSigningCertificate' factory: " + obj.GetType().Name, "obj");
		}

		// Token: 0x06002CB2 RID: 11442 RVA: 0x0010F6DC File Offset: 0x0010E6DC
		private OtherSigningCertificate(Asn1Sequence seq)
		{
			if (seq == null)
			{
				throw new ArgumentNullException("seq");
			}
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

		// Token: 0x06002CB3 RID: 11443 RVA: 0x0010F766 File Offset: 0x0010E766
		public OtherSigningCertificate(params OtherCertID[] certs) : this(certs, null)
		{
		}

		// Token: 0x06002CB4 RID: 11444 RVA: 0x0010F770 File Offset: 0x0010E770
		public OtherSigningCertificate(OtherCertID[] certs, params PolicyInformation[] policies)
		{
			if (certs == null)
			{
				throw new ArgumentNullException("certs");
			}
			this.certs = new DerSequence(certs);
			if (policies != null)
			{
				this.policies = new DerSequence(policies);
			}
		}

		// Token: 0x06002CB5 RID: 11445 RVA: 0x0010F7A1 File Offset: 0x0010E7A1
		public OtherSigningCertificate(IEnumerable certs) : this(certs, null)
		{
		}

		// Token: 0x06002CB6 RID: 11446 RVA: 0x0010F7AC File Offset: 0x0010E7AC
		public OtherSigningCertificate(IEnumerable certs, IEnumerable policies)
		{
			if (certs == null)
			{
				throw new ArgumentNullException("certs");
			}
			if (!CollectionUtilities.CheckElementsAreOfType(certs, typeof(OtherCertID)))
			{
				throw new ArgumentException("Must contain only 'OtherCertID' objects", "certs");
			}
			this.certs = new DerSequence(Asn1EncodableVector.FromEnumerable(certs));
			if (policies != null)
			{
				if (!CollectionUtilities.CheckElementsAreOfType(policies, typeof(PolicyInformation)))
				{
					throw new ArgumentException("Must contain only 'PolicyInformation' objects", "policies");
				}
				this.policies = new DerSequence(Asn1EncodableVector.FromEnumerable(policies));
			}
		}

		// Token: 0x06002CB7 RID: 11447 RVA: 0x0010F838 File Offset: 0x0010E838
		public OtherCertID[] GetCerts()
		{
			OtherCertID[] array = new OtherCertID[this.certs.Count];
			for (int i = 0; i < this.certs.Count; i++)
			{
				array[i] = OtherCertID.GetInstance(this.certs[i].ToAsn1Object());
			}
			return array;
		}

		// Token: 0x06002CB8 RID: 11448 RVA: 0x0010F888 File Offset: 0x0010E888
		public PolicyInformation[] GetPolicies()
		{
			if (this.policies == null)
			{
				return null;
			}
			PolicyInformation[] array = new PolicyInformation[this.policies.Count];
			for (int i = 0; i < this.policies.Count; i++)
			{
				array[i] = PolicyInformation.GetInstance(this.policies[i].ToAsn1Object());
			}
			return array;
		}

		// Token: 0x06002CB9 RID: 11449 RVA: 0x0010F8E0 File Offset: 0x0010E8E0
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

		// Token: 0x04001EB5 RID: 7861
		private readonly Asn1Sequence certs;

		// Token: 0x04001EB6 RID: 7862
		private readonly Asn1Sequence policies;
	}
}
