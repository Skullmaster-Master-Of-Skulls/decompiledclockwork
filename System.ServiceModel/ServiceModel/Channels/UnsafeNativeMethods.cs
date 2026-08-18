using System;
using System.ComponentModel;
using System.EnterpriseServices;
using System.Runtime;
using System.Runtime.ConstrainedExecution;
using System.Runtime.Interop;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using System.Security.Principal;
using System.ServiceModel.Activation;
using System.Text;
using System.Threading;
using System.Transactions;
using Microsoft.Win32.SafeHandles;

namespace System.ServiceModel.Channels
{
	// Token: 0x020008BF RID: 2239
	[SuppressUnmanagedCodeSecurity]
	internal static class UnsafeNativeMethods
	{
		// Token: 0x0600555A RID: 21850
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[DllImport("kernel32.dll")]
		internal static extern int CloseHandle(IntPtr handle);

		// Token: 0x0600555B RID: 21851
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[DllImport("secur32.dll")]
		internal static extern int SspiFreeAuthIdentity([In] IntPtr ppAuthIdentity);

		// Token: 0x0600555C RID: 21852
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[DllImport("secur32.dll")]
		internal static extern uint SspiExcludePackage([In] IntPtr AuthIdentity, [MarshalAs(UnmanagedType.LPWStr)] [In] string pszPackageName, out IntPtr ppNewAuthIdentity);

		// Token: 0x0600555D RID: 21853
		[DllImport("kernel32.dll", SetLastError = true)]
		internal unsafe static extern int ConnectNamedPipe(PipeHandle handle, NativeOverlapped* lpOverlapped);

		// Token: 0x0600555E RID: 21854
		[DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		internal static extern PipeHandle CreateFile(string lpFileName, int dwDesiredAccess, int dwShareMode, IntPtr lpSECURITY_ATTRIBUTES, int dwCreationDisposition, int dwFlagsAndAttributes, IntPtr hTemplateFile);

		// Token: 0x0600555F RID: 21855
		[DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		internal static extern SafeFileMappingHandle CreateFileMapping(IntPtr fileHandle, UnsafeNativeMethods.SECURITY_ATTRIBUTES securityAttributes, int protect, int sizeHigh, int sizeLow, string name);

		// Token: 0x06005560 RID: 21856
		[DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		internal static extern PipeHandle CreateNamedPipe(string name, int openMode, int pipeMode, int maxInstances, int outBufSize, int inBufSize, int timeout, UnsafeNativeMethods.SECURITY_ATTRIBUTES securityAttributes);

		// Token: 0x06005561 RID: 21857
		[DllImport("kernel32.dll", SetLastError = true)]
		internal static extern int DisconnectNamedPipe(PipeHandle handle);

		// Token: 0x06005562 RID: 21858
		[DllImport("kernel32.dll", ExactSpelling = true, SetLastError = true)]
		internal static extern bool DuplicateHandle(IntPtr hSourceProcessHandle, PipeHandle hSourceHandle, SafeCloseHandle hTargetProcessHandle, out IntPtr lpTargetHandle, int dwDesiredAccess, bool bInheritHandle, int dwOptions);

		// Token: 0x06005563 RID: 21859
		[DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
		internal static extern int FormatMessage(int dwFlags, IntPtr lpSource, int dwMessageId, int dwLanguageId, StringBuilder lpBuffer, int nSize, IntPtr arguments);

		// Token: 0x06005564 RID: 21860
		[DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
		internal static extern int FormatMessage(int dwFlags, SafeLibraryHandle lpSource, int dwMessageId, int dwLanguageId, StringBuilder lpBuffer, int nSize, IntPtr arguments);

		// Token: 0x06005565 RID: 21861
		[DllImport("kernel32.dll", SetLastError = true)]
		internal unsafe static extern int GetOverlappedResult(PipeHandle handle, NativeOverlapped* overlapped, out int bytesTransferred, int wait);

		// Token: 0x06005566 RID: 21862
		[DllImport("kernel32.dll", SetLastError = true)]
		internal unsafe static extern int GetOverlappedResult(IntPtr handle, NativeOverlapped* overlapped, out int bytesTransferred, int wait);

		// Token: 0x06005567 RID: 21863 RVA: 0x00139683 File Offset: 0x00137883
		[SecuritySafeCritical]
		[PermissionSet(SecurityAction.Demand, Unrestricted = true)]
		internal unsafe static bool HasOverlappedIoCompleted(NativeOverlapped* overlapped)
		{
			return overlapped->InternalLow != (IntPtr)259;
		}

		// Token: 0x06005568 RID: 21864
		[DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		internal static extern SafeFileMappingHandle OpenFileMapping(int access, bool inheritHandle, string name);

		// Token: 0x06005569 RID: 21865
		[DllImport("kernel32.dll", SetLastError = true)]
		internal static extern SafeViewOfFileHandle MapViewOfFile(SafeFileMappingHandle handle, int dwDesiredAccess, int dwFileOffsetHigh, int dwFileOffsetLow, IntPtr dwNumberOfBytesToMap);

		// Token: 0x0600556A RID: 21866
		[SuppressUnmanagedCodeSecurity]
		[HostProtection(SecurityAction.LinkDemand)]
		[DllImport("kernel32.dll", SetLastError = true)]
		public static extern int QueryPerformanceCounter(out long time);

		// Token: 0x0600556B RID: 21867
		[DllImport("kernel32.dll", SetLastError = true)]
		internal unsafe static extern int ReadFile(IntPtr handle, byte* bytes, int numBytesToRead, IntPtr numBytesRead_mustBeZero, NativeOverlapped* overlapped);

		// Token: 0x0600556C RID: 21868
		[DllImport("kernel32.dll", SetLastError = true)]
		internal static extern int SetNamedPipeHandleState(PipeHandle handle, ref int mode, IntPtr collectionCount, IntPtr collectionDataTimeout);

		// Token: 0x0600556D RID: 21869
		[DllImport("kernel32.dll", SetLastError = true)]
		internal unsafe static extern int WriteFile(IntPtr handle, byte* bytes, int numBytesToWrite, IntPtr numBytesWritten_mustBeZero, NativeOverlapped* lpOverlapped);

		// Token: 0x0600556E RID: 21870
		[DllImport("kernel32.dll", SetLastError = true)]
		internal static extern bool GetNamedPipeClientProcessId(PipeHandle handle, out int id);

		// Token: 0x0600556F RID: 21871
		[DllImport("kernel32.dll", SetLastError = true)]
		internal static extern bool GetNamedPipeServerProcessId(PipeHandle handle, out int id);

		// Token: 0x06005570 RID: 21872
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[DllImport("kernel32.dll", ExactSpelling = true)]
		internal static extern int UnmapViewOfFile(IntPtr lpBaseAddress);

		// Token: 0x06005571 RID: 21873
		[DllImport("kernel32.dll", ExactSpelling = true)]
		public static extern bool SetWaitableTimer(SafeWaitHandle handle, ref long dueTime, int period, IntPtr mustBeZero, IntPtr mustBeZeroAlso, bool resume);

		// Token: 0x06005572 RID: 21874
		[DllImport("kernel32.dll", BestFitMapping = false, CharSet = CharSet.Auto)]
		public static extern SafeWaitHandle CreateWaitableTimer(IntPtr mustBeZero, bool manualReset, string timerName);

		// Token: 0x06005573 RID: 21875
		[DllImport("ws2_32.dll", SetLastError = true)]
		internal unsafe static extern int WSARecv(IntPtr handle, UnsafeNativeMethods.WSABuffer* buffers, int bufferCount, out int bytesTransferred, ref int socketFlags, NativeOverlapped* nativeOverlapped, IntPtr completionRoutine);

		// Token: 0x06005574 RID: 21876
		[DllImport("ws2_32.dll", SetLastError = true)]
		internal unsafe static extern bool WSAGetOverlappedResult(IntPtr socketHandle, NativeOverlapped* overlapped, out int bytesTransferred, bool wait, out uint flags);

		// Token: 0x06005575 RID: 21877 RVA: 0x0013969A File Offset: 0x0013789A
		internal static string GetComputerName(ComputerNameFormat nameType)
		{
			return UnsafeNativeMethods.GetComputerName(nameType);
		}

		// Token: 0x06005576 RID: 21878
		[DllImport("userenv.dll", SetLastError = true)]
		internal static extern int DeriveAppContainerSidFromAppContainerName([MarshalAs(UnmanagedType.LPWStr)] [In] string appContainerName, out IntPtr appContainerSid);

		// Token: 0x06005577 RID: 21879
		[DllImport("advapi32.dll", SetLastError = true)]
		internal static extern IntPtr FreeSid(IntPtr pSid);

		// Token: 0x06005578 RID: 21880
		[DllImport("kernel32.dll")]
		internal static extern int PackageFamilyNameFromFullName([MarshalAs(UnmanagedType.LPWStr)] [In] string packageFullName, ref uint packageFamilyNameLength, [MarshalAs(UnmanagedType.LPWStr)] [In] [Out] StringBuilder packageFamilyName);

		// Token: 0x06005579 RID: 21881
		[DllImport("kernel32.dll", SetLastError = true)]
		internal static extern bool GetAppContainerNamedObjectPath(IntPtr token, IntPtr appContainerSid, uint objectPathLength, [MarshalAs(UnmanagedType.LPWStr)] [In] [Out] StringBuilder objectPath, ref uint returnLength);

		// Token: 0x0600557A RID: 21882
		[DllImport("kernel32.dll", SetLastError = true)]
		internal static extern IntPtr GetCurrentProcess();

		// Token: 0x0600557B RID: 21883
		[DllImport("advapi32.dll", SetLastError = true)]
		internal static extern bool OpenProcessToken(IntPtr ProcessHandle, TokenAccessLevels DesiredAccess, out SafeCloseHandle TokenHandle);

		// Token: 0x0600557C RID: 21884
		[DllImport("advapi32.dll", SetLastError = true)]
		private static extern bool GetTokenInformation(SafeCloseHandle tokenHandle, ListenerUnsafeNativeMethods.TOKEN_INFORMATION_CLASS tokenInformationClass, byte[] tokenInformation, uint tokenInformationLength, out uint returnLength);

		// Token: 0x0600557D RID: 21885
		[DllImport("advapi32.dll", SetLastError = true)]
		private static extern bool GetTokenInformation(SafeCloseHandle tokenHandle, ListenerUnsafeNativeMethods.TOKEN_INFORMATION_CLASS tokenInformationClass, out uint tokenInformation, uint tokenInformationLength, out uint returnLength);

		// Token: 0x0600557E RID: 21886 RVA: 0x001396A4 File Offset: 0x001378A4
		internal unsafe static SecurityIdentifier GetAppContainerSid(SafeCloseHandle tokenHandle)
		{
			uint tokenInformationLength = UnsafeNativeMethods.GetTokenInformationLength(tokenHandle, ListenerUnsafeNativeMethods.TOKEN_INFORMATION_CLASS.TokenAppContainerSid);
			byte[] array = new byte[tokenInformationLength];
			byte[] array2;
			byte* ptr;
			if ((array2 = array) == null || array2.Length == 0)
			{
				ptr = null;
			}
			else
			{
				ptr = &array2[0];
			}
			if (!UnsafeNativeMethods.GetTokenInformation(tokenHandle, ListenerUnsafeNativeMethods.TOKEN_INFORMATION_CLASS.TokenAppContainerSid, array, tokenInformationLength, out tokenInformationLength))
			{
				int lastWin32Error = Marshal.GetLastWin32Error();
				throw FxTrace.Exception.AsError(new Win32Exception(lastWin32Error));
			}
			UnsafeNativeMethods.TokenAppContainerInfo* ptr2 = (UnsafeNativeMethods.TokenAppContainerInfo*)ptr;
			return new SecurityIdentifier(ptr2->psid);
		}

		// Token: 0x0600557F RID: 21887 RVA: 0x00139710 File Offset: 0x00137910
		private static uint GetTokenInformationLength(SafeCloseHandle token, ListenerUnsafeNativeMethods.TOKEN_INFORMATION_CLASS tokenInformationClass)
		{
			uint result;
			if (!UnsafeNativeMethods.GetTokenInformation(token, tokenInformationClass, null, 0U, out result))
			{
				int lastWin32Error = Marshal.GetLastWin32Error();
				if (lastWin32Error != 122)
				{
					throw FxTrace.Exception.AsError(new Win32Exception(lastWin32Error));
				}
			}
			return result;
		}

		// Token: 0x06005580 RID: 21888 RVA: 0x0013974C File Offset: 0x0013794C
		internal static int GetSessionId(SafeCloseHandle tokenHandle)
		{
			uint result;
			uint num;
			if (!UnsafeNativeMethods.GetTokenInformation(tokenHandle, ListenerUnsafeNativeMethods.TOKEN_INFORMATION_CLASS.TokenSessionId, out result, 4U, out num))
			{
				int lastWin32Error = Marshal.GetLastWin32Error();
				throw FxTrace.Exception.AsError(new Win32Exception(lastWin32Error));
			}
			return (int)result;
		}

		// Token: 0x06005581 RID: 21889 RVA: 0x00139780 File Offset: 0x00137980
		internal static bool RunningInAppContainer(SafeCloseHandle tokenHandle)
		{
			uint num;
			uint num2;
			if (!UnsafeNativeMethods.GetTokenInformation(tokenHandle, ListenerUnsafeNativeMethods.TOKEN_INFORMATION_CLASS.TokenIsAppContainer, out num, 4U, out num2))
			{
				int lastWin32Error = Marshal.GetLastWin32Error();
				throw FxTrace.Exception.AsError(new Win32Exception(lastWin32Error));
			}
			return num == 1U;
		}

		// Token: 0x06005582 RID: 21890
		[DllImport("mqrt.dll", CharSet = CharSet.Unicode)]
		public static extern int MQOpenQueue(string formatName, int access, int shareMode, out MsmqQueueHandle handle);

		// Token: 0x06005583 RID: 21891
		[DllImport("mqrt.dll", CharSet = CharSet.Unicode)]
		public static extern int MQBeginTransaction(out ITransaction refTransaction);

		// Token: 0x06005584 RID: 21892
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[DllImport("mqrt.dll", CharSet = CharSet.Unicode)]
		public static extern int MQCloseQueue(IntPtr handle);

		// Token: 0x06005585 RID: 21893
		[DllImport("mqrt.dll", CharSet = CharSet.Unicode)]
		public static extern int MQSendMessage(MsmqQueueHandle handle, IntPtr properties, IntPtr transaction);

		// Token: 0x06005586 RID: 21894
		[DllImport("mqrt.dll", CharSet = CharSet.Unicode)]
		public static extern int MQSendMessage(MsmqQueueHandle handle, IntPtr properties, IDtcTransaction transaction);

		// Token: 0x06005587 RID: 21895
		[DllImport("mqrt.dll", CharSet = CharSet.Unicode)]
		public unsafe static extern int MQReceiveMessage(MsmqQueueHandle handle, int timeout, int action, IntPtr properties, NativeOverlapped* nativeOverlapped, IntPtr receiveCallback, IntPtr cursorHandle, IntPtr transaction);

		// Token: 0x06005588 RID: 21896
		[DllImport("mqrt.dll", CharSet = CharSet.Unicode)]
		public unsafe static extern int MQReceiveMessage(IntPtr handle, int timeout, int action, IntPtr properties, NativeOverlapped* nativeOverlapped, IntPtr receiveCallback, IntPtr cursorHandle, IntPtr transaction);

		// Token: 0x06005589 RID: 21897
		[DllImport("mqrt.dll", CharSet = CharSet.Unicode)]
		public unsafe static extern int MQReceiveMessage(MsmqQueueHandle handle, int timeout, int action, IntPtr properties, NativeOverlapped* nativeOverlapped, IntPtr receiveCallback, IntPtr cursorHandle, IDtcTransaction transaction);

		// Token: 0x0600558A RID: 21898
		[DllImport("mqrt.dll", CharSet = CharSet.Unicode)]
		public unsafe static extern int MQReceiveMessage(IntPtr handle, int timeout, int action, IntPtr properties, NativeOverlapped* nativeOverlapped, IntPtr receiveCallback, IntPtr cursorHandle, IDtcTransaction transaction);

		// Token: 0x0600558B RID: 21899
		[DllImport("mqrt.dll", CharSet = CharSet.Unicode)]
		public unsafe static extern int MQReceiveMessage(MsmqQueueHandle handle, int timeout, int action, IntPtr properties, NativeOverlapped* nativeOverlapped, UnsafeNativeMethods.MQReceiveCallback receiveCallback, IntPtr cursorHandle, IntPtr transaction);

		// Token: 0x0600558C RID: 21900
		[DllImport("mqrt.dll", CharSet = CharSet.Unicode)]
		public unsafe static extern int MQReceiveMessage(IntPtr handle, int timeout, int action, IntPtr properties, NativeOverlapped* nativeOverlapped, IntPtr receiveCallback, IntPtr cursorHandle, ITransaction transaction);

		// Token: 0x0600558D RID: 21901
		[DllImport("mqrt.dll", CharSet = CharSet.Unicode)]
		public unsafe static extern int MQReceiveMessageByLookupId(MsmqQueueHandle handle, long lookupId, int action, IntPtr properties, NativeOverlapped* nativeOverlapped, IntPtr receiveCallback, IDtcTransaction transaction);

		// Token: 0x0600558E RID: 21902
		[DllImport("mqrt.dll", CharSet = CharSet.Unicode)]
		public unsafe static extern int MQReceiveMessageByLookupId(MsmqQueueHandle handle, long lookupId, int action, IntPtr properties, NativeOverlapped* nativeOverlapped, IntPtr receiveCallback, IntPtr transaction);

		// Token: 0x0600558F RID: 21903
		[DllImport("mqrt.dll", CharSet = CharSet.Unicode)]
		public unsafe static extern int MQReceiveMessageByLookupId(MsmqQueueHandle handle, long lookupId, int action, IntPtr properties, NativeOverlapped* nativeOverlapped, IntPtr receiveCallback, ITransaction transaction);

		// Token: 0x06005590 RID: 21904
		[DllImport("mqrt.dll", CharSet = CharSet.Unicode)]
		public static extern int MQGetPrivateComputerInformation(string computerName, IntPtr properties);

		// Token: 0x06005591 RID: 21905
		[DllImport("mqrt.dll", CharSet = CharSet.Unicode)]
		public static extern int MQMarkMessageRejected(MsmqQueueHandle handle, long lookupId);

		// Token: 0x06005592 RID: 21906
		[DllImport("mqrt.dll", CharSet = CharSet.Unicode)]
		public static extern int MQMoveMessage(MsmqQueueHandle sourceQueueHandle, MsmqQueueHandle destinationQueueHandle, long lookupId, IntPtr transaction);

		// Token: 0x06005593 RID: 21907
		[DllImport("mqrt.dll", CharSet = CharSet.Unicode)]
		public static extern int MQMoveMessage(MsmqQueueHandle sourceQueueHandle, MsmqQueueHandle destinationQueueHandle, long lookupId, IDtcTransaction transaction);

		// Token: 0x06005594 RID: 21908
		[DllImport("mqrt.dll", CharSet = CharSet.Unicode)]
		public unsafe static extern int MQGetOverlappedResult(NativeOverlapped* nativeOverlapped);

		// Token: 0x06005595 RID: 21909
		[DllImport("mqrt.dll", CharSet = CharSet.Unicode)]
		public static extern int MQGetQueueProperties(string formatName, IntPtr properties);

		// Token: 0x06005596 RID: 21910
		[DllImport("mqrt.dll", CharSet = CharSet.Unicode)]
		public static extern int MQPathNameToFormatName(string pathName, StringBuilder formatName, ref int count);

		// Token: 0x06005597 RID: 21911
		[DllImport("mqrt.dll", CharSet = CharSet.Unicode)]
		public static extern int MQMgmtGetInfo(string computerName, string objectName, IntPtr properties);

		// Token: 0x06005598 RID: 21912
		[DllImport("mqrt.dll", CharSet = CharSet.Unicode)]
		public static extern void MQFreeMemory(IntPtr nativeBuffer);

		// Token: 0x06005599 RID: 21913
		[DllImport("kernel32.dll", SetLastError = true)]
		public static extern int GetHandleInformation(MsmqQueueHandle handle, out int flags);

		// Token: 0x0600559A RID: 21914
		[DllImport("kernel32.dll", SetLastError = true)]
		public static extern bool GlobalMemoryStatusEx(ref UnsafeNativeMethods.MEMORYSTATUSEX lpBuffer);

		// Token: 0x0600559B RID: 21915
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[DllImport("kernel32.dll", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
		internal static extern IntPtr VirtualAlloc(IntPtr lpAddress, UIntPtr dwSize, uint flAllocationType, uint flProtect);

		// Token: 0x0600559C RID: 21916
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[DllImport("kernel32.dll", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
		internal static extern bool VirtualFree(IntPtr lpAddress, UIntPtr dwSize, uint dwFreeType);

		// Token: 0x0600559D RID: 21917
		[DllImport("kernel32.dll", CharSet = CharSet.Auto)]
		internal static extern IntPtr GetProcAddress(SafeLibraryHandle hModule, [MarshalAs(UnmanagedType.LPStr)] [In] string lpProcName);

		// Token: 0x0600559E RID: 21918
		[DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		internal static extern SafeLibraryHandle LoadLibrary(string libFilename);

		// Token: 0x0600559F RID: 21919
		[DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		internal static extern SafeLibraryHandle LoadLibraryEx(string lpModuleName, IntPtr hFile, uint dwFlags);

		// Token: 0x060055A0 RID: 21920
		[DllImport("bcrypt.dll", SetLastError = true)]
		internal static extern int BCryptGetFipsAlgorithmMode([MarshalAs(UnmanagedType.U1)] out bool pfEnabled);

		// Token: 0x060055A1 RID: 21921 RVA: 0x001397B7 File Offset: 0x001379B7
		private static IntPtr GetCurrentProcessToken()
		{
			return new IntPtr(-4);
		}

		// Token: 0x060055A2 RID: 21922
		[SecuritySafeCritical]
		[DllImport("kernel32.dll", EntryPoint = "AppPolicyGetClrCompat")]
		[return: MarshalAs(UnmanagedType.I4)]
		private static extern int _AppPolicyGetClrCompat(IntPtr processToken, out UnsafeNativeMethods.AppPolicyClrCompat appPolicyClrCompat);

		// Token: 0x060055A3 RID: 21923
		[SecuritySafeCritical]
		[DllImport("kernel32.dll", EntryPoint = "GetCurrentPackageId")]
		[return: MarshalAs(UnmanagedType.I4)]
		private static extern int _GetCurrentPackageId(ref int pBufferLength, byte[] pBuffer);

		// Token: 0x060055A4 RID: 21924
		[DllImport("kernel32.dll", BestFitMapping = false, CharSet = CharSet.Auto)]
		private static extern IntPtr GetModuleHandle(string modName);

		// Token: 0x060055A5 RID: 21925
		[DllImport("kernel32.dll", BestFitMapping = false, CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
		private static extern IntPtr GetProcAddress(IntPtr hModule, string methodName);

		// Token: 0x060055A6 RID: 21926 RVA: 0x001397C0 File Offset: 0x001379C0
		[SecurityCritical]
		private static bool DoesWin32MethodExist(string moduleName, string methodName)
		{
			IntPtr moduleHandle = UnsafeNativeMethods.GetModuleHandle(moduleName);
			if (moduleHandle == IntPtr.Zero)
			{
				return false;
			}
			IntPtr procAddress = UnsafeNativeMethods.GetProcAddress(moduleHandle, methodName);
			return procAddress != IntPtr.Zero;
		}

		// Token: 0x060055A7 RID: 21927 RVA: 0x001397F8 File Offset: 0x001379F8
		[SecuritySafeCritical]
		private static bool _IsTailoredApplication()
		{
			Version v = new Version(6, 2, 0, 0);
			OperatingSystem osversion = Environment.OSVersion;
			bool flag = osversion.Platform == PlatformID.Win32NT && osversion.Version >= v;
			if (flag && UnsafeNativeMethods.DoesWin32MethodExist("kernel32.dll", "AppPolicyGetClrCompat"))
			{
				UnsafeNativeMethods.AppPolicyClrCompat appPolicyClrCompat;
				return UnsafeNativeMethods._AppPolicyGetClrCompat(UnsafeNativeMethods.GetCurrentProcessToken(), out appPolicyClrCompat) == 0 && appPolicyClrCompat == UnsafeNativeMethods.AppPolicyClrCompat.AppPolicyClrCompat_Universal;
			}
			if (flag && UnsafeNativeMethods.DoesWin32MethodExist("kernel32.dll", "GetCurrentPackageId"))
			{
				int num = 0;
				return UnsafeNativeMethods._GetCurrentPackageId(ref num, null) == 122;
			}
			return false;
		}

		// Token: 0x04003379 RID: 13177
		public const string KERNEL32 = "kernel32.dll";

		// Token: 0x0400337A RID: 13178
		public const string ADVAPI32 = "advapi32.dll";

		// Token: 0x0400337B RID: 13179
		public const string BCRYPT = "bcrypt.dll";

		// Token: 0x0400337C RID: 13180
		public const string MQRT = "mqrt.dll";

		// Token: 0x0400337D RID: 13181
		public const string SECUR32 = "secur32.dll";

		// Token: 0x0400337E RID: 13182
		public const string USERENV = "userenv.dll";

		// Token: 0x0400337F RID: 13183
		public const string WS2_32 = "ws2_32.dll";

		// Token: 0x04003380 RID: 13184
		public const int ERROR_SUCCESS = 0;

		// Token: 0x04003381 RID: 13185
		public const int ERROR_FILE_NOT_FOUND = 2;

		// Token: 0x04003382 RID: 13186
		public const int ERROR_ACCESS_DENIED = 5;

		// Token: 0x04003383 RID: 13187
		public const int ERROR_INVALID_HANDLE = 6;

		// Token: 0x04003384 RID: 13188
		public const int ERROR_NOT_ENOUGH_MEMORY = 8;

		// Token: 0x04003385 RID: 13189
		public const int ERROR_OUTOFMEMORY = 14;

		// Token: 0x04003386 RID: 13190
		public const int ERROR_SHARING_VIOLATION = 32;

		// Token: 0x04003387 RID: 13191
		public const int ERROR_NETNAME_DELETED = 64;

		// Token: 0x04003388 RID: 13192
		public const int ERROR_INVALID_PARAMETER = 87;

		// Token: 0x04003389 RID: 13193
		public const int ERROR_BROKEN_PIPE = 109;

		// Token: 0x0400338A RID: 13194
		public const int ERROR_ALREADY_EXISTS = 183;

		// Token: 0x0400338B RID: 13195
		public const int ERROR_PIPE_BUSY = 231;

		// Token: 0x0400338C RID: 13196
		public const int ERROR_NO_DATA = 232;

		// Token: 0x0400338D RID: 13197
		public const int ERROR_MORE_DATA = 234;

		// Token: 0x0400338E RID: 13198
		public const int WAIT_TIMEOUT = 258;

		// Token: 0x0400338F RID: 13199
		public const int ERROR_PIPE_CONNECTED = 535;

		// Token: 0x04003390 RID: 13200
		public const int ERROR_OPERATION_ABORTED = 995;

		// Token: 0x04003391 RID: 13201
		public const int ERROR_IO_PENDING = 997;

		// Token: 0x04003392 RID: 13202
		public const int ERROR_SERVICE_ALREADY_RUNNING = 1056;

		// Token: 0x04003393 RID: 13203
		public const int ERROR_SERVICE_DISABLED = 1058;

		// Token: 0x04003394 RID: 13204
		public const int ERROR_NO_TRACKING_SERVICE = 1172;

		// Token: 0x04003395 RID: 13205
		public const int ERROR_ALLOTTED_SPACE_EXCEEDED = 1344;

		// Token: 0x04003396 RID: 13206
		public const int ERROR_NO_SYSTEM_RESOURCES = 1450;

		// Token: 0x04003397 RID: 13207
		private const int ERROR_INSUFFICIENT_BUFFER = 122;

		// Token: 0x04003398 RID: 13208
		public const int STATUS_PENDING = 259;

		// Token: 0x04003399 RID: 13209
		public const int WSAACCESS = 10013;

		// Token: 0x0400339A RID: 13210
		public const int WSAEMFILE = 10024;

		// Token: 0x0400339B RID: 13211
		public const int WSAEMSGSIZE = 10040;

		// Token: 0x0400339C RID: 13212
		public const int WSAEADDRINUSE = 10048;

		// Token: 0x0400339D RID: 13213
		public const int WSAEADDRNOTAVAIL = 10049;

		// Token: 0x0400339E RID: 13214
		public const int WSAENETDOWN = 10050;

		// Token: 0x0400339F RID: 13215
		public const int WSAENETUNREACH = 10051;

		// Token: 0x040033A0 RID: 13216
		public const int WSAENETRESET = 10052;

		// Token: 0x040033A1 RID: 13217
		public const int WSAECONNABORTED = 10053;

		// Token: 0x040033A2 RID: 13218
		public const int WSAECONNRESET = 10054;

		// Token: 0x040033A3 RID: 13219
		public const int WSAENOBUFS = 10055;

		// Token: 0x040033A4 RID: 13220
		public const int WSAESHUTDOWN = 10058;

		// Token: 0x040033A5 RID: 13221
		public const int WSAETIMEDOUT = 10060;

		// Token: 0x040033A6 RID: 13222
		public const int WSAECONNREFUSED = 10061;

		// Token: 0x040033A7 RID: 13223
		public const int WSAEHOSTDOWN = 10064;

		// Token: 0x040033A8 RID: 13224
		public const int WSAEHOSTUNREACH = 10065;

		// Token: 0x040033A9 RID: 13225
		public const int DUPLICATE_CLOSE_SOURCE = 1;

		// Token: 0x040033AA RID: 13226
		public const int DUPLICATE_SAME_ACCESS = 2;

		// Token: 0x040033AB RID: 13227
		public const int FILE_FLAG_OVERLAPPED = 1073741824;

		// Token: 0x040033AC RID: 13228
		public const int FILE_FLAG_FIRST_PIPE_INSTANCE = 524288;

		// Token: 0x040033AD RID: 13229
		public const int GENERIC_ALL = 268435456;

		// Token: 0x040033AE RID: 13230
		public const int GENERIC_READ = -2147483648;

		// Token: 0x040033AF RID: 13231
		public const int GENERIC_WRITE = 1073741824;

		// Token: 0x040033B0 RID: 13232
		public const int FILE_CREATE_PIPE_INSTANCE = 4;

		// Token: 0x040033B1 RID: 13233
		public const int FILE_WRITE_ATTRIBUTES = 256;

		// Token: 0x040033B2 RID: 13234
		public const int FILE_WRITE_DATA = 2;

		// Token: 0x040033B3 RID: 13235
		public const int FILE_WRITE_EA = 16;

		// Token: 0x040033B4 RID: 13236
		public const int OPEN_EXISTING = 3;

		// Token: 0x040033B5 RID: 13237
		public const int PIPE_ACCESS_DUPLEX = 3;

		// Token: 0x040033B6 RID: 13238
		public const int PIPE_UNLIMITED_INSTANCES = 255;

		// Token: 0x040033B7 RID: 13239
		public const int PIPE_TYPE_BYTE = 0;

		// Token: 0x040033B8 RID: 13240
		public const int PIPE_TYPE_MESSAGE = 4;

		// Token: 0x040033B9 RID: 13241
		public const int PIPE_READMODE_BYTE = 0;

		// Token: 0x040033BA RID: 13242
		public const int PIPE_READMODE_MESSAGE = 2;

		// Token: 0x040033BB RID: 13243
		public const uint MEM_COMMIT = 4096U;

		// Token: 0x040033BC RID: 13244
		public const uint MEM_DECOMMIT = 16384U;

		// Token: 0x040033BD RID: 13245
		public const int PAGE_READWRITE = 4;

		// Token: 0x040033BE RID: 13246
		public const int FILE_MAP_WRITE = 2;

		// Token: 0x040033BF RID: 13247
		public const int FILE_MAP_READ = 4;

		// Token: 0x040033C0 RID: 13248
		public const int SDDL_REVISION_1 = 1;

		// Token: 0x040033C1 RID: 13249
		public const int SECURITY_ANONYMOUS = 0;

		// Token: 0x040033C2 RID: 13250
		public const int SECURITY_QOS_PRESENT = 1048576;

		// Token: 0x040033C3 RID: 13251
		public const int SECURITY_IDENTIFICATION = 65536;

		// Token: 0x040033C4 RID: 13252
		public const int FORMAT_MESSAGE_ALLOCATE_BUFFER = 256;

		// Token: 0x040033C5 RID: 13253
		public const int FORMAT_MESSAGE_IGNORE_INSERTS = 512;

		// Token: 0x040033C6 RID: 13254
		public const int FORMAT_MESSAGE_FROM_STRING = 1024;

		// Token: 0x040033C7 RID: 13255
		public const int FORMAT_MESSAGE_FROM_SYSTEM = 4096;

		// Token: 0x040033C8 RID: 13256
		public const int FORMAT_MESSAGE_ARGUMENT_ARRAY = 8192;

		// Token: 0x040033C9 RID: 13257
		public const int FORMAT_MESSAGE_FROM_HMODULE = 2048;

		// Token: 0x040033CA RID: 13258
		public const int MQ_RECEIVE_ACCESS = 1;

		// Token: 0x040033CB RID: 13259
		public const int MQ_SEND_ACCESS = 2;

		// Token: 0x040033CC RID: 13260
		public const int MQ_MOVE_ACCESS = 4;

		// Token: 0x040033CD RID: 13261
		public const int MQ_DENY_NONE = 0;

		// Token: 0x040033CE RID: 13262
		public const int MQ_DENY_RECEIVE_SHARE = 1;

		// Token: 0x040033CF RID: 13263
		public const int MQ_ACTION_RECEIVE = 0;

		// Token: 0x040033D0 RID: 13264
		public const int MQ_ACTION_PEEK_CURRENT = -2147483648;

		// Token: 0x040033D1 RID: 13265
		public const int MQ_ACTION_PEEK_NEXT = -2147483647;

		// Token: 0x040033D2 RID: 13266
		public const int MQ_LOOKUP_RECEIVE_CURRENT = 1073741856;

		// Token: 0x040033D3 RID: 13267
		public const int MQ_LOOKUP_PEEK_CURRENT = 1073741840;

		// Token: 0x040033D4 RID: 13268
		public const int MQ_NO_TRANSACTION = 0;

		// Token: 0x040033D5 RID: 13269
		public const int MQ_MTS_TRANSACTION = 1;

		// Token: 0x040033D6 RID: 13270
		public const int MQ_SINGLE_MESSAGE = 3;

		// Token: 0x040033D7 RID: 13271
		public const int MQ_INFORMATION_PROPERTY = 1074659329;

		// Token: 0x040033D8 RID: 13272
		public const int MQ_INFORMATION_ILLEGAL_PROPERTY = 1074659330;

		// Token: 0x040033D9 RID: 13273
		public const int MQ_INFORMATION_PROPERTY_IGNORED = 1074659331;

		// Token: 0x040033DA RID: 13274
		public const int MQ_INFORMATION_UNSUPPORTED_PROPERTY = 1074659332;

		// Token: 0x040033DB RID: 13275
		public const int MQ_INFORMATION_DUPLICATE_PROPERTY = 1074659333;

		// Token: 0x040033DC RID: 13276
		public const int MQ_INFORMATION_OPERATION_PENDING = 1074659334;

		// Token: 0x040033DD RID: 13277
		public const int MQ_INFORMATION_FORMATNAME_BUFFER_TOO_SMALL = 1074659337;

		// Token: 0x040033DE RID: 13278
		public const int MQ_INFORMATION_INTERNAL_USER_CERT_EXIST = 1074659338;

		// Token: 0x040033DF RID: 13279
		public const int MQ_INFORMATION_OWNER_IGNORED = 1074659339;

		// Token: 0x040033E0 RID: 13280
		public const int MQ_ERROR = -1072824319;

		// Token: 0x040033E1 RID: 13281
		public const int MQ_ERROR_PROPERTY = -1072824318;

		// Token: 0x040033E2 RID: 13282
		public const int MQ_ERROR_QUEUE_NOT_FOUND = -1072824317;

		// Token: 0x040033E3 RID: 13283
		public const int MQ_ERROR_QUEUE_NOT_ACTIVE = -1072824316;

		// Token: 0x040033E4 RID: 13284
		public const int MQ_ERROR_QUEUE_EXISTS = -1072824315;

		// Token: 0x040033E5 RID: 13285
		public const int MQ_ERROR_INVALID_PARAMETER = -1072824314;

		// Token: 0x040033E6 RID: 13286
		public const int MQ_ERROR_INVALID_HANDLE = -1072824313;

		// Token: 0x040033E7 RID: 13287
		public const int MQ_ERROR_OPERATION_CANCELLED = -1072824312;

		// Token: 0x040033E8 RID: 13288
		public const int MQ_ERROR_SHARING_VIOLATION = -1072824311;

		// Token: 0x040033E9 RID: 13289
		public const int MQ_ERROR_SERVICE_NOT_AVAILABLE = -1072824309;

		// Token: 0x040033EA RID: 13290
		public const int MQ_ERROR_MACHINE_NOT_FOUND = -1072824307;

		// Token: 0x040033EB RID: 13291
		public const int MQ_ERROR_ILLEGAL_SORT = -1072824304;

		// Token: 0x040033EC RID: 13292
		public const int MQ_ERROR_ILLEGAL_USER = -1072824303;

		// Token: 0x040033ED RID: 13293
		public const int MQ_ERROR_NO_DS = -1072824301;

		// Token: 0x040033EE RID: 13294
		public const int MQ_ERROR_ILLEGAL_QUEUE_PATHNAME = -1072824300;

		// Token: 0x040033EF RID: 13295
		public const int MQ_ERROR_ILLEGAL_PROPERTY_VALUE = -1072824296;

		// Token: 0x040033F0 RID: 13296
		public const int MQ_ERROR_ILLEGAL_PROPERTY_VT = -1072824295;

		// Token: 0x040033F1 RID: 13297
		public const int MQ_ERROR_BUFFER_OVERFLOW = -1072824294;

		// Token: 0x040033F2 RID: 13298
		public const int MQ_ERROR_IO_TIMEOUT = -1072824293;

		// Token: 0x040033F3 RID: 13299
		public const int MQ_ERROR_ILLEGAL_CURSOR_ACTION = -1072824292;

		// Token: 0x040033F4 RID: 13300
		public const int MQ_ERROR_MESSAGE_ALREADY_RECEIVED = -1072824291;

		// Token: 0x040033F5 RID: 13301
		public const int MQ_ERROR_ILLEGAL_FORMATNAME = -1072824290;

		// Token: 0x040033F6 RID: 13302
		public const int MQ_ERROR_FORMATNAME_BUFFER_TOO_SMALL = -1072824289;

		// Token: 0x040033F7 RID: 13303
		public const int MQ_ERROR_UNSUPPORTED_FORMATNAME_OPERATION = -1072824288;

		// Token: 0x040033F8 RID: 13304
		public const int MQ_ERROR_ILLEGAL_SECURITY_DESCRIPTOR = -1072824287;

		// Token: 0x040033F9 RID: 13305
		public const int MQ_ERROR_SENDERID_BUFFER_TOO_SMALL = -1072824286;

		// Token: 0x040033FA RID: 13306
		public const int MQ_ERROR_SECURITY_DESCRIPTOR_TOO_SMALL = -1072824285;

		// Token: 0x040033FB RID: 13307
		public const int MQ_ERROR_CANNOT_IMPERSONATE_CLIENT = -1072824284;

		// Token: 0x040033FC RID: 13308
		public const int MQ_ERROR_ACCESS_DENIED = -1072824283;

		// Token: 0x040033FD RID: 13309
		public const int MQ_ERROR_PRIVILEGE_NOT_HELD = -1072824282;

		// Token: 0x040033FE RID: 13310
		public const int MQ_ERROR_INSUFFICIENT_RESOURCES = -1072824281;

		// Token: 0x040033FF RID: 13311
		public const int MQ_ERROR_USER_BUFFER_TOO_SMALL = -1072824280;

		// Token: 0x04003400 RID: 13312
		public const int MQ_ERROR_MESSAGE_STORAGE_FAILED = -1072824278;

		// Token: 0x04003401 RID: 13313
		public const int MQ_ERROR_SENDER_CERT_BUFFER_TOO_SMALL = -1072824277;

		// Token: 0x04003402 RID: 13314
		public const int MQ_ERROR_INVALID_CERTIFICATE = -1072824276;

		// Token: 0x04003403 RID: 13315
		public const int MQ_ERROR_CORRUPTED_INTERNAL_CERTIFICATE = -1072824275;

		// Token: 0x04003404 RID: 13316
		public const int MQ_ERROR_INTERNAL_USER_CERT_EXIST = -1072824274;

		// Token: 0x04003405 RID: 13317
		public const int MQ_ERROR_NO_INTERNAL_USER_CERT = -1072824273;

		// Token: 0x04003406 RID: 13318
		public const int MQ_ERROR_CORRUPTED_SECURITY_DATA = -1072824272;

		// Token: 0x04003407 RID: 13319
		public const int MQ_ERROR_CORRUPTED_PERSONAL_CERT_STORE = -1072824271;

		// Token: 0x04003408 RID: 13320
		public const int MQ_ERROR_COMPUTER_DOES_NOT_SUPPORT_ENCRYPTION = -1072824269;

		// Token: 0x04003409 RID: 13321
		public const int MQ_ERROR_BAD_SECURITY_CONTEXT = -1072824267;

		// Token: 0x0400340A RID: 13322
		public const int MQ_ERROR_COULD_NOT_GET_USER_SID = -1072824266;

		// Token: 0x0400340B RID: 13323
		public const int MQ_ERROR_COULD_NOT_GET_ACCOUNT_INFO = -1072824265;

		// Token: 0x0400340C RID: 13324
		public const int MQ_ERROR_ILLEGAL_MQCOLUMNS = -1072824264;

		// Token: 0x0400340D RID: 13325
		public const int MQ_ERROR_ILLEGAL_PROPID = -1072824263;

		// Token: 0x0400340E RID: 13326
		public const int MQ_ERROR_ILLEGAL_RELATION = -1072824262;

		// Token: 0x0400340F RID: 13327
		public const int MQ_ERROR_ILLEGAL_PROPERTY_SIZE = -1072824261;

		// Token: 0x04003410 RID: 13328
		public const int MQ_ERROR_ILLEGAL_RESTRICTION_PROPID = -1072824260;

		// Token: 0x04003411 RID: 13329
		public const int MQ_ERROR_ILLEGAL_MQQUEUEPROPS = -1072824259;

		// Token: 0x04003412 RID: 13330
		public const int MQ_ERROR_PROPERTY_NOTALLOWED = -1072824258;

		// Token: 0x04003413 RID: 13331
		public const int MQ_ERROR_INSUFFICIENT_PROPERTIES = -1072824257;

		// Token: 0x04003414 RID: 13332
		public const int MQ_ERROR_MACHINE_EXISTS = -1072824256;

		// Token: 0x04003415 RID: 13333
		public const int MQ_ERROR_ILLEGAL_MQQMPROPS = -1072824255;

		// Token: 0x04003416 RID: 13334
		public const int MQ_ERROR_DS_IS_FULL = -1072824254;

		// Token: 0x04003417 RID: 13335
		public const int MQ_ERROR_DS_ERROR = -1072824253;

		// Token: 0x04003418 RID: 13336
		public const int MQ_ERROR_INVALID_OWNER = -1072824252;

		// Token: 0x04003419 RID: 13337
		public const int MQ_ERROR_UNSUPPORTED_ACCESS_MODE = -1072824251;

		// Token: 0x0400341A RID: 13338
		public const int MQ_ERROR_RESULT_BUFFER_TOO_SMALL = -1072824250;

		// Token: 0x0400341B RID: 13339
		public const int MQ_ERROR_DELETE_CN_IN_USE = -1072824248;

		// Token: 0x0400341C RID: 13340
		public const int MQ_ERROR_NO_RESPONSE_FROM_OBJECT_SERVER = -1072824247;

		// Token: 0x0400341D RID: 13341
		public const int MQ_ERROR_OBJECT_SERVER_NOT_AVAILABLE = -1072824246;

		// Token: 0x0400341E RID: 13342
		public const int MQ_ERROR_QUEUE_NOT_AVAILABLE = -1072824245;

		// Token: 0x0400341F RID: 13343
		public const int MQ_ERROR_DTC_CONNECT = -1072824244;

		// Token: 0x04003420 RID: 13344
		public const int MQ_ERROR_TRANSACTION_IMPORT = -1072824242;

		// Token: 0x04003421 RID: 13345
		public const int MQ_ERROR_TRANSACTION_USAGE = -1072824240;

		// Token: 0x04003422 RID: 13346
		public const int MQ_ERROR_TRANSACTION_SEQUENCE = -1072824239;

		// Token: 0x04003423 RID: 13347
		public const int MQ_ERROR_MISSING_CONNECTOR_TYPE = -1072824235;

		// Token: 0x04003424 RID: 13348
		public const int MQ_ERROR_STALE_HANDLE = -1072824234;

		// Token: 0x04003425 RID: 13349
		public const int MQ_ERROR_TRANSACTION_ENLIST = -1072824232;

		// Token: 0x04003426 RID: 13350
		public const int MQ_ERROR_QUEUE_DELETED = -1072824230;

		// Token: 0x04003427 RID: 13351
		public const int MQ_ERROR_ILLEGAL_CONTEXT = -1072824229;

		// Token: 0x04003428 RID: 13352
		public const int MQ_ERROR_ILLEGAL_SORT_PROPID = -1072824228;

		// Token: 0x04003429 RID: 13353
		public const int MQ_ERROR_LABEL_TOO_LONG = -1072824227;

		// Token: 0x0400342A RID: 13354
		public const int MQ_ERROR_LABEL_BUFFER_TOO_SMALL = -1072824226;

		// Token: 0x0400342B RID: 13355
		public const int MQ_ERROR_MQIS_SERVER_EMPTY = -1072824225;

		// Token: 0x0400342C RID: 13356
		public const int MQ_ERROR_MQIS_READONLY_MODE = -1072824224;

		// Token: 0x0400342D RID: 13357
		public const int MQ_ERROR_SYMM_KEY_BUFFER_TOO_SMALL = -1072824223;

		// Token: 0x0400342E RID: 13358
		public const int MQ_ERROR_SIGNATURE_BUFFER_TOO_SMALL = -1072824222;

		// Token: 0x0400342F RID: 13359
		public const int MQ_ERROR_PROV_NAME_BUFFER_TOO_SMALL = -1072824221;

		// Token: 0x04003430 RID: 13360
		public const int MQ_ERROR_ILLEGAL_OPERATION = -1072824220;

		// Token: 0x04003431 RID: 13361
		public const int MQ_ERROR_WRITE_NOT_ALLOWED = -1072824219;

		// Token: 0x04003432 RID: 13362
		public const int MQ_ERROR_WKS_CANT_SERVE_CLIENT = -1072824218;

		// Token: 0x04003433 RID: 13363
		public const int MQ_ERROR_DEPEND_WKS_LICENSE_OVERFLOW = -1072824217;

		// Token: 0x04003434 RID: 13364
		public const int MQ_ERROR_REMOTE_MACHINE_NOT_AVAILABLE = -1072824215;

		// Token: 0x04003435 RID: 13365
		public const int MQ_ERROR_UNSUPPORTED_OPERATION = -1072824214;

		// Token: 0x04003436 RID: 13366
		public const int MQ_ERROR_ENCRYPTION_PROVIDER_NOT_SUPPORTED = -1072824213;

		// Token: 0x04003437 RID: 13367
		public const int MQ_ERROR_CANNOT_SET_CRYPTO_SEC_DESCR = -1072824212;

		// Token: 0x04003438 RID: 13368
		public const int MQ_ERROR_CERTIFICATE_NOT_PROVIDED = -1072824211;

		// Token: 0x04003439 RID: 13369
		public const int MQ_ERROR_Q_DNS_PROPERTY_NOT_SUPPORTED = -1072824210;

		// Token: 0x0400343A RID: 13370
		public const int MQ_ERROR_CANNOT_CREATE_CERT_STORE = -1072824209;

		// Token: 0x0400343B RID: 13371
		public const int MQ_ERROR_CANNOT_OPEN_CERT_STORE = -1072824208;

		// Token: 0x0400343C RID: 13372
		public const int MQ_ERROR_ILLEGAL_ENTERPRISE_OPERATION = -1072824207;

		// Token: 0x0400343D RID: 13373
		public const int MQ_ERROR_CANNOT_GRANT_ADD_GUID = -1072824206;

		// Token: 0x0400343E RID: 13374
		public const int MQ_ERROR_CANNOT_LOAD_MSMQOCM = -1072824205;

		// Token: 0x0400343F RID: 13375
		public const int MQ_ERROR_NO_ENTRY_POINT_MSMQOCM = -1072824204;

		// Token: 0x04003440 RID: 13376
		public const int MQ_ERROR_NO_MSMQ_SERVERS_ON_DC = -1072824203;

		// Token: 0x04003441 RID: 13377
		public const int MQ_ERROR_CANNOT_JOIN_DOMAIN = -1072824202;

		// Token: 0x04003442 RID: 13378
		public const int MQ_ERROR_CANNOT_CREATE_ON_GC = -1072824201;

		// Token: 0x04003443 RID: 13379
		public const int MQ_ERROR_GUID_NOT_MATCHING = -1072824200;

		// Token: 0x04003444 RID: 13380
		public const int MQ_ERROR_PUBLIC_KEY_NOT_FOUND = -1072824199;

		// Token: 0x04003445 RID: 13381
		public const int MQ_ERROR_PUBLIC_KEY_DOES_NOT_EXIST = -1072824198;

		// Token: 0x04003446 RID: 13382
		public const int MQ_ERROR_ILLEGAL_MQPRIVATEPROPS = -1072824197;

		// Token: 0x04003447 RID: 13383
		public const int MQ_ERROR_NO_GC_IN_DOMAIN = -1072824196;

		// Token: 0x04003448 RID: 13384
		public const int MQ_ERROR_NO_MSMQ_SERVERS_ON_GC = -1072824195;

		// Token: 0x04003449 RID: 13385
		public const int MQ_ERROR_CANNOT_GET_DN = -1072824194;

		// Token: 0x0400344A RID: 13386
		public const int MQ_ERROR_CANNOT_HASH_DATA_EX = -1072824193;

		// Token: 0x0400344B RID: 13387
		public const int MQ_ERROR_CANNOT_SIGN_DATA_EX = -1072824192;

		// Token: 0x0400344C RID: 13388
		public const int MQ_ERROR_CANNOT_CREATE_HASH_EX = -1072824191;

		// Token: 0x0400344D RID: 13389
		public const int MQ_ERROR_FAIL_VERIFY_SIGNATURE_EX = -1072824190;

		// Token: 0x0400344E RID: 13390
		public const int MQ_ERROR_CANNOT_DELETE_PSC_OBJECTS = -1072824189;

		// Token: 0x0400344F RID: 13391
		public const int MQ_ERROR_NO_MQUSER_OU = -1072824188;

		// Token: 0x04003450 RID: 13392
		public const int MQ_ERROR_CANNOT_LOAD_MQAD = -1072824187;

		// Token: 0x04003451 RID: 13393
		public const int MQ_ERROR_CANNOT_LOAD_MQDSSRV = -1072824186;

		// Token: 0x04003452 RID: 13394
		public const int MQ_ERROR_PROPERTIES_CONFLICT = -1072824185;

		// Token: 0x04003453 RID: 13395
		public const int MQ_ERROR_MESSAGE_NOT_FOUND = -1072824184;

		// Token: 0x04003454 RID: 13396
		public const int MQ_ERROR_CANT_RESOLVE_SITES = -1072824183;

		// Token: 0x04003455 RID: 13397
		public const int MQ_ERROR_NOT_SUPPORTED_BY_DEPENDENT_CLIENTS = -1072824182;

		// Token: 0x04003456 RID: 13398
		public const int MQ_ERROR_OPERATION_NOT_SUPPORTED_BY_REMOTE_COMPUTER = -1072824181;

		// Token: 0x04003457 RID: 13399
		public const int MQ_ERROR_NOT_A_CORRECT_OBJECT_CLASS = -1072824180;

		// Token: 0x04003458 RID: 13400
		public const int MQ_ERROR_MULTI_SORT_KEYS = -1072824179;

		// Token: 0x04003459 RID: 13401
		public const int MQ_ERROR_GC_NEEDED = -1072824178;

		// Token: 0x0400345A RID: 13402
		public const int MQ_ERROR_DS_BIND_ROOT_FOREST = -1072824177;

		// Token: 0x0400345B RID: 13403
		public const int MQ_ERROR_DS_LOCAL_USER = -1072824176;

		// Token: 0x0400345C RID: 13404
		public const int MQ_ERROR_Q_ADS_PROPERTY_NOT_SUPPORTED = -1072824175;

		// Token: 0x0400345D RID: 13405
		public const int MQ_ERROR_BAD_XML_FORMAT = -1072824174;

		// Token: 0x0400345E RID: 13406
		public const int MQ_ERROR_UNSUPPORTED_CLASS = -1072824173;

		// Token: 0x0400345F RID: 13407
		public const int MQ_ERROR_UNINITIALIZED_OBJECT = -1072824172;

		// Token: 0x04003460 RID: 13408
		public const int MQ_ERROR_CANNOT_CREATE_PSC_OBJECTS = -1072824171;

		// Token: 0x04003461 RID: 13409
		public const int MQ_ERROR_CANNOT_UPDATE_PSC_OBJECTS = -1072824170;

		// Token: 0x04003462 RID: 13410
		public const int MQ_ERROR_MESSAGE_LOCKED_UNDER_TRANSACTION = -1072824164;

		// Token: 0x04003463 RID: 13411
		public const int MQMSG_DELIVERY_EXPRESS = 0;

		// Token: 0x04003464 RID: 13412
		public const int MQMSG_DELIVERY_RECOVERABLE = 1;

		// Token: 0x04003465 RID: 13413
		public const int PROPID_M_MSGID_SIZE = 20;

		// Token: 0x04003466 RID: 13414
		public const int PROPID_M_CORRELATIONID_SIZE = 20;

		// Token: 0x04003467 RID: 13415
		public const int MQ_MAX_MSG_LABEL_LEN = 250;

		// Token: 0x04003468 RID: 13416
		public const int MQMSG_JOURNAL_NONE = 0;

		// Token: 0x04003469 RID: 13417
		public const int MQMSG_DEADLETTER = 1;

		// Token: 0x0400346A RID: 13418
		public const int MQMSG_JOURNAL = 2;

		// Token: 0x0400346B RID: 13419
		public const int MQMSG_ACKNOWLEDGMENT_NONE = 0;

		// Token: 0x0400346C RID: 13420
		public const int MQMSG_ACKNOWLEDGMENT_POS_ARRIVAL = 1;

		// Token: 0x0400346D RID: 13421
		public const int MQMSG_ACKNOWLEDGMENT_POS_RECEIVE = 2;

		// Token: 0x0400346E RID: 13422
		public const int MQMSG_ACKNOWLEDGMENT_NEG_ARRIVAL = 4;

		// Token: 0x0400346F RID: 13423
		public const int MQMSG_ACKNOWLEDGMENT_NEG_RECEIVE = 8;

		// Token: 0x04003470 RID: 13424
		public const int MQMSG_CLASS_NORMAL = 0;

		// Token: 0x04003471 RID: 13425
		public const int MQMSG_CLASS_REPORT = 1;

		// Token: 0x04003472 RID: 13426
		public const int MQMSG_SENDERID_TYPE_NONE = 0;

		// Token: 0x04003473 RID: 13427
		public const int MQMSG_SENDERID_TYPE_SID = 1;

		// Token: 0x04003474 RID: 13428
		public const int MQMSG_AUTH_LEVEL_NONE = 0;

		// Token: 0x04003475 RID: 13429
		public const int MQMSG_AUTH_LEVEL_ALWAYS = 1;

		// Token: 0x04003476 RID: 13430
		public const int MQMSG_PRIV_LEVEL_NONE = 0;

		// Token: 0x04003477 RID: 13431
		public const int MQMSG_PRIV_LEVEL_BODY_BASE = 1;

		// Token: 0x04003478 RID: 13432
		public const int MQMSG_PRIV_LEVEL_BODY_ENHANCED = 3;

		// Token: 0x04003479 RID: 13433
		public const int MQMSG_TRACE_NONE = 0;

		// Token: 0x0400347A RID: 13434
		public const int MQMSG_SEND_ROUTE_TO_REPORT_QUEUE = 1;

		// Token: 0x0400347B RID: 13435
		public const int PROPID_M_BASE = 0;

		// Token: 0x0400347C RID: 13436
		public const int PROPID_M_CLASS = 1;

		// Token: 0x0400347D RID: 13437
		public const int PROPID_M_MSGID = 2;

		// Token: 0x0400347E RID: 13438
		public const int PROPID_M_CORRELATIONID = 3;

		// Token: 0x0400347F RID: 13439
		public const int PROPID_M_PRIORITY = 4;

		// Token: 0x04003480 RID: 13440
		public const int PROPID_M_DELIVERY = 5;

		// Token: 0x04003481 RID: 13441
		public const int PROPID_M_ACKNOWLEDGE = 6;

		// Token: 0x04003482 RID: 13442
		public const int PROPID_M_JOURNAL = 7;

		// Token: 0x04003483 RID: 13443
		public const int PROPID_M_APPSPECIFIC = 8;

		// Token: 0x04003484 RID: 13444
		public const int PROPID_M_BODY = 9;

		// Token: 0x04003485 RID: 13445
		public const int PROPID_M_BODY_SIZE = 10;

		// Token: 0x04003486 RID: 13446
		public const int PROPID_M_LABEL = 11;

		// Token: 0x04003487 RID: 13447
		public const int PROPID_M_LABEL_LEN = 12;

		// Token: 0x04003488 RID: 13448
		public const int PROPID_M_TIME_TO_REACH_QUEUE = 13;

		// Token: 0x04003489 RID: 13449
		public const int PROPID_M_TIME_TO_BE_RECEIVED = 14;

		// Token: 0x0400348A RID: 13450
		public const int PROPID_M_RESP_QUEUE = 15;

		// Token: 0x0400348B RID: 13451
		public const int PROPID_M_RESP_QUEUE_LEN = 16;

		// Token: 0x0400348C RID: 13452
		public const int PROPID_M_ADMIN_QUEUE = 17;

		// Token: 0x0400348D RID: 13453
		public const int PROPID_M_ADMIN_QUEUE_LEN = 18;

		// Token: 0x0400348E RID: 13454
		public const int PROPID_M_VERSION = 19;

		// Token: 0x0400348F RID: 13455
		public const int PROPID_M_SENDERID = 20;

		// Token: 0x04003490 RID: 13456
		public const int PROPID_M_SENDERID_LEN = 21;

		// Token: 0x04003491 RID: 13457
		public const int PROPID_M_SENDERID_TYPE = 22;

		// Token: 0x04003492 RID: 13458
		public const int PROPID_M_PRIV_LEVEL = 23;

		// Token: 0x04003493 RID: 13459
		public const int PROPID_M_AUTH_LEVEL = 24;

		// Token: 0x04003494 RID: 13460
		public const int PROPID_M_AUTHENTICATED = 25;

		// Token: 0x04003495 RID: 13461
		public const int PROPID_M_HASH_ALG = 26;

		// Token: 0x04003496 RID: 13462
		public const int PROPID_M_ENCRYPTION_ALG = 27;

		// Token: 0x04003497 RID: 13463
		public const int PROPID_M_SENDER_CERT = 28;

		// Token: 0x04003498 RID: 13464
		public const int PROPID_M_SENDER_CERT_LEN = 29;

		// Token: 0x04003499 RID: 13465
		public const int PROPID_M_SRC_MACHINE_ID = 30;

		// Token: 0x0400349A RID: 13466
		public const int PROPID_M_SENTTIME = 31;

		// Token: 0x0400349B RID: 13467
		public const int PROPID_M_ARRIVEDTIME = 32;

		// Token: 0x0400349C RID: 13468
		public const int PROPID_M_DEST_QUEUE = 33;

		// Token: 0x0400349D RID: 13469
		public const int PROPID_M_DEST_QUEUE_LEN = 34;

		// Token: 0x0400349E RID: 13470
		public const int PROPID_M_EXTENSION = 35;

		// Token: 0x0400349F RID: 13471
		public const int PROPID_M_EXTENSION_LEN = 36;

		// Token: 0x040034A0 RID: 13472
		public const int PROPID_M_SECURITY_CONTEXT = 37;

		// Token: 0x040034A1 RID: 13473
		public const int PROPID_M_CONNECTOR_TYPE = 38;

		// Token: 0x040034A2 RID: 13474
		public const int PROPID_M_XACT_STATUS_QUEUE = 39;

		// Token: 0x040034A3 RID: 13475
		public const int PROPID_M_XACT_STATUS_QUEUE_LEN = 40;

		// Token: 0x040034A4 RID: 13476
		public const int PROPID_M_TRACE = 41;

		// Token: 0x040034A5 RID: 13477
		public const int PROPID_M_BODY_TYPE = 42;

		// Token: 0x040034A6 RID: 13478
		public const int PROPID_M_DEST_SYMM_KEY = 43;

		// Token: 0x040034A7 RID: 13479
		public const int PROPID_M_DEST_SYMM_KEY_LEN = 44;

		// Token: 0x040034A8 RID: 13480
		public const int PROPID_M_SIGNATURE = 45;

		// Token: 0x040034A9 RID: 13481
		public const int PROPID_M_SIGNATURE_LEN = 46;

		// Token: 0x040034AA RID: 13482
		public const int PROPID_M_PROV_TYPE = 47;

		// Token: 0x040034AB RID: 13483
		public const int PROPID_M_PROV_NAME = 48;

		// Token: 0x040034AC RID: 13484
		public const int PROPID_M_PROV_NAME_LEN = 49;

		// Token: 0x040034AD RID: 13485
		public const int PROPID_M_FIRST_IN_XACT = 50;

		// Token: 0x040034AE RID: 13486
		public const int PROPID_M_LAST_IN_XACT = 51;

		// Token: 0x040034AF RID: 13487
		public const int PROPID_M_XACTID = 52;

		// Token: 0x040034B0 RID: 13488
		public const int PROPID_M_AUTHENTICATED_EX = 53;

		// Token: 0x040034B1 RID: 13489
		public const int PROPID_M_RESP_FORMAT_NAME = 54;

		// Token: 0x040034B2 RID: 13490
		public const int PROPID_M_RESP_FORMAT_NAME_LEN = 55;

		// Token: 0x040034B3 RID: 13491
		public const int PROPID_M_DEST_FORMAT_NAME = 58;

		// Token: 0x040034B4 RID: 13492
		public const int PROPID_M_DEST_FORMAT_NAME_LEN = 59;

		// Token: 0x040034B5 RID: 13493
		public const int PROPID_M_LOOKUPID = 60;

		// Token: 0x040034B6 RID: 13494
		public const int PROPID_M_SOAP_ENVELOPE = 61;

		// Token: 0x040034B7 RID: 13495
		public const int PROPID_M_SOAP_ENVELOPE_LEN = 62;

		// Token: 0x040034B8 RID: 13496
		public const int PROPID_M_COMPOUND_MESSAGE = 63;

		// Token: 0x040034B9 RID: 13497
		public const int PROPID_M_COMPOUND_MESSAGE_SIZE = 64;

		// Token: 0x040034BA RID: 13498
		public const int PROPID_M_SOAP_HEADER = 65;

		// Token: 0x040034BB RID: 13499
		public const int PROPID_M_SOAP_BODY = 66;

		// Token: 0x040034BC RID: 13500
		public const int PROPID_M_DEADLETTER_QUEUE = 67;

		// Token: 0x040034BD RID: 13501
		public const int PROPID_M_DEADLETTER_QUEUE_LEN = 68;

		// Token: 0x040034BE RID: 13502
		public const int PROPID_M_ABORT_COUNT = 69;

		// Token: 0x040034BF RID: 13503
		public const int PROPID_M_MOVE_COUNT = 70;

		// Token: 0x040034C0 RID: 13504
		public const int PROPID_M_GROUP_ID = 71;

		// Token: 0x040034C1 RID: 13505
		public const int PROPID_M_GROUP_ID_LEN = 72;

		// Token: 0x040034C2 RID: 13506
		public const int PROPID_M_FIRST_IN_GROUP = 73;

		// Token: 0x040034C3 RID: 13507
		public const int PROPID_M_LAST_IN_GROUP = 74;

		// Token: 0x040034C4 RID: 13508
		public const int PROPID_M_LAST_MOVE_TIME = 75;

		// Token: 0x040034C5 RID: 13509
		public const int PROPID_Q_BASE = 100;

		// Token: 0x040034C6 RID: 13510
		public const int PROPID_Q_INSTANCE = 101;

		// Token: 0x040034C7 RID: 13511
		public const int PROPID_Q_TYPE = 102;

		// Token: 0x040034C8 RID: 13512
		public const int PROPID_Q_PATHNAME = 103;

		// Token: 0x040034C9 RID: 13513
		public const int PROPID_Q_JOURNAL = 104;

		// Token: 0x040034CA RID: 13514
		public const int PROPID_Q_QUOTA = 105;

		// Token: 0x040034CB RID: 13515
		public const int PROPID_Q_BASEPRIORITY = 106;

		// Token: 0x040034CC RID: 13516
		public const int PROPID_Q_JOURNAL_QUOTA = 107;

		// Token: 0x040034CD RID: 13517
		public const int PROPID_Q_LABEL = 108;

		// Token: 0x040034CE RID: 13518
		public const int PROPID_Q_CREATE_TIME = 109;

		// Token: 0x040034CF RID: 13519
		public const int PROPID_Q_MODIFY_TIME = 110;

		// Token: 0x040034D0 RID: 13520
		public const int PROPID_Q_AUTHENTICATE = 111;

		// Token: 0x040034D1 RID: 13521
		public const int PROPID_Q_PRIV_LEVEL = 112;

		// Token: 0x040034D2 RID: 13522
		public const int PROPID_Q_TRANSACTION = 113;

		// Token: 0x040034D3 RID: 13523
		public const int PROPID_Q_PATHNAME_DNS = 124;

		// Token: 0x040034D4 RID: 13524
		public const int PROPID_Q_MULTICAST_ADDRESS = 125;

		// Token: 0x040034D5 RID: 13525
		public const int PROPID_Q_ADS_PATH = 126;

		// Token: 0x040034D6 RID: 13526
		public const int PROPID_PC_BASE = 5800;

		// Token: 0x040034D7 RID: 13527
		public const int PROPID_PC_VERSION = 5801;

		// Token: 0x040034D8 RID: 13528
		public const int PROPID_PC_DS_ENABLED = 5802;

		// Token: 0x040034D9 RID: 13529
		public const int PROPID_MGMT_QUEUE_BASE = 0;

		// Token: 0x040034DA RID: 13530
		public const int PROPID_MGMT_QUEUE_SUBQUEUE_NAMES = 27;

		// Token: 0x040034DB RID: 13531
		public const int MQ_TRANSACTIONAL_NONE = 0;

		// Token: 0x040034DC RID: 13532
		public const int MQ_TRANSACTIONAL = 1;

		// Token: 0x040034DD RID: 13533
		public const int ALG_CLASS_HASH = 32768;

		// Token: 0x040034DE RID: 13534
		public const int ALG_CLASS_DATA_ENCRYPT = 24576;

		// Token: 0x040034DF RID: 13535
		public const int ALG_TYPE_ANY = 0;

		// Token: 0x040034E0 RID: 13536
		public const int ALG_TYPE_STREAM = 2048;

		// Token: 0x040034E1 RID: 13537
		public const int ALG_TYPE_BLOCK = 1536;

		// Token: 0x040034E2 RID: 13538
		public const int ALG_SID_MD5 = 3;

		// Token: 0x040034E3 RID: 13539
		public const int ALG_SID_SHA1 = 4;

		// Token: 0x040034E4 RID: 13540
		public const int ALG_SID_SHA_256 = 12;

		// Token: 0x040034E5 RID: 13541
		public const int ALG_SID_SHA_512 = 14;

		// Token: 0x040034E6 RID: 13542
		public const int ALG_SID_RC4 = 1;

		// Token: 0x040034E7 RID: 13543
		public const int ALG_SID_AES = 17;

		// Token: 0x040034E8 RID: 13544
		public const int CALG_MD5 = 32771;

		// Token: 0x040034E9 RID: 13545
		public const int CALG_SHA1 = 32772;

		// Token: 0x040034EA RID: 13546
		public const int CALG_SHA_256 = 32780;

		// Token: 0x040034EB RID: 13547
		public const int CALG_SHA_512 = 32782;

		// Token: 0x040034EC RID: 13548
		public const int CALG_RC4 = 26625;

		// Token: 0x040034ED RID: 13549
		public const int CALG_AES = 26129;

		// Token: 0x040034EE RID: 13550
		public const int PROV_RSA_AES = 24;

		// Token: 0x040034EF RID: 13551
		public const string MS_ENH_RSA_AES_PROV = "Microsoft Enhanced RSA and AES Cryptographic Provider";

		// Token: 0x040034F0 RID: 13552
		public const ushort VT_NULL = 1;

		// Token: 0x040034F1 RID: 13553
		public const ushort VT_BOOL = 11;

		// Token: 0x040034F2 RID: 13554
		public const ushort VT_UI1 = 17;

		// Token: 0x040034F3 RID: 13555
		public const ushort VT_UI2 = 18;

		// Token: 0x040034F4 RID: 13556
		public const ushort VT_UI4 = 19;

		// Token: 0x040034F5 RID: 13557
		public const ushort VT_UI8 = 21;

		// Token: 0x040034F6 RID: 13558
		public const ushort VT_LPWSTR = 31;

		// Token: 0x040034F7 RID: 13559
		public const ushort VT_VECTOR = 4096;

		// Token: 0x040034F8 RID: 13560
		public const uint MAX_PATH = 260U;

		// Token: 0x040034F9 RID: 13561
		public const uint LOAD_LIBRARY_AS_DATAFILE = 2U;

		// Token: 0x040034FA RID: 13562
		public const uint LOAD_LIBRARY_SEARCH_SYSTEM32 = 2048U;

		// Token: 0x040034FB RID: 13563
		internal static Lazy<bool> IsTailoredApplication = new Lazy<bool>(() => UnsafeNativeMethods._IsTailoredApplication());

		// Token: 0x02000D7E RID: 3454
		[StructLayout(LayoutKind.Sequential)]
		internal class SECURITY_ATTRIBUTES
		{
			// Token: 0x04004872 RID: 18546
			internal int nLength = Marshal.SizeOf(typeof(UnsafeNativeMethods.SECURITY_ATTRIBUTES));

			// Token: 0x04004873 RID: 18547
			internal IntPtr lpSecurityDescriptor = IntPtr.Zero;

			// Token: 0x04004874 RID: 18548
			internal bool bInheritHandle;
		}

		// Token: 0x02000D7F RID: 3455
		// (Invoke) Token: 0x06007E7B RID: 32379
		public unsafe delegate void MQReceiveCallback(int error, IntPtr handle, int timeout, int action, IntPtr props, NativeOverlapped* nativeOverlapped, IntPtr cursor);

		// Token: 0x02000D80 RID: 3456
		internal struct WSABuffer
		{
			// Token: 0x04004875 RID: 18549
			public int length;

			// Token: 0x04004876 RID: 18550
			public IntPtr buffer;
		}

		// Token: 0x02000D81 RID: 3457
		private struct TokenAppContainerInfo
		{
			// Token: 0x04004877 RID: 18551
			public IntPtr psid;
		}

		// Token: 0x02000D82 RID: 3458
		[StructLayout(LayoutKind.Sequential)]
		public class MQMSGPROPS
		{
			// Token: 0x04004878 RID: 18552
			public int count;

			// Token: 0x04004879 RID: 18553
			public IntPtr ids;

			// Token: 0x0400487A RID: 18554
			public IntPtr variants;

			// Token: 0x0400487B RID: 18555
			public IntPtr status;
		}

		// Token: 0x02000D83 RID: 3459
		[StructLayout(LayoutKind.Explicit)]
		public struct MQPROPVARIANT
		{
			// Token: 0x0400487C RID: 18556
			[FieldOffset(0)]
			public ushort vt;

			// Token: 0x0400487D RID: 18557
			[FieldOffset(2)]
			public ushort reserved1;

			// Token: 0x0400487E RID: 18558
			[FieldOffset(4)]
			public ushort reserved2;

			// Token: 0x0400487F RID: 18559
			[FieldOffset(6)]
			public ushort reserved3;

			// Token: 0x04004880 RID: 18560
			[FieldOffset(8)]
			public byte byteValue;

			// Token: 0x04004881 RID: 18561
			[FieldOffset(8)]
			public short shortValue;

			// Token: 0x04004882 RID: 18562
			[FieldOffset(8)]
			public int intValue;

			// Token: 0x04004883 RID: 18563
			[FieldOffset(8)]
			public long longValue;

			// Token: 0x04004884 RID: 18564
			[FieldOffset(8)]
			public IntPtr intPtr;

			// Token: 0x04004885 RID: 18565
			[FieldOffset(8)]
			public UnsafeNativeMethods.MQPROPVARIANT.CAUI1 byteArrayValue;

			// Token: 0x04004886 RID: 18566
			[FieldOffset(8)]
			public UnsafeNativeMethods.MQPROPVARIANT.CALPWSTR stringArraysValue;

			// Token: 0x02000F6D RID: 3949
			public struct CAUI1
			{
				// Token: 0x04004F19 RID: 20249
				public int size;

				// Token: 0x04004F1A RID: 20250
				public IntPtr intPtr;
			}

			// Token: 0x02000F6E RID: 3950
			public struct CALPWSTR
			{
				// Token: 0x04004F1B RID: 20251
				public int count;

				// Token: 0x04004F1C RID: 20252
				public IntPtr stringArrays;
			}
		}

		// Token: 0x02000D84 RID: 3460
		public struct MEMORYSTATUSEX
		{
			// Token: 0x04004887 RID: 18567
			public uint dwLength;

			// Token: 0x04004888 RID: 18568
			public uint dwMemoryLoad;

			// Token: 0x04004889 RID: 18569
			public ulong ullTotalPhys;

			// Token: 0x0400488A RID: 18570
			public ulong ullAvailPhys;

			// Token: 0x0400488B RID: 18571
			public ulong ullTotalPageFile;

			// Token: 0x0400488C RID: 18572
			public ulong ullAvailPageFile;

			// Token: 0x0400488D RID: 18573
			public ulong ullTotalVirtual;

			// Token: 0x0400488E RID: 18574
			public ulong ullAvailVirtual;

			// Token: 0x0400488F RID: 18575
			public ulong ullAvailExtendedVirtual;
		}

		// Token: 0x02000D85 RID: 3461
		private enum AppPolicyClrCompat
		{
			// Token: 0x04004891 RID: 18577
			AppPolicyClrCompat_Others,
			// Token: 0x04004892 RID: 18578
			AppPolicyClrCompat_ClassicDesktop,
			// Token: 0x04004893 RID: 18579
			AppPolicyClrCompat_Universal,
			// Token: 0x04004894 RID: 18580
			AppPolicyClrCompat_PackagedDesktop
		}
	}
}
