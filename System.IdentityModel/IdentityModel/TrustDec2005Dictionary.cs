using System;
using System.Xml;

namespace System.IdentityModel
{
	// Token: 0x020000CE RID: 206
	internal class TrustDec2005Dictionary : TrustDictionary
	{
		// Token: 0x06000618 RID: 1560 RVA: 0x00017A2C File Offset: 0x00015C2C
		public TrustDec2005Dictionary(IdentityModelDictionary dictionary) : base(dictionary)
		{
			this.CombinedHashLabel = dictionary.CreateString("AUTH-HASH", 196);
			this.RequestSecurityTokenResponse = dictionary.CreateString("RequestSecurityTokenResponse", 197);
			this.TokenType = dictionary.CreateString("TokenType", 147);
			this.KeySize = dictionary.CreateString("KeySize", 198);
			this.RequestedTokenReference = dictionary.CreateString("RequestedTokenReference", 199);
			this.AppliesTo = dictionary.CreateString("AppliesTo", 200);
			this.Authenticator = dictionary.CreateString("Authenticator", 201);
			this.CombinedHash = dictionary.CreateString("CombinedHash", 202);
			this.BinaryExchange = dictionary.CreateString("BinaryExchange", 203);
			this.Lifetime = dictionary.CreateString("Lifetime", 204);
			this.RequestedSecurityToken = dictionary.CreateString("RequestedSecurityToken", 205);
			this.Entropy = dictionary.CreateString("Entropy", 206);
			this.RequestedProofToken = dictionary.CreateString("RequestedProofToken", 207);
			this.ComputedKey = dictionary.CreateString("ComputedKey", 208);
			this.RequestSecurityToken = dictionary.CreateString("RequestSecurityToken", 209);
			this.RequestType = dictionary.CreateString("RequestType", 210);
			this.Context = dictionary.CreateString("Context", 211);
			this.BinarySecret = dictionary.CreateString("BinarySecret", 212);
			this.Type = dictionary.CreateString("Type", 83);
			this.SpnegoValueTypeUri = dictionary.CreateString("http://schemas.xmlsoap.org/ws/2005/02/trust/spnego", 240);
			this.TlsnegoValueTypeUri = dictionary.CreateString("http://schemas.xmlsoap.org/ws/2005/02/trust/tlsnego", 241);
			this.Prefix = dictionary.CreateString("trust", 242);
			this.RequestSecurityTokenIssuance = dictionary.CreateString("http://docs.oasis-open.org/ws-sx/ws-trust/200512/RST/Issue", 243);
			this.RequestSecurityTokenIssuanceResponse = dictionary.CreateString("http://docs.oasis-open.org/ws-sx/ws-trust/200512/RSTR/Issue", 244);
			this.RequestTypeIssue = dictionary.CreateString("http://docs.oasis-open.org/ws-sx/ws-trust/200512/Issue", 245);
			this.AsymmetricKeyBinarySecret = dictionary.CreateString("http://docs.oasis-open.org/ws-sx/ws-trust/200512/AsymmetricKey", 246);
			this.SymmetricKeyBinarySecret = dictionary.CreateString("http://docs.oasis-open.org/ws-sx/ws-trust/200512/SymmetricKey", 247);
			this.NonceBinarySecret = dictionary.CreateString("http://docs.oasis-open.org/ws-sx/ws-trust/200512/Nonce", 248);
			this.Psha1ComputedKeyUri = dictionary.CreateString("http://docs.oasis-open.org/ws-sx/ws-trust/200512/CK/PSHA1", 249);
			this.KeyType = dictionary.CreateString("KeyType", 230);
			this.SymmetricKeyType = dictionary.CreateString("http://docs.oasis-open.org/ws-sx/ws-trust/200512/SymmetricKey", 247);
			this.PublicKeyType = dictionary.CreateString("http://docs.oasis-open.org/ws-sx/ws-trust/200512/PublicKey", 250);
			this.Claims = dictionary.CreateString("Claims", 232);
			this.InvalidRequestFaultCode = dictionary.CreateString("InvalidRequest", 233);
			this.FailedAuthenticationFaultCode = dictionary.CreateString("FailedAuthentication", 136);
			this.UseKey = dictionary.CreateString("UseKey", 234);
			this.SignWith = dictionary.CreateString("SignWith", 235);
			this.EncryptWith = dictionary.CreateString("EncryptWith", 236);
			this.EncryptionAlgorithm = dictionary.CreateString("EncryptionAlgorithm", 237);
			this.CanonicalizationAlgorithm = dictionary.CreateString("CanonicalizationAlgorithm", 238);
			this.ComputedKeyAlgorithm = dictionary.CreateString("ComputedKeyAlgorithm", 239);
			this.RequestSecurityTokenResponseCollection = dictionary.CreateString("RequestSecurityTokenResponseCollection", 193);
			this.Namespace = dictionary.CreateString("http://docs.oasis-open.org/ws-sx/ws-trust/200512", 251);
			this.BinarySecretClauseType = dictionary.CreateString("http://docs.oasis-open.org/ws-sx/ws-trust/200512#BinarySecret", 252);
			this.RequestSecurityTokenCollectionIssuanceFinalResponse = dictionary.CreateString("http://docs.oasis-open.org/ws-sx/ws-trust/200512/RSTRC/IssueFinal", 253);
			this.RequestSecurityTokenRenewal = dictionary.CreateString("http://docs.oasis-open.org/ws-sx/ws-trust/200512/RST/Renew", 254);
			this.RequestSecurityTokenRenewalResponse = dictionary.CreateString("http://docs.oasis-open.org/ws-sx/ws-trust/200512/RSTR/Renew", 255);
			this.RequestSecurityTokenCollectionRenewalFinalResponse = dictionary.CreateString("http://docs.oasis-open.org/ws-sx/ws-trust/200512/RSTR/RenewFinal", 256);
			this.RequestSecurityTokenCancellation = dictionary.CreateString("http://docs.oasis-open.org/ws-sx/ws-trust/200512/RST/Cancel", 257);
			this.RequestSecurityTokenCancellationResponse = dictionary.CreateString("http://docs.oasis-open.org/ws-sx/ws-trust/200512/RSTR/Cancel", 258);
			this.RequestSecurityTokenCollectionCancellationFinalResponse = dictionary.CreateString("http://docs.oasis-open.org/ws-sx/ws-trust/200512/RSTR/CancelFinal", 259);
			this.RequestTypeRenew = dictionary.CreateString("http://docs.oasis-open.org/ws-sx/ws-trust/200512/Renew", 260);
			this.RequestTypeClose = dictionary.CreateString("http://docs.oasis-open.org/ws-sx/ws-trust/200512/Cancel", 261);
			this.RenewTarget = dictionary.CreateString("RenewTarget", 222);
			this.CloseTarget = dictionary.CreateString("CancelTarget", 223);
			this.RequestedTokenClosed = dictionary.CreateString("RequestedTokenCancelled", 224);
			this.RequestedAttachedReference = dictionary.CreateString("RequestedAttachedReference", 225);
			this.RequestedUnattachedReference = dictionary.CreateString("RequestedUnattachedReference", 226);
			this.IssuedTokensHeader = dictionary.CreateString("IssuedTokens", 227);
			this.KeyWrapAlgorithm = dictionary.CreateString("KeyWrapAlgorithm", 262);
			this.BearerKeyType = dictionary.CreateString("http://docs.oasis-open.org/ws-sx/ws-trust/200512/Bearer", 263);
			this.SecondaryParameters = dictionary.CreateString("SecondaryParameters", 264);
			this.Dialect = dictionary.CreateString("Dialect", 265);
			this.DialectType = dictionary.CreateString("http://schemas.xmlsoap.org/ws/2005/05/identity", 266);
		}

		// Token: 0x06000619 RID: 1561 RVA: 0x00017FC0 File Offset: 0x000161C0
		public TrustDec2005Dictionary(IXmlDictionary dictionary) : base(dictionary)
		{
			this.CombinedHashLabel = this.LookupDictionaryString(dictionary, "AUTH-HASH");
			this.RequestSecurityTokenResponse = this.LookupDictionaryString(dictionary, "RequestSecurityTokenResponse");
			this.TokenType = this.LookupDictionaryString(dictionary, "TokenType");
			this.KeySize = this.LookupDictionaryString(dictionary, "KeySize");
			this.RequestedTokenReference = this.LookupDictionaryString(dictionary, "RequestedTokenReference");
			this.AppliesTo = this.LookupDictionaryString(dictionary, "AppliesTo");
			this.Authenticator = this.LookupDictionaryString(dictionary, "Authenticator");
			this.CombinedHash = this.LookupDictionaryString(dictionary, "CombinedHash");
			this.BinaryExchange = this.LookupDictionaryString(dictionary, "BinaryExchange");
			this.Lifetime = this.LookupDictionaryString(dictionary, "Lifetime");
			this.RequestedSecurityToken = this.LookupDictionaryString(dictionary, "RequestedSecurityToken");
			this.Entropy = this.LookupDictionaryString(dictionary, "Entropy");
			this.RequestedProofToken = this.LookupDictionaryString(dictionary, "RequestedProofToken");
			this.ComputedKey = this.LookupDictionaryString(dictionary, "ComputedKey");
			this.RequestSecurityToken = this.LookupDictionaryString(dictionary, "RequestSecurityToken");
			this.RequestType = this.LookupDictionaryString(dictionary, "RequestType");
			this.Context = this.LookupDictionaryString(dictionary, "Context");
			this.BinarySecret = this.LookupDictionaryString(dictionary, "BinarySecret");
			this.Type = this.LookupDictionaryString(dictionary, "Type");
			this.SpnegoValueTypeUri = this.LookupDictionaryString(dictionary, "http://schemas.xmlsoap.org/ws/2005/02/trust/spnego");
			this.TlsnegoValueTypeUri = this.LookupDictionaryString(dictionary, "http://schemas.xmlsoap.org/ws/2005/02/trust/tlsnego");
			this.Prefix = this.LookupDictionaryString(dictionary, "trust");
			this.RequestSecurityTokenIssuance = this.LookupDictionaryString(dictionary, "http://docs.oasis-open.org/ws-sx/ws-trust/200512/RST/Issue");
			this.RequestSecurityTokenIssuanceResponse = this.LookupDictionaryString(dictionary, "http://docs.oasis-open.org/ws-sx/ws-trust/200512/RSTR/Issue");
			this.RequestTypeIssue = this.LookupDictionaryString(dictionary, "http://docs.oasis-open.org/ws-sx/ws-trust/200512/Issue");
			this.AsymmetricKeyBinarySecret = this.LookupDictionaryString(dictionary, "http://docs.oasis-open.org/ws-sx/ws-trust/200512/AsymmetricKey");
			this.SymmetricKeyBinarySecret = this.LookupDictionaryString(dictionary, "http://docs.oasis-open.org/ws-sx/ws-trust/200512/SymmetricKey");
			this.NonceBinarySecret = this.LookupDictionaryString(dictionary, "http://docs.oasis-open.org/ws-sx/ws-trust/200512/Nonce");
			this.Psha1ComputedKeyUri = this.LookupDictionaryString(dictionary, "http://docs.oasis-open.org/ws-sx/ws-trust/200512/CK/PSHA1");
			this.KeyType = this.LookupDictionaryString(dictionary, "KeyType");
			this.SymmetricKeyType = this.LookupDictionaryString(dictionary, "http://docs.oasis-open.org/ws-sx/ws-trust/200512/SymmetricKey");
			this.PublicKeyType = this.LookupDictionaryString(dictionary, "http://docs.oasis-open.org/ws-sx/ws-trust/200512/PublicKey");
			this.Claims = this.LookupDictionaryString(dictionary, "Claims");
			this.InvalidRequestFaultCode = this.LookupDictionaryString(dictionary, "InvalidRequest");
			this.FailedAuthenticationFaultCode = this.LookupDictionaryString(dictionary, "FailedAuthentication");
			this.UseKey = this.LookupDictionaryString(dictionary, "UseKey");
			this.SignWith = this.LookupDictionaryString(dictionary, "SignWith");
			this.EncryptWith = this.LookupDictionaryString(dictionary, "EncryptWith");
			this.EncryptionAlgorithm = this.LookupDictionaryString(dictionary, "EncryptionAlgorithm");
			this.CanonicalizationAlgorithm = this.LookupDictionaryString(dictionary, "CanonicalizationAlgorithm");
			this.ComputedKeyAlgorithm = this.LookupDictionaryString(dictionary, "ComputedKeyAlgorithm");
			this.RequestSecurityTokenResponseCollection = this.LookupDictionaryString(dictionary, "RequestSecurityTokenResponseCollection");
			this.Namespace = this.LookupDictionaryString(dictionary, "http://docs.oasis-open.org/ws-sx/ws-trust/200512");
			this.BinarySecretClauseType = this.LookupDictionaryString(dictionary, "http://docs.oasis-open.org/ws-sx/ws-trust/200512#BinarySecret");
			this.RequestSecurityTokenCollectionIssuanceFinalResponse = this.LookupDictionaryString(dictionary, "http://docs.oasis-open.org/ws-sx/ws-trust/200512/RSTRC/IssueFinal");
			this.RequestSecurityTokenRenewal = this.LookupDictionaryString(dictionary, "http://docs.oasis-open.org/ws-sx/ws-trust/200512/RST/Renew");
			this.RequestSecurityTokenRenewalResponse = this.LookupDictionaryString(dictionary, "http://docs.oasis-open.org/ws-sx/ws-trust/200512/RSTR/Renew");
			this.RequestSecurityTokenCollectionRenewalFinalResponse = this.LookupDictionaryString(dictionary, "http://docs.oasis-open.org/ws-sx/ws-trust/200512/RSTR/RenewFinal");
			this.RequestSecurityTokenCancellation = this.LookupDictionaryString(dictionary, "http://docs.oasis-open.org/ws-sx/ws-trust/200512/RST/Cancel");
			this.RequestSecurityTokenCancellationResponse = this.LookupDictionaryString(dictionary, "http://docs.oasis-open.org/ws-sx/ws-trust/200512/RSTR/Cancel");
			this.RequestSecurityTokenCollectionCancellationFinalResponse = this.LookupDictionaryString(dictionary, "http://docs.oasis-open.org/ws-sx/ws-trust/200512/RSTR/CancelFinal");
			this.RequestTypeRenew = this.LookupDictionaryString(dictionary, "http://docs.oasis-open.org/ws-sx/ws-trust/200512/Renew");
			this.RequestTypeClose = this.LookupDictionaryString(dictionary, "http://docs.oasis-open.org/ws-sx/ws-trust/200512/Cancel");
			this.RenewTarget = this.LookupDictionaryString(dictionary, "RenewTarget");
			this.CloseTarget = this.LookupDictionaryString(dictionary, "CancelTarget");
			this.RequestedTokenClosed = this.LookupDictionaryString(dictionary, "RequestedTokenCancelled");
			this.RequestedAttachedReference = this.LookupDictionaryString(dictionary, "RequestedAttachedReference");
			this.RequestedUnattachedReference = this.LookupDictionaryString(dictionary, "RequestedUnattachedReference");
			this.IssuedTokensHeader = this.LookupDictionaryString(dictionary, "IssuedTokens");
			this.KeyWrapAlgorithm = this.LookupDictionaryString(dictionary, "KeyWrapAlgorithm");
			this.BearerKeyType = this.LookupDictionaryString(dictionary, "http://docs.oasis-open.org/ws-sx/ws-trust/200512/Bearer");
			this.SecondaryParameters = this.LookupDictionaryString(dictionary, "SecondaryParameters");
			this.Dialect = this.LookupDictionaryString(dictionary, "Dialect");
			this.DialectType = this.LookupDictionaryString(dictionary, "http://schemas.xmlsoap.org/ws/2005/05/identity");
		}

		// Token: 0x0600061A RID: 1562 RVA: 0x00018454 File Offset: 0x00016654
		private XmlDictionaryString LookupDictionaryString(IXmlDictionary dictionary, string value)
		{
			XmlDictionaryString result;
			if (!dictionary.TryLookup(value, out result))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument(SR.GetString("XDCannotFindValueInDictionaryString", new object[]
				{
					value
				}));
			}
			return result;
		}
	}
}
