using System;

namespace System.IdentityModel.Tokens
{
	// Token: 0x02000159 RID: 345
	public static class SamlConstants
	{
		// Token: 0x17000294 RID: 660
		// (get) Token: 0x06000A80 RID: 2688 RVA: 0x00002434 File Offset: 0x00000634
		public static int MajorVersionValue
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x17000295 RID: 661
		// (get) Token: 0x06000A81 RID: 2689 RVA: 0x00002434 File Offset: 0x00000634
		public static int MinorVersionValue
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x17000296 RID: 662
		// (get) Token: 0x06000A82 RID: 2690 RVA: 0x0002FD4A File Offset: 0x0002DF4A
		public static string Namespace
		{
			get
			{
				return "urn:oasis:names:tc:SAML:1.0:assertion";
			}
		}

		// Token: 0x17000297 RID: 663
		// (get) Token: 0x06000A83 RID: 2691 RVA: 0x0002FD51 File Offset: 0x0002DF51
		public static string HolderOfKey
		{
			get
			{
				return "urn:oasis:names:tc:SAML:1.0:cm:holder-of-key";
			}
		}

		// Token: 0x17000298 RID: 664
		// (get) Token: 0x06000A84 RID: 2692 RVA: 0x0002FD58 File Offset: 0x0002DF58
		public static string SenderVouches
		{
			get
			{
				return "urn:oasis:names:tc:SAML:1.0:cm:sender-vouches";
			}
		}

		// Token: 0x17000299 RID: 665
		// (get) Token: 0x06000A85 RID: 2693 RVA: 0x0002FD5F File Offset: 0x0002DF5F
		public static string UserName
		{
			get
			{
				return "UserName";
			}
		}

		// Token: 0x1700029A RID: 666
		// (get) Token: 0x06000A86 RID: 2694 RVA: 0x0002FD66 File Offset: 0x0002DF66
		public static string UserNameNamespace
		{
			get
			{
				return "urn:oasis:names:tc:SAML:1.1:nameid-format:WindowsDomainQualifiedName";
			}
		}

		// Token: 0x1700029B RID: 667
		// (get) Token: 0x06000A87 RID: 2695 RVA: 0x0002FD6D File Offset: 0x0002DF6D
		public static string EmailName
		{
			get
			{
				return "EmailName";
			}
		}

		// Token: 0x1700029C RID: 668
		// (get) Token: 0x06000A88 RID: 2696 RVA: 0x0002FD74 File Offset: 0x0002DF74
		public static string EmailNamespace
		{
			get
			{
				return "urn:oasis:names:tc:SAML:1.1:nameid-format:emailAddress";
			}
		}

		// Token: 0x04000BC5 RID: 3013
		public const string Prefix = "saml";

		// Token: 0x04000BC6 RID: 3014
		internal static string[] AcceptedDateTimeFormats = new string[]
		{
			"yyyy-MM-ddTHH:mm:ss.fffffffZ",
			"yyyy-MM-ddTHH:mm:ss.ffffffZ",
			"yyyy-MM-ddTHH:mm:ss.fffffZ",
			"yyyy-MM-ddTHH:mm:ss.ffffZ",
			"yyyy-MM-ddTHH:mm:ss.fffZ",
			"yyyy-MM-ddTHH:mm:ss.ffZ",
			"yyyy-MM-ddTHH:mm:ss.fZ",
			"yyyy-MM-ddTHH:mm:ssZ",
			"yyyy-MM-ddTHH:mm:ss.fffffffzzz",
			"yyyy-MM-ddTHH:mm:ss.ffffffzzz",
			"yyyy-MM-ddTHH:mm:ss.fffffzzz",
			"yyyy-MM-ddTHH:mm:ss.ffffzzz",
			"yyyy-MM-ddTHH:mm:ss.fffzzz",
			"yyyy-MM-ddTHH:mm:ss.ffzzz",
			"yyyy-MM-ddTHH:mm:ss.fzzz",
			"yyyy-MM-ddTHH:mm:sszzz"
		};

		// Token: 0x04000BC7 RID: 3015
		internal const string AssertionIdPrefix = "SamlSecurityToken-";

		// Token: 0x04000BC8 RID: 3016
		internal const string GeneratedDateTimeFormat = "yyyy-MM-ddTHH:mm:ss.fffZ";

		// Token: 0x0200026B RID: 619
		internal static class AuthenticationMethods
		{
			// Token: 0x040010D5 RID: 4309
			public const string HardwareTokenString = "URI:urn:oasis:names:tc:SAML:1.0:am:HardwareToken";

			// Token: 0x040010D6 RID: 4310
			public const string KerberosString = "urn:ietf:rfc:1510";

			// Token: 0x040010D7 RID: 4311
			public const string PasswordString = "urn:oasis:names:tc:SAML:1.0:am:password";

			// Token: 0x040010D8 RID: 4312
			public const string PgpString = "urn:oasis:names:tc:SAML:1.0:am:PGP";

			// Token: 0x040010D9 RID: 4313
			public const string SecureRemotePasswordString = "urn:ietf:rfc:2945";

			// Token: 0x040010DA RID: 4314
			public const string SignatureString = "urn:ietf:rfc:3075";

			// Token: 0x040010DB RID: 4315
			public const string SpkiString = "urn:oasis:names:tc:SAML:1.0:am:SPKI";

			// Token: 0x040010DC RID: 4316
			public const string TlsClientString = "urn:ietf:rfc:2246";

			// Token: 0x040010DD RID: 4317
			public const string UnspecifiedString = "urn:oasis:names:tc:SAML:1.0:am:unspecified";

			// Token: 0x040010DE RID: 4318
			public const string WindowsString = "urn:federation:authentication:windows";

			// Token: 0x040010DF RID: 4319
			public const string X509String = "urn:oasis:names:tc:SAML:1.0:am:X509-PKI";

			// Token: 0x040010E0 RID: 4320
			public const string XkmsString = "urn:oasis:names:tc:SAML:1.0:am:XKMS";
		}

		// Token: 0x0200026C RID: 620
		internal static class ElementNames
		{
			// Token: 0x040010E1 RID: 4321
			public const string Action = "Action";

			// Token: 0x040010E2 RID: 4322
			public const string Advice = "Advice";

			// Token: 0x040010E3 RID: 4323
			public const string Assertion = "Assertion";

			// Token: 0x040010E4 RID: 4324
			public const string AssertionIdReference = "AssertionIDReference";

			// Token: 0x040010E5 RID: 4325
			public const string Attribute = "Attribute";

			// Token: 0x040010E6 RID: 4326
			public const string AttributeStatement = "AttributeStatement";

			// Token: 0x040010E7 RID: 4327
			public const string AttributeValue = "AttributeValue";

			// Token: 0x040010E8 RID: 4328
			public const string Audience = "Audience";

			// Token: 0x040010E9 RID: 4329
			public const string AudienceRestrictionCondition = "AudienceRestrictionCondition";

			// Token: 0x040010EA RID: 4330
			public const string AuthenticationStatement = "AuthenticationStatement";

			// Token: 0x040010EB RID: 4331
			public const string AuthorityBinding = "AuthorityBinding";

			// Token: 0x040010EC RID: 4332
			public const string AuthorizationDecisionStatement = "AuthorizationDecisionStatement";

			// Token: 0x040010ED RID: 4333
			public const string Conditions = "Conditions";

			// Token: 0x040010EE RID: 4334
			public const string DoNotCacheCondition = "DoNotCacheCondition";

			// Token: 0x040010EF RID: 4335
			public const string Evidence = "Evidence";

			// Token: 0x040010F0 RID: 4336
			public const string NameIdentifier = "NameIdentifier";

			// Token: 0x040010F1 RID: 4337
			public const string SubjectConfirmation = "SubjectConfirmation";

			// Token: 0x040010F2 RID: 4338
			public const string Subject = "Subject";

			// Token: 0x040010F3 RID: 4339
			public const string SubjectConfirmationData = "SubjectConfirmationData";

			// Token: 0x040010F4 RID: 4340
			public const string SubjectConfirmationMethod = "ConfirmationMethod";

			// Token: 0x040010F5 RID: 4341
			public const string SubjectLocality = "SubjectLocality";
		}

		// Token: 0x0200026D RID: 621
		internal static class AttributeNames
		{
			// Token: 0x040010F6 RID: 4342
			public const string AssertionId = "AssertionID";

			// Token: 0x040010F7 RID: 4343
			public const string AttributeName = "AttributeName";

			// Token: 0x040010F8 RID: 4344
			public const string AttributeNamespace = "AttributeNamespace";

			// Token: 0x040010F9 RID: 4345
			public const string AuthenticationInstant = "AuthenticationInstant";

			// Token: 0x040010FA RID: 4346
			public const string AuthenticationMethod = "AuthenticationMethod";

			// Token: 0x040010FB RID: 4347
			public const string AuthorityBinding = "AuthorityBinding";

			// Token: 0x040010FC RID: 4348
			public const string AuthorityKind = "AuthorityKind";

			// Token: 0x040010FD RID: 4349
			public const string Binding = "Binding";

			// Token: 0x040010FE RID: 4350
			public const string Decision = "Decision";

			// Token: 0x040010FF RID: 4351
			public const string Issuer = "Issuer";

			// Token: 0x04001100 RID: 4352
			public const string IssueInstant = "IssueInstant";

			// Token: 0x04001101 RID: 4353
			public const string Location = "Location";

			// Token: 0x04001102 RID: 4354
			public const string MajorVersion = "MajorVersion";

			// Token: 0x04001103 RID: 4355
			public const string MinorVersion = "MinorVersion";

			// Token: 0x04001104 RID: 4356
			public const string OriginalIssuer = "OriginalIssuer";

			// Token: 0x04001105 RID: 4357
			public const string NamespaceAttributePrefix = "xmlns";

			// Token: 0x04001106 RID: 4358
			public const string NameIdentifierFormat = "Format";

			// Token: 0x04001107 RID: 4359
			public const string NameIdentifierNameQualifier = "NameQualifier";

			// Token: 0x04001108 RID: 4360
			public const string Namespace = "Namespace";

			// Token: 0x04001109 RID: 4361
			public const string NotBefore = "NotBefore";

			// Token: 0x0400110A RID: 4362
			public const string NotOnOrAfter = "NotOnOrAfter";

			// Token: 0x0400110B RID: 4363
			public const string Resource = "Resource";

			// Token: 0x0400110C RID: 4364
			public const string SubjectLocalityDNSAddress = "DNSAddress";

			// Token: 0x0400110D RID: 4365
			public const string SubjectLocalityIPAddress = "IPAddress";
		}
	}
}
