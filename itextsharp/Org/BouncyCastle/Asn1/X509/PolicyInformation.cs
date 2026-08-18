using System;

namespace Org.BouncyCastle.Asn1.X509
{
	// Token: 0x020001A9 RID: 425
	public class PolicyInformation : Asn1Encodable
	{
		// Token: 0x06001032 RID: 4146 RVA: 0x0005D9B8 File Offset: 0x0005C9B8
		private PolicyInformation(Asn1Sequence seq)
		{
			if (seq.Count < 1 || seq.Count > 2)
			{
				throw new ArgumentException("Bad sequence size: " + seq.Count);
			}
			this.policyIdentifier = DerObjectIdentifier.GetInstance(seq[0]);
			if (seq.Count > 1)
			{
				this.policyQualifiers = Asn1Sequence.GetInstance(seq[1]);
			}
		}

		// Token: 0x06001033 RID: 4147 RVA: 0x0005DA25 File Offset: 0x0005CA25
		public PolicyInformation(DerObjectIdentifier policyIdentifier)
		{
			this.policyIdentifier = policyIdentifier;
		}

		// Token: 0x06001034 RID: 4148 RVA: 0x0005DA34 File Offset: 0x0005CA34
		public PolicyInformation(DerObjectIdentifier policyIdentifier, Asn1Sequence policyQualifiers)
		{
			this.policyIdentifier = policyIdentifier;
			this.policyQualifiers = policyQualifiers;
		}

		// Token: 0x06001035 RID: 4149 RVA: 0x0005DA4A File Offset: 0x0005CA4A
		public static PolicyInformation GetInstance(object obj)
		{
			if (obj == null || obj is PolicyInformation)
			{
				return (PolicyInformation)obj;
			}
			return new PolicyInformation(Asn1Sequence.GetInstance(obj));
		}

		// Token: 0x17000303 RID: 771
		// (get) Token: 0x06001036 RID: 4150 RVA: 0x0005DA69 File Offset: 0x0005CA69
		public DerObjectIdentifier PolicyIdentifier
		{
			get
			{
				return this.policyIdentifier;
			}
		}

		// Token: 0x17000304 RID: 772
		// (get) Token: 0x06001037 RID: 4151 RVA: 0x0005DA71 File Offset: 0x0005CA71
		public Asn1Sequence PolicyQualifiers
		{
			get
			{
				return this.policyQualifiers;
			}
		}

		// Token: 0x06001038 RID: 4152 RVA: 0x0005DA7C File Offset: 0x0005CA7C
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(new Asn1Encodable[]
			{
				this.policyIdentifier
			});
			if (this.policyQualifiers != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					this.policyQualifiers
				});
			}
			return new DerSequence(asn1EncodableVector);
		}

		// Token: 0x04000BE9 RID: 3049
		private readonly DerObjectIdentifier policyIdentifier;

		// Token: 0x04000BEA RID: 3050
		private readonly Asn1Sequence policyQualifiers;
	}
}
