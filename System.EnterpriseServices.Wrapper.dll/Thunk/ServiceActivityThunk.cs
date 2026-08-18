using System;
using System.Runtime.InteropServices;

namespace System.EnterpriseServices.Thunk
{
	// Token: 0x02000093 RID: 147
	internal class ServiceActivityThunk
	{
		// Token: 0x06000105 RID: 261 RVA: 0x00005850 File Offset: 0x00004C50
		public unsafe ServiceActivityThunk(ServiceConfigThunk psct)
		{
			IUnknown* serviceConfigUnknown = psct.ServiceConfigUnknown;
			this.m_pSA = null;
			IServiceActivity* pSA;
			int errorCode = calli(System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(IUnknown*,_GUID modopt(System.Runtime.CompilerServices.IsConst)* modopt(System.Runtime.CompilerServices.IsImplicitlyDereferenced),System.Void**), serviceConfigUnknown, ref <Module>.IID_IServiceActivity, ref pSA, ServiceDomainThunk.CoCreateActivity);
			IUnknown* ptr = serviceConfigUnknown;
			object obj = calli(System.UInt32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr), ptr, *(*(long*)ptr + 16L));
			Marshal.ThrowExceptionForHR(errorCode);
			this.m_pSA = pSA;
		}

		// Token: 0x06000106 RID: 262 RVA: 0x000058A0 File Offset: 0x00004CA0
		protected unsafe override void Finalize()
		{
			IServiceActivity* pSA = this.m_pSA;
			if (pSA != null)
			{
				IServiceActivity* ptr = pSA;
				object obj = calli(System.UInt32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr), ptr, *(*(long*)ptr + 16L));
				this.m_pSA = null;
			}
		}

		// Token: 0x06000107 RID: 263 RVA: 0x000058D0 File Offset: 0x00004CD0
		public unsafe void SynchronousCall(object pObj)
		{
			IUnknown* ptr = null;
			IServiceCall* ptr2 = null;
			try
			{
				ptr = (IUnknown*)((void*)Marshal.GetIUnknownForObject(pObj));
				int errorCode = calli(System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,_GUID modopt(System.Runtime.CompilerServices.IsConst)* modopt(System.Runtime.CompilerServices.IsImplicitlyDereferenced),System.Void**), ptr, ref <Module>.IID_IServiceCall, ref ptr2, *(*(long*)ptr));
				Marshal.ThrowExceptionForHR(errorCode);
				IServiceActivity* pSA = this.m_pSA;
				errorCode = calli(System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,System.EnterpriseServices.Thunk.IServiceCall*), pSA, ptr2, *(*(long*)pSA + 24L));
				Marshal.ThrowExceptionForHR(errorCode);
			}
			finally
			{
				if (ptr2 != null)
				{
					IServiceCall* ptr3 = ptr2;
					object obj = calli(System.UInt32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr), ptr3, *(*(long*)ptr3 + 16L));
				}
				if (ptr != null)
				{
					IUnknown* ptr4 = ptr;
					object obj2 = calli(System.UInt32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr), ptr4, *(*(long*)ptr4 + 16L));
				}
			}
		}

		// Token: 0x06000108 RID: 264 RVA: 0x00005970 File Offset: 0x00004D70
		public unsafe void AsynchronousCall(object pObj)
		{
			IUnknown* ptr = null;
			IServiceCall* ptr2 = null;
			try
			{
				ptr = (IUnknown*)((void*)Marshal.GetIUnknownForObject(pObj));
				int errorCode = calli(System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,_GUID modopt(System.Runtime.CompilerServices.IsConst)* modopt(System.Runtime.CompilerServices.IsImplicitlyDereferenced),System.Void**), ptr, ref <Module>.IID_IServiceCall, ref ptr2, *(*(long*)ptr));
				Marshal.ThrowExceptionForHR(errorCode);
				IServiceActivity* pSA = this.m_pSA;
				errorCode = calli(System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,System.EnterpriseServices.Thunk.IServiceCall*), pSA, ptr2, *(*(long*)pSA + 32L));
				Marshal.ThrowExceptionForHR(errorCode);
			}
			finally
			{
				if (ptr2 != null)
				{
					IServiceCall* ptr3 = ptr2;
					object obj = calli(System.UInt32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr), ptr3, *(*(long*)ptr3 + 16L));
				}
				if (ptr != null)
				{
					IUnknown* ptr4 = ptr;
					object obj2 = calli(System.UInt32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr), ptr4, *(*(long*)ptr4 + 16L));
				}
			}
		}

		// Token: 0x06000109 RID: 265 RVA: 0x00005A10 File Offset: 0x00004E10
		public unsafe void BindToCurrentThread()
		{
			IServiceActivity* pSA = this.m_pSA;
			Marshal.ThrowExceptionForHR(calli(System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr), pSA, *(*(long*)pSA + 40L)));
		}

		// Token: 0x0600010A RID: 266 RVA: 0x00005A40 File Offset: 0x00004E40
		public unsafe void UnbindFromThread()
		{
			IServiceActivity* pSA = this.m_pSA;
			Marshal.ThrowExceptionForHR(calli(System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr), pSA, *(*(long*)pSA + 48L)));
		}

		// Token: 0x0600010B RID: 267 RVA: 0x00005B14 File Offset: 0x00004F14
		public void {dtor}()
		{
			GC.SuppressFinalize(this);
			this.Finalize();
		}

		// Token: 0x0400015A RID: 346
		public unsafe IServiceActivity* m_pSA;
	}
}
