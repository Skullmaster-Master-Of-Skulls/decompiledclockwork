using System;
using System.Collections;
using Org.BouncyCastle.Asn1.X509;

namespace Org.BouncyCastle.Asn1.Pkcs
{
	// Token: 0x0200044A RID: 1098
	public class SignerInfo : Asn1Encodable
	{
		// Token: 0x0600251D RID: 9501 RVA: 0x000E1650 File Offset: 0x000E0650
		public static SignerInfo GetInstance(object obj)
		{
			if (obj is SignerInfo)
			{
				return (SignerInfo)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new SignerInfo((Asn1Sequence)obj);
			}
			throw new ArgumentException("Unknown object in factory: " + obj.GetType().FullName, "obj");
		}

		// Token: 0x0600251E RID: 9502 RVA: 0x000E169F File Offset: 0x000E069F
		public SignerInfo(DerInteger version, IssuerAndSerialNumber issuerAndSerialNumber, AlgorithmIdentifier digAlgorithm, Asn1Set authenticatedAttributes, AlgorithmIdentifier digEncryptionAlgorithm, Asn1OctetString encryptedDigest, Asn1Set unauthenticatedAttributes)
		{
			this.version = version;
			this.issuerAndSerialNumber = issuerAndSerialNumber;
			this.digAlgorithm = digAlgorithm;
			this.authenticatedAttributes = authenticatedAttributes;
			this.digEncryptionAlgorithm = digEncryptionAlgorithm;
			this.encryptedDigest = encryptedDigest;
			this.unauthenticatedAttributes = unauthenticatedAttributes;
		}

		// Token: 0x0600251F RID: 9503 RVA: 0x000E16DC File Offset: 0x000E06DC
		public SignerInfo(Asn1Sequence seq)
		{
			IEnumerator enumerator = seq.GetEnumerator();
			enumerator.MoveNext();
			this.version = (DerInteger)enumerator.Current;
			enumerator.MoveNext();
			this.issuerAndSerialNumber = IssuerAndSerialNumber.GetInstance(enumerator.Current);
			enumerator.MoveNext();
			this.digAlgorithm = AlgorithmIdentifier.GetInstance(enumerator.Current);
			enumerator.MoveNext();
			object obj = enumerator.Current;
			if (obj is Asn1TaggedObject)
			{
				this.authenticatedAttributes = Asn1Set.GetInstance((Asn1TaggedObject)obj, false);
				enumerator.MoveNext();
				this.digEncryptionAlgorithm = AlgorithmIdentifier.GetInstance(enumerator.Current);
			}
			else
			{
				this.authenticatedAttributes = null;
				this.digEncryptionAlgorithm = AlgorithmIdentifier.GetInstance(obj);
			}
			enumerator.MoveNext();
			this.encryptedDigest = Asn1OctetString.GetInstance(enumerator.Current);
			if (enumerator.MoveNext())
			{
				this.unauthenticatedAttributes = Asn1Set.GetInstance((Asn1TaggedObject)enumerator.Current, false);
				return;
			}
			this.unauthenticatedAttributes = null;
		}

		// Token: 0x17000659 RID: 1625
		// (get) Token: 0x06002520 RID: 9504 RVA: 0x000E17D2 File Offset: 0x000E07D2
		public DerInteger Version
		{
			get
			{
				return this.version;
			}
		}

		// Token: 0x1700065A RID: 1626
		// (get) Token: 0x06002521 RID: 9505 RVA: 0x000E17DA File Offset: 0x000E07DA
		public IssuerAndSerialNumber IssuerAndSerialNumber
		{
			get
			{
				return this.issuerAndSerialNumber;
			}
		}

		// Token: 0x1700065B RID: 1627
		// (get) Token: 0x06002522 RID: 9506 RVA: 0x000E17E2 File Offset: 0x000E07E2
		public Asn1Set AuthenticatedAttributes
		{
			get
			{
				return this.authenticatedAttributes;
			}
		}

		// Token: 0x1700065C RID: 1628
		// (get) Token: 0x06002523 RID: 9507 RVA: 0x000E17EA File Offset: 0x000E07EA
		public AlgorithmIdentifier DigestAlgorithm
		{
			get
			{
				return this.digAlgorithm;
			}
		}

		// Token: 0x1700065D RID: 1629
		// (get) Token: 0x06002524 RID: 9508 RVA: 0x000E17F2 File Offset: 0x000E07F2
		public Asn1OctetString EncryptedDigest
		{
			get
			{
				return this.encryptedDigest;
			}
		}

		// Token: 0x1700065E RID: 1630
		// (get) Token: 0x06002525 RID: 9509 RVA: 0x000E17FA File Offset: 0x000E07FA
		public AlgorithmIdentifier DigestEncryptionAlgorithm
		{
			get
			{
				return this.digEncryptionAlgorithm;
			}
		}

		// Token: 0x1700065F RID: 1631
		// (get) Token: 0x06002526 RID: 9510 RVA: 0x000E1802 File Offset: 0x000E0802
		public Asn1Set UnauthenticatedAttributes
		{
			get
			{
				return this.unauthenticatedAttributes;
			}
		}

		// Token: 0x06002527 RID: 9511 RVA: 0x000E180C File Offset: 0x000E080C
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(new Asn1Encodable[]
			{
				this.version,
				this.issuerAndSerialNumber,
				this.digAlgorithm
			});
			if (this.authenticatedAttributes != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					new DerTaggedObject(false, 0, this.authenticatedAttributes)
				});
			}
			asn1EncodableVector.Add(new Asn1Encodable[]
			{
				this.digEncryptionAlgorithm,
				this.encryptedDigest
			});
			if (this.unauthenticatedAttributes != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					new DerTaggedObject(false, 1, this.unauthenticatedAttributes)
				});
			}
			return new DerSequence(asn1EncodableVector);
		}

		// Token: 0x04001A0B RID: 6667
		private DerInteger version;

		// Token: 0x04001A0C RID: 6668
		private IssuerAndSerialNumber issuerAndSerialNumber;

		// Token: 0x04001A0D RID: 6669
		private AlgorithmIdentifier digAlgorithm;

		// Token: 0x04001A0E RID: 6670
		private Asn1Set authenticatedAttributes;

		// Token: 0x04001A0F RID: 6671
		private AlgorithmIdentifier digEncryptionAlgorithm;

		// Token: 0x04001A10 RID: 6672
		private Asn1OctetString encryptedDigest;

		// Token: 0x04001A11 RID: 6673
		private Asn1Set unauthenticatedAttributes;
	}
}
