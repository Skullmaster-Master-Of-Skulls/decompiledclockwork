using System;
using System.Collections;
using Org.BouncyCastle.Utilities.Collections;

namespace Org.BouncyCastle.Asn1.Esf
{
	// Token: 0x0200048D RID: 1165
	public class SignaturePolicyId : Asn1Encodable
	{
		// Token: 0x0600276F RID: 10095 RVA: 0x000EDD88 File Offset: 0x000ECD88
		public static SignaturePolicyId GetInstance(object obj)
		{
			if (obj == null || obj is SignaturePolicyId)
			{
				return (SignaturePolicyId)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new SignaturePolicyId((Asn1Sequence)obj);
			}
			throw new ArgumentException("Unknown object in 'SignaturePolicyId' factory: " + obj.GetType().Name, "obj");
		}

		// Token: 0x06002770 RID: 10096 RVA: 0x000EDDDC File Offset: 0x000ECDDC
		private SignaturePolicyId(Asn1Sequence seq)
		{
			if (seq == null)
			{
				throw new ArgumentNullException("seq");
			}
			if (seq.Count < 2 || seq.Count > 3)
			{
				throw new ArgumentException("Bad sequence size: " + seq.Count, "seq");
			}
			this.sigPolicyIdentifier = (DerObjectIdentifier)seq[0].ToAsn1Object();
			this.sigPolicyHash = OtherHashAlgAndValue.GetInstance(seq[1].ToAsn1Object());
			if (seq.Count > 2)
			{
				this.sigPolicyQualifiers = (Asn1Sequence)seq[2].ToAsn1Object();
			}
		}

		// Token: 0x06002771 RID: 10097 RVA: 0x000EDE7D File Offset: 0x000ECE7D
		public SignaturePolicyId(DerObjectIdentifier sigPolicyIdentifier, OtherHashAlgAndValue sigPolicyHash) : this(sigPolicyIdentifier, sigPolicyHash, null)
		{
		}

		// Token: 0x06002772 RID: 10098 RVA: 0x000EDE88 File Offset: 0x000ECE88
		public SignaturePolicyId(DerObjectIdentifier sigPolicyIdentifier, OtherHashAlgAndValue sigPolicyHash, params SigPolicyQualifierInfo[] sigPolicyQualifiers)
		{
			if (sigPolicyIdentifier == null)
			{
				throw new ArgumentNullException("sigPolicyIdentifier");
			}
			if (sigPolicyHash == null)
			{
				throw new ArgumentNullException("sigPolicyHash");
			}
			this.sigPolicyIdentifier = sigPolicyIdentifier;
			this.sigPolicyHash = sigPolicyHash;
			if (sigPolicyQualifiers != null)
			{
				this.sigPolicyQualifiers = new DerSequence(sigPolicyQualifiers);
			}
		}

		// Token: 0x06002773 RID: 10099 RVA: 0x000EDED4 File Offset: 0x000ECED4
		public SignaturePolicyId(DerObjectIdentifier sigPolicyIdentifier, OtherHashAlgAndValue sigPolicyHash, IEnumerable sigPolicyQualifiers)
		{
			if (sigPolicyIdentifier == null)
			{
				throw new ArgumentNullException("sigPolicyIdentifier");
			}
			if (sigPolicyHash == null)
			{
				throw new ArgumentNullException("sigPolicyHash");
			}
			this.sigPolicyIdentifier = sigPolicyIdentifier;
			this.sigPolicyHash = sigPolicyHash;
			if (sigPolicyQualifiers != null)
			{
				if (!CollectionUtilities.CheckElementsAreOfType(sigPolicyQualifiers, typeof(SigPolicyQualifierInfo)))
				{
					throw new ArgumentException("Must contain only 'SigPolicyQualifierInfo' objects", "sigPolicyQualifiers");
				}
				this.sigPolicyQualifiers = new DerSequence(Asn1EncodableVector.FromEnumerable(sigPolicyQualifiers));
			}
		}

		// Token: 0x170006CD RID: 1741
		// (get) Token: 0x06002774 RID: 10100 RVA: 0x000EDF47 File Offset: 0x000ECF47
		public DerObjectIdentifier SigPolicyIdentifier
		{
			get
			{
				return this.sigPolicyIdentifier;
			}
		}

		// Token: 0x170006CE RID: 1742
		// (get) Token: 0x06002775 RID: 10101 RVA: 0x000EDF4F File Offset: 0x000ECF4F
		public OtherHashAlgAndValue SigPolicyHash
		{
			get
			{
				return this.sigPolicyHash;
			}
		}

		// Token: 0x06002776 RID: 10102 RVA: 0x000EDF58 File Offset: 0x000ECF58
		public SigPolicyQualifierInfo[] GetSigPolicyQualifiers()
		{
			if (this.sigPolicyQualifiers == null)
			{
				return null;
			}
			SigPolicyQualifierInfo[] array = new SigPolicyQualifierInfo[this.sigPolicyQualifiers.Count];
			for (int i = 0; i < this.sigPolicyQualifiers.Count; i++)
			{
				array[i] = SigPolicyQualifierInfo.GetInstance(this.sigPolicyQualifiers[i]);
			}
			return array;
		}

		// Token: 0x06002777 RID: 10103 RVA: 0x000EDFAC File Offset: 0x000ECFAC
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(new Asn1Encodable[]
			{
				this.sigPolicyIdentifier,
				this.sigPolicyHash.ToAsn1Object()
			});
			if (this.sigPolicyQualifiers != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					this.sigPolicyQualifiers.ToAsn1Object()
				});
			}
			return new DerSequence(asn1EncodableVector);
		}

		// Token: 0x04001B28 RID: 6952
		private readonly DerObjectIdentifier sigPolicyIdentifier;

		// Token: 0x04001B29 RID: 6953
		private readonly OtherHashAlgAndValue sigPolicyHash;

		// Token: 0x04001B2A RID: 6954
		private readonly Asn1Sequence sigPolicyQualifiers;
	}
}
