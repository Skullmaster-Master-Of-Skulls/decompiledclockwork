using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Deployment.Internal.Isolation
{
	// Token: 0x0200006A RID: 106
	[Guid("07662534-750b-4ed5-9cfb-1c5bc5acfd07")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	internal interface IStateManager
	{
		// Token: 0x06000210 RID: 528
		[SecurityCritical]
		void PrepareApplicationState([In] UIntPtr Inputs, ref UIntPtr Outputs);

		// Token: 0x06000211 RID: 529
		[SecurityCritical]
		void SetApplicationRunningState([In] uint Flags, [In] IActContext Context, [In] uint RunningState, out uint Disposition);

		// Token: 0x06000212 RID: 530
		[SecurityCritical]
		void GetApplicationStateFilesystemLocation([In] uint Flags, [In] IDefinitionAppId Appidentity, [In] IDefinitionIdentity ComponentIdentity, [In] UIntPtr Coordinates, [MarshalAs(UnmanagedType.LPWStr)] out string Path);

		// Token: 0x06000213 RID: 531
		[SecurityCritical]
		void Scavenge([In] uint Flags, out uint Disposition);
	}
}
