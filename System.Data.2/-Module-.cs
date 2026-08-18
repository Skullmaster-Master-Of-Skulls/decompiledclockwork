using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using System.Threading;
using <CppImplementationDetails>;
using <CrtImplementationDetails>;

// Token: 0x02000001 RID: 1
internal class <Module>
{
	// Token: 0x06000001 RID: 1 RVA: 0x00002AB8 File Offset: 0x00001EB8
	internal unsafe static int memcpy_s(void* _Destination, ulong _DestinationSize, void* _Source, ulong _SourceSize)
	{
		if (_SourceSize == null)
		{
			return 0;
		}
		if (_Destination == null)
		{
			*<Module>._errno() = 22;
			<Module>._invalid_parameter_noinfo();
			return 22;
		}
		if (_Source != null && _DestinationSize >= _SourceSize)
		{
			cpblk(_Destination, _Source, _SourceSize);
			return 0;
		}
		initblk(_Destination, 0, _DestinationSize);
		if (_Source == null)
		{
			*<Module>._errno() = 22;
			<Module>._invalid_parameter_noinfo();
			return 22;
		}
		if (_DestinationSize < _SourceSize)
		{
			*<Module>._errno() = 34;
			<Module>._invalid_parameter_noinfo();
			return 34;
		}
		return 22;
	}

	// Token: 0x06000002 RID: 2 RVA: 0x00002B20 File Offset: 0x00001F20
	internal unsafe static Sni_Consumer_Info* {ctor}(Sni_Consumer_Info* A_0)
	{
		*A_0 = 0;
		*(A_0 + 8L) = 0L;
		*(A_0 + 16L) = 0L;
		*(A_0 + 24L) = 0L;
		*(A_0 + 32L) = 0L;
		*(A_0 + 40L) = 0L;
		*(A_0 + 48L) = 0;
		*(A_0 + 56L) = 0L;
		*(A_0 + 64L) = 0L;
		return A_0;
	}

	// Token: 0x06000003 RID: 3 RVA: 0x00002B70 File Offset: 0x00001F70
	internal unsafe static SNI_CLIENT_CONSUMER_INFO* {ctor}(SNI_CLIENT_CONSUMER_INFO* A_0)
	{
		<Module>.Sni_Consumer_Info.{ctor}(A_0);
		*(A_0 + 72L) = 0L;
		*(A_0 + 80L) = 0;
		*(A_0 + 88L) = 0L;
		*(A_0 + 96L) = 0;
		*(A_0 + 104L) = 0L;
		*(A_0 + 112L) = 0;
		*(A_0 + 116L) = 0;
		*(A_0 + 120L) = 0;
		*(A_0 + 124L) = -1;
		*(A_0 + 128L) = 0;
		*(A_0 + 132L) = 0;
		*(A_0 + 136L) = -1;
		*(A_0 + 140L) = 0;
		return A_0;
	}

	// Token: 0x06000004 RID: 4 RVA: 0x00002BF0 File Offset: 0x00001FF0
	internal unsafe static Guid FromGUID(_GUID* guid)
	{
		Guid result = new Guid((uint)(*guid), *(guid + 4L), *(guid + 6L), *(guid + 8L), *(guid + 9L), *(guid + 10L), *(guid + 11L), *(guid + 12L), *(guid + 13L), *(guid + 14L), *(guid + 15L));
		return result;
	}

	// Token: 0x06000005 RID: 5 RVA: 0x00002C40 File Offset: 0x00002040
	internal unsafe static IUnknown* GetDefaultAppDomain()
	{
		ICorRuntimeHost* ptr = null;
		try
		{
			Guid riid = <Module>.?A0x0e118935.FromGUID(ref <Module>._GUID_cb2f6722_ab3a_11d2_9c40_00c04fa30a3e);
			ptr = (ICorRuntimeHost*)RuntimeEnvironment.GetRuntimeInterfaceAsIntPtr(<Module>.?A0x0e118935.FromGUID(ref <Module>._GUID_cb2f6723_ab3a_11d2_9c40_00c04fa30a3e), riid).ToPointer();
		}
		catch (Exception)
		{
			return 0L;
		}
		IUnknown* ptr2 = null;
		int num = calli(System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,IUnknown**), ptr, ref ptr2, *(*(long*)ptr + 104L));
		ICorRuntimeHost* ptr3 = ptr;
		object obj = calli(System.UInt32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr), ptr3, *(*(long*)ptr3 + 16L));
		return (num >= 0) ? ptr2 : null;
	}

	// Token: 0x06000006 RID: 6 RVA: 0x00005CA0 File Offset: 0x000050A0
	internal static void <CrtImplementationDetails>.ThrowNestedModuleLoadException(Exception innerException, Exception nestedException)
	{
		throw new ModuleLoadExceptionHandlerException("A nested exception occurred after the primary exception that caused the C++ module to fail to load.\n", innerException, nestedException);
	}

	// Token: 0x06000007 RID: 7 RVA: 0x00005754 File Offset: 0x00004B54
	internal static void <CrtImplementationDetails>.ThrowModuleLoadException(string errorMessage)
	{
		throw new ModuleLoadException(errorMessage);
	}

	// Token: 0x06000008 RID: 8 RVA: 0x00005768 File Offset: 0x00004B68
	internal static void <CrtImplementationDetails>.ThrowModuleLoadException(string errorMessage, Exception innerException)
	{
		throw new ModuleLoadException(errorMessage, innerException);
	}

	// Token: 0x06000009 RID: 9 RVA: 0x000057FC File Offset: 0x00004BFC
	internal static void <CrtImplementationDetails>.RegisterModuleUninitializer(EventHandler handler)
	{
		ModuleUninitializer._ModuleUninitializer.AddHandler(handler);
	}

	// Token: 0x0600000A RID: 10 RVA: 0x00002BF0 File Offset: 0x00001FF0
	[SecuritySafeCritical]
	internal unsafe static Guid <CrtImplementationDetails>.FromGUID(_GUID* guid)
	{
		Guid result = new Guid((uint)(*guid), *(guid + 4L), *(guid + 6L), *(guid + 8L), *(guid + 9L), *(guid + 10L), *(guid + 11L), *(guid + 12L), *(guid + 13L), *(guid + 14L), *(guid + 15L));
		return result;
	}

	// Token: 0x0600000B RID: 11 RVA: 0x00005814 File Offset: 0x00004C14
	[SecurityCritical]
	internal unsafe static int __get_default_appdomain(IUnknown** ppUnk)
	{
		ICorRuntimeHost* ptr = null;
		int num;
		try
		{
			Guid riid = <Module>.<CrtImplementationDetails>.FromGUID(ref <Module>._GUID_cb2f6722_ab3a_11d2_9c40_00c04fa30a3e);
			ptr = (ICorRuntimeHost*)RuntimeEnvironment.GetRuntimeInterfaceAsIntPtr(<Module>.<CrtImplementationDetails>.FromGUID(ref <Module>._GUID_cb2f6723_ab3a_11d2_9c40_00c04fa30a3e), riid).ToPointer();
			goto IL_36;
		}
		catch (Exception e)
		{
			num = Marshal.GetHRForException(e);
		}
		if (num < 0)
		{
			return num;
		}
		IL_36:
		long num2 = *(*(long*)ptr + 104L);
		num = calli(System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,IUnknown**), ptr, ppUnk, num2);
		ICorRuntimeHost* ptr2 = ptr;
		object obj = calli(System.UInt32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr), ptr2, *(*(long*)ptr2 + 16L));
		return num;
	}

	// Token: 0x0600000C RID: 12 RVA: 0x00005894 File Offset: 0x00004C94
	internal unsafe static void __release_appdomain(IUnknown* ppUnk)
	{
		object obj = calli(System.UInt32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr), ppUnk, *(*(long*)ppUnk + 16L));
	}

	// Token: 0x0600000D RID: 13 RVA: 0x000058B0 File Offset: 0x00004CB0
	[SecurityCritical]
	internal unsafe static AppDomain <CrtImplementationDetails>.GetDefaultDomain()
	{
		IUnknown* ptr = null;
		int num = <Module>.__get_default_appdomain(&ptr);
		if (num >= 0)
		{
			try
			{
				IntPtr pUnk = new IntPtr((void*)ptr);
				return (AppDomain)Marshal.GetObjectForIUnknown(pUnk);
			}
			finally
			{
				<Module>.__release_appdomain(ptr);
			}
		}
		Marshal.ThrowExceptionForHR(num);
		return null;
	}

	// Token: 0x0600000E RID: 14 RVA: 0x00005910 File Offset: 0x00004D10
	[SecurityCritical]
	internal unsafe static void <CrtImplementationDetails>.DoCallBackInDefaultDomain(method function, void* cookie)
	{
		Guid riid = <Module>.<CrtImplementationDetails>.FromGUID(ref <Module>._GUID_90f1a06c_7712_4762_86b5_7a5eba6bdb02);
		ICLRRuntimeHost* ptr = (ICLRRuntimeHost*)RuntimeEnvironment.GetRuntimeInterfaceAsIntPtr(<Module>.<CrtImplementationDetails>.FromGUID(ref <Module>._GUID_90f1a06e_7712_4762_86b5_7a5eba6bdb02), riid).ToPointer();
		try
		{
			AppDomain appDomain = <Module>.<CrtImplementationDetails>.GetDefaultDomain();
			long num = *(*(long*)ptr + 64L);
			uint id = (uint)appDomain.Id;
			int num2 = calli(System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,System.UInt32 modopt(System.Runtime.CompilerServices.IsLong),System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl) (System.Void*),System.Void*), ptr, id, function, cookie, num);
			if (num2 < 0)
			{
				Marshal.ThrowExceptionForHR(num2);
			}
		}
		finally
		{
			ICLRRuntimeHost* ptr2 = ptr;
			object obj = calli(System.UInt32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr), ptr2, *(*(long*)ptr2 + 16L));
		}
	}

	// Token: 0x0600000F RID: 15 RVA: 0x0000599C File Offset: 0x00004D9C
	[return: MarshalAs(UnmanagedType.U1)]
	internal static bool __scrt_is_safe_for_managed_code()
	{
		uint _scrt_native_dllmain_reason = <Module>.__scrt_native_dllmain_reason;
		if (_scrt_native_dllmain_reason != 0U && _scrt_native_dllmain_reason != 1U)
		{
			return 1;
		}
		return 0;
	}

	// Token: 0x06000010 RID: 16 RVA: 0x000059D0 File Offset: 0x00004DD0
	[SecuritySafeCritical]
	internal unsafe static int <CrtImplementationDetails>.DefaultDomain.DoNothing(void* cookie)
	{
		GC.KeepAlive(int.MaxValue);
		return 0;
	}

	// Token: 0x06000011 RID: 17 RVA: 0x000059F0 File Offset: 0x00004DF0
	[SecuritySafeCritical]
	[return: MarshalAs(UnmanagedType.U1)]
	internal unsafe static bool <CrtImplementationDetails>.DefaultDomain.HasPerProcess()
	{
		if (<Module>.?hasPerProcess@DefaultDomain@<CrtImplementationDetails>@@0W4TriBool@2@A == (TriBool)2)
		{
			void** ptr = (void**)(&<Module>.__xc_mp_a);
			if (ref <Module>.__xc_mp_a < ref <Module>.__xc_mp_z)
			{
				while (*(long*)ptr == 0L)
				{
					ptr += 8L / (long)sizeof(void*);
					if (ptr >= (void**)(&<Module>.__xc_mp_z))
					{
						goto IL_35;
					}
				}
				<Module>.?hasPerProcess@DefaultDomain@<CrtImplementationDetails>@@0W4TriBool@2@A = (TriBool)(-1);
				return 1;
			}
			IL_35:
			<Module>.?hasPerProcess@DefaultDomain@<CrtImplementationDetails>@@0W4TriBool@2@A = (TriBool)0;
			return 0;
		}
		return (<Module>.?hasPerProcess@DefaultDomain@<CrtImplementationDetails>@@0W4TriBool@2@A == (TriBool)(-1)) ? 1 : 0;
	}

	// Token: 0x06000012 RID: 18 RVA: 0x00005A44 File Offset: 0x00004E44
	[SecuritySafeCritical]
	[return: MarshalAs(UnmanagedType.U1)]
	internal unsafe static bool <CrtImplementationDetails>.DefaultDomain.HasNative()
	{
		if (<Module>.?hasNative@DefaultDomain@<CrtImplementationDetails>@@0W4TriBool@2@A == (TriBool)2)
		{
			void** ptr = (void**)(&<Module>.__xi_a);
			if (ref <Module>.__xi_a < ref <Module>.__xi_z)
			{
				while (*(long*)ptr == 0L)
				{
					ptr += 8L / (long)sizeof(void*);
					if (ptr >= (void**)(&<Module>.__xi_z))
					{
						goto IL_35;
					}
				}
				<Module>.?hasNative@DefaultDomain@<CrtImplementationDetails>@@0W4TriBool@2@A = (TriBool)(-1);
				return 1;
			}
			IL_35:
			void** ptr2 = (void**)(&<Module>.__xc_a);
			if (ref <Module>.__xc_a < ref <Module>.__xc_z)
			{
				while (*(long*)ptr2 == 0L)
				{
					ptr2 += 8L / (long)sizeof(void*);
					if (ptr2 >= (void**)(&<Module>.__xc_z))
					{
						goto IL_62;
					}
				}
				<Module>.?hasNative@DefaultDomain@<CrtImplementationDetails>@@0W4TriBool@2@A = (TriBool)(-1);
				return 1;
			}
			IL_62:
			<Module>.?hasNative@DefaultDomain@<CrtImplementationDetails>@@0W4TriBool@2@A = (TriBool)0;
			return 0;
		}
		return (<Module>.?hasNative@DefaultDomain@<CrtImplementationDetails>@@0W4TriBool@2@A == (TriBool)(-1)) ? 1 : 0;
	}

	// Token: 0x06000013 RID: 19 RVA: 0x00005AC4 File Offset: 0x00004EC4
	[SecuritySafeCritical]
	[return: MarshalAs(UnmanagedType.U1)]
	internal static bool <CrtImplementationDetails>.DefaultDomain.NeedsInitialization()
	{
		int num;
		if ((<Module>.<CrtImplementationDetails>.DefaultDomain.HasPerProcess() != null && !<Module>.?InitializedPerProcess@DefaultDomain@<CrtImplementationDetails>@@2_NA) || (<Module>.<CrtImplementationDetails>.DefaultDomain.HasNative() != null && !<Module>.?InitializedNative@DefaultDomain@<CrtImplementationDetails>@@2_NA && <Module>.__scrt_current_native_startup_state == (__scrt_native_startup_state)0))
		{
			num = 1;
		}
		else
		{
			num = 0;
		}
		return (byte)num;
	}

	// Token: 0x06000014 RID: 20 RVA: 0x00005AFC File Offset: 0x00004EFC
	[return: MarshalAs(UnmanagedType.U1)]
	internal static bool <CrtImplementationDetails>.DefaultDomain.NeedsUninitialization()
	{
		return <Module>.?Entered@DefaultDomain@<CrtImplementationDetails>@@2_NA;
	}

	// Token: 0x06000015 RID: 21 RVA: 0x00005B10 File Offset: 0x00004F10
	[SecurityCritical]
	internal static void <CrtImplementationDetails>.DefaultDomain.Initialize()
	{
		<Module>.<CrtImplementationDetails>.DoCallBackInDefaultDomain(<Module>.__unep@?DoNothing@DefaultDomain@<CrtImplementationDetails>@@$$FCAJPEAX@Z, null);
	}

	// Token: 0x06000016 RID: 22 RVA: 0x000029E8 File Offset: 0x00001DE8
	internal static void ??__E?Initialized@CurrentDomain@<CrtImplementationDetails>@@$$Q2HA@@YMXXZ()
	{
		<Module>.?Initialized@CurrentDomain@<CrtImplementationDetails>@@$$Q2HA = 0;
	}

	// Token: 0x06000017 RID: 23 RVA: 0x000029FC File Offset: 0x00001DFC
	internal static void ??__E?Uninitialized@CurrentDomain@<CrtImplementationDetails>@@$$Q2HA@@YMXXZ()
	{
		<Module>.?Uninitialized@CurrentDomain@<CrtImplementationDetails>@@$$Q2HA = 0;
	}

	// Token: 0x06000018 RID: 24 RVA: 0x00002A10 File Offset: 0x00001E10
	internal static void ??__E?IsDefaultDomain@CurrentDomain@<CrtImplementationDetails>@@$$Q2_NA@@YMXXZ()
	{
		<Module>.?IsDefaultDomain@CurrentDomain@<CrtImplementationDetails>@@$$Q2_NA = false;
	}

	// Token: 0x06000019 RID: 25 RVA: 0x00002A24 File Offset: 0x00001E24
	internal static void ??__E?InitializedVtables@CurrentDomain@<CrtImplementationDetails>@@$$Q2W4Progress@2@A@@YMXXZ()
	{
		<Module>.?InitializedVtables@CurrentDomain@<CrtImplementationDetails>@@$$Q2W4Progress@2@A = (Progress)0;
	}

	// Token: 0x0600001A RID: 26 RVA: 0x00002A38 File Offset: 0x00001E38
	internal static void ??__E?InitializedNative@CurrentDomain@<CrtImplementationDetails>@@$$Q2W4Progress@2@A@@YMXXZ()
	{
		<Module>.?InitializedNative@CurrentDomain@<CrtImplementationDetails>@@$$Q2W4Progress@2@A = (Progress)0;
	}

	// Token: 0x0600001B RID: 27 RVA: 0x00002A4C File Offset: 0x00001E4C
	internal static void ??__E?InitializedPerProcess@CurrentDomain@<CrtImplementationDetails>@@$$Q2W4Progress@2@A@@YMXXZ()
	{
		<Module>.?InitializedPerProcess@CurrentDomain@<CrtImplementationDetails>@@$$Q2W4Progress@2@A = (Progress)0;
	}

	// Token: 0x0600001C RID: 28 RVA: 0x00002A60 File Offset: 0x00001E60
	internal static void ??__E?InitializedPerAppDomain@CurrentDomain@<CrtImplementationDetails>@@$$Q2W4Progress@2@A@@YMXXZ()
	{
		<Module>.?InitializedPerAppDomain@CurrentDomain@<CrtImplementationDetails>@@$$Q2W4Progress@2@A = (Progress)0;
	}

	// Token: 0x0600001D RID: 29 RVA: 0x00005CF4 File Offset: 0x000050F4
	[DebuggerStepThrough]
	[SecuritySafeCritical]
	internal unsafe static void <CrtImplementationDetails>.LanguageSupport.InitializeVtables(LanguageSupport* A_0)
	{
		<Module>.gcroot<System::String\u0020^>.=(A_0, "The C++ module failed to load during vtable initialization.\n");
		<Module>.?InitializedVtables@CurrentDomain@<CrtImplementationDetails>@@$$Q2W4Progress@2@A = (Progress)1;
		<Module>._initterm_m((method*)(&<Module>.__xi_vt_a), (method*)(&<Module>.__xi_vt_z));
		<Module>.?InitializedVtables@CurrentDomain@<CrtImplementationDetails>@@$$Q2W4Progress@2@A = (Progress)2;
	}

	// Token: 0x0600001E RID: 30 RVA: 0x00005D28 File Offset: 0x00005128
	[SecuritySafeCritical]
	internal unsafe static void <CrtImplementationDetails>.LanguageSupport.InitializeDefaultAppDomain(LanguageSupport* A_0)
	{
		<Module>.gcroot<System::String\u0020^>.=(A_0, "The C++ module failed to load while attempting to initialize the default appdomain.\n");
		<Module>.<CrtImplementationDetails>.DefaultDomain.Initialize();
	}

	// Token: 0x0600001F RID: 31 RVA: 0x00005D48 File Offset: 0x00005148
	[DebuggerStepThrough]
	[SecuritySafeCritical]
	internal unsafe static void <CrtImplementationDetails>.LanguageSupport.InitializeNative(LanguageSupport* A_0)
	{
		<Module>.gcroot<System::String\u0020^>.=(A_0, "The C++ module failed to load during native initialization.\n");
		<Module>.__security_init_cookie();
		<Module>.?InitializedNative@DefaultDomain@<CrtImplementationDetails>@@2_NA = true;
		if (<Module>.__scrt_is_safe_for_managed_code() == null)
		{
			<Module>.abort();
		}
		if (<Module>.__scrt_current_native_startup_state == (__scrt_native_startup_state)1)
		{
			<Module>.abort();
		}
		if (<Module>.__scrt_current_native_startup_state == (__scrt_native_startup_state)0)
		{
			<Module>.?InitializedNative@CurrentDomain@<CrtImplementationDetails>@@$$Q2W4Progress@2@A = (Progress)1;
			<Module>.__scrt_current_native_startup_state = (__scrt_native_startup_state)1;
			if (<Module>._initterm_e((method*)(&<Module>.__xi_a), (method*)(&<Module>.__xi_z)) != 0)
			{
				<Module>.<CrtImplementationDetails>.ThrowModuleLoadException(<Module>.gcroot<System::String\u0020^>..PE$AAVString@System@@(A_0));
			}
			<Module>._initterm((method*)(&<Module>.__xc_a), (method*)(&<Module>.__xc_z));
			<Module>.__scrt_current_native_startup_state = (__scrt_native_startup_state)2;
			<Module>.?InitializedNativeFromCCTOR@DefaultDomain@<CrtImplementationDetails>@@2_NA = true;
			<Module>.?InitializedNative@CurrentDomain@<CrtImplementationDetails>@@$$Q2W4Progress@2@A = (Progress)2;
		}
	}

	// Token: 0x06000020 RID: 32 RVA: 0x00005DD8 File Offset: 0x000051D8
	[SecurityCritical]
	[DebuggerStepThrough]
	internal unsafe static void <CrtImplementationDetails>.LanguageSupport.InitializePerProcess(LanguageSupport* A_0)
	{
		<Module>.gcroot<System::String\u0020^>.=(A_0, "The C++ module failed to load during process initialization.\n");
		<Module>.?InitializedPerProcess@CurrentDomain@<CrtImplementationDetails>@@$$Q2W4Progress@2@A = (Progress)1;
		<Module>._initatexit_m();
		<Module>._initterm_m((method*)(&<Module>.__xc_mp_a), (method*)(&<Module>.__xc_mp_z));
		<Module>.?InitializedPerProcess@CurrentDomain@<CrtImplementationDetails>@@$$Q2W4Progress@2@A = (Progress)2;
		<Module>.?InitializedPerProcess@DefaultDomain@<CrtImplementationDetails>@@2_NA = true;
	}

	// Token: 0x06000021 RID: 33 RVA: 0x00005E18 File Offset: 0x00005218
	[DebuggerStepThrough]
	[SecurityCritical]
	internal unsafe static void <CrtImplementationDetails>.LanguageSupport.InitializePerAppDomain(LanguageSupport* A_0)
	{
		<Module>.gcroot<System::String\u0020^>.=(A_0, "The C++ module failed to load during appdomain initialization.\n");
		<Module>.?InitializedPerAppDomain@CurrentDomain@<CrtImplementationDetails>@@$$Q2W4Progress@2@A = (Progress)1;
		<Module>._initatexit_app_domain();
		<Module>._initterm_m((method*)(&<Module>.__xc_ma_a), (method*)(&<Module>.__xc_ma_z));
		<Module>.?InitializedPerAppDomain@CurrentDomain@<CrtImplementationDetails>@@$$Q2W4Progress@2@A = (Progress)2;
	}

	// Token: 0x06000022 RID: 34 RVA: 0x00005E54 File Offset: 0x00005254
	[SecurityCritical]
	[DebuggerStepThrough]
	internal unsafe static void <CrtImplementationDetails>.LanguageSupport.InitializeUninitializer(LanguageSupport* A_0)
	{
		<Module>.gcroot<System::String\u0020^>.=(A_0, "The C++ module failed to load during registration for the unload events.\n");
		<Module>.<CrtImplementationDetails>.RegisterModuleUninitializer(new EventHandler(<Module>.<CrtImplementationDetails>.LanguageSupport.DomainUnload));
	}

	// Token: 0x06000023 RID: 35 RVA: 0x00005E80 File Offset: 0x00005280
	[DebuggerStepThrough]
	[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
	[SecurityCritical]
	internal unsafe static void <CrtImplementationDetails>.LanguageSupport._Initialize(LanguageSupport* A_0)
	{
		<Module>.?IsDefaultDomain@CurrentDomain@<CrtImplementationDetails>@@$$Q2_NA = AppDomain.CurrentDomain.IsDefaultAppDomain();
		<Module>.?Entered@DefaultDomain@<CrtImplementationDetails>@@2_NA = (<Module>.?IsDefaultDomain@CurrentDomain@<CrtImplementationDetails>@@$$Q2_NA || <Module>.?Entered@DefaultDomain@<CrtImplementationDetails>@@2_NA);
		void* ptr = <Module>._getFiberPtrId();
		int num = 0;
		int num2 = 0;
		int num3 = 0;
		RuntimeHelpers.PrepareConstrainedRegions();
		try
		{
			while (num2 == 0)
			{
				try
				{
				}
				finally
				{
					void* ptr2 = Interlocked.CompareExchange(ref <Module>.__scrt_native_startup_lock, ptr, 0L);
					if (ptr2 == null)
					{
						num2 = 1;
					}
					else if (ptr2 == ptr)
					{
						num = 1;
						num2 = 1;
					}
				}
				if (num2 == 0)
				{
					<Module>.Sleep(1000);
				}
			}
			<Module>.<CrtImplementationDetails>.LanguageSupport.InitializeVtables(A_0);
			if (<Module>.?IsDefaultDomain@CurrentDomain@<CrtImplementationDetails>@@$$Q2_NA)
			{
				<Module>.<CrtImplementationDetails>.LanguageSupport.InitializeNative(A_0);
				<Module>.<CrtImplementationDetails>.LanguageSupport.InitializePerProcess(A_0);
			}
			else
			{
				num3 = ((<Module>.<CrtImplementationDetails>.DefaultDomain.NeedsInitialization() != 0) ? 1 : num3);
			}
		}
		finally
		{
			if (num == 0)
			{
				Interlocked.Exchange(ref <Module>.__scrt_native_startup_lock, 0L);
			}
		}
		if (num3 != 0)
		{
			<Module>.<CrtImplementationDetails>.LanguageSupport.InitializeDefaultAppDomain(A_0);
		}
		<Module>.<CrtImplementationDetails>.LanguageSupport.InitializePerAppDomain(A_0);
		<Module>.?Initialized@CurrentDomain@<CrtImplementationDetails>@@$$Q2HA = 1;
		<Module>.<CrtImplementationDetails>.LanguageSupport.InitializeUninitializer(A_0);
	}

	// Token: 0x06000024 RID: 36 RVA: 0x00005B2C File Offset: 0x00004F2C
	[SecurityCritical]
	internal static void <CrtImplementationDetails>.LanguageSupport.UninitializeAppDomain()
	{
		<Module>._app_exit_callback();
	}

	// Token: 0x06000025 RID: 37 RVA: 0x00005B40 File Offset: 0x00004F40
	[SecurityCritical]
	internal unsafe static int <CrtImplementationDetails>.LanguageSupport._UninitializeDefaultDomain(void* cookie)
	{
		<Module>._exit_callback();
		<Module>.?InitializedPerProcess@DefaultDomain@<CrtImplementationDetails>@@2_NA = false;
		if (<Module>.?InitializedNativeFromCCTOR@DefaultDomain@<CrtImplementationDetails>@@2_NA)
		{
			<Module>._cexit();
			<Module>.__scrt_current_native_startup_state = (__scrt_native_startup_state)0;
			<Module>.?InitializedNativeFromCCTOR@DefaultDomain@<CrtImplementationDetails>@@2_NA = false;
		}
		<Module>.?InitializedNative@DefaultDomain@<CrtImplementationDetails>@@2_NA = false;
		return 0;
	}

	// Token: 0x06000026 RID: 38 RVA: 0x00005B78 File Offset: 0x00004F78
	[SecurityCritical]
	internal static void <CrtImplementationDetails>.LanguageSupport.UninitializeDefaultDomain()
	{
		if (<Module>.<CrtImplementationDetails>.DefaultDomain.NeedsUninitialization() != null)
		{
			if (AppDomain.CurrentDomain.IsDefaultAppDomain())
			{
				<Module>.<CrtImplementationDetails>.LanguageSupport._UninitializeDefaultDomain(null);
			}
			else
			{
				<Module>.<CrtImplementationDetails>.DoCallBackInDefaultDomain(<Module>.__unep@?_UninitializeDefaultDomain@LanguageSupport@<CrtImplementationDetails>@@$$FCAJPEAX@Z, null);
			}
		}
	}

	// Token: 0x06000027 RID: 39 RVA: 0x00005BB0 File Offset: 0x00004FB0
	[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
	[PrePrepareMethod]
	[SecurityCritical]
	internal static void <CrtImplementationDetails>.LanguageSupport.DomainUnload(object A_0, EventArgs A_1)
	{
		if (<Module>.?Initialized@CurrentDomain@<CrtImplementationDetails>@@$$Q2HA != 0 && Interlocked.Exchange(ref <Module>.?Uninitialized@CurrentDomain@<CrtImplementationDetails>@@$$Q2HA, 1) == 0)
		{
			byte b = (Interlocked.Decrement(ref <Module>.?Count@AllDomains@<CrtImplementationDetails>@@2HA) == 0) ? 1 : 0;
			<Module>.<CrtImplementationDetails>.LanguageSupport.UninitializeAppDomain();
			if (b != 0)
			{
				<Module>.<CrtImplementationDetails>.LanguageSupport.UninitializeDefaultDomain();
			}
		}
	}

	// Token: 0x06000028 RID: 40 RVA: 0x00005F84 File Offset: 0x00005384
	[SecurityCritical]
	[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
	[DebuggerStepThrough]
	internal unsafe static void <CrtImplementationDetails>.LanguageSupport.Cleanup(LanguageSupport* A_0, Exception innerException)
	{
		try
		{
			bool flag = ((Interlocked.Decrement(ref <Module>.?Count@AllDomains@<CrtImplementationDetails>@@2HA) == 0) ? 1 : 0) != 0;
			<Module>.<CrtImplementationDetails>.LanguageSupport.UninitializeAppDomain();
			if (flag)
			{
				<Module>.<CrtImplementationDetails>.LanguageSupport.UninitializeDefaultDomain();
			}
		}
		catch (Exception nestedException)
		{
			<Module>.<CrtImplementationDetails>.ThrowNestedModuleLoadException(innerException, nestedException);
		}
		catch (object obj)
		{
			<Module>.<CrtImplementationDetails>.ThrowNestedModuleLoadException(innerException, null);
		}
	}

	// Token: 0x06000029 RID: 41 RVA: 0x00005FF8 File Offset: 0x000053F8
	[SecurityCritical]
	internal unsafe static LanguageSupport* <CrtImplementationDetails>.LanguageSupport.{ctor}(LanguageSupport* A_0)
	{
		<Module>.gcroot<System::String\u0020^>.{ctor}(A_0);
		return A_0;
	}

	// Token: 0x0600002A RID: 42 RVA: 0x00006010 File Offset: 0x00005410
	[SecurityCritical]
	internal unsafe static void <CrtImplementationDetails>.LanguageSupport.{dtor}(LanguageSupport* A_0)
	{
		<Module>.gcroot<System::String\u0020^>.{dtor}(A_0);
	}

	// Token: 0x0600002B RID: 43 RVA: 0x00006024 File Offset: 0x00005424
	[SecurityCritical]
	[DebuggerStepThrough]
	[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
	internal unsafe static void <CrtImplementationDetails>.LanguageSupport.Initialize(LanguageSupport* A_0)
	{
		bool flag = false;
		RuntimeHelpers.PrepareConstrainedRegions();
		try
		{
			<Module>.gcroot<System::String\u0020^>.=(A_0, "The C++ module failed to load.\n");
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
			}
			finally
			{
				Interlocked.Increment(ref <Module>.?Count@AllDomains@<CrtImplementationDetails>@@2HA);
				flag = true;
			}
			<Module>.<CrtImplementationDetails>.LanguageSupport._Initialize(A_0);
		}
		catch (Exception innerException)
		{
			if (flag)
			{
				<Module>.<CrtImplementationDetails>.LanguageSupport.Cleanup(A_0, innerException);
			}
			<Module>.<CrtImplementationDetails>.ThrowModuleLoadException(<Module>.gcroot<System::String\u0020^>..PE$AAVString@System@@(A_0), innerException);
		}
		catch (object obj)
		{
			if (flag)
			{
				<Module>.<CrtImplementationDetails>.LanguageSupport.Cleanup(A_0, null);
			}
			<Module>.<CrtImplementationDetails>.ThrowModuleLoadException(<Module>.gcroot<System::String\u0020^>..PE$AAVString@System@@(A_0), null);
		}
	}

	// Token: 0x0600002C RID: 44 RVA: 0x000060E0 File Offset: 0x000054E0
	[DebuggerStepThrough]
	[SecurityCritical]
	static unsafe <Module>()
	{
		LanguageSupport languageSupport;
		<Module>.<CrtImplementationDetails>.LanguageSupport.{ctor}(ref languageSupport);
		try
		{
			<Module>.<CrtImplementationDetails>.LanguageSupport.Initialize(ref languageSupport);
		}
		catch
		{
			<Module>.___CxxCallUnwindDtor(ldftn(<CrtImplementationDetails>.LanguageSupport.{dtor}), (void*)(&languageSupport));
			throw;
		}
		<Module>.<CrtImplementationDetails>.LanguageSupport.{dtor}(ref languageSupport);
	}

	// Token: 0x0600002D RID: 45 RVA: 0x00005BEC File Offset: 0x00004FEC
	[SecuritySafeCritical]
	internal unsafe static string PE$AAVString@System@@(gcroot<System::String\u0020^>* A_0)
	{
		IntPtr value = new IntPtr(*A_0);
		return ((GCHandle)value).Target;
	}

	// Token: 0x0600002E RID: 46 RVA: 0x00005C10 File Offset: 0x00005010
	[SecurityCritical]
	[DebuggerStepThrough]
	internal unsafe static gcroot<System::String\u0020^>* =(gcroot<System::String\u0020^>* A_0, string t)
	{
		IntPtr value = new IntPtr(*A_0);
		((GCHandle)value).Target = t;
		return A_0;
	}

	// Token: 0x0600002F RID: 47 RVA: 0x00005C38 File Offset: 0x00005038
	[SecurityCritical]
	[DebuggerStepThrough]
	internal unsafe static void {dtor}(gcroot<System::String\u0020^>* A_0)
	{
		IntPtr value = new IntPtr(*A_0);
		((GCHandle)value).Free();
		*A_0 = 0L;
	}

	// Token: 0x06000030 RID: 48 RVA: 0x00005C60 File Offset: 0x00005060
	[DebuggerStepThrough]
	[SecuritySafeCritical]
	internal unsafe static gcroot<System::String\u0020^>* {ctor}(gcroot<System::String\u0020^>* A_0)
	{
		*A_0 = ((IntPtr)GCHandle.Alloc(null)).ToPointer();
		return A_0;
	}

	// Token: 0x06000031 RID: 49 RVA: 0x00006154 File Offset: 0x00005554
	[SecurityCritical]
	[DebuggerStepThrough]
	internal static ValueType <CrtImplementationDetails>.AtExitLock._handle()
	{
		if (<Module>.?_lock@AtExitLock@<CrtImplementationDetails>@@$$Q0PEAXEA != null)
		{
			IntPtr value = new IntPtr(<Module>.?_lock@AtExitLock@<CrtImplementationDetails>@@$$Q0PEAXEA);
			return GCHandle.FromIntPtr(value);
		}
		return null;
	}

	// Token: 0x06000032 RID: 50 RVA: 0x00006414 File Offset: 0x00005814
	[DebuggerStepThrough]
	[SecurityCritical]
	internal static void <CrtImplementationDetails>.AtExitLock._lock_Construct(object value)
	{
		<Module>.?_lock@AtExitLock@<CrtImplementationDetails>@@$$Q0PEAXEA = null;
		<Module>.<CrtImplementationDetails>.AtExitLock._lock_Set(value);
	}

	// Token: 0x06000033 RID: 51 RVA: 0x00006184 File Offset: 0x00005584
	[SecurityCritical]
	[DebuggerStepThrough]
	internal static void <CrtImplementationDetails>.AtExitLock._lock_Set(object value)
	{
		ValueType valueType = <Module>.<CrtImplementationDetails>.AtExitLock._handle();
		if (valueType == null)
		{
			valueType = GCHandle.Alloc(value);
			<Module>.?_lock@AtExitLock@<CrtImplementationDetails>@@$$Q0PEAXEA = GCHandle.ToIntPtr((GCHandle)valueType).ToPointer();
		}
		else
		{
			((GCHandle)valueType).Target = value;
		}
	}

	// Token: 0x06000034 RID: 52 RVA: 0x000061D4 File Offset: 0x000055D4
	[SecurityCritical]
	[DebuggerStepThrough]
	internal static object <CrtImplementationDetails>.AtExitLock._lock_Get()
	{
		ValueType valueType = <Module>.<CrtImplementationDetails>.AtExitLock._handle();
		if (valueType != null)
		{
			return ((GCHandle)valueType).Target;
		}
		return null;
	}

	// Token: 0x06000035 RID: 53 RVA: 0x000061F8 File Offset: 0x000055F8
	[SecurityCritical]
	[DebuggerStepThrough]
	internal static void <CrtImplementationDetails>.AtExitLock._lock_Destruct()
	{
		ValueType valueType = <Module>.<CrtImplementationDetails>.AtExitLock._handle();
		if (valueType != null)
		{
			((GCHandle)valueType).Free();
			<Module>.?_lock@AtExitLock@<CrtImplementationDetails>@@$$Q0PEAXEA = null;
		}
	}

	// Token: 0x06000036 RID: 54 RVA: 0x00006220 File Offset: 0x00005620
	[SecurityCritical]
	[DebuggerStepThrough]
	[return: MarshalAs(UnmanagedType.U1)]
	internal static bool <CrtImplementationDetails>.AtExitLock.IsInitialized()
	{
		return (<Module>.<CrtImplementationDetails>.AtExitLock._lock_Get() != null) ? 1 : 0;
	}

	// Token: 0x06000037 RID: 55 RVA: 0x00006430 File Offset: 0x00005830
	[SecurityCritical]
	[DebuggerStepThrough]
	internal static void <CrtImplementationDetails>.AtExitLock.AddRef()
	{
		if (<Module>.<CrtImplementationDetails>.AtExitLock.IsInitialized() == null)
		{
			<Module>.<CrtImplementationDetails>.AtExitLock._lock_Construct(new object());
			<Module>.?_ref_count@AtExitLock@<CrtImplementationDetails>@@$$Q0HA = 0;
		}
		<Module>.?_ref_count@AtExitLock@<CrtImplementationDetails>@@$$Q0HA++;
	}

	// Token: 0x06000038 RID: 56 RVA: 0x0000623C File Offset: 0x0000563C
	[SecurityCritical]
	[DebuggerStepThrough]
	internal static void <CrtImplementationDetails>.AtExitLock.RemoveRef()
	{
		<Module>.?_ref_count@AtExitLock@<CrtImplementationDetails>@@$$Q0HA += -1;
		if (<Module>.?_ref_count@AtExitLock@<CrtImplementationDetails>@@$$Q0HA == 0)
		{
			<Module>.<CrtImplementationDetails>.AtExitLock._lock_Destruct();
		}
	}

	// Token: 0x06000039 RID: 57 RVA: 0x00006460 File Offset: 0x00005860
	[SecurityCritical]
	[DebuggerStepThrough]
	[return: MarshalAs(UnmanagedType.U1)]
	internal static bool __alloc_global_lock()
	{
		<Module>.<CrtImplementationDetails>.AtExitLock.AddRef();
		return <Module>.<CrtImplementationDetails>.AtExitLock.IsInitialized();
	}

	// Token: 0x0600003A RID: 58 RVA: 0x00006264 File Offset: 0x00005664
	[SecurityCritical]
	[DebuggerStepThrough]
	internal static void __dealloc_global_lock()
	{
		<Module>.<CrtImplementationDetails>.AtExitLock.RemoveRef();
	}

	// Token: 0x0600003B RID: 59 RVA: 0x00006278 File Offset: 0x00005678
	[SecurityCritical]
	internal unsafe static void _exit_callback()
	{
		if (<Module>.?A0x618b78b6.__exit_list_size != 0UL)
		{
			method* ptr = (method*)<Module>.DecodePointer((void*)<Module>.?A0x618b78b6.__onexitbegin_m);
			method* ptr2 = (method*)<Module>.DecodePointer((void*)<Module>.?A0x618b78b6.__onexitend_m);
			if (ptr != -1L && ptr != null && ptr2 != null)
			{
				method* ptr3 = ptr;
				method* ptr4 = ptr2;
				for (;;)
				{
					ptr2 -= 8L / (long)sizeof(method);
					if (ptr2 < ptr)
					{
						break;
					}
					if (*(long*)ptr2 != <Module>.EncodePointer(null))
					{
						void* ptr5 = <Module>.DecodePointer(*(long*)ptr2);
						*(long*)ptr2 = <Module>.EncodePointer(null);
						calli(System.Void(), ptr5);
						method* ptr6 = (method*)<Module>.DecodePointer((void*)<Module>.?A0x618b78b6.__onexitbegin_m);
						method* ptr7 = (method*)<Module>.DecodePointer((void*)<Module>.?A0x618b78b6.__onexitend_m);
						if (ptr3 != ptr6 || ptr4 != ptr7)
						{
							ptr3 = ptr6;
							ptr = ptr6;
							ptr4 = ptr7;
							ptr2 = ptr7;
						}
					}
				}
				IntPtr hglobal = new IntPtr((void*)ptr);
				Marshal.FreeHGlobal(hglobal);
			}
			<Module>.?A0x618b78b6.__dealloc_global_lock();
		}
	}

	// Token: 0x0600003C RID: 60 RVA: 0x00006478 File Offset: 0x00005878
	[DebuggerStepThrough]
	[SecurityCritical]
	internal unsafe static int _initatexit_m()
	{
		int result = 0;
		if (<Module>.?A0x618b78b6.__alloc_global_lock() == 1)
		{
			<Module>.?A0x618b78b6.__onexitbegin_m = (method*)<Module>.EncodePointer(Marshal.AllocHGlobal(256).ToPointer());
			<Module>.?A0x618b78b6.__onexitend_m = <Module>.?A0x618b78b6.__onexitbegin_m;
			<Module>.?A0x618b78b6.__exit_list_size = 32UL;
			result = 1;
		}
		return result;
	}

	// Token: 0x0600003D RID: 61 RVA: 0x000064C0 File Offset: 0x000058C0
	[DebuggerStepThrough]
	[SecurityCritical]
	internal unsafe static int _initatexit_app_domain()
	{
		if (<Module>.?A0x618b78b6.__alloc_global_lock() == 1)
		{
			<Module>.__onexitbegin_app_domain = (method*)<Module>.EncodePointer(Marshal.AllocHGlobal(256).ToPointer());
			<Module>.__onexitend_app_domain = <Module>.__onexitbegin_app_domain;
			<Module>.__exit_list_size_app_domain = 32UL;
		}
		return 1;
	}

	// Token: 0x0600003E RID: 62 RVA: 0x00006328 File Offset: 0x00005728
	[SecurityCritical]
	[HandleProcessCorruptedStateExceptions]
	internal unsafe static void _app_exit_callback()
	{
		if (<Module>.__exit_list_size_app_domain != 0UL)
		{
			method* ptr = (method*)<Module>.DecodePointer((void*)<Module>.__onexitbegin_app_domain);
			method* ptr2 = (method*)<Module>.DecodePointer((void*)<Module>.__onexitend_app_domain);
			try
			{
				if (ptr != -1L && ptr != null && ptr2 != null)
				{
					method* ptr3 = ptr;
					method* ptr4 = ptr2;
					for (;;)
					{
						do
						{
							ptr2 -= 8L / (long)sizeof(method);
						}
						while (ptr2 >= ptr && *(long*)ptr2 == <Module>.EncodePointer(null));
						if (ptr2 < ptr)
						{
							break;
						}
						method system.Void_u0020() = <Module>.DecodePointer(*(long*)ptr2);
						*(long*)ptr2 = <Module>.EncodePointer(null);
						calli(System.Void(), system.Void_u0020());
						method* ptr5 = (method*)<Module>.DecodePointer((void*)<Module>.__onexitbegin_app_domain);
						method* ptr6 = (method*)<Module>.DecodePointer((void*)<Module>.__onexitend_app_domain);
						if (ptr3 != ptr5 || ptr4 != ptr6)
						{
							ptr3 = ptr5;
							ptr = ptr5;
							ptr4 = ptr6;
							ptr2 = ptr6;
						}
					}
				}
			}
			finally
			{
				IntPtr hglobal = new IntPtr((void*)ptr);
				Marshal.FreeHGlobal(hglobal);
				<Module>.?A0x618b78b6.__dealloc_global_lock();
			}
		}
	}

	// Token: 0x0600003F RID: 63
	[SuppressUnmanagedCodeSecurity]
	[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
	[SecurityCritical]
	[DllImport("KERNEL32.dll")]
	public unsafe static extern void* DecodePointer(void* _Ptr);

	// Token: 0x06000040 RID: 64
	[SecurityCritical]
	[SuppressUnmanagedCodeSecurity]
	[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
	[DllImport("KERNEL32.dll")]
	public unsafe static extern void* EncodePointer(void* _Ptr);

	// Token: 0x06000041 RID: 65 RVA: 0x00006504 File Offset: 0x00005904
	[DebuggerStepThrough]
	[SecurityCritical]
	internal unsafe static int _initterm_e(method* pfbegin, method* pfend)
	{
		int num = 0;
		if (pfbegin < pfend)
		{
			while (num == 0)
			{
				ulong num2 = (ulong)(*(long*)pfbegin);
				if (num2 != 0UL)
				{
					num = calli(System.Int32 modopt(System.Runtime.CompilerServices.CallConvCdecl)(), num2);
				}
				pfbegin += 8L / (long)sizeof(method);
				if (pfbegin >= pfend)
				{
					break;
				}
			}
		}
		return num;
	}

	// Token: 0x06000042 RID: 66 RVA: 0x00006534 File Offset: 0x00005934
	[DebuggerStepThrough]
	[SecurityCritical]
	internal unsafe static void _initterm(method* pfbegin, method* pfend)
	{
		if (pfbegin < pfend)
		{
			do
			{
				ulong num = (ulong)(*(long*)pfbegin);
				if (num != 0UL)
				{
					calli(System.Void modopt(System.Runtime.CompilerServices.CallConvCdecl)(), num);
				}
				pfbegin += 8L / (long)sizeof(method);
			}
			while (pfbegin < pfend);
		}
	}

	// Token: 0x06000043 RID: 67 RVA: 0x0000655C File Offset: 0x0000595C
	[DebuggerStepThrough]
	internal static ModuleHandle <CrtImplementationDetails>.ThisModule.Handle()
	{
		return typeof(ThisModule).Module.ModuleHandle;
	}

	// Token: 0x06000044 RID: 68 RVA: 0x000065AC File Offset: 0x000059AC
	[SecurityCritical]
	[DebuggerStepThrough]
	[SecurityPermission(SecurityAction.Assert, UnmanagedCode = true)]
	internal unsafe static void _initterm_m(method* pfbegin, method* pfend)
	{
		if (pfbegin < pfend)
		{
			do
			{
				ulong num = (ulong)(*(long*)pfbegin);
				if (num != 0UL)
				{
					object obj = calli(System.Void modopt(System.Runtime.CompilerServices.IsConst)*(), <Module>.<CrtImplementationDetails>.ThisModule.ResolveMethod<void\u0020const\u0020*\u0020__clrcall(void)>(num));
				}
				pfbegin += 8L / (long)sizeof(method);
			}
			while (pfbegin < pfend);
		}
	}

	// Token: 0x06000045 RID: 69 RVA: 0x00006580 File Offset: 0x00005980
	[DebuggerStepThrough]
	[SecurityCritical]
	internal static method <CrtImplementationDetails>.ThisModule.ResolveMethod<void\u0020const\u0020*\u0020__clrcall(void)>(method methodToken)
	{
		return <Module>.<CrtImplementationDetails>.ThisModule.Handle().ResolveMethodHandle(methodToken).GetFunctionPointer().ToPointer();
	}

	// Token: 0x06000046 RID: 70 RVA: 0x000065DC File Offset: 0x000059DC
	[HandleProcessCorruptedStateExceptions]
	[SecurityCritical]
	[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
	[SecurityPermission(SecurityAction.Assert, UnmanagedCode = true)]
	internal unsafe static void ___CxxCallUnwindDtor(method pDtor, void* pThis)
	{
		try
		{
			calli(System.Void(System.Void*), pThis, pDtor);
		}
		catch when (endfilter(<Module>.__FrameUnwindFilter(Marshal.GetExceptionPointers()) != null))
		{
		}
	}

	// Token: 0x06000047 RID: 71 RVA: 0x000048D0 File Offset: 0x00003CD0
	[SuppressUnmanagedCodeSecurity]
	[SecurityCritical]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig)]
	internal unsafe static extern void* NativeGetData(int*);

	// Token: 0x06000048 RID: 72 RVA: 0x000048E0 File Offset: 0x00003CE0
	[SuppressUnmanagedCodeSecurity]
	[SecurityCritical]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig)]
	[return: MarshalAs(UnmanagedType.U1)]
	internal unsafe static extern bool NativeSetData(void*, int);

	// Token: 0x06000049 RID: 73 RVA: 0x00002CF0 File Offset: 0x000020F0
	[SecurityCritical]
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig)]
	internal unsafe static extern uint SNIWriteSyncOverAsync(SNI_ConnWrapper*, SNI_Packet*);

	// Token: 0x0600004A RID: 74 RVA: 0x00002DD0 File Offset: 0x000021D0
	[SecurityCritical]
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig)]
	internal unsafe static extern uint SNIReadSyncOverAsync(SNI_ConnWrapper*, SNI_Packet**, int);

	// Token: 0x0600004B RID: 75 RVA: 0x00002FF0 File Offset: 0x000023F0
	[SuppressUnmanagedCodeSecurity]
	[SecurityCritical]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig)]
	internal unsafe static extern uint SNIOpenWrapper(Sni_Consumer_Info*, ushort*, void*, SNI_ConnWrapper**, int);

	// Token: 0x0600004C RID: 76 RVA: 0x00004880 File Offset: 0x00003C80
	[SecurityCritical]
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig)]
	internal unsafe static extern void* __delDtor(SNI_ConnWrapper*, uint);

	// Token: 0x0600004D RID: 77 RVA: 0x00003110 File Offset: 0x00002510
	[SuppressUnmanagedCodeSecurity]
	[SecurityCritical]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig)]
	internal unsafe static extern uint SNIOpenSyncExWrapper(SNI_CLIENT_CONSUMER_INFO*, SNI_ConnWrapper**);

	// Token: 0x0600004E RID: 78 RVA: 0x00003220 File Offset: 0x00002620
	[SecurityCritical]
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig)]
	internal unsafe static extern uint UnmanagedIsTokenRestricted(void*, int*);

	// Token: 0x0600004F RID: 79 RVA: 0x000141C4 File Offset: 0x000135C4
	[SuppressUnmanagedCodeSecurity]
	[SecurityCritical]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig)]
	internal unsafe static extern int SNIServerEnumRead(void*, ushort*, int, int*);

	// Token: 0x06000050 RID: 80 RVA: 0x00014C70 File Offset: 0x00014070
	[SuppressUnmanagedCodeSecurity]
	[SecurityCritical]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig)]
	internal unsafe static extern uint SNISecGenClientContext(SNI_Conn*, byte*, uint, byte*, uint*, int*, ushort*, uint, ushort*, ushort*);

	// Token: 0x06000051 RID: 81 RVA: 0x0000CB3C File Offset: 0x0000BF3C
	[SecurityCritical]
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig)]
	internal unsafe static extern uint SNIQueryInfo(uint, void*);

	// Token: 0x06000052 RID: 82 RVA: 0x00035580 File Offset: 0x00034980
	[SuppressUnmanagedCodeSecurity]
	[SecurityCritical]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig)]
	internal static extern uint SNISecADALInitialize();

	// Token: 0x06000053 RID: 83 RVA: 0x00033C89 File Offset: 0x00033089
	[SecurityCritical]
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged)]
	internal unsafe static extern int* _errno();

	// Token: 0x06000054 RID: 84 RVA: 0x0000DEE4 File Offset: 0x0000D2E4
	[SuppressUnmanagedCodeSecurity]
	[SecurityCritical]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig)]
	internal unsafe static extern uint SNIWaitForSSLHandshakeToComplete(SNI_Conn*, uint);

	// Token: 0x06000055 RID: 85 RVA: 0x00013D98 File Offset: 0x00013198
	[SecurityCritical]
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig)]
	internal unsafe static extern void* SNIServerEnumOpen(ushort*, int);

	// Token: 0x06000056 RID: 86 RVA: 0x0000D594 File Offset: 0x0000C994
	[SuppressUnmanagedCodeSecurity]
	[SecurityCritical]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig)]
	internal unsafe static extern uint SNISetInfo(SNI_Conn*, uint, void*);

	// Token: 0x06000057 RID: 87 RVA: 0x00033C83 File Offset: 0x00033083
	[SuppressUnmanagedCodeSecurity]
	[SecurityCritical]
	[DllImport("", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged)]
	internal static extern void _invalid_parameter_noinfo();

	// Token: 0x06000058 RID: 88 RVA: 0x0000B344 File Offset: 0x0000A744
	[SecurityCritical]
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig)]
	internal unsafe static extern uint SNIInitialize(void*);

	// Token: 0x06000059 RID: 89 RVA: 0x0000A10C File Offset: 0x0000950C
	[SecurityCritical]
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig)]
	internal unsafe static extern uint SNIAddProvider(SNI_Conn*, ProviderNum, void*);

	// Token: 0x0600005A RID: 90 RVA: 0x00015340 File Offset: 0x00014740
	[SuppressUnmanagedCodeSecurity]
	[SecurityCritical]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig)]
	internal unsafe static extern uint SNISecInitPackage(uint*);

	// Token: 0x0600005B RID: 91 RVA: 0x0000ABC0 File Offset: 0x00009FC0
	[SecurityCritical]
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig)]
	internal unsafe static extern uint SNIGetInfo(SNI_Conn*, uint, void*);

	// Token: 0x0600005C RID: 92 RVA: 0x00034E9C File Offset: 0x0003429C
	[SecurityCritical]
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig)]
	internal unsafe static extern uint SNISecADALGetAccessToken(ushort*, ushort*, ushort*, ushort*, _GUID*, ushort*, bool*, ushort**, uint*, ushort**, uint*, uint*, uint*, _FILETIME*);

	// Token: 0x0600005D RID: 93 RVA: 0x0000D1B4 File Offset: 0x0000C5B4
	[SuppressUnmanagedCodeSecurity]
	[SecurityCritical]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig)]
	internal unsafe static extern uint SNIRemoveProvider(SNI_Conn*, ProviderNum);

	// Token: 0x0600005E RID: 94 RVA: 0x00033884 File Offset: 0x00032C84
	[SuppressUnmanagedCodeSecurity]
	[SecurityCritical]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig)]
	internal unsafe static extern void delete[](void*);

	// Token: 0x0600005F RID: 95 RVA: 0x0000EC50 File Offset: 0x0000E050
	[SecurityCritical]
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig)]
	internal unsafe static extern void SNIGetLastError(SNI_ERROR*);

	// Token: 0x06000060 RID: 96 RVA: 0x000059C0 File Offset: 0x00004DC0
	[SecurityCritical]
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig)]
	internal unsafe static extern void* _getFiberPtrId();

	// Token: 0x06000061 RID: 97 RVA: 0x000055C2 File Offset: 0x000049C2
	[SecurityCritical]
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged)]
	internal static extern void _cexit();

	// Token: 0x06000062 RID: 98 RVA: 0x000049C2 File Offset: 0x00003DC2
	[SuppressUnmanagedCodeSecurity]
	[SecurityCritical]
	[DllImport("", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged)]
	internal static extern void Sleep(uint);

	// Token: 0x06000063 RID: 99 RVA: 0x00034E69 File Offset: 0x00034269
	[SuppressUnmanagedCodeSecurity]
	[SecurityCritical]
	[DllImport("", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged)]
	internal static extern void abort();

	// Token: 0x06000064 RID: 100 RVA: 0x00004D38 File Offset: 0x00004138
	[SecurityCritical]
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig)]
	internal static extern void __security_init_cookie();

	// Token: 0x06000065 RID: 101 RVA: 0x00034E63 File Offset: 0x00034263
	[SuppressUnmanagedCodeSecurity]
	[SecurityCritical]
	[DllImport("", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged)]
	internal unsafe static extern int __FrameUnwindFilter(_EXCEPTION_POINTERS*);

	// Token: 0x04000001 RID: 1 RVA: 0x0014CC60 File Offset: 0x0014A460
	internal static $ArrayType$$$BY08$$CBG ??_C@_1BC@LEJJAHNB@?$AAs?$AAe?$AAs?$AAs?$AAi?$AAo?$AAn?$AA?3@;

	// Token: 0x04000002 RID: 2 RVA: 0x0014CBF0 File Offset: 0x0014A3F0
	internal static __s_GUID _GUID_cb2f6723_ab3a_11d2_9c40_00c04fa30a3e;

	// Token: 0x04000003 RID: 3 RVA: 0x0014CBE0 File Offset: 0x0014A3E0
	internal static __s_GUID _GUID_cb2f6722_ab3a_11d2_9c40_00c04fa30a3e;

	// Token: 0x04000004 RID: 4 RVA: 0x0034F670 File Offset: 0x0034CC70
	internal unsafe static void* ?data@SqlDependencyProcessDispatcherStorage@@0PEAXEA;

	// Token: 0x04000005 RID: 5 RVA: 0x0034F678 File Offset: 0x0034CC78
	internal static int ?size@SqlDependencyProcessDispatcherStorage@@0HA;

	// Token: 0x04000006 RID: 6 RVA: 0x0034F67C File Offset: 0x0034CC7C
	internal static volatile int ?lock@SqlDependencyProcessDispatcherStorage@@0JC;

	// Token: 0x04000007 RID: 7 RVA: 0x0034F000 File Offset: 0x0034C600
	public static method __m2mep@?memcpy_s@?A0x0e118935@@$$J0YAHQEAX_KQEBX1@Z;

	// Token: 0x04000008 RID: 8 RVA: 0x0014CC88 File Offset: 0x0014A488
	unsafe static int** __unep@?SNIWriteAsyncWrapper@@$$FYAKPEAUSNI_ConnWrapper@@PEAVSNI_Packet@@@Z;

	// Token: 0x04000009 RID: 9 RVA: 0x0014CC90 File Offset: 0x0014A490
	unsafe static int** __unep@?SNIReadAsyncWrapper@@$$FYAKPEAUSNI_ConnWrapper@@PEAPEAVSNI_Packet@@@Z;

	// Token: 0x0400000A RID: 10 RVA: 0x0014CCB0 File Offset: 0x0014A4B0
	unsafe static int** __unep@?SNIPacketGetDataWrapper@@$$FYAKPEAVSNI_Packet@@PEAEKPEAK@Z;

	// Token: 0x0400000B RID: 11 RVA: 0x0014CC50 File Offset: 0x0014A450
	unsafe static int** __unep@?SNIServerEnumClose@@$$J0YAXPEAX@Z;

	// Token: 0x0400000C RID: 12 RVA: 0x0014CC58 File Offset: 0x0014A458
	unsafe static int** __unep@?SNIClose@@$$J0YAKPEAVSNI_Conn@@@Z;

	// Token: 0x0400000D RID: 13 RVA: 0x0014CC78 File Offset: 0x0014A478
	unsafe static int** __unep@?SNITerminate@@$$J0YAKXZ;

	// Token: 0x0400000E RID: 14 RVA: 0x0014CC80 File Offset: 0x0014A480
	unsafe static int** __unep@?SNICheckConnection@@$$J0YAKPEAVSNI_Conn@@@Z;

	// Token: 0x0400000F RID: 15 RVA: 0x0014CC98 File Offset: 0x0014A498
	unsafe static int** __unep@?SNIPacketAllocate@@$$J0YAPEAVSNI_Packet@@PEAVSNI_Conn@@W4SNI_Packet_IOType@@@Z;

	// Token: 0x04000010 RID: 16 RVA: 0x0014CCA0 File Offset: 0x0014A4A0
	unsafe static int** __unep@?SNIPacketRelease@@$$J0YAXPEAVSNI_Packet@@@Z;

	// Token: 0x04000011 RID: 17 RVA: 0x0014CCA8 File Offset: 0x0014A4A8
	unsafe static int** __unep@?SNIPacketReset@@$$J0YAXPEAVSNI_Conn@@W4SNI_Packet_IOType@@PEAVSNI_Packet@@W4ConsumerNum@@@Z;

	// Token: 0x04000012 RID: 18 RVA: 0x0014CCB8 File Offset: 0x0014A4B8
	unsafe static int** __unep@?SNIPacketSetData@@$$J0YAXPEAVSNI_Packet@@PEBEK@Z;

	// Token: 0x04000013 RID: 19
	[FixedAddressValueType]
	internal static int ?Uninitialized@CurrentDomain@<CrtImplementationDetails>@@$$Q2HA;

	// Token: 0x04000014 RID: 20 RVA: 0x0014C6C0 File Offset: 0x00149EC0
	internal static method ?Uninitialized$initializer$@CurrentDomain@<CrtImplementationDetails>@@$$Q2P6MXXZEA;

	// Token: 0x04000015 RID: 21
	[FixedAddressValueType]
	internal static Progress ?InitializedNative@CurrentDomain@<CrtImplementationDetails>@@$$Q2W4Progress@2@A;

	// Token: 0x04000016 RID: 22 RVA: 0x0014C6D8 File Offset: 0x00149ED8
	internal static method ?InitializedNative$initializer$@CurrentDomain@<CrtImplementationDetails>@@$$Q2P6MXXZEA;

	// Token: 0x04000017 RID: 23 RVA: 0x0014CCE0 File Offset: 0x0014A4E0
	internal static __s_GUID _GUID_90f1a06c_7712_4762_86b5_7a5eba6bdb02;

	// Token: 0x04000018 RID: 24 RVA: 0x0014CCF0 File Offset: 0x0014A4F0
	internal static __s_GUID _GUID_90f1a06e_7712_4762_86b5_7a5eba6bdb02;

	// Token: 0x04000019 RID: 25
	[FixedAddressValueType]
	internal static Progress ?InitializedPerAppDomain@CurrentDomain@<CrtImplementationDetails>@@$$Q2W4Progress@2@A;

	// Token: 0x0400001A RID: 26 RVA: 0x0034F9A4 File Offset: 0x0034CFA4
	internal static bool ?Entered@DefaultDomain@<CrtImplementationDetails>@@2_NA;

	// Token: 0x0400001B RID: 27 RVA: 0x0034F044 File Offset: 0x0034C644
	internal static TriBool ?hasNative@DefaultDomain@<CrtImplementationDetails>@@0W4TriBool@2@A;

	// Token: 0x0400001C RID: 28 RVA: 0x0034F9A7 File Offset: 0x0034CFA7
	internal static bool ?InitializedPerProcess@DefaultDomain@<CrtImplementationDetails>@@2_NA;

	// Token: 0x0400001D RID: 29 RVA: 0x0034F9A0 File Offset: 0x0034CFA0
	internal static int ?Count@AllDomains@<CrtImplementationDetails>@@2HA;

	// Token: 0x0400001E RID: 30
	[FixedAddressValueType]
	internal static int ?Initialized@CurrentDomain@<CrtImplementationDetails>@@$$Q2HA;

	// Token: 0x0400001F RID: 31 RVA: 0x0034F9A6 File Offset: 0x0034CFA6
	internal static bool ?InitializedNativeFromCCTOR@DefaultDomain@<CrtImplementationDetails>@@2_NA;

	// Token: 0x04000020 RID: 32
	[FixedAddressValueType]
	internal static bool ?IsDefaultDomain@CurrentDomain@<CrtImplementationDetails>@@$$Q2_NA;

	// Token: 0x04000021 RID: 33
	[FixedAddressValueType]
	internal static Progress ?InitializedVtables@CurrentDomain@<CrtImplementationDetails>@@$$Q2W4Progress@2@A;

	// Token: 0x04000022 RID: 34 RVA: 0x0034F9A5 File Offset: 0x0034CFA5
	internal static bool ?InitializedNative@DefaultDomain@<CrtImplementationDetails>@@2_NA;

	// Token: 0x04000023 RID: 35
	[FixedAddressValueType]
	internal static Progress ?InitializedPerProcess@CurrentDomain@<CrtImplementationDetails>@@$$Q2W4Progress@2@A;

	// Token: 0x04000024 RID: 36 RVA: 0x0034F040 File Offset: 0x0034C640
	internal static TriBool ?hasPerProcess@DefaultDomain@<CrtImplementationDetails>@@0W4TriBool@2@A;

	// Token: 0x04000025 RID: 37 RVA: 0x0014C700 File Offset: 0x00149F00
	internal static $ArrayType$$$BY00Q6MPEBXXZ __xc_mp_z;

	// Token: 0x04000026 RID: 38 RVA: 0x0014C710 File Offset: 0x00149F10
	internal static $ArrayType$$$BY00Q6MPEBXXZ __xi_vt_z;

	// Token: 0x04000027 RID: 39 RVA: 0x0014C6E0 File Offset: 0x00149EE0
	internal static method ?InitializedPerProcess$initializer$@CurrentDomain@<CrtImplementationDetails>@@$$Q2P6MXXZEA;

	// Token: 0x04000028 RID: 40 RVA: 0x0014C6B0 File Offset: 0x00149EB0
	internal static $ArrayType$$$BY00Q6MPEBXXZ __xc_ma_a;

	// Token: 0x04000029 RID: 41 RVA: 0x0014C6F0 File Offset: 0x00149EF0
	internal static $ArrayType$$$BY00Q6MPEBXXZ __xc_ma_z;

	// Token: 0x0400002A RID: 42 RVA: 0x0014C6E8 File Offset: 0x00149EE8
	internal static method ?InitializedPerAppDomain$initializer$@CurrentDomain@<CrtImplementationDetails>@@$$Q2P6MXXZEA;

	// Token: 0x0400002B RID: 43 RVA: 0x0014C708 File Offset: 0x00149F08
	internal static $ArrayType$$$BY00Q6MPEBXXZ __xi_vt_a;

	// Token: 0x0400002C RID: 44 RVA: 0x0014C6B8 File Offset: 0x00149EB8
	internal static method ?Initialized$initializer$@CurrentDomain@<CrtImplementationDetails>@@$$Q2P6MXXZEA;

	// Token: 0x0400002D RID: 45 RVA: 0x0014C6F8 File Offset: 0x00149EF8
	internal static $ArrayType$$$BY00Q6MPEBXXZ __xc_mp_a;

	// Token: 0x0400002E RID: 46 RVA: 0x0014C6D0 File Offset: 0x00149ED0
	internal static method ?InitializedVtables$initializer$@CurrentDomain@<CrtImplementationDetails>@@$$Q2P6MXXZEA;

	// Token: 0x0400002F RID: 47 RVA: 0x0014C6C8 File Offset: 0x00149EC8
	internal static method ?IsDefaultDomain$initializer$@CurrentDomain@<CrtImplementationDetails>@@$$Q2P6MXXZEA;

	// Token: 0x04000030 RID: 48 RVA: 0x0034F048 File Offset: 0x0034C648
	public static method __m2mep@?DoNothing@DefaultDomain@<CrtImplementationDetails>@@$$FCAJPEAX@Z;

	// Token: 0x04000031 RID: 49 RVA: 0x0034F058 File Offset: 0x0034C658
	public static method __m2mep@?_UninitializeDefaultDomain@LanguageSupport@<CrtImplementationDetails>@@$$FCAJPEAX@Z;

	// Token: 0x04000032 RID: 50 RVA: 0x0014CD00 File Offset: 0x0014A500
	public unsafe static int** __unep@?DoNothing@DefaultDomain@<CrtImplementationDetails>@@$$FCAJPEAX@Z;

	// Token: 0x04000033 RID: 51 RVA: 0x0014CD08 File Offset: 0x0014A508
	public unsafe static int** __unep@?_UninitializeDefaultDomain@LanguageSupport@<CrtImplementationDetails>@@$$FCAJPEAX@Z;

	// Token: 0x04000034 RID: 52 RVA: 0x0034FB10 File Offset: 0x0034D110
	internal unsafe static method* __onexitbegin_m;

	// Token: 0x04000035 RID: 53 RVA: 0x0034FB08 File Offset: 0x0034D108
	internal static ulong __exit_list_size;

	// Token: 0x04000036 RID: 54
	[FixedAddressValueType]
	internal unsafe static method* __onexitend_app_domain;

	// Token: 0x04000037 RID: 55
	[FixedAddressValueType]
	internal unsafe static void* ?_lock@AtExitLock@<CrtImplementationDetails>@@$$Q0PEAXEA;

	// Token: 0x04000038 RID: 56
	[FixedAddressValueType]
	internal static int ?_ref_count@AtExitLock@<CrtImplementationDetails>@@$$Q0HA;

	// Token: 0x04000039 RID: 57 RVA: 0x0034FB18 File Offset: 0x0034D118
	internal unsafe static method* __onexitend_m;

	// Token: 0x0400003A RID: 58
	[FixedAddressValueType]
	internal static ulong __exit_list_size_app_domain;

	// Token: 0x0400003B RID: 59
	[FixedAddressValueType]
	internal unsafe static method* __onexitbegin_app_domain;

	// Token: 0x0400003C RID: 60 RVA: 0x001576A4 File Offset: 0x00154EA4
	internal static uint SNI_MAX_COMPOSED_SPN;

	// Token: 0x0400003D RID: 61 RVA: 0x0014CD20 File Offset: 0x0014A520
	internal static _GUID IID_ITransactionLocal;

	// Token: 0x0400003E RID: 62 RVA: 0x0014CD10 File Offset: 0x0014A510
	internal static _GUID IID_IChapteredRowset;

	// Token: 0x0400003F RID: 63 RVA: 0x0014C688 File Offset: 0x00149E88
	internal static $ArrayType$$$BY0A@P6AHXZ __xi_z;

	// Token: 0x04000040 RID: 64 RVA: 0x0034F940 File Offset: 0x0034CF40
	internal static __scrt_native_startup_state __scrt_current_native_startup_state;

	// Token: 0x04000041 RID: 65 RVA: 0x0034F948 File Offset: 0x0034CF48
	internal unsafe static void* __scrt_native_startup_lock;

	// Token: 0x04000042 RID: 66 RVA: 0x0014C660 File Offset: 0x00149E60
	internal static $ArrayType$$$BY0A@P6AXXZ __xc_a;

	// Token: 0x04000043 RID: 67 RVA: 0x0014C678 File Offset: 0x00149E78
	internal static $ArrayType$$$BY0A@P6AHXZ __xi_a;

	// Token: 0x04000044 RID: 68 RVA: 0x0034F010 File Offset: 0x0034C610
	internal static uint __scrt_native_dllmain_reason;

	// Token: 0x04000045 RID: 69 RVA: 0x0014C670 File Offset: 0x00149E70
	internal static $ArrayType$$$BY0A@P6AXXZ __xc_z;
}
