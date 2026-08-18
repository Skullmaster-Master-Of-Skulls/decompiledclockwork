using System;
using System.IO;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.Cms;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.IO;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Security;

namespace Org.BouncyCastle.Cms
{
	// Token: 0x02000137 RID: 311
	public class CmsEnvelopedDataGenerator : CmsEnvelopedGenerator
	{
		// Token: 0x06000B61 RID: 2913 RVA: 0x00040094 File Offset: 0x0003F094
		public CmsEnvelopedDataGenerator()
		{
		}

		// Token: 0x06000B62 RID: 2914 RVA: 0x0004009C File Offset: 0x0003F09C
		public CmsEnvelopedDataGenerator(SecureRandom rand) : base(rand)
		{
		}

		// Token: 0x06000B63 RID: 2915 RVA: 0x000400A8 File Offset: 0x0003F0A8
		private CmsEnvelopedData Generate(CmsProcessable content, string encryptionOid, CipherKeyGenerator keyGen)
		{
			AlgorithmIdentifier contentEncryptionAlgorithm = null;
			KeyParameter keyParameter;
			Asn1OctetString encryptedContent;
			try
			{
				byte[] array = keyGen.GenerateKey();
				keyParameter = ParameterUtilities.CreateKeyParameter(encryptionOid, array);
				Asn1Encodable asn1Params = this.GenerateAsn1Parameters(encryptionOid, array);
				ICipherParameters parameters;
				contentEncryptionAlgorithm = this.GetAlgorithmIdentifier(encryptionOid, keyParameter, asn1Params, out parameters);
				IBufferedCipher cipher = CipherUtilities.GetCipher(encryptionOid);
				cipher.Init(true, new ParametersWithRandom(parameters, this.rand));
				MemoryStream memoryStream = new MemoryStream();
				CipherStream cipherStream = new CipherStream(memoryStream, null, cipher);
				content.Write(cipherStream);
				cipherStream.Close();
				encryptedContent = new BerOctetString(memoryStream.ToArray());
			}
			catch (SecurityUtilityException e)
			{
				throw new CmsException("couldn't create cipher.", e);
			}
			catch (InvalidKeyException e2)
			{
				throw new CmsException("key invalid in message.", e2);
			}
			catch (IOException e3)
			{
				throw new CmsException("exception decoding algorithm parameters.", e3);
			}
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(new Asn1Encodable[0]);
			foreach (object obj in this.recipientInfoGenerators)
			{
				RecipientInfoGenerator recipientInfoGenerator = (RecipientInfoGenerator)obj;
				try
				{
					asn1EncodableVector.Add(new Asn1Encodable[]
					{
						recipientInfoGenerator.Generate(keyParameter, this.rand)
					});
				}
				catch (InvalidKeyException e4)
				{
					throw new CmsException("key inappropriate for algorithm.", e4);
				}
				catch (GeneralSecurityException e5)
				{
					throw new CmsException("error making encrypted content.", e5);
				}
			}
			EncryptedContentInfo encryptedContentInfo = new EncryptedContentInfo(CmsObjectIdentifiers.Data, contentEncryptionAlgorithm, encryptedContent);
			ContentInfo contentInfo = new ContentInfo(CmsObjectIdentifiers.EnvelopedData, new EnvelopedData(null, new DerSet(asn1EncodableVector), encryptedContentInfo, null));
			return new CmsEnvelopedData(contentInfo);
		}

		// Token: 0x06000B64 RID: 2916 RVA: 0x00040264 File Offset: 0x0003F264
		public CmsEnvelopedData Generate(CmsProcessable content, string encryptionOid)
		{
			CmsEnvelopedData result;
			try
			{
				CipherKeyGenerator keyGenerator = GeneratorUtilities.GetKeyGenerator(encryptionOid);
				keyGenerator.Init(new KeyGenerationParameters(this.rand, keyGenerator.DefaultStrength));
				result = this.Generate(content, encryptionOid, keyGenerator);
			}
			catch (SecurityUtilityException e)
			{
				throw new CmsException("can't find key generation algorithm.", e);
			}
			return result;
		}

		// Token: 0x06000B65 RID: 2917 RVA: 0x000402BC File Offset: 0x0003F2BC
		public CmsEnvelopedData Generate(CmsProcessable content, string encryptionOid, int keySize)
		{
			CmsEnvelopedData result;
			try
			{
				CipherKeyGenerator keyGenerator = GeneratorUtilities.GetKeyGenerator(encryptionOid);
				keyGenerator.Init(new KeyGenerationParameters(this.rand, keySize));
				result = this.Generate(content, encryptionOid, keyGenerator);
			}
			catch (SecurityUtilityException e)
			{
				throw new CmsException("can't find key generation algorithm.", e);
			}
			return result;
		}
	}
}
