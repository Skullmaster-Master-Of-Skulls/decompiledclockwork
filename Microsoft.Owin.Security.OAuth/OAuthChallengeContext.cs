using System;
using Microsoft.Owin.Security.Provider;

namespace Microsoft.Owin.Security.OAuth
{
	// Token: 0x02000026 RID: 38
	public class OAuthChallengeContext : BaseContext
	{
		// Token: 0x06000115 RID: 277 RVA: 0x00007155 File Offset: 0x00005355
		public OAuthChallengeContext(IOwinContext context, string challenge) : base(context)
		{
			this.Challenge = challenge;
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x06000116 RID: 278 RVA: 0x00007165 File Offset: 0x00005365
		// (set) Token: 0x06000117 RID: 279 RVA: 0x0000716D File Offset: 0x0000536D
		public string Challenge { get; protected set; }
	}
}
