using System;
using System.Collections;
using System.IO;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.Cms;
using Org.BouncyCastle.Asn1.Pkcs;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Security;

namespace Org.BouncyCastle.Cms
{
	// Token: 0x020003E2 RID: 994
	internal class CmsEnvelopedHelper
	{
		// Token: 0x06002292 RID: 8850 RVA: 0x000D6740 File Offset: 0x000D5740
		static CmsEnvelopedHelper()
		{
			CmsEnvelopedHelper.KeySizes.Add(CmsEnvelopedGenerator.DesEde3Cbc, 192);
			CmsEnvelopedHelper.KeySizes.Add(CmsEnvelopedGenerator.Aes128Cbc, 128);
			CmsEnvelopedHelper.KeySizes.Add(CmsEnvelopedGenerator.Aes192Cbc, 192);
			CmsEnvelopedHelper.KeySizes.Add(CmsEnvelopedGenerator.Aes256Cbc, 256);
			CmsEnvelopedHelper.BaseCipherNames.Add(CmsEnvelopedGenerator.DesEde3Cbc, "DESEDE");
			CmsEnvelopedHelper.BaseCipherNames.Add(CmsEnvelopedGenerator.Aes128Cbc, "AES");
			CmsEnvelopedHelper.BaseCipherNames.Add(CmsEnvelopedGenerator.Aes192Cbc, "AES");
			CmsEnvelopedHelper.BaseCipherNames.Add(CmsEnvelopedGenerator.Aes256Cbc, "AES");
		}

		// Token: 0x06002293 RID: 8851 RVA: 0x000D681F File Offset: 0x000D581F
		private string GetAsymmetricEncryptionAlgName(string encryptionAlgOid)
		{
			if (PkcsObjectIdentifiers.RsaEncryption.Id.Equals(encryptionAlgOid))
			{
				return "RSA/ECB/PKCS1Padding";
			}
			return encryptionAlgOid;
		}

		// Token: 0x06002294 RID: 8852 RVA: 0x000D683C File Offset: 0x000D583C
		internal IBufferedCipher CreateAsymmetricCipher(string encryptionOid)
		{
			IBufferedCipher cipher;
			try
			{
				cipher = CipherUtilities.GetCipher(encryptionOid);
			}
			catch (SecurityUtilityException)
			{
				cipher = CipherUtilities.GetCipher(this.GetAsymmetricEncryptionAlgName(encryptionOid));
			}
			return cipher;
		}

		// Token: 0x06002295 RID: 8853 RVA: 0x000D6874 File Offset: 0x000D5874
		internal IWrapper CreateWrapper(string encryptionOid)
		{
			IWrapper wrapper;
			try
			{
				wrapper = WrapperUtilities.GetWrapper(encryptionOid);
			}
			catch (SecurityUtilityException)
			{
				wrapper = WrapperUtilities.GetWrapper(this.GetAsymmetricEncryptionAlgName(encryptionOid));
			}
			return wrapper;
		}

		// Token: 0x06002296 RID: 8854 RVA: 0x000D68AC File Offset: 0x000D58AC
		internal string GetRfc3211WrapperName(string oid)
		{
			if (oid == null)
			{
				throw new ArgumentNullException("oid");
			}
			string text = (string)CmsEnvelopedHelper.BaseCipherNames[oid];
			if (text == null)
			{
				throw new ArgumentException("no name for " + oid, "oid");
			}
			return text + "RFC3211Wrap";
		}

		// Token: 0x06002297 RID: 8855 RVA: 0x000D68FC File Offset: 0x000D58FC
		internal int GetKeySize(string oid)
		{
			if (!CmsEnvelopedHelper.KeySizes.Contains(oid))
			{
				throw new ArgumentException("no keysize for " + oid, "oid");
			}
			return (int)CmsEnvelopedHelper.KeySizes[oid];
		}

		// Token: 0x06002298 RID: 8856 RVA: 0x000D6934 File Offset: 0x000D5934
		internal static IList ReadRecipientInfos(Asn1Set recipientInfos, byte[] contentOctets, AlgorithmIdentifier encAlg, AlgorithmIdentifier macAlg, AlgorithmIdentifier authEncAlg)
		{
			IList list = new ArrayList();
			foreach (object obj in recipientInfos)
			{
				Asn1Encodable o = (Asn1Encodable)obj;
				RecipientInfo instance = RecipientInfo.GetInstance(o);
				MemoryStream contentStream = new MemoryStream(contentOctets, false);
				CmsEnvelopedHelper.ReadRecipientInfo(list, instance, contentStream, encAlg, macAlg, authEncAlg);
			}
			return list;
		}

		// Token: 0x06002299 RID: 8857 RVA: 0x000D69AC File Offset: 0x000D59AC
		internal static IList ReadRecipientInfos(IEnumerable recipientInfoIter, Stream contentStream, AlgorithmIdentifier encAlg, AlgorithmIdentifier macAlg, AlgorithmIdentifier authEncAlg)
		{
			IList list = new ArrayList();
			foreach (object obj in recipientInfoIter)
			{
				RecipientInfo info = (RecipientInfo)obj;
				CmsEnvelopedHelper.ReadRecipientInfo(list, info, contentStream, encAlg, macAlg, authEncAlg);
			}
			return list;
		}

		// Token: 0x0600229A RID: 8858 RVA: 0x000D6A0C File Offset: 0x000D5A0C
		private static void ReadRecipientInfo(IList infos, RecipientInfo info, Stream contentStream, AlgorithmIdentifier encAlg, AlgorithmIdentifier macAlg, AlgorithmIdentifier authEncAlg)
		{
			Asn1Encodable info2 = info.Info;
			if (info2 is KeyTransRecipientInfo)
			{
				infos.Add(new KeyTransRecipientInformation((KeyTransRecipientInfo)info2, encAlg, macAlg, authEncAlg, contentStream));
				return;
			}
			if (info2 is KekRecipientInfo)
			{
				infos.Add(new KekRecipientInformation((KekRecipientInfo)info2, encAlg, macAlg, authEncAlg, contentStream));
				return;
			}
			if (info2 is KeyAgreeRecipientInfo)
			{
				KeyAgreeRecipientInformation.ReadRecipientInfo(infos, (KeyAgreeRecipientInfo)info2, encAlg, macAlg, authEncAlg, contentStream);
				return;
			}
			if (info2 is PasswordRecipientInfo)
			{
				infos.Add(new PasswordRecipientInformation((PasswordRecipientInfo)info2, encAlg, macAlg, authEncAlg, contentStream));
			}
		}

		// Token: 0x040017B4 RID: 6068
		internal static readonly CmsEnvelopedHelper Instance = new CmsEnvelopedHelper();

		// Token: 0x040017B5 RID: 6069
		private static readonly IDictionary KeySizes = new Hashtable();

		// Token: 0x040017B6 RID: 6070
		private static readonly IDictionary BaseCipherNames = new Hashtable();
	}
}
