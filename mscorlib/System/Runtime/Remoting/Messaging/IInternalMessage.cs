using System;
using System.Security.Permissions;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x02000701 RID: 1793
	internal interface IInternalMessage
	{
		// Token: 0x17000AD5 RID: 2773
		// (get) Token: 0x06003FD9 RID: 16345
		// (set) Token: 0x06003FDA RID: 16346
		ServerIdentity ServerIdentityObject { [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.Infrastructure)] get; [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.Infrastructure)] set; }

		// Token: 0x17000AD6 RID: 2774
		// (get) Token: 0x06003FDB RID: 16347
		// (set) Token: 0x06003FDC RID: 16348
		Identity IdentityObject { [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.Infrastructure)] get; [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.Infrastructure)] set; }

		// Token: 0x06003FDD RID: 16349
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.Infrastructure)]
		void SetURI(string uri);

		// Token: 0x06003FDE RID: 16350
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.Infrastructure)]
		void SetCallContext(LogicalCallContext callContext);

		// Token: 0x06003FDF RID: 16351
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.Infrastructure)]
		bool HasProperties();
	}
}
