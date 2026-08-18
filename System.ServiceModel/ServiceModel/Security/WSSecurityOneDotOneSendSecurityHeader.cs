using System;
using System.IdentityModel;
using System.IdentityModel.Tokens;
using System.IO;
using System.Security.Cryptography;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;

namespace System.ServiceModel.Security
{
	// Token: 0x0200028E RID: 654
	internal sealed class WSSecurityOneDotOneSendSecurityHeader : WSSecurityOneDotZeroSendSecurityHeader
	{
		// Token: 0x06001302 RID: 4866 RVA: 0x00044730 File Offset: 0x00042930
		public WSSecurityOneDotOneSendSecurityHeader(Message message, string actor, bool mustUnderstand, bool relay, SecurityStandardsManager standardsManager, SecurityAlgorithmSuite algorithmSuite, MessageDirection direction) : base(message, actor, mustUnderstand, relay, standardsManager, algorithmSuite, direction)
		{
		}

		// Token: 0x06001303 RID: 4867 RVA: 0x00044744 File Offset: 0x00042944
		protected override ISignatureValueSecurityElement[] CreateSignatureConfirmationElements(SignatureConfirmations signatureConfirmations)
		{
			if (signatureConfirmations == null || signatureConfirmations.Count == 0)
			{
				return null;
			}
			ISignatureValueSecurityElement[] array = new ISignatureValueSecurityElement[signatureConfirmations.Count];
			for (int i = 0; i < signatureConfirmations.Count; i++)
			{
				byte[] signatureValue;
				bool flag;
				signatureConfirmations.GetConfirmation(i, out signatureValue, out flag);
				array[i] = new SignatureConfirmationElement(base.GenerateId(), signatureValue, base.StandardsManager.SecurityVersion);
			}
			return array;
		}

		// Token: 0x06001304 RID: 4868 RVA: 0x000447A4 File Offset: 0x000429A4
		protected override EncryptedHeader EncryptHeader(MessageHeader plainTextHeader, SymmetricAlgorithm algorithm, SecurityKeyIdentifier keyIdentifier, MessageVersion version, string id, MemoryStream stream)
		{
			EncryptedHeaderXml encryptedHeaderXml = new EncryptedHeaderXml(version, false);
			encryptedHeaderXml.SecurityTokenSerializer = base.StandardsManager.SecurityTokenSerializer;
			encryptedHeaderXml.EncryptionMethod = base.EncryptionAlgorithm;
			encryptedHeaderXml.EncryptionMethodDictionaryString = base.EncryptionAlgorithmDictionaryString;
			encryptedHeaderXml.KeyIdentifier = keyIdentifier;
			encryptedHeaderXml.Id = id;
			encryptedHeaderXml.MustUnderstand = this.MustUnderstand;
			encryptedHeaderXml.Relay = this.Relay;
			encryptedHeaderXml.Actor = this.Actor;
			encryptedHeaderXml.SetUpEncryption(algorithm, stream);
			return new EncryptedHeader(plainTextHeader, encryptedHeaderXml, EncryptedHeaderXml.ElementName.Value, EncryptedHeaderXml.NamespaceUri.Value, version);
		}
	}
}
