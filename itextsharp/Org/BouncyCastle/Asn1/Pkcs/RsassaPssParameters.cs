using System;
using Org.BouncyCastle.Asn1.Oiw;
using Org.BouncyCastle.Asn1.X509;

namespace Org.BouncyCastle.Asn1.Pkcs
{
	// Token: 0x02000627 RID: 1575
	public class RsassaPssParameters : Asn1Encodable
	{
		// Token: 0x06003570 RID: 13680 RVA: 0x0014B76C File Offset: 0x0014A76C
		public static RsassaPssParameters GetInstance(object obj)
		{
			if (obj == null || obj is RsassaPssParameters)
			{
				return (RsassaPssParameters)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new RsassaPssParameters((Asn1Sequence)obj);
			}
			throw new ArgumentException("Unknown object in factory: " + obj.GetType().FullName, "obj");
		}

		// Token: 0x06003571 RID: 13681 RVA: 0x0014B7BE File Offset: 0x0014A7BE
		public RsassaPssParameters()
		{
			this.hashAlgorithm = RsassaPssParameters.DefaultHashAlgorithm;
			this.maskGenAlgorithm = RsassaPssParameters.DefaultMaskGenFunction;
			this.saltLength = RsassaPssParameters.DefaultSaltLength;
			this.trailerField = RsassaPssParameters.DefaultTrailerField;
		}

		// Token: 0x06003572 RID: 13682 RVA: 0x0014B7F2 File Offset: 0x0014A7F2
		public RsassaPssParameters(AlgorithmIdentifier hashAlgorithm, AlgorithmIdentifier maskGenAlgorithm, DerInteger saltLength, DerInteger trailerField)
		{
			this.hashAlgorithm = hashAlgorithm;
			this.maskGenAlgorithm = maskGenAlgorithm;
			this.saltLength = saltLength;
			this.trailerField = trailerField;
		}

		// Token: 0x06003573 RID: 13683 RVA: 0x0014B818 File Offset: 0x0014A818
		public RsassaPssParameters(Asn1Sequence seq)
		{
			this.hashAlgorithm = RsassaPssParameters.DefaultHashAlgorithm;
			this.maskGenAlgorithm = RsassaPssParameters.DefaultMaskGenFunction;
			this.saltLength = RsassaPssParameters.DefaultSaltLength;
			this.trailerField = RsassaPssParameters.DefaultTrailerField;
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
					this.saltLength = DerInteger.GetInstance(asn1TaggedObject, true);
					break;
				case 3:
					this.trailerField = DerInteger.GetInstance(asn1TaggedObject, true);
					break;
				default:
					throw new ArgumentException("unknown tag");
				}
			}
		}

		// Token: 0x17000942 RID: 2370
		// (get) Token: 0x06003574 RID: 13684 RVA: 0x0014B8DB File Offset: 0x0014A8DB
		public AlgorithmIdentifier HashAlgorithm
		{
			get
			{
				return this.hashAlgorithm;
			}
		}

		// Token: 0x17000943 RID: 2371
		// (get) Token: 0x06003575 RID: 13685 RVA: 0x0014B8E3 File Offset: 0x0014A8E3
		public AlgorithmIdentifier MaskGenAlgorithm
		{
			get
			{
				return this.maskGenAlgorithm;
			}
		}

		// Token: 0x17000944 RID: 2372
		// (get) Token: 0x06003576 RID: 13686 RVA: 0x0014B8EB File Offset: 0x0014A8EB
		public DerInteger SaltLength
		{
			get
			{
				return this.saltLength;
			}
		}

		// Token: 0x17000945 RID: 2373
		// (get) Token: 0x06003577 RID: 13687 RVA: 0x0014B8F3 File Offset: 0x0014A8F3
		public DerInteger TrailerField
		{
			get
			{
				return this.trailerField;
			}
		}

		// Token: 0x06003578 RID: 13688 RVA: 0x0014B8FC File Offset: 0x0014A8FC
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(new Asn1Encodable[0]);
			if (!this.hashAlgorithm.Equals(RsassaPssParameters.DefaultHashAlgorithm))
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					new DerTaggedObject(true, 0, this.hashAlgorithm)
				});
			}
			if (!this.maskGenAlgorithm.Equals(RsassaPssParameters.DefaultMaskGenFunction))
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					new DerTaggedObject(true, 1, this.maskGenAlgorithm)
				});
			}
			if (!this.saltLength.Equals(RsassaPssParameters.DefaultSaltLength))
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					new DerTaggedObject(true, 2, this.saltLength)
				});
			}
			if (!this.trailerField.Equals(RsassaPssParameters.DefaultTrailerField))
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					new DerTaggedObject(true, 3, this.trailerField)
				});
			}
			return new DerSequence(asn1EncodableVector);
		}

		// Token: 0x040023B8 RID: 9144
		private AlgorithmIdentifier hashAlgorithm;

		// Token: 0x040023B9 RID: 9145
		private AlgorithmIdentifier maskGenAlgorithm;

		// Token: 0x040023BA RID: 9146
		private DerInteger saltLength;

		// Token: 0x040023BB RID: 9147
		private DerInteger trailerField;

		// Token: 0x040023BC RID: 9148
		public static readonly AlgorithmIdentifier DefaultHashAlgorithm = new AlgorithmIdentifier(OiwObjectIdentifiers.IdSha1, DerNull.Instance);

		// Token: 0x040023BD RID: 9149
		public static readonly AlgorithmIdentifier DefaultMaskGenFunction = new AlgorithmIdentifier(PkcsObjectIdentifiers.IdMgf1, RsassaPssParameters.DefaultHashAlgorithm);

		// Token: 0x040023BE RID: 9150
		public static readonly DerInteger DefaultSaltLength = new DerInteger(20);

		// Token: 0x040023BF RID: 9151
		public static readonly DerInteger DefaultTrailerField = new DerInteger(1);
	}
}
