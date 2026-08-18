using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.Runtime.Remoting.Services
{
	// Token: 0x020007A7 RID: 1959
	[ComVisible(true)]
	public interface ITrackingHandler
	{
		// Token: 0x060045AF RID: 17839
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.Infrastructure)]
		void MarshaledObject(object obj, ObjRef or);

		// Token: 0x060045B0 RID: 17840
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.Infrastructure)]
		void UnmarshaledObject(object obj, ObjRef or);

		// Token: 0x060045B1 RID: 17841
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.Infrastructure)]
		void DisconnectedObject(object obj);
	}
}
