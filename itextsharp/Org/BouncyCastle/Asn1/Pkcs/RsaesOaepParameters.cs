using System;
using Org.BouncyCastle.Asn1.Oiw;
using Org.BouncyCastle.Asn1.X509;

namespace Org.BouncyCastle.Asn1.Pkcs
{
	// Token: 0x020001AE RID: 430
	public class RsaesOaepParameters : Asn1Encodable
	{
		// Token: 0x06001056 RID: 4182 RVA: 0x0005E16C File Offset: 0x0005D16C
		public static RsaesOaepParameters GetInstance(object obj)
		{
			if (obj is RsaesOaepParameters)
			{
				return (RsaesOaepParameters)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new RsaesOaepParameters((Asn1Sequence)obj);
			}
			throw new ArgumentException("Unknown object in factory: " + obj.GetType().FullName, "obj");
		}

		// Token: 0x06001057 RID: 4183 RVA: 0x0005E1BB File Offset: 0x0005D1BB
		public RsaesOaepParameters()
		{
			this.hashAlgorithm = RsaesOaepParameters.DefaultHashAlgorithm;
			this.maskGenAlgorithm = RsaesOaepParameters.DefaultMaskGenFunction;
			this.pSourceAlgorithm = RsaesOaepParameters.DefaultPSourceAlgorithm;
		}

		// Token: 0x06001058 RID: 4184 RVA: 0x0005E1E4 File Offset: 0x0005D1E4
		public RsaesOaepParameters(AlgorithmIdentifier hashAlgorithm, AlgorithmIdentifier maskGenAlgorithm, AlgorithmIdentifier pSourceAlgorithm)
		{
			this.hashAlgorithm = hashAlgorithm;
			this.maskGenAlgorithm = maskGenAlgorithm;
			this.pSourceAlgorithm = pSourceAlgorithm;
		}

		// Token: 0x06001059 RID: 4185 RVA: 0x0005E204 File Offset: 0x0005D204
		public RsaesOaepParameters(Asn1Sequence seq)
		{
			this.hashAlgorithm = RsaesOaepParameters.DefaultHashAlgorithm;
			this.maskGenAlgorithm = RsaesOaepParameters.DefaultMaskGenFunction;
			this.pSourceAlgorithm = RsaesOaepParameters.DefaultPSourceAlgorithm;
			for (int num = 0; num != seq.Count; num++)
			{
				Asn1TaggedObject asn1TaggedObject = (Asn1TaggedObject)seq[num];
				switch (asn1TaggedObject.TagNo)
				{
				case 0:
					this.hashAlgorithm = AlgorithmIdentifier.GetInstance(asn1TaggedObject, true);
					break;
				case 1:
					this.maskGenAlgorithm = AlgorithmIdentifier.GetInstance(asn1TaggedObject, true);
					break;
				case 2:
					this.pSourceAlgorithm = AlgorithmIdentifier.GetInstance(asn1TaggedObject, true);
					break;
				default:
					throw new ArgumentException("unknown tag");
				}
			}
		}

		// Token: 0x1700030F RID: 783
		// (get) Token: 0x0600105A RID: 4186 RVA: 0x0005E2A9 File Offset: 0x0005D2A9
		public AlgorithmIdentifier HashAlgorithm
		{
			get
			{
				return this.hashAlgorithm;
			}
		}

		// Token: 0x17000310 RID: 784
		// (get) Token: 0x0600105B RID: 4187 RVA: 0x0005E2B1 File Offset: 0x0005D2B1
		public AlgorithmIdentifier MaskGenAlgorithm
		{
			get
			{
				return this.maskGenAlgorithm;
			}
		}

		// Token: 0x17000311 RID: 785
		// (get) Token: 0x0600105C RID: 4188 RVA: 0x0005E2B9 File Offset: 0x0005D2B9
		public AlgorithmIdentifier PSourceAlgorithm
		{
			get
			{
				return this.pSourceAlgorithm;
			}
		}

		// Token: 0x0600105D RID: 4189 RVA: 0x0005E2C4 File Offset: 0x0005D2C4
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(new Asn1Encodable[0]);
			if (!this.hashAlgorithm.Equals(RsaesOaepParameters.DefaultHashAlgorithm))
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					new DerTaggedObject(true, 0, this.hashAlgorithm)
				});
			}
			if (!this.maskGenAlgorithm.Equals(RsaesOaepParameters.DefaultMaskGenFunction))
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					new DerTaggedObject(true, 1, this.maskGenAlgorithm)
				});
			}
			if (!this.pSourceAlgorithm.Equals(RsaesOaepParameters.DefaultPSourceAlgorithm))
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					new DerTaggedObject(true, 2, this.pSourceAlgorithm)
				});
			}
			return new DerSequence(asn1EncodableVector);
		}

		// Token: 0x04000C07 RID: 3079
		private AlgorithmIdentifier hashAlgorithm;

		// Token: 0x04000C08 RID: 3080
		private AlgorithmIdentifier maskGenAlgorithm;

		// Token: 0x04000C09 RID: 3081
		private AlgorithmIdentifier pSourceAlgorithm;

		// Token: 0x04000C0A RID: 3082
		public static readonly AlgorithmIdentifier DefaultHashAlgorithm = new AlgorithmIdentifier(OiwObjectIdentifiers.IdSha1, DerNull.Instance);

		// Token: 0x04000C0B RID: 3083
		public static readonly AlgorithmIdentifier DefaultMaskGenFunction = new AlgorithmIdentifier(PkcsObjectIdentifiers.IdMgf1, RsaesOaepParameters.DefaultHashAlgorithm);

		// Token: 0x04000C0C RID: 3084
		public static readonly AlgorithmIdentifier DefaultPSourceAlgorithm = new AlgorithmIdentifier(PkcsObjectIdentifiers.IdPSpecified, new DerOctetString(new byte[0]));
	}
}
