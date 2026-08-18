using System;
using System.ServiceModel;

namespace WCFExtrasPlus.Soap
{
	// Token: 0x02000002 RID: 2
	public static class ClientSoapHeaderHelper
	{
		// Token: 0x06000001 RID: 1 RVA: 0x000020D0 File Offset: 0x000002D0
		public static void SetHeader(this IClientChannel channel, string headerName, object value)
		{
			channel.Extensions.Find<SoapHeadersClientHook>().Headers[headerName] = value;
		}

		// Token: 0x06000002 RID: 2 RVA: 0x000020E9 File Offset: 0x000002E9
		public static T GetHeader<T>(this IClientChannel channel, string headerName) where T : class
		{
			return (T)((object)channel.Extensions.Find<SoapHeadersClientHook>().Headers[headerName]);
		}
	}
}
