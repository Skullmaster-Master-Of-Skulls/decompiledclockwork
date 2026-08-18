using System;
using System.Xml;

namespace System.IdentityModel
{
	// Token: 0x020000CB RID: 203
	internal class SecurityJan2004Dictionary
	{
		// Token: 0x0600060E RID: 1550 RVA: 0x000172D0 File Offset: 0x000154D0
		public SecurityJan2004Dictionary(IdentityModelDictionary dictionary)
		{
			this.Prefix = dictionary.CreateString("o", 119);
			this.NonceElement = dictionary.CreateString("Nonce", 120);
			this.PasswordElement = dictionary.CreateString("Password", 121);
			this.PasswordTextName = dictionary.CreateString("PasswordText", 122);
			this.UserNameElement = dictionary.CreateString("Username", 123);
			this.UserNameTokenElement = dictionary.CreateString("UsernameToken", 124);
			this.BinarySecurityToken = dictionary.CreateString("BinarySecurityToken", 125);
			this.EncodingType = dictionary.CreateString("EncodingType", 126);
			this.Reference = dictionary.CreateString("Reference", 2);
			this.URI = dictionary.CreateString("URI", 1);
			this.KeyIdentifier = dictionary.CreateString("KeyIdentifier", 127);
			this.EncodingTypeValueBase64Binary = dictionary.CreateString("http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-soap-message-security-1.0#Base64Binary", 128);
			this.EncodingTypeValueHexBinary = dictionary.CreateString("http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-soap-message-security-1.0#HexBinary", 129);
			this.EncodingTypeValueText = dictionary.CreateString("http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-soap-message-security-1.0#Text", 130);
			this.X509SKIValueType = dictionary.CreateString("http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-x509-token-profile-1.0#X509SubjectKeyIdentifier", 131);
			this.KerberosTokenTypeGSS = dictionary.CreateString("http://docs.oasis-open.org/wss/oasis-wss-kerberos-token-profile-1.1#GSS_Kerberosv5_AP_REQ", 132);
			this.KerberosTokenType1510 = dictionary.CreateString("http://docs.oasis-open.org/wss/oasis-wss-kerberos-token-profile-1.1#GSS_Kerberosv5_AP_REQ1510", 133);
			this.SamlAssertionIdValueType = dictionary.CreateString("http://docs.oasis-open.org/wss/oasis-wss-saml-token-profile-1.0#SAMLAssertionID", 134);
			this.SamlAssertion = dictionary.CreateString("Assertion", 28);
			this.SamlUri = dictionary.CreateString("urn:oasis:names:tc:SAML:1.0:assertion", 55);
			this.RelAssertionValueType = dictionary.CreateString("http://docs.oasis-open.org/wss/oasis-wss-rel-token-profile-1.0.pdf#license", 135);
			this.FailedAuthenticationFaultCode = dictionary.CreateString("FailedAuthentication", 136);
			this.InvalidSecurityTokenFaultCode = dictionary.CreateString("InvalidSecurityToken", 137);
			this.InvalidSecurityFaultCode = dictionary.CreateString("InvalidSecurity", 138);
			this.SecurityTokenReference = dictionary.CreateString("SecurityTokenReference", 139);
			this.Namespace = dictionary.CreateString("http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd", 140);
			this.Security = dictionary.CreateString("Security", 141);
			this.ValueType = dictionary.CreateString("ValueType", 142);
			this.TypeAttribute = dictionary.CreateString("Type", 83);
			this.KerberosHashValueType = dictionary.CreateString("http://docs.oasis-open.org/wss/oasis-wss-kerberos-token-profile-1.1#Kerberosv5APREQSHA1", 143);
		}

		// Token: 0x0600060F RID: 1551 RVA: 0x0001754C File Offset: 0x0001574C
		public SecurityJan2004Dictionary(IXmlDictionary dictionary)
		{
			this.Prefix = this.LookupDictionaryString(dictionary, "o");
			this.NonceElement = this.LookupDictionaryString(dictionary, "Nonce");
			this.PasswordElement = this.LookupDictionaryString(dictionary, "Password");
			this.PasswordTextName = this.LookupDictionaryString(dictionary, "PasswordText");
			this.UserNameElement = this.LookupDictionaryString(dictionary, "Username");
			this.UserNameTokenElement = this.LookupDictionaryString(dictionary, "UsernameToken");
			this.BinarySecurityToken = this.LookupDictionaryString(dictionary, "BinarySecurityToken");
			this.EncodingType = this.LookupDictionaryString(dictionary, "EncodingType");
			this.Reference = this.LookupDictionaryString(dictionary, "Reference");
			this.URI = this.LookupDictionaryString(dictionary, "URI");
			this.KeyIdentifier = this.LookupDictionaryString(dictionary, "KeyIdentifier");
			this.EncodingTypeValueBase64Binary = this.LookupDictionaryString(dictionary, "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-soap-message-security-1.0#Base64Binary");
			this.EncodingTypeValueHexBinary = this.LookupDictionaryString(dictionary, "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-soap-message-security-1.0#HexBinary");
			this.EncodingTypeValueText = this.LookupDictionaryString(dictionary, "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-soap-message-security-1.0#Text");
			this.X509SKIValueType = this.LookupDictionaryString(dictionary, "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-x509-token-profile-1.0#X509SubjectKeyIdentifier");
			this.KerberosTokenTypeGSS = this.LookupDictionaryString(dictionary, "http://docs.oasis-open.org/wss/oasis-wss-kerberos-token-profile-1.1#GSS_Kerberosv5_AP_REQ");
			this.KerberosTokenType1510 = this.LookupDictionaryString(dictionary, "http://docs.oasis-open.org/wss/oasis-wss-kerberos-token-profile-1.1#GSS_Kerberosv5_AP_REQ1510");
			this.SamlAssertionIdValueType = this.LookupDictionaryString(dictionary, "http://docs.oasis-open.org/wss/oasis-wss-saml-token-profile-1.0#SAMLAssertionID");
			this.SamlAssertion = this.LookupDictionaryString(dictionary, "Assertion");
			this.SamlUri = this.LookupDictionaryString(dictionary, "urn:oasis:names:tc:SAML:1.0:assertion");
			this.RelAssertionValueType = this.LookupDictionaryString(dictionary, "http://docs.oasis-open.org/wss/oasis-wss-rel-token-profile-1.0.pdf#license");
			this.FailedAuthenticationFaultCode = this.LookupDictionaryString(dictionary, "FailedAuthentication");
			this.InvalidSecurityTokenFaultCode = this.LookupDictionaryString(dictionary, "InvalidSecurityToken");
			this.InvalidSecurityFaultCode = this.LookupDictionaryString(dictionary, "InvalidSecurity");
			this.SecurityTokenReference = this.LookupDictionaryString(dictionary, "SecurityTokenReference");
			this.Namespace = this.LookupDictionaryString(dictionary, "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd");
			this.Security = this.LookupDictionaryString(dictionary, "Security");
			this.ValueType = this.LookupDictionaryString(dictionary, "ValueType");
			this.TypeAttribute = this.LookupDictionaryString(dictionary, "Type");
			this.KerberosHashValueType = this.LookupDictionaryString(dictionary, "http://docs.oasis-open.org/wss/oasis-wss-kerberos-token-profile-1.1#Kerberosv5APREQSHA1");
		}

		// Token: 0x06000610 RID: 1552 RVA: 0x0001777C File Offset: 0x0001597C
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

		// Token: 0x0400057D RID: 1405
		public XmlDictionaryString Prefix;

		// Token: 0x0400057E RID: 1406
		public XmlDictionaryString NonceElement;

		// Token: 0x0400057F RID: 1407
		public XmlDictionaryString PasswordElement;

		// Token: 0x04000580 RID: 1408
		public XmlDictionaryString PasswordTextName;

		// Token: 0x04000581 RID: 1409
		public XmlDictionaryString UserNameElement;

		// Token: 0x04000582 RID: 1410
		public XmlDictionaryString UserNameTokenElement;

		// Token: 0x04000583 RID: 1411
		public XmlDictionaryString BinarySecurityToken;

		// Token: 0x04000584 RID: 1412
		public XmlDictionaryString EncodingType;

		// Token: 0x04000585 RID: 1413
		public XmlDictionaryString Reference;

		// Token: 0x04000586 RID: 1414
		public XmlDictionaryString URI;

		// Token: 0x04000587 RID: 1415
		public XmlDictionaryString KeyIdentifier;

		// Token: 0x04000588 RID: 1416
		public XmlDictionaryString EncodingTypeValueBase64Binary;

		// Token: 0x04000589 RID: 1417
		public XmlDictionaryString EncodingTypeValueHexBinary;

		// Token: 0x0400058A RID: 1418
		public XmlDictionaryString EncodingTypeValueText;

		// Token: 0x0400058B RID: 1419
		public XmlDictionaryString X509SKIValueType;

		// Token: 0x0400058C RID: 1420
		public XmlDictionaryString KerberosTokenTypeGSS;

		// Token: 0x0400058D RID: 1421
		public XmlDictionaryString KerberosTokenType1510;

		// Token: 0x0400058E RID: 1422
		public XmlDictionaryString SamlAssertionIdValueType;

		// Token: 0x0400058F RID: 1423
		public XmlDictionaryString SamlAssertion;

		// Token: 0x04000590 RID: 1424
		public XmlDictionaryString SamlUri;

		// Token: 0x04000591 RID: 1425
		public XmlDictionaryString RelAssertionValueType;

		// Token: 0x04000592 RID: 1426
		public XmlDictionaryString FailedAuthenticationFaultCode;

		// Token: 0x04000593 RID: 1427
		public XmlDictionaryString InvalidSecurityTokenFaultCode;

		// Token: 0x04000594 RID: 1428
		public XmlDictionaryString InvalidSecurityFaultCode;

		// Token: 0x04000595 RID: 1429
		public XmlDictionaryString SecurityTokenReference;

		// Token: 0x04000596 RID: 1430
		public XmlDictionaryString Namespace;

		// Token: 0x04000597 RID: 1431
		public XmlDictionaryString Security;

		// Token: 0x04000598 RID: 1432
		public XmlDictionaryString ValueType;

		// Token: 0x04000599 RID: 1433
		public XmlDictionaryString TypeAttribute;

		// Token: 0x0400059A RID: 1434
		public XmlDictionaryString KerberosHashValueType;
	}
}
