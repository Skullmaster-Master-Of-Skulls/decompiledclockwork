using System;
using System.EnterpriseServices.Thunk;
using System.Runtime.InteropServices;

namespace System.EnterpriseServices.CompensatingResourceManager
{
	// Token: 0x02000082 RID: 130
	internal class CrmLogControl
	{
		// Token: 0x060000D8 RID: 216 RVA: 0x000042E4 File Offset: 0x000036E4
		public unsafe CrmLogControl(IntPtr p)
		{
			IUnknown* ptr = p.ToInt64();
			if (ptr == null)
			{
				throw new NullReferenceException();
			}
			ICrmLogControl* pCtrl;
			int num = calli(System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,_GUID modopt(System.Runtime.CompilerServices.IsConst)* modopt(System.Runtime.CompilerServices.IsImplicitlyDereferenced),System.Void**), ptr, ref <Module>.IID_ICrmLogControl, ref pCtrl, *(*(long*)ptr));
			if (num < 0)
			{
				Marshal.ThrowExceptionForHR(num);
			}
			this._pCtrl = pCtrl;
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x00004294 File Offset: 0x00003694
		public unsafe CrmLogControl()
		{
			this._pCtrl = null;
			ICrmLogControl* pCtrl;
			int num = <Module>.CoCreateInstance(ref <Module>.CLSID_CRMClerk, null, 21, ref <Module>.IID_ICrmLogControl, (void**)(&pCtrl));
			if (num < 0)
			{
				Marshal.ThrowExceptionForHR(num);
			}
			this._pCtrl = pCtrl;
		}

		// Token: 0x060000DA RID: 218 RVA: 0x00004364 File Offset: 0x00003764
		public unsafe string GetTransactionUOW()
		{
			ICrmLogControl* pCtrl = this._pCtrl;
			char* ptr;
			int num = calli(System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,System.Char**), pCtrl, ref ptr, *(*(long*)pCtrl + 24L));
			if (num < 0)
			{
				Marshal.ThrowExceptionForHR(num);
			}
			IntPtr ptr2 = new IntPtr((void*)ptr);
			string result = Marshal.PtrToStringBSTR(ptr2);
			<Module>.SysFreeString(ptr);
			return result;
		}

		// Token: 0x060000DB RID: 219 RVA: 0x000043B4 File Offset: 0x000037B4
		public unsafe void RegisterCompensator(string progid, string desc, int flags)
		{
			char* ptr = null;
			char* ptr2 = null;
			try
			{
				char* ptr3 = Marshal.StringToCoTaskMemUni(progid).ToInt64();
				char* ptr4 = Marshal.StringToCoTaskMemUni(desc).ToInt64();
				ICrmLogControl* pCtrl = this._pCtrl;
				int num = calli(System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,System.Char modopt(System.Runtime.CompilerServices.IsConst)*,System.Char modopt(System.Runtime.CompilerServices.IsConst)*,System.Int32 modopt(System.Runtime.CompilerServices.IsLong)), pCtrl, ptr3, ptr4, flags, *(*(long*)pCtrl + 32L));
				if (num < 0)
				{
					Marshal.ThrowExceptionForHR(num);
				}
			}
			finally
			{
				<Module>.CoTaskMemFree((void*)ptr);
				<Module>.CoTaskMemFree((void*)ptr2);
			}
		}

		// Token: 0x060000DC RID: 220 RVA: 0x00004444 File Offset: 0x00003844
		public unsafe void ForceLog()
		{
			ICrmLogControl* pCtrl = this._pCtrl;
			int num = calli(System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr), pCtrl, *(*(long*)pCtrl + 48L));
			if (num < 0)
			{
				Marshal.ThrowExceptionForHR(num);
			}
		}

		// Token: 0x060000DD RID: 221 RVA: 0x00004474 File Offset: 0x00003874
		public unsafe void ForgetLogRecord()
		{
			ICrmLogControl* pCtrl = this._pCtrl;
			int num = calli(System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr), pCtrl, *(*(long*)pCtrl + 56L));
			if (num < 0)
			{
				Marshal.ThrowExceptionForHR(num);
			}
		}

		// Token: 0x060000DE RID: 222 RVA: 0x000044A4 File Offset: 0x000038A4
		public unsafe void ForceTransactionToAbort()
		{
			ICrmLogControl* pCtrl = this._pCtrl;
			int num = calli(System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr), pCtrl, *(*(long*)pCtrl + 64L));
			if (num < 0)
			{
				Marshal.ThrowExceptionForHR(num);
			}
		}

		// Token: 0x060000DF RID: 223 RVA: 0x000044D4 File Offset: 0x000038D4
		public unsafe void WriteLogRecord(byte[] b)
		{
			tagBLOB length = b.Length;
			fixed (byte* ptr = &b[0])
			{
				*(ref length + 8) = ptr;
				ICrmLogControl* pCtrl = this._pCtrl;
				int num = calli(System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,tagBLOB* modopt(System.Runtime.CompilerServices.IsConst) modopt(System.Runtime.CompilerServices.IsConst),System.UInt32 modopt(System.Runtime.CompilerServices.IsLong)), pCtrl, ref length, 1, *(*(long*)pCtrl + 72L));
				if (num < 0)
				{
					Marshal.ThrowExceptionForHR(num);
				}
			}
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x00004334 File Offset: 0x00003734
		public unsafe void Dispose()
		{
			ICrmLogControl* pCtrl = this._pCtrl;
			if (pCtrl != null)
			{
				ICrmLogControl* ptr = pCtrl;
				object obj = calli(System.UInt32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr), ptr, *(*(long*)ptr + 16L));
				this._pCtrl = null;
			}
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x00004824 File Offset: 0x00003C24
		public CrmMonitorLogRecords GetMonitor()
		{
			IntPtr mon = new IntPtr(this._pCtrl);
			return new CrmMonitorLogRecords(mon);
		}

		// Token: 0x04000148 RID: 328
		private unsafe ICrmLogControl* _pCtrl;
	}
}
