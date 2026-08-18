using System;
using System.Collections.Generic;
using TechnoPro.ClockWorkServer.Client.ServiceFactory;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.Appointments;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsWorkshops;
using TechnoPro.Common.ClientManager.ICore;
using TechnoPro.Common.ClientManager.ICore.AppointmentsWorkshops;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities.Appointments;
using TechnoPro.Common.Unity.IoC;

namespace TechnoPro.Common.ClientManager.Core.AppointmentsWorkshops
{
	// Token: 0x02000087 RID: 135
	public class WorkshopAppointmentClientManager : IWorkshopAppointmentClientManager, IWebService
	{
		// Token: 0x060004D8 RID: 1240 RVA: 0x00015D90 File Offset: 0x00013F90
		public IList<WorkshopAppointmentDTO> LoadWorkshopAppointmentsByWorkshopId(DateTime StartDate, DateTime EndDate, int WorkshopId)
		{
			LoadWorkshopAppointmentsByWorkshopIdReq loadWorkshopAppointmentsByWorkshopIdReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadWorkshopAppointmentsByWorkshopIdReq>();
			loadWorkshopAppointmentsByWorkshopIdReq.StartDate = StartDate;
			loadWorkshopAppointmentsByWorkshopIdReq.EndDate = EndDate;
			loadWorkshopAppointmentsByWorkshopIdReq.WorkshopId = WorkshopId;
			return ClientServiceFactory.GetClientInstance<IWorkshopAppointment>().LoadWorkshopAppointmentsByWorkshopId(loadWorkshopAppointmentsByWorkshopIdReq).WorkshopAppointments;
		}

		// Token: 0x060004D9 RID: 1241 RVA: 0x00015DD8 File Offset: 0x00013FD8
		public IList<WorkshopAppointmentDTO> LoadWorkshopAppointmentsWithNoWorkshopId(DateTime StartDate, DateTime EndDate, int appTypeId)
		{
			LoadWorkshopAppointmentsWithNoWorkshopByAppTypeReq loadWorkshopAppointmentsWithNoWorkshopByAppTypeReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadWorkshopAppointmentsWithNoWorkshopByAppTypeReq>();
			loadWorkshopAppointmentsWithNoWorkshopByAppTypeReq.StartDate = StartDate;
			loadWorkshopAppointmentsWithNoWorkshopByAppTypeReq.EndDate = EndDate;
			loadWorkshopAppointmentsWithNoWorkshopByAppTypeReq.AppTypeId = appTypeId;
			return ClientServiceFactory.GetClientInstance<IWorkshopAppointment>().LoadWorkshopAppointmentsWithNoWorkshopId(loadWorkshopAppointmentsWithNoWorkshopByAppTypeReq).WorkshopAppointments;
		}

		// Token: 0x060004DA RID: 1242 RVA: 0x00015E20 File Offset: 0x00014020
		public int CreateWorkshopAppointment(WorkshopAppointmentDTO WorkshopApp)
		{
			CreateWorkshopAppointmentReq createWorkshopAppointmentReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<CreateWorkshopAppointmentReq>();
			createWorkshopAppointmentReq.WorkshopAppointment = WorkshopApp;
			return ClientServiceFactory.GetClientInstance<IWorkshopAppointment>().CreateWorkshopAppointment(createWorkshopAppointmentReq).WorkshopAppointmentId;
		}

		// Token: 0x060004DB RID: 1243 RVA: 0x00015E58 File Offset: 0x00014058
		public void UpdateWorkshopAppointment(WorkshopAppointmentDTO WorkshopApp)
		{
			UpdateWorkshopAppointmentReq updateWorkshopAppointmentReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<UpdateWorkshopAppointmentReq>();
			updateWorkshopAppointmentReq.WorkshopAppointment = WorkshopApp;
			ClientServiceFactory.GetClientInstance<IWorkshopAppointment>().UpdateWorkshopAppointment(updateWorkshopAppointmentReq);
		}

		// Token: 0x060004DC RID: 1244 RVA: 0x00015E88 File Offset: 0x00014088
		public WorkshopAppointmentDTO LoadWorkshopAppointmentById(int workshopAppId)
		{
			LoadWorkshopAppointmentReq loadWorkshopAppointmentReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadWorkshopAppointmentReq>();
			loadWorkshopAppointmentReq.WorkshopAppointmentId = workshopAppId;
			return ClientServiceFactory.GetClientInstance<IWorkshopAppointment>().LoadWorkshopAppointment(loadWorkshopAppointmentReq).Appointment;
		}

		// Token: 0x060004DD RID: 1245 RVA: 0x00015EC0 File Offset: 0x000140C0
		public void DeleteWorkshopAppointment(int AppointmentId)
		{
			DeleteWorkshopAppointmentReq deleteWorkshopAppointmentReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<DeleteWorkshopAppointmentReq>();
			deleteWorkshopAppointmentReq.AppointmentId = AppointmentId;
			ClientServiceFactory.GetClientInstance<IWorkshopAppointment>().DeleteWorkshopAppointment(deleteWorkshopAppointmentReq);
		}

		// Token: 0x060004DE RID: 1246 RVA: 0x00015EF0 File Offset: 0x000140F0
		public void UncancelWorkshopAppointment(int AppointmentId)
		{
			UncancelWorkshopAppointmentReq uncancelWorkshopAppointmentReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<UncancelWorkshopAppointmentReq>();
			uncancelWorkshopAppointmentReq.AppointmentId = AppointmentId;
			ClientServiceFactory.GetClientInstance<IWorkshopAppointment>().UncancelWorkshopAppointment(uncancelWorkshopAppointmentReq);
		}

		// Token: 0x060004DF RID: 1247 RVA: 0x00015F20 File Offset: 0x00014120
		public void CancelWorkshopAppointment(int AppointmentId, AppCancelInfoDTO CancelReason)
		{
			CancelWorkshopAppointmentReq cancelWorkshopAppointmentReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<CancelWorkshopAppointmentReq>();
			cancelWorkshopAppointmentReq.AppointmentId = AppointmentId;
			cancelWorkshopAppointmentReq.CancelInfo = CancelReason;
			ClientServiceFactory.GetClientInstance<IWorkshopAppointment>().CancelWorkshopAppointment(cancelWorkshopAppointmentReq);
		}

		// Token: 0x060004E0 RID: 1248 RVA: 0x00015F58 File Offset: 0x00014158
		public void UpdateWorkshopAppointmentParts(WorkshopAppointmentDTO Appointment, eAppointmentPart PartsToUpdate)
		{
			UpdateWorkshopAppointmentPartsReq updateWorkshopAppointmentPartsReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<UpdateWorkshopAppointmentPartsReq>();
			updateWorkshopAppointmentPartsReq.WorkshopAppointment = Appointment;
			updateWorkshopAppointmentPartsReq.PartsToUpdate = PartsToUpdate;
			ClientServiceFactory.GetClientInstance<IWorkshopAppointment>().UpdateWorkshopAppointmentParts(updateWorkshopAppointmentPartsReq);
		}

		// Token: 0x060004E1 RID: 1249 RVA: 0x00015F90 File Offset: 0x00014190
		public void InsertOrUpdateAppointmentMemo(int AppointmentId, string MemoText)
		{
			InsertOrUpdateAppointmentMemoReq insertOrUpdateAppointmentMemoReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<InsertOrUpdateAppointmentMemoReq>();
			insertOrUpdateAppointmentMemoReq.AppointmentId = AppointmentId;
			insertOrUpdateAppointmentMemoReq.MemoText = MemoText;
			ClientServiceFactory.GetClientInstance<IWorkshopAppointment>().InsertOrUpdateAppointmentMemo(insertOrUpdateAppointmentMemoReq);
		}

		// Token: 0x060004E2 RID: 1250 RVA: 0x00015FC8 File Offset: 0x000141C8
		public void UpdateAppointmentWorkshopId(int AppointmentId, int NewWorkshopId)
		{
			UpdateAppointmentWorkshopIdReq updateAppointmentWorkshopIdReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<UpdateAppointmentWorkshopIdReq>();
			updateAppointmentWorkshopIdReq.AppointmentId = AppointmentId;
			updateAppointmentWorkshopIdReq.NewWorkshopId = NewWorkshopId;
			ClientServiceFactory.GetClientInstance<IWorkshopAppointment>().UpdateAppointmentWorkshopId(updateAppointmentWorkshopIdReq);
		}
	}
}
