using System;
using System.Xml;

namespace System.ServiceModel
{
	// Token: 0x0200006F RID: 111
	internal class SecurityXXX2005Dictionary
	{
		// Token: 0x0600026F RID: 623 RVA: 0x0000E860 File Offset: 0x0000CA60
		public SecurityXXX2005Dictionary(ServiceModelDictionary dictionary)
		{
			this.EncryptedHeader = dictionary.CreateString("EncryptedHeader", 60);
			this.Namespace = dictionary.CreateString("http://docs.oasis-open.org/wss/oasis-wss-wssecurity-secext-1.1.xsd", 61);
			this.Prefix = dictionary.CreateString("k", 185);
			this.SignatureConfirmation = dictionary.CreateString("SignatureConfirmation", 186);
			this.ValueAttribute = dictionary.CreateString("Value", 77);
			this.TokenTypeAttribute = dictionary.CreateString("TokenType", 187);
			this.ThumbprintSha1ValueType = dictionary.CreateString("http://docs.oasis-open.org/wss/oasis-wss-soap-message-security-1.1#ThumbprintSHA1", 188);
			this.EncryptedKeyTokenType = dictionary.CreateString("http://docs.oasis-open.org/wss/oasis-wss-soap-message-security-1.1#EncryptedKey", 189);
			this.EncryptedKeyHashValueType = dictionary.CreateString("http://docs.oasis-open.org/wss/oasis-wss-soap-message-security-1.1#EncryptedKeySHA1", 190);
			this.SamlTokenType = dictionary.CreateString("http://docs.oasis-open.org/wss/oasis-wss-saml-token-profile-1.1#SAMLV1.1", 191);
			this.Saml20TokenType = dictionary.CreateString("http://docs.oasis-open.org/wss/oasis-wss-saml-token-profile-1.1#SAMLV2.0", 192);
			this.Saml11AssertionValueType = dictionary.CreateString("http://docs.oasis-open.org/wss/oasis-wss-saml-token-profile-1.1#SAMLID", 193);
		}

		// Token: 0x040005F4 RID: 1524
		public XmlDictionaryString EncryptedHeader;

		// Token: 0x040005F5 RID: 1525
		public XmlDictionaryString Namespace;

		// Token: 0x040005F6 RID: 1526
		public XmlDictionaryString Prefix;

		// Token: 0x040005F7 RID: 1527
		public XmlDictionaryString SignatureConfirmation;

		// Token: 0x040005F8 RID: 1528
		public XmlDictionaryString ValueAttribute;

		// Token: 0x040005F9 RID: 1529
		public XmlDictionaryString TokenTypeAttribute;

		// Token: 0x040005FA RID: 1530
		public XmlDictionaryString ThumbprintSha1ValueType;

		// Token: 0x040005FB RID: 1531
		public XmlDictionaryString EncryptedKeyTokenType;

		// Token: 0x040005FC RID: 1532
		public XmlDictionaryString EncryptedKeyHashValueType;

		// Token: 0x040005FD RID: 1533
		public XmlDictionaryString SamlTokenType;

		// Token: 0x040005FE RID: 1534
		public XmlDictionaryString Saml20TokenType;

		// Token: 0x040005FF RID: 1535
		public XmlDictionaryString Saml11AssertionValueType;
	}
}
