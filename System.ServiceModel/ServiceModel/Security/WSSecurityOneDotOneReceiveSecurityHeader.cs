using System;
using System.IdentityModel.Tokens;
using System.Security.Cryptography;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Security.Tokens;
using System.Xml;

namespace System.ServiceModel.Security
{
	// Token: 0x0200028D RID: 653
	internal class WSSecurityOneDotOneReceiveSecurityHeader : WSSecurityOneDotZeroReceiveSecurityHeader
	{
		// Token: 0x06001300 RID: 4864 RVA: 0x0004452C File Offset: 0x0004272C
		public WSSecurityOneDotOneReceiveSecurityHeader(Message message, string actor, bool mustUnderstand, bool relay, SecurityStandardsManager standardsManager, SecurityAlgorithmSuite algorithmSuite, int headerIndex, MessageDirection direction) : base(message, actor, mustUnderstand, relay, standardsManager, algorithmSuite, headerIndex, direction)
		{
		}

		// Token: 0x06001301 RID: 4865 RVA: 0x0004454C File Offset: 0x0004274C
		protected override DecryptedHeader DecryptHeader(XmlDictionaryReader reader, WrappedKeySecurityToken wrappedKeyToken)
		{
			EncryptedHeaderXml encryptedHeaderXml = new EncryptedHeaderXml(base.Version, base.MessageDirection == MessageDirection.Output);
			encryptedHeaderXml.SecurityTokenSerializer = base.StandardsManager.SecurityTokenSerializer;
			encryptedHeaderXml.ReadFrom(reader, base.MaxReceivedMessageSize);
			if (encryptedHeaderXml.MustUnderstand != this.MustUnderstand)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new MessageSecurityException(SR.GetString("EncryptedHeaderAttributeMismatch", new object[]
				{
					XD.MessageDictionary.MustUnderstand.Value,
					encryptedHeaderXml.MustUnderstand,
					this.MustUnderstand
				})));
			}
			if (encryptedHeaderXml.Relay != this.Relay)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new MessageSecurityException(SR.GetString("EncryptedHeaderAttributeMismatch", new object[]
				{
					XD.Message12Dictionary.Relay.Value,
					encryptedHeaderXml.Relay,
					this.Relay
				})));
			}
			if (encryptedHeaderXml.Actor != this.Actor)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new MessageSecurityException(SR.GetString("EncryptedHeaderAttributeMismatch", new object[]
				{
					base.Version.Envelope.DictionaryActor,
					encryptedHeaderXml.Actor,
					this.Actor
				})));
			}
			SecurityToken token;
			if (wrappedKeyToken == null)
			{
				token = WSSecurityOneDotZeroReceiveSecurityHeader.ResolveKeyIdentifier(encryptedHeaderXml.KeyIdentifier, base.CombinedPrimaryTokenResolver, false);
			}
			else
			{
				token = wrappedKeyToken;
			}
			base.RecordEncryptionToken(token);
			DecryptedHeader result;
			using (SymmetricAlgorithm symmetricAlgorithm = WSSecurityOneDotZeroReceiveSecurityHeader.CreateDecryptionAlgorithm(token, encryptedHeaderXml.EncryptionMethod, base.AlgorithmSuite))
			{
				encryptedHeaderXml.SetUpDecryption(symmetricAlgorithm);
				result = new DecryptedHeader(encryptedHeaderXml.GetDecryptedBuffer(), base.SecurityVerifiedMessage.GetEnvelopeAttributes(), base.SecurityVerifiedMessage.GetHeaderAttributes(), base.Version, base.StandardsManager.IdManager, base.ReaderQuotas);
			}
			return result;
		}
	}
}
