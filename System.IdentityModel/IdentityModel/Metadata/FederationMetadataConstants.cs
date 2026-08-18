using System;

namespace System.IdentityModel.Metadata
{
	// Token: 0x020000F3 RID: 243
	internal static class FederationMetadataConstants
	{
		// Token: 0x04000A6C RID: 2668
		public const string Namespace = "http://docs.oasis-open.org/wsfed/federation/200706";

		// Token: 0x04000A6D RID: 2669
		public const string Prefix = "fed";

		// Token: 0x02000257 RID: 599
		public static class Elements
		{
			// Token: 0x04000FE8 RID: 4072
			public const string ClaimTypesOffered = "ClaimTypesOffered";

			// Token: 0x04000FE9 RID: 4073
			public const string ClaimTypesRequested = "ClaimTypesRequested";

			// Token: 0x04000FEA RID: 4074
			public const string TargetScopes = "TargetScopes";

			// Token: 0x04000FEB RID: 4075
			public const string TokenTypesOffered = "TokenTypesOffered";

			// Token: 0x04000FEC RID: 4076
			public const string ApplicationServiceType = "ApplicationServiceType";

			// Token: 0x04000FED RID: 4077
			public const string SecurityTokenServiceType = "SecurityTokenServiceType";

			// Token: 0x04000FEE RID: 4078
			public const string ApplicationServiceEndpoint = "ApplicationServiceEndpoint";

			// Token: 0x04000FEF RID: 4079
			public const string PassiveRequestorEndpoint = "PassiveRequestorEndpoint";

			// Token: 0x04000FF0 RID: 4080
			public const string SecurityTokenServiceEndpoint = "SecurityTokenServiceEndpoint";
		}
	}
}
