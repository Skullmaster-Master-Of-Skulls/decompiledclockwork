using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsRecurring;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x02000025 RID: 37
	internal class RecurringAppointmentClientBaseProxy : ClientBase<IRecurringAppointment>, IRecurringAppointment, IService
	{
		// Token: 0x0600020F RID: 527 RVA: 0x00007430 File Offset: 0x00005630
		public RecurringAppointmentClientBaseProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x06000210 RID: 528 RVA: 0x0000743B File Offset: 0x0000563B
		public RecurringAppointmentClientBaseProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x06000211 RID: 529 RVA: 0x00007448 File Offset: 0x00005648
		public LoadCurrentRecurringAppointmentsSetResp LoadCurrentRecurringAppointmentsSet(LoadCurrentRecurringAppointmentsSetReq Request)
		{
			return base.Channel.LoadCurrentRecurringAppointmentsSet(Request);
		}

		// Token: 0x06000212 RID: 530 RVA: 0x00007466 File Offset: 0x00005666
		public void UpdateRecurringAppointmentGroupInformationAndDates(UpdateRecurringAppointmentGroupInformationAndDatesReq Request)
		{
			base.Channel.UpdateRecurringAppointmentGroupInformationAndDates(Request);
		}

		// Token: 0x06000213 RID: 531 RVA: 0x00007478 File Offset: 0x00005678
		public UpdateRecurringAppointmentInstancesResp UpdateRecurringAppointmentInstances(UpdateRecurringAppointmentInstancesReq Request)
		{
			return base.Channel.UpdateRecurringAppointmentInstances(Request);
		}

		// Token: 0x06000214 RID: 532 RVA: 0x00007498 File Offset: 0x00005698
		public IsUserAllowedToEditAllAppointmentsInARecurringSetResp IsUserAllowedToEditAllAppointmentsInARecurringSet(IsUserAllowedToEditAllAppointmentsInARecurringSetReq Request)
		{
			return base.Channel.IsUserAllowedToEditAllAppointmentsInARecurringSet(Request);
		}

		// Token: 0x06000215 RID: 533 RVA: 0x000074B8 File Offset: 0x000056B8
		public LoadAppointmentsInARecurringSetWithPermissionsToEditForASpecificUserResp LoadAppointmentsInARecurringSetWithPermissionsToEditForASpecificUser(LoadAppointmentsInARecurringSetWithPermissionsToEditForASpecificUserReq Request)
		{
			return base.Channel.LoadAppointmentsInARecurringSetWithPermissionsToEditForASpecificUser(Request);
		}

		// Token: 0x06000216 RID: 534 RVA: 0x000074D8 File Offset: 0x000056D8
		public UpdateRecurringAppointmentAttendeesResp UpdateRecurringAppointmentAttendees(UpdateRecurringAppointmentAttendeesReq Request)
		{
			return base.Channel.UpdateRecurringAppointmentAttendees(Request);
		}

		// Token: 0x06000217 RID: 535 RVA: 0x000074F8 File Offset: 0x000056F8
		public UpdateRecurringWorkshopAppointmentInstancesResp UpdateRecurringWorkshopAppointmentInstances(UpdateRecurringWorkshopAppointmentInstancesReq Request)
		{
			return base.Channel.UpdateRecurringWorkshopAppointmentInstances(Request);
		}
	}
}
