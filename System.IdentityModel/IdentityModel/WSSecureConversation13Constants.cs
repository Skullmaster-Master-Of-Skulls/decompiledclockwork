using System;

namespace System.IdentityModel
{
	// Token: 0x020000BB RID: 187
	internal static class WSSecureConversation13Constants
	{
		// Token: 0x040004E2 RID: 1250
		public const string Namespace = "http://docs.oasis-open.org/ws-sx/ws-secureconversation/200512";

		// Token: 0x040004E3 RID: 1251
		public static readonly Uri NamespaceUri = new Uri("http://docs.oasis-open.org/ws-sx/ws-secureconversation/200512");

		// Token: 0x040004E4 RID: 1252
		public const string Prefix = "sc";

		// Token: 0x040004E5 RID: 1253
		public const string TokenTypeURI = "http://docs.oasis-open.org/ws-sx/ws-secureconversation/200512/sct";

		// Token: 0x040004E6 RID: 1254
		public const int DefaultDerivedKeyLength = 32;

		// Token: 0x02000242 RID: 578
		public static class ElementNames
		{
			// Token: 0x04000F7E RID: 3966
			public const string Name = "SecurityContextToken";

			// Token: 0x04000F7F RID: 3967
			public const string Identifier = "Identifier";

			// Token: 0x04000F80 RID: 3968
			public const string Instance = "Instance";
		}

		// Token: 0x02000243 RID: 579
		public static class Attributes
		{
			// Token: 0x04000F81 RID: 3969
			public const string Length = "Length";

			// Token: 0x04000F82 RID: 3970
			public const string Nonce = "Nonce";

			// Token: 0x04000F83 RID: 3971
			public const string Instance = "Instance";
		}
	}
}
