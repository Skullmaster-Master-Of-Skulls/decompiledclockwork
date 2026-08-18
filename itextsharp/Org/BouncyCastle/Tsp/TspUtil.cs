using System;
using System.Collections;
using System.IO;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.Cms;
using Org.BouncyCastle.Asn1.CryptoPro;
using Org.BouncyCastle.Asn1.Nist;
using Org.BouncyCastle.Asn1.Oiw;
using Org.BouncyCastle.Asn1.Pkcs;
using Org.BouncyCastle.Asn1.TeleTrust;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Cms;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Utilities;
using Org.BouncyCastle.X509;

namespace Org.BouncyCastle.Tsp
{
	// Token: 0x0200017B RID: 379
	public class TspUtil
	{
		// Token: 0x06000ED0 RID: 3792 RVA: 0x000561BC File Offset: 0x000551BC
		static TspUtil()
		{
			TspUtil.digestLengths.Add(PkcsObjectIdentifiers.MD5.Id, 16);
			TspUtil.digestLengths.Add(OiwObjectIdentifiers.IdSha1.Id, 20);
			TspUtil.digestLengths.Add(NistObjectIdentifiers.IdSha224.Id, 28);
			TspUtil.digestLengths.Add(NistObjectIdentifiers.IdSha256.Id, 32);
			TspUtil.digestLengths.Add(NistObjectIdentifiers.IdSha384.Id, 48);
			TspUtil.digestLengths.Add(NistObjectIdentifiers.IdSha512.Id, 64);
			TspUtil.digestNames.Add(PkcsObjectIdentifiers.MD5.Id, "MD5");
			TspUtil.digestNames.Add(OiwObjectIdentifiers.IdSha1.Id, "SHA1");
			TspUtil.digestNames.Add(NistObjectIdentifiers.IdSha224.Id, "SHA224");
			TspUtil.digestNames.Add(NistObjectIdentifiers.IdSha256.Id, "SHA256");
			TspUtil.digestNames.Add(NistObjectIdentifiers.IdSha384.Id, "SHA384");
			TspUtil.digestNames.Add(NistObjectIdentifiers.IdSha512.Id, "SHA512");
			TspUtil.digestNames.Add(PkcsObjectIdentifiers.Sha1WithRsaEncryption.Id, "SHA1");
			TspUtil.digestNames.Add(PkcsObjectIdentifiers.Sha224WithRsaEncryption.Id, "SHA224");
			TspUtil.digestNames.Add(PkcsObjectIdentifiers.Sha256WithRsaEncryption.Id, "SHA256");
			TspUtil.digestNames.Add(PkcsObjectIdentifiers.Sha384WithRsaEncryption.Id, "SHA384");
			TspUtil.digestNames.Add(PkcsObjectIdentifiers.Sha512WithRsaEncryption.Id, "SHA512");
			TspUtil.digestNames.Add(TeleTrusTObjectIdentifiers.RipeMD128.Id, "RIPEMD128");
			TspUtil.digestNames.Add(TeleTrusTObjectIdentifiers.RipeMD160.Id, "RIPEMD160");
			TspUtil.digestNames.Add(TeleTrusTObjectIdentifiers.RipeMD256.Id, "RIPEMD256");
			TspUtil.digestNames.Add(CryptoProObjectIdentifiers.GostR3411.Id, "GOST3411");
		}

		// Token: 0x06000ED1 RID: 3793 RVA: 0x000563F8 File Offset: 0x000553F8
		public static ICollection GetSignatureTimestamps(SignerInformation signerInfo)
		{
			IList list = new ArrayList();
			Org.BouncyCastle.Asn1.Cms.AttributeTable unsignedAttributes = signerInfo.UnsignedAttributes;
			if (unsignedAttributes != null)
			{
				foreach (object obj in unsignedAttributes.GetAll(PkcsObjectIdentifiers.IdAASignatureTimeStampToken))
				{
					Org.BouncyCastle.Asn1.Cms.Attribute attribute = (Org.BouncyCastle.Asn1.Cms.Attribute)obj;
					foreach (object obj2 in attribute.AttrValues)
					{
						Asn1Encodable asn1Encodable = (Asn1Encodable)obj2;
						try
						{
							Org.BouncyCastle.Asn1.Cms.ContentInfo instance = Org.BouncyCastle.Asn1.Cms.ContentInfo.GetInstance(asn1Encodable.ToAsn1Object());
							TimeStampToken timeStampToken = new TimeStampToken(instance);
							TimeStampTokenInfo timeStampInfo = timeStampToken.TimeStampInfo;
							byte[] a = DigestUtilities.CalculateDigest(TspUtil.GetDigestAlgName(timeStampInfo.MessageImprintAlgOid), signerInfo.GetSignature());
							if (!Arrays.ConstantTimeAreEqual(a, timeStampInfo.GetMessageImprintDigest()))
							{
								throw new TspValidationException("Incorrect digest in message imprint");
							}
							list.Add(timeStampToken);
						}
						catch (SecurityUtilityException)
						{
							throw new TspValidationException("Unknown hash algorithm specified in timestamp");
						}
						catch (Exception)
						{
							throw new TspValidationException("Timestamp could not be parsed");
						}
					}
				}
			}
			return list;
		}

		// Token: 0x06000ED2 RID: 3794 RVA: 0x0005654C File Offset: 0x0005554C
		public static void ValidateCertificate(X509Certificate cert)
		{
			if (cert.Version != 3)
			{
				throw new ArgumentException("Certificate must have an ExtendedKeyUsage extension.");
			}
			Asn1OctetString extensionValue = cert.GetExtensionValue(X509Extensions.ExtendedKeyUsage);
			if (extensionValue == null)
			{
				throw new TspValidationException("Certificate must have an ExtendedKeyUsage extension.");
			}
			if (!cert.GetCriticalExtensionOids().Contains(X509Extensions.ExtendedKeyUsage.Id))
			{
				throw new TspValidationException("Certificate must have an ExtendedKeyUsage extension marked as critical.");
			}
			try
			{
				ExtendedKeyUsage instance = ExtendedKeyUsage.GetInstance(Asn1Object.FromByteArray(extensionValue.GetOctets()));
				if (!instance.HasKeyPurposeId(KeyPurposeID.IdKPTimeStamping) || instance.Count != 1)
				{
					throw new TspValidationException("ExtendedKeyUsage not solely time stamping.");
				}
			}
			catch (IOException)
			{
				throw new TspValidationException("cannot process ExtendedKeyUsage extension");
			}
		}

		// Token: 0x06000ED3 RID: 3795 RVA: 0x000565FC File Offset: 0x000555FC
		internal static string GetDigestAlgName(string digestAlgOID)
		{
			string text = (string)TspUtil.digestNames[digestAlgOID];
			if (text == null)
			{
				return digestAlgOID;
			}
			return text;
		}

		// Token: 0x06000ED4 RID: 3796 RVA: 0x00056620 File Offset: 0x00055620
		internal static int GetDigestLength(string digestAlgOID)
		{
			int result;
			try
			{
				if (TspUtil.digestLengths.Contains(digestAlgOID))
				{
					result = (int)TspUtil.digestLengths[digestAlgOID];
				}
				else
				{
					result = TspUtil.CreateDigestInstance(digestAlgOID).GetDigestSize();
				}
			}
			catch (SecurityUtilityException e)
			{
				throw new TspException("digest algorithm cannot be found.", e);
			}
			return result;
		}

		// Token: 0x06000ED5 RID: 3797 RVA: 0x0005667C File Offset: 0x0005567C
		internal static IDigest CreateDigestInstance(string digestAlgOID)
		{
			string digestAlgName = TspUtil.GetDigestAlgName(digestAlgOID);
			return DigestUtilities.GetDigest(digestAlgName);
		}

		// Token: 0x04000B05 RID: 2821
		private static readonly IDictionary digestLengths = new Hashtable();

		// Token: 0x04000B06 RID: 2822
		private static readonly IDictionary digestNames = new Hashtable();
	}
}
