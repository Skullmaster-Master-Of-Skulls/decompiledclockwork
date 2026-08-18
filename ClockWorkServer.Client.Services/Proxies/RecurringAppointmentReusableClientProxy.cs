using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsRecurring;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x02000024 RID: 36
	public class RecurringAppointmentReusableClientProxy : WCFTokenBasedReusableClientProxy<IRecurringAppointment>, IRecurringAppointment, IService
	{
		// Token: 0x06000206 RID: 518 RVA: 0x0000728E File Offset: 0x0000548E
		public RecurringAppointmentReusableClientProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x06000207 RID: 519 RVA: 0x00007299 File Offset: 0x00005499
		public RecurringAppointmentReusableClientProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x06000208 RID: 520 RVA: 0x000072A8 File Offset: 0x000054A8
		public LoadCurrentRecurringAppointmentsSetResp LoadCurrentRecurringAppointmentsSet(LoadCurrentRecurringAppointmentsSetReq Request)
		{
			return this.WrapServiceMethod<LoadCurrentRecurringAppointmentsSetResp>(() => this.Proxy.LoadCurrentRecurringAppointmentsSet(Request));
		}

		// Token: 0x06000209 RID: 521 RVA: 0x000072E0 File Offset: 0x000054E0
		public void UpdateRecurringAppointmentGroupInformationAndDates(UpdateRecurringAppointmentGroupInformationAndDatesReq Request)
		{
			this.WrapServiceMethod(delegate()
			{
				this.Proxy.UpdateRecurringAppointmentGroupInformationAndDates(Request);
			});
		}

		// Token: 0x0600020A RID: 522 RVA: 0x00007318 File Offset: 0x00005518
		public UpdateRecurringAppointmentInstancesResp UpdateRecurringAppointmentInstances(UpdateRecurringAppointmentInstancesReq Request)
		{
			return this.WrapServiceMethod<UpdateRecurringAppointmentInstancesResp>(() => this.Proxy.UpdateRecurringAppointmentInstances(Request));
		}

		// Token: 0x0600020B RID: 523 RVA: 0x00007350 File Offset: 0x00005550
		public IsUserAllowedToEditAllAppointmentsInARecurringSetResp IsUserAllowedToEditAllAppointmentsInARecurringSet(IsUserAllowedToEditAllAppointmentsInARecurringSetReq Request)
		{
			return this.WrapServiceMethod<IsUserAllowedToEditAllAppointmentsInARecurringSetResp>(() => this.Proxy.IsUserAllowedToEditAllAppointmentsInARecurringSet(Request));
		}

		// Token: 0x0600020C RID: 524 RVA: 0x00007388 File Offset: 0x00005588
		public LoadAppointmentsInARecurringSetWithPermissionsToEditForASpecificUserResp LoadAppointmentsInARecurringSetWithPermissionsToEditForASpecificUser(LoadAppointmentsInARecurringSetWithPermissionsToEditForASpecificUserReq Request)
		{
			return this.WrapServiceMethod<LoadAppointmentsInARecurringSetWithPermissionsToEditForASpecificUserResp>(() => this.Proxy.LoadAppointmentsInARecurringSetWithPermissionsToEditForASpecificUser(Request));
		}

		// Token: 0x0600020D RID: 525 RVA: 0x000073C0 File Offset: 0x000055C0
		public UpdateRecurringAppointmentAttendeesResp UpdateRecurringAppointmentAttendees(UpdateRecurringAppointmentAttendeesReq Request)
		{
			return this.WrapServiceMethod<UpdateRecurringAppointmentAttendeesResp>(() => this.Proxy.UpdateRecurringAppointmentAttendees(Request));
		}

		// Token: 0x0600020E RID: 526 RVA: 0x000073F8 File Offset: 0x000055F8
		public UpdateRecurringWorkshopAppointmentInstancesResp UpdateRecurringWorkshopAppointmentInstances(UpdateRecurringWorkshopAppointmentInstancesReq Request)
		{
			return this.WrapServiceMethod<UpdateRecurringWorkshopAppointmentInstancesResp>(() => this.Proxy.UpdateRecurringWorkshopAppointmentInstances(Request));
		}
	}
}
