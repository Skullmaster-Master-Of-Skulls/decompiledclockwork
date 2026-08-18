using System;
using System.Xml;

namespace System.ServiceModel
{
	// Token: 0x0200006E RID: 110
	internal class SecurityJan2004Dictionary
	{
		// Token: 0x0600026E RID: 622 RVA: 0x0000E5D0 File Offset: 0x0000C7D0
		public SecurityJan2004Dictionary(ServiceModelDictionary dictionary)
		{
			this.SecurityTokenReference = dictionary.CreateString("SecurityTokenReference", 30);
			this.Namespace = dictionary.CreateString("http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd", 36);
			this.Security = dictionary.CreateString("Security", 52);
			this.ValueType = dictionary.CreateString("ValueType", 58);
			this.TypeAttribute = dictionary.CreateString("Type", 59);
			this.Prefix = dictionary.CreateString("o", 164);
			this.NonceElement = dictionary.CreateString("Nonce", 40);
			this.PasswordElement = dictionary.CreateString("Password", 165);
			this.PasswordTextName = dictionary.CreateString("PasswordText", 166);
			this.UserNameElement = dictionary.CreateString("Username", 167);
			this.UserNameTokenElement = dictionary.CreateString("UsernameToken", 168);
			this.BinarySecurityToken = dictionary.CreateString("BinarySecurityToken", 169);
			this.EncodingType = dictionary.CreateString("EncodingType", 170);
			this.Reference = dictionary.CreateString("Reference", 12);
			this.URI = dictionary.CreateString("URI", 11);
			this.KeyIdentifier = dictionary.CreateString("KeyIdentifier", 171);
			this.EncodingTypeValueBase64Binary = dictionary.CreateString("http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-soap-message-security-1.0#Base64Binary", 172);
			this.EncodingTypeValueHexBinary = dictionary.CreateString("http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-soap-message-security-1.0#HexBinary", 173);
			this.EncodingTypeValueText = dictionary.CreateString("http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-soap-message-security-1.0#Text", 174);
			this.X509SKIValueType = dictionary.CreateString("http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-x509-token-profile-1.0#X509SubjectKeyIdentifier", 175);
			this.KerberosTokenTypeGSS = dictionary.CreateString("http://docs.oasis-open.org/wss/oasis-wss-kerberos-token-profile-1.1#GSS_Kerberosv5_AP_REQ", 176);
			this.KerberosTokenType1510 = dictionary.CreateString("http://docs.oasis-open.org/wss/oasis-wss-kerberos-token-profile-1.1#GSS_Kerberosv5_AP_REQ1510", 177);
			this.SamlAssertionIdValueType = dictionary.CreateString("http://docs.oasis-open.org/wss/oasis-wss-saml-token-profile-1.0#SAMLAssertionID", 178);
			this.SamlAssertion = dictionary.CreateString("Assertion", 179);
			this.SamlUri = dictionary.CreateString("urn:oasis:names:tc:SAML:1.0:assertion", 180);
			this.RelAssertionValueType = dictionary.CreateString("http://docs.oasis-open.org/wss/oasis-wss-rel-token-profile-1.0.pdf#license", 181);
			this.FailedAuthenticationFaultCode = dictionary.CreateString("FailedAuthentication", 182);
			this.InvalidSecurityTokenFaultCode = dictionary.CreateString("InvalidSecurityToken", 183);
			this.InvalidSecurityFaultCode = dictionary.CreateString("InvalidSecurity", 184);
			this.KerberosHashValueType = dictionary.CreateString("http://docs.oasis-open.org/wss/oasis-wss-kerberos-token-profile-1.1#Kerberosv5APREQSHA1", 427);
		}

		// Token: 0x040005D6 RID: 1494
		public XmlDictionaryString SecurityTokenReference;

		// Token: 0x040005D7 RID: 1495
		public XmlDictionaryString Namespace;

		// Token: 0x040005D8 RID: 1496
		public XmlDictionaryString Security;

		// Token: 0x040005D9 RID: 1497
		public XmlDictionaryString ValueType;

		// Token: 0x040005DA RID: 1498
		public XmlDictionaryString TypeAttribute;

		// Token: 0x040005DB RID: 1499
		public XmlDictionaryString Prefix;

		// Token: 0x040005DC RID: 1500
		public XmlDictionaryString NonceElement;

		// Token: 0x040005DD RID: 1501
		public XmlDictionaryString PasswordElement;

		// Token: 0x040005DE RID: 1502
		public XmlDictionaryString PasswordTextName;

		// Token: 0x040005DF RID: 1503
		public XmlDictionaryString UserNameElement;

		// Token: 0x040005E0 RID: 1504
		public XmlDictionaryString UserNameTokenElement;

		// Token: 0x040005E1 RID: 1505
		public XmlDictionaryString BinarySecurityToken;

		// Token: 0x040005E2 RID: 1506
		public XmlDictionaryString EncodingType;

		// Token: 0x040005E3 RID: 1507
		public XmlDictionaryString Reference;

		// Token: 0x040005E4 RID: 1508
		public XmlDictionaryString URI;

		// Token: 0x040005E5 RID: 1509
		public XmlDictionaryString KeyIdentifier;

		// Token: 0x040005E6 RID: 1510
		public XmlDictionaryString EncodingTypeValueBase64Binary;

		// Token: 0x040005E7 RID: 1511
		public XmlDictionaryString EncodingTypeValueHexBinary;

		// Token: 0x040005E8 RID: 1512
		public XmlDictionaryString EncodingTypeValueText;

		// Token: 0x040005E9 RID: 1513
		public XmlDictionaryString X509SKIValueType;

		// Token: 0x040005EA RID: 1514
		public XmlDictionaryString KerberosTokenTypeGSS;

		// Token: 0x040005EB RID: 1515
		public XmlDictionaryString KerberosTokenType1510;

		// Token: 0x040005EC RID: 1516
		public XmlDictionaryString SamlAssertionIdValueType;

		// Token: 0x040005ED RID: 1517
		public XmlDictionaryString SamlAssertion;

		// Token: 0x040005EE RID: 1518
		public XmlDictionaryString SamlUri;

		// Token: 0x040005EF RID: 1519
		public XmlDictionaryString RelAssertionValueType;

		// Token: 0x040005F0 RID: 1520
		public XmlDictionaryString FailedAuthenticationFaultCode;

		// Token: 0x040005F1 RID: 1521
		public XmlDictionaryString InvalidSecurityTokenFaultCode;

		// Token: 0x040005F2 RID: 1522
		public XmlDictionaryString InvalidSecurityFaultCode;

		// Token: 0x040005F3 RID: 1523
		public XmlDictionaryString KerberosHashValueType;
	}
}
