using System;
using System.Runtime.InteropServices;

namespace System.EnterpriseServices.Thunk
{
	// Token: 0x02000091 RID: 145
	internal class ServiceConfigThunk
	{
		// Token: 0x060000E7 RID: 231 RVA: 0x000048F0 File Offset: 0x00003CF0
		public unsafe ServiceConfigThunk()
		{
			this.m_pUnkSC = null;
			this.m_tracker = 0;
			IUnknown* pUnkSC;
			int num = <Module>.CoCreateInstance(ref <Module>.CLSID_CServiceConfig, null, 1, ref <Module>.IID_IUnknown, (void**)(&pUnkSC));
			if (num == -2147221008)
			{
				int num2 = <Module>.CoInitializeEx(null, 0);
				if (num2 < 0)
				{
					Marshal.ThrowExceptionForHR(num2);
				}
				int num3 = <Module>.CoCreateInstance(ref <Module>.CLSID_CServiceConfig, null, 1, ref <Module>.IID_IUnknown, (void**)(&pUnkSC));
				if (num3 < 0)
				{
					Marshal.ThrowExceptionForHR(num3);
				}
			}
			else if (num < 0)
			{
				Marshal.ThrowExceptionForHR(num);
			}
			IntPtr pTrackerAppName = Marshal.StringToCoTaskMemUni(new string((sbyte*)(&<Module>.?A0x26f75eb1.unnamed-global-0)));
			this.m_pTrackerAppName = pTrackerAppName;
			IntPtr pTrackerCtxName = Marshal.StringToCoTaskMemUni(new string((sbyte*)(&<Module>.?A0x26f75eb1.unnamed-global-1)));
			this.m_pTrackerCtxName = pTrackerCtxName;
			this.m_pUnkSC = pUnkSC;
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x000049B0 File Offset: 0x00003DB0
		protected unsafe override void Finalize()
		{
			IUnknown* pUnkSC = this.m_pUnkSC;
			if (pUnkSC != null)
			{
				IUnknown* ptr = pUnkSC;
				object obj = calli(System.UInt32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr), ptr, *(*(long*)ptr + 16L));
				this.m_pUnkSC = null;
			}
			if (this.m_pTrackerAppName != IntPtr.Zero)
			{
				Marshal.FreeCoTaskMem(this.m_pTrackerAppName);
			}
			if (this.m_pTrackerCtxName != IntPtr.Zero)
			{
				Marshal.FreeCoTaskMem(this.m_pTrackerCtxName);
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x060000E9 RID: 233 RVA: 0x00004A30 File Offset: 0x00003E30
		public unsafe IUnknown* ServiceConfigUnknown
		{
			get
			{
				IUnknown* pUnkSC = this.m_pUnkSC;
				if (pUnkSC != null)
				{
					IUnknown* ptr = pUnkSC;
					object obj = calli(System.UInt32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr), ptr, *(*(long*)ptr + 8L));
				}
				return this.m_pUnkSC;
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x060000EA RID: 234 RVA: 0x00004EA0 File Offset: 0x000042A0
		public unsafe bool SupportsSysTxn
		{
			[return: MarshalAs(UnmanagedType.U1)]
			get
			{
				IUnknown* ptr = null;
				if (!<Module>.?A0x26f75eb1.?fInitialized@?1??get_SupportsSysTxn@ServiceConfigThunk@Thunk@EnterpriseServices@System@@QE$AAM_NXZ@4_NA)
				{
					IUnknown* pUnkSC = this.m_pUnkSC;
					<Module>.?A0x26f75eb1.?fSupportsSysTxn@?1??get_SupportsSysTxn@ServiceConfigThunk@Thunk@EnterpriseServices@System@@QE$AAM_NXZ@4_NA = (calli(System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,_GUID modopt(System.Runtime.CompilerServices.IsConst)* modopt(System.Runtime.CompilerServices.IsImplicitlyDereferenced),System.Void**), pUnkSC, ref <Module>._GUID_33caf1a1_fcb8_472b_b45e_967448ded6d8, ref ptr, *(*(long*)pUnkSC)) >= 0);
					<Module>.?A0x26f75eb1.?fInitialized@?1??get_SupportsSysTxn@ServiceConfigThunk@Thunk@EnterpriseServices@System@@QE$AAM_NXZ@4_NA = true;
					if (ptr != null)
					{
						IUnknown* ptr2 = ptr;
						object obj = calli(System.UInt32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr), ptr2, *(*(long*)ptr2 + 16L));
					}
				}
				return <Module>.?A0x26f75eb1.?fSupportsSysTxn@?1??get_SupportsSysTxn@ServiceConfigThunk@Thunk@EnterpriseServices@System@@QE$AAM_NXZ@4_NA;
			}
		}

		// Token: 0x17000016 RID: 22
		// (set) Token: 0x060000EB RID: 235 RVA: 0x00004A60 File Offset: 0x00003E60
		public unsafe int Inheritance
		{
			set
			{
				IServiceInheritanceConfig* ptr = null;
				try
				{
					IUnknown* pUnkSC = this.m_pUnkSC;
					int errorCode = calli(System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,_GUID modopt(System.Runtime.CompilerServices.IsConst)* modopt(System.Runtime.CompilerServices.IsImplicitlyDereferenced),System.Void**), pUnkSC, ref <Module>.IID_IServiceInheritanceConfig, ref ptr, *(*(long*)pUnkSC));
					Marshal.ThrowExceptionForHR(errorCode);
					errorCode = calli(System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,System.Int32), ptr, value, *(*(long*)ptr + 24L));
					Marshal.ThrowExceptionForHR(errorCode);
				}
				finally
				{
					if (ptr != null)
					{
						IServiceInheritanceConfig* ptr2 = ptr;
						object obj = calli(System.UInt32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr), ptr2, *(*(long*)ptr2 + 16L));
					}
				}
			}
		}

		// Token: 0x17000015 RID: 21
		// (set) Token: 0x060000EC RID: 236 RVA: 0x00004AE0 File Offset: 0x00003EE0
		public unsafe int ThreadPool
		{
			set
			{
				IServiceThreadPoolConfig* ptr = null;
				try
				{
					IUnknown* pUnkSC = this.m_pUnkSC;
					int errorCode = calli(System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,_GUID modopt(System.Runtime.CompilerServices.IsConst)* modopt(System.Runtime.CompilerServices.IsImplicitlyDereferenced),System.Void**), pUnkSC, ref <Module>.IID_IServiceThreadPoolConfig, ref ptr, *(*(long*)pUnkSC));
					Marshal.ThrowExceptionForHR(errorCode);
					errorCode = calli(System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,System.Int32), ptr, value, *(*(long*)ptr + 24L));
					Marshal.ThrowExceptionForHR(errorCode);
				}
				finally
				{
					if (ptr != null)
					{
						IServiceThreadPoolConfig* ptr2 = ptr;
						object obj = calli(System.UInt32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr), ptr2, *(*(long*)ptr2 + 16L));
					}
				}
			}
		}

		// Token: 0x17000014 RID: 20
		// (set) Token: 0x060000ED RID: 237 RVA: 0x00004B60 File Offset: 0x00003F60
		public unsafe int Binding
		{
			set
			{
				IServiceThreadPoolConfig* ptr = null;
				try
				{
					IUnknown* pUnkSC = this.m_pUnkSC;
					int errorCode = calli(System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,_GUID modopt(System.Runtime.CompilerServices.IsConst)* modopt(System.Runtime.CompilerServices.IsImplicitlyDereferenced),System.Void**), pUnkSC, ref <Module>.IID_IServiceThreadPoolConfig, ref ptr, *(*(long*)pUnkSC));
					Marshal.ThrowExceptionForHR(errorCode);
					errorCode = calli(System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,System.Int32), ptr, value, *(*(long*)ptr + 32L));
					Marshal.ThrowExceptionForHR(errorCode);
				}
				finally
				{
					if (ptr != null)
					{
						IServiceThreadPoolConfig* ptr2 = ptr;
						object obj = calli(System.UInt32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr), ptr2, *(*(long*)ptr2 + 16L));
					}
				}
			}
		}

		// Token: 0x17000013 RID: 19
		// (set) Token: 0x060000EE RID: 238 RVA: 0x00004BE0 File Offset: 0x00003FE0
		public unsafe int Transaction
		{
			set
			{
				IServiceTransactionConfig* ptr = null;
				try
				{
					IUnknown* pUnkSC = this.m_pUnkSC;
					int errorCode = calli(System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,_GUID modopt(System.Runtime.CompilerServices.IsConst)* modopt(System.Runtime.CompilerServices.IsImplicitlyDereferenced),System.Void**), pUnkSC, ref <Module>.IID_IServiceTransactionConfig, ref ptr, *(*(long*)pUnkSC));
					Marshal.ThrowExceptionForHR(errorCode);
					if (value > 0)
					{
						value += -1;
					}
					errorCode = calli(System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,System.Int32), ptr, value, *(*(long*)ptr + 24L));
					Marshal.ThrowExceptionForHR(errorCode);
				}
				finally
				{
					if (ptr != null)
					{
						IServiceTransactionConfig* ptr2 = ptr;
						object obj = calli(System.UInt32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr), ptr2, *(*(long*)ptr2 + 16L));
					}
				}
			}
		}

		// Token: 0x17000012 RID: 18
		// (set) Token: 0x060000EF RID: 239 RVA: 0x00004C60 File Offset: 0x00004060
		public unsafe int TxIsolationLevel
		{
			set
			{
				IServiceTransactionConfig* ptr = null;
				try
				{
					IUnknown* pUnkSC = this.m_pUnkSC;
					int errorCode = calli(System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,_GUID modopt(System.Runtime.CompilerServices.IsConst)* modopt(System.Runtime.CompilerServices.IsImplicitlyDereferenced),System.Void**), pUnkSC, ref <Module>.IID_IServiceTransactionConfig, ref ptr, *(*(long*)pUnkSC));
					Marshal.ThrowExceptionForHR(errorCode);
					errorCode = calli(System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,System.Int32), ptr, value, *(*(long*)ptr + 32L));
					Marshal.ThrowExceptionForHR(errorCode);
				}
				finally
				{
					if (ptr != null)
					{
						IServiceTransactionConfig* ptr2 = ptr;
						object obj = calli(System.UInt32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr), ptr2, *(*(long*)ptr2 + 16L));
					}
				}
			}
		}

		// Token: 0x17000011 RID: 17
		// (set) Token: 0x060000F0 RID: 240 RVA: 0x00004CE0 File Offset: 0x000040E0
		public unsafe int TxTimeout
		{
			set
			{
				IServiceTransactionConfig* ptr = null;
				try
				{
					IUnknown* pUnkSC = this.m_pUnkSC;
					int errorCode = calli(System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,_GUID modopt(System.Runtime.CompilerServices.IsConst)* modopt(System.Runtime.CompilerServices.IsImplicitlyDereferenced),System.Void**), pUnkSC, ref <Module>.IID_IServiceTransactionConfig, ref ptr, *(*(long*)pUnkSC));
					Marshal.ThrowExceptionForHR(errorCode);
					errorCode = calli(System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,System.UInt32 modopt(System.Runtime.CompilerServices.IsLong)), ptr, value, *(*(long*)ptr + 40L));
					Marshal.ThrowExceptionForHR(errorCode);
				}
				finally
				{
					if (ptr != null)
					{
						IServiceTransactionConfig* ptr2 = ptr;
						object obj = calli(System.UInt32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr), ptr2, *(*(long*)ptr2 + 16L));
					}
				}
			}
		}

		// Token: 0x17000010 RID: 16
		// (set) Token: 0x060000F1 RID: 241 RVA: 0x00004D60 File Offset: 0x00004160
		public unsafe string TipUrl
		{
			set
			{
				IServiceTransactionConfig* ptr = null;
				IntPtr intPtr = 0;
				try
				{
					IUnknown* pUnkSC = this.m_pUnkSC;
					int errorCode = calli(System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,_GUID modopt(System.Runtime.CompilerServices.IsConst)* modopt(System.Runtime.CompilerServices.IsImplicitlyDereferenced),System.Void**), pUnkSC, ref <Module>.IID_IServiceTransactionConfig, ref ptr, *(*(long*)pUnkSC));
					Marshal.ThrowExceptionForHR(errorCode);
					intPtr = Marshal.StringToCoTaskMemUni(value);
					long num = *(long*)ptr + 48L;
					errorCode = calli(System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,System.Char modopt(System.Runtime.CompilerServices.IsConst)*), ptr, (void*)intPtr, *num);
					Marshal.ThrowExceptionForHR(errorCode);
				}
				finally
				{
					if (intPtr != IntPtr.Zero)
					{
						Marshal.FreeCoTaskMem(intPtr);
					}
					if (ptr != null)
					{
						IServiceTransactionConfig* ptr2 = ptr;
						object obj = calli(System.UInt32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr), ptr2, *(*(long*)ptr2 + 16L));
					}
				}
			}
		}

		// Token: 0x1700000F RID: 15
		// (set) Token: 0x060000F2 RID: 242 RVA: 0x00004E00 File Offset: 0x00004200
		public unsafe string TxDesc
		{
			set
			{
				IServiceTransactionConfig* ptr = null;
				IntPtr intPtr = 0;
				try
				{
					IUnknown* pUnkSC = this.m_pUnkSC;
					int errorCode = calli(System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,_GUID modopt(System.Runtime.CompilerServices.IsConst)* modopt(System.Runtime.CompilerServices.IsImplicitlyDereferenced),System.Void**), pUnkSC, ref <Module>.IID_IServiceTransactionConfig, ref ptr, *(*(long*)pUnkSC));
					Marshal.ThrowExceptionForHR(errorCode);
					intPtr = Marshal.StringToCoTaskMemUni(value);
					long num = *(long*)ptr + 56L;
					errorCode = calli(System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,System.Char modopt(System.Runtime.CompilerServices.IsConst)*), ptr, (void*)intPtr, *num);
					Marshal.ThrowExceptionForHR(errorCode);
				}
				finally
				{
					if (intPtr != IntPtr.Zero)
					{
						Marshal.FreeCoTaskMem(intPtr);
					}
					if (ptr != null)
					{
						IServiceTransactionConfig* ptr2 = ptr;
						object obj = calli(System.UInt32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr), ptr2, *(*(long*)ptr2 + 16L));
					}
				}
			}
		}

		// Token: 0x1700000E RID: 14
		// (set) Token: 0x060000F3 RID: 243 RVA: 0x00004FD0 File Offset: 0x000043D0
		public unsafe object Byot
		{
			set
			{
				IUnknown* ptr = null;
				ITransaction* ptr2 = null;
				IServiceTransactionConfig* ptr3 = null;
				try
				{
					int errorCode;
					if (value != null)
					{
						ptr = (IUnknown*)((void*)Marshal.GetIUnknownForObject(value));
						errorCode = calli(System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,_GUID modopt(System.Runtime.CompilerServices.IsConst)* modopt(System.Runtime.CompilerServices.IsImplicitlyDereferenced),System.Void**), ptr, ref <Module>._GUID_0fb15084_af41_11ce_bd2b_204c4f4f5020, ref ptr2, *(*(long*)ptr));
						Marshal.ThrowExceptionForHR(errorCode);
					}
					IUnknown* pUnkSC = this.m_pUnkSC;
					errorCode = calli(System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,_GUID modopt(System.Runtime.CompilerServices.IsConst)* modopt(System.Runtime.CompilerServices.IsImplicitlyDereferenced),System.Void**), pUnkSC, ref <Module>.IID_IServiceTransactionConfig, ref ptr3, *(*(long*)pUnkSC));
					Marshal.ThrowExceptionForHR(errorCode);
					errorCode = calli(System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,System.EnterpriseServices.Thunk.ITransaction*), ptr3, ptr2, *(*(long*)ptr3 + 64L));
					Marshal.ThrowExceptionForHR(errorCode);
					GC.KeepAlive(value);
				}
				finally
				{
					if (ptr3 != null)
					{
						IServiceTransactionConfig* ptr4 = ptr3;
						object obj = calli(System.UInt32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr), ptr4, *(*(long*)ptr4 + 16L));
					}
					if (ptr != null)
					{
						IUnknown* ptr5 = ptr;
						object obj2 = calli(System.UInt32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr), ptr5, *(*(long*)ptr5 + 16L));
					}
					if (ptr2 != null)
					{
						ITransaction* ptr6 = ptr2;
						object obj3 = calli(System.UInt32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr), ptr6, *(*(long*)ptr6 + 16L));
					}
				}
			}
		}

		// Token: 0x1700000D RID: 13
		// (set) Token: 0x060000F4 RID: 244 RVA: 0x00004F00 File Offset: 0x00004300
		public unsafe object ByotSysTxn
		{
			set
			{
				IUnknown* ptr = null;
				IUnknown* ptr2 = null;
				IServiceSysTxnConfigInternal* ptr3 = null;
				try
				{
					int errorCode;
					if (value != null)
					{
						ptr = (IUnknown*)((void*)Marshal.GetIUnknownForObject(value));
						errorCode = calli(System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,_GUID modopt(System.Runtime.CompilerServices.IsConst)* modopt(System.Runtime.CompilerServices.IsImplicitlyDereferenced),System.Void**), ptr, ref <Module>._GUID_02558374_df2e_4dae_bd6b_1d5c994f9bdc, ref ptr2, *(*(long*)ptr));
						Marshal.ThrowExceptionForHR(errorCode);
					}
					IUnknown* pUnkSC = this.m_pUnkSC;
					errorCode = calli(System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,_GUID modopt(System.Runtime.CompilerServices.IsConst)* modopt(System.Runtime.CompilerServices.IsImplicitlyDereferenced),System.Void**), pUnkSC, ref <Module>._GUID_33caf1a1_fcb8_472b_b45e_967448ded6d8, ref ptr3, *(*(long*)pUnkSC));
					Marshal.ThrowExceptionForHR(errorCode);
					errorCode = calli(System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,IUnknown*), ptr3, ptr2, *(*(long*)ptr3 + 72L));
					Marshal.ThrowExceptionForHR(errorCode);
					GC.KeepAlive(value);
				}
				finally
				{
					if (ptr3 != null)
					{
						IServiceSysTxnConfigInternal* ptr4 = ptr3;
						object obj = calli(System.UInt32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr), ptr4, *(*(long*)ptr4 + 16L));
					}
					if (ptr != null)
					{
						IUnknown* ptr5 = ptr;
						object obj2 = calli(System.UInt32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr), ptr5, *(*(long*)ptr5 + 16L));
					}
					if (ptr2 != null)
					{
						IUnknown* ptr6 = ptr2;
						object obj3 = calli(System.UInt32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr), ptr6, *(*(long*)ptr6 + 16L));
					}
				}
			}
		}

		// Token: 0x1700000C RID: 12
		// (set) Token: 0x060000F5 RID: 245 RVA: 0x000050A0 File Offset: 0x000044A0
		public unsafe int Synchronization
		{
			set
			{
				IServiceSynchronizationConfig* ptr = null;
				try
				{
					IUnknown* pUnkSC = this.m_pUnkSC;
					int errorCode = calli(System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,_GUID modopt(System.Runtime.CompilerServices.IsConst)* modopt(System.Runtime.CompilerServices.IsImplicitlyDereferenced),System.Void**), pUnkSC, ref <Module>.IID_IServiceSynchronizationConfig, ref ptr, *(*(long*)pUnkSC));
					Marshal.ThrowExceptionForHR(errorCode);
					if (value > 0)
					{
						value += -1;
					}
					errorCode = calli(System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,System.Int32), ptr, value, *(*(long*)ptr + 24L));
					Marshal.ThrowExceptionForHR(errorCode);
				}
				finally
				{
					if (ptr != null)
					{
						IServiceSynchronizationConfig* ptr2 = ptr;
						object obj = calli(System.UInt32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr), ptr2, *(*(long*)ptr2 + 16L));
					}
				}
			}
		}

		// Token: 0x1700000B RID: 11
		// (set) Token: 0x060000F6 RID: 246 RVA: 0x00005120 File Offset: 0x00004520
		public unsafe bool IISIntrinsics
		{
			[param: MarshalAs(UnmanagedType.U1)]
			set
			{
				IServiceIISIntrinsicsConfig* ptr = null;
				try
				{
					IUnknown* pUnkSC = this.m_pUnkSC;
					int errorCode = calli(System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,_GUID modopt(System.Runtime.CompilerServices.IsConst)* modopt(System.Runtime.CompilerServices.IsImplicitlyDereferenced),System.Void**), pUnkSC, ref <Module>.IID_IServiceIISIntrinsicsConfig, ref ptr, *(*(long*)pUnkSC));
					Marshal.ThrowExceptionForHR(errorCode);
					int num = value ? 1 : 0;
					errorCode = calli(System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,System.Int32), ptr, num, *(*(long*)ptr + 24L));
					Marshal.ThrowExceptionForHR(errorCode);
				}
				finally
				{
					if (ptr != null)
					{
						IServiceIISIntrinsicsConfig* ptr2 = ptr;
						object obj = calli(System.UInt32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr), ptr2, *(*(long*)ptr2 + 16L));
					}
				}
			}
		}

		// Token: 0x1700000A RID: 10
		// (set) Token: 0x060000F7 RID: 247 RVA: 0x000051A0 File Offset: 0x000045A0
		public unsafe bool COMTIIntrinsics
		{
			[param: MarshalAs(UnmanagedType.U1)]
			set
			{
				IServiceComTIIntrinsicsConfig* ptr = null;
				try
				{
					IUnknown* pUnkSC = this.m_pUnkSC;
					int errorCode = calli(System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,_GUID modopt(System.Runtime.CompilerServices.IsConst)* modopt(System.Runtime.CompilerServices.IsImplicitlyDereferenced),System.Void**), pUnkSC, ref <Module>.IID_IServiceComTIIntrinsicsConfig, ref ptr, *(*(long*)pUnkSC));
					Marshal.ThrowExceptionForHR(errorCode);
					int num = value ? 1 : 0;
					errorCode = calli(System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,System.Int32), ptr, num, *(*(long*)ptr + 24L));
					Marshal.ThrowExceptionForHR(errorCode);
				}
				finally
				{
					if (ptr != null)
					{
						IServiceComTIIntrinsicsConfig* ptr2 = ptr;
						object obj = calli(System.UInt32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr), ptr2, *(*(long*)ptr2 + 16L));
					}
				}
			}
		}

		// Token: 0x17000009 RID: 9
		// (set) Token: 0x060000F8 RID: 248 RVA: 0x00005220 File Offset: 0x00004620
		public unsafe bool Tracker
		{
			[param: MarshalAs(UnmanagedType.U1)]
			set
			{
				IServiceTrackerConfig* ptr = null;
				try
				{
					IUnknown* pUnkSC = this.m_pUnkSC;
					int errorCode = calli(System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,_GUID modopt(System.Runtime.CompilerServices.IsConst)* modopt(System.Runtime.CompilerServices.IsImplicitlyDereferenced),System.Void**), pUnkSC, ref <Module>.IID_IServiceTrackerConfig, ref ptr, *(*(long*)pUnkSC));
					Marshal.ThrowExceptionForHR(errorCode);
					int num = value ? 1 : 0;
					long num2 = *(long*)ptr + 24L;
					errorCode = calli(System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,System.Int32,System.Char modopt(System.Runtime.CompilerServices.IsConst)*,System.Char modopt(System.Runtime.CompilerServices.IsConst)*), ptr, num, (void*)this.m_pTrackerAppName, (void*)this.m_pTrackerCtxName, *num2);
					Marshal.ThrowExceptionForHR(errorCode);
				}
				finally
				{
					if (ptr != null)
					{
						IServiceTrackerConfig* ptr2 = ptr;
						object obj = calli(System.UInt32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr), ptr2, *(*(long*)ptr2 + 16L));
					}
				}
				int tracker = value ? 1 : 0;
				this.m_tracker = tracker;
				GC.KeepAlive(this);
			}
		}

		// Token: 0x17000008 RID: 8
		// (set) Token: 0x060000F9 RID: 249 RVA: 0x000052E0 File Offset: 0x000046E0
		public unsafe string TrackerAppName
		{
			set
			{
				IServiceTrackerConfig* ptr = null;
				IntPtr intPtr = 0;
				try
				{
					IUnknown* pUnkSC = this.m_pUnkSC;
					int num = calli(System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,_GUID modopt(System.Runtime.CompilerServices.IsConst)* modopt(System.Runtime.CompilerServices.IsImplicitlyDereferenced),System.Void**), pUnkSC, ref <Module>.IID_IServiceTrackerConfig, ref ptr, *(*(long*)pUnkSC));
					Marshal.ThrowExceptionForHR(num);
					intPtr = Marshal.StringToCoTaskMemUni(value);
					long num2 = *(long*)ptr + 24L;
					num = calli(System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,System.Int32,System.Char modopt(System.Runtime.CompilerServices.IsConst)*,System.Char modopt(System.Runtime.CompilerServices.IsConst)*), ptr, this.m_tracker, (void*)intPtr, (void*)this.m_pTrackerCtxName, *num2);
					if (num < 0)
					{
						Marshal.FreeCoTaskMem(intPtr);
						intPtr = Marshal.StringToCoTaskMemUni(new string((sbyte*)(&<Module>.?A0x26f75eb1.unnamed-global-2)));
						Marshal.ThrowExceptionForHR(num);
					}
				}
				finally
				{
					if (this.m_pTrackerAppName != IntPtr.Zero)
					{
						Marshal.FreeCoTaskMem(this.m_pTrackerAppName);
					}
					if (ptr != null)
					{
						IServiceTrackerConfig* ptr2 = ptr;
						object obj = calli(System.UInt32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr), ptr2, *(*(long*)ptr2 + 16L));
					}
					this.m_pTrackerAppName = intPtr;
				}
				GC.KeepAlive(this);
			}
		}

		// Token: 0x17000007 RID: 7
		// (set) Token: 0x060000FA RID: 250 RVA: 0x000053D0 File Offset: 0x000047D0
		public unsafe string TrackerCtxName
		{
			set
			{
				IServiceTrackerConfig* ptr = null;
				IntPtr intPtr = 0;
				try
				{
					IUnknown* pUnkSC = this.m_pUnkSC;
					int num = calli(System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,_GUID modopt(System.Runtime.CompilerServices.IsConst)* modopt(System.Runtime.CompilerServices.IsImplicitlyDereferenced),System.Void**), pUnkSC, ref <Module>.IID_IServiceTrackerConfig, ref ptr, *(*(long*)pUnkSC));
					Marshal.ThrowExceptionForHR(num);
					intPtr = Marshal.StringToCoTaskMemUni(value);
					long num2 = *(long*)ptr + 24L;
					num = calli(System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,System.Int32,System.Char modopt(System.Runtime.CompilerServices.IsConst)*,System.Char modopt(System.Runtime.CompilerServices.IsConst)*), ptr, this.m_tracker, (void*)this.m_pTrackerAppName, (void*)intPtr, *num2);
					if (num < 0)
					{
						Marshal.FreeCoTaskMem(intPtr);
						intPtr = Marshal.StringToCoTaskMemUni(new string((sbyte*)(&<Module>.?A0x26f75eb1.unnamed-global-3)));
						Marshal.ThrowExceptionForHR(num);
					}
				}
				finally
				{
					if (this.m_pTrackerCtxName != IntPtr.Zero)
					{
						Marshal.FreeCoTaskMem(this.m_pTrackerCtxName);
					}
					if (ptr != null)
					{
						IServiceTrackerConfig* ptr2 = ptr;
						object obj = calli(System.UInt32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr), ptr2, *(*(long*)ptr2 + 16L));
					}
					this.m_pTrackerCtxName = intPtr;
				}
				GC.KeepAlive(this);
			}
		}

		// Token: 0x17000006 RID: 6
		// (set) Token: 0x060000FB RID: 251 RVA: 0x000054C0 File Offset: 0x000048C0
		public unsafe int Sxs
		{
			set
			{
				IServiceSxsConfig* ptr = null;
				try
				{
					IUnknown* pUnkSC = this.m_pUnkSC;
					int errorCode = calli(System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,_GUID modopt(System.Runtime.CompilerServices.IsConst)* modopt(System.Runtime.CompilerServices.IsImplicitlyDereferenced),System.Void**), pUnkSC, ref <Module>.IID_IServiceSxsConfig, ref ptr, *(*(long*)pUnkSC));
					Marshal.ThrowExceptionForHR(errorCode);
					errorCode = calli(System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,System.Int32), ptr, value, *(*(long*)ptr + 24L));
					Marshal.ThrowExceptionForHR(errorCode);
				}
				finally
				{
					if (ptr != null)
					{
						IServiceSxsConfig* ptr2 = ptr;
						object obj = calli(System.UInt32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr), ptr2, *(*(long*)ptr2 + 16L));
					}
				}
			}
		}

		// Token: 0x17000005 RID: 5
		// (set) Token: 0x060000FC RID: 252 RVA: 0x00005540 File Offset: 0x00004940
		public unsafe string SxsName
		{
			set
			{
				IServiceSxsConfig* ptr = null;
				IntPtr intPtr = 0;
				try
				{
					IUnknown* pUnkSC = this.m_pUnkSC;
					int errorCode = calli(System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,_GUID modopt(System.Runtime.CompilerServices.IsConst)* modopt(System.Runtime.CompilerServices.IsImplicitlyDereferenced),System.Void**), pUnkSC, ref <Module>.IID_IServiceSxsConfig, ref ptr, *(*(long*)pUnkSC));
					Marshal.ThrowExceptionForHR(errorCode);
					intPtr = Marshal.StringToCoTaskMemUni(value);
					long num = *(long*)ptr + 32L;
					errorCode = calli(System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,System.Char modopt(System.Runtime.CompilerServices.IsConst)*), ptr, (void*)intPtr, *num);
					Marshal.ThrowExceptionForHR(errorCode);
				}
				finally
				{
					if (intPtr != IntPtr.Zero)
					{
						Marshal.FreeCoTaskMem(intPtr);
					}
					if (ptr != null)
					{
						IServiceSxsConfig* ptr2 = ptr;
						object obj = calli(System.UInt32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr), ptr2, *(*(long*)ptr2 + 16L));
					}
				}
			}
		}

		// Token: 0x17000004 RID: 4
		// (set) Token: 0x060000FD RID: 253 RVA: 0x000055E0 File Offset: 0x000049E0
		public unsafe string SxsDirectory
		{
			set
			{
				IServiceSxsConfig* ptr = null;
				IntPtr intPtr = 0;
				try
				{
					IUnknown* pUnkSC = this.m_pUnkSC;
					int errorCode = calli(System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,_GUID modopt(System.Runtime.CompilerServices.IsConst)* modopt(System.Runtime.CompilerServices.IsImplicitlyDereferenced),System.Void**), pUnkSC, ref <Module>.IID_IServiceSxsConfig, ref ptr, *(*(long*)pUnkSC));
					Marshal.ThrowExceptionForHR(errorCode);
					intPtr = Marshal.StringToCoTaskMemUni(value);
					long num = *(long*)ptr + 40L;
					errorCode = calli(System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,System.Char modopt(System.Runtime.CompilerServices.IsConst)*), ptr, (void*)intPtr, *num);
					Marshal.ThrowExceptionForHR(errorCode);
				}
				finally
				{
					if (intPtr != IntPtr.Zero)
					{
						Marshal.FreeCoTaskMem(intPtr);
					}
					if (ptr != null)
					{
						IServiceSxsConfig* ptr2 = ptr;
						object obj = calli(System.UInt32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr), ptr2, *(*(long*)ptr2 + 16L));
					}
				}
			}
		}

		// Token: 0x17000003 RID: 3
		// (set) Token: 0x060000FE RID: 254 RVA: 0x00005680 File Offset: 0x00004A80
		public unsafe int Partition
		{
			set
			{
				IServicePartitionConfig* ptr = null;
				try
				{
					IUnknown* pUnkSC = this.m_pUnkSC;
					int errorCode = calli(System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,_GUID modopt(System.Runtime.CompilerServices.IsConst)* modopt(System.Runtime.CompilerServices.IsImplicitlyDereferenced),System.Void**), pUnkSC, ref <Module>.IID_IServicePartitionConfig, ref ptr, *(*(long*)pUnkSC));
					Marshal.ThrowExceptionForHR(errorCode);
					errorCode = calli(System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,System.Int32), ptr, value, *(*(long*)ptr + 24L));
					Marshal.ThrowExceptionForHR(errorCode);
				}
				finally
				{
					if (ptr != null)
					{
						IServicePartitionConfig* ptr2 = ptr;
						object obj = calli(System.UInt32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr), ptr2, *(*(long*)ptr2 + 16L));
					}
				}
			}
		}

		// Token: 0x17000002 RID: 2
		// (set) Token: 0x060000FF RID: 255 RVA: 0x00005700 File Offset: 0x00004B00
		public unsafe Guid PartitionId
		{
			set
			{
				IServicePartitionConfig* ptr = null;
				IntPtr intPtr = 0;
				try
				{
					IUnknown* pUnkSC = this.m_pUnkSC;
					int errorCode = calli(System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,_GUID modopt(System.Runtime.CompilerServices.IsConst)* modopt(System.Runtime.CompilerServices.IsImplicitlyDereferenced),System.Void**), pUnkSC, ref <Module>.IID_IServicePartitionConfig, ref ptr, *(*(long*)pUnkSC));
					Marshal.ThrowExceptionForHR(errorCode);
					intPtr = Marshal.AllocCoTaskMem(Marshal.SizeOf(value));
					Marshal.StructureToPtr(value, intPtr, false);
					long num = *(long*)ptr + 32L;
					errorCode = calli(System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,_GUID modopt(System.Runtime.CompilerServices.IsConst)* modopt(System.Runtime.CompilerServices.IsImplicitlyDereferenced)), ptr, (void*)intPtr, *num);
					Marshal.ThrowExceptionForHR(errorCode);
				}
				finally
				{
					if (intPtr != IntPtr.Zero)
					{
						Marshal.FreeCoTaskMem(intPtr);
					}
					if (ptr != null)
					{
						IServicePartitionConfig* ptr2 = ptr;
						object obj = calli(System.UInt32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr), ptr2, *(*(long*)ptr2 + 16L));
					}
				}
			}
		}

		// Token: 0x06000100 RID: 256 RVA: 0x00005AF8 File Offset: 0x00004EF8
		public void {dtor}()
		{
			GC.SuppressFinalize(this);
			this.Finalize();
		}

		// Token: 0x04000153 RID: 339
		private unsafe IUnknown* m_pUnkSC;

		// Token: 0x04000154 RID: 340
		private int m_tracker;

		// Token: 0x04000155 RID: 341
		private IntPtr m_pTrackerAppName;

		// Token: 0x04000156 RID: 342
		private IntPtr m_pTrackerCtxName;
	}
}
