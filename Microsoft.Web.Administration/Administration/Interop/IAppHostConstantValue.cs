using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;

namespace Microsoft.Web.Administration.Interop
{
	// Token: 0x02000043 RID: 67
	[SuppressUnmanagedCodeSecurity]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("0716CAF8-7D05-4A46-8099-77594BE91394")]
	[ComImport]
	internal interface IAppHostConstantValue
	{
		// Token: 0x170000EA RID: 234
		// (get) Token: 0x06000212 RID: 530
		string Name { [MethodImpl(MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.BStr)] get; }

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x06000213 RID: 531
		uint Value { [MethodImpl(MethodImplOptions.InternalCall)] get; }
	}
}
