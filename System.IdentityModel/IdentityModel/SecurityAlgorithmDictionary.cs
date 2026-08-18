using System;
using System.Xml;

namespace System.IdentityModel
{
	// Token: 0x020000C9 RID: 201
	internal class SecurityAlgorithmDictionary
	{
		// Token: 0x06000608 RID: 1544 RVA: 0x00016E60 File Offset: 0x00015060
		public SecurityAlgorithmDictionary(IdentityModelDictionary dictionary)
		{
			this.Aes128Encryption = dictionary.CreateString("http://www.w3.org/2001/04/xmlenc#aes128-cbc", 95);
			this.Aes128KeyWrap = dictionary.CreateString("http://www.w3.org/2001/04/xmlenc#kw-aes128", 96);
			this.Aes192Encryption = dictionary.CreateString("http://www.w3.org/2001/04/xmlenc#aes192-cbc", 97);
			this.Aes192KeyWrap = dictionary.CreateString("http://www.w3.org/2001/04/xmlenc#kw-aes192", 98);
			this.Aes256Encryption = dictionary.CreateString("http://www.w3.org/2001/04/xmlenc#aes256-cbc", 99);
			this.Aes256KeyWrap = dictionary.CreateString("http://www.w3.org/2001/04/xmlenc#kw-aes256", 100);
			this.DesEncryption = dictionary.CreateString("http://www.w3.org/2001/04/xmlenc#des-cbc", 101);
			this.DsaSha1Signature = dictionary.CreateString("http://www.w3.org/2000/09/xmldsig#dsa-sha1", 102);
			this.ExclusiveC14n = dictionary.CreateString("http://www.w3.org/2001/10/xml-exc-c14n#", 20);
			this.ExclusiveC14nWithComments = dictionary.CreateString("http://www.w3.org/2001/10/xml-exc-c14n#WithComments", 103);
			this.HmacSha1Signature = dictionary.CreateString("http://www.w3.org/2000/09/xmldsig#hmac-sha1", 104);
			this.HmacSha256Signature = dictionary.CreateString("http://www.w3.org/2001/04/xmldsig-more#hmac-sha256", 105);
			this.Psha1KeyDerivation = dictionary.CreateString("http://schemas.xmlsoap.org/ws/2005/02/sc/dk/p_sha1", 106);
			this.Ripemd160Digest = dictionary.CreateString("http://www.w3.org/2001/04/xmlenc#ripemd160", 107);
			this.RsaOaepKeyWrap = dictionary.CreateString("http://www.w3.org/2001/04/xmlenc#rsa-oaep-mgf1p", 108);
			this.RsaSha1Signature = dictionary.CreateString("http://www.w3.org/2000/09/xmldsig#rsa-sha1", 109);
			this.RsaSha256Signature = dictionary.CreateString("http://www.w3.org/2001/04/xmldsig-more#rsa-sha256", 110);
			this.RsaV15KeyWrap = dictionary.CreateString("http://www.w3.org/2001/04/xmlenc#rsa-1_5", 111);
			this.Sha1Digest = dictionary.CreateString("http://www.w3.org/2000/09/xmldsig#sha1", 112);
			this.Sha256Digest = dictionary.CreateString("http://www.w3.org/2001/04/xmlenc#sha256", 113);
			this.Sha512Digest = dictionary.CreateString("http://www.w3.org/2001/04/xmlenc#sha512", 114);
			this.TripleDesEncryption = dictionary.CreateString("http://www.w3.org/2001/04/xmlenc#tripledes-cbc", 115);
			this.TripleDesKeyWrap = dictionary.CreateString("http://www.w3.org/2001/04/xmlenc#kw-tripledes", 116);
			this.TlsSspiKeyWrap = dictionary.CreateString("http://schemas.xmlsoap.org/2005/02/trust/tlsnego#TLS_Wrap", 117);
			this.WindowsSspiKeyWrap = dictionary.CreateString("http://schemas.xmlsoap.org/2005/02/trust/spnego#GSS_Wrap", 118);
		}

		// Token: 0x06000609 RID: 1545 RVA: 0x00017050 File Offset: 0x00015250
		public SecurityAlgorithmDictionary(IXmlDictionary dictionary)
		{
			this.Aes128Encryption = this.LookupDictionaryString(dictionary, "http://www.w3.org/2001/04/xmlenc#aes128-cbc");
			this.Aes128KeyWrap = this.LookupDictionaryString(dictionary, "http://www.w3.org/2001/04/xmlenc#kw-aes128");
			this.Aes192Encryption = this.LookupDictionaryString(dictionary, "http://www.w3.org/2001/04/xmlenc#aes192-cbc");
			this.Aes192KeyWrap = this.LookupDictionaryString(dictionary, "http://www.w3.org/2001/04/xmlenc#kw-aes192");
			this.Aes256Encryption = this.LookupDictionaryString(dictionary, "http://www.w3.org/2001/04/xmlenc#aes256-cbc");
			this.Aes256KeyWrap = this.LookupDictionaryString(dictionary, "http://www.w3.org/2001/04/xmlenc#kw-aes256");
			this.DesEncryption = this.LookupDictionaryString(dictionary, "http://www.w3.org/2001/04/xmlenc#des-cbc");
			this.DsaSha1Signature = this.LookupDictionaryString(dictionary, "http://www.w3.org/2000/09/xmldsig#dsa-sha1");
			this.ExclusiveC14n = this.LookupDictionaryString(dictionary, "http://www.w3.org/2001/10/xml-exc-c14n#");
			this.ExclusiveC14nWithComments = this.LookupDictionaryString(dictionary, "http://www.w3.org/2001/10/xml-exc-c14n#WithComments");
			this.HmacSha1Signature = this.LookupDictionaryString(dictionary, "http://www.w3.org/2000/09/xmldsig#hmac-sha1");
			this.HmacSha256Signature = this.LookupDictionaryString(dictionary, "http://www.w3.org/2001/04/xmldsig-more#hmac-sha256");
			this.Psha1KeyDerivation = this.LookupDictionaryString(dictionary, "http://schemas.xmlsoap.org/ws/2005/02/sc/dk/p_sha1");
			this.Ripemd160Digest = this.LookupDictionaryString(dictionary, "http://www.w3.org/2001/04/xmlenc#ripemd160");
			this.RsaOaepKeyWrap = this.LookupDictionaryString(dictionary, "http://www.w3.org/2001/04/xmlenc#rsa-oaep-mgf1p");
			this.RsaSha1Signature = this.LookupDictionaryString(dictionary, "http://www.w3.org/2000/09/xmldsig#rsa-sha1");
			this.RsaSha256Signature = this.LookupDictionaryString(dictionary, "http://www.w3.org/2001/04/xmldsig-more#rsa-sha256");
			this.RsaV15KeyWrap = this.LookupDictionaryString(dictionary, "http://www.w3.org/2001/04/xmlenc#rsa-1_5");
			this.Sha1Digest = this.LookupDictionaryString(dictionary, "http://www.w3.org/2000/09/xmldsig#sha1");
			this.Sha256Digest = this.LookupDictionaryString(dictionary, "http://www.w3.org/2001/04/xmlenc#sha256");
			this.Sha512Digest = this.LookupDictionaryString(dictionary, "http://www.w3.org/2001/04/xmlenc#sha512");
			this.TripleDesEncryption = this.LookupDictionaryString(dictionary, "http://www.w3.org/2001/04/xmlenc#tripledes-cbc");
			this.TripleDesKeyWrap = this.LookupDictionaryString(dictionary, "http://www.w3.org/2001/04/xmlenc#kw-tripledes");
			this.TlsSspiKeyWrap = this.LookupDictionaryString(dictionary, "http://schemas.xmlsoap.org/2005/02/trust/tlsnego#TLS_Wrap");
			this.WindowsSspiKeyWrap = this.LookupDictionaryString(dictionary, "http://schemas.xmlsoap.org/2005/02/trust/spnego#GSS_Wrap");
		}

		// Token: 0x0600060A RID: 1546 RVA: 0x00017228 File Offset: 0x00015428
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

		// Token: 0x04000563 RID: 1379
		public XmlDictionaryString Aes128Encryption;

		// Token: 0x04000564 RID: 1380
		public XmlDictionaryString Aes128KeyWrap;

		// Token: 0x04000565 RID: 1381
		public XmlDictionaryString Aes192Encryption;

		// Token: 0x04000566 RID: 1382
		public XmlDictionaryString Aes192KeyWrap;

		// Token: 0x04000567 RID: 1383
		public XmlDictionaryString Aes256Encryption;

		// Token: 0x04000568 RID: 1384
		public XmlDictionaryString Aes256KeyWrap;

		// Token: 0x04000569 RID: 1385
		public XmlDictionaryString DesEncryption;

		// Token: 0x0400056A RID: 1386
		public XmlDictionaryString DsaSha1Signature;

		// Token: 0x0400056B RID: 1387
		public XmlDictionaryString ExclusiveC14n;

		// Token: 0x0400056C RID: 1388
		public XmlDictionaryString ExclusiveC14nWithComments;

		// Token: 0x0400056D RID: 1389
		public XmlDictionaryString HmacSha1Signature;

		// Token: 0x0400056E RID: 1390
		public XmlDictionaryString HmacSha256Signature;

		// Token: 0x0400056F RID: 1391
		public XmlDictionaryString Psha1KeyDerivation;

		// Token: 0x04000570 RID: 1392
		public XmlDictionaryString Ripemd160Digest;

		// Token: 0x04000571 RID: 1393
		public XmlDictionaryString RsaOaepKeyWrap;

		// Token: 0x04000572 RID: 1394
		public XmlDictionaryString RsaSha1Signature;

		// Token: 0x04000573 RID: 1395
		public XmlDictionaryString RsaSha256Signature;

		// Token: 0x04000574 RID: 1396
		public XmlDictionaryString RsaV15KeyWrap;

		// Token: 0x04000575 RID: 1397
		public XmlDictionaryString Sha1Digest;

		// Token: 0x04000576 RID: 1398
		public XmlDictionaryString Sha256Digest;

		// Token: 0x04000577 RID: 1399
		public XmlDictionaryString Sha512Digest;

		// Token: 0x04000578 RID: 1400
		public XmlDictionaryString TripleDesEncryption;

		// Token: 0x04000579 RID: 1401
		public XmlDictionaryString TripleDesKeyWrap;

		// Token: 0x0400057A RID: 1402
		public XmlDictionaryString TlsSspiKeyWrap;

		// Token: 0x0400057B RID: 1403
		public XmlDictionaryString WindowsSspiKeyWrap;
	}
}
