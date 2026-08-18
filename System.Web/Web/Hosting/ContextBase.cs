using System;
using System.Runtime.Remoting.Messaging;
using System.Security.Permissions;

namespace System.Web.Hosting
{
	// Token: 0x02000288 RID: 648
	internal class ContextBase
	{
		// Token: 0x17000733 RID: 1843
		// (get) Token: 0x06002150 RID: 8528 RVA: 0x000925F4 File Offset: 0x000915F4
		// (set) Token: 0x06002151 RID: 8529 RVA: 0x000925FB File Offset: 0x000915FB
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

		// Token: 0x06002152 RID: 8530 RVA: 0x00092604 File Offset: 0x00091604
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
