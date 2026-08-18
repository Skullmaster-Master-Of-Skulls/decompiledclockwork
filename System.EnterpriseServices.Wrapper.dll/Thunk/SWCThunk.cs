using System;
using System.Runtime.InteropServices;

namespace System.EnterpriseServices.Thunk
{
	// Token: 0x02000094 RID: 148
	internal class SWCThunk
	{
		// Token: 0x0600010C RID: 268 RVA: 0x000048DC File Offset: 0x00003CDC
		private SWCThunk()
		{
		}

		// Token: 0x0600010D RID: 269 RVA: 0x00005A70 File Offset: 0x00004E70
		[return: MarshalAs(UnmanagedType.U1)]
		public unsafe static bool IsSWCSupported()
		{
			IUnknown* ptr = null;
			IServiceTransactionConfig* ptr2 = null;
			int num = <Module>.CoCreateInstance(ref <Module>.CLSID_CServiceConfig, null, 1, ref <Module>.IID_IUnknown, (void**)(&ptr));
			if (num == -2147221164)
			{
				return false;
			}
			if (num >= 0)
			{
				num = calli(System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,_GUID modopt(System.Runtime.CompilerServices.IsConst)* modopt(System.Runtime.CompilerServices.IsImplicitlyDereferenced),System.Void**), ptr, ref <Module>.IID_IServiceTransactionConfig, ref ptr2, *(*(long*)ptr));
				if (num == -2147467262)
				{
					IUnknown* ptr3 = ptr;
					object obj = calli(System.UInt32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr), ptr3, *(*(long*)ptr3 + 16L));
					return false;
				}
			}
			if (ptr2 != null)
			{
				IServiceTransactionConfig* ptr4 = ptr2;
				object obj2 = calli(System.UInt32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr), ptr4, *(*(long*)ptr4 + 16L));
			}
			if (ptr != null)
			{
				IUnknown* ptr5 = ptr;
				object obj3 = calli(System.UInt32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr), ptr5, *(*(long*)ptr5 + 16L));
			}
			Marshal.ThrowExceptionForHR(num);
			return true;
		}
	}
}
