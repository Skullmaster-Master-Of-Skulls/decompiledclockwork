using System;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Principal;
using System.Text;

namespace System.IdentityModel
{
	// Token: 0x02000062 RID: 98
	[SuppressUnmanagedCodeSecurity]
	internal static class NativeMethods
	{
		// Token: 0x060002F9 RID: 761
		[DllImport("advapi32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		internal static extern bool LogonUser([In] string lpszUserName, [In] string lpszDomain, [In] string lpszPassword, [In] uint dwLogonType, [In] uint dwLogonProvider, out SafeCloseHandle phToken);

		// Token: 0x060002FA RID: 762
		[DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		internal static extern bool GetTokenInformation([In] IntPtr tokenHandle, [In] uint tokenInformationClass, [In] SafeHGlobalHandle tokenInformation, [In] uint tokenInformationLength, out uint returnLength);

		// Token: 0x060002FB RID: 763
		[DllImport("advapi32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		internal static extern bool CryptAcquireContextW(out SafeProvHandle phProv, [In] string pszContainer, [In] string pszProvider, [In] uint dwProvType, [In] uint dwFlags);

		// Token: 0x060002FC RID: 764
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		internal unsafe static extern bool CryptImportKey([In] SafeProvHandle hProv, [In] void* pbData, [In] uint dwDataLen, [In] IntPtr hPubKey, [In] uint dwFlags, out SafeKeyHandle phKey);

		// Token: 0x060002FD RID: 765
		[DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		internal static extern bool CryptGetKeyParam([In] SafeKeyHandle phKey, [In] uint dwParam, [In] IntPtr pbData, [In] [Out] ref uint dwDataLen, [In] uint dwFlags);

		// Token: 0x060002FE RID: 766
		[DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		internal unsafe static extern bool CryptSetKeyParam([In] SafeKeyHandle phKey, [In] uint dwParam, [In] void* pbData, [In] uint dwFlags);

		// Token: 0x060002FF RID: 767
		[DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		internal unsafe static extern bool CryptEncrypt([In] SafeKeyHandle phKey, [In] IntPtr hHash, [In] bool final, [In] uint dwFlags, [In] void* pbData, [In] [Out] ref int dwDataLen, [In] int dwBufLen);

		// Token: 0x06000300 RID: 768
		[DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		internal unsafe static extern bool CryptDecrypt([In] SafeKeyHandle phKey, [In] IntPtr hHash, [In] bool final, [In] uint dwFlags, [In] void* pbData, [In] [Out] ref int dwDataLen);

		// Token: 0x06000301 RID: 769
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		internal static extern bool CryptDestroyKey([In] IntPtr phKey);

		// Token: 0x06000302 RID: 770
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		internal static extern bool CryptReleaseContext([In] IntPtr hProv, [In] uint dwFlags);

		// Token: 0x06000303 RID: 771
		[DllImport("advapi32.dll", CharSet = CharSet.Unicode, ExactSpelling = true, SetLastError = true)]
		internal static extern bool LookupPrivilegeValueW([In] string lpSystemName, [In] string lpName, out LUID Luid);

		// Token: 0x06000304 RID: 772
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[DllImport("advapi32.dll", SetLastError = true)]
		internal static extern bool AdjustTokenPrivileges([In] SafeCloseHandle tokenHandle, [In] bool disableAllPrivileges, [In] ref TOKEN_PRIVILEGE newState, [In] uint bufferLength, out TOKEN_PRIVILEGE previousState, out uint returnLength);

		// Token: 0x06000305 RID: 773
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[DllImport("advapi32.dll", SetLastError = true)]
		internal static extern bool RevertToSelf();

		// Token: 0x06000306 RID: 774
		[DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		internal static extern bool OpenProcessToken([In] IntPtr processToken, [In] TokenAccessLevels desiredAccess, out SafeCloseHandle tokenHandle);

		// Token: 0x06000307 RID: 775
		[DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		internal static extern bool OpenThreadToken([In] IntPtr threadHandle, [In] TokenAccessLevels desiredAccess, [In] bool openAsSelf, out SafeCloseHandle tokenHandle);

		// Token: 0x06000308 RID: 776
		[DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		internal static extern IntPtr GetCurrentProcess();

		// Token: 0x06000309 RID: 777
		[DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		internal static extern IntPtr GetCurrentThread();

		// Token: 0x0600030A RID: 778
		[DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		internal static extern bool DuplicateTokenEx([In] SafeCloseHandle existingTokenHandle, [In] TokenAccessLevels desiredAccess, [In] IntPtr tokenAttributes, [In] SECURITY_IMPERSONATION_LEVEL impersonationLevel, [In] TokenType tokenType, out SafeCloseHandle duplicateTokenHandle);

		// Token: 0x0600030B RID: 779
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		internal static extern bool SetThreadToken([In] IntPtr threadHandle, [In] SafeCloseHandle threadToken);

		// Token: 0x0600030C RID: 780
		[DllImport("secur32.dll", CharSet = CharSet.Auto)]
		internal static extern int LsaRegisterLogonProcess([In] ref UNICODE_INTPTR_STRING logonProcessName, out SafeLsaLogonProcessHandle lsaHandle, out IntPtr securityMode);

		// Token: 0x0600030D RID: 781
		[DllImport("secur32.dll", CharSet = CharSet.Auto)]
		internal static extern int LsaConnectUntrusted(out SafeLsaLogonProcessHandle lsaHandle);

		// Token: 0x0600030E RID: 782
		[DllImport("advapi32.dll", CharSet = CharSet.Unicode)]
		internal static extern int LsaNtStatusToWinError([In] int status);

		// Token: 0x0600030F RID: 783
		[DllImport("secur32.dll", CharSet = CharSet.Auto)]
		internal static extern int LsaLookupAuthenticationPackage([In] SafeLsaLogonProcessHandle lsaHandle, [In] ref UNICODE_INTPTR_STRING packageName, out uint authenticationPackage);

		// Token: 0x06000310 RID: 784
		[DllImport("advapi32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		internal static extern bool AllocateLocallyUniqueId(out LUID Luid);

		// Token: 0x06000311 RID: 785
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[DllImport("secur32.dll")]
		internal static extern int LsaFreeReturnBuffer(IntPtr handle);

		// Token: 0x06000312 RID: 786
		[DllImport("secur32.dll", CharSet = CharSet.Auto)]
		internal static extern int LsaLogonUser([In] SafeLsaLogonProcessHandle LsaHandle, [In] ref UNICODE_INTPTR_STRING OriginName, [In] SecurityLogonType LogonType, [In] uint AuthenticationPackage, [In] IntPtr AuthenticationInformation, [In] uint AuthenticationInformationLength, [In] IntPtr LocalGroups, [In] ref TOKEN_SOURCE SourceContext, out SafeLsaReturnBufferHandle ProfileBuffer, out uint ProfileBufferLength, out LUID LogonId, out SafeCloseHandle Token, out QUOTA_LIMITS Quotas, out int SubStatus);

		// Token: 0x06000313 RID: 787
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[DllImport("secur32.dll", CharSet = CharSet.Auto)]
		internal static extern int LsaDeregisterLogonProcess([In] IntPtr handle);

		// Token: 0x06000314 RID: 788
		[DllImport("credui.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		internal static extern uint SspiPromptForCredentials(string pszTargetName, ref CREDUI_INFO pUiInfo, uint dwAuthError, string pszPackage, IntPtr authIdentity, out IntPtr ppAuthIdentity, [MarshalAs(UnmanagedType.Bool)] ref bool pfSave, uint dwFlags);

		// Token: 0x06000315 RID: 789
		[DllImport("credui.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		[return: MarshalAs(UnmanagedType.U1)]
		internal static extern bool SspiIsPromptingNeeded(uint ErrorOrNtStatus);

		// Token: 0x06000316 RID: 790
		[DllImport("secur32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		[return: MarshalAs(UnmanagedType.U1)]
		internal static extern bool TranslateName(string input, EXTENDED_NAME_FORMAT inputFormat, EXTENDED_NAME_FORMAT outputFormat, StringBuilder outputString, out uint size);

		// Token: 0x04000322 RID: 802
		private const string ADVAPI32 = "advapi32.dll";

		// Token: 0x04000323 RID: 803
		private const string KERNEL32 = "kernel32.dll";

		// Token: 0x04000324 RID: 804
		private const string SECUR32 = "secur32.dll";

		// Token: 0x04000325 RID: 805
		private const string CREDUI = "credui.dll";

		// Token: 0x04000326 RID: 806
		internal const uint STATUS_NO_MEMORY = 3221225495U;

		// Token: 0x04000327 RID: 807
		internal const uint STATUS_INSUFFICIENT_RESOURCES = 3221225626U;

		// Token: 0x04000328 RID: 808
		internal const uint STATUS_ACCESS_DENIED = 3221225506U;

		// Token: 0x04000329 RID: 809
		internal const uint STATUS_ACCOUNT_RESTRICTION = 3221225582U;

		// Token: 0x0400032A RID: 810
		internal static byte[] LsaSourceName = new byte[]
		{
			87,
			67,
			70
		};

		// Token: 0x0400032B RID: 811
		internal static byte[] LsaKerberosName = new byte[]
		{
			75,
			101,
			114,
			98,
			101,
			114,
			111,
			115
		};

		// Token: 0x0400032C RID: 812
		internal const uint KERB_CERTIFICATE_S4U_LOGON_FLAG_CHECK_DUPLICATES = 1U;

		// Token: 0x0400032D RID: 813
		internal const uint KERB_CERTIFICATE_S4U_LOGON_FLAG_CHECK_LOGONHOURS = 2U;

		// Token: 0x0400032E RID: 814
		internal const int ERROR_ACCESS_DENIED = 5;

		// Token: 0x0400032F RID: 815
		internal const int ERROR_BAD_LENGTH = 24;

		// Token: 0x04000330 RID: 816
		internal const int ERROR_INSUFFICIENT_BUFFER = 122;

		// Token: 0x04000331 RID: 817
		internal const uint SE_GROUP_ENABLED = 4U;

		// Token: 0x04000332 RID: 818
		internal const uint SE_GROUP_USE_FOR_DENY_ONLY = 16U;

		// Token: 0x04000333 RID: 819
		internal const uint SE_GROUP_LOGON_ID = 3221225472U;

		// Token: 0x04000334 RID: 820
		internal const int PROV_RSA_AES = 24;

		// Token: 0x04000335 RID: 821
		internal const int KP_IV = 1;

		// Token: 0x04000336 RID: 822
		internal const uint CRYPT_DELETEKEYSET = 16U;

		// Token: 0x04000337 RID: 823
		internal const uint CRYPT_VERIFYCONTEXT = 4026531840U;

		// Token: 0x04000338 RID: 824
		internal const byte PLAINTEXTKEYBLOB = 8;

		// Token: 0x04000339 RID: 825
		internal const byte CUR_BLOB_VERSION = 2;

		// Token: 0x0400033A RID: 826
		internal const int ALG_CLASS_DATA_ENCRYPT = 24576;

		// Token: 0x0400033B RID: 827
		internal const int ALG_TYPE_BLOCK = 1536;

		// Token: 0x0400033C RID: 828
		internal const int CALG_AES_128 = 26126;

		// Token: 0x0400033D RID: 829
		internal const int CALG_AES_192 = 26127;

		// Token: 0x0400033E RID: 830
		internal const int CALG_AES_256 = 26128;
	}
}
