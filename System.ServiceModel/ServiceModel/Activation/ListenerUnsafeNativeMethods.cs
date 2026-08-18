using System;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security;
using System.ServiceModel.ComIntegration;
using System.Text;

namespace System.ServiceModel.Activation
{
	// Token: 0x020005CB RID: 1483
	[SuppressUnmanagedCodeSecurity]
	internal static class ListenerUnsafeNativeMethods
	{
		// Token: 0x060039A4 RID: 14756
		[DllImport("kernel32.dll", ExactSpelling = true, SetLastError = true)]
		internal static extern bool IsDebuggerPresent();

		// Token: 0x060039A5 RID: 14757
		[DllImport("kernel32.dll", ExactSpelling = true)]
		internal static extern void DebugBreak();

		// Token: 0x060039A6 RID: 14758
		[DllImport("advapi32.dll", ExactSpelling = true, SetLastError = true)]
		internal unsafe static extern bool AdjustTokenPrivileges(SafeCloseHandle tokenHandle, bool disableAllPrivileges, ListenerUnsafeNativeMethods.TOKEN_PRIVILEGES* newState, int bufferLength, IntPtr previousState, IntPtr returnLength);

		// Token: 0x060039A7 RID: 14759
		[DllImport("advapi32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		internal static extern bool LookupAccountName(string systemName, string accountName, byte[] sid, ref uint cbSid, StringBuilder referencedDomainName, ref uint cchReferencedDomainName, out short peUse);

		// Token: 0x060039A8 RID: 14760
		[DllImport("advapi32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		internal unsafe static extern bool LookupPrivilegeValue(IntPtr lpSystemName, string lpName, LUID* lpLuid);

		// Token: 0x060039A9 RID: 14761
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[DllImport("advapi32.dll", ExactSpelling = true, SetLastError = true)]
		internal static extern bool CloseServiceHandle(IntPtr handle);

		// Token: 0x060039AA RID: 14762
		[DllImport("advapi32.dll", ExactSpelling = true, SetLastError = true)]
		internal static extern bool GetKernelObjectSecurity(SafeCloseHandle handle, int securityInformation, [Out] byte[] pSecurityDescriptor, int nLength, out int lpnLengthNeeded);

		// Token: 0x060039AB RID: 14763
		[DllImport("advapi32.dll", ExactSpelling = true, SetLastError = true)]
		internal static extern bool GetTokenInformation(SafeCloseHandle tokenHandle, ListenerUnsafeNativeMethods.TOKEN_INFORMATION_CLASS tokenInformationClass, [Out] byte[] pTokenInformation, int tokenInformationLength, out int returnLength);

		// Token: 0x060039AC RID: 14764
		[DllImport("kernel32.dll", ExactSpelling = true, SetLastError = true)]
		internal static extern SafeCloseHandle OpenProcess(int dwDesiredAccess, bool bInheritHandle, int dwProcessId);

		// Token: 0x060039AD RID: 14765
		[DllImport("kernel32.dll", ExactSpelling = true, SetLastError = true)]
		internal static extern IntPtr GetCurrentProcess();

		// Token: 0x060039AE RID: 14766
		[DllImport("advapi32.dll", ExactSpelling = true, SetLastError = true)]
		internal static extern bool OpenProcessToken(SafeCloseHandle processHandle, int desiredAccess, out SafeCloseHandle tokenHandle);

		// Token: 0x060039AF RID: 14767
		[DllImport("advapi32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		internal static extern SafeServiceHandle OpenSCManager(string lpMachineName, string lpDatabaseName, int dwDesiredAccess);

		// Token: 0x060039B0 RID: 14768
		[DllImport("advapi32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		internal static extern SafeServiceHandle OpenService(SafeServiceHandle hSCManager, string lpServiceName, int dwDesiredAccess);

		// Token: 0x060039B1 RID: 14769
		[DllImport("advapi32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		internal static extern bool QueryServiceConfig(SafeServiceHandle hService, [Out] byte[] pServiceConfig, int cbBufSize, out int pcbBytesNeeded);

		// Token: 0x060039B2 RID: 14770
		[DllImport("advapi32.dll", ExactSpelling = true, SetLastError = true)]
		internal static extern bool QueryServiceStatus(SafeServiceHandle hService, out ListenerUnsafeNativeMethods.SERVICE_STATUS_PROCESS status);

		// Token: 0x060039B3 RID: 14771
		[DllImport("advapi32.dll", ExactSpelling = true, SetLastError = true)]
		internal static extern bool QueryServiceStatusEx(SafeServiceHandle hService, int InfoLevel, [Out] byte[] pBuffer, int cbBufSize, out int pcbBytesNeeded);

		// Token: 0x060039B4 RID: 14772
		[DllImport("advapi32.dll", ExactSpelling = true, SetLastError = true)]
		internal static extern bool SetKernelObjectSecurity(SafeCloseHandle handle, int securityInformation, [In] byte[] pSecurityDescriptor);

		// Token: 0x060039B5 RID: 14773
		[DllImport("advapi32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		internal static extern bool StartService(SafeServiceHandle hSCManager, int dwNumServiceArgs, string[] lpServiceArgVectors);

		// Token: 0x04002A1A RID: 10778
		private const string ADVAPI32 = "advapi32.dll";

		// Token: 0x04002A1B RID: 10779
		private const string KERNEL32 = "kernel32.dll";

		// Token: 0x04002A1C RID: 10780
		internal const int OWNER_SECURITY_INFORMATION = 1;

		// Token: 0x04002A1D RID: 10781
		internal const int DACL_SECURITY_INFORMATION = 4;

		// Token: 0x04002A1E RID: 10782
		internal const int ERROR_FILE_NOT_FOUND = 2;

		// Token: 0x04002A1F RID: 10783
		internal const int ERROR_INSUFFICIENT_BUFFER = 122;

		// Token: 0x04002A20 RID: 10784
		internal const int ERROR_SERVICE_ALREADY_RUNNING = 1056;

		// Token: 0x04002A21 RID: 10785
		internal const int PROCESS_QUERY_INFORMATION = 1024;

		// Token: 0x04002A22 RID: 10786
		internal const int PROCESS_DUP_HANDLE = 64;

		// Token: 0x04002A23 RID: 10787
		internal const int READ_CONTROL = 131072;

		// Token: 0x04002A24 RID: 10788
		internal const int TOKEN_QUERY = 8;

		// Token: 0x04002A25 RID: 10789
		internal const int WRITE_DAC = 262144;

		// Token: 0x04002A26 RID: 10790
		internal const int TOKEN_ADJUST_PRIVILEGES = 32;

		// Token: 0x04002A27 RID: 10791
		internal const int SC_MANAGER_CONNECT = 1;

		// Token: 0x04002A28 RID: 10792
		internal const int SC_STATUS_PROCESS_INFO = 0;

		// Token: 0x04002A29 RID: 10793
		internal const int SERVICE_QUERY_CONFIG = 1;

		// Token: 0x04002A2A RID: 10794
		internal const int SERVICE_QUERY_STATUS = 4;

		// Token: 0x04002A2B RID: 10795
		internal const int SERVICE_RUNNING = 4;

		// Token: 0x04002A2C RID: 10796
		internal const int SERVICE_START = 16;

		// Token: 0x04002A2D RID: 10797
		internal const int SERVICE_START_PENDING = 2;

		// Token: 0x02000CB6 RID: 3254
		[Flags]
		internal enum SidAttribute : uint
		{
			// Token: 0x04004540 RID: 17728
			SE_GROUP_MANDATORY = 1U,
			// Token: 0x04004541 RID: 17729
			SE_GROUP_ENABLED_BY_DEFAULT = 2U,
			// Token: 0x04004542 RID: 17730
			SE_GROUP_ENABLED = 4U,
			// Token: 0x04004543 RID: 17731
			SE_GROUP_OWNER = 8U,
			// Token: 0x04004544 RID: 17732
			SE_GROUP_USE_FOR_DENY_ONLY = 16U,
			// Token: 0x04004545 RID: 17733
			SE_GROUP_RESOURCE = 536870912U,
			// Token: 0x04004546 RID: 17734
			SE_GROUP_LOGON_ID = 3221225472U
		}

		// Token: 0x02000CB7 RID: 3255
		internal enum TOKEN_INFORMATION_CLASS
		{
			// Token: 0x04004548 RID: 17736
			TokenUser = 1,
			// Token: 0x04004549 RID: 17737
			TokenGroups,
			// Token: 0x0400454A RID: 17738
			TokenPrivileges,
			// Token: 0x0400454B RID: 17739
			TokenOwner,
			// Token: 0x0400454C RID: 17740
			TokenPrimaryGroup,
			// Token: 0x0400454D RID: 17741
			TokenDefaultDacl,
			// Token: 0x0400454E RID: 17742
			TokenSource,
			// Token: 0x0400454F RID: 17743
			TokenType,
			// Token: 0x04004550 RID: 17744
			TokenImpersonationLevel,
			// Token: 0x04004551 RID: 17745
			TokenStatistics,
			// Token: 0x04004552 RID: 17746
			TokenRestrictedSids,
			// Token: 0x04004553 RID: 17747
			TokenSessionId,
			// Token: 0x04004554 RID: 17748
			TokenGroupsAndPrivileges,
			// Token: 0x04004555 RID: 17749
			TokenSessionReference,
			// Token: 0x04004556 RID: 17750
			TokenSandBoxInert,
			// Token: 0x04004557 RID: 17751
			TokenAuditPolicy,
			// Token: 0x04004558 RID: 17752
			TokenOrigin,
			// Token: 0x04004559 RID: 17753
			TokenElevationType,
			// Token: 0x0400455A RID: 17754
			TokenLinkedToken,
			// Token: 0x0400455B RID: 17755
			TokenElevation,
			// Token: 0x0400455C RID: 17756
			TokenHasRestrictions,
			// Token: 0x0400455D RID: 17757
			TokenAccessInformation,
			// Token: 0x0400455E RID: 17758
			TokenVirtualizationAllowed,
			// Token: 0x0400455F RID: 17759
			TokenVirtualizationEnabled,
			// Token: 0x04004560 RID: 17760
			TokenIntegrityLevel,
			// Token: 0x04004561 RID: 17761
			TokenUIAccess,
			// Token: 0x04004562 RID: 17762
			TokenMandatoryPolicy,
			// Token: 0x04004563 RID: 17763
			TokenLogonSid,
			// Token: 0x04004564 RID: 17764
			TokenIsAppContainer,
			// Token: 0x04004565 RID: 17765
			TokenCapabilities,
			// Token: 0x04004566 RID: 17766
			TokenAppContainerSid,
			// Token: 0x04004567 RID: 17767
			TokenAppContainerNumber,
			// Token: 0x04004568 RID: 17768
			TokenUserClaimAttributes,
			// Token: 0x04004569 RID: 17769
			TokenDeviceClaimAttributes,
			// Token: 0x0400456A RID: 17770
			TokenRestrictedUserClaimAttributes,
			// Token: 0x0400456B RID: 17771
			TokenRestrictedDeviceClaimAttributes,
			// Token: 0x0400456C RID: 17772
			TokenDeviceGroups,
			// Token: 0x0400456D RID: 17773
			TokenRestrictedDeviceGroups,
			// Token: 0x0400456E RID: 17774
			MaxTokenInfoClass
		}

		// Token: 0x02000CB8 RID: 3256
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		internal struct QUERY_SERVICE_CONFIG
		{
			// Token: 0x0400456F RID: 17775
			internal int dwServiceType;

			// Token: 0x04004570 RID: 17776
			internal int dwStartType;

			// Token: 0x04004571 RID: 17777
			internal int dwErrorControl;

			// Token: 0x04004572 RID: 17778
			internal string lpBinaryPathName;

			// Token: 0x04004573 RID: 17779
			internal string lpLoadOrderGroup;

			// Token: 0x04004574 RID: 17780
			internal int dwTagId;

			// Token: 0x04004575 RID: 17781
			internal string lpDependencies;

			// Token: 0x04004576 RID: 17782
			internal string lpServiceStartName;

			// Token: 0x04004577 RID: 17783
			internal string lpDisplayName;
		}

		// Token: 0x02000CB9 RID: 3257
		internal struct SERVICE_STATUS_PROCESS
		{
			// Token: 0x04004578 RID: 17784
			internal int dwServiceType;

			// Token: 0x04004579 RID: 17785
			internal int dwCurrentState;

			// Token: 0x0400457A RID: 17786
			internal int dwControlsAccepted;

			// Token: 0x0400457B RID: 17787
			internal int dwWin32ExitCode;

			// Token: 0x0400457C RID: 17788
			internal int dwServiceSpecificExitCode;

			// Token: 0x0400457D RID: 17789
			internal int dwCheckPoint;

			// Token: 0x0400457E RID: 17790
			internal int dwWaitHint;

			// Token: 0x0400457F RID: 17791
			internal int dwProcessId;

			// Token: 0x04004580 RID: 17792
			internal int dwServiceFlags;
		}

		// Token: 0x02000CBA RID: 3258
		internal struct SID_AND_ATTRIBUTES
		{
			// Token: 0x04004581 RID: 17793
			internal IntPtr Sid;

			// Token: 0x04004582 RID: 17794
			internal ListenerUnsafeNativeMethods.SidAttribute Attributes;
		}

		// Token: 0x02000CBB RID: 3259
		internal struct TOKEN_GROUPS
		{
			// Token: 0x04004583 RID: 17795
			internal int GroupCount;

			// Token: 0x04004584 RID: 17796
			internal IntPtr Groups;
		}

		// Token: 0x02000CBC RID: 3260
		internal struct TOKEN_USER
		{
			// Token: 0x04004585 RID: 17797
			internal IntPtr User;
		}

		// Token: 0x02000CBD RID: 3261
		internal struct TOKEN_PRIVILEGES
		{
			// Token: 0x04004586 RID: 17798
			internal int PrivilegeCount;

			// Token: 0x04004587 RID: 17799
			internal LUID_AND_ATTRIBUTES Privileges;
		}

		// Token: 0x02000CBE RID: 3262
		[Guid("CB2F6722-AB3A-11D2-9C40-00C04FA30A3E")]
		[ComConversionLoss]
		[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
		[ComImport]
		internal interface ICorRuntimeHost
		{
			// Token: 0x0600797C RID: 31100
			void Void0();

			// Token: 0x0600797D RID: 31101
			void Void1();

			// Token: 0x0600797E RID: 31102
			void Void2();

			// Token: 0x0600797F RID: 31103
			void Void3();

			// Token: 0x06007980 RID: 31104
			void Void4();

			// Token: 0x06007981 RID: 31105
			void Void5();

			// Token: 0x06007982 RID: 31106
			void Void6();

			// Token: 0x06007983 RID: 31107
			void Void7();

			// Token: 0x06007984 RID: 31108
			void Void8();

			// Token: 0x06007985 RID: 31109
			void Void9();

			// Token: 0x06007986 RID: 31110
			void GetDefaultDomain([MarshalAs(UnmanagedType.IUnknown)] out object pAppDomain);
		}
	}
}
