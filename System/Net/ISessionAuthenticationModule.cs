using System;

namespace System.Net
{
	// Token: 0x020004D4 RID: 1236
	internal interface ISessionAuthenticationModule : IAuthenticationModule
	{
		// Token: 0x06002674 RID: 9844
		bool Update(string challenge, WebRequest webRequest);

		// Token: 0x06002675 RID: 9845
		void ClearSession(WebRequest webRequest);

		// Token: 0x170007FC RID: 2044
		// (get) Token: 0x06002676 RID: 9846
		bool CanUseDefaultCredentials { get; }
	}
}
