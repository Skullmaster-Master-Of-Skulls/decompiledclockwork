using System;

namespace System.IdentityModel.Tokens
{
	// Token: 0x02000166 RID: 358
	public static class SecurityAlgorithms
	{
		// Token: 0x04000BFA RID: 3066
		public const string Aes128Encryption = "http://www.w3.org/2001/04/xmlenc#aes128-cbc";

		// Token: 0x04000BFB RID: 3067
		public const string Aes128KeyWrap = "http://www.w3.org/2001/04/xmlenc#kw-aes128";

		// Token: 0x04000BFC RID: 3068
		public const string Aes192Encryption = "http://www.w3.org/2001/04/xmlenc#aes192-cbc";

		// Token: 0x04000BFD RID: 3069
		public const string Aes192KeyWrap = "http://www.w3.org/2001/04/xmlenc#kw-aes192";

		// Token: 0x04000BFE RID: 3070
		public const string Aes256Encryption = "http://www.w3.org/2001/04/xmlenc#aes256-cbc";

		// Token: 0x04000BFF RID: 3071
		public const string Aes256KeyWrap = "http://www.w3.org/2001/04/xmlenc#kw-aes256";

		// Token: 0x04000C00 RID: 3072
		public const string DesEncryption = "http://www.w3.org/2001/04/xmlenc#des-cbc";

		// Token: 0x04000C01 RID: 3073
		public const string DsaSha1Signature = "http://www.w3.org/2000/09/xmldsig#dsa-sha1";

		// Token: 0x04000C02 RID: 3074
		public const string ExclusiveC14n = "http://www.w3.org/2001/10/xml-exc-c14n#";

		// Token: 0x04000C03 RID: 3075
		public const string ExclusiveC14nWithComments = "http://www.w3.org/2001/10/xml-exc-c14n#WithComments";

		// Token: 0x04000C04 RID: 3076
		public const string HmacSha1Signature = "http://www.w3.org/2000/09/xmldsig#hmac-sha1";

		// Token: 0x04000C05 RID: 3077
		public const string HmacSha256Signature = "http://www.w3.org/2001/04/xmldsig-more#hmac-sha256";

		// Token: 0x04000C06 RID: 3078
		public const string Psha1KeyDerivation = "http://schemas.xmlsoap.org/ws/2005/02/sc/dk/p_sha1";

		// Token: 0x04000C07 RID: 3079
		public const string Psha1KeyDerivationDec2005 = "http://docs.oasis-open.org/ws-sx/ws-secureconversation/200512/dk/p_sha1";

		// Token: 0x04000C08 RID: 3080
		public const string Ripemd160Digest = "http://www.w3.org/2001/04/xmlenc#ripemd160";

		// Token: 0x04000C09 RID: 3081
		public const string RsaOaepKeyWrap = "http://www.w3.org/2001/04/xmlenc#rsa-oaep-mgf1p";

		// Token: 0x04000C0A RID: 3082
		public const string RsaSha1Signature = "http://www.w3.org/2000/09/xmldsig#rsa-sha1";

		// Token: 0x04000C0B RID: 3083
		public const string RsaSha256Signature = "http://www.w3.org/2001/04/xmldsig-more#rsa-sha256";

		// Token: 0x04000C0C RID: 3084
		public const string RsaV15KeyWrap = "http://www.w3.org/2001/04/xmlenc#rsa-1_5";

		// Token: 0x04000C0D RID: 3085
		public const string Sha1Digest = "http://www.w3.org/2000/09/xmldsig#sha1";

		// Token: 0x04000C0E RID: 3086
		public const string Sha256Digest = "http://www.w3.org/2001/04/xmlenc#sha256";

		// Token: 0x04000C0F RID: 3087
		public const string Sha512Digest = "http://www.w3.org/2001/04/xmlenc#sha512";

		// Token: 0x04000C10 RID: 3088
		public const string StrTransform = "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-soap-message-security-1.0#STR-Transform";

		// Token: 0x04000C11 RID: 3089
		public const string TripleDesEncryption = "http://www.w3.org/2001/04/xmlenc#tripledes-cbc";

		// Token: 0x04000C12 RID: 3090
		public const string TripleDesKeyWrap = "http://www.w3.org/2001/04/xmlenc#kw-tripledes";

		// Token: 0x04000C13 RID: 3091
		public const string TlsSspiKeyWrap = "http://schemas.xmlsoap.org/2005/02/trust/tlsnego#TLS_Wrap";

		// Token: 0x04000C14 RID: 3092
		public const string WindowsSspiKeyWrap = "http://schemas.xmlsoap.org/2005/02/trust/spnego#GSS_Wrap";

		// Token: 0x04000C15 RID: 3093
		internal const int DefaultSymmetricKeyLength = 256;

		// Token: 0x04000C16 RID: 3094
		internal const string DefaultEncryptionAlgorithm = "http://www.w3.org/2001/04/xmlenc#aes256-cbc";

		// Token: 0x04000C17 RID: 3095
		internal const string DefaultAsymmetricKeyWrapAlgorithm = "http://www.w3.org/2001/04/xmlenc#rsa-oaep-mgf1p";

		// Token: 0x04000C18 RID: 3096
		internal const string DefaultAsymmetricSignatureAlgorithm = "http://www.w3.org/2001/04/xmldsig-more#rsa-sha256";

		// Token: 0x04000C19 RID: 3097
		internal const string DefaultDigestAlgorithm = "http://www.w3.org/2001/04/xmlenc#sha256";
	}
}
