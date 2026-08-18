using System;
using System.EnterpriseServices.Thunk;
using System.Runtime.InteropServices;

namespace System.EnterpriseServices.CompensatingResourceManager
{
	// Token: 0x02000081 RID: 129
	internal class CrmMonitorLogRecords
	{
		// Token: 0x060000D3 RID: 211 RVA: 0x00004524 File Offset: 0x00003924
		public unsafe CrmMonitorLogRecords(IntPtr mon)
		{
			IUnknown* ptr = mon.ToInt64();
			if (ptr == null)
			{
				throw new NullReferenceException();
			}
			ICrmMonitorLogRecords* pMon;
			int num = calli(System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,_GUID modopt(System.Runtime.CompilerServices.IsConst)* modopt(System.Runtime.CompilerServices.IsImplicitlyDereferenced),System.Void**), ptr, ref <Module>.IID_ICrmMonitorLogRecords, ref pMon, *(*(long*)ptr));
			if (num < 0)
			{
				Marshal.ThrowExceptionForHR(num);
			}
			this._pMon = pMon;
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x000045A4 File Offset: 0x000039A4
		public unsafe int GetCount()
		{
			ICrmMonitorLogRecords* pMon = this._pMon;
			int result;
			int num = calli(System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,System.Int32 modopt(System.Runtime.CompilerServices.IsLong)*), pMon, ref result, *(*(long*)pMon + 24L));
			if (num < 0)
			{
				Marshal.ThrowExceptionForHR(num);
			}
			return result;
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x000045D4 File Offset: 0x000039D4
		public unsafe int GetTransactionState()
		{
			ICrmMonitorLogRecords* pMon = this._pMon;
			int result;
			int num = calli(System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,System.Int32*), pMon, ref result, *(*(long*)pMon + 32L));
			if (num < 0)
			{
				Marshal.ThrowExceptionForHR(num);
			}
			return result;
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x00004604 File Offset: 0x00003A04
		public unsafe _LogRecord GetLogRecord(int index)
		{
			ICrmMonitorLogRecords* pMon = this._pMon;
			tagCrmLogRecordRead dwCrmFlags;
			int num = calli(System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,System.UInt32 modopt(System.Runtime.CompilerServices.IsLong),System.EnterpriseServices.Thunk.tagCrmLogRecordRead*), pMon, index, ref dwCrmFlags, *(*(long*)pMon + 48L));
			if (num < 0)
			{
				Marshal.ThrowExceptionForHR(num);
			}
			_LogRecord result = default(_LogRecord);
			result.dwCrmFlags = dwCrmFlags;
			result.dwSequenceNumber = *(ref dwCrmFlags + 4);
			result.blobUserData.cbSize = *(ref dwCrmFlags + 8);
			IntPtr pBlobData = new IntPtr(*(ref dwCrmFlags + 16));
			result.blobUserData.pBlobData = pBlobData;
			return result;
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x00004574 File Offset: 0x00003974
		public unsafe void Dispose()
		{
			ICrmMonitorLogRecords* pMon = this._pMon;
			if (pMon != null)
			{
				ICrmMonitorLogRecords* ptr = pMon;
				object obj = calli(System.UInt32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr), ptr, *(*(long*)ptr + 16L));
				this._pMon = null;
			}
		}

		// Token: 0x04000147 RID: 327
		private unsafe ICrmMonitorLogRecords* _pMon;
	}
}
