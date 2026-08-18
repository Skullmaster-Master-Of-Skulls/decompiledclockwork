using System;
using System.Collections.Generic;
using TechnoPro.ClockWorkServer.Contracts.DTO.Appointments;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsWorkshops;
using TechnoPro.Common.ClientManager.ICore;
using TechnoPro.Common.ClientManager.ICore.AppointmentsWorkshops;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities.Appointments;
using TechnoPro.Common.Unity.IoC;
using TechnoPro.Common.Web.Security.Proxy;

namespace TechnoPro.Common.Rest.ClientManager.Core.AppointmentsWorkshops
{
	// Token: 0x02000072 RID: 114
	public class WorkshopAppointmentRestClientManager : BearerTokenRestProxy<IWorkshopAppointmentClientManager>, IWorkshopAppointmentClientManager, IWebService
	{
		// Token: 0x06000458 RID: 1112 RVA: 0x0000C9B7 File Offset: 0x0000ABB7
		public WorkshopAppointmentRestClientManager(string serviceAddress, string token = null) : base(serviceAddress, token)
		{
		}

		// Token: 0x06000459 RID: 1113 RVA: 0x0000C9C1 File Offset: 0x0000ABC1
		public WorkshopAppointmentRestClientManager(string serviceAddress, string serviceAddressSuffix, string token = null) : base(serviceAddress, serviceAddressSuffix, token)
		{
		}

		// Token: 0x0600045A RID: 1114 RVA: 0x0000C9CC File Offset: 0x0000ABCC
		public IList<WorkshopAppointmentDTO> LoadWorkshopAppointmentsByWorkshopId(DateTime StartDate, DateTime EndDate, int WorkshopId)
		{
			return base.GetMany<WorkshopAppointmentDTO>(string.Format("workshopappointment/workshopid/{0}/range/{1}/{2}", WorkshopId, StartDate, EndDate), true);
		}

		// Token: 0x0600045B RID: 1115 RVA: 0x0000C9F1 File Offset: 0x0000ABF1
		public IList<WorkshopAppointmentDTO> LoadWorkshopAppointmentsWithNoWorkshopId(DateTime StartDate, DateTime EndDate, int appTypeId)
		{
			return base.GetMany<WorkshopAppointmentDTO>(string.Format("workshopappointment/withnoworkshopid/apptypeid/{0}/range/{1}/{2}", appTypeId, StartDate, EndDate), true);
		}

		// Token: 0x0600045C RID: 1116 RVA: 0x0000CA16 File Offset: 0x0000AC16
		public int CreateWorkshopAppointment(WorkshopAppointmentDTO WorkshopApp)
		{
			return base.Post<WorkshopAppointmentDTO, int>(WorkshopApp, "workshopappointment");
		}

		// Token: 0x0600045D RID: 1117 RVA: 0x0000CA24 File Offset: 0x0000AC24
		public void UpdateWorkshopAppointment(WorkshopAppointmentDTO WorkshopApp)
		{
			base.Put<WorkshopAppointmentDTO>(WorkshopApp, "workshopappointment");
		}

		// Token: 0x0600045E RID: 1118 RVA: 0x0000CA32 File Offset: 0x0000AC32
		public WorkshopAppointmentDTO LoadWorkshopAppointmentById(int workshopAppId)
		{
			return base.Get<WorkshopAppointmentDTO>(string.Format("workshopappointment/workshopappid/{0}", workshopAppId), true);
		}

		// Token: 0x0600045F RID: 1119 RVA: 0x0000CA4B File Offset: 0x0000AC4B
		public void DeleteWorkshopAppointment(int AppointmentId)
		{
			base.Delete(string.Format("workshopappointment/appid/{0}", AppointmentId));
		}

		// Token: 0x06000460 RID: 1120 RVA: 0x0000CA63 File Offset: 0x0000AC63
		public void UncancelWorkshopAppointment(int AppointmentId)
		{
			base.Post(string.Format("workshopappointment/uncancel/appid/{0}", AppointmentId));
		}

		// Token: 0x06000461 RID: 1121 RVA: 0x0000CA7C File Offset: 0x0000AC7C
		public void CancelWorkshopAppointment(int AppointmentId, AppCancelInfoDTO CancelReason)
		{
			CancelWorkshopAppointmentReq cancelWorkshopAppointmentReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<CancelWorkshopAppointmentReq>();
			cancelWorkshopAppointmentReq.AppointmentId = AppointmentId;
			cancelWorkshopAppointmentReq.CancelInfo = CancelReason;
			base.Post<CancelWorkshopAppointmentReq>(cancelWorkshopAppointmentReq, "workshopappointment/cancel");
		}

		// Token: 0x06000462 RID: 1122 RVA: 0x0000CAB0 File Offset: 0x0000ACB0
		public void UpdateWorkshopAppointmentParts(WorkshopAppointmentDTO Appointment, eAppointmentPart PartsToUpdate)
		{
			UpdateWorkshopAppointmentPartsReq updateWorkshopAppointmentPartsReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<UpdateWorkshopAppointmentPartsReq>();
			updateWorkshopAppointmentPartsReq.WorkshopAppointment = Appointment;
			updateWorkshopAppointmentPartsReq.PartsToUpdate = PartsToUpdate;
			base.Put<UpdateWorkshopAppointmentPartsReq>(updateWorkshopAppointmentPartsReq, "workshopappointment/parts");
		}

		// Token: 0x06000463 RID: 1123 RVA: 0x0000CAE2 File Offset: 0x0000ACE2
		public void InsertOrUpdateAppointmentMemo(int AppointmentId, string MemoText)
		{
			InsertOrUpdateAppointmentMemoReq insertOrUpdateAppointmentMemoReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<InsertOrUpdateAppointmentMemoReq>();
			insertOrUpdateAppointmentMemoReq.AppointmentId = AppointmentId;
			insertOrUpdateAppointmentMemoReq.MemoText = MemoText;
			base.Post("workshopappointment/memo");
		}

		// Token: 0x06000464 RID: 1124 RVA: 0x0000CB08 File Offset: 0x0000AD08
		public void UpdateAppointmentWorkshopId(int AppointmentId, int NewWorkshopId)
		{
			UpdateAppointmentWorkshopIdReq updateAppointmentWorkshopIdReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<UpdateAppointmentWorkshopIdReq>();
			updateAppointmentWorkshopIdReq.AppointmentId = AppointmentId;
			updateAppointmentWorkshopIdReq.NewWorkshopId = NewWorkshopId;
			base.Put<UpdateAppointmentWorkshopIdReq>(updateAppointmentWorkshopIdReq, "workshopappointment/id");
		}
	}
}
