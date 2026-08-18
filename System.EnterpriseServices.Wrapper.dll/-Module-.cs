using System;
using System.Diagnostics;
using System.EnterpriseServices.Thunk;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security;
using System.Threading;
using <CppImplementationDetails>;
using <CrtImplementationDetails>;

// Token: 0x02000001 RID: 1
internal class <Module>
{
	// Token: 0x06000001 RID: 1 RVA: 0x0000111C File Offset: 0x0000051C
	internal unsafe static int IsEqualGUID(_GUID* rguid1, _GUID* rguid2)
	{
		ulong num = 16UL;
		_GUID* ptr = rguid2;
		sbyte b = *rguid1;
		sbyte b2 = *rguid2;
		if (b >= b2)
		{
			long num2 = rguid1 - rguid2;
			while (b <= b2)
			{
				if (num == 1UL)
				{
					return 1;
				}
				num -= 1UL;
				ptr += 1L;
				b = *(num2 + ptr);
				b2 = *ptr;
				if (b < b2)
				{
					break;
				}
			}
		}
		return 0;
	}

	// Token: 0x06000002 RID: 2 RVA: 0x0000116C File Offset: 0x0000056C
	internal unsafe static int ==(_GUID* guidOne, _GUID* guidOther)
	{
		return <Module>.IsEqualGUID(guidOne, guidOther);
	}

	// Token: 0x06000003 RID: 3 RVA: 0x00001DB8 File Offset: 0x000011B8
	internal unsafe static IInitializeSpy* {ctor}(IInitializeSpy* A_0)
	{
		return A_0;
	}

	// Token: 0x06000004 RID: 4 RVA: 0x00006AE8 File Offset: 0x00005EE8
	[return: MarshalAs(UnmanagedType.U1)]
	internal static bool <CrtImplementationDetails>.NativeDll.IsInDllMain()
	{
		return (<Module>.__native_dllmain_reason != uint.MaxValue) ? 1 : 0;
	}

	// Token: 0x06000005 RID: 5 RVA: 0x00006B04 File Offset: 0x00005F04
	[return: MarshalAs(UnmanagedType.U1)]
	internal static bool <CrtImplementationDetails>.NativeDll.IsInProcessAttach()
	{
		return (<Module>.__native_dllmain_reason == 1U) ? 1 : 0;
	}

	// Token: 0x06000006 RID: 6 RVA: 0x00006B1C File Offset: 0x00005F1C
	[return: MarshalAs(UnmanagedType.U1)]
	internal static bool <CrtImplementationDetails>.NativeDll.IsInProcessDetach()
	{
		return (<Module>.__native_dllmain_reason == 0U) ? 1 : 0;
	}

	// Token: 0x06000007 RID: 7 RVA: 0x00006B34 File Offset: 0x00005F34
	[return: MarshalAs(UnmanagedType.U1)]
	internal static bool <CrtImplementationDetails>.NativeDll.IsInVcclrit()
	{
		return (<Module>.__native_vcclrit_reason != uint.MaxValue) ? 1 : 0;
	}

	// Token: 0x06000008 RID: 8 RVA: 0x00006B50 File Offset: 0x00005F50
	[return: MarshalAs(UnmanagedType.U1)]
	internal static bool <CrtImplementationDetails>.NativeDll.IsSafeForManagedCode()
	{
		if (((<Module>.__native_dllmain_reason != 4294967295U) ? 1 : 0) == 0)
		{
			return 1;
		}
		if (((<Module>.__native_vcclrit_reason != 4294967295U) ? 1 : 0) != 0)
		{
			return 1;
		}
		int num;
		if (((<Module>.__native_dllmain_reason == 1U) ? 1 : 0) == 0 && ((<Module>.__native_dllmain_reason == 0U) ? 1 : 0) == 0)
		{
			num = 1;
		}
		else
		{
			num = 0;
		}
		return (byte)num;
	}

	// Token: 0x06000009 RID: 9 RVA: 0x00007308 File Offset: 0x00006708
	internal static void <CrtImplementationDetails>.ThrowNestedModuleLoadException(System.Exception innerException, System.Exception nestedException)
	{
		throw new ModuleLoadExceptionHandlerException("A nested exception occurred after the primary exception that caused the C++ module to fail to load.\n", innerException, nestedException);
	}

	// Token: 0x0600000A RID: 10 RVA: 0x00006D7C File Offset: 0x0000617C
	internal static void <CrtImplementationDetails>.ThrowModuleLoadException(string errorMessage)
	{
		throw new ModuleLoadException(errorMessage);
	}

	// Token: 0x0600000B RID: 11 RVA: 0x00006D90 File Offset: 0x00006190
	internal static void <CrtImplementationDetails>.ThrowModuleLoadException(string errorMessage, System.Exception innerException)
	{
		throw new ModuleLoadException(errorMessage, innerException);
	}

	// Token: 0x0600000C RID: 12 RVA: 0x00006E24 File Offset: 0x00006224
	internal static void <CrtImplementationDetails>.RegisterModuleUninitializer(EventHandler handler)
	{
		ModuleUninitializer._ModuleUninitializer.AddHandler(handler);
	}

	// Token: 0x0600000D RID: 13 RVA: 0x00006E3C File Offset: 0x0000623C
	internal unsafe static int __get_default_appdomain(IUnknown** ppUnk)
	{
		int num = 0;
		IUnknown* ptr = null;
		ICorRuntimeHost* ptr2 = null;
		try
		{
			num = <Module>.CoCreateInstance(ref <Module>._GUID_cb2f6723_ab3a_11d2_9c40_00c04fa30a3e, null, 1, ref <Module>._GUID_00000000_0000_0000_c000_000000000046, (void**)(&ptr));
			if (num >= 0)
			{
				num = calli(System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,_GUID modopt(System.Runtime.CompilerServices.IsConst)* modopt(System.Runtime.CompilerServices.IsImplicitlyDereferenced),System.Void**), ptr, ref <Module>._GUID_cb2f6722_ab3a_11d2_9c40_00c04fa30a3e, ref ptr2, *(*(long*)ptr));
				if (num >= 0)
				{
					num = calli(System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,IUnknown**), ptr2, ppUnk, *(*(long*)ptr2 + 104L));
				}
			}
		}
		finally
		{
			if (ptr != null)
			{
				IUnknown* ptr3 = ptr;
				object obj = calli(System.UInt32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr), ptr3, *(*(long*)ptr3 + 16L));
			}
			if (ptr2 != null)
			{
				ICorRuntimeHost* ptr4 = ptr2;
				object obj2 = calli(System.UInt32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr), ptr4, *(*(long*)ptr4 + 16L));
			}
		}
		return num;
	}

	// Token: 0x0600000E RID: 14 RVA: 0x00006ED0 File Offset: 0x000062D0
	internal unsafe static void __release_appdomain(IUnknown* ppUnk)
	{
		object obj = calli(System.UInt32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr), ppUnk, *(*(long*)ppUnk + 16L));
	}

	// Token: 0x0600000F RID: 15 RVA: 0x00006EEC File Offset: 0x000062EC
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
				IUnknown* ptr2 = ptr;
				object obj = calli(System.UInt32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr), ptr2, *(*(long*)ptr2 + 16L));
			}
		}
		Marshal.ThrowExceptionForHR(num);
		return null;
	}

	// Token: 0x06000010 RID: 16 RVA: 0x00006F54 File Offset: 0x00006354
	internal unsafe static void <CrtImplementationDetails>.DoCallBackInDefaultDomain(method function, void* cookie)
	{
		ICLRRuntimeHost* ptr = null;
		try
		{
			int num = <Module>.CorBindToRuntimeEx(null, null, 0, ref <Module>._GUID_90f1a06e_7712_4762_86b5_7a5eba6bdb02, ref <Module>._GUID_90f1a06c_7712_4762_86b5_7a5eba6bdb02, (void**)(&ptr));
			if (num < 0)
			{
				Marshal.ThrowExceptionForHR(num);
			}
			AppDomain appDomain = <Module>.<CrtImplementationDetails>.GetDefaultDomain();
			long num2 = *(long*)ptr + 64L;
			int num3 = calli(System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,System.UInt32 modopt(System.Runtime.CompilerServices.IsLong),System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl) (System.Void*),System.Void*), ptr, appDomain.Id, function, cookie, *num2);
			if (num3 < 0)
			{
				Marshal.ThrowExceptionForHR(num3);
			}
		}
		finally
		{
			if (ptr != null)
			{
				ICLRRuntimeHost* ptr2 = ptr;
				object obj = calli(System.UInt32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr), ptr2, *(*(long*)ptr2 + 16L));
			}
		}
	}

	// Token: 0x06000011 RID: 17 RVA: 0x0000700C File Offset: 0x0000640C
	internal unsafe static int <CrtImplementationDetails>.DefaultDomain.DoNothing(void* cookie)
	{
		GC.KeepAlive(int.MaxValue);
		return 0;
	}

	// Token: 0x06000012 RID: 18 RVA: 0x0000702C File Offset: 0x0000642C
	[return: MarshalAs(UnmanagedType.U1)]
	internal unsafe static bool <CrtImplementationDetails>.DefaultDomain.HasPerProcess()
	{
		if (<Module>.?hasPerProcess@DefaultDomain@<CrtImplementationDetails>@@0W4State@TriBool@2@A == (TriBool.State)2)
		{
			void** ptr = (void**)(&<Module>.?A0x2d87f2c9.__xc_mp_a);
			if (ref <Module>.?A0x2d87f2c9.__xc_mp_a < ref <Module>.?A0x2d87f2c9.__xc_mp_z)
			{
				while (*(long*)ptr == 0L)
				{
					ptr += 8L / (long)sizeof(void*);
					if (ptr >= (void**)(&<Module>.?A0x2d87f2c9.__xc_mp_z))
					{
						goto IL_35;
					}
				}
				<Module>.?hasPerProcess@DefaultDomain@<CrtImplementationDetails>@@0W4State@TriBool@2@A = (TriBool.State)(-1);
				return 1;
			}
			IL_35:
			<Module>.?hasPerProcess@DefaultDomain@<CrtImplementationDetails>@@0W4State@TriBool@2@A = (TriBool.State)0;
			return 0;
		}
		return (<Module>.?hasPerProcess@DefaultDomain@<CrtImplementationDetails>@@0W4State@TriBool@2@A == (TriBool.State)(-1)) ? 1 : 0;
	}

	// Token: 0x06000013 RID: 19 RVA: 0x00007080 File Offset: 0x00006480
	[return: MarshalAs(UnmanagedType.U1)]
	internal unsafe static bool <CrtImplementationDetails>.DefaultDomain.HasNative()
	{
		if (<Module>.?hasNative@DefaultDomain@<CrtImplementationDetails>@@0W4State@TriBool@2@A == (TriBool.State)2)
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
				<Module>.?hasNative@DefaultDomain@<CrtImplementationDetails>@@0W4State@TriBool@2@A = (TriBool.State)(-1);
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
				<Module>.?hasNative@DefaultDomain@<CrtImplementationDetails>@@0W4State@TriBool@2@A = (TriBool.State)(-1);
				return 1;
			}
			IL_62:
			<Module>.?hasNative@DefaultDomain@<CrtImplementationDetails>@@0W4State@TriBool@2@A = (TriBool.State)0;
			return 0;
		}
		return (<Module>.?hasNative@DefaultDomain@<CrtImplementationDetails>@@0W4State@TriBool@2@A == (TriBool.State)(-1)) ? 1 : 0;
	}

	// Token: 0x06000014 RID: 20 RVA: 0x00007100 File Offset: 0x00006500
	[return: MarshalAs(UnmanagedType.U1)]
	internal static bool <CrtImplementationDetails>.DefaultDomain.NeedsInitialization()
	{
		int num;
		if ((<Module>.<CrtImplementationDetails>.DefaultDomain.HasPerProcess() != null && !<Module>.?InitializedPerProcess@DefaultDomain@<CrtImplementationDetails>@@2_NA) || (<Module>.<CrtImplementationDetails>.DefaultDomain.HasNative() != null && !<Module>.?InitializedNative@DefaultDomain@<CrtImplementationDetails>@@2_NA && <Module>.__native_startup_state == (__enative_startup_state)0))
		{
			num = 1;
		}
		else
		{
			num = 0;
		}
		return (byte)num;
	}

	// Token: 0x06000015 RID: 21 RVA: 0x0000713C File Offset: 0x0000653C
	[return: MarshalAs(UnmanagedType.U1)]
	internal static bool <CrtImplementationDetails>.DefaultDomain.NeedsUninitialization()
	{
		return <Module>.?Entered@DefaultDomain@<CrtImplementationDetails>@@2_NA;
	}

	// Token: 0x06000016 RID: 22 RVA: 0x00007150 File Offset: 0x00006550
	internal static void <CrtImplementationDetails>.DefaultDomain.Initialize()
	{
		<Module>.<CrtImplementationDetails>.DoCallBackInDefaultDomain(<Module>.__unep@?DoNothing@DefaultDomain@<CrtImplementationDetails>@@$$FCAJPEAX@Z, null);
	}

	// Token: 0x06000017 RID: 23 RVA: 0x00009850 File Offset: 0x00008C50
	internal static void ??__E?Initialized@CurrentDomain@<CrtImplementationDetails>@@$$Q2HA@@YMXXZ()
	{
		<Module>.?Initialized@CurrentDomain@<CrtImplementationDetails>@@$$Q2HA = 0;
	}

	// Token: 0x06000018 RID: 24 RVA: 0x00009864 File Offset: 0x00008C64
	internal static void ??__E?Uninitialized@CurrentDomain@<CrtImplementationDetails>@@$$Q2HA@@YMXXZ()
	{
		<Module>.?Uninitialized@CurrentDomain@<CrtImplementationDetails>@@$$Q2HA = 0;
	}

	// Token: 0x06000019 RID: 25 RVA: 0x00009878 File Offset: 0x00008C78
	internal static void ??__E?IsDefaultDomain@CurrentDomain@<CrtImplementationDetails>@@$$Q2_NA@@YMXXZ()
	{
		<Module>.?IsDefaultDomain@CurrentDomain@<CrtImplementationDetails>@@$$Q2_NA = false;
	}

	// Token: 0x0600001A RID: 26 RVA: 0x0000988C File Offset: 0x00008C8C
	internal static void ??__E?InitializedVtables@CurrentDomain@<CrtImplementationDetails>@@$$Q2W4State@Progress@2@A@@YMXXZ()
	{
		<Module>.?InitializedVtables@CurrentDomain@<CrtImplementationDetails>@@$$Q2W4State@Progress@2@A = (Progress.State)0;
	}

	// Token: 0x0600001B RID: 27 RVA: 0x000098A0 File Offset: 0x00008CA0
	internal static void ??__E?InitializedNative@CurrentDomain@<CrtImplementationDetails>@@$$Q2W4State@Progress@2@A@@YMXXZ()
	{
		<Module>.?InitializedNative@CurrentDomain@<CrtImplementationDetails>@@$$Q2W4State@Progress@2@A = (Progress.State)0;
	}

	// Token: 0x0600001C RID: 28 RVA: 0x000098B4 File Offset: 0x00008CB4
	internal static void ??__E?InitializedPerProcess@CurrentDomain@<CrtImplementationDetails>@@$$Q2W4State@Progress@2@A@@YMXXZ()
	{
		<Module>.?InitializedPerProcess@CurrentDomain@<CrtImplementationDetails>@@$$Q2W4State@Progress@2@A = (Progress.State)0;
	}

	// Token: 0x0600001D RID: 29 RVA: 0x000098C8 File Offset: 0x00008CC8
	internal static void ??__E?InitializedPerAppDomain@CurrentDomain@<CrtImplementationDetails>@@$$Q2W4State@Progress@2@A@@YMXXZ()
	{
		<Module>.?InitializedPerAppDomain@CurrentDomain@<CrtImplementationDetails>@@$$Q2W4State@Progress@2@A = (Progress.State)0;
	}

	// Token: 0x0600001E RID: 30 RVA: 0x00007380 File Offset: 0x00006780
	[DebuggerStepThrough]
	internal unsafe static void <CrtImplementationDetails>.LanguageSupport.InitializeVtables(LanguageSupport* A_0)
	{
		string target = "The C++ module failed to load during vtable initialization.\n";
		IntPtr value = new IntPtr(*A_0);
		((GCHandle)value).Target = target;
		<Module>.?InitializedVtables@CurrentDomain@<CrtImplementationDetails>@@$$Q2W4State@Progress@2@A = (Progress.State)1;
		<Module>._initterm_m((method*)(&<Module>.?A0x2d87f2c9.__xi_vt_a), (method*)(&<Module>.?A0x2d87f2c9.__xi_vt_z));
		<Module>.?InitializedVtables@CurrentDomain@<CrtImplementationDetails>@@$$Q2W4State@Progress@2@A = (Progress.State)2;
	}

	// Token: 0x0600001F RID: 31 RVA: 0x000073C8 File Offset: 0x000067C8
	internal unsafe static void <CrtImplementationDetails>.LanguageSupport.InitializeDefaultAppDomain(LanguageSupport* A_0)
	{
		string target = "The C++ module failed to load while attempting to initialize the default appdomain.\n";
		IntPtr value = new IntPtr(*A_0);
		((GCHandle)value).Target = target;
		<Module>.<CrtImplementationDetails>.DoCallBackInDefaultDomain(<Module>.__unep@?DoNothing@DefaultDomain@<CrtImplementationDetails>@@$$FCAJPEAX@Z, null);
	}

	// Token: 0x06000020 RID: 32 RVA: 0x00007400 File Offset: 0x00006800
	[DebuggerStepThrough]
	internal unsafe static void <CrtImplementationDetails>.LanguageSupport.InitializeNative(LanguageSupport* A_0)
	{
		string target = "The C++ module failed to load during native initialization.\n";
		IntPtr value = new IntPtr(*A_0);
		((GCHandle)value).Target = target;
		<Module>.__security_init_cookie();
		<Module>.?InitializedNative@DefaultDomain@<CrtImplementationDetails>@@2_NA = true;
		if (<Module>.<CrtImplementationDetails>.NativeDll.IsSafeForManagedCode() == null)
		{
			<Module>._amsg_exit(33);
		}
		if (<Module>.__native_startup_state == (__enative_startup_state)1)
		{
			<Module>._amsg_exit(33);
		}
		else if (<Module>.__native_startup_state == (__enative_startup_state)0)
		{
			<Module>.?InitializedNative@CurrentDomain@<CrtImplementationDetails>@@$$Q2W4State@Progress@2@A = (Progress.State)1;
			<Module>.__native_startup_state = (__enative_startup_state)1;
			if (<Module>._initterm_e((method*)(&<Module>.__xi_a), (method*)(&<Module>.__xi_z)) != 0)
			{
				IntPtr value2 = new IntPtr(*A_0);
				throw new ModuleLoadException(((GCHandle)value2).Target);
			}
			<Module>._initterm((method*)(&<Module>.__xc_a), (method*)(&<Module>.__xc_z));
			<Module>.__native_startup_state = (__enative_startup_state)2;
			<Module>.?InitializedNativeFromCCTOR@DefaultDomain@<CrtImplementationDetails>@@2_NA = true;
			<Module>.?InitializedNative@CurrentDomain@<CrtImplementationDetails>@@$$Q2W4State@Progress@2@A = (Progress.State)2;
		}
	}

	// Token: 0x06000021 RID: 33 RVA: 0x000074C0 File Offset: 0x000068C0
	[DebuggerStepThrough]
	internal unsafe static void <CrtImplementationDetails>.LanguageSupport.InitializePerProcess(LanguageSupport* A_0)
	{
		string target = "The C++ module failed to load during process initialization.\n";
		IntPtr value = new IntPtr(*A_0);
		((GCHandle)value).Target = target;
		<Module>.?InitializedPerProcess@CurrentDomain@<CrtImplementationDetails>@@$$Q2W4State@Progress@2@A = (Progress.State)1;
		<Module>._initatexit_m();
		<Module>._initterm_m((method*)(&<Module>.?A0x2d87f2c9.__xc_mp_a), (method*)(&<Module>.?A0x2d87f2c9.__xc_mp_z));
		<Module>.?InitializedPerProcess@CurrentDomain@<CrtImplementationDetails>@@$$Q2W4State@Progress@2@A = (Progress.State)2;
		<Module>.?InitializedPerProcess@DefaultDomain@<CrtImplementationDetails>@@2_NA = true;
	}

	// Token: 0x06000022 RID: 34 RVA: 0x00007514 File Offset: 0x00006914
	[DebuggerStepThrough]
	internal unsafe static void <CrtImplementationDetails>.LanguageSupport.InitializePerAppDomain(LanguageSupport* A_0)
	{
		string target = "The C++ module failed to load during appdomain initialization.\n";
		IntPtr value = new IntPtr(*A_0);
		((GCHandle)value).Target = target;
		<Module>.?InitializedPerAppDomain@CurrentDomain@<CrtImplementationDetails>@@$$Q2W4State@Progress@2@A = (Progress.State)1;
		<Module>._initatexit_app_domain();
		<Module>._initterm_m((method*)(&<Module>.?A0x2d87f2c9.__xc_ma_a), (method*)(&<Module>.?A0x2d87f2c9.__xc_ma_z));
		<Module>.?InitializedPerAppDomain@CurrentDomain@<CrtImplementationDetails>@@$$Q2W4State@Progress@2@A = (Progress.State)2;
	}

	// Token: 0x06000023 RID: 35 RVA: 0x00007560 File Offset: 0x00006960
	[DebuggerStepThrough]
	internal unsafe static void <CrtImplementationDetails>.LanguageSupport.InitializeUninitializer(LanguageSupport* A_0)
	{
		string target = "The C++ module failed to load during registration for the unload events.\n";
		IntPtr value = new IntPtr(*A_0);
		((GCHandle)value).Target = target;
		EventHandler handler = new EventHandler(<Module>.<CrtImplementationDetails>.LanguageSupport.DomainUnload);
		ModuleUninitializer._ModuleUninitializer.AddHandler(handler);
	}

	// Token: 0x06000024 RID: 36 RVA: 0x000075A4 File Offset: 0x000069A4
	[DebuggerStepThrough]
	[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
	internal unsafe static void <CrtImplementationDetails>.LanguageSupport._Initialize(LanguageSupport* A_0)
	{
		<Module>.?IsDefaultDomain@CurrentDomain@<CrtImplementationDetails>@@$$Q2_NA = AppDomain.CurrentDomain.IsDefaultAppDomain();
		<Module>.?Entered@DefaultDomain@<CrtImplementationDetails>@@2_NA = (<Module>.?IsDefaultDomain@CurrentDomain@<CrtImplementationDetails>@@$$Q2_NA || <Module>.?Entered@DefaultDomain@<CrtImplementationDetails>@@2_NA);
		void* ptr = <Module>._getFiberPtrId();
		int num = 0;
		int num2 = 0;
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
					void* ptr2 = Interlocked.CompareExchange(ref <Module>.__native_startup_lock, ptr, 0L);
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
			if (!<Module>.?IsDefaultDomain@CurrentDomain@<CrtImplementationDetails>@@$$Q2_NA && <Module>.<CrtImplementationDetails>.DefaultDomain.NeedsInitialization() != null)
			{
				<Module>.<CrtImplementationDetails>.LanguageSupport.InitializeDefaultAppDomain(A_0);
			}
		}
		finally
		{
			if (num == 0)
			{
				Interlocked.Exchange(ref <Module>.__native_startup_lock, 0L);
			}
		}
		string target = "The C++ module failed to load during vtable initialization.\n";
		IntPtr value = new IntPtr(*A_0);
		((GCHandle)value).Target = target;
		<Module>.?InitializedVtables@CurrentDomain@<CrtImplementationDetails>@@$$Q2W4State@Progress@2@A = (Progress.State)1;
		<Module>._initterm_m((method*)(&<Module>.?A0x2d87f2c9.__xi_vt_a), (method*)(&<Module>.?A0x2d87f2c9.__xi_vt_z));
		<Module>.?InitializedVtables@CurrentDomain@<CrtImplementationDetails>@@$$Q2W4State@Progress@2@A = (Progress.State)2;
		if (<Module>.?IsDefaultDomain@CurrentDomain@<CrtImplementationDetails>@@$$Q2_NA)
		{
			<Module>.<CrtImplementationDetails>.LanguageSupport.InitializeNative(A_0);
			string target2 = "The C++ module failed to load during process initialization.\n";
			IntPtr value2 = new IntPtr(*A_0);
			((GCHandle)value2).Target = target2;
			<Module>.?InitializedPerProcess@CurrentDomain@<CrtImplementationDetails>@@$$Q2W4State@Progress@2@A = (Progress.State)1;
			<Module>._initatexit_m();
			<Module>._initterm_m((method*)(&<Module>.?A0x2d87f2c9.__xc_mp_a), (method*)(&<Module>.?A0x2d87f2c9.__xc_mp_z));
			<Module>.?InitializedPerProcess@CurrentDomain@<CrtImplementationDetails>@@$$Q2W4State@Progress@2@A = (Progress.State)2;
			<Module>.?InitializedPerProcess@DefaultDomain@<CrtImplementationDetails>@@2_NA = true;
		}
		string target3 = "The C++ module failed to load during appdomain initialization.\n";
		IntPtr value3 = new IntPtr(*A_0);
		((GCHandle)value3).Target = target3;
		<Module>.?InitializedPerAppDomain@CurrentDomain@<CrtImplementationDetails>@@$$Q2W4State@Progress@2@A = (Progress.State)1;
		<Module>._initatexit_app_domain();
		<Module>._initterm_m((method*)(&<Module>.?A0x2d87f2c9.__xc_ma_a), (method*)(&<Module>.?A0x2d87f2c9.__xc_ma_z));
		<Module>.?InitializedPerAppDomain@CurrentDomain@<CrtImplementationDetails>@@$$Q2W4State@Progress@2@A = (Progress.State)2;
		<Module>.?Initialized@CurrentDomain@<CrtImplementationDetails>@@$$Q2HA = 1;
		string target4 = "The C++ module failed to load during registration for the unload events.\n";
		IntPtr value4 = new IntPtr(*A_0);
		((GCHandle)value4).Target = target4;
		EventHandler handler = new EventHandler(<Module>.<CrtImplementationDetails>.LanguageSupport.DomainUnload);
		ModuleUninitializer._ModuleUninitializer.AddHandler(handler);
	}

	// Token: 0x06000025 RID: 37 RVA: 0x0000716C File Offset: 0x0000656C
	internal static void <CrtImplementationDetails>.LanguageSupport.UninitializeAppDomain()
	{
		<Module>._app_exit_callback();
	}

	// Token: 0x06000026 RID: 38 RVA: 0x00007180 File Offset: 0x00006580
	internal unsafe static int <CrtImplementationDetails>.LanguageSupport._UninitializeDefaultDomain(void* cookie)
	{
		<Module>._exit_callback();
		<Module>.?InitializedPerProcess@DefaultDomain@<CrtImplementationDetails>@@2_NA = false;
		if (<Module>.?InitializedNativeFromCCTOR@DefaultDomain@<CrtImplementationDetails>@@2_NA)
		{
			<Module>._cexit();
			<Module>.__native_startup_state = (__enative_startup_state)0;
			<Module>.?InitializedNativeFromCCTOR@DefaultDomain@<CrtImplementationDetails>@@2_NA = false;
		}
		<Module>.?InitializedNative@DefaultDomain@<CrtImplementationDetails>@@2_NA = false;
		return 0;
	}

	// Token: 0x06000027 RID: 39 RVA: 0x000071BC File Offset: 0x000065BC
	internal static void <CrtImplementationDetails>.LanguageSupport.UninitializeDefaultDomain()
	{
		if (<Module>.?Entered@DefaultDomain@<CrtImplementationDetails>@@2_NA)
		{
			if (AppDomain.CurrentDomain.IsDefaultAppDomain())
			{
				<Module>._exit_callback();
				<Module>.?InitializedPerProcess@DefaultDomain@<CrtImplementationDetails>@@2_NA = false;
				if (<Module>.?InitializedNativeFromCCTOR@DefaultDomain@<CrtImplementationDetails>@@2_NA)
				{
					<Module>._cexit();
					<Module>.__native_startup_state = (__enative_startup_state)0;
					<Module>.?InitializedNativeFromCCTOR@DefaultDomain@<CrtImplementationDetails>@@2_NA = false;
				}
				<Module>.?InitializedNative@DefaultDomain@<CrtImplementationDetails>@@2_NA = false;
			}
			else
			{
				<Module>.<CrtImplementationDetails>.DoCallBackInDefaultDomain(<Module>.__unep@?_UninitializeDefaultDomain@LanguageSupport@<CrtImplementationDetails>@@$$FCAJPEAX@Z, null);
			}
		}
	}

	// Token: 0x06000028 RID: 40 RVA: 0x00007218 File Offset: 0x00006618
	[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
	[PrePrepareMethod]
	internal static void <CrtImplementationDetails>.LanguageSupport.DomainUnload(object source, EventArgs arguments)
	{
		if (<Module>.?Initialized@CurrentDomain@<CrtImplementationDetails>@@$$Q2HA != 0 && Interlocked.Exchange(ref <Module>.?Uninitialized@CurrentDomain@<CrtImplementationDetails>@@$$Q2HA, 1) == 0)
		{
			byte b = (Interlocked.Decrement(ref <Module>.?Count@AllDomains@<CrtImplementationDetails>@@2HA) == 0) ? 1 : 0;
			<Module>._app_exit_callback();
			if (b != 0)
			{
				<Module>.<CrtImplementationDetails>.LanguageSupport.UninitializeDefaultDomain();
			}
		}
	}

	// Token: 0x06000029 RID: 41 RVA: 0x0000778C File Offset: 0x00006B8C
	[DebuggerStepThrough]
	[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
	internal unsafe static void <CrtImplementationDetails>.LanguageSupport.Cleanup(LanguageSupport* A_0, System.Exception innerException)
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
		catch (System.Exception nestedException)
		{
			throw new ModuleLoadExceptionHandlerException("A nested exception occurred after the primary exception that caused the C++ module to fail to load.\n", innerException, nestedException);
		}
		catch (object obj)
		{
			throw new ModuleLoadExceptionHandlerException("A nested exception occurred after the primary exception that caused the C++ module to fail to load.\n", innerException, null);
		}
	}

	// Token: 0x0600002A RID: 42 RVA: 0x0000780C File Offset: 0x00006C0C
	[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
	[DebuggerStepThrough]
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
		catch (System.Exception innerException)
		{
			if (flag)
			{
				<Module>.<CrtImplementationDetails>.LanguageSupport.Cleanup(A_0, innerException);
			}
			throw new ModuleLoadException(<Module>.gcroot<System::String\u0020^>..PE$AAVString@System@@(A_0), innerException);
		}
		catch (object obj)
		{
			if (flag)
			{
				<Module>.<CrtImplementationDetails>.LanguageSupport.Cleanup(A_0, null);
			}
			throw new ModuleLoadException(<Module>.gcroot<System::String\u0020^>..PE$AAVString@System@@(A_0), null);
		}
	}

	// Token: 0x0600002B RID: 43 RVA: 0x00007920 File Offset: 0x00006D20
	[DebuggerStepThrough]
	static unsafe <Module>()
	{
		LanguageSupport value = ((IntPtr)GCHandle.Alloc(null)).ToPointer();
		try
		{
			<Module>.<CrtImplementationDetails>.LanguageSupport.Initialize(ref value);
		}
		catch
		{
			<Module>.___CxxCallUnwindDtor(ldftn(<CrtImplementationDetails>.LanguageSupport.{dtor}), (void*)(&value));
			throw;
		}
		IntPtr value2 = new IntPtr(value);
		((GCHandle)value2).Free();
	}

	// Token: 0x0600002C RID: 44 RVA: 0x000078CC File Offset: 0x00006CCC
	internal unsafe static LanguageSupport* <CrtImplementationDetails>.LanguageSupport.{ctor}(LanguageSupport* A_0)
	{
		*A_0 = ((IntPtr)GCHandle.Alloc(null)).ToPointer();
		return A_0;
	}

	// Token: 0x0600002D RID: 45 RVA: 0x000078F4 File Offset: 0x00006CF4
	internal unsafe static void <CrtImplementationDetails>.LanguageSupport.{dtor}(LanguageSupport* A_0)
	{
		IntPtr value = new IntPtr(*A_0);
		((GCHandle)value).Free();
		*A_0 = 0L;
	}

	// Token: 0x0600002E RID: 46 RVA: 0x00007254 File Offset: 0x00006654
	[DebuggerStepThrough]
	internal unsafe static gcroot<System::String\u0020^>* {ctor}(gcroot<System::String\u0020^>* A_0)
	{
		*A_0 = ((IntPtr)GCHandle.Alloc(null)).ToPointer();
		return A_0;
	}

	// Token: 0x0600002F RID: 47 RVA: 0x00007278 File Offset: 0x00006678
	[DebuggerStepThrough]
	internal unsafe static void {dtor}(gcroot<System::String\u0020^>* A_0)
	{
		IntPtr value = new IntPtr(*A_0);
		((GCHandle)value).Free();
		*A_0 = 0L;
	}

	// Token: 0x06000030 RID: 48 RVA: 0x000072A0 File Offset: 0x000066A0
	[DebuggerStepThrough]
	internal unsafe static gcroot<System::String\u0020^>* =(gcroot<System::String\u0020^>* A_0, string t)
	{
		IntPtr value = new IntPtr(*A_0);
		((GCHandle)value).Target = t;
		return A_0;
	}

	// Token: 0x06000031 RID: 49 RVA: 0x000072C8 File Offset: 0x000066C8
	internal unsafe static string PE$AAVString@System@@(gcroot<System::String\u0020^>* A_0)
	{
		IntPtr value = new IntPtr(*A_0);
		return ((GCHandle)value).Target;
	}

	// Token: 0x06000032 RID: 50 RVA: 0x00007990 File Offset: 0x00006D90
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

	// Token: 0x06000033 RID: 51 RVA: 0x00007F38 File Offset: 0x00007338
	[DebuggerStepThrough]
	internal static void <CrtImplementationDetails>.AtExitLock._lock_Construct(object value)
	{
		<Module>.?_lock@AtExitLock@<CrtImplementationDetails>@@$$Q0PEAXEA = null;
		<Module>.<CrtImplementationDetails>.AtExitLock._lock_Set(value);
	}

	// Token: 0x06000034 RID: 52 RVA: 0x000079C0 File Offset: 0x00006DC0
	[DebuggerStepThrough]
	internal static void <CrtImplementationDetails>.AtExitLock._lock_Set(object value)
	{
		ValueType valueType;
		if (<Module>.?_lock@AtExitLock@<CrtImplementationDetails>@@$$Q0PEAXEA != null)
		{
			IntPtr value2 = new IntPtr(<Module>.?_lock@AtExitLock@<CrtImplementationDetails>@@$$Q0PEAXEA);
			valueType = GCHandle.FromIntPtr(value2);
		}
		else
		{
			valueType = null;
		}
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

	// Token: 0x06000035 RID: 53 RVA: 0x00007A2C File Offset: 0x00006E2C
	[DebuggerStepThrough]
	internal static object <CrtImplementationDetails>.AtExitLock._lock_Get()
	{
		ValueType valueType;
		if (<Module>.?_lock@AtExitLock@<CrtImplementationDetails>@@$$Q0PEAXEA != null)
		{
			IntPtr value = new IntPtr(<Module>.?_lock@AtExitLock@<CrtImplementationDetails>@@$$Q0PEAXEA);
			valueType = GCHandle.FromIntPtr(value);
		}
		else
		{
			valueType = null;
		}
		if (valueType != null)
		{
			return ((GCHandle)valueType).Target;
		}
		return null;
	}

	// Token: 0x06000036 RID: 54 RVA: 0x00007A6C File Offset: 0x00006E6C
	[DebuggerStepThrough]
	internal static void <CrtImplementationDetails>.AtExitLock._lock_Destruct()
	{
		ValueType valueType;
		if (<Module>.?_lock@AtExitLock@<CrtImplementationDetails>@@$$Q0PEAXEA != null)
		{
			IntPtr value = new IntPtr(<Module>.?_lock@AtExitLock@<CrtImplementationDetails>@@$$Q0PEAXEA);
			valueType = GCHandle.FromIntPtr(value);
		}
		else
		{
			valueType = null;
		}
		if (valueType != null)
		{
			((GCHandle)valueType).Free();
			<Module>.?_lock@AtExitLock@<CrtImplementationDetails>@@$$Q0PEAXEA = null;
		}
	}

	// Token: 0x06000037 RID: 55 RVA: 0x00007AB4 File Offset: 0x00006EB4
	[DebuggerStepThrough]
	[return: MarshalAs(UnmanagedType.U1)]
	internal static bool <CrtImplementationDetails>.AtExitLock.IsInitialized()
	{
		return (<Module>.<CrtImplementationDetails>.AtExitLock._lock_Get() != null) ? 1 : 0;
	}

	// Token: 0x06000038 RID: 56 RVA: 0x00007F54 File Offset: 0x00007354
	[DebuggerStepThrough]
	internal static void <CrtImplementationDetails>.AtExitLock.AddRef()
	{
		if (((<Module>.<CrtImplementationDetails>.AtExitLock._lock_Get() != null) ? 1 : 0) == 0)
		{
			object value = new object();
			<Module>.?_lock@AtExitLock@<CrtImplementationDetails>@@$$Q0PEAXEA = null;
			<Module>.<CrtImplementationDetails>.AtExitLock._lock_Set(value);
			<Module>.?_ref_count@AtExitLock@<CrtImplementationDetails>@@$$Q0HA = 0;
		}
		<Module>.?_ref_count@AtExitLock@<CrtImplementationDetails>@@$$Q0HA++;
	}

	// Token: 0x06000039 RID: 57 RVA: 0x00007AD0 File Offset: 0x00006ED0
	[DebuggerStepThrough]
	internal static void <CrtImplementationDetails>.AtExitLock.RemoveRef()
	{
		<Module>.?_ref_count@AtExitLock@<CrtImplementationDetails>@@$$Q0HA += -1;
		if (<Module>.?_ref_count@AtExitLock@<CrtImplementationDetails>@@$$Q0HA == 0)
		{
			<Module>.<CrtImplementationDetails>.AtExitLock._lock_Destruct();
		}
	}

	// Token: 0x0600003A RID: 58 RVA: 0x00007AF8 File Offset: 0x00006EF8
	[DebuggerStepThrough]
	internal static void <CrtImplementationDetails>.AtExitLock.Enter()
	{
		Monitor.Enter(<Module>.<CrtImplementationDetails>.AtExitLock._lock_Get());
	}

	// Token: 0x0600003B RID: 59 RVA: 0x00007B10 File Offset: 0x00006F10
	[DebuggerStepThrough]
	internal static void <CrtImplementationDetails>.AtExitLock.Exit()
	{
		Monitor.Exit(<Module>.<CrtImplementationDetails>.AtExitLock._lock_Get());
	}

	// Token: 0x0600003C RID: 60 RVA: 0x00007B28 File Offset: 0x00006F28
	[DebuggerStepThrough]
	[return: MarshalAs(UnmanagedType.U1)]
	internal static bool __global_lock()
	{
		bool result = false;
		if (((<Module>.<CrtImplementationDetails>.AtExitLock._lock_Get() != null) ? 1 : 0) != 0)
		{
			Monitor.Enter(<Module>.<CrtImplementationDetails>.AtExitLock._lock_Get());
			result = true;
		}
		return result;
	}

	// Token: 0x0600003D RID: 61 RVA: 0x00007B54 File Offset: 0x00006F54
	[DebuggerStepThrough]
	[return: MarshalAs(UnmanagedType.U1)]
	internal static bool __global_unlock()
	{
		bool result = false;
		if (((<Module>.<CrtImplementationDetails>.AtExitLock._lock_Get() != null) ? 1 : 0) != 0)
		{
			Monitor.Exit(<Module>.<CrtImplementationDetails>.AtExitLock._lock_Get());
			result = true;
		}
		return result;
	}

	// Token: 0x0600003E RID: 62 RVA: 0x00007F94 File Offset: 0x00007394
	[DebuggerStepThrough]
	[return: MarshalAs(UnmanagedType.U1)]
	internal static bool __alloc_global_lock()
	{
		if (((<Module>.<CrtImplementationDetails>.AtExitLock._lock_Get() != null) ? 1 : 0) == 0)
		{
			object value = new object();
			<Module>.?_lock@AtExitLock@<CrtImplementationDetails>@@$$Q0PEAXEA = null;
			<Module>.<CrtImplementationDetails>.AtExitLock._lock_Set(value);
			<Module>.?_ref_count@AtExitLock@<CrtImplementationDetails>@@$$Q0HA = 0;
		}
		<Module>.?_ref_count@AtExitLock@<CrtImplementationDetails>@@$$Q0HA++;
		return (<Module>.<CrtImplementationDetails>.AtExitLock._lock_Get() != null) ? 1 : 0;
	}

	// Token: 0x0600003F RID: 63 RVA: 0x00007B80 File Offset: 0x00006F80
	[DebuggerStepThrough]
	internal static void __dealloc_global_lock()
	{
		<Module>.?_ref_count@AtExitLock@<CrtImplementationDetails>@@$$Q0HA += -1;
		if (<Module>.?_ref_count@AtExitLock@<CrtImplementationDetails>@@$$Q0HA == 0)
		{
			<Module>.<CrtImplementationDetails>.AtExitLock._lock_Destruct();
		}
	}

	// Token: 0x06000040 RID: 64 RVA: 0x00007BA8 File Offset: 0x00006FA8
	internal unsafe static int _atexit_helper(method func, ulong* __pexit_list_size, method** __ponexitend, method** __ponexitbegin)
	{
		method system.Void_u0020() = 0L;
		if (func == null)
		{
			return -1;
		}
		if (((<Module>.<CrtImplementationDetails>.AtExitLock._lock_Get() != null) ? 1 : 0) != 0)
		{
			Monitor.Enter(<Module>.<CrtImplementationDetails>.AtExitLock._lock_Get());
			try
			{
				if (*__pexit_list_size - 1UL < (ulong)(*(long*)__ponexitend - *(long*)__ponexitbegin) >> 3)
				{
					try
					{
						ulong num = *__pexit_list_size * 8UL;
						ulong num2 = (num < 4096UL) ? num : 4096UL;
						IntPtr cb = new IntPtr((int)(num + num2));
						IntPtr pv = new IntPtr(*(long*)__ponexitbegin);
						IntPtr intPtr = Marshal.ReAllocHGlobal(pv, cb);
						*(long*)__ponexitend = *(long*)__ponexitend + (byte*)((byte*)intPtr.ToPointer() - *(long*)__ponexitbegin);
						*(long*)__ponexitbegin = intPtr.ToPointer();
						ulong num3 = *__pexit_list_size;
						ulong num4 = (512UL < num3) ? 512UL : num3;
						*__pexit_list_size = num3 + num4;
					}
					catch (OutOfMemoryException)
					{
						IntPtr cb2 = new IntPtr((int)(*__pexit_list_size * 8UL + 12UL));
						IntPtr pv2 = new IntPtr(*(long*)__ponexitbegin);
						IntPtr intPtr2 = Marshal.ReAllocHGlobal(pv2, cb2);
						*(long*)__ponexitend = *(long*)__ponexitend + (byte*)((byte*)intPtr2.ToPointer() - *(long*)__ponexitbegin);
						*(long*)__ponexitbegin = intPtr2.ToPointer();
						*__pexit_list_size += 4UL;
					}
				}
				*(*(long*)__ponexitend) = func;
				*(long*)__ponexitend = *(long*)__ponexitend + 8L;
				system.Void_u0020() = func;
			}
			catch (OutOfMemoryException)
			{
			}
			finally
			{
				if (((<Module>.<CrtImplementationDetails>.AtExitLock._lock_Get() != null) ? 1 : 0) != 0)
				{
					Monitor.Exit(<Module>.<CrtImplementationDetails>.AtExitLock._lock_Get());
				}
			}
			if (system.Void_u0020() != null)
			{
				return 0;
			}
		}
		return -1;
	}

	// Token: 0x06000041 RID: 65 RVA: 0x00007D28 File Offset: 0x00007128
	internal unsafe static void _exit_callback()
	{
		if (<Module>.?A0x311fdb2b.__exit_list_size != 0UL)
		{
			method* ptr = <Module>._decode_pointer((void*)<Module>.?A0x311fdb2b.__onexitbegin_m);
			method* ptr2 = <Module>._decode_pointer((void*)<Module>.?A0x311fdb2b.__onexitend_m);
			if (ptr != -1L && ptr != null && ptr2 != null)
			{
				for (;;)
				{
					ptr2 -= 8L / (long)sizeof(method);
					if (ptr2 < ptr)
					{
						break;
					}
					if (*(long*)ptr2 != <Module>._encoded_null())
					{
						void* ptr3 = <Module>._decode_pointer(*(long*)ptr2);
						*(long*)ptr2 = <Module>._encoded_null();
						calli(System.Void(), ptr3);
						ptr = <Module>._decode_pointer((void*)<Module>.?A0x311fdb2b.__onexitbegin_m);
						ptr2 = <Module>._decode_pointer((void*)<Module>.?A0x311fdb2b.__onexitend_m);
					}
				}
				IntPtr hglobal = new IntPtr((void*)ptr);
				Marshal.FreeHGlobal(hglobal);
			}
			<Module>.?_ref_count@AtExitLock@<CrtImplementationDetails>@@$$Q0HA += -1;
			if (<Module>.?_ref_count@AtExitLock@<CrtImplementationDetails>@@$$Q0HA == 0)
			{
				<Module>.<CrtImplementationDetails>.AtExitLock._lock_Destruct();
			}
		}
	}

	// Token: 0x06000042 RID: 66 RVA: 0x00007FE0 File Offset: 0x000073E0
	[DebuggerStepThrough]
	internal static int _initatexit_m()
	{
		int result = 0;
		if (((<Module>.<CrtImplementationDetails>.AtExitLock._lock_Get() != null) ? 1 : 0) == 0)
		{
			object value = new object();
			<Module>.?_lock@AtExitLock@<CrtImplementationDetails>@@$$Q0PEAXEA = null;
			<Module>.<CrtImplementationDetails>.AtExitLock._lock_Set(value);
			<Module>.?_ref_count@AtExitLock@<CrtImplementationDetails>@@$$Q0HA = 0;
		}
		<Module>.?_ref_count@AtExitLock@<CrtImplementationDetails>@@$$Q0HA++;
		if (((<Module>.<CrtImplementationDetails>.AtExitLock._lock_Get() != null) ? 1 : 0) == 1)
		{
			<Module>.?A0x311fdb2b.__onexitbegin_m = <Module>._encode_pointer(Marshal.AllocHGlobal(256).ToPointer());
			<Module>.?A0x311fdb2b.__onexitend_m = <Module>.?A0x311fdb2b.__onexitbegin_m;
			<Module>.?A0x311fdb2b.__exit_list_size = 32UL;
			result = 1;
		}
		return result;
	}

	// Token: 0x06000043 RID: 67 RVA: 0x00008064 File Offset: 0x00007464
	internal unsafe static method _onexit_m(method _Function)
	{
		method* ptr = <Module>._decode_pointer((void*)<Module>.?A0x311fdb2b.__onexitbegin_m);
		method* ptr2 = <Module>._decode_pointer((void*)<Module>.?A0x311fdb2b.__onexitend_m);
		int num = <Module>._atexit_helper(<Module>._encode_pointer(_Function), &<Module>.?A0x311fdb2b.__exit_list_size, &ptr2, &ptr);
		<Module>.?A0x311fdb2b.__onexitbegin_m = <Module>._encode_pointer((void*)ptr);
		<Module>.?A0x311fdb2b.__onexitend_m = <Module>._encode_pointer((void*)ptr2);
		return (num == -1) ? 0L : _Function;
	}

	// Token: 0x06000044 RID: 68 RVA: 0x00007DCC File Offset: 0x000071CC
	internal unsafe static int _atexit_m(method func)
	{
		method* ptr = <Module>._decode_pointer((void*)<Module>.?A0x311fdb2b.__onexitbegin_m);
		method* ptr2 = <Module>._decode_pointer((void*)<Module>.?A0x311fdb2b.__onexitend_m);
		int result = <Module>._atexit_helper(<Module>._encode_pointer(func), &<Module>.?A0x311fdb2b.__exit_list_size, &ptr2, &ptr);
		<Module>.?A0x311fdb2b.__onexitbegin_m = <Module>._encode_pointer((void*)ptr);
		<Module>.?A0x311fdb2b.__onexitend_m = <Module>._encode_pointer((void*)ptr2);
		return result;
	}

	// Token: 0x06000045 RID: 69 RVA: 0x000080BC File Offset: 0x000074BC
	[DebuggerStepThrough]
	internal static int _initatexit_app_domain()
	{
		if (((<Module>.<CrtImplementationDetails>.AtExitLock._lock_Get() != null) ? 1 : 0) == 0)
		{
			object value = new object();
			<Module>.?_lock@AtExitLock@<CrtImplementationDetails>@@$$Q0PEAXEA = null;
			<Module>.<CrtImplementationDetails>.AtExitLock._lock_Set(value);
			<Module>.?_ref_count@AtExitLock@<CrtImplementationDetails>@@$$Q0HA = 0;
		}
		<Module>.?_ref_count@AtExitLock@<CrtImplementationDetails>@@$$Q0HA++;
		if (((<Module>.<CrtImplementationDetails>.AtExitLock._lock_Get() != null) ? 1 : 0) == 1)
		{
			<Module>.__onexitbegin_app_domain = <Module>._encode_pointer(Marshal.AllocHGlobal(256).ToPointer());
			<Module>.__onexitend_app_domain = <Module>.__onexitbegin_app_domain;
			<Module>.__exit_list_size_app_domain = 32UL;
		}
		return 1;
	}

	// Token: 0x06000046 RID: 70 RVA: 0x00007E1C File Offset: 0x0000721C
	internal unsafe static void _app_exit_callback()
	{
		if (<Module>.__exit_list_size_app_domain != 0UL)
		{
			method* ptr = <Module>._decode_pointer((void*)<Module>.__onexitbegin_app_domain);
			method* ptr2 = <Module>._decode_pointer((void*)<Module>.__onexitend_app_domain);
			try
			{
				if (ptr != -1L && ptr != null && ptr2 != null)
				{
					for (;;)
					{
						ptr2 -= 8L / (long)sizeof(method);
						if (ptr2 < ptr || *(long*)ptr2 != <Module>._encoded_null())
						{
							if (ptr2 < ptr)
							{
								break;
							}
							method system.Void_u0020() = <Module>._decode_pointer(*(long*)ptr2);
							*(long*)ptr2 = <Module>._encoded_null();
							calli(System.Void(), system.Void_u0020());
							ptr = <Module>._decode_pointer((void*)<Module>.__onexitbegin_app_domain);
							ptr2 = <Module>._decode_pointer((void*)<Module>.__onexitend_app_domain);
						}
					}
				}
			}
			finally
			{
				IntPtr hglobal = new IntPtr((void*)ptr);
				Marshal.FreeHGlobal(hglobal);
				<Module>.?_ref_count@AtExitLock@<CrtImplementationDetails>@@$$Q0HA += -1;
				if (<Module>.?_ref_count@AtExitLock@<CrtImplementationDetails>@@$$Q0HA == 0)
				{
					<Module>.<CrtImplementationDetails>.AtExitLock._lock_Destruct();
				}
			}
		}
	}

	// Token: 0x06000047 RID: 71 RVA: 0x0000813C File Offset: 0x0000753C
	internal unsafe static method _onexit_m_appdomain(method _Function)
	{
		method* ptr = <Module>._decode_pointer((void*)<Module>.__onexitbegin_app_domain);
		method* ptr2 = <Module>._decode_pointer((void*)<Module>.__onexitend_app_domain);
		int num = <Module>._atexit_helper(<Module>._encode_pointer(_Function), &<Module>.__exit_list_size_app_domain, &ptr2, &ptr);
		<Module>.__onexitbegin_app_domain = <Module>._encode_pointer((void*)ptr);
		<Module>.__onexitend_app_domain = <Module>._encode_pointer((void*)ptr2);
		return (num == -1) ? 0L : _Function;
	}

	// Token: 0x06000048 RID: 72 RVA: 0x00007EE8 File Offset: 0x000072E8
	[DebuggerStepThrough]
	internal unsafe static int _atexit_m_appdomain(method func)
	{
		method* ptr = <Module>._decode_pointer((void*)<Module>.__onexitbegin_app_domain);
		method* ptr2 = <Module>._decode_pointer((void*)<Module>.__onexitend_app_domain);
		int result = <Module>._atexit_helper(<Module>._encode_pointer(func), &<Module>.__exit_list_size_app_domain, &ptr2, &ptr);
		<Module>.__onexitbegin_app_domain = <Module>._encode_pointer((void*)ptr);
		<Module>.__onexitend_app_domain = <Module>._encode_pointer((void*)ptr2);
		return result;
	}

	// Token: 0x06000049 RID: 73 RVA: 0x00008194 File Offset: 0x00007594
	[DebuggerStepThrough]
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

	// Token: 0x0600004A RID: 74 RVA: 0x000081C4 File Offset: 0x000075C4
	[DebuggerStepThrough]
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

	// Token: 0x0600004B RID: 75 RVA: 0x000081EC File Offset: 0x000075EC
	[DebuggerStepThrough]
	internal static ModuleHandle <CrtImplementationDetails>.ThisModule.Handle()
	{
		return typeof(ThisModule).Module.ModuleHandle;
	}

	// Token: 0x0600004C RID: 76 RVA: 0x0000824C File Offset: 0x0000764C
	[DebuggerStepThrough]
	internal unsafe static void _initterm_m(method* pfbegin, method* pfend)
	{
		if (pfbegin < pfend)
		{
			do
			{
				ulong num = (ulong)(*(long*)pfbegin);
				if (num != 0UL)
				{
					method system.Void_u0020modopt(System.Runtime.CompilerServices.IsConst)*_u0020() = num;
					object obj = calli(System.Void modopt(System.Runtime.CompilerServices.IsConst)*(), typeof(ThisModule).Module.ModuleHandle.ResolveMethodHandle(system.Void_u0020modopt(System.Runtime.CompilerServices.IsConst)*_u0020()).GetFunctionPointer().ToPointer());
				}
				pfbegin += 8L / (long)sizeof(method);
			}
			while (pfbegin < pfend);
		}
	}

	// Token: 0x0600004D RID: 77 RVA: 0x00008210 File Offset: 0x00007610
	[DebuggerStepThrough]
	internal static method <CrtImplementationDetails>.ThisModule.ResolveMethod<void\u0020const\u0020*\u0020__clrcall(void)>(method methodToken)
	{
		return typeof(ThisModule).Module.ModuleHandle.ResolveMethodHandle(methodToken).GetFunctionPointer().ToPointer();
	}

	// Token: 0x0600004E RID: 78 RVA: 0x000082A4 File Offset: 0x000076A4
	[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
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

	// Token: 0x0600004F RID: 79 RVA: 0x000082E8 File Offset: 0x000076E8
	[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
	internal unsafe static void ___CxxCallUnwindDelDtor(method pDtor, void* pThis)
	{
		try
		{
			calli(System.Void(System.Void*), pThis, pDtor);
		}
		catch when (endfilter(<Module>.__FrameUnwindFilter(Marshal.GetExceptionPointers()) != null))
		{
		}
	}

	// Token: 0x06000050 RID: 80 RVA: 0x0000832C File Offset: 0x0000772C
	[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
	internal unsafe static void ___CxxCallUnwindVecDtor(method pVecDtor, void* ptr, ulong size, int count, method pDtor)
	{
		try
		{
			calli(System.Void(System.Void*,System.UInt64,System.Int32,System.Void (System.Void*)), ptr, size, count, pDtor, pVecDtor);
		}
		catch when (endfilter(<Module>.__FrameUnwindFilter(Marshal.GetExceptionPointers()) != null))
		{
		}
	}

	// Token: 0x06000051 RID: 81 RVA: 0x00001980 File Offset: 0x00000D80
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig)]
	public unsafe static extern int GetEnabled(InitializeSpy*);

	// Token: 0x06000052 RID: 82 RVA: 0x00001020 File Offset: 0x00000420
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig)]
	public static extern int InitSpy();

	// Token: 0x06000053 RID: 83 RVA: 0x000064B0 File Offset: 0x000058B0
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig)]
	public unsafe static extern int GetContext(_GUID*, void**);

	// Token: 0x06000054 RID: 84 RVA: 0x00006590 File Offset: 0x00005990
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig)]
	public static extern int IsDefaultContext();

	// Token: 0x06000055 RID: 85 RVA: 0x00009104 File Offset: 0x00008504
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged)]
	public unsafe static extern int CloseHandle(void*);

	// Token: 0x06000056 RID: 86 RVA: 0x00006ABC File Offset: 0x00005EBC
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged)]
	public unsafe static extern method GetProcAddress(HINSTANCE__*, sbyte*);

	// Token: 0x06000057 RID: 87 RVA: 0x00006AC8 File Offset: 0x00005EC8
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged)]
	public static extern uint GetLastError();

	// Token: 0x06000058 RID: 88 RVA: 0x00009176 File Offset: 0x00008576
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged)]
	public unsafe static extern int LookupAccountSidW(char*, void*, char*, uint*, char*, uint*, int*);

	// Token: 0x06000059 RID: 89 RVA: 0x00006AE0 File Offset: 0x00005EE0
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged)]
	public unsafe static extern HINSTANCE__* LoadLibraryW(char*);

	// Token: 0x0600005A RID: 90 RVA: 0x000090FE File Offset: 0x000084FE
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged)]
	public unsafe static extern void* GetCurrentThread();

	// Token: 0x0600005B RID: 91 RVA: 0x0000917C File Offset: 0x0000857C
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged)]
	public unsafe static extern int RegCloseKey(HKEY__*);

	// Token: 0x0600005C RID: 92 RVA: 0x0000912E File Offset: 0x0000852E
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged)]
	public static extern int __CxxQueryExceptionSize();

	// Token: 0x0600005D RID: 93 RVA: 0x00009116 File Offset: 0x00008516
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged)]
	public unsafe static extern int __CxxDetectRethrow(void*);

	// Token: 0x0600005E RID: 94 RVA: 0x00006680 File Offset: 0x00005A80
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig)]
	public static extern ulong GetContextToken();

	// Token: 0x0600005F RID: 95 RVA: 0x0000915E File Offset: 0x0000855E
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged)]
	public unsafe static extern void SysFreeString(char*);

	// Token: 0x06000060 RID: 96 RVA: 0x0000911C File Offset: 0x0000851C
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged)]
	public unsafe static extern void _CxxThrowException(void*, _s__ThrowInfo*);

	// Token: 0x06000061 RID: 97 RVA: 0x00009182 File Offset: 0x00008582
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged)]
	public unsafe static extern int RegOpenKeyExW(HKEY__*, char*, uint, uint, HKEY__**);

	// Token: 0x06000062 RID: 98 RVA: 0x00009110 File Offset: 0x00008510
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged)]
	public unsafe static extern void __CxxUnregisterExceptionObject(void*, int);

	// Token: 0x06000063 RID: 99 RVA: 0x00009146 File Offset: 0x00008546
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged)]
	public unsafe static extern int CoGetStandardMarshal(_GUID*, IUnknown*, uint, void*, uint, IMarshal**);

	// Token: 0x06000064 RID: 100 RVA: 0x00009128 File Offset: 0x00008528
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged)]
	public unsafe static extern int __CxxExceptionFilter(void*, void*, int, void*);

	// Token: 0x06000065 RID: 101 RVA: 0x000093F0 File Offset: 0x000087F0
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig)]
	public static extern int DllRegisterServer();

	// Token: 0x06000066 RID: 102 RVA: 0x00009164 File Offset: 0x00008564
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged)]
	public unsafe static extern int SafeArrayGetElement(tagSAFEARRAY*, int*, void*);

	// Token: 0x06000067 RID: 103 RVA: 0x00006240 File Offset: 0x00005640
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig)]
	public unsafe static extern int ReleaseMarshaledInterface(byte*, int);

	// Token: 0x06000068 RID: 104 RVA: 0x0000914C File Offset: 0x0000854C
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged)]
	public unsafe static extern int CoCreateInstanceEx(_GUID*, IUnknown*, uint, _COSERVERINFO*, uint, tagMULTI_QI*);

	// Token: 0x06000069 RID: 105 RVA: 0x00009158 File Offset: 0x00008558
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged)]
	public unsafe static extern int SafeArrayDestroy(tagSAFEARRAY*);

	// Token: 0x0600006A RID: 106 RVA: 0x00009092 File Offset: 0x00008492
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged)]
	public unsafe static extern int CoGetMarshalSizeMax(uint*, _GUID*, IUnknown*, uint, void*, uint);

	// Token: 0x0600006B RID: 107 RVA: 0x000060F0 File Offset: 0x000054F0
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig)]
	public unsafe static extern int MarshalInterface(byte*, int, IUnknown*, uint, uint);

	// Token: 0x0600006C RID: 108 RVA: 0x00009122 File Offset: 0x00008522
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged)]
	public unsafe static extern int __CxxRegisterExceptionObject(void*, void*);

	// Token: 0x0600006D RID: 109 RVA: 0x0000910A File Offset: 0x0000850A
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged)]
	public unsafe static extern HINSTANCE__* GetModuleHandleW(char*);

	// Token: 0x0600006E RID: 110 RVA: 0x00006890 File Offset: 0x00005C90
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig)]
	public static extern ulong GetContextCheck();

	// Token: 0x0600006F RID: 111 RVA: 0x000090B6 File Offset: 0x000084B6
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged)]
	public unsafe static extern void* GetCurrentProcess();

	// Token: 0x06000070 RID: 112 RVA: 0x00009140 File Offset: 0x00008540
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged)]
	public unsafe static extern int CoCreateInstance(_GUID*, IUnknown*, uint, _GUID*, void**);

	// Token: 0x06000071 RID: 113 RVA: 0x000061A0 File Offset: 0x000055A0
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig)]
	public unsafe static extern int UnmarshalInterface(byte*, int, void**);

	// Token: 0x06000072 RID: 114 RVA: 0x00009086 File Offset: 0x00008486
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged)]
	public unsafe static extern void CoTaskMemFree(void*);

	// Token: 0x06000073 RID: 115 RVA: 0x00009170 File Offset: 0x00008570
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged)]
	public unsafe static extern void VariantInit(tagVARIANT*);

	// Token: 0x06000074 RID: 116 RVA: 0x0000916A File Offset: 0x0000856A
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged)]
	public unsafe static extern int VariantClear(tagVARIANT*);

	// Token: 0x06000075 RID: 117 RVA: 0x00009152 File Offset: 0x00008552
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged)]
	public unsafe static extern int CoInitializeEx(void*, uint);

	// Token: 0x06000076 RID: 118 RVA: 0x00006A20 File Offset: 0x00005E20
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig)]
	public unsafe static extern TransactionStatus* CreateInstance();

	// Token: 0x06000077 RID: 119 RVA: 0x00006BB0 File Offset: 0x00005FB0
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig)]
	public unsafe static extern void* _getFiberPtrId();

	// Token: 0x06000078 RID: 120 RVA: 0x00008F60 File Offset: 0x00008360
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged)]
	public static extern void _amsg_exit(int);

	// Token: 0x06000079 RID: 121 RVA: 0x00008FA0 File Offset: 0x000083A0
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig)]
	public static extern void __security_init_cookie();

	// Token: 0x0600007A RID: 122 RVA: 0x000090AA File Offset: 0x000084AA
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged)]
	public static extern void Sleep(uint);

	// Token: 0x0600007B RID: 123 RVA: 0x00009188 File Offset: 0x00008588
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged)]
	public unsafe static extern int CorBindToRuntimeEx(char*, char*, uint, _GUID*, _GUID*, void**);

	// Token: 0x0600007C RID: 124 RVA: 0x00009134 File Offset: 0x00008534
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged)]
	public static extern void _cexit();

	// Token: 0x0600007D RID: 125 RVA: 0x00008E4E File Offset: 0x0000824E
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged)]
	public unsafe static extern void* _encode_pointer(void*);

	// Token: 0x0600007E RID: 126 RVA: 0x00008F5A File Offset: 0x0000835A
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged)]
	public unsafe static extern void* _decode_pointer(void*);

	// Token: 0x0600007F RID: 127 RVA: 0x00008F54 File Offset: 0x00008354
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged)]
	public unsafe static extern void* _encoded_null();

	// Token: 0x06000080 RID: 128 RVA: 0x0000913A File Offset: 0x0000853A
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged)]
	public unsafe static extern int __FrameUnwindFilter(_EXCEPTION_POINTERS*);

	// Token: 0x04000001 RID: 1 RVA: 0x0000C4E8 File Offset: 0x00009AE8
	internal static $ArrayType$$$BY09$$CBD ??_C@_09IMMMOKKM@ole32?4dll?$AA@;

	// Token: 0x04000002 RID: 2 RVA: 0x0000C4D0 File Offset: 0x00009AD0
	internal static $ArrayType$$$BY0BI@$$CBD ??_C@_0BI@MFIKFOCM@CoRegisterInitializeSpy?$AA@;

	// Token: 0x04000003 RID: 3 RVA: 0x0000C4B8 File Offset: 0x00009AB8
	internal static $ArrayType$$$BY0BG@$$CBD ??_C@_0BG@DGNPCPPD@CoRevokeInitializeSpy?$AA@;

	// Token: 0x04000004 RID: 4 RVA: 0x0001FCA8 File Offset: 0x0001D2A8
	internal static _s__RTTICompleteObjectLocator ??_R4InitializeSpy@Thunk@EnterpriseServices@System@@6B@;

	// Token: 0x04000005 RID: 5 RVA: 0x0001FD80 File Offset: 0x0001D380
	internal static _s__RTTIBaseClassDescriptor2 ??_R1A@?0A@EA@IUnknown@@8;

	// Token: 0x04000006 RID: 6 RVA: 0x0001FCE0 File Offset: 0x0001D2E0
	internal static $_s__RTTIBaseClassArray$_extraBytes_24 ??_R2InitializeSpy@Thunk@EnterpriseServices@System@@8;

	// Token: 0x04000007 RID: 7 RVA: 0x0001FDA8 File Offset: 0x0001D3A8
	internal static _s__RTTIClassHierarchyDescriptor ??_R3IUnknown@@8;

	// Token: 0x04000008 RID: 8 RVA: 0x0001FD00 File Offset: 0x0001D300
	internal static _s__RTTIBaseClassDescriptor2 ??_R1A@?0A@EA@InitializeSpy@Thunk@EnterpriseServices@System@@8;

	// Token: 0x04000009 RID: 9 RVA: 0x0001FDC0 File Offset: 0x0001D3C0
	internal static $_s__RTTIBaseClassArray$_extraBytes_8 ??_R2IUnknown@@8;

	// Token: 0x0400000A RID: 10 RVA: 0x0001FD50 File Offset: 0x0001D350
	internal static _s__RTTIClassHierarchyDescriptor ??_R3IInitializeSpy@@8;

	// Token: 0x0400000B RID: 11 RVA: 0x00022008 File Offset: 0x0001EC08
	internal static $ArrayType$$$BY0N@Q6AXXZ ??_7InitializeSpy@Thunk@EnterpriseServices@System@@6B@;

	// Token: 0x0400000C RID: 12 RVA: 0x0000C4A8 File Offset: 0x00009AA8
	internal static __s_GUID _GUID_00000144_0000_0000_c000_000000000046;

	// Token: 0x0400000D RID: 13 RVA: 0x000220B8 File Offset: 0x0001ECB8
	internal static $_TypeDescriptor$_extraBytes_21 ??_R0?AUIInitializeSpy@@@8;

	// Token: 0x0400000E RID: 14 RVA: 0x000220E0 File Offset: 0x0001ECE0
	internal static $_TypeDescriptor$_extraBytes_15 ??_R0?AUIUnknown@@@8;

	// Token: 0x0400000F RID: 15 RVA: 0x000229F0 File Offset: 0x0001F5F0
	internal unsafe static InitializeSpy* g_pSpy;

	// Token: 0x04000010 RID: 16 RVA: 0x0000C45C File Offset: 0x00009A5C
	internal static int ?BUCKET_COUNT@?$SimpleHashtable@K_K@Thunk@EnterpriseServices@System@@0HB;

	// Token: 0x04000011 RID: 17 RVA: 0x0000C460 File Offset: 0x00009A60
	internal static int ?BUCKET_COUNT@?$SimpleHashtable@_KH@Thunk@EnterpriseServices@System@@0HB;

	// Token: 0x04000012 RID: 18 RVA: 0x0001FCC8 File Offset: 0x0001D2C8
	internal static _s__RTTIClassHierarchyDescriptor ??_R3InitializeSpy@Thunk@EnterpriseServices@System@@8;

	// Token: 0x04000013 RID: 19 RVA: 0x0001FD28 File Offset: 0x0001D328
	internal static _s__RTTIBaseClassDescriptor2 ??_R1A@?0A@EA@IInitializeSpy@@8;

	// Token: 0x04000014 RID: 20 RVA: 0x00022070 File Offset: 0x0001EC70
	internal static $_TypeDescriptor$_extraBytes_52 ??_R0?AVInitializeSpy@Thunk@EnterpriseServices@System@@@8;

	// Token: 0x04000015 RID: 21 RVA: 0x0001FD68 File Offset: 0x0001D368
	internal static $_s__RTTIBaseClassArray$_extraBytes_16 ??_R2IInitializeSpy@@8;

	// Token: 0x04000016 RID: 22 RVA: 0x00022108 File Offset: 0x0001ED08
	public static method __m2mep@?IsEqualGUID@@$$J0YAHAEBU_GUID@@0@Z;

	// Token: 0x04000017 RID: 23 RVA: 0x00022118 File Offset: 0x0001ED18
	public static method __m2mep@??8@$$J0YAHAEBU_GUID@@0@Z;

	// Token: 0x04000018 RID: 24 RVA: 0x00022128 File Offset: 0x0001ED28
	public static method __m2mep@??0IInitializeSpy@@$$FQEAA@XZ;

	// Token: 0x04000019 RID: 25 RVA: 0x0000C550 File Offset: 0x00009B50
	internal static __s_GUID _GUID_7d40fcc8_f81e_462e_bba1_8a99ebdc826c;

	// Token: 0x0400001A RID: 26 RVA: 0x0000C560 File Offset: 0x00009B60
	internal static __s_GUID _GUID_02558374_df2e_4dae_bd6b_1d5c994f9bdc;

	// Token: 0x0400001B RID: 27 RVA: 0x0000C540 File Offset: 0x00009B40
	internal static __s_GUID _GUID_0fb15084_af41_11ce_bd2b_204c4f4f5020;

	// Token: 0x0400001C RID: 28 RVA: 0x0000C578 File Offset: 0x00009B78
	internal static $ArrayType$$$BY0N@$$CB_W unnamed-global-0;

	// Token: 0x0400001D RID: 29 RVA: 0x0000C598 File Offset: 0x00009B98
	internal static $ArrayType$$$BY0BA@$$CBD unnamed-global-1;

	// Token: 0x0400001E RID: 30 RVA: 0x0000C5A8 File Offset: 0x00009BA8
	internal static $ArrayType$$$BY0P@$$CBD unnamed-global-2;

	// Token: 0x0400001F RID: 31 RVA: 0x0000C618 File Offset: 0x00009C18
	internal static $ArrayType$$$BY06$$CBD unnamed-global-0;

	// Token: 0x04000020 RID: 32 RVA: 0x0000C620 File Offset: 0x00009C20
	internal static $ArrayType$$$BY0P@$$CBD unnamed-global-1;

	// Token: 0x04000021 RID: 33 RVA: 0x0000C630 File Offset: 0x00009C30
	internal static $ArrayType$$$BY0N@$$CB_W unnamed-global-2;

	// Token: 0x04000022 RID: 34 RVA: 0x0000C650 File Offset: 0x00009C50
	internal static $ArrayType$$$BY0BE@$$CBD unnamed-global-3;

	// Token: 0x04000023 RID: 35 RVA: 0x0000C668 File Offset: 0x00009C68
	internal static $ArrayType$$$BY0N@$$CB_W unnamed-global-4;

	// Token: 0x04000024 RID: 36 RVA: 0x0000C688 File Offset: 0x00009C88
	internal static $ArrayType$$$BY0BB@$$CBD unnamed-global-5;

	// Token: 0x04000025 RID: 37 RVA: 0x0000C6A0 File Offset: 0x00009CA0
	internal static $ArrayType$$$BY07$$CB_W unnamed-global-6;

	// Token: 0x04000026 RID: 38 RVA: 0x0000C6B0 File Offset: 0x00009CB0
	internal static $ArrayType$$$BY0CN@$$CB_W unnamed-global-7;

	// Token: 0x04000027 RID: 39 RVA: 0x0000C710 File Offset: 0x00009D10
	internal static $ArrayType$$$BY0M@$$CB_W unnamed-global-8;

	// Token: 0x04000028 RID: 40 RVA: 0x0000C728 File Offset: 0x00009D28
	internal static $ArrayType$$$BY0BF@$$CBD unnamed-global-9;

	// Token: 0x04000029 RID: 41 RVA: 0x00022A10 File Offset: 0x0001F610
	internal static volatile int ?fIsWow@?1??IsWin64@Proxy@Thunk@EnterpriseServices@System@@CM_NAEA_N@Z@4HC;

	// Token: 0x0400002A RID: 42 RVA: 0x00022A18 File Offset: 0x0001F618
	internal static volatile int ?fInit@?1??IsWin64@Proxy@Thunk@EnterpriseServices@System@@CM_NAEA_N@Z@4HC;

	// Token: 0x0400002B RID: 43 RVA: 0x00022A14 File Offset: 0x0001F614
	internal static volatile int ?fWin64@?1??IsWin64@Proxy@Thunk@EnterpriseServices@System@@CM_NAEA_N@Z@4HC;

	// Token: 0x0400002C RID: 44 RVA: 0x00022130 File Offset: 0x0001ED30
	internal static uint ?dwExts@?1??GetManagedExts@Proxy@Thunk@EnterpriseServices@System@@SMHXZ@4KA;

	// Token: 0x0400002D RID: 45 RVA: 0x0000C608 File Offset: 0x00009C08
	internal static _GUID IID_IObjContext;

	// Token: 0x0400002E RID: 46 RVA: 0x0000C748 File Offset: 0x00009D48
	internal static __s_GUID _GUID_2732fd59_b2b4_4d44_878c_8b8f09626008;

	// Token: 0x0400002F RID: 47 RVA: 0x0000C740 File Offset: 0x00009D40
	unsafe static int** __unep@?SendDestructionEventsCallback@Thunk@EnterpriseServices@System@@$$FYAJPEAUtagComCallData@123@@Z;

	// Token: 0x04000030 RID: 48 RVA: 0x0000C758 File Offset: 0x00009D58
	unsafe static int** __unep@?FilteringCallbackFunction@Thunk@EnterpriseServices@System@@$$FYAJPEAUtagComCallData@123@@Z;

	// Token: 0x04000031 RID: 49 RVA: 0x0000C7D0 File Offset: 0x00009DD0
	internal static $ArrayType$$$BY0M@$$CB_W ??_C@_1BI@NMLGLHFF@?$AAc?$AAo?$AAm?$AAs?$AAv?$AAc?$AAs?$AA?4?$AAd?$AAl?$AAl?$AA?$AA@;

	// Token: 0x04000032 RID: 50 RVA: 0x0000C7B8 File Offset: 0x00009DB8
	internal static $ArrayType$$$BY0BF@$$CBD ??_C@_0BF@EEGEFJCM@CoEnterServiceDomain?$AA@;

	// Token: 0x04000033 RID: 51 RVA: 0x0000C7A0 File Offset: 0x00009DA0
	internal static $ArrayType$$$BY0BF@$$CBD ??_C@_0BF@JEIDNIFH@CoLeaveServiceDomain?$AA@;

	// Token: 0x04000034 RID: 52 RVA: 0x0000C788 File Offset: 0x00009D88
	internal static $ArrayType$$$BY0BB@$$CBD ??_C@_0BB@LLBGKOGP@CoCreateActivity?$AA@;

	// Token: 0x04000035 RID: 53 RVA: 0x0000C780 File Offset: 0x00009D80
	internal static $ArrayType$$$BY00$$CBD unnamed-global-0;

	// Token: 0x04000036 RID: 54 RVA: 0x0000C781 File Offset: 0x00009D81
	internal static $ArrayType$$$BY00$$CBD unnamed-global-1;

	// Token: 0x04000037 RID: 55 RVA: 0x0000C782 File Offset: 0x00009D82
	internal static $ArrayType$$$BY00$$CBD unnamed-global-2;

	// Token: 0x04000038 RID: 56 RVA: 0x0000C783 File Offset: 0x00009D83
	internal static $ArrayType$$$BY00$$CBD unnamed-global-3;

	// Token: 0x04000039 RID: 57 RVA: 0x00022A1C File Offset: 0x0001F61C
	internal static bool ?fSupportsSysTxn@?1??get_SupportsSysTxn@ServiceConfigThunk@Thunk@EnterpriseServices@System@@QE$AAM_NXZ@4_NA;

	// Token: 0x0400003A RID: 58 RVA: 0x0000C7E8 File Offset: 0x00009DE8
	internal static __s_GUID _GUID_33caf1a1_fcb8_472b_b45e_967448ded6d8;

	// Token: 0x0400003B RID: 59 RVA: 0x00022A1D File Offset: 0x0001F61D
	internal static bool ?fInitialized@?1??get_SupportsSysTxn@ServiceConfigThunk@Thunk@EnterpriseServices@System@@QE$AAM_NXZ@4_NA;

	// Token: 0x0400003C RID: 60 RVA: 0x0000CA38 File Offset: 0x0000A038
	internal static __s_GUID _GUID_90f1a06e_7712_4762_86b5_7a5eba6bdb02;

	// Token: 0x0400003D RID: 61 RVA: 0x0000C9F8 File Offset: 0x00009FF8
	internal static __s_GUID _GUID_cb2f6722_ab3a_11d2_9c40_00c04fa30a3e;

	// Token: 0x0400003E RID: 62 RVA: 0x0000C428 File Offset: 0x00009A28
	internal static $ArrayType$$$BY00Q6MPEBXXZ __xc_mp_z;

	// Token: 0x0400003F RID: 63 RVA: 0x0000CA08 File Offset: 0x0000A008
	internal static __s_GUID _GUID_00000000_0000_0000_c000_000000000046;

	// Token: 0x04000040 RID: 64 RVA: 0x0000C430 File Offset: 0x00009A30
	internal static $ArrayType$$$BY00Q6MPEBXXZ __xi_vt_a;

	// Token: 0x04000041 RID: 65
	[FixedAddressValueType]
	internal static Progress.State ?InitializedVtables@CurrentDomain@<CrtImplementationDetails>@@$$Q2W4State@Progress@2@A;

	// Token: 0x04000042 RID: 66 RVA: 0x0000C3F8 File Offset: 0x000099F8
	internal static method ?InitializedVtables$initializer$@CurrentDomain@<CrtImplementationDetails>@@$$Q2P6MXXZEA;

	// Token: 0x04000043 RID: 67
	[FixedAddressValueType]
	internal static bool ?IsDefaultDomain@CurrentDomain@<CrtImplementationDetails>@@$$Q2_NA;

	// Token: 0x04000044 RID: 68 RVA: 0x0000C3F0 File Offset: 0x000099F0
	internal static method ?IsDefaultDomain$initializer$@CurrentDomain@<CrtImplementationDetails>@@$$Q2P6MXXZEA;

	// Token: 0x04000045 RID: 69 RVA: 0x0000C3D8 File Offset: 0x000099D8
	internal static $ArrayType$$$BY00Q6MPEBXXZ __xc_ma_a;

	// Token: 0x04000046 RID: 70
	[FixedAddressValueType]
	internal static Progress.State ?InitializedPerAppDomain@CurrentDomain@<CrtImplementationDetails>@@$$Q2W4State@Progress@2@A;

	// Token: 0x04000047 RID: 71 RVA: 0x0000C410 File Offset: 0x00009A10
	internal static method ?InitializedPerAppDomain$initializer$@CurrentDomain@<CrtImplementationDetails>@@$$Q2P6MXXZEA;

	// Token: 0x04000048 RID: 72 RVA: 0x0000C418 File Offset: 0x00009A18
	internal static $ArrayType$$$BY00Q6MPEBXXZ __xc_ma_z;

	// Token: 0x04000049 RID: 73
	[FixedAddressValueType]
	internal static Progress.State ?InitializedNative@CurrentDomain@<CrtImplementationDetails>@@$$Q2W4State@Progress@2@A;

	// Token: 0x0400004A RID: 74 RVA: 0x0000C400 File Offset: 0x00009A00
	internal static method ?InitializedNative$initializer$@CurrentDomain@<CrtImplementationDetails>@@$$Q2P6MXXZEA;

	// Token: 0x0400004B RID: 75 RVA: 0x0000CA18 File Offset: 0x0000A018
	internal static __s_GUID _GUID_cb2f6723_ab3a_11d2_9c40_00c04fa30a3e;

	// Token: 0x0400004C RID: 76 RVA: 0x0000C438 File Offset: 0x00009A38
	internal static $ArrayType$$$BY00Q6MPEBXXZ __xi_vt_z;

	// Token: 0x0400004D RID: 77
	[FixedAddressValueType]
	internal static int ?Uninitialized@CurrentDomain@<CrtImplementationDetails>@@$$Q2HA;

	// Token: 0x0400004E RID: 78 RVA: 0x0000C3E8 File Offset: 0x000099E8
	internal static method ?Uninitialized$initializer$@CurrentDomain@<CrtImplementationDetails>@@$$Q2P6MXXZEA;

	// Token: 0x0400004F RID: 79
	[FixedAddressValueType]
	internal static int ?Initialized@CurrentDomain@<CrtImplementationDetails>@@$$Q2HA;

	// Token: 0x04000050 RID: 80 RVA: 0x0000C3E0 File Offset: 0x000099E0
	internal static method ?Initialized$initializer$@CurrentDomain@<CrtImplementationDetails>@@$$Q2P6MXXZEA;

	// Token: 0x04000051 RID: 81 RVA: 0x00022C47 File Offset: 0x0001F847
	internal static bool ?InitializedPerProcess@DefaultDomain@<CrtImplementationDetails>@@2_NA;

	// Token: 0x04000052 RID: 82
	[FixedAddressValueType]
	internal static Progress.State ?InitializedPerProcess@CurrentDomain@<CrtImplementationDetails>@@$$Q2W4State@Progress@2@A;

	// Token: 0x04000053 RID: 83 RVA: 0x00022C44 File Offset: 0x0001F844
	internal static bool ?Entered@DefaultDomain@<CrtImplementationDetails>@@2_NA;

	// Token: 0x04000054 RID: 84 RVA: 0x00022C45 File Offset: 0x0001F845
	internal static bool ?InitializedNative@DefaultDomain@<CrtImplementationDetails>@@2_NA;

	// Token: 0x04000055 RID: 85 RVA: 0x00022C40 File Offset: 0x0001F840
	internal static int ?Count@AllDomains@<CrtImplementationDetails>@@2HA;

	// Token: 0x04000056 RID: 86 RVA: 0x0000C9E4 File Offset: 0x00009FE4
	internal static uint ?ProcessAttach@NativeDll@<CrtImplementationDetails>@@0IB;

	// Token: 0x04000057 RID: 87 RVA: 0x0000C9E8 File Offset: 0x00009FE8
	internal static uint ?ThreadAttach@NativeDll@<CrtImplementationDetails>@@0IB;

	// Token: 0x04000058 RID: 88 RVA: 0x00022250 File Offset: 0x0001EE50
	internal static TriBool.State ?hasNative@DefaultDomain@<CrtImplementationDetails>@@0W4State@TriBool@2@A;

	// Token: 0x04000059 RID: 89 RVA: 0x0000C9E0 File Offset: 0x00009FE0
	internal static uint ?ProcessDetach@NativeDll@<CrtImplementationDetails>@@0IB;

	// Token: 0x0400005A RID: 90 RVA: 0x0000C9EC File Offset: 0x00009FEC
	internal static uint ?ThreadDetach@NativeDll@<CrtImplementationDetails>@@0IB;

	// Token: 0x0400005B RID: 91 RVA: 0x0000C9F0 File Offset: 0x00009FF0
	internal static uint ?ProcessVerifier@NativeDll@<CrtImplementationDetails>@@0IB;

	// Token: 0x0400005C RID: 92 RVA: 0x0002224C File Offset: 0x0001EE4C
	internal static TriBool.State ?hasPerProcess@DefaultDomain@<CrtImplementationDetails>@@0W4State@TriBool@2@A;

	// Token: 0x0400005D RID: 93 RVA: 0x00022C46 File Offset: 0x0001F846
	internal static bool ?InitializedNativeFromCCTOR@DefaultDomain@<CrtImplementationDetails>@@2_NA;

	// Token: 0x0400005E RID: 94 RVA: 0x0000C420 File Offset: 0x00009A20
	internal static $ArrayType$$$BY00Q6MPEBXXZ __xc_mp_a;

	// Token: 0x0400005F RID: 95 RVA: 0x0000CA28 File Offset: 0x0000A028
	internal static __s_GUID _GUID_90f1a06c_7712_4762_86b5_7a5eba6bdb02;

	// Token: 0x04000060 RID: 96 RVA: 0x0000C408 File Offset: 0x00009A08
	internal static method ?InitializedPerProcess$initializer$@CurrentDomain@<CrtImplementationDetails>@@$$Q2P6MXXZEA;

	// Token: 0x04000061 RID: 97 RVA: 0x00022260 File Offset: 0x0001EE60
	public static method __m2mep@?IsInDllMain@NativeDll@<CrtImplementationDetails>@@$$FSA_NXZ;

	// Token: 0x04000062 RID: 98 RVA: 0x00022270 File Offset: 0x0001EE70
	public static method __m2mep@?IsInProcessAttach@NativeDll@<CrtImplementationDetails>@@$$FSA_NXZ;

	// Token: 0x04000063 RID: 99 RVA: 0x00022280 File Offset: 0x0001EE80
	public static method __m2mep@?IsInProcessDetach@NativeDll@<CrtImplementationDetails>@@$$FSA_NXZ;

	// Token: 0x04000064 RID: 100 RVA: 0x00022290 File Offset: 0x0001EE90
	public static method __m2mep@?IsInVcclrit@NativeDll@<CrtImplementationDetails>@@$$FSA_NXZ;

	// Token: 0x04000065 RID: 101 RVA: 0x000222A0 File Offset: 0x0001EEA0
	public static method __m2mep@?IsSafeForManagedCode@NativeDll@<CrtImplementationDetails>@@$$FSA_NXZ;

	// Token: 0x04000066 RID: 102 RVA: 0x00022400 File Offset: 0x0001F000
	public static method __m2mep@?ThrowNestedModuleLoadException@<CrtImplementationDetails>@@$$FYMXPE$AAVException@System@@0@Z;

	// Token: 0x04000067 RID: 103 RVA: 0x000222B0 File Offset: 0x0001EEB0
	public static method __m2mep@?ThrowModuleLoadException@<CrtImplementationDetails>@@$$FYMXPE$AAVString@System@@@Z;

	// Token: 0x04000068 RID: 104 RVA: 0x000222C0 File Offset: 0x0001EEC0
	public static method __m2mep@?ThrowModuleLoadException@<CrtImplementationDetails>@@$$FYMXPE$AAVString@System@@PE$AAVException@3@@Z;

	// Token: 0x04000069 RID: 105 RVA: 0x000222D0 File Offset: 0x0001EED0
	public static method __m2mep@?RegisterModuleUninitializer@<CrtImplementationDetails>@@$$FYMXPE$AAVEventHandler@System@@@Z;

	// Token: 0x0400006A RID: 106 RVA: 0x000222E0 File Offset: 0x0001EEE0
	public static method __m2mep@?__get_default_appdomain@@$$FYAJPEAPEAUIUnknown@@@Z;

	// Token: 0x0400006B RID: 107 RVA: 0x000222F0 File Offset: 0x0001EEF0
	public static method __m2mep@?__release_appdomain@@$$FYAXPEAUIUnknown@@@Z;

	// Token: 0x0400006C RID: 108 RVA: 0x00022300 File Offset: 0x0001EF00
	public static method __m2mep@?GetDefaultDomain@<CrtImplementationDetails>@@$$FYMPE$AAVAppDomain@System@@XZ;

	// Token: 0x0400006D RID: 109 RVA: 0x00022310 File Offset: 0x0001EF10
	public static method __m2mep@?DoCallBackInDefaultDomain@<CrtImplementationDetails>@@$$FYAXP6AJPEAX@Z0@Z;

	// Token: 0x0400006E RID: 110 RVA: 0x00022320 File Offset: 0x0001EF20
	public static method __m2mep@?DoNothing@DefaultDomain@<CrtImplementationDetails>@@$$FCAJPEAX@Z;

	// Token: 0x0400006F RID: 111 RVA: 0x00022330 File Offset: 0x0001EF30
	public static method __m2mep@?HasPerProcess@DefaultDomain@<CrtImplementationDetails>@@$$FSA_NXZ;

	// Token: 0x04000070 RID: 112 RVA: 0x00022340 File Offset: 0x0001EF40
	public static method __m2mep@?HasNative@DefaultDomain@<CrtImplementationDetails>@@$$FSA_NXZ;

	// Token: 0x04000071 RID: 113 RVA: 0x00022350 File Offset: 0x0001EF50
	public static method __m2mep@?NeedsInitialization@DefaultDomain@<CrtImplementationDetails>@@$$FSA_NXZ;

	// Token: 0x04000072 RID: 114 RVA: 0x00022360 File Offset: 0x0001EF60
	public static method __m2mep@?NeedsUninitialization@DefaultDomain@<CrtImplementationDetails>@@$$FSA_NXZ;

	// Token: 0x04000073 RID: 115 RVA: 0x00022370 File Offset: 0x0001EF70
	public static method __m2mep@?Initialize@DefaultDomain@<CrtImplementationDetails>@@$$FSAXXZ;

	// Token: 0x04000074 RID: 116 RVA: 0x00022410 File Offset: 0x0001F010
	public static method __m2mep@?InitializeVtables@LanguageSupport@<CrtImplementationDetails>@@$$FAEAAXXZ;

	// Token: 0x04000075 RID: 117 RVA: 0x00022420 File Offset: 0x0001F020
	public static method __m2mep@?InitializeDefaultAppDomain@LanguageSupport@<CrtImplementationDetails>@@$$FAEAAXXZ;

	// Token: 0x04000076 RID: 118 RVA: 0x00022430 File Offset: 0x0001F030
	public static method __m2mep@?InitializeNative@LanguageSupport@<CrtImplementationDetails>@@$$FAEAAXXZ;

	// Token: 0x04000077 RID: 119 RVA: 0x00022440 File Offset: 0x0001F040
	public static method __m2mep@?InitializePerProcess@LanguageSupport@<CrtImplementationDetails>@@$$FAEAAXXZ;

	// Token: 0x04000078 RID: 120 RVA: 0x00022450 File Offset: 0x0001F050
	public static method __m2mep@?InitializePerAppDomain@LanguageSupport@<CrtImplementationDetails>@@$$FAEAAXXZ;

	// Token: 0x04000079 RID: 121 RVA: 0x00022460 File Offset: 0x0001F060
	public static method __m2mep@?InitializeUninitializer@LanguageSupport@<CrtImplementationDetails>@@$$FAEAAXXZ;

	// Token: 0x0400007A RID: 122 RVA: 0x00022470 File Offset: 0x0001F070
	public static method __m2mep@?_Initialize@LanguageSupport@<CrtImplementationDetails>@@$$FAEAAXXZ;

	// Token: 0x0400007B RID: 123 RVA: 0x00022380 File Offset: 0x0001EF80
	public static method __m2mep@?UninitializeAppDomain@LanguageSupport@<CrtImplementationDetails>@@$$FCAXXZ;

	// Token: 0x0400007C RID: 124 RVA: 0x00022390 File Offset: 0x0001EF90
	public static method __m2mep@?_UninitializeDefaultDomain@LanguageSupport@<CrtImplementationDetails>@@$$FCAJPEAX@Z;

	// Token: 0x0400007D RID: 125 RVA: 0x000223A0 File Offset: 0x0001EFA0
	public static method __m2mep@?UninitializeDefaultDomain@LanguageSupport@<CrtImplementationDetails>@@$$FCAXXZ;

	// Token: 0x0400007E RID: 126 RVA: 0x000223B0 File Offset: 0x0001EFB0
	public static method __m2mep@?DomainUnload@LanguageSupport@<CrtImplementationDetails>@@$$FCMXPE$AAVObject@System@@PE$AAVEventArgs@4@@Z;

	// Token: 0x0400007F RID: 127 RVA: 0x00022480 File Offset: 0x0001F080
	public static method __m2mep@?Cleanup@LanguageSupport@<CrtImplementationDetails>@@$$FAEAMXPE$AAVException@System@@@Z;

	// Token: 0x04000080 RID: 128 RVA: 0x00022490 File Offset: 0x0001F090
	public static method __m2mep@?Initialize@LanguageSupport@<CrtImplementationDetails>@@$$FQEAAXXZ;

	// Token: 0x04000081 RID: 129 RVA: 0x000224C0 File Offset: 0x0001F0C0
	public static method cctor@@$$FYMXXZ;

	// Token: 0x04000082 RID: 130 RVA: 0x000224A0 File Offset: 0x0001F0A0
	public static method __m2mep@??0LanguageSupport@<CrtImplementationDetails>@@$$FQEAA@XZ;

	// Token: 0x04000083 RID: 131 RVA: 0x000224B0 File Offset: 0x0001F0B0
	public static method __m2mep@??1LanguageSupport@<CrtImplementationDetails>@@$$FQEAA@XZ;

	// Token: 0x04000084 RID: 132 RVA: 0x000223C0 File Offset: 0x0001EFC0
	public static method __m2mep@??0?$gcroot@PE$AAVString@System@@@@$$FQEAA@XZ;

	// Token: 0x04000085 RID: 133 RVA: 0x000223D0 File Offset: 0x0001EFD0
	public static method __m2mep@??1?$gcroot@PE$AAVString@System@@@@$$FQEAA@XZ;

	// Token: 0x04000086 RID: 134 RVA: 0x000223E0 File Offset: 0x0001EFE0
	public static method __m2mep@??4?$gcroot@PE$AAVString@System@@@@$$FQEAMAEAU0@PE$AAVString@System@@@Z;

	// Token: 0x04000087 RID: 135 RVA: 0x000223F0 File Offset: 0x0001EFF0
	public static method __m2mep@??B?$gcroot@PE$AAVString@System@@@@$$FQEBMPE$AAVString@System@@XZ;

	// Token: 0x04000088 RID: 136 RVA: 0x0000CA48 File Offset: 0x0000A048
	public unsafe static int** __unep@?DoNothing@DefaultDomain@<CrtImplementationDetails>@@$$FCAJPEAX@Z;

	// Token: 0x04000089 RID: 137 RVA: 0x0000CA50 File Offset: 0x0000A050
	public unsafe static int** __unep@?_UninitializeDefaultDomain@LanguageSupport@<CrtImplementationDetails>@@$$FCAJPEAX@Z;

	// Token: 0x0400008A RID: 138
	[FixedAddressValueType]
	internal static ulong __exit_list_size_app_domain;

	// Token: 0x0400008B RID: 139
	[FixedAddressValueType]
	internal unsafe static method* __onexitbegin_app_domain;

	// Token: 0x0400008C RID: 140 RVA: 0x00022E28 File Offset: 0x0001FA28
	internal static ulong __exit_list_size;

	// Token: 0x0400008D RID: 141
	[FixedAddressValueType]
	internal unsafe static method* __onexitend_app_domain;

	// Token: 0x0400008E RID: 142 RVA: 0x00022E18 File Offset: 0x0001FA18
	internal unsafe static method* __onexitbegin_m;

	// Token: 0x0400008F RID: 143 RVA: 0x00022E20 File Offset: 0x0001FA20
	internal unsafe static method* __onexitend_m;

	// Token: 0x04000090 RID: 144
	[FixedAddressValueType]
	internal static int ?_ref_count@AtExitLock@<CrtImplementationDetails>@@$$Q0HA;

	// Token: 0x04000091 RID: 145
	[FixedAddressValueType]
	internal unsafe static void* ?_lock@AtExitLock@<CrtImplementationDetails>@@$$Q0PEAXEA;

	// Token: 0x04000092 RID: 146 RVA: 0x00022508 File Offset: 0x0001F108
	public static method __m2mep@?_handle@AtExitLock@<CrtImplementationDetails>@@$$FCMPE$AAVGCHandle@InteropServices@Runtime@System@@XZ;

	// Token: 0x04000093 RID: 147 RVA: 0x00022608 File Offset: 0x0001F208
	public static method __m2mep@?_lock_Construct@AtExitLock@<CrtImplementationDetails>@@$$FCMXPE$AAVObject@System@@@Z;

	// Token: 0x04000094 RID: 148 RVA: 0x00022518 File Offset: 0x0001F118
	public static method __m2mep@?_lock_Set@AtExitLock@<CrtImplementationDetails>@@$$FCMXPE$AAVObject@System@@@Z;

	// Token: 0x04000095 RID: 149 RVA: 0x00022528 File Offset: 0x0001F128
	public static method __m2mep@?_lock_Get@AtExitLock@<CrtImplementationDetails>@@$$FCMPE$AAVObject@System@@XZ;

	// Token: 0x04000096 RID: 150 RVA: 0x00022538 File Offset: 0x0001F138
	public static method __m2mep@?_lock_Destruct@AtExitLock@<CrtImplementationDetails>@@$$FCAXXZ;

	// Token: 0x04000097 RID: 151 RVA: 0x00022548 File Offset: 0x0001F148
	public static method __m2mep@?IsInitialized@AtExitLock@<CrtImplementationDetails>@@$$FSA_NXZ;

	// Token: 0x04000098 RID: 152 RVA: 0x00022618 File Offset: 0x0001F218
	public static method __m2mep@?AddRef@AtExitLock@<CrtImplementationDetails>@@$$FSAXXZ;

	// Token: 0x04000099 RID: 153 RVA: 0x00022558 File Offset: 0x0001F158
	public static method __m2mep@?RemoveRef@AtExitLock@<CrtImplementationDetails>@@$$FSAXXZ;

	// Token: 0x0400009A RID: 154 RVA: 0x00022568 File Offset: 0x0001F168
	public static method __m2mep@?Enter@AtExitLock@<CrtImplementationDetails>@@$$FSAXXZ;

	// Token: 0x0400009B RID: 155 RVA: 0x00022578 File Offset: 0x0001F178
	public static method __m2mep@?Exit@AtExitLock@<CrtImplementationDetails>@@$$FSAXXZ;

	// Token: 0x0400009C RID: 156 RVA: 0x00022588 File Offset: 0x0001F188
	public static method __m2mep@?__global_lock@?A0x311fdb2b@@$$FYA_NXZ;

	// Token: 0x0400009D RID: 157 RVA: 0x00022598 File Offset: 0x0001F198
	public static method __m2mep@?__global_unlock@?A0x311fdb2b@@$$FYA_NXZ;

	// Token: 0x0400009E RID: 158 RVA: 0x00022628 File Offset: 0x0001F228
	public static method __m2mep@?__alloc_global_lock@?A0x311fdb2b@@$$FYA_NXZ;

	// Token: 0x0400009F RID: 159 RVA: 0x000225A8 File Offset: 0x0001F1A8
	public static method __m2mep@?__dealloc_global_lock@?A0x311fdb2b@@$$FYAXXZ;

	// Token: 0x040000A0 RID: 160 RVA: 0x000225B8 File Offset: 0x0001F1B8
	public static method __m2mep@?_atexit_helper@@$$J0YMHP6MXXZPEA_KPEAPEAP6MXXZ2@Z;

	// Token: 0x040000A1 RID: 161 RVA: 0x000225C8 File Offset: 0x0001F1C8
	public static method __m2mep@?_exit_callback@@$$J0YMXXZ;

	// Token: 0x040000A2 RID: 162 RVA: 0x00022638 File Offset: 0x0001F238
	public static method __m2mep@?_initatexit_m@@$$J0YMHXZ;

	// Token: 0x040000A3 RID: 163 RVA: 0x00022648 File Offset: 0x0001F248
	public static method __m2mep@?_onexit_m@@$$J0YMP6MHXZP6MHXZ@Z;

	// Token: 0x040000A4 RID: 164 RVA: 0x000225D8 File Offset: 0x0001F1D8
	public static method __m2mep@?_atexit_m@@$$J0YMHP6MXXZ@Z;

	// Token: 0x040000A5 RID: 165 RVA: 0x00022658 File Offset: 0x0001F258
	public static method __m2mep@?_initatexit_app_domain@@$$J0YMHXZ;

	// Token: 0x040000A6 RID: 166 RVA: 0x000225E8 File Offset: 0x0001F1E8
	public static method __m2mep@?_app_exit_callback@@$$J0YMXXZ;

	// Token: 0x040000A7 RID: 167 RVA: 0x00022668 File Offset: 0x0001F268
	public static method __m2mep@?_onexit_m_appdomain@@$$J0YMP6MHXZP6MHXZ@Z;

	// Token: 0x040000A8 RID: 168 RVA: 0x000225F8 File Offset: 0x0001F1F8
	public static method __m2mep@?_atexit_m_appdomain@@$$J0YMHP6MXXZ@Z;

	// Token: 0x040000A9 RID: 169 RVA: 0x00022678 File Offset: 0x0001F278
	public static method __m2mep@?_initterm_e@@$$FYMHPEAP6AHXZ0@Z;

	// Token: 0x040000AA RID: 170 RVA: 0x00022688 File Offset: 0x0001F288
	public static method __m2mep@?_initterm@@$$FYMXPEAP6AXXZ0@Z;

	// Token: 0x040000AB RID: 171 RVA: 0x00022698 File Offset: 0x0001F298
	public static method __m2mep@?Handle@ThisModule@<CrtImplementationDetails>@@$$FCM?AVModuleHandle@System@@XZ;

	// Token: 0x040000AC RID: 172 RVA: 0x000226B8 File Offset: 0x0001F2B8
	public static method __m2mep@?_initterm_m@@$$FYMXPEBQ6MPEBXXZ0@Z;

	// Token: 0x040000AD RID: 173 RVA: 0x000226A8 File Offset: 0x0001F2A8
	public static method __m2mep@??$ResolveMethod@$$A6MPEBXXZ@ThisModule@<CrtImplementationDetails>@@$$FSMP6MPEBXXZP6MPEBXXZ@Z;

	// Token: 0x040000AE RID: 174 RVA: 0x000226C8 File Offset: 0x0001F2C8
	public static method __m2mep@?___CxxCallUnwindDtor@@$$J0YMXP6MXPEAX@Z0@Z;

	// Token: 0x040000AF RID: 175 RVA: 0x000226D8 File Offset: 0x0001F2D8
	public static method __m2mep@?___CxxCallUnwindDelDtor@@$$J0YMXP6MXPEAX@Z0@Z;

	// Token: 0x040000B0 RID: 176 RVA: 0x000226E8 File Offset: 0x0001F2E8
	public static method __m2mep@?___CxxCallUnwindVecDtor@@$$J0YMXP6MXPEAX_KHP6MX0@Z@Z01H2@Z;

	// Token: 0x040000B1 RID: 177 RVA: 0x0000CA60 File Offset: 0x0000A060
	public static $ArrayType$$$BY01Q6AXXZ ??_7type_info@@6B@;

	// Token: 0x040000B2 RID: 178 RVA: 0x0000CAD0 File Offset: 0x0000A0D0
	public static _GUID IID_IComThreadingInfo;

	// Token: 0x040000B3 RID: 179 RVA: 0x0000CAA0 File Offset: 0x0000A0A0
	public static _GUID IID_IUnknown;

	// Token: 0x040000B4 RID: 180 RVA: 0x0000CAB0 File Offset: 0x0000A0B0
	public static _GUID IID_IInitializeSpy;

	// Token: 0x040000B5 RID: 181 RVA: 0x0000E0B0 File Offset: 0x0000B6B0
	public static _GUID IID_IObjectContext;

	// Token: 0x040000B6 RID: 182 RVA: 0x0000DFA0 File Offset: 0x0000B5A0
	public static _GUID IID_IContextState;

	// Token: 0x040000B7 RID: 183 RVA: 0x0000E000 File Offset: 0x0000B600
	public static _GUID IID_IObjectContextInfo;

	// Token: 0x040000B8 RID: 184 RVA: 0x0000CCB0 File Offset: 0x0000A2B0
	public static _GUID IID_IGlobalInterfaceTable;

	// Token: 0x040000B9 RID: 185 RVA: 0x0000F320 File Offset: 0x0000C920
	public static _GUID IID_IServicedComponentInfo;

	// Token: 0x040000BA RID: 186 RVA: 0x0000F0C0 File Offset: 0x0000C6C0
	public static _GUID CLSID_StdGlobalInterfaceTable;

	// Token: 0x040000BB RID: 187 RVA: 0x0000F310 File Offset: 0x0000C910
	public static _GUID IID_IContextCallback;

	// Token: 0x040000BC RID: 188 RVA: 0x0000D300 File Offset: 0x0000A900
	public static _GUID IID_IEnterActivityWithNoLock;

	// Token: 0x040000BD RID: 189 RVA: 0x0000DD50 File Offset: 0x0000B350
	public static _GUID IID_IManagedActivationEvents;

	// Token: 0x040000BE RID: 190 RVA: 0x0000F330 File Offset: 0x0000C930
	public static _GUID IID_IRemoteDispatch;

	// Token: 0x040000BF RID: 191 RVA: 0x0000DF10 File Offset: 0x0000B510
	public static _GUID IID_ICrmMonitor;

	// Token: 0x040000C0 RID: 192 RVA: 0x0000DF60 File Offset: 0x0000B560
	public static _GUID IID_ICrmLogControl;

	// Token: 0x040000C1 RID: 193 RVA: 0x0000E2C0 File Offset: 0x0000B8C0
	public static _GUID CLSID_CRMRecoveryClerk;

	// Token: 0x040000C2 RID: 194 RVA: 0x0000E2D0 File Offset: 0x0000B8D0
	public static _GUID CLSID_CRMClerk;

	// Token: 0x040000C3 RID: 195 RVA: 0x0000DF30 File Offset: 0x0000B530
	public static _GUID IID_ICrmMonitorLogRecords;

	// Token: 0x040000C4 RID: 196 RVA: 0x0000DE20 File Offset: 0x0000B420
	public static _GUID IID_IServiceActivity;

	// Token: 0x040000C5 RID: 197 RVA: 0x0000DE60 File Offset: 0x0000B460
	public static _GUID IID_IServiceTrackerConfig;

	// Token: 0x040000C6 RID: 198 RVA: 0x0000DE80 File Offset: 0x0000B480
	public static _GUID IID_IServiceTransactionConfig;

	// Token: 0x040000C7 RID: 199 RVA: 0x0000DEB0 File Offset: 0x0000B4B0
	public static _GUID IID_IServiceInheritanceConfig;

	// Token: 0x040000C8 RID: 200 RVA: 0x0000DEA0 File Offset: 0x0000B4A0
	public static _GUID IID_IServiceThreadPoolConfig;

	// Token: 0x040000C9 RID: 201 RVA: 0x0000DEE0 File Offset: 0x0000B4E0
	public static _GUID IID_IServiceComTIIntrinsicsConfig;

	// Token: 0x040000CA RID: 202 RVA: 0x0000DED0 File Offset: 0x0000B4D0
	public static _GUID IID_IServiceSxsConfig;

	// Token: 0x040000CB RID: 203 RVA: 0x0000DE70 File Offset: 0x0000B470
	public static _GUID IID_IServiceSynchronizationConfig;

	// Token: 0x040000CC RID: 204 RVA: 0x0000E380 File Offset: 0x0000B980
	public static _GUID CLSID_CServiceConfig;

	// Token: 0x040000CD RID: 205 RVA: 0x0000DEF0 File Offset: 0x0000B4F0
	public static _GUID IID_IServiceIISIntrinsicsConfig;

	// Token: 0x040000CE RID: 206 RVA: 0x0000DE50 File Offset: 0x0000B450
	public static _GUID IID_IServicePartitionConfig;

	// Token: 0x040000CF RID: 207 RVA: 0x0000DE40 File Offset: 0x0000B440
	public static _GUID IID_IServiceCall;

	// Token: 0x040000D0 RID: 208 RVA: 0x0000C3B8 File Offset: 0x000099B8
	public static $ArrayType$$$BY0A@P6AXXZ __xc_z;

	// Token: 0x040000D1 RID: 209 RVA: 0x00022724 File Offset: 0x0001F324
	public static volatile uint __native_vcclrit_reason;

	// Token: 0x040000D2 RID: 210 RVA: 0x0000C3B0 File Offset: 0x000099B0
	public static $ArrayType$$$BY0A@P6AXXZ __xc_a;

	// Token: 0x040000D3 RID: 211 RVA: 0x0000C3C0 File Offset: 0x000099C0
	public static $ArrayType$$$BY0A@P6AHXZ __xi_a;

	// Token: 0x040000D4 RID: 212 RVA: 0x00023418 File Offset: 0x00020018
	public static volatile __enative_startup_state __native_startup_state;

	// Token: 0x040000D5 RID: 213 RVA: 0x0000C3D0 File Offset: 0x000099D0
	public static $ArrayType$$$BY0A@P6AHXZ __xi_z;

	// Token: 0x040000D6 RID: 214 RVA: 0x00023420 File Offset: 0x00020020
	public unsafe static void* __native_startup_lock;

	// Token: 0x040000D7 RID: 215 RVA: 0x00022720 File Offset: 0x0001F320
	public static volatile uint __native_dllmain_reason;
}
