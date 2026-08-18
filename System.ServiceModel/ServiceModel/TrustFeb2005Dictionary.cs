using System;

namespace System.ServiceModel
{
	// Token: 0x02000073 RID: 115
	internal class TrustFeb2005Dictionary : TrustDictionary
	{
		// Token: 0x06000274 RID: 628 RVA: 0x0000F0B4 File Offset: 0x0000D2B4
		public TrustFeb2005Dictionary(ServiceModelDictionary dictionary) : base(dictionary)
		{
			this.RequestSecurityTokenResponseCollection = dictionary.CreateString("RequestSecurityTokenResponseCollection", 62);
			this.Namespace = dictionary.CreateString("http://schemas.xmlsoap.org/ws/2005/02/trust", 63);
			this.BinarySecretClauseType = dictionary.CreateString("http://schemas.xmlsoap.org/ws/2005/02/trust#BinarySecret", 64);
			this.CombinedHashLabel = dictionary.CreateString("AUTH-HASH", 194);
			this.RequestSecurityTokenResponse = dictionary.CreateString("RequestSecurityTokenResponse", 195);
			this.TokenType = dictionary.CreateString("TokenType", 187);
			this.KeySize = dictionary.CreateString("KeySize", 196);
			this.RequestedTokenReference = dictionary.CreateString("RequestedTokenReference", 197);
			this.AppliesTo = dictionary.CreateString("AppliesTo", 198);
			this.Authenticator = dictionary.CreateString("Authenticator", 199);
			this.CombinedHash = dictionary.CreateString("CombinedHash", 200);
			this.BinaryExchange = dictionary.CreateString("BinaryExchange", 201);
			this.Lifetime = dictionary.CreateString("Lifetime", 202);
			this.RequestedSecurityToken = dictionary.CreateString("RequestedSecurityToken", 203);
			this.Entropy = dictionary.CreateString("Entropy", 204);
			this.RequestedProofToken = dictionary.CreateString("RequestedProofToken", 205);
			this.ComputedKey = dictionary.CreateString("ComputedKey", 206);
			this.RequestSecurityToken = dictionary.CreateString("RequestSecurityToken", 207);
			this.RequestType = dictionary.CreateString("RequestType", 208);
			this.Context = dictionary.CreateString("Context", 209);
			this.BinarySecret = dictionary.CreateString("BinarySecret", 210);
			this.Type = dictionary.CreateString("Type", 59);
			this.SpnegoValueTypeUri = dictionary.CreateString("http://schemas.microsoft.com/net/2004/07/secext/WS-SPNego", 233);
			this.TlsnegoValueTypeUri = dictionary.CreateString("http://schemas.microsoft.com/net/2004/07/secext/TLSNego", 234);
			this.Prefix = dictionary.CreateString("t", 235);
			this.RequestSecurityTokenIssuance = dictionary.CreateString("http://schemas.xmlsoap.org/ws/2005/02/trust/RST/Issue", 236);
			this.RequestSecurityTokenIssuanceResponse = dictionary.CreateString("http://schemas.xmlsoap.org/ws/2005/02/trust/RSTR/Issue", 237);
			this.RequestTypeIssue = dictionary.CreateString("http://schemas.xmlsoap.org/ws/2005/02/trust/Issue", 238);
			this.SymmetricKeyBinarySecret = dictionary.CreateString("http://schemas.xmlsoap.org/ws/2005/02/trust/SymmetricKey", 239);
			this.Psha1ComputedKeyUri = dictionary.CreateString("http://schemas.xmlsoap.org/ws/2005/02/trust/CK/PSHA1", 240);
			this.NonceBinarySecret = dictionary.CreateString("http://schemas.xmlsoap.org/ws/2005/02/trust/Nonce", 241);
			this.RenewTarget = dictionary.CreateString("RenewTarget", 242);
			this.CloseTarget = dictionary.CreateString("CancelTarget", 243);
			this.RequestedTokenClosed = dictionary.CreateString("RequestedTokenCancelled", 244);
			this.RequestedAttachedReference = dictionary.CreateString("RequestedAttachedReference", 245);
			this.RequestedUnattachedReference = dictionary.CreateString("RequestedUnattachedReference", 246);
			this.IssuedTokensHeader = dictionary.CreateString("IssuedTokens", 247);
			this.RequestTypeRenew = dictionary.CreateString("http://schemas.xmlsoap.org/ws/2005/02/trust/Renew", 248);
			this.RequestTypeClose = dictionary.CreateString("http://schemas.xmlsoap.org/ws/2005/02/trust/Cancel", 249);
			this.KeyType = dictionary.CreateString("KeyType", 221);
			this.SymmetricKeyType = dictionary.CreateString("http://schemas.xmlsoap.org/ws/2005/02/trust/SymmetricKey", 239);
			this.PublicKeyType = dictionary.CreateString("http://schemas.xmlsoap.org/ws/2005/02/trust/PublicKey", 250);
			this.Claims = dictionary.CreateString("Claims", 224);
			this.InvalidRequestFaultCode = dictionary.CreateString("InvalidRequest", 225);
			this.FailedAuthenticationFaultCode = dictionary.CreateString("FailedAuthentication", 182);
			this.UseKey = dictionary.CreateString("UseKey", 232);
			this.SignWith = dictionary.CreateString("SignWith", 227);
			this.EncryptWith = dictionary.CreateString("EncryptWith", 228);
			this.EncryptionAlgorithm = dictionary.CreateString("EncryptionAlgorithm", 229);
			this.CanonicalizationAlgorithm = dictionary.CreateString("CanonicalizationAlgorithm", 230);
			this.ComputedKeyAlgorithm = dictionary.CreateString("ComputedKeyAlgorithm", 231);
		}
	}
}
