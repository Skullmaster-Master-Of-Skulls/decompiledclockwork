using System;
using System.Reflection;
using System.Runtime.InteropServices;

namespace System.EnterpriseServices.Thunk
{
	// Token: 0x02000055 RID: 85
	internal class Tracker
	{
		// Token: 0x060000AA RID: 170 RVA: 0x00002970 File Offset: 0x00001D70
		internal unsafe Tracker(ISendMethodEvents* pTracker)
		{
			this._pTracker = pTracker;
			object obj = calli(System.UInt32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr), pTracker, *(*(long*)pTracker + 8L));
		}

		// Token: 0x060000AB RID: 171 RVA: 0x00003758 File Offset: 0x00002B58
		public unsafe void SendMethodCall(IntPtr pIdentity, MethodBase method)
		{
			if (this._pTracker != null)
			{
				Guid guid = Marshal.GenerateGuidForType(method.ReflectedType);
				_GUID guid2;
				cpblk(ref guid2, ref guid, 16);
				int num = 4;
				if (method.ReflectedType.IsInterface)
				{
					num = Marshal.GetComSlotForMethodInfo(method);
				}
				long num2 = *(long*)this._pTracker + 24L;
				object obj = calli(System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,System.Void modopt(System.Runtime.CompilerServices.IsConst)*,_GUID modopt(System.Runtime.CompilerServices.IsConst)* modopt(System.Runtime.CompilerServices.IsImplicitlyDereferenced),System.UInt32 modopt(System.Runtime.CompilerServices.IsLong)), this._pTracker, (void*)pIdentity, ref guid2, num, *num2);
			}
		}

		// Token: 0x060000AC RID: 172 RVA: 0x000037C8 File Offset: 0x00002BC8
		public unsafe void SendMethodReturn(IntPtr pIdentity, MethodBase method, Exception except)
		{
			if (this._pTracker != null)
			{
				Guid guid = Marshal.GenerateGuidForType(method.ReflectedType);
				_GUID guid2;
				cpblk(ref guid2, ref guid, 16);
				int num = 4;
				if (method.ReflectedType.IsInterface)
				{
					num = Marshal.GetComSlotForMethodInfo(method);
				}
				int num2 = 0;
				if (except != null)
				{
					num2 = Marshal.GetHRForException(except);
				}
				long num3 = *(long*)this._pTracker + 32L;
				object obj = calli(System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,System.Void modopt(System.Runtime.CompilerServices.IsConst)*,_GUID modopt(System.Runtime.CompilerServices.IsConst)* modopt(System.Runtime.CompilerServices.IsImplicitlyDereferenced),System.UInt32 modopt(System.Runtime.CompilerServices.IsLong),System.Int32 modopt(System.Runtime.CompilerServices.IsLong),System.Int32 modopt(System.Runtime.CompilerServices.IsLong)), this._pTracker, (void*)pIdentity, ref guid2, num, 0, num2, *num3);
			}
		}

		// Token: 0x060000AD RID: 173 RVA: 0x00002998 File Offset: 0x00001D98
		public unsafe void Release()
		{
			ISendMethodEvents* pTracker = this._pTracker;
			if (pTracker != null)
			{
				ISendMethodEvents* ptr = pTracker;
				object obj = calli(System.UInt32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr), ptr, *(*(long*)ptr + 16L));
				this._pTracker = null;
			}
		}

		// Token: 0x04000117 RID: 279
		private unsafe ISendMethodEvents* _pTracker;
	}
}
