using System;
using System.Collections.Generic;
using System.Linq;
using TechnoPro.ClockWorkServer.Common.Services.Impl.Adapters;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsList;
using TechnoPro.ClockWorkServer.Contracts.DTO.AvailabilitySchedule2;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;
using TechnoPro.Common.Core.Appointments;
using TechnoPro.Common.Core.AppointmentsList;
using TechnoPro.Common.Core.Mappers;
using TechnoPro.Common.Core.Mappers.AppointmentsList;
using TechnoPro.Common.Core.Mappers.AvailabilitySchedule2;
using TechnoPro.Common.Core.Mappers.Files;
using TechnoPro.Common.Core.SpireDoc;
using TechnoPro.Common.ICore.Appointments;
using TechnoPro.Common.ICore.AppointmentsList;
using TechnoPro.Common.ICore.MailMerging;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.AppointmentsList;
using TechnoPro.Common.Public.Entities.AvailabilitySchedule2;
using TechnoPro.Common.Public.Entities.Files;
using TechnoPro.Common.Public.Entities.MailMergeEntities.DocumentForPrint;
using TechnoPro.Common.Public.Entities.People;

namespace TechnoPro.ClockWorkServer.Common.Services.Impl
{
	// Token: 0x0200000F RID: 15
	public class ListAppointmentServiceManager : IListAppointment, IService
	{
		// Token: 0x060000AB RID: 171 RVA: 0x0000474C File Offset: 0x0000294C
		public int CheckConnectivity()
		{
			return 1;
		}

		// Token: 0x060000AC RID: 172 RVA: 0x00004760 File Offset: 0x00002960
		public CreateListAppointmentResp CreateListAppointment(CreateListAppointmentReq Request)
		{
			IListAppointmentManager listAppointmentManager = new ListAppointmentManager(Request.GetOperationContext());
			ListAppointment listAppointment = Request.Appointment.ToDomainObject();
			listAppointmentManager.CreateListAppointment(false, listAppointment);
			return new CreateListAppointmentResp
			{
				AppointmentId = listAppointment.AppointmentId
			};
		}

		// Token: 0x060000AD RID: 173 RVA: 0x000047A8 File Offset: 0x000029A8
		public UpdateListAppointmentResp UpdateListAppointment(UpdateListAppointmentReq Request)
		{
			IListAppointmentManager listAppointmentManager = new ListAppointmentManager(Request.GetOperationContext());
			listAppointmentManager.UpdateListAppointment(false, Request.Appointment.ToDomainObject());
			return new UpdateListAppointmentResp();
		}

		// Token: 0x060000AE RID: 174 RVA: 0x000047E0 File Offset: 0x000029E0
		public CancelListAppointmentResp CancelListAppointment(CancelListAppointmentReq Request)
		{
			IListAppointmentManager listAppointmentManager = new ListAppointmentManager(Request.GetOperationContext());
			listAppointmentManager.CancelListAppointment(false, Request.AppointmentId);
			return new CancelListAppointmentResp();
		}

		// Token: 0x060000AF RID: 175 RVA: 0x00004814 File Offset: 0x00002A14
		public UnCancelListAppointmentResp UnCancelListAppointment(UnCancelListAppointmentReq Request)
		{
			IListAppointmentManager listAppointmentManager = new ListAppointmentManager(Request.GetOperationContext());
			listAppointmentManager.UnCancelListAppointment(false, Request.AppointmentId);
			return new UnCancelListAppointmentResp();
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x00004848 File Offset: 0x00002A48
		public MarkListAppointmentAsTentativeResp MarkListAppointmentAsTentative(MarkListAppointmentAsTentativeReq Request)
		{
			IListAppointmentManager listAppointmentManager = new ListAppointmentManager(Request.GetOperationContext());
			listAppointmentManager.MarkListAppointmentAsTentative(false, Request.AppointmentId);
			return new MarkListAppointmentAsTentativeResp();
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x0000487C File Offset: 0x00002A7C
		public UnMarkListAppointmentAsTentativeResp UnMarkListAppointmentAsTentative(UnMarkListAppointmentAsTentativeReq Request)
		{
			IListAppointmentManager listAppointmentManager = new ListAppointmentManager(Request.GetOperationContext());
			listAppointmentManager.UnMarkListAppointmentAsTentative(false, Request.AppointmentId);
			return new UnMarkListAppointmentAsTentativeResp();
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x000048B0 File Offset: 0x00002AB0
		public DeleteListAppointmentResp DeleteListAppointment(DeleteListAppointmentReq Request)
		{
			IListAppointmentManager listAppointmentManager = new ListAppointmentManager(Request.GetOperationContext());
			listAppointmentManager.DeleteListAppointment(false, Request.AppointmentId);
			return new DeleteListAppointmentResp();
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x000048E4 File Offset: 0x00002AE4
		public FreeTimeSearchResp FreeTimeSearch(FreeTimeSearchReq Request)
		{
			IListAppointmentManager listAppointmentManager = new ListAppointmentManager(Request.GetOperationContext());
			List<Availability2Item> list = listAppointmentManager.FreeTimeSearch(Request.PersonIds, Request.StartDateTime, Request.EndDateTime);
			FreeTimeSearchResp freeTimeSearchResp = new FreeTimeSearchResp();
			freeTimeSearchResp.Items = list.ConvertAll<Availability2ItemDTO>((Availability2Item f) => f.ToDTO());
			return freeTimeSearchResp;
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x0000494C File Offset: 0x00002B4C
		public LoadOverlappingAvailabilitiesResp LoadOverlappingAvailabilities(LoadOverlappingAvailabilitiesReq Request)
		{
			IListAppointmentManager listAppointmentManager = new ListAppointmentManager(Request.GetOperationContext());
			List<Availability2Item> list = listAppointmentManager.LoadOverlappingAvailabilities(Request.PersonId, Request.StartDateTime, Request.EndDateTime);
			LoadOverlappingAvailabilitiesResp loadOverlappingAvailabilitiesResp = new LoadOverlappingAvailabilitiesResp();
			loadOverlappingAvailabilitiesResp.Items = list.ConvertAll<Availability2ItemDTO>((Availability2Item f) => f.ToDTO());
			return loadOverlappingAvailabilitiesResp;
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x000049B4 File Offset: 0x00002BB4
		public LoadClosedDaysResp LoadClosedDays(LoadClosedDaysReq Request)
		{
			IListAppointmentManager listAppointmentManager = new ListAppointmentManager(Request.GetOperationContext());
			IList<ClosedDay> source = listAppointmentManager.LoadClosedDays(Request.PersonIds, Request.StartDate, Request.EndDate);
			LoadClosedDaysResp loadClosedDaysResp = new LoadClosedDaysResp();
			loadClosedDaysResp.ClosedDays = source.ToList<ClosedDay>().ConvertAll<ClosedDayDTO>((ClosedDay f) => f.ToDTO());
			return loadClosedDaysResp;
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x00004A24 File Offset: 0x00002C24
		public IsDayClosedResp IsDayClosed(IsDayClosedReq Request)
		{
			IListAppointmentManager listAppointmentManager = new ListAppointmentManager(Request.GetOperationContext());
			ClosedDay closedDay = listAppointmentManager.IsDayClosed(Request.PersonId, Request.Date);
			return new IsDayClosedResp
			{
				IsClosed = (closedDay != null),
				DayClosed = closedDay.ToDTO()
			};
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x00004A74 File Offset: 0x00002C74
		public void CreateClosedDay(CreateClosedDayReq Request)
		{
			IListAppointmentManager listAppointmentManager = new ListAppointmentManager(Request.GetOperationContext());
			listAppointmentManager.CreateClosedDay(Request.ClosedDays.ToList<ClosedDayDTO>().ConvertAll<ClosedDay>((ClosedDayDTO f) => f.ToDomainObject()));
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x00004AC4 File Offset: 0x00002CC4
		public void DeleteClosedDay(DeleteClosedDayReq Request)
		{
			IListAppointmentManager listAppointmentManager = new ListAppointmentManager(Request.GetOperationContext());
			listAppointmentManager.DeleteClosedDay(Request.PersonId, Request.Date);
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x00004AF4 File Offset: 0x00002CF4
		public void CreateAvailabilities(CreateAvailabilitiesReq Request)
		{
			IListAppointmentManager listAppointmentManager = new ListAppointmentManager(Request.GetOperationContext());
			listAppointmentManager.CreateAvailabilities(Request.Availabilities.ConvertAll<Availability2Item>((Availability2ItemDTO f) => f.ToDomainObject()));
		}

		// Token: 0x060000BA RID: 186 RVA: 0x00004B40 File Offset: 0x00002D40
		public void DeleteAvailability(DeleteAvailabilityReq Request)
		{
			IListAppointmentManager listAppointmentManager = new ListAppointmentManager(Request.GetOperationContext());
			listAppointmentManager.DeleteAvailability(Request.AvailabilityIds);
		}

		// Token: 0x060000BB RID: 187 RVA: 0x00004B68 File Offset: 0x00002D68
		public void UpdateAvailability(UpdateAvailabilityReq Request)
		{
			IListAppointmentManager listAppointmentManager = new ListAppointmentManager(Request.GetOperationContext());
			listAppointmentManager.UpdateAvailability(Request.Availabilities.ConvertAll<Availability2Item>((Availability2ItemDTO f) => f.ToDomainObject()));
		}

		// Token: 0x060000BC RID: 188 RVA: 0x00004BB4 File Offset: 0x00002DB4
		public PrintMedicalCalendarResp PrintMedicalCalendar(PrintMedicalCalendarReq Request)
		{
			OperationContext operationContext = Request.GetOperationContext();
			IListAppointmentManager listAppointmentManager = new ListAppointmentManager(operationContext);
			IList<DocumentPrintItem> printItems = listAppointmentManager.GenerateMedicalCalendarDocumentPrintItems(Request.StartDate, Request.NumDays, Request.Staff.ToList<PersonBaseDTO>().ConvertAll<PersonBase>((PersonBaseDTO f) => f.ToDomainObject()), Request.HideCancelled);
			string fileName = string.Format("CalendarPrintout_{0}-{1}.pdf", Request.StartDate.ToString("yyyy.MM.dd"), Request.StartDate.AddDays((double)Request.NumDays).ToString("yyyy.MM.dd"));
			IMailMergingDocManager mailMergingDocManager = new MailMergingDocManager(operationContext);
			BinaryFile binaryFile = mailMergingDocManager.GenerateDocumentFromPrintCodes(printItems, fileName, (eFileFormat)Request.OutputFormat);
			return new PrintMedicalCalendarResp
			{
				File = ((binaryFile == null) ? null : binaryFile.ToDTO())
			};
		}

		// Token: 0x060000BD RID: 189 RVA: 0x00004C94 File Offset: 0x00002E94
		public LoadAvailabilityResp LoadAvailability(LoadAvailabilityReq Request)
		{
			IListAppointmentManager listAppointmentManager = new ListAppointmentManager(Request.GetOperationContext());
			IList<Availability2Item> list = listAppointmentManager.LoadAvailability(Request.PersonIds, Request.StartDate, Request.NumDays);
			LoadAvailabilityResp loadAvailabilityResp = new LoadAvailabilityResp();
			IList<Availability2ItemDTO> availability;
			if (list != null)
			{
				availability = list.ToList<Availability2Item>().ConvertAll<Availability2ItemDTO>((Availability2Item f) => f.ToDTO());
			}
			else
			{
				availability = null;
			}
			loadAvailabilityResp.Availability = availability;
			return loadAvailabilityResp;
		}

		// Token: 0x060000BE RID: 190 RVA: 0x00004D08 File Offset: 0x00002F08
		public LoadAppointmentsResp LoadAppointments(LoadAppointmentsReq Request)
		{
			IListAppointmentManager listAppointmentManager = new ListAppointmentManager(Request.GetOperationContext());
			IList<ListAppointment> list = listAppointmentManager.LoadAppointments(Request.PersonIds, Request.StartDate, Request.NumDays, Request.LoadIsStudentsFirstAppointment);
			LoadAppointmentsResp loadAppointmentsResp = new LoadAppointmentsResp();
			IList<ListAppointmentDTO> appointments;
			if (list != null)
			{
				appointments = list.ToList<ListAppointment>().ConvertAll<ListAppointmentDTO>((ListAppointment f) => f.ToDTO());
			}
			else
			{
				appointments = null;
			}
			loadAppointmentsResp.Appointments = appointments;
			return loadAppointmentsResp;
		}

		// Token: 0x060000BF RID: 191 RVA: 0x00004D84 File Offset: 0x00002F84
		public LoadAppointmentsWithAvailabilityResp LoadAppointmentsWithAvailability(LoadAppointmentsWithAvailabilityReq Request)
		{
			IListAppointmentManager listAppointmentManager = new ListAppointmentManager(Request.GetOperationContext());
			IList<ListAppointmentOrAvailability> list = listAppointmentManager.LoadAppointmentsWithAvailability(Request.PersonIds, Request.StartDate, Request.NumDays, Request.LoadIsStudentsFirstAppointment, Request.HideCancelledAppointments);
			LoadAppointmentsWithAvailabilityResp loadAppointmentsWithAvailabilityResp = new LoadAppointmentsWithAvailabilityResp();
			IList<ListAppointmentOrAvailabilityDTO> appointments;
			if (list != null)
			{
				appointments = list.ToList<ListAppointmentOrAvailability>().ConvertAll<ListAppointmentOrAvailabilityDTO>((ListAppointmentOrAvailability f) => f.ToDTO());
			}
			else
			{
				appointments = null;
			}
			loadAppointmentsWithAvailabilityResp.Appointments = appointments;
			return loadAppointmentsWithAvailabilityResp;
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x00004E04 File Offset: 0x00003004
		public LoadAppointmentByIdResp LoadAppointmentById(LoadAppointmentByIdReq Request)
		{
			IListAppointmentManager listAppointmentManager = new ListAppointmentManager(Request.GetOperationContext());
			ListAppointment listAppointment = listAppointmentManager.LoadAppointmentById(Request.AppointmentId, Request.LoadIsStudentsFirstAppointment);
			return new LoadAppointmentByIdResp
			{
				Appointment = ((listAppointment == null) ? null : listAppointment.ToDTO())
			};
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x00004E50 File Offset: 0x00003050
		public void MarkIn(MarkInReq Request)
		{
			IListAppointmentManager listAppointmentManager = new ListAppointmentManager(Request.GetOperationContext());
			listAppointmentManager.MarkIn(false, Request.AppointmentId, Request.NewIn);
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x00004E80 File Offset: 0x00003080
		public void MarkNoShow(MarkNoShowReq Request)
		{
			IListAppointmentManager listAppointmentManager = new ListAppointmentManager(Request.GetOperationContext());
			listAppointmentManager.MarkNoShow(false, Request.AppointmentId, Request.NewNoShow);
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x00004EB0 File Offset: 0x000030B0
		public void MarkConfirmed(MarkConfirmedReq Request)
		{
			IListAppointmentManager listAppointmentManager = new ListAppointmentManager(Request.GetOperationContext());
			listAppointmentManager.MarkConfirmed(false, Request.AppointmentId, Request.NewConfirmed);
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x00004EE0 File Offset: 0x000030E0
		public LoadSingleDayAvailabilityStatusesByUserResp LoadSingleDayAvailabilityStatusesByUser(LoadSingleDayAvailabilityStatusesByUserReq Request)
		{
			IListAppointmentManager listAppointmentManager = new ListAppointmentManager(Request.GetOperationContext());
			Dictionary<DateTime, eAvailabilityCode> items = listAppointmentManager.LoadSingleDayAvailabilityStatusesByUser(Request.PersonId, Request.StartDate, Request.NumDays);
			return new LoadSingleDayAvailabilityStatusesByUserResp
			{
				Items = items
			};
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x00004F24 File Offset: 0x00003124
		public LoadAvailabilityByIdResp LoadAvailabilityById(LoadAvailabilityByIdReq Request)
		{
			IListAppointmentManager listAppointmentManager = new ListAppointmentManager(Request.GetOperationContext());
			Availability2Item availability2Item = listAppointmentManager.LoadAvailabilityById(Request.Availability2ItemId);
			return new LoadAvailabilityByIdResp
			{
				AvailabilityItem = ((availability2Item == null) ? null : availability2Item.ToDTO())
			};
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x00004F68 File Offset: 0x00003168
		public LoadDistinctUsersAStudentHasHadAtLeastOneAppointmentWithResp LoadDistinctUsersAStudentHasHadAtLeastOneAppointmentWith(LoadDistinctUsersAStudentHasHadAtLeastOneAppointmentWithReq Request)
		{
			IBaseAppointmentManager baseAppointmentManager = new BaseAppointmentManager(Request.GetOperationContext());
			IList<PersonBase> list = baseAppointmentManager.LoadDistinctUsersAStudentHasHadAtLeastOneAppointmentWith(Request.StudentPersonId, Request.StaffGroupIds);
			LoadDistinctUsersAStudentHasHadAtLeastOneAppointmentWithResp loadDistinctUsersAStudentHasHadAtLeastOneAppointmentWithResp = new LoadDistinctUsersAStudentHasHadAtLeastOneAppointmentWithResp();
			IList<PersonBaseDTO> staff;
			if (list != null)
			{
				staff = list.ToList<PersonBase>().ConvertAll<PersonBaseDTO>((PersonBase g) => g.ToDTO());
			}
			else
			{
				staff = null;
			}
			loadDistinctUsersAStudentHasHadAtLeastOneAppointmentWithResp.Staff = staff;
			return loadDistinctUsersAStudentHasHadAtLeastOneAppointmentWithResp;
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x00004FD8 File Offset: 0x000031D8
		public void InsertOrUpdateAppointmentMemo(InsertOrUpdateAppointmentMemoReq Request)
		{
			IBaseAppointmentManager baseAppointmentManager = new BaseAppointmentManager(Request.GetOperationContext());
			baseAppointmentManager.InsertOrUpdateAppointmentMemo(false, Request.AppointmentId, Request.MemoText);
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x00005008 File Offset: 0x00003208
		public void FixAvailabilityAppointmentMappings(FixAvailabilityAppointmentMappingsReq Request)
		{
			IListAppointmentManager listAppointmentManager = new ListAppointmentManager(Request.GetOperationContext());
			listAppointmentManager.FixAvailabilityAppointmentMappings(Request.StartDate, Request.EndDate);
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x00005038 File Offset: 0x00003238
		public LoadAvailability2MarkersResp LoadAvailability2Markers(LoadAvailability2MarkersReq Request)
		{
			IListAppointmentManager listAppointmentManager = new ListAppointmentManager(Request.GetOperationContext());
			IList<Availability2Marker> list = listAppointmentManager.LoadAvailability2Markers();
			LoadAvailability2MarkersResp loadAvailability2MarkersResp = new LoadAvailability2MarkersResp();
			IList<Availability2MarkerDTO> markers;
			if (list != null)
			{
				markers = list.ToList<Availability2Marker>().ConvertAll<Availability2MarkerDTO>((Availability2Marker g) => g.ToDTO());
			}
			else
			{
				markers = null;
			}
			loadAvailability2MarkersResp.Markers = markers;
			return loadAvailability2MarkersResp;
		}

		// Token: 0x060000CA RID: 202 RVA: 0x0000509C File Offset: 0x0000329C
		public CreateAvailability2MarkerResp CreateAvailability2Marker(CreateAvailability2MarkerReq Request)
		{
			IListAppointmentManager listAppointmentManager = new ListAppointmentManager(Request.GetOperationContext());
			int availability2MarkerId = listAppointmentManager.CreateAvailability2Marker(Request.Marker.ToDomainObject());
			return new CreateAvailability2MarkerResp
			{
				Availability2MarkerId = availability2MarkerId
			};
		}

		// Token: 0x060000CB RID: 203 RVA: 0x000050DC File Offset: 0x000032DC
		public void DeleteAvailability2Marker(DeleteAvailability2MarkerReq Request)
		{
			IListAppointmentManager listAppointmentManager = new ListAppointmentManager(Request.GetOperationContext());
			listAppointmentManager.DeleteAvailability2Marker(Request.Availability2MarkerId);
		}

		// Token: 0x060000CC RID: 204 RVA: 0x00005104 File Offset: 0x00003304
		public void UpdateAvailability2Marker(UpdateAvailability2MarkerReq Request)
		{
			IListAppointmentManager listAppointmentManager = new ListAppointmentManager(Request.GetOperationContext());
			listAppointmentManager.UpdateAvailability2Marker(Request.Marker.ToDomainObject());
		}
	}
}
