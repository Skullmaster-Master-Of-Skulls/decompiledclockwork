using System;
using System.Xml;

namespace System.IdentityModel
{
	// Token: 0x020000CC RID: 204
	internal class SecurityXXX2005Dictionary
	{
		// Token: 0x06000611 RID: 1553 RVA: 0x000177B4 File Offset: 0x000159B4
		public SecurityXXX2005Dictionary(IdentityModelDictionary dictionary)
		{
			this.Prefix = dictionary.CreateString("k", 144);
			this.SignatureConfirmation = dictionary.CreateString("SignatureConfirmation", 145);
			this.ValueAttribute = dictionary.CreateString("Value", 146);
			this.TokenTypeAttribute = dictionary.CreateString("TokenType", 147);
			this.ThumbprintSha1ValueType = dictionary.CreateString("http://docs.oasis-open.org/wss/oasis-wss-soap-message-security-1.1#ThumbprintSHA1", 148);
			this.EncryptedKeyTokenType = dictionary.CreateString("http://docs.oasis-open.org/wss/oasis-wss-soap-message-security-1.1#EncryptedKey", 149);
			this.EncryptedKeyHashValueType = dictionary.CreateString("http://docs.oasis-open.org/wss/oasis-wss-soap-message-security-1.1#EncryptedKeySHA1", 150);
			this.SamlTokenType = dictionary.CreateString("http://docs.oasis-open.org/wss/oasis-wss-saml-token-profile-1.1#SAMLV1.1", 151);
			this.Saml20TokenType = dictionary.CreateString("http://docs.oasis-open.org/wss/oasis-wss-saml-token-profile-1.1#SAMLV2.0", 152);
			this.Saml11AssertionValueType = dictionary.CreateString("http://docs.oasis-open.org/wss/oasis-wss-saml-token-profile-1.1#SAMLID", 153);
			this.EncryptedHeader = dictionary.CreateString("EncryptedHeader", 154);
			this.Namespace = dictionary.CreateString("http://docs.oasis-open.org/wss/oasis-wss-wssecurity-secext-1.1.xsd", 155);
		}

		// Token: 0x06000612 RID: 1554 RVA: 0x000178D0 File Offset: 0x00015AD0
		public SecurityXXX2005Dictionary(IXmlDictionary dictionary)
		{
			this.Prefix = this.LookupDictionaryString(dictionary, "k");
			this.SignatureConfirmation = this.LookupDictionaryString(dictionary, "SignatureConfirmation");
			this.ValueAttribute = this.LookupDictionaryString(dictionary, "Value");
			this.TokenTypeAttribute = this.LookupDictionaryString(dictionary, "TokenType");
			this.ThumbprintSha1ValueType = this.LookupDictionaryString(dictionary, "http://docs.oasis-open.org/wss/oasis-wss-soap-message-security-1.1#ThumbprintSHA1");
			this.EncryptedKeyTokenType = this.LookupDictionaryString(dictionary, "http://docs.oasis-open.org/wss/oasis-wss-soap-message-security-1.1#EncryptedKey");
			this.EncryptedKeyHashValueType = this.LookupDictionaryString(dictionary, "http://docs.oasis-open.org/wss/oasis-wss-soap-message-security-1.1#EncryptedKeySHA1");
			this.SamlTokenType = this.LookupDictionaryString(dictionary, "http://docs.oasis-open.org/wss/oasis-wss-saml-token-profile-1.1#SAMLV1.1");
			this.Saml20TokenType = this.LookupDictionaryString(dictionary, "http://docs.oasis-open.org/wss/oasis-wss-saml-token-profile-1.1#SAMLV2.0");
			this.Saml11AssertionValueType = this.LookupDictionaryString(dictionary, "http://docs.oasis-open.org/wss/oasis-wss-saml-token-profile-1.1#SAMLID");
			this.EncryptedHeader = this.LookupDictionaryString(dictionary, "EncryptedHeader");
			this.Namespace = this.LookupDictionaryString(dictionary, "http://docs.oasis-open.org/wss/oasis-wss-wssecurity-secext-1.1.xsd");
		}

		// Token: 0x06000613 RID: 1555 RVA: 0x000179BC File Offset: 0x00015BBC
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

		// Token: 0x0400059B RID: 1435
		public XmlDictionaryString Prefix;

		// Token: 0x0400059C RID: 1436
		public XmlDictionaryString SignatureConfirmation;

		// Token: 0x0400059D RID: 1437
		public XmlDictionaryString ValueAttribute;

		// Token: 0x0400059E RID: 1438
		public XmlDictionaryString TokenTypeAttribute;

		// Token: 0x0400059F RID: 1439
		public XmlDictionaryString ThumbprintSha1ValueType;

		// Token: 0x040005A0 RID: 1440
		public XmlDictionaryString EncryptedKeyTokenType;

		// Token: 0x040005A1 RID: 1441
		public XmlDictionaryString EncryptedKeyHashValueType;

		// Token: 0x040005A2 RID: 1442
		public XmlDictionaryString SamlTokenType;

		// Token: 0x040005A3 RID: 1443
		public XmlDictionaryString Saml20TokenType;

		// Token: 0x040005A4 RID: 1444
		public XmlDictionaryString Saml11AssertionValueType;

		// Token: 0x040005A5 RID: 1445
		public XmlDictionaryString EncryptedHeader;

		// Token: 0x040005A6 RID: 1446
		public XmlDictionaryString Namespace;
	}
}
