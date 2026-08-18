using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Microsoft.Web.Administration.Interop
{
	// Token: 0x02000050 RID: 80
	[Guid("8BED2C68-A5FB-4B28-8581-A0DC5267419F")]
	[InterfaceType(1)]
	[ComImport]
	internal interface IAppHostPropertySchemaCollection
	{
		// Token: 0x17000116 RID: 278
		// (get) Token: 0x06000255 RID: 597
		[DispId(1610678272)]
		uint Count { [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x17000117 RID: 279
		[DispId(0)]
		IAppHostPropertySchema this[object cIndex]
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			[return: MarshalAs(UnmanagedType.Interface)]
			get;
		}
	}
}
