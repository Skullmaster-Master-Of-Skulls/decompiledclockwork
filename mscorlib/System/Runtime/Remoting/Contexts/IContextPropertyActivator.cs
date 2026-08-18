using System;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Activation;
using System.Security.Permissions;

namespace System.Runtime.Remoting.Contexts
{
	// Token: 0x020006C8 RID: 1736
	[ComVisible(true)]
	public interface IContextPropertyActivator
	{
		// Token: 0x06003EB1 RID: 16049
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.Infrastructure)]
		bool IsOKToActivate(IConstructionCallMessage msg);

		// Token: 0x06003EB2 RID: 16050
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.Infrastructure)]
		void CollectFromClientContext(IConstructionCallMessage msg);

		// Token: 0x06003EB3 RID: 16051
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.Infrastructure)]
		bool DeliverClientContextToServerContext(IConstructionCallMessage msg);

		// Token: 0x06003EB4 RID: 16052
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.Infrastructure)]
		void CollectFromServerContext(IConstructionReturnMessage msg);

		// Token: 0x06003EB5 RID: 16053
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.Infrastructure)]
		bool DeliverServerContextToClientContext(IConstructionReturnMessage msg);
	}
}
