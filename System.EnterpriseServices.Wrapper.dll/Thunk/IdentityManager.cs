using System;
using System.Globalization;
using System.Runtime.InteropServices;

namespace System.EnterpriseServices.Thunk
{
	// Token: 0x02000002 RID: 2
	internal class IdentityManager
	{
		// Token: 0x06000081 RID: 129 RVA: 0x00001000 File Offset: 0x00000400
		private IdentityManager()
		{
		}

		// Token: 0x06000082 RID: 130 RVA: 0x000017D8 File Offset: 0x00000BD8
		private static void Init()
		{
			int num = <Module>.System.EnterpriseServices.Thunk.InitSpy();
			if (num < 0)
			{
				Marshal.ThrowExceptionForHR(num);
			}
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000083 RID: 131 RVA: 0x000017F8 File Offset: 0x00000BF8
		public static bool Enabled
		{
			[return: MarshalAs(UnmanagedType.U1)]
			get
			{
				int num = <Module>.System.EnterpriseServices.Thunk.InitSpy();
				if (num < 0)
				{
					Marshal.ThrowExceptionForHR(num);
				}
				return <Module>.System.EnterpriseServices.Thunk.InitializeSpy.GetEnabled(<Module>.System.EnterpriseServices.Thunk.g_pSpy) != 0;
			}
		}

		// Token: 0x06000084 RID: 132 RVA: 0x00001828 File Offset: 0x00000C28
		public unsafe static void NoticeApartment()
		{
			int num = <Module>.System.EnterpriseServices.Thunk.InitSpy();
			if (num < 0)
			{
				Marshal.ThrowExceptionForHR(num);
			}
			int num2 = <Module>.System.EnterpriseServices.Thunk.InitSpy();
			if (num2 < 0)
			{
				Marshal.ThrowExceptionForHR(num2);
			}
			if (<Module>.System.EnterpriseServices.Thunk.InitializeSpy.GetEnabled(<Module>.System.EnterpriseServices.Thunk.g_pSpy) != null)
			{
				InitializeSpy* system.EnterpriseServices.Thunk.g_pSpy = <Module>.System.EnterpriseServices.Thunk.g_pSpy;
				int num3 = calli(System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr), system.EnterpriseServices.Thunk.g_pSpy, *(*(long*)system.EnterpriseServices.Thunk.g_pSpy + 80L));
				if (num3 < 0)
				{
					Marshal.ThrowExceptionForHR(num3);
				}
			}
		}

		// Token: 0x06000085 RID: 133 RVA: 0x00001888 File Offset: 0x00000C88
		public unsafe static string CreateIdentityUri(IntPtr pUnk)
		{
			int num = <Module>.System.EnterpriseServices.Thunk.InitSpy();
			if (num < 0)
			{
				Marshal.ThrowExceptionForHR(num);
			}
			InitializeSpy* system.EnterpriseServices.Thunk.g_pSpy = <Module>.System.EnterpriseServices.Thunk.g_pSpy;
			int num2 = calli(System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr), system.EnterpriseServices.Thunk.g_pSpy, *(*(long*)system.EnterpriseServices.Thunk.g_pSpy + 80L));
			if (num2 < 0)
			{
				Marshal.ThrowExceptionForHR(num2);
			}
			long num3 = *(long*)<Module>.System.EnterpriseServices.Thunk.g_pSpy + 64L;
			ulong num5;
			ulong num6;
			int num4 = calli(System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,IUnknown*,System.UInt64*,System.UInt64*), <Module>.System.EnterpriseServices.Thunk.g_pSpy, pUnk.ToInt64(), ref num5, ref num6, *num3);
			if (num4 < 0)
			{
				Marshal.ThrowExceptionForHR(num4);
			}
			ulong num7 = num6;
			ulong num8 = num5;
			return "servicedcomponent-local-identity://" + num8.ToString(CultureInfo.InvariantCulture) + ":" + num7.ToString(CultureInfo.InvariantCulture);
		}

		// Token: 0x06000086 RID: 134 RVA: 0x00001928 File Offset: 0x00000D28
		[return: MarshalAs(UnmanagedType.U1)]
		public unsafe static bool IsInProcess(IntPtr pUnk)
		{
			int num = <Module>.System.EnterpriseServices.Thunk.InitSpy();
			if (num < 0)
			{
				Marshal.ThrowExceptionForHR(num);
			}
			int num2 = 1;
			long num3 = *(long*)<Module>.System.EnterpriseServices.Thunk.g_pSpy + 88L;
			int num4 = calli(System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,IUnknown*,System.Int32*), <Module>.System.EnterpriseServices.Thunk.g_pSpy, pUnk.ToInt64(), ref num2, *num3);
			if (num4 < 0)
			{
				Marshal.ThrowExceptionForHR(num4);
			}
			return num2 != 0;
		}
	}
}
