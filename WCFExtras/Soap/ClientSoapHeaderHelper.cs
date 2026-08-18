using System;
using System.ServiceModel;

namespace WCFExtras.Soap
{
	// Token: 0x0200000B RID: 11
	public static class ClientSoapHeaderHelper
	{
		// Token: 0x06000036 RID: 54 RVA: 0x00003019 File Offset: 0x00001219
		public static void SetHeader(this IClientChannel channel, string headerName, object value)
		{
			channel.Extensions.Find<SoapHeadersClientHook>().Headers[headerName] = value;
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00003034 File Offset: 0x00001234
		public static T GetHeader<T>(this IClientChannel channel, string headerName) where T : class
		{
			return (T)((object)channel.Extensions.Find<SoapHeadersClientHook>().Headers[headerName]);
		}
	}
}
