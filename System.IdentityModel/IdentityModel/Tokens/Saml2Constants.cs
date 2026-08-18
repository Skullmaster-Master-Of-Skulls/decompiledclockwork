using System;

namespace System.IdentityModel.Tokens
{
	// Token: 0x0200013C RID: 316
	internal static class Saml2Constants
	{
		// Token: 0x04000B50 RID: 2896
		public const string Prefix = "saml";

		// Token: 0x04000B51 RID: 2897
		public const string Namespace = "urn:oasis:names:tc:SAML:2.0:assertion";

		// Token: 0x02000260 RID: 608
		internal static class ActionNamespaces
		{
			// Token: 0x04001024 RID: 4132
			public static readonly Uri Rwedc = new Uri("urn:oasis:names:tc:SAML:1.0:action:rwedc");

			// Token: 0x04001025 RID: 4133
			public static readonly Uri RwedcNegation = new Uri("urn:oasis:names:tc:SAML:1.0:action:rwedc-negation");

			// Token: 0x04001026 RID: 4134
			public static readonly Uri Ghpp = new Uri("urn:oasis:names:tc:SAML:1.0:action:ghpp");

			// Token: 0x04001027 RID: 4135
			public static readonly Uri Unix = new Uri("urn:oasis:names:tc:SAML:1.0:action:unix");

			// Token: 0x04001028 RID: 4136
			public const string RwedcString = "urn:oasis:names:tc:SAML:1.0:action:rwedc";

			// Token: 0x04001029 RID: 4137
			public const string RwedcNegationString = "urn:oasis:names:tc:SAML:1.0:action:rwedc-negation";

			// Token: 0x0400102A RID: 4138
			public const string GhppString = "urn:oasis:names:tc:SAML:1.0:action:ghpp";

			// Token: 0x0400102B RID: 4139
			public const string UnixString = "urn:oasis:names:tc:SAML:1.0:action:unix";
		}

		// Token: 0x02000261 RID: 609
		internal static class Attributes
		{
			// Token: 0x0400102C RID: 4140
			public const string Address = "Address";

			// Token: 0x0400102D RID: 4141
			public const string AuthnInstant = "AuthnInstant";

			// Token: 0x0400102E RID: 4142
			public const string Count = "Count";

			// Token: 0x0400102F RID: 4143
			public const string Decision = "Decision";

			// Token: 0x04001030 RID: 4144
			public const string DNSName = "DNSName";

			// Token: 0x04001031 RID: 4145
			public const string Format = "Format";

			// Token: 0x04001032 RID: 4146
			public const string FriendlyName = "FriendlyName";

			// Token: 0x04001033 RID: 4147
			public const string ID = "ID";

			// Token: 0x04001034 RID: 4148
			public const string InResponseTo = "InResponseTo";

			// Token: 0x04001035 RID: 4149
			public const string IssueInstant = "IssueInstant";

			// Token: 0x04001036 RID: 4150
			public const string Method = "Method";

			// Token: 0x04001037 RID: 4151
			public const string Name = "Name";

			// Token: 0x04001038 RID: 4152
			public const string NameFormat = "NameFormat";

			// Token: 0x04001039 RID: 4153
			public const string NameQualifier = "NameQualifier";

			// Token: 0x0400103A RID: 4154
			public const string Namespace = "Namespace";

			// Token: 0x0400103B RID: 4155
			public const string NotBefore = "NotBefore";

			// Token: 0x0400103C RID: 4156
			public const string NotOnOrAfter = "NotOnOrAfter";

			// Token: 0x0400103D RID: 4157
			public const string OriginalIssuer = "OriginalIssuer";

			// Token: 0x0400103E RID: 4158
			public const string Recipient = "Recipient";

			// Token: 0x0400103F RID: 4159
			public const string Resource = "Resource";

			// Token: 0x04001040 RID: 4160
			public const string SessionIndex = "SessionIndex";

			// Token: 0x04001041 RID: 4161
			public const string SessionNotOnOrAfter = "SessionNotOnOrAfter";

			// Token: 0x04001042 RID: 4162
			public const string SPNameQualifier = "SPNameQualifier";

			// Token: 0x04001043 RID: 4163
			public const string SPProvidedID = "SPProvidedID";

			// Token: 0x04001044 RID: 4164
			public const string Version = "Version";
		}

		// Token: 0x02000262 RID: 610
		internal static class AuthenticationContextClasses
		{
			// Token: 0x04001045 RID: 4165
			public static readonly Uri InternetProtocol = new Uri("urn:oasis:names:tc:SAML:2.0:ac:classes:InternetProtocol");

			// Token: 0x04001046 RID: 4166
			public static readonly Uri InternetProtocolPassword = new Uri("urn:oasis:names:tc:SAML:2.0:ac:classes:InternetProtocolPassword");

			// Token: 0x04001047 RID: 4167
			public static readonly Uri Kerberos = new Uri("urn:oasis:names:tc:SAML:2.0:ac:classes:Kerberos");

			// Token: 0x04001048 RID: 4168
			public static readonly Uri MobileOneFactorUnregistered = new Uri("urn:oasis:names:tc:SAML:2.0:ac:classes:MobileOneFactorUnregistered");

			// Token: 0x04001049 RID: 4169
			public static readonly Uri MobileTwoFactorUnregistered = new Uri("urn:oasis:names:tc:SAML:2.0:ac:classes:MobileTwoFactorUnregistered");

			// Token: 0x0400104A RID: 4170
			public static readonly Uri MobileOneFactorContract = new Uri("urn:oasis:names:tc:SAML:2.0:ac:classes:MobileOneFactorContract");

			// Token: 0x0400104B RID: 4171
			public static readonly Uri MobileTwoFactorContract = new Uri("urn:oasis:names:tc:SAML:2.0:ac:classes:MobileTwoFactorContract");

			// Token: 0x0400104C RID: 4172
			public static readonly Uri Password = new Uri("urn:oasis:names:tc:SAML:2.0:ac:classes:Password");

			// Token: 0x0400104D RID: 4173
			public static readonly Uri PasswordProtectedTransport = new Uri("urn:oasis:names:tc:SAML:2.0:ac:classes:PasswordProtectedTransport");

			// Token: 0x0400104E RID: 4174
			public static readonly Uri PreviousSession = new Uri("urn:oasis:names:tc:SAML:2.0:ac:classes:PreviousSession");

			// Token: 0x0400104F RID: 4175
			public static readonly Uri X509 = new Uri("urn:oasis:names:tc:SAML:2.0:ac:classes:X509");

			// Token: 0x04001050 RID: 4176
			public static readonly Uri Pgp = new Uri("urn:oasis:names:tc:SAML:2.0:ac:classes:PGP");

			// Token: 0x04001051 RID: 4177
			public static readonly Uri Spki = new Uri("urn:oasis:names:tc:SAML:2.0:ac:classes:SPKI");

			// Token: 0x04001052 RID: 4178
			public static readonly Uri XmlDSig = new Uri("urn:oasis:names:tc:SAML:2.0:ac:classes:XMLDSig");

			// Token: 0x04001053 RID: 4179
			public static readonly Uri Smartcard = new Uri("urn:oasis:names:tc:SAML:2.0:ac:classes:Smartcard");

			// Token: 0x04001054 RID: 4180
			public static readonly Uri SmartcardPki = new Uri("urn:oasis:names:tc:SAML:2.0:ac:classes:SmartcardPKI");

			// Token: 0x04001055 RID: 4181
			public static readonly Uri SoftwarePki = new Uri("urn:oasis:names:tc:SAML:2.0:ac:classes:SoftwarePKI");

			// Token: 0x04001056 RID: 4182
			public static readonly Uri Telephony = new Uri("urn:oasis:names:tc:SAML:2.0:ac:classes:Telephony");

			// Token: 0x04001057 RID: 4183
			public static readonly Uri NomadTelephony = new Uri("urn:oasis:names:tc:SAML:2.0:ac:classes:NomadTelephony");

			// Token: 0x04001058 RID: 4184
			public static readonly Uri PersonalTelephony = new Uri("urn:oasis:names:tc:SAML:2.0:ac:classes:PersonalTelephony");

			// Token: 0x04001059 RID: 4185
			public static readonly Uri AuthenticatedTelephony = new Uri("urn:oasis:names:tc:SAML:2.0:ac:classes:AuthenticatedTelephony");

			// Token: 0x0400105A RID: 4186
			public static readonly Uri SecureRemotePassword = new Uri("urn:oasis:names:tc:SAML:2.0:ac:classes:SecureRemotePassword");

			// Token: 0x0400105B RID: 4187
			public static readonly Uri TlsClient = new Uri("urn:oasis:names:tc:SAML:2.0:ac:classes:TLSClient");

			// Token: 0x0400105C RID: 4188
			public static readonly Uri TimeSyncToken = new Uri("urn:oasis:names:tc:SAML:2.0:ac:classes:TimeSyncToken");

			// Token: 0x0400105D RID: 4189
			public static readonly Uri Unspecified = new Uri("urn:oasis:names:tc:SAML:2.0:ac:classes:Unspecified");

			// Token: 0x0400105E RID: 4190
			public const string InternetProtocolString = "urn:oasis:names:tc:SAML:2.0:ac:classes:InternetProtocol";

			// Token: 0x0400105F RID: 4191
			public const string InternetProtocolPasswordString = "urn:oasis:names:tc:SAML:2.0:ac:classes:InternetProtocolPassword";

			// Token: 0x04001060 RID: 4192
			public const string KerberosString = "urn:oasis:names:tc:SAML:2.0:ac:classes:Kerberos";

			// Token: 0x04001061 RID: 4193
			public const string MobileOneFactorUnregisteredString = "urn:oasis:names:tc:SAML:2.0:ac:classes:MobileOneFactorUnregistered";

			// Token: 0x04001062 RID: 4194
			public const string MobileTwoFactorUnregisteredString = "urn:oasis:names:tc:SAML:2.0:ac:classes:MobileTwoFactorUnregistered";

			// Token: 0x04001063 RID: 4195
			public const string MobileOneFactorContractString = "urn:oasis:names:tc:SAML:2.0:ac:classes:MobileOneFactorContract";

			// Token: 0x04001064 RID: 4196
			public const string MobileTwoFactorContractString = "urn:oasis:names:tc:SAML:2.0:ac:classes:MobileTwoFactorContract";

			// Token: 0x04001065 RID: 4197
			public const string PasswordString = "urn:oasis:names:tc:SAML:2.0:ac:classes:Password";

			// Token: 0x04001066 RID: 4198
			public const string PasswordProtectedTransportString = "urn:oasis:names:tc:SAML:2.0:ac:classes:PasswordProtectedTransport";

			// Token: 0x04001067 RID: 4199
			public const string PreviousSessionString = "urn:oasis:names:tc:SAML:2.0:ac:classes:PreviousSession";

			// Token: 0x04001068 RID: 4200
			public const string X509String = "urn:oasis:names:tc:SAML:2.0:ac:classes:X509";

			// Token: 0x04001069 RID: 4201
			public const string PgpString = "urn:oasis:names:tc:SAML:2.0:ac:classes:PGP";

			// Token: 0x0400106A RID: 4202
			public const string SpkiString = "urn:oasis:names:tc:SAML:2.0:ac:classes:SPKI";

			// Token: 0x0400106B RID: 4203
			public const string XmlDsigString = "urn:oasis:names:tc:SAML:2.0:ac:classes:XMLDSig";

			// Token: 0x0400106C RID: 4204
			public const string SecureRempotePasswordString = "urn:oasis:names:tc:SAML:2.0:ac:classes:SecureRemotePassword";

			// Token: 0x0400106D RID: 4205
			public const string SmartcardString = "urn:oasis:names:tc:SAML:2.0:ac:classes:Smartcard";

			// Token: 0x0400106E RID: 4206
			public const string SmartcardPkiString = "urn:oasis:names:tc:SAML:2.0:ac:classes:SmartcardPKI";

			// Token: 0x0400106F RID: 4207
			public const string SoftwarePkiString = "urn:oasis:names:tc:SAML:2.0:ac:classes:SoftwarePKI";

			// Token: 0x04001070 RID: 4208
			public const string TelephonyString = "urn:oasis:names:tc:SAML:2.0:ac:classes:Telephony";

			// Token: 0x04001071 RID: 4209
			public const string NomadTelephonyString = "urn:oasis:names:tc:SAML:2.0:ac:classes:NomadTelephony";

			// Token: 0x04001072 RID: 4210
			public const string PersonalTelephonyString = "urn:oasis:names:tc:SAML:2.0:ac:classes:PersonalTelephony";

			// Token: 0x04001073 RID: 4211
			public const string AuthenticatedTelephonyString = "urn:oasis:names:tc:SAML:2.0:ac:classes:AuthenticatedTelephony";

			// Token: 0x04001074 RID: 4212
			public const string SecureRemotePasswordString = "urn:oasis:names:tc:SAML:2.0:ac:classes:SecureRemotePassword";

			// Token: 0x04001075 RID: 4213
			public const string TlsClientString = "urn:oasis:names:tc:SAML:2.0:ac:classes:TLSClient";

			// Token: 0x04001076 RID: 4214
			public const string TimeSyncTokenString = "urn:oasis:names:tc:SAML:2.0:ac:classes:TimeSyncToken";

			// Token: 0x04001077 RID: 4215
			public const string UnspecifiedString = "urn:oasis:names:tc:SAML:2.0:ac:classes:Unspecified";

			// Token: 0x04001078 RID: 4216
			public const string WindowsString = "urn:federation:authentication:windows";
		}

		// Token: 0x02000263 RID: 611
		internal static class ConfirmationMethods
		{
			// Token: 0x04001079 RID: 4217
			public const string BearerString = "urn:oasis:names:tc:SAML:2.0:cm:bearer";

			// Token: 0x0400107A RID: 4218
			public const string HolderOfKeyString = "urn:oasis:names:tc:SAML:2.0:cm:holder-of-key";

			// Token: 0x0400107B RID: 4219
			public const string SenderVouchesString = "urn:oasis:names:tc:SAML:2.0:cm:sender-vouches";

			// Token: 0x0400107C RID: 4220
			public static readonly Uri Bearer = new Uri("urn:oasis:names:tc:SAML:2.0:cm:bearer");

			// Token: 0x0400107D RID: 4221
			public static readonly Uri HolderOfKey = new Uri("urn:oasis:names:tc:SAML:2.0:cm:holder-of-key");

			// Token: 0x0400107E RID: 4222
			public static readonly Uri SenderVouches = new Uri("urn:oasis:names:tc:SAML:2.0:cm:sender-vouches");
		}

		// Token: 0x02000264 RID: 612
		internal static class Elements
		{
			// Token: 0x0400107F RID: 4223
			public const string Action = "Action";

			// Token: 0x04001080 RID: 4224
			public const string Advice = "Advice";

			// Token: 0x04001081 RID: 4225
			public const string Assertion = "Assertion";

			// Token: 0x04001082 RID: 4226
			public const string AssertionIDRef = "AssertionIDRef";

			// Token: 0x04001083 RID: 4227
			public const string AssertionURIRef = "AssertionURIRef";

			// Token: 0x04001084 RID: 4228
			public const string Attribute = "Attribute";

			// Token: 0x04001085 RID: 4229
			public const string AttributeStatement = "AttributeStatement";

			// Token: 0x04001086 RID: 4230
			public const string AttributeValue = "AttributeValue";

			// Token: 0x04001087 RID: 4231
			public const string Audience = "Audience";

			// Token: 0x04001088 RID: 4232
			public const string AudienceRestriction = "AudienceRestriction";

			// Token: 0x04001089 RID: 4233
			public const string AuthenticatingAuthority = "AuthenticatingAuthority";

			// Token: 0x0400108A RID: 4234
			public const string AuthnContext = "AuthnContext";

			// Token: 0x0400108B RID: 4235
			public const string AuthnContextClassRef = "AuthnContextClassRef";

			// Token: 0x0400108C RID: 4236
			public const string AuthnContextDecl = "AuthnContextDecl";

			// Token: 0x0400108D RID: 4237
			public const string AuthnContextDeclRef = "AuthnContextDeclRef";

			// Token: 0x0400108E RID: 4238
			public const string AuthnStatement = "AuthnStatement";

			// Token: 0x0400108F RID: 4239
			public const string AuthzDecisionStatement = "AuthzDecisionStatement";

			// Token: 0x04001090 RID: 4240
			public const string BaseID = "BaseID";

			// Token: 0x04001091 RID: 4241
			public const string Condition = "Condition";

			// Token: 0x04001092 RID: 4242
			public const string Conditions = "Conditions";

			// Token: 0x04001093 RID: 4243
			public const string EncryptedAssertion = "EncryptedAssertion";

			// Token: 0x04001094 RID: 4244
			public const string EncryptedAttribute = "EncryptedAttribute";

			// Token: 0x04001095 RID: 4245
			public const string EncryptedID = "EncryptedID";

			// Token: 0x04001096 RID: 4246
			public const string Evidence = "Evidence";

			// Token: 0x04001097 RID: 4247
			public const string Issuer = "Issuer";

			// Token: 0x04001098 RID: 4248
			public const string NameID = "NameID";

			// Token: 0x04001099 RID: 4249
			public const string OneTimeUse = "OneTimeUse";

			// Token: 0x0400109A RID: 4250
			public const string ProxyRestricton = "ProxyRestriction";

			// Token: 0x0400109B RID: 4251
			public const string Statement = "Statement";

			// Token: 0x0400109C RID: 4252
			public const string Subject = "Subject";

			// Token: 0x0400109D RID: 4253
			public const string SubjectConfirmation = "SubjectConfirmation";

			// Token: 0x0400109E RID: 4254
			public const string SubjectConfirmationData = "SubjectConfirmationData";

			// Token: 0x0400109F RID: 4255
			public const string SubjectLocality = "SubjectLocality";
		}

		// Token: 0x02000265 RID: 613
		internal static class NameIdentifierFormats
		{
			// Token: 0x040010A0 RID: 4256
			public static readonly Uri Unspecified = new Uri("urn:oasis:names:tc:SAML:1.1:nameid-format:unspecified");

			// Token: 0x040010A1 RID: 4257
			public static readonly Uri EmailAddress = new Uri("urn:oasis:names:tc:SAML:1.1:nameid-format:emailAddress");

			// Token: 0x040010A2 RID: 4258
			public static readonly Uri X509SubjectName = new Uri("urn:oasis:names:tc:SAML:1.1:nameid-format:X509SubjectName");

			// Token: 0x040010A3 RID: 4259
			public static readonly Uri WindowsDomainQualifiedName = new Uri("urn:oasis:names:tc:SAML:1.1:nameid-format:WindowsDomainQualifiedName");

			// Token: 0x040010A4 RID: 4260
			public static readonly Uri Kerberos = new Uri("urn:oasis:names:tc:SAML:2.0:nameid-format:kerberos");

			// Token: 0x040010A5 RID: 4261
			public static readonly Uri Entity = new Uri("urn:oasis:names:tc:SAML:2.0:nameid-format:entity");

			// Token: 0x040010A6 RID: 4262
			public static readonly Uri Persistent = new Uri("urn:oasis:names:tc:SAML:2.0:nameid-format:persistent");

			// Token: 0x040010A7 RID: 4263
			public static readonly Uri Transient = new Uri("urn:oasis:names:tc:SAML:2.0:nameid-format:transient");

			// Token: 0x040010A8 RID: 4264
			public static readonly Uri Encrypted = new Uri("urn:oasis:names:tc:SAML:2.0:nameid-format:encrypted");

			// Token: 0x040010A9 RID: 4265
			public const string UnspecifiedString = "urn:oasis:names:tc:SAML:1.1:nameid-format:unspecified";

			// Token: 0x040010AA RID: 4266
			public const string EmailAddressString = "urn:oasis:names:tc:SAML:1.1:nameid-format:emailAddress";

			// Token: 0x040010AB RID: 4267
			public const string X509SubjectNameString = "urn:oasis:names:tc:SAML:1.1:nameid-format:X509SubjectName";

			// Token: 0x040010AC RID: 4268
			public const string WindowsDomainQualifiedNameString = "urn:oasis:names:tc:SAML:1.1:nameid-format:WindowsDomainQualifiedName";

			// Token: 0x040010AD RID: 4269
			public const string KerberosString = "urn:oasis:names:tc:SAML:2.0:nameid-format:kerberos";

			// Token: 0x040010AE RID: 4270
			public const string EntityString = "urn:oasis:names:tc:SAML:2.0:nameid-format:entity";

			// Token: 0x040010AF RID: 4271
			public const string PersistentString = "urn:oasis:names:tc:SAML:2.0:nameid-format:persistent";

			// Token: 0x040010B0 RID: 4272
			public const string TransientString = "urn:oasis:names:tc:SAML:2.0:nameid-format:transient";

			// Token: 0x040010B1 RID: 4273
			public const string EncryptedString = "urn:oasis:names:tc:SAML:2.0:nameid-format:encrypted";
		}

		// Token: 0x02000266 RID: 614
		internal static class Types
		{
			// Token: 0x040010B2 RID: 4274
			public const string ActionType = "ActionType";

			// Token: 0x040010B3 RID: 4275
			public const string AdviceType = "AdviceType";

			// Token: 0x040010B4 RID: 4276
			public const string AssertionType = "AssertionType";

			// Token: 0x040010B5 RID: 4277
			public const string AttributeStatementType = "AttributeStatementType";

			// Token: 0x040010B6 RID: 4278
			public const string AttributeType = "AttributeType";

			// Token: 0x040010B7 RID: 4279
			public const string AudienceRestrictionType = "AudienceRestrictionType";

			// Token: 0x040010B8 RID: 4280
			public const string AuthnContextType = "AuthnContextType";

			// Token: 0x040010B9 RID: 4281
			public const string AuthnStatementType = "AuthnStatementType";

			// Token: 0x040010BA RID: 4282
			public const string AuthzDecisionStatementType = "AuthzDecisionStatementType";

			// Token: 0x040010BB RID: 4283
			public const string BaseIDAbstractType = "BaseIDAbstractType";

			// Token: 0x040010BC RID: 4284
			public const string ConditionAbstractType = "ConditionAbstractType";

			// Token: 0x040010BD RID: 4285
			public const string ConditionsType = "ConditionsType";

			// Token: 0x040010BE RID: 4286
			public const string EncryptedElementType = "EncryptedElementType";

			// Token: 0x040010BF RID: 4287
			public const string EvidenceType = "EvidenceType";

			// Token: 0x040010C0 RID: 4288
			public const string KeyInfoConfirmationDataType = "KeyInfoConfirmationDataType";

			// Token: 0x040010C1 RID: 4289
			public const string NameIDType = "NameIDType";

			// Token: 0x040010C2 RID: 4290
			public const string OneTimeUseType = "OneTimeUseType";

			// Token: 0x040010C3 RID: 4291
			public const string ProxyRestrictionType = "ProxyRestrictionType";

			// Token: 0x040010C4 RID: 4292
			public const string SubjectType = "SubjectType";

			// Token: 0x040010C5 RID: 4293
			public const string SubjectConfirmationDataType = "SubjectConfirmationDataType";

			// Token: 0x040010C6 RID: 4294
			public const string SubjectConfirmationType = "SubjectConfirmationType";

			// Token: 0x040010C7 RID: 4295
			public const string SubjectLocalityType = "SubjectLocalityType";

			// Token: 0x040010C8 RID: 4296
			public const string StatementAbstractType = "StatementAbstractType";
		}
	}
}
