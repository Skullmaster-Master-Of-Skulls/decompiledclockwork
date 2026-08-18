using System;
using System.Collections;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Security.Certificates;
using Org.BouncyCastle.Utilities;
using Org.BouncyCastle.X509.Store;

namespace Org.BouncyCastle.X509
{
	// Token: 0x02000466 RID: 1126
	public class AttributeCertificateHolder : IX509Selector, ICloneable
	{
		// Token: 0x06002644 RID: 9796 RVA: 0x000E7C42 File Offset: 0x000E6C42
		internal AttributeCertificateHolder(Asn1Sequence seq)
		{
			this.holder = Holder.GetInstance(seq);
		}

		// Token: 0x06002645 RID: 9797 RVA: 0x000E7C56 File Offset: 0x000E6C56
		public AttributeCertificateHolder(X509Name issuerName, BigInteger serialNumber)
		{
			this.holder = new Holder(new IssuerSerial(this.GenerateGeneralNames(issuerName), new DerInteger(serialNumber)));
		}

		// Token: 0x06002646 RID: 9798 RVA: 0x000E7C7C File Offset: 0x000E6C7C
		public AttributeCertificateHolder(X509Certificate cert)
		{
			X509Name issuerX509Principal;
			try
			{
				issuerX509Principal = PrincipalUtilities.GetIssuerX509Principal(cert);
			}
			catch (Exception ex)
			{
				throw new CertificateParsingException(ex.Message);
			}
			this.holder = new Holder(new IssuerSerial(this.GenerateGeneralNames(issuerX509Principal), new DerInteger(cert.SerialNumber)));
		}

		// Token: 0x06002647 RID: 9799 RVA: 0x000E7CD8 File Offset: 0x000E6CD8
		public AttributeCertificateHolder(X509Name principal)
		{
			this.holder = new Holder(this.GenerateGeneralNames(principal));
		}

		// Token: 0x06002648 RID: 9800 RVA: 0x000E7CF2 File Offset: 0x000E6CF2
		public AttributeCertificateHolder(int digestedObjectType, string digestAlgorithm, string otherObjectTypeID, byte[] objectDigest)
		{
			this.holder = new Holder(new ObjectDigestInfo(digestedObjectType, otherObjectTypeID, new AlgorithmIdentifier(digestAlgorithm), Arrays.Clone(objectDigest)));
		}

		// Token: 0x17000697 RID: 1687
		// (get) Token: 0x06002649 RID: 9801 RVA: 0x000E7D1C File Offset: 0x000E6D1C
		public int DigestedObjectType
		{
			get
			{
				ObjectDigestInfo objectDigestInfo = this.holder.ObjectDigestInfo;
				if (objectDigestInfo != null)
				{
					return objectDigestInfo.DigestedObjectType.Value.IntValue;
				}
				return -1;
			}
		}

		// Token: 0x17000698 RID: 1688
		// (get) Token: 0x0600264A RID: 9802 RVA: 0x000E7D4C File Offset: 0x000E6D4C
		public string DigestAlgorithm
		{
			get
			{
				ObjectDigestInfo objectDigestInfo = this.holder.ObjectDigestInfo;
				if (objectDigestInfo != null)
				{
					return objectDigestInfo.DigestAlgorithm.ObjectID.Id;
				}
				return null;
			}
		}

		// Token: 0x0600264B RID: 9803 RVA: 0x000E7D7C File Offset: 0x000E6D7C
		public byte[] GetObjectDigest()
		{
			ObjectDigestInfo objectDigestInfo = this.holder.ObjectDigestInfo;
			if (objectDigestInfo != null)
			{
				return objectDigestInfo.ObjectDigest.GetBytes();
			}
			return null;
		}

		// Token: 0x17000699 RID: 1689
		// (get) Token: 0x0600264C RID: 9804 RVA: 0x000E7DA8 File Offset: 0x000E6DA8
		public string OtherObjectTypeID
		{
			get
			{
				ObjectDigestInfo objectDigestInfo = this.holder.ObjectDigestInfo;
				if (objectDigestInfo != null)
				{
					return objectDigestInfo.OtherObjectTypeID.Id;
				}
				return null;
			}
		}

		// Token: 0x0600264D RID: 9805 RVA: 0x000E7DD1 File Offset: 0x000E6DD1
		private GeneralNames GenerateGeneralNames(X509Name principal)
		{
			return new GeneralNames(new GeneralName(principal));
		}

		// Token: 0x0600264E RID: 9806 RVA: 0x000E7DE0 File Offset: 0x000E6DE0
		private bool MatchesDN(X509Name subject, GeneralNames targets)
		{
			GeneralName[] names = targets.GetNames();
			for (int num = 0; num != names.Length; num++)
			{
				GeneralName generalName = names[num];
				if (generalName.TagNo == 4)
				{
					try
					{
						if (X509Name.GetInstance(generalName.Name).Equivalent(subject))
						{
							return true;
						}
					}
					catch (Exception)
					{
					}
				}
			}
			return false;
		}

		// Token: 0x0600264F RID: 9807 RVA: 0x000E7E40 File Offset: 0x000E6E40
		private object[] GetNames(GeneralName[] names)
		{
			ArrayList arrayList = new ArrayList(names.Length);
			for (int num = 0; num != names.Length; num++)
			{
				if (names[num].TagNo == 4)
				{
					arrayList.Add(X509Name.GetInstance(names[num].Name));
				}
			}
			return arrayList.ToArray();
		}

		// Token: 0x06002650 RID: 9808 RVA: 0x000E7E8C File Offset: 0x000E6E8C
		private X509Name[] GetPrincipals(GeneralNames names)
		{
			object[] names2 = this.GetNames(names.GetNames());
			ArrayList arrayList = new ArrayList(names2.Length);
			for (int num = 0; num != names2.Length; num++)
			{
				if (names2[num] is X509Name)
				{
					arrayList.Add(names2[num]);
				}
			}
			return (X509Name[])arrayList.ToArray(typeof(X509Name));
		}

		// Token: 0x06002651 RID: 9809 RVA: 0x000E7EE6 File Offset: 0x000E6EE6
		public X509Name[] GetEntityNames()
		{
			if (this.holder.EntityName != null)
			{
				return this.GetPrincipals(this.holder.EntityName);
			}
			return null;
		}

		// Token: 0x06002652 RID: 9810 RVA: 0x000E7F08 File Offset: 0x000E6F08
		public X509Name[] GetIssuer()
		{
			if (this.holder.BaseCertificateID != null)
			{
				return this.GetPrincipals(this.holder.BaseCertificateID.Issuer);
			}
			return null;
		}

		// Token: 0x1700069A RID: 1690
		// (get) Token: 0x06002653 RID: 9811 RVA: 0x000E7F2F File Offset: 0x000E6F2F
		public BigInteger SerialNumber
		{
			get
			{
				if (this.holder.BaseCertificateID != null)
				{
					return this.holder.BaseCertificateID.Serial.Value;
				}
				return null;
			}
		}

		// Token: 0x06002654 RID: 9812 RVA: 0x000E7F55 File Offset: 0x000E6F55
		public object Clone()
		{
			return new AttributeCertificateHolder((Asn1Sequence)this.holder.ToAsn1Object());
		}

		// Token: 0x06002655 RID: 9813 RVA: 0x000E7F6C File Offset: 0x000E6F6C
		public bool Match(X509Certificate x509Cert)
		{
			try
			{
				if (this.holder.BaseCertificateID != null)
				{
					return this.holder.BaseCertificateID.Serial.Value.Equals(x509Cert.SerialNumber) && this.MatchesDN(PrincipalUtilities.GetIssuerX509Principal(x509Cert), this.holder.BaseCertificateID.Issuer);
				}
				if (this.holder.EntityName != null && this.MatchesDN(PrincipalUtilities.GetSubjectX509Principal(x509Cert), this.holder.EntityName))
				{
					return true;
				}
				if (this.holder.ObjectDigestInfo != null)
				{
					IDigest digest = null;
					try
					{
						digest = DigestUtilities.GetDigest(this.DigestAlgorithm);
					}
					catch (Exception)
					{
						return false;
					}
					switch (this.DigestedObjectType)
					{
					case 0:
					{
						byte[] encoded = SubjectPublicKeyInfoFactory.CreateSubjectPublicKeyInfo(x509Cert.GetPublicKey()).GetEncoded();
						digest.BlockUpdate(encoded, 0, encoded.Length);
						break;
					}
					case 1:
					{
						byte[] encoded2 = x509Cert.GetEncoded();
						digest.BlockUpdate(encoded2, 0, encoded2.Length);
						break;
					}
					}
					if (!Arrays.AreEqual(DigestUtilities.DoFinal(digest), this.GetObjectDigest()))
					{
						return false;
					}
				}
			}
			catch (CertificateEncodingException)
			{
				return false;
			}
			return false;
		}

		// Token: 0x06002656 RID: 9814 RVA: 0x000E80BC File Offset: 0x000E70BC
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			if (!(obj is AttributeCertificateHolder))
			{
				return false;
			}
			AttributeCertificateHolder attributeCertificateHolder = (AttributeCertificateHolder)obj;
			return this.holder.Equals(attributeCertificateHolder.holder);
		}

		// Token: 0x06002657 RID: 9815 RVA: 0x000E80F1 File Offset: 0x000E70F1
		public override int GetHashCode()
		{
			return this.holder.GetHashCode();
		}

		// Token: 0x06002658 RID: 9816 RVA: 0x000E80FE File Offset: 0x000E70FE
		public bool Match(object obj)
		{
			return obj is X509Certificate && this.Match((X509Certificate)obj);
		}

		// Token: 0x04001AA2 RID: 6818
		internal readonly Holder holder;
	}
}
