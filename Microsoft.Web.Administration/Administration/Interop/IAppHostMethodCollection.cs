using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Microsoft.Web.Administration.Interop
{
	// Token: 0x02000041 RID: 65
	[Guid("D6C7CD8F-BB8D-4F96-B591-D3A5F1320269")]
	[InterfaceType(1)]
	[ComImport]
	internal interface IAppHostMethodCollection
	{
		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x0600020B RID: 523
		uint Count { [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x170000E7 RID: 231
		IAppHostMethod this[object cIndex]
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			[return: MarshalAs(UnmanagedType.Interface)]
			get;
		}
	}
}
