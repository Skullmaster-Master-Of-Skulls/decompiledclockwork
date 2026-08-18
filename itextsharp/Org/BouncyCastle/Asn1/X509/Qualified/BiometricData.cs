using System;

namespace Org.BouncyCastle.Asn1.X509.Qualified
{
	// Token: 0x020004CF RID: 1231
	public class BiometricData : Asn1Encodable
	{
		// Token: 0x060029FB RID: 10747 RVA: 0x000FFC90 File Offset: 0x000FEC90
		public static BiometricData GetInstance(object obj)
		{
			if (obj == null || obj is BiometricData)
			{
				return (BiometricData)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new BiometricData(Asn1Sequence.GetInstance(obj));
			}
			throw new ArgumentException("unknown object in GetInstance: " + obj.GetType().FullName, "obj");
		}

		// Token: 0x060029FC RID: 10748 RVA: 0x000FFCE4 File Offset: 0x000FECE4
		private BiometricData(Asn1Sequence seq)
		{
			this.typeOfBiometricData = TypeOfBiometricData.GetInstance(seq[0]);
			this.hashAlgorithm = AlgorithmIdentifier.GetInstance(seq[1]);
			this.biometricDataHash = Asn1OctetString.GetInstance(seq[2]);
			if (seq.Count > 3)
			{
				this.sourceDataUri = DerIA5String.GetInstance(seq[3]);
			}
		}

		// Token: 0x060029FD RID: 10749 RVA: 0x000FFD48 File Offset: 0x000FED48
		public BiometricData(TypeOfBiometricData typeOfBiometricData, AlgorithmIdentifier hashAlgorithm, Asn1OctetString biometricDataHash, DerIA5String sourceDataUri)
		{
			this.typeOfBiometricData = typeOfBiometricData;
			this.hashAlgorithm = hashAlgorithm;
			this.biometricDataHash = biometricDataHash;
			this.sourceDataUri = sourceDataUri;
		}

		// Token: 0x060029FE RID: 10750 RVA: 0x000FFD6D File Offset: 0x000FED6D
		public BiometricData(TypeOfBiometricData typeOfBiometricData, AlgorithmIdentifier hashAlgorithm, Asn1OctetString biometricDataHash)
		{
			this.typeOfBiometricData = typeOfBiometricData;
			this.hashAlgorithm = hashAlgorithm;
			this.biometricDataHash = biometricDataHash;
			this.sourceDataUri = null;
		}

		// Token: 0x17000743 RID: 1859
		// (get) Token: 0x060029FF RID: 10751 RVA: 0x000FFD91 File Offset: 0x000FED91
		public TypeOfBiometricData TypeOfBiometricData
		{
			get
			{
				return this.typeOfBiometricData;
			}
		}

		// Token: 0x17000744 RID: 1860
		// (get) Token: 0x06002A00 RID: 10752 RVA: 0x000FFD99 File Offset: 0x000FED99
		public AlgorithmIdentifier HashAlgorithm
		{
			get
			{
				return this.hashAlgorithm;
			}
		}

		// Token: 0x17000745 RID: 1861
		// (get) Token: 0x06002A01 RID: 10753 RVA: 0x000FFDA1 File Offset: 0x000FEDA1
		public Asn1OctetString BiometricDataHash
		{
			get
			{
				return this.biometricDataHash;
			}
		}

		// Token: 0x17000746 RID: 1862
		// (get) Token: 0x06002A02 RID: 10754 RVA: 0x000FFDA9 File Offset: 0x000FEDA9
		public DerIA5String SourceDataUri
		{
			get
			{
				return this.sourceDataUri;
			}
		}

		// Token: 0x06002A03 RID: 10755 RVA: 0x000FFDB4 File Offset: 0x000FEDB4
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(new Asn1Encodable[]
			{
				this.typeOfBiometricData,
				this.hashAlgorithm,
				this.biometricDataHash
			});
			if (this.sourceDataUri != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					this.sourceDataUri
				});
			}
			return new DerSequence(asn1EncodableVector);
		}

		// Token: 0x04001D44 RID: 7492
		private readonly TypeOfBiometricData typeOfBiometricData;

		// Token: 0x04001D45 RID: 7493
		private readonly AlgorithmIdentifier hashAlgorithm;

		// Token: 0x04001D46 RID: 7494
		private readonly Asn1OctetString biometricDataHash;

		// Token: 0x04001D47 RID: 7495
		private readonly DerIA5String sourceDataUri;
	}
}
