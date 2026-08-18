using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Web.Security
{
	// Token: 0x020005CC RID: 1484
	[ComVisible(false)]
	[SuppressUnmanagedCodeSecurity]
	internal static class NativeComInterfaces
	{
		// Token: 0x04002889 RID: 10377
		internal const int ADS_SETTYPE_FULL = 1;

		// Token: 0x0400288A RID: 10378
		internal const int ADS_SETTYPE_DN = 4;

		// Token: 0x0400288B RID: 10379
		internal const int ADS_FORMAT_PROVIDER = 10;

		// Token: 0x0400288C RID: 10380
		internal const int ADS_FORMAT_SERVER = 9;

		// Token: 0x0400288D RID: 10381
		internal const int ADS_FORMAT_X500_DN = 7;

		// Token: 0x0400288E RID: 10382
		internal const int ADS_ESCAPEDMODE_ON = 2;

		// Token: 0x0400288F RID: 10383
		internal const int ADS_ESCAPEDMODE_OFF = 3;

		// Token: 0x020009FF RID: 2559
		[Guid("080d0d78-f421-11d0-a36e-00c04fb950dc")]
		[ComImport]
		internal class Pathname
		{
			// Token: 0x06006D56 RID: 27990
			[MethodImpl(MethodImplOptions.InternalCall)]
			public extern Pathname();
		}

		// Token: 0x02000A00 RID: 2560
		[Guid("D592AED4-F420-11D0-A36E-00C04FB950DC")]
		[InterfaceType(ComInterfaceType.InterfaceIsDual)]
		[ComImport]
		internal interface IAdsPathname
		{
			// Token: 0x06006D57 RID: 27991
			[SuppressUnmanagedCodeSecurity]
			int Set([MarshalAs(UnmanagedType.BStr)] [In] string bstrADsPath, [MarshalAs(UnmanagedType.U4)] [In] int lnSetType);

			// Token: 0x06006D58 RID: 27992
			int SetDisplayType([MarshalAs(UnmanagedType.U4)] [In] int lnDisplayType);

			// Token: 0x06006D59 RID: 27993
			[SuppressUnmanagedCodeSecurity]
			[return: MarshalAs(UnmanagedType.BStr)]
			string Retrieve([MarshalAs(UnmanagedType.U4)] [In] int lnFormatType);

			// Token: 0x06006D5A RID: 27994
			[return: MarshalAs(UnmanagedType.U4)]
			int GetNumElements();

			// Token: 0x06006D5B RID: 27995
			[return: MarshalAs(UnmanagedType.BStr)]
			string GetElement([MarshalAs(UnmanagedType.U4)] [In] int lnElementIndex);

			// Token: 0x06006D5C RID: 27996
			void AddLeafElement([MarshalAs(UnmanagedType.BStr)] [In] string bstrLeafElement);

			// Token: 0x06006D5D RID: 27997
			void RemoveLeafElement();

			// Token: 0x06006D5E RID: 27998
			[return: MarshalAs(UnmanagedType.Interface)]
			object CopyPath();

			// Token: 0x06006D5F RID: 27999
			[SuppressUnmanagedCodeSecurity]
			[return: MarshalAs(UnmanagedType.BStr)]
			string GetEscapedElement([MarshalAs(UnmanagedType.U4)] [In] int lnReserved, [MarshalAs(UnmanagedType.BStr)] [In] string bstrInStr);

			// Token: 0x17001E22 RID: 7714
			// (get) Token: 0x06006D60 RID: 28000
			// (set) Token: 0x06006D61 RID: 28001
			int EscapedMode { get; [SuppressUnmanagedCodeSecurity] set; }
		}

		// Token: 0x02000A01 RID: 2561
		[Guid("927971f5-0939-11d1-8be1-00c04fd8d503")]
		[ComImport]
		internal class LargeInteger
		{
			// Token: 0x06006D62 RID: 28002
			[MethodImpl(MethodImplOptions.InternalCall)]
			public extern LargeInteger();
		}

		// Token: 0x02000A02 RID: 2562
		[Guid("9068270b-0939-11d1-8be1-00c04fd8d503")]
		[InterfaceType(ComInterfaceType.InterfaceIsDual)]
		[ComImport]
		internal interface IAdsLargeInteger
		{
			// Token: 0x17001E23 RID: 7715
			// (get) Token: 0x06006D63 RID: 28003
			// (set) Token: 0x06006D64 RID: 28004
			long HighPart { [SuppressUnmanagedCodeSecurity] get; [SuppressUnmanagedCodeSecurity] set; }

			// Token: 0x17001E24 RID: 7716
			// (get) Token: 0x06006D65 RID: 28005
			// (set) Token: 0x06006D66 RID: 28006
			long LowPart { [SuppressUnmanagedCodeSecurity] get; [SuppressUnmanagedCodeSecurity] set; }
		}
	}
}
