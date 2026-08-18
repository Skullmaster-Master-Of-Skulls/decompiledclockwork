using System;
using System.Collections;
using System.Net.Cache;
using System.Net.Sockets;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Threading;
using Microsoft.Win32.SafeHandles;

namespace System.Net
{
	// Token: 0x02000445 RID: 1093
	[SuppressUnmanagedCodeSecurity]
	internal static class UnsafeNclNativeMethods
	{
		// Token: 0x06002257 RID: 8791
		[DllImport("kernel32.dll")]
		internal static extern IntPtr CreateSemaphore([In] IntPtr lpSemaphoreAttributes, [In] int lInitialCount, [In] int lMaximumCount, [In] IntPtr lpName);

		// Token: 0x06002258 RID: 8792
		[DllImport("kernel32.dll")]
		internal static extern bool ReleaseSemaphore([In] IntPtr hSemaphore, [In] int lReleaseCount, [In] IntPtr lpPreviousCount);

		// Token: 0x06002259 RID: 8793
		[DllImport("kernel32.dll", CallingConvention = CallingConvention.StdCall, ExactSpelling = true, SetLastError = true)]
		internal static extern uint GetCurrentThreadId();

		// Token: 0x0600225A RID: 8794
		[DllImport("bcrypt.dll")]
		internal static extern uint BCryptGetFipsAlgorithmMode([MarshalAs(UnmanagedType.U1)] out bool pfEnabled);

		// Token: 0x0600225B RID: 8795
		[DllImport("kernel32.dll", ExactSpelling = true)]
		internal static extern void DebugBreak();

		// Token: 0x04002239 RID: 8761
		private const string KERNEL32 = "kernel32.dll";

		// Token: 0x0400223A RID: 8762
		private const string WS2_32 = "ws2_32.dll";

		// Token: 0x0400223B RID: 8763
		private const string SECUR32 = "secur32.dll";

		// Token: 0x0400223C RID: 8764
		private const string CRYPT32 = "crypt32.dll";

		// Token: 0x0400223D RID: 8765
		private const string ADVAPI32 = "advapi32.dll";

		// Token: 0x0400223E RID: 8766
		private const string HTTPAPI = "httpapi.dll";

		// Token: 0x0400223F RID: 8767
		private const string SCHANNEL = "schannel.dll";

		// Token: 0x04002240 RID: 8768
		private const string SECURITY = "security.dll";

		// Token: 0x04002241 RID: 8769
		private const string RASAPI32 = "rasapi32.dll";

		// Token: 0x04002242 RID: 8770
		private const string WININET = "wininet.dll";

		// Token: 0x04002243 RID: 8771
		private const string WINHTTP = "winhttp.dll";

		// Token: 0x04002244 RID: 8772
		private const string BCRYPT = "bcrypt.dll";

		// Token: 0x02000446 RID: 1094
		internal static class ErrorCodes
		{
			// Token: 0x04002245 RID: 8773
			internal const uint ERROR_SUCCESS = 0U;

			// Token: 0x04002246 RID: 8774
			internal const uint ERROR_HANDLE_EOF = 38U;

			// Token: 0x04002247 RID: 8775
			internal const uint ERROR_NOT_SUPPORTED = 50U;

			// Token: 0x04002248 RID: 8776
			internal const uint ERROR_INVALID_PARAMETER = 87U;

			// Token: 0x04002249 RID: 8777
			internal const uint ERROR_ALREADY_EXISTS = 183U;

			// Token: 0x0400224A RID: 8778
			internal const uint ERROR_MORE_DATA = 234U;

			// Token: 0x0400224B RID: 8779
			internal const uint ERROR_OPERATION_ABORTED = 995U;

			// Token: 0x0400224C RID: 8780
			internal const uint ERROR_IO_PENDING = 997U;

			// Token: 0x0400224D RID: 8781
			internal const uint ERROR_NOT_FOUND = 1168U;
		}

		// Token: 0x02000447 RID: 1095
		internal static class NTStatus
		{
			// Token: 0x0400224E RID: 8782
			internal const uint STATUS_SUCCESS = 0U;

			// Token: 0x0400224F RID: 8783
			internal const uint STATUS_OBJECT_NAME_NOT_FOUND = 3221225524U;
		}

		// Token: 0x02000448 RID: 1096
		[SuppressUnmanagedCodeSecurity]
		internal static class RegistryHelper
		{
			// Token: 0x0600225C RID: 8796
			[DllImport("advapi32.dll", BestFitMapping = false, CharSet = CharSet.Auto, SetLastError = true, ThrowOnUnmappableChar = true)]
			internal static extern uint RegOpenKeyEx(IntPtr key, string subKey, uint ulOptions, uint samDesired, out SafeRegistryHandle resultSubKey);

			// Token: 0x0600225D RID: 8797
			[DllImport("advapi32.dll", BestFitMapping = false, CharSet = CharSet.Auto, SetLastError = true, ThrowOnUnmappableChar = true)]
			internal static extern uint RegOpenKeyEx(SafeRegistryHandle key, string subKey, uint ulOptions, uint samDesired, out SafeRegistryHandle resultSubKey);

			// Token: 0x0600225E RID: 8798
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
			[DllImport("advapi32.dll", ExactSpelling = true, SetLastError = true)]
			internal static extern uint RegCloseKey(IntPtr key);

			// Token: 0x0600225F RID: 8799
			[DllImport("advapi32.dll", ExactSpelling = true, SetLastError = true)]
			internal static extern uint RegNotifyChangeKeyValue(SafeRegistryHandle key, bool watchSubTree, uint notifyFilter, SafeWaitHandle regEvent, bool async);

			// Token: 0x06002260 RID: 8800
			[DllImport("advapi32.dll", ExactSpelling = true, SetLastError = true)]
			internal static extern uint RegOpenCurrentUser(uint samDesired, out SafeRegistryHandle resultKey);

			// Token: 0x06002261 RID: 8801
			[DllImport("advapi32.dll", BestFitMapping = false, CharSet = CharSet.Auto, SetLastError = true, ThrowOnUnmappableChar = true)]
			internal static extern uint RegQueryValueEx(SafeRegistryHandle key, string valueName, IntPtr reserved, out uint type, [Out] byte[] data, [In] [Out] ref uint size);

			// Token: 0x04002250 RID: 8784
			internal const uint REG_NOTIFY_CHANGE_LAST_SET = 4U;

			// Token: 0x04002251 RID: 8785
			internal const uint REG_BINARY = 3U;

			// Token: 0x04002252 RID: 8786
			internal const uint KEY_READ = 131097U;

			// Token: 0x04002253 RID: 8787
			internal static readonly IntPtr HKEY_CURRENT_USER = (IntPtr)(-2147483647);

			// Token: 0x04002254 RID: 8788
			internal static readonly IntPtr HKEY_LOCAL_MACHINE = (IntPtr)(-2147483646);
		}

		// Token: 0x02000449 RID: 1097
		[SuppressUnmanagedCodeSecurity]
		internal class RasHelper
		{
			// Token: 0x06002263 RID: 8803 RVA: 0x0008880B File Offset: 0x0008780B
			static RasHelper()
			{
				UnsafeNclNativeMethods.RasHelper.InitRasSupported();
			}

			// Token: 0x06002264 RID: 8804 RVA: 0x00088814 File Offset: 0x00087814
			internal RasHelper()
			{
				if (!UnsafeNclNativeMethods.RasHelper.s_RasSupported)
				{
					throw new InvalidOperationException(SR.GetString("net_log_proxy_ras_notsupported_exception"));
				}
				this.m_RasEvent = new ManualResetEvent(false);
				uint num = UnsafeNclNativeMethods.RasHelper.RasConnectionNotification((IntPtr)(-1), this.m_RasEvent.SafeWaitHandle, 3U);
				if (num != 0U)
				{
					this.m_Suppressed = true;
					this.m_RasEvent.Close();
					this.m_RasEvent = null;
				}
			}

			// Token: 0x17000750 RID: 1872
			// (get) Token: 0x06002265 RID: 8805 RVA: 0x0008887E File Offset: 0x0008787E
			internal static bool RasSupported
			{
				get
				{
					return UnsafeNclNativeMethods.RasHelper.s_RasSupported;
				}
			}

			// Token: 0x17000751 RID: 1873
			// (get) Token: 0x06002266 RID: 8806 RVA: 0x00088888 File Offset: 0x00087888
			internal bool HasChanged
			{
				get
				{
					if (this.m_Suppressed)
					{
						return false;
					}
					ManualResetEvent rasEvent = this.m_RasEvent;
					if (rasEvent == null)
					{
						throw new ObjectDisposedException(base.GetType().FullName);
					}
					return rasEvent.WaitOne(0, false);
				}
			}

			// Token: 0x06002267 RID: 8807 RVA: 0x000888C4 File Offset: 0x000878C4
			internal void Reset()
			{
				if (!this.m_Suppressed)
				{
					ManualResetEvent rasEvent = this.m_RasEvent;
					if (rasEvent == null)
					{
						throw new ObjectDisposedException(base.GetType().FullName);
					}
					rasEvent.Reset();
				}
			}

			// Token: 0x06002268 RID: 8808 RVA: 0x000888FC File Offset: 0x000878FC
			internal static string GetCurrentConnectoid()
			{
				uint num = (uint)Marshal.SizeOf(typeof(UnsafeNclNativeMethods.RasHelper.RASCONN));
				if (!UnsafeNclNativeMethods.RasHelper.s_RasSupported)
				{
					return null;
				}
				uint num2 = 4U;
				UnsafeNclNativeMethods.RasHelper.RASCONN[] array;
				checked
				{
					uint num4;
					for (;;)
					{
						uint num3 = num * num2;
						array = new UnsafeNclNativeMethods.RasHelper.RASCONN[num2];
						array[0].dwSize = num;
						num4 = UnsafeNclNativeMethods.RasHelper.RasEnumConnections(array, ref num3, ref num2);
						if (num4 != 603U)
						{
							break;
						}
						num2 = (num3 + num - 1U) / num;
					}
					if (num2 == 0U || num4 != 0U)
					{
						return null;
					}
				}
				for (uint num5 = 0U; num5 < num2; num5 += 1U)
				{
					UnsafeNclNativeMethods.RasHelper.RASCONNSTATUS rasconnstatus = default(UnsafeNclNativeMethods.RasHelper.RASCONNSTATUS);
					rasconnstatus.dwSize = (uint)Marshal.SizeOf(rasconnstatus);
					if (UnsafeNclNativeMethods.RasHelper.RasGetConnectStatus(array[(int)((UIntPtr)num5)].hrasconn, ref rasconnstatus) == 0U && rasconnstatus.rasconnstate == UnsafeNclNativeMethods.RasHelper.RASCONNSTATE.RASCS_Connected)
					{
						return array[(int)((UIntPtr)num5)].szEntryName;
					}
				}
				return null;
			}

			// Token: 0x06002269 RID: 8809 RVA: 0x000889CA File Offset: 0x000879CA
			private static void InitRasSupported()
			{
				if (ComNetOS.InstallationType == WindowsInstallationType.ServerCore)
				{
					UnsafeNclNativeMethods.RasHelper.s_RasSupported = false;
					return;
				}
				UnsafeNclNativeMethods.RasHelper.s_RasSupported = true;
			}

			// Token: 0x0600226A RID: 8810
			[DllImport("rasapi32.dll", BestFitMapping = false, CharSet = CharSet.Auto, ThrowOnUnmappableChar = true)]
			private static extern uint RasEnumConnections([In] [Out] UnsafeNclNativeMethods.RasHelper.RASCONN[] lprasconn, ref uint lpcb, ref uint lpcConnections);

			// Token: 0x0600226B RID: 8811
			[DllImport("rasapi32.dll", BestFitMapping = false, CharSet = CharSet.Auto, ThrowOnUnmappableChar = true)]
			private static extern uint RasGetConnectStatus([In] IntPtr hrasconn, [In] [Out] ref UnsafeNclNativeMethods.RasHelper.RASCONNSTATUS lprasconnstatus);

			// Token: 0x0600226C RID: 8812
			[DllImport("rasapi32.dll", BestFitMapping = false, CharSet = CharSet.Auto, ThrowOnUnmappableChar = true)]
			private static extern uint RasConnectionNotification([In] IntPtr hrasconn, [In] SafeWaitHandle hEvent, uint dwFlags);

			// Token: 0x04002255 RID: 8789
			private const int RAS_MaxEntryName = 256;

			// Token: 0x04002256 RID: 8790
			private const int RAS_MaxDeviceType = 16;

			// Token: 0x04002257 RID: 8791
			private const int RAS_MaxDeviceName = 128;

			// Token: 0x04002258 RID: 8792
			private const int RAS_MaxPhoneNumber = 128;

			// Token: 0x04002259 RID: 8793
			private const int RAS_MaxCallbackNumber = 128;

			// Token: 0x0400225A RID: 8794
			private const uint RASCN_Connection = 1U;

			// Token: 0x0400225B RID: 8795
			private const uint RASCN_Disconnection = 2U;

			// Token: 0x0400225C RID: 8796
			private const int UNLEN = 256;

			// Token: 0x0400225D RID: 8797
			private const int PWLEN = 256;

			// Token: 0x0400225E RID: 8798
			private const int DNLEN = 15;

			// Token: 0x0400225F RID: 8799
			private const int MAX_PATH = 260;

			// Token: 0x04002260 RID: 8800
			private const uint RASBASE = 600U;

			// Token: 0x04002261 RID: 8801
			private const uint ERROR_DIAL_ALREADY_IN_PROGRESS = 756U;

			// Token: 0x04002262 RID: 8802
			private const uint ERROR_BUFFER_TOO_SMALL = 603U;

			// Token: 0x04002263 RID: 8803
			private const int RASCS_PAUSED = 4096;

			// Token: 0x04002264 RID: 8804
			private const int RASCS_DONE = 8192;

			// Token: 0x04002265 RID: 8805
			private static bool s_RasSupported;

			// Token: 0x04002266 RID: 8806
			private ManualResetEvent m_RasEvent;

			// Token: 0x04002267 RID: 8807
			private bool m_Suppressed;

			// Token: 0x0200044A RID: 1098
			[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto, Pack = 4)]
			private struct RASCONN
			{
				// Token: 0x04002268 RID: 8808
				internal uint dwSize;

				// Token: 0x04002269 RID: 8809
				internal IntPtr hrasconn;

				// Token: 0x0400226A RID: 8810
				[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 257)]
				internal string szEntryName;

				// Token: 0x0400226B RID: 8811
				[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 17)]
				internal string szDeviceType;

				// Token: 0x0400226C RID: 8812
				[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 129)]
				internal string szDeviceName;
			}

			// Token: 0x0200044B RID: 1099
			[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
			private struct RASCONNSTATUS
			{
				// Token: 0x0400226D RID: 8813
				internal uint dwSize;

				// Token: 0x0400226E RID: 8814
				internal UnsafeNclNativeMethods.RasHelper.RASCONNSTATE rasconnstate;

				// Token: 0x0400226F RID: 8815
				internal uint dwError;

				// Token: 0x04002270 RID: 8816
				[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 17)]
				internal string szDeviceType;

				// Token: 0x04002271 RID: 8817
				[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 129)]
				internal string szDeviceName;
			}

			// Token: 0x0200044C RID: 1100
			[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
			private struct RASDIALPARAMS
			{
				// Token: 0x04002272 RID: 8818
				internal uint dwSize;

				// Token: 0x04002273 RID: 8819
				[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 257)]
				internal string szEntryName;

				// Token: 0x04002274 RID: 8820
				[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 129)]
				internal string szPhoneNumber;

				// Token: 0x04002275 RID: 8821
				[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 129)]
				internal string szCallbackNumber;

				// Token: 0x04002276 RID: 8822
				[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 257)]
				internal string szUserName;

				// Token: 0x04002277 RID: 8823
				[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 257)]
				internal string szPassword;

				// Token: 0x04002278 RID: 8824
				[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 16)]
				internal string szDomain;
			}

			// Token: 0x0200044D RID: 1101
			private enum RASCONNSTATE
			{
				// Token: 0x0400227A RID: 8826
				RASCS_OpenPort,
				// Token: 0x0400227B RID: 8827
				RASCS_PortOpened,
				// Token: 0x0400227C RID: 8828
				RASCS_ConnectDevice,
				// Token: 0x0400227D RID: 8829
				RASCS_DeviceConnected,
				// Token: 0x0400227E RID: 8830
				RASCS_AllDevicesConnected,
				// Token: 0x0400227F RID: 8831
				RASCS_Authenticate,
				// Token: 0x04002280 RID: 8832
				RASCS_AuthNotify,
				// Token: 0x04002281 RID: 8833
				RASCS_AuthRetry,
				// Token: 0x04002282 RID: 8834
				RASCS_AuthCallback,
				// Token: 0x04002283 RID: 8835
				RASCS_AuthChangePassword,
				// Token: 0x04002284 RID: 8836
				RASCS_AuthProject,
				// Token: 0x04002285 RID: 8837
				RASCS_AuthLinkSpeed,
				// Token: 0x04002286 RID: 8838
				RASCS_AuthAck,
				// Token: 0x04002287 RID: 8839
				RASCS_ReAuthenticate,
				// Token: 0x04002288 RID: 8840
				RASCS_Authenticated,
				// Token: 0x04002289 RID: 8841
				RASCS_PrepareForCallback,
				// Token: 0x0400228A RID: 8842
				RASCS_WaitForModemReset,
				// Token: 0x0400228B RID: 8843
				RASCS_WaitForCallback,
				// Token: 0x0400228C RID: 8844
				RASCS_Projected,
				// Token: 0x0400228D RID: 8845
				RASCS_StartAuthentication,
				// Token: 0x0400228E RID: 8846
				RASCS_CallbackComplete,
				// Token: 0x0400228F RID: 8847
				RASCS_LogonNetwork,
				// Token: 0x04002290 RID: 8848
				RASCS_SubEntryConnected,
				// Token: 0x04002291 RID: 8849
				RASCS_SubEntryDisconnected,
				// Token: 0x04002292 RID: 8850
				RASCS_Interactive = 4096,
				// Token: 0x04002293 RID: 8851
				RASCS_RetryAuthentication,
				// Token: 0x04002294 RID: 8852
				RASCS_CallbackSetByCaller,
				// Token: 0x04002295 RID: 8853
				RASCS_PasswordExpired,
				// Token: 0x04002296 RID: 8854
				RASCS_InvokeEapUI,
				// Token: 0x04002297 RID: 8855
				RASCS_Connected = 8192,
				// Token: 0x04002298 RID: 8856
				RASCS_Disconnected
			}
		}

		// Token: 0x0200044E RID: 1102
		[SuppressUnmanagedCodeSecurity]
		internal static class SafeNetHandles_SECUR32
		{
			// Token: 0x0600226D RID: 8813
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
			[DllImport("secur32.dll", ExactSpelling = true, SetLastError = true)]
			internal static extern int FreeContextBuffer([In] IntPtr contextBuffer);

			// Token: 0x0600226E RID: 8814
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
			[DllImport("secur32.dll", ExactSpelling = true, SetLastError = true)]
			internal static extern int FreeCredentialsHandle(ref SSPIHandle handlePtr);

			// Token: 0x0600226F RID: 8815
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
			[DllImport("secur32.dll", ExactSpelling = true, SetLastError = true)]
			internal static extern int DeleteSecurityContext(ref SSPIHandle handlePtr);

			// Token: 0x06002270 RID: 8816
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
			[DllImport("secur32.dll", ExactSpelling = true, SetLastError = true)]
			internal unsafe static extern int AcceptSecurityContext(ref SSPIHandle credentialHandle, [In] void* inContextPtr, [In] SecurityBufferDescriptor inputBuffer, [In] ContextFlags inFlags, [In] Endianness endianness, ref SSPIHandle outContextPtr, [In] [Out] SecurityBufferDescriptor outputBuffer, [In] [Out] ref ContextFlags attributes, out long timeStamp);

			// Token: 0x06002271 RID: 8817
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
			[DllImport("secur32.dll", ExactSpelling = true, SetLastError = true)]
			internal unsafe static extern int QueryContextAttributesA(ref SSPIHandle contextHandle, [In] ContextAttribute attribute, [In] void* buffer);

			// Token: 0x06002272 RID: 8818
			[DllImport("secur32.dll", BestFitMapping = false, CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true, ThrowOnUnmappableChar = true)]
			internal unsafe static extern int AcquireCredentialsHandleA([In] string principal, [In] string moduleName, [In] int usage, [In] void* logonID, [In] ref AuthIdentity authdata, [In] void* keyCallback, [In] void* keyArgument, ref SSPIHandle handlePtr, out long timeStamp);

			// Token: 0x06002273 RID: 8819
			[DllImport("secur32.dll", BestFitMapping = false, CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true, ThrowOnUnmappableChar = true)]
			internal unsafe static extern int AcquireCredentialsHandleA([In] string principal, [In] string moduleName, [In] int usage, [In] void* logonID, [In] IntPtr zero, [In] void* keyCallback, [In] void* keyArgument, ref SSPIHandle handlePtr, out long timeStamp);

			// Token: 0x06002274 RID: 8820
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
			[DllImport("secur32.dll", ExactSpelling = true, SetLastError = true)]
			internal unsafe static extern int InitializeSecurityContextA(ref SSPIHandle credentialHandle, [In] void* inContextPtr, [In] byte* targetName, [In] ContextFlags inFlags, [In] int reservedI, [In] Endianness endianness, [In] SecurityBufferDescriptor inputBuffer, [In] int reservedII, ref SSPIHandle outContextPtr, [In] [Out] SecurityBufferDescriptor outputBuffer, [In] [Out] ref ContextFlags attributes, out long timeStamp);

			// Token: 0x06002275 RID: 8821
			[DllImport("secur32.dll", ExactSpelling = true, SetLastError = true)]
			internal static extern int EnumerateSecurityPackagesA(out int pkgnum, out SafeFreeContextBuffer_SECUR32 handle);
		}

		// Token: 0x0200044F RID: 1103
		[SuppressUnmanagedCodeSecurity]
		internal static class SafeNetHandles_SECURITY
		{
			// Token: 0x06002276 RID: 8822
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
			[DllImport("security.dll", ExactSpelling = true, SetLastError = true)]
			internal static extern int FreeContextBuffer([In] IntPtr contextBuffer);

			// Token: 0x06002277 RID: 8823
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
			[DllImport("security.dll", ExactSpelling = true, SetLastError = true)]
			internal static extern int FreeCredentialsHandle(ref SSPIHandle handlePtr);

			// Token: 0x06002278 RID: 8824
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
			[DllImport("security.dll", ExactSpelling = true, SetLastError = true)]
			internal static extern int DeleteSecurityContext(ref SSPIHandle handlePtr);

			// Token: 0x06002279 RID: 8825
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
			[DllImport("security.dll", ExactSpelling = true, SetLastError = true)]
			internal unsafe static extern int AcceptSecurityContext(ref SSPIHandle credentialHandle, [In] void* inContextPtr, [In] SecurityBufferDescriptor inputBuffer, [In] ContextFlags inFlags, [In] Endianness endianness, ref SSPIHandle outContextPtr, [In] [Out] SecurityBufferDescriptor outputBuffer, [In] [Out] ref ContextFlags attributes, out long timeStamp);

			// Token: 0x0600227A RID: 8826
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
			[DllImport("security.dll", ExactSpelling = true, SetLastError = true)]
			internal unsafe static extern int QueryContextAttributesW(ref SSPIHandle contextHandle, [In] ContextAttribute attribute, [In] void* buffer);

			// Token: 0x0600227B RID: 8827
			[DllImport("security.dll", ExactSpelling = true, SetLastError = true)]
			internal static extern int EnumerateSecurityPackagesW(out int pkgnum, out SafeFreeContextBuffer_SECURITY handle);

			// Token: 0x0600227C RID: 8828
			[DllImport("security.dll", CharSet = CharSet.Unicode, ExactSpelling = true, SetLastError = true)]
			internal unsafe static extern int AcquireCredentialsHandleW([In] string principal, [In] string moduleName, [In] int usage, [In] void* logonID, [In] ref AuthIdentity authdata, [In] void* keyCallback, [In] void* keyArgument, ref SSPIHandle handlePtr, out long timeStamp);

			// Token: 0x0600227D RID: 8829
			[DllImport("security.dll", CharSet = CharSet.Unicode, ExactSpelling = true, SetLastError = true)]
			internal unsafe static extern int AcquireCredentialsHandleW([In] string principal, [In] string moduleName, [In] int usage, [In] void* logonID, [In] IntPtr zero, [In] void* keyCallback, [In] void* keyArgument, ref SSPIHandle handlePtr, out long timeStamp);

			// Token: 0x0600227E RID: 8830
			[DllImport("security.dll", CharSet = CharSet.Unicode, ExactSpelling = true, SetLastError = true)]
			internal unsafe static extern int AcquireCredentialsHandleW([In] string principal, [In] string moduleName, [In] int usage, [In] void* logonID, [In] ref SecureCredential authData, [In] void* keyCallback, [In] void* keyArgument, ref SSPIHandle handlePtr, out long timeStamp);

			// Token: 0x0600227F RID: 8831
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
			[DllImport("security.dll", ExactSpelling = true, SetLastError = true)]
			internal unsafe static extern int InitializeSecurityContextW(ref SSPIHandle credentialHandle, [In] void* inContextPtr, [In] byte* targetName, [In] ContextFlags inFlags, [In] int reservedI, [In] Endianness endianness, [In] SecurityBufferDescriptor inputBuffer, [In] int reservedII, ref SSPIHandle outContextPtr, [In] [Out] SecurityBufferDescriptor outputBuffer, [In] [Out] ref ContextFlags attributes, out long timeStamp);

			// Token: 0x06002280 RID: 8832
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
			[DllImport("security.dll", ExactSpelling = true, SetLastError = true)]
			internal unsafe static extern int CompleteAuthToken([In] void* inContextPtr, [In] [Out] SecurityBufferDescriptor inputBuffers);
		}

		// Token: 0x02000450 RID: 1104
		[SuppressUnmanagedCodeSecurity]
		internal static class SafeNetHandles_SCHANNEL
		{
			// Token: 0x06002281 RID: 8833
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
			[DllImport("schannel.dll", ExactSpelling = true, SetLastError = true)]
			internal static extern int FreeContextBuffer([In] IntPtr contextBuffer);

			// Token: 0x06002282 RID: 8834
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
			[DllImport("schannel.dll", ExactSpelling = true, SetLastError = true)]
			internal unsafe static extern int QueryContextAttributesA(ref SSPIHandle contextHandle, [In] ContextAttribute attribute, [In] void* buffer);

			// Token: 0x06002283 RID: 8835
			[DllImport("schannel.dll", ExactSpelling = true, SetLastError = true)]
			internal static extern int EnumerateSecurityPackagesA(out int pkgnum, out SafeFreeContextBuffer_SCHANNEL handle);

			// Token: 0x06002284 RID: 8836
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
			[DllImport("schannel.dll", ExactSpelling = true, SetLastError = true)]
			internal unsafe static extern int InitializeSecurityContextA(ref SSPIHandle credentialHandle, [In] void* inContextPtr, [In] byte* targetName, [In] ContextFlags inFlags, [In] int reservedI, [In] Endianness endianness, [In] SecurityBufferDescriptor inputBuffer, [In] int reservedII, ref SSPIHandle outContextPtr, [In] [Out] SecurityBufferDescriptor outputBuffer, [In] [Out] ref ContextFlags attributes, out long timeStamp);

			// Token: 0x06002285 RID: 8837
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
			[DllImport("schannel.dll", ExactSpelling = true, SetLastError = true)]
			internal unsafe static extern int AcceptSecurityContext(ref SSPIHandle credentialHandle, [In] void* inContextPtr, [In] SecurityBufferDescriptor inputBuffer, [In] ContextFlags inFlags, [In] Endianness endianness, ref SSPIHandle outContextPtr, [In] [Out] SecurityBufferDescriptor outputBuffer, [In] [Out] ref ContextFlags attributes, out long timeStamp);

			// Token: 0x06002286 RID: 8838
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
			[DllImport("schannel.dll", ExactSpelling = true, SetLastError = true)]
			internal static extern int DeleteSecurityContext(ref SSPIHandle handlePtr);

			// Token: 0x06002287 RID: 8839
			[DllImport("schannel.dll", BestFitMapping = false, CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true, ThrowOnUnmappableChar = true)]
			internal unsafe static extern int AcquireCredentialsHandleA([In] string principal, [In] string moduleName, [In] int usage, [In] void* logonID, [In] ref SecureCredential authData, [In] void* keyCallback, [In] void* keyArgument, ref SSPIHandle handlePtr, out long timeStamp);

			// Token: 0x06002288 RID: 8840
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
			[DllImport("schannel.dll", ExactSpelling = true, SetLastError = true)]
			internal static extern int FreeCredentialsHandle(ref SSPIHandle handlePtr);
		}

		// Token: 0x02000451 RID: 1105
		[SuppressUnmanagedCodeSecurity]
		internal static class SafeNetHandlesSafeOverlappedFree
		{
			// Token: 0x06002289 RID: 8841
			[DllImport("kernel32.dll", ExactSpelling = true, SetLastError = true)]
			internal static extern SafeOverlappedFree LocalAlloc(int uFlags, UIntPtr sizetdwBytes);
		}

		// Token: 0x02000452 RID: 1106
		[SuppressUnmanagedCodeSecurity]
		internal static class SafeNetHandlesXPOrLater
		{
			// Token: 0x0600228A RID: 8842
			[DllImport("ws2_32.dll", BestFitMapping = false, CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true, ThrowOnUnmappableChar = true)]
			internal static extern int getaddrinfo([In] string nodename, [In] string servicename, [In] ref AddressInfo hints, out SafeFreeAddrInfo handle);

			// Token: 0x0600228B RID: 8843
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
			[DllImport("ws2_32.dll", ExactSpelling = true, SetLastError = true)]
			internal static extern void freeaddrinfo([In] IntPtr info);
		}

		// Token: 0x02000453 RID: 1107
		[SuppressUnmanagedCodeSecurity]
		internal static class SafeNetHandles
		{
			// Token: 0x0600228C RID: 8844
			[DllImport("security.dll", ExactSpelling = true, SetLastError = true)]
			internal static extern int QuerySecurityContextToken(ref SSPIHandle phContext, out SafeCloseHandle handle);

			// Token: 0x0600228D RID: 8845
			[DllImport("httpapi.dll", CallingConvention = CallingConvention.StdCall, ExactSpelling = true, SetLastError = true)]
			internal static extern uint HttpCreateHttpHandle(out SafeCloseHandle pReqQueueHandle, uint options);

			// Token: 0x0600228E RID: 8846
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
			[DllImport("kernel32.dll", ExactSpelling = true, SetLastError = true)]
			internal static extern bool CloseHandle(IntPtr handle);

			// Token: 0x0600228F RID: 8847
			[DllImport("kernel32.dll", ExactSpelling = true, SetLastError = true)]
			internal static extern SafeLocalFree LocalAlloc(int uFlags, UIntPtr sizetdwBytes);

			// Token: 0x06002290 RID: 8848
			[DllImport("kernel32.dll", EntryPoint = "LocalAlloc", SetLastError = true)]
			internal static extern SafeLocalFreeChannelBinding LocalAllocChannelBinding(int uFlags, UIntPtr sizetdwBytes);

			// Token: 0x06002291 RID: 8849
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
			[DllImport("kernel32.dll", ExactSpelling = true, SetLastError = true)]
			internal static extern IntPtr LocalFree(IntPtr handle);

			// Token: 0x06002292 RID: 8850
			[DllImport("kernel32.dll", BestFitMapping = false, CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true, ThrowOnUnmappableChar = true)]
			internal unsafe static extern SafeLoadLibrary LoadLibraryExA([In] string lpwLibFileName, [In] void* hFile, [In] uint dwFlags);

			// Token: 0x06002293 RID: 8851
			[DllImport("kernel32.dll", CharSet = CharSet.Unicode, ExactSpelling = true, SetLastError = true)]
			internal unsafe static extern SafeLoadLibrary LoadLibraryExW([In] string lpwLibFileName, [In] void* hFile, [In] uint dwFlags);

			// Token: 0x06002294 RID: 8852
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
			[DllImport("kernel32.dll", ExactSpelling = true, SetLastError = true)]
			internal static extern bool FreeLibrary([In] IntPtr hModule);

			// Token: 0x06002295 RID: 8853
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
			[DllImport("crypt32.dll", ExactSpelling = true, SetLastError = true)]
			internal static extern void CertFreeCertificateChain([In] IntPtr pChainContext);

			// Token: 0x06002296 RID: 8854
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
			[DllImport("crypt32.dll", ExactSpelling = true, SetLastError = true)]
			internal static extern bool CertFreeCertificateContext([In] IntPtr certContext);

			// Token: 0x06002297 RID: 8855
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
			[DllImport("kernel32.dll", ExactSpelling = true, SetLastError = true)]
			internal static extern IntPtr GlobalFree(IntPtr handle);

			// Token: 0x06002298 RID: 8856
			[DllImport("ws2_32.dll", ExactSpelling = true, SetLastError = true)]
			internal static extern SafeCloseSocket.InnerSafeCloseSocket accept([In] IntPtr socketHandle, [Out] byte[] socketAddress, [In] [Out] ref int socketAddressSize);

			// Token: 0x06002299 RID: 8857
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
			[DllImport("ws2_32.dll", ExactSpelling = true, SetLastError = true)]
			internal static extern SocketError closesocket([In] IntPtr socketHandle);

			// Token: 0x0600229A RID: 8858
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
			[DllImport("ws2_32.dll", ExactSpelling = true, SetLastError = true)]
			internal static extern SocketError ioctlsocket([In] IntPtr handle, [In] int cmd, [In] [Out] ref int argp);

			// Token: 0x0600229B RID: 8859
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
			[DllImport("ws2_32.dll", ExactSpelling = true, SetLastError = true)]
			internal static extern SocketError WSAEventSelect([In] IntPtr handle, [In] IntPtr Event, [In] AsyncEventBits NetworkEvents);

			// Token: 0x0600229C RID: 8860
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
			[DllImport("ws2_32.dll", ExactSpelling = true, SetLastError = true)]
			internal static extern SocketError setsockopt([In] IntPtr handle, [In] SocketOptionLevel optionLevel, [In] SocketOptionName optionName, [In] ref Linger linger, [In] int optionLength);

			// Token: 0x0600229D RID: 8861
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
			[DllImport("wininet.dll", ExactSpelling = true, SetLastError = true)]
			internal unsafe static extern bool RetrieveUrlCacheEntryFileW([In] char* urlName, [In] byte* entryPtr, [In] [Out] ref int entryBufSize, [In] int dwReserved);

			// Token: 0x0600229E RID: 8862
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
			[DllImport("wininet.dll", ExactSpelling = true, SetLastError = true)]
			internal unsafe static extern bool UnlockUrlCacheEntryFileW([In] char* urlName, [In] int dwReserved);
		}

		// Token: 0x02000454 RID: 1108
		[SuppressUnmanagedCodeSecurity]
		internal static class OSSOCK
		{
			// Token: 0x0600229F RID: 8863
			[DllImport("ws2_32.dll", CharSet = CharSet.Auto, SetLastError = true)]
			internal static extern SafeCloseSocket.InnerSafeCloseSocket WSASocket([In] AddressFamily addressFamily, [In] SocketType socketType, [In] ProtocolType protocolType, [In] IntPtr protocolInfo, [In] uint group, [In] SocketConstructorFlags flags);

			// Token: 0x060022A0 RID: 8864
			[DllImport("ws2_32.dll", CharSet = CharSet.Auto, SetLastError = true)]
			internal unsafe static extern SafeCloseSocket.InnerSafeCloseSocket WSASocket([In] AddressFamily addressFamily, [In] SocketType socketType, [In] ProtocolType protocolType, [In] byte* pinnedBuffer, [In] uint group, [In] SocketConstructorFlags flags);

			// Token: 0x060022A1 RID: 8865
			[DllImport("ws2_32.dll", BestFitMapping = false, CharSet = CharSet.Ansi, SetLastError = true, ThrowOnUnmappableChar = true)]
			internal static extern SocketError WSAStartup([In] short wVersionRequested, out WSAData lpWSAData);

			// Token: 0x060022A2 RID: 8866
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
			[DllImport("ws2_32.dll", SetLastError = true)]
			internal static extern SocketError ioctlsocket([In] SafeCloseSocket socketHandle, [In] int cmd, [In] [Out] ref int argp);

			// Token: 0x060022A3 RID: 8867
			[DllImport("ws2_32.dll", BestFitMapping = false, CharSet = CharSet.Ansi, SetLastError = true, ThrowOnUnmappableChar = true)]
			internal static extern IntPtr gethostbyname([In] string host);

			// Token: 0x060022A4 RID: 8868
			[DllImport("ws2_32.dll", SetLastError = true)]
			internal static extern IntPtr gethostbyaddr([In] ref int addr, [In] int len, [In] ProtocolFamily type);

			// Token: 0x060022A5 RID: 8869
			[DllImport("ws2_32.dll", BestFitMapping = false, CharSet = CharSet.Ansi, SetLastError = true, ThrowOnUnmappableChar = true)]
			internal static extern SocketError gethostname([Out] StringBuilder hostName, [In] int bufferLength);

			// Token: 0x060022A6 RID: 8870
			[DllImport("ws2_32.dll", BestFitMapping = false, CharSet = CharSet.Ansi, SetLastError = true, ThrowOnUnmappableChar = true)]
			internal static extern int inet_addr([In] string cp);

			// Token: 0x060022A7 RID: 8871
			[DllImport("ws2_32.dll", SetLastError = true)]
			internal static extern SocketError getpeername([In] SafeCloseSocket socketHandle, [Out] byte[] socketAddress, [In] [Out] ref int socketAddressSize);

			// Token: 0x060022A8 RID: 8872
			[DllImport("ws2_32.dll", SetLastError = true)]
			internal static extern SocketError getsockopt([In] SafeCloseSocket socketHandle, [In] SocketOptionLevel optionLevel, [In] SocketOptionName optionName, out int optionValue, [In] [Out] ref int optionLength);

			// Token: 0x060022A9 RID: 8873
			[DllImport("ws2_32.dll", SetLastError = true)]
			internal static extern SocketError getsockopt([In] SafeCloseSocket socketHandle, [In] SocketOptionLevel optionLevel, [In] SocketOptionName optionName, [Out] byte[] optionValue, [In] [Out] ref int optionLength);

			// Token: 0x060022AA RID: 8874
			[DllImport("ws2_32.dll", SetLastError = true)]
			internal static extern SocketError getsockopt([In] SafeCloseSocket socketHandle, [In] SocketOptionLevel optionLevel, [In] SocketOptionName optionName, out Linger optionValue, [In] [Out] ref int optionLength);

			// Token: 0x060022AB RID: 8875
			[DllImport("ws2_32.dll", SetLastError = true)]
			internal static extern SocketError getsockopt([In] SafeCloseSocket socketHandle, [In] SocketOptionLevel optionLevel, [In] SocketOptionName optionName, out IPMulticastRequest optionValue, [In] [Out] ref int optionLength);

			// Token: 0x060022AC RID: 8876
			[DllImport("ws2_32.dll", SetLastError = true)]
			internal static extern SocketError getsockopt([In] SafeCloseSocket socketHandle, [In] SocketOptionLevel optionLevel, [In] SocketOptionName optionName, out IPv6MulticastRequest optionValue, [In] [Out] ref int optionLength);

			// Token: 0x060022AD RID: 8877
			[DllImport("ws2_32.dll", SetLastError = true)]
			internal static extern SocketError setsockopt([In] SafeCloseSocket socketHandle, [In] SocketOptionLevel optionLevel, [In] SocketOptionName optionName, [In] ref int optionValue, [In] int optionLength);

			// Token: 0x060022AE RID: 8878
			[DllImport("ws2_32.dll", SetLastError = true)]
			internal static extern SocketError setsockopt([In] SafeCloseSocket socketHandle, [In] SocketOptionLevel optionLevel, [In] SocketOptionName optionName, [In] byte[] optionValue, [In] int optionLength);

			// Token: 0x060022AF RID: 8879
			[DllImport("ws2_32.dll", SetLastError = true)]
			internal static extern SocketError setsockopt([In] SafeCloseSocket socketHandle, [In] SocketOptionLevel optionLevel, [In] SocketOptionName optionName, [In] ref IntPtr pointer, [In] int optionLength);

			// Token: 0x060022B0 RID: 8880
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
			[DllImport("ws2_32.dll", SetLastError = true)]
			internal static extern SocketError setsockopt([In] SafeCloseSocket socketHandle, [In] SocketOptionLevel optionLevel, [In] SocketOptionName optionName, [In] ref Linger linger, [In] int optionLength);

			// Token: 0x060022B1 RID: 8881
			[DllImport("ws2_32.dll", SetLastError = true)]
			internal static extern SocketError setsockopt([In] SafeCloseSocket socketHandle, [In] SocketOptionLevel optionLevel, [In] SocketOptionName optionName, [In] ref IPMulticastRequest mreq, [In] int optionLength);

			// Token: 0x060022B2 RID: 8882
			[DllImport("ws2_32.dll", SetLastError = true)]
			internal static extern SocketError setsockopt([In] SafeCloseSocket socketHandle, [In] SocketOptionLevel optionLevel, [In] SocketOptionName optionName, [In] ref IPv6MulticastRequest mreq, [In] int optionLength);

			// Token: 0x060022B3 RID: 8883
			[DllImport("mswsock.dll", SetLastError = true)]
			internal static extern bool AcceptEx([In] SafeCloseSocket listenSocketHandle, [In] SafeCloseSocket acceptSocketHandle, [In] IntPtr buffer, [In] int len, [In] int localAddressLength, [In] int remoteAddressLength, out int bytesReceived, [In] SafeHandle overlapped);

			// Token: 0x060022B4 RID: 8884
			[DllImport("mswsock.dll", SetLastError = true)]
			internal static extern bool TransmitFile([In] SafeCloseSocket socket, [In] SafeHandle fileHandle, [In] int numberOfBytesToWrite, [In] int numberOfBytesPerSend, [In] SafeHandle overlapped, [In] TransmitFileBuffers buffers, [In] TransmitFileOptions flags);

			// Token: 0x060022B5 RID: 8885
			[DllImport("mswsock.dll", SetLastError = true)]
			internal static extern bool TransmitFile([In] SafeCloseSocket socket, [In] SafeHandle fileHandle, [In] int numberOfBytesToWrite, [In] int numberOfBytesPerSend, [In] IntPtr overlapped, [In] IntPtr buffers, [In] TransmitFileOptions flags);

			// Token: 0x060022B6 RID: 8886
			[DllImport("mswsock.dll", SetLastError = true)]
			internal static extern bool TransmitFile([In] SafeCloseSocket socket, [In] IntPtr fileHandle, [In] int numberOfBytesToWrite, [In] int numberOfBytesPerSend, [In] IntPtr overlapped, [In] IntPtr buffers, [In] TransmitFileOptions flags);

			// Token: 0x060022B7 RID: 8887
			[DllImport("mswsock.dll", EntryPoint = "TransmitFile", SetLastError = true)]
			internal static extern bool TransmitFile2([In] SafeCloseSocket socket, [In] IntPtr fileHandle, [In] int numberOfBytesToWrite, [In] int numberOfBytesPerSend, [In] SafeHandle overlapped, [In] TransmitFileBuffers buffers, [In] TransmitFileOptions flags);

			// Token: 0x060022B8 RID: 8888
			[DllImport("mswsock.dll", EntryPoint = "TransmitFile", SetLastError = true)]
			internal static extern bool TransmitFile_Blocking([In] IntPtr socket, [In] SafeHandle fileHandle, [In] int numberOfBytesToWrite, [In] int numberOfBytesPerSend, [In] SafeHandle overlapped, [In] TransmitFileBuffers buffers, [In] TransmitFileOptions flags);

			// Token: 0x060022B9 RID: 8889
			[DllImport("mswsock.dll", EntryPoint = "TransmitFile", SetLastError = true)]
			internal static extern bool TransmitFile_Blocking2([In] IntPtr socket, [In] IntPtr fileHandle, [In] int numberOfBytesToWrite, [In] int numberOfBytesPerSend, [In] SafeHandle overlapped, [In] TransmitFileBuffers buffers, [In] TransmitFileOptions flags);

			// Token: 0x060022BA RID: 8890
			[DllImport("mswsock.dll", SetLastError = true)]
			internal static extern void GetAcceptExSockaddrs([In] IntPtr buffer, [In] int receiveDataLength, [In] int localAddressLength, [In] int remoteAddressLength, out IntPtr localSocketAddress, out int localSocketAddressLength, out IntPtr remoteSocketAddress, out int remoteSocketAddressLength);

			// Token: 0x060022BB RID: 8891
			[DllImport("ws2_32.dll", SetLastError = true)]
			internal unsafe static extern int send([In] IntPtr socketHandle, [In] byte* pinnedBuffer, [In] int len, [In] SocketFlags socketFlags);

			// Token: 0x060022BC RID: 8892
			[DllImport("ws2_32.dll", SetLastError = true)]
			internal unsafe static extern int recv([In] IntPtr socketHandle, [In] byte* pinnedBuffer, [In] int len, [In] SocketFlags socketFlags);

			// Token: 0x060022BD RID: 8893
			[DllImport("ws2_32.dll", SetLastError = true)]
			internal static extern SocketError listen([In] SafeCloseSocket socketHandle, [In] int backlog);

			// Token: 0x060022BE RID: 8894
			[DllImport("ws2_32.dll", SetLastError = true)]
			internal static extern SocketError bind([In] SafeCloseSocket socketHandle, [In] byte[] socketAddress, [In] int socketAddressSize);

			// Token: 0x060022BF RID: 8895
			[DllImport("ws2_32.dll", SetLastError = true)]
			internal static extern SocketError shutdown([In] SafeCloseSocket socketHandle, [In] int how);

			// Token: 0x060022C0 RID: 8896
			[DllImport("ws2_32.dll", SetLastError = true)]
			internal unsafe static extern int sendto([In] IntPtr socketHandle, [In] byte* pinnedBuffer, [In] int len, [In] SocketFlags socketFlags, [In] byte[] socketAddress, [In] int socketAddressSize);

			// Token: 0x060022C1 RID: 8897
			[DllImport("ws2_32.dll", SetLastError = true)]
			internal unsafe static extern int recvfrom([In] IntPtr socketHandle, [In] byte* pinnedBuffer, [In] int len, [In] SocketFlags socketFlags, [Out] byte[] socketAddress, [In] [Out] ref int socketAddressSize);

			// Token: 0x060022C2 RID: 8898
			[DllImport("ws2_32.dll", SetLastError = true)]
			internal static extern SocketError getsockname([In] SafeCloseSocket socketHandle, [Out] byte[] socketAddress, [In] [Out] ref int socketAddressSize);

			// Token: 0x060022C3 RID: 8899
			[DllImport("ws2_32.dll", SetLastError = true)]
			internal static extern int select([In] int ignoredParameter, [In] [Out] IntPtr[] readfds, [In] [Out] IntPtr[] writefds, [In] [Out] IntPtr[] exceptfds, [In] ref TimeValue timeout);

			// Token: 0x060022C4 RID: 8900
			[DllImport("ws2_32.dll", SetLastError = true)]
			internal static extern int select([In] int ignoredParameter, [In] [Out] IntPtr[] readfds, [In] [Out] IntPtr[] writefds, [In] [Out] IntPtr[] exceptfds, [In] IntPtr nullTimeout);

			// Token: 0x060022C5 RID: 8901
			[DllImport("ws2_32.dll", SetLastError = true)]
			internal static extern SocketError WSAConnect([In] IntPtr socketHandle, [In] byte[] socketAddress, [In] int socketAddressSize, [In] IntPtr inBuffer, [In] IntPtr outBuffer, [In] IntPtr sQOS, [In] IntPtr gQOS);

			// Token: 0x060022C6 RID: 8902
			[DllImport("ws2_32.dll", SetLastError = true)]
			internal static extern SocketError WSASend([In] SafeCloseSocket socketHandle, [In] ref WSABuffer buffer, [In] int bufferCount, out int bytesTransferred, [In] SocketFlags socketFlags, [In] SafeHandle overlapped, [In] IntPtr completionRoutine);

			// Token: 0x060022C7 RID: 8903
			[DllImport("ws2_32.dll", SetLastError = true)]
			internal static extern SocketError WSASend([In] SafeCloseSocket socketHandle, [In] WSABuffer[] buffersArray, [In] int bufferCount, out int bytesTransferred, [In] SocketFlags socketFlags, [In] SafeHandle overlapped, [In] IntPtr completionRoutine);

			// Token: 0x060022C8 RID: 8904
			[DllImport("ws2_32.dll", SetLastError = true)]
			internal static extern SocketError WSASend([In] SafeCloseSocket socketHandle, [In] IntPtr buffers, [In] int bufferCount, out int bytesTransferred, [In] SocketFlags socketFlags, [In] IntPtr overlapped, [In] IntPtr completionRoutine);

			// Token: 0x060022C9 RID: 8905
			[DllImport("ws2_32.dll", EntryPoint = "WSASend", SetLastError = true)]
			internal static extern SocketError WSASend_Blocking([In] IntPtr socketHandle, [In] WSABuffer[] buffersArray, [In] int bufferCount, out int bytesTransferred, [In] SocketFlags socketFlags, [In] SafeHandle overlapped, [In] IntPtr completionRoutine);

			// Token: 0x060022CA RID: 8906
			[DllImport("ws2_32.dll", SetLastError = true)]
			internal static extern SocketError WSASendTo([In] SafeCloseSocket socketHandle, [In] ref WSABuffer buffer, [In] int bufferCount, out int bytesTransferred, [In] SocketFlags socketFlags, [In] IntPtr socketAddress, [In] int socketAddressSize, [In] SafeHandle overlapped, [In] IntPtr completionRoutine);

			// Token: 0x060022CB RID: 8907
			[DllImport("ws2_32.dll", SetLastError = true)]
			internal static extern SocketError WSASendTo([In] SafeCloseSocket socketHandle, [In] WSABuffer[] buffersArray, [In] int bufferCount, out int bytesTransferred, [In] SocketFlags socketFlags, [In] IntPtr socketAddress, [In] int socketAddressSize, [In] SafeNativeOverlapped overlapped, [In] IntPtr completionRoutine);

			// Token: 0x060022CC RID: 8908
			[DllImport("ws2_32.dll", SetLastError = true)]
			internal static extern SocketError WSARecv([In] SafeCloseSocket socketHandle, [In] [Out] ref WSABuffer buffer, [In] int bufferCount, out int bytesTransferred, [In] [Out] ref SocketFlags socketFlags, [In] SafeHandle overlapped, [In] IntPtr completionRoutine);

			// Token: 0x060022CD RID: 8909
			[DllImport("ws2_32.dll", SetLastError = true)]
			internal static extern SocketError WSARecv([In] SafeCloseSocket socketHandle, [In] [Out] WSABuffer[] buffers, [In] int bufferCount, out int bytesTransferred, [In] [Out] ref SocketFlags socketFlags, [In] SafeHandle overlapped, [In] IntPtr completionRoutine);

			// Token: 0x060022CE RID: 8910
			[DllImport("ws2_32.dll", SetLastError = true)]
			internal static extern SocketError WSARecv([In] SafeCloseSocket socketHandle, [In] IntPtr buffers, [In] int bufferCount, out int bytesTransferred, [In] [Out] ref SocketFlags socketFlags, [In] IntPtr overlapped, [In] IntPtr completionRoutine);

			// Token: 0x060022CF RID: 8911
			[DllImport("ws2_32.dll", EntryPoint = "WSARecv", SetLastError = true)]
			internal static extern SocketError WSARecv_Blocking([In] IntPtr socketHandle, [In] [Out] WSABuffer[] buffers, [In] int bufferCount, out int bytesTransferred, [In] [Out] ref SocketFlags socketFlags, [In] SafeHandle overlapped, [In] IntPtr completionRoutine);

			// Token: 0x060022D0 RID: 8912
			[DllImport("ws2_32.dll", SetLastError = true)]
			internal static extern SocketError WSARecvFrom([In] SafeCloseSocket socketHandle, [In] [Out] ref WSABuffer buffer, [In] int bufferCount, out int bytesTransferred, [In] [Out] ref SocketFlags socketFlags, [In] IntPtr socketAddressPointer, [In] IntPtr socketAddressSizePointer, [In] SafeHandle overlapped, [In] IntPtr completionRoutine);

			// Token: 0x060022D1 RID: 8913
			[DllImport("ws2_32.dll", SetLastError = true)]
			internal static extern SocketError WSARecvFrom([In] SafeCloseSocket socketHandle, [In] [Out] WSABuffer[] buffers, [In] int bufferCount, out int bytesTransferred, [In] [Out] ref SocketFlags socketFlags, [In] IntPtr socketAddressPointer, [In] IntPtr socketAddressSizePointer, [In] SafeNativeOverlapped overlapped, [In] IntPtr completionRoutine);

			// Token: 0x060022D2 RID: 8914
			[DllImport("ws2_32.dll", SetLastError = true)]
			internal static extern SocketError WSAEventSelect([In] SafeCloseSocket socketHandle, [In] SafeHandle Event, [In] AsyncEventBits NetworkEvents);

			// Token: 0x060022D3 RID: 8915
			[DllImport("ws2_32.dll", SetLastError = true)]
			internal static extern SocketError WSAEventSelect([In] SafeCloseSocket socketHandle, [In] IntPtr Event, [In] AsyncEventBits NetworkEvents);

			// Token: 0x060022D4 RID: 8916
			[DllImport("ws2_32.dll", SetLastError = true)]
			internal static extern SocketError WSAIoctl([In] SafeCloseSocket socketHandle, [In] int ioControlCode, [In] [Out] ref Guid guid, [In] int guidSize, out IntPtr funcPtr, [In] int funcPtrSize, out int bytesTransferred, [In] IntPtr shouldBeNull, [In] IntPtr shouldBeNull2);

			// Token: 0x060022D5 RID: 8917
			[DllImport("ws2_32.dll", EntryPoint = "WSAIoctl", SetLastError = true)]
			internal static extern SocketError WSAIoctl_Blocking([In] IntPtr socketHandle, [In] int ioControlCode, [In] byte[] inBuffer, [In] int inBufferSize, [Out] byte[] outBuffer, [In] int outBufferSize, out int bytesTransferred, [In] SafeHandle overlapped, [In] IntPtr completionRoutine);

			// Token: 0x060022D6 RID: 8918
			[DllImport("ws2_32.dll", EntryPoint = "WSAIoctl", SetLastError = true)]
			internal static extern SocketError WSAIoctl_Blocking_Internal([In] IntPtr socketHandle, [In] uint ioControlCode, [In] IntPtr inBuffer, [In] int inBufferSize, [Out] IntPtr outBuffer, [In] int outBufferSize, out int bytesTransferred, [In] SafeHandle overlapped, [In] IntPtr completionRoutine);

			// Token: 0x060022D7 RID: 8919
			[DllImport("ws2_32.dll", SetLastError = true)]
			internal static extern SocketError WSAEnumNetworkEvents([In] SafeCloseSocket socketHandle, [In] SafeWaitHandle Event, [In] [Out] ref NetworkEvents networkEvents);

			// Token: 0x060022D8 RID: 8920
			[DllImport("ws2_32.dll", SetLastError = true)]
			internal unsafe static extern int WSADuplicateSocket([In] SafeCloseSocket socketHandle, [In] uint targetProcessID, [In] byte* pinnedBuffer);

			// Token: 0x060022D9 RID: 8921
			[DllImport("ws2_32.dll", SetLastError = true)]
			internal static extern bool WSAGetOverlappedResult([In] SafeCloseSocket socketHandle, [In] SafeHandle overlapped, out uint bytesTransferred, [In] bool wait, out SocketFlags socketFlags);

			// Token: 0x060022DA RID: 8922
			[DllImport("ws2_32.dll", BestFitMapping = false, CharSet = CharSet.Ansi, SetLastError = true, ThrowOnUnmappableChar = true)]
			internal static extern SocketError WSAStringToAddress([In] string addressString, [In] AddressFamily addressFamily, [In] IntPtr lpProtocolInfo, [Out] byte[] socketAddress, [In] [Out] ref int socketAddressSize);

			// Token: 0x060022DB RID: 8923
			[DllImport("ws2_32.dll", BestFitMapping = false, CharSet = CharSet.Ansi, SetLastError = true, ThrowOnUnmappableChar = true)]
			internal static extern SocketError WSAAddressToString([In] byte[] socketAddress, [In] int socketAddressSize, [In] IntPtr lpProtocolInfo, [Out] StringBuilder addressString, [In] [Out] ref int addressStringLength);

			// Token: 0x060022DC RID: 8924
			[DllImport("ws2_32.dll", BestFitMapping = false, CharSet = CharSet.Ansi, SetLastError = true, ThrowOnUnmappableChar = true)]
			internal static extern SocketError getnameinfo([In] byte[] sa, [In] int salen, [In] [Out] StringBuilder host, [In] int hostlen, [In] [Out] StringBuilder serv, [In] int servlen, [In] int flags);

			// Token: 0x060022DD RID: 8925
			[DllImport("ws2_32.dll", CharSet = CharSet.Auto, SetLastError = true)]
			internal static extern int WSAEnumProtocols([MarshalAs(UnmanagedType.LPArray)] [In] int[] lpiProtocols, [In] SafeLocalFree lpProtocolBuffer, [In] [Out] ref uint lpdwBufferLength);

			// Token: 0x04002299 RID: 8857
			private const string WS2_32 = "ws2_32.dll";

			// Token: 0x0400229A RID: 8858
			private const string mswsock = "mswsock.dll";

			// Token: 0x02000455 RID: 1109
			[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
			internal struct WSAPROTOCOLCHAIN
			{
				// Token: 0x0400229B RID: 8859
				internal int ChainLen;

				// Token: 0x0400229C RID: 8860
				[MarshalAs(UnmanagedType.ByValArray, SizeConst = 7)]
				internal uint[] ChainEntries;
			}

			// Token: 0x02000456 RID: 1110
			[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
			internal struct WSAPROTOCOL_INFO
			{
				// Token: 0x0400229D RID: 8861
				internal uint dwServiceFlags1;

				// Token: 0x0400229E RID: 8862
				internal uint dwServiceFlags2;

				// Token: 0x0400229F RID: 8863
				internal uint dwServiceFlags3;

				// Token: 0x040022A0 RID: 8864
				internal uint dwServiceFlags4;

				// Token: 0x040022A1 RID: 8865
				internal uint dwProviderFlags;

				// Token: 0x040022A2 RID: 8866
				private Guid ProviderId;

				// Token: 0x040022A3 RID: 8867
				internal uint dwCatalogEntryId;

				// Token: 0x040022A4 RID: 8868
				private UnsafeNclNativeMethods.OSSOCK.WSAPROTOCOLCHAIN ProtocolChain;

				// Token: 0x040022A5 RID: 8869
				internal int iVersion;

				// Token: 0x040022A6 RID: 8870
				internal AddressFamily iAddressFamily;

				// Token: 0x040022A7 RID: 8871
				internal int iMaxSockAddr;

				// Token: 0x040022A8 RID: 8872
				internal int iMinSockAddr;

				// Token: 0x040022A9 RID: 8873
				internal int iSocketType;

				// Token: 0x040022AA RID: 8874
				internal int iProtocol;

				// Token: 0x040022AB RID: 8875
				internal int iProtocolMaxOffset;

				// Token: 0x040022AC RID: 8876
				internal int iNetworkByteOrder;

				// Token: 0x040022AD RID: 8877
				internal int iSecurityScheme;

				// Token: 0x040022AE RID: 8878
				internal uint dwMessageSize;

				// Token: 0x040022AF RID: 8879
				internal uint dwProviderReserved;

				// Token: 0x040022B0 RID: 8880
				[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
				internal string szProtocol;
			}

			// Token: 0x02000457 RID: 1111
			internal struct ControlData
			{
				// Token: 0x040022B1 RID: 8881
				internal UIntPtr length;

				// Token: 0x040022B2 RID: 8882
				internal uint level;

				// Token: 0x040022B3 RID: 8883
				internal uint type;

				// Token: 0x040022B4 RID: 8884
				internal uint address;

				// Token: 0x040022B5 RID: 8885
				internal uint index;
			}

			// Token: 0x02000458 RID: 1112
			internal struct ControlDataIPv6
			{
				// Token: 0x040022B6 RID: 8886
				internal UIntPtr length;

				// Token: 0x040022B7 RID: 8887
				internal uint level;

				// Token: 0x040022B8 RID: 8888
				internal uint type;

				// Token: 0x040022B9 RID: 8889
				[MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
				internal byte[] address;

				// Token: 0x040022BA RID: 8890
				internal uint index;
			}

			// Token: 0x02000459 RID: 1113
			internal struct WSAMsg
			{
				// Token: 0x040022BB RID: 8891
				internal IntPtr socketAddress;

				// Token: 0x040022BC RID: 8892
				internal uint addressLength;

				// Token: 0x040022BD RID: 8893
				internal IntPtr buffers;

				// Token: 0x040022BE RID: 8894
				internal uint count;

				// Token: 0x040022BF RID: 8895
				internal WSABuffer controlBuffer;

				// Token: 0x040022C0 RID: 8896
				internal SocketFlags flags;
			}

			// Token: 0x0200045A RID: 1114
			[Flags]
			internal enum TransmitPacketsElementFlags : uint
			{
				// Token: 0x040022C2 RID: 8898
				None = 0U,
				// Token: 0x040022C3 RID: 8899
				Memory = 1U,
				// Token: 0x040022C4 RID: 8900
				File = 2U,
				// Token: 0x040022C5 RID: 8901
				EndOfPacket = 4U
			}

			// Token: 0x0200045B RID: 1115
			[StructLayout(LayoutKind.Explicit)]
			internal struct TransmitPacketsElement
			{
				// Token: 0x040022C6 RID: 8902
				[FieldOffset(0)]
				internal UnsafeNclNativeMethods.OSSOCK.TransmitPacketsElementFlags flags;

				// Token: 0x040022C7 RID: 8903
				[FieldOffset(4)]
				internal uint length;

				// Token: 0x040022C8 RID: 8904
				[FieldOffset(8)]
				internal long fileOffset;

				// Token: 0x040022C9 RID: 8905
				[FieldOffset(8)]
				internal IntPtr buffer;

				// Token: 0x040022CA RID: 8906
				[FieldOffset(16)]
				internal IntPtr fileHandle;
			}

			// Token: 0x0200045C RID: 1116
			internal struct SOCKET_ADDRESS
			{
				// Token: 0x040022CB RID: 8907
				internal IntPtr lpSockAddr;

				// Token: 0x040022CC RID: 8908
				internal int iSockaddrLength;
			}

			// Token: 0x0200045D RID: 1117
			internal struct SOCKET_ADDRESS_LIST
			{
				// Token: 0x040022CD RID: 8909
				internal int iAddressCount;

				// Token: 0x040022CE RID: 8910
				internal UnsafeNclNativeMethods.OSSOCK.SOCKET_ADDRESS Addresses;
			}

			// Token: 0x0200045E RID: 1118
			internal struct TransmitFileBuffersStruct
			{
				// Token: 0x040022CF RID: 8911
				internal IntPtr preBuffer;

				// Token: 0x040022D0 RID: 8912
				internal int preBufferLength;

				// Token: 0x040022D1 RID: 8913
				internal IntPtr postBuffer;

				// Token: 0x040022D2 RID: 8914
				internal int postBufferLength;
			}
		}

		// Token: 0x0200045F RID: 1119
		[SuppressUnmanagedCodeSecurity]
		internal static class NativePKI
		{
			// Token: 0x060022DE RID: 8926
			[DllImport("crypt32.dll", ExactSpelling = true, SetLastError = true)]
			internal static extern int CertVerifyCertificateChainPolicy([In] IntPtr policy, [In] SafeFreeCertChain chainContext, [In] ref ChainPolicyParameter cpp, [In] [Out] ref ChainPolicyStatus ps);

			// Token: 0x040022D3 RID: 8915
			private const string CRYPT32 = "crypt32.dll";
		}

		// Token: 0x02000460 RID: 1120
		[SuppressUnmanagedCodeSecurity]
		internal static class NativeNTSSPI
		{
			// Token: 0x060022DF RID: 8927
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
			[DllImport("security.dll", ExactSpelling = true, SetLastError = true)]
			internal static extern int EncryptMessage(ref SSPIHandle contextHandle, [In] uint qualityOfProtection, [In] [Out] SecurityBufferDescriptor inputOutput, [In] uint sequenceNumber);

			// Token: 0x060022E0 RID: 8928
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
			[DllImport("security.dll", ExactSpelling = true, SetLastError = true)]
			internal unsafe static extern int DecryptMessage([In] ref SSPIHandle contextHandle, [In] [Out] SecurityBufferDescriptor inputOutput, [In] uint sequenceNumber, uint* qualityOfProtection);

			// Token: 0x040022D4 RID: 8916
			private const string SECURITY = "security.dll";
		}

		// Token: 0x02000461 RID: 1121
		[SuppressUnmanagedCodeSecurity]
		internal static class NativeSSLWin9xSSPI
		{
			// Token: 0x060022E1 RID: 8929
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
			[DllImport("schannel.dll", ExactSpelling = true, SetLastError = true)]
			internal static extern int SealMessage(ref SSPIHandle contextHandle, [In] uint qualityOfProtection, [In] [Out] SecurityBufferDescriptor inputOutput, [In] uint sequenceNumber);

			// Token: 0x060022E2 RID: 8930
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
			[DllImport("schannel.dll", ExactSpelling = true, SetLastError = true)]
			internal static extern int UnsealMessage([In] ref SSPIHandle contextHandle, [In] [Out] SecurityBufferDescriptor inputOutput, [In] IntPtr qualityOfProtection, [In] uint sequenceNumber);

			// Token: 0x040022D5 RID: 8917
			private const string SCHANNEL = "schannel.dll";

			// Token: 0x040022D6 RID: 8918
			private const string SECUR32 = "secur32.dll";
		}

		// Token: 0x02000462 RID: 1122
		[SuppressUnmanagedCodeSecurity]
		internal static class WinInet
		{
			// Token: 0x060022E3 RID: 8931
			[DllImport("wininet.dll", BestFitMapping = false, CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true, ThrowOnUnmappableChar = true)]
			internal static extern bool DetectAutoProxyUrl([Out] StringBuilder autoProxyUrl, [In] int autoProxyUrlLength, [In] int detectFlags);
		}

		// Token: 0x02000463 RID: 1123
		[SuppressUnmanagedCodeSecurity]
		internal static class WinHttp
		{
			// Token: 0x060022E4 RID: 8932
			[DllImport("winhttp.dll", ExactSpelling = true, SetLastError = true)]
			internal static extern bool WinHttpDetectAutoProxyConfigUrl(UnsafeNclNativeMethods.WinHttp.AutoDetectType autoDetectFlags, out SafeGlobalFree autoConfigUrl);

			// Token: 0x060022E5 RID: 8933
			[DllImport("winhttp.dll", SetLastError = true)]
			internal static extern bool WinHttpGetIEProxyConfigForCurrentUser(ref UnsafeNclNativeMethods.WinHttp.WINHTTP_CURRENT_USER_IE_PROXY_CONFIG proxyConfig);

			// Token: 0x060022E6 RID: 8934
			[DllImport("winhttp.dll", CharSet = CharSet.Unicode, SetLastError = true)]
			internal static extern SafeInternetHandle WinHttpOpen(string userAgent, UnsafeNclNativeMethods.WinHttp.AccessType accessType, string proxyName, string proxyBypass, int dwFlags);

			// Token: 0x060022E7 RID: 8935
			[DllImport("winhttp.dll", CharSet = CharSet.Unicode, SetLastError = true)]
			internal static extern bool WinHttpSetTimeouts(SafeInternetHandle session, int resolveTimeout, int connectTimeout, int sendTimeout, int receiveTimeout);

			// Token: 0x060022E8 RID: 8936
			[DllImport("winhttp.dll", CharSet = CharSet.Unicode, SetLastError = true)]
			internal static extern bool WinHttpGetProxyForUrl(SafeInternetHandle session, string url, [In] ref UnsafeNclNativeMethods.WinHttp.WINHTTP_AUTOPROXY_OPTIONS autoProxyOptions, out UnsafeNclNativeMethods.WinHttp.WINHTTP_PROXY_INFO proxyInfo);

			// Token: 0x060022E9 RID: 8937
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
			[DllImport("winhttp.dll", CharSet = CharSet.Unicode, SetLastError = true)]
			internal static extern bool WinHttpCloseHandle(IntPtr httpSession);

			// Token: 0x02000464 RID: 1124
			[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
			internal struct WINHTTP_CURRENT_USER_IE_PROXY_CONFIG
			{
				// Token: 0x040022D7 RID: 8919
				public bool AutoDetect;

				// Token: 0x040022D8 RID: 8920
				public IntPtr AutoConfigUrl;

				// Token: 0x040022D9 RID: 8921
				public IntPtr Proxy;

				// Token: 0x040022DA RID: 8922
				public IntPtr ProxyBypass;
			}

			// Token: 0x02000465 RID: 1125
			[Flags]
			internal enum AutoProxyFlags
			{
				// Token: 0x040022DC RID: 8924
				AutoDetect = 1,
				// Token: 0x040022DD RID: 8925
				AutoProxyConfigUrl = 2,
				// Token: 0x040022DE RID: 8926
				RunInProcess = 65536,
				// Token: 0x040022DF RID: 8927
				RunOutProcessOnly = 131072
			}

			// Token: 0x02000466 RID: 1126
			internal enum AccessType
			{
				// Token: 0x040022E1 RID: 8929
				DefaultProxy,
				// Token: 0x040022E2 RID: 8930
				NoProxy,
				// Token: 0x040022E3 RID: 8931
				NamedProxy = 3
			}

			// Token: 0x02000467 RID: 1127
			[Flags]
			internal enum AutoDetectType
			{
				// Token: 0x040022E5 RID: 8933
				None = 0,
				// Token: 0x040022E6 RID: 8934
				Dhcp = 1,
				// Token: 0x040022E7 RID: 8935
				DnsA = 2
			}

			// Token: 0x02000468 RID: 1128
			[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
			internal struct WINHTTP_AUTOPROXY_OPTIONS
			{
				// Token: 0x040022E8 RID: 8936
				public UnsafeNclNativeMethods.WinHttp.AutoProxyFlags Flags;

				// Token: 0x040022E9 RID: 8937
				public UnsafeNclNativeMethods.WinHttp.AutoDetectType AutoDetectFlags;

				// Token: 0x040022EA RID: 8938
				[MarshalAs(UnmanagedType.LPWStr)]
				public string AutoConfigUrl;

				// Token: 0x040022EB RID: 8939
				private IntPtr lpvReserved;

				// Token: 0x040022EC RID: 8940
				private int dwReserved;

				// Token: 0x040022ED RID: 8941
				public bool AutoLogonIfChallenged;
			}

			// Token: 0x02000469 RID: 1129
			[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
			internal struct WINHTTP_PROXY_INFO
			{
				// Token: 0x040022EE RID: 8942
				public UnsafeNclNativeMethods.WinHttp.AccessType AccessType;

				// Token: 0x040022EF RID: 8943
				public IntPtr Proxy;

				// Token: 0x040022F0 RID: 8944
				public IntPtr ProxyBypass;
			}

			// Token: 0x0200046A RID: 1130
			internal enum ErrorCodes
			{
				// Token: 0x040022F2 RID: 8946
				Success,
				// Token: 0x040022F3 RID: 8947
				OutOfHandles = 12001,
				// Token: 0x040022F4 RID: 8948
				Timeout,
				// Token: 0x040022F5 RID: 8949
				InternalError = 12004,
				// Token: 0x040022F6 RID: 8950
				InvalidUrl,
				// Token: 0x040022F7 RID: 8951
				UnrecognizedScheme,
				// Token: 0x040022F8 RID: 8952
				NameNotResolved,
				// Token: 0x040022F9 RID: 8953
				InvalidOption = 12009,
				// Token: 0x040022FA RID: 8954
				OptionNotSettable = 12011,
				// Token: 0x040022FB RID: 8955
				Shutdown,
				// Token: 0x040022FC RID: 8956
				LoginFailure = 12015,
				// Token: 0x040022FD RID: 8957
				OperationCancelled = 12017,
				// Token: 0x040022FE RID: 8958
				IncorrectHandleType,
				// Token: 0x040022FF RID: 8959
				IncorrectHandleState,
				// Token: 0x04002300 RID: 8960
				CannotConnect = 12029,
				// Token: 0x04002301 RID: 8961
				ConnectionError,
				// Token: 0x04002302 RID: 8962
				ResendRequest = 12032,
				// Token: 0x04002303 RID: 8963
				AuthCertNeeded = 12044,
				// Token: 0x04002304 RID: 8964
				CannotCallBeforeOpen = 12100,
				// Token: 0x04002305 RID: 8965
				CannotCallBeforeSend,
				// Token: 0x04002306 RID: 8966
				CannotCallAfterSend,
				// Token: 0x04002307 RID: 8967
				CannotCallAfterOpen,
				// Token: 0x04002308 RID: 8968
				HeaderNotFound = 12150,
				// Token: 0x04002309 RID: 8969
				InvalidServerResponse = 12152,
				// Token: 0x0400230A RID: 8970
				InvalidHeader,
				// Token: 0x0400230B RID: 8971
				InvalidQueryRequest,
				// Token: 0x0400230C RID: 8972
				HeaderAlreadyExists,
				// Token: 0x0400230D RID: 8973
				RedirectFailed,
				// Token: 0x0400230E RID: 8974
				AutoProxyServiceError = 12178,
				// Token: 0x0400230F RID: 8975
				BadAutoProxyScript = 12166,
				// Token: 0x04002310 RID: 8976
				UnableToDownloadScript,
				// Token: 0x04002311 RID: 8977
				NotInitialized = 12172,
				// Token: 0x04002312 RID: 8978
				SecureFailure = 12175,
				// Token: 0x04002313 RID: 8979
				SecureCertDateInvalid = 12037,
				// Token: 0x04002314 RID: 8980
				SecureCertCNInvalid,
				// Token: 0x04002315 RID: 8981
				SecureInvalidCA = 12045,
				// Token: 0x04002316 RID: 8982
				SecureCertRevFailed = 12057,
				// Token: 0x04002317 RID: 8983
				SecureChannelError = 12157,
				// Token: 0x04002318 RID: 8984
				SecureInvalidCert = 12169,
				// Token: 0x04002319 RID: 8985
				SecureCertRevoked,
				// Token: 0x0400231A RID: 8986
				SecureCertWrongUsage = 12179,
				// Token: 0x0400231B RID: 8987
				AudodetectionFailed,
				// Token: 0x0400231C RID: 8988
				HeaderCountExceeded,
				// Token: 0x0400231D RID: 8989
				HeaderSizeOverflow,
				// Token: 0x0400231E RID: 8990
				ChunkedEncodingHeaderSizeOverflow,
				// Token: 0x0400231F RID: 8991
				ResponseDrainOverflow,
				// Token: 0x04002320 RID: 8992
				ClientCertNoPrivateKey,
				// Token: 0x04002321 RID: 8993
				ClientCertNoAccessPrivateKey
			}
		}

		// Token: 0x0200046B RID: 1131
		[SuppressUnmanagedCodeSecurity]
		internal static class UnsafeWinInetCache
		{
			// Token: 0x060022EA RID: 8938
			[DllImport("wininet.dll", CharSet = CharSet.Unicode, ExactSpelling = true, SetLastError = true)]
			internal static extern bool CreateUrlCacheEntryW([In] string urlName, [In] int expectedFileSize, [In] string fileExtension, [Out] StringBuilder fileName, [In] int dwReserved);

			// Token: 0x060022EB RID: 8939
			[DllImport("wininet.dll", CharSet = CharSet.Unicode, ExactSpelling = true, SetLastError = true)]
			internal unsafe static extern bool CommitUrlCacheEntryW([In] string urlName, [In] string localFileName, [In] _WinInetCache.FILETIME expireTime, [In] _WinInetCache.FILETIME lastModifiedTime, [In] _WinInetCache.EntryType EntryType, [In] byte* headerInfo, [In] int headerSizeTChars, [In] string fileExtension, [In] string originalUrl);

			// Token: 0x060022EC RID: 8940
			[DllImport("wininet.dll", CharSet = CharSet.Unicode, ExactSpelling = true, SetLastError = true)]
			internal unsafe static extern bool GetUrlCacheEntryInfoW([In] string urlName, [In] byte* entryPtr, [In] [Out] ref int bufferSz);

			// Token: 0x060022ED RID: 8941
			[DllImport("wininet.dll", CharSet = CharSet.Unicode, ExactSpelling = true, SetLastError = true)]
			internal unsafe static extern bool SetUrlCacheEntryInfoW([In] string lpszUrlName, [In] byte* EntryPtr, [In] _WinInetCache.Entry_FC fieldControl);

			// Token: 0x060022EE RID: 8942
			[DllImport("wininet.dll", CharSet = CharSet.Unicode, ExactSpelling = true, SetLastError = true)]
			internal static extern bool DeleteUrlCacheEntryW([In] string urlName);

			// Token: 0x060022EF RID: 8943
			[DllImport("wininet.dll", CharSet = CharSet.Unicode, ExactSpelling = true, SetLastError = true)]
			internal static extern bool UnlockUrlCacheEntryFileW([In] string urlName, [In] int dwReserved);

			// Token: 0x04002322 RID: 8994
			public const int MAX_PATH = 260;
		}

		// Token: 0x0200046C RID: 1132
		[SuppressUnmanagedCodeSecurity]
		internal static class HttpApi
		{
			// Token: 0x060022F0 RID: 8944
			[DllImport("httpapi.dll", CallingConvention = CallingConvention.StdCall, ExactSpelling = true, SetLastError = true)]
			internal unsafe static extern uint HttpInitialize(UnsafeNclNativeMethods.HttpApi.HTTPAPI_VERSION Version, uint Flags, void* Reserved);

			// Token: 0x060022F1 RID: 8945
			[DllImport("httpapi.dll", CallingConvention = CallingConvention.StdCall, ExactSpelling = true, SetLastError = true)]
			internal unsafe static extern uint HttpReceiveRequestEntityBody(SafeCloseHandle RequestQueueHandle, ulong RequestId, uint Flags, void* pEntityBuffer, uint EntityBufferLength, uint* pBytesReturned, NativeOverlapped* pOverlapped);

			// Token: 0x060022F2 RID: 8946
			[DllImport("httpapi.dll", CallingConvention = CallingConvention.StdCall, ExactSpelling = true, SetLastError = true)]
			internal unsafe static extern uint HttpReceiveClientCertificate(SafeCloseHandle RequestQueueHandle, ulong ConnectionId, uint Flags, UnsafeNclNativeMethods.HttpApi.HTTP_SSL_CLIENT_CERT_INFO* pSslClientCertInfo, uint SslClientCertInfoSize, uint* pBytesReceived, NativeOverlapped* pOverlapped);

			// Token: 0x060022F3 RID: 8947
			[DllImport("httpapi.dll", CallingConvention = CallingConvention.StdCall, ExactSpelling = true, SetLastError = true)]
			internal unsafe static extern uint HttpReceiveClientCertificate(SafeCloseHandle RequestQueueHandle, ulong ConnectionId, uint Flags, byte* pSslClientCertInfo, uint SslClientCertInfoSize, uint* pBytesReceived, NativeOverlapped* pOverlapped);

			// Token: 0x060022F4 RID: 8948
			[DllImport("httpapi.dll", CallingConvention = CallingConvention.StdCall, ExactSpelling = true, SetLastError = true)]
			internal unsafe static extern uint HttpReceiveHttpRequest(SafeCloseHandle RequestQueueHandle, ulong RequestId, uint Flags, UnsafeNclNativeMethods.HttpApi.HTTP_REQUEST* pRequestBuffer, uint RequestBufferLength, uint* pBytesReturned, NativeOverlapped* pOverlapped);

			// Token: 0x060022F5 RID: 8949
			[DllImport("httpapi.dll", CallingConvention = CallingConvention.StdCall, ExactSpelling = true, SetLastError = true)]
			internal unsafe static extern uint HttpAddUrl(SafeCloseHandle RequestQueueHandle, ushort* pFullyQualifiedUrl, void* pReserved);

			// Token: 0x060022F6 RID: 8950
			[DllImport("httpapi.dll", CallingConvention = CallingConvention.StdCall, ExactSpelling = true, SetLastError = true)]
			internal unsafe static extern uint HttpRemoveUrl(SafeCloseHandle RequestQueueHandle, ushort* pFullyQualifiedUrl);

			// Token: 0x060022F7 RID: 8951
			[DllImport("httpapi.dll", CallingConvention = CallingConvention.StdCall, ExactSpelling = true, SetLastError = true)]
			internal unsafe static extern uint HttpSendHttpResponse(SafeCloseHandle RequestQueueHandle, ulong RequestId, uint Flags, UnsafeNclNativeMethods.HttpApi.HTTP_RESPONSE* pHttpResponse, void* pCachePolicy, uint* pBytesSent, SafeLocalFree pRequestBuffer, uint RequestBufferLength, NativeOverlapped* pOverlapped, void* pLogData);

			// Token: 0x060022F8 RID: 8952
			[DllImport("httpapi.dll", CallingConvention = CallingConvention.StdCall, ExactSpelling = true, SetLastError = true)]
			internal unsafe static extern uint HttpSendResponseEntityBody(SafeCloseHandle RequestQueueHandle, ulong RequestId, uint Flags, ushort EntityChunkCount, UnsafeNclNativeMethods.HttpApi.HTTP_DATA_CHUNK* pEntityChunks, uint* pBytesSent, SafeLocalFree pRequestBuffer, uint RequestBufferLength, NativeOverlapped* pOverlapped, void* pLogData);

			// Token: 0x060022F9 RID: 8953
			[DllImport("httpapi.dll", CallingConvention = CallingConvention.StdCall, ExactSpelling = true, SetLastError = true)]
			internal unsafe static extern uint HttpWaitForDisconnect(SafeCloseHandle RequestQueueHandle, ulong ConnectionId, NativeOverlapped* pOverlapped);

			// Token: 0x17000752 RID: 1874
			// (get) Token: 0x060022FA RID: 8954 RVA: 0x000889E1 File Offset: 0x000879E1
			internal static bool ExtendedProtectionSupported
			{
				get
				{
					return UnsafeNclNativeMethods.HttpApi.extendedProtectionSupported;
				}
			}

			// Token: 0x060022FB RID: 8955 RVA: 0x000889E8 File Offset: 0x000879E8
			static HttpApi()
			{
				SafeLoadLibrary safeLoadLibrary = SafeLoadLibrary.LoadLibraryEx("httpapi.dll");
				if (safeLoadLibrary.IsInvalid)
				{
					return;
				}
				try
				{
					UnsafeNclNativeMethods.HttpApi.HTTPAPI_VERSION version = default(UnsafeNclNativeMethods.HttpApi.HTTPAPI_VERSION);
					version.HttpApiMajorVersion = 1;
					version.HttpApiMinorVersion = 0;
					UnsafeNclNativeMethods.HttpApi.extendedProtectionSupported = true;
					uint num;
					if (ComNetOS.IsWin7)
					{
						num = UnsafeNclNativeMethods.HttpApi.HttpInitialize(version, 1U, null);
					}
					else
					{
						num = UnsafeNclNativeMethods.HttpApi.HttpInitialize(version, 5U, null);
						if (num == 87U)
						{
							if (Logging.On)
							{
								Logging.PrintWarning(Logging.HttpListener, SR.GetString("net_listener_cbt_not_supported"));
							}
							UnsafeNclNativeMethods.HttpApi.extendedProtectionSupported = false;
							num = UnsafeNclNativeMethods.HttpApi.HttpInitialize(version, 1U, null);
						}
					}
					UnsafeNclNativeMethods.HttpApi.supported = (num == 0U);
				}
				finally
				{
					safeLoadLibrary.Close();
				}
			}

			// Token: 0x17000753 RID: 1875
			// (get) Token: 0x060022FC RID: 8956 RVA: 0x00088B4C File Offset: 0x00087B4C
			internal static bool Supported
			{
				get
				{
					return UnsafeNclNativeMethods.HttpApi.supported;
				}
			}

			// Token: 0x060022FD RID: 8957 RVA: 0x00088B54 File Offset: 0x00087B54
			internal unsafe static WebHeaderCollection GetHeaders(byte[] memoryBlob, IntPtr originalAddress)
			{
				WebHeaderCollection webHeaderCollection = new WebHeaderCollection(WebHeaderCollectionType.HttpListenerRequest);
				fixed (byte* ptr = memoryBlob)
				{
					UnsafeNclNativeMethods.HttpApi.HTTP_REQUEST* ptr2 = (UnsafeNclNativeMethods.HttpApi.HTTP_REQUEST*)ptr;
					long num = (long)((byte*)ptr - (byte*)((void*)originalAddress));
					if (ptr2->Headers.UnknownHeaderCount != 0)
					{
						UnsafeNclNativeMethods.HttpApi.HTTP_UNKNOWN_HEADER* ptr3 = num + ptr2->Headers.pUnknownHeaders / sizeof(UnsafeNclNativeMethods.HttpApi.HTTP_UNKNOWN_HEADER);
						for (int i = 0; i < (int)ptr2->Headers.UnknownHeaderCount; i++)
						{
							if (ptr3->pName != null && ptr3->NameLength > 0 && ptr3->pRawValue != null && ptr3->RawValueLength > 0)
							{
								string name = new string(ptr3->pName + num, 0, (int)ptr3->NameLength);
								string value = new string(ptr3->pRawValue + num, 0, (int)ptr3->RawValueLength);
								webHeaderCollection.AddInternal(name, value);
							}
							ptr3++;
						}
					}
					UnsafeNclNativeMethods.HttpApi.HTTP_KNOWN_HEADER* ptr4 = &ptr2->Headers.KnownHeaders;
					for (int i = 0; i < 40; i++)
					{
						if (ptr4->RawValueLength != 0 && ptr4->pRawValue != null)
						{
							string value2 = new string(ptr4->pRawValue + num, 0, (int)ptr4->RawValueLength);
							webHeaderCollection.AddInternal(UnsafeNclNativeMethods.HttpApi.HTTP_REQUEST_HEADER_ID.ToString(i), value2);
						}
						ptr4++;
					}
				}
				return webHeaderCollection;
			}

			// Token: 0x060022FE RID: 8958 RVA: 0x00088CAC File Offset: 0x00087CAC
			private unsafe static string GetKnownHeader(UnsafeNclNativeMethods.HttpApi.HTTP_REQUEST* request, long fixup, int headerIndex)
			{
				string result = null;
				UnsafeNclNativeMethods.HttpApi.HTTP_KNOWN_HEADER* ptr = &request->Headers.KnownHeaders + headerIndex;
				if (ptr->RawValueLength != 0 && ptr->pRawValue != null)
				{
					result = new string(ptr->pRawValue + fixup, 0, (int)ptr->RawValueLength);
				}
				return result;
			}

			// Token: 0x060022FF RID: 8959 RVA: 0x00088CFB File Offset: 0x00087CFB
			internal unsafe static string GetKnownHeader(UnsafeNclNativeMethods.HttpApi.HTTP_REQUEST* request, int headerIndex)
			{
				return UnsafeNclNativeMethods.HttpApi.GetKnownHeader(request, 0L, headerIndex);
			}

			// Token: 0x06002300 RID: 8960 RVA: 0x00088D08 File Offset: 0x00087D08
			internal unsafe static string GetKnownHeader(byte[] memoryBlob, IntPtr originalAddress, int headerIndex)
			{
				fixed (byte* ptr = memoryBlob)
				{
					return UnsafeNclNativeMethods.HttpApi.GetKnownHeader((UnsafeNclNativeMethods.HttpApi.HTTP_REQUEST*)ptr, (long)((byte*)ptr - (byte*)((void*)originalAddress)), headerIndex);
				}
			}

			// Token: 0x06002301 RID: 8961 RVA: 0x00088D44 File Offset: 0x00087D44
			private unsafe static string GetVerb(UnsafeNclNativeMethods.HttpApi.HTTP_REQUEST* request, long fixup)
			{
				string result = null;
				if (request->Verb > UnsafeNclNativeMethods.HttpApi.HTTP_VERB.HttpVerbUnknown && request->Verb < UnsafeNclNativeMethods.HttpApi.HTTP_VERB.HttpVerbMaximum)
				{
					result = UnsafeNclNativeMethods.HttpApi.HttpVerbs[(int)request->Verb];
				}
				else if (request->Verb == UnsafeNclNativeMethods.HttpApi.HTTP_VERB.HttpVerbUnknown && request->pUnknownVerb != null)
				{
					result = new string(request->pUnknownVerb + fixup, 0, (int)request->UnknownVerbLength);
				}
				return result;
			}

			// Token: 0x06002302 RID: 8962 RVA: 0x00088D9F File Offset: 0x00087D9F
			internal unsafe static string GetVerb(UnsafeNclNativeMethods.HttpApi.HTTP_REQUEST* request)
			{
				return UnsafeNclNativeMethods.HttpApi.GetVerb(request, 0L);
			}

			// Token: 0x06002303 RID: 8963 RVA: 0x00088DAC File Offset: 0x00087DAC
			internal unsafe static string GetVerb(byte[] memoryBlob, IntPtr originalAddress)
			{
				fixed (byte* ptr = memoryBlob)
				{
					return UnsafeNclNativeMethods.HttpApi.GetVerb((UnsafeNclNativeMethods.HttpApi.HTTP_REQUEST*)ptr, (long)((byte*)ptr - (byte*)((void*)originalAddress)));
				}
			}

			// Token: 0x06002304 RID: 8964 RVA: 0x00088DE8 File Offset: 0x00087DE8
			internal unsafe static UnsafeNclNativeMethods.HttpApi.HTTP_VERB GetKnownVerb(byte[] memoryBlob, IntPtr originalAddress)
			{
				UnsafeNclNativeMethods.HttpApi.HTTP_VERB result = UnsafeNclNativeMethods.HttpApi.HTTP_VERB.HttpVerbUnknown;
				fixed (byte* ptr = memoryBlob)
				{
					UnsafeNclNativeMethods.HttpApi.HTTP_REQUEST* ptr2 = (UnsafeNclNativeMethods.HttpApi.HTTP_REQUEST*)ptr;
					if (ptr2->Verb > UnsafeNclNativeMethods.HttpApi.HTTP_VERB.HttpVerbUnparsed && ptr2->Verb < UnsafeNclNativeMethods.HttpApi.HTTP_VERB.HttpVerbMaximum)
					{
						result = ptr2->Verb;
					}
				}
				return result;
			}

			// Token: 0x06002305 RID: 8965 RVA: 0x00088E30 File Offset: 0x00087E30
			internal unsafe static uint GetChunks(byte[] memoryBlob, IntPtr originalAddress, ref int dataChunkIndex, ref uint dataChunkOffset, byte[] buffer, int offset, int size)
			{
				uint num = 0U;
				fixed (byte* ptr = memoryBlob)
				{
					UnsafeNclNativeMethods.HttpApi.HTTP_REQUEST* ptr2 = (UnsafeNclNativeMethods.HttpApi.HTTP_REQUEST*)ptr;
					long num2 = (long)((byte*)ptr - (byte*)((void*)originalAddress));
					if (ptr2->EntityChunkCount > 0 && dataChunkIndex < (int)ptr2->EntityChunkCount && dataChunkIndex != -1)
					{
						UnsafeNclNativeMethods.HttpApi.HTTP_DATA_CHUNK* ptr3 = num2 + (ptr2->pEntityChunks + dataChunkIndex) / sizeof(UnsafeNclNativeMethods.HttpApi.HTTP_DATA_CHUNK);
						fixed (byte* ptr4 = buffer)
						{
							byte* ptr5 = ptr4 + offset;
							while (dataChunkIndex < (int)ptr2->EntityChunkCount && (ulong)num < (ulong)((long)size))
							{
								if (dataChunkOffset >= ptr3->BufferLength)
								{
									dataChunkOffset = 0U;
									dataChunkIndex++;
									ptr3++;
								}
								else
								{
									byte* ptr6 = ptr3->pBuffer + dataChunkOffset + num2;
									uint num3 = ptr3->BufferLength - dataChunkOffset;
									if (num3 > (uint)size)
									{
										num3 = (uint)size;
									}
									for (uint num4 = 0U; num4 < num3; num4 += 1U)
									{
										*(ptr5++) = *(ptr6++);
									}
									num += num3;
									dataChunkOffset += num3;
								}
							}
						}
					}
					if (dataChunkIndex == (int)ptr2->EntityChunkCount)
					{
						dataChunkIndex = -1;
					}
				}
				return num;
			}

			// Token: 0x06002306 RID: 8966 RVA: 0x00088F68 File Offset: 0x00087F68
			internal unsafe static IPEndPoint GetRemoteEndPoint(byte[] memoryBlob, IntPtr originalAddress)
			{
				SocketAddress socketAddress = new SocketAddress(AddressFamily.InterNetwork, 16);
				SocketAddress socketAddress2 = new SocketAddress(AddressFamily.InterNetworkV6, 28);
				fixed (byte* ptr = memoryBlob)
				{
					UnsafeNclNativeMethods.HttpApi.HTTP_REQUEST* ptr2 = (UnsafeNclNativeMethods.HttpApi.HTTP_REQUEST*)ptr;
					IntPtr address = (ptr2->Address.pRemoteAddress != null) ? ((IntPtr)((void*)((byte*)((IntPtr)((long)((byte*)ptr - (byte*)((void*)originalAddress)))) + ptr2->Address.pRemoteAddress))) : IntPtr.Zero;
					UnsafeNclNativeMethods.HttpApi.CopyOutAddress(address, ref socketAddress, ref socketAddress2);
				}
				IPEndPoint result = null;
				if (socketAddress != null)
				{
					result = (IPEndPoint.Any.Create(socketAddress) as IPEndPoint);
				}
				else if (socketAddress2 != null)
				{
					result = (IPEndPoint.IPv6Any.Create(socketAddress2) as IPEndPoint);
				}
				return result;
			}

			// Token: 0x06002307 RID: 8967 RVA: 0x0008901C File Offset: 0x0008801C
			internal unsafe static IPEndPoint GetLocalEndPoint(byte[] memoryBlob, IntPtr originalAddress)
			{
				SocketAddress socketAddress = new SocketAddress(AddressFamily.InterNetwork, 16);
				SocketAddress socketAddress2 = new SocketAddress(AddressFamily.InterNetworkV6, 28);
				fixed (byte* ptr = memoryBlob)
				{
					UnsafeNclNativeMethods.HttpApi.HTTP_REQUEST* ptr2 = (UnsafeNclNativeMethods.HttpApi.HTTP_REQUEST*)ptr;
					IntPtr address = (ptr2->Address.pLocalAddress != null) ? ((IntPtr)((void*)((byte*)((IntPtr)((long)((byte*)ptr - (byte*)((void*)originalAddress)))) + ptr2->Address.pLocalAddress))) : IntPtr.Zero;
					UnsafeNclNativeMethods.HttpApi.CopyOutAddress(address, ref socketAddress, ref socketAddress2);
				}
				IPEndPoint result = null;
				if (socketAddress != null)
				{
					result = (IPEndPoint.Any.Create(socketAddress) as IPEndPoint);
				}
				else if (socketAddress2 != null)
				{
					result = (IPEndPoint.IPv6Any.Create(socketAddress2) as IPEndPoint);
				}
				return result;
			}

			// Token: 0x06002308 RID: 8968 RVA: 0x000890D0 File Offset: 0x000880D0
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
			private unsafe static void CopyOutAddress(IntPtr address, ref SocketAddress v4address, ref SocketAddress v6address)
			{
				if (address != IntPtr.Zero)
				{
					ushort num = *(ushort*)((void*)address);
					if (num == 2)
					{
						v6address = null;
						fixed (byte* buffer = v4address.m_Buffer)
						{
							for (int i = 2; i < 16; i++)
							{
								buffer[i] = ((byte*)((void*)address))[i];
							}
						}
						return;
					}
					if (num == 23)
					{
						v4address = null;
						fixed (byte* buffer2 = v6address.m_Buffer)
						{
							for (int j = 2; j < 28; j++)
							{
								buffer2[j] = ((byte*)((void*)address))[j];
							}
						}
						return;
					}
				}
				v4address = null;
				v6address = null;
			}

			// Token: 0x04002323 RID: 8995
			private const string HTTPAPI = "httpapi.dll";

			// Token: 0x04002324 RID: 8996
			private const int HttpHeaderRequestMaximum = 41;

			// Token: 0x04002325 RID: 8997
			private const int HttpHeaderResponseMaximum = 30;

			// Token: 0x04002326 RID: 8998
			internal static readonly string[] HttpVerbs = new string[]
			{
				null,
				"Unknown",
				"Invalid",
				"OPTIONS",
				"GET",
				"HEAD",
				"POST",
				"PUT",
				"DELETE",
				"TRACE",
				"CONNECT",
				"TRACK",
				"MOVE",
				"COPY",
				"PROPFIND",
				"PROPPATCH",
				"MKCOL",
				"LOCK",
				"UNLOCK",
				"SEARCH"
			};

			// Token: 0x04002327 RID: 8999
			private static bool extendedProtectionSupported;

			// Token: 0x04002328 RID: 9000
			private static bool supported;

			// Token: 0x0200046D RID: 1133
			internal struct HTTP_VERSION
			{
				// Token: 0x04002329 RID: 9001
				internal ushort MajorVersion;

				// Token: 0x0400232A RID: 9002
				internal ushort MinorVersion;
			}

			// Token: 0x0200046E RID: 1134
			internal struct HTTP_KNOWN_HEADER
			{
				// Token: 0x0400232B RID: 9003
				internal ushort RawValueLength;

				// Token: 0x0400232C RID: 9004
				internal unsafe sbyte* pRawValue;
			}

			// Token: 0x0200046F RID: 1135
			[StructLayout(LayoutKind.Sequential, Size = 32)]
			internal struct HTTP_DATA_CHUNK
			{
				// Token: 0x0400232D RID: 9005
				internal UnsafeNclNativeMethods.HttpApi.HTTP_DATA_CHUNK_TYPE DataChunkType;

				// Token: 0x0400232E RID: 9006
				internal uint p0;

				// Token: 0x0400232F RID: 9007
				internal unsafe byte* pBuffer;

				// Token: 0x04002330 RID: 9008
				internal uint BufferLength;
			}

			// Token: 0x02000470 RID: 1136
			internal struct HTTPAPI_VERSION
			{
				// Token: 0x04002331 RID: 9009
				internal ushort HttpApiMajorVersion;

				// Token: 0x04002332 RID: 9010
				internal ushort HttpApiMinorVersion;
			}

			// Token: 0x02000471 RID: 1137
			internal struct HTTP_COOKED_URL
			{
				// Token: 0x04002333 RID: 9011
				internal ushort FullUrlLength;

				// Token: 0x04002334 RID: 9012
				internal ushort HostLength;

				// Token: 0x04002335 RID: 9013
				internal ushort AbsPathLength;

				// Token: 0x04002336 RID: 9014
				internal ushort QueryStringLength;

				// Token: 0x04002337 RID: 9015
				internal unsafe ushort* pFullUrl;

				// Token: 0x04002338 RID: 9016
				internal unsafe ushort* pHost;

				// Token: 0x04002339 RID: 9017
				internal unsafe ushort* pAbsPath;

				// Token: 0x0400233A RID: 9018
				internal unsafe ushort* pQueryString;
			}

			// Token: 0x02000472 RID: 1138
			internal struct SOCKADDR
			{
				// Token: 0x0400233B RID: 9019
				internal ushort sa_family;

				// Token: 0x0400233C RID: 9020
				internal byte sa_data;

				// Token: 0x0400233D RID: 9021
				internal byte sa_data_02;

				// Token: 0x0400233E RID: 9022
				internal byte sa_data_03;

				// Token: 0x0400233F RID: 9023
				internal byte sa_data_04;

				// Token: 0x04002340 RID: 9024
				internal byte sa_data_05;

				// Token: 0x04002341 RID: 9025
				internal byte sa_data_06;

				// Token: 0x04002342 RID: 9026
				internal byte sa_data_07;

				// Token: 0x04002343 RID: 9027
				internal byte sa_data_08;

				// Token: 0x04002344 RID: 9028
				internal byte sa_data_09;

				// Token: 0x04002345 RID: 9029
				internal byte sa_data_10;

				// Token: 0x04002346 RID: 9030
				internal byte sa_data_11;

				// Token: 0x04002347 RID: 9031
				internal byte sa_data_12;

				// Token: 0x04002348 RID: 9032
				internal byte sa_data_13;

				// Token: 0x04002349 RID: 9033
				internal byte sa_data_14;
			}

			// Token: 0x02000473 RID: 1139
			internal struct HTTP_TRANSPORT_ADDRESS
			{
				// Token: 0x0400234A RID: 9034
				internal unsafe UnsafeNclNativeMethods.HttpApi.SOCKADDR* pRemoteAddress;

				// Token: 0x0400234B RID: 9035
				internal unsafe UnsafeNclNativeMethods.HttpApi.SOCKADDR* pLocalAddress;
			}

			// Token: 0x02000474 RID: 1140
			internal struct HTTP_SSL_CLIENT_CERT_INFO
			{
				// Token: 0x0400234C RID: 9036
				internal uint CertFlags;

				// Token: 0x0400234D RID: 9037
				internal uint CertEncodedSize;

				// Token: 0x0400234E RID: 9038
				internal unsafe byte* pCertEncoded;

				// Token: 0x0400234F RID: 9039
				internal unsafe void* Token;

				// Token: 0x04002350 RID: 9040
				internal byte CertDeniedByMapper;
			}

			// Token: 0x02000475 RID: 1141
			internal enum HTTP_SERVICE_BINDING_TYPE : uint
			{
				// Token: 0x04002352 RID: 9042
				HttpServiceBindingTypeNone,
				// Token: 0x04002353 RID: 9043
				HttpServiceBindingTypeW,
				// Token: 0x04002354 RID: 9044
				HttpServiceBindingTypeA
			}

			// Token: 0x02000476 RID: 1142
			internal struct HTTP_SERVICE_BINDING_BASE
			{
				// Token: 0x04002355 RID: 9045
				internal UnsafeNclNativeMethods.HttpApi.HTTP_SERVICE_BINDING_TYPE Type;
			}

			// Token: 0x02000477 RID: 1143
			internal struct HTTP_REQUEST_CHANNEL_BIND_STATUS
			{
				// Token: 0x04002356 RID: 9046
				internal IntPtr ServiceName;

				// Token: 0x04002357 RID: 9047
				internal IntPtr ChannelToken;

				// Token: 0x04002358 RID: 9048
				internal uint ChannelTokenSize;

				// Token: 0x04002359 RID: 9049
				internal uint Flags;
			}

			// Token: 0x02000478 RID: 1144
			internal struct HTTP_UNKNOWN_HEADER
			{
				// Token: 0x0400235A RID: 9050
				internal ushort NameLength;

				// Token: 0x0400235B RID: 9051
				internal ushort RawValueLength;

				// Token: 0x0400235C RID: 9052
				internal unsafe sbyte* pName;

				// Token: 0x0400235D RID: 9053
				internal unsafe sbyte* pRawValue;
			}

			// Token: 0x02000479 RID: 1145
			internal struct HTTP_SSL_INFO
			{
				// Token: 0x0400235E RID: 9054
				internal ushort ServerCertKeySize;

				// Token: 0x0400235F RID: 9055
				internal ushort ConnectionKeySize;

				// Token: 0x04002360 RID: 9056
				internal uint ServerCertIssuerSize;

				// Token: 0x04002361 RID: 9057
				internal uint ServerCertSubjectSize;

				// Token: 0x04002362 RID: 9058
				internal unsafe sbyte* pServerCertIssuer;

				// Token: 0x04002363 RID: 9059
				internal unsafe sbyte* pServerCertSubject;

				// Token: 0x04002364 RID: 9060
				internal unsafe UnsafeNclNativeMethods.HttpApi.HTTP_SSL_CLIENT_CERT_INFO* pClientCertInfo;

				// Token: 0x04002365 RID: 9061
				internal uint SslClientCertNegotiated;
			}

			// Token: 0x0200047A RID: 1146
			internal struct HTTP_RESPONSE_HEADERS
			{
				// Token: 0x04002366 RID: 9062
				internal ushort UnknownHeaderCount;

				// Token: 0x04002367 RID: 9063
				internal unsafe UnsafeNclNativeMethods.HttpApi.HTTP_UNKNOWN_HEADER* pUnknownHeaders;

				// Token: 0x04002368 RID: 9064
				internal ushort TrailerCount;

				// Token: 0x04002369 RID: 9065
				internal unsafe UnsafeNclNativeMethods.HttpApi.HTTP_UNKNOWN_HEADER* pTrailers;

				// Token: 0x0400236A RID: 9066
				internal UnsafeNclNativeMethods.HttpApi.HTTP_KNOWN_HEADER KnownHeaders;

				// Token: 0x0400236B RID: 9067
				internal UnsafeNclNativeMethods.HttpApi.HTTP_KNOWN_HEADER KnownHeaders_02;

				// Token: 0x0400236C RID: 9068
				internal UnsafeNclNativeMethods.HttpApi.HTTP_KNOWN_HEADER KnownHeaders_03;

				// Token: 0x0400236D RID: 9069
				internal UnsafeNclNativeMethods.HttpApi.HTTP_KNOWN_HEADER KnownHeaders_04;

				// Token: 0x0400236E RID: 9070
				internal UnsafeNclNativeMethods.HttpApi.HTTP_KNOWN_HEADER KnownHeaders_05;

				// Token: 0x0400236F RID: 9071
				internal UnsafeNclNativeMethods.HttpApi.HTTP_KNOWN_HEADER KnownHeaders_06;

				// Token: 0x04002370 RID: 9072
				internal UnsafeNclNativeMethods.HttpApi.HTTP_KNOWN_HEADER KnownHeaders_07;

				// Token: 0x04002371 RID: 9073
				internal UnsafeNclNativeMethods.HttpApi.HTTP_KNOWN_HEADER KnownHeaders_08;

				// Token: 0x04002372 RID: 9074
				internal UnsafeNclNativeMethods.HttpApi.HTTP_KNOWN_HEADER KnownHeaders_09;

				// Token: 0x04002373 RID: 9075
				internal UnsafeNclNativeMethods.HttpApi.HTTP_KNOWN_HEADER KnownHeaders_10;

				// Token: 0x04002374 RID: 9076
				internal UnsafeNclNativeMethods.HttpApi.HTTP_KNOWN_HEADER KnownHeaders_11;

				// Token: 0x04002375 RID: 9077
				internal UnsafeNclNativeMethods.HttpApi.HTTP_KNOWN_HEADER KnownHeaders_12;

				// Token: 0x04002376 RID: 9078
				internal UnsafeNclNativeMethods.HttpApi.HTTP_KNOWN_HEADER KnownHeaders_13;

				// Token: 0x04002377 RID: 9079
				internal UnsafeNclNativeMethods.HttpApi.HTTP_KNOWN_HEADER KnownHeaders_14;

				// Token: 0x04002378 RID: 9080
				internal UnsafeNclNativeMethods.HttpApi.HTTP_KNOWN_HEADER KnownHeaders_15;

				// Token: 0x04002379 RID: 9081
				internal UnsafeNclNativeMethods.HttpApi.HTTP_KNOWN_HEADER KnownHeaders_16;

				// Token: 0x0400237A RID: 9082
				internal UnsafeNclNativeMethods.HttpApi.HTTP_KNOWN_HEADER KnownHeaders_17;

				// Token: 0x0400237B RID: 9083
				internal UnsafeNclNativeMethods.HttpApi.HTTP_KNOWN_HEADER KnownHeaders_18;

				// Token: 0x0400237C RID: 9084
				internal UnsafeNclNativeMethods.HttpApi.HTTP_KNOWN_HEADER KnownHeaders_19;

				// Token: 0x0400237D RID: 9085
				internal UnsafeNclNativeMethods.HttpApi.HTTP_KNOWN_HEADER KnownHeaders_20;

				// Token: 0x0400237E RID: 9086
				internal UnsafeNclNativeMethods.HttpApi.HTTP_KNOWN_HEADER KnownHeaders_21;

				// Token: 0x0400237F RID: 9087
				internal UnsafeNclNativeMethods.HttpApi.HTTP_KNOWN_HEADER KnownHeaders_22;

				// Token: 0x04002380 RID: 9088
				internal UnsafeNclNativeMethods.HttpApi.HTTP_KNOWN_HEADER KnownHeaders_23;

				// Token: 0x04002381 RID: 9089
				internal UnsafeNclNativeMethods.HttpApi.HTTP_KNOWN_HEADER KnownHeaders_24;

				// Token: 0x04002382 RID: 9090
				internal UnsafeNclNativeMethods.HttpApi.HTTP_KNOWN_HEADER KnownHeaders_25;

				// Token: 0x04002383 RID: 9091
				internal UnsafeNclNativeMethods.HttpApi.HTTP_KNOWN_HEADER KnownHeaders_26;

				// Token: 0x04002384 RID: 9092
				internal UnsafeNclNativeMethods.HttpApi.HTTP_KNOWN_HEADER KnownHeaders_27;

				// Token: 0x04002385 RID: 9093
				internal UnsafeNclNativeMethods.HttpApi.HTTP_KNOWN_HEADER KnownHeaders_28;

				// Token: 0x04002386 RID: 9094
				internal UnsafeNclNativeMethods.HttpApi.HTTP_KNOWN_HEADER KnownHeaders_29;

				// Token: 0x04002387 RID: 9095
				internal UnsafeNclNativeMethods.HttpApi.HTTP_KNOWN_HEADER KnownHeaders_30;
			}

			// Token: 0x0200047B RID: 1147
			internal struct HTTP_REQUEST_HEADERS
			{
				// Token: 0x04002388 RID: 9096
				internal ushort UnknownHeaderCount;

				// Token: 0x04002389 RID: 9097
				internal unsafe UnsafeNclNativeMethods.HttpApi.HTTP_UNKNOWN_HEADER* pUnknownHeaders;

				// Token: 0x0400238A RID: 9098
				internal ushort TrailerCount;

				// Token: 0x0400238B RID: 9099
				internal unsafe UnsafeNclNativeMethods.HttpApi.HTTP_UNKNOWN_HEADER* pTrailers;

				// Token: 0x0400238C RID: 9100
				internal UnsafeNclNativeMethods.HttpApi.HTTP_KNOWN_HEADER KnownHeaders;

				// Token: 0x0400238D RID: 9101
				internal UnsafeNclNativeMethods.HttpApi.HTTP_KNOWN_HEADER KnownHeaders_02;

				// Token: 0x0400238E RID: 9102
				internal UnsafeNclNativeMethods.HttpApi.HTTP_KNOWN_HEADER KnownHeaders_03;

				// Token: 0x0400238F RID: 9103
				internal UnsafeNclNativeMethods.HttpApi.HTTP_KNOWN_HEADER KnownHeaders_04;

				// Token: 0x04002390 RID: 9104
				internal UnsafeNclNativeMethods.HttpApi.HTTP_KNOWN_HEADER KnownHeaders_05;

				// Token: 0x04002391 RID: 9105
				internal UnsafeNclNativeMethods.HttpApi.HTTP_KNOWN_HEADER KnownHeaders_06;

				// Token: 0x04002392 RID: 9106
				internal UnsafeNclNativeMethods.HttpApi.HTTP_KNOWN_HEADER KnownHeaders_07;

				// Token: 0x04002393 RID: 9107
				internal UnsafeNclNativeMethods.HttpApi.HTTP_KNOWN_HEADER KnownHeaders_08;

				// Token: 0x04002394 RID: 9108
				internal UnsafeNclNativeMethods.HttpApi.HTTP_KNOWN_HEADER KnownHeaders_09;

				// Token: 0x04002395 RID: 9109
				internal UnsafeNclNativeMethods.HttpApi.HTTP_KNOWN_HEADER KnownHeaders_10;

				// Token: 0x04002396 RID: 9110
				internal UnsafeNclNativeMethods.HttpApi.HTTP_KNOWN_HEADER KnownHeaders_11;

				// Token: 0x04002397 RID: 9111
				internal UnsafeNclNativeMethods.HttpApi.HTTP_KNOWN_HEADER KnownHeaders_12;

				// Token: 0x04002398 RID: 9112
				internal UnsafeNclNativeMethods.HttpApi.HTTP_KNOWN_HEADER KnownHeaders_13;

				// Token: 0x04002399 RID: 9113
				internal UnsafeNclNativeMethods.HttpApi.HTTP_KNOWN_HEADER KnownHeaders_14;

				// Token: 0x0400239A RID: 9114
				internal UnsafeNclNativeMethods.HttpApi.HTTP_KNOWN_HEADER KnownHeaders_15;

				// Token: 0x0400239B RID: 9115
				internal UnsafeNclNativeMethods.HttpApi.HTTP_KNOWN_HEADER KnownHeaders_16;

				// Token: 0x0400239C RID: 9116
				internal UnsafeNclNativeMethods.HttpApi.HTTP_KNOWN_HEADER KnownHeaders_17;

				// Token: 0x0400239D RID: 9117
				internal UnsafeNclNativeMethods.HttpApi.HTTP_KNOWN_HEADER KnownHeaders_18;

				// Token: 0x0400239E RID: 9118
				internal UnsafeNclNativeMethods.HttpApi.HTTP_KNOWN_HEADER KnownHeaders_19;

				// Token: 0x0400239F RID: 9119
				internal UnsafeNclNativeMethods.HttpApi.HTTP_KNOWN_HEADER KnownHeaders_20;

				// Token: 0x040023A0 RID: 9120
				internal UnsafeNclNativeMethods.HttpApi.HTTP_KNOWN_HEADER KnownHeaders_21;

				// Token: 0x040023A1 RID: 9121
				internal UnsafeNclNativeMethods.HttpApi.HTTP_KNOWN_HEADER KnownHeaders_22;

				// Token: 0x040023A2 RID: 9122
				internal UnsafeNclNativeMethods.HttpApi.HTTP_KNOWN_HEADER KnownHeaders_23;

				// Token: 0x040023A3 RID: 9123
				internal UnsafeNclNativeMethods.HttpApi.HTTP_KNOWN_HEADER KnownHeaders_24;

				// Token: 0x040023A4 RID: 9124
				internal UnsafeNclNativeMethods.HttpApi.HTTP_KNOWN_HEADER KnownHeaders_25;

				// Token: 0x040023A5 RID: 9125
				internal UnsafeNclNativeMethods.HttpApi.HTTP_KNOWN_HEADER KnownHeaders_26;

				// Token: 0x040023A6 RID: 9126
				internal UnsafeNclNativeMethods.HttpApi.HTTP_KNOWN_HEADER KnownHeaders_27;

				// Token: 0x040023A7 RID: 9127
				internal UnsafeNclNativeMethods.HttpApi.HTTP_KNOWN_HEADER KnownHeaders_28;

				// Token: 0x040023A8 RID: 9128
				internal UnsafeNclNativeMethods.HttpApi.HTTP_KNOWN_HEADER KnownHeaders_29;

				// Token: 0x040023A9 RID: 9129
				internal UnsafeNclNativeMethods.HttpApi.HTTP_KNOWN_HEADER KnownHeaders_30;

				// Token: 0x040023AA RID: 9130
				internal UnsafeNclNativeMethods.HttpApi.HTTP_KNOWN_HEADER KnownHeaders_31;

				// Token: 0x040023AB RID: 9131
				internal UnsafeNclNativeMethods.HttpApi.HTTP_KNOWN_HEADER KnownHeaders_32;

				// Token: 0x040023AC RID: 9132
				internal UnsafeNclNativeMethods.HttpApi.HTTP_KNOWN_HEADER KnownHeaders_33;

				// Token: 0x040023AD RID: 9133
				internal UnsafeNclNativeMethods.HttpApi.HTTP_KNOWN_HEADER KnownHeaders_34;

				// Token: 0x040023AE RID: 9134
				internal UnsafeNclNativeMethods.HttpApi.HTTP_KNOWN_HEADER KnownHeaders_35;

				// Token: 0x040023AF RID: 9135
				internal UnsafeNclNativeMethods.HttpApi.HTTP_KNOWN_HEADER KnownHeaders_36;

				// Token: 0x040023B0 RID: 9136
				internal UnsafeNclNativeMethods.HttpApi.HTTP_KNOWN_HEADER KnownHeaders_37;

				// Token: 0x040023B1 RID: 9137
				internal UnsafeNclNativeMethods.HttpApi.HTTP_KNOWN_HEADER KnownHeaders_38;

				// Token: 0x040023B2 RID: 9138
				internal UnsafeNclNativeMethods.HttpApi.HTTP_KNOWN_HEADER KnownHeaders_39;

				// Token: 0x040023B3 RID: 9139
				internal UnsafeNclNativeMethods.HttpApi.HTTP_KNOWN_HEADER KnownHeaders_40;

				// Token: 0x040023B4 RID: 9140
				internal UnsafeNclNativeMethods.HttpApi.HTTP_KNOWN_HEADER KnownHeaders_41;
			}

			// Token: 0x0200047C RID: 1148
			internal enum HTTP_VERB
			{
				// Token: 0x040023B6 RID: 9142
				HttpVerbUnparsed,
				// Token: 0x040023B7 RID: 9143
				HttpVerbUnknown,
				// Token: 0x040023B8 RID: 9144
				HttpVerbInvalid,
				// Token: 0x040023B9 RID: 9145
				HttpVerbOPTIONS,
				// Token: 0x040023BA RID: 9146
				HttpVerbGET,
				// Token: 0x040023BB RID: 9147
				HttpVerbHEAD,
				// Token: 0x040023BC RID: 9148
				HttpVerbPOST,
				// Token: 0x040023BD RID: 9149
				HttpVerbPUT,
				// Token: 0x040023BE RID: 9150
				HttpVerbDELETE,
				// Token: 0x040023BF RID: 9151
				HttpVerbTRACE,
				// Token: 0x040023C0 RID: 9152
				HttpVerbCONNECT,
				// Token: 0x040023C1 RID: 9153
				HttpVerbTRACK,
				// Token: 0x040023C2 RID: 9154
				HttpVerbMOVE,
				// Token: 0x040023C3 RID: 9155
				HttpVerbCOPY,
				// Token: 0x040023C4 RID: 9156
				HttpVerbPROPFIND,
				// Token: 0x040023C5 RID: 9157
				HttpVerbPROPPATCH,
				// Token: 0x040023C6 RID: 9158
				HttpVerbMKCOL,
				// Token: 0x040023C7 RID: 9159
				HttpVerbLOCK,
				// Token: 0x040023C8 RID: 9160
				HttpVerbUNLOCK,
				// Token: 0x040023C9 RID: 9161
				HttpVerbSEARCH,
				// Token: 0x040023CA RID: 9162
				HttpVerbMaximum
			}

			// Token: 0x0200047D RID: 1149
			internal enum HTTP_DATA_CHUNK_TYPE
			{
				// Token: 0x040023CC RID: 9164
				HttpDataChunkFromMemory,
				// Token: 0x040023CD RID: 9165
				HttpDataChunkFromFileHandle,
				// Token: 0x040023CE RID: 9166
				HttpDataChunkFromFragmentCache,
				// Token: 0x040023CF RID: 9167
				HttpDataChunkMaximum
			}

			// Token: 0x0200047E RID: 1150
			internal struct HTTP_RESPONSE
			{
				// Token: 0x040023D0 RID: 9168
				internal uint Flags;

				// Token: 0x040023D1 RID: 9169
				internal UnsafeNclNativeMethods.HttpApi.HTTP_VERSION Version;

				// Token: 0x040023D2 RID: 9170
				internal ushort StatusCode;

				// Token: 0x040023D3 RID: 9171
				internal ushort ReasonLength;

				// Token: 0x040023D4 RID: 9172
				internal unsafe sbyte* pReason;

				// Token: 0x040023D5 RID: 9173
				internal UnsafeNclNativeMethods.HttpApi.HTTP_RESPONSE_HEADERS Headers;

				// Token: 0x040023D6 RID: 9174
				internal ushort EntityChunkCount;

				// Token: 0x040023D7 RID: 9175
				internal unsafe UnsafeNclNativeMethods.HttpApi.HTTP_DATA_CHUNK* pEntityChunks;
			}

			// Token: 0x0200047F RID: 1151
			internal struct HTTP_REQUEST
			{
				// Token: 0x040023D8 RID: 9176
				internal uint Flags;

				// Token: 0x040023D9 RID: 9177
				internal ulong ConnectionId;

				// Token: 0x040023DA RID: 9178
				internal ulong RequestId;

				// Token: 0x040023DB RID: 9179
				internal ulong UrlContext;

				// Token: 0x040023DC RID: 9180
				internal UnsafeNclNativeMethods.HttpApi.HTTP_VERSION Version;

				// Token: 0x040023DD RID: 9181
				internal UnsafeNclNativeMethods.HttpApi.HTTP_VERB Verb;

				// Token: 0x040023DE RID: 9182
				internal ushort UnknownVerbLength;

				// Token: 0x040023DF RID: 9183
				internal ushort RawUrlLength;

				// Token: 0x040023E0 RID: 9184
				internal unsafe sbyte* pUnknownVerb;

				// Token: 0x040023E1 RID: 9185
				internal unsafe sbyte* pRawUrl;

				// Token: 0x040023E2 RID: 9186
				internal UnsafeNclNativeMethods.HttpApi.HTTP_COOKED_URL CookedUrl;

				// Token: 0x040023E3 RID: 9187
				internal UnsafeNclNativeMethods.HttpApi.HTTP_TRANSPORT_ADDRESS Address;

				// Token: 0x040023E4 RID: 9188
				internal UnsafeNclNativeMethods.HttpApi.HTTP_REQUEST_HEADERS Headers;

				// Token: 0x040023E5 RID: 9189
				internal ulong BytesReceived;

				// Token: 0x040023E6 RID: 9190
				internal ushort EntityChunkCount;

				// Token: 0x040023E7 RID: 9191
				internal unsafe UnsafeNclNativeMethods.HttpApi.HTTP_DATA_CHUNK* pEntityChunks;

				// Token: 0x040023E8 RID: 9192
				internal ulong RawConnectionId;

				// Token: 0x040023E9 RID: 9193
				internal unsafe UnsafeNclNativeMethods.HttpApi.HTTP_SSL_INFO* pSslInfo;
			}

			// Token: 0x02000480 RID: 1152
			[Flags]
			internal enum HTTP_FLAGS : uint
			{
				// Token: 0x040023EB RID: 9195
				NONE = 0U,
				// Token: 0x040023EC RID: 9196
				HTTP_RECEIVE_REQUEST_FLAG_COPY_BODY = 1U,
				// Token: 0x040023ED RID: 9197
				HTTP_RECEIVE_SECURE_CHANNEL_TOKEN = 1U,
				// Token: 0x040023EE RID: 9198
				HTTP_SEND_RESPONSE_FLAG_DISCONNECT = 1U,
				// Token: 0x040023EF RID: 9199
				HTTP_SEND_RESPONSE_FLAG_MORE_DATA = 2U,
				// Token: 0x040023F0 RID: 9200
				HTTP_SEND_RESPONSE_FLAG_RAW_HEADER = 4U,
				// Token: 0x040023F1 RID: 9201
				HTTP_SEND_REQUEST_FLAG_MORE_DATA = 1U,
				// Token: 0x040023F2 RID: 9202
				HTTP_INITIALIZE_SERVER = 1U,
				// Token: 0x040023F3 RID: 9203
				HTTP_INITIALIZE_CBT = 4U
			}

			// Token: 0x02000481 RID: 1153
			internal static class HTTP_REQUEST_HEADER_ID
			{
				// Token: 0x06002309 RID: 8969 RVA: 0x00089190 File Offset: 0x00088190
				static HTTP_REQUEST_HEADER_ID()
				{
					UnsafeNclNativeMethods.HttpApi.HTTP_REQUEST_HEADER_ID.m_Hashtable = new Hashtable(41);
					for (int i = 0; i < 40; i++)
					{
						UnsafeNclNativeMethods.HttpApi.HTTP_REQUEST_HEADER_ID.m_Hashtable.Add(UnsafeNclNativeMethods.HttpApi.HTTP_REQUEST_HEADER_ID.m_Strings[i], i);
					}
				}

				// Token: 0x0600230A RID: 8970 RVA: 0x00089343 File Offset: 0x00088343
				internal static string ToString(int position)
				{
					return UnsafeNclNativeMethods.HttpApi.HTTP_REQUEST_HEADER_ID.m_Strings[position];
				}

				// Token: 0x040023F4 RID: 9204
				private static Hashtable m_Hashtable;

				// Token: 0x040023F5 RID: 9205
				private static string[] m_Strings = new string[]
				{
					"Cache-Control",
					"Connection",
					"Date",
					"Keep-Alive",
					"Pragma",
					"Trailer",
					"Transfer-Encoding",
					"Upgrade",
					"Via",
					"Warning",
					"Allow",
					"Content-Length",
					"Content-Type",
					"Content-Encoding",
					"Content-Language",
					"Content-Location",
					"Content-MD5",
					"Content-Range",
					"Expires",
					"Last-Modified",
					"Accept",
					"Accept-Charset",
					"Accept-Encoding",
					"Accept-Language",
					"Authorization",
					"Cookie",
					"Expect",
					"From",
					"Host",
					"If-Match",
					"If-Modified-Since",
					"If-None-Match",
					"If-Range",
					"If-Unmodified-Since",
					"Max-Forwards",
					"Proxy-Authorization",
					"Referer",
					"Range",
					"Te",
					"Translate",
					"User-Agent"
				};
			}

			// Token: 0x02000482 RID: 1154
			internal static class HTTP_RESPONSE_HEADER_ID
			{
				// Token: 0x0600230B RID: 8971 RVA: 0x0008934C File Offset: 0x0008834C
				static HTTP_RESPONSE_HEADER_ID()
				{
					UnsafeNclNativeMethods.HttpApi.HTTP_RESPONSE_HEADER_ID.m_Hashtable = new Hashtable(30);
					for (int i = 0; i < 29; i++)
					{
						UnsafeNclNativeMethods.HttpApi.HTTP_RESPONSE_HEADER_ID.m_Hashtable.Add(UnsafeNclNativeMethods.HttpApi.HTTP_RESPONSE_HEADER_ID.m_Strings[i], i);
					}
				}

				// Token: 0x0600230C RID: 8972 RVA: 0x0008949C File Offset: 0x0008849C
				internal static int IndexOfKnownHeader(string HeaderName)
				{
					object obj = UnsafeNclNativeMethods.HttpApi.HTTP_RESPONSE_HEADER_ID.m_Hashtable[HeaderName];
					if (obj != null)
					{
						return (int)obj;
					}
					return -1;
				}

				// Token: 0x0600230D RID: 8973 RVA: 0x000894C0 File Offset: 0x000884C0
				internal static string ToString(int position)
				{
					return UnsafeNclNativeMethods.HttpApi.HTTP_RESPONSE_HEADER_ID.m_Strings[position];
				}

				// Token: 0x040023F6 RID: 9206
				private static Hashtable m_Hashtable;

				// Token: 0x040023F7 RID: 9207
				private static string[] m_Strings = new string[]
				{
					"Cache-Control",
					"Connection",
					"Date",
					"Keep-Alive",
					"Pragma",
					"Trailer",
					"Transfer-Encoding",
					"Upgrade",
					"Via",
					"Warning",
					"Allow",
					"Content-Length",
					"Content-Type",
					"Content-Encoding",
					"Content-Language",
					"Content-Location",
					"Content-MD5",
					"Content-Range",
					"Expires",
					"Last-Modified",
					"Accept-Ranges",
					"Age",
					"ETag",
					"Location",
					"Proxy-Authenticate",
					"Retry-After",
					"Server",
					"Set-Cookie",
					"Vary",
					"WWW-Authenticate"
				};

				// Token: 0x02000483 RID: 1155
				internal enum Enum
				{
					// Token: 0x040023F9 RID: 9209
					HttpHeaderCacheControl,
					// Token: 0x040023FA RID: 9210
					HttpHeaderConnection,
					// Token: 0x040023FB RID: 9211
					HttpHeaderDate,
					// Token: 0x040023FC RID: 9212
					HttpHeaderKeepAlive,
					// Token: 0x040023FD RID: 9213
					HttpHeaderPragma,
					// Token: 0x040023FE RID: 9214
					HttpHeaderTrailer,
					// Token: 0x040023FF RID: 9215
					HttpHeaderTransferEncoding,
					// Token: 0x04002400 RID: 9216
					HttpHeaderUpgrade,
					// Token: 0x04002401 RID: 9217
					HttpHeaderVia,
					// Token: 0x04002402 RID: 9218
					HttpHeaderWarning,
					// Token: 0x04002403 RID: 9219
					HttpHeaderAllow,
					// Token: 0x04002404 RID: 9220
					HttpHeaderContentLength,
					// Token: 0x04002405 RID: 9221
					HttpHeaderContentType,
					// Token: 0x04002406 RID: 9222
					HttpHeaderContentEncoding,
					// Token: 0x04002407 RID: 9223
					HttpHeaderContentLanguage,
					// Token: 0x04002408 RID: 9224
					HttpHeaderContentLocation,
					// Token: 0x04002409 RID: 9225
					HttpHeaderContentMd5,
					// Token: 0x0400240A RID: 9226
					HttpHeaderContentRange,
					// Token: 0x0400240B RID: 9227
					HttpHeaderExpires,
					// Token: 0x0400240C RID: 9228
					HttpHeaderLastModified,
					// Token: 0x0400240D RID: 9229
					HttpHeaderAcceptRanges,
					// Token: 0x0400240E RID: 9230
					HttpHeaderAge,
					// Token: 0x0400240F RID: 9231
					HttpHeaderEtag,
					// Token: 0x04002410 RID: 9232
					HttpHeaderLocation,
					// Token: 0x04002411 RID: 9233
					HttpHeaderProxyAuthenticate,
					// Token: 0x04002412 RID: 9234
					HttpHeaderRetryAfter,
					// Token: 0x04002413 RID: 9235
					HttpHeaderServer,
					// Token: 0x04002414 RID: 9236
					HttpHeaderSetCookie,
					// Token: 0x04002415 RID: 9237
					HttpHeaderVary,
					// Token: 0x04002416 RID: 9238
					HttpHeaderWwwAuthenticate,
					// Token: 0x04002417 RID: 9239
					HttpHeaderResponseMaximum,
					// Token: 0x04002418 RID: 9240
					HttpHeaderMaximum = 41
				}
			}
		}
	}
}
