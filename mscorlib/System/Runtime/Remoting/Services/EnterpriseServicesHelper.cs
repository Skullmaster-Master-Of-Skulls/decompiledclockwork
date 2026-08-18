using System;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Activation;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Remoting.Proxies;
using System.Security.Permissions;

namespace System.Runtime.Remoting.Services
{
	// Token: 0x020006DB RID: 1755
	[ComVisible(true)]
	[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.Infrastructure)]
	public sealed class EnterpriseServicesHelper
	{
		// Token: 0x06003F20 RID: 16160 RVA: 0x000D83E8 File Offset: 0x000D73E8
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		public static object WrapIUnknownWithComObject(IntPtr punk)
		{
			return Marshal.InternalWrapIUnknownWithComObject(punk);
		}

		// Token: 0x06003F21 RID: 16161 RVA: 0x000D83F0 File Offset: 0x000D73F0
		[ComVisible(true)]
		public static IConstructionReturnMessage CreateConstructionReturnMessage(IConstructionCallMessage ctorMsg, MarshalByRefObject retObj)
		{
			return new ConstructorReturnMessage(retObj, null, 0, null, ctorMsg);
		}

		// Token: 0x06003F22 RID: 16162 RVA: 0x000D840C File Offset: 0x000D740C
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		public static void SwitchWrappers(RealProxy oldcp, RealProxy newcp)
		{
			object transparentProxy = oldcp.GetTransparentProxy();
			object transparentProxy2 = newcp.GetTransparentProxy();
			RemotingServices.GetServerContextForProxy(transparentProxy);
			RemotingServices.GetServerContextForProxy(transparentProxy2);
			Marshal.InternalSwitchCCW(transparentProxy, transparentProxy2);
		}
	}
}
