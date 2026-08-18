using System;
using System.Xml;

namespace System.IdentityModel
{
	// Token: 0x020000C5 RID: 197
	internal class SamlDictionary
	{
		// Token: 0x060005FB RID: 1531 RVA: 0x00015D28 File Offset: 0x00013F28
		public SamlDictionary(IdentityModelDictionary dictionary)
		{
			this.Access = dictionary.CreateString("Access", 24);
			this.AccessDecision = dictionary.CreateString("AccessDecision", 25);
			this.Action = dictionary.CreateString("Action", 26);
			this.Advice = dictionary.CreateString("Advice", 27);
			this.Assertion = dictionary.CreateString("Assertion", 28);
			this.AssertionId = dictionary.CreateString("AssertionID", 29);
			this.AssertionIdReference = dictionary.CreateString("AssertionIDReference", 30);
			this.Attribute = dictionary.CreateString("Attribute", 31);
			this.AttributeName = dictionary.CreateString("AttributeName", 32);
			this.AttributeNamespace = dictionary.CreateString("AttributeNamespace", 33);
			this.AttributeStatement = dictionary.CreateString("AttributeStatement", 34);
			this.AttributeValue = dictionary.CreateString("AttributeValue", 35);
			this.Audience = dictionary.CreateString("Audience", 36);
			this.AudienceRestrictionCondition = dictionary.CreateString("AudienceRestrictionCondition", 37);
			this.AuthenticationInstant = dictionary.CreateString("AuthenticationInstant", 38);
			this.AuthenticationMethod = dictionary.CreateString("AuthenticationMethod", 39);
			this.AuthenticationStatement = dictionary.CreateString("AuthenticationStatement", 40);
			this.AuthorityBinding = dictionary.CreateString("AuthorityBinding", 41);
			this.AuthorityKind = dictionary.CreateString("AuthorityKind", 42);
			this.AuthorizationDecisionStatement = dictionary.CreateString("AuthorizationDecisionStatement", 43);
			this.Binding = dictionary.CreateString("Binding", 44);
			this.Condition = dictionary.CreateString("Condition", 45);
			this.Conditions = dictionary.CreateString("Conditions", 46);
			this.Decision = dictionary.CreateString("Decision", 47);
			this.DoNotCacheCondition = dictionary.CreateString("DoNotCacheCondition", 48);
			this.Evidence = dictionary.CreateString("Evidence", 49);
			this.IssueInstant = dictionary.CreateString("IssueInstant", 50);
			this.Issuer = dictionary.CreateString("Issuer", 51);
			this.Location = dictionary.CreateString("Location", 52);
			this.MajorVersion = dictionary.CreateString("MajorVersion", 53);
			this.MinorVersion = dictionary.CreateString("MinorVersion", 54);
			this.Namespace = dictionary.CreateString("urn:oasis:names:tc:SAML:1.0:assertion", 55);
			this.NameIdentifier = dictionary.CreateString("NameIdentifier", 56);
			this.NameIdentifierFormat = dictionary.CreateString("Format", 57);
			this.NameIdentifierNameQualifier = dictionary.CreateString("NameQualifier", 58);
			this.ActionNamespaceAttribute = dictionary.CreateString("Namespace", 59);
			this.NotBefore = dictionary.CreateString("NotBefore", 60);
			this.NotOnOrAfter = dictionary.CreateString("NotOnOrAfter", 61);
			this.PreferredPrefix = dictionary.CreateString("saml", 62);
			this.Statement = dictionary.CreateString("Statement", 63);
			this.Subject = dictionary.CreateString("Subject", 64);
			this.SubjectConfirmation = dictionary.CreateString("SubjectConfirmation", 65);
			this.SubjectConfirmationData = dictionary.CreateString("SubjectConfirmationData", 66);
			this.SubjectConfirmationMethod = dictionary.CreateString("ConfirmationMethod", 67);
			this.HolderOfKey = dictionary.CreateString("urn:oasis:names:tc:SAML:1.0:cm:holder-of-key", 68);
			this.SenderVouches = dictionary.CreateString("urn:oasis:names:tc:SAML:1.0:cm:sender-vouches", 69);
			this.SubjectLocality = dictionary.CreateString("SubjectLocality", 70);
			this.SubjectLocalityDNSAddress = dictionary.CreateString("DNSAddress", 71);
			this.SubjectLocalityIPAddress = dictionary.CreateString("IPAddress", 72);
			this.SubjectStatement = dictionary.CreateString("SubjectStatement", 73);
			this.UnspecifiedAuthenticationMethod = dictionary.CreateString("urn:oasis:names:tc:SAML:1.0:am:unspecified", 74);
			this.NamespaceAttributePrefix = dictionary.CreateString("xmlns", 75);
			this.Resource = dictionary.CreateString("Resource", 76);
			this.UserName = dictionary.CreateString("UserName", 77);
			this.UserNameNamespace = dictionary.CreateString("urn:oasis:names:tc:SAML:1.1:nameid-format:WindowsDomainQualifiedName", 78);
			this.EmailName = dictionary.CreateString("EmailName", 79);
			this.EmailNamespace = dictionary.CreateString("urn:oasis:names:tc:SAML:1.1:nameid-format:emailAddress", 80);
		}

		// Token: 0x060005FC RID: 1532 RVA: 0x00016178 File Offset: 0x00014378
		public SamlDictionary(IXmlDictionary dictionary)
		{
			this.Access = this.LookupDictionaryString(dictionary, "Access");
			this.AccessDecision = this.LookupDictionaryString(dictionary, "AccessDecision");
			this.Action = this.LookupDictionaryString(dictionary, "Action");
			this.Advice = this.LookupDictionaryString(dictionary, "Advice");
			this.Assertion = this.LookupDictionaryString(dictionary, "Assertion");
			this.AssertionId = this.LookupDictionaryString(dictionary, "AssertionID");
			this.AssertionIdReference = this.LookupDictionaryString(dictionary, "AssertionIDReference");
			this.Attribute = this.LookupDictionaryString(dictionary, "Attribute");
			this.AttributeName = this.LookupDictionaryString(dictionary, "AttributeName");
			this.AttributeNamespace = this.LookupDictionaryString(dictionary, "AttributeNamespace");
			this.AttributeStatement = this.LookupDictionaryString(dictionary, "AttributeStatement");
			this.AttributeValue = this.LookupDictionaryString(dictionary, "AttributeValue");
			this.Audience = this.LookupDictionaryString(dictionary, "Audience");
			this.AudienceRestrictionCondition = this.LookupDictionaryString(dictionary, "AudienceRestrictionCondition");
			this.AuthenticationInstant = this.LookupDictionaryString(dictionary, "AuthenticationInstant");
			this.AuthenticationMethod = this.LookupDictionaryString(dictionary, "AuthenticationMethod");
			this.AuthenticationStatement = this.LookupDictionaryString(dictionary, "AuthenticationStatement");
			this.AuthorityBinding = this.LookupDictionaryString(dictionary, "AuthorityBinding");
			this.AuthorityKind = this.LookupDictionaryString(dictionary, "AuthorityKind");
			this.AuthorizationDecisionStatement = this.LookupDictionaryString(dictionary, "AuthorizationDecisionStatement");
			this.Binding = this.LookupDictionaryString(dictionary, "Binding");
			this.Condition = this.LookupDictionaryString(dictionary, "Condition");
			this.Conditions = this.LookupDictionaryString(dictionary, "Conditions");
			this.Decision = this.LookupDictionaryString(dictionary, "Decision");
			this.DoNotCacheCondition = this.LookupDictionaryString(dictionary, "DoNotCacheCondition");
			this.Evidence = this.LookupDictionaryString(dictionary, "Evidence");
			this.IssueInstant = this.LookupDictionaryString(dictionary, "IssueInstant");
			this.Issuer = this.LookupDictionaryString(dictionary, "Issuer");
			this.Location = this.LookupDictionaryString(dictionary, "Location");
			this.MajorVersion = this.LookupDictionaryString(dictionary, "MajorVersion");
			this.MinorVersion = this.LookupDictionaryString(dictionary, "MinorVersion");
			this.Namespace = this.LookupDictionaryString(dictionary, "urn:oasis:names:tc:SAML:1.0:assertion");
			this.NameIdentifier = this.LookupDictionaryString(dictionary, "NameIdentifier");
			this.NameIdentifierFormat = this.LookupDictionaryString(dictionary, "Format");
			this.NameIdentifierNameQualifier = this.LookupDictionaryString(dictionary, "NameQualifier");
			this.ActionNamespaceAttribute = this.LookupDictionaryString(dictionary, "Namespace");
			this.NotBefore = this.LookupDictionaryString(dictionary, "NotBefore");
			this.NotOnOrAfter = this.LookupDictionaryString(dictionary, "NotOnOrAfter");
			this.PreferredPrefix = this.LookupDictionaryString(dictionary, "saml");
			this.Statement = this.LookupDictionaryString(dictionary, "Statement");
			this.Subject = this.LookupDictionaryString(dictionary, "Subject");
			this.SubjectConfirmation = this.LookupDictionaryString(dictionary, "SubjectConfirmation");
			this.SubjectConfirmationData = this.LookupDictionaryString(dictionary, "SubjectConfirmationData");
			this.SubjectConfirmationMethod = this.LookupDictionaryString(dictionary, "ConfirmationMethod");
			this.HolderOfKey = this.LookupDictionaryString(dictionary, "urn:oasis:names:tc:SAML:1.0:cm:holder-of-key");
			this.SenderVouches = this.LookupDictionaryString(dictionary, "urn:oasis:names:tc:SAML:1.0:cm:sender-vouches");
			this.SubjectLocality = this.LookupDictionaryString(dictionary, "SubjectLocality");
			this.SubjectLocalityDNSAddress = this.LookupDictionaryString(dictionary, "DNSAddress");
			this.SubjectLocalityIPAddress = this.LookupDictionaryString(dictionary, "IPAddress");
			this.SubjectStatement = this.LookupDictionaryString(dictionary, "SubjectStatement");
			this.UnspecifiedAuthenticationMethod = this.LookupDictionaryString(dictionary, "urn:oasis:names:tc:SAML:1.0:am:unspecified");
			this.NamespaceAttributePrefix = this.LookupDictionaryString(dictionary, "xmlns");
			this.Resource = this.LookupDictionaryString(dictionary, "Resource");
			this.UserName = this.LookupDictionaryString(dictionary, "UserName");
			this.UserNameNamespace = this.LookupDictionaryString(dictionary, "urn:oasis:names:tc:SAML:1.1:nameid-format:WindowsDomainQualifiedName");
			this.EmailName = this.LookupDictionaryString(dictionary, "EmailName");
			this.EmailNamespace = this.LookupDictionaryString(dictionary, "urn:oasis:names:tc:SAML:1.1:nameid-format:emailAddress");
		}

		// Token: 0x060005FD RID: 1533 RVA: 0x00016590 File Offset: 0x00014790
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

		// Token: 0x04000511 RID: 1297
		public XmlDictionaryString Access;

		// Token: 0x04000512 RID: 1298
		public XmlDictionaryString AccessDecision;

		// Token: 0x04000513 RID: 1299
		public XmlDictionaryString Action;

		// Token: 0x04000514 RID: 1300
		public XmlDictionaryString Advice;

		// Token: 0x04000515 RID: 1301
		public XmlDictionaryString Assertion;

		// Token: 0x04000516 RID: 1302
		public XmlDictionaryString AssertionId;

		// Token: 0x04000517 RID: 1303
		public XmlDictionaryString AssertionIdReference;

		// Token: 0x04000518 RID: 1304
		public XmlDictionaryString Attribute;

		// Token: 0x04000519 RID: 1305
		public XmlDictionaryString AttributeName;

		// Token: 0x0400051A RID: 1306
		public XmlDictionaryString AttributeNamespace;

		// Token: 0x0400051B RID: 1307
		public XmlDictionaryString AttributeStatement;

		// Token: 0x0400051C RID: 1308
		public XmlDictionaryString AttributeValue;

		// Token: 0x0400051D RID: 1309
		public XmlDictionaryString Audience;

		// Token: 0x0400051E RID: 1310
		public XmlDictionaryString AudienceRestrictionCondition;

		// Token: 0x0400051F RID: 1311
		public XmlDictionaryString AuthenticationInstant;

		// Token: 0x04000520 RID: 1312
		public XmlDictionaryString AuthenticationMethod;

		// Token: 0x04000521 RID: 1313
		public XmlDictionaryString AuthenticationStatement;

		// Token: 0x04000522 RID: 1314
		public XmlDictionaryString AuthorityBinding;

		// Token: 0x04000523 RID: 1315
		public XmlDictionaryString AuthorityKind;

		// Token: 0x04000524 RID: 1316
		public XmlDictionaryString AuthorizationDecisionStatement;

		// Token: 0x04000525 RID: 1317
		public XmlDictionaryString Binding;

		// Token: 0x04000526 RID: 1318
		public XmlDictionaryString Condition;

		// Token: 0x04000527 RID: 1319
		public XmlDictionaryString Conditions;

		// Token: 0x04000528 RID: 1320
		public XmlDictionaryString Decision;

		// Token: 0x04000529 RID: 1321
		public XmlDictionaryString DoNotCacheCondition;

		// Token: 0x0400052A RID: 1322
		public XmlDictionaryString Evidence;

		// Token: 0x0400052B RID: 1323
		public XmlDictionaryString IssueInstant;

		// Token: 0x0400052C RID: 1324
		public XmlDictionaryString Issuer;

		// Token: 0x0400052D RID: 1325
		public XmlDictionaryString Location;

		// Token: 0x0400052E RID: 1326
		public XmlDictionaryString MajorVersion;

		// Token: 0x0400052F RID: 1327
		public XmlDictionaryString MinorVersion;

		// Token: 0x04000530 RID: 1328
		public XmlDictionaryString Namespace;

		// Token: 0x04000531 RID: 1329
		public XmlDictionaryString NameIdentifier;

		// Token: 0x04000532 RID: 1330
		public XmlDictionaryString NameIdentifierFormat;

		// Token: 0x04000533 RID: 1331
		public XmlDictionaryString NameIdentifierNameQualifier;

		// Token: 0x04000534 RID: 1332
		public XmlDictionaryString ActionNamespaceAttribute;

		// Token: 0x04000535 RID: 1333
		public XmlDictionaryString NotBefore;

		// Token: 0x04000536 RID: 1334
		public XmlDictionaryString NotOnOrAfter;

		// Token: 0x04000537 RID: 1335
		public XmlDictionaryString PreferredPrefix;

		// Token: 0x04000538 RID: 1336
		public XmlDictionaryString Statement;

		// Token: 0x04000539 RID: 1337
		public XmlDictionaryString Subject;

		// Token: 0x0400053A RID: 1338
		public XmlDictionaryString SubjectConfirmation;

		// Token: 0x0400053B RID: 1339
		public XmlDictionaryString SubjectConfirmationData;

		// Token: 0x0400053C RID: 1340
		public XmlDictionaryString SubjectConfirmationMethod;

		// Token: 0x0400053D RID: 1341
		public XmlDictionaryString HolderOfKey;

		// Token: 0x0400053E RID: 1342
		public XmlDictionaryString SenderVouches;

		// Token: 0x0400053F RID: 1343
		public XmlDictionaryString SubjectLocality;

		// Token: 0x04000540 RID: 1344
		public XmlDictionaryString SubjectLocalityDNSAddress;

		// Token: 0x04000541 RID: 1345
		public XmlDictionaryString SubjectLocalityIPAddress;

		// Token: 0x04000542 RID: 1346
		public XmlDictionaryString SubjectStatement;

		// Token: 0x04000543 RID: 1347
		public XmlDictionaryString UnspecifiedAuthenticationMethod;

		// Token: 0x04000544 RID: 1348
		public XmlDictionaryString NamespaceAttributePrefix;

		// Token: 0x04000545 RID: 1349
		public XmlDictionaryString Resource;

		// Token: 0x04000546 RID: 1350
		public XmlDictionaryString UserName;

		// Token: 0x04000547 RID: 1351
		public XmlDictionaryString UserNameNamespace;

		// Token: 0x04000548 RID: 1352
		public XmlDictionaryString EmailName;

		// Token: 0x04000549 RID: 1353
		public XmlDictionaryString EmailNamespace;
	}
}
