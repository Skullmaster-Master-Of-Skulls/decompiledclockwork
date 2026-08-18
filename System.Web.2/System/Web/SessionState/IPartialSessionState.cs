using System;
using System.Collections.Generic;

namespace System.Web.SessionState
{
	// Token: 0x0200011E RID: 286
	public interface IPartialSessionState
	{
		// Token: 0x1700057F RID: 1407
		// (get) Token: 0x06001183 RID: 4483
		IList<string> PartialSessionStateKeys { get; }
	}
}
