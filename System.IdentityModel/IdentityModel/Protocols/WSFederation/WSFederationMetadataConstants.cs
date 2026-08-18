using System;

namespace System.IdentityModel.Protocols.WSFederation
{
	// Token: 0x02000217 RID: 535
	internal static class WSFederationMetadataConstants
	{
		// Token: 0x04000EDD RID: 3805
		public const string Namespace = "http://docs.oasis-open.org/wsfed/federation/200706";

		// Token: 0x04000EDE RID: 3806
		public const string Prefix = "fed";

		// Token: 0x04000EDF RID: 3807
		public const string WSTransferAction = "http://schemas.xmlsoap.org/ws/2004/09/transfer/Get";

		// Token: 0x04000EE0 RID: 3808
		public const string WSTransferResponseAction = "http://schemas.xmlsoap.org/ws/2004/09/transfer/GetResponse";

		// Token: 0x04000EE1 RID: 3809
		public const string FederationMetadataHandler = "FederationMetadataHandler";

		// Token: 0x020002D0 RID: 720
		public static class Attributes
		{
			// Token: 0x04001275 RID: 4725
			public const string RealmName = "RealmName";

			// Token: 0x04001276 RID: 4726
			public const string FederationId = "FederationID";

			// Token: 0x04001277 RID: 4727
			public const string Uri = "Uri";

			// Token: 0x04001278 RID: 4728
			public const string Optional = "Optional";
		}

		// Token: 0x020002D1 RID: 721
		public static class Elements
		{
			// Token: 0x04001279 RID: 4729
			public const string AttributeServiceEndpoint = "AttributeServiceEndpoint";

			// Token: 0x0400127A RID: 4730
			public const string AutomaticPseudonyms = "AutomaticPseudonyms";

			// Token: 0x0400127B RID: 4731
			public const string ClaimTypesOffered = "ClaimTypesOffered";

			// Token: 0x0400127C RID: 4732
			public const string Federation = "Federation";

			// Token: 0x0400127D RID: 4733
			public const string FederationMetadata = "FederationMetadata";

			// Token: 0x0400127E RID: 4734
			public const string IssuerName = "IssuerName";

			// Token: 0x0400127F RID: 4735
			public const string IssuerNamesOffered = "IssuerNamesOffered";

			// Token: 0x04001280 RID: 4736
			public const string MetadataReference = "MetadataReference";

			// Token: 0x04001281 RID: 4737
			public const string PassiveRequestorEndpoints = "PassiveRequestorEndpoints";

			// Token: 0x04001282 RID: 4738
			public const string PseudonymServiceEndpoint = "PseudonymServiceEndpoint";

			// Token: 0x04001283 RID: 4739
			public const string SingleSignoutNotificationEndpoint = "SingleSignoutNotificationEndpoint";

			// Token: 0x04001284 RID: 4740
			public const string SingleSignOutSubscriptionEndpoint = "SingleSignOutSubscriptionEndpoint";

			// Token: 0x04001285 RID: 4741
			public const string TokenIssuerEndpoints = "TokenIssuerEndpoints";

			// Token: 0x04001286 RID: 4742
			public const string TokenIssuerName = "TokenIssuerName";

			// Token: 0x04001287 RID: 4743
			public const string TokenKeyTransferKeyInfo = "TokenKeyTransferKeyInfo";

			// Token: 0x04001288 RID: 4744
			public const string TokenSigningKeyInfo = "TokenSigningKeyInfo";

			// Token: 0x04001289 RID: 4745
			public const string TokenType = "TokenType";

			// Token: 0x0400128A RID: 4746
			public const string TokenTypesOffered = "TokenTypesOffered";
		}
	}
}
