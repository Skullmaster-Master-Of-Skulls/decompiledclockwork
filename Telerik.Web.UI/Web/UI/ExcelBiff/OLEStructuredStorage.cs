using System;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using Telerik.Web.UI.Common.Export.ExcelBiff;

namespace Telerik.Web.UI.ExcelBiff
{
	// Token: 0x02000AC5 RID: 2757
	internal sealed class OLEStructuredStorage
	{
		// Token: 0x06006848 RID: 26696 RVA: 0x00186B7C File Offset: 0x00184D7C
		internal static int CreateILockBytesOnHGlobal(IntPtr hGlobal, bool fDeleteOnRelease, out OLEStructuredStorage.UCOMILockBytes lockBytes)
		{
			return SafeNativeMethods.CreateILockBytesOnHGlobal(hGlobal, fDeleteOnRelease, out lockBytes);
		}

		// Token: 0x06006849 RID: 26697 RVA: 0x00186B86 File Offset: 0x00184D86
		internal static int StgCreateDocfile([MarshalAs(UnmanagedType.LPWStr)] string wcsName, int grfMode, int reserved, out OLEStructuredStorage.UCOMIStorage storage)
		{
			return SafeNativeMethods.StgCreateDocfile(wcsName, grfMode, reserved, out storage);
		}

		// Token: 0x0600684A RID: 26698 RVA: 0x00186B91 File Offset: 0x00184D91
		internal static int StgCreateDocfileOnILockBytes(OLEStructuredStorage.UCOMILockBytes plkbyt, int grfMode, int reserved, out OLEStructuredStorage.UCOMIStorage storage)
		{
			return SafeNativeMethods.StgCreateDocfileOnILockBytes(plkbyt, grfMode, reserved, out storage);
		}

		// Token: 0x0600684B RID: 26699 RVA: 0x00186B9C File Offset: 0x00184D9C
		internal static int StgCreateStorageEx([MarshalAs(UnmanagedType.LPWStr)] string wcsName, int grfMode, int stgfmt, int grfAttr, IntPtr StgOptions, IntPtr reserved2, ref Guid refiid, out OLEStructuredStorage.UCOMIStorage storage)
		{
			return SafeNativeMethods.StgCreateStorageEx(wcsName, grfMode, stgfmt, grfAttr, StgOptions, reserved2, ref refiid, out storage);
		}

		// Token: 0x0600684C RID: 26700 RVA: 0x00186BAF File Offset: 0x00184DAF
		internal static int StgOpenStorage([MarshalAs(UnmanagedType.LPWStr)] string wcsName, IntPtr stgPriority, int grfMode, IntPtr snbExclude, int reserved, out OLEStructuredStorage.UCOMIStorage storage)
		{
			return SafeNativeMethods.StgOpenStorage(wcsName, stgPriority, grfMode, snbExclude, reserved, out storage);
		}

		// Token: 0x04001B86 RID: 7046
		internal const int STGM_CREATE = 4096;

		// Token: 0x04001B87 RID: 7047
		internal const int STGM_DELETEONRELEASE = 67108864;

		// Token: 0x04001B88 RID: 7048
		internal const int STGM_READ = 0;

		// Token: 0x04001B89 RID: 7049
		internal const int STGM_READWRITE = 2;

		// Token: 0x04001B8A RID: 7050
		internal const int STGM_SHARE_DENY_NONE = 64;

		// Token: 0x04001B8B RID: 7051
		internal const int STGM_SHARE_EXCLUSIVE = 16;

		// Token: 0x04001B8C RID: 7052
		internal const int STGM_SIMPLE = 134217728;

		// Token: 0x02000AC6 RID: 2758
		[Guid("0000000d-0000-0000-C000-000000000046")]
		[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
		private interface IEnumSTATSTG
		{
			// Token: 0x0600684E RID: 26702
			void Next(int celt, out System.Runtime.InteropServices.ComTypes.STATSTG rgelt, out int pceltFetched);
		}

		// Token: 0x02000AC7 RID: 2759
		[Guid("0000000a-0000-0000-C000-000000000046")]
		[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
		internal interface UCOMILockBytes
		{
			// Token: 0x0600684F RID: 26703
			void ReadAt(ulong offset, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)] [Out] byte[] pv, int cb, out int pcbRead);

			// Token: 0x06006850 RID: 26704
			void WriteAt(ulong offset, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)] byte[] pv, int cb, out int pcbWritten);

			// Token: 0x06006851 RID: 26705
			void Flush();

			// Token: 0x06006852 RID: 26706
			void SetSize(int cb);

			// Token: 0x06006853 RID: 26707
			void LockRegion(int libOffset, int cb, long dwLoclType);

			// Token: 0x06006854 RID: 26708
			void UnlockRegion(int libOffset, int cb, long dwLoclType);

			// Token: 0x06006855 RID: 26709
			void Stat(out System.Runtime.InteropServices.ComTypes.STATSTG pstatstg, int grfStatFlag);
		}

		// Token: 0x02000AC8 RID: 2760
		[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
		[Guid("0000000b-0000-0000-C000-000000000046")]
		internal interface UCOMIStorage
		{
			// Token: 0x06006856 RID: 26710
			void CreateStream([MarshalAs(UnmanagedType.LPWStr)] string wcsName, int grfMode, int reserved1, int reserved2, out IStream stream);

			// Token: 0x06006857 RID: 26711
			void OpenStream([MarshalAs(UnmanagedType.LPWStr)] string wcsName, IntPtr reserved1, int grfMode, int reserved2, out IStream stream);

			// Token: 0x06006858 RID: 26712
			void CreateStorage([MarshalAs(UnmanagedType.LPWStr)] string wcsName, int grfMode, int reserved1, int reserved2, out OLEStructuredStorage.UCOMIStorage storage);

			// Token: 0x06006859 RID: 26713
			void OpenStorage([MarshalAs(UnmanagedType.LPWStr)] string wcsName, OLEStructuredStorage.UCOMIStorage pstgPriority, int grfMode, IntPtr[] snbExclude, int reserved1, out OLEStructuredStorage.UCOMIStorage storage);

			// Token: 0x0600685A RID: 26714
			void CopyTo(int ciidExclude, IntPtr[] rgiidExclude, IntPtr[] snbExclude, out OLEStructuredStorage.UCOMIStorage storage);

			// Token: 0x0600685B RID: 26715
			void MoveTo([MarshalAs(UnmanagedType.LPWStr)] string wcsName, OLEStructuredStorage.UCOMIStorage pstgDest, [MarshalAs(UnmanagedType.LPWStr)] string pwcsNewName, int grfFlags);

			// Token: 0x0600685C RID: 26716
			void Commit(int grfCommitFlags);
		}
	}
}
