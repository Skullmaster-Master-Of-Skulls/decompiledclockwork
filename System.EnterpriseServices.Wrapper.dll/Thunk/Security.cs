using System;
using System.Runtime.InteropServices;
using <CppImplementationDetails>;

namespace System.EnterpriseServices.Thunk
{
	// Token: 0x02000043 RID: 67
	internal class Security
	{
		// Token: 0x06000097 RID: 151 RVA: 0x00002720 File Offset: 0x00001B20
		private Security()
		{
		}

		// Token: 0x06000098 RID: 152 RVA: 0x00002734 File Offset: 0x00001B34
		// Note: this type is marked as 'beforefieldinit'.
		static Security()
		{
			Security._fInit = 0;
		}

		// Token: 0x06000099 RID: 153 RVA: 0x0000275C File Offset: 0x00001B5C
		private unsafe static int Init()
		{
			if (Security._fInit == 0)
			{
				lock (typeof(Security))
				{
					if (Security._fInit == 0)
					{
						Security._cPackages = 0U;
						HINSTANCE__* ptr = <Module>.LoadLibraryW((char*)(&<Module>.?A0x98c6aa4b.unnamed-global-0));
						if (ptr != null && ptr != -1L)
						{
							Security.OpenThreadToken = <Module>.GetProcAddress(ptr, (sbyte*)(&<Module>.?A0x98c6aa4b.unnamed-global-1));
							Security.SetThreadToken = <Module>.GetProcAddress(ptr, (sbyte*)(&<Module>.?A0x98c6aa4b.unnamed-global-2));
						}
						Security._fInit = 1;
					}
				}
			}
			return 0;
		}

		// Token: 0x0600009A RID: 154 RVA: 0x000027FC File Offset: 0x00001BFC
		public unsafe static string GetEveryoneAccountName()
		{
			_SID1 sid = 1;
			*(ref sid + 1) = 1;
			*(ref sid + 2) = 0;
			*(ref sid + 3) = 0;
			*(ref sid + 4) = 0;
			*(ref sid + 5) = 0;
			*(ref sid + 6) = 0;
			*(ref sid + 7) = 1;
			*(ref sid + 8) = 0;
			uint num = 260U;
			uint num2 = 260U;
			$ArrayType$$$BY0BAE@_W $ArrayType$$$BY0BAE@_W;
			$ArrayType$$$BY0BAE@_W $ArrayType$$$BY0BAE@_W2;
			int num3;
			if (<Module>.LookupAccountSidW(null, (void*)(&sid), (char*)(&$ArrayType$$$BY0BAE@_W), (uint*)(&num2), (char*)(&$ArrayType$$$BY0BAE@_W2), (uint*)(&num), &num3) == null)
			{
				int num4;
				if (<Module>.GetLastError() <= 0)
				{
					num4 = <Module>.GetLastError();
				}
				else
				{
					num4 = ((<Module>.GetLastError() & 65535) | -2147024896);
				}
				if (num4 < 0)
				{
					Marshal.ThrowExceptionForHR(num4);
				}
			}
			IntPtr ptr = new IntPtr(ref $ArrayType$$$BY0BAE@_W);
			return Marshal.PtrToStringUni(ptr);
		}

		// Token: 0x0600009B RID: 155 RVA: 0x0000289C File Offset: 0x00001C9C
		public unsafe static IntPtr SuspendImpersonation()
		{
			void* value = null;
			int num = Security.Init();
			if (num < 0)
			{
				Marshal.ThrowExceptionForHR(num);
			}
			if (Security.OpenThreadToken != null && Security.SetThreadToken != null && calli(System.Int32 modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.Void*,System.UInt32 modopt(System.Runtime.CompilerServices.IsLong),System.Int32,System.Void**), <Module>.GetCurrentThread(), 4, 1, ref value, Security.OpenThreadToken))
			{
				object obj = calli(System.Int32 modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.Void**,System.Void*), 0L, 0L, Security.SetThreadToken);
				IntPtr result = new IntPtr(value);
				return result;
			}
			return IntPtr.Zero;
		}

		// Token: 0x0600009C RID: 156 RVA: 0x0000290C File Offset: 0x00001D0C
		public static void ResumeImpersonation(IntPtr hToken)
		{
			if (Security.OpenThreadToken != null && Security.SetThreadToken != null)
			{
				IntPtr value = new IntPtr(0);
				if (hToken != value)
				{
					object obj = calli(System.Int32 modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.Void**,System.Void*), 0L, hToken.ToInt64(), Security.SetThreadToken);
					<Module>.CloseHandle(hToken.ToInt64());
				}
			}
		}

		// Token: 0x04000109 RID: 265
		private static int _fInit = 0;

		// Token: 0x0400010A RID: 266
		private static uint _cPackages;

		// Token: 0x0400010B RID: 267
		private unsafe static _SecPkgInfoW* _pPackageInfo;

		// Token: 0x0400010C RID: 268
		private static method OpenThreadToken = 0L;

		// Token: 0x0400010D RID: 269
		private static method SetThreadToken = 0L;
	}
}
