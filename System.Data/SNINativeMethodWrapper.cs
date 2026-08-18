using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using System.Text;

// Token: 0x02000002 RID: 2
[CLSCompliant(false)]
internal class SNINativeMethodWrapper
{
	// Token: 0x0600004B RID: 75 RVA: 0x001D507C File Offset: 0x001D447C
	internal static byte[] GetData()
	{
		byte[] array = null;
		int num;
		IntPtr intPtr = (IntPtr)<Module>.SqlDependencyProcessDispatcherStorage.NativeGetData(ref num);
		if (intPtr != IntPtr.Zero)
		{
			array = new byte[num];
			Marshal.Copy(intPtr, array, 0, num);
		}
		return array;
	}

	// Token: 0x0600004C RID: 76 RVA: 0x001D50D0 File Offset: 0x001D44D0
	internal static void SetData(byte[] data)
	{
		ref byte byte& = ref data[0];
		<Module>.SqlDependencyProcessDispatcherStorage.NativeSetData(ref byte&, data.Length);
	}

	// Token: 0x0600004D RID: 77 RVA: 0x001D5198 File Offset: 0x001D4598
	internal static _AppDomain GetDefaultAppDomain()
	{
		IntPtr pUnk = (IntPtr)<Module>.SqlDependencyProcessDispatcherStorage.NativeGetDefaultAppDomain();
		object objectForIUnknown = Marshal.GetObjectForIUnknown(pUnk);
		Marshal.Release(pUnk);
		return objectForIUnknown as _AppDomain;
	}

	// Token: 0x0600004E RID: 78 RVA: 0x001D5244 File Offset: 0x001D4644
	internal static IntPtr SNIServerEnumOpen()
	{
		IntPtr result = new IntPtr(<Module>.SNIServerEnumOpen(null, 1));
		return result;
	}

	// Token: 0x0600004F RID: 79 RVA: 0x001D5264 File Offset: 0x001D4664
	internal unsafe static int SNIServerEnumRead(IntPtr handle, char[] wStr, int pcbBuf, ref bool fMore)
	{
		ref ushort uint16& = ref wStr[0];
		int num = fMore ? 1 : 0;
		int result = <Module>.SNIServerEnumRead(handle.ToPointer(), ref uint16&, pcbBuf, &num);
		byte b = (num != 0) ? 1 : 0;
		fMore = (b != 0);
		return result;
	}

	// Token: 0x06000050 RID: 80 RVA: 0x001D5298 File Offset: 0x001D4698
	[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
	internal static void SNIServerEnumClose(IntPtr handle)
	{
		method _unep@?SNIServerEnumClose@@$$J0YAXPEAX@Z = <Module>.__unep@?SNIServerEnumClose@@$$J0YAXPEAX@Z;
		calli(System.Void modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.Void*), handle.ToPointer(), _unep@?SNIServerEnumClose@@$$J0YAXPEAX@Z);
	}

	// Token: 0x06000051 RID: 81 RVA: 0x001D52B8 File Offset: 0x001D46B8
	[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
	internal static uint SNIClose(IntPtr pConn)
	{
		method _unep@?SNIClose@@$$J0YAKPEAVSNI_Conn@@@Z = <Module>.__unep@?SNIClose@@$$J0YAKPEAVSNI_Conn@@@Z;
		return calli(System.UInt32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(SNI_Conn*), pConn.ToPointer(), _unep@?SNIClose@@$$J0YAKPEAVSNI_Conn@@@Z);
	}

	// Token: 0x06000052 RID: 82 RVA: 0x001D52D8 File Offset: 0x001D46D8
	internal static uint SNIInitialize()
	{
		return <Module>.SNIInitialize(null);
	}

	// Token: 0x06000053 RID: 83 RVA: 0x001D52EC File Offset: 0x001D46EC
	private unsafe static void MarshalConsumerInfo(SNINativeMethodWrapper.ConsumerInfo consumerInfo, SNI_CONSUMER_INFO* native_consumerInfo)
	{
		*native_consumerInfo = consumerInfo.defaultBufferSize;
		void* ptr;
		if (null == consumerInfo.readDelegate)
		{
			ptr = null;
		}
		else
		{
			ptr = Marshal.GetFunctionPointerForDelegate(consumerInfo.readDelegate).ToPointer();
		}
		*(native_consumerInfo + 16L) = ptr;
		void* ptr2;
		if (null == consumerInfo.writeDelegate)
		{
			ptr2 = null;
		}
		else
		{
			ptr2 = Marshal.GetFunctionPointerForDelegate(consumerInfo.writeDelegate).ToPointer();
		}
		*(native_consumerInfo + 24L) = ptr2;
		*(native_consumerInfo + 8L) = consumerInfo.key.ToPointer();
	}

	// Token: 0x06000054 RID: 84 RVA: 0x001D5370 File Offset: 0x001D4770
	internal unsafe static uint SNIOpenSyncEx(SNINativeMethodWrapper.ConsumerInfo consumerInfo, string constring, ref IntPtr pConn, byte[] spnBuffer, byte[] instanceName, [MarshalAs(UnmanagedType.U1)] bool fOverrideCache, [MarshalAs(UnmanagedType.U1)] bool fSync, int timeout, [MarshalAs(UnmanagedType.U1)] bool fParallel)
	{
		SNI_CLIENT_CONSUMER_INFO sni_CLIENT_CONSUMER_INFO;
		*(ref sni_CLIENT_CONSUMER_INFO + 64) = null;
		*(ref sni_CLIENT_CONSUMER_INFO + 72) = 0;
		*(ref sni_CLIENT_CONSUMER_INFO + 80) = 0L;
		*(ref sni_CLIENT_CONSUMER_INFO + 88) = 0;
		*(ref sni_CLIENT_CONSUMER_INFO + 96) = 0L;
		*(ref sni_CLIENT_CONSUMER_INFO + 104) = 0;
		*(ref sni_CLIENT_CONSUMER_INFO + 108) = 0;
		*(ref sni_CLIENT_CONSUMER_INFO + 112) = 0;
		*(ref sni_CLIENT_CONSUMER_INFO + 116) = -1;
		*(ref sni_CLIENT_CONSUMER_INFO + 120) = 0;
		ref byte ptr = constring;
		if (ref ptr != null)
		{
			ptr = (ulong)RuntimeHelpers.OffsetToStringData + ref ptr;
		}
		ref ushort uint16_u0020modopt(IsConst)& = ref ptr;
		byte condition = (null == pConn.ToPointer()) ? 1 : 0;
		Debug.Assert(condition != 0, "Verrifying variable is really not initallized.");
		SNI_Conn* value = null;
		ref byte byte& = ref (spnBuffer != null) ? ref spnBuffer[0] : 0L;
		ref byte byte&2 = ref instanceName[0];
		SNINativeMethodWrapper.MarshalConsumerInfo(consumerInfo, ref sni_CLIENT_CONSUMER_INFO);
		*(ref sni_CLIENT_CONSUMER_INFO + 64) = ref uint16_u0020modopt(IsConst)&;
		*(ref sni_CLIENT_CONSUMER_INFO + 72) = 0;
		if (spnBuffer != null)
		{
			*(ref sni_CLIENT_CONSUMER_INFO + 80) = ref byte&;
			*(ref sni_CLIENT_CONSUMER_INFO + 88) = spnBuffer.Length;
		}
		*(ref sni_CLIENT_CONSUMER_INFO + 96) = ref byte&2;
		*(ref sni_CLIENT_CONSUMER_INFO + 104) = instanceName.Length;
		*(ref sni_CLIENT_CONSUMER_INFO + 108) = (fOverrideCache ? 1 : 0);
		*(ref sni_CLIENT_CONSUMER_INFO + 112) = (fSync ? 1 : 0);
		*(ref sni_CLIENT_CONSUMER_INFO + 116) = timeout;
		*(ref sni_CLIENT_CONSUMER_INFO + 120) = (fParallel ? 1 : 0);
		uint result = <Module>.SNIOpenSyncEx(&sni_CLIENT_CONSUMER_INFO, &value);
		IntPtr intPtr = (IntPtr)((void*)value);
		pConn = intPtr;
		return result;
	}

	// Token: 0x06000055 RID: 85 RVA: 0x001D547C File Offset: 0x001D487C
	internal unsafe static uint SNIOpen(SNINativeMethodWrapper.ConsumerInfo consumerInfo, string constring, SafeHandle parent, ref IntPtr pConn, [MarshalAs(UnmanagedType.U1)] bool fSync)
	{
		uint result = 0U;
		SNI_CONSUMER_INFO sni_CONSUMER_INFO;
		SNINativeMethodWrapper.MarshalConsumerInfo(consumerInfo, ref sni_CONSUMER_INFO);
		SNI_Conn* value = null;
		ref byte byte& = ref Encoding.ASCII.GetBytes(constring)[0];
		bool flag = false;
		RuntimeHelpers.PrepareConstrainedRegions();
		try
		{
			parent.DangerousAddRef(ref flag);
			Debug.Assert(flag, "AddRef Failed!");
			result = <Module>.SNIOpen(&sni_CONSUMER_INFO, ref byte&, parent.DangerousGetHandle().ToPointer(), &value, fSync ? 1 : 0);
		}
		finally
		{
			if (flag)
			{
				parent.DangerousRelease();
			}
		}
		IntPtr intPtr = (IntPtr)((void*)value);
		pConn = intPtr;
		return result;
	}

	// Token: 0x06000056 RID: 86 RVA: 0x001D5518 File Offset: 0x001D4918
	[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
	internal unsafe static void SNIPacketAllocate(SafeHandle pConn, SNINativeMethodWrapper.IOType ioType, ref IntPtr ret)
	{
		bool flag = false;
		RuntimeHelpers.PrepareConstrainedRegions();
		try
		{
			pConn.DangerousAddRef(ref flag);
			Debug.Assert(flag, "AddRef Failed!");
			SNI_Conn* ptr = (SNI_Conn*)pConn.DangerousGetHandle().ToPointer();
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
			}
			finally
			{
				method _unep@?SNIPacketAllocate@@$$J0YAPEAVSNI_Packet@@PEAVSNI_Conn@@K@Z = <Module>.__unep@?SNIPacketAllocate@@$$J0YAPEAVSNI_Packet@@PEAVSNI_Conn@@K@Z;
				IntPtr intPtr = (IntPtr)calli(SNI_Packet* modopt(System.Runtime.CompilerServices.CallConvCdecl)(SNI_Conn*,System.UInt32 modopt(System.Runtime.CompilerServices.IsLong)), ptr, ioType, _unep@?SNIPacketAllocate@@$$J0YAPEAVSNI_Packet@@PEAVSNI_Conn@@K@Z);
				ret = intPtr;
			}
		}
		finally
		{
			if (flag)
			{
				pConn.DangerousRelease();
			}
		}
	}

	// Token: 0x06000057 RID: 87 RVA: 0x001D55B4 File Offset: 0x001D49B4
	internal static IntPtr SNIPacketGetConnection(IntPtr packet)
	{
		ref SNI_Packet sni_Packet& = packet.ToPointer();
		return (IntPtr)<Module>.SNIPacketGetConnection(ref sni_Packet&);
	}

	// Token: 0x06000058 RID: 88 RVA: 0x001D55D4 File Offset: 0x001D49D4
	internal unsafe static void SNIPacketGetData(IntPtr packet, ref IntPtr data, ref uint dataSize)
	{
		ref SNI_Packet sni_Packet& = packet.ToPointer();
		byte* value = null;
		uint num = 0U;
		<Module>.SNIPacketGetData(ref sni_Packet&, &value, (uint*)(&num));
		IntPtr intPtr = (IntPtr)((void*)value);
		data = intPtr;
		dataSize = num;
	}

	// Token: 0x06000059 RID: 89 RVA: 0x001D560C File Offset: 0x001D4A0C
	internal unsafe static void SNIPacketReset(SafeHandle pConn, SNINativeMethodWrapper.IOType ioType, SafeHandle packet)
	{
		bool flag = false;
		bool flag2 = false;
		RuntimeHelpers.PrepareConstrainedRegions();
		try
		{
			pConn.DangerousAddRef(ref flag);
			Debug.Assert(flag, "AddRef Failed!");
			packet.DangerousAddRef(ref flag2);
			Debug.Assert(flag2, "AddRef Failed!");
			SNI_Conn* ptr = (SNI_Conn*)pConn.DangerousGetHandle().ToPointer();
			SNI_Packet* ptr2 = (SNI_Packet*)packet.DangerousGetHandle().ToPointer();
			<Module>.SNIPacketReset(ptr, ioType, ptr2);
		}
		finally
		{
			if (flag)
			{
				pConn.DangerousRelease();
			}
			if (flag2)
			{
				packet.DangerousRelease();
			}
		}
	}

	// Token: 0x0600005A RID: 90 RVA: 0x001D56A0 File Offset: 0x001D4AA0
	[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
	internal static void SNIPacketRelease(IntPtr packet)
	{
		ref SNI_Packet sni_Packet& = packet.ToPointer();
		method _unep@?SNIPacketRelease@@$$J0YAXPEAVSNI_Packet@@@Z = <Module>.__unep@?SNIPacketRelease@@$$J0YAXPEAVSNI_Packet@@@Z;
		calli(System.Void modopt(System.Runtime.CompilerServices.CallConvCdecl)(SNI_Packet*), ref sni_Packet&, _unep@?SNIPacketRelease@@$$J0YAXPEAVSNI_Packet@@@Z);
	}

	// Token: 0x0600005B RID: 91 RVA: 0x001D56C4 File Offset: 0x001D4AC4
	internal unsafe static void SNIPacketSetData(SafeHandle packet, byte[] data, int length)
	{
		ref byte byte_u0020modopt(IsConst)& = ref data[0];
		RuntimeHelpers.PrepareConstrainedRegions();
		bool flag = false;
		try
		{
			packet.DangerousAddRef(ref flag);
			Debug.Assert(flag, "AddRef Failed!");
			<Module>.SNIPacketSetData((SNI_Packet*)packet.DangerousGetHandle().ToPointer(), ref byte_u0020modopt(IsConst)&, length);
		}
		finally
		{
			if (flag)
			{
				packet.DangerousRelease();
			}
		}
	}

	// Token: 0x0600005C RID: 92 RVA: 0x001D574C File Offset: 0x001D4B4C
	[ResourceConsumption(ResourceScope.Machine, ResourceScope.Machine)]
	[ResourceExposure(ResourceScope.None)]
	internal static int SNIQueryInfo(SNINativeMethodWrapper.QTypes qType, ref IntPtr qInfo)
	{
		byte condition = (qType == SNINativeMethodWrapper.QTypes.SNI_QUERY_LOCALDB_HMODULE) ? 1 : 0;
		Debug.Assert(condition != 0, "qType is unsupported or unknown");
		ref IntPtr intPtr& = ref qInfo;
		return <Module>.SNIQueryInfo((uint)qType, ref intPtr&);
	}

	// Token: 0x0600005D RID: 93 RVA: 0x001D5730 File Offset: 0x001D4B30
	internal unsafe static int SNIQueryInfo(SNINativeMethodWrapper.QTypes qType, ref uint qInfo)
	{
		uint num = qInfo;
		int result = <Module>.SNIQueryInfo((uint)qType, (void*)(&num));
		qInfo = num;
		return result;
	}

	// Token: 0x0600005E RID: 94 RVA: 0x001D5774 File Offset: 0x001D4B74
	internal unsafe static uint SNISetInfo(SafeHandle pConn, SNINativeMethodWrapper.QTypes qtype, ref uint qInfo)
	{
		uint num = qInfo;
		bool flag = false;
		RuntimeHelpers.PrepareConstrainedRegions();
		uint result;
		try
		{
			pConn.DangerousAddRef(ref flag);
			Debug.Assert(flag, "AddRef Failed!");
			result = <Module>.SNISetInfo((SNI_Conn*)pConn.DangerousGetHandle().ToPointer(), (uint)qtype, (void*)(&num));
		}
		finally
		{
			if (flag)
			{
				pConn.DangerousRelease();
			}
		}
		qInfo = num;
		return result;
	}

	// Token: 0x0600005F RID: 95 RVA: 0x001D57E4 File Offset: 0x001D4BE4
	internal unsafe static uint SNIReadAsync(SafeHandle pConn, ref IntPtr packet)
	{
		SNI_Packet* value = null;
		bool flag = false;
		RuntimeHelpers.PrepareConstrainedRegions();
		uint result;
		try
		{
			pConn.DangerousAddRef(ref flag);
			Debug.Assert(flag, "AddRef Failed!");
			result = <Module>.SNIReadAsync((SNI_Conn*)pConn.DangerousGetHandle().ToPointer(), &value, null);
		}
		finally
		{
			if (flag)
			{
				pConn.DangerousRelease();
			}
		}
		IntPtr intPtr = (IntPtr)((void*)value);
		packet = intPtr;
		return result;
	}

	// Token: 0x06000060 RID: 96 RVA: 0x001D5860 File Offset: 0x001D4C60
	internal unsafe static uint SNIReadSync(SafeHandle pConn, ref IntPtr packet, int timeout)
	{
		SNI_Packet* value = null;
		bool flag = false;
		RuntimeHelpers.PrepareConstrainedRegions();
		uint result;
		try
		{
			pConn.DangerousAddRef(ref flag);
			Debug.Assert(flag, "AddRef Failed!");
			result = <Module>.SNIReadSync((SNI_Conn*)pConn.DangerousGetHandle().ToPointer(), &value, timeout);
		}
		finally
		{
			if (flag)
			{
				pConn.DangerousRelease();
			}
		}
		IntPtr intPtr = (IntPtr)((void*)value);
		packet = intPtr;
		return result;
	}

	// Token: 0x06000061 RID: 97 RVA: 0x001D58DC File Offset: 0x001D4CDC
	[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
	internal static uint SNITerminate()
	{
		return calli(System.UInt32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(), <Module>.__unep@?SNITerminate@@$$J0YAKXZ);
	}

	// Token: 0x06000062 RID: 98 RVA: 0x001D58F4 File Offset: 0x001D4CF4
	internal unsafe static uint SNIWriteAsync(SafeHandle pConn, SafeHandle packet)
	{
		bool flag = false;
		bool flag2 = false;
		RuntimeHelpers.PrepareConstrainedRegions();
		uint result;
		try
		{
			pConn.DangerousAddRef(ref flag);
			Debug.Assert(flag, "AddRef Failed!");
			packet.DangerousAddRef(ref flag2);
			Debug.Assert(flag2, "AddRef Failed!");
			SNI_Conn* ptr = (SNI_Conn*)pConn.DangerousGetHandle().ToPointer();
			SNI_Packet* ptr2 = (SNI_Packet*)packet.DangerousGetHandle().ToPointer();
			result = <Module>.SNIWriteAsync(ptr, ptr2, null);
		}
		finally
		{
			if (flag)
			{
				pConn.DangerousRelease();
			}
			if (flag2)
			{
				packet.DangerousRelease();
			}
		}
		return result;
	}

	// Token: 0x06000063 RID: 99 RVA: 0x001D598C File Offset: 0x001D4D8C
	internal unsafe static uint SNIWriteSync(SafeHandle pConn, SafeHandle packet)
	{
		bool flag = false;
		bool flag2 = false;
		RuntimeHelpers.PrepareConstrainedRegions();
		uint result;
		try
		{
			pConn.DangerousAddRef(ref flag);
			Debug.Assert(flag, "AddRef Failed!");
			packet.DangerousAddRef(ref flag2);
			Debug.Assert(flag2, "AddRef Failed!");
			SNI_Conn* ptr = (SNI_Conn*)pConn.DangerousGetHandle().ToPointer();
			SNI_Packet* ptr2 = (SNI_Packet*)packet.DangerousGetHandle().ToPointer();
			result = <Module>.SNIWriteSync(ptr, ptr2, null);
		}
		finally
		{
			if (flag)
			{
				pConn.DangerousRelease();
			}
			if (flag2)
			{
				packet.DangerousRelease();
			}
		}
		return result;
	}

	// Token: 0x06000064 RID: 100 RVA: 0x001D5A24 File Offset: 0x001D4E24
	internal unsafe static uint SNIAddProvider(SafeHandle pConn, SNINativeMethodWrapper.ProviderEnum providerEnum, ref uint info)
	{
		uint num = info;
		bool flag = false;
		RuntimeHelpers.PrepareConstrainedRegions();
		uint result;
		try
		{
			pConn.DangerousAddRef(ref flag);
			Debug.Assert(flag, "AddRef Failed!");
			result = <Module>.SNIAddProvider((SNI_Conn*)pConn.DangerousGetHandle().ToPointer(), (ProviderNum)providerEnum, (void*)(&num));
		}
		finally
		{
			if (flag)
			{
				pConn.DangerousRelease();
			}
		}
		info = num;
		return result;
	}

	// Token: 0x06000065 RID: 101 RVA: 0x001D5A94 File Offset: 0x001D4E94
	internal unsafe static uint SNIRemoveProvider(SafeHandle pConn, SNINativeMethodWrapper.ProviderEnum providerEnum)
	{
		bool flag = false;
		RuntimeHelpers.PrepareConstrainedRegions();
		uint result;
		try
		{
			pConn.DangerousAddRef(ref flag);
			Debug.Assert(flag, "AddRef Failed!");
			result = <Module>.SNIRemoveProvider((SNI_Conn*)pConn.DangerousGetHandle().ToPointer(), (ProviderNum)providerEnum);
		}
		finally
		{
			if (flag)
			{
				pConn.DangerousRelease();
			}
		}
		return result;
	}

	// Token: 0x06000066 RID: 102 RVA: 0x001D5AFC File Offset: 0x001D4EFC
	internal unsafe static void SNIGetLastError(SNINativeMethodWrapper.SNI_Error error)
	{
		SNI_ERROR provider;
		<Module>.SNIGetLastError(&provider);
		error.provider = provider;
		error.errorMessage = new char[522];
		int num = 0;
		long num2 = ref provider + 4;
		do
		{
			error.errorMessage[num] = (char)(*num2);
			num++;
			num2 += 2L;
		}
		while (num < 261);
		error.nativeError = (uint)(*(ref provider + 528));
		error.sniError = (uint)(*(ref provider + 532));
		IntPtr ptr = (IntPtr)(*(ref provider + 536));
		error.fileName = Marshal.PtrToStringUni(ptr);
		IntPtr ptr2 = (IntPtr)(*(ref provider + 544));
		error.function = Marshal.PtrToStringUni(ptr2);
		error.lineNumber = (uint)(*(ref provider + 552));
	}

	// Token: 0x06000067 RID: 103 RVA: 0x001D5BB0 File Offset: 0x001D4FB0
	internal unsafe static uint SNISecInitPackage(ref uint maxLength)
	{
		uint num = maxLength;
		uint result = <Module>.SNISecInitPackage((uint*)(&num));
		maxLength = num;
		return result;
	}

	// Token: 0x06000068 RID: 104 RVA: 0x001D5BCC File Offset: 0x001D4FCC
	internal unsafe static uint SNISecGenClientContext(SafeHandle pConnectionObject, byte[] inBuff, uint receivedLength, byte[] OutBuff, ref uint sendLength, byte[] serverUserName)
	{
		uint num = sendLength;
		ref byte byte& = ref (inBuff != null) ? ref inBuff[0] : 0L;
		ref byte byte&2 = ref OutBuff[0];
		ref byte byte&3 = ref serverUserName[0];
		bool flag = false;
		RuntimeHelpers.PrepareConstrainedRegions();
		uint result;
		try
		{
			pConnectionObject.DangerousAddRef(ref flag);
			Debug.Assert(flag, "AddRef Failed!");
			SNI_Conn* ptr = (SNI_Conn*)pConnectionObject.DangerousGetHandle().ToPointer();
			int num2;
			if (serverUserName == null)
			{
				num2 = 0;
			}
			else
			{
				num2 = serverUserName.Length;
			}
			int num3;
			result = <Module>.SNISecGenClientContext(ptr, ref byte&, receivedLength, ref byte&2, (uint*)(&num), &num3, ref byte&3, num2, null, null);
		}
		finally
		{
			if (flag)
			{
				pConnectionObject.DangerousRelease();
			}
		}
		sendLength = num;
		return result;
	}

	// Token: 0x06000069 RID: 105 RVA: 0x001D5C78 File Offset: 0x001D5078
	[ResourceConsumption(ResourceScope.Machine, ResourceScope.Machine)]
	[ResourceExposure(ResourceScope.None)]
	internal unsafe static uint SNIWaitForSSLHandshakeToComplete(SafeHandle pConn, int timeoutMilliseconds)
	{
		bool flag = false;
		RuntimeHelpers.PrepareConstrainedRegions();
		uint result;
		try
		{
			pConn.DangerousAddRef(ref flag);
			Debug.Assert(flag, "AddRef Failed!");
			result = <Module>.SNIWaitForSSLHandshakeToComplete((SNI_Conn*)pConn.DangerousGetHandle().ToPointer(), timeoutMilliseconds);
		}
		finally
		{
			if (flag)
			{
				pConn.DangerousRelease();
			}
		}
		return result;
	}

	// Token: 0x04000042 RID: 66
	internal static int SNI_LocalDBErrorCode = 50;

	// Token: 0x04000043 RID: 67
	internal static int SniMaxComposedSpnLength = (int)<Module>.SNI_MAX_COMPOSED_SPN;

	// Token: 0x02000003 RID: 3
	internal enum QTypes
	{
		// Token: 0x04000045 RID: 69
		SNI_QUERY_LOCALDB_HMODULE = 24,
		// Token: 0x04000046 RID: 70
		SNI_QUERY_CONN_SECPKG = 10,
		// Token: 0x04000047 RID: 71
		SNI_QUERY_CONN_PARENTCONNID = 9,
		// Token: 0x04000048 RID: 72
		SNI_QUERY_CONN_CONNID = 8,
		// Token: 0x04000049 RID: 73
		SNI_QUERY_CONN_PROVIDERNUM = 7,
		// Token: 0x0400004A RID: 74
		SNI_QUERY_CONN_ENCRYPT = 6,
		// Token: 0x0400004B RID: 75
		SNI_QUERY_CERTIFICATE = 5,
		// Token: 0x0400004C RID: 76
		SNI_QUERY_SERVER_ENCRYPT_POSSIBLE = 4,
		// Token: 0x0400004D RID: 77
		SNI_QUERY_CLIENT_ENCRYPT_POSSIBLE = 3,
		// Token: 0x0400004E RID: 78
		SNI_QUERY_CONN_KEY = 2,
		// Token: 0x0400004F RID: 79
		SNI_QUERY_CONN_BUFSIZE = 1,
		// Token: 0x04000050 RID: 80
		SNI_QUERY_CONN_INFO = 0
	}

	// Token: 0x02000004 RID: 4
	internal enum ProviderEnum
	{
		// Token: 0x04000052 RID: 82
		INVALID_PROV = 10,
		// Token: 0x04000053 RID: 83
		MAX_PROVS = 9,
		// Token: 0x04000054 RID: 84
		VIA_PROV = 8,
		// Token: 0x04000055 RID: 85
		TCP_PROV = 7,
		// Token: 0x04000056 RID: 86
		SSL_PROV = 6,
		// Token: 0x04000057 RID: 87
		SMUX_PROV = 5,
		// Token: 0x04000058 RID: 88
		SM_PROV = 4,
		// Token: 0x04000059 RID: 89
		SIGN_PROV = 3,
		// Token: 0x0400005A RID: 90
		SESSION_PROV = 2,
		// Token: 0x0400005B RID: 91
		NP_PROV = 1,
		// Token: 0x0400005C RID: 92
		HTTP_PROV = 0
	}

	// Token: 0x02000005 RID: 5
	internal enum IOType
	{
		// Token: 0x0400005E RID: 94
		WRITE = 1,
		// Token: 0x0400005F RID: 95
		READ = 0
	}

	// Token: 0x02000006 RID: 6
	// (Invoke) Token: 0x0600006D RID: 109
	internal delegate void SqlAsyncCallbackDelegate(IntPtr ptr1, IntPtr ptr2, uint num);

	// Token: 0x02000007 RID: 7
	[CLSCompliant(false)]
	internal class ConsumerInfo
	{
		// Token: 0x04000060 RID: 96
		internal int defaultBufferSize;

		// Token: 0x04000061 RID: 97
		internal SNINativeMethodWrapper.SqlAsyncCallbackDelegate readDelegate;

		// Token: 0x04000062 RID: 98
		internal SNINativeMethodWrapper.SqlAsyncCallbackDelegate writeDelegate;

		// Token: 0x04000063 RID: 99
		internal IntPtr key;
	}

	// Token: 0x02000008 RID: 8
	[CLSCompliant(false)]
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
	internal class SNI_Error
	{
		// Token: 0x04000064 RID: 100
		internal SNINativeMethodWrapper.ProviderEnum provider;

		// Token: 0x04000065 RID: 101
		internal char[] errorMessage;

		// Token: 0x04000066 RID: 102
		internal uint nativeError;

		// Token: 0x04000067 RID: 103
		internal uint sniError;

		// Token: 0x04000068 RID: 104
		internal string fileName;

		// Token: 0x04000069 RID: 105
		internal string function;

		// Token: 0x0400006A RID: 106
		internal uint lineNumber;
	}
}
