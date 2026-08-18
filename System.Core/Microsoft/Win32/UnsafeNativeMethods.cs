using System;
using System.Diagnostics.Eventing;
using System.Diagnostics.Eventing.Reader;
using System.IO;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Threading;
using Microsoft.Win32.SafeHandles;

namespace Microsoft.Win32
{
	// Token: 0x02000012 RID: 18
	[SuppressUnmanagedCodeSecurity]
	internal static class UnsafeNativeMethods
	{
		// Token: 0x0600005A RID: 90
		[SecurityCritical]
		[DllImport("kernel32.dll")]
		internal static extern int GetFileType(SafeFileHandle handle);

		// Token: 0x0600005B RID: 91
		[SecurityCritical]
		[DllImport("kernel32.dll", SetLastError = true)]
		internal unsafe static extern int WriteFile(SafeFileHandle handle, byte* bytes, int numBytesToWrite, out int numBytesWritten, NativeOverlapped* lpOverlapped);

		// Token: 0x0600005C RID: 92
		[SecurityCritical]
		[DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		private static extern SafeFileHandle CreateFile(string lpFileName, int dwDesiredAccess, FileShare dwShareMode, UnsafeNativeMethods.SECURITY_ATTRIBUTES securityAttrs, FileMode dwCreationDisposition, int dwFlagsAndAttributes, IntPtr hTemplateFile);

		// Token: 0x0600005D RID: 93 RVA: 0x000031F8 File Offset: 0x000013F8
		[SecurityCritical]
		internal static SafeFileHandle SafeCreateFile(string lpFileName, int dwDesiredAccess, FileShare dwShareMode, UnsafeNativeMethods.SECURITY_ATTRIBUTES securityAttrs, FileMode dwCreationDisposition, int dwFlagsAndAttributes, IntPtr hTemplateFile)
		{
			SafeFileHandle safeFileHandle = UnsafeNativeMethods.CreateFile(lpFileName, dwDesiredAccess, dwShareMode, securityAttrs, dwCreationDisposition, dwFlagsAndAttributes, hTemplateFile);
			if (!safeFileHandle.IsInvalid)
			{
				int fileType = UnsafeNativeMethods.GetFileType(safeFileHandle);
				if (fileType != 1)
				{
					safeFileHandle.Dispose();
					throw new NotSupportedException(SR.GetString("NotSupported_IONonFileDevices"));
				}
			}
			return safeFileHandle;
		}

		// Token: 0x0600005E RID: 94
		[SecurityCritical]
		[DllImport("kernel32.dll")]
		internal static extern int SetErrorMode(int newMode);

		// Token: 0x0600005F RID: 95
		[SecurityCritical]
		[DllImport("kernel32.dll", EntryPoint = "SetFilePointer", SetLastError = true)]
		private unsafe static extern int SetFilePointerWin32(SafeFileHandle handle, int lo, int* hi, int origin);

		// Token: 0x06000060 RID: 96 RVA: 0x00003240 File Offset: 0x00001440
		[SecurityCritical]
		internal unsafe static long SetFilePointer(SafeFileHandle handle, long offset, SeekOrigin origin, out int hr)
		{
			hr = 0;
			int num = (int)offset;
			int num2 = (int)(offset >> 32);
			num = UnsafeNativeMethods.SetFilePointerWin32(handle, num, &num2, (int)origin);
			if (num == -1 && (hr = Marshal.GetLastWin32Error()) != 0)
			{
				return -1L;
			}
			return (long)((ulong)num2 << 32 | (ulong)num);
		}

		// Token: 0x06000061 RID: 97 RVA: 0x00003280 File Offset: 0x00001480
		internal static int MakeHRFromErrorCode(int errorCode)
		{
			return -2147024896 | errorCode;
		}

		// Token: 0x06000062 RID: 98
		[SecurityCritical]
		[DllImport("kernel32.dll", BestFitMapping = false, CharSet = CharSet.Auto)]
		internal static extern int FormatMessage(int dwFlags, IntPtr lpSource, int dwMessageId, int dwLanguageId, StringBuilder lpBuffer, int nSize, IntPtr va_list_arguments);

		// Token: 0x06000063 RID: 99 RVA: 0x0000328C File Offset: 0x0000148C
		[SecurityCritical]
		internal static string GetMessage(int errorCode)
		{
			StringBuilder stringBuilder = new StringBuilder(512);
			int num = UnsafeNativeMethods.FormatMessage(12800, UnsafeNativeMethods.NULL, errorCode, 0, stringBuilder, stringBuilder.Capacity, UnsafeNativeMethods.NULL);
			if (num != 0)
			{
				return stringBuilder.ToString();
			}
			return "UnknownError_Num " + errorCode.ToString();
		}

		// Token: 0x06000064 RID: 100
		[SecurityCritical]
		[DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		internal static extern SafeLibraryHandle LoadLibraryEx(string libFilename, IntPtr reserved, int flags);

		// Token: 0x06000065 RID: 101
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[SecurityCritical]
		[DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool FreeLibrary(IntPtr hModule);

		// Token: 0x06000066 RID: 102
		[SecurityCritical]
		[DllImport("kernel32.dll", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool CloseHandle(IntPtr handle);

		// Token: 0x06000067 RID: 103
		[SecurityCritical]
		[DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		internal static extern IntPtr GetCurrentProcess();

		// Token: 0x06000068 RID: 104
		[SecurityCritical]
		[DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool DuplicateHandle(IntPtr hSourceProcessHandle, SafePipeHandle hSourceHandle, IntPtr hTargetProcessHandle, out SafePipeHandle lpTargetHandle, uint dwDesiredAccess, [MarshalAs(UnmanagedType.Bool)] bool bInheritHandle, uint dwOptions);

		// Token: 0x06000069 RID: 105
		[SecurityCritical]
		[DllImport("kernel32.dll")]
		internal static extern int GetFileType(SafePipeHandle handle);

		// Token: 0x0600006A RID: 106
		[SecurityCritical]
		[DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool CreatePipe(out SafePipeHandle hReadPipe, out SafePipeHandle hWritePipe, UnsafeNativeMethods.SECURITY_ATTRIBUTES lpPipeAttributes, int nSize);

		// Token: 0x0600006B RID: 107
		[SecurityCritical]
		[DllImport("kernel32.dll", BestFitMapping = false, CharSet = CharSet.Auto, EntryPoint = "CreateFile", SetLastError = true)]
		internal static extern SafePipeHandle CreateNamedPipeClient(string lpFileName, int dwDesiredAccess, FileShare dwShareMode, UnsafeNativeMethods.SECURITY_ATTRIBUTES securityAttrs, FileMode dwCreationDisposition, int dwFlagsAndAttributes, IntPtr hTemplateFile);

		// Token: 0x0600006C RID: 108
		[SecurityCritical]
		[DllImport("kernel32.dll", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal unsafe static extern bool ConnectNamedPipe(SafePipeHandle handle, NativeOverlapped* overlapped);

		// Token: 0x0600006D RID: 109
		[SecurityCritical]
		[DllImport("kernel32.dll", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool ConnectNamedPipe(SafePipeHandle handle, IntPtr overlapped);

		// Token: 0x0600006E RID: 110
		[SecurityCritical]
		[DllImport("kernel32.dll", BestFitMapping = false, CharSet = CharSet.Auto, SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool WaitNamedPipe(string name, int timeout);

		// Token: 0x0600006F RID: 111
		[SecurityCritical]
		[DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool GetNamedPipeHandleState(SafePipeHandle hNamedPipe, out int lpState, IntPtr lpCurInstances, IntPtr lpMaxCollectionCount, IntPtr lpCollectDataTimeout, IntPtr lpUserName, int nMaxUserNameSize);

		// Token: 0x06000070 RID: 112
		[SecurityCritical]
		[DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool GetNamedPipeHandleState(SafePipeHandle hNamedPipe, IntPtr lpState, out int lpCurInstances, IntPtr lpMaxCollectionCount, IntPtr lpCollectDataTimeout, IntPtr lpUserName, int nMaxUserNameSize);

		// Token: 0x06000071 RID: 113
		[SecurityCritical]
		[DllImport("kernel32.dll", BestFitMapping = false, CharSet = CharSet.Auto, SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool GetNamedPipeHandleState(SafePipeHandle hNamedPipe, IntPtr lpState, IntPtr lpCurInstances, IntPtr lpMaxCollectionCount, IntPtr lpCollectDataTimeout, StringBuilder lpUserName, int nMaxUserNameSize);

		// Token: 0x06000072 RID: 114
		[SecurityCritical]
		[DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool GetNamedPipeInfo(SafePipeHandle hNamedPipe, out int lpFlags, IntPtr lpOutBufferSize, IntPtr lpInBufferSize, IntPtr lpMaxInstances);

		// Token: 0x06000073 RID: 115
		[SecurityCritical]
		[DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool GetNamedPipeInfo(SafePipeHandle hNamedPipe, IntPtr lpFlags, out int lpOutBufferSize, IntPtr lpInBufferSize, IntPtr lpMaxInstances);

		// Token: 0x06000074 RID: 116
		[SecurityCritical]
		[DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool GetNamedPipeInfo(SafePipeHandle hNamedPipe, IntPtr lpFlags, IntPtr lpOutBufferSize, out int lpInBufferSize, IntPtr lpMaxInstances);

		// Token: 0x06000075 RID: 117
		[SecurityCritical]
		[DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal unsafe static extern bool SetNamedPipeHandleState(SafePipeHandle hNamedPipe, int* lpMode, IntPtr lpMaxCollectionCount, IntPtr lpCollectDataTimeout);

		// Token: 0x06000076 RID: 118
		[SecurityCritical]
		[DllImport("kernel32.dll", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool DisconnectNamedPipe(SafePipeHandle hNamedPipe);

		// Token: 0x06000077 RID: 119
		[SecurityCritical]
		[DllImport("kernel32.dll", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool FlushFileBuffers(SafePipeHandle hNamedPipe);

		// Token: 0x06000078 RID: 120
		[SecurityCritical]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		[DllImport("advapi32.dll", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool RevertToSelf();

		// Token: 0x06000079 RID: 121
		[SecurityCritical]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		[DllImport("advapi32.dll", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool ImpersonateNamedPipeClient(SafePipeHandle hNamedPipe);

		// Token: 0x0600007A RID: 122
		[SecurityCritical]
		[DllImport("kernel32.dll", BestFitMapping = false, CharSet = CharSet.Auto, SetLastError = true)]
		internal static extern SafePipeHandle CreateNamedPipe(string pipeName, int openMode, int pipeMode, int maxInstances, int outBufferSize, int inBufferSize, int defaultTimeout, UnsafeNativeMethods.SECURITY_ATTRIBUTES securityAttributes);

		// Token: 0x0600007B RID: 123
		[SecurityCritical]
		[DllImport("kernel32.dll", SetLastError = true)]
		internal unsafe static extern int ReadFile(SafePipeHandle handle, byte* bytes, int numBytesToRead, IntPtr numBytesRead_mustBeZero, NativeOverlapped* overlapped);

		// Token: 0x0600007C RID: 124
		[SecurityCritical]
		[DllImport("kernel32.dll", SetLastError = true)]
		internal unsafe static extern int ReadFile(SafePipeHandle handle, byte* bytes, int numBytesToRead, out int numBytesRead, IntPtr mustBeZero);

		// Token: 0x0600007D RID: 125
		[SecurityCritical]
		[DllImport("kernel32.dll", SetLastError = true)]
		internal unsafe static extern int WriteFile(SafePipeHandle handle, byte* bytes, int numBytesToWrite, IntPtr numBytesWritten_mustBeZero, NativeOverlapped* lpOverlapped);

		// Token: 0x0600007E RID: 126
		[SecurityCritical]
		[DllImport("kernel32.dll", SetLastError = true)]
		internal unsafe static extern int WriteFile(SafePipeHandle handle, byte* bytes, int numBytesToWrite, out int numBytesWritten, IntPtr mustBeZero);

		// Token: 0x0600007F RID: 127
		[SecurityCritical]
		[DllImport("kernel32.dll", SetLastError = true)]
		internal static extern bool SetEndOfFile(IntPtr hNamedPipe);

		// Token: 0x06000080 RID: 128
		[SecurityCritical]
		[DllImport("advapi32.dll", CharSet = CharSet.Unicode, ExactSpelling = true)]
		internal unsafe static extern uint EventRegister([In] ref Guid providerId, [In] UnsafeNativeMethods.EtwEnableCallback enableCallback, [In] void* callbackContext, [In] [Out] ref long registrationHandle);

		// Token: 0x06000081 RID: 129
		[SecurityCritical]
		[DllImport("advapi32.dll", CharSet = CharSet.Unicode, ExactSpelling = true)]
		internal static extern int EventUnregister([In] long registrationHandle);

		// Token: 0x06000082 RID: 130
		[SecurityCritical]
		[DllImport("advapi32.dll", CharSet = CharSet.Unicode, ExactSpelling = true)]
		internal static extern int EventEnabled([In] long registrationHandle, [In] ref EventDescriptor eventDescriptor);

		// Token: 0x06000083 RID: 131
		[SecurityCritical]
		[DllImport("advapi32.dll", CharSet = CharSet.Unicode, ExactSpelling = true)]
		internal static extern int EventProviderEnabled([In] long registrationHandle, [In] byte level, [In] long keywords);

		// Token: 0x06000084 RID: 132
		[SecurityCritical]
		[DllImport("advapi32.dll", CharSet = CharSet.Unicode, ExactSpelling = true)]
		internal unsafe static extern uint EventWrite([In] long registrationHandle, [In] ref EventDescriptor eventDescriptor, [In] uint userDataCount, [In] void* userData);

		// Token: 0x06000085 RID: 133
		[SecurityCritical]
		[DllImport("advapi32.dll", CharSet = CharSet.Unicode, ExactSpelling = true)]
		internal unsafe static extern uint EventWrite([In] long registrationHandle, [In] EventDescriptor* eventDescriptor, [In] uint userDataCount, [In] void* userData);

		// Token: 0x06000086 RID: 134
		[SecurityCritical]
		[DllImport("advapi32.dll", CharSet = CharSet.Unicode, ExactSpelling = true)]
		internal unsafe static extern uint EventWriteTransfer([In] long registrationHandle, [In] ref EventDescriptor eventDescriptor, [In] Guid* activityId, [In] Guid* relatedActivityId, [In] uint userDataCount, [In] void* userData);

		// Token: 0x06000087 RID: 135
		[SecurityCritical]
		[DllImport("advapi32.dll", CharSet = CharSet.Unicode, ExactSpelling = true)]
		internal unsafe static extern uint EventWriteString([In] long registrationHandle, [In] byte level, [In] long keywords, [In] char* message);

		// Token: 0x06000088 RID: 136
		[SecurityCritical]
		[DllImport("advapi32.dll", CharSet = CharSet.Unicode, ExactSpelling = true)]
		internal static extern uint EventActivityIdControl([In] int ControlCode, [In] [Out] ref Guid ActivityId);

		// Token: 0x06000089 RID: 137
		[SecurityCritical]
		[DllImport("advapi32.dll", CharSet = CharSet.Unicode, ExactSpelling = true)]
		internal static extern uint PerfStartProvider([In] ref Guid ProviderGuid, [In] UnsafeNativeMethods.PERFLIBREQUEST ControlCallback, out SafePerfProviderHandle phProvider);

		// Token: 0x0600008A RID: 138
		[SecurityCritical]
		[DllImport("advapi32.dll", CharSet = CharSet.Unicode, ExactSpelling = true)]
		internal static extern uint PerfStopProvider([In] IntPtr hProvider);

		// Token: 0x0600008B RID: 139
		[SecurityCritical]
		[DllImport("advapi32.dll", CharSet = CharSet.Unicode, ExactSpelling = true)]
		internal unsafe static extern uint PerfSetCounterSetInfo([In] SafePerfProviderHandle hProvider, [In] [Out] UnsafeNativeMethods.PerfCounterSetInfoStruct* pTemplate, [In] uint dwTemplateSize);

		// Token: 0x0600008C RID: 140
		[SecurityCritical]
		[DllImport("advapi32.dll", CharSet = CharSet.Unicode, ExactSpelling = true, SetLastError = true)]
		internal unsafe static extern UnsafeNativeMethods.PerfCounterSetInstanceStruct* PerfCreateInstance([In] SafePerfProviderHandle hProvider, [In] ref Guid CounterSetGuid, [In] string szInstanceName, [In] uint dwInstance);

		// Token: 0x0600008D RID: 141
		[SecurityCritical]
		[DllImport("advapi32.dll", CharSet = CharSet.Unicode, ExactSpelling = true)]
		internal unsafe static extern uint PerfDeleteInstance([In] SafePerfProviderHandle hProvider, [In] UnsafeNativeMethods.PerfCounterSetInstanceStruct* InstanceBlock);

		// Token: 0x0600008E RID: 142
		[SecurityCritical]
		[DllImport("advapi32.dll", CharSet = CharSet.Unicode, ExactSpelling = true)]
		internal unsafe static extern uint PerfSetCounterRefValue([In] SafePerfProviderHandle hProvider, [In] UnsafeNativeMethods.PerfCounterSetInstanceStruct* pInstance, [In] uint CounterId, [In] void* lpAddr);

		// Token: 0x0600008F RID: 143
		[SecurityCritical]
		[DllImport("wevtapi.dll", SetLastError = true)]
		internal static extern EventLogHandle EvtQuery(EventLogHandle session, [MarshalAs(UnmanagedType.LPWStr)] string path, [MarshalAs(UnmanagedType.LPWStr)] string query, int flags);

		// Token: 0x06000090 RID: 144
		[SecurityCritical]
		[DllImport("wevtapi.dll", CharSet = CharSet.Auto, SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool EvtSeek(EventLogHandle resultSet, long position, EventLogHandle bookmark, int timeout, [MarshalAs(UnmanagedType.I4)] UnsafeNativeMethods.EvtSeekFlags flags);

		// Token: 0x06000091 RID: 145
		[SecurityCritical]
		[DllImport("wevtapi.dll", SetLastError = true)]
		internal static extern EventLogHandle EvtSubscribe(EventLogHandle session, SafeWaitHandle signalEvent, [MarshalAs(UnmanagedType.LPWStr)] string path, [MarshalAs(UnmanagedType.LPWStr)] string query, EventLogHandle bookmark, IntPtr context, IntPtr callback, int flags);

		// Token: 0x06000092 RID: 146
		[SecurityCritical]
		[DllImport("wevtapi.dll", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool EvtNext(EventLogHandle queryHandle, int eventSize, [MarshalAs(UnmanagedType.LPArray)] IntPtr[] events, int timeout, int flags, ref int returned);

		// Token: 0x06000093 RID: 147
		[SecurityCritical]
		[DllImport("wevtapi.dll", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool EvtCancel(EventLogHandle handle);

		// Token: 0x06000094 RID: 148
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[SecurityCritical]
		[DllImport("wevtapi.dll")]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool EvtClose(IntPtr handle);

		// Token: 0x06000095 RID: 149
		[SecurityCritical]
		[DllImport("wevtapi.dll", CharSet = CharSet.Auto, SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool EvtGetEventInfo(EventLogHandle eventHandle, [MarshalAs(UnmanagedType.I4)] UnsafeNativeMethods.EvtEventPropertyId propertyId, int bufferSize, IntPtr bufferPtr, out int bufferUsed);

		// Token: 0x06000096 RID: 150
		[SecurityCritical]
		[DllImport("wevtapi.dll", CharSet = CharSet.Auto, SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool EvtGetQueryInfo(EventLogHandle queryHandle, [MarshalAs(UnmanagedType.I4)] UnsafeNativeMethods.EvtQueryPropertyId propertyId, int bufferSize, IntPtr buffer, ref int bufferRequired);

		// Token: 0x06000097 RID: 151
		[SecurityCritical]
		[DllImport("wevtapi.dll", CharSet = CharSet.Auto, SetLastError = true)]
		internal static extern EventLogHandle EvtOpenPublisherMetadata(EventLogHandle session, [MarshalAs(UnmanagedType.LPWStr)] string publisherId, [MarshalAs(UnmanagedType.LPWStr)] string logFilePath, int locale, int flags);

		// Token: 0x06000098 RID: 152
		[SecurityCritical]
		[DllImport("wevtapi.dll", CharSet = CharSet.Auto, SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool EvtGetPublisherMetadataProperty(EventLogHandle publisherMetadataHandle, [MarshalAs(UnmanagedType.I4)] UnsafeNativeMethods.EvtPublisherMetadataPropertyId propertyId, int flags, int publisherMetadataPropertyBufferSize, IntPtr publisherMetadataPropertyBuffer, out int publisherMetadataPropertyBufferUsed);

		// Token: 0x06000099 RID: 153
		[SecurityCritical]
		[DllImport("wevtapi.dll", CharSet = CharSet.Auto, SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool EvtGetObjectArraySize(EventLogHandle objectArray, out int objectArraySize);

		// Token: 0x0600009A RID: 154
		[SecurityCritical]
		[DllImport("wevtapi.dll", CharSet = CharSet.Auto, SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool EvtGetObjectArrayProperty(EventLogHandle objectArray, int propertyId, int arrayIndex, int flags, int propertyValueBufferSize, IntPtr propertyValueBuffer, out int propertyValueBufferUsed);

		// Token: 0x0600009B RID: 155
		[SecurityCritical]
		[DllImport("wevtapi.dll", CharSet = CharSet.Auto, SetLastError = true)]
		internal static extern EventLogHandle EvtOpenEventMetadataEnum(EventLogHandle publisherMetadata, int flags);

		// Token: 0x0600009C RID: 156
		[SecurityCritical]
		[DllImport("wevtapi.dll", CharSet = CharSet.Auto, SetLastError = true)]
		internal static extern EventLogHandle EvtNextEventMetadata(EventLogHandle eventMetadataEnum, int flags);

		// Token: 0x0600009D RID: 157
		[SecurityCritical]
		[DllImport("wevtapi.dll", CharSet = CharSet.Auto, SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool EvtGetEventMetadataProperty(EventLogHandle eventMetadata, [MarshalAs(UnmanagedType.I4)] UnsafeNativeMethods.EvtEventMetadataPropertyId propertyId, int flags, int eventMetadataPropertyBufferSize, IntPtr eventMetadataPropertyBuffer, out int eventMetadataPropertyBufferUsed);

		// Token: 0x0600009E RID: 158
		[SecurityCritical]
		[DllImport("wevtapi.dll", CharSet = CharSet.Auto, SetLastError = true)]
		internal static extern EventLogHandle EvtOpenChannelEnum(EventLogHandle session, int flags);

		// Token: 0x0600009F RID: 159
		[SecurityCritical]
		[DllImport("wevtapi.dll", CharSet = CharSet.Auto, SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool EvtNextChannelPath(EventLogHandle channelEnum, int channelPathBufferSize, [MarshalAs(UnmanagedType.LPWStr)] [Out] StringBuilder channelPathBuffer, out int channelPathBufferUsed);

		// Token: 0x060000A0 RID: 160
		[SecurityCritical]
		[DllImport("wevtapi.dll", CharSet = CharSet.Auto, SetLastError = true)]
		internal static extern EventLogHandle EvtOpenPublisherEnum(EventLogHandle session, int flags);

		// Token: 0x060000A1 RID: 161
		[SecurityCritical]
		[DllImport("wevtapi.dll", CharSet = CharSet.Auto, SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool EvtNextPublisherId(EventLogHandle publisherEnum, int publisherIdBufferSize, [MarshalAs(UnmanagedType.LPWStr)] [Out] StringBuilder publisherIdBuffer, out int publisherIdBufferUsed);

		// Token: 0x060000A2 RID: 162
		[SecurityCritical]
		[DllImport("wevtapi.dll", CharSet = CharSet.Auto, SetLastError = true)]
		internal static extern EventLogHandle EvtOpenChannelConfig(EventLogHandle session, [MarshalAs(UnmanagedType.LPWStr)] string channelPath, int flags);

		// Token: 0x060000A3 RID: 163
		[SecurityCritical]
		[DllImport("wevtapi.dll", CharSet = CharSet.Auto, SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool EvtSaveChannelConfig(EventLogHandle channelConfig, int flags);

		// Token: 0x060000A4 RID: 164
		[SecurityCritical]
		[DllImport("wevtapi.dll", CharSet = CharSet.Auto, SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool EvtSetChannelConfigProperty(EventLogHandle channelConfig, [MarshalAs(UnmanagedType.I4)] UnsafeNativeMethods.EvtChannelConfigPropertyId propertyId, int flags, ref UnsafeNativeMethods.EvtVariant propertyValue);

		// Token: 0x060000A5 RID: 165
		[SecurityCritical]
		[DllImport("wevtapi.dll", CharSet = CharSet.Auto, SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool EvtGetChannelConfigProperty(EventLogHandle channelConfig, [MarshalAs(UnmanagedType.I4)] UnsafeNativeMethods.EvtChannelConfigPropertyId propertyId, int flags, int propertyValueBufferSize, IntPtr propertyValueBuffer, out int propertyValueBufferUsed);

		// Token: 0x060000A6 RID: 166
		[SecurityCritical]
		[DllImport("wevtapi.dll", CharSet = CharSet.Auto, SetLastError = true)]
		internal static extern EventLogHandle EvtOpenLog(EventLogHandle session, [MarshalAs(UnmanagedType.LPWStr)] string path, [MarshalAs(UnmanagedType.I4)] PathType flags);

		// Token: 0x060000A7 RID: 167
		[SecurityCritical]
		[DllImport("wevtapi.dll", CharSet = CharSet.Auto, SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool EvtGetLogInfo(EventLogHandle log, [MarshalAs(UnmanagedType.I4)] UnsafeNativeMethods.EvtLogPropertyId propertyId, int propertyValueBufferSize, IntPtr propertyValueBuffer, out int propertyValueBufferUsed);

		// Token: 0x060000A8 RID: 168
		[SecurityCritical]
		[DllImport("wevtapi.dll", CharSet = CharSet.Auto, SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool EvtExportLog(EventLogHandle session, [MarshalAs(UnmanagedType.LPWStr)] string channelPath, [MarshalAs(UnmanagedType.LPWStr)] string query, [MarshalAs(UnmanagedType.LPWStr)] string targetFilePath, int flags);

		// Token: 0x060000A9 RID: 169
		[SecurityCritical]
		[DllImport("wevtapi.dll", CharSet = CharSet.Auto, SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool EvtArchiveExportedLog(EventLogHandle session, [MarshalAs(UnmanagedType.LPWStr)] string logFilePath, int locale, int flags);

		// Token: 0x060000AA RID: 170
		[SecurityCritical]
		[DllImport("wevtapi.dll", CharSet = CharSet.Auto, SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool EvtClearLog(EventLogHandle session, [MarshalAs(UnmanagedType.LPWStr)] string channelPath, [MarshalAs(UnmanagedType.LPWStr)] string targetFilePath, int flags);

		// Token: 0x060000AB RID: 171
		[SecurityCritical]
		[DllImport("wevtapi.dll", CharSet = CharSet.Auto, SetLastError = true)]
		internal static extern EventLogHandle EvtCreateRenderContext(int valuePathsCount, [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPWStr)] string[] valuePaths, [MarshalAs(UnmanagedType.I4)] UnsafeNativeMethods.EvtRenderContextFlags flags);

		// Token: 0x060000AC RID: 172
		[SecurityCritical]
		[DllImport("wevtapi.dll", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool EvtRender(EventLogHandle context, EventLogHandle eventHandle, UnsafeNativeMethods.EvtRenderFlags flags, int buffSize, [MarshalAs(UnmanagedType.LPWStr)] [Out] StringBuilder buffer, out int buffUsed, out int propCount);

		// Token: 0x060000AD RID: 173
		[SecurityCritical]
		[DllImport("wevtapi.dll", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool EvtRender(EventLogHandle context, EventLogHandle eventHandle, UnsafeNativeMethods.EvtRenderFlags flags, int buffSize, IntPtr buffer, out int buffUsed, out int propCount);

		// Token: 0x060000AE RID: 174
		[SecurityCritical]
		[DllImport("wevtapi.dll", CharSet = CharSet.Auto, SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool EvtFormatMessage(EventLogHandle publisherMetadataHandle, EventLogHandle eventHandle, uint messageId, int valueCount, UnsafeNativeMethods.EvtStringVariant[] values, [MarshalAs(UnmanagedType.I4)] UnsafeNativeMethods.EvtFormatMessageFlags flags, int bufferSize, [MarshalAs(UnmanagedType.LPWStr)] [Out] StringBuilder buffer, out int bufferUsed);

		// Token: 0x060000AF RID: 175
		[SecurityCritical]
		[DllImport("wevtapi.dll", CharSet = CharSet.Auto, EntryPoint = "EvtFormatMessage", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool EvtFormatMessageBuffer(EventLogHandle publisherMetadataHandle, EventLogHandle eventHandle, uint messageId, int valueCount, IntPtr values, [MarshalAs(UnmanagedType.I4)] UnsafeNativeMethods.EvtFormatMessageFlags flags, int bufferSize, IntPtr buffer, out int bufferUsed);

		// Token: 0x060000B0 RID: 176
		[SecurityCritical]
		[DllImport("wevtapi.dll", CharSet = CharSet.Auto, SetLastError = true)]
		internal static extern EventLogHandle EvtOpenSession([MarshalAs(UnmanagedType.I4)] UnsafeNativeMethods.EvtLoginClass loginClass, ref UnsafeNativeMethods.EvtRpcLogin login, int timeout, int flags);

		// Token: 0x060000B1 RID: 177
		[SecurityCritical]
		[DllImport("wevtapi.dll", CharSet = CharSet.Auto, SetLastError = true)]
		internal static extern EventLogHandle EvtCreateBookmark([MarshalAs(UnmanagedType.LPWStr)] string bookmarkXml);

		// Token: 0x060000B2 RID: 178
		[SecurityCritical]
		[DllImport("wevtapi.dll", CharSet = CharSet.Auto, SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool EvtUpdateBookmark(EventLogHandle bookmark, EventLogHandle eventHandle);

		// Token: 0x060000B3 RID: 179
		[SecurityCritical]
		[DllImport("kernel32.dll", SetLastError = true)]
		internal static extern void GetSystemInfo(ref UnsafeNativeMethods.SYSTEM_INFO lpSystemInfo);

		// Token: 0x060000B4 RID: 180
		[SecurityCritical]
		[DllImport("kernel32.dll", ExactSpelling = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool UnmapViewOfFile(IntPtr lpBaseAddress);

		// Token: 0x060000B5 RID: 181
		[SecurityCritical]
		[DllImport("kernel32.dll", SetLastError = true)]
		internal static extern int GetFileSize(SafeMemoryMappedFileHandle hFile, out int highSize);

		// Token: 0x060000B6 RID: 182
		[SecurityCritical]
		[DllImport("kernel32.dll", SetLastError = true)]
		internal static extern IntPtr VirtualQuery(SafeMemoryMappedViewHandle address, ref UnsafeNativeMethods.MEMORY_BASIC_INFORMATION buffer, IntPtr sizeOfBuffer);

		// Token: 0x060000B7 RID: 183
		[SecurityCritical]
		[DllImport("kernel32.dll", BestFitMapping = false, CharSet = CharSet.Auto, SetLastError = true)]
		internal static extern SafeMemoryMappedFileHandle CreateFileMapping(SafeFileHandle hFile, UnsafeNativeMethods.SECURITY_ATTRIBUTES lpAttributes, int fProtect, int dwMaximumSizeHigh, int dwMaximumSizeLow, string lpName);

		// Token: 0x060000B8 RID: 184
		[SecurityCritical]
		[DllImport("kernel32.dll", ExactSpelling = true, SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal unsafe static extern bool FlushViewOfFile(byte* lpBaseAddress, IntPtr dwNumberOfBytesToFlush);

		// Token: 0x060000B9 RID: 185
		[SecurityCritical]
		[DllImport("kernel32.dll", BestFitMapping = false, CharSet = CharSet.Auto, SetLastError = true)]
		internal static extern SafeMemoryMappedFileHandle OpenFileMapping(int dwDesiredAccess, [MarshalAs(UnmanagedType.Bool)] bool bInheritHandle, string lpName);

		// Token: 0x060000BA RID: 186
		[SecurityCritical]
		[DllImport("kernel32.dll", ExactSpelling = true, SetLastError = true)]
		internal static extern SafeMemoryMappedViewHandle MapViewOfFile(SafeMemoryMappedFileHandle handle, int dwDesiredAccess, uint dwFileOffsetHigh, uint dwFileOffsetLow, UIntPtr dwNumberOfBytesToMap);

		// Token: 0x060000BB RID: 187
		[SecurityCritical]
		[DllImport("kernel32.dll", SetLastError = true)]
		internal static extern IntPtr VirtualAlloc(SafeMemoryMappedViewHandle address, UIntPtr numBytes, int commitOrReserve, int pageProtectionMode);

		// Token: 0x060000BC RID: 188 RVA: 0x000032DF File Offset: 0x000014DF
		[SecurityCritical]
		internal static bool GlobalMemoryStatusEx(ref UnsafeNativeMethods.MEMORYSTATUSEX lpBuffer)
		{
			lpBuffer.dwLength = (uint)Marshal.SizeOf(typeof(UnsafeNativeMethods.MEMORYSTATUSEX));
			return UnsafeNativeMethods.GlobalMemoryStatusExNative(ref lpBuffer);
		}

		// Token: 0x060000BD RID: 189
		[SecurityCritical]
		[DllImport("kernel32.dll", CharSet = CharSet.Auto, EntryPoint = "GlobalMemoryStatusEx", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		private static extern bool GlobalMemoryStatusExNative([In] [Out] ref UnsafeNativeMethods.MEMORYSTATUSEX lpBuffer);

		// Token: 0x060000BE RID: 190
		[SecurityCritical]
		[DllImport("kernel32.dll", SetLastError = true)]
		internal unsafe static extern bool CancelIoEx(SafeHandle handle, NativeOverlapped* lpOverlapped);

		// Token: 0x04000074 RID: 116
		internal const string KERNEL32 = "kernel32.dll";

		// Token: 0x04000075 RID: 117
		internal const string ADVAPI32 = "advapi32.dll";

		// Token: 0x04000076 RID: 118
		internal const string WEVTAPI = "wevtapi.dll";

		// Token: 0x04000077 RID: 119
		internal static readonly IntPtr NULL = IntPtr.Zero;

		// Token: 0x04000078 RID: 120
		internal const int CREDUI_MAX_USERNAME_LENGTH = 513;

		// Token: 0x04000079 RID: 121
		internal const int ERROR_SUCCESS = 0;

		// Token: 0x0400007A RID: 122
		internal const int ERROR_FILE_NOT_FOUND = 2;

		// Token: 0x0400007B RID: 123
		internal const int ERROR_PATH_NOT_FOUND = 3;

		// Token: 0x0400007C RID: 124
		internal const int ERROR_ACCESS_DENIED = 5;

		// Token: 0x0400007D RID: 125
		internal const int ERROR_INVALID_HANDLE = 6;

		// Token: 0x0400007E RID: 126
		internal const int ERROR_NOT_ENOUGH_MEMORY = 8;

		// Token: 0x0400007F RID: 127
		internal const int ERROR_INVALID_DRIVE = 15;

		// Token: 0x04000080 RID: 128
		internal const int ERROR_NO_MORE_FILES = 18;

		// Token: 0x04000081 RID: 129
		internal const int ERROR_NOT_READY = 21;

		// Token: 0x04000082 RID: 130
		internal const int ERROR_BAD_LENGTH = 24;

		// Token: 0x04000083 RID: 131
		internal const int ERROR_SHARING_VIOLATION = 32;

		// Token: 0x04000084 RID: 132
		internal const int ERROR_LOCK_VIOLATION = 33;

		// Token: 0x04000085 RID: 133
		internal const int ERROR_HANDLE_EOF = 38;

		// Token: 0x04000086 RID: 134
		internal const int ERROR_FILE_EXISTS = 80;

		// Token: 0x04000087 RID: 135
		internal const int ERROR_INVALID_PARAMETER = 87;

		// Token: 0x04000088 RID: 136
		internal const int ERROR_BROKEN_PIPE = 109;

		// Token: 0x04000089 RID: 137
		internal const int ERROR_INSUFFICIENT_BUFFER = 122;

		// Token: 0x0400008A RID: 138
		internal const int ERROR_INVALID_NAME = 123;

		// Token: 0x0400008B RID: 139
		internal const int ERROR_BAD_PATHNAME = 161;

		// Token: 0x0400008C RID: 140
		internal const int ERROR_ALREADY_EXISTS = 183;

		// Token: 0x0400008D RID: 141
		internal const int ERROR_ENVVAR_NOT_FOUND = 203;

		// Token: 0x0400008E RID: 142
		internal const int ERROR_FILENAME_EXCED_RANGE = 206;

		// Token: 0x0400008F RID: 143
		internal const int ERROR_PIPE_BUSY = 231;

		// Token: 0x04000090 RID: 144
		internal const int ERROR_NO_DATA = 232;

		// Token: 0x04000091 RID: 145
		internal const int ERROR_PIPE_NOT_CONNECTED = 233;

		// Token: 0x04000092 RID: 146
		internal const int ERROR_MORE_DATA = 234;

		// Token: 0x04000093 RID: 147
		internal const int ERROR_NO_MORE_ITEMS = 259;

		// Token: 0x04000094 RID: 148
		internal const int ERROR_PIPE_CONNECTED = 535;

		// Token: 0x04000095 RID: 149
		internal const int ERROR_PIPE_LISTENING = 536;

		// Token: 0x04000096 RID: 150
		internal const int ERROR_OPERATION_ABORTED = 995;

		// Token: 0x04000097 RID: 151
		internal const int ERROR_IO_PENDING = 997;

		// Token: 0x04000098 RID: 152
		internal const int ERROR_NOT_FOUND = 1168;

		// Token: 0x04000099 RID: 153
		internal const int ERROR_ARITHMETIC_OVERFLOW = 534;

		// Token: 0x0400009A RID: 154
		internal const int ERROR_RESOURCE_LANG_NOT_FOUND = 1815;

		// Token: 0x0400009B RID: 155
		internal const int ERROR_EVT_MESSAGE_NOT_FOUND = 15027;

		// Token: 0x0400009C RID: 156
		internal const int ERROR_EVT_MESSAGE_ID_NOT_FOUND = 15028;

		// Token: 0x0400009D RID: 157
		internal const int ERROR_EVT_UNRESOLVED_VALUE_INSERT = 15029;

		// Token: 0x0400009E RID: 158
		internal const int ERROR_EVT_UNRESOLVED_PARAMETER_INSERT = 15030;

		// Token: 0x0400009F RID: 159
		internal const int ERROR_EVT_MAX_INSERTS_REACHED = 15031;

		// Token: 0x040000A0 RID: 160
		internal const int ERROR_EVT_MESSAGE_LOCALE_NOT_FOUND = 15033;

		// Token: 0x040000A1 RID: 161
		internal const int ERROR_MUI_FILE_NOT_FOUND = 15100;

		// Token: 0x040000A2 RID: 162
		internal const int SECURITY_SQOS_PRESENT = 1048576;

		// Token: 0x040000A3 RID: 163
		internal const int SECURITY_ANONYMOUS = 0;

		// Token: 0x040000A4 RID: 164
		internal const int SECURITY_IDENTIFICATION = 65536;

		// Token: 0x040000A5 RID: 165
		internal const int SECURITY_IMPERSONATION = 131072;

		// Token: 0x040000A6 RID: 166
		internal const int SECURITY_DELEGATION = 196608;

		// Token: 0x040000A7 RID: 167
		internal const int GENERIC_READ = -2147483648;

		// Token: 0x040000A8 RID: 168
		internal const int GENERIC_WRITE = 1073741824;

		// Token: 0x040000A9 RID: 169
		internal const int STD_INPUT_HANDLE = -10;

		// Token: 0x040000AA RID: 170
		internal const int STD_OUTPUT_HANDLE = -11;

		// Token: 0x040000AB RID: 171
		internal const int STD_ERROR_HANDLE = -12;

		// Token: 0x040000AC RID: 172
		internal const int DUPLICATE_SAME_ACCESS = 2;

		// Token: 0x040000AD RID: 173
		internal const int PIPE_ACCESS_INBOUND = 1;

		// Token: 0x040000AE RID: 174
		internal const int PIPE_ACCESS_OUTBOUND = 2;

		// Token: 0x040000AF RID: 175
		internal const int PIPE_ACCESS_DUPLEX = 3;

		// Token: 0x040000B0 RID: 176
		internal const int PIPE_TYPE_BYTE = 0;

		// Token: 0x040000B1 RID: 177
		internal const int PIPE_TYPE_MESSAGE = 4;

		// Token: 0x040000B2 RID: 178
		internal const int PIPE_READMODE_BYTE = 0;

		// Token: 0x040000B3 RID: 179
		internal const int PIPE_READMODE_MESSAGE = 2;

		// Token: 0x040000B4 RID: 180
		internal const int PIPE_UNLIMITED_INSTANCES = 255;

		// Token: 0x040000B5 RID: 181
		internal const int FILE_FLAG_FIRST_PIPE_INSTANCE = 524288;

		// Token: 0x040000B6 RID: 182
		internal const int FILE_SHARE_READ = 1;

		// Token: 0x040000B7 RID: 183
		internal const int FILE_SHARE_WRITE = 2;

		// Token: 0x040000B8 RID: 184
		internal const int FILE_ATTRIBUTE_NORMAL = 128;

		// Token: 0x040000B9 RID: 185
		internal const int FILE_FLAG_OVERLAPPED = 1073741824;

		// Token: 0x040000BA RID: 186
		internal const int OPEN_EXISTING = 3;

		// Token: 0x040000BB RID: 187
		internal const int FILE_TYPE_DISK = 1;

		// Token: 0x040000BC RID: 188
		internal const int FILE_TYPE_CHAR = 2;

		// Token: 0x040000BD RID: 189
		internal const int FILE_TYPE_PIPE = 3;

		// Token: 0x040000BE RID: 190
		internal const int MEM_COMMIT = 4096;

		// Token: 0x040000BF RID: 191
		internal const int MEM_RESERVE = 8192;

		// Token: 0x040000C0 RID: 192
		internal const int INVALID_FILE_SIZE = -1;

		// Token: 0x040000C1 RID: 193
		internal const int PAGE_READWRITE = 4;

		// Token: 0x040000C2 RID: 194
		internal const int PAGE_READONLY = 2;

		// Token: 0x040000C3 RID: 195
		internal const int PAGE_WRITECOPY = 8;

		// Token: 0x040000C4 RID: 196
		internal const int PAGE_EXECUTE_READ = 32;

		// Token: 0x040000C5 RID: 197
		internal const int PAGE_EXECUTE_READWRITE = 64;

		// Token: 0x040000C6 RID: 198
		internal const int FILE_MAP_COPY = 1;

		// Token: 0x040000C7 RID: 199
		internal const int FILE_MAP_WRITE = 2;

		// Token: 0x040000C8 RID: 200
		internal const int FILE_MAP_READ = 4;

		// Token: 0x040000C9 RID: 201
		internal const int FILE_MAP_EXECUTE = 32;

		// Token: 0x040000CA RID: 202
		internal const int SEM_FAILCRITICALERRORS = 1;

		// Token: 0x040000CB RID: 203
		private const int FORMAT_MESSAGE_IGNORE_INSERTS = 512;

		// Token: 0x040000CC RID: 204
		private const int FORMAT_MESSAGE_FROM_SYSTEM = 4096;

		// Token: 0x040000CD RID: 205
		private const int FORMAT_MESSAGE_ARGUMENT_ARRAY = 8192;

		// Token: 0x020002D8 RID: 728
		[StructLayout(LayoutKind.Sequential)]
		internal class SECURITY_ATTRIBUTES
		{
			// Token: 0x04000CE6 RID: 3302
			internal int nLength;

			// Token: 0x04000CE7 RID: 3303
			[SecurityCritical]
			internal unsafe byte* pSecurityDescriptor;

			// Token: 0x04000CE8 RID: 3304
			internal int bInheritHandle;
		}

		// Token: 0x020002D9 RID: 729
		// (Invoke) Token: 0x06001A40 RID: 6720
		[SecurityCritical(SecurityCriticalScope.Everything)]
		internal unsafe delegate void EtwEnableCallback([In] ref Guid sourceId, [In] int isEnabled, [In] byte level, [In] long matchAnyKeywords, [In] long matchAllKeywords, [In] void* filterData, [In] void* callbackContext);

		// Token: 0x020002DA RID: 730
		[StructLayout(LayoutKind.Explicit, Size = 40)]
		internal struct PerfCounterSetInfoStruct
		{
			// Token: 0x04000CE9 RID: 3305
			[FieldOffset(0)]
			internal Guid CounterSetGuid;

			// Token: 0x04000CEA RID: 3306
			[FieldOffset(16)]
			internal Guid ProviderGuid;

			// Token: 0x04000CEB RID: 3307
			[FieldOffset(32)]
			internal uint NumCounters;

			// Token: 0x04000CEC RID: 3308
			[FieldOffset(36)]
			internal uint InstanceType;
		}

		// Token: 0x020002DB RID: 731
		[StructLayout(LayoutKind.Explicit, Size = 32)]
		internal struct PerfCounterInfoStruct
		{
			// Token: 0x04000CED RID: 3309
			[FieldOffset(0)]
			internal uint CounterId;

			// Token: 0x04000CEE RID: 3310
			[FieldOffset(4)]
			internal uint CounterType;

			// Token: 0x04000CEF RID: 3311
			[FieldOffset(8)]
			internal long Attrib;

			// Token: 0x04000CF0 RID: 3312
			[FieldOffset(16)]
			internal uint Size;

			// Token: 0x04000CF1 RID: 3313
			[FieldOffset(20)]
			internal uint DetailLevel;

			// Token: 0x04000CF2 RID: 3314
			[FieldOffset(24)]
			internal uint Scale;

			// Token: 0x04000CF3 RID: 3315
			[FieldOffset(28)]
			internal uint Offset;
		}

		// Token: 0x020002DC RID: 732
		[StructLayout(LayoutKind.Explicit, Size = 32)]
		internal struct PerfCounterSetInstanceStruct
		{
			// Token: 0x04000CF4 RID: 3316
			[FieldOffset(0)]
			internal Guid CounterSetGuid;

			// Token: 0x04000CF5 RID: 3317
			[FieldOffset(16)]
			internal uint dwSize;

			// Token: 0x04000CF6 RID: 3318
			[FieldOffset(20)]
			internal uint InstanceId;

			// Token: 0x04000CF7 RID: 3319
			[FieldOffset(24)]
			internal uint InstanceNameOffset;

			// Token: 0x04000CF8 RID: 3320
			[FieldOffset(28)]
			internal uint InstanceNameSize;
		}

		// Token: 0x020002DD RID: 733
		// (Invoke) Token: 0x06001A44 RID: 6724
		[SecurityCritical(SecurityCriticalScope.Everything)]
		internal unsafe delegate uint PERFLIBREQUEST([In] uint RequestCode, [In] void* Buffer, [In] uint BufferSize);

		// Token: 0x020002DE RID: 734
		[Flags]
		internal enum EvtQueryFlags
		{
			// Token: 0x04000CFA RID: 3322
			EvtQueryChannelPath = 1,
			// Token: 0x04000CFB RID: 3323
			EvtQueryFilePath = 2,
			// Token: 0x04000CFC RID: 3324
			EvtQueryForwardDirection = 256,
			// Token: 0x04000CFD RID: 3325
			EvtQueryReverseDirection = 512,
			// Token: 0x04000CFE RID: 3326
			EvtQueryTolerateQueryErrors = 4096
		}

		// Token: 0x020002DF RID: 735
		[Flags]
		internal enum EvtSubscribeFlags
		{
			// Token: 0x04000D00 RID: 3328
			EvtSubscribeToFutureEvents = 1,
			// Token: 0x04000D01 RID: 3329
			EvtSubscribeStartAtOldestRecord = 2,
			// Token: 0x04000D02 RID: 3330
			EvtSubscribeStartAfterBookmark = 3,
			// Token: 0x04000D03 RID: 3331
			EvtSubscribeTolerateQueryErrors = 4096,
			// Token: 0x04000D04 RID: 3332
			EvtSubscribeStrict = 65536
		}

		// Token: 0x020002E0 RID: 736
		internal enum EvtVariantType
		{
			// Token: 0x04000D06 RID: 3334
			EvtVarTypeNull,
			// Token: 0x04000D07 RID: 3335
			EvtVarTypeString,
			// Token: 0x04000D08 RID: 3336
			EvtVarTypeAnsiString,
			// Token: 0x04000D09 RID: 3337
			EvtVarTypeSByte,
			// Token: 0x04000D0A RID: 3338
			EvtVarTypeByte,
			// Token: 0x04000D0B RID: 3339
			EvtVarTypeInt16,
			// Token: 0x04000D0C RID: 3340
			EvtVarTypeUInt16,
			// Token: 0x04000D0D RID: 3341
			EvtVarTypeInt32,
			// Token: 0x04000D0E RID: 3342
			EvtVarTypeUInt32,
			// Token: 0x04000D0F RID: 3343
			EvtVarTypeInt64,
			// Token: 0x04000D10 RID: 3344
			EvtVarTypeUInt64,
			// Token: 0x04000D11 RID: 3345
			EvtVarTypeSingle,
			// Token: 0x04000D12 RID: 3346
			EvtVarTypeDouble,
			// Token: 0x04000D13 RID: 3347
			EvtVarTypeBoolean,
			// Token: 0x04000D14 RID: 3348
			EvtVarTypeBinary,
			// Token: 0x04000D15 RID: 3349
			EvtVarTypeGuid,
			// Token: 0x04000D16 RID: 3350
			EvtVarTypeSizeT,
			// Token: 0x04000D17 RID: 3351
			EvtVarTypeFileTime,
			// Token: 0x04000D18 RID: 3352
			EvtVarTypeSysTime,
			// Token: 0x04000D19 RID: 3353
			EvtVarTypeSid,
			// Token: 0x04000D1A RID: 3354
			EvtVarTypeHexInt32,
			// Token: 0x04000D1B RID: 3355
			EvtVarTypeHexInt64,
			// Token: 0x04000D1C RID: 3356
			EvtVarTypeEvtHandle = 32,
			// Token: 0x04000D1D RID: 3357
			EvtVarTypeEvtXml = 35,
			// Token: 0x04000D1E RID: 3358
			EvtVarTypeStringArray = 129,
			// Token: 0x04000D1F RID: 3359
			EvtVarTypeUInt32Array = 136
		}

		// Token: 0x020002E1 RID: 737
		internal enum EvtMasks
		{
			// Token: 0x04000D21 RID: 3361
			EVT_VARIANT_TYPE_MASK = 127,
			// Token: 0x04000D22 RID: 3362
			EVT_VARIANT_TYPE_ARRAY
		}

		// Token: 0x020002E2 RID: 738
		internal struct SystemTime
		{
			// Token: 0x04000D23 RID: 3363
			[MarshalAs(UnmanagedType.U2)]
			public short Year;

			// Token: 0x04000D24 RID: 3364
			[MarshalAs(UnmanagedType.U2)]
			public short Month;

			// Token: 0x04000D25 RID: 3365
			[MarshalAs(UnmanagedType.U2)]
			public short DayOfWeek;

			// Token: 0x04000D26 RID: 3366
			[MarshalAs(UnmanagedType.U2)]
			public short Day;

			// Token: 0x04000D27 RID: 3367
			[MarshalAs(UnmanagedType.U2)]
			public short Hour;

			// Token: 0x04000D28 RID: 3368
			[MarshalAs(UnmanagedType.U2)]
			public short Minute;

			// Token: 0x04000D29 RID: 3369
			[MarshalAs(UnmanagedType.U2)]
			public short Second;

			// Token: 0x04000D2A RID: 3370
			[MarshalAs(UnmanagedType.U2)]
			public short Milliseconds;
		}

		// Token: 0x020002E3 RID: 739
		[SecurityCritical(SecurityCriticalScope.Everything)]
		[StructLayout(LayoutKind.Explicit, CharSet = CharSet.Auto)]
		internal struct EvtVariant
		{
			// Token: 0x04000D2B RID: 3371
			[FieldOffset(0)]
			public uint UInteger;

			// Token: 0x04000D2C RID: 3372
			[FieldOffset(0)]
			public int Integer;

			// Token: 0x04000D2D RID: 3373
			[FieldOffset(0)]
			public byte UInt8;

			// Token: 0x04000D2E RID: 3374
			[FieldOffset(0)]
			public short Short;

			// Token: 0x04000D2F RID: 3375
			[FieldOffset(0)]
			public ushort UShort;

			// Token: 0x04000D30 RID: 3376
			[FieldOffset(0)]
			public uint Bool;

			// Token: 0x04000D31 RID: 3377
			[FieldOffset(0)]
			public byte ByteVal;

			// Token: 0x04000D32 RID: 3378
			[FieldOffset(0)]
			public byte SByte;

			// Token: 0x04000D33 RID: 3379
			[FieldOffset(0)]
			public ulong ULong;

			// Token: 0x04000D34 RID: 3380
			[FieldOffset(0)]
			public long Long;

			// Token: 0x04000D35 RID: 3381
			[FieldOffset(0)]
			public float Single;

			// Token: 0x04000D36 RID: 3382
			[FieldOffset(0)]
			public double Double;

			// Token: 0x04000D37 RID: 3383
			[FieldOffset(0)]
			public IntPtr StringVal;

			// Token: 0x04000D38 RID: 3384
			[FieldOffset(0)]
			public IntPtr AnsiString;

			// Token: 0x04000D39 RID: 3385
			[FieldOffset(0)]
			public IntPtr SidVal;

			// Token: 0x04000D3A RID: 3386
			[FieldOffset(0)]
			public IntPtr Binary;

			// Token: 0x04000D3B RID: 3387
			[FieldOffset(0)]
			public IntPtr Reference;

			// Token: 0x04000D3C RID: 3388
			[FieldOffset(0)]
			public IntPtr Handle;

			// Token: 0x04000D3D RID: 3389
			[FieldOffset(0)]
			public IntPtr GuidReference;

			// Token: 0x04000D3E RID: 3390
			[FieldOffset(0)]
			public ulong FileTime;

			// Token: 0x04000D3F RID: 3391
			[FieldOffset(0)]
			public IntPtr SystemTime;

			// Token: 0x04000D40 RID: 3392
			[FieldOffset(0)]
			public IntPtr SizeT;

			// Token: 0x04000D41 RID: 3393
			[FieldOffset(8)]
			public uint Count;

			// Token: 0x04000D42 RID: 3394
			[FieldOffset(12)]
			public uint Type;
		}

		// Token: 0x020002E4 RID: 740
		internal enum EvtEventPropertyId
		{
			// Token: 0x04000D44 RID: 3396
			EvtEventQueryIDs,
			// Token: 0x04000D45 RID: 3397
			EvtEventPath
		}

		// Token: 0x020002E5 RID: 741
		internal enum EvtQueryPropertyId
		{
			// Token: 0x04000D47 RID: 3399
			EvtQueryNames,
			// Token: 0x04000D48 RID: 3400
			EvtQueryStatuses
		}

		// Token: 0x020002E6 RID: 742
		internal enum EvtPublisherMetadataPropertyId
		{
			// Token: 0x04000D4A RID: 3402
			EvtPublisherMetadataPublisherGuid,
			// Token: 0x04000D4B RID: 3403
			EvtPublisherMetadataResourceFilePath,
			// Token: 0x04000D4C RID: 3404
			EvtPublisherMetadataParameterFilePath,
			// Token: 0x04000D4D RID: 3405
			EvtPublisherMetadataMessageFilePath,
			// Token: 0x04000D4E RID: 3406
			EvtPublisherMetadataHelpLink,
			// Token: 0x04000D4F RID: 3407
			EvtPublisherMetadataPublisherMessageID,
			// Token: 0x04000D50 RID: 3408
			EvtPublisherMetadataChannelReferences,
			// Token: 0x04000D51 RID: 3409
			EvtPublisherMetadataChannelReferencePath,
			// Token: 0x04000D52 RID: 3410
			EvtPublisherMetadataChannelReferenceIndex,
			// Token: 0x04000D53 RID: 3411
			EvtPublisherMetadataChannelReferenceID,
			// Token: 0x04000D54 RID: 3412
			EvtPublisherMetadataChannelReferenceFlags,
			// Token: 0x04000D55 RID: 3413
			EvtPublisherMetadataChannelReferenceMessageID,
			// Token: 0x04000D56 RID: 3414
			EvtPublisherMetadataLevels,
			// Token: 0x04000D57 RID: 3415
			EvtPublisherMetadataLevelName,
			// Token: 0x04000D58 RID: 3416
			EvtPublisherMetadataLevelValue,
			// Token: 0x04000D59 RID: 3417
			EvtPublisherMetadataLevelMessageID,
			// Token: 0x04000D5A RID: 3418
			EvtPublisherMetadataTasks,
			// Token: 0x04000D5B RID: 3419
			EvtPublisherMetadataTaskName,
			// Token: 0x04000D5C RID: 3420
			EvtPublisherMetadataTaskEventGuid,
			// Token: 0x04000D5D RID: 3421
			EvtPublisherMetadataTaskValue,
			// Token: 0x04000D5E RID: 3422
			EvtPublisherMetadataTaskMessageID,
			// Token: 0x04000D5F RID: 3423
			EvtPublisherMetadataOpcodes,
			// Token: 0x04000D60 RID: 3424
			EvtPublisherMetadataOpcodeName,
			// Token: 0x04000D61 RID: 3425
			EvtPublisherMetadataOpcodeValue,
			// Token: 0x04000D62 RID: 3426
			EvtPublisherMetadataOpcodeMessageID,
			// Token: 0x04000D63 RID: 3427
			EvtPublisherMetadataKeywords,
			// Token: 0x04000D64 RID: 3428
			EvtPublisherMetadataKeywordName,
			// Token: 0x04000D65 RID: 3429
			EvtPublisherMetadataKeywordValue,
			// Token: 0x04000D66 RID: 3430
			EvtPublisherMetadataKeywordMessageID
		}

		// Token: 0x020002E7 RID: 743
		internal enum EvtChannelReferenceFlags
		{
			// Token: 0x04000D68 RID: 3432
			EvtChannelReferenceImported = 1
		}

		// Token: 0x020002E8 RID: 744
		internal enum EvtEventMetadataPropertyId
		{
			// Token: 0x04000D6A RID: 3434
			EventMetadataEventID,
			// Token: 0x04000D6B RID: 3435
			EventMetadataEventVersion,
			// Token: 0x04000D6C RID: 3436
			EventMetadataEventChannel,
			// Token: 0x04000D6D RID: 3437
			EventMetadataEventLevel,
			// Token: 0x04000D6E RID: 3438
			EventMetadataEventOpcode,
			// Token: 0x04000D6F RID: 3439
			EventMetadataEventTask,
			// Token: 0x04000D70 RID: 3440
			EventMetadataEventKeyword,
			// Token: 0x04000D71 RID: 3441
			EventMetadataEventMessageID,
			// Token: 0x04000D72 RID: 3442
			EventMetadataEventTemplate
		}

		// Token: 0x020002E9 RID: 745
		internal enum EvtChannelConfigPropertyId
		{
			// Token: 0x04000D74 RID: 3444
			EvtChannelConfigEnabled,
			// Token: 0x04000D75 RID: 3445
			EvtChannelConfigIsolation,
			// Token: 0x04000D76 RID: 3446
			EvtChannelConfigType,
			// Token: 0x04000D77 RID: 3447
			EvtChannelConfigOwningPublisher,
			// Token: 0x04000D78 RID: 3448
			EvtChannelConfigClassicEventlog,
			// Token: 0x04000D79 RID: 3449
			EvtChannelConfigAccess,
			// Token: 0x04000D7A RID: 3450
			EvtChannelLoggingConfigRetention,
			// Token: 0x04000D7B RID: 3451
			EvtChannelLoggingConfigAutoBackup,
			// Token: 0x04000D7C RID: 3452
			EvtChannelLoggingConfigMaxSize,
			// Token: 0x04000D7D RID: 3453
			EvtChannelLoggingConfigLogFilePath,
			// Token: 0x04000D7E RID: 3454
			EvtChannelPublishingConfigLevel,
			// Token: 0x04000D7F RID: 3455
			EvtChannelPublishingConfigKeywords,
			// Token: 0x04000D80 RID: 3456
			EvtChannelPublishingConfigControlGuid,
			// Token: 0x04000D81 RID: 3457
			EvtChannelPublishingConfigBufferSize,
			// Token: 0x04000D82 RID: 3458
			EvtChannelPublishingConfigMinBuffers,
			// Token: 0x04000D83 RID: 3459
			EvtChannelPublishingConfigMaxBuffers,
			// Token: 0x04000D84 RID: 3460
			EvtChannelPublishingConfigLatency,
			// Token: 0x04000D85 RID: 3461
			EvtChannelPublishingConfigClockType,
			// Token: 0x04000D86 RID: 3462
			EvtChannelPublishingConfigSidType,
			// Token: 0x04000D87 RID: 3463
			EvtChannelPublisherList,
			// Token: 0x04000D88 RID: 3464
			EvtChannelConfigPropertyIdEND
		}

		// Token: 0x020002EA RID: 746
		internal enum EvtLogPropertyId
		{
			// Token: 0x04000D8A RID: 3466
			EvtLogCreationTime,
			// Token: 0x04000D8B RID: 3467
			EvtLogLastAccessTime,
			// Token: 0x04000D8C RID: 3468
			EvtLogLastWriteTime,
			// Token: 0x04000D8D RID: 3469
			EvtLogFileSize,
			// Token: 0x04000D8E RID: 3470
			EvtLogAttributes,
			// Token: 0x04000D8F RID: 3471
			EvtLogNumberOfLogRecords,
			// Token: 0x04000D90 RID: 3472
			EvtLogOldestRecordNumber,
			// Token: 0x04000D91 RID: 3473
			EvtLogFull
		}

		// Token: 0x020002EB RID: 747
		internal enum EvtExportLogFlags
		{
			// Token: 0x04000D93 RID: 3475
			EvtExportLogChannelPath = 1,
			// Token: 0x04000D94 RID: 3476
			EvtExportLogFilePath,
			// Token: 0x04000D95 RID: 3477
			EvtExportLogTolerateQueryErrors = 4096
		}

		// Token: 0x020002EC RID: 748
		internal enum EvtRenderContextFlags
		{
			// Token: 0x04000D97 RID: 3479
			EvtRenderContextValues,
			// Token: 0x04000D98 RID: 3480
			EvtRenderContextSystem,
			// Token: 0x04000D99 RID: 3481
			EvtRenderContextUser
		}

		// Token: 0x020002ED RID: 749
		internal enum EvtRenderFlags
		{
			// Token: 0x04000D9B RID: 3483
			EvtRenderEventValues,
			// Token: 0x04000D9C RID: 3484
			EvtRenderEventXml,
			// Token: 0x04000D9D RID: 3485
			EvtRenderBookmark
		}

		// Token: 0x020002EE RID: 750
		internal enum EvtFormatMessageFlags
		{
			// Token: 0x04000D9F RID: 3487
			EvtFormatMessageEvent = 1,
			// Token: 0x04000DA0 RID: 3488
			EvtFormatMessageLevel,
			// Token: 0x04000DA1 RID: 3489
			EvtFormatMessageTask,
			// Token: 0x04000DA2 RID: 3490
			EvtFormatMessageOpcode,
			// Token: 0x04000DA3 RID: 3491
			EvtFormatMessageKeyword,
			// Token: 0x04000DA4 RID: 3492
			EvtFormatMessageChannel,
			// Token: 0x04000DA5 RID: 3493
			EvtFormatMessageProvider,
			// Token: 0x04000DA6 RID: 3494
			EvtFormatMessageId,
			// Token: 0x04000DA7 RID: 3495
			EvtFormatMessageXml
		}

		// Token: 0x020002EF RID: 751
		internal enum EvtSystemPropertyId
		{
			// Token: 0x04000DA9 RID: 3497
			EvtSystemProviderName,
			// Token: 0x04000DAA RID: 3498
			EvtSystemProviderGuid,
			// Token: 0x04000DAB RID: 3499
			EvtSystemEventID,
			// Token: 0x04000DAC RID: 3500
			EvtSystemQualifiers,
			// Token: 0x04000DAD RID: 3501
			EvtSystemLevel,
			// Token: 0x04000DAE RID: 3502
			EvtSystemTask,
			// Token: 0x04000DAF RID: 3503
			EvtSystemOpcode,
			// Token: 0x04000DB0 RID: 3504
			EvtSystemKeywords,
			// Token: 0x04000DB1 RID: 3505
			EvtSystemTimeCreated,
			// Token: 0x04000DB2 RID: 3506
			EvtSystemEventRecordId,
			// Token: 0x04000DB3 RID: 3507
			EvtSystemActivityID,
			// Token: 0x04000DB4 RID: 3508
			EvtSystemRelatedActivityID,
			// Token: 0x04000DB5 RID: 3509
			EvtSystemProcessID,
			// Token: 0x04000DB6 RID: 3510
			EvtSystemThreadID,
			// Token: 0x04000DB7 RID: 3511
			EvtSystemChannel,
			// Token: 0x04000DB8 RID: 3512
			EvtSystemComputer,
			// Token: 0x04000DB9 RID: 3513
			EvtSystemUserID,
			// Token: 0x04000DBA RID: 3514
			EvtSystemVersion,
			// Token: 0x04000DBB RID: 3515
			EvtSystemPropertyIdEND
		}

		// Token: 0x020002F0 RID: 752
		internal enum EvtLoginClass
		{
			// Token: 0x04000DBD RID: 3517
			EvtRpcLogin = 1
		}

		// Token: 0x020002F1 RID: 753
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
		internal struct EvtRpcLogin
		{
			// Token: 0x04000DBE RID: 3518
			[MarshalAs(UnmanagedType.LPWStr)]
			public string Server;

			// Token: 0x04000DBF RID: 3519
			[MarshalAs(UnmanagedType.LPWStr)]
			public string User;

			// Token: 0x04000DC0 RID: 3520
			[MarshalAs(UnmanagedType.LPWStr)]
			public string Domain;

			// Token: 0x04000DC1 RID: 3521
			[SecurityCritical]
			public CoTaskMemUnicodeSafeHandle Password;

			// Token: 0x04000DC2 RID: 3522
			public int Flags;
		}

		// Token: 0x020002F2 RID: 754
		[Flags]
		internal enum EvtSeekFlags
		{
			// Token: 0x04000DC4 RID: 3524
			EvtSeekRelativeToFirst = 1,
			// Token: 0x04000DC5 RID: 3525
			EvtSeekRelativeToLast = 2,
			// Token: 0x04000DC6 RID: 3526
			EvtSeekRelativeToCurrent = 3,
			// Token: 0x04000DC7 RID: 3527
			EvtSeekRelativeToBookmark = 4,
			// Token: 0x04000DC8 RID: 3528
			EvtSeekOriginMask = 7,
			// Token: 0x04000DC9 RID: 3529
			EvtSeekStrict = 65536
		}

		// Token: 0x020002F3 RID: 755
		[StructLayout(LayoutKind.Explicit, CharSet = CharSet.Auto)]
		internal struct EvtStringVariant
		{
			// Token: 0x04000DCA RID: 3530
			[FieldOffset(0)]
			[MarshalAs(UnmanagedType.LPWStr)]
			public string StringVal;

			// Token: 0x04000DCB RID: 3531
			[FieldOffset(8)]
			public uint Count;

			// Token: 0x04000DCC RID: 3532
			[FieldOffset(12)]
			public uint Type;
		}

		// Token: 0x020002F4 RID: 756
		[SecurityCritical(SecurityCriticalScope.Everything)]
		internal struct MEMORY_BASIC_INFORMATION
		{
			// Token: 0x04000DCD RID: 3533
			internal unsafe void* BaseAddress;

			// Token: 0x04000DCE RID: 3534
			internal unsafe void* AllocationBase;

			// Token: 0x04000DCF RID: 3535
			internal uint AllocationProtect;

			// Token: 0x04000DD0 RID: 3536
			internal UIntPtr RegionSize;

			// Token: 0x04000DD1 RID: 3537
			internal uint State;

			// Token: 0x04000DD2 RID: 3538
			internal uint Protect;

			// Token: 0x04000DD3 RID: 3539
			internal uint Type;
		}

		// Token: 0x020002F5 RID: 757
		internal struct SYSTEM_INFO
		{
			// Token: 0x04000DD4 RID: 3540
			internal int dwOemId;

			// Token: 0x04000DD5 RID: 3541
			internal int dwPageSize;

			// Token: 0x04000DD6 RID: 3542
			internal IntPtr lpMinimumApplicationAddress;

			// Token: 0x04000DD7 RID: 3543
			internal IntPtr lpMaximumApplicationAddress;

			// Token: 0x04000DD8 RID: 3544
			internal IntPtr dwActiveProcessorMask;

			// Token: 0x04000DD9 RID: 3545
			internal int dwNumberOfProcessors;

			// Token: 0x04000DDA RID: 3546
			internal int dwProcessorType;

			// Token: 0x04000DDB RID: 3547
			internal int dwAllocationGranularity;

			// Token: 0x04000DDC RID: 3548
			internal short wProcessorLevel;

			// Token: 0x04000DDD RID: 3549
			internal short wProcessorRevision;
		}

		// Token: 0x020002F6 RID: 758
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
		internal struct MEMORYSTATUSEX
		{
			// Token: 0x04000DDE RID: 3550
			internal uint dwLength;

			// Token: 0x04000DDF RID: 3551
			internal uint dwMemoryLoad;

			// Token: 0x04000DE0 RID: 3552
			internal ulong ullTotalPhys;

			// Token: 0x04000DE1 RID: 3553
			internal ulong ullAvailPhys;

			// Token: 0x04000DE2 RID: 3554
			internal ulong ullTotalPageFile;

			// Token: 0x04000DE3 RID: 3555
			internal ulong ullAvailPageFile;

			// Token: 0x04000DE4 RID: 3556
			internal ulong ullTotalVirtual;

			// Token: 0x04000DE5 RID: 3557
			internal ulong ullAvailVirtual;

			// Token: 0x04000DE6 RID: 3558
			internal ulong ullAvailExtendedVirtual;
		}
	}
}
