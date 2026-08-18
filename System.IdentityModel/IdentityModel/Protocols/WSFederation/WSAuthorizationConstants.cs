using System;

namespace System.IdentityModel.Protocols.WSFederation
{
	// Token: 0x02000216 RID: 534
	internal static class WSAuthorizationConstants
	{
		// Token: 0x04000ED9 RID: 3801
		public const string Prefix = "auth";

		// Token: 0x04000EDA RID: 3802
		public const string Namespace = "http://docs.oasis-open.org/wsfed/authorization/200706";

		// Token: 0x04000EDB RID: 3803
		public const string Dialect = "http://docs.oasis-open.org/wsfed/authorization/200706/authclaims";

		// Token: 0x04000EDC RID: 3804
		public const string Action = "http://docs.oasis-open.org/wsfed/authorization/200706/claims/action";

		// Token: 0x020002CE RID: 718
		public static class Attributes
		{
			// Token: 0x0400126D RID: 4717
			public const string Name = "Name";

			// Token: 0x0400126E RID: 4718
			public const string Scope = "Scope";
		}

		// Token: 0x020002CF RID: 719
		public static class Elements
		{
			// Token: 0x0400126F RID: 4719
			public const string AdditionalContext = "AdditionalContext";

			// Token: 0x04001270 RID: 4720
			public const string ClaimType = "ClaimType";

			// Token: 0x04001271 RID: 4721
			public const string ContextItem = "ContextItem";

			// Token: 0x04001272 RID: 4722
			public const string Description = "Description";

			// Token: 0x04001273 RID: 4723
			public const string DisplayName = "DisplayName";

			// Token: 0x04001274 RID: 4724
			public const string Value = "Value";
		}
	}
}
