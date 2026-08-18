using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using System.Security;
using <CppImplementationDetails>;

// Token: 0x02000002 RID: 2
[CLSCompliant(false)]
internal class SNINativeMethodWrapper
{
	// Token: 0x06000066 RID: 102 RVA: 0x000032A0 File Offset: 0x000026A0
	[ResourceExposure(ResourceScope.Process)]
	[ResourceConsumption(ResourceScope.Process, ResourceScope.Process)]
	internal static byte[] GetData()
	{
		int num;
		IntPtr intPtr = (IntPtr)<Module>.SqlDependencyProcessDispatcherStorage.NativeGetData(ref num);
		byte[] array = null;
		if (intPtr != IntPtr.Zero)
		{
			array = new byte[num];
			Marshal.Copy(intPtr, array, 0, num);
		}
		return array;
	}

	// Token: 0x06000067 RID: 103 RVA: 0x000032DC File Offset: 0x000026DC
	[ResourceConsumption(ResourceScope.Process, ResourceScope.Process)]
	[ResourceExposure(ResourceScope.Process)]
	internal static void SetData(byte[] data)
	{
		ref byte byte& = ref data[0];
		<Module>.SqlDependencyProcessDispatcherStorage.NativeSetData(ref byte&, data.Length);
	}

	// Token: 0x06000068 RID: 104 RVA: 0x000032FC File Offset: 0x000026FC
	[ResourceExposure(ResourceScope.Process)]
	[ResourceConsumption(ResourceScope.Process, ResourceScope.Process)]
	internal static _AppDomain GetDefaultAppDomain()
	{
		IntPtr pUnk = (IntPtr)<Module>.?A0x0e118935.GetDefaultAppDomain();
		object objectForIUnknown = Marshal.GetObjectForIUnknown(pUnk);
		Marshal.Release(pUnk);
		return objectForIUnknown as _AppDomain;
	}

	// Token: 0x06000069 RID: 105 RVA: 0x00003328 File Offset: 0x00002728
	[ResourceConsumption(ResourceScope.Machine, ResourceScope.Machine)]
	[ResourceExposure(ResourceScope.None)]
	internal static IntPtr SNIServerEnumOpen()
	{
		IntPtr result = new IntPtr(<Module>.SNIServerEnumOpen(null, 1));
		return result;
	}

	// Token: 0x0600006A RID: 106 RVA: 0x00003348 File Offset: 0x00002748
	[ResourceExposure(ResourceScope.None)]
	[ResourceConsumption(ResourceScope.Machine, ResourceScope.Machine)]
	internal unsafe static int SNIServerEnumRead(IntPtr handle, char[] wStr, int pcbBuf, ref bool fMore)
	{
		ref ushort uint16& = ref wStr[0];
		int num = fMore ? 1 : 0;
		int result = <Module>.SNIServerEnumRead(handle.ToPointer(), ref uint16&, pcbBuf, &num);
		byte b = (num != 0) ? 1 : 0;
		fMore = (b != 0);
		return result;
	}

	// Token: 0x0600006B RID: 107 RVA: 0x0000337C File Offset: 0x0000277C
	[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
	[ResourceExposure(ResourceScope.None)]
	internal static void SNIServerEnumClose(IntPtr handle)
	{
		method _unep@?SNIServerEnumClose@@$$J0YAXPEAX@Z = <Module>.__unep@?SNIServerEnumClose@@$$J0YAXPEAX@Z;
		calli(System.Void modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.Void*), handle.ToPointer(), _unep@?SNIServerEnumClose@@$$J0YAXPEAX@Z);
	}

	// Token: 0x0600006C RID: 108 RVA: 0x0000339C File Offset: 0x0000279C
	[ResourceExposure(ResourceScope.None)]
	[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
	internal unsafe static uint SNIClose(IntPtr pConn)
	{
		method _unep@?SNIClose@@$$J0YAKPEAVSNI_Conn@@@Z = <Module>.__unep@?SNIClose@@$$J0YAKPEAVSNI_Conn@@@Z;
		SNI_ConnWrapper* ptr = (SNI_ConnWrapper*)pConn.ToPointer();
		uint result = calli(System.UInt32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(SNI_Conn*), *(long*)ptr, _unep@?SNIClose@@$$J0YAKPEAVSNI_Conn@@@Z);
		<Module>.SNI_ConnWrapper.__delDtor(ptr, 1U);
		return result;
	}

	// Token: 0x0600006D RID: 109 RVA: 0x000033C8 File Offset: 0x000027C8
	[ResourceExposure(ResourceScope.None)]
	[ResourceConsumption(ResourceScope.Machine, ResourceScope.Machine)]
	internal static uint SNIInitialize()
	{
		return <Module>.SNIInitialize(null);
	}

	// Token: 0x0600006E RID: 110 RVA: 0x000033DC File Offset: 0x000027DC
	[ResourceExposure(ResourceScope.None)]
	private unsafe static void MarshalConsumerInfo(SNINativeMethodWrapper.ConsumerInfo consumerInfo, Sni_Consumer_Info* native_consumerInfo)
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

	// Token: 0x0600006F RID: 111 RVA: 0x00003460 File Offset: 0x00002860
	[ResourceConsumption(ResourceScope.Machine, ResourceScope.Machine)]
	[ResourceExposure(ResourceScope.None)]
	internal unsafe static uint SNIOpenSyncEx(SNINativeMethodWrapper.ConsumerInfo consumerInfo, string constring, ref IntPtr pConn, byte[] spnBuffer, byte[] instanceName, [MarshalAs(UnmanagedType.U1)] bool fOverrideCache, [MarshalAs(UnmanagedType.U1)] bool fSync, int timeout, [MarshalAs(UnmanagedType.U1)] bool fParallel, int transparentNetworkResolutionStateNo, int totalTimeout, [MarshalAs(UnmanagedType.U1)] bool isAzureSqlServerEndpoint)
	{
		SNI_CLIENT_CONSUMER_INFO sni_CLIENT_CONSUMER_INFO;
		<Module>.SNI_CLIENT_CONSUMER_INFO.{ctor}(ref sni_CLIENT_CONSUMER_INFO);
		ref byte ptr = constring;
		if (ref ptr != null)
		{
			ptr = (long)RuntimeHelpers.OffsetToStringData + ref ptr;
		}
		ref ushort uint16_u0020modopt(IsConst)& = ref ptr;
		byte condition = (null == pConn.ToPointer()) ? 1 : 0;
		Debug.Assert(condition != 0, "Verrifying variable is really not initallized.");
		SNI_ConnWrapper* value = null;
		ref byte byte& = ref (spnBuffer != null) ? ref spnBuffer[0] : 0L;
		ref byte byte&2 = ref instanceName[0];
		SNINativeMethodWrapper.MarshalConsumerInfo(consumerInfo, ref sni_CLIENT_CONSUMER_INFO);
		*(ref sni_CLIENT_CONSUMER_INFO + 72) = ref uint16_u0020modopt(IsConst)&;
		*(ref sni_CLIENT_CONSUMER_INFO + 80) = 0;
		if (spnBuffer != null)
		{
			*(ref sni_CLIENT_CONSUMER_INFO + 88) = ref byte&;
			*(ref sni_CLIENT_CONSUMER_INFO + 96) = spnBuffer.Length;
		}
		*(ref sni_CLIENT_CONSUMER_INFO + 104) = ref byte&2;
		*(ref sni_CLIENT_CONSUMER_INFO + 112) = instanceName.Length;
		*(ref sni_CLIENT_CONSUMER_INFO + 116) = (fOverrideCache ? 1 : 0);
		*(ref sni_CLIENT_CONSUMER_INFO + 120) = (fSync ? 1 : 0);
		*(ref sni_CLIENT_CONSUMER_INFO + 124) = timeout;
		*(ref sni_CLIENT_CONSUMER_INFO + 128) = (fParallel ? 1 : 0);
		*(ref sni_CLIENT_CONSUMER_INFO + 140) = (isAzureSqlServerEndpoint ? 1 : 0);
		if (transparentNetworkResolutionStateNo != 0)
		{
			if (transparentNetworkResolutionStateNo != 1)
			{
				if (transparentNetworkResolutionStateNo == 2)
				{
					*(ref sni_CLIENT_CONSUMER_INFO + 132) = 2;
				}
			}
			else
			{
				*(ref sni_CLIENT_CONSUMER_INFO + 132) = 1;
			}
		}
		else
		{
			*(ref sni_CLIENT_CONSUMER_INFO + 132) = 0;
		}
		*(ref sni_CLIENT_CONSUMER_INFO + 136) = totalTimeout;
		uint result = <Module>.SNIOpenSyncExWrapper(&sni_CLIENT_CONSUMER_INFO, &value);
		IntPtr intPtr = (IntPtr)((void*)value);
		pConn = intPtr;
		return result;
	}

	// Token: 0x06000070 RID: 112 RVA: 0x00003574 File Offset: 0x00002974
	[HandleProcessCorruptedStateExceptions]
	[ResourceExposure(ResourceScope.None)]
	[ResourceConsumption(ResourceScope.Machine, ResourceScope.Machine)]
	internal unsafe static uint SNIOpenMarsSession(SNINativeMethodWrapper.ConsumerInfo consumerInfo, SafeHandle parent, ref IntPtr pConn, [MarshalAs(UnmanagedType.U1)] bool fSync)
	{
		uint result = 0U;
		Sni_Consumer_Info sni_Consumer_Info;
		<Module>.Sni_Consumer_Info.{ctor}(ref sni_Consumer_Info);
		$ArrayType$$$BY08G $ArrayType$$$BY08G;
		cpblk(ref $ArrayType$$$BY08G, ref <Module>.??_C@_1BC@LEJJAHNB@?$AAs?$AAe?$AAs?$AAs?$AAi?$AAo?$AAn?$AA?3@, 18);
		SNINativeMethodWrapper.MarshalConsumerInfo(consumerInfo, ref sni_Consumer_Info);
		SNI_ConnWrapper* value = null;
		bool flag = false;
		RuntimeHelpers.PrepareConstrainedRegions();
		try
		{
			parent.DangerousAddRef(ref flag);
			Debug.Assert(flag, "AddRef Failed!");
			void* ptr = parent.DangerousGetHandle().ToPointer();
			result = <Module>.SNIOpenWrapper(&sni_Consumer_Info, (ushort*)(&$ArrayType$$$BY08G), *(long*)ptr, &value, fSync ? 1 : 0);
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

	// Token: 0x06000071 RID: 113 RVA: 0x00003614 File Offset: 0x00002A14
	[HandleProcessCorruptedStateExceptions]
	[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
	[ResourceExposure(ResourceScope.None)]
	internal unsafe static void SNIPacketAllocate(SafeHandle pConn, SNINativeMethodWrapper.IOType ioType, ref IntPtr ret)
	{
		bool flag = false;
		RuntimeHelpers.PrepareConstrainedRegions();
		try
		{
			pConn.DangerousAddRef(ref flag);
			Debug.Assert(flag, "AddRef Failed!");
			SNI_ConnWrapper* ptr = (SNI_ConnWrapper*)pConn.DangerousGetHandle().ToPointer();
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
			}
			finally
			{
				IntPtr intPtr = (IntPtr)calli(SNI_Packet* modopt(System.Runtime.CompilerServices.CallConvCdecl)(SNI_Conn*,SNI_Packet_IOType), *(long*)ptr, ioType, SNINativeMethodWrapper.SNIPacketAllocatePtr);
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

	// Token: 0x06000072 RID: 114 RVA: 0x000036B0 File Offset: 0x00002AB0
	[ResourceExposure(ResourceScope.None)]
	internal static uint SNIPacketGetData(IntPtr packet, byte[] readBuffer, ref uint dataSize)
	{
		ref SNI_Packet sni_Packet& = packet.ToPointer();
		ref byte byte& = ref readBuffer[0];
		uint num = 0;
		uint result = calli(System.UInt32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(SNI_Packet*,System.Byte*,System.UInt32 modopt(System.Runtime.CompilerServices.IsLong),System.UInt32 modopt(System.Runtime.CompilerServices.IsLong)*), ref sni_Packet&, ref byte&, readBuffer.Length, ref num, SNINativeMethodWrapper.SNIPacketGetDataWrapperPtr);
		dataSize = num;
		return result;
	}

	// Token: 0x06000073 RID: 115 RVA: 0x000036E4 File Offset: 0x00002AE4
	[ResourceExposure(ResourceScope.None)]
	[HandleProcessCorruptedStateExceptions]
	internal unsafe static void SNIPacketReset(SafeHandle pConn, SNINativeMethodWrapper.IOType ioType, SafeHandle packet, SNINativeMethodWrapper.ConsumerNumber consNum)
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
			ref long ptr = ref *(long*)pConn.DangerousGetHandle().ToPointer();
			SNI_Packet* ptr2 = (SNI_Packet*)packet.DangerousGetHandle().ToPointer();
			calli(System.Void modopt(System.Runtime.CompilerServices.CallConvCdecl)(SNI_Conn*,SNI_Packet_IOType,SNI_Packet*,ConsumerNum), ptr, ioType, ptr2, consNum, SNINativeMethodWrapper.SNIPacketResetPtr);
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

	// Token: 0x06000074 RID: 116 RVA: 0x00003780 File Offset: 0x00002B80
	[ResourceExposure(ResourceScope.None)]
	[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
	internal static void SNIPacketRelease(IntPtr packet)
	{
		ref SNI_Packet sni_Packet& = packet.ToPointer();
		calli(System.Void modopt(System.Runtime.CompilerServices.CallConvCdecl)(SNI_Packet*), ref sni_Packet&, SNINativeMethodWrapper.SNIPacketReleasePtr);
	}

	// Token: 0x06000075 RID: 117 RVA: 0x000037A0 File Offset: 0x00002BA0
	[ResourceExposure(ResourceScope.None)]
	[HandleProcessCorruptedStateExceptions]
	internal unsafe static void SNIPacketSetData(SafeHandle packet, byte[] data, int length, SecureString[] passwords, int[] passwordOffsets)
	{
		byte condition;
		if (passwords != null && (passwordOffsets == null || passwords.Length != passwordOffsets.Length))
		{
			condition = 0;
		}
		else
		{
			condition = 1;
		}
		Debug.Assert(condition != 0, "The number of passwords does not match the number of password offsets");
		ref byte byte_u0020modopt(IsConst)& = ref data[0];
		bool flag = false;
		bool flag2 = false;
		IntPtr intPtr = IntPtr.Zero;
		RuntimeHelpers.PrepareConstrainedRegions();
		try
		{
			if (passwords != null)
			{
				for (int i = 0; i < passwords.Length; i++)
				{
					if (passwords[i] != null)
					{
						RuntimeHelpers.PrepareConstrainedRegions();
						try
						{
							intPtr = Marshal.SecureStringToCoTaskMemUnicode(passwords[i]);
							ushort* ptr = (ushort*)intPtr.ToPointer();
							byte* ptr2 = (byte*)intPtr.ToPointer();
							int length2 = passwords[i].Length;
							for (int j = 0; j < length2; j++)
							{
								int num = (int)(*ptr);
								byte b = (byte)num;
								byte b2 = (byte)(num >> 8);
								*ptr2 = (byte)(((uint)((byte)((int)b << 4 & 240)) | (uint)b >> 4) ^ 165U);
								ptr2 += 1L;
								*ptr2 = (byte)(((uint)((byte)((int)b2 << 4 & 240)) | (uint)b2 >> 4) ^ 165U);
								ptr2 += 1L;
								ptr += 2L / 2L;
							}
							flag2 = true;
							Marshal.Copy(intPtr, data, passwordOffsets[i], length2 * 2);
						}
						finally
						{
							if (intPtr != IntPtr.Zero)
							{
								Marshal.ZeroFreeCoTaskMemUnicode(intPtr);
							}
						}
					}
				}
			}
			packet.DangerousAddRef(ref flag);
			Debug.Assert(flag, "AddRef Failed!");
			calli(System.Void modopt(System.Runtime.CompilerServices.CallConvCdecl)(SNI_Packet*,System.Byte modopt(System.Runtime.CompilerServices.IsConst)*,System.UInt32 modopt(System.Runtime.CompilerServices.IsLong)), packet.DangerousGetHandle().ToPointer(), ref byte_u0020modopt(IsConst)&, length, SNINativeMethodWrapper.SNIPacketSetDataPtr);
		}
		finally
		{
			if (flag)
			{
				packet.DangerousRelease();
			}
			if (flag2)
			{
				for (int k = 0; k < data.Length; k++)
				{
					data[k] = 0;
				}
			}
		}
	}

	// Token: 0x06000076 RID: 118 RVA: 0x00003980 File Offset: 0x00002D80
	[ResourceExposure(ResourceScope.None)]
	[ResourceConsumption(ResourceScope.Machine, ResourceScope.Machine)]
	internal static int SNIQueryInfo(SNINativeMethodWrapper.QTypes qType, ref IntPtr qInfo)
	{
		byte condition = (qType == SNINativeMethodWrapper.QTypes.SNI_QUERY_LOCALDB_HMODULE) ? 1 : 0;
		Debug.Assert(condition != 0, "qType is unsupported or unknown");
		ref IntPtr intPtr& = ref qInfo;
		return <Module>.SNIQueryInfo((uint)qType, ref intPtr&);
	}

	// Token: 0x06000077 RID: 119 RVA: 0x00003944 File Offset: 0x00002D44
	[ResourceExposure(ResourceScope.None)]
	[ResourceConsumption(ResourceScope.Machine, ResourceScope.Machine)]
	internal unsafe static int SNIQueryInfo(SNINativeMethodWrapper.QTypes qType, ref uint qInfo)
	{
		uint num = qInfo;
		byte condition;
		if (qType != SNINativeMethodWrapper.QTypes.SNI_QUERY_CLIENT_ENCRYPT_POSSIBLE && qType != SNINativeMethodWrapper.QTypes.SNI_QUERY_SERVER_ENCRYPT_POSSIBLE && qType != SNINativeMethodWrapper.QTypes.SNI_QUERY_TCP_SKIP_IO_COMPLETION_ON_SUCCESS)
		{
			condition = 0;
		}
		else
		{
			condition = 1;
		}
		Debug.Assert(condition != 0, "qType is unsupported or unknown");
		int result = <Module>.SNIQueryInfo((uint)qType, (void*)(&num));
		qInfo = num;
		return result;
	}

	// Token: 0x06000078 RID: 120 RVA: 0x000039A8 File Offset: 0x00002DA8
	[HandleProcessCorruptedStateExceptions]
	[ResourceExposure(ResourceScope.None)]
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
			result = <Module>.SNISetInfo(*(long*)pConn.DangerousGetHandle().ToPointer(), (uint)qtype, (void*)(&num));
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

	// Token: 0x06000079 RID: 121 RVA: 0x00003A18 File Offset: 0x00002E18
	[HandleProcessCorruptedStateExceptions]
	[ResourceExposure(ResourceScope.None)]
	internal unsafe static uint SniGetConnectionId(SafeHandle pConn, ref Guid connId)
	{
		bool flag = false;
		RuntimeHelpers.PrepareConstrainedRegions();
		uint num;
		try
		{
			pConn.DangerousAddRef(ref flag);
			Debug.Assert(flag, "AddRef Failed!");
			_GUID guid;
			num = <Module>.SNIGetInfo(*(long*)pConn.DangerousGetHandle().ToPointer(), 9U, (void*)(&guid));
			if (0U == num)
			{
				Guid guid2 = <Module>.?A0x0e118935.FromGUID(ref guid);
				connId = guid2;
			}
		}
		finally
		{
			if (flag)
			{
				pConn.DangerousRelease();
			}
		}
		return num;
	}

	// Token: 0x0600007A RID: 122 RVA: 0x00003A98 File Offset: 0x00002E98
	[HandleProcessCorruptedStateExceptions]
	[ResourceExposure(ResourceScope.None)]
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
			result = calli(System.UInt32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(SNI_ConnWrapper*,SNI_Packet**), pConn.DangerousGetHandle().ToPointer(), ref value, SNINativeMethodWrapper.SNIReadAsyncWrapperPtr);
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

	// Token: 0x0600007B RID: 123 RVA: 0x00003B18 File Offset: 0x00002F18
	[HandleProcessCorruptedStateExceptions]
	[ResourceExposure(ResourceScope.None)]
	internal unsafe static uint SNIReadSyncOverAsync(SafeHandle pConn, ref IntPtr packet, int timeout)
	{
		SNI_Packet* value = null;
		bool flag = false;
		RuntimeHelpers.PrepareConstrainedRegions();
		uint result;
		try
		{
			pConn.DangerousAddRef(ref flag);
			Debug.Assert(flag, "AddRef Failed!");
			result = <Module>.SNIReadSyncOverAsync((SNI_ConnWrapper*)pConn.DangerousGetHandle().ToPointer(), &value, timeout);
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

	// Token: 0x0600007C RID: 124 RVA: 0x00003B94 File Offset: 0x00002F94
	[HandleProcessCorruptedStateExceptions]
	[ResourceExposure(ResourceScope.None)]
	internal unsafe static uint SNICheckConnection(SafeHandle pConn)
	{
		bool flag = false;
		RuntimeHelpers.PrepareConstrainedRegions();
		uint result;
		try
		{
			pConn.DangerousAddRef(ref flag);
			Debug.Assert(flag, "AddRef Failed!");
			result = calli(System.UInt32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(SNI_Conn*), *(long*)pConn.DangerousGetHandle().ToPointer(), SNINativeMethodWrapper.SNICheckConnectionPtr);
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

	// Token: 0x0600007D RID: 125 RVA: 0x00003C00 File Offset: 0x00003000
	[ResourceExposure(ResourceScope.None)]
	[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
	internal static uint SNITerminate()
	{
		return calli(System.UInt32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(), <Module>.__unep@?SNITerminate@@$$J0YAKXZ);
	}

	// Token: 0x0600007E RID: 126 RVA: 0x00003C18 File Offset: 0x00003018
	[ResourceExposure(ResourceScope.None)]
	[HandleProcessCorruptedStateExceptions]
	internal unsafe static uint SNIWritePacket(SafeHandle pConn, SafeHandle packet, [MarshalAs(UnmanagedType.U1)] bool sync)
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
			SNI_ConnWrapper* ptr = (SNI_ConnWrapper*)pConn.DangerousGetHandle().ToPointer();
			SNI_Packet* ptr2 = (SNI_Packet*)packet.DangerousGetHandle().ToPointer();
			if (sync)
			{
				result = <Module>.SNIWriteSyncOverAsync(ptr, ptr2);
			}
			else
			{
				result = calli(System.UInt32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(SNI_ConnWrapper*,SNI_Packet*), ptr, ptr2, SNINativeMethodWrapper.SNIWriteAsyncWrapperPtr);
			}
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

	// Token: 0x0600007F RID: 127 RVA: 0x00003CC8 File Offset: 0x000030C8
	[HandleProcessCorruptedStateExceptions]
	[ResourceConsumption(ResourceScope.Machine, ResourceScope.Machine)]
	[ResourceExposure(ResourceScope.None)]
	internal unsafe static uint SNIAddProvider(SafeHandle pConn, SNINativeMethodWrapper.ProviderEnum providerEnum, ref uint info)
	{
		uint num = info;
		bool flag = false;
		RuntimeHelpers.PrepareConstrainedRegions();
		uint num2;
		try
		{
			pConn.DangerousAddRef(ref flag);
			Debug.Assert(flag, "AddRef Failed!");
			SNI_ConnWrapper* ptr = (SNI_ConnWrapper*)pConn.DangerousGetHandle().ToPointer();
			num2 = <Module>.SNIAddProvider(*(long*)ptr, (ProviderNum)providerEnum, (void*)(&num));
			if (num2 == 0U)
			{
				int num3;
				num2 = <Module>.SNIGetInfo(*(long*)ptr, 34U, (void*)(&num3));
				byte condition = (num2 == 0U) ? 1 : 0;
				Debug.Assert(condition != 0, "SNIGetInfo cannot fail with this QType");
				int num4 = (num3 != 0) ? 1 : 0;
				*(byte*)(ptr + 1226L / (long)sizeof(SNI_ConnWrapper)) = (byte)num4;
			}
		}
		finally
		{
			if (flag)
			{
				pConn.DangerousRelease();
			}
		}
		info = num;
		return num2;
	}

	// Token: 0x06000080 RID: 128 RVA: 0x00003D70 File Offset: 0x00003170
	[HandleProcessCorruptedStateExceptions]
	[ResourceExposure(ResourceScope.None)]
	internal unsafe static uint SNIRemoveProvider(SafeHandle pConn, SNINativeMethodWrapper.ProviderEnum providerEnum)
	{
		bool flag = false;
		RuntimeHelpers.PrepareConstrainedRegions();
		uint result;
		try
		{
			pConn.DangerousAddRef(ref flag);
			Debug.Assert(flag, "AddRef Failed!");
			result = <Module>.SNIRemoveProvider(*(long*)pConn.DangerousGetHandle().ToPointer(), (ProviderNum)providerEnum);
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

	// Token: 0x06000081 RID: 129 RVA: 0x00003DD8 File Offset: 0x000031D8
	[ResourceExposure(ResourceScope.None)]
	internal unsafe static void SNIGetLastError(SNINativeMethodWrapper.SNI_Error error)
	{
		SNI_ERROR provider;
		<Module>.SNIGetLastError(&provider);
		error.provider = provider;
		error.errorMessage = new char[261];
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

	// Token: 0x06000082 RID: 130 RVA: 0x00003E90 File Offset: 0x00003290
	[ResourceExposure(ResourceScope.None)]
	[ResourceConsumption(ResourceScope.Machine, ResourceScope.Machine)]
	internal unsafe static uint SNISecInitPackage(ref uint maxLength)
	{
		uint num = maxLength;
		uint result = <Module>.SNISecInitPackage(&num);
		maxLength = num;
		return result;
	}

	// Token: 0x06000083 RID: 131 RVA: 0x00003EAC File Offset: 0x000032AC
	[ResourceExposure(ResourceScope.None)]
	[ResourceConsumption(ResourceScope.Machine, ResourceScope.Machine)]
	[HandleProcessCorruptedStateExceptions]
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
			ref long ptr = ref *(long*)pConnectionObject.DangerousGetHandle().ToPointer();
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
			result = <Module>.SNISecGenClientContext(ptr, ref byte&, receivedLength, ref byte&2, &num, &num3, ref byte&3, num2, null, null);
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

	// Token: 0x06000084 RID: 132 RVA: 0x00003F5C File Offset: 0x0000335C
	[ResourceExposure(ResourceScope.None)]
	[HandleProcessCorruptedStateExceptions]
	[ResourceConsumption(ResourceScope.Machine, ResourceScope.Machine)]
	internal unsafe static uint SNIWaitForSSLHandshakeToComplete(SafeHandle pConn, int timeoutMilliseconds)
	{
		bool flag = false;
		RuntimeHelpers.PrepareConstrainedRegions();
		uint result;
		try
		{
			pConn.DangerousAddRef(ref flag);
			Debug.Assert(flag, "AddRef Failed!");
			result = <Module>.SNIWaitForSSLHandshakeToComplete(*(long*)pConn.DangerousGetHandle().ToPointer(), timeoutMilliseconds);
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

	// Token: 0x04000046 RID: 70
	internal static int SniMaxComposedSpnLength = (int)<Module>.SNI_MAX_COMPOSED_SPN;

	// Token: 0x04000047 RID: 71
	private static method SNICheckConnectionPtr = <Module>.__unep@?SNICheckConnection@@$$J0YAKPEAVSNI_Conn@@@Z;

	// Token: 0x04000048 RID: 72
	private static method SNIWriteAsyncWrapperPtr = <Module>.__unep@?SNIWriteAsyncWrapper@@$$FYAKPEAUSNI_ConnWrapper@@PEAVSNI_Packet@@@Z;

	// Token: 0x04000049 RID: 73
	private static method SNIReadAsyncWrapperPtr = <Module>.__unep@?SNIReadAsyncWrapper@@$$FYAKPEAUSNI_ConnWrapper@@PEAPEAVSNI_Packet@@@Z;

	// Token: 0x0400004A RID: 74
	private static method SNIPacketAllocatePtr = <Module>.__unep@?SNIPacketAllocate@@$$J0YAPEAVSNI_Packet@@PEAVSNI_Conn@@W4SNI_Packet_IOType@@@Z;

	// Token: 0x0400004B RID: 75
	private static method SNIPacketReleasePtr = <Module>.__unep@?SNIPacketRelease@@$$J0YAXPEAVSNI_Packet@@@Z;

	// Token: 0x0400004C RID: 76
	private static method SNIPacketResetPtr = <Module>.__unep@?SNIPacketReset@@$$J0YAXPEAVSNI_Conn@@W4SNI_Packet_IOType@@PEAVSNI_Packet@@W4ConsumerNum@@@Z;

	// Token: 0x0400004D RID: 77
	private static method SNIPacketGetDataWrapperPtr = <Module>.__unep@?SNIPacketGetDataWrapper@@$$FYAKPEAVSNI_Packet@@PEAEKPEAK@Z;

	// Token: 0x0400004E RID: 78
	private static method SNIPacketSetDataPtr = <Module>.__unep@?SNIPacketSetData@@$$J0YAXPEAVSNI_Packet@@PEBEK@Z;

	// Token: 0x02000003 RID: 3
	internal enum QTypes
	{
		// Token: 0x04000050 RID: 80
		SNI_QUERY_CONN_INFO,
		// Token: 0x04000051 RID: 81
		SNI_QUERY_CONN_BUFSIZE,
		// Token: 0x04000052 RID: 82
		SNI_QUERY_CONN_KEY,
		// Token: 0x04000053 RID: 83
		SNI_QUERY_CLIENT_ENCRYPT_POSSIBLE,
		// Token: 0x04000054 RID: 84
		SNI_QUERY_SERVER_ENCRYPT_POSSIBLE,
		// Token: 0x04000055 RID: 85
		SNI_QUERY_CERTIFICATE,
		// Token: 0x04000056 RID: 86
		SNI_QUERY_CONN_ENCRYPT = 7,
		// Token: 0x04000057 RID: 87
		SNI_QUERY_CONN_PROVIDERNUM,
		// Token: 0x04000058 RID: 88
		SNI_QUERY_CONN_CONNID,
		// Token: 0x04000059 RID: 89
		SNI_QUERY_CONN_PARENTCONNID,
		// Token: 0x0400005A RID: 90
		SNI_QUERY_CONN_SECPKG,
		// Token: 0x0400005B RID: 91
		SNI_QUERY_CONN_NETPACKETSIZE,
		// Token: 0x0400005C RID: 92
		SNI_QUERY_CONN_NODENUM,
		// Token: 0x0400005D RID: 93
		SNI_QUERY_CONN_PACKETSRECD,
		// Token: 0x0400005E RID: 94
		SNI_QUERY_CONN_PACKETSSENT,
		// Token: 0x0400005F RID: 95
		SNI_QUERY_CONN_PEERADDR,
		// Token: 0x04000060 RID: 96
		SNI_QUERY_CONN_PEERPORT,
		// Token: 0x04000061 RID: 97
		SNI_QUERY_CONN_LASTREADTIME,
		// Token: 0x04000062 RID: 98
		SNI_QUERY_CONN_LASTWRITETIME,
		// Token: 0x04000063 RID: 99
		SNI_QUERY_CONN_CONSUMER_ID,
		// Token: 0x04000064 RID: 100
		SNI_QUERY_CONN_CONNECTTIME,
		// Token: 0x04000065 RID: 101
		SNI_QUERY_CONN_HTTPENDPOINT,
		// Token: 0x04000066 RID: 102
		SNI_QUERY_CONN_LOCALADDR,
		// Token: 0x04000067 RID: 103
		SNI_QUERY_CONN_LOCALPORT,
		// Token: 0x04000068 RID: 104
		SNI_QUERY_CONN_SSLHANDSHAKESTATE,
		// Token: 0x04000069 RID: 105
		SNI_QUERY_CONN_SOBUFAUTOTUNING,
		// Token: 0x0400006A RID: 106
		SNI_QUERY_CONN_SECPKGNAME,
		// Token: 0x0400006B RID: 107
		SNI_QUERY_CONN_SECPKGMUTUALAUTH,
		// Token: 0x0400006C RID: 108
		SNI_QUERY_CONN_CONSUMERCONNID,
		// Token: 0x0400006D RID: 109
		SNI_QUERY_CONN_SNIUCI,
		// Token: 0x0400006E RID: 110
		SNI_QUERY_LOCALDB_HMODULE = 6,
		// Token: 0x0400006F RID: 111
		SNI_QUERY_TCP_SKIP_IO_COMPLETION_ON_SUCCESS = 35
	}

	// Token: 0x02000004 RID: 4
	internal enum ProviderEnum
	{
		// Token: 0x04000071 RID: 113
		HTTP_PROV,
		// Token: 0x04000072 RID: 114
		NP_PROV,
		// Token: 0x04000073 RID: 115
		SESSION_PROV,
		// Token: 0x04000074 RID: 116
		SIGN_PROV,
		// Token: 0x04000075 RID: 117
		SM_PROV,
		// Token: 0x04000076 RID: 118
		SMUX_PROV,
		// Token: 0x04000077 RID: 119
		SSL_PROV,
		// Token: 0x04000078 RID: 120
		TCP_PROV,
		// Token: 0x04000079 RID: 121
		VIA_PROV,
		// Token: 0x0400007A RID: 122
		MAX_PROVS,
		// Token: 0x0400007B RID: 123
		INVALID_PROV
	}

	// Token: 0x02000005 RID: 5
	internal enum IOType
	{
		// Token: 0x0400007D RID: 125
		READ,
		// Token: 0x0400007E RID: 126
		WRITE
	}

	// Token: 0x02000006 RID: 6
	internal enum ConsumerNumber
	{
		// Token: 0x04000080 RID: 128
		SNI_Consumer_SNI,
		// Token: 0x04000081 RID: 129
		SNI_Consumer_SSB,
		// Token: 0x04000082 RID: 130
		SNI_Consumer_PacketIsReleased,
		// Token: 0x04000083 RID: 131
		SNI_Consumer_Invalid
	}

	// Token: 0x02000007 RID: 7
	// (Invoke) Token: 0x06000088 RID: 136
	internal delegate void SqlAsyncCallbackDelegate(IntPtr ptr1, IntPtr ptr2, uint num);

	// Token: 0x02000008 RID: 8
	[CLSCompliant(false)]
	internal class ConsumerInfo
	{
		// Token: 0x04000084 RID: 132
		internal int defaultBufferSize;

		// Token: 0x04000085 RID: 133
		internal SNINativeMethodWrapper.SqlAsyncCallbackDelegate readDelegate;

		// Token: 0x04000086 RID: 134
		internal SNINativeMethodWrapper.SqlAsyncCallbackDelegate writeDelegate;

		// Token: 0x04000087 RID: 135
		internal IntPtr key;
	}

	// Token: 0x02000009 RID: 9
	[CLSCompliant(false)]
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
	internal class SNI_Error
	{
		// Token: 0x04000088 RID: 136
		internal SNINativeMethodWrapper.ProviderEnum provider;

		// Token: 0x04000089 RID: 137
		internal char[] errorMessage;

		// Token: 0x0400008A RID: 138
		internal uint nativeError;

		// Token: 0x0400008B RID: 139
		internal uint sniError;

		// Token: 0x0400008C RID: 140
		internal string fileName;

		// Token: 0x0400008D RID: 141
		internal string function;

		// Token: 0x0400008E RID: 142
		internal uint lineNumber;
	}
}
