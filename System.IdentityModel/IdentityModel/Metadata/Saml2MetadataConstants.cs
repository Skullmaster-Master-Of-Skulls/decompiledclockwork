using System;

namespace System.IdentityModel.Metadata
{
	// Token: 0x02000103 RID: 259
	internal static class Saml2MetadataConstants
	{
		// Token: 0x04000A97 RID: 2711
		public const string Namespace = "urn:oasis:names:tc:SAML:2.0:metadata";

		// Token: 0x02000258 RID: 600
		public static class Attributes
		{
			// Token: 0x04000FF1 RID: 4081
			public const string Id = "ID";

			// Token: 0x04000FF2 RID: 4082
			public const string ContactType = "contactType";

			// Token: 0x04000FF3 RID: 4083
			public const string Algorithm = "Algorithm";

			// Token: 0x04000FF4 RID: 4084
			public const string Use = "use";

			// Token: 0x04000FF5 RID: 4085
			public const string Binding = "Binding";

			// Token: 0x04000FF6 RID: 4086
			public const string EndpointIndex = "index";

			// Token: 0x04000FF7 RID: 4087
			public const string EndpointIsDefault = "isDefault";

			// Token: 0x04000FF8 RID: 4088
			public const string Location = "Location";

			// Token: 0x04000FF9 RID: 4089
			public const string ResponseLocation = "ResponseLocation";

			// Token: 0x04000FFA RID: 4090
			public const string EntityId = "entityID";

			// Token: 0x04000FFB RID: 4091
			public const string ErrorUrl = "errorURL";

			// Token: 0x04000FFC RID: 4092
			public const string ProtocolsSupported = "protocolSupportEnumeration";

			// Token: 0x04000FFD RID: 4093
			public const string ValidUntil = "validUntil";

			// Token: 0x04000FFE RID: 4094
			public const string EntityGroupName = "Name";

			// Token: 0x04000FFF RID: 4095
			public const string ServiceDescription = "ServiceDescription";

			// Token: 0x04001000 RID: 4096
			public const string ServiceDisplayName = "ServiceDisplayName";

			// Token: 0x04001001 RID: 4097
			public const string WantAuthenticationRequestsSigned = "WantAuthnRequestsSigned";

			// Token: 0x04001002 RID: 4098
			public const string AuthenticationRequestsSigned = "AuthnRequestsSigned";

			// Token: 0x04001003 RID: 4099
			public const string WantAssertionsSigned = "WantAssertionsSigned";
		}

		// Token: 0x02000259 RID: 601
		public static class Elements
		{
			// Token: 0x04001004 RID: 4100
			public const string EntitiesDescriptor = "EntitiesDescriptor";

			// Token: 0x04001005 RID: 4101
			public const string EntityDescriptor = "EntityDescriptor";

			// Token: 0x04001006 RID: 4102
			public const string IdpssoDescriptor = "IDPSSODescriptor";

			// Token: 0x04001007 RID: 4103
			public const string RoleDescriptor = "RoleDescriptor";

			// Token: 0x04001008 RID: 4104
			public const string SpssoDescriptor = "SPSSODescriptor";

			// Token: 0x04001009 RID: 4105
			public const string Company = "Company";

			// Token: 0x0400100A RID: 4106
			public const string ContactPerson = "ContactPerson";

			// Token: 0x0400100B RID: 4107
			public const string EmailAddress = "EmailAddress";

			// Token: 0x0400100C RID: 4108
			public const string GivenName = "GivenName";

			// Token: 0x0400100D RID: 4109
			public const string Surname = "SurName";

			// Token: 0x0400100E RID: 4110
			public const string TelephoneNumber = "TelephoneNumber";

			// Token: 0x0400100F RID: 4111
			public const string Organization = "Organization";

			// Token: 0x04001010 RID: 4112
			public const string OrganizationDisplayName = "OrganizationDisplayName";

			// Token: 0x04001011 RID: 4113
			public const string OrganizationName = "OrganizationName";

			// Token: 0x04001012 RID: 4114
			public const string OrganizationUrl = "OrganizationURL";

			// Token: 0x04001013 RID: 4115
			public const string EncryptionMethod = "EncryptionMethod";

			// Token: 0x04001014 RID: 4116
			public const string KeyDescriptor = "KeyDescriptor";

			// Token: 0x04001015 RID: 4117
			public const string ArtifactResolutionService = "ArtifactResolutionService";

			// Token: 0x04001016 RID: 4118
			public const string NameIDFormat = "NameIDFormat";

			// Token: 0x04001017 RID: 4119
			public const string SingleLogoutService = "SingleLogoutService";

			// Token: 0x04001018 RID: 4120
			public const string SingleSignOnService = "SingleSignOnService";

			// Token: 0x04001019 RID: 4121
			public const string AssertionConsumerService = "AssertionConsumerService";
		}
	}
}
