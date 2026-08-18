using System;
using System.Data.OleDb;
using System.Runtime.InteropServices;

namespace System.Data.Common
{
	// Token: 0x0200030A RID: 778
	internal static class NativeMethods
	{
		// Token: 0x06003126 RID: 12582
		[DllImport("kernel32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
		internal static extern IntPtr MapViewOfFile(IntPtr hFileMappingObject, int dwDesiredAccess, int dwFileOffsetHigh, int dwFileOffsetLow, IntPtr dwNumberOfBytesToMap);

		// Token: 0x06003127 RID: 12583
		[DllImport("kernel32.dll", BestFitMapping = false, CharSet = CharSet.Ansi, ThrowOnUnmappableChar = true)]
		internal static extern IntPtr OpenFileMappingA(int dwDesiredAccess, bool bInheritHandle, [MarshalAs(UnmanagedType.LPStr)] string lpName);

		// Token: 0x06003128 RID: 12584
		[DllImport("kernel32.dll", BestFitMapping = false, CharSet = CharSet.Ansi, ThrowOnUnmappableChar = true)]
		internal static extern IntPtr CreateFileMappingA(IntPtr hFile, IntPtr pAttr, int flProtect, int dwMaximumSizeHigh, int dwMaximumSizeLow, [MarshalAs(UnmanagedType.LPStr)] string lpName);

		// Token: 0x06003129 RID: 12585
		[DllImport("kernel32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
		internal static extern bool UnmapViewOfFile(IntPtr lpBaseAddress);

		// Token: 0x0600312A RID: 12586
		[DllImport("kernel32.dll", CharSet = CharSet.Auto, ExactSpelling = true, SetLastError = true)]
		internal static extern bool CloseHandle(IntPtr handle);

		// Token: 0x0600312B RID: 12587
		[DllImport("advapi32.dll", CharSet = CharSet.Auto, ExactSpelling = true, SetLastError = true)]
		internal static extern bool AllocateAndInitializeSid(IntPtr pIdentifierAuthority, byte nSubAuthorityCount, int dwSubAuthority0, int dwSubAuthority1, int dwSubAuthority2, int dwSubAuthority3, int dwSubAuthority4, int dwSubAuthority5, int dwSubAuthority6, int dwSubAuthority7, ref IntPtr pSid);

		// Token: 0x0600312C RID: 12588
		[DllImport("advapi32.dll", CharSet = CharSet.Auto, ExactSpelling = true, SetLastError = true)]
		internal static extern int GetLengthSid(IntPtr pSid);

		// Token: 0x0600312D RID: 12589
		[DllImport("advapi32.dll", CharSet = CharSet.Auto, ExactSpelling = true, SetLastError = true)]
		internal static extern bool InitializeAcl(IntPtr pAcl, int nAclLength, int dwAclRevision);

		// Token: 0x0600312E RID: 12590
		[DllImport("advapi32.dll", CharSet = CharSet.Auto, ExactSpelling = true, SetLastError = true)]
		internal static extern bool AddAccessDeniedAce(IntPtr pAcl, int dwAceRevision, int AccessMask, IntPtr pSid);

		// Token: 0x0600312F RID: 12591
		[DllImport("advapi32.dll", CharSet = CharSet.Auto, ExactSpelling = true, SetLastError = true)]
		internal static extern bool AddAccessAllowedAce(IntPtr pAcl, int dwAceRevision, uint AccessMask, IntPtr pSid);

		// Token: 0x06003130 RID: 12592
		[DllImport("advapi32.dll", CharSet = CharSet.Auto, ExactSpelling = true, SetLastError = true)]
		internal static extern bool InitializeSecurityDescriptor(IntPtr pSecurityDescriptor, int dwRevision);

		// Token: 0x06003131 RID: 12593
		[DllImport("advapi32.dll", CharSet = CharSet.Auto, ExactSpelling = true, SetLastError = true)]
		internal static extern bool SetSecurityDescriptorDacl(IntPtr pSecurityDescriptor, bool bDaclPresent, IntPtr pDacl, bool bDaclDefaulted);

		// Token: 0x06003132 RID: 12594
		[DllImport("advapi32.dll", CharSet = CharSet.Auto, ExactSpelling = true, SetLastError = true)]
		internal static extern IntPtr FreeSid(IntPtr pSid);

		// Token: 0x0200043E RID: 1086
		[Guid("0c733a1e-2a1c-11ce-ade5-00aa0044773d")]
		[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
		[ComImport]
		internal interface ISourcesRowset
		{
			// Token: 0x06003651 RID: 13905
			[PreserveSig]
			OleDbHResult GetSourcesRowset([In] IntPtr pUnkOuter, [MarshalAs(UnmanagedType.LPStruct)] [In] Guid riid, [In] int cPropertySets, [In] IntPtr rgProperties, [MarshalAs(UnmanagedType.Interface)] out object ppRowset);
		}

		// Token: 0x0200043F RID: 1087
		[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
		[Guid("0C733A5E-2A1C-11CE-ADE5-00AA0044773D")]
		[ComImport]
		internal interface ITransactionJoin
		{
			// Token: 0x06003652 RID: 13906
			[Obsolete("not used", true)]
			[PreserveSig]
			int GetOptionsObject();

			// Token: 0x06003653 RID: 13907
			void JoinTransaction([MarshalAs(UnmanagedType.Interface)] [In] object punkTransactionCoord, [In] int isoLevel, [In] int isoFlags, [In] IntPtr pOtherOptions);
		}
	}
}
