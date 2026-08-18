using System;
using System.IdentityModel;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Security;
using System.Security.Principal;
using System.Text;

namespace System.ServiceModel.ComIntegration
{
	// Token: 0x02000255 RID: 597
	[SuppressUnmanagedCodeSecurity]
	internal static class SafeNativeMethods
	{
		// Token: 0x0600112A RID: 4394
		[DllImport("advapi32.dll", BestFitMapping = false, CharSet = CharSet.Unicode)]
		internal static extern int RegOpenKeyEx(RegistryHandle hKey, string lpSubKey, int ulOptions, int samDesired, out RegistryHandle hkResult);

		// Token: 0x0600112B RID: 4395
		[DllImport("advapi32.dll", BestFitMapping = false, CharSet = CharSet.Unicode)]
		internal static extern int RegSetValueEx(RegistryHandle hKey, string lpValueName, int Reserved, int dwType, string val, int cbData);

		// Token: 0x0600112C RID: 4396
		[DllImport("advapi32.dll")]
		internal static extern int RegCloseKey(IntPtr handle);

		// Token: 0x0600112D RID: 4397
		[DllImport("advapi32.dll", BestFitMapping = false, CharSet = CharSet.Unicode)]
		internal static extern int RegQueryValueEx(RegistryHandle hKey, string lpValueName, int[] lpReserved, ref int lpType, [Out] byte[] lpData, ref int lpcbData);

		// Token: 0x0600112E RID: 4398
		[DllImport("advapi32.dll", BestFitMapping = false, CharSet = CharSet.Unicode)]
		internal static extern int RegEnumKey(RegistryHandle hKey, int index, StringBuilder lpName, ref int len);

		// Token: 0x0600112F RID: 4399
		[DllImport("advapi32.dll", BestFitMapping = false, CharSet = CharSet.Unicode)]
		internal static extern int RegDeleteKey(RegistryHandle hKey, string lpValueName);

		// Token: 0x06001130 RID: 4400
		[DllImport("advapi32.dll", SetLastError = true)]
		internal static extern bool DuplicateTokenEx([In] SafeCloseHandle ExistingToken, [In] TokenAccessLevels DesiredAccess, [In] IntPtr TokenAttributes, [In] SecurityImpersonationLevel ImpersonationLevel, [In] TokenType TokenType, out SafeCloseHandle NewToken);

		// Token: 0x06001131 RID: 4401
		[DllImport("advapi32.dll", SetLastError = true)]
		internal static extern bool AccessCheck([In] byte[] SecurityDescriptor, [In] SafeCloseHandle ClientToken, [In] int DesiredAccess, [In] GENERIC_MAPPING GenericMapping, out PRIVILEGE_SET PrivilegeSet, [In] [Out] ref uint PrivilegeSetLength, out uint GrantedAccess, out bool AccessStatus);

		// Token: 0x06001132 RID: 4402
		[DllImport("advapi32.dll", EntryPoint = "ImpersonateAnonymousToken", SetLastError = true)]
		internal static extern bool ImpersonateAnonymousUserOnCurrentThread([In] IntPtr CurrentThread);

		// Token: 0x06001133 RID: 4403
		[DllImport("advapi32.dll", EntryPoint = "OpenThreadToken", SetLastError = true)]
		internal static extern bool OpenCurrentThreadToken([In] IntPtr ThreadHandle, [In] TokenAccessLevels DesiredAccess, [In] bool OpenAsSelf, out SafeCloseHandle TokenHandle);

		// Token: 0x06001134 RID: 4404
		[DllImport("advapi32.dll", EntryPoint = "SetThreadToken", SetLastError = true)]
		internal static extern bool SetCurrentThreadToken([In] IntPtr ThreadHandle, [In] SafeCloseHandle TokenHandle);

		// Token: 0x06001135 RID: 4405
		[DllImport("kernel32.dll", SetLastError = true)]
		internal static extern IntPtr GetCurrentThread();

		// Token: 0x06001136 RID: 4406
		[DllImport("kernel32.dll")]
		internal static extern int GetCurrentThreadId();

		// Token: 0x06001137 RID: 4407
		[DllImport("advapi32.dll", SetLastError = true)]
		internal static extern bool RevertToSelf();

		// Token: 0x06001138 RID: 4408
		[DllImport("advapi32.dll", SetLastError = true)]
		internal static extern bool GetTokenInformation([In] SafeCloseHandle TokenHandle, [In] TOKEN_INFORMATION_CLASS TokenInformationClass, [In] SafeHandle TokenInformation, [Out] uint TokenInformationLength, out uint ReturnLength);

		// Token: 0x06001139 RID: 4409
		[DllImport("kernel32.dll", SetLastError = true)]
		internal static extern IntPtr GetCurrentProcess();

		// Token: 0x0600113A RID: 4410
		[DllImport("advapi32.dll", EntryPoint = "OpenProcessToken", SetLastError = true)]
		internal static extern bool GetCurrentProcessToken([In] IntPtr ProcessHandle, [In] TokenAccessLevels DesiredAccess, out SafeCloseHandle TokenHandle);

		// Token: 0x0600113B RID: 4411
		[DllImport("ole32.dll", ExactSpelling = true, PreserveSig = false)]
		[return: MarshalAs(UnmanagedType.Interface)]
		public static extern object CoCreateInstance([MarshalAs(UnmanagedType.LPStruct)] [In] Guid rclsid, [MarshalAs(UnmanagedType.IUnknown)] [In] object pUnkOuter, [In] CLSCTX dwClsContext, [MarshalAs(UnmanagedType.LPStruct)] [In] Guid riid);

		// Token: 0x0600113C RID: 4412
		[DllImport("ole32.dll", ExactSpelling = true, PreserveSig = false)]
		[return: MarshalAs(UnmanagedType.Interface)]
		public static extern IStream CreateStreamOnHGlobal([In] SafeHGlobalHandle hGlobal, [MarshalAs(UnmanagedType.Bool)] [In] bool fDeleteOnRelease);

		// Token: 0x0600113D RID: 4413
		[DllImport("ole32.dll", ExactSpelling = true, PreserveSig = false)]
		public static extern SafeHGlobalHandle GetHGlobalFromStream(IStream stream);

		// Token: 0x0600113E RID: 4414
		[DllImport("ole32.dll", ExactSpelling = true, PreserveSig = false)]
		[return: MarshalAs(UnmanagedType.Interface)]
		public static extern object CoGetObjectContext([MarshalAs(UnmanagedType.LPStruct)] [In] Guid riid);

		// Token: 0x0600113F RID: 4415
		[DllImport("comsvcs.dll", ExactSpelling = true, PreserveSig = false)]
		[return: MarshalAs(UnmanagedType.Interface)]
		public static extern object CoCreateActivity([MarshalAs(UnmanagedType.IUnknown)] [In] object pIUnknown, [MarshalAs(UnmanagedType.LPStruct)] [In] Guid riid);

		// Token: 0x06001140 RID: 4416
		[DllImport("ole32.dll", ExactSpelling = true, PreserveSig = false)]
		internal static extern IntPtr CoSwitchCallContext(IntPtr newSecurityObject);

		// Token: 0x06001141 RID: 4417
		[DllImport("kernel32.dll", ExactSpelling = true)]
		internal static extern IntPtr GlobalLock(SafeHGlobalHandle hGlobal);

		// Token: 0x06001142 RID: 4418
		[DllImport("kernel32.dll", ExactSpelling = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool GlobalUnlock(SafeHGlobalHandle hGlobal);

		// Token: 0x06001143 RID: 4419
		[DllImport("kernel32.dll", ExactSpelling = true)]
		internal static extern IntPtr GlobalSize(SafeHGlobalHandle hGlobal);

		// Token: 0x06001144 RID: 4420
		[DllImport("oleaut32.dll", CharSet = CharSet.Unicode, ExactSpelling = true)]
		internal static extern int LoadRegTypeLib(ref Guid rguid, ushort major, ushort minor, int lcid, [MarshalAs(UnmanagedType.Interface)] out object typeLib);

		// Token: 0x06001145 RID: 4421
		[DllImport("oleaut32.dll", CharSet = CharSet.Unicode, ExactSpelling = true)]
		internal static extern int SafeArrayGetDim(IntPtr pSafeArray);

		// Token: 0x06001146 RID: 4422
		[DllImport("oleaut32.dll", CharSet = CharSet.Unicode, ExactSpelling = true)]
		internal static extern int SafeArrayGetElemsize(IntPtr pSafeArray);

		// Token: 0x06001147 RID: 4423
		[DllImport("oleaut32.dll", CharSet = CharSet.Unicode, ExactSpelling = true, PreserveSig = false)]
		internal static extern int SafeArrayGetLBound(IntPtr pSafeArray, int cDims);

		// Token: 0x06001148 RID: 4424
		[DllImport("oleaut32.dll", CharSet = CharSet.Unicode, ExactSpelling = true, PreserveSig = false)]
		internal static extern int SafeArrayGetUBound(IntPtr pSafeArray, int cDims);

		// Token: 0x06001149 RID: 4425
		[DllImport("oleaut32.dll", CharSet = CharSet.Unicode, ExactSpelling = true, PreserveSig = false)]
		internal static extern IntPtr SafeArrayAccessData(IntPtr pSafeArray);

		// Token: 0x0600114A RID: 4426
		[DllImport("oleaut32.dll", CharSet = CharSet.Unicode, ExactSpelling = true, PreserveSig = false)]
		internal static extern void SafeArrayUnaccessData(IntPtr pSafeArray);

		// Token: 0x0600114B RID: 4427
		[DllImport("secur32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		[return: MarshalAs(UnmanagedType.U1)]
		internal static extern bool TranslateName(string input, EXTENDED_NAME_FORMAT inputFormat, EXTENDED_NAME_FORMAT outputFormat, StringBuilder outputString, out uint size);

		// Token: 0x0600114C RID: 4428
		[DllImport("netapi32.dll", CharSet = CharSet.Unicode, EntryPoint = "DsGetDcNameW", ExactSpelling = true, SetLastError = true)]
		internal static extern int DsGetDcName([In] string computerName, [In] string domainName, [In] IntPtr domainGuid, [In] string siteName, [In] uint flags, out IntPtr domainControllerInfo);

		// Token: 0x0600114D RID: 4429
		[DllImport("netapi32.dll")]
		internal static extern int NetApiBufferFree([In] IntPtr buffer);

		// Token: 0x0400193C RID: 6460
		internal const string KERNEL32 = "kernel32.dll";

		// Token: 0x0400193D RID: 6461
		internal const string ADVAPI32 = "advapi32.dll";

		// Token: 0x0400193E RID: 6462
		internal const string OLE32 = "ole32.dll";

		// Token: 0x0400193F RID: 6463
		internal const string OLEAUT32 = "oleaut32.dll";

		// Token: 0x04001940 RID: 6464
		internal const string COMSVCS = "comsvcs.dll";

		// Token: 0x04001941 RID: 6465
		internal const string SECUR32 = "secur32.dll";

		// Token: 0x04001942 RID: 6466
		internal const string NETAPI32 = "netapi32.dll";

		// Token: 0x04001943 RID: 6467
		internal const int ERROR_MORE_DATA = 234;

		// Token: 0x04001944 RID: 6468
		internal const int ERROR_SUCCESS = 0;

		// Token: 0x04001945 RID: 6469
		internal const int ERROR_INVALID_HANDLE = 6;

		// Token: 0x04001946 RID: 6470
		internal const int ERROR_NOT_SUPPORTED = 50;

		// Token: 0x04001947 RID: 6471
		internal const int READ_CONTROL = 131072;

		// Token: 0x04001948 RID: 6472
		internal const int SYNCHRONIZE = 1048576;

		// Token: 0x04001949 RID: 6473
		internal const int STANDARD_RIGHTS_READ = 131072;

		// Token: 0x0400194A RID: 6474
		internal const int STANDARD_RIGHTS_WRITE = 131072;

		// Token: 0x0400194B RID: 6475
		internal const int KEY_QUERY_VALUE = 1;

		// Token: 0x0400194C RID: 6476
		internal const int KEY_SET_VALUE = 2;

		// Token: 0x0400194D RID: 6477
		internal const int KEY_CREATE_SUB_KEY = 4;

		// Token: 0x0400194E RID: 6478
		internal const int KEY_ENUMERATE_SUB_KEYS = 8;

		// Token: 0x0400194F RID: 6479
		internal const int KEY_NOTIFY = 16;

		// Token: 0x04001950 RID: 6480
		internal const int KEY_CREATE_LINK = 32;

		// Token: 0x04001951 RID: 6481
		internal const int KEY_READ = 131097;

		// Token: 0x04001952 RID: 6482
		internal const int KEY_WRITE = 131078;

		// Token: 0x04001953 RID: 6483
		internal const int REG_NONE = 0;

		// Token: 0x04001954 RID: 6484
		internal const int REG_SZ = 1;

		// Token: 0x04001955 RID: 6485
		internal const int REG_EXPAND_SZ = 2;

		// Token: 0x04001956 RID: 6486
		internal const int KEY_WOW64_32KEY = 512;

		// Token: 0x04001957 RID: 6487
		internal const int KEY_WOW64_64KEY = 256;

		// Token: 0x04001958 RID: 6488
		internal const int REG_BINARY = 3;

		// Token: 0x04001959 RID: 6489
		internal const int REG_DWORD = 4;

		// Token: 0x0400195A RID: 6490
		internal const int REG_DWORD_LITTLE_ENDIAN = 4;

		// Token: 0x0400195B RID: 6491
		internal const int REG_DWORD_BIG_ENDIAN = 5;

		// Token: 0x0400195C RID: 6492
		internal const int REG_LINK = 6;

		// Token: 0x0400195D RID: 6493
		internal const int REG_MULTI_SZ = 7;

		// Token: 0x0400195E RID: 6494
		internal const int REG_RESOURCE_LIST = 8;

		// Token: 0x0400195F RID: 6495
		internal const int REG_FULL_RESOURCE_DESCRIPTOR = 9;

		// Token: 0x04001960 RID: 6496
		internal const int REG_RESOURCE_REQUIREMENTS_LIST = 10;

		// Token: 0x04001961 RID: 6497
		internal const int REG_QWORD = 11;

		// Token: 0x04001962 RID: 6498
		internal const int HWND_BROADCAST = 65535;

		// Token: 0x04001963 RID: 6499
		internal const int WM_SETTINGCHANGE = 26;
	}
}
