using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsWorkshops;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x02000041 RID: 65
	internal class WorkshopAppointmentClientBaseProxy : ClientBase<IWorkshopAppointment>, IWorkshopAppointment, IService
	{
		// Token: 0x06000333 RID: 819 RVA: 0x00009E09 File Offset: 0x00008009
		public WorkshopAppointmentClientBaseProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x06000334 RID: 820 RVA: 0x00009E14 File Offset: 0x00008014
		public WorkshopAppointmentClientBaseProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x06000335 RID: 821 RVA: 0x00009E20 File Offset: 0x00008020
		public CancelWorkshopAppointmentResp CancelWorkshopAppointment(CancelWorkshopAppointmentReq Request)
		{
			return base.Channel.CancelWorkshopAppointment(Request);
		}

		// Token: 0x06000336 RID: 822 RVA: 0x00009E40 File Offset: 0x00008040
		public LoadWorkshopAppointmentsByWorkshopIdResp LoadWorkshopAppointmentsByWorkshopId(LoadWorkshopAppointmentsByWorkshopIdReq Request)
		{
			return base.Channel.LoadWorkshopAppointmentsByWorkshopId(Request);
		}

		// Token: 0x06000337 RID: 823 RVA: 0x00009E60 File Offset: 0x00008060
		public UncancelWorkshopAppointmentResp UncancelWorkshopAppointment(UncancelWorkshopAppointmentReq Request)
		{
			return base.Channel.UncancelWorkshopAppointment(Request);
		}

		// Token: 0x06000338 RID: 824 RVA: 0x00009E80 File Offset: 0x00008080
		public DeleteWorkshopAppointmentResp DeleteWorkshopAppointment(DeleteWorkshopAppointmentReq request)
		{
			return base.Channel.DeleteWorkshopAppointment(request);
		}

		// Token: 0x06000339 RID: 825 RVA: 0x00009EA0 File Offset: 0x000080A0
		public CreateWorkshopAppointmentResp CreateWorkshopAppointment(CreateWorkshopAppointmentReq Request)
		{
			return base.Channel.CreateWorkshopAppointment(Request);
		}

		// Token: 0x0600033A RID: 826 RVA: 0x00009EC0 File Offset: 0x000080C0
		public UpdateWorkshopAppointmentResp UpdateWorkshopAppointment(UpdateWorkshopAppointmentReq Request)
		{
			return base.Channel.UpdateWorkshopAppointment(Request);
		}

		// Token: 0x0600033B RID: 827 RVA: 0x00009EE0 File Offset: 0x000080E0
		public LoadWorkshopAppointmentResp LoadWorkshopAppointment(LoadWorkshopAppointmentReq Request)
		{
			return base.Channel.LoadWorkshopAppointment(Request);
		}

		// Token: 0x0600033C RID: 828 RVA: 0x00009F00 File Offset: 0x00008100
		public LoadWorkshopAppointmentsWithNoWorkshopByAppTypeResp LoadWorkshopAppointmentsWithNoWorkshopId(LoadWorkshopAppointmentsWithNoWorkshopByAppTypeReq Request)
		{
			return base.Channel.LoadWorkshopAppointmentsWithNoWorkshopId(Request);
		}

		// Token: 0x0600033D RID: 829 RVA: 0x00009F1E File Offset: 0x0000811E
		public void UpdateWorkshopAppointmentParts(UpdateWorkshopAppointmentPartsReq Request)
		{
			base.Channel.UpdateWorkshopAppointmentParts(Request);
		}

		// Token: 0x0600033E RID: 830 RVA: 0x00009F2E File Offset: 0x0000812E
		public void InsertOrUpdateAppointmentMemo(InsertOrUpdateAppointmentMemoReq Request)
		{
			base.Channel.InsertOrUpdateAppointmentMemo(Request);
		}

		// Token: 0x0600033F RID: 831 RVA: 0x00009F3E File Offset: 0x0000813E
		public void UpdateAppointmentWorkshopId(UpdateAppointmentWorkshopIdReq Request)
		{
			base.Channel.UpdateAppointmentWorkshopId(Request);
		}
	}
}
