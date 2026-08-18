using System;
using System.Collections;
using System.Text;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.Misc;
using Org.BouncyCastle.Asn1.Utilities;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Security.Certificates;
using Org.BouncyCastle.Utilities;
using Org.BouncyCastle.Utilities.Encoders;
using Org.BouncyCastle.X509.Extension;

namespace Org.BouncyCastle.X509
{
	// Token: 0x02000069 RID: 105
	public class X509Certificate : X509ExtensionBase
	{
		// Token: 0x06000363 RID: 867 RVA: 0x00011CBE File Offset: 0x00010CBE
		protected X509Certificate()
		{
		}

		// Token: 0x06000364 RID: 868 RVA: 0x00011CC8 File Offset: 0x00010CC8
		public X509Certificate(X509CertificateStructure c)
		{
			this.c = c;
			try
			{
				Asn1OctetString extensionValue = this.GetExtensionValue(new DerObjectIdentifier("2.5.29.19"));
				if (extensionValue != null)
				{
					this.basicConstraints = BasicConstraints.GetInstance(X509ExtensionUtilities.FromExtensionValue(extensionValue));
				}
			}
			catch (Exception arg)
			{
				throw new CertificateParsingException("cannot construct BasicConstraints: " + arg);
			}
			try
			{
				Asn1OctetString extensionValue2 = this.GetExtensionValue(new DerObjectIdentifier("2.5.29.15"));
				if (extensionValue2 != null)
				{
					DerBitString instance = DerBitString.GetInstance(X509ExtensionUtilities.FromExtensionValue(extensionValue2));
					byte[] bytes = instance.GetBytes();
					int num = bytes.Length * 8 - instance.PadBits;
					this.keyUsage = new bool[(num < 9) ? 9 : num];
					for (int num2 = 0; num2 != num; num2++)
					{
						this.keyUsage[num2] = (((int)bytes[num2 / 8] & 128 >> num2 % 8) != 0);
					}
				}
				else
				{
					this.keyUsage = null;
				}
			}
			catch (Exception arg2)
			{
				throw new CertificateParsingException("cannot construct KeyUsage: " + arg2);
			}
		}

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x06000365 RID: 869 RVA: 0x00011DDC File Offset: 0x00010DDC
		public virtual bool IsValidNow
		{
			get
			{
				return this.IsValid(DateTime.UtcNow);
			}
		}

		// Token: 0x06000366 RID: 870 RVA: 0x00011DE9 File Offset: 0x00010DE9
		public virtual bool IsValid(DateTime time)
		{
			return time.CompareTo(this.NotBefore) >= 0 && time.CompareTo(this.NotAfter) <= 0;
		}

		// Token: 0x06000367 RID: 871 RVA: 0x00011E10 File Offset: 0x00010E10
		public virtual void CheckValidity()
		{
			this.CheckValidity(DateTime.UtcNow);
		}

		// Token: 0x06000368 RID: 872 RVA: 0x00011E20 File Offset: 0x00010E20
		public virtual void CheckValidity(DateTime time)
		{
			if (time.CompareTo(this.NotAfter) > 0)
			{
				throw new CertificateExpiredException("certificate expired on " + this.c.EndDate.GetTime());
			}
			if (time.CompareTo(this.NotBefore) < 0)
			{
				throw new CertificateNotYetValidException("certificate not valid until " + this.c.StartDate.GetTime());
			}
		}

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x06000369 RID: 873 RVA: 0x00011E8D File Offset: 0x00010E8D
		public virtual int Version
		{
			get
			{
				return this.c.Version;
			}
		}

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x0600036A RID: 874 RVA: 0x00011E9A File Offset: 0x00010E9A
		public virtual BigInteger SerialNumber
		{
			get
			{
				return this.c.SerialNumber.Value;
			}
		}

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x0600036B RID: 875 RVA: 0x00011EAC File Offset: 0x00010EAC
		public virtual X509Name IssuerDN
		{
			get
			{
				return this.c.Issuer;
			}
		}

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x0600036C RID: 876 RVA: 0x00011EB9 File Offset: 0x00010EB9
		public virtual X509Name SubjectDN
		{
			get
			{
				return this.c.Subject;
			}
		}

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x0600036D RID: 877 RVA: 0x00011EC6 File Offset: 0x00010EC6
		public virtual DateTime NotBefore
		{
			get
			{
				return this.c.StartDate.ToDateTime();
			}
		}

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x0600036E RID: 878 RVA: 0x00011ED8 File Offset: 0x00010ED8
		public virtual DateTime NotAfter
		{
			get
			{
				return this.c.EndDate.ToDateTime();
			}
		}

		// Token: 0x0600036F RID: 879 RVA: 0x00011EEA File Offset: 0x00010EEA
		public virtual byte[] GetTbsCertificate()
		{
			return this.c.TbsCertificate.GetDerEncoded();
		}

		// Token: 0x06000370 RID: 880 RVA: 0x00011EFC File Offset: 0x00010EFC
		public virtual byte[] GetSignature()
		{
			return this.c.Signature.GetBytes();
		}

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x06000371 RID: 881 RVA: 0x00011F0E File Offset: 0x00010F0E
		public virtual string SigAlgName
		{
			get
			{
				return SignerUtilities.GetEncodingName(this.c.SignatureAlgorithm.ObjectID);
			}
		}

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x06000372 RID: 882 RVA: 0x00011F25 File Offset: 0x00010F25
		public virtual string SigAlgOid
		{
			get
			{
				return this.c.SignatureAlgorithm.ObjectID.Id;
			}
		}

		// Token: 0x06000373 RID: 883 RVA: 0x00011F3C File Offset: 0x00010F3C
		public virtual byte[] GetSigAlgParams()
		{
			if (this.c.SignatureAlgorithm.Parameters != null)
			{
				return this.c.SignatureAlgorithm.Parameters.GetDerEncoded();
			}
			return null;
		}

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x06000374 RID: 884 RVA: 0x00011F67 File Offset: 0x00010F67
		public virtual DerBitString IssuerUniqueID
		{
			get
			{
				return this.c.TbsCertificate.IssuerUniqueID;
			}
		}

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x06000375 RID: 885 RVA: 0x00011F79 File Offset: 0x00010F79
		public virtual DerBitString SubjectUniqueID
		{
			get
			{
				return this.c.TbsCertificate.SubjectUniqueID;
			}
		}

		// Token: 0x06000376 RID: 886 RVA: 0x00011F8B File Offset: 0x00010F8B
		public virtual bool[] GetKeyUsage()
		{
			if (this.keyUsage != null)
			{
				return (bool[])this.keyUsage.Clone();
			}
			return null;
		}

		// Token: 0x06000377 RID: 887 RVA: 0x00011FA8 File Offset: 0x00010FA8
		public virtual IList GetExtendedKeyUsage()
		{
			Asn1OctetString extensionValue = this.GetExtensionValue(new DerObjectIdentifier("2.5.29.37"));
			if (extensionValue == null)
			{
				return null;
			}
			IList result;
			try
			{
				Asn1Sequence instance = Asn1Sequence.GetInstance(X509ExtensionUtilities.FromExtensionValue(extensionValue));
				ArrayList arrayList = new ArrayList();
				foreach (object obj in instance)
				{
					DerObjectIdentifier derObjectIdentifier = (DerObjectIdentifier)obj;
					arrayList.Add(derObjectIdentifier.Id);
				}
				result = arrayList;
			}
			catch (Exception exception)
			{
				throw new CertificateParsingException("error processing extended key usage extension", exception);
			}
			return result;
		}

		// Token: 0x06000378 RID: 888 RVA: 0x00012054 File Offset: 0x00011054
		public virtual int GetBasicConstraints()
		{
			if (this.basicConstraints == null || !this.basicConstraints.IsCA())
			{
				return -1;
			}
			if (this.basicConstraints.PathLenConstraint == null)
			{
				return int.MaxValue;
			}
			return this.basicConstraints.PathLenConstraint.IntValue;
		}

		// Token: 0x06000379 RID: 889 RVA: 0x00012090 File Offset: 0x00011090
		public virtual ICollection GetSubjectAlternativeNames()
		{
			return this.GetAlternativeNames("2.5.29.17");
		}

		// Token: 0x0600037A RID: 890 RVA: 0x0001209D File Offset: 0x0001109D
		public virtual ICollection GetIssuerAlternativeNames()
		{
			return this.GetAlternativeNames("2.5.29.18");
		}

		// Token: 0x0600037B RID: 891 RVA: 0x000120AC File Offset: 0x000110AC
		protected virtual ICollection GetAlternativeNames(string oid)
		{
			Asn1OctetString extensionValue = this.GetExtensionValue(new DerObjectIdentifier(oid));
			if (extensionValue == null)
			{
				return null;
			}
			Asn1Object obj = X509ExtensionUtilities.FromExtensionValue(extensionValue);
			GeneralNames instance = GeneralNames.GetInstance(obj);
			ArrayList arrayList = new ArrayList();
			foreach (GeneralName generalName in instance.GetNames())
			{
				arrayList.Add(new ArrayList
				{
					generalName.TagNo,
					generalName.Name.ToString()
				});
			}
			return arrayList;
		}

		// Token: 0x0600037C RID: 892 RVA: 0x0001213A File Offset: 0x0001113A
		protected override X509Extensions GetX509Extensions()
		{
			if (this.c.Version != 3)
			{
				return null;
			}
			return this.c.TbsCertificate.Extensions;
		}

		// Token: 0x0600037D RID: 893 RVA: 0x0001215C File Offset: 0x0001115C
		public virtual AsymmetricKeyParameter GetPublicKey()
		{
			return PublicKeyFactory.CreateKey(this.c.SubjectPublicKeyInfo);
		}

		// Token: 0x0600037E RID: 894 RVA: 0x0001216E File Offset: 0x0001116E
		public virtual byte[] GetEncoded()
		{
			return this.c.GetDerEncoded();
		}

		// Token: 0x0600037F RID: 895 RVA: 0x0001217C File Offset: 0x0001117C
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			X509Certificate x509Certificate = obj as X509Certificate;
			return x509Certificate != null && this.c.Equals(x509Certificate.c);
		}

		// Token: 0x06000380 RID: 896 RVA: 0x000121AC File Offset: 0x000111AC
		public override int GetHashCode()
		{
			lock (this)
			{
				if (!this.hashValueSet)
				{
					this.hashValue = this.c.GetHashCode();
					this.hashValueSet = true;
				}
			}
			return this.hashValue;
		}

		// Token: 0x06000381 RID: 897 RVA: 0x00012200 File Offset: 0x00011200
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			string newLine = Platform.NewLine;
			stringBuilder.Append("  [0]         Version: ").Append(this.Version).Append(newLine);
			stringBuilder.Append("         SerialNumber: ").Append(this.SerialNumber).Append(newLine);
			stringBuilder.Append("             IssuerDN: ").Append(this.IssuerDN).Append(newLine);
			stringBuilder.Append("           Start Date: ").Append(this.NotBefore).Append(newLine);
			stringBuilder.Append("           Final Date: ").Append(this.NotAfter).Append(newLine);
			stringBuilder.Append("            SubjectDN: ").Append(this.SubjectDN).Append(newLine);
			stringBuilder.Append("           Public Key: ").Append(this.GetPublicKey()).Append(newLine);
			stringBuilder.Append("  Signature Algorithm: ").Append(this.SigAlgName).Append(newLine);
			byte[] signature = this.GetSignature();
			stringBuilder.Append("            Signature: ").Append(Hex.ToHexString(signature, 0, 20)).Append(newLine);
			for (int i = 20; i < signature.Length; i += 20)
			{
				int length = Math.Min(20, signature.Length - i);
				stringBuilder.Append("                       ").Append(Hex.ToHexString(signature, i, length)).Append(newLine);
			}
			X509Extensions extensions = this.c.TbsCertificate.Extensions;
			if (extensions != null)
			{
				IEnumerator enumerator = extensions.ExtensionOids.GetEnumerator();
				if (enumerator.MoveNext())
				{
					stringBuilder.Append("       Extensions: \n");
				}
				do
				{
					DerObjectIdentifier derObjectIdentifier = (DerObjectIdentifier)enumerator.Current;
					X509Extension extension = extensions.GetExtension(derObjectIdentifier);
					if (extension.Value != null)
					{
						byte[] octets = extension.Value.GetOctets();
						Asn1Object asn1Object = Asn1Object.FromByteArray(octets);
						stringBuilder.Append("                       critical(").Append(extension.IsCritical).Append(") ");
						try
						{
							if (derObjectIdentifier.Equals(X509Extensions.BasicConstraints))
							{
								stringBuilder.Append(BasicConstraints.GetInstance(asn1Object));
							}
							else if (derObjectIdentifier.Equals(X509Extensions.KeyUsage))
							{
								stringBuilder.Append(KeyUsage.GetInstance(asn1Object));
							}
							else if (derObjectIdentifier.Equals(MiscObjectIdentifiers.NetscapeCertType))
							{
								stringBuilder.Append(new NetscapeCertType((DerBitString)asn1Object));
							}
							else if (derObjectIdentifier.Equals(MiscObjectIdentifiers.NetscapeRevocationUrl))
							{
								stringBuilder.Append(new NetscapeRevocationUrl((DerIA5String)asn1Object));
							}
							else if (derObjectIdentifier.Equals(MiscObjectIdentifiers.VerisignCzagExtension))
							{
								stringBuilder.Append(new VerisignCzagExtension((DerIA5String)asn1Object));
							}
							else
							{
								stringBuilder.Append(derObjectIdentifier.Id);
								stringBuilder.Append(" value = ").Append(Asn1Dump.DumpAsString(asn1Object));
							}
						}
						catch (Exception)
						{
							stringBuilder.Append(derObjectIdentifier.Id);
							stringBuilder.Append(" value = ").Append("*****");
						}
					}
					stringBuilder.Append(newLine);
				}
				while (enumerator.MoveNext());
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000382 RID: 898 RVA: 0x00012530 File Offset: 0x00011530
		public virtual void Verify(AsymmetricKeyParameter key)
		{
			string signatureName = X509SignatureUtilities.GetSignatureName(this.c.SignatureAlgorithm);
			ISigner signer = SignerUtilities.GetSigner(signatureName);
			this.CheckSignature(key, signer);
		}

		// Token: 0x06000383 RID: 899 RVA: 0x00012560 File Offset: 0x00011560
		protected virtual void CheckSignature(AsymmetricKeyParameter publicKey, ISigner signature)
		{
			if (!X509Certificate.IsAlgIDEqual(this.c.SignatureAlgorithm, this.c.TbsCertificate.Signature))
			{
				throw new CertificateException("signature algorithm in TBS cert not same as outer cert");
			}
			Asn1Encodable parameters = this.c.SignatureAlgorithm.Parameters;
			X509SignatureUtilities.SetSignatureParameters(signature, parameters);
			signature.Init(false, publicKey);
			byte[] tbsCertificate = this.GetTbsCertificate();
			signature.BlockUpdate(tbsCertificate, 0, tbsCertificate.Length);
			byte[] signature2 = this.GetSignature();
			if (!signature.VerifySignature(signature2))
			{
				throw new InvalidKeyException("Public key presented not for certificate signature");
			}
		}

		// Token: 0x06000384 RID: 900 RVA: 0x000125E8 File Offset: 0x000115E8
		private static bool IsAlgIDEqual(AlgorithmIdentifier id1, AlgorithmIdentifier id2)
		{
			if (!id1.ObjectID.Equals(id2.ObjectID))
			{
				return false;
			}
			Asn1Encodable parameters = id1.Parameters;
			Asn1Encodable parameters2 = id2.Parameters;
			if (parameters == null == (parameters2 == null))
			{
				return object.Equals(parameters, parameters2);
			}
			if (parameters != null)
			{
				return parameters.ToAsn1Object() is Asn1Null;
			}
			return parameters2.ToAsn1Object() is Asn1Null;
		}

		// Token: 0x040001C4 RID: 452
		private readonly X509CertificateStructure c;

		// Token: 0x040001C5 RID: 453
		private readonly BasicConstraints basicConstraints;

		// Token: 0x040001C6 RID: 454
		private readonly bool[] keyUsage;

		// Token: 0x040001C7 RID: 455
		private bool hashValueSet;

		// Token: 0x040001C8 RID: 456
		private int hashValue;
	}
}
