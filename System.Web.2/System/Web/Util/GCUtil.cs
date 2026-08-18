using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.Web.Util
{
	// Token: 0x020001E3 RID: 483
	[SecurityPermission(SecurityAction.LinkDemand, UnmanagedCode = true)]
	internal static class GCUtil
	{
		// Token: 0x060017C3 RID: 6083 RVA: 0x0004A8BC File Offset: 0x00048ABC
		public static IntPtr RootObject(object obj)
		{
			if (obj == null)
			{
				return IntPtr.Zero;
			}
			return (IntPtr)GCHandle.Alloc(obj);
		}

		// Token: 0x060017C4 RID: 6084 RVA: 0x0004A8D4 File Offset: 0x00048AD4
		public static object UnrootObject(IntPtr pointer)
		{
			if (pointer != IntPtr.Zero)
			{
				GCHandle gchandle = (GCHandle)pointer;
				if (gchandle.IsAllocated)
				{
					object target = gchandle.Target;
					gchandle.Free();
					return target;
				}
			}
			return null;
		}
	}
}
