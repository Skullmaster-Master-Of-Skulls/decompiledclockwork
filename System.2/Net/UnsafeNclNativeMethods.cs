using System;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Net.Cache;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using Microsoft.Win32;
using Microsoft.Win32.SafeHandles;

namespace System.Net
{
	// Token: 0x02000166 RID: 358
	[SuppressUnmanagedCodeSecurity]
	internal static class UnsafeNclNativeMethods
	{
		// Token: 0x06000CFF RID: 3327
		[DllImport("kernel32.dll")]
		internal static extern IntPtr CreateSemaphore([In] IntPtr lpSemaphoreAttributes, [In] int lInitialCount, [In] int lMaximumCount, [In] IntPtr lpName);

		// Token: 0x06000D00 RID: 3328
		[DllImport("kernel32.dll")]
		internal static extern bool ReleaseSemaphore([In] IntPtr hSemaphore, [In] int lReleaseCount, [In] IntPtr lpPreviousCount);

		// Token: 0x06000D01 RID: 3329
		[DllImport("kernel32.dll", CallingConvention = CallingConvention.StdCall, ExactSpelling = true, SetLastError = true)]
		internal static extern uint GetCurrentThreadId();

		// Token: 0x06000D02 RID: 3330
		[DllImport("kernel32.dll", CallingConvention = CallingConvention.StdCall, ExactSpelling = true, SetLastError = true)]
		internal unsafe static extern uint CancelIoEx(CriticalHandle handle, NativeOverlapped* overlapped);

		// Token: 0x06000D03 RID: 3331
		[DllImport("kernel32.dll", CallingConvention = CallingConvention.StdCall, ExactSpelling = true, SetLastError = true)]
		internal static extern uint CancelIoEx(SafeHandle handle, IntPtr overlapped);

		// Token: 0x06000D04 RID: 3332
		[DllImport("kernel32.dll", CallingConvention = CallingConvention.StdCall, ExactSpelling = true, SetLastError = true)]
		internal static extern bool SetFileCompletionNotificationModes(CriticalHandle handle, UnsafeNclNativeMethods.FileCompletionNotificationModes modes);

		// Token: 0x06000D05 RID: 3333
		[DllImport("kernel32.dll", SetLastError = true)]
		internal static extern IntPtr GetProcessHeap();

		// Token: 0x06000D06 RID: 3334
		[DllImport("kernel32.dll", SetLastError = true)]
		internal static extern bool HeapFree([In] IntPtr hHeap, [In] uint dwFlags, [In] IntPtr lpMem);

		// Token: 0x06000D07 RID: 3335
		[SecurityCritical]
		[DllImport("kernel32.dll", SetLastError = true)]
		internal static extern IntPtr GetProcAddress(SafeLoadLibrary hModule, string entryPoint);

		// Token: 0x06000D08 RID: 3336
		[SecurityCritical]
		[DllImport("kernel32.dll", SetLastError = true)]
		internal static extern IntPtr GetProcAddress(IntPtr hModule, string entryPoint);

		// Token: 0x06000D09 RID: 3337
		[DllImport("kernel32.dll", ExactSpelling = true)]
		internal static extern void DebugBreak();

		// Token: 0x06000D0A RID: 3338
		[DllImport("ole32.dll", PreserveSig = false)]
		public static extern void CoCreateInstance([In] ref Guid clsid, IntPtr pUnkOuter, int context, [In] ref Guid iid, [MarshalAs(UnmanagedType.IUnknown)] out object o);

		// Token: 0x040011E1 RID: 4577
		private const string KERNEL32 = "kernel32.dll";

		// Token: 0x040011E2 RID: 4578
		private const string WS2_32 = "ws2_32.dll";

		// Token: 0x040011E3 RID: 4579
		private const string SECUR32 = "secur32.dll";

		// Token: 0x040011E4 RID: 4580
		private const string CRYPT32 = "crypt32.dll";

		// Token: 0x040011E5 RID: 4581
		private const string ADVAPI32 = "advapi32.dll";

		// Token: 0x040011E6 RID: 4582
		private const string HTTPAPI = "httpapi.dll";

		// Token: 0x040011E7 RID: 4583
		private const string SCHANNEL = "schannel.dll";

		// Token: 0x040011E8 RID: 4584
		private const string RASAPI32 = "rasapi32.dll";

		// Token: 0x040011E9 RID: 4585
		private const string WININET = "wininet.dll";

		// Token: 0x040011EA RID: 4586
		private const string WINHTTP = "winhttp.dll";

		// Token: 0x040011EB RID: 4587
		private const string BCRYPT = "bcrypt.dll";

		// Token: 0x040011EC RID: 4588
		private const string USER32 = "user32.dll";

		// Token: 0x040011ED RID: 4589
		private const string TOKENBINDING = "tokenbinding.dll";

		// Token: 0x040011EE RID: 4590
		private const string OLE32 = "ole32.dll";

		// Token: 0x040011EF RID: 4591
		internal const int CLSCTX_SERVER = 21;

		// Token: 0x02000712 RID: 1810
		internal static class ErrorCodes
		{
			// Token: 0x04003128 RID: 12584
			internal const uint ERROR_SUCCESS = 0U;

			// Token: 0x04003129 RID: 12585
			internal const uint ERROR_HANDLE_EOF = 38U;

			// Token: 0x0400312A RID: 12586
			internal const uint ERROR_NOT_SUPPORTED = 50U;

			// Token: 0x0400312B RID: 12587
			internal const uint ERROR_INVALID_PARAMETER = 87U;

			// Token: 0x0400312C RID: 12588
			internal const uint ERROR_ALREADY_EXISTS = 183U;

			// Token: 0x0400312D RID: 12589
			internal const uint ERROR_MORE_DATA = 234U;

			// Token: 0x0400312E RID: 12590
			internal const uint ERROR_OPERATION_ABORTED = 995U;

			// Token: 0x0400312F RID: 12591
			internal const uint ERROR_IO_PENDING = 997U;

			// Token: 0x04003130 RID: 12592
			internal const uint ERROR_NOT_FOUND = 1168U;

			// Token: 0x04003131 RID: 12593
			internal const uint ERROR_CONNECTION_INVALID = 1229U;
		}

		// Token: 0x02000713 RID: 1811
		internal static class NTStatus
		{
			// Token: 0x04003132 RID: 12594
			internal const uint STATUS_SUCCESS = 0U;

			// Token: 0x04003133 RID: 12595
			internal const uint STATUS_OBJECT_NAME_NOT_FOUND = 3221225524U;
		}

		// Token: 0x02000714 RID: 1812
		[Flags]
		internal enum FileCompletionNotificationModes : byte
		{
			// Token: 0x04003135 RID: 12597
			None = 0,
			// Token: 0x04003136 RID: 12598
			SkipCompletionPortOnSuccess = 1,
			// Token: 0x04003137 RID: 12599
			SkipSetEventOnHandle = 2
		}

		// Token: 0x02000715 RID: 1813
		[SuppressUnmanagedCodeSecurity]
		internal static class RegistryHelper
		{
			// Token: 0x060040AD RID: 16557
			[DllImport("advapi32.dll", BestFitMapping = false, CharSet = CharSet.Auto, SetLastError = true, ThrowOnUnmappableChar = true)]
			internal static extern uint RegOpenKeyEx(IntPtr key, string subKey, uint ulOptions, uint samDesired, out SafeRegistryHandle resultSubKey);

			// Token: 0x060040AE RID: 16558
			[DllImport("advapi32.dll", BestFitMapping = false, CharSet = CharSet.Auto, SetLastError = true, ThrowOnUnmappableChar = true)]
			internal static extern uint RegOpenKeyEx(SafeRegistryHandle key, string subKey, uint ulOptions, uint samDesired, out SafeRegistryHandle resultSubKey);

			// Token: 0x060040AF RID: 16559
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
			[DllImport("advapi32.dll", ExactSpelling = true, SetLastError = true)]
			internal static extern uint RegCloseKey(IntPtr key);

			// Token: 0x060040B0 RID: 16560
			[DllImport("advapi32.dll", ExactSpelling = true, SetLastError = true)]
			internal static extern uint RegNotifyChangeKeyValue(SafeRegistryHandle key, bool watchSubTree, uint notifyFilter, SafeWaitHandle regEvent, bool async);

			// Token: 0x060040B1 RID: 16561
			[DllImport("advapi32.dll", ExactSpelling = true, SetLastError = true)]
			internal static extern uint RegOpenCurrentUser(uint samDesired, out SafeRegistryHandle resultKey);

			// Token: 0x060040B2 RID: 16562
			[DllImport("advapi32.dll", BestFitMapping = false, CharSet = CharSet.Auto, SetLastError = true, ThrowOnUnmappableChar = true)]
			internal static extern uint RegQueryValueEx(SafeRegistryHandle key, string valueName, IntPtr reserved, out uint type, [Out] byte[] data, [In] [Out] ref uint size);

			// Token: 0x04003138 RID: 12600
			internal const uint REG_NOTIFY_CHANGE_LAST_SET = 4U;

			// Token: 0x04003139 RID: 12601
			internal const uint REG_BINARY = 3U;

			// Token: 0x0400313A RID: 12602
			internal const uint KEY_READ = 131097U;

			// Token: 0x0400313B RID: 12603
			internal static readonly IntPtr HKEY_CURRENT_USER = (IntPtr)(-2147483647);

			// Token: 0x0400313C RID: 12604
			internal static readonly IntPtr HKEY_LOCAL_MACHINE = (IntPtr)(-2147483646);
		}

		// Token: 0x02000716 RID: 1814
		[SuppressUnmanagedCodeSecurity]
		internal class RasHelper
		{
			// Token: 0x060040B4 RID: 16564 RVA: 0x0010F444 File Offset: 0x0010D644
			static RasHelper()
			{
				if (ComNetOS.InstallationType == WindowsInstallationType.ServerCore)
				{
					UnsafeNclNativeMethods.RasHelper.s_RasSupported = false;
				}
				else
				{
					UnsafeNclNativeMethods.RasHelper.s_RasSupported = true;
				}
				if (Logging.On)
				{
					Logging.PrintInfo(Logging.Web, SR.GetString("net_log_proxy_ras_supported", new object[]
					{
						UnsafeNclNativeMethods.RasHelper.s_RasSupported
					}));
				}
			}

			// Token: 0x060040B5 RID: 16565 RVA: 0x0010F498 File Offset: 0x0010D698
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

			// Token: 0x17000EF5 RID: 3829
			// (get) Token: 0x060040B6 RID: 16566 RVA: 0x0010F502 File Offset: 0x0010D702
			internal static bool RasSupported
			{
				get
				{
					return UnsafeNclNativeMethods.RasHelper.s_RasSupported;
				}
			}

			// Token: 0x17000EF6 RID: 3830
			// (get) Token: 0x060040B7 RID: 16567 RVA: 0x0010F50C File Offset: 0x0010D70C
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

			// Token: 0x060040B8 RID: 16568 RVA: 0x0010F548 File Offset: 0x0010D748
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

			// Token: 0x060040B9 RID: 16569 RVA: 0x0010F580 File Offset: 0x0010D780
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
					if (UnsafeNclNativeMethods.RasHelper.RasGetConnectStatus(array[(int)num5].hrasconn, ref rasconnstatus) == 0U && rasconnstatus.rasconnstate == UnsafeNclNativeMethods.RasHelper.RASCONNSTATE.RASCS_Connected)
					{
						return array[(int)num5].szEntryName;
					}
				}
				return null;
			}

			// Token: 0x060040BA RID: 16570
			[DllImport("rasapi32.dll", BestFitMapping = false, CharSet = CharSet.Auto, ThrowOnUnmappableChar = true)]
			private static extern uint RasEnumConnections([In] [Out] UnsafeNclNativeMethods.RasHelper.RASCONN[] lprasconn, ref uint lpcb, ref uint lpcConnections);

			// Token: 0x060040BB RID: 16571
			[DllImport("rasapi32.dll", BestFitMapping = false, CharSet = CharSet.Auto, ThrowOnUnmappableChar = true)]
			private static extern uint RasGetConnectStatus([In] IntPtr hrasconn, [In] [Out] ref UnsafeNclNativeMethods.RasHelper.RASCONNSTATUS lprasconnstatus);

			// Token: 0x060040BC RID: 16572
			[DllImport("rasapi32.dll", BestFitMapping = false, CharSet = CharSet.Auto, ThrowOnUnmappableChar = true)]
			private static extern uint RasConnectionNotification([In] IntPtr hrasconn, [In] SafeWaitHandle hEvent, uint dwFlags);

			// Token: 0x0400313D RID: 12605
			private static readonly bool s_RasSupported;

			// Token: 0x0400313E RID: 12606
			private ManualResetEvent m_RasEvent;

			// Token: 0x0400313F RID: 12607
			private bool m_Suppressed;

			// Token: 0x04003140 RID: 12608
			private const int RAS_MaxEntryName = 256;

			// Token: 0x04003141 RID: 12609
			private const int RAS_MaxDeviceType = 16;

			// Token: 0x04003142 RID: 12610
			private const int RAS_MaxDeviceName = 128;

			// Token: 0x04003143 RID: 12611
			private const int RAS_MaxPhoneNumber = 128;

			// Token: 0x04003144 RID: 12612
			private const int RAS_MaxCallbackNumber = 128;

			// Token: 0x04003145 RID: 12613
			private const uint RASCN_Connection = 1U;

			// Token: 0x04003146 RID: 12614
			private const uint RASCN_Disconnection = 2U;

			// Token: 0x04003147 RID: 12615
			private const int UNLEN = 256;

			// Token: 0x04003148 RID: 12616
			private const int PWLEN = 256;

			// Token: 0x04003149 RID: 12617
			private const int DNLEN = 15;

			// Token: 0x0400314A RID: 12618
			private const int MAX_PATH = 260;

			// Token: 0x0400314B RID: 12619
			private const uint RASBASE = 600U;

			// Token: 0x0400314C RID: 12620
			private const uint ERROR_DIAL_ALREADY_IN_PROGRESS = 756U;

			// Token: 0x0400314D RID: 12621
			private const uint ERROR_BUFFER_TOO_SMALL = 603U;

			// Token: 0x0400314E RID: 12622
			private const int RASCS_PAUSED = 4096;

			// Token: 0x0400314F RID: 12623
			private const int RASCS_DONE = 8192;

			// Token: 0x020008CE RID: 2254
			[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto, Pack = 4)]
			private struct RASCONN
			{
				// Token: 0x04003B75 RID: 15221
				internal uint dwSize;

				// Token: 0x04003B76 RID: 15222
				internal IntPtr hrasconn;

				// Token: 0x04003B77 RID: 15223
				[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 257)]
				internal string szEntryName;

				// Token: 0x04003B78 RID: 15224
				[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 17)]
				internal string szDeviceType;

				// Token: 0x04003B79 RID: 15225
				[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 129)]
				internal string szDeviceName;
			}

			// Token: 0x020008CF RID: 2255
			[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
			private struct RASCONNSTATUS
			{
				// Token: 0x04003B7A RID: 15226
				internal uint dwSize;

				// Token: 0x04003B7B RID: 15227
				internal UnsafeNclNativeMethods.RasHelper.RASCONNSTATE rasconnstate;

				// Token: 0x04003B7C RID: 15228
				internal uint dwError;

				// Token: 0x04003B7D RID: 15229
				[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 17)]
				internal string szDeviceType;

				// Token: 0x04003B7E RID: 15230
				[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 129)]
				internal string szDeviceName;
			}

			// Token: 0x020008D0 RID: 2256
			[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
			private struct RASDIALPARAMS
			{
				// Token: 0x04003B7F RID: 15231
				internal uint dwSize;

				// Token: 0x04003B80 RID: 15232
				[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 257)]
				internal string szEntryName;

				// Token: 0x04003B81 RID: 15233
				[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 129)]
				internal string szPhoneNumber;

				// Token: 0x04003B82 RID: 15234
				[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 129)]
				internal string szCallbackNumber;

				// Token: 0x04003B83 RID: 15235
				[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 257)]
				internal string szUserName;

				// Token: 0x04003B84 RID: 15236
				[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 257)]
				internal string szPassword;

				// Token: 0x04003B85 RID: 15237
				[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 16)]
				internal string szDomain;
			}

			// Token: 0x020008D1 RID: 2257
			private enum RASCONNSTATE
			{
				// Token: 0x04003B87 RID: 15239
				RASCS_OpenPort,
				// Token: 0x04003B88 RID: 15240
				RASCS_PortOpened,
				// Token: 0x04003B89 RID: 15241
				RASCS_ConnectDevice,
				// Token: 0x04003B8A RID: 15242
				RASCS_DeviceConnected,
				// Token: 0x04003B8B RID: 15243
				RASCS_AllDevicesConnected,
				// Token: 0x04003B8C RID: 15244
				RASCS_Authenticate,
				// Token: 0x04003B8D RID: 15245
				RASCS_AuthNotify,
				// Token: 0x04003B8E RID: 15246
				RASCS_AuthRetry,
				// Token: 0x04003B8F RID: 15247
				RASCS_AuthCallback,
				// Token: 0x04003B90 RID: 15248
				RASCS_AuthChangePassword,
				// Token: 0x04003B91 RID: 15249
				RASCS_AuthProject,
				// Token: 0x04003B92 RID: 15250
				RASCS_AuthLinkSpeed,
				// Token: 0x04003B93 RID: 15251
				RASCS_AuthAck,
				// Token: 0x04003B94 RID: 15252
				RASCS_ReAuthenticate,
				// Token: 0x04003B95 RID: 15253
				RASCS_Authenticated,
				// Token: 0x04003B96 RID: 15254
				RASCS_PrepareForCallback,
				// Token: 0x04003B97 RID: 15255
				RASCS_WaitForModemReset,
				// Token: 0x04003B98 RID: 15256
				RASCS_WaitForCallback,
				// Token: 0x04003B99 RID: 15257
				RASCS_Projected,
				// Token: 0x04003B9A RID: 15258
				RASCS_StartAuthentication,
				// Token: 0x04003B9B RID: 15259
				RASCS_CallbackComplete,
				// Token: 0x04003B9C RID: 15260
				RASCS_LogonNetwork,
				// Token: 0x04003B9D RID: 15261
				RASCS_SubEntryConnected,
				// Token: 0x04003B9E RID: 15262
				RASCS_SubEntryDisconnected,
				// Token: 0x04003B9F RID: 15263
				RASCS_Interactive = 4096,
				// Token: 0x04003BA0 RID: 15264
				RASCS_RetryAuthentication,
				// Token: 0x04003BA1 RID: 15265
				RASCS_CallbackSetByCaller,
				// Token: 0x04003BA2 RID: 15266
				RASCS_PasswordExpired,
				// Token: 0x04003BA3 RID: 15267
				RASCS_InvokeEapUI,
				// Token: 0x04003BA4 RID: 15268
				RASCS_Connected = 8192,
				// Token: 0x04003BA5 RID: 15269
				RASCS_Disconnected
			}
		}

		// Token: 0x02000717 RID: 1815
		[SuppressUnmanagedCodeSecurity]
		internal static class SafeNetHandles_SECURITY
		{
			// Token: 0x060040BD RID: 16573
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
			[DllImport("secur32.dll", ExactSpelling = true, SetLastError = true)]
			internal static extern int FreeContextBuffer([In] IntPtr contextBuffer);

			// Token: 0x060040BE RID: 16574
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
			[DllImport("secur32.dll", ExactSpelling = true, SetLastError = true)]
			internal static extern int FreeCredentialsHandle(ref SSPIHandle handlePtr);

			// Token: 0x060040BF RID: 16575
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
			[DllImport("secur32.dll", ExactSpelling = true, SetLastError = true)]
			internal static extern int DeleteSecurityContext(ref SSPIHandle handlePtr);

			// Token: 0x060040C0 RID: 16576
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
			[DllImport("secur32.dll", ExactSpelling = true, SetLastError = true)]
			internal unsafe static extern int AcceptSecurityContext(ref SSPIHandle credentialHandle, [In] void* inContextPtr, [In] SecurityBufferDescriptor inputBuffer, [In] ContextFlags inFlags, [In] Endianness endianness, ref SSPIHandle outContextPtr, [In] [Out] SecurityBufferDescriptor outputBuffer, [In] [Out] ref ContextFlags attributes, out long timeStamp);

			// Token: 0x060040C1 RID: 16577
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
			[DllImport("secur32.dll", ExactSpelling = true, SetLastError = true)]
			internal unsafe static extern int QueryContextAttributesW(ref SSPIHandle contextHandle, [In] ContextAttribute attribute, [In] void* buffer);

			// Token: 0x060040C2 RID: 16578
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
			[DllImport("secur32.dll", ExactSpelling = true, SetLastError = true)]
			internal static extern int SetContextAttributesW(ref SSPIHandle contextHandle, [In] ContextAttribute attribute, [In] byte[] buffer, [In] int bufferSize);

			// Token: 0x060040C3 RID: 16579
			[DllImport("secur32.dll", ExactSpelling = true, SetLastError = true)]
			internal static extern int EnumerateSecurityPackagesW(out int pkgnum, out SafeFreeContextBuffer_SECURITY handle);

			// Token: 0x060040C4 RID: 16580
			[DllImport("secur32.dll", CharSet = CharSet.Unicode, ExactSpelling = true, SetLastError = true)]
			internal unsafe static extern int AcquireCredentialsHandleW([In] string principal, [In] string moduleName, [In] int usage, [In] void* logonID, [In] ref AuthIdentity authdata, [In] void* keyCallback, [In] void* keyArgument, ref SSPIHandle handlePtr, out long timeStamp);

			// Token: 0x060040C5 RID: 16581
			[DllImport("secur32.dll", CharSet = CharSet.Unicode, ExactSpelling = true, SetLastError = true)]
			internal unsafe static extern int AcquireCredentialsHandleW([In] string principal, [In] string moduleName, [In] int usage, [In] void* logonID, [In] IntPtr zero, [In] void* keyCallback, [In] void* keyArgument, ref SSPIHandle handlePtr, out long timeStamp);

			// Token: 0x060040C6 RID: 16582
			[DllImport("secur32.dll", CharSet = CharSet.Unicode, ExactSpelling = true, SetLastError = true)]
			internal unsafe static extern int AcquireCredentialsHandleW([In] string principal, [In] string moduleName, [In] int usage, [In] void* logonID, [In] SafeSspiAuthDataHandle authdata, [In] void* keyCallback, [In] void* keyArgument, ref SSPIHandle handlePtr, out long timeStamp);

			// Token: 0x060040C7 RID: 16583
			[DllImport("secur32.dll", CharSet = CharSet.Unicode, ExactSpelling = true, SetLastError = true)]
			internal unsafe static extern int AcquireCredentialsHandleW([In] string principal, [In] string moduleName, [In] int usage, [In] void* logonID, [In] ref SecureCredential authData, [In] void* keyCallback, [In] void* keyArgument, ref SSPIHandle handlePtr, out long timeStamp);

			// Token: 0x060040C8 RID: 16584
			[DllImport("secur32.dll", CharSet = CharSet.Unicode, ExactSpelling = true, SetLastError = true)]
			internal unsafe static extern int AcquireCredentialsHandleW([In] string principal, [In] string moduleName, [In] int usage, [In] void* logonID, [In] ref SecureCredential2 authData, [In] void* keyCallback, [In] void* keyArgument, ref SSPIHandle handlePtr, out long timeStamp);

			// Token: 0x060040C9 RID: 16585
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
			[DllImport("secur32.dll", ExactSpelling = true, SetLastError = true)]
			internal unsafe static extern int InitializeSecurityContextW(ref SSPIHandle credentialHandle, [In] void* inContextPtr, [In] byte* targetName, [In] ContextFlags inFlags, [In] int reservedI, [In] Endianness endianness, [In] SecurityBufferDescriptor inputBuffer, [In] int reservedII, ref SSPIHandle outContextPtr, [In] [Out] SecurityBufferDescriptor outputBuffer, [In] [Out] ref ContextFlags attributes, out long timeStamp);

			// Token: 0x060040CA RID: 16586
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
			[DllImport("secur32.dll", ExactSpelling = true, SetLastError = true)]
			internal unsafe static extern int CompleteAuthToken([In] void* inContextPtr, [In] [Out] SecurityBufferDescriptor inputBuffers);

			// Token: 0x060040CB RID: 16587
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
			[DllImport("secur32.dll", ExactSpelling = true, SetLastError = true)]
			internal unsafe static extern int ApplyControlToken([In] void* inContextPtr, [In] [Out] SecurityBufferDescriptor inputBuffers);
		}

		// Token: 0x02000718 RID: 1816
		[SuppressUnmanagedCodeSecurity]
		internal static class SafeNetHandlesSafeOverlappedFree
		{
			// Token: 0x060040CC RID: 16588
			[DllImport("kernel32.dll", ExactSpelling = true, SetLastError = true)]
			internal static extern SafeOverlappedFree LocalAlloc(int uFlags, UIntPtr sizetdwBytes);
		}

		// Token: 0x02000719 RID: 1817
		[SuppressUnmanagedCodeSecurity]
		internal static class SafeNetHandlesXPOrLater
		{
			// Token: 0x060040CD RID: 16589
			[DllImport("ws2_32.dll", BestFitMapping = false, CharSet = CharSet.Unicode, ExactSpelling = true, SetLastError = true, ThrowOnUnmappableChar = true)]
			internal static extern int GetAddrInfoW([In] string nodename, [In] string servicename, [In] ref AddressInfo hints, out SafeFreeAddrInfo handle);

			// Token: 0x060040CE RID: 16590
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
			[DllImport("ws2_32.dll", ExactSpelling = true, SetLastError = true)]
			internal static extern void freeaddrinfo([In] IntPtr info);
		}

		// Token: 0x0200071A RID: 1818
		[SuppressUnmanagedCodeSecurity]
		internal static class SafeNetHandles
		{
			// Token: 0x060040CF RID: 16591
			[DllImport("secur32.dll", ExactSpelling = true, SetLastError = true)]
			internal static extern int QuerySecurityContextToken(ref SSPIHandle phContext, out SafeCloseHandle handle);

			// Token: 0x060040D0 RID: 16592
			[DllImport("httpapi.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode, ExactSpelling = true, SetLastError = true)]
			internal static extern uint HttpCreateRequestQueue(UnsafeNclNativeMethods.HttpApi.HTTPAPI_VERSION version, string pName, NativeMethods.SECURITY_ATTRIBUTES pSecurityAttributes, uint flags, out HttpRequestQueueV2Handle pReqQueueHandle);

			// Token: 0x060040D1 RID: 16593
			[DllImport("httpapi.dll", CallingConvention = CallingConvention.StdCall, ExactSpelling = true, SetLastError = true)]
			internal static extern uint HttpCloseRequestQueue(IntPtr pReqQueueHandle);

			// Token: 0x060040D2 RID: 16594
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
			[DllImport("kernel32.dll", ExactSpelling = true, SetLastError = true)]
			internal static extern bool CloseHandle(IntPtr handle);

			// Token: 0x060040D3 RID: 16595
			[DllImport("kernel32.dll", ExactSpelling = true, SetLastError = true)]
			internal static extern SafeLocalFree LocalAlloc(int uFlags, UIntPtr sizetdwBytes);

			// Token: 0x060040D4 RID: 16596
			[DllImport("kernel32.dll", EntryPoint = "LocalAlloc", SetLastError = true)]
			internal static extern SafeLocalFreeChannelBinding LocalAllocChannelBinding(int uFlags, UIntPtr sizetdwBytes);

			// Token: 0x060040D5 RID: 16597
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
			[DllImport("kernel32.dll", ExactSpelling = true, SetLastError = true)]
			internal static extern IntPtr LocalFree(IntPtr handle);

			// Token: 0x060040D6 RID: 16598
			[DllImport("kernel32.dll", CharSet = CharSet.Unicode, ExactSpelling = true, SetLastError = true)]
			internal unsafe static extern SafeLoadLibrary LoadLibraryExW([In] string lpwLibFileName, [In] void* hFile, [In] uint dwFlags);

			// Token: 0x060040D7 RID: 16599
			[DllImport("kernel32.dll", CharSet = CharSet.Unicode, ExactSpelling = true, SetLastError = true)]
			public static extern IntPtr GetModuleHandleW(string modName);

			// Token: 0x060040D8 RID: 16600
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
			[DllImport("kernel32.dll", ExactSpelling = true, SetLastError = true)]
			internal static extern bool FreeLibrary([In] IntPtr hModule);

			// Token: 0x060040D9 RID: 16601
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
			[DllImport("crypt32.dll", ExactSpelling = true, SetLastError = true)]
			internal static extern void CertFreeCertificateChain([In] IntPtr pChainContext);

			// Token: 0x060040DA RID: 16602
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
			[DllImport("crypt32.dll", ExactSpelling = true, SetLastError = true)]
			internal static extern void CertFreeCertificateChainList([In] IntPtr ppChainContext);

			// Token: 0x060040DB RID: 16603
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
			[DllImport("crypt32.dll", ExactSpelling = true, SetLastError = true)]
			internal static extern bool CertFreeCertificateContext([In] IntPtr certContext);

			// Token: 0x060040DC RID: 16604
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
			[DllImport("kernel32.dll", ExactSpelling = true, SetLastError = true)]
			internal static extern IntPtr GlobalFree(IntPtr handle);

			// Token: 0x060040DD RID: 16605
			[DllImport("ws2_32.dll", ExactSpelling = true, SetLastError = true)]
			internal static extern SafeCloseSocket.InnerSafeCloseSocket accept([In] IntPtr socketHandle, [Out] byte[] socketAddress, [In] [Out] ref int socketAddressSize);

			// Token: 0x060040DE RID: 16606
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
			[DllImport("ws2_32.dll", ExactSpelling = true, SetLastError = true)]
			internal static extern SocketError closesocket([In] IntPtr socketHandle);

			// Token: 0x060040DF RID: 16607
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
			[DllImport("ws2_32.dll", ExactSpelling = true, SetLastError = true)]
			internal static extern SocketError ioctlsocket([In] IntPtr handle, [In] int cmd, [In] [Out] ref int argp);

			// Token: 0x060040E0 RID: 16608
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
			[DllImport("ws2_32.dll", ExactSpelling = true, SetLastError = true)]
			internal static extern SocketError WSAEventSelect([In] IntPtr handle, [In] IntPtr Event, [In] AsyncEventBits NetworkEvents);

			// Token: 0x060040E1 RID: 16609
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
			[DllImport("ws2_32.dll", ExactSpelling = true, SetLastError = true)]
			internal static extern SocketError setsockopt([In] IntPtr handle, [In] SocketOptionLevel optionLevel, [In] SocketOptionName optionName, [In] ref Linger linger, [In] int optionLength);

			// Token: 0x060040E2 RID: 16610
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
			[DllImport("wininet.dll", ExactSpelling = true, SetLastError = true)]
			internal unsafe static extern bool RetrieveUrlCacheEntryFileW([In] char* urlName, [In] byte* entryPtr, [In] [Out] ref int entryBufSize, [In] int dwReserved);

			// Token: 0x060040E3 RID: 16611
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
			[DllImport("wininet.dll", ExactSpelling = true, SetLastError = true)]
			internal unsafe static extern bool UnlockUrlCacheEntryFileW([In] char* urlName, [In] int dwReserved);
		}

		// Token: 0x0200071B RID: 1819
		[SuppressUnmanagedCodeSecurity]
		internal static class OSSOCK
		{
			// Token: 0x060040E4 RID: 16612
			[DllImport("ws2_32.dll", CharSet = CharSet.Auto, SetLastError = true)]
			internal static extern SafeCloseSocket.InnerSafeCloseSocket WSASocket([In] AddressFamily addressFamily, [In] SocketType socketType, [In] ProtocolType protocolType, [In] IntPtr protocolInfo, [In] uint group, [In] SocketConstructorFlags flags);

			// Token: 0x060040E5 RID: 16613
			[DllImport("ws2_32.dll", CharSet = CharSet.Auto, SetLastError = true)]
			internal unsafe static extern SafeCloseSocket.InnerSafeCloseSocket WSASocket([In] AddressFamily addressFamily, [In] SocketType socketType, [In] ProtocolType protocolType, [In] byte* pinnedBuffer, [In] uint group, [In] SocketConstructorFlags flags);

			// Token: 0x060040E6 RID: 16614
			[DllImport("ws2_32.dll", BestFitMapping = false, CharSet = CharSet.Ansi, SetLastError = true, ThrowOnUnmappableChar = true)]
			internal static extern SocketError WSAStartup([In] short wVersionRequested, out WSAData lpWSAData);

			// Token: 0x060040E7 RID: 16615
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
			[DllImport("ws2_32.dll", SetLastError = true)]
			internal static extern SocketError ioctlsocket([In] SafeCloseSocket socketHandle, [In] int cmd, [In] [Out] ref int argp);

			// Token: 0x060040E8 RID: 16616
			[DllImport("ws2_32.dll", BestFitMapping = false, CharSet = CharSet.Ansi, SetLastError = true, ThrowOnUnmappableChar = true)]
			internal static extern IntPtr gethostbyname([In] string host);

			// Token: 0x060040E9 RID: 16617
			[DllImport("ws2_32.dll", SetLastError = true)]
			internal static extern IntPtr gethostbyaddr([In] ref int addr, [In] int len, [In] ProtocolFamily type);

			// Token: 0x060040EA RID: 16618
			[DllImport("ws2_32.dll", BestFitMapping = false, CharSet = CharSet.Ansi, SetLastError = true, ThrowOnUnmappableChar = true)]
			internal static extern SocketError gethostname([Out] StringBuilder hostName, [In] int bufferLength);

			// Token: 0x060040EB RID: 16619
			[DllImport("ws2_32.dll", SetLastError = true)]
			internal static extern SocketError getpeername([In] SafeCloseSocket socketHandle, [Out] byte[] socketAddress, [In] [Out] ref int socketAddressSize);

			// Token: 0x060040EC RID: 16620
			[DllImport("ws2_32.dll", SetLastError = true)]
			internal static extern SocketError getsockopt([In] SafeCloseSocket socketHandle, [In] SocketOptionLevel optionLevel, [In] SocketOptionName optionName, out int optionValue, [In] [Out] ref int optionLength);

			// Token: 0x060040ED RID: 16621
			[DllImport("ws2_32.dll", SetLastError = true)]
			internal static extern SocketError getsockopt([In] SafeCloseSocket socketHandle, [In] SocketOptionLevel optionLevel, [In] SocketOptionName optionName, [Out] byte[] optionValue, [In] [Out] ref int optionLength);

			// Token: 0x060040EE RID: 16622
			[DllImport("ws2_32.dll", SetLastError = true)]
			internal static extern SocketError getsockopt([In] SafeCloseSocket socketHandle, [In] SocketOptionLevel optionLevel, [In] SocketOptionName optionName, out Linger optionValue, [In] [Out] ref int optionLength);

			// Token: 0x060040EF RID: 16623
			[DllImport("ws2_32.dll", SetLastError = true)]
			internal static extern SocketError getsockopt([In] SafeCloseSocket socketHandle, [In] SocketOptionLevel optionLevel, [In] SocketOptionName optionName, out IPMulticastRequest optionValue, [In] [Out] ref int optionLength);

			// Token: 0x060040F0 RID: 16624
			[DllImport("ws2_32.dll", SetLastError = true)]
			internal static extern SocketError getsockopt([In] SafeCloseSocket socketHandle, [In] SocketOptionLevel optionLevel, [In] SocketOptionName optionName, out IPv6MulticastRequest optionValue, [In] [Out] ref int optionLength);

			// Token: 0x060040F1 RID: 16625
			[DllImport("ws2_32.dll", SetLastError = true)]
			internal static extern SocketError setsockopt([In] SafeCloseSocket socketHandle, [In] SocketOptionLevel optionLevel, [In] SocketOptionName optionName, [In] ref int optionValue, [In] int optionLength);

			// Token: 0x060040F2 RID: 16626
			[DllImport("ws2_32.dll", SetLastError = true)]
			internal static extern SocketError setsockopt([In] SafeCloseSocket socketHandle, [In] SocketOptionLevel optionLevel, [In] SocketOptionName optionName, [In] byte[] optionValue, [In] int optionLength);

			// Token: 0x060040F3 RID: 16627
			[DllImport("ws2_32.dll", SetLastError = true)]
			internal static extern SocketError setsockopt([In] SafeCloseSocket socketHandle, [In] SocketOptionLevel optionLevel, [In] SocketOptionName optionName, [In] ref IntPtr pointer, [In] int optionLength);

			// Token: 0x060040F4 RID: 16628
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
			[DllImport("ws2_32.dll", SetLastError = true)]
			internal static extern SocketError setsockopt([In] SafeCloseSocket socketHandle, [In] SocketOptionLevel optionLevel, [In] SocketOptionName optionName, [In] ref Linger linger, [In] int optionLength);

			// Token: 0x060040F5 RID: 16629
			[DllImport("ws2_32.dll", SetLastError = true)]
			internal static extern SocketError setsockopt([In] SafeCloseSocket socketHandle, [In] SocketOptionLevel optionLevel, [In] SocketOptionName optionName, [In] ref IPMulticastRequest mreq, [In] int optionLength);

			// Token: 0x060040F6 RID: 16630
			[DllImport("ws2_32.dll", SetLastError = true)]
			internal static extern SocketError setsockopt([In] SafeCloseSocket socketHandle, [In] SocketOptionLevel optionLevel, [In] SocketOptionName optionName, [In] ref IPv6MulticastRequest mreq, [In] int optionLength);

			// Token: 0x060040F7 RID: 16631
			[DllImport("mswsock.dll", SetLastError = true)]
			internal static extern bool TransmitFile([In] SafeCloseSocket socket, [In] SafeHandle fileHandle, [In] int numberOfBytesToWrite, [In] int numberOfBytesPerSend, [In] SafeHandle overlapped, [In] TransmitFileBuffers buffers, [In] TransmitFileOptions flags);

			// Token: 0x060040F8 RID: 16632
			[DllImport("mswsock.dll", EntryPoint = "TransmitFile", SetLastError = true)]
			internal static extern bool TransmitFile2([In] SafeCloseSocket socket, [In] IntPtr fileHandle, [In] int numberOfBytesToWrite, [In] int numberOfBytesPerSend, [In] SafeHandle overlapped, [In] TransmitFileBuffers buffers, [In] TransmitFileOptions flags);

			// Token: 0x060040F9 RID: 16633
			[DllImport("mswsock.dll", EntryPoint = "TransmitFile", SetLastError = true)]
			internal static extern bool TransmitFile_Blocking([In] IntPtr socket, [In] SafeHandle fileHandle, [In] int numberOfBytesToWrite, [In] int numberOfBytesPerSend, [In] SafeHandle overlapped, [In] TransmitFileBuffers buffers, [In] TransmitFileOptions flags);

			// Token: 0x060040FA RID: 16634
			[DllImport("mswsock.dll", EntryPoint = "TransmitFile", SetLastError = true)]
			internal static extern bool TransmitFile_Blocking2([In] IntPtr socket, [In] IntPtr fileHandle, [In] int numberOfBytesToWrite, [In] int numberOfBytesPerSend, [In] SafeHandle overlapped, [In] TransmitFileBuffers buffers, [In] TransmitFileOptions flags);

			// Token: 0x060040FB RID: 16635
			[DllImport("ws2_32.dll", SetLastError = true)]
			internal unsafe static extern int send([In] IntPtr socketHandle, [In] byte* pinnedBuffer, [In] int len, [In] SocketFlags socketFlags);

			// Token: 0x060040FC RID: 16636
			[DllImport("ws2_32.dll", SetLastError = true)]
			internal unsafe static extern int recv([In] IntPtr socketHandle, [In] byte* pinnedBuffer, [In] int len, [In] SocketFlags socketFlags);

			// Token: 0x060040FD RID: 16637
			[DllImport("ws2_32.dll", SetLastError = true)]
			internal static extern SocketError listen([In] SafeCloseSocket socketHandle, [In] int backlog);

			// Token: 0x060040FE RID: 16638
			[DllImport("ws2_32.dll", SetLastError = true)]
			internal static extern SocketError bind([In] SafeCloseSocket socketHandle, [In] byte[] socketAddress, [In] int socketAddressSize);

			// Token: 0x060040FF RID: 16639
			[DllImport("ws2_32.dll", SetLastError = true)]
			internal static extern SocketError shutdown([In] SafeCloseSocket socketHandle, [In] int how);

			// Token: 0x06004100 RID: 16640
			[DllImport("ws2_32.dll", SetLastError = true)]
			internal unsafe static extern int sendto([In] IntPtr socketHandle, [In] byte* pinnedBuffer, [In] int len, [In] SocketFlags socketFlags, [In] byte[] socketAddress, [In] int socketAddressSize);

			// Token: 0x06004101 RID: 16641
			[DllImport("ws2_32.dll", SetLastError = true)]
			internal unsafe static extern int recvfrom([In] IntPtr socketHandle, [In] byte* pinnedBuffer, [In] int len, [In] SocketFlags socketFlags, [Out] byte[] socketAddress, [In] [Out] ref int socketAddressSize);

			// Token: 0x06004102 RID: 16642
			[DllImport("ws2_32.dll", SetLastError = true)]
			internal static extern SocketError getsockname([In] SafeCloseSocket socketHandle, [Out] byte[] socketAddress, [In] [Out] ref int socketAddressSize);

			// Token: 0x06004103 RID: 16643
			[DllImport("ws2_32.dll", SetLastError = true)]
			internal static extern int select([In] int ignoredParameter, [In] [Out] IntPtr[] readfds, [In] [Out] IntPtr[] writefds, [In] [Out] IntPtr[] exceptfds, [In] ref TimeValue timeout);

			// Token: 0x06004104 RID: 16644
			[DllImport("ws2_32.dll", SetLastError = true)]
			internal static extern int select([In] int ignoredParameter, [In] [Out] IntPtr[] readfds, [In] [Out] IntPtr[] writefds, [In] [Out] IntPtr[] exceptfds, [In] IntPtr nullTimeout);

			// Token: 0x06004105 RID: 16645
			[DllImport("ws2_32.dll", SetLastError = true)]
			internal static extern SocketError WSAConnect([In] IntPtr socketHandle, [In] byte[] socketAddress, [In] int socketAddressSize, [In] IntPtr inBuffer, [In] IntPtr outBuffer, [In] IntPtr sQOS, [In] IntPtr gQOS);

			// Token: 0x06004106 RID: 16646
			[DllImport("ws2_32.dll", SetLastError = true)]
			internal static extern SocketError WSASend([In] SafeCloseSocket socketHandle, [In] ref WSABuffer buffer, [In] int bufferCount, out int bytesTransferred, [In] SocketFlags socketFlags, [In] SafeHandle overlapped, [In] IntPtr completionRoutine);

			// Token: 0x06004107 RID: 16647
			[DllImport("ws2_32.dll", SetLastError = true)]
			internal static extern SocketError WSASend([In] SafeCloseSocket socketHandle, [In] WSABuffer[] buffersArray, [In] int bufferCount, out int bytesTransferred, [In] SocketFlags socketFlags, [In] SafeHandle overlapped, [In] IntPtr completionRoutine);

			// Token: 0x06004108 RID: 16648
			[DllImport("ws2_32.dll", EntryPoint = "WSASend", SetLastError = true)]
			internal static extern SocketError WSASend_Blocking([In] IntPtr socketHandle, [In] WSABuffer[] buffersArray, [In] int bufferCount, out int bytesTransferred, [In] SocketFlags socketFlags, [In] SafeHandle overlapped, [In] IntPtr completionRoutine);

			// Token: 0x06004109 RID: 16649
			[DllImport("ws2_32.dll", SetLastError = true)]
			internal static extern SocketError WSASendTo([In] SafeCloseSocket socketHandle, [In] ref WSABuffer buffer, [In] int bufferCount, out int bytesTransferred, [In] SocketFlags socketFlags, [In] IntPtr socketAddress, [In] int socketAddressSize, [In] SafeHandle overlapped, [In] IntPtr completionRoutine);

			// Token: 0x0600410A RID: 16650
			[DllImport("ws2_32.dll", SetLastError = true)]
			internal static extern SocketError WSASendTo([In] SafeCloseSocket socketHandle, [In] WSABuffer[] buffersArray, [In] int bufferCount, out int bytesTransferred, [In] SocketFlags socketFlags, [In] IntPtr socketAddress, [In] int socketAddressSize, [In] SafeNativeOverlapped overlapped, [In] IntPtr completionRoutine);

			// Token: 0x0600410B RID: 16651
			[DllImport("ws2_32.dll", SetLastError = true)]
			internal static extern SocketError WSARecv([In] SafeCloseSocket socketHandle, [In] ref WSABuffer buffer, [In] int bufferCount, out int bytesTransferred, [In] [Out] ref SocketFlags socketFlags, [In] SafeHandle overlapped, [In] IntPtr completionRoutine);

			// Token: 0x0600410C RID: 16652
			[DllImport("ws2_32.dll", SetLastError = true)]
			internal static extern SocketError WSARecv([In] SafeCloseSocket socketHandle, [In] [Out] WSABuffer[] buffers, [In] int bufferCount, out int bytesTransferred, [In] [Out] ref SocketFlags socketFlags, [In] SafeHandle overlapped, [In] IntPtr completionRoutine);

			// Token: 0x0600410D RID: 16653
			[DllImport("ws2_32.dll", EntryPoint = "WSARecv", SetLastError = true)]
			internal static extern SocketError WSARecv_Blocking([In] IntPtr socketHandle, [In] [Out] WSABuffer[] buffers, [In] int bufferCount, out int bytesTransferred, [In] [Out] ref SocketFlags socketFlags, [In] SafeHandle overlapped, [In] IntPtr completionRoutine);

			// Token: 0x0600410E RID: 16654
			[DllImport("ws2_32.dll", SetLastError = true)]
			internal static extern SocketError WSARecvFrom([In] SafeCloseSocket socketHandle, [In] ref WSABuffer buffer, [In] int bufferCount, out int bytesTransferred, [In] [Out] ref SocketFlags socketFlags, [In] IntPtr socketAddressPointer, [In] IntPtr socketAddressSizePointer, [In] SafeHandle overlapped, [In] IntPtr completionRoutine);

			// Token: 0x0600410F RID: 16655
			[DllImport("ws2_32.dll", SetLastError = true)]
			internal static extern SocketError WSARecvFrom([In] SafeCloseSocket socketHandle, [In] [Out] WSABuffer[] buffers, [In] int bufferCount, out int bytesTransferred, [In] [Out] ref SocketFlags socketFlags, [In] IntPtr socketAddressPointer, [In] IntPtr socketAddressSizePointer, [In] SafeNativeOverlapped overlapped, [In] IntPtr completionRoutine);

			// Token: 0x06004110 RID: 16656
			[DllImport("ws2_32.dll", SetLastError = true)]
			internal static extern SocketError WSAEventSelect([In] SafeCloseSocket socketHandle, [In] SafeHandle Event, [In] AsyncEventBits NetworkEvents);

			// Token: 0x06004111 RID: 16657
			[DllImport("ws2_32.dll", SetLastError = true)]
			internal static extern SocketError WSAEventSelect([In] SafeCloseSocket socketHandle, [In] IntPtr Event, [In] AsyncEventBits NetworkEvents);

			// Token: 0x06004112 RID: 16658
			[DllImport("ws2_32.dll", SetLastError = true)]
			internal static extern SocketError WSAIoctl([In] SafeCloseSocket socketHandle, [In] int ioControlCode, [In] [Out] ref Guid guid, [In] int guidSize, out IntPtr funcPtr, [In] int funcPtrSize, out int bytesTransferred, [In] IntPtr shouldBeNull, [In] IntPtr shouldBeNull2);

			// Token: 0x06004113 RID: 16659
			[DllImport("ws2_32.dll", EntryPoint = "WSAIoctl", SetLastError = true)]
			internal static extern SocketError WSAIoctl_Blocking([In] IntPtr socketHandle, [In] int ioControlCode, [In] byte[] inBuffer, [In] int inBufferSize, [Out] byte[] outBuffer, [In] int outBufferSize, out int bytesTransferred, [In] SafeHandle overlapped, [In] IntPtr completionRoutine);

			// Token: 0x06004114 RID: 16660
			[DllImport("ws2_32.dll", EntryPoint = "WSAIoctl", SetLastError = true)]
			internal static extern SocketError WSAIoctl_Blocking_Internal([In] IntPtr socketHandle, [In] uint ioControlCode, [In] IntPtr inBuffer, [In] int inBufferSize, [Out] IntPtr outBuffer, [In] int outBufferSize, out int bytesTransferred, [In] SafeHandle overlapped, [In] IntPtr completionRoutine);

			// Token: 0x06004115 RID: 16661
			[DllImport("ws2_32.dll", SetLastError = true)]
			internal static extern SocketError WSAEnumNetworkEvents([In] SafeCloseSocket socketHandle, [In] SafeWaitHandle Event, [In] [Out] ref NetworkEvents networkEvents);

			// Token: 0x06004116 RID: 16662
			[DllImport("ws2_32.dll", SetLastError = true)]
			internal unsafe static extern int WSADuplicateSocket([In] SafeCloseSocket socketHandle, [In] uint targetProcessID, [In] byte* pinnedBuffer);

			// Token: 0x06004117 RID: 16663
			[DllImport("ws2_32.dll", SetLastError = true)]
			internal static extern bool WSAGetOverlappedResult([In] SafeCloseSocket socketHandle, [In] SafeHandle overlapped, out uint bytesTransferred, [In] bool wait, out SocketFlags socketFlags);

			// Token: 0x06004118 RID: 16664
			[DllImport("ws2_32.dll", BestFitMapping = false, CharSet = CharSet.Unicode, SetLastError = true, ThrowOnUnmappableChar = false)]
			internal static extern SocketError WSAStringToAddress([In] string addressString, [In] AddressFamily addressFamily, [In] IntPtr lpProtocolInfo, [Out] byte[] socketAddress, [In] [Out] ref int socketAddressSize);

			// Token: 0x06004119 RID: 16665
			[DllImport("ws2_32.dll", BestFitMapping = false, CharSet = CharSet.Ansi, SetLastError = true, ThrowOnUnmappableChar = true)]
			internal static extern SocketError WSAAddressToString([In] byte[] socketAddress, [In] int socketAddressSize, [In] IntPtr lpProtocolInfo, [Out] StringBuilder addressString, [In] [Out] ref int addressStringLength);

			// Token: 0x0600411A RID: 16666
			[DllImport("ws2_32.dll", BestFitMapping = false, CharSet = CharSet.Unicode, SetLastError = true, ThrowOnUnmappableChar = true)]
			internal static extern SocketError GetNameInfoW([In] byte[] sa, [In] int salen, [In] [Out] StringBuilder host, [In] int hostlen, [In] [Out] StringBuilder serv, [In] int servlen, [In] int flags);

			// Token: 0x0600411B RID: 16667
			[DllImport("ws2_32.dll", CharSet = CharSet.Auto, SetLastError = true)]
			internal static extern int WSAEnumProtocols([MarshalAs(UnmanagedType.LPArray)] [In] int[] lpiProtocols, [In] SafeLocalFree lpProtocolBuffer, [In] [Out] ref uint lpdwBufferLength);

			// Token: 0x04003150 RID: 12624
			private const string WS2_32 = "ws2_32.dll";

			// Token: 0x04003151 RID: 12625
			private const string mswsock = "mswsock.dll";

			// Token: 0x020008D2 RID: 2258
			[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
			internal struct WSAPROTOCOLCHAIN
			{
				// Token: 0x04003BA6 RID: 15270
				internal int ChainLen;

				// Token: 0x04003BA7 RID: 15271
				[MarshalAs(UnmanagedType.ByValArray, SizeConst = 7)]
				internal uint[] ChainEntries;
			}

			// Token: 0x020008D3 RID: 2259
			[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
			internal struct WSAPROTOCOL_INFO
			{
				// Token: 0x04003BA8 RID: 15272
				internal uint dwServiceFlags1;

				// Token: 0x04003BA9 RID: 15273
				internal uint dwServiceFlags2;

				// Token: 0x04003BAA RID: 15274
				internal uint dwServiceFlags3;

				// Token: 0x04003BAB RID: 15275
				internal uint dwServiceFlags4;

				// Token: 0x04003BAC RID: 15276
				internal uint dwProviderFlags;

				// Token: 0x04003BAD RID: 15277
				private Guid ProviderId;

				// Token: 0x04003BAE RID: 15278
				internal uint dwCatalogEntryId;

				// Token: 0x04003BAF RID: 15279
				private UnsafeNclNativeMethods.OSSOCK.WSAPROTOCOLCHAIN ProtocolChain;

				// Token: 0x04003BB0 RID: 15280
				internal int iVersion;

				// Token: 0x04003BB1 RID: 15281
				internal AddressFamily iAddressFamily;

				// Token: 0x04003BB2 RID: 15282
				internal int iMaxSockAddr;

				// Token: 0x04003BB3 RID: 15283
				internal int iMinSockAddr;

				// Token: 0x04003BB4 RID: 15284
				internal int iSocketType;

				// Token: 0x04003BB5 RID: 15285
				internal int iProtocol;

				// Token: 0x04003BB6 RID: 15286
				internal int iProtocolMaxOffset;

				// Token: 0x04003BB7 RID: 15287
				internal int iNetworkByteOrder;

				// Token: 0x04003BB8 RID: 15288
				internal int iSecurityScheme;

				// Token: 0x04003BB9 RID: 15289
				internal uint dwMessageSize;

				// Token: 0x04003BBA RID: 15290
				internal uint dwProviderReserved;

				// Token: 0x04003BBB RID: 15291
				[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
				internal string szProtocol;
			}

			// Token: 0x020008D4 RID: 2260
			internal struct ControlData
			{
				// Token: 0x04003BBC RID: 15292
				internal UIntPtr length;

				// Token: 0x04003BBD RID: 15293
				internal uint level;

				// Token: 0x04003BBE RID: 15294
				internal uint type;

				// Token: 0x04003BBF RID: 15295
				internal uint address;

				// Token: 0x04003BC0 RID: 15296
				internal uint index;
			}

			// Token: 0x020008D5 RID: 2261
			internal struct ControlDataIPv6
			{
				// Token: 0x04003BC1 RID: 15297
				internal UIntPtr length;

				// Token: 0x04003BC2 RID: 15298
				internal uint level;

				// Token: 0x04003BC3 RID: 15299
				internal uint type;

				// Token: 0x04003BC4 RID: 15300
				[MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
				internal byte[] address;

				// Token: 0x04003BC5 RID: 15301
				internal uint index;
			}

			// Token: 0x020008D6 RID: 2262
			internal struct WSAMsg
			{
				// Token: 0x04003BC6 RID: 15302
				internal IntPtr socketAddress;

				// Token: 0x04003BC7 RID: 15303
				internal uint addressLength;

				// Token: 0x04003BC8 RID: 15304
				internal IntPtr buffers;

				// Token: 0x04003BC9 RID: 15305
				internal uint count;

				// Token: 0x04003BCA RID: 15306
				internal WSABuffer controlBuffer;

				// Token: 0x04003BCB RID: 15307
				internal SocketFlags flags;
			}

			// Token: 0x020008D7 RID: 2263
			[Flags]
			internal enum TransmitPacketsElementFlags : uint
			{
				// Token: 0x04003BCD RID: 15309
				None = 0U,
				// Token: 0x04003BCE RID: 15310
				Memory = 1U,
				// Token: 0x04003BCF RID: 15311
				File = 2U,
				// Token: 0x04003BD0 RID: 15312
				EndOfPacket = 4U
			}

			// Token: 0x020008D8 RID: 2264
			[StructLayout(LayoutKind.Explicit)]
			internal struct TransmitPacketsElement
			{
				// Token: 0x04003BD1 RID: 15313
				[FieldOffset(0)]
				internal UnsafeNclNativeMethods.OSSOCK.TransmitPacketsElementFlags flags;

				// Token: 0x04003BD2 RID: 15314
				[FieldOffset(4)]
				internal uint length;

				// Token: 0x04003BD3 RID: 15315
				[FieldOffset(8)]
				internal long fileOffset;

				// Token: 0x04003BD4 RID: 15316
				[FieldOffset(8)]
				internal IntPtr buffer;

				// Token: 0x04003BD5 RID: 15317
				[FieldOffset(16)]
				internal IntPtr fileHandle;
			}

			// Token: 0x020008D9 RID: 2265
			internal struct SOCKET_ADDRESS
			{
				// Token: 0x04003BD6 RID: 15318
				internal IntPtr lpSockAddr;

				// Token: 0x04003BD7 RID: 15319
				internal int iSockaddrLength;
			}

			// Token: 0x020008DA RID: 2266
			internal struct SOCKET_ADDRESS_LIST
			{
				// Token: 0x04003BD8 RID: 15320
				internal int iAddressCount;

				// Token: 0x04003BD9 RID: 15321
				internal UnsafeNclNativeMethods.OSSOCK.SOCKET_ADDRESS Addresses;
			}

			// Token: 0x020008DB RID: 2267
			internal struct TransmitFileBuffersStruct
			{
				// Token: 0x04003BDA RID: 15322
				internal IntPtr preBuffer;

				// Token: 0x04003BDB RID: 15323
				internal int preBufferLength;

				// Token: 0x04003BDC RID: 15324
				internal IntPtr postBuffer;

				// Token: 0x04003BDD RID: 15325
				internal int postBufferLength;
			}
		}

		// Token: 0x0200071C RID: 1820
		[SuppressUnmanagedCodeSecurity]
		internal static class NativePKI
		{
			// Token: 0x0600411C RID: 16668
			[DllImport("crypt32.dll", ExactSpelling = true, SetLastError = true)]
			internal static extern int CertVerifyCertificateChainPolicy([In] IntPtr policy, [In] SafeFreeCertChain chainContext, [In] ref ChainPolicyParameter cpp, [In] [Out] ref ChainPolicyStatus ps);

			// Token: 0x0600411D RID: 16669
			[DllImport("crypt32.dll", ExactSpelling = true, SetLastError = true)]
			private static extern bool CertSelectCertificateChains([In] IntPtr pSelectionContext, [In] UnsafeNclNativeMethods.NativePKI.CertificateSelect flags, [In] IntPtr pChainParameters, [In] int cCriteria, [In] SafeCertSelectCritera rgpCriteria, [In] IntPtr hStore, out int pcSelection, out SafeFreeCertChainList pprgpSelection);

			// Token: 0x0600411E RID: 16670 RVA: 0x0010F64C File Offset: 0x0010D84C
			[FriendAccessAllowed]
			internal static X509CertificateCollection FindClientCertificates()
			{
				if (!ComNetOS.IsWin7orLater)
				{
					throw new PlatformNotSupportedException();
				}
				X509CertificateCollection x509CertificateCollection = new X509CertificateCollection();
				X509Store x509Store = new X509Store(StoreName.My, StoreLocation.CurrentUser);
				x509Store.Open(OpenFlags.MaxAllowed);
				int num = 0;
				SafeFreeCertChainList safeFreeCertChainList = null;
				SafeCertSelectCritera safeCertSelectCritera = new SafeCertSelectCritera();
				try
				{
					if (!UnsafeNclNativeMethods.NativePKI.CertSelectCertificateChains(IntPtr.Zero, UnsafeNclNativeMethods.NativePKI.CertificateSelect.HasPrivateKey, IntPtr.Zero, safeCertSelectCritera.Count, safeCertSelectCritera, x509Store.StoreHandle, out num, out safeFreeCertChainList))
					{
						throw new Win32Exception();
					}
					for (int i = 0; i < num; i++)
					{
						using (SafeFreeCertChain safeFreeCertChain = new SafeFreeCertChain(Marshal.ReadIntPtr(safeFreeCertChainList.DangerousGetHandle() + i * Marshal.SizeOf(typeof(IntPtr))), true))
						{
							X509Chain x509Chain = new X509Chain(safeFreeCertChain.DangerousGetHandle());
							if (x509Chain.ChainElements.Count > 0)
							{
								X509Certificate2 certificate = x509Chain.ChainElements[0].Certificate;
								x509CertificateCollection.Add(certificate);
							}
							x509Chain.Reset();
						}
					}
				}
				finally
				{
					x509Store.Close();
					safeFreeCertChainList.Dispose();
					safeCertSelectCritera.Dispose();
				}
				return x509CertificateCollection;
			}

			// Token: 0x020008DC RID: 2268
			internal struct CRYPT_OBJID_BLOB
			{
				// Token: 0x04003BDE RID: 15326
				public uint cbData;

				// Token: 0x04003BDF RID: 15327
				public IntPtr pbData;
			}

			// Token: 0x020008DD RID: 2269
			internal struct CERT_EXTENSION
			{
				// Token: 0x04003BE0 RID: 15328
				public IntPtr pszObjId;

				// Token: 0x04003BE1 RID: 15329
				public uint fCritical;

				// Token: 0x04003BE2 RID: 15330
				public UnsafeNclNativeMethods.NativePKI.CRYPT_OBJID_BLOB Value;
			}

			// Token: 0x020008DE RID: 2270
			internal struct CERT_SELECT_CRITERIA
			{
				// Token: 0x04003BE3 RID: 15331
				public uint dwType;

				// Token: 0x04003BE4 RID: 15332
				public uint cPara;

				// Token: 0x04003BE5 RID: 15333
				public IntPtr ppPara;
			}

			// Token: 0x020008DF RID: 2271
			[Flags]
			private enum CertificateSelect
			{
				// Token: 0x04003BE7 RID: 15335
				None = 0,
				// Token: 0x04003BE8 RID: 15336
				AllowExpired = 1,
				// Token: 0x04003BE9 RID: 15337
				TrustedRoot = 2,
				// Token: 0x04003BEA RID: 15338
				DisallowSelfsigned = 4,
				// Token: 0x04003BEB RID: 15339
				HasPrivateKey = 8,
				// Token: 0x04003BEC RID: 15340
				HasKeyForSignature = 16,
				// Token: 0x04003BED RID: 15341
				HasKeyForKeyExchange = 32,
				// Token: 0x04003BEE RID: 15342
				HardwareOnly = 64,
				// Token: 0x04003BEF RID: 15343
				AllowDuplicates = 128
			}
		}

		// Token: 0x0200071D RID: 1821
		[SuppressUnmanagedCodeSecurity]
		internal static class NativeNTSSPI
		{
			// Token: 0x0600411F RID: 16671
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
			[DllImport("secur32.dll", ExactSpelling = true, SetLastError = true)]
			internal static extern int EncryptMessage(ref SSPIHandle contextHandle, [In] uint qualityOfProtection, [In] [Out] SecurityBufferDescriptor inputOutput, [In] uint sequenceNumber);

			// Token: 0x06004120 RID: 16672
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
			[DllImport("secur32.dll", ExactSpelling = true, SetLastError = true)]
			internal unsafe static extern int DecryptMessage([In] ref SSPIHandle contextHandle, [In] [Out] SecurityBufferDescriptor inputOutput, [In] uint sequenceNumber, uint* qualityOfProtection);
		}

		// Token: 0x0200071E RID: 1822
		[SuppressUnmanagedCodeSecurity]
		internal static class WinHttp
		{
			// Token: 0x06004121 RID: 16673
			[DllImport("winhttp.dll", ExactSpelling = true, SetLastError = true)]
			internal static extern bool WinHttpDetectAutoProxyConfigUrl(UnsafeNclNativeMethods.WinHttp.AutoDetectType autoDetectFlags, out SafeGlobalFree autoConfigUrl);

			// Token: 0x06004122 RID: 16674
			[DllImport("winhttp.dll", SetLastError = true)]
			internal static extern bool WinHttpGetIEProxyConfigForCurrentUser(ref UnsafeNclNativeMethods.WinHttp.WINHTTP_CURRENT_USER_IE_PROXY_CONFIG proxyConfig);

			// Token: 0x06004123 RID: 16675
			[DllImport("winhttp.dll", CharSet = CharSet.Unicode, SetLastError = true)]
			internal static extern SafeInternetHandle WinHttpOpen(string userAgent, UnsafeNclNativeMethods.WinHttp.AccessType accessType, string proxyName, string proxyBypass, int dwFlags);

			// Token: 0x06004124 RID: 16676
			[DllImport("winhttp.dll", CharSet = CharSet.Unicode, SetLastError = true)]
			internal static extern bool WinHttpSetTimeouts(SafeInternetHandle session, int resolveTimeout, int connectTimeout, int sendTimeout, int receiveTimeout);

			// Token: 0x06004125 RID: 16677
			[DllImport("winhttp.dll", CharSet = CharSet.Unicode, SetLastError = true)]
			internal static extern bool WinHttpGetProxyForUrl(SafeInternetHandle session, string url, [In] ref UnsafeNclNativeMethods.WinHttp.WINHTTP_AUTOPROXY_OPTIONS autoProxyOptions, out UnsafeNclNativeMethods.WinHttp.WINHTTP_PROXY_INFO proxyInfo);

			// Token: 0x06004126 RID: 16678
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
			[DllImport("winhttp.dll", CharSet = CharSet.Unicode, SetLastError = true)]
			internal static extern bool WinHttpCloseHandle(IntPtr httpSession);

			// Token: 0x020008E0 RID: 2272
			[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
			internal struct WINHTTP_CURRENT_USER_IE_PROXY_CONFIG
			{
				// Token: 0x04003BF0 RID: 15344
				public bool AutoDetect;

				// Token: 0x04003BF1 RID: 15345
				public IntPtr AutoConfigUrl;

				// Token: 0x04003BF2 RID: 15346
				public IntPtr Proxy;

				// Token: 0x04003BF3 RID: 15347
				public IntPtr ProxyBypass;
			}

			// Token: 0x020008E1 RID: 2273
			[Flags]
			internal enum AutoProxyFlags
			{
				// Token: 0x04003BF5 RID: 15349
				AutoDetect = 1,
				// Token: 0x04003BF6 RID: 15350
				AutoProxyConfigUrl = 2,
				// Token: 0x04003BF7 RID: 15351
				RunInProcess = 65536,
				// Token: 0x04003BF8 RID: 15352
				RunOutProcessOnly = 131072
			}

			// Token: 0x020008E2 RID: 2274
			internal enum AccessType
			{
				// Token: 0x04003BFA RID: 15354
				DefaultProxy,
				// Token: 0x04003BFB RID: 15355
				NoProxy,
				// Token: 0x04003BFC RID: 15356
				NamedProxy = 3
			}

			// Token: 0x020008E3 RID: 2275
			[Flags]
			internal enum AutoDetectType
			{
				// Token: 0x04003BFE RID: 15358
				None = 0,
				// Token: 0x04003BFF RID: 15359
				Dhcp = 1,
				// Token: 0x04003C00 RID: 15360
				DnsA = 2
			}

			// Token: 0x020008E4 RID: 2276
			[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
			internal struct WINHTTP_AUTOPROXY_OPTIONS
			{
				// Token: 0x04003C01 RID: 15361
				public UnsafeNclNativeMethods.WinHttp.AutoProxyFlags Flags;

				// Token: 0x04003C02 RID: 15362
				public UnsafeNclNativeMethods.WinHttp.AutoDetectType AutoDetectFlags;

				// Token: 0x04003C03 RID: 15363
				[MarshalAs(UnmanagedType.LPWStr)]
				public string AutoConfigUrl;

				// Token: 0x04003C04 RID: 15364
				private IntPtr lpvReserved;

				// Token: 0x04003C05 RID: 15365
				private int dwReserved;

				// Token: 0x04003C06 RID: 15366
				public bool AutoLogonIfChallenged;
			}

			// Token: 0x020008E5 RID: 2277
			[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
			internal struct WINHTTP_PROXY_INFO
			{
				// Token: 0x04003C07 RID: 15367
				public UnsafeNclNativeMethods.WinHttp.AccessType AccessType;

				// Token: 0x04003C08 RID: 15368
				public IntPtr Proxy;

				// Token: 0x04003C09 RID: 15369
				public IntPtr ProxyBypass;
			}

			// Token: 0x020008E6 RID: 2278
			internal enum ErrorCodes
			{
				// Token: 0x04003C0B RID: 15371
				Success,
				// Token: 0x04003C0C RID: 15372
				OutOfHandles = 12001,
				// Token: 0x04003C0D RID: 15373
				Timeout,
				// Token: 0x04003C0E RID: 15374
				InternalError = 12004,
				// Token: 0x04003C0F RID: 15375
				InvalidUrl,
				// Token: 0x04003C10 RID: 15376
				UnrecognizedScheme,
				// Token: 0x04003C11 RID: 15377
				NameNotResolved,
				// Token: 0x04003C12 RID: 15378
				InvalidOption = 12009,
				// Token: 0x04003C13 RID: 15379
				OptionNotSettable = 12011,
				// Token: 0x04003C14 RID: 15380
				Shutdown,
				// Token: 0x04003C15 RID: 15381
				LoginFailure = 12015,
				// Token: 0x04003C16 RID: 15382
				OperationCancelled = 12017,
				// Token: 0x04003C17 RID: 15383
				IncorrectHandleType,
				// Token: 0x04003C18 RID: 15384
				IncorrectHandleState,
				// Token: 0x04003C19 RID: 15385
				CannotConnect = 12029,
				// Token: 0x04003C1A RID: 15386
				ConnectionError,
				// Token: 0x04003C1B RID: 15387
				ResendRequest = 12032,
				// Token: 0x04003C1C RID: 15388
				AuthCertNeeded = 12044,
				// Token: 0x04003C1D RID: 15389
				CannotCallBeforeOpen = 12100,
				// Token: 0x04003C1E RID: 15390
				CannotCallBeforeSend,
				// Token: 0x04003C1F RID: 15391
				CannotCallAfterSend,
				// Token: 0x04003C20 RID: 15392
				CannotCallAfterOpen,
				// Token: 0x04003C21 RID: 15393
				HeaderNotFound = 12150,
				// Token: 0x04003C22 RID: 15394
				InvalidServerResponse = 12152,
				// Token: 0x04003C23 RID: 15395
				InvalidHeader,
				// Token: 0x04003C24 RID: 15396
				InvalidQueryRequest,
				// Token: 0x04003C25 RID: 15397
				HeaderAlreadyExists,
				// Token: 0x04003C26 RID: 15398
				RedirectFailed,
				// Token: 0x04003C27 RID: 15399
				AutoProxyServiceError = 12178,
				// Token: 0x04003C28 RID: 15400
				BadAutoProxyScript = 12166,
				// Token: 0x04003C29 RID: 15401
				UnableToDownloadScript,
				// Token: 0x04003C2A RID: 15402
				NotInitialized = 12172,
				// Token: 0x04003C2B RID: 15403
				SecureFailure = 12175,
				// Token: 0x04003C2C RID: 15404
				SecureCertDateInvalid = 12037,
				// Token: 0x04003C2D RID: 15405
				SecureCertCNInvalid,
				// Token: 0x04003C2E RID: 15406
				SecureInvalidCA = 12045,
				// Token: 0x04003C2F RID: 15407
				SecureCertRevFailed = 12057,
				// Token: 0x04003C30 RID: 15408
				SecureChannelError = 12157,
				// Token: 0x04003C31 RID: 15409
				SecureInvalidCert = 12169,
				// Token: 0x04003C32 RID: 15410
				SecureCertRevoked,
				// Token: 0x04003C33 RID: 15411
				SecureCertWrongUsage = 12179,
				// Token: 0x04003C34 RID: 15412
				AudodetectionFailed,
				// Token: 0x04003C35 RID: 15413
				HeaderCountExceeded,
				// Token: 0x04003C36 RID: 15414
				HeaderSizeOverflow,
				// Token: 0x04003C37 RID: 15415
				ChunkedEncodingHeaderSizeOverflow,
				// Token: 0x04003C38 RID: 15416
				ResponseDrainOverflow,
				// Token: 0x04003C39 RID: 15417
				ClientCertNoPrivateKey,
				// Token: 0x04003C3A RID: 15418
				ClientCertNoAccessPrivateKey
			}
		}

		// Token: 0x0200071F RID: 1823
		[SuppressUnmanagedCodeSecurity]
		internal static class UnsafeWinInetCache
		{
			// Token: 0x06004127 RID: 16679
			[DllImport("wininet.dll", CharSet = CharSet.Unicode, ExactSpelling = true, SetLastError = true)]
			internal static extern bool CreateUrlCacheEntryW([In] string urlName, [In] int expectedFileSize, [In] string fileExtension, [Out] StringBuilder fileName, [In] int dwReserved);

			// Token: 0x06004128 RID: 16680
			[DllImport("wininet.dll", CharSet = CharSet.Unicode, ExactSpelling = true, SetLastError = true)]
			internal unsafe static extern bool CommitUrlCacheEntryW([In] string urlName, [In] string localFileName, [In] _WinInetCache.FILETIME expireTime, [In] _WinInetCache.FILETIME lastModifiedTime, [In] _WinInetCache.EntryType EntryType, [In] byte* headerInfo, [In] int headerSizeTChars, [In] string fileExtension, [In] string originalUrl);

			// Token: 0x06004129 RID: 16681
			[DllImport("wininet.dll", CharSet = CharSet.Unicode, ExactSpelling = true, SetLastError = true)]
			internal unsafe static extern bool GetUrlCacheEntryInfoW([In] string urlName, [In] byte* entryPtr, [In] [Out] ref int bufferSz);

			// Token: 0x0600412A RID: 16682
			[DllImport("wininet.dll", CharSet = CharSet.Unicode, ExactSpelling = true, SetLastError = true)]
			internal unsafe static extern bool SetUrlCacheEntryInfoW([In] string lpszUrlName, [In] byte* EntryPtr, [In] _WinInetCache.Entry_FC fieldControl);

			// Token: 0x0600412B RID: 16683
			[DllImport("wininet.dll", CharSet = CharSet.Unicode, ExactSpelling = true, SetLastError = true)]
			internal static extern bool DeleteUrlCacheEntryW([In] string urlName);

			// Token: 0x0600412C RID: 16684
			[DllImport("wininet.dll", CharSet = CharSet.Unicode, ExactSpelling = true, SetLastError = true)]
			internal static extern bool UnlockUrlCacheEntryFileW([In] string urlName, [In] int dwReserved);

			// Token: 0x04003152 RID: 12626
			public const int MAX_PATH = 260;
		}

		// Token: 0x02000720 RID: 1824
		[SuppressUnmanagedCodeSecurity]
		internal static class SspiHelper
		{
			// Token: 0x0600412D RID: 16685
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
			[DllImport("secur32.dll", ExactSpelling = true, SetLastError = true)]
			internal static extern SecurityStatus SspiFreeAuthIdentity([In] IntPtr authData);

			// Token: 0x0600412E RID: 16686
			[DllImport("secur32.dll", CharSet = CharSet.Unicode, ExactSpelling = true, SetLastError = true)]
			internal static extern SecurityStatus SspiEncodeStringsAsAuthIdentity([In] string userName, [In] string domainName, [In] string password, out SafeSspiAuthDataHandle authData);
		}

		// Token: 0x02000721 RID: 1825
		[SuppressUnmanagedCodeSecurity]
		internal static class HttpApi
		{
			// Token: 0x0600412F RID: 16687
			[DllImport("httpapi.dll", CallingConvention = CallingConvention.StdCall, ExactSpelling = true, SetLastError = true)]
			internal unsafe static extern uint HttpInitialize(UnsafeNclNativeMethods.HttpApi.HTTPAPI_VERSION version, uint flags, void* pReserved);

			// Token: 0x06004130 RID: 16688
			[DllImport("httpapi.dll", CallingConvention = CallingConvention.StdCall, ExactSpelling = true, SetLastError = true)]
			internal unsafe static extern uint HttpReceiveRequestEntityBody(CriticalHandle requestQueueHandle, ulong requestId, uint flags, void* pEntityBuffer, uint entityBufferLength, out uint bytesReturned, NativeOverlapped* pOverlapped);

			// Token: 0x06004131 RID: 16689
			[DllImport("httpapi.dll", CallingConvention = CallingConvention.StdCall, EntryPoint = "HttpReceiveRequestEntityBody", ExactSpelling = true, SetLastError = true)]
			internal unsafe static extern uint HttpReceiveRequestEntityBody2(CriticalHandle requestQueueHandle, ulong requestId, uint flags, void* pEntityBuffer, uint entityBufferLength, out uint bytesReturned, [In] SafeHandle pOverlapped);

			// Token: 0x06004132 RID: 16690
			[DllImport("httpapi.dll", CallingConvention = CallingConvention.StdCall, ExactSpelling = true, SetLastError = true)]
			internal unsafe static extern uint HttpReceiveClientCertificate(CriticalHandle requestQueueHandle, ulong connectionId, uint flags, UnsafeNclNativeMethods.HttpApi.HTTP_SSL_CLIENT_CERT_INFO* pSslClientCertInfo, uint sslClientCertInfoSize, uint* pBytesReceived, NativeOverlapped* pOverlapped);

			// Token: 0x06004133 RID: 16691
			[DllImport("httpapi.dll", CallingConvention = CallingConvention.StdCall, ExactSpelling = true, SetLastError = true)]
			internal unsafe static extern uint HttpReceiveClientCertificate(CriticalHandle requestQueueHandle, ulong connectionId, uint flags, byte* pSslClientCertInfo, uint sslClientCertInfoSize, uint* pBytesReceived, NativeOverlapped* pOverlapped);

			// Token: 0x06004134 RID: 16692
			[DllImport("httpapi.dll", CallingConvention = CallingConvention.StdCall, ExactSpelling = true, SetLastError = true)]
			internal unsafe static extern uint HttpReceiveHttpRequest(CriticalHandle requestQueueHandle, ulong requestId, uint flags, UnsafeNclNativeMethods.HttpApi.HTTP_REQUEST* pRequestBuffer, uint requestBufferLength, uint* pBytesReturned, NativeOverlapped* pOverlapped);

			// Token: 0x06004135 RID: 16693
			[DllImport("httpapi.dll", CallingConvention = CallingConvention.StdCall, ExactSpelling = true, SetLastError = true)]
			internal unsafe static extern uint HttpSendHttpResponse(CriticalHandle requestQueueHandle, ulong requestId, uint flags, UnsafeNclNativeMethods.HttpApi.HTTP_RESPONSE* pHttpResponse, void* pCachePolicy, uint* pBytesSent, SafeLocalFree pRequestBuffer, uint requestBufferLength, NativeOverlapped* pOverlapped, void* pLogData);

			// Token: 0x06004136 RID: 16694
			[DllImport("httpapi.dll", CallingConvention = CallingConvention.StdCall, ExactSpelling = true, SetLastError = true)]
			internal unsafe static extern uint HttpSendResponseEntityBody(CriticalHandle requestQueueHandle, ulong requestId, uint flags, ushort entityChunkCount, UnsafeNclNativeMethods.HttpApi.HTTP_DATA_CHUNK* pEntityChunks, uint* pBytesSent, SafeLocalFree pRequestBuffer, uint requestBufferLength, NativeOverlapped* pOverlapped, void* pLogData);

			// Token: 0x06004137 RID: 16695
			[DllImport("httpapi.dll", CallingConvention = CallingConvention.StdCall, ExactSpelling = true, SetLastError = true)]
			internal static extern uint HttpCancelHttpRequest(CriticalHandle requestQueueHandle, ulong requestId, IntPtr pOverlapped);

			// Token: 0x06004138 RID: 16696
			[DllImport("httpapi.dll", CallingConvention = CallingConvention.StdCall, EntryPoint = "HttpSendResponseEntityBody", ExactSpelling = true, SetLastError = true)]
			internal static extern uint HttpSendResponseEntityBody2(CriticalHandle requestQueueHandle, ulong requestId, uint flags, ushort entityChunkCount, IntPtr pEntityChunks, out uint pBytesSent, SafeLocalFree pRequestBuffer, uint requestBufferLength, SafeHandle pOverlapped, IntPtr pLogData);

			// Token: 0x06004139 RID: 16697
			[DllImport("httpapi.dll", CallingConvention = CallingConvention.StdCall, ExactSpelling = true, SetLastError = true)]
			internal unsafe static extern uint HttpWaitForDisconnect(CriticalHandle requestQueueHandle, ulong connectionId, NativeOverlapped* pOverlapped);

			// Token: 0x0600413A RID: 16698
			[DllImport("httpapi.dll", CallingConvention = CallingConvention.StdCall, ExactSpelling = true, SetLastError = true)]
			internal unsafe static extern uint HttpCreateServerSession(UnsafeNclNativeMethods.HttpApi.HTTPAPI_VERSION version, ulong* serverSessionId, uint reserved);

			// Token: 0x0600413B RID: 16699
			[DllImport("httpapi.dll", CallingConvention = CallingConvention.StdCall, ExactSpelling = true, SetLastError = true)]
			internal unsafe static extern uint HttpCreateUrlGroup(ulong serverSessionId, ulong* urlGroupId, uint reserved);

			// Token: 0x0600413C RID: 16700
			[DllImport("httpapi.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode, ExactSpelling = true, SetLastError = true)]
			internal static extern uint HttpAddUrlToUrlGroup(ulong urlGroupId, string pFullyQualifiedUrl, ulong context, uint pReserved);

			// Token: 0x0600413D RID: 16701
			[DllImport("httpapi.dll", CallingConvention = CallingConvention.StdCall, ExactSpelling = true, SetLastError = true)]
			internal static extern uint HttpSetUrlGroupProperty(ulong urlGroupId, UnsafeNclNativeMethods.HttpApi.HTTP_SERVER_PROPERTY serverProperty, IntPtr pPropertyInfo, uint propertyInfoLength);

			// Token: 0x0600413E RID: 16702
			[DllImport("httpapi.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode, ExactSpelling = true, SetLastError = true)]
			internal static extern uint HttpRemoveUrlFromUrlGroup(ulong urlGroupId, string pFullyQualifiedUrl, uint flags);

			// Token: 0x0600413F RID: 16703
			[DllImport("httpapi.dll", CallingConvention = CallingConvention.StdCall, ExactSpelling = true, SetLastError = true)]
			internal static extern uint HttpCloseServerSession(ulong serverSessionId);

			// Token: 0x06004140 RID: 16704
			[DllImport("httpapi.dll", CallingConvention = CallingConvention.StdCall, ExactSpelling = true, SetLastError = true)]
			internal static extern uint HttpCloseUrlGroup(ulong urlGroupId);

			// Token: 0x06004141 RID: 16705
			[DllImport("tokenbinding.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode)]
			public unsafe static extern int TokenBindingVerifyMessage([In] byte* tokenBindingMessage, [In] uint tokenBindingMessageSize, [In] UnsafeNclNativeMethods.HttpApi.TOKENBINDING_KEY_PARAMETERS_TYPE keyType, [In] byte* tlsUnique, [In] uint tlsUniqueSize, out UnsafeNclNativeMethods.HttpApi.HeapAllocHandle resultList);

			// Token: 0x06004142 RID: 16706
			[DllImport("tokenbinding.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode, EntryPoint = "TokenBindingVerifyMessage")]
			public unsafe static extern int TokenBindingVerifyMessage_V1([In] byte* tokenBindingMessage, [In] uint tokenBindingMessageSize, [In] IntPtr keyType, [In] byte* tlsUnique, [In] uint tlsUniqueSize, out UnsafeNclNativeMethods.HttpApi.HeapAllocHandle resultList);

			// Token: 0x17000EF7 RID: 3831
			// (get) Token: 0x06004143 RID: 16707 RVA: 0x0010F774 File Offset: 0x0010D974
			internal static UnsafeNclNativeMethods.HttpApi.HTTPAPI_VERSION Version
			{
				get
				{
					return UnsafeNclNativeMethods.HttpApi.version;
				}
			}

			// Token: 0x17000EF8 RID: 3832
			// (get) Token: 0x06004144 RID: 16708 RVA: 0x0010F77B File Offset: 0x0010D97B
			internal static UnsafeNclNativeMethods.HttpApi.HTTP_API_VERSION ApiVersion
			{
				get
				{
					if (UnsafeNclNativeMethods.HttpApi.version.HttpApiMajorVersion == 2 && UnsafeNclNativeMethods.HttpApi.version.HttpApiMinorVersion == 0)
					{
						return UnsafeNclNativeMethods.HttpApi.HTTP_API_VERSION.Version20;
					}
					if (UnsafeNclNativeMethods.HttpApi.version.HttpApiMajorVersion == 1 && UnsafeNclNativeMethods.HttpApi.version.HttpApiMinorVersion == 0)
					{
						return UnsafeNclNativeMethods.HttpApi.HTTP_API_VERSION.Version10;
					}
					return UnsafeNclNativeMethods.HttpApi.HTTP_API_VERSION.Invalid;
				}
			}

			// Token: 0x17000EF9 RID: 3833
			// (get) Token: 0x06004145 RID: 16709 RVA: 0x0010F7B4 File Offset: 0x0010D9B4
			internal static bool ExtendedProtectionSupported
			{
				get
				{
					return UnsafeNclNativeMethods.HttpApi.extendedProtectionSupported;
				}
			}

			// Token: 0x06004146 RID: 16710 RVA: 0x0010F7C0 File Offset: 0x0010D9C0
			static HttpApi()
			{
				UnsafeNclNativeMethods.HttpApi.InitHttpApi(2, 0);
			}

			// Token: 0x06004147 RID: 16711 RVA: 0x0010F884 File Offset: 0x0010DA84
			private static void InitHttpApi(ushort majorVersion, ushort minorVersion)
			{
				UnsafeNclNativeMethods.HttpApi.version.HttpApiMajorVersion = majorVersion;
				UnsafeNclNativeMethods.HttpApi.version.HttpApiMinorVersion = minorVersion;
				UnsafeNclNativeMethods.HttpApi.extendedProtectionSupported = true;
				uint num;
				if (ComNetOS.IsWin7orLater)
				{
					num = UnsafeNclNativeMethods.HttpApi.HttpInitialize(UnsafeNclNativeMethods.HttpApi.version, 1U, null);
				}
				else
				{
					num = UnsafeNclNativeMethods.HttpApi.HttpInitialize(UnsafeNclNativeMethods.HttpApi.version, 5U, null);
					if (num == 87U)
					{
						if (Logging.On)
						{
							Logging.PrintWarning(Logging.HttpListener, SR.GetString("net_listener_cbt_not_supported"));
						}
						UnsafeNclNativeMethods.HttpApi.extendedProtectionSupported = false;
						num = UnsafeNclNativeMethods.HttpApi.HttpInitialize(UnsafeNclNativeMethods.HttpApi.version, 1U, null);
					}
				}
				UnsafeNclNativeMethods.HttpApi.supported = (num == 0U);
			}

			// Token: 0x17000EFA RID: 3834
			// (get) Token: 0x06004148 RID: 16712 RVA: 0x0010F917 File Offset: 0x0010DB17
			internal static bool Supported
			{
				get
				{
					return UnsafeNclNativeMethods.HttpApi.supported;
				}
			}

			// Token: 0x06004149 RID: 16713 RVA: 0x0010F920 File Offset: 0x0010DB20
			internal unsafe static WebHeaderCollection GetHeaders(byte[] memoryBlob, IntPtr originalAddress)
			{
				WebHeaderCollection webHeaderCollection = new WebHeaderCollection(WebHeaderCollectionType.HttpListenerRequest);
				fixed (byte[] array = memoryBlob)
				{
					byte* ptr;
					if (memoryBlob == null || array.Length == 0)
					{
						ptr = null;
					}
					else
					{
						ptr = &array[0];
					}
					UnsafeNclNativeMethods.HttpApi.HTTP_REQUEST* ptr2 = (UnsafeNclNativeMethods.HttpApi.HTTP_REQUEST*)ptr;
					long num = (long)((byte*)ptr - (byte*)((void*)originalAddress));
					if (ptr2->Headers.UnknownHeaderCount != 0)
					{
						UnsafeNclNativeMethods.HttpApi.HTTP_UNKNOWN_HEADER* ptr3 = num + ptr2->Headers.pUnknownHeaders / sizeof(UnsafeNclNativeMethods.HttpApi.HTTP_UNKNOWN_HEADER);
						for (int i = 0; i < (int)ptr2->Headers.UnknownHeaderCount; i++)
						{
							if (ptr3->pName != null && ptr3->NameLength > 0)
							{
								string name = new string(ptr3->pName + num, 0, (int)ptr3->NameLength);
								string value;
								if (ptr3->pRawValue != null && ptr3->RawValueLength > 0)
								{
									value = new string(ptr3->pRawValue + num, 0, (int)ptr3->RawValueLength);
								}
								else
								{
									value = string.Empty;
								}
								webHeaderCollection.AddInternal(name, value);
							}
							ptr3++;
						}
					}
					UnsafeNclNativeMethods.HttpApi.HTTP_KNOWN_HEADER* ptr4 = &ptr2->Headers.KnownHeaders;
					for (int i = 0; i < 41; i++)
					{
						if (ptr4->pRawValue != null)
						{
							string value2 = new string(ptr4->pRawValue + num, 0, (int)ptr4->RawValueLength);
							webHeaderCollection.AddInternal(UnsafeNclNativeMethods.HttpApi.HTTP_REQUEST_HEADER_ID.ToString(i), value2);
						}
						ptr4++;
					}
				}
				return webHeaderCollection;
			}

			// Token: 0x0600414A RID: 16714 RVA: 0x0010FA7C File Offset: 0x0010DC7C
			private unsafe static string GetKnownHeader(UnsafeNclNativeMethods.HttpApi.HTTP_REQUEST* request, long fixup, int headerIndex)
			{
				string result = null;
				UnsafeNclNativeMethods.HttpApi.HTTP_KNOWN_HEADER* ptr = &request->Headers.KnownHeaders + headerIndex;
				if (ptr->pRawValue != null)
				{
					result = new string(ptr->pRawValue + fixup, 0, (int)ptr->RawValueLength);
				}
				return result;
			}

			// Token: 0x0600414B RID: 16715 RVA: 0x0010FAC3 File Offset: 0x0010DCC3
			internal unsafe static string GetKnownHeader(UnsafeNclNativeMethods.HttpApi.HTTP_REQUEST* request, int headerIndex)
			{
				return UnsafeNclNativeMethods.HttpApi.GetKnownHeader(request, 0L, headerIndex);
			}

			// Token: 0x0600414C RID: 16716 RVA: 0x0010FAD0 File Offset: 0x0010DCD0
			internal unsafe static string GetKnownHeader(byte[] memoryBlob, IntPtr originalAddress, int headerIndex)
			{
				byte* ptr;
				if (memoryBlob == null || memoryBlob.Length == 0)
				{
					ptr = null;
				}
				else
				{
					ptr = &memoryBlob[0];
				}
				return UnsafeNclNativeMethods.HttpApi.GetKnownHeader((UnsafeNclNativeMethods.HttpApi.HTTP_REQUEST*)ptr, (long)((byte*)ptr - (byte*)((void*)originalAddress)), headerIndex);
			}

			// Token: 0x0600414D RID: 16717 RVA: 0x0010FB08 File Offset: 0x0010DD08
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

			// Token: 0x0600414E RID: 16718 RVA: 0x0010FB63 File Offset: 0x0010DD63
			internal unsafe static string GetVerb(UnsafeNclNativeMethods.HttpApi.HTTP_REQUEST* request)
			{
				return UnsafeNclNativeMethods.HttpApi.GetVerb(request, 0L);
			}

			// Token: 0x0600414F RID: 16719 RVA: 0x0010FB70 File Offset: 0x0010DD70
			internal unsafe static string GetVerb(byte[] memoryBlob, IntPtr originalAddress)
			{
				byte* ptr;
				if (memoryBlob == null || memoryBlob.Length == 0)
				{
					ptr = null;
				}
				else
				{
					ptr = &memoryBlob[0];
				}
				return UnsafeNclNativeMethods.HttpApi.GetVerb((UnsafeNclNativeMethods.HttpApi.HTTP_REQUEST*)ptr, (long)((byte*)ptr - (byte*)((void*)originalAddress)));
			}

			// Token: 0x06004150 RID: 16720 RVA: 0x0010FBA8 File Offset: 0x0010DDA8
			internal unsafe static UnsafeNclNativeMethods.HttpApi.HTTP_VERB GetKnownVerb(byte[] memoryBlob, IntPtr originalAddress)
			{
				UnsafeNclNativeMethods.HttpApi.HTTP_VERB result = UnsafeNclNativeMethods.HttpApi.HTTP_VERB.HttpVerbUnknown;
				fixed (byte[] array = memoryBlob)
				{
					byte* ptr;
					if (memoryBlob == null || array.Length == 0)
					{
						ptr = null;
					}
					else
					{
						ptr = &array[0];
					}
					UnsafeNclNativeMethods.HttpApi.HTTP_REQUEST* ptr2 = (UnsafeNclNativeMethods.HttpApi.HTTP_REQUEST*)ptr;
					if (ptr2->Verb > UnsafeNclNativeMethods.HttpApi.HTTP_VERB.HttpVerbUnparsed && ptr2->Verb < UnsafeNclNativeMethods.HttpApi.HTTP_VERB.HttpVerbMaximum)
					{
						result = ptr2->Verb;
					}
				}
				return result;
			}

			// Token: 0x06004151 RID: 16721 RVA: 0x0010FBF0 File Offset: 0x0010DDF0
			internal unsafe static uint GetChunks(byte[] memoryBlob, IntPtr originalAddress, ref int dataChunkIndex, ref uint dataChunkOffset, byte[] buffer, int offset, int size)
			{
				uint num = 0U;
				fixed (byte[] array = memoryBlob)
				{
					byte* ptr;
					if (memoryBlob == null || array.Length == 0)
					{
						ptr = null;
					}
					else
					{
						ptr = &array[0];
					}
					UnsafeNclNativeMethods.HttpApi.HTTP_REQUEST* ptr2 = (UnsafeNclNativeMethods.HttpApi.HTTP_REQUEST*)ptr;
					long num2 = (long)((byte*)ptr - (byte*)((void*)originalAddress));
					if (ptr2->EntityChunkCount > 0 && dataChunkIndex < (int)ptr2->EntityChunkCount && dataChunkIndex != -1)
					{
						UnsafeNclNativeMethods.HttpApi.HTTP_DATA_CHUNK* ptr3 = num2 + (ptr2->pEntityChunks + dataChunkIndex) / sizeof(UnsafeNclNativeMethods.HttpApi.HTTP_DATA_CHUNK);
						fixed (byte[] array2 = buffer)
						{
							byte* ptr4;
							if (buffer == null || array2.Length == 0)
							{
								ptr4 = null;
							}
							else
							{
								ptr4 = &array2[0];
							}
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

			// Token: 0x06004152 RID: 16722 RVA: 0x0010FD24 File Offset: 0x0010DF24
			internal unsafe static IPEndPoint GetRemoteEndPoint(byte[] memoryBlob, IntPtr originalAddress)
			{
				SocketAddress socketAddress = new SocketAddress(AddressFamily.InterNetwork, 16);
				SocketAddress socketAddress2 = new SocketAddress(AddressFamily.InterNetworkV6, 28);
				fixed (byte[] array = memoryBlob)
				{
					byte* ptr;
					if (memoryBlob == null || array.Length == 0)
					{
						ptr = null;
					}
					else
					{
						ptr = &array[0];
					}
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

			// Token: 0x06004153 RID: 16723 RVA: 0x0010FDD4 File Offset: 0x0010DFD4
			internal unsafe static IPEndPoint GetLocalEndPoint(byte[] memoryBlob, IntPtr originalAddress)
			{
				SocketAddress socketAddress = new SocketAddress(AddressFamily.InterNetwork, 16);
				SocketAddress socketAddress2 = new SocketAddress(AddressFamily.InterNetworkV6, 28);
				fixed (byte[] array = memoryBlob)
				{
					byte* ptr;
					if (memoryBlob == null || array.Length == 0)
					{
						ptr = null;
					}
					else
					{
						ptr = &array[0];
					}
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

			// Token: 0x06004154 RID: 16724 RVA: 0x0010FE84 File Offset: 0x0010E084
			internal unsafe static UnsafeNclNativeMethods.HttpApi.HTTP_REQUEST_TOKEN_BINDING_INFO* GetTlsTokenBindingRequestInfo(byte[] memoryBlob, IntPtr originalAddress)
			{
				fixed (byte[] array = memoryBlob)
				{
					byte* ptr;
					if (memoryBlob == null || array.Length == 0)
					{
						ptr = null;
					}
					else
					{
						ptr = &array[0];
					}
					UnsafeNclNativeMethods.HttpApi.HTTP_REQUEST_V2* ptr2 = (UnsafeNclNativeMethods.HttpApi.HTTP_REQUEST_V2*)ptr;
					long num = (long)((byte*)ptr - (byte*)((void*)originalAddress));
					for (int i = 0; i < (int)ptr2->RequestInfoCount; i++)
					{
						UnsafeNclNativeMethods.HttpApi.HTTP_REQUEST_INFO* ptr3 = num + (ptr2->pRequestInfo + i) / sizeof(UnsafeNclNativeMethods.HttpApi.HTTP_REQUEST_INFO);
						if (ptr3 != null && ptr3->InfoType == UnsafeNclNativeMethods.HttpApi.HTTP_REQUEST_INFO_TYPE.HttpRequestInfoTypeSslTokenBinding)
						{
							return (UnsafeNclNativeMethods.HttpApi.HTTP_REQUEST_TOKEN_BINDING_INFO*)((byte*)ptr3->pInfo + num);
						}
					}
				}
				return null;
			}

			// Token: 0x06004155 RID: 16725 RVA: 0x0010FF04 File Offset: 0x0010E104
			internal unsafe static UnsafeNclNativeMethods.HttpApi.HTTP_REQUEST_TOKEN_BINDING_INFO_V1* GetTlsTokenBindingRequestInfo_V1(byte[] memoryBlob, IntPtr originalAddress)
			{
				fixed (byte[] array = memoryBlob)
				{
					byte* ptr;
					if (memoryBlob == null || array.Length == 0)
					{
						ptr = null;
					}
					else
					{
						ptr = &array[0];
					}
					UnsafeNclNativeMethods.HttpApi.HTTP_REQUEST_V2* ptr2 = (UnsafeNclNativeMethods.HttpApi.HTTP_REQUEST_V2*)ptr;
					long num = (long)((byte*)ptr - (byte*)((void*)originalAddress));
					for (int i = 0; i < (int)ptr2->RequestInfoCount; i++)
					{
						UnsafeNclNativeMethods.HttpApi.HTTP_REQUEST_INFO* ptr3 = num + (ptr2->pRequestInfo + i) / sizeof(UnsafeNclNativeMethods.HttpApi.HTTP_REQUEST_INFO);
						if (ptr3 != null && ptr3->InfoType == UnsafeNclNativeMethods.HttpApi.HTTP_REQUEST_INFO_TYPE.HttpRequestInfoTypeSslTokenBindingDraft)
						{
							return (UnsafeNclNativeMethods.HttpApi.HTTP_REQUEST_TOKEN_BINDING_INFO_V1*)((byte*)ptr3->pInfo + num);
						}
					}
				}
				return null;
			}

			// Token: 0x06004156 RID: 16726 RVA: 0x0010FF84 File Offset: 0x0010E184
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
			private unsafe static void CopyOutAddress(IntPtr address, ref SocketAddress v4address, ref SocketAddress v6address)
			{
				if (address != IntPtr.Zero)
				{
					ushort num = *(ushort*)((void*)address);
					if (num == 2)
					{
						v6address = null;
						byte[] array;
						byte* ptr;
						if ((array = v4address.m_Buffer) == null || array.Length == 0)
						{
							ptr = null;
						}
						else
						{
							ptr = &array[0];
						}
						for (int i = 2; i < 16; i++)
						{
							ptr[i] = ((byte*)((void*)address))[i];
						}
						array = null;
						return;
					}
					if (num == 23)
					{
						v4address = null;
						byte[] array;
						byte* ptr2;
						if ((array = v6address.m_Buffer) == null || array.Length == 0)
						{
							ptr2 = null;
						}
						else
						{
							ptr2 = &array[0];
						}
						for (int j = 2; j < 28; j++)
						{
							ptr2[j] = ((byte*)((void*)address))[j];
						}
						array = null;
						return;
					}
				}
				v4address = null;
				v6address = null;
			}

			// Token: 0x04003153 RID: 12627
			internal const int MaxTimeout = 6;

			// Token: 0x04003154 RID: 12628
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

			// Token: 0x04003155 RID: 12629
			private const int HttpHeaderRequestMaximum = 41;

			// Token: 0x04003156 RID: 12630
			private const int HttpHeaderResponseMaximum = 30;

			// Token: 0x04003157 RID: 12631
			private static UnsafeNclNativeMethods.HttpApi.HTTPAPI_VERSION version;

			// Token: 0x04003158 RID: 12632
			private static volatile bool extendedProtectionSupported;

			// Token: 0x04003159 RID: 12633
			private static volatile bool supported;

			// Token: 0x020008E7 RID: 2279
			internal sealed class HeapAllocHandle : SafeHandleZeroOrMinusOneIsInvalid
			{
				// Token: 0x0600463F RID: 17983 RVA: 0x00125609 File Offset: 0x00123809
				private HeapAllocHandle() : base(true)
				{
				}

				// Token: 0x06004640 RID: 17984 RVA: 0x00125612 File Offset: 0x00123812
				protected override bool ReleaseHandle()
				{
					return UnsafeNclNativeMethods.HeapFree(UnsafeNclNativeMethods.HttpApi.HeapAllocHandle.ProcessHeap, 0U, this.handle);
				}

				// Token: 0x04003C3B RID: 15419
				private static readonly IntPtr ProcessHeap = UnsafeNclNativeMethods.GetProcessHeap();
			}

			// Token: 0x020008E8 RID: 2280
			internal enum HTTP_API_VERSION
			{
				// Token: 0x04003C3D RID: 15421
				Invalid,
				// Token: 0x04003C3E RID: 15422
				Version10,
				// Token: 0x04003C3F RID: 15423
				Version20
			}

			// Token: 0x020008E9 RID: 2281
			internal enum HTTP_SERVER_PROPERTY
			{
				// Token: 0x04003C41 RID: 15425
				HttpServerAuthenticationProperty,
				// Token: 0x04003C42 RID: 15426
				HttpServerLoggingProperty,
				// Token: 0x04003C43 RID: 15427
				HttpServerQosProperty,
				// Token: 0x04003C44 RID: 15428
				HttpServerTimeoutsProperty,
				// Token: 0x04003C45 RID: 15429
				HttpServerQueueLengthProperty,
				// Token: 0x04003C46 RID: 15430
				HttpServerStateProperty,
				// Token: 0x04003C47 RID: 15431
				HttpServer503VerbosityProperty,
				// Token: 0x04003C48 RID: 15432
				HttpServerBindingProperty,
				// Token: 0x04003C49 RID: 15433
				HttpServerExtendedAuthenticationProperty,
				// Token: 0x04003C4A RID: 15434
				HttpServerListenEndpointProperty,
				// Token: 0x04003C4B RID: 15435
				HttpServerChannelBindProperty,
				// Token: 0x04003C4C RID: 15436
				HttpServerProtectionLevelProperty
			}

			// Token: 0x020008EA RID: 2282
			internal enum HTTP_REQUEST_INFO_TYPE
			{
				// Token: 0x04003C4E RID: 15438
				HttpRequestInfoTypeAuth,
				// Token: 0x04003C4F RID: 15439
				HttpRequestInfoTypeChannelBind,
				// Token: 0x04003C50 RID: 15440
				HttpRequestInfoTypeSslProtocol,
				// Token: 0x04003C51 RID: 15441
				HttpRequestInfoTypeSslTokenBindingDraft,
				// Token: 0x04003C52 RID: 15442
				HttpRequestInfoTypeSslTokenBinding
			}

			// Token: 0x020008EB RID: 2283
			internal enum HTTP_RESPONSE_INFO_TYPE
			{
				// Token: 0x04003C54 RID: 15444
				HttpResponseInfoTypeMultipleKnownHeaders,
				// Token: 0x04003C55 RID: 15445
				HttpResponseInfoTypeAuthenticationProperty,
				// Token: 0x04003C56 RID: 15446
				HttpResponseInfoTypeQosProperty
			}

			// Token: 0x020008EC RID: 2284
			internal enum HTTP_TIMEOUT_TYPE
			{
				// Token: 0x04003C58 RID: 15448
				EntityBody,
				// Token: 0x04003C59 RID: 15449
				DrainEntityBody,
				// Token: 0x04003C5A RID: 15450
				RequestQueue,
				// Token: 0x04003C5B RID: 15451
				IdleConnection,
				// Token: 0x04003C5C RID: 15452
				HeaderWait,
				// Token: 0x04003C5D RID: 15453
				MinSendRate
			}

			// Token: 0x020008ED RID: 2285
			internal struct HTTP_VERSION
			{
				// Token: 0x04003C5E RID: 15454
				internal ushort MajorVersion;

				// Token: 0x04003C5F RID: 15455
				internal ushort MinorVersion;
			}

			// Token: 0x020008EE RID: 2286
			internal struct HTTP_KNOWN_HEADER
			{
				// Token: 0x04003C60 RID: 15456
				internal ushort RawValueLength;

				// Token: 0x04003C61 RID: 15457
				internal unsafe sbyte* pRawValue;
			}

			// Token: 0x020008EF RID: 2287
			[StructLayout(LayoutKind.Sequential, Size = 32)]
			internal struct HTTP_DATA_CHUNK
			{
				// Token: 0x04003C62 RID: 15458
				internal UnsafeNclNativeMethods.HttpApi.HTTP_DATA_CHUNK_TYPE DataChunkType;

				// Token: 0x04003C63 RID: 15459
				internal uint p0;

				// Token: 0x04003C64 RID: 15460
				internal unsafe byte* pBuffer;

				// Token: 0x04003C65 RID: 15461
				internal uint BufferLength;
			}

			// Token: 0x020008F0 RID: 2288
			internal struct HTTPAPI_VERSION
			{
				// Token: 0x04003C66 RID: 15462
				internal ushort HttpApiMajorVersion;

				// Token: 0x04003C67 RID: 15463
				internal ushort HttpApiMinorVersion;
			}

			// Token: 0x020008F1 RID: 2289
			internal struct HTTP_COOKED_URL
			{
				// Token: 0x04003C68 RID: 15464
				internal ushort FullUrlLength;

				// Token: 0x04003C69 RID: 15465
				internal ushort HostLength;

				// Token: 0x04003C6A RID: 15466
				internal ushort AbsPathLength;

				// Token: 0x04003C6B RID: 15467
				internal ushort QueryStringLength;

				// Token: 0x04003C6C RID: 15468
				internal unsafe ushort* pFullUrl;

				// Token: 0x04003C6D RID: 15469
				internal unsafe ushort* pHost;

				// Token: 0x04003C6E RID: 15470
				internal unsafe ushort* pAbsPath;

				// Token: 0x04003C6F RID: 15471
				internal unsafe ushort* pQueryString;
			}

			// Token: 0x020008F2 RID: 2290
			internal struct SOCKADDR
			{
				// Token: 0x04003C70 RID: 15472
				internal ushort sa_family;

				// Token: 0x04003C71 RID: 15473
				internal byte sa_data;

				// Token: 0x04003C72 RID: 15474
				internal byte sa_data_02;

				// Token: 0x04003C73 RID: 15475
				internal byte sa_data_03;

				// Token: 0x04003C74 RID: 15476
				internal byte sa_data_04;

				// Token: 0x04003C75 RID: 15477
				internal byte sa_data_05;

				// Token: 0x04003C76 RID: 15478
				internal byte sa_data_06;

				// Token: 0x04003C77 RID: 15479
				internal byte sa_data_07;

				// Token: 0x04003C78 RID: 15480
				internal byte sa_data_08;

				// Token: 0x04003C79 RID: 15481
				internal byte sa_data_09;

				// Token: 0x04003C7A RID: 15482
				internal byte sa_data_10;

				// Token: 0x04003C7B RID: 15483
				internal byte sa_data_11;

				// Token: 0x04003C7C RID: 15484
				internal byte sa_data_12;

				// Token: 0x04003C7D RID: 15485
				internal byte sa_data_13;

				// Token: 0x04003C7E RID: 15486
				internal byte sa_data_14;
			}

			// Token: 0x020008F3 RID: 2291
			internal struct HTTP_TRANSPORT_ADDRESS
			{
				// Token: 0x04003C7F RID: 15487
				internal unsafe UnsafeNclNativeMethods.HttpApi.SOCKADDR* pRemoteAddress;

				// Token: 0x04003C80 RID: 15488
				internal unsafe UnsafeNclNativeMethods.HttpApi.SOCKADDR* pLocalAddress;
			}

			// Token: 0x020008F4 RID: 2292
			internal struct HTTP_SSL_CLIENT_CERT_INFO
			{
				// Token: 0x04003C81 RID: 15489
				internal uint CertFlags;

				// Token: 0x04003C82 RID: 15490
				internal uint CertEncodedSize;

				// Token: 0x04003C83 RID: 15491
				internal unsafe byte* pCertEncoded;

				// Token: 0x04003C84 RID: 15492
				internal unsafe void* Token;

				// Token: 0x04003C85 RID: 15493
				internal byte CertDeniedByMapper;
			}

			// Token: 0x020008F5 RID: 2293
			internal enum HTTP_SERVICE_BINDING_TYPE : uint
			{
				// Token: 0x04003C87 RID: 15495
				HttpServiceBindingTypeNone,
				// Token: 0x04003C88 RID: 15496
				HttpServiceBindingTypeW,
				// Token: 0x04003C89 RID: 15497
				HttpServiceBindingTypeA
			}

			// Token: 0x020008F6 RID: 2294
			internal struct HTTP_SERVICE_BINDING_BASE
			{
				// Token: 0x04003C8A RID: 15498
				internal UnsafeNclNativeMethods.HttpApi.HTTP_SERVICE_BINDING_TYPE Type;
			}

			// Token: 0x020008F7 RID: 2295
			internal struct HTTP_REQUEST_CHANNEL_BIND_STATUS
			{
				// Token: 0x04003C8B RID: 15499
				internal IntPtr ServiceName;

				// Token: 0x04003C8C RID: 15500
				internal IntPtr ChannelToken;

				// Token: 0x04003C8D RID: 15501
				internal uint ChannelTokenSize;

				// Token: 0x04003C8E RID: 15502
				internal uint Flags;
			}

			// Token: 0x020008F8 RID: 2296
			internal struct HTTP_UNKNOWN_HEADER
			{
				// Token: 0x04003C8F RID: 15503
				internal ushort NameLength;

				// Token: 0x04003C90 RID: 15504
				internal ushort RawValueLength;

				// Token: 0x04003C91 RID: 15505
				internal unsafe sbyte* pName;

				// Token: 0x04003C92 RID: 15506
				internal unsafe sbyte* pRawValue;
			}

			// Token: 0x020008F9 RID: 2297
			internal struct HTTP_SSL_INFO
			{
				// Token: 0x04003C93 RID: 15507
				internal ushort ServerCertKeySize;

				// Token: 0x04003C94 RID: 15508
				internal ushort ConnectionKeySize;

				// Token: 0x04003C95 RID: 15509
				internal uint ServerCertIssuerSize;

				// Token: 0x04003C96 RID: 15510
				internal uint ServerCertSubjectSize;

				// Token: 0x04003C97 RID: 15511
				internal unsafe sbyte* pServerCertIssuer;

				// Token: 0x04003C98 RID: 15512
				internal unsafe sbyte* pServerCertSubject;

				// Token: 0x04003C99 RID: 15513
				internal unsafe UnsafeNclNativeMethods.HttpApi.HTTP_SSL_CLIENT_CERT_INFO* pClientCertInfo;

				// Token: 0x04003C9A RID: 15514
				internal uint SslClientCertNegotiated;
			}

			// Token: 0x020008FA RID: 2298
			internal struct HTTP_RESPONSE_HEADERS
			{
				// Token: 0x04003C9B RID: 15515
				internal ushort UnknownHeaderCount;

				// Token: 0x04003C9C RID: 15516
				internal unsafe UnsafeNclNativeMethods.HttpApi.HTTP_UNKNOWN_HEADER* pUnknownHeaders;

				// Token: 0x04003C9D RID: 15517
				internal ushort TrailerCount;

				// Token: 0x04003C9E RID: 15518
				internal unsafe UnsafeNclNativeMethods.HttpApi.HTTP_UNKNOWN_HEADER* pTrailers;

				// Token: 0x04003C9F RID: 15519
				internal UnsafeNclNativeMethods.HttpApi.HTTP_KNOWN_HEADER KnownHeaders;

				// Token: 0x04003CA0 RID: 15520
				internal UnsafeNclNativeMethods.HttpApi.HTTP_KNOWN_HEADER KnownHeaders_02;

				// Token: 0x04003CA1 RID: 15521
				internal UnsafeNclNativeMethods.HttpApi.HTTP_KNOWN_HEADER KnownHeaders_03;

				// Token: 0x04003CA2 RID: 15522
				internal UnsafeNclNativeMethods.HttpApi.HTTP_KNOWN_HEADER KnownHeaders_04;

				// Token: 0x04003CA3 RID: 15523
				internal UnsafeNclNativeMethods.HttpApi.HTTP_KNOWN_HEADER KnownHeaders_05;

				// Token: 0x04003CA4 RID: 15524
				internal UnsafeNclNativeMethods.HttpApi.HTTP_KNOWN_HEADER KnownHeaders_06;

				// Token: 0x04003CA5 RID: 15525
				internal UnsafeNclNativeMethods.HttpApi.HTTP_KNOWN_HEADER KnownHeaders_07;

				// Token: 0x04003CA6 RID: 15526
				internal UnsafeNclNativeMethods.HttpApi.HTTP_KNOWN_HEADER KnownHeaders_08;

				// Token: 0x04003CA7 RID: 15527
				internal UnsafeNclNativeMethods.HttpApi.HTTP_KNOWN_HEADER KnownHeaders_09;

				// Token: 0x04003CA8 RID: 15528
				internal UnsafeNclNativeMethods.HttpApi.HTTP_KNOWN_HEADER KnownHeaders_10;

				// Token: 0x04003CA9 RID: 15529
				internal UnsafeNclNativeMethods.HttpApi.HTTP_KNOWN_HEADER KnownHeaders_11;

				// Token: 0x04003CAA RID: 15530
				internal UnsafeNclNativeMethods.HttpApi.HTTP_KNOWN_HEADER KnownHeaders_12;

				// Token: 0x04003CAB RID: 15531
				internal UnsafeNclNativeMethods.HttpApi.HTTP_KNOWN_HEADER KnownHeaders_13;

				// Token: 0x04003CAC RID: 15532
				internal UnsafeNclNativeMethods.HttpApi.HTTP_KNOWN_HEADER KnownHeaders_14;

				// Token: 0x04003CAD RID: 15533
				internal UnsafeNclNativeMethods.HttpApi.HTTP_KNOWN_HEADER KnownHeaders_15;

				// Token: 0x04003CAE RID: 15534
				internal UnsafeNclNativeMethods.HttpApi.HTTP_KNOWN_HEADER KnownHeaders_16;

				// Token: 0x04003CAF RID: 15535
				internal UnsafeNclNativeMethods.HttpApi.HTTP_KNOWN_HEADER KnownHeaders_17;

				// Token: 0x04003CB0 RID: 15536
				internal UnsafeNclNativeMethods.HttpApi.HTTP_KNOWN_HEADER KnownHeaders_18;

				// Token: 0x04003CB1 RID: 15537
				internal UnsafeNclNativeMethods.HttpApi.HTTP_KNOWN_HEADER KnownHeaders_19;

				// Token: 0x04003CB2 RID: 15538
				internal UnsafeNclNativeMethods.HttpApi.HTTP_KNOWN_HEADER KnownHeaders_20;

				// Token: 0x04003CB3 RID: 15539
				internal UnsafeNclNativeMethods.HttpApi.HTTP_KNOWN_HEADER KnownHeaders_21;

				// Token: 0x04003CB4 RID: 15540
				internal UnsafeNclNativeMethods.HttpApi.HTTP_KNOWN_HEADER KnownHeaders_22;

				// Token: 0x04003CB5 RID: 15541
				internal UnsafeNclNativeMethods.HttpApi.HTTP_KNOWN_HEADER KnownHeaders_23;

				// Token: 0x04003CB6 RID: 15542
				internal UnsafeNclNativeMethods.HttpApi.HTTP_KNOWN_HEADER KnownHeaders_24;

				// Token: 0x04003CB7 RID: 15543
				internal UnsafeNclNativeMethods.HttpApi.HTTP_KNOWN_HEADER KnownHeaders_25;

				// Token: 0x04003CB8 RID: 15544
				internal UnsafeNclNativeMethods.HttpApi.HTTP_KNOWN_HEADER KnownHeaders_26;

				// Token: 0x04003CB9 RID: 15545
				internal UnsafeNclNativeMethods.HttpApi.HTTP_KNOWN_HEADER KnownHeaders_27;

				// Token: 0x04003CBA RID: 15546
				internal UnsafeNclNativeMethods.HttpApi.HTTP_KNOWN_HEADER KnownHeaders_28;

				// Token: 0x04003CBB RID: 15547
				internal UnsafeNclNativeMethods.HttpApi.HTTP_KNOWN_HEADER KnownHeaders_29;

				// Token: 0x04003CBC RID: 15548
				internal UnsafeNclNativeMethods.HttpApi.HTTP_KNOWN_HEADER KnownHeaders_30;
			}

			// Token: 0x020008FB RID: 2299
			internal struct HTTP_REQUEST_HEADERS
			{
				// Token: 0x04003CBD RID: 15549
				internal ushort UnknownHeaderCount;

				// Token: 0x04003CBE RID: 15550
				internal unsafe UnsafeNclNativeMethods.HttpApi.HTTP_UNKNOWN_HEADER* pUnknownHeaders;

				// Token: 0x04003CBF RID: 15551
				internal ushort TrailerCount;

				// Token: 0x04003CC0 RID: 15552
				internal unsafe UnsafeNclNativeMethods.HttpApi.HTTP_UNKNOWN_HEADER* pTrailers;

				// Token: 0x04003CC1 RID: 15553
				internal UnsafeNclNativeMethods.HttpApi.HTTP_KNOWN_HEADER KnownHeaders;

				// Token: 0x04003CC2 RID: 15554
				internal UnsafeNclNativeMethods.HttpApi.HTTP_KNOWN_HEADER KnownHeaders_02;

				// Token: 0x04003CC3 RID: 15555
				internal UnsafeNclNativeMethods.HttpApi.HTTP_KNOWN_HEADER KnownHeaders_03;

				// Token: 0x04003CC4 RID: 15556
				internal UnsafeNclNativeMethods.HttpApi.HTTP_KNOWN_HEADER KnownHeaders_04;

				// Token: 0x04003CC5 RID: 15557
				internal UnsafeNclNativeMethods.HttpApi.HTTP_KNOWN_HEADER KnownHeaders_05;

				// Token: 0x04003CC6 RID: 15558
				internal UnsafeNclNativeMethods.HttpApi.HTTP_KNOWN_HEADER KnownHeaders_06;

				// Token: 0x04003CC7 RID: 15559
				internal UnsafeNclNativeMethods.HttpApi.HTTP_KNOWN_HEADER KnownHeaders_07;

				// Token: 0x04003CC8 RID: 15560
				internal UnsafeNclNativeMethods.HttpApi.HTTP_KNOWN_HEADER KnownHeaders_08;

				// Token: 0x04003CC9 RID: 15561
				internal UnsafeNclNativeMethods.HttpApi.HTTP_KNOWN_HEADER KnownHeaders_09;

				// Token: 0x04003CCA RID: 15562
				internal UnsafeNclNativeMethods.HttpApi.HTTP_KNOWN_HEADER KnownHeaders_10;

				// Token: 0x04003CCB RID: 15563
				internal UnsafeNclNativeMethods.HttpApi.HTTP_KNOWN_HEADER KnownHeaders_11;

				// Token: 0x04003CCC RID: 15564
				internal UnsafeNclNativeMethods.HttpApi.HTTP_KNOWN_HEADER KnownHeaders_12;

				// Token: 0x04003CCD RID: 15565
				internal UnsafeNclNativeMethods.HttpApi.HTTP_KNOWN_HEADER KnownHeaders_13;

				// Token: 0x04003CCE RID: 15566
				internal UnsafeNclNativeMethods.HttpApi.HTTP_KNOWN_HEADER KnownHeaders_14;

				// Token: 0x04003CCF RID: 15567
				internal UnsafeNclNativeMethods.HttpApi.HTTP_KNOWN_HEADER KnownHeaders_15;

				// Token: 0x04003CD0 RID: 15568
				internal UnsafeNclNativeMethods.HttpApi.HTTP_KNOWN_HEADER KnownHeaders_16;

				// Token: 0x04003CD1 RID: 15569
				internal UnsafeNclNativeMethods.HttpApi.HTTP_KNOWN_HEADER KnownHeaders_17;

				// Token: 0x04003CD2 RID: 15570
				internal UnsafeNclNativeMethods.HttpApi.HTTP_KNOWN_HEADER KnownHeaders_18;

				// Token: 0x04003CD3 RID: 15571
				internal UnsafeNclNativeMethods.HttpApi.HTTP_KNOWN_HEADER KnownHeaders_19;

				// Token: 0x04003CD4 RID: 15572
				internal UnsafeNclNativeMethods.HttpApi.HTTP_KNOWN_HEADER KnownHeaders_20;

				// Token: 0x04003CD5 RID: 15573
				internal UnsafeNclNativeMethods.HttpApi.HTTP_KNOWN_HEADER KnownHeaders_21;

				// Token: 0x04003CD6 RID: 15574
				internal UnsafeNclNativeMethods.HttpApi.HTTP_KNOWN_HEADER KnownHeaders_22;

				// Token: 0x04003CD7 RID: 15575
				internal UnsafeNclNativeMethods.HttpApi.HTTP_KNOWN_HEADER KnownHeaders_23;

				// Token: 0x04003CD8 RID: 15576
				internal UnsafeNclNativeMethods.HttpApi.HTTP_KNOWN_HEADER KnownHeaders_24;

				// Token: 0x04003CD9 RID: 15577
				internal UnsafeNclNativeMethods.HttpApi.HTTP_KNOWN_HEADER KnownHeaders_25;

				// Token: 0x04003CDA RID: 15578
				internal UnsafeNclNativeMethods.HttpApi.HTTP_KNOWN_HEADER KnownHeaders_26;

				// Token: 0x04003CDB RID: 15579
				internal UnsafeNclNativeMethods.HttpApi.HTTP_KNOWN_HEADER KnownHeaders_27;

				// Token: 0x04003CDC RID: 15580
				internal UnsafeNclNativeMethods.HttpApi.HTTP_KNOWN_HEADER KnownHeaders_28;

				// Token: 0x04003CDD RID: 15581
				internal UnsafeNclNativeMethods.HttpApi.HTTP_KNOWN_HEADER KnownHeaders_29;

				// Token: 0x04003CDE RID: 15582
				internal UnsafeNclNativeMethods.HttpApi.HTTP_KNOWN_HEADER KnownHeaders_30;

				// Token: 0x04003CDF RID: 15583
				internal UnsafeNclNativeMethods.HttpApi.HTTP_KNOWN_HEADER KnownHeaders_31;

				// Token: 0x04003CE0 RID: 15584
				internal UnsafeNclNativeMethods.HttpApi.HTTP_KNOWN_HEADER KnownHeaders_32;

				// Token: 0x04003CE1 RID: 15585
				internal UnsafeNclNativeMethods.HttpApi.HTTP_KNOWN_HEADER KnownHeaders_33;

				// Token: 0x04003CE2 RID: 15586
				internal UnsafeNclNativeMethods.HttpApi.HTTP_KNOWN_HEADER KnownHeaders_34;

				// Token: 0x04003CE3 RID: 15587
				internal UnsafeNclNativeMethods.HttpApi.HTTP_KNOWN_HEADER KnownHeaders_35;

				// Token: 0x04003CE4 RID: 15588
				internal UnsafeNclNativeMethods.HttpApi.HTTP_KNOWN_HEADER KnownHeaders_36;

				// Token: 0x04003CE5 RID: 15589
				internal UnsafeNclNativeMethods.HttpApi.HTTP_KNOWN_HEADER KnownHeaders_37;

				// Token: 0x04003CE6 RID: 15590
				internal UnsafeNclNativeMethods.HttpApi.HTTP_KNOWN_HEADER KnownHeaders_38;

				// Token: 0x04003CE7 RID: 15591
				internal UnsafeNclNativeMethods.HttpApi.HTTP_KNOWN_HEADER KnownHeaders_39;

				// Token: 0x04003CE8 RID: 15592
				internal UnsafeNclNativeMethods.HttpApi.HTTP_KNOWN_HEADER KnownHeaders_40;

				// Token: 0x04003CE9 RID: 15593
				internal UnsafeNclNativeMethods.HttpApi.HTTP_KNOWN_HEADER KnownHeaders_41;
			}

			// Token: 0x020008FC RID: 2300
			internal enum HTTP_VERB
			{
				// Token: 0x04003CEB RID: 15595
				HttpVerbUnparsed,
				// Token: 0x04003CEC RID: 15596
				HttpVerbUnknown,
				// Token: 0x04003CED RID: 15597
				HttpVerbInvalid,
				// Token: 0x04003CEE RID: 15598
				HttpVerbOPTIONS,
				// Token: 0x04003CEF RID: 15599
				HttpVerbGET,
				// Token: 0x04003CF0 RID: 15600
				HttpVerbHEAD,
				// Token: 0x04003CF1 RID: 15601
				HttpVerbPOST,
				// Token: 0x04003CF2 RID: 15602
				HttpVerbPUT,
				// Token: 0x04003CF3 RID: 15603
				HttpVerbDELETE,
				// Token: 0x04003CF4 RID: 15604
				HttpVerbTRACE,
				// Token: 0x04003CF5 RID: 15605
				HttpVerbCONNECT,
				// Token: 0x04003CF6 RID: 15606
				HttpVerbTRACK,
				// Token: 0x04003CF7 RID: 15607
				HttpVerbMOVE,
				// Token: 0x04003CF8 RID: 15608
				HttpVerbCOPY,
				// Token: 0x04003CF9 RID: 15609
				HttpVerbPROPFIND,
				// Token: 0x04003CFA RID: 15610
				HttpVerbPROPPATCH,
				// Token: 0x04003CFB RID: 15611
				HttpVerbMKCOL,
				// Token: 0x04003CFC RID: 15612
				HttpVerbLOCK,
				// Token: 0x04003CFD RID: 15613
				HttpVerbUNLOCK,
				// Token: 0x04003CFE RID: 15614
				HttpVerbSEARCH,
				// Token: 0x04003CFF RID: 15615
				HttpVerbMaximum
			}

			// Token: 0x020008FD RID: 2301
			internal enum HTTP_DATA_CHUNK_TYPE
			{
				// Token: 0x04003D01 RID: 15617
				HttpDataChunkFromMemory,
				// Token: 0x04003D02 RID: 15618
				HttpDataChunkFromFileHandle,
				// Token: 0x04003D03 RID: 15619
				HttpDataChunkFromFragmentCache,
				// Token: 0x04003D04 RID: 15620
				HttpDataChunkMaximum
			}

			// Token: 0x020008FE RID: 2302
			internal struct HTTP_RESPONSE_INFO
			{
				// Token: 0x04003D05 RID: 15621
				internal UnsafeNclNativeMethods.HttpApi.HTTP_RESPONSE_INFO_TYPE Type;

				// Token: 0x04003D06 RID: 15622
				internal uint Length;

				// Token: 0x04003D07 RID: 15623
				internal unsafe void* pInfo;
			}

			// Token: 0x020008FF RID: 2303
			internal struct HTTP_RESPONSE
			{
				// Token: 0x04003D08 RID: 15624
				internal uint Flags;

				// Token: 0x04003D09 RID: 15625
				internal UnsafeNclNativeMethods.HttpApi.HTTP_VERSION Version;

				// Token: 0x04003D0A RID: 15626
				internal ushort StatusCode;

				// Token: 0x04003D0B RID: 15627
				internal ushort ReasonLength;

				// Token: 0x04003D0C RID: 15628
				internal unsafe sbyte* pReason;

				// Token: 0x04003D0D RID: 15629
				internal UnsafeNclNativeMethods.HttpApi.HTTP_RESPONSE_HEADERS Headers;

				// Token: 0x04003D0E RID: 15630
				internal ushort EntityChunkCount;

				// Token: 0x04003D0F RID: 15631
				internal unsafe UnsafeNclNativeMethods.HttpApi.HTTP_DATA_CHUNK* pEntityChunks;

				// Token: 0x04003D10 RID: 15632
				internal ushort ResponseInfoCount;

				// Token: 0x04003D11 RID: 15633
				internal unsafe UnsafeNclNativeMethods.HttpApi.HTTP_RESPONSE_INFO* pResponseInfo;
			}

			// Token: 0x02000900 RID: 2304
			internal struct HTTP_REQUEST_INFO
			{
				// Token: 0x04003D12 RID: 15634
				internal UnsafeNclNativeMethods.HttpApi.HTTP_REQUEST_INFO_TYPE InfoType;

				// Token: 0x04003D13 RID: 15635
				internal uint InfoLength;

				// Token: 0x04003D14 RID: 15636
				internal unsafe void* pInfo;
			}

			// Token: 0x02000901 RID: 2305
			internal struct HTTP_REQUEST
			{
				// Token: 0x04003D15 RID: 15637
				internal uint Flags;

				// Token: 0x04003D16 RID: 15638
				internal ulong ConnectionId;

				// Token: 0x04003D17 RID: 15639
				internal ulong RequestId;

				// Token: 0x04003D18 RID: 15640
				internal ulong UrlContext;

				// Token: 0x04003D19 RID: 15641
				internal UnsafeNclNativeMethods.HttpApi.HTTP_VERSION Version;

				// Token: 0x04003D1A RID: 15642
				internal UnsafeNclNativeMethods.HttpApi.HTTP_VERB Verb;

				// Token: 0x04003D1B RID: 15643
				internal ushort UnknownVerbLength;

				// Token: 0x04003D1C RID: 15644
				internal ushort RawUrlLength;

				// Token: 0x04003D1D RID: 15645
				internal unsafe sbyte* pUnknownVerb;

				// Token: 0x04003D1E RID: 15646
				internal unsafe sbyte* pRawUrl;

				// Token: 0x04003D1F RID: 15647
				internal UnsafeNclNativeMethods.HttpApi.HTTP_COOKED_URL CookedUrl;

				// Token: 0x04003D20 RID: 15648
				internal UnsafeNclNativeMethods.HttpApi.HTTP_TRANSPORT_ADDRESS Address;

				// Token: 0x04003D21 RID: 15649
				internal UnsafeNclNativeMethods.HttpApi.HTTP_REQUEST_HEADERS Headers;

				// Token: 0x04003D22 RID: 15650
				internal ulong BytesReceived;

				// Token: 0x04003D23 RID: 15651
				internal ushort EntityChunkCount;

				// Token: 0x04003D24 RID: 15652
				internal unsafe UnsafeNclNativeMethods.HttpApi.HTTP_DATA_CHUNK* pEntityChunks;

				// Token: 0x04003D25 RID: 15653
				internal ulong RawConnectionId;

				// Token: 0x04003D26 RID: 15654
				internal unsafe UnsafeNclNativeMethods.HttpApi.HTTP_SSL_INFO* pSslInfo;
			}

			// Token: 0x02000902 RID: 2306
			internal struct HTTP_REQUEST_V2
			{
				// Token: 0x04003D27 RID: 15655
				internal UnsafeNclNativeMethods.HttpApi.HTTP_REQUEST RequestV1;

				// Token: 0x04003D28 RID: 15656
				internal ushort RequestInfoCount;

				// Token: 0x04003D29 RID: 15657
				internal unsafe UnsafeNclNativeMethods.HttpApi.HTTP_REQUEST_INFO* pRequestInfo;
			}

			// Token: 0x02000903 RID: 2307
			internal struct HTTP_TIMEOUT_LIMIT_INFO
			{
				// Token: 0x04003D2A RID: 15658
				internal UnsafeNclNativeMethods.HttpApi.HTTP_FLAGS Flags;

				// Token: 0x04003D2B RID: 15659
				internal ushort EntityBody;

				// Token: 0x04003D2C RID: 15660
				internal ushort DrainEntityBody;

				// Token: 0x04003D2D RID: 15661
				internal ushort RequestQueue;

				// Token: 0x04003D2E RID: 15662
				internal ushort IdleConnection;

				// Token: 0x04003D2F RID: 15663
				internal ushort HeaderWait;

				// Token: 0x04003D30 RID: 15664
				internal uint MinSendRate;
			}

			// Token: 0x02000904 RID: 2308
			internal struct HTTP_BINDING_INFO
			{
				// Token: 0x04003D31 RID: 15665
				internal UnsafeNclNativeMethods.HttpApi.HTTP_FLAGS Flags;

				// Token: 0x04003D32 RID: 15666
				internal IntPtr RequestQueueHandle;
			}

			// Token: 0x02000905 RID: 2309
			internal struct HTTP_REQUEST_TOKEN_BINDING_INFO
			{
				// Token: 0x04003D33 RID: 15667
				public unsafe byte* TokenBinding;

				// Token: 0x04003D34 RID: 15668
				public uint TokenBindingSize;

				// Token: 0x04003D35 RID: 15669
				public unsafe byte* TlsUnique;

				// Token: 0x04003D36 RID: 15670
				public uint TlsUniqueSize;

				// Token: 0x04003D37 RID: 15671
				public UnsafeNclNativeMethods.HttpApi.TOKENBINDING_KEY_PARAMETERS_TYPE KeyType;
			}

			// Token: 0x02000906 RID: 2310
			internal struct HTTP_REQUEST_TOKEN_BINDING_INFO_V1
			{
				// Token: 0x04003D38 RID: 15672
				public unsafe byte* TokenBinding;

				// Token: 0x04003D39 RID: 15673
				public uint TokenBindingSize;

				// Token: 0x04003D3A RID: 15674
				public unsafe byte* TlsUnique;

				// Token: 0x04003D3B RID: 15675
				public uint TlsUniqueSize;

				// Token: 0x04003D3C RID: 15676
				public IntPtr KeyType;
			}

			// Token: 0x02000907 RID: 2311
			internal enum TOKENBINDING_HASH_ALGORITHM_V1 : byte
			{
				// Token: 0x04003D3E RID: 15678
				TOKENBINDING_HASH_ALGORITHM_SHA256 = 4
			}

			// Token: 0x02000908 RID: 2312
			internal enum TOKENBINDING_SIGNATURE_ALGORITHM_V1 : byte
			{
				// Token: 0x04003D40 RID: 15680
				TOKENBINDING_SIGNATURE_ALGORITHM_RSA = 1,
				// Token: 0x04003D41 RID: 15681
				TOKENBINDING_SIGNATURE_ALGORITHM_ECDSAP256 = 3
			}

			// Token: 0x02000909 RID: 2313
			internal enum TOKENBINDING_TYPE : byte
			{
				// Token: 0x04003D43 RID: 15683
				TOKENBINDING_TYPE_PROVIDED,
				// Token: 0x04003D44 RID: 15684
				TOKENBINDING_TYPE_REFERRED
			}

			// Token: 0x0200090A RID: 2314
			internal enum TOKENBINDING_EXTENSION_FORMAT
			{
				// Token: 0x04003D46 RID: 15686
				TOKENBINDING_EXTENSION_FORMAT_UNDEFINED
			}

			// Token: 0x0200090B RID: 2315
			internal enum TOKENBINDING_KEY_PARAMETERS_TYPE : byte
			{
				// Token: 0x04003D48 RID: 15688
				TOKENBINDING_KEY_PARAMETERS_TYPE_RSA_PKCS_SHA256,
				// Token: 0x04003D49 RID: 15689
				TOKENBINDING_KEY_PARAMETERS_TYPE_RSA_PSS_SHA256,
				// Token: 0x04003D4A RID: 15690
				TOKENBINDING_KEY_PARAMETERS_TYPE_ECDSA_SHA256
			}

			// Token: 0x0200090C RID: 2316
			internal struct TOKENBINDING_IDENTIFIER
			{
				// Token: 0x04003D4B RID: 15691
				public UnsafeNclNativeMethods.HttpApi.TOKENBINDING_KEY_PARAMETERS_TYPE keyType;
			}

			// Token: 0x0200090D RID: 2317
			internal struct TOKENBINDING_IDENTIFIER_V1
			{
				// Token: 0x04003D4C RID: 15692
				public UnsafeNclNativeMethods.HttpApi.TOKENBINDING_TYPE bindingType;

				// Token: 0x04003D4D RID: 15693
				public UnsafeNclNativeMethods.HttpApi.TOKENBINDING_HASH_ALGORITHM_V1 hashAlgorithm;

				// Token: 0x04003D4E RID: 15694
				public UnsafeNclNativeMethods.HttpApi.TOKENBINDING_SIGNATURE_ALGORITHM_V1 signatureAlgorithm;
			}

			// Token: 0x0200090E RID: 2318
			internal struct TOKENBINDING_RESULT_DATA
			{
				// Token: 0x04003D4F RID: 15695
				public UnsafeNclNativeMethods.HttpApi.TOKENBINDING_TYPE bindingType;

				// Token: 0x04003D50 RID: 15696
				public uint identifierSize;

				// Token: 0x04003D51 RID: 15697
				public unsafe UnsafeNclNativeMethods.HttpApi.TOKENBINDING_IDENTIFIER* identifierData;

				// Token: 0x04003D52 RID: 15698
				public UnsafeNclNativeMethods.HttpApi.TOKENBINDING_EXTENSION_FORMAT extensionFormat;

				// Token: 0x04003D53 RID: 15699
				public uint extensionSize;

				// Token: 0x04003D54 RID: 15700
				public IntPtr extensionData;
			}

			// Token: 0x0200090F RID: 2319
			internal struct TOKENBINDING_RESULT_DATA_V1
			{
				// Token: 0x04003D55 RID: 15701
				public uint identifierSize;

				// Token: 0x04003D56 RID: 15702
				public unsafe UnsafeNclNativeMethods.HttpApi.TOKENBINDING_IDENTIFIER_V1* identifierData;

				// Token: 0x04003D57 RID: 15703
				public UnsafeNclNativeMethods.HttpApi.TOKENBINDING_EXTENSION_FORMAT extensionFormat;

				// Token: 0x04003D58 RID: 15704
				public uint extensionSize;

				// Token: 0x04003D59 RID: 15705
				public IntPtr extensionData;
			}

			// Token: 0x02000910 RID: 2320
			internal struct TOKENBINDING_RESULT_LIST
			{
				// Token: 0x04003D5A RID: 15706
				public uint resultCount;

				// Token: 0x04003D5B RID: 15707
				public unsafe UnsafeNclNativeMethods.HttpApi.TOKENBINDING_RESULT_DATA* resultData;
			}

			// Token: 0x02000911 RID: 2321
			internal struct TOKENBINDING_RESULT_LIST_V1
			{
				// Token: 0x04003D5C RID: 15708
				public uint resultCount;

				// Token: 0x04003D5D RID: 15709
				public unsafe UnsafeNclNativeMethods.HttpApi.TOKENBINDING_RESULT_DATA_V1* resultData;
			}

			// Token: 0x02000912 RID: 2322
			[Flags]
			internal enum HTTP_FLAGS : uint
			{
				// Token: 0x04003D5F RID: 15711
				NONE = 0U,
				// Token: 0x04003D60 RID: 15712
				HTTP_RECEIVE_REQUEST_FLAG_COPY_BODY = 1U,
				// Token: 0x04003D61 RID: 15713
				HTTP_RECEIVE_SECURE_CHANNEL_TOKEN = 1U,
				// Token: 0x04003D62 RID: 15714
				HTTP_SEND_RESPONSE_FLAG_DISCONNECT = 1U,
				// Token: 0x04003D63 RID: 15715
				HTTP_SEND_RESPONSE_FLAG_MORE_DATA = 2U,
				// Token: 0x04003D64 RID: 15716
				HTTP_SEND_RESPONSE_FLAG_BUFFER_DATA = 4U,
				// Token: 0x04003D65 RID: 15717
				HTTP_SEND_RESPONSE_FLAG_RAW_HEADER = 4U,
				// Token: 0x04003D66 RID: 15718
				HTTP_SEND_REQUEST_FLAG_MORE_DATA = 1U,
				// Token: 0x04003D67 RID: 15719
				HTTP_PROPERTY_FLAG_PRESENT = 1U,
				// Token: 0x04003D68 RID: 15720
				HTTP_INITIALIZE_SERVER = 1U,
				// Token: 0x04003D69 RID: 15721
				HTTP_INITIALIZE_CBT = 4U,
				// Token: 0x04003D6A RID: 15722
				HTTP_SEND_RESPONSE_FLAG_OPAQUE = 64U
			}

			// Token: 0x02000913 RID: 2323
			internal static class HTTP_REQUEST_HEADER_ID
			{
				// Token: 0x06004642 RID: 17986 RVA: 0x00125631 File Offset: 0x00123831
				internal static string ToString(int position)
				{
					return UnsafeNclNativeMethods.HttpApi.HTTP_REQUEST_HEADER_ID.m_Strings[position];
				}

				// Token: 0x04003D6B RID: 15723
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

			// Token: 0x02000914 RID: 2324
			internal static class HTTP_RESPONSE_HEADER_ID
			{
				// Token: 0x06004644 RID: 17988 RVA: 0x001257C0 File Offset: 0x001239C0
				static HTTP_RESPONSE_HEADER_ID()
				{
					UnsafeNclNativeMethods.HttpApi.HTTP_RESPONSE_HEADER_ID.m_Hashtable = new Hashtable(30);
					for (int i = 0; i < 30; i++)
					{
						UnsafeNclNativeMethods.HttpApi.HTTP_RESPONSE_HEADER_ID.m_Hashtable.Add(UnsafeNclNativeMethods.HttpApi.HTTP_RESPONSE_HEADER_ID.m_Strings[i], i);
					}
				}

				// Token: 0x06004645 RID: 17989 RVA: 0x00125910 File Offset: 0x00123B10
				internal static int IndexOfKnownHeader(string HeaderName)
				{
					object obj = UnsafeNclNativeMethods.HttpApi.HTTP_RESPONSE_HEADER_ID.m_Hashtable[HeaderName];
					if (obj != null)
					{
						return (int)obj;
					}
					return -1;
				}

				// Token: 0x06004646 RID: 17990 RVA: 0x00125934 File Offset: 0x00123B34
				internal static string ToString(int position)
				{
					return UnsafeNclNativeMethods.HttpApi.HTTP_RESPONSE_HEADER_ID.m_Strings[position];
				}

				// Token: 0x04003D6C RID: 15724
				private static Hashtable m_Hashtable;

				// Token: 0x04003D6D RID: 15725
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

				// Token: 0x0200093B RID: 2363
				internal enum Enum
				{
					// Token: 0x04003DF5 RID: 15861
					HttpHeaderCacheControl,
					// Token: 0x04003DF6 RID: 15862
					HttpHeaderConnection,
					// Token: 0x04003DF7 RID: 15863
					HttpHeaderDate,
					// Token: 0x04003DF8 RID: 15864
					HttpHeaderKeepAlive,
					// Token: 0x04003DF9 RID: 15865
					HttpHeaderPragma,
					// Token: 0x04003DFA RID: 15866
					HttpHeaderTrailer,
					// Token: 0x04003DFB RID: 15867
					HttpHeaderTransferEncoding,
					// Token: 0x04003DFC RID: 15868
					HttpHeaderUpgrade,
					// Token: 0x04003DFD RID: 15869
					HttpHeaderVia,
					// Token: 0x04003DFE RID: 15870
					HttpHeaderWarning,
					// Token: 0x04003DFF RID: 15871
					HttpHeaderAllow,
					// Token: 0x04003E00 RID: 15872
					HttpHeaderContentLength,
					// Token: 0x04003E01 RID: 15873
					HttpHeaderContentType,
					// Token: 0x04003E02 RID: 15874
					HttpHeaderContentEncoding,
					// Token: 0x04003E03 RID: 15875
					HttpHeaderContentLanguage,
					// Token: 0x04003E04 RID: 15876
					HttpHeaderContentLocation,
					// Token: 0x04003E05 RID: 15877
					HttpHeaderContentMd5,
					// Token: 0x04003E06 RID: 15878
					HttpHeaderContentRange,
					// Token: 0x04003E07 RID: 15879
					HttpHeaderExpires,
					// Token: 0x04003E08 RID: 15880
					HttpHeaderLastModified,
					// Token: 0x04003E09 RID: 15881
					HttpHeaderAcceptRanges,
					// Token: 0x04003E0A RID: 15882
					HttpHeaderAge,
					// Token: 0x04003E0B RID: 15883
					HttpHeaderEtag,
					// Token: 0x04003E0C RID: 15884
					HttpHeaderLocation,
					// Token: 0x04003E0D RID: 15885
					HttpHeaderProxyAuthenticate,
					// Token: 0x04003E0E RID: 15886
					HttpHeaderRetryAfter,
					// Token: 0x04003E0F RID: 15887
					HttpHeaderServer,
					// Token: 0x04003E10 RID: 15888
					HttpHeaderSetCookie,
					// Token: 0x04003E11 RID: 15889
					HttpHeaderVary,
					// Token: 0x04003E12 RID: 15890
					HttpHeaderWwwAuthenticate,
					// Token: 0x04003E13 RID: 15891
					HttpHeaderResponseMaximum,
					// Token: 0x04003E14 RID: 15892
					HttpHeaderMaximum = 41
				}
			}
		}

		// Token: 0x02000722 RID: 1826
		[SuppressUnmanagedCodeSecurity]
		internal static class SecureStringHelper
		{
			// Token: 0x06004157 RID: 16727 RVA: 0x00110040 File Offset: 0x0010E240
			internal static string CreateString(SecureString secureString)
			{
				IntPtr intPtr = IntPtr.Zero;
				if (secureString == null || secureString.Length == 0)
				{
					return string.Empty;
				}
				string result;
				try
				{
					intPtr = Marshal.SecureStringToBSTR(secureString);
					result = Marshal.PtrToStringBSTR(intPtr);
				}
				finally
				{
					if (intPtr != IntPtr.Zero)
					{
						Marshal.ZeroFreeBSTR(intPtr);
					}
				}
				return result;
			}

			// Token: 0x06004158 RID: 16728 RVA: 0x0011009C File Offset: 0x0010E29C
			internal unsafe static SecureString CreateSecureString(string plainString)
			{
				if (plainString == null || plainString.Length == 0)
				{
					return new SecureString();
				}
				SecureString result;
				fixed (string text = plainString)
				{
					char* ptr = text;
					if (ptr != null)
					{
						ptr += RuntimeHelpers.OffsetToStringData / 2;
					}
					result = new SecureString(ptr, plainString.Length);
				}
				return result;
			}
		}

		// Token: 0x02000723 RID: 1827
		[FriendAccessAllowed]
		internal class AppXHelper
		{
			// Token: 0x06004159 RID: 16729 RVA: 0x001100DC File Offset: 0x0010E2DC
			[SecuritySafeCritical]
			private static IntPtr GetPrimaryWindowHandle()
			{
				IntPtr result = IntPtr.Zero;
				UnsafeNclNativeMethods.AppXHelper.GuiThreadInfo guiThreadInfo = default(UnsafeNclNativeMethods.AppXHelper.GuiThreadInfo);
				guiThreadInfo.cbSize = Marshal.SizeOf(guiThreadInfo);
				if (UnsafeNclNativeMethods.AppXHelper.GetGUIThreadInfo(0, ref guiThreadInfo) != 0U && guiThreadInfo.hwndActive != IntPtr.Zero)
				{
					int num;
					UnsafeNclNativeMethods.AppXHelper.GetWindowThreadProcessId(guiThreadInfo.hwndActive, out num);
					if (num == Process.GetCurrentProcess().Id)
					{
						result = guiThreadInfo.hwndActive;
					}
				}
				return result;
			}

			// Token: 0x0600415A RID: 16730
			[DllImport("user32.dll", ExactSpelling = true, SetLastError = true)]
			private static extern uint GetGUIThreadInfo(int threadId, ref UnsafeNclNativeMethods.AppXHelper.GuiThreadInfo info);

			// Token: 0x0600415B RID: 16731
			[DllImport("user32.dll", ExactSpelling = true)]
			private static extern uint GetWindowThreadProcessId(IntPtr hwnd, out int processId);

			// Token: 0x0400315A RID: 12634
			[SecuritySafeCritical]
			internal static Lazy<IntPtr> PrimaryWindowHandle = new Lazy<IntPtr>(() => UnsafeNclNativeMethods.AppXHelper.GetPrimaryWindowHandle());

			// Token: 0x02000915 RID: 2325
			private struct GuiThreadInfo
			{
				// Token: 0x04003D6E RID: 15726
				public int cbSize;

				// Token: 0x04003D6F RID: 15727
				public int flags;

				// Token: 0x04003D70 RID: 15728
				public IntPtr hwndActive;

				// Token: 0x04003D71 RID: 15729
				public IntPtr hwndFocus;

				// Token: 0x04003D72 RID: 15730
				public IntPtr hwndCapture;

				// Token: 0x04003D73 RID: 15731
				public IntPtr hwndMenuOwner;

				// Token: 0x04003D74 RID: 15732
				public IntPtr hwndMoveSize;

				// Token: 0x04003D75 RID: 15733
				public IntPtr hwndCaret;

				// Token: 0x04003D76 RID: 15734
				public int left;

				// Token: 0x04003D77 RID: 15735
				public int top;

				// Token: 0x04003D78 RID: 15736
				public int right;

				// Token: 0x04003D79 RID: 15737
				public int bottom;
			}
		}

		// Token: 0x02000724 RID: 1828
		internal static class TokenBindingOSHelper
		{
			// Token: 0x0600415E RID: 16734 RVA: 0x0011016C File Offset: 0x0010E36C
			[SecurityCritical]
			private static void EnsureTokenBindingOSHelperInitialized()
			{
				if (UnsafeNclNativeMethods.TokenBindingOSHelper.s_Initialized)
				{
					return;
				}
				object obj = UnsafeNclNativeMethods.TokenBindingOSHelper.s_Lock;
				lock (obj)
				{
					if (!UnsafeNclNativeMethods.TokenBindingOSHelper.s_Initialized)
					{
						try
						{
							string library = Path.Combine(Environment.SystemDirectory, "tokenbinding.dll");
							SafeLoadLibrary safeLoadLibrary = SafeLoadLibrary.LoadLibraryEx(library);
							if (!safeLoadLibrary.IsInvalid)
							{
								UnsafeNclNativeMethods.TokenBindingOSHelper.s_supportsTokenBinding = safeLoadLibrary.HasFunction("TokenBindingVerifyMessage");
							}
							UnsafeNclNativeMethods.TokenBindingOSHelper.s_Initialized = true;
						}
						catch (Exception exception)
						{
							if (NclUtilities.IsFatal(exception))
							{
								throw;
							}
						}
					}
				}
			}

			// Token: 0x17000EFB RID: 3835
			// (get) Token: 0x0600415F RID: 16735 RVA: 0x00110210 File Offset: 0x0010E410
			internal static bool SupportsTokenBinding
			{
				get
				{
					UnsafeNclNativeMethods.TokenBindingOSHelper.EnsureTokenBindingOSHelperInitialized();
					return UnsafeNclNativeMethods.TokenBindingOSHelper.s_supportsTokenBinding;
				}
			}

			// Token: 0x0400315B RID: 12635
			private static bool s_supportsTokenBinding = false;

			// Token: 0x0400315C RID: 12636
			private static object s_Lock = new object();

			// Token: 0x0400315D RID: 12637
			private static volatile bool s_Initialized = false;
		}
	}
}
