using System;

namespace Org.BouncyCastle.Asn1.CryptoPro
{
	// Token: 0x02000266 RID: 614
	public class Gost3410PublicKeyAlgParameters : Asn1Encodable
	{
		// Token: 0x0600172B RID: 5931 RVA: 0x000857C1 File Offset: 0x000847C1
		public static Gost3410PublicKeyAlgParameters GetInstance(Asn1TaggedObject obj, bool explicitly)
		{
			return Gost3410PublicKeyAlgParameters.GetInstance(Asn1Sequence.GetInstance(obj, explicitly));
		}

		// Token: 0x0600172C RID: 5932 RVA: 0x000857D0 File Offset: 0x000847D0
		public static Gost3410PublicKeyAlgParameters GetInstance(object obj)
		{
			if (obj == null || obj is Gost3410PublicKeyAlgParameters)
			{
				return (Gost3410PublicKeyAlgParameters)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new Gost3410PublicKeyAlgParameters((Asn1Sequence)obj);
			}
			throw new ArgumentException("Invalid GOST3410Parameter: " + obj.GetType().Name);
		}

		// Token: 0x0600172D RID: 5933 RVA: 0x0008581D File Offset: 0x0008481D
		public Gost3410PublicKeyAlgParameters(DerObjectIdentifier publicKeyParamSet, DerObjectIdentifier digestParamSet) : this(publicKeyParamSet, digestParamSet, null)
		{
		}

		// Token: 0x0600172E RID: 5934 RVA: 0x00085828 File Offset: 0x00084828
		public Gost3410PublicKeyAlgParameters(DerObjectIdentifier publicKeyParamSet, DerObjectIdentifier digestParamSet, DerObjectIdentifier encryptionParamSet)
		{
			if (publicKeyParamSet == null)
			{
				throw new ArgumentNullException("publicKeyParamSet");
			}
			if (digestParamSet == null)
			{
				throw new ArgumentNullException("digestParamSet");
			}
			this.publicKeyParamSet = publicKeyParamSet;
			this.digestParamSet = digestParamSet;
			this.encryptionParamSet = encryptionParamSet;
		}

		// Token: 0x0600172F RID: 5935 RVA: 0x00085864 File Offset: 0x00084864
		public Gost3410PublicKeyAlgParameters(Asn1Sequence seq)
		{
			this.publicKeyParamSet = (DerObjectIdentifier)seq[0];
			this.digestParamSet = (DerObjectIdentifier)seq[1];
			if (seq.Count > 2)
			{
				this.encryptionParamSet = (DerObjectIdentifier)seq[2];
			}
		}

		// Token: 0x1700043B RID: 1083
		// (get) Token: 0x06001730 RID: 5936 RVA: 0x000858B6 File Offset: 0x000848B6
		public DerObjectIdentifier PublicKeyParamSet
		{
			get
			{
				return this.publicKeyParamSet;
			}
		}

		// Token: 0x1700043C RID: 1084
		// (get) Token: 0x06001731 RID: 5937 RVA: 0x000858BE File Offset: 0x000848BE
		public DerObjectIdentifier DigestParamSet
		{
			get
			{
				return this.digestParamSet;
			}
		}

		// Token: 0x1700043D RID: 1085
		// (get) Token: 0x06001732 RID: 5938 RVA: 0x000858C6 File Offset: 0x000848C6
		public DerObjectIdentifier EncryptionParamSet
		{
			get
			{
				return this.encryptionParamSet;
			}
		}

		// Token: 0x06001733 RID: 5939 RVA: 0x000858D0 File Offset: 0x000848D0
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(new Asn1Encodable[]
			{
				this.publicKeyParamSet,
				this.digestParamSet
			});
			if (this.encryptionParamSet != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					this.encryptionParamSet
				});
			}
			return new DerSequence(asn1EncodableVector);
		}

		// Token: 0x04000FD3 RID: 4051
		private DerObjectIdentifier publicKeyParamSet;

		// Token: 0x04000FD4 RID: 4052
		private DerObjectIdentifier digestParamSet;

		// Token: 0x04000FD5 RID: 4053
		private DerObjectIdentifier encryptionParamSet;
	}
}
