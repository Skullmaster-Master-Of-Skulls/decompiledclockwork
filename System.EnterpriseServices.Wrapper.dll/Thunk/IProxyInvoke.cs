using System;
using System.Runtime.Remoting.Messaging;

namespace System.EnterpriseServices.Thunk
{
	// Token: 0x02000053 RID: 83
	internal interface IProxyInvoke
	{
		// Token: 0x060000A2 RID: 162
		IMessage LocalInvoke(IMessage msg);

		// Token: 0x060000A3 RID: 163
		IntPtr GetOuterIUnknown();
	}
}
