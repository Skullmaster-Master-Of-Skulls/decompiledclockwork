using System;
using System.Collections;
using System.IO;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.Cms;
using Org.BouncyCastle.Asn1.Cms.Ecc;
using Org.BouncyCastle.Asn1.Pkcs;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Asn1.X9;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Pkcs;
using Org.BouncyCastle.Security;

namespace Org.BouncyCastle.Cms
{
	// Token: 0x020002AE RID: 686
	public class KeyAgreeRecipientInformation : RecipientInformation
	{
		// Token: 0x060019F0 RID: 6640 RVA: 0x0009A1F0 File Offset: 0x000991F0
		internal static void ReadRecipientInfo(IList infos, KeyAgreeRecipientInfo info, AlgorithmIdentifier encAlg, AlgorithmIdentifier macAlg, AlgorithmIdentifier authEncAlg, Stream data)
		{
			try
			{
				foreach (object obj in info.RecipientEncryptedKeys)
				{
					Asn1Encodable asn1Encodable = (Asn1Encodable)obj;
					RecipientEncryptedKey instance = RecipientEncryptedKey.GetInstance(asn1Encodable.ToAsn1Object());
					RecipientID recipientID = new RecipientID();
					KeyAgreeRecipientIdentifier identifier = instance.Identifier;
					Org.BouncyCastle.Asn1.Cms.IssuerAndSerialNumber issuerAndSerialNumber = identifier.IssuerAndSerialNumber;
					if (issuerAndSerialNumber != null)
					{
						recipientID.Issuer = issuerAndSerialNumber.Name;
						recipientID.SerialNumber = issuerAndSerialNumber.SerialNumber.Value;
					}
					else
					{
						RecipientKeyIdentifier rkeyID = identifier.RKeyID;
						recipientID.SubjectKeyIdentifier = rkeyID.SubjectKeyIdentifier.GetOctets();
					}
					infos.Add(new KeyAgreeRecipientInformation(info, recipientID, instance.EncryptedKey, encAlg, macAlg, authEncAlg, data));
				}
			}
			catch (IOException innerException)
			{
				throw new ArgumentException("invalid rid in KeyAgreeRecipientInformation", innerException);
			}
		}

		// Token: 0x060019F1 RID: 6641 RVA: 0x0009A2E8 File Offset: 0x000992E8
		public KeyAgreeRecipientInformation(KeyAgreeRecipientInfo info, RecipientID rid, Asn1OctetString encryptedKey, AlgorithmIdentifier encAlg, AlgorithmIdentifier macAlg, AlgorithmIdentifier authEncAlg, Stream data) : base(encAlg, macAlg, authEncAlg, info.KeyEncryptionAlgorithm, data)
		{
			this.info = info;
			this.rid = rid;
			this.encryptedKey = encryptedKey;
		}

		// Token: 0x060019F2 RID: 6642 RVA: 0x0009A314 File Offset: 0x00099314
		private AsymmetricKeyParameter GetSenderPublicKey(AsymmetricKeyParameter receiverPrivateKey, OriginatorIdentifierOrKey originator)
		{
			OriginatorPublicKey originatorPublicKey = originator.OriginatorPublicKey;
			if (originatorPublicKey != null)
			{
				return this.GetPublicKeyFromOriginatorPublicKey(receiverPrivateKey, originatorPublicKey);
			}
			OriginatorID originatorID = new OriginatorID();
			Org.BouncyCastle.Asn1.Cms.IssuerAndSerialNumber issuerAndSerialNumber = originator.IssuerAndSerialNumber;
			if (issuerAndSerialNumber != null)
			{
				originatorID.Issuer = issuerAndSerialNumber.Name;
				originatorID.SerialNumber = issuerAndSerialNumber.SerialNumber.Value;
			}
			else
			{
				SubjectKeyIdentifier subjectKeyIdentifier = originator.SubjectKeyIdentifier;
				originatorID.SubjectKeyIdentifier = subjectKeyIdentifier.GetKeyIdentifier();
			}
			return this.GetPublicKeyFromOriginatorID(originatorID);
		}

		// Token: 0x060019F3 RID: 6643 RVA: 0x0009A380 File Offset: 0x00099380
		private AsymmetricKeyParameter GetPublicKeyFromOriginatorPublicKey(AsymmetricKeyParameter receiverPrivateKey, OriginatorPublicKey originatorPublicKey)
		{
			PrivateKeyInfo privateKeyInfo = PrivateKeyInfoFactory.CreatePrivateKeyInfo(receiverPrivateKey);
			SubjectPublicKeyInfo keyInfo = new SubjectPublicKeyInfo(privateKeyInfo.AlgorithmID, originatorPublicKey.PublicKey.GetBytes());
			return PublicKeyFactory.CreateKey(keyInfo);
		}

		// Token: 0x060019F4 RID: 6644 RVA: 0x0009A3B1 File Offset: 0x000993B1
		private AsymmetricKeyParameter GetPublicKeyFromOriginatorID(OriginatorID origID)
		{
			throw new CmsException("No support for 'originator' as IssuerAndSerialNumber or SubjectKeyIdentifier");
		}

		// Token: 0x060019F5 RID: 6645 RVA: 0x0009A3C0 File Offset: 0x000993C0
		private KeyParameter CalculateAgreedWrapKey(string wrapAlg, AsymmetricKeyParameter senderPublicKey, AsymmetricKeyParameter receiverPrivateKey)
		{
			DerObjectIdentifier objectID = this.keyEncAlg.ObjectID;
			ICipherParameters cipherParameters = senderPublicKey;
			ICipherParameters cipherParameters2 = receiverPrivateKey;
			if (objectID.Id.Equals(CmsEnvelopedGenerator.ECMqvSha1Kdf))
			{
				byte[] octets = this.info.UserKeyingMaterial.GetOctets();
				MQVuserKeyingMaterial instance = MQVuserKeyingMaterial.GetInstance(Asn1Object.FromByteArray(octets));
				AsymmetricKeyParameter publicKeyFromOriginatorPublicKey = this.GetPublicKeyFromOriginatorPublicKey(receiverPrivateKey, instance.EphemeralPublicKey);
				cipherParameters = new MqvPublicParameters((ECPublicKeyParameters)cipherParameters, (ECPublicKeyParameters)publicKeyFromOriginatorPublicKey);
				cipherParameters2 = new MqvPrivateParameters((ECPrivateKeyParameters)cipherParameters2, (ECPrivateKeyParameters)cipherParameters2);
			}
			IBasicAgreement basicAgreementWithKdf = AgreementUtilities.GetBasicAgreementWithKdf(objectID, wrapAlg);
			basicAgreementWithKdf.Init(cipherParameters2);
			BigInteger s = basicAgreementWithKdf.CalculateAgreement(cipherParameters);
			int qLength = GeneratorUtilities.GetDefaultKeySize(wrapAlg) / 8;
			byte[] keyBytes = X9IntegerConverter.IntegerToBytes(s, qLength);
			return ParameterUtilities.CreateKeyParameter(wrapAlg, keyBytes);
		}

		// Token: 0x060019F6 RID: 6646 RVA: 0x0009A47C File Offset: 0x0009947C
		private KeyParameter UnwrapSessionKey(string wrapAlg, KeyParameter agreedKey)
		{
			AlgorithmIdentifier activeAlgID = base.GetActiveAlgID();
			string id = activeAlgID.ObjectID.Id;
			byte[] octets = this.encryptedKey.GetOctets();
			IWrapper wrapper = WrapperUtilities.GetWrapper(wrapAlg);
			wrapper.Init(false, agreedKey);
			byte[] keyBytes = wrapper.Unwrap(octets, 0, octets.Length);
			return ParameterUtilities.CreateKeyParameter(id, keyBytes);
		}

		// Token: 0x060019F7 RID: 6647 RVA: 0x0009A4CC File Offset: 0x000994CC
		internal KeyParameter GetSessionKey(AsymmetricKeyParameter receiverPrivateKey)
		{
			KeyParameter result;
			try
			{
				string id = DerObjectIdentifier.GetInstance(Asn1Sequence.GetInstance(this.keyEncAlg.Parameters)[0]).Id;
				AsymmetricKeyParameter senderPublicKey = this.GetSenderPublicKey(receiverPrivateKey, this.info.Originator);
				KeyParameter agreedKey = this.CalculateAgreedWrapKey(id, senderPublicKey, receiverPrivateKey);
				result = this.UnwrapSessionKey(id, agreedKey);
			}
			catch (SecurityUtilityException e)
			{
				throw new CmsException("couldn't create cipher.", e);
			}
			catch (InvalidKeyException e2)
			{
				throw new CmsException("key invalid in message.", e2);
			}
			catch (Exception e3)
			{
				throw new CmsException("originator key invalid.", e3);
			}
			return result;
		}

		// Token: 0x060019F8 RID: 6648 RVA: 0x0009A578 File Offset: 0x00099578
		public override CmsTypedStream GetContentStream(ICipherParameters key)
		{
			if (!(key is AsymmetricKeyParameter))
			{
				throw new ArgumentException("KeyAgreement requires asymmetric key", "key");
			}
			AsymmetricKeyParameter asymmetricKeyParameter = (AsymmetricKeyParameter)key;
			if (!asymmetricKeyParameter.IsPrivate)
			{
				throw new ArgumentException("Expected private key", "key");
			}
			KeyParameter sessionKey = this.GetSessionKey(asymmetricKeyParameter);
			return base.GetContentFromSessionKey(sessionKey);
		}

		// Token: 0x0400114E RID: 4430
		private KeyAgreeRecipientInfo info;

		// Token: 0x0400114F RID: 4431
		private Asn1OctetString encryptedKey;
	}
}
