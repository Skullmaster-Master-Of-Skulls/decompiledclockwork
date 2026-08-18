using System;

namespace System.IdentityModel
{
	// Token: 0x020000BC RID: 188
	internal static class WSSecureConversationFeb2005Constants
	{
		// Token: 0x040004E7 RID: 1255
		public const string Namespace = "http://schemas.xmlsoap.org/ws/2005/02/sc";

		// Token: 0x040004E8 RID: 1256
		public static readonly Uri NamespaceUri = new Uri("http://schemas.xmlsoap.org/ws/2005/02/sc");

		// Token: 0x040004E9 RID: 1257
		public const string Prefix = "sc";

		// Token: 0x040004EA RID: 1258
		public const string TokenTypeURI = "http://schemas.xmlsoap.org/ws/2005/02/sc/sct";

		// Token: 0x040004EB RID: 1259
		public const int DefaultDerivedKeyLength = 32;

		// Token: 0x02000244 RID: 580
		public static class ElementNames
		{
			// Token: 0x04000F84 RID: 3972
			public const string Name = "SecurityContextToken";

			// Token: 0x04000F85 RID: 3973
			public const string Identifier = "Identifier";

			// Token: 0x04000F86 RID: 3974
			public const string Instance = "Instance";
		}

		// Token: 0x02000245 RID: 581
		public static class Attributes
		{
			// Token: 0x04000F87 RID: 3975
			public const string Length = "Length";

			// Token: 0x04000F88 RID: 3976
			public const string Nonce = "Nonce";

			// Token: 0x04000F89 RID: 3977
			public const string Instance = "Instance";
		}
	}
}
