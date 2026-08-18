using System;
using System.Collections.Generic;
using System.Xml;

namespace System.ServiceModel
{
	// Token: 0x02000042 RID: 66
	internal class TrustDec2005Dictionary : TrustDictionary
	{
		// Token: 0x060001FC RID: 508 RVA: 0x00009D30 File Offset: 0x00007F30
		public TrustDec2005Dictionary(XmlDictionary dictionary)
		{
			this.CombinedHashLabel = dictionary.Add("AUTH-HASH");
			this.RequestSecurityTokenResponse = dictionary.Add("RequestSecurityTokenResponse");
			this.TokenType = dictionary.Add("TokenType");
			this.KeySize = dictionary.Add("KeySize");
			this.RequestedTokenReference = dictionary.Add("RequestedTokenReference");
			this.AppliesTo = dictionary.Add("AppliesTo");
			this.Authenticator = dictionary.Add("Authenticator");
			this.CombinedHash = dictionary.Add("CombinedHash");
			this.BinaryExchange = dictionary.Add("BinaryExchange");
			this.Lifetime = dictionary.Add("Lifetime");
			this.RequestedSecurityToken = dictionary.Add("RequestedSecurityToken");
			this.Entropy = dictionary.Add("Entropy");
			this.RequestedProofToken = dictionary.Add("RequestedProofToken");
			this.ComputedKey = dictionary.Add("ComputedKey");
			this.RequestSecurityToken = dictionary.Add("RequestSecurityToken");
			this.RequestType = dictionary.Add("RequestType");
			this.Context = dictionary.Add("Context");
			this.BinarySecret = dictionary.Add("BinarySecret");
			this.Type = dictionary.Add("Type");
			this.SpnegoValueTypeUri = dictionary.Add("http://schemas.xmlsoap.org/ws/2005/02/trust/spnego");
			this.TlsnegoValueTypeUri = dictionary.Add("http://schemas.xmlsoap.org/ws/2005/02/trust/tlsnego");
			this.Prefix = dictionary.Add("trust");
			this.RequestSecurityTokenIssuance = dictionary.Add("http://docs.oasis-open.org/ws-sx/ws-trust/200512/RST/Issue");
			this.RequestSecurityTokenIssuanceResponse = dictionary.Add("http://docs.oasis-open.org/ws-sx/ws-trust/200512/RSTR/Issue");
			this.RequestTypeIssue = dictionary.Add("http://docs.oasis-open.org/ws-sx/ws-trust/200512/Issue");
			this.AsymmetricKeyBinarySecret = dictionary.Add("http://docs.oasis-open.org/ws-sx/ws-trust/200512/AsymmetricKey");
			this.SymmetricKeyBinarySecret = dictionary.Add("http://docs.oasis-open.org/ws-sx/ws-trust/200512/SymmetricKey");
			this.NonceBinarySecret = dictionary.Add("http://docs.oasis-open.org/ws-sx/ws-trust/200512/Nonce");
			this.Psha1ComputedKeyUri = dictionary.Add("http://docs.oasis-open.org/ws-sx/ws-trust/200512/CK/PSHA1");
			this.KeyType = dictionary.Add("KeyType");
			this.SymmetricKeyType = dictionary.Add("http://docs.oasis-open.org/ws-sx/ws-trust/200512/SymmetricKey");
			this.PublicKeyType = dictionary.Add("http://docs.oasis-open.org/ws-sx/ws-trust/200512/PublicKey");
			this.Claims = dictionary.Add("Claims");
			this.InvalidRequestFaultCode = dictionary.Add("InvalidRequest");
			this.FailedAuthenticationFaultCode = dictionary.Add("FailedAuthentication");
			this.UseKey = dictionary.Add("UseKey");
			this.SignWith = dictionary.Add("SignWith");
			this.EncryptWith = dictionary.Add("EncryptWith");
			this.EncryptionAlgorithm = dictionary.Add("EncryptionAlgorithm");
			this.CanonicalizationAlgorithm = dictionary.Add("CanonicalizationAlgorithm");
			this.ComputedKeyAlgorithm = dictionary.Add("ComputedKeyAlgorithm");
			this.RequestSecurityTokenResponseCollection = dictionary.Add("RequestSecurityTokenResponseCollection");
			this.Namespace = dictionary.Add("http://docs.oasis-open.org/ws-sx/ws-trust/200512");
			this.BinarySecretClauseType = dictionary.Add("http://docs.oasis-open.org/ws-sx/ws-trust/200512#BinarySecret");
			this.RequestSecurityTokenCollectionIssuanceFinalResponse = dictionary.Add("http://docs.oasis-open.org/ws-sx/ws-trust/200512/RSTRC/IssueFinal");
			this.RequestSecurityTokenRenewal = dictionary.Add("http://docs.oasis-open.org/ws-sx/ws-trust/200512/RST/Renew");
			this.RequestSecurityTokenRenewalResponse = dictionary.Add("http://docs.oasis-open.org/ws-sx/ws-trust/200512/RSTR/Renew");
			this.RequestSecurityTokenCollectionRenewalFinalResponse = dictionary.Add("http://docs.oasis-open.org/ws-sx/ws-trust/200512/RSTR/RenewFinal");
			this.RequestSecurityTokenCancellation = dictionary.Add("http://docs.oasis-open.org/ws-sx/ws-trust/200512/RST/Cancel");
			this.RequestSecurityTokenCancellationResponse = dictionary.Add("http://docs.oasis-open.org/ws-sx/ws-trust/200512/RSTR/Cancel");
			this.RequestSecurityTokenCollectionCancellationFinalResponse = dictionary.Add("http://docs.oasis-open.org/ws-sx/ws-trust/200512/RSTR/CancelFinal");
			this.RequestTypeRenew = dictionary.Add("http://docs.oasis-open.org/ws-sx/ws-trust/200512/Renew");
			this.RequestTypeClose = dictionary.Add("http://docs.oasis-open.org/ws-sx/ws-trust/200512/Cancel");
			this.RenewTarget = dictionary.Add("RenewTarget");
			this.CloseTarget = dictionary.Add("CancelTarget");
			this.RequestedTokenClosed = dictionary.Add("RequestedTokenCancelled");
			this.RequestedAttachedReference = dictionary.Add("RequestedAttachedReference");
			this.RequestedUnattachedReference = dictionary.Add("RequestedUnattachedReference");
			this.IssuedTokensHeader = dictionary.Add("IssuedTokens");
			this.KeyWrapAlgorithm = dictionary.Add("KeyWrapAlgorithm");
			this.BearerKeyType = dictionary.Add("http://docs.oasis-open.org/ws-sx/ws-trust/200512/Bearer");
			this.SecondaryParameters = dictionary.Add("SecondaryParameters");
			this.Dialect = dictionary.Add("Dialect");
			this.DialectType = dictionary.Add("http://schemas.xmlsoap.org/ws/2005/05/identity");
		}

		// Token: 0x060001FD RID: 509 RVA: 0x0000A19C File Offset: 0x0000839C
		public void PopulateFeb2005DictionaryString()
		{
			this.Feb2005DictionaryStrings.Add(XD.TrustFeb2005Dictionary.RequestSecurityTokenResponseCollection);
			this.Feb2005DictionaryStrings.Add(XD.TrustFeb2005Dictionary.Namespace);
			this.Feb2005DictionaryStrings.Add(XD.TrustFeb2005Dictionary.BinarySecretClauseType);
			this.Feb2005DictionaryStrings.Add(XD.TrustFeb2005Dictionary.CombinedHashLabel);
			this.Feb2005DictionaryStrings.Add(XD.TrustFeb2005Dictionary.RequestSecurityTokenResponse);
			this.Feb2005DictionaryStrings.Add(XD.TrustFeb2005Dictionary.TokenType);
			this.Feb2005DictionaryStrings.Add(XD.TrustFeb2005Dictionary.KeySize);
			this.Feb2005DictionaryStrings.Add(XD.TrustFeb2005Dictionary.RequestedTokenReference);
			this.Feb2005DictionaryStrings.Add(XD.TrustFeb2005Dictionary.AppliesTo);
			this.Feb2005DictionaryStrings.Add(XD.TrustFeb2005Dictionary.Authenticator);
			this.Feb2005DictionaryStrings.Add(XD.TrustFeb2005Dictionary.CombinedHash);
			this.Feb2005DictionaryStrings.Add(XD.TrustFeb2005Dictionary.BinaryExchange);
			this.Feb2005DictionaryStrings.Add(XD.TrustFeb2005Dictionary.Lifetime);
			this.Feb2005DictionaryStrings.Add(XD.TrustFeb2005Dictionary.RequestedSecurityToken);
			this.Feb2005DictionaryStrings.Add(XD.TrustFeb2005Dictionary.Entropy);
			this.Feb2005DictionaryStrings.Add(XD.TrustFeb2005Dictionary.RequestedProofToken);
			this.Feb2005DictionaryStrings.Add(XD.TrustFeb2005Dictionary.ComputedKey);
			this.Feb2005DictionaryStrings.Add(XD.TrustFeb2005Dictionary.RequestSecurityToken);
			this.Feb2005DictionaryStrings.Add(XD.TrustFeb2005Dictionary.RequestType);
			this.Feb2005DictionaryStrings.Add(XD.TrustFeb2005Dictionary.Context);
			this.Feb2005DictionaryStrings.Add(XD.TrustFeb2005Dictionary.BinarySecret);
			this.Feb2005DictionaryStrings.Add(XD.TrustFeb2005Dictionary.Type);
			this.Feb2005DictionaryStrings.Add(XD.TrustFeb2005Dictionary.SpnegoValueTypeUri);
			this.Feb2005DictionaryStrings.Add(XD.TrustFeb2005Dictionary.TlsnegoValueTypeUri);
			this.Feb2005DictionaryStrings.Add(XD.TrustFeb2005Dictionary.Prefix);
			this.Feb2005DictionaryStrings.Add(XD.TrustFeb2005Dictionary.RequestSecurityTokenIssuance);
			this.Feb2005DictionaryStrings.Add(XD.TrustFeb2005Dictionary.RequestSecurityTokenIssuanceResponse);
			this.Feb2005DictionaryStrings.Add(XD.TrustFeb2005Dictionary.RequestTypeIssue);
			this.Feb2005DictionaryStrings.Add(XD.TrustFeb2005Dictionary.SymmetricKeyBinarySecret);
			this.Feb2005DictionaryStrings.Add(XD.TrustFeb2005Dictionary.Psha1ComputedKeyUri);
			this.Feb2005DictionaryStrings.Add(XD.TrustFeb2005Dictionary.NonceBinarySecret);
			this.Feb2005DictionaryStrings.Add(XD.TrustFeb2005Dictionary.RenewTarget);
			this.Feb2005DictionaryStrings.Add(XD.TrustFeb2005Dictionary.CloseTarget);
			this.Feb2005DictionaryStrings.Add(XD.TrustFeb2005Dictionary.RequestedTokenClosed);
			this.Feb2005DictionaryStrings.Add(XD.TrustFeb2005Dictionary.RequestedAttachedReference);
			this.Feb2005DictionaryStrings.Add(XD.TrustFeb2005Dictionary.RequestedUnattachedReference);
			this.Feb2005DictionaryStrings.Add(XD.TrustFeb2005Dictionary.IssuedTokensHeader);
			this.Feb2005DictionaryStrings.Add(XD.TrustFeb2005Dictionary.RequestTypeRenew);
			this.Feb2005DictionaryStrings.Add(XD.TrustFeb2005Dictionary.RequestTypeClose);
			this.Feb2005DictionaryStrings.Add(XD.TrustFeb2005Dictionary.KeyType);
			this.Feb2005DictionaryStrings.Add(XD.TrustFeb2005Dictionary.SymmetricKeyType);
			this.Feb2005DictionaryStrings.Add(XD.TrustFeb2005Dictionary.PublicKeyType);
			this.Feb2005DictionaryStrings.Add(XD.TrustFeb2005Dictionary.Claims);
			this.Feb2005DictionaryStrings.Add(XD.TrustFeb2005Dictionary.InvalidRequestFaultCode);
			this.Feb2005DictionaryStrings.Add(XD.TrustFeb2005Dictionary.FailedAuthenticationFaultCode);
			this.Feb2005DictionaryStrings.Add(XD.TrustFeb2005Dictionary.UseKey);
			this.Feb2005DictionaryStrings.Add(XD.TrustFeb2005Dictionary.SignWith);
			this.Feb2005DictionaryStrings.Add(XD.TrustFeb2005Dictionary.EncryptWith);
			this.Feb2005DictionaryStrings.Add(XD.TrustFeb2005Dictionary.EncryptionAlgorithm);
			this.Feb2005DictionaryStrings.Add(XD.TrustFeb2005Dictionary.CanonicalizationAlgorithm);
			this.Feb2005DictionaryStrings.Add(XD.TrustFeb2005Dictionary.ComputedKeyAlgorithm);
		}

		// Token: 0x060001FE RID: 510 RVA: 0x0000A5D8 File Offset: 0x000087D8
		public void PopulateDec2005DictionaryStrings()
		{
			this.Dec2005DictionaryString.Add(DXD.TrustDec2005Dictionary.CombinedHashLabel);
			this.Dec2005DictionaryString.Add(DXD.TrustDec2005Dictionary.RequestSecurityTokenResponse);
			this.Dec2005DictionaryString.Add(DXD.TrustDec2005Dictionary.TokenType);
			this.Dec2005DictionaryString.Add(DXD.TrustDec2005Dictionary.KeySize);
			this.Dec2005DictionaryString.Add(DXD.TrustDec2005Dictionary.RequestedTokenReference);
			this.Dec2005DictionaryString.Add(DXD.TrustDec2005Dictionary.AppliesTo);
			this.Dec2005DictionaryString.Add(DXD.TrustDec2005Dictionary.Authenticator);
			this.Dec2005DictionaryString.Add(DXD.TrustDec2005Dictionary.CombinedHash);
			this.Dec2005DictionaryString.Add(DXD.TrustDec2005Dictionary.BinaryExchange);
			this.Dec2005DictionaryString.Add(DXD.TrustDec2005Dictionary.Lifetime);
			this.Dec2005DictionaryString.Add(DXD.TrustDec2005Dictionary.RequestedSecurityToken);
			this.Dec2005DictionaryString.Add(DXD.TrustDec2005Dictionary.Entropy);
			this.Dec2005DictionaryString.Add(DXD.TrustDec2005Dictionary.RequestedProofToken);
			this.Dec2005DictionaryString.Add(DXD.TrustDec2005Dictionary.ComputedKey);
			this.Dec2005DictionaryString.Add(DXD.TrustDec2005Dictionary.RequestSecurityToken);
			this.Dec2005DictionaryString.Add(DXD.TrustDec2005Dictionary.RequestType);
			this.Dec2005DictionaryString.Add(DXD.TrustDec2005Dictionary.Context);
			this.Dec2005DictionaryString.Add(DXD.TrustDec2005Dictionary.BinarySecret);
			this.Dec2005DictionaryString.Add(DXD.TrustDec2005Dictionary.Type);
			this.Dec2005DictionaryString.Add(DXD.TrustDec2005Dictionary.SpnegoValueTypeUri);
			this.Dec2005DictionaryString.Add(DXD.TrustDec2005Dictionary.TlsnegoValueTypeUri);
			this.Dec2005DictionaryString.Add(DXD.TrustDec2005Dictionary.Prefix);
			this.Dec2005DictionaryString.Add(DXD.TrustDec2005Dictionary.RequestSecurityTokenIssuance);
			this.Dec2005DictionaryString.Add(DXD.TrustDec2005Dictionary.RequestSecurityTokenIssuanceResponse);
			this.Dec2005DictionaryString.Add(DXD.TrustDec2005Dictionary.RequestTypeIssue);
			this.Dec2005DictionaryString.Add(DXD.TrustDec2005Dictionary.AsymmetricKeyBinarySecret);
			this.Dec2005DictionaryString.Add(DXD.TrustDec2005Dictionary.SymmetricKeyBinarySecret);
			this.Dec2005DictionaryString.Add(DXD.TrustDec2005Dictionary.NonceBinarySecret);
			this.Dec2005DictionaryString.Add(DXD.TrustDec2005Dictionary.Psha1ComputedKeyUri);
			this.Dec2005DictionaryString.Add(DXD.TrustDec2005Dictionary.KeyType);
			this.Dec2005DictionaryString.Add(DXD.TrustDec2005Dictionary.SymmetricKeyType);
			this.Dec2005DictionaryString.Add(DXD.TrustDec2005Dictionary.PublicKeyType);
			this.Dec2005DictionaryString.Add(DXD.TrustDec2005Dictionary.Claims);
			this.Dec2005DictionaryString.Add(DXD.TrustDec2005Dictionary.InvalidRequestFaultCode);
			this.Dec2005DictionaryString.Add(DXD.TrustDec2005Dictionary.FailedAuthenticationFaultCode);
			this.Dec2005DictionaryString.Add(DXD.TrustDec2005Dictionary.UseKey);
			this.Dec2005DictionaryString.Add(DXD.TrustDec2005Dictionary.SignWith);
			this.Dec2005DictionaryString.Add(DXD.TrustDec2005Dictionary.EncryptWith);
			this.Dec2005DictionaryString.Add(DXD.TrustDec2005Dictionary.EncryptionAlgorithm);
			this.Dec2005DictionaryString.Add(DXD.TrustDec2005Dictionary.CanonicalizationAlgorithm);
			this.Dec2005DictionaryString.Add(DXD.TrustDec2005Dictionary.ComputedKeyAlgorithm);
			this.Dec2005DictionaryString.Add(DXD.TrustDec2005Dictionary.RequestSecurityTokenResponseCollection);
			this.Dec2005DictionaryString.Add(DXD.TrustDec2005Dictionary.Namespace);
			this.Dec2005DictionaryString.Add(DXD.TrustDec2005Dictionary.BinarySecretClauseType);
			this.Dec2005DictionaryString.Add(DXD.TrustDec2005Dictionary.RequestSecurityTokenCollectionIssuanceFinalResponse);
			this.Dec2005DictionaryString.Add(DXD.TrustDec2005Dictionary.RequestSecurityTokenRenewal);
			this.Dec2005DictionaryString.Add(DXD.TrustDec2005Dictionary.RequestSecurityTokenRenewalResponse);
			this.Dec2005DictionaryString.Add(DXD.TrustDec2005Dictionary.RequestSecurityTokenCollectionRenewalFinalResponse);
			this.Dec2005DictionaryString.Add(DXD.TrustDec2005Dictionary.RequestSecurityTokenCancellation);
			this.Dec2005DictionaryString.Add(DXD.TrustDec2005Dictionary.RequestSecurityTokenCancellationResponse);
			this.Dec2005DictionaryString.Add(DXD.TrustDec2005Dictionary.RequestSecurityTokenCollectionCancellationFinalResponse);
			this.Dec2005DictionaryString.Add(DXD.TrustDec2005Dictionary.RequestTypeRenew);
			this.Dec2005DictionaryString.Add(DXD.TrustDec2005Dictionary.RequestTypeClose);
			this.Dec2005DictionaryString.Add(DXD.TrustDec2005Dictionary.RenewTarget);
			this.Dec2005DictionaryString.Add(DXD.TrustDec2005Dictionary.CloseTarget);
			this.Dec2005DictionaryString.Add(DXD.TrustDec2005Dictionary.RequestedTokenClosed);
			this.Dec2005DictionaryString.Add(DXD.TrustDec2005Dictionary.RequestedAttachedReference);
			this.Dec2005DictionaryString.Add(DXD.TrustDec2005Dictionary.RequestedUnattachedReference);
			this.Dec2005DictionaryString.Add(DXD.TrustDec2005Dictionary.IssuedTokensHeader);
			this.Dec2005DictionaryString.Add(DXD.TrustDec2005Dictionary.KeyWrapAlgorithm);
			this.Dec2005DictionaryString.Add(DXD.TrustDec2005Dictionary.BearerKeyType);
			this.Dec2005DictionaryString.Add(DXD.TrustDec2005Dictionary.SecondaryParameters);
			this.Dec2005DictionaryString.Add(DXD.TrustDec2005Dictionary.Dialect);
			this.Dec2005DictionaryString.Add(DXD.TrustDec2005Dictionary.DialectType);
		}

		// Token: 0x040001DF RID: 479
		public XmlDictionaryString AsymmetricKeyBinarySecret;

		// Token: 0x040001E0 RID: 480
		public XmlDictionaryString RequestSecurityTokenCollectionIssuanceFinalResponse;

		// Token: 0x040001E1 RID: 481
		public XmlDictionaryString RequestSecurityTokenRenewal;

		// Token: 0x040001E2 RID: 482
		public XmlDictionaryString RequestSecurityTokenRenewalResponse;

		// Token: 0x040001E3 RID: 483
		public XmlDictionaryString RequestSecurityTokenCollectionRenewalFinalResponse;

		// Token: 0x040001E4 RID: 484
		public XmlDictionaryString RequestSecurityTokenCancellation;

		// Token: 0x040001E5 RID: 485
		public XmlDictionaryString RequestSecurityTokenCancellationResponse;

		// Token: 0x040001E6 RID: 486
		public XmlDictionaryString RequestSecurityTokenCollectionCancellationFinalResponse;

		// Token: 0x040001E7 RID: 487
		public XmlDictionaryString KeyWrapAlgorithm;

		// Token: 0x040001E8 RID: 488
		public XmlDictionaryString BearerKeyType;

		// Token: 0x040001E9 RID: 489
		public XmlDictionaryString SecondaryParameters;

		// Token: 0x040001EA RID: 490
		public XmlDictionaryString Dialect;

		// Token: 0x040001EB RID: 491
		public XmlDictionaryString DialectType;

		// Token: 0x040001EC RID: 492
		public List<XmlDictionaryString> Feb2005DictionaryStrings = new List<XmlDictionaryString>();

		// Token: 0x040001ED RID: 493
		public List<XmlDictionaryString> Dec2005DictionaryString = new List<XmlDictionaryString>();
	}
}
