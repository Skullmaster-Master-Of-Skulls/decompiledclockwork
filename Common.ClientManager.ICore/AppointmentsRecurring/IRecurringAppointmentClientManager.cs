using System;
using System.Collections.Generic;
using TechnoPro.ClockWorkServer.Contracts.DTO.Appointments;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsCalendar;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsRecurring;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsWorkshops;
using TechnoPro.Common.Public;

namespace TechnoPro.Common.ClientManager.ICore.AppointmentsRecurring
{
	// Token: 0x02000090 RID: 144
	public interface IRecurringAppointmentClientManager : IWebService
	{
		// Token: 0x06000449 RID: 1097
		AppointmentRecurringInfoDTO LoadCurrentRecurringAppointmentsSet(int MasterGroupCode);

		// Token: 0x0600044A RID: 1098
		void UpdateRecurringAppointmentGroupInformationAndDates(AppointmentRecurringInfoDTO RecurringItems);

		// Token: 0x0600044B RID: 1099
		IList<RecurringInstanceDTO> UpdateRecurringAppointmentInstances(BaseExtendedAppointmentDTO MasterAppointment, IList<RecurringInstanceDTO> RecurringInstances, RecurringInstanceSetModifyBehaviourDTO ModifyBehaviour);

		// Token: 0x0600044C RID: 1100
		IList<RecurringInstanceDTO> UpdateRecurringAppointmentInstances(AppointmentDTO MasterAppointment, IList<RecurringInstanceDTO> RecurringInstances, RecurringInstanceSetModifyBehaviourDTO ModifyBehaviour);

		// Token: 0x0600044D RID: 1101
		void DeleteEntireRecurringSet(int GroupCode);

		// Token: 0x0600044E RID: 1102
		bool IsUserAllowedToEditAllAppointmentsInARecurringSet(int AppointmentId, int PersonId);

		// Token: 0x0600044F RID: 1103
		IDictionary<int, bool> LoadAppointmentsInARecurringSetWithPermissionsToEditForASpecificUser(int AppointmentId, int PersonId);

		// Token: 0x06000450 RID: 1104
		void UpdateRecurringAppointmentAttendees(int groupCode, int appIdAlreadyUpdated, IList<AttendeeDTO> attendeesAdded, IList<AttendeeDTO> attendeesModified, IList<int> attendeePersonIdsRemoved);

		// Token: 0x06000451 RID: 1105
		IList<RecurringInstanceDTO> UpdateRecurringWorkshopAppointmentInstances(WorkshopAppointmentDTO workshopApp, IList<RecurringInstanceDTO> RecurringInstances, RecurringInstanceSetModifyBehaviourDTO ModifyBehaivour);
	}
}
