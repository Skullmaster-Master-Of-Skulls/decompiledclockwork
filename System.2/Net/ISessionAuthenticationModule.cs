using System;

namespace System.Net
{
	// Token: 0x020001B8 RID: 440
	internal interface ISessionAuthenticationModule : IAuthenticationModule
	{
		// Token: 0x0600114A RID: 4426
		bool Update(string challenge, WebRequest webRequest);

		// Token: 0x0600114B RID: 4427
		void ClearSession(WebRequest webRequest);

		// Token: 0x170003C1 RID: 961
		// (get) Token: 0x0600114C RID: 4428
		bool CanUseDefaultCredentials { get; }
	}
}
