using System;
using System.Collections.Generic;
using System.Linq;
using TechnoPro.ClockWorkServer.Common.Services.Impl.Adapters;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsWorkshops;
using TechnoPro.Common.Core.Appointments;
using TechnoPro.Common.Core.AppointmentsWorkshops;
using TechnoPro.Common.Core.Mappers.Appointments;
using TechnoPro.Common.Core.Mappers.AppointmentsWorkshops;
using TechnoPro.Common.ICore.Appointments;
using TechnoPro.Common.ICore.AppointmentsWorkshops;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities.AppointmentsWorkshops;

namespace TechnoPro.ClockWorkServer.Common.Services.Impl
{
	// Token: 0x0200001D RID: 29
	public class WorkshopAppointmentServiceManager : IWorkshopAppointment, IService
	{
		// Token: 0x06000151 RID: 337 RVA: 0x000072E4 File Offset: 0x000054E4
		public int CheckConnectivity()
		{
			return 1;
		}

		// Token: 0x06000152 RID: 338 RVA: 0x000072F8 File Offset: 0x000054F8
		public LoadWorkshopAppointmentsWithNoWorkshopByAppTypeResp LoadWorkshopAppointmentsWithNoWorkshopId(LoadWorkshopAppointmentsWithNoWorkshopByAppTypeReq Request)
		{
			IWorkshopAppointmentManager workshopAppointmentManager = new WorkshopAppointmentManager(Request.GetOperationContext());
			List<WorkshopAppointment> list = workshopAppointmentManager.LoadWorkshopAppointmentsWithNoWorkshopId(Request.StartDate, Request.EndDate, Request.AppTypeId).ToList<WorkshopAppointment>();
			LoadWorkshopAppointmentsWithNoWorkshopByAppTypeResp loadWorkshopAppointmentsWithNoWorkshopByAppTypeResp = new LoadWorkshopAppointmentsWithNoWorkshopByAppTypeResp();
			loadWorkshopAppointmentsWithNoWorkshopByAppTypeResp.WorkshopAppointments = list.ConvertAll<WorkshopAppointmentDTO>((WorkshopAppointment f) => f.ToDTO());
			return loadWorkshopAppointmentsWithNoWorkshopByAppTypeResp;
		}

		// Token: 0x06000153 RID: 339 RVA: 0x00007368 File Offset: 0x00005568
		public CancelWorkshopAppointmentResp CancelWorkshopAppointment(CancelWorkshopAppointmentReq Request)
		{
			IWorkshopAppointmentManager workshopAppointmentManager = new WorkshopAppointmentManager(Request.GetOperationContext());
			workshopAppointmentManager.CancelWorkshopAppointment(false, Request.AppointmentId, Request.CancelInfo.ToDomainObject());
			return new CancelWorkshopAppointmentResp();
		}

		// Token: 0x06000154 RID: 340 RVA: 0x000073A4 File Offset: 0x000055A4
		public UncancelWorkshopAppointmentResp UncancelWorkshopAppointment(UncancelWorkshopAppointmentReq Request)
		{
			IWorkshopAppointmentManager workshopAppointmentManager = new WorkshopAppointmentManager(Request.GetOperationContext());
			workshopAppointmentManager.UncancelWorkshopAppointment(false, Request.AppointmentId);
			return new UncancelWorkshopAppointmentResp();
		}

		// Token: 0x06000155 RID: 341 RVA: 0x000073D8 File Offset: 0x000055D8
		public LoadWorkshopAppointmentsByWorkshopIdResp LoadWorkshopAppointmentsByWorkshopId(LoadWorkshopAppointmentsByWorkshopIdReq Request)
		{
			IWorkshopAppointmentManager workshopAppointmentManager = new WorkshopAppointmentManager(Request.GetOperationContext());
			List<WorkshopAppointment> list = workshopAppointmentManager.LoadWorkshopAppointmentsByWorkshopId(Request.StartDate, Request.EndDate, Request.WorkshopId).ToList<WorkshopAppointment>();
			LoadWorkshopAppointmentsByWorkshopIdResp loadWorkshopAppointmentsByWorkshopIdResp = new LoadWorkshopAppointmentsByWorkshopIdResp();
			loadWorkshopAppointmentsByWorkshopIdResp.WorkshopAppointments = list.ConvertAll<WorkshopAppointmentDTO>((WorkshopAppointment f) => f.ToDTO());
			return loadWorkshopAppointmentsByWorkshopIdResp;
		}

		// Token: 0x06000156 RID: 342 RVA: 0x00007448 File Offset: 0x00005648
		public LoadWorkshopAppointmentResp LoadWorkshopAppointment(LoadWorkshopAppointmentReq Request)
		{
			IWorkshopAppointmentManager workshopAppointmentManager = new WorkshopAppointmentManager(Request.GetOperationContext());
			WorkshopAppointment workshopAppointment = workshopAppointmentManager.LoadWorkshopAppointmentById(Request.WorkshopAppointmentId);
			return new LoadWorkshopAppointmentResp
			{
				Appointment = ((workshopAppointment == null) ? null : workshopAppointment.ToDTO())
			};
		}

		// Token: 0x06000157 RID: 343 RVA: 0x0000748C File Offset: 0x0000568C
		public DeleteWorkshopAppointmentResp DeleteWorkshopAppointment(DeleteWorkshopAppointmentReq Request)
		{
			IWorkshopAppointmentManager workshopAppointmentManager = new WorkshopAppointmentManager(Request.GetOperationContext());
			workshopAppointmentManager.DeleteWorkshopAppointment(false, Request.AppointmentId);
			return new DeleteWorkshopAppointmentResp();
		}

		// Token: 0x06000158 RID: 344 RVA: 0x000074C0 File Offset: 0x000056C0
		public CreateWorkshopAppointmentResp CreateWorkshopAppointment(CreateWorkshopAppointmentReq Request)
		{
			IWorkshopAppointmentManager workshopAppointmentManager = new WorkshopAppointmentManager(Request.GetOperationContext());
			int workshopAppointmentId = workshopAppointmentManager.CreateWorkshopAppointment(false, Request.WorkshopAppointment.ToDomainObject());
			return new CreateWorkshopAppointmentResp
			{
				WorkshopAppointmentId = workshopAppointmentId
			};
		}

		// Token: 0x06000159 RID: 345 RVA: 0x00007500 File Offset: 0x00005700
		public UpdateWorkshopAppointmentResp UpdateWorkshopAppointment(UpdateWorkshopAppointmentReq Request)
		{
			IWorkshopAppointmentManager workshopAppointmentManager = new WorkshopAppointmentManager(Request.GetOperationContext());
			workshopAppointmentManager.UpdateWorkshopAppointment(false, Request.WorkshopAppointment.ToDomainObject());
			return new UpdateWorkshopAppointmentResp();
		}

		// Token: 0x0600015A RID: 346 RVA: 0x00007538 File Offset: 0x00005738
		public void UpdateWorkshopAppointmentParts(UpdateWorkshopAppointmentPartsReq Request)
		{
			IBaseAppointmentManager baseAppointmentManager = new BaseAppointmentManager(Request.GetOperationContext());
			baseAppointmentManager.UpdateAppointmentParts(false, Request.WorkshopAppointment.ToDomainObject(), Request.PartsToUpdate);
		}

		// Token: 0x0600015B RID: 347 RVA: 0x0000756C File Offset: 0x0000576C
		public void InsertOrUpdateAppointmentMemo(InsertOrUpdateAppointmentMemoReq Request)
		{
			IBaseAppointmentManager baseAppointmentManager = new BaseAppointmentManager(Request.GetOperationContext());
			baseAppointmentManager.InsertOrUpdateAppointmentMemo(false, Request.AppointmentId, Request.MemoText);
		}

		// Token: 0x0600015C RID: 348 RVA: 0x0000759C File Offset: 0x0000579C
		public void UpdateAppointmentWorkshopId(UpdateAppointmentWorkshopIdReq Request)
		{
			IWorkshopAppointmentManager workshopAppointmentManager = new WorkshopAppointmentManager(Request.GetOperationContext());
			workshopAppointmentManager.UpdateAppointmentWorkshopId(false, Request.AppointmentId, Request.NewWorkshopId);
		}
	}
}
