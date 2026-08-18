using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;

namespace Microsoft.Web.Administration.Interop
{
	// Token: 0x02000055 RID: 85
	[SuppressUnmanagedCodeSecurity]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("450386DB-7409-4667-935E-384DBBEE2A9E")]
	[ComImport]
	internal interface IAppHostPropertySchema
	{
		// Token: 0x17000129 RID: 297
		// (get) Token: 0x0600026D RID: 621
		string Name { [MethodImpl(MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.BStr)] get; }

		// Token: 0x1700012A RID: 298
		// (get) Token: 0x0600026E RID: 622
		string Type { [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x1700012B RID: 299
		// (get) Token: 0x0600026F RID: 623
		object DefaultValue { [MethodImpl(MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.Struct)] get; }

		// Token: 0x1700012C RID: 300
		// (get) Token: 0x06000270 RID: 624
		bool IsRequired { [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x1700012D RID: 301
		// (get) Token: 0x06000271 RID: 625
		bool IsUniqueKey { [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x1700012E RID: 302
		// (get) Token: 0x06000272 RID: 626
		bool IsCombinedKey { [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x1700012F RID: 303
		// (get) Token: 0x06000273 RID: 627
		bool IsExpanded { [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x17000130 RID: 304
		// (get) Token: 0x06000274 RID: 628
		string ValidationType { [MethodImpl(MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.BStr)] get; }

		// Token: 0x17000131 RID: 305
		// (get) Token: 0x06000275 RID: 629
		string ValidationParameter { [MethodImpl(MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.BStr)] get; }

		// Token: 0x06000276 RID: 630
		[MethodImpl(MethodImplOptions.InternalCall)]
		[return: MarshalAs(UnmanagedType.Struct)]
		object GetMetadata([MarshalAs(UnmanagedType.BStr)] [In] string bstrMetadataType);

		// Token: 0x17000132 RID: 306
		// (get) Token: 0x06000277 RID: 631
		bool IsCaseSensitive { [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x17000133 RID: 307
		// (get) Token: 0x06000278 RID: 632
		IAppHostConstantValueCollection PossibleValues { [MethodImpl(MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.Interface)] get; }

		// Token: 0x17000134 RID: 308
		// (get) Token: 0x06000279 RID: 633
		bool DoesAllowInfinite { [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x17000135 RID: 309
		// (get) Token: 0x0600027A RID: 634
		bool IsEncrypted { [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x17000136 RID: 310
		// (get) Token: 0x0600027B RID: 635
		string TimeSpanFormat { [MethodImpl(MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.BStr)] get; }
	}
}
