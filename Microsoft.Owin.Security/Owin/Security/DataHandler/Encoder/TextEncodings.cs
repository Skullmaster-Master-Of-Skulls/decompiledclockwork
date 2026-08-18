using System;

namespace Microsoft.Owin.Security.DataHandler.Encoder
{
	// Token: 0x0200002E RID: 46
	public static class TextEncodings
	{
		// Token: 0x1700002D RID: 45
		// (get) Token: 0x060000BB RID: 187 RVA: 0x00004409 File Offset: 0x00002609
		public static ITextEncoder Base64
		{
			get
			{
				return TextEncodings.Base64Instance;
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x060000BC RID: 188 RVA: 0x00004410 File Offset: 0x00002610
		public static ITextEncoder Base64Url
		{
			get
			{
				return TextEncodings.Base64UrlInstance;
			}
		}

		// Token: 0x04000048 RID: 72
		private static readonly ITextEncoder Base64Instance = new Base64TextEncoder();

		// Token: 0x04000049 RID: 73
		private static readonly ITextEncoder Base64UrlInstance = new Base64UrlTextEncoder();
	}
}
