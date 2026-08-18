using System;

namespace System.ServiceModel
{
	// Token: 0x02000091 RID: 145
	internal static class SecurityAlgorithmStrings
	{
		// Token: 0x040007DE RID: 2014
		public const string Aes128Encryption = "http://www.w3.org/2001/04/xmlenc#aes128-cbc";

		// Token: 0x040007DF RID: 2015
		public const string Aes128KeyWrap = "http://www.w3.org/2001/04/xmlenc#kw-aes128";

		// Token: 0x040007E0 RID: 2016
		public const string Aes192Encryption = "http://www.w3.org/2001/04/xmlenc#aes192-cbc";

		// Token: 0x040007E1 RID: 2017
		public const string Aes192KeyWrap = "http://www.w3.org/2001/04/xmlenc#kw-aes192";

		// Token: 0x040007E2 RID: 2018
		public const string Aes256Encryption = "http://www.w3.org/2001/04/xmlenc#aes256-cbc";

		// Token: 0x040007E3 RID: 2019
		public const string Aes256KeyWrap = "http://www.w3.org/2001/04/xmlenc#kw-aes256";

		// Token: 0x040007E4 RID: 2020
		public const string DesEncryption = "http://www.w3.org/2001/04/xmlenc#des-cbc";

		// Token: 0x040007E5 RID: 2021
		public const string DsaSha1Signature = "http://www.w3.org/2000/09/xmldsig#dsa-sha1";

		// Token: 0x040007E6 RID: 2022
		public const string ExclusiveC14n = "http://www.w3.org/2001/10/xml-exc-c14n#";

		// Token: 0x040007E7 RID: 2023
		public const string ExclusiveC14nWithComments = "http://www.w3.org/2001/10/xml-exc-c14n#WithComments";

		// Token: 0x040007E8 RID: 2024
		public const string HmacSha1Signature = "http://www.w3.org/2000/09/xmldsig#hmac-sha1";

		// Token: 0x040007E9 RID: 2025
		public const string HmacSha256Signature = "http://www.w3.org/2001/04/xmldsig-more#hmac-sha256";

		// Token: 0x040007EA RID: 2026
		public const string Psha1KeyDerivation = "http://schemas.xmlsoap.org/ws/2005/02/sc/dk/p_sha1";

		// Token: 0x040007EB RID: 2027
		public const string Ripemd160Digest = "http://www.w3.org/2001/04/xmlenc#ripemd160";

		// Token: 0x040007EC RID: 2028
		public const string RsaOaepKeyWrap = "http://www.w3.org/2001/04/xmlenc#rsa-oaep-mgf1p";

		// Token: 0x040007ED RID: 2029
		public const string RsaSha1Signature = "http://www.w3.org/2000/09/xmldsig#rsa-sha1";

		// Token: 0x040007EE RID: 2030
		public const string RsaSha256Signature = "http://www.w3.org/2001/04/xmldsig-more#rsa-sha256";

		// Token: 0x040007EF RID: 2031
		public const string RsaV15KeyWrap = "http://www.w3.org/2001/04/xmlenc#rsa-1_5";

		// Token: 0x040007F0 RID: 2032
		public const string Sha1Digest = "http://www.w3.org/2000/09/xmldsig#sha1";

		// Token: 0x040007F1 RID: 2033
		public const string Sha256Digest = "http://www.w3.org/2001/04/xmlenc#sha256";

		// Token: 0x040007F2 RID: 2034
		public const string Sha512Digest = "http://www.w3.org/2001/04/xmlenc#sha512";

		// Token: 0x040007F3 RID: 2035
		public const string TripleDesEncryption = "http://www.w3.org/2001/04/xmlenc#tripledes-cbc";

		// Token: 0x040007F4 RID: 2036
		public const string TripleDesKeyWrap = "http://www.w3.org/2001/04/xmlenc#kw-tripledes";

		// Token: 0x040007F5 RID: 2037
		public const string TlsSspiKeyWrap = "http://schemas.xmlsoap.org/2005/02/trust/tlsnego#TLS_Wrap";

		// Token: 0x040007F6 RID: 2038
		public const string WindowsSspiKeyWrap = "http://schemas.xmlsoap.org/2005/02/trust/spnego#GSS_Wrap";

		// Token: 0x040007F7 RID: 2039
		public const string StrTransform = "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-soap-message-security-1.0#STR-Transform";
	}
}
