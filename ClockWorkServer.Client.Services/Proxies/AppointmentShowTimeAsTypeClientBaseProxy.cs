using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.Appointments.BaseAppParameters;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x02000049 RID: 73
	internal class AppointmentShowTimeAsTypeClientBaseProxy : ClientBase<IAppointmentShowTimeAsType>, IAppointmentShowTimeAsType, IService
	{
		// Token: 0x06000395 RID: 917 RVA: 0x0000ABB9 File Offset: 0x00008DB9
		public AppointmentShowTimeAsTypeClientBaseProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x06000396 RID: 918 RVA: 0x0000ABC4 File Offset: 0x00008DC4
		public AppointmentShowTimeAsTypeClientBaseProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x06000397 RID: 919 RVA: 0x0000ABD0 File Offset: 0x00008DD0
		public CreateShowTimeAsTypeResp CreateShowTimeAsType(CreateShowTimeAsTypeReq Request)
		{
			return base.Channel.CreateShowTimeAsType(Request);
		}

		// Token: 0x06000398 RID: 920 RVA: 0x0000ABEE File Offset: 0x00008DEE
		public void DeleteShowTimeAsTypeByAppCode(DeleteShowTimeAsTypeByAppCodeReq Request)
		{
			base.Channel.DeleteShowTimeAsTypeByAppCode(Request);
		}

		// Token: 0x06000399 RID: 921 RVA: 0x0000ABFE File Offset: 0x00008DFE
		public void DeleteShowTimeAsTypeById(DeleteShowTimeAsTypeByIdReq Request)
		{
			base.Channel.DeleteShowTimeAsTypeById(Request);
		}

		// Token: 0x0600039A RID: 922 RVA: 0x0000AC10 File Offset: 0x00008E10
		public LoadAllShowTimeAsTypesResp LoadAllShowTimeAsTypes(LoadAllShowTimeAsTypesReq Request)
		{
			return base.Channel.LoadAllShowTimeAsTypes(Request);
		}

		// Token: 0x0600039B RID: 923 RVA: 0x0000AC30 File Offset: 0x00008E30
		public LoadShowTimeAsTypeByIdResp LoadShowTimeAsTypeById(LoadShowTimeAsTypeByIdReq Request)
		{
			return base.Channel.LoadShowTimeAsTypeById(Request);
		}

		// Token: 0x0600039C RID: 924 RVA: 0x0000AC50 File Offset: 0x00008E50
		public LoadShowTimeAsTypeByAppCodeResp LoadShowTimeAsTypeByAppCode(LoadShowTimeAsTypeByAppCodeReq Request)
		{
			return base.Channel.LoadShowTimeAsTypeByAppCode(Request);
		}

		// Token: 0x0600039D RID: 925 RVA: 0x0000AC6E File Offset: 0x00008E6E
		public void UpdateShowTimeAsType(UpdateShowTimeAsTypeReq Request)
		{
			base.Channel.UpdateShowTimeAsType(Request);
		}
	}
}
