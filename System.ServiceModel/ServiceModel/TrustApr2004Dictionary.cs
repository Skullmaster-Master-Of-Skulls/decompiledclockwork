using System;

namespace System.ServiceModel
{
	// Token: 0x02000072 RID: 114
	internal class TrustApr2004Dictionary : TrustDictionary
	{
		// Token: 0x06000273 RID: 627 RVA: 0x0000ECF4 File Offset: 0x0000CEF4
		public TrustApr2004Dictionary(ServiceModelDictionary dictionary) : base(dictionary)
		{
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
			this.RequestSecurityTokenResponseCollection = dictionary.CreateString("RequestSecurityTokenResponseCollection", 62);
			this.Context = dictionary.CreateString("Context", 209);
			this.BinarySecret = dictionary.CreateString("BinarySecret", 210);
			this.Type = dictionary.CreateString("Type", 59);
			this.SpnegoValueTypeUri = dictionary.CreateString("http://schemas.xmlsoap.org/ws/2005/02/trust/spnego", 211);
			this.TlsnegoValueTypeUri = dictionary.CreateString(" http://schemas.xmlsoap.org/ws/2005/02/trust/tlsnego", 212);
			this.Prefix = dictionary.CreateString("wst", 213);
			this.Namespace = dictionary.CreateString("http://schemas.xmlsoap.org/ws/2004/04/trust", 214);
			this.RequestSecurityTokenIssuance = dictionary.CreateString("http://schemas.xmlsoap.org/ws/2004/04/security/trust/RST/Issue", 215);
			this.RequestSecurityTokenIssuanceResponse = dictionary.CreateString("http://schemas.xmlsoap.org/ws/2004/04/security/trust/RSTR/Issue", 216);
			this.RequestTypeIssue = dictionary.CreateString("http://schemas.xmlsoap.org/ws/2004/04/security/trust/Issue", 217);
			this.Psha1ComputedKeyUri = dictionary.CreateString("http://schemas.xmlsoap.org/ws/2004/04/security/trust/CK/PSHA1", 218);
			this.SymmetricKeyBinarySecret = dictionary.CreateString("http://schemas.xmlsoap.org/ws/2004/04/security/trust/SymmetricKey", 219);
			this.NonceBinarySecret = dictionary.CreateString("http://schemas.xmlsoap.org/ws/2004/04/security/trust/Nonce", 220);
			this.KeyType = dictionary.CreateString("KeyType", 221);
			this.SymmetricKeyType = dictionary.CreateString("http://schemas.xmlsoap.org/ws/2004/04/trust/SymmetricKey", 222);
			this.PublicKeyType = dictionary.CreateString("http://schemas.xmlsoap.org/ws/2004/04/trust/PublicKey", 223);
			this.Claims = dictionary.CreateString("Claims", 224);
			this.InvalidRequestFaultCode = dictionary.CreateString("InvalidRequest", 225);
			this.FailedAuthenticationFaultCode = dictionary.CreateString("FailedAuthentication", 182);
			this.RequestFailedFaultCode = dictionary.CreateString("RequestFailed", 226);
			this.SignWith = dictionary.CreateString("SignWith", 227);
			this.EncryptWith = dictionary.CreateString("EncryptWith", 228);
			this.EncryptionAlgorithm = dictionary.CreateString("EncryptionAlgorithm", 229);
			this.CanonicalizationAlgorithm = dictionary.CreateString("CanonicalizationAlgorithm", 230);
			this.ComputedKeyAlgorithm = dictionary.CreateString("ComputedKeyAlgorithm", 231);
			this.UseKey = dictionary.CreateString("UseKey", 232);
		}
	}
}
