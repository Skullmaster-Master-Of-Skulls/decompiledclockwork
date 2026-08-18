using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Deployment.Internal.Isolation
{
	// Token: 0x0200003C RID: 60
	[Guid("587bf538-4d90-4a3c-9ef1-58a200a8a9e7")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	internal interface IDefinitionIdentity
	{
		// Token: 0x06000123 RID: 291
		[SecurityCritical]
		[return: MarshalAs(UnmanagedType.LPWStr)]
		string GetAttribute([MarshalAs(UnmanagedType.LPWStr)] [In] string Namespace, [MarshalAs(UnmanagedType.LPWStr)] [In] string Name);

		// Token: 0x06000124 RID: 292
		[SecurityCritical]
		void SetAttribute([MarshalAs(UnmanagedType.LPWStr)] [In] string Namespace, [MarshalAs(UnmanagedType.LPWStr)] [In] string Name, [MarshalAs(UnmanagedType.LPWStr)] [In] string Value);

		// Token: 0x06000125 RID: 293
		[SecurityCritical]
		IEnumIDENTITY_ATTRIBUTE EnumAttributes();

		// Token: 0x06000126 RID: 294
		[SecurityCritical]
		IDefinitionIdentity Clone([In] IntPtr cDeltas, [MarshalAs(UnmanagedType.LPArray)] [In] IDENTITY_ATTRIBUTE[] Deltas);
	}
}
