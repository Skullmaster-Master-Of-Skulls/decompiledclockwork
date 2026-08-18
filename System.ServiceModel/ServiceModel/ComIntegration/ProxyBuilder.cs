using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace System.ServiceModel.ComIntegration
{
	// Token: 0x02000242 RID: 578
	internal static class ProxyBuilder
	{
		// Token: 0x0600111A RID: 4378 RVA: 0x0003EF90 File Offset: 0x0003D190
		internal static void Build(Dictionary<MonikerHelper.MonikerAttribute, string> propertyTable, ref Guid riid, IntPtr ppv)
		{
			if (IntPtr.Zero == ppv)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("ppv");
			}
			Marshal.WriteIntPtr(ppv, IntPtr.Zero);
			string text;
			IProxyCreator proxyCreator;
			if (propertyTable.TryGetValue(MonikerHelper.MonikerAttribute.Wsdl, out text))
			{
				proxyCreator = new WsdlServiceChannelBuilder(propertyTable);
			}
			else if (propertyTable.TryGetValue(MonikerHelper.MonikerAttribute.MexAddress, out text))
			{
				proxyCreator = new MexServiceChannelBuilder(propertyTable);
			}
			else
			{
				proxyCreator = new TypedServiceChannelBuilder(propertyTable);
			}
			IProxyManager proxyManager = new ProxyManager(proxyCreator);
			Marshal.WriteIntPtr(ppv, OuterProxyWrapper.CreateOuterProxyInstance(proxyManager, ref riid));
		}
	}
}
