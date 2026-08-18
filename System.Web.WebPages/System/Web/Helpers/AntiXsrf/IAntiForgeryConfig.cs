using System;

namespace System.Web.Helpers.AntiXsrf
{
	// Token: 0x0200002B RID: 43
	internal interface IAntiForgeryConfig
	{
		// Token: 0x17000035 RID: 53
		// (get) Token: 0x0600012B RID: 299
		IAntiForgeryAdditionalDataProvider AdditionalDataProvider { get; }

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x0600012C RID: 300
		string CookieName { get; }

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x0600012D RID: 301
		string FormFieldName { get; }

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x0600012E RID: 302
		bool RequireSSL { get; }

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x0600012F RID: 303
		bool SuppressIdentityHeuristicChecks { get; }

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x06000130 RID: 304
		string UniqueClaimTypeIdentifier { get; }

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x06000131 RID: 305
		bool SuppressXFrameOptionsHeader { get; }
	}
}
