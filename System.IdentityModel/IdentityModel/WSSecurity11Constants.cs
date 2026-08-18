using System;

namespace System.IdentityModel
{
	// Token: 0x020000BE RID: 190
	internal static class WSSecurity11Constants
	{
		// Token: 0x040004F6 RID: 1270
		public const string FragmentBaseAddress = "http://docs.oasis-open.org/wss/oasis-wss-soap-message-security-1.1";

		// Token: 0x040004F7 RID: 1271
		public const string Namespace = "http://docs.oasis-open.org/wss/oasis-wss-wssecurity-secext-1.1.xsd";

		// Token: 0x040004F8 RID: 1272
		public const string Prefix = "wsse11";

		// Token: 0x0200024A RID: 586
		public static class Attributes
		{
			// Token: 0x04000FA2 RID: 4002
			public const string TokenType = "TokenType";
		}

		// Token: 0x0200024B RID: 587
		public static class KeyTypes
		{
			// Token: 0x04000FA3 RID: 4003
			public const string CardSpaceV1Sha1Thumbprint = "http://docs.oasis-open.org/wss/2004/xx/oasis-2004xx-wss-soap-message-security-1.1#ThumbprintSHA1";

			// Token: 0x04000FA4 RID: 4004
			public const string Sha1Thumbprint = "http://docs.oasis-open.org/wss/oasis-wss-soap-message-security-1.1#ThumbprintSHA1";
		}
	}
}
