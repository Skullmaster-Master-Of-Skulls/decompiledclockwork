using System;
using System.Diagnostics;
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
	// Token: 0x06000001 RID: 1 RVA: 0x001D6898 File Offset: 0x001D5C98
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

	// Token: 0x06000002 RID: 2 RVA: 0x001D6AF4 File Offset: 0x001D5EF4
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

	// Token: 0x06000003 RID: 3 RVA: 0x001D6B88 File Offset: 0x001D5F88
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

	// Token: 0x06000004 RID: 4 RVA: 0x001D6BF0 File Offset: 0x001D5FF0
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

	// Token: 0x06000005 RID: 5 RVA: 0x001D6C7C File Offset: 0x001D607C
	internal unsafe static int <CrtImplementationDetails>.DefaultDomain.DoNothing(void* cookie)
	{
		GC.KeepAlive(int.MaxValue);
		return 0;
	}

	// Token: 0x06000006 RID: 6 RVA: 0x001D6C9C File Offset: 0x001D609C
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

	// Token: 0x06000007 RID: 7 RVA: 0x001D6CF0 File Offset: 0x001D60F0
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

	// Token: 0x06000008 RID: 8 RVA: 0x001D6D70 File Offset: 0x001D6170
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

	// Token: 0x06000009 RID: 9 RVA: 0x002DD06C File Offset: 0x002DC46C
	internal static void ??__E?Initialized@CurrentDomain@<CrtImplementationDetails>@@$$Q2HA@@YMXXZ()
	{
		<Module>.?Initialized@CurrentDomain@<CrtImplementationDetails>@@$$Q2HA = 0;
	}

	// Token: 0x0600000A RID: 10 RVA: 0x002DD080 File Offset: 0x002DC480
	internal static void ??__E?Uninitialized@CurrentDomain@<CrtImplementationDetails>@@$$Q2HA@@YMXXZ()
	{
		<Module>.?Uninitialized@CurrentDomain@<CrtImplementationDetails>@@$$Q2HA = 0;
	}

	// Token: 0x0600000B RID: 11 RVA: 0x002DD094 File Offset: 0x002DC494
	internal static void ??__E?IsDefaultDomain@CurrentDomain@<CrtImplementationDetails>@@$$Q2_NA@@YMXXZ()
	{
		<Module>.?IsDefaultDomain@CurrentDomain@<CrtImplementationDetails>@@$$Q2_NA = false;
	}

	// Token: 0x0600000C RID: 12 RVA: 0x002DD0A8 File Offset: 0x002DC4A8
	internal static void ??__E?InitializedVtables@CurrentDomain@<CrtImplementationDetails>@@$$Q2W4State@Progress@2@A@@YMXXZ()
	{
		<Module>.?InitializedVtables@CurrentDomain@<CrtImplementationDetails>@@$$Q2W4State@Progress@2@A = (Progress.State)0;
	}

	// Token: 0x0600000D RID: 13 RVA: 0x002DD0BC File Offset: 0x002DC4BC
	internal static void ??__E?InitializedNative@CurrentDomain@<CrtImplementationDetails>@@$$Q2W4State@Progress@2@A@@YMXXZ()
	{
		<Module>.?InitializedNative@CurrentDomain@<CrtImplementationDetails>@@$$Q2W4State@Progress@2@A = (Progress.State)0;
	}

	// Token: 0x0600000E RID: 14 RVA: 0x002DD0D0 File Offset: 0x002DC4D0
	internal static void ??__E?InitializedPerProcess@CurrentDomain@<CrtImplementationDetails>@@$$Q2W4State@Progress@2@A@@YMXXZ()
	{
		<Module>.?InitializedPerProcess@CurrentDomain@<CrtImplementationDetails>@@$$Q2W4State@Progress@2@A = (Progress.State)0;
	}

	// Token: 0x0600000F RID: 15 RVA: 0x002DD0E4 File Offset: 0x002DC4E4
	internal static void ??__E?InitializedPerAppDomain@CurrentDomain@<CrtImplementationDetails>@@$$Q2W4State@Progress@2@A@@YMXXZ()
	{
		<Module>.?InitializedPerAppDomain@CurrentDomain@<CrtImplementationDetails>@@$$Q2W4State@Progress@2@A = (Progress.State)0;
	}

	// Token: 0x06000010 RID: 16 RVA: 0x001D6F58 File Offset: 0x001D6358
	internal unsafe static void <CrtImplementationDetails>.LanguageSupport.InitializeDefaultAppDomain(LanguageSupport* A_0)
	{
		string target = "The C++ module failed to load while attempting to initialize the default appdomain.\n";
		IntPtr value = new IntPtr(*A_0);
		((GCHandle)value).Target = target;
		<Module>.<CrtImplementationDetails>.DoCallBackInDefaultDomain(<Module>.__unep@?DoNothing@DefaultDomain@<CrtImplementationDetails>@@$$FCAJPEAX@Z, null);
	}

	// Token: 0x06000011 RID: 17 RVA: 0x001D6F90 File Offset: 0x001D6390
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

	// Token: 0x06000012 RID: 18 RVA: 0x001D7050 File Offset: 0x001D6450
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

	// Token: 0x06000013 RID: 19 RVA: 0x001D6DAC File Offset: 0x001D61AC
	internal static void <CrtImplementationDetails>.LanguageSupport.UninitializeAppDomain()
	{
		<Module>._app_exit_callback();
	}

	// Token: 0x06000014 RID: 20 RVA: 0x001D6DC0 File Offset: 0x001D61C0
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

	// Token: 0x06000015 RID: 21 RVA: 0x001D6DFC File Offset: 0x001D61FC
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

	// Token: 0x06000016 RID: 22 RVA: 0x001D6E58 File Offset: 0x001D6258
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

	// Token: 0x06000017 RID: 23 RVA: 0x001D7238 File Offset: 0x001D6638
	[DebuggerStepThrough]
	[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
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
			throw new ModuleLoadExceptionHandlerException("A nested exception occurred after the primary exception that caused the C++ module to fail to load.\n", innerException, nestedException);
		}
		catch (object obj)
		{
			throw new ModuleLoadExceptionHandlerException("A nested exception occurred after the primary exception that caused the C++ module to fail to load.\n", innerException, null);
		}
	}

	// Token: 0x06000018 RID: 24 RVA: 0x001D72B8 File Offset: 0x001D66B8
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
		catch (Exception innerException)
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

	// Token: 0x06000019 RID: 25 RVA: 0x001D73A4 File Offset: 0x001D67A4
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

	// Token: 0x0600001A RID: 26 RVA: 0x001D7378 File Offset: 0x001D6778
	internal unsafe static void <CrtImplementationDetails>.LanguageSupport.{dtor}(LanguageSupport* A_0)
	{
		IntPtr value = new IntPtr(*A_0);
		((GCHandle)value).Free();
		*A_0 = 0L;
	}

	// Token: 0x0600001B RID: 27 RVA: 0x001D6E94 File Offset: 0x001D6294
	[DebuggerStepThrough]
	internal unsafe static gcroot<System::String\u0020^>* =(gcroot<System::String\u0020^>* A_0, string t)
	{
		IntPtr value = new IntPtr(*A_0);
		((GCHandle)value).Target = t;
		return A_0;
	}

	// Token: 0x0600001C RID: 28 RVA: 0x001D6EBC File Offset: 0x001D62BC
	internal unsafe static string PE$AAVString@System@@(gcroot<System::String\u0020^>* A_0)
	{
		IntPtr value = new IntPtr(*A_0);
		return ((GCHandle)value).Target;
	}

	// Token: 0x0600001D RID: 29 RVA: 0x001D7414 File Offset: 0x001D6814
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

	// Token: 0x0600001E RID: 30 RVA: 0x001D7480 File Offset: 0x001D6880
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

	// Token: 0x0600001F RID: 31 RVA: 0x001D74C0 File Offset: 0x001D68C0
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

	// Token: 0x06000020 RID: 32 RVA: 0x001D7508 File Offset: 0x001D6908
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

	// Token: 0x06000021 RID: 33 RVA: 0x001D7678 File Offset: 0x001D6A78
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

	// Token: 0x06000022 RID: 34 RVA: 0x001D76FC File Offset: 0x001D6AFC
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

	// Token: 0x06000023 RID: 35 RVA: 0x001D75AC File Offset: 0x001D69AC
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

	// Token: 0x06000024 RID: 36 RVA: 0x001D777C File Offset: 0x001D6B7C
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

	// Token: 0x06000025 RID: 37 RVA: 0x001D77AC File Offset: 0x001D6BAC
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

	// Token: 0x06000026 RID: 38 RVA: 0x001D77D4 File Offset: 0x001D6BD4
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

	// Token: 0x06000027 RID: 39 RVA: 0x001D782C File Offset: 0x001D6C2C
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

	// Token: 0x06000028 RID: 40 RVA: 0x001D50C0 File Offset: 0x001D44C0
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig)]
	public unsafe static extern void* NativeGetData(int*);

	// Token: 0x06000029 RID: 41 RVA: 0x001D50F0 File Offset: 0x001D44F0
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig)]
	[return: MarshalAs(UnmanagedType.U1)]
	public unsafe static extern bool NativeSetData(void*, int);

	// Token: 0x0600002A RID: 42 RVA: 0x001D51D0 File Offset: 0x001D45D0
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig)]
	public unsafe static extern IUnknown* NativeGetDefaultAppDomain();

	// Token: 0x0600002B RID: 43 RVA: 0x001C6A80 File Offset: 0x001C5E80
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig)]
	public unsafe static extern int SNIServerEnumRead(void*, ushort*, int, int*);

	// Token: 0x0600002C RID: 44 RVA: 0x001C8080 File Offset: 0x001C7480
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig)]
	public unsafe static extern void* SNIServerEnumOpen(ushort*, int);

	// Token: 0x0600002D RID: 45 RVA: 0x0019DF30 File Offset: 0x0019D330
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig)]
	public unsafe static extern SNI_Conn* SNIPacketGetConnection(SNI_Packet*);

	// Token: 0x0600002E RID: 46 RVA: 0x001B7B00 File Offset: 0x001B6F00
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig)]
	public unsafe static extern uint SNIInitialize(void*);

	// Token: 0x0600002F RID: 47 RVA: 0x001B4900 File Offset: 0x001B3D00
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig)]
	public unsafe static extern uint SNIWriteSync(SNI_Conn*, SNI_Packet*, SNI_ProvInfo*);

	// Token: 0x06000030 RID: 48 RVA: 0x001B5000 File Offset: 0x001B4400
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig)]
	public unsafe static extern uint SNIRemoveProvider(SNI_Conn*, ProviderNum);

	// Token: 0x06000031 RID: 49 RVA: 0x001BB4D0 File Offset: 0x001BA8D0
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig)]
	public unsafe static extern uint SNISecGenClientContext(SNI_Conn*, byte*, uint, byte*, uint*, int*, sbyte*, uint, ushort*, ushort*);

	// Token: 0x06000032 RID: 50 RVA: 0x001BA320 File Offset: 0x001B9720
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig)]
	public unsafe static extern void SNIGetLastError(SNI_ERROR*);

	// Token: 0x06000033 RID: 51 RVA: 0x001B9790 File Offset: 0x001B8B90
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig)]
	public unsafe static extern uint SNIOpen(SNI_CONSUMER_INFO*, sbyte*, void*, SNI_Conn**, int);

	// Token: 0x06000034 RID: 52 RVA: 0x001B65D0 File Offset: 0x001B59D0
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig)]
	public unsafe static extern uint SNIAddProvider(SNI_Conn*, ProviderNum, void*);

	// Token: 0x06000035 RID: 53 RVA: 0x001A7B10 File Offset: 0x001A6F10
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig)]
	public unsafe static extern uint SNIOpenSyncEx(SNI_CLIENT_CONSUMER_INFO*, SNI_Conn**);

	// Token: 0x06000036 RID: 54 RVA: 0x001B8050 File Offset: 0x001B7450
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig)]
	public unsafe static extern uint SNIReadAsync(SNI_Conn*, SNI_Packet**, void*);

	// Token: 0x06000037 RID: 55 RVA: 0x0019DEF0 File Offset: 0x0019D2F0
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig)]
	public unsafe static extern void SNIPacketSetData(SNI_Packet*, byte*, uint);

	// Token: 0x06000038 RID: 56 RVA: 0x0019DEB0 File Offset: 0x0019D2B0
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig)]
	public unsafe static extern void SNIPacketReset(SNI_Conn*, uint, SNI_Packet*);

	// Token: 0x06000039 RID: 57 RVA: 0x001B4800 File Offset: 0x001B3C00
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig)]
	public unsafe static extern uint SNIReadSync(SNI_Conn*, SNI_Packet**, int);

	// Token: 0x0600003A RID: 58 RVA: 0x001B4BD0 File Offset: 0x001B3FD0
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig)]
	public unsafe static extern uint SNIWaitForSSLHandshakeToComplete(SNI_Conn*, uint);

	// Token: 0x0600003B RID: 59 RVA: 0x0019DED0 File Offset: 0x0019D2D0
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig)]
	public unsafe static extern void SNIPacketGetData(SNI_Packet*, byte**, uint*);

	// Token: 0x0600003C RID: 60 RVA: 0x001B4CA0 File Offset: 0x001B40A0
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig)]
	public unsafe static extern uint SNISetInfo(SNI_Conn*, uint, void*);

	// Token: 0x0600003D RID: 61 RVA: 0x001B9BB0 File Offset: 0x001B8FB0
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig)]
	public unsafe static extern uint SNIWriteAsync(SNI_Conn*, SNI_Packet*, SNI_ProvInfo*);

	// Token: 0x0600003E RID: 62 RVA: 0x001B49E0 File Offset: 0x001B3DE0
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig)]
	public unsafe static extern uint SNIQueryInfo(uint, void*);

	// Token: 0x0600003F RID: 63 RVA: 0x001BC730 File Offset: 0x001BBB30
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig)]
	public unsafe static extern uint SNISecInitPackage(uint*);

	// Token: 0x06000040 RID: 64 RVA: 0x001D68F0 File Offset: 0x001D5CF0
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig)]
	public unsafe static extern void* _getFiberPtrId();

	// Token: 0x06000041 RID: 65 RVA: 0x001D801A File Offset: 0x001D741A
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged)]
	public static extern void _amsg_exit(int);

	// Token: 0x06000042 RID: 66 RVA: 0x001D8060 File Offset: 0x001D7460
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig)]
	public static extern void __security_init_cookie();

	// Token: 0x06000043 RID: 67 RVA: 0x001D829A File Offset: 0x001D769A
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged)]
	public static extern void Sleep(uint);

	// Token: 0x06000044 RID: 68 RVA: 0x001D8294 File Offset: 0x001D7694
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged)]
	public unsafe static extern int CorBindToRuntimeEx(char*, char*, uint, _GUID*, _GUID*, void**);

	// Token: 0x06000045 RID: 69 RVA: 0x001D82AC File Offset: 0x001D76AC
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged)]
	public static extern void _cexit();

	// Token: 0x06000046 RID: 70 RVA: 0x001D82B8 File Offset: 0x001D76B8
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged)]
	public unsafe static extern int CoCreateInstance(_GUID*, IUnknown*, uint, _GUID*, void**);

	// Token: 0x06000047 RID: 71 RVA: 0x001D7F0E File Offset: 0x001D730E
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged)]
	public unsafe static extern void* _encode_pointer(void*);

	// Token: 0x06000048 RID: 72 RVA: 0x001D8014 File Offset: 0x001D7414
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged)]
	public unsafe static extern void* _decode_pointer(void*);

	// Token: 0x06000049 RID: 73 RVA: 0x001D800E File Offset: 0x001D740E
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged)]
	public unsafe static extern void* _encoded_null();

	// Token: 0x0600004A RID: 74 RVA: 0x001D82B2 File Offset: 0x001D76B2
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged)]
	public unsafe static extern int __FrameUnwindFilter(_EXCEPTION_POINTERS*);

	// Token: 0x04000001 RID: 1 RVA: 0x002EAE40 File Offset: 0x002E8C40
	internal static volatile int ?lock@SqlDependencyProcessDispatcherStorage@@0JC;

	// Token: 0x04000002 RID: 2 RVA: 0x00015458 File Offset: 0x00014858
	internal static _GUID IID_ICorRuntimeHost;

	// Token: 0x04000003 RID: 3 RVA: 0x00015448 File Offset: 0x00014848
	internal static _GUID CLSID_CorRuntimeHost;

	// Token: 0x04000004 RID: 4 RVA: 0x002EB320 File Offset: 0x002E9120
	internal static int ?size@SqlDependencyProcessDispatcherStorage@@0HA;

	// Token: 0x04000005 RID: 5 RVA: 0x002EB378 File Offset: 0x002E9178
	internal unsafe static void* ?data@SqlDependencyProcessDispatcherStorage@@0PEAXEA;

	// Token: 0x04000006 RID: 6 RVA: 0x00015468 File Offset: 0x00014868
	unsafe static int** __unep@?SNIServerEnumClose@@$$J0YAXPEAX@Z;

	// Token: 0x04000007 RID: 7 RVA: 0x00015470 File Offset: 0x00014870
	unsafe static int** __unep@?SNIClose@@$$J0YAKPEAVSNI_Conn@@@Z;

	// Token: 0x04000008 RID: 8 RVA: 0x00015478 File Offset: 0x00014878
	unsafe static int** __unep@?SNIPacketAllocate@@$$J0YAPEAVSNI_Packet@@PEAVSNI_Conn@@K@Z;

	// Token: 0x04000009 RID: 9 RVA: 0x00015480 File Offset: 0x00014880
	unsafe static int** __unep@?SNIPacketRelease@@$$J0YAXPEAVSNI_Packet@@@Z;

	// Token: 0x0400000A RID: 10 RVA: 0x00015488 File Offset: 0x00014888
	unsafe static int** __unep@?SNITerminate@@$$J0YAKXZ;

	// Token: 0x0400000B RID: 11 RVA: 0x000155C8 File Offset: 0x000149C8
	internal static __s_GUID _GUID_90f1a06e_7712_4762_86b5_7a5eba6bdb02;

	// Token: 0x0400000C RID: 12 RVA: 0x00015588 File Offset: 0x00014988
	internal static __s_GUID _GUID_cb2f6722_ab3a_11d2_9c40_00c04fa30a3e;

	// Token: 0x0400000D RID: 13 RVA: 0x00001638 File Offset: 0x00000A38
	internal static $ArrayType$$$BY00Q6MPEBXXZ __xc_mp_z;

	// Token: 0x0400000E RID: 14 RVA: 0x00015598 File Offset: 0x00014998
	internal static __s_GUID _GUID_00000000_0000_0000_c000_000000000046;

	// Token: 0x0400000F RID: 15 RVA: 0x00001640 File Offset: 0x00000A40
	internal static $ArrayType$$$BY00Q6MPEBXXZ __xi_vt_a;

	// Token: 0x04000010 RID: 16
	[FixedAddressValueType]
	internal static Progress.State ?InitializedVtables@CurrentDomain@<CrtImplementationDetails>@@$$Q2W4State@Progress@2@A;

	// Token: 0x04000011 RID: 17 RVA: 0x00001608 File Offset: 0x00000A08
	internal static method ?InitializedVtables$initializer$@CurrentDomain@<CrtImplementationDetails>@@$$Q2P6MXXZEA;

	// Token: 0x04000012 RID: 18
	[FixedAddressValueType]
	internal static bool ?IsDefaultDomain@CurrentDomain@<CrtImplementationDetails>@@$$Q2_NA;

	// Token: 0x04000013 RID: 19 RVA: 0x00001600 File Offset: 0x00000A00
	internal static method ?IsDefaultDomain$initializer$@CurrentDomain@<CrtImplementationDetails>@@$$Q2P6MXXZEA;

	// Token: 0x04000014 RID: 20 RVA: 0x000015E8 File Offset: 0x000009E8
	internal static $ArrayType$$$BY00Q6MPEBXXZ __xc_ma_a;

	// Token: 0x04000015 RID: 21
	[FixedAddressValueType]
	internal static Progress.State ?InitializedPerAppDomain@CurrentDomain@<CrtImplementationDetails>@@$$Q2W4State@Progress@2@A;

	// Token: 0x04000016 RID: 22 RVA: 0x00001620 File Offset: 0x00000A20
	internal static method ?InitializedPerAppDomain$initializer$@CurrentDomain@<CrtImplementationDetails>@@$$Q2P6MXXZEA;

	// Token: 0x04000017 RID: 23 RVA: 0x00001628 File Offset: 0x00000A28
	internal static $ArrayType$$$BY00Q6MPEBXXZ __xc_ma_z;

	// Token: 0x04000018 RID: 24
	[FixedAddressValueType]
	internal static Progress.State ?InitializedNative@CurrentDomain@<CrtImplementationDetails>@@$$Q2W4State@Progress@2@A;

	// Token: 0x04000019 RID: 25 RVA: 0x00001610 File Offset: 0x00000A10
	internal static method ?InitializedNative$initializer$@CurrentDomain@<CrtImplementationDetails>@@$$Q2P6MXXZEA;

	// Token: 0x0400001A RID: 26 RVA: 0x000155A8 File Offset: 0x000149A8
	internal static __s_GUID _GUID_cb2f6723_ab3a_11d2_9c40_00c04fa30a3e;

	// Token: 0x0400001B RID: 27 RVA: 0x00001648 File Offset: 0x00000A48
	internal static $ArrayType$$$BY00Q6MPEBXXZ __xi_vt_z;

	// Token: 0x0400001C RID: 28
	[FixedAddressValueType]
	internal static int ?Uninitialized@CurrentDomain@<CrtImplementationDetails>@@$$Q2HA;

	// Token: 0x0400001D RID: 29 RVA: 0x000015F8 File Offset: 0x000009F8
	internal static method ?Uninitialized$initializer$@CurrentDomain@<CrtImplementationDetails>@@$$Q2P6MXXZEA;

	// Token: 0x0400001E RID: 30
	[FixedAddressValueType]
	internal static int ?Initialized@CurrentDomain@<CrtImplementationDetails>@@$$Q2HA;

	// Token: 0x0400001F RID: 31 RVA: 0x000015F0 File Offset: 0x000009F0
	internal static method ?Initialized$initializer$@CurrentDomain@<CrtImplementationDetails>@@$$Q2P6MXXZEA;

	// Token: 0x04000020 RID: 32 RVA: 0x002EB8EF File Offset: 0x002E96EF
	internal static bool ?InitializedPerProcess@DefaultDomain@<CrtImplementationDetails>@@2_NA;

	// Token: 0x04000021 RID: 33
	[FixedAddressValueType]
	internal static Progress.State ?InitializedPerProcess@CurrentDomain@<CrtImplementationDetails>@@$$Q2W4State@Progress@2@A;

	// Token: 0x04000022 RID: 34 RVA: 0x002EB8EC File Offset: 0x002E96EC
	internal static bool ?Entered@DefaultDomain@<CrtImplementationDetails>@@2_NA;

	// Token: 0x04000023 RID: 35 RVA: 0x002EB8ED File Offset: 0x002E96ED
	internal static bool ?InitializedNative@DefaultDomain@<CrtImplementationDetails>@@2_NA;

	// Token: 0x04000024 RID: 36 RVA: 0x002EB8E8 File Offset: 0x002E96E8
	internal static int ?Count@AllDomains@<CrtImplementationDetails>@@2HA;

	// Token: 0x04000025 RID: 37 RVA: 0x002E463C File Offset: 0x002E243C
	internal static TriBool.State ?hasNative@DefaultDomain@<CrtImplementationDetails>@@0W4State@TriBool@2@A;

	// Token: 0x04000026 RID: 38 RVA: 0x002E4638 File Offset: 0x002E2438
	internal static TriBool.State ?hasPerProcess@DefaultDomain@<CrtImplementationDetails>@@0W4State@TriBool@2@A;

	// Token: 0x04000027 RID: 39 RVA: 0x002EB8EE File Offset: 0x002E96EE
	internal static bool ?InitializedNativeFromCCTOR@DefaultDomain@<CrtImplementationDetails>@@2_NA;

	// Token: 0x04000028 RID: 40 RVA: 0x00001630 File Offset: 0x00000A30
	internal static $ArrayType$$$BY00Q6MPEBXXZ __xc_mp_a;

	// Token: 0x04000029 RID: 41 RVA: 0x000155B8 File Offset: 0x000149B8
	internal static __s_GUID _GUID_90f1a06c_7712_4762_86b5_7a5eba6bdb02;

	// Token: 0x0400002A RID: 42 RVA: 0x00001618 File Offset: 0x00000A18
	internal static method ?InitializedPerProcess$initializer$@CurrentDomain@<CrtImplementationDetails>@@$$Q2P6MXXZEA;

	// Token: 0x0400002B RID: 43 RVA: 0x002E4648 File Offset: 0x002E2448
	public static method __m2mep@?DoNothing@DefaultDomain@<CrtImplementationDetails>@@$$FCAJPEAX@Z;

	// Token: 0x0400002C RID: 44 RVA: 0x002E4658 File Offset: 0x002E2458
	public static method __m2mep@?_UninitializeDefaultDomain@LanguageSupport@<CrtImplementationDetails>@@$$FCAJPEAX@Z;

	// Token: 0x0400002D RID: 45 RVA: 0x000155D8 File Offset: 0x000149D8
	public unsafe static int** __unep@?DoNothing@DefaultDomain@<CrtImplementationDetails>@@$$FCAJPEAX@Z;

	// Token: 0x0400002E RID: 46 RVA: 0x000155E0 File Offset: 0x000149E0
	public unsafe static int** __unep@?_UninitializeDefaultDomain@LanguageSupport@<CrtImplementationDetails>@@$$FCAJPEAX@Z;

	// Token: 0x0400002F RID: 47
	[FixedAddressValueType]
	internal static ulong __exit_list_size_app_domain;

	// Token: 0x04000030 RID: 48
	[FixedAddressValueType]
	internal unsafe static method* __onexitbegin_app_domain;

	// Token: 0x04000031 RID: 49 RVA: 0x002EBAD0 File Offset: 0x002E98D0
	internal static ulong __exit_list_size;

	// Token: 0x04000032 RID: 50
	[FixedAddressValueType]
	internal unsafe static method* __onexitend_app_domain;

	// Token: 0x04000033 RID: 51 RVA: 0x002EBAC0 File Offset: 0x002E98C0
	internal unsafe static method* __onexitbegin_m;

	// Token: 0x04000034 RID: 52 RVA: 0x002EBAC8 File Offset: 0x002E98C8
	internal unsafe static method* __onexitend_m;

	// Token: 0x04000035 RID: 53
	[FixedAddressValueType]
	internal static int ?_ref_count@AtExitLock@<CrtImplementationDetails>@@$$Q0HA;

	// Token: 0x04000036 RID: 54
	[FixedAddressValueType]
	internal unsafe static void* ?_lock@AtExitLock@<CrtImplementationDetails>@@$$Q0PEAXEA;

	// Token: 0x04000037 RID: 55 RVA: 0x00015660 File Offset: 0x00014A60
	public static _GUID IID_IChapteredRowset;

	// Token: 0x04000038 RID: 56 RVA: 0x00015990 File Offset: 0x00014D90
	public static _GUID IID_ITransactionLocal;

	// Token: 0x04000039 RID: 57 RVA: 0x0000D39C File Offset: 0x0000C79C
	public static uint SNI_MAX_COMPOSED_SPN;

	// Token: 0x0400003A RID: 58 RVA: 0x000015C8 File Offset: 0x000009C8
	public static $ArrayType$$$BY0A@P6AXXZ __xc_z;

	// Token: 0x0400003B RID: 59 RVA: 0x002E4674 File Offset: 0x002E2474
	public static volatile uint __native_vcclrit_reason;

	// Token: 0x0400003C RID: 60 RVA: 0x000015B8 File Offset: 0x000009B8
	public static $ArrayType$$$BY0A@P6AXXZ __xc_a;

	// Token: 0x0400003D RID: 61 RVA: 0x000015D0 File Offset: 0x000009D0
	public static $ArrayType$$$BY0A@P6AHXZ __xi_a;

	// Token: 0x0400003E RID: 62 RVA: 0x002EC090 File Offset: 0x002E9E90
	public static volatile __enative_startup_state __native_startup_state;

	// Token: 0x0400003F RID: 63 RVA: 0x000015E0 File Offset: 0x000009E0
	public static $ArrayType$$$BY0A@P6AHXZ __xi_z;

	// Token: 0x04000040 RID: 64 RVA: 0x002EC098 File Offset: 0x002E9E98
	public unsafe static void* __native_startup_lock;

	// Token: 0x04000041 RID: 65 RVA: 0x002E4670 File Offset: 0x002E2470
	public static volatile uint __native_dllmain_reason;
}
