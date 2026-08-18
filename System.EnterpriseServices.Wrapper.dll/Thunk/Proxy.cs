using System;
using System.Collections;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Remoting;
using System.Threading;
using <CppImplementationDetails>;
using Microsoft.Win32;

namespace System.EnterpriseServices.Thunk
{
	// Token: 0x02000056 RID: 86
	internal class Proxy
	{
		// Token: 0x060000AE RID: 174 RVA: 0x000029C8 File Offset: 0x00001DC8
		private Proxy()
		{
		}

		// Token: 0x060000AF RID: 175 RVA: 0x00002F38 File Offset: 0x00002338
		[return: MarshalAs(UnmanagedType.U1)]
		private unsafe static bool CheckRegistered(Guid id, Assembly assembly, [MarshalAs(UnmanagedType.U1)] bool checkCache, [MarshalAs(UnmanagedType.U1)] bool cacheOnly)
		{
			if (checkCache && Proxy._regCache[assembly] != null)
			{
				return true;
			}
			if (cacheOnly)
			{
				return false;
			}
			bool flag = false;
			string text = new string((char*)(&<Module>.?A0xf7eb705c.unnamed-global-6)) + id.ToString() + new string((sbyte*)(&<Module>.?A0xf7eb705c.unnamed-global-5));
			RegistryKey registryKey = Registry.ClassesRoot.OpenSubKey(text, false);
			if (registryKey != null)
			{
				Proxy._regCache[assembly] = bool.TrueString;
			}
			else if (Proxy.IsWin64(ref flag))
			{
				IntPtr hglobal = Marshal.StringToHGlobalUni(text);
				char* ptr = (char*)hglobal.ToPointer();
				int num = flag ? 256 : 512;
				HKEY__* ptr2;
				bool flag2 = <Module>.RegOpenKeyExW(-2147483648L, (char*)ptr, 0, num | 131097, &ptr2) != null;
				Marshal.FreeHGlobal(hglobal);
				if (flag2)
				{
					return false;
				}
				<Module>.RegCloseKey(ptr2);
				return true;
			}
			return ((registryKey != null) ? 1 : 0) != 0;
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x00003CA8 File Offset: 0x000030A8
		private static void LazyRegister(Guid id, Type serverType, [MarshalAs(UnmanagedType.U1)] bool checkCache)
		{
			if (serverType.Assembly != Proxy._thisAssembly)
			{
				Assembly assembly = serverType.Assembly;
				if (!checkCache || Proxy._regCache[assembly] == null)
				{
					Proxy._regmutex.WaitOne();
					try
					{
						if (!Proxy.CheckRegistered(id, serverType.Assembly, checkCache, false))
						{
							Proxy.RegisterAssembly(serverType.Assembly);
						}
					}
					finally
					{
						Proxy._regmutex.ReleaseMutex();
					}
				}
			}
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x00003008 File Offset: 0x00002408
		private unsafe static void RegisterAssembly(Assembly assembly)
		{
			try
			{
				((IThunkInstallation)Activator.CreateInstance(Type.GetType(new string((char*)(&<Module>.?A0xf7eb705c.unnamed-global-7))))).DefaultInstall(assembly.Location);
			}
			finally
			{
				Proxy._regCache[assembly] = bool.TrueString;
			}
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x00002E58 File Offset: 0x00002258
		[return: MarshalAs(UnmanagedType.U1)]
		private unsafe static bool IsWin64(bool* A_0)
		{
			if (<Module>.?A0xf7eb705c.?fInit@?1??IsWin64@Proxy@Thunk@EnterpriseServices@System@@CM_NAEA_N@Z@4HC == 0)
			{
				*A_0 = 0;
				method procAddress = <Module>.GetProcAddress(<Module>.GetModuleHandleW((char*)(&<Module>.?A0xf7eb705c.unnamed-global-4)), (sbyte*)(&<Module>.?A0xf7eb705c.unnamed-global-3));
				int num;
				if (procAddress != null)
				{
					_SYSTEM_INFO system_INFO;
					calli(System.Void modopt(System.Runtime.CompilerServices.CallConvCdecl)(_SYSTEM_INFO*), ref system_INFO, procAddress);
					if (system_INFO != 6 && system_INFO != 9)
					{
						num = 0;
					}
					else
					{
						int num2 = 0;
						method procAddress2 = <Module>.GetProcAddress(<Module>.GetModuleHandleW((char*)(&<Module>.?A0xf7eb705c.unnamed-global-2)), (sbyte*)(&<Module>.?A0xf7eb705c.unnamed-global-1));
						if (procAddress2 == null)
						{
							num2 = 0;
						}
						else if (!calli(System.Int32 modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.Void*,System.Int32*), <Module>.GetCurrentProcess(), ref num2, procAddress2))
						{
							num2 = 0;
						}
						else if (num2 == 1)
						{
							*A_0 = 1;
						}
						num = 1;
					}
				}
				else
				{
					num = 0;
				}
				<Module>.?A0xf7eb705c.?fWin64@?1??IsWin64@Proxy@Thunk@EnterpriseServices@System@@CM_NAEA_N@Z@4HC = num;
				<Module>.?A0xf7eb705c.?fIsWow@?1??IsWin64@Proxy@Thunk@EnterpriseServices@System@@CM_NAEA_N@Z@4HC = ((*A_0 != 0) ? 1 : 0);
				<Module>.?A0xf7eb705c.?fInit@?1??IsWin64@Proxy@Thunk@EnterpriseServices@System@@CM_NAEA_N@Z@4HC = 1;
				return num != 0;
			}
			byte b = (<Module>.?A0xf7eb705c.?fIsWow@?1??IsWin64@Proxy@Thunk@EnterpriseServices@System@@CM_NAEA_N@Z@4HC != 0) ? 1 : 0;
			*A_0 = b;
			return <Module>.?A0xf7eb705c.?fWin64@?1??IsWin64@Proxy@Thunk@EnterpriseServices@System@@CM_NAEA_N@Z@4HC != 0;
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x00002DE8 File Offset: 0x000021E8
		private unsafe static void IsWow64ProcessInternal(int* A_0)
		{
			method procAddress = <Module>.GetProcAddress(<Module>.GetModuleHandleW((char*)(&<Module>.?A0xf7eb705c.unnamed-global-2)), (sbyte*)(&<Module>.?A0xf7eb705c.unnamed-global-1));
			if (procAddress == null)
			{
				*A_0 = 0;
			}
			else if (!calli(System.Int32 modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.Void*,System.Int32*), <Module>.GetCurrentProcess(), A_0, procAddress))
			{
				*A_0 = 0;
			}
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x00002E28 File Offset: 0x00002228
		[return: MarshalAs(UnmanagedType.U1)]
		private unsafe static bool GetNativeSystemInfoInternal(_SYSTEM_INFO* A_0)
		{
			method procAddress = <Module>.GetProcAddress(<Module>.GetModuleHandleW((char*)(&<Module>.?A0xf7eb705c.unnamed-global-4)), (sbyte*)(&<Module>.?A0xf7eb705c.unnamed-global-3));
			if (procAddress == null)
			{
				return false;
			}
			calli(System.Void modopt(System.Runtime.CompilerServices.CallConvCdecl)(_SYSTEM_INFO*), A_0, procAddress);
			return true;
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x00002B38 File Offset: 0x00001F38
		public unsafe static void Init()
		{
			long num = (long)stackalloc byte[<Module>.__CxxQueryExceptionSize()];
			if (Thread.CurrentThread.ApartmentState == ApartmentState.Unknown)
			{
				Thread.CurrentThread.ApartmentState = ApartmentState.MTA;
			}
			if (!Proxy._fInit)
			{
				uint exceptionCode;
				try
				{
					IntPtr hToken = IntPtr.Zero;
					if (Proxy._classSyncRoot == null)
					{
						object value = new object();
						Interlocked.CompareExchange(ref Proxy._classSyncRoot, value, null);
					}
					lock (Proxy._classSyncRoot)
					{
						try
						{
							hToken = Security.SuspendImpersonation();
							if (!Proxy._fInit)
							{
								Proxy._regCache = new Hashtable();
								IGlobalInterfaceTable* pGIT = null;
								int num2 = <Module>.CoCreateInstance(ref <Module>.CLSID_StdGlobalInterfaceTable, null, 1, ref <Module>.IID_IGlobalInterfaceTable, (void**)(&pGIT));
								Proxy._pGIT = pGIT;
								if (num2 < 0)
								{
									Marshal.ThrowExceptionForHR(num2);
								}
								Proxy._thisAssembly = Assembly.GetExecutingAssembly();
								Proxy._regmutex = new Mutex(false, new string((sbyte*)(&<Module>.?A0xf7eb705c.unnamed-global-0)) + RemotingConfiguration.ProcessId);
								Thread.MemoryBarrier();
								Proxy._fInit = true;
							}
						}
						finally
						{
							Security.ResumeImpersonation(hToken);
						}
					}
				}
				catch when (delegate
				{
					// Failed to create a 'catch-when' expression
					exceptionCode = (uint)Marshal.GetExceptionCode();
					endfilter(<Module>.__CxxExceptionFilter(Marshal.GetExceptionPointers(), null, 0, null) != null);
				})
				{
					uint num3 = 0U;
					<Module>.__CxxRegisterExceptionObject(Marshal.GetExceptionPointers(), num);
					try
					{
						try
						{
							<Module>._CxxThrowException(null, null);
							goto IL_12E;
						}
						catch when (delegate
						{
							// Failed to create a 'catch-when' expression
							num3 = <Module>.__CxxDetectRethrow(Marshal.GetExceptionPointers());
							endfilter(num3 != 0U);
						})
						{
						}
						if (num3 != 0U)
						{
							throw;
						}
						IL_12E:;
					}
					finally
					{
						<Module>.__CxxUnregisterExceptionObject(num, (int)num3);
					}
				}
			}
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x00002D08 File Offset: 0x00002108
		public unsafe static int StoreObject(IntPtr ptr)
		{
			Proxy.Init();
			IUnknown* ptr2 = ptr.ToInt64();
			long num = *(long*)Proxy._pGIT + 24L;
			uint result;
			int num2 = calli(System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,IUnknown*,_GUID modopt(System.Runtime.CompilerServices.IsConst)* modopt(System.Runtime.CompilerServices.IsImplicitlyDereferenced),System.UInt32 modopt(System.Runtime.CompilerServices.IsLong)*), Proxy._pGIT, ptr2, ref <Module>.IID_IUnknown, ref result, *num);
			if (num2 < 0)
			{
				Marshal.ThrowExceptionForHR(num2);
			}
			return (int)result;
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x00002D58 File Offset: 0x00002158
		public unsafe static IntPtr GetObject(int cookie)
		{
			Proxy.Init();
			IUnknown* value = null;
			long num = *(long*)Proxy._pGIT + 40L;
			int num2 = calli(System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,System.UInt32 modopt(System.Runtime.CompilerServices.IsLong),_GUID modopt(System.Runtime.CompilerServices.IsConst)* modopt(System.Runtime.CompilerServices.IsImplicitlyDereferenced),System.Void**), Proxy._pGIT, cookie, ref <Module>.IID_IUnknown, ref value, *num);
			if (num2 < 0)
			{
				Marshal.ThrowExceptionForHR(num2);
			}
			IntPtr result = new IntPtr(value);
			return result;
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x00002DA8 File Offset: 0x000021A8
		public unsafe static void RevokeObject(int cookie)
		{
			Proxy.Init();
			long num = *(long*)Proxy._pGIT + 32L;
			int num2 = calli(System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,System.UInt32 modopt(System.Runtime.CompilerServices.IsLong)), Proxy._pGIT, cookie, *num);
			if (num2 < 0)
			{
				Marshal.ThrowExceptionForHR(num2);
			}
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x00003D38 File Offset: 0x00003138
		public unsafe static IntPtr CoCreateObject(Type serverType, [MarshalAs(UnmanagedType.U1)] bool bQuerySCInfo, ref bool bIsAnotherProcess, ref string uri)
		{
			Proxy.Init();
			IUnknown* ptr = null;
			bool flag = true;
			Guid id = Marshal.GenerateGuidForType(serverType);
			do
			{
				IUnknown* ptr2 = null;
				IServicedComponentInfo* ptr3 = null;
				tagSAFEARRAY* ptr4 = null;
				try
				{
					Proxy.LazyRegister(id, serverType, flag);
					_GUID guid;
					cpblk(ref guid, ref id, 16);
					$ArrayType$$$BY01UtagMULTI_QI@@ $ArrayType$$$BY01UtagMULTI_QI@@ = 0L;
					initblk(ref $ArrayType$$$BY01UtagMULTI_QI@@ + 8, 0, 40L);
					$ArrayType$$$BY01UtagMULTI_QI@@ = ref <Module>.IID_IUnknown;
					*(ref $ArrayType$$$BY01UtagMULTI_QI@@ + 24) = ref <Module>.IID_IServicedComponentInfo;
					int num;
					if (bQuerySCInfo && !IdentityManager.Enabled)
					{
						num = 2;
					}
					else
					{
						num = 1;
					}
					int num2 = <Module>.CoCreateInstanceEx(ref guid, null, 23, null, num, (tagMULTI_QI*)(&$ArrayType$$$BY01UtagMULTI_QI@@));
					if (num2 >= 0)
					{
						ptr2 = ((*(ref $ArrayType$$$BY01UtagMULTI_QI@@ + 16) >= 0) ? (*(ref $ArrayType$$$BY01UtagMULTI_QI@@ + 8)) : ptr2);
						if (bQuerySCInfo && !IdentityManager.Enabled)
						{
							ptr3 = ((*(ref $ArrayType$$$BY01UtagMULTI_QI@@ + 40) >= 0) ? (*(ref $ArrayType$$$BY01UtagMULTI_QI@@ + 32)) : ptr3);
						}
						if (*(ref $ArrayType$$$BY01UtagMULTI_QI@@ + 16) < 0)
						{
							int num3 = *(ref $ArrayType$$$BY01UtagMULTI_QI@@ + 16);
							Marshal.ThrowExceptionForHR(*(ref $ArrayType$$$BY01UtagMULTI_QI@@ + 16));
						}
						if (bQuerySCInfo)
						{
							if (*(ref $ArrayType$$$BY01UtagMULTI_QI@@ + 40) < 0)
							{
								int num4 = *(ref $ArrayType$$$BY01UtagMULTI_QI@@ + 40);
								Marshal.ThrowExceptionForHR(*(ref $ArrayType$$$BY01UtagMULTI_QI@@ + 40));
							}
							if (IdentityManager.Enabled)
							{
								IntPtr pUnk = new IntPtr(ptr2);
								byte b = (!IdentityManager.IsInProcess(pUnk)) ? 1 : 0;
								bIsAnotherProcess = (b != 0);
								if (b != 0)
								{
									IntPtr pUnk2 = new IntPtr(ptr2);
									uri = IdentityManager.CreateIdentityUri(pUnk2);
									goto IL_227;
								}
								goto IL_227;
							}
							else if (ptr3 != null)
							{
								char* ptr5 = null;
								char* ptr6 = null;
								int num5 = 0;
								num5 = Proxy.INFO_PROCESSID;
								num2 = calli(System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,System.Int32*,tagSAFEARRAY**), ptr3, ref num5, ref ptr4, *(*(long*)ptr3 + 24L));
								if (num2 < 0)
								{
									Marshal.ThrowExceptionForHR(num2);
								}
								int num6 = 0;
								<Module>.SafeArrayGetElement(ptr4, (int*)(&num6), (void*)(&ptr5));
								IntPtr ptr7 = new IntPtr((void*)ptr5);
								string strB = Marshal.PtrToStringBSTR(ptr7);
								string processId = RemotingConfiguration.ProcessId;
								if (ptr5 != null)
								{
									<Module>.SysFreeString(ptr5);
								}
								<Module>.SafeArrayDestroy(ptr4);
								ptr4 = null;
								if (string.Compare(processId, strB, StringComparison.Ordinal) == 0)
								{
									bIsAnotherProcess = false;
									goto IL_227;
								}
								bIsAnotherProcess = true;
								num5 = Proxy.INFO_URI;
								num2 = calli(System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,System.Int32*,tagSAFEARRAY**), ptr3, ref num5, ref ptr4, *(*(long*)ptr3 + 24L));
								if (num2 < 0)
								{
									Marshal.ThrowExceptionForHR(num2);
								}
								num6 = 0;
								<Module>.SafeArrayGetElement(ptr4, (int*)(&num6), (void*)(&ptr6));
								IntPtr ptr8 = new IntPtr((void*)ptr6);
								uri = Marshal.PtrToStringBSTR(ptr8);
								if (ptr6 != null)
								{
									<Module>.SysFreeString(ptr6);
								}
								<Module>.SafeArrayDestroy(ptr4);
								ptr4 = null;
								goto IL_227;
							}
						}
						bIsAnotherProcess = true;
					}
					else if (num2 == -2147221164 && flag)
					{
						flag = false;
					}
					else
					{
						Marshal.ThrowExceptionForHR(num2);
					}
					IL_227:
					ptr = ptr2;
					ptr2 = null;
				}
				finally
				{
					if (ptr2 != null)
					{
						IUnknown* ptr9 = ptr2;
						object obj = calli(System.UInt32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr), ptr9, *(*(long*)ptr9 + 16L));
					}
					if (ptr3 != null)
					{
						IServicedComponentInfo* ptr10 = ptr3;
						object obj2 = calli(System.UInt32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr), ptr10, *(*(long*)ptr10 + 16L));
					}
					if (ptr4 != null)
					{
						<Module>.SafeArrayDestroy(ptr4);
					}
				}
			}
			while (ptr == null);
			IntPtr result = new IntPtr(ptr);
			return result;
		}

		// Token: 0x060000BA RID: 186 RVA: 0x00003068 File Offset: 0x00002468
		public unsafe static int GetMarshalSize(object o)
		{
			Proxy.Init();
			IUnknown* ptr = null;
			uint num = 0U;
			try
			{
				ptr = Marshal.GetIUnknownForObject(o).ToInt64();
				if (<Module>.CoGetMarshalSizeMax((uint*)(&num), ref <Module>.IID_IUnknown, ptr, 2, null, 0) >= 0)
				{
					num = (uint)((ulong)num + 4UL);
				}
				else
				{
					num = uint.MaxValue;
				}
			}
			finally
			{
				if (ptr != null)
				{
					IUnknown* ptr2 = ptr;
					object obj = calli(System.UInt32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr), ptr2, *(*(long*)ptr2 + 16L));
				}
			}
			return (int)num;
		}

		// Token: 0x060000BB RID: 187 RVA: 0x00003158 File Offset: 0x00002558
		[return: MarshalAs(UnmanagedType.U1)]
		public unsafe static bool MarshalObject(object o, byte[] b, int cb)
		{
			Proxy.Init();
			IUnknown* ptr = null;
			fixed (byte* ptr2 = &b[0])
			{
				byte* ptr3 = ptr2;
				try
				{
					ptr = Marshal.GetIUnknownForObject(o).ToInt64();
					int num = <Module>.MarshalInterface(ptr3, cb, ptr, 2, 0);
					if (num < 0)
					{
						Marshal.ThrowExceptionForHR(num);
					}
				}
				finally
				{
					if (ptr != null)
					{
						IUnknown* ptr4 = ptr;
						object obj = calli(System.UInt32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr), ptr4, *(*(long*)ptr4 + 16L));
					}
				}
				return true;
			}
		}

		// Token: 0x060000BC RID: 188 RVA: 0x000030E8 File Offset: 0x000024E8
		public unsafe static IntPtr UnmarshalObject(byte[] b)
		{
			Proxy.Init();
			IUnknown* value = null;
			int length = b.Length;
			fixed (byte* ptr = &b[0])
			{
				byte* ptr2 = ptr;
				try
				{
					int num = <Module>.UnmarshalInterface(ptr2, length, (void**)(&value));
					if (num < 0)
					{
						Marshal.ThrowExceptionForHR(num);
					}
				}
				finally
				{
				}
				IntPtr result = new IntPtr(value);
				return result;
			}
		}

		// Token: 0x060000BD RID: 189 RVA: 0x00003218 File Offset: 0x00002618
		public unsafe static void ReleaseMarshaledObject(byte[] b)
		{
			Proxy.Init();
			fixed (byte* ptr = &b[0])
			{
				byte* ptr2 = ptr;
				try
				{
					int num = <Module>.ReleaseMarshaledInterface(ptr2, b.Length);
					if (num < 0)
					{
						Marshal.ThrowExceptionForHR(num);
					}
				}
				finally
				{
				}
			}
		}

		// Token: 0x060000BE RID: 190 RVA: 0x000031D8 File Offset: 0x000025D8
		public unsafe static IntPtr GetStandardMarshal(IntPtr pUnk)
		{
			IMarshal* value;
			int num = <Module>.CoGetStandardMarshal(ref <Module>.IID_IUnknown, pUnk.ToInt64(), 2, null, 0, &value);
			if (num < 0)
			{
				Marshal.ThrowExceptionForHR(num);
			}
			IntPtr result = new IntPtr(value);
			return result;
		}

		// Token: 0x060000BF RID: 191 RVA: 0x00003278 File Offset: 0x00002678
		public static IntPtr GetContextCheck()
		{
			Proxy.Init();
			IntPtr result = new IntPtr(<Module>.GetContextCheck());
			return result;
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x00003298 File Offset: 0x00002698
		public static IntPtr GetCurrentContextToken()
		{
			Proxy.Init();
			IntPtr result = new IntPtr(<Module>.GetContextToken());
			return result;
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x000032B8 File Offset: 0x000026B8
		public unsafe static IntPtr GetCurrentContext()
		{
			Proxy.Init();
			IUnknown* value;
			int context = <Module>.GetContext(ref <Module>.IID_IUnknown, (void**)(&value));
			if (context < 0)
			{
				Marshal.ThrowExceptionForHR(context);
			}
			IntPtr result = new IntPtr(value);
			return result;
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x000032F8 File Offset: 0x000026F8
		public static int CallFunction(IntPtr pfn, IntPtr data)
		{
			long num = data.ToInt64();
			method system.Int32_u0020modopt(System.Runtime.CompilerServices.IsLong)_u0020modopt(System.Runtime.CompilerServices.CallConvCdecl)_u0020(System.Void*) = pfn.ToInt64();
			return calli(System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.Void*), num, system.Int32_u0020modopt(System.Runtime.CompilerServices.IsLong)_u0020modopt(System.Runtime.CompilerServices.CallConvCdecl)_u0020(System.Void*));
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x00003328 File Offset: 0x00002728
		public unsafe static void PoolUnmark(IntPtr pPooledObject)
		{
			IManagedPooledObj* ptr = pPooledObject.ToInt64();
			object obj = calli(System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,System.Int32), ptr, 0, *(*(long*)ptr + 24L));
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x00003358 File Offset: 0x00002758
		public unsafe static void PoolMark(IntPtr pPooledObject)
		{
			IManagedPooledObj* ptr = pPooledObject.ToInt64();
			object obj = calli(System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,System.Int32), ptr, 1, *(*(long*)ptr + 24L));
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x00003388 File Offset: 0x00002788
		public unsafe static int GetManagedExts()
		{
			if (<Module>.?A0xf7eb705c.?dwExts@?1??GetManagedExts@Proxy@Thunk@EnterpriseServices@System@@SMHXZ@4KA == 4294967295U)
			{
				uint num = 0U;
				HINSTANCE__* ptr = <Module>.LoadLibraryW((char*)(&<Module>.?A0xf7eb705c.unnamed-global-8));
				if (ptr != null && ptr != -1L)
				{
					method procAddress = <Module>.GetProcAddress(ptr, (sbyte*)(&<Module>.?A0xf7eb705c.unnamed-global-9));
					if (procAddress != null)
					{
						num = ((calli(System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.UInt32 modopt(System.Runtime.CompilerServices.IsLong)*), ref num, procAddress) < 0) ? 0U : num);
					}
				}
				<Module>.?A0xf7eb705c.?dwExts@?1??GetManagedExts@Proxy@Thunk@EnterpriseServices@System@@SMHXZ@4KA = num;
			}
			return (int)<Module>.?A0xf7eb705c.?dwExts@?1??GetManagedExts@Proxy@Thunk@EnterpriseServices@System@@SMHXZ@4KA;
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x000033E8 File Offset: 0x000027E8
		public unsafe static void SendCreationEvents(IntPtr ctx, IntPtr stub, [MarshalAs(UnmanagedType.U1)] bool fDist)
		{
			IUnknown* ptr = ctx.ToInt64();
			IObjContext* ptr2 = null;
			IManagedObjectInfo* ptr3 = stub.ToInt64();
			IEnumContextProps* ptr4 = null;
			int num = calli(System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,_GUID modopt(System.Runtime.CompilerServices.IsConst)* modopt(System.Runtime.CompilerServices.IsImplicitlyDereferenced),System.Void**), ptr, ref <Module>.System.EnterpriseServices.Thunk.?A0xf7eb705c.IID_IObjContext, ref ptr2, *(*(long*)ptr));
			if (num >= 0)
			{
				try
				{
					num = calli(System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,IEnumContextProps**), ptr2, ref ptr4, *(*(long*)ptr2 + 48L));
					if (num >= 0)
					{
						uint num2 = 0U;
						num = calli(System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,System.UInt32 modopt(System.Runtime.CompilerServices.IsLong)*), ptr4, ref num2, *(*(long*)ptr4 + 56L));
						if (num < 0)
						{
							Marshal.ThrowExceptionForHR(num);
						}
						for (uint num3 = 0U; num3 < num2; num3 += 1U)
						{
							uint num4 = 0U;
							tagContextProperty tagContextProperty;
							num = calli(System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,System.UInt32 modopt(System.Runtime.CompilerServices.IsLong),tagContextProperty*,System.UInt32 modopt(System.Runtime.CompilerServices.IsLong)*), ptr4, 1, ref tagContextProperty, ref num4, *(*(long*)ptr4 + 24L));
							if (num < 0)
							{
								Marshal.ThrowExceptionForHR(num);
							}
							if (num4 != 1U)
							{
								break;
							}
							IManagedActivationEvents* ptr5 = null;
							num = calli(System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,_GUID modopt(System.Runtime.CompilerServices.IsConst)* modopt(System.Runtime.CompilerServices.IsImplicitlyDereferenced),System.Void**), *(ref tagContextProperty + 24), ref <Module>.IID_IManagedActivationEvents, ref ptr5, *(*(*(ref tagContextProperty + 24))));
							if (num >= 0)
							{
								object obj = calli(System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,System.EnterpriseServices.Thunk.IManagedObjectInfo*,System.Int32), ptr5, ptr3, fDist, *(*(long*)ptr5 + 24L));
								IManagedActivationEvents* ptr6 = ptr5;
								object obj2 = calli(System.UInt32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr), ptr6, *(*(long*)ptr6 + 16L));
							}
							object obj3 = calli(System.UInt32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr), *(ref tagContextProperty + 24), *(*(*(ref tagContextProperty + 24)) + 16L));
						}
					}
				}
				finally
				{
					if (ptr2 != null)
					{
						IObjContext* ptr7 = ptr2;
						object obj4 = calli(System.UInt32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr), ptr7, *(*(long*)ptr7 + 16L));
					}
					if (ptr4 != null)
					{
						IEnumContextProps* ptr8 = ptr4;
						object obj5 = calli(System.UInt32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr), ptr8, *(*(long*)ptr8 + 16L));
					}
				}
			}
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x00003538 File Offset: 0x00002938
		public unsafe static void SendDestructionEvents(IntPtr ctx, IntPtr stub, [MarshalAs(UnmanagedType.U1)] bool disposing)
		{
			DestructData destructData = ctx.ToInt64();
			*(ref destructData + 8) = stub.ToInt64();
			tagComCallData tagComCallData = 0;
			*(ref tagComCallData + 4) = 0;
			*(ref tagComCallData + 8) = ref destructData;
			IContextCallback* ptr = null;
			int num = 0;
			try
			{
				num = calli(System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,_GUID modopt(System.Runtime.CompilerServices.IsConst)* modopt(System.Runtime.CompilerServices.IsImplicitlyDereferenced),System.Void**), destructData, ref <Module>.IID_IContextCallback, ref ptr, *(*destructData));
				if (num < 0)
				{
					Marshal.ThrowExceptionForHR(num);
				}
				_GUID* ptr2 = ref disposing ? ref <Module>.IID_IUnknown : ref <Module>.IID_IEnterActivityWithNoLock;
				_GUID guid;
				cpblk(ref guid, ptr2, 16);
				num = calli(System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl) (System.EnterpriseServices.Thunk.tagComCallData*),System.EnterpriseServices.Thunk.tagComCallData*,_GUID modopt(System.Runtime.CompilerServices.IsConst)* modopt(System.Runtime.CompilerServices.IsImplicitlyDereferenced),System.Int32,IUnknown*), ptr, <Module>.__unep@?SendDestructionEventsCallback@Thunk@EnterpriseServices@System@@$$FYAJPEAUtagComCallData@123@@Z, ref tagComCallData, ref guid, 2, 0L, *(*(long*)ptr + 24L));
			}
			finally
			{
				if (ptr != null)
				{
					IContextCallback* ptr3 = ptr;
					object obj = calli(System.UInt32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr), ptr3, *(*(long*)ptr3 + 16L));
				}
			}
			if (num < 0)
			{
				Marshal.ThrowExceptionForHR(num);
			}
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x00003608 File Offset: 0x00002A08
		public unsafe static Tracker FindTracker(IntPtr ctx)
		{
			_GUID guid = -324292941;
			*(ref guid + 4) = 32537;
			*(ref guid + 6) = 4562;
			*(ref guid + 8) = 151;
			*(ref guid + 9) = 142;
			*(ref guid + 10) = 0;
			*(ref guid + 11) = 0;
			*(ref guid + 12) = 248;
			*(ref guid + 13) = 117;
			*(ref guid + 14) = 126;
			*(ref guid + 15) = 42;
			IUnknown* ptr = null;
			ISendMethodEvents* ptr2 = null;
			IObjContext* ptr3 = null;
			uint num = 0U;
			Tracker result;
			try
			{
				long num2 = ctx.ToInt64();
				int num3 = calli(System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,_GUID modopt(System.Runtime.CompilerServices.IsConst)* modopt(System.Runtime.CompilerServices.IsImplicitlyDereferenced),System.Void**), num2, ref <Module>.System.EnterpriseServices.Thunk.?A0xf7eb705c.IID_IObjContext, ref ptr3, *(*num2));
				if (num3 < 0)
				{
					result = null;
				}
				else
				{
					num3 = calli(System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,_GUID modopt(System.Runtime.CompilerServices.IsConst)* modopt(System.Runtime.CompilerServices.IsImplicitlyDereferenced),System.UInt32 modopt(System.Runtime.CompilerServices.IsLong)*,IUnknown**), ptr3, ref guid, ref num, ref ptr, *(*(long*)ptr3 + 40L));
					if (num3 >= 0 && ptr != null)
					{
						num3 = calli(System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,_GUID modopt(System.Runtime.CompilerServices.IsConst)* modopt(System.Runtime.CompilerServices.IsImplicitlyDereferenced),System.Void**), ptr, ref <Module>._GUID_2732fd59_b2b4_4d44_878c_8b8f09626008, ref ptr2, *(*(long*)ptr));
						if (num3 < 0)
						{
							ptr2 = null;
							result = null;
						}
						else
						{
							result = new Tracker(ptr2);
						}
					}
					else
					{
						ptr = null;
						result = null;
					}
				}
			}
			finally
			{
				if (ptr3 != null)
				{
					IObjContext* ptr4 = ptr3;
					object obj = calli(System.UInt32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr), ptr4, *(*(long*)ptr4 + 16L));
				}
				if (ptr != null)
				{
					IUnknown* ptr5 = ptr;
					object obj2 = calli(System.UInt32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr), ptr5, *(*(long*)ptr5 + 16L));
				}
				if (ptr2 != null)
				{
					ISendMethodEvents* ptr6 = ptr2;
					object obj3 = calli(System.UInt32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr), ptr6, *(*(long*)ptr6 + 16L));
				}
			}
			return result;
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x00002B18 File Offset: 0x00001F18
		public static int RegisterProxyStub()
		{
			return <Module>.DllRegisterServer();
		}

		// Token: 0x04000118 RID: 280
		private static bool _fInit;

		// Token: 0x04000119 RID: 281
		private static Hashtable _regCache;

		// Token: 0x0400011A RID: 282
		private unsafe static IGlobalInterfaceTable* _pGIT;

		// Token: 0x0400011B RID: 283
		private static Assembly _thisAssembly;

		// Token: 0x0400011C RID: 284
		private static Mutex _regmutex;

		// Token: 0x0400011D RID: 285
		private static object _classSyncRoot;

		// Token: 0x0400011E RID: 286
		public static int INFO_PROCESSID = 1;

		// Token: 0x0400011F RID: 287
		public static int INFO_APPDOMAINID = 2;

		// Token: 0x04000120 RID: 288
		public static int INFO_URI = 4;
	}
}
