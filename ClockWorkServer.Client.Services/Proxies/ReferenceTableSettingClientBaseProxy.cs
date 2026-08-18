using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.Settings;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x02000134 RID: 308
	internal class ReferenceTableSettingClientBaseProxy : ClientBase<IReferenceTableSetting>, IReferenceTableSetting, IService
	{
		// Token: 0x06000C19 RID: 3097 RVA: 0x0001E638 File Offset: 0x0001C838
		public ReferenceTableSettingClientBaseProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x06000C1A RID: 3098 RVA: 0x0001E643 File Offset: 0x0001C843
		public ReferenceTableSettingClientBaseProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x06000C1B RID: 3099 RVA: 0x0001E650 File Offset: 0x0001C850
		public GetValuesFromColumnResp GetValuesFromColumn(GetValuesFromColumnReq request)
		{
			return base.Channel.GetValuesFromColumn(request);
		}

		// Token: 0x06000C1C RID: 3100 RVA: 0x0001E670 File Offset: 0x0001C870
		public GetValuesFromColumnsResp GetValuesFromColumns(GetValuesFromColumnsReq request)
		{
			return base.Channel.GetValuesFromColumns(request);
		}
	}
}
