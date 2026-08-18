using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsWorkshops;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x02000040 RID: 64
	public class WorkshopAppointmentReusableClientProxy : WCFTokenBasedReusableClientProxy<IWorkshopAppointment>, IWorkshopAppointment, IService
	{
		// Token: 0x06000326 RID: 806 RVA: 0x00009B8A File Offset: 0x00007D8A
		public WorkshopAppointmentReusableClientProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x06000327 RID: 807 RVA: 0x00009B95 File Offset: 0x00007D95
		public WorkshopAppointmentReusableClientProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x06000328 RID: 808 RVA: 0x00009BA4 File Offset: 0x00007DA4
		public CancelWorkshopAppointmentResp CancelWorkshopAppointment(CancelWorkshopAppointmentReq Request)
		{
			return this.WrapServiceMethod<CancelWorkshopAppointmentResp>(() => this.Proxy.CancelWorkshopAppointment(Request));
		}

		// Token: 0x06000329 RID: 809 RVA: 0x00009BDC File Offset: 0x00007DDC
		public LoadWorkshopAppointmentsByWorkshopIdResp LoadWorkshopAppointmentsByWorkshopId(LoadWorkshopAppointmentsByWorkshopIdReq Request)
		{
			return this.WrapServiceMethod<LoadWorkshopAppointmentsByWorkshopIdResp>(() => this.Proxy.LoadWorkshopAppointmentsByWorkshopId(Request));
		}

		// Token: 0x0600032A RID: 810 RVA: 0x00009C14 File Offset: 0x00007E14
		public UncancelWorkshopAppointmentResp UncancelWorkshopAppointment(UncancelWorkshopAppointmentReq Request)
		{
			return this.WrapServiceMethod<UncancelWorkshopAppointmentResp>(() => this.Proxy.UncancelWorkshopAppointment(Request));
		}

		// Token: 0x0600032B RID: 811 RVA: 0x00009C4C File Offset: 0x00007E4C
		public DeleteWorkshopAppointmentResp DeleteWorkshopAppointment(DeleteWorkshopAppointmentReq request)
		{
			return this.WrapServiceMethod<DeleteWorkshopAppointmentResp>(() => this.Proxy.DeleteWorkshopAppointment(request));
		}

		// Token: 0x0600032C RID: 812 RVA: 0x00009C84 File Offset: 0x00007E84
		public CreateWorkshopAppointmentResp CreateWorkshopAppointment(CreateWorkshopAppointmentReq Request)
		{
			return this.WrapServiceMethod<CreateWorkshopAppointmentResp>(() => this.Proxy.CreateWorkshopAppointment(Request));
		}

		// Token: 0x0600032D RID: 813 RVA: 0x00009CBC File Offset: 0x00007EBC
		public UpdateWorkshopAppointmentResp UpdateWorkshopAppointment(UpdateWorkshopAppointmentReq Request)
		{
			return this.WrapServiceMethod<UpdateWorkshopAppointmentResp>(() => this.Proxy.UpdateWorkshopAppointment(Request));
		}

		// Token: 0x0600032E RID: 814 RVA: 0x00009CF4 File Offset: 0x00007EF4
		public LoadWorkshopAppointmentResp LoadWorkshopAppointment(LoadWorkshopAppointmentReq Request)
		{
			return this.WrapServiceMethod<LoadWorkshopAppointmentResp>(() => this.Proxy.LoadWorkshopAppointment(Request));
		}

		// Token: 0x0600032F RID: 815 RVA: 0x00009D2C File Offset: 0x00007F2C
		public LoadWorkshopAppointmentsWithNoWorkshopByAppTypeResp LoadWorkshopAppointmentsWithNoWorkshopId(LoadWorkshopAppointmentsWithNoWorkshopByAppTypeReq Request)
		{
			return this.WrapServiceMethod<LoadWorkshopAppointmentsWithNoWorkshopByAppTypeResp>(() => this.Proxy.LoadWorkshopAppointmentsWithNoWorkshopId(Request));
		}

		// Token: 0x06000330 RID: 816 RVA: 0x00009D64 File Offset: 0x00007F64
		public void UpdateWorkshopAppointmentParts(UpdateWorkshopAppointmentPartsReq Request)
		{
			this.WrapServiceMethod(delegate()
			{
				this.Proxy.UpdateWorkshopAppointmentParts(Request);
			});
		}

		// Token: 0x06000331 RID: 817 RVA: 0x00009D9C File Offset: 0x00007F9C
		public void InsertOrUpdateAppointmentMemo(InsertOrUpdateAppointmentMemoReq Request)
		{
			this.WrapServiceMethod(delegate()
			{
				this.Proxy.InsertOrUpdateAppointmentMemo(Request);
			});
		}

		// Token: 0x06000332 RID: 818 RVA: 0x00009DD4 File Offset: 0x00007FD4
		public void UpdateAppointmentWorkshopId(UpdateAppointmentWorkshopIdReq Request)
		{
			this.WrapServiceMethod(delegate()
			{
				this.Proxy.UpdateAppointmentWorkshopId(Request);
			});
		}
	}
}
