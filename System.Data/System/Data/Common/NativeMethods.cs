using System;
using System.Data.OleDb;
using System.Runtime.InteropServices;

namespace System.Data.Common
{
	// Token: 0x02000156 RID: 342
	internal static class NativeMethods
	{
		// Token: 0x0600158E RID: 5518
		[DllImport("kernel32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
		internal static extern IntPtr MapViewOfFile(IntPtr hFileMappingObject, int dwDesiredAccess, int dwFileOffsetHigh, int dwFileOffsetLow, IntPtr dwNumberOfBytesToMap);

		// Token: 0x0600158F RID: 5519
		[DllImport("kernel32.dll", BestFitMapping = false, CharSet = CharSet.Ansi, ThrowOnUnmappableChar = true)]
		internal static extern IntPtr OpenFileMappingA(int dwDesiredAccess, bool bInheritHandle, [MarshalAs(UnmanagedType.LPStr)] string lpName);

		// Token: 0x06001590 RID: 5520
		[DllImport("kernel32.dll", BestFitMapping = false, CharSet = CharSet.Ansi, ThrowOnUnmappableChar = true)]
		internal static extern IntPtr CreateFileMappingA(IntPtr hFile, IntPtr pAttr, int flProtect, int dwMaximumSizeHigh, int dwMaximumSizeLow, [MarshalAs(UnmanagedType.LPStr)] string lpName);

		// Token: 0x06001591 RID: 5521
		[DllImport("kernel32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
		internal static extern bool UnmapViewOfFile(IntPtr lpBaseAddress);

		// Token: 0x06001592 RID: 5522
		[DllImport("kernel32.dll", CharSet = CharSet.Auto, ExactSpelling = true, SetLastError = true)]
		internal static extern bool CloseHandle(IntPtr handle);

		// Token: 0x06001593 RID: 5523
		[DllImport("advapi32.dll", CharSet = CharSet.Auto, ExactSpelling = true, SetLastError = true)]
		internal static extern bool AllocateAndInitializeSid(IntPtr pIdentifierAuthority, byte nSubAuthorityCount, int dwSubAuthority0, int dwSubAuthority1, int dwSubAuthority2, int dwSubAuthority3, int dwSubAuthority4, int dwSubAuthority5, int dwSubAuthority6, int dwSubAuthority7, ref IntPtr pSid);

		// Token: 0x06001594 RID: 5524
		[DllImport("advapi32.dll", CharSet = CharSet.Auto, ExactSpelling = true, SetLastError = true)]
		internal static extern int GetLengthSid(IntPtr pSid);

		// Token: 0x06001595 RID: 5525
		[DllImport("advapi32.dll", CharSet = CharSet.Auto, ExactSpelling = true, SetLastError = true)]
		internal static extern bool InitializeAcl(IntPtr pAcl, int nAclLength, int dwAclRevision);

		// Token: 0x06001596 RID: 5526
		[DllImport("advapi32.dll", CharSet = CharSet.Auto, ExactSpelling = true, SetLastError = true)]
		internal static extern bool AddAccessDeniedAce(IntPtr pAcl, int dwAceRevision, int AccessMask, IntPtr pSid);

		// Token: 0x06001597 RID: 5527
		[DllImport("advapi32.dll", CharSet = CharSet.Auto, ExactSpelling = true, SetLastError = true)]
		internal static extern bool AddAccessAllowedAce(IntPtr pAcl, int dwAceRevision, uint AccessMask, IntPtr pSid);

		// Token: 0x06001598 RID: 5528
		[DllImport("advapi32.dll", CharSet = CharSet.Auto, ExactSpelling = true, SetLastError = true)]
		internal static extern bool InitializeSecurityDescriptor(IntPtr pSecurityDescriptor, int dwRevision);

		// Token: 0x06001599 RID: 5529
		[DllImport("advapi32.dll", CharSet = CharSet.Auto, ExactSpelling = true, SetLastError = true)]
		internal static extern bool SetSecurityDescriptorDacl(IntPtr pSecurityDescriptor, bool bDaclPresent, IntPtr pDacl, bool bDaclDefaulted);

		// Token: 0x0600159A RID: 5530
		[DllImport("advapi32.dll", CharSet = CharSet.Auto, ExactSpelling = true, SetLastError = true)]
		internal static extern IntPtr FreeSid(IntPtr pSid);

		// Token: 0x02000157 RID: 343
		[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
		[Guid("0c733a1e-2a1c-11ce-ade5-00aa0044773d")]
		[ComImport]
		internal interface ISourcesRowset
		{
			// Token: 0x0600159B RID: 5531
			[PreserveSig]
			OleDbHResult GetSourcesRowset([In] IntPtr pUnkOuter, [MarshalAs(UnmanagedType.LPStruct)] [In] Guid riid, [In] int cPropertySets, [In] IntPtr rgProperties, [MarshalAs(UnmanagedType.Interface)] out object ppRowset);
		}

		// Token: 0x02000158 RID: 344
		[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
		[Guid("0C733A5E-2A1C-11CE-ADE5-00AA0044773D")]
		[ComImport]
		internal interface ITransactionJoin
		{
			// Token: 0x0600159C RID: 5532
			[Obsolete("not used", true)]
			[PreserveSig]
			int GetOptionsObject();

			// Token: 0x0600159D RID: 5533
			void JoinTransaction([MarshalAs(UnmanagedType.Interface)] [In] object punkTransactionCoord, [In] int isoLevel, [In] int isoFlags, [In] IntPtr pOtherOptions);
		}
	}
}
