using System;
using System.Collections.Generic;
using TechnoPro.ClockWorkServer.Contracts.DTO.Appointments;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsCalendar;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsList;

namespace TechnoPro.Common.ClientManager.Notifications.Entities
{
	// Token: 0x02000019 RID: 25
	public static class MessagingAdapters
	{
		// Token: 0x060000B2 RID: 178 RVA: 0x00003208 File Offset: 0x00001408
		public static MessageAppointmentsParameter GetMessageAppointmentsParameterFromAppointment(this BaseBasicAppointmentDTO Appointment)
		{
			if (Appointment == null)
			{
				return null;
			}
			MessageAppointmentsParameter messageAppointmentsParameter = new MessageAppointmentsParameter();
			messageAppointmentsParameter.AppointmentId = Appointment.AppointmentId;
			messageAppointmentsParameter.StartDate = Appointment.StartDateTime;
			messageAppointmentsParameter.PersonIds = Appointment.Attendees.ConvertAll<int>((AttendeeDTO f) => f.Person.PersonId);
			return messageAppointmentsParameter;
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x00003268 File Offset: 0x00001468
		public static AppointmentDTO GetAppointmentFromListAppointment(this ListAppointmentDTO listApp)
		{
			AppointmentDTO appointmentDTO = new AppointmentDTO
			{
				AppointmentId = listApp.AppointmentId,
				StartDateTime = listApp.StartDate,
				Attendees = new List<AttendeeDTO>()
			};
			if (listApp.Staff != null)
			{
				appointmentDTO.Attendees.Add(new AttendeeDTO
				{
					Person = listApp.Staff
				});
			}
			if (listApp.Student != null)
			{
				appointmentDTO.Attendees.Add(new AttendeeDTO
				{
					Person = listApp.Student
				});
			}
			return appointmentDTO;
		}
	}
}
