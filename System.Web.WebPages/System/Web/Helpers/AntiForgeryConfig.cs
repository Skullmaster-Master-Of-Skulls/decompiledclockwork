using System;
using System.ComponentModel;
using System.Text;

namespace System.Web.Helpers
{
	// Token: 0x02000033 RID: 51
	public static class AntiForgeryConfig
	{
		// Token: 0x1700004A RID: 74
		// (get) Token: 0x06000163 RID: 355 RVA: 0x000052A0 File Offset: 0x000034A0
		// (set) Token: 0x06000164 RID: 356 RVA: 0x000052A7 File Offset: 0x000034A7
		public static IAntiForgeryAdditionalDataProvider AdditionalDataProvider { get; set; }

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x06000165 RID: 357 RVA: 0x000052AF File Offset: 0x000034AF
		// (set) Token: 0x06000166 RID: 358 RVA: 0x000052C7 File Offset: 0x000034C7
		public static string CookieName
		{
			get
			{
				if (AntiForgeryConfig._cookieName == null)
				{
					AntiForgeryConfig._cookieName = AntiForgeryConfig.GetAntiForgeryCookieName();
				}
				return AntiForgeryConfig._cookieName;
			}
			set
			{
				AntiForgeryConfig._cookieName = value;
			}
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x06000167 RID: 359 RVA: 0x000052CF File Offset: 0x000034CF
		// (set) Token: 0x06000168 RID: 360 RVA: 0x000052D6 File Offset: 0x000034D6
		public static bool RequireSsl { get; set; }

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x06000169 RID: 361 RVA: 0x000052DE File Offset: 0x000034DE
		// (set) Token: 0x0600016A RID: 362 RVA: 0x000052E5 File Offset: 0x000034E5
		public static bool SuppressXFrameOptionsHeader { get; set; }

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x0600016B RID: 363 RVA: 0x000052ED File Offset: 0x000034ED
		// (set) Token: 0x0600016C RID: 364 RVA: 0x000052F4 File Offset: 0x000034F4
		[EditorBrowsable(EditorBrowsableState.Never)]
		public static bool SuppressIdentityHeuristicChecks { get; set; }

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x0600016D RID: 365 RVA: 0x000052FC File Offset: 0x000034FC
		// (set) Token: 0x0600016E RID: 366 RVA: 0x0000530C File Offset: 0x0000350C
		public static string UniqueClaimTypeIdentifier
		{
			get
			{
				return AntiForgeryConfig._uniqueClaimTypeIdentifier ?? string.Empty;
			}
			set
			{
				AntiForgeryConfig._uniqueClaimTypeIdentifier = value;
			}
		}

		// Token: 0x0600016F RID: 367 RVA: 0x00005314 File Offset: 0x00003514
		private static string GetAntiForgeryCookieName()
		{
			return AntiForgeryConfig.GetAntiForgeryCookieName(HttpRuntime.AppDomainAppVirtualPath);
		}

		// Token: 0x06000170 RID: 368 RVA: 0x00005320 File Offset: 0x00003520
		internal static string GetAntiForgeryCookieName(string appPath)
		{
			if (string.IsNullOrEmpty(appPath) || appPath == "/")
			{
				return "__RequestVerificationToken";
			}
			return "__RequestVerificationToken_" + HttpServerUtility.UrlTokenEncode(Encoding.UTF8.GetBytes(appPath));
		}

		// Token: 0x04000071 RID: 113
		internal const string AntiForgeryTokenFieldName = "__RequestVerificationToken";

		// Token: 0x04000072 RID: 114
		private static string _cookieName;

		// Token: 0x04000073 RID: 115
		private static string _uniqueClaimTypeIdentifier;
	}
}
