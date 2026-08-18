using System;
using System.Runtime.Remoting.Messaging;
using System.Security.Permissions;

namespace System.Web.Hosting
{
	// Token: 0x020007A9 RID: 1961
	internal class ContextBase
	{
		// Token: 0x17001B23 RID: 6947
		// (get) Token: 0x06005D1C RID: 23836 RVA: 0x00142FDC File Offset: 0x001411DC
		// (set) Token: 0x06005D1D RID: 23837 RVA: 0x00142FE3 File Offset: 0x001411E3
		internal static object Current
		{
			get
			{
				return CallContext.HostContext;
			}
			[SecurityPermission(SecurityAction.Demand, Unrestricted = true)]
			set
			{
				CallContext.HostContext = value;
			}
		}

		// Token: 0x06005D1E RID: 23838 RVA: 0x00142FEC File Offset: 0x001411EC
		internal static object SwitchContext(object newContext)
		{
			object hostContext = CallContext.HostContext;
			if (hostContext != newContext)
			{
				CallContext.HostContext = newContext;
			}
			return hostContext;
		}
	}
}
