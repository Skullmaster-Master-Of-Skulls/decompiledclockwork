using System;
using System.Runtime;
using System.Runtime.InteropServices;

namespace System.ServiceModel.ComIntegration
{
	// Token: 0x02000256 RID: 598
	internal static class InterfaceHelper
	{
		// Token: 0x0600114E RID: 4430 RVA: 0x0003F570 File Offset: 0x0003D770
		internal static IntPtr GetInterfacePtrForObject(Guid iid, object obj)
		{
			IntPtr iunknownForObject = Marshal.GetIUnknownForObject(obj);
			if (IntPtr.Zero == iunknownForObject)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentException(SR.GetString("UnableToRetrievepUnk")));
			}
			IntPtr zero = IntPtr.Zero;
			int num = Marshal.QueryInterface(iunknownForObject, ref iid, out zero);
			Marshal.Release(iunknownForObject);
			if (num != HR.S_OK)
			{
				throw Fx.AssertAndThrow("QueryInterface should succeed");
			}
			return zero;
		}
	}
}
