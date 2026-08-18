using System;
using System.Xml;

namespace System.ServiceModel
{
	// Token: 0x0200006D RID: 109
	internal class SecurityAlgorithmDictionary
	{
		// Token: 0x0600026D RID: 621 RVA: 0x0000E398 File Offset: 0x0000C598
		public SecurityAlgorithmDictionary(ServiceModelDictionary dictionary)
		{
			this.Aes128Encryption = dictionary.CreateString("http://www.w3.org/2001/04/xmlenc#aes128-cbc", 138);
			this.Aes128KeyWrap = dictionary.CreateString("http://www.w3.org/2001/04/xmlenc#kw-aes128", 139);
			this.Aes192Encryption = dictionary.CreateString("http://www.w3.org/2001/04/xmlenc#aes192-cbc", 140);
			this.Aes192KeyWrap = dictionary.CreateString("http://www.w3.org/2001/04/xmlenc#kw-aes192", 141);
			this.Aes256Encryption = dictionary.CreateString("http://www.w3.org/2001/04/xmlenc#aes256-cbc", 142);
			this.Aes256KeyWrap = dictionary.CreateString("http://www.w3.org/2001/04/xmlenc#kw-aes256", 143);
			this.DesEncryption = dictionary.CreateString("http://www.w3.org/2001/04/xmlenc#des-cbc", 144);
			this.DsaSha1Signature = dictionary.CreateString("http://www.w3.org/2000/09/xmldsig#dsa-sha1", 145);
			this.ExclusiveC14n = dictionary.CreateString("http://www.w3.org/2001/10/xml-exc-c14n#", 111);
			this.ExclusiveC14nWithComments = dictionary.CreateString("http://www.w3.org/2001/10/xml-exc-c14n#WithComments", 146);
			this.HmacSha1Signature = dictionary.CreateString("http://www.w3.org/2000/09/xmldsig#hmac-sha1", 147);
			this.HmacSha256Signature = dictionary.CreateString("http://www.w3.org/2001/04/xmldsig-more#hmac-sha256", 148);
			this.Psha1KeyDerivation = dictionary.CreateString("http://schemas.xmlsoap.org/ws/2005/02/sc/dk/p_sha1", 149);
			this.Ripemd160Digest = dictionary.CreateString("http://www.w3.org/2001/04/xmlenc#ripemd160", 150);
			this.RsaOaepKeyWrap = dictionary.CreateString("http://www.w3.org/2001/04/xmlenc#rsa-oaep-mgf1p", 151);
			this.RsaSha1Signature = dictionary.CreateString("http://www.w3.org/2000/09/xmldsig#rsa-sha1", 152);
			this.RsaSha256Signature = dictionary.CreateString("http://www.w3.org/2001/04/xmldsig-more#rsa-sha256", 153);
			this.RsaV15KeyWrap = dictionary.CreateString("http://www.w3.org/2001/04/xmlenc#rsa-1_5", 154);
			this.Sha1Digest = dictionary.CreateString("http://www.w3.org/2000/09/xmldsig#sha1", 155);
			this.Sha256Digest = dictionary.CreateString("http://www.w3.org/2001/04/xmlenc#sha256", 156);
			this.Sha512Digest = dictionary.CreateString("http://www.w3.org/2001/04/xmlenc#sha512", 157);
			this.TripleDesEncryption = dictionary.CreateString("http://www.w3.org/2001/04/xmlenc#tripledes-cbc", 158);
			this.TripleDesKeyWrap = dictionary.CreateString("http://www.w3.org/2001/04/xmlenc#kw-tripledes", 159);
			this.TlsSspiKeyWrap = dictionary.CreateString("http://schemas.xmlsoap.org/2005/02/trust/tlsnego#TLS_Wrap", 160);
			this.WindowsSspiKeyWrap = dictionary.CreateString("http://schemas.xmlsoap.org/2005/02/trust/spnego#GSS_Wrap", 161);
		}

		// Token: 0x040005BD RID: 1469
		public XmlDictionaryString Aes128Encryption;

		// Token: 0x040005BE RID: 1470
		public XmlDictionaryString Aes128KeyWrap;

		// Token: 0x040005BF RID: 1471
		public XmlDictionaryString Aes192Encryption;

		// Token: 0x040005C0 RID: 1472
		public XmlDictionaryString Aes192KeyWrap;

		// Token: 0x040005C1 RID: 1473
		public XmlDictionaryString Aes256Encryption;

		// Token: 0x040005C2 RID: 1474
		public XmlDictionaryString Aes256KeyWrap;

		// Token: 0x040005C3 RID: 1475
		public XmlDictionaryString DesEncryption;

		// Token: 0x040005C4 RID: 1476
		public XmlDictionaryString DsaSha1Signature;

		// Token: 0x040005C5 RID: 1477
		public XmlDictionaryString ExclusiveC14n;

		// Token: 0x040005C6 RID: 1478
		public XmlDictionaryString ExclusiveC14nWithComments;

		// Token: 0x040005C7 RID: 1479
		public XmlDictionaryString HmacSha1Signature;

		// Token: 0x040005C8 RID: 1480
		public XmlDictionaryString HmacSha256Signature;

		// Token: 0x040005C9 RID: 1481
		public XmlDictionaryString Psha1KeyDerivation;

		// Token: 0x040005CA RID: 1482
		public XmlDictionaryString Ripemd160Digest;

		// Token: 0x040005CB RID: 1483
		public XmlDictionaryString RsaOaepKeyWrap;

		// Token: 0x040005CC RID: 1484
		public XmlDictionaryString RsaSha1Signature;

		// Token: 0x040005CD RID: 1485
		public XmlDictionaryString RsaSha256Signature;

		// Token: 0x040005CE RID: 1486
		public XmlDictionaryString RsaV15KeyWrap;

		// Token: 0x040005CF RID: 1487
		public XmlDictionaryString Sha1Digest;

		// Token: 0x040005D0 RID: 1488
		public XmlDictionaryString Sha256Digest;

		// Token: 0x040005D1 RID: 1489
		public XmlDictionaryString Sha512Digest;

		// Token: 0x040005D2 RID: 1490
		public XmlDictionaryString TripleDesEncryption;

		// Token: 0x040005D3 RID: 1491
		public XmlDictionaryString TripleDesKeyWrap;

		// Token: 0x040005D4 RID: 1492
		public XmlDictionaryString TlsSspiKeyWrap;

		// Token: 0x040005D5 RID: 1493
		public XmlDictionaryString WindowsSspiKeyWrap;
	}
}
