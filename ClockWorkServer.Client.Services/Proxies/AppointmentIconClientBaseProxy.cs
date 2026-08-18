using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.Appointments.BaseAppParameters;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x0200016F RID: 367
	internal class AppointmentIconClientBaseProxy : ClientBase<IAppointmentIcon>, IAppointmentIcon, IService
	{
		// Token: 0x06000E46 RID: 3654 RVA: 0x000250D8 File Offset: 0x000232D8
		public AppointmentIconClientBaseProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x06000E47 RID: 3655 RVA: 0x000250E3 File Offset: 0x000232E3
		public AppointmentIconClientBaseProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x06000E48 RID: 3656 RVA: 0x000250EF File Offset: 0x000232EF
		public void DeleteAppointmentIconsNotInList(DeleteAppointmentIconsNotInListReq Request)
		{
			base.Channel.DeleteAppointmentIconsNotInList(Request);
		}

		// Token: 0x06000E49 RID: 3657 RVA: 0x00025100 File Offset: 0x00023300
		public InsertOrUpdateAppointmentIconResp InsertOrUpdateAppointmentIcon(InsertOrUpdateAppointmentIconReq Request)
		{
			return base.Channel.InsertOrUpdateAppointmentIcon(Request);
		}

		// Token: 0x06000E4A RID: 3658 RVA: 0x00025120 File Offset: 0x00023320
		public LoadAppointmentIconResp LoadAppointmentIcon(LoadAppointmentIconReq Request)
		{
			return base.Channel.LoadAppointmentIcon(Request);
		}

		// Token: 0x06000E4B RID: 3659 RVA: 0x00025140 File Offset: 0x00023340
		public LoadAppointmentIconByIconInfoIdResp LoadAppointmentIconByIconInfoId(LoadAppointmentIconByIconInfoIdReq Request)
		{
			return base.Channel.LoadAppointmentIconByIconInfoId(Request);
		}

		// Token: 0x06000E4C RID: 3660 RVA: 0x00025160 File Offset: 0x00023360
		public LoadAppointmentIconsByAppointmentResp LoadAppointmentIconsByAppointment(LoadAppointmentIconsByAppointmentReq Request)
		{
			return base.Channel.LoadAppointmentIconsByAppointment(Request);
		}

		// Token: 0x06000E4D RID: 3661 RVA: 0x0002517E File Offset: 0x0002337E
		public void DeleteAppointmentIcon(DeleteAppointmentIconReq Request)
		{
			base.Channel.DeleteAppointmentIcon(Request);
		}

		// Token: 0x06000E4E RID: 3662 RVA: 0x00025190 File Offset: 0x00023390
		public LoadAllIconInfosResp LoadAllIconInfos(LoadAllIconInfosReq Request)
		{
			return base.Channel.LoadAllIconInfos(Request);
		}

		// Token: 0x06000E4F RID: 3663 RVA: 0x000251B0 File Offset: 0x000233B0
		public LoadAppointmentIconByIconNumResp LoadAppointmentIconByIconNum(LoadAppointmentIconByIconNumReq Request)
		{
			return base.Channel.LoadAppointmentIconByIconNum(Request);
		}
	}
}
