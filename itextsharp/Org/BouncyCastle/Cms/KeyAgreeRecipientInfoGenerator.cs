using System;
using System.Collections;
using System.IO;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.Cms;
using Org.BouncyCastle.Asn1.Cms.Ecc;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Asn1.X9;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.X509;

namespace Org.BouncyCastle.Cms
{
	// Token: 0x0200061D RID: 1565
	internal class KeyAgreeRecipientInfoGenerator : RecipientInfoGenerator
	{
		// Token: 0x06003534 RID: 13620 RVA: 0x0014A531 File Offset: 0x00149531
		internal KeyAgreeRecipientInfoGenerator()
		{
		}

		// Token: 0x17000932 RID: 2354
		// (set) Token: 0x06003535 RID: 13621 RVA: 0x0014A539 File Offset: 0x00149539
		internal DerObjectIdentifier AlgorithmOid
		{
			set
			{
				this.algorithmOID = value;
			}
		}

		// Token: 0x17000933 RID: 2355
		// (set) Token: 0x06003536 RID: 13622 RVA: 0x0014A542 File Offset: 0x00149542
		internal ICollection RecipientCerts
		{
			set
			{
				this.recipientCerts = new ArrayList(value);
			}
		}

		// Token: 0x17000934 RID: 2356
		// (set) Token: 0x06003537 RID: 13623 RVA: 0x0014A550 File Offset: 0x00149550
		internal AsymmetricCipherKeyPair SenderKeyPair
		{
			set
			{
				this.senderKeyPair = value;
			}
		}

		// Token: 0x17000935 RID: 2357
		// (set) Token: 0x06003538 RID: 13624 RVA: 0x0014A559 File Offset: 0x00149559
		internal DerObjectIdentifier WrapAlgorithmOid
		{
			set
			{
				this.wrapAlgorithmOID = value;
			}
		}

		// Token: 0x06003539 RID: 13625 RVA: 0x0014A564 File Offset: 0x00149564
		public RecipientInfo Generate(KeyParameter contentEncryptionKey, SecureRandom random)
		{
			byte[] key = contentEncryptionKey.GetKey();
			string id = this.algorithmOID.Id;
			string id2 = this.wrapAlgorithmOID.Id;
			AsymmetricKeyParameter @public = this.senderKeyPair.Public;
			ICipherParameters cipherParameters = this.senderKeyPair.Private;
			OriginatorIdentifierOrKey originator;
			try
			{
				originator = new OriginatorIdentifierOrKey(KeyAgreeRecipientInfoGenerator.CreateOriginatorPublicKey(@public));
			}
			catch (IOException arg)
			{
				throw new InvalidKeyException("cannot extract originator public key: " + arg);
			}
			Asn1OctetString ukm = null;
			if (id.Equals(CmsEnvelopedGenerator.ECMqvSha1Kdf))
			{
				try
				{
					IAsymmetricCipherKeyPairGenerator keyPairGenerator = GeneratorUtilities.GetKeyPairGenerator(id);
					keyPairGenerator.Init(((ECPublicKeyParameters)@public).CreateKeyGenerationParameters(random));
					AsymmetricCipherKeyPair asymmetricCipherKeyPair = keyPairGenerator.GenerateKeyPair();
					ukm = new DerOctetString(new MQVuserKeyingMaterial(KeyAgreeRecipientInfoGenerator.CreateOriginatorPublicKey(asymmetricCipherKeyPair.Public), null));
					cipherParameters = new MqvPrivateParameters((ECPrivateKeyParameters)cipherParameters, (ECPrivateKeyParameters)asymmetricCipherKeyPair.Private, (ECPublicKeyParameters)asymmetricCipherKeyPair.Public);
				}
				catch (IOException arg2)
				{
					throw new InvalidKeyException("cannot extract MQV ephemeral public key: " + arg2);
				}
				catch (SecurityUtilityException arg3)
				{
					throw new InvalidKeyException("cannot determine MQV ephemeral key pair parameters from public key: " + arg3);
				}
			}
			DerSequence parameters = new DerSequence(new Asn1Encodable[]
			{
				this.wrapAlgorithmOID,
				DerNull.Instance
			});
			AlgorithmIdentifier keyEncryptionAlgorithm = new AlgorithmIdentifier(this.algorithmOID, parameters);
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(new Asn1Encodable[0]);
			foreach (object obj in this.recipientCerts)
			{
				X509Certificate x509Certificate = (X509Certificate)obj;
				TbsCertificateStructure instance;
				try
				{
					instance = TbsCertificateStructure.GetInstance(Asn1Object.FromByteArray(x509Certificate.GetTbsCertificate()));
				}
				catch (Exception)
				{
					throw new ArgumentException("can't extract TBS structure from certificate");
				}
				IssuerAndSerialNumber issuerSerial = new IssuerAndSerialNumber(instance.Issuer, instance.SerialNumber.Value);
				KeyAgreeRecipientIdentifier id3 = new KeyAgreeRecipientIdentifier(issuerSerial);
				ICipherParameters cipherParameters2 = x509Certificate.GetPublicKey();
				if (id.Equals(CmsEnvelopedGenerator.ECMqvSha1Kdf))
				{
					cipherParameters2 = new MqvPublicParameters((ECPublicKeyParameters)cipherParameters2, (ECPublicKeyParameters)cipherParameters2);
				}
				IBasicAgreement basicAgreementWithKdf = AgreementUtilities.GetBasicAgreementWithKdf(id, id2);
				basicAgreementWithKdf.Init(new ParametersWithRandom(cipherParameters, random));
				BigInteger s = basicAgreementWithKdf.CalculateAgreement(cipherParameters2);
				int qLength = GeneratorUtilities.GetDefaultKeySize(id2) / 8;
				byte[] keyBytes = X9IntegerConverter.IntegerToBytes(s, qLength);
				KeyParameter parameters2 = ParameterUtilities.CreateKeyParameter(id2, keyBytes);
				IWrapper wrapper = KeyAgreeRecipientInfoGenerator.Helper.CreateWrapper(id2);
				wrapper.Init(true, new ParametersWithRandom(parameters2, random));
				byte[] str = wrapper.Wrap(key, 0, key.Length);
				Asn1OctetString encryptedKey = new DerOctetString(str);
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					new RecipientEncryptedKey(id3, encryptedKey)
				});
			}
			return new RecipientInfo(new KeyAgreeRecipientInfo(originator, ukm, keyEncryptionAlgorithm, new DerSequence(asn1EncodableVector)));
		}

		// Token: 0x0600353A RID: 13626 RVA: 0x0014A888 File Offset: 0x00149888
		private static OriginatorPublicKey CreateOriginatorPublicKey(AsymmetricKeyParameter publicKey)
		{
			SubjectPublicKeyInfo subjectPublicKeyInfo = SubjectPublicKeyInfoFactory.CreateSubjectPublicKeyInfo(publicKey);
			return new OriginatorPublicKey(new AlgorithmIdentifier(subjectPublicKeyInfo.AlgorithmID.ObjectID, DerNull.Instance), subjectPublicKeyInfo.PublicKeyData.GetBytes());
		}

		// Token: 0x04002393 RID: 9107
		private static readonly CmsEnvelopedHelper Helper = CmsEnvelopedHelper.Instance;

		// Token: 0x04002394 RID: 9108
		private DerObjectIdentifier algorithmOID;

		// Token: 0x04002395 RID: 9109
		private AsymmetricCipherKeyPair senderKeyPair;

		// Token: 0x04002396 RID: 9110
		private ArrayList recipientCerts;

		// Token: 0x04002397 RID: 9111
		private DerObjectIdentifier wrapAlgorithmOID;
	}
}
