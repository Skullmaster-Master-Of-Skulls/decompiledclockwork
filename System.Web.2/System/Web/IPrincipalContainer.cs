using System;
using System.Security.Principal;

namespace System.Web
{
	// Token: 0x02000049 RID: 73
	internal interface IPrincipalContainer
	{
		// Token: 0x1700025E RID: 606
		// (get) Token: 0x06000570 RID: 1392
		// (set) Token: 0x06000571 RID: 1393
		IPrincipal Principal { get; set; }
	}
}
