using System;
using System.EnterpriseServices.Thunk;
using System.Runtime.InteropServices;

namespace System.EnterpriseServices.CompensatingResourceManager
{
	// Token: 0x02000083 RID: 131
	internal class CrmMonitor
	{
		// Token: 0x060000E2 RID: 226 RVA: 0x00004684 File Offset: 0x00003A84
		public unsafe CrmMonitor()
		{
			ICrmMonitor* pMon;
			int num = <Module>.CoCreateInstance(ref <Module>.CLSID_CRMRecoveryClerk, null, 21, ref <Module>.IID_ICrmMonitor, (void**)(&pMon));
			if (num < 0)
			{
				Marshal.ThrowExceptionForHR(num);
			}
			this._pMon = pMon;
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x000046C4 File Offset: 0x00003AC4
		public unsafe object GetClerks()
		{
			ICrmMonitor* pMon = this._pMon;
			ICrmMonitorClerks* ptr;
			int num = calli(System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,System.EnterpriseServices.Thunk.ICrmMonitorClerks**), pMon, ref ptr, *(*(long*)pMon + 24L));
			if (num < 0)
			{
				Marshal.ThrowExceptionForHR(num);
			}
			object result = null;
			try
			{
				IntPtr pUnk = new IntPtr((void*)ptr);
				result = Marshal.GetObjectForIUnknown(pUnk);
			}
			finally
			{
				ICrmMonitorClerks* ptr2 = ptr;
				object obj = calli(System.UInt32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr), ptr2, *(*(long*)ptr2 + 16L));
			}
			return result;
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x00004734 File Offset: 0x00003B34
		public unsafe CrmLogControl HoldClerk(object idx)
		{
			CrmLogControl result = null;
			tagVARIANT tagVARIANT;
			IntPtr intPtr = new IntPtr(ref tagVARIANT);
			IntPtr pDstNativeVariant = intPtr;
			<Module>.VariantInit(&tagVARIANT);
			tagVARIANT tagVARIANT2;
			<Module>.VariantInit(&tagVARIANT2);
			Marshal.GetNativeVariantForObject(idx, pDstNativeVariant);
			ICrmMonitor* pMon = this._pMon;
			int num = calli(System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,tagVARIANT,tagVARIANT*), pMon, tagVARIANT, ref tagVARIANT2, *(*(long*)pMon + 32L));
			<Module>.VariantClear(&tagVARIANT);
			if (num < 0)
			{
				Marshal.ThrowExceptionForHR(num);
			}
			IUnknown* value = *(ref tagVARIANT2 + 8);
			if (*(ref tagVARIANT2 + 8) != 0L)
			{
				try
				{
					IntPtr p = new IntPtr(value);
					result = new CrmLogControl(p);
				}
				finally
				{
					<Module>.VariantClear(&tagVARIANT2);
				}
			}
			return result;
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x000047E4 File Offset: 0x00003BE4
		public unsafe void AddRef()
		{
			ICrmMonitor* pMon = this._pMon;
			object obj = calli(System.UInt32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr), pMon, *(*(long*)pMon + 8L));
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x00004804 File Offset: 0x00003C04
		public unsafe void Release()
		{
			ICrmMonitor* pMon = this._pMon;
			object obj = calli(System.UInt32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr), pMon, *(*(long*)pMon + 16L));
		}

		// Token: 0x04000149 RID: 329
		private unsafe ICrmMonitor* _pMon;
	}
}
