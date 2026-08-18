using System;
using System.Runtime.InteropServices;

namespace System.EnterpriseServices.Thunk
{
	// Token: 0x02000092 RID: 146
	internal class ServiceDomainThunk
	{
		// Token: 0x06000101 RID: 257 RVA: 0x00004844 File Offset: 0x00003C44
		private ServiceDomainThunk()
		{
		}

		// Token: 0x06000102 RID: 258 RVA: 0x00004858 File Offset: 0x00003C58
		// Note: this type is marked as 'beforefieldinit'.
		unsafe static ServiceDomainThunk()
		{
			HINSTANCE__* ptr = <Module>.LoadLibraryW((char*)(&<Module>.??_C@_1BI@NMLGLHFF@?$AAc?$AAo?$AAm?$AAs?$AAv?$AAc?$AAs?$AA?4?$AAd?$AAl?$AAl?$AA?$AA@));
			if (ptr == null || ptr == -1L)
			{
				int num;
				if (<Module>.GetLastError() <= 0)
				{
					num = <Module>.GetLastError();
				}
				else
				{
					num = ((<Module>.GetLastError() & 65535) | -2147024896);
				}
				if (num < 0)
				{
					Marshal.ThrowExceptionForHR(num);
				}
			}
			ServiceDomainThunk.CoEnterServiceDomain = <Module>.GetProcAddress(ptr, (sbyte*)(&<Module>.??_C@_0BF@EEGEFJCM@CoEnterServiceDomain?$AA@));
			ServiceDomainThunk.CoLeaveServiceDomain = <Module>.GetProcAddress(ptr, (sbyte*)(&<Module>.??_C@_0BF@JEIDNIFH@CoLeaveServiceDomain?$AA@));
			ServiceDomainThunk.CoCreateActivity = <Module>.GetProcAddress(ptr, (sbyte*)(&<Module>.??_C@_0BB@LLBGKOGP@CoCreateActivity?$AA@));
		}

		// Token: 0x06000103 RID: 259 RVA: 0x000057C0 File Offset: 0x00004BC0
		public unsafe static void EnterServiceDomain(ServiceConfigThunk psct)
		{
			IUnknown* serviceConfigUnknown = psct.ServiceConfigUnknown;
			int errorCode = calli(System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(IUnknown*), serviceConfigUnknown, ServiceDomainThunk.CoEnterServiceDomain);
			IUnknown* ptr = serviceConfigUnknown;
			object obj = calli(System.UInt32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr), ptr, *(*(long*)ptr + 16L));
			Marshal.ThrowExceptionForHR(errorCode);
		}

		// Token: 0x06000104 RID: 260 RVA: 0x00005800 File Offset: 0x00004C00
		public unsafe static int LeaveServiceDomain()
		{
			int num = 0;
			TransactionStatus* ptr = <Module>.System.EnterpriseServices.Thunk.TransactionStatus.CreateInstance();
			if (ptr == null)
			{
				throw new OutOfMemoryException();
			}
			num = calli(System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(IUnknown*), ptr, ServiceDomainThunk.CoLeaveServiceDomain);
			if (num >= 0)
			{
				object obj = calli(System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,System.Int32 modopt(System.Runtime.CompilerServices.IsLong)*), ptr, ref num, *(*(long*)ptr + 32L));
			}
			TransactionStatus* ptr2 = ptr;
			object obj2 = calli(System.UInt32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr), ptr2, *(*(long*)ptr2 + 16L));
			return num;
		}

		// Token: 0x04000157 RID: 343
		internal static method CoEnterServiceDomain;

		// Token: 0x04000158 RID: 344
		internal static method CoLeaveServiceDomain;

		// Token: 0x04000159 RID: 345
		internal static method CoCreateActivity;
	}
}
