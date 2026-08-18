using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Microsoft.Web.Administration.Interop
{
	// Token: 0x0200004F RID: 79
	[Guid("0344CDDA-151E-4CBF-82DA-66AE61E97754")]
	[InterfaceType(1)]
	[ComImport]
	internal interface IAppHostElementSchemaCollection
	{
		// Token: 0x17000114 RID: 276
		// (get) Token: 0x06000253 RID: 595
		[DispId(1610678272)]
		uint Count { [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x17000115 RID: 277
		[DispId(0)]
		IAppHostElementSchema this[object cIndex]
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			[return: MarshalAs(UnmanagedType.Interface)]
			get;
		}
	}
}
