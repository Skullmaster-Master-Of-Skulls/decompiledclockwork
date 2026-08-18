using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;

namespace Microsoft.Web.Administration.Interop
{
	// Token: 0x02000054 RID: 84
	[Guid("EAFE4895-A929-41EA-B14D-613E23F62B71")]
	[SuppressUnmanagedCodeSecurity]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	internal interface IAppHostPropertyException
	{
		// Token: 0x1700011F RID: 287
		// (get) Token: 0x06000263 RID: 611
		uint LineNumber { [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x17000120 RID: 288
		// (get) Token: 0x06000264 RID: 612
		string FileName { [MethodImpl(MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.BStr)] get; }

		// Token: 0x17000121 RID: 289
		// (get) Token: 0x06000265 RID: 613
		string ConfigPath { [MethodImpl(MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.BStr)] get; }

		// Token: 0x17000122 RID: 290
		// (get) Token: 0x06000266 RID: 614
		string ErrorLine { [MethodImpl(MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.BStr)] get; }

		// Token: 0x17000123 RID: 291
		// (get) Token: 0x06000267 RID: 615
		string PreErrorLine { [MethodImpl(MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.BStr)] get; }

		// Token: 0x17000124 RID: 292
		// (get) Token: 0x06000268 RID: 616
		string PostErrorLine { [MethodImpl(MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.BStr)] get; }

		// Token: 0x17000125 RID: 293
		// (get) Token: 0x06000269 RID: 617
		string ErrorString { [MethodImpl(MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.BStr)] get; }

		// Token: 0x17000126 RID: 294
		// (get) Token: 0x0600026A RID: 618
		string InvalidValue { [MethodImpl(MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.BStr)] get; }

		// Token: 0x17000127 RID: 295
		// (get) Token: 0x0600026B RID: 619
		string ValidationFailureReason { [MethodImpl(MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.BStr)] get; }

		// Token: 0x17000128 RID: 296
		// (get) Token: 0x0600026C RID: 620
		object[] ValidationFailureParameters { [MethodImpl(MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.SafeArray, SafeArraySubType = VarEnum.VT_VARIANT)] get; }
	}
}
