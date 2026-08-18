using System;
using System.Runtime;
using System.Runtime.InteropServices;

namespace System.ServiceModel.ComIntegration
{
	// Token: 0x02000241 RID: 577
	internal static class OuterProxyWrapper
	{
		// Token: 0x06001117 RID: 4375 RVA: 0x0003EEB4 File Offset: 0x0003D0B4
		public static IntPtr CreateOuterProxyInstance(IProxyManager proxyManager, ref Guid riid)
		{
			IntPtr zero = IntPtr.Zero;
			IProxyProvider proxyProvider = OuterProxyWrapper.proxySupport.GetProxyProvider();
			if (proxyProvider == null)
			{
				throw Fx.AssertAndThrowFatal("Proxy Provider cannot be NULL");
			}
			Guid guid = riid;
			int num = proxyProvider.CreateOuterProxyInstance(proxyManager, ref guid, out zero);
			Marshal.ReleaseComObject(proxyProvider);
			if (num != HR.S_OK)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new COMException(SR.GetString("FailedProxyProviderCreation"), num));
			}
			return zero;
		}

		// Token: 0x06001118 RID: 4376 RVA: 0x0003EF20 File Offset: 0x0003D120
		public static IntPtr CreateDispatchProxy(IntPtr pOuter, IPseudoDispatch proxy)
		{
			IntPtr zero = IntPtr.Zero;
			IProxyProvider proxyProvider = OuterProxyWrapper.proxySupport.GetProxyProvider();
			if (proxyProvider == null)
			{
				throw Fx.AssertAndThrowFatal("Proxy Provider cannot be NULL");
			}
			int num = proxyProvider.CreateDispatchProxyInstance(pOuter, proxy, out zero);
			Marshal.ReleaseComObject(proxyProvider);
			if (num != HR.S_OK)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new COMException(SR.GetString("FailedProxyProviderCreation"), num));
			}
			return zero;
		}

		// Token: 0x0400189C RID: 6300
		private static ProxySupportWrapper proxySupport = new ProxySupportWrapper();
	}
}
