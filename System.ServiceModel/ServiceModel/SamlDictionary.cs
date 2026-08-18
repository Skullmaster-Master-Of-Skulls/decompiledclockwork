using System;
using System.Xml;

namespace System.ServiceModel
{
	// Token: 0x02000069 RID: 105
	internal class SamlDictionary
	{
		// Token: 0x06000268 RID: 616 RVA: 0x0000DB00 File Offset: 0x0000BD00
		public SamlDictionary(ServiceModelDictionary dictionary)
		{
			this.Access = dictionary.CreateString("Access", 251);
			this.AccessDecision = dictionary.CreateString("AccessDecision", 252);
			this.Action = dictionary.CreateString("Action", 5);
			this.Advice = dictionary.CreateString("Advice", 253);
			this.Assertion = dictionary.CreateString("Assertion", 179);
			this.AssertionId = dictionary.CreateString("AssertionID", 254);
			this.AssertionIdReference = dictionary.CreateString("AssertionIDReference", 255);
			this.Attribute = dictionary.CreateString("Attribute", 256);
			this.AttributeName = dictionary.CreateString("AttributeName", 257);
			this.AttributeNamespace = dictionary.CreateString("AttributeNamespace", 258);
			this.AttributeStatement = dictionary.CreateString("AttributeStatement", 259);
			this.AttributeValue = dictionary.CreateString("AttributeValue", 260);
			this.Audience = dictionary.CreateString("Audience", 261);
			this.AudienceRestrictionCondition = dictionary.CreateString("AudienceRestrictionCondition", 262);
			this.AuthenticationInstant = dictionary.CreateString("AuthenticationInstant", 263);
			this.AuthenticationMethod = dictionary.CreateString("AuthenticationMethod", 264);
			this.AuthenticationStatement = dictionary.CreateString("AuthenticationStatement", 265);
			this.AuthorityBinding = dictionary.CreateString("AuthorityBinding", 266);
			this.AuthorityKind = dictionary.CreateString("AuthorityKind", 267);
			this.AuthorizationDecisionStatement = dictionary.CreateString("AuthorizationDecisionStatement", 268);
			this.Binding = dictionary.CreateString("Binding", 269);
			this.Condition = dictionary.CreateString("Condition", 270);
			this.Conditions = dictionary.CreateString("Conditions", 271);
			this.Decision = dictionary.CreateString("Decision", 272);
			this.DoNotCacheCondition = dictionary.CreateString("DoNotCacheCondition", 273);
			this.Evidence = dictionary.CreateString("Evidence", 274);
			this.IssueInstant = dictionary.CreateString("IssueInstant", 275);
			this.Issuer = dictionary.CreateString("Issuer", 276);
			this.Location = dictionary.CreateString("Location", 277);
			this.MajorVersion = dictionary.CreateString("MajorVersion", 278);
			this.MinorVersion = dictionary.CreateString("MinorVersion", 279);
			this.Namespace = dictionary.CreateString("urn:oasis:names:tc:SAML:1.0:assertion", 180);
			this.NameIdentifier = dictionary.CreateString("NameIdentifier", 280);
			this.NameIdentifierFormat = dictionary.CreateString("Format", 281);
			this.NameIdentifierNameQualifier = dictionary.CreateString("NameQualifier", 282);
			this.ActionNamespaceAttribute = dictionary.CreateString("Namespace", 283);
			this.NotBefore = dictionary.CreateString("NotBefore", 284);
			this.NotOnOrAfter = dictionary.CreateString("NotOnOrAfter", 285);
			this.PreferredPrefix = dictionary.CreateString("saml", 286);
			this.Statement = dictionary.CreateString("Statement", 287);
			this.Subject = dictionary.CreateString("Subject", 288);
			this.SubjectConfirmation = dictionary.CreateString("SubjectConfirmation", 289);
			this.SubjectConfirmationData = dictionary.CreateString("SubjectConfirmationData", 290);
			this.SubjectConfirmationMethod = dictionary.CreateString("ConfirmationMethod", 291);
			this.HolderOfKey = dictionary.CreateString("urn:oasis:names:tc:SAML:1.0:cm:holder-of-key", 292);
			this.SenderVouches = dictionary.CreateString("urn:oasis:names:tc:SAML:1.0:cm:sender-vouches", 293);
			this.SubjectLocality = dictionary.CreateString("SubjectLocality", 294);
			this.SubjectLocalityDNSAddress = dictionary.CreateString("DNSAddress", 295);
			this.SubjectLocalityIPAddress = dictionary.CreateString("IPAddress", 296);
			this.SubjectStatement = dictionary.CreateString("SubjectStatement", 297);
			this.UnspecifiedAuthenticationMethod = dictionary.CreateString("urn:oasis:names:tc:SAML:1.0:am:unspecified", 298);
			this.NamespaceAttributePrefix = dictionary.CreateString("xmlns", 299);
			this.Resource = dictionary.CreateString("Resource", 300);
			this.UserName = dictionary.CreateString("UserName", 301);
			this.UserNameNamespace = dictionary.CreateString("urn:oasis:names:tc:SAML:1.1:nameid-format:WindowsDomainQualifiedName", 302);
			this.EmailName = dictionary.CreateString("EmailName", 303);
			this.EmailNamespace = dictionary.CreateString("urn:oasis:names:tc:SAML:1.1:nameid-format:emailAddress", 304);
		}

		// Token: 0x0400056C RID: 1388
		public XmlDictionaryString Access;

		// Token: 0x0400056D RID: 1389
		public XmlDictionaryString AccessDecision;

		// Token: 0x0400056E RID: 1390
		public XmlDictionaryString Action;

		// Token: 0x0400056F RID: 1391
		public XmlDictionaryString Advice;

		// Token: 0x04000570 RID: 1392
		public XmlDictionaryString Assertion;

		// Token: 0x04000571 RID: 1393
		public XmlDictionaryString AssertionId;

		// Token: 0x04000572 RID: 1394
		public XmlDictionaryString AssertionIdReference;

		// Token: 0x04000573 RID: 1395
		public XmlDictionaryString Attribute;

		// Token: 0x04000574 RID: 1396
		public XmlDictionaryString AttributeName;

		// Token: 0x04000575 RID: 1397
		public XmlDictionaryString AttributeNamespace;

		// Token: 0x04000576 RID: 1398
		public XmlDictionaryString AttributeStatement;

		// Token: 0x04000577 RID: 1399
		public XmlDictionaryString AttributeValue;

		// Token: 0x04000578 RID: 1400
		public XmlDictionaryString Audience;

		// Token: 0x04000579 RID: 1401
		public XmlDictionaryString AudienceRestrictionCondition;

		// Token: 0x0400057A RID: 1402
		public XmlDictionaryString AuthenticationInstant;

		// Token: 0x0400057B RID: 1403
		public XmlDictionaryString AuthenticationMethod;

		// Token: 0x0400057C RID: 1404
		public XmlDictionaryString AuthenticationStatement;

		// Token: 0x0400057D RID: 1405
		public XmlDictionaryString AuthorityBinding;

		// Token: 0x0400057E RID: 1406
		public XmlDictionaryString AuthorityKind;

		// Token: 0x0400057F RID: 1407
		public XmlDictionaryString AuthorizationDecisionStatement;

		// Token: 0x04000580 RID: 1408
		public XmlDictionaryString Binding;

		// Token: 0x04000581 RID: 1409
		public XmlDictionaryString Condition;

		// Token: 0x04000582 RID: 1410
		public XmlDictionaryString Conditions;

		// Token: 0x04000583 RID: 1411
		public XmlDictionaryString Decision;

		// Token: 0x04000584 RID: 1412
		public XmlDictionaryString DoNotCacheCondition;

		// Token: 0x04000585 RID: 1413
		public XmlDictionaryString Evidence;

		// Token: 0x04000586 RID: 1414
		public XmlDictionaryString IssueInstant;

		// Token: 0x04000587 RID: 1415
		public XmlDictionaryString Issuer;

		// Token: 0x04000588 RID: 1416
		public XmlDictionaryString Location;

		// Token: 0x04000589 RID: 1417
		public XmlDictionaryString MajorVersion;

		// Token: 0x0400058A RID: 1418
		public XmlDictionaryString MinorVersion;

		// Token: 0x0400058B RID: 1419
		public XmlDictionaryString Namespace;

		// Token: 0x0400058C RID: 1420
		public XmlDictionaryString NameIdentifier;

		// Token: 0x0400058D RID: 1421
		public XmlDictionaryString NameIdentifierFormat;

		// Token: 0x0400058E RID: 1422
		public XmlDictionaryString NameIdentifierNameQualifier;

		// Token: 0x0400058F RID: 1423
		public XmlDictionaryString ActionNamespaceAttribute;

		// Token: 0x04000590 RID: 1424
		public XmlDictionaryString NotBefore;

		// Token: 0x04000591 RID: 1425
		public XmlDictionaryString NotOnOrAfter;

		// Token: 0x04000592 RID: 1426
		public XmlDictionaryString PreferredPrefix;

		// Token: 0x04000593 RID: 1427
		public XmlDictionaryString Statement;

		// Token: 0x04000594 RID: 1428
		public XmlDictionaryString Subject;

		// Token: 0x04000595 RID: 1429
		public XmlDictionaryString SubjectConfirmation;

		// Token: 0x04000596 RID: 1430
		public XmlDictionaryString SubjectConfirmationData;

		// Token: 0x04000597 RID: 1431
		public XmlDictionaryString SubjectConfirmationMethod;

		// Token: 0x04000598 RID: 1432
		public XmlDictionaryString HolderOfKey;

		// Token: 0x04000599 RID: 1433
		public XmlDictionaryString SenderVouches;

		// Token: 0x0400059A RID: 1434
		public XmlDictionaryString SubjectLocality;

		// Token: 0x0400059B RID: 1435
		public XmlDictionaryString SubjectLocalityDNSAddress;

		// Token: 0x0400059C RID: 1436
		public XmlDictionaryString SubjectLocalityIPAddress;

		// Token: 0x0400059D RID: 1437
		public XmlDictionaryString SubjectStatement;

		// Token: 0x0400059E RID: 1438
		public XmlDictionaryString UnspecifiedAuthenticationMethod;

		// Token: 0x0400059F RID: 1439
		public XmlDictionaryString NamespaceAttributePrefix;

		// Token: 0x040005A0 RID: 1440
		public XmlDictionaryString Resource;

		// Token: 0x040005A1 RID: 1441
		public XmlDictionaryString UserName;

		// Token: 0x040005A2 RID: 1442
		public XmlDictionaryString UserNameNamespace;

		// Token: 0x040005A3 RID: 1443
		public XmlDictionaryString EmailName;

		// Token: 0x040005A4 RID: 1444
		public XmlDictionaryString EmailNamespace;
	}
}
