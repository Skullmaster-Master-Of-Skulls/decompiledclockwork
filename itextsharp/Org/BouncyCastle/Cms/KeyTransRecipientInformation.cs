using System;
using System.IO;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.Cms;
using Org.BouncyCastle.Asn1.Pkcs;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Security;

namespace Org.BouncyCastle.Cms
{
	// Token: 0x02000436 RID: 1078
	public class KeyTransRecipientInformation : RecipientInformation
	{
		// Token: 0x060024A5 RID: 9381 RVA: 0x000DF10F File Offset: 0x000DE10F
		[Obsolete]
		public KeyTransRecipientInformation(KeyTransRecipientInfo info, AlgorithmIdentifier encAlg, Stream data) : this(info, encAlg, null, null, data)
		{
		}

		// Token: 0x060024A6 RID: 9382 RVA: 0x000DF11C File Offset: 0x000DE11C
		[Obsolete]
		public KeyTransRecipientInformation(KeyTransRecipientInfo info, AlgorithmIdentifier encAlg, AlgorithmIdentifier macAlg, Stream data) : this(info, encAlg, macAlg, null, data)
		{
		}

		// Token: 0x060024A7 RID: 9383 RVA: 0x000DF12C File Offset: 0x000DE12C
		public KeyTransRecipientInformation(KeyTransRecipientInfo info, AlgorithmIdentifier encAlg, AlgorithmIdentifier macAlg, AlgorithmIdentifier authEncAlg, Stream data) : base(encAlg, macAlg, authEncAlg, info.KeyEncryptionAlgorithm, data)
		{
			this.info = info;
			this.rid = new RecipientID();
			RecipientIdentifier recipientIdentifier = info.RecipientIdentifier;
			try
			{
				if (recipientIdentifier.IsTagged)
				{
					Asn1OctetString instance = Asn1OctetString.GetInstance(recipientIdentifier.ID);
					this.rid.SubjectKeyIdentifier = instance.GetOctets();
				}
				else
				{
					Org.BouncyCastle.Asn1.Cms.IssuerAndSerialNumber instance2 = Org.BouncyCastle.Asn1.Cms.IssuerAndSerialNumber.GetInstance(recipientIdentifier.ID);
					this.rid.Issuer = instance2.Name;
					this.rid.SerialNumber = instance2.SerialNumber.Value;
				}
			}
			catch (IOException)
			{
				throw new ArgumentException("invalid rid in KeyTransRecipientInformation");
			}
		}

		// Token: 0x060024A8 RID: 9384 RVA: 0x000DF1DC File Offset: 0x000DE1DC
		private string GetExchangeEncryptionAlgorithmName(DerObjectIdentifier oid)
		{
			if (PkcsObjectIdentifiers.RsaEncryption.Equals(oid))
			{
				return "RSA//PKCS1Padding";
			}
			return oid.Id;
		}

		// Token: 0x060024A9 RID: 9385 RVA: 0x000DF1F8 File Offset: 0x000DE1F8
		internal KeyParameter UnwrapKey(ICipherParameters key)
		{
			byte[] octets = this.info.EncryptedKey.GetOctets();
			string exchangeEncryptionAlgorithmName = this.GetExchangeEncryptionAlgorithmName(this.keyEncAlg.ObjectID);
			KeyParameter result;
			try
			{
				IWrapper wrapper = WrapperUtilities.GetWrapper(exchangeEncryptionAlgorithmName);
				wrapper.Init(false, key);
				AlgorithmIdentifier activeAlgID = base.GetActiveAlgID();
				result = ParameterUtilities.CreateKeyParameter(activeAlgID.ObjectID, wrapper.Unwrap(octets, 0, octets.Length));
			}
			catch (SecurityUtilityException e)
			{
				throw new CmsException("couldn't create cipher.", e);
			}
			catch (InvalidKeyException e2)
			{
				throw new CmsException("key invalid in message.", e2);
			}
			catch (DataLengthException e3)
			{
				throw new CmsException("illegal blocksize in message.", e3);
			}
			catch (InvalidCipherTextException e4)
			{
				throw new CmsException("bad padding in message.", e4);
			}
			return result;
		}

		// Token: 0x060024AA RID: 9386 RVA: 0x000DF2CC File Offset: 0x000DE2CC
		public override CmsTypedStream GetContentStream(ICipherParameters key)
		{
			KeyParameter sKey = this.UnwrapKey(key);
			return base.GetContentFromSessionKey(sKey);
		}

		// Token: 0x04001998 RID: 6552
		private KeyTransRecipientInfo info;
	}
}
