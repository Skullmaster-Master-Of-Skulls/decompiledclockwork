using System;

namespace System.Web.Helpers
{
	// Token: 0x02000034 RID: 52
	public interface IAntiForgeryAdditionalDataProvider
	{
		// Token: 0x06000171 RID: 369
		string GetAdditionalData(HttpContextBase context);

		// Token: 0x06000172 RID: 370
		bool ValidateAdditionalData(HttpContextBase context, string additionalData);
	}
}
