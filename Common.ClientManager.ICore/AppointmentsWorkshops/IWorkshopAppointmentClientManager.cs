using System;
using System.Collections.Generic;
using TechnoPro.ClockWorkServer.Contracts.DTO.Appointments;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsWorkshops;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities.Appointments;

namespace TechnoPro.Common.ClientManager.ICore.AppointmentsWorkshops
{
	// Token: 0x02000081 RID: 129
	public interface IWorkshopAppointmentClientManager : IWebService
	{
		// Token: 0x060003C6 RID: 966
		IList<WorkshopAppointmentDTO> LoadWorkshopAppointmentsByWorkshopId(DateTime StartDate, DateTime EndDate, int WorkshopId);

		// Token: 0x060003C7 RID: 967
		IList<WorkshopAppointmentDTO> LoadWorkshopAppointmentsWithNoWorkshopId(DateTime StartDate, DateTime EndDate, int appTypeId);

		// Token: 0x060003C8 RID: 968
		int CreateWorkshopAppointment(WorkshopAppointmentDTO WorkshopApp);

		// Token: 0x060003C9 RID: 969
		void UpdateWorkshopAppointment(WorkshopAppointmentDTO WorkshopApp);

		// Token: 0x060003CA RID: 970
		WorkshopAppointmentDTO LoadWorkshopAppointmentById(int workshopAppId);

		// Token: 0x060003CB RID: 971
		void DeleteWorkshopAppointment(int AppointmentId);

		// Token: 0x060003CC RID: 972
		void UncancelWorkshopAppointment(int AppointmentId);

		// Token: 0x060003CD RID: 973
		void CancelWorkshopAppointment(int AppointmentId, AppCancelInfoDTO CancelReason);

		// Token: 0x060003CE RID: 974
		void UpdateWorkshopAppointmentParts(WorkshopAppointmentDTO Appointment, eAppointmentPart PartsToUpdate);

		// Token: 0x060003CF RID: 975
		void InsertOrUpdateAppointmentMemo(int AppointmentId, string MemoText);

		// Token: 0x060003D0 RID: 976
		void UpdateAppointmentWorkshopId(int AppointmentId, int NewWorkshopId);
	}
}
