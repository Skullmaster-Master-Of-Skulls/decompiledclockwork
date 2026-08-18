using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.Settings;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x02000133 RID: 307
	public class ReferenceTableSettingReusableClientProxy : WCFTokenBasedReusableClientProxy<IReferenceTableSetting>, IReferenceTableSetting, IService
	{
		// Token: 0x06000C15 RID: 3093 RVA: 0x0001E5AE File Offset: 0x0001C7AE
		public ReferenceTableSettingReusableClientProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x06000C16 RID: 3094 RVA: 0x0001E5B9 File Offset: 0x0001C7B9
		public ReferenceTableSettingReusableClientProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x06000C17 RID: 3095 RVA: 0x0001E5C8 File Offset: 0x0001C7C8
		public GetValuesFromColumnResp GetValuesFromColumn(GetValuesFromColumnReq request)
		{
			return this.WrapServiceMethod<GetValuesFromColumnResp>(() => this.Proxy.GetValuesFromColumn(request));
		}

		// Token: 0x06000C18 RID: 3096 RVA: 0x0001E600 File Offset: 0x0001C800
		public GetValuesFromColumnsResp GetValuesFromColumns(GetValuesFromColumnsReq request)
		{
			return this.WrapServiceMethod<GetValuesFromColumnsResp>(() => this.Proxy.GetValuesFromColumns(request));
		}
	}
}
