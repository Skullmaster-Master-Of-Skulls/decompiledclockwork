using System;
using System.Collections.Generic;
using System.Linq;
using TechnoPro.ClockWorkServer.Common.Services.Impl.Adapters;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms;
using TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms.AppointmentNotes;
using TechnoPro.Common.Core.DynamicForms;
using TechnoPro.Common.Core.Mappers.DynamicForms.AppointmentNotes;
using TechnoPro.Common.DataStructure;
using TechnoPro.Common.ICore.DynamicForms;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities.DynamicForms.AppointmentNotes;

namespace TechnoPro.ClockWorkServer.Common.Services.Impl
{
	// Token: 0x0200003B RID: 59
	public class AppointmentNotesServiceManager : IAppointmentNotes, IService
	{
		// Token: 0x0600023F RID: 575 RVA: 0x0000B334 File Offset: 0x00009534
		public int CheckConnectivity()
		{
			return 1;
		}

		// Token: 0x06000240 RID: 576 RVA: 0x0000B348 File Offset: 0x00009548
		public LoadAllAppointmentIdsWithNotesResp LoadAllAppointmentIdsWithNotes(LoadAllAppointmentIdsWithNotesReq Request)
		{
			IAppointmentNotesManager appointmentNotesManager = new AppointmentNotesManager(Request.GetOperationContext());
			IAppointmentNotesManager appointmentNotesManager2 = appointmentNotesManager;
			int studentPersonId = Request.StudentPersonId;
			Range<DateTime> dateRange;
			if (Request.StartDate == null)
			{
				dateRange = null;
			}
			else
			{
				Range<DateTime> range = new Range<DateTime>();
				range.Start = Request.StartDate.Value;
				dateRange = range;
				range.End = ((Request.EndDate != null) ? Request.EndDate.Value : Request.StartDate.Value);
			}
			IList<int> appointmentIds = appointmentNotesManager2.LoadAllAppointmentIdsWithNotes(studentPersonId, dateRange, (Request.ScreenNums == null) ? null : Request.ScreenNums.ToArray<int>());
			return new LoadAllAppointmentIdsWithNotesResp
			{
				AppointmentIds = appointmentIds
			};
		}

		// Token: 0x06000241 RID: 577 RVA: 0x0000B3F8 File Offset: 0x000095F8
		public GetAppointmentNotesSummaryHtmlResp GetAppointmentNotesSummaryHtml(GetAppointmentNotesSummaryHtmlReq Request)
		{
			IAppointmentNotesManager appointmentNotesManager = new AppointmentNotesManager(Request.GetOperationContext());
			string appointmentNotesSummaryHtml = appointmentNotesManager.GetAppointmentNotesSummaryHtml(Request.StudentPersonId, (Request.AppointmentIds == null) ? null : Request.AppointmentIds.ToArray<int>(), (Request.ScreenNums == null) ? null : Request.ScreenNums.ToArray<int>());
			return new GetAppointmentNotesSummaryHtmlResp
			{
				SummaryHtml = appointmentNotesSummaryHtml
			};
		}

		// Token: 0x06000242 RID: 578 RVA: 0x0000B45C File Offset: 0x0000965C
		public void SaveAppointmentNotesToFirstRtfInFirstFormAttachedToAppointmentType(SaveAppointmentNotesToFirstRtfInFirstFormAttachedToAppointmentTypeReq Request)
		{
			IAppointmentNotesManager appointmentNotesManager = new AppointmentNotesManager(Request.GetOperationContext());
			appointmentNotesManager.SaveAppointmentNotesToFirstRtfInFirstFormAttachedToAppointmentType(Request.StudentPersonId, Request.AppointmentId, Request.AppTypeId, Request.NotesRtf ?? "");
		}

		// Token: 0x06000243 RID: 579 RVA: 0x0000B4A0 File Offset: 0x000096A0
		public LoadAppointmentNotesRtfFromFirstRtfInFirstFormAttachedToAppointmentTypeResp LoadAppointmentNotesRtfFromFirstRtfInFirstFormAttachedToAppointmentType(LoadAppointmentNotesRtfFromFirstRtfInFirstFormAttachedToAppointmentTypeReq Request)
		{
			IAppointmentNotesManager appointmentNotesManager = new AppointmentNotesManager(Request.GetOperationContext());
			string text = appointmentNotesManager.LoadAppointmentNotesRtfFromFirstRtfInFirstFormAttachedToAppointmentType(Request.StudentPersonId, Request.AppointmentId, Request.AppTypeId);
			return new LoadAppointmentNotesRtfFromFirstRtfInFirstFormAttachedToAppointmentTypeResp
			{
				NotesRtf = (text ?? "")
			};
		}

		// Token: 0x06000244 RID: 580 RVA: 0x0000B4F0 File Offset: 0x000096F0
		public LoadNotesAppointmentsForStudentNoAttendeesResp LoadNotesAppointmentsForStudentNoAttendees(LoadNotesAppointmentsForStudentNoAttendeesReq Request)
		{
			IAppointmentNotesManager appointmentNotesManager = new AppointmentNotesManager(Request.GetOperationContext());
			Range<DateTime> dateRange = (Request.StartDate != null && Request.EndDate != null) ? new Range<DateTime>(Request.StartDate.Value, Request.EndDate.Value) : null;
			IList<NotesAppointment> list = appointmentNotesManager.LoadNotesAppointmentsForStudentNoAttendees(Request.PrimaryStudentPersonId, dateRange, Request.AppTypeIds, Request.ScreenNums);
			List<NotesAppointmentDTO> list2;
			if (list == null)
			{
				list2 = null;
			}
			else
			{
				list2 = (from g in list
				select g.ToDTO()).ToList<NotesAppointmentDTO>();
			}
			List<NotesAppointmentDTO> list3 = list2;
			foreach (NotesAppointmentDTO notesAppointmentDTO in list3)
			{
				notesAppointmentDTO.Attendees = null;
			}
			return new LoadNotesAppointmentsForStudentNoAttendeesResp
			{
				NotesAppointmentsNoAttendees = list3
			};
		}

		// Token: 0x06000245 RID: 581 RVA: 0x0000B5F8 File Offset: 0x000097F8
		public LoadNotesAppointmentByAppointmentIdResp LoadNotesAppointmentByAppointmentId(LoadNotesAppointmentByAppointmentIdReq Request)
		{
			IAppointmentNotesManager appointmentNotesManager = new AppointmentNotesManager(Request.GetOperationContext());
			NotesAppointment notesAppointment = appointmentNotesManager.LoadNotesAppointmentByAppointmentId(Request.AppointmentId, Request.PrimaryStudentPersonId, Request.ScreenNums);
			return new LoadNotesAppointmentByAppointmentIdResp
			{
				NotesAppointment = ((notesAppointment != null) ? notesAppointment.ToDTO() : null)
			};
		}

		// Token: 0x06000246 RID: 582 RVA: 0x0000B648 File Offset: 0x00009848
		public LoadNotesAppointmentExtendedInfosResp LoadNotesAppointmentExtendedInfos(LoadNotesAppointmentExtendedInfosReq Request)
		{
			IAppointmentNotesManager appointmentNotesManager = new AppointmentNotesManager(Request.GetOperationContext());
			IAppointmentNotesManager appointmentNotesManager2 = appointmentNotesManager;
			IList<int> appointmentIds = Request.AppointmentIds;
			IList<NotesAppointmentExtendedInfo> list = appointmentNotesManager2.LoadNotesAppointmentExtendedInfos((appointmentIds != null) ? appointmentIds.ToArray<int>() : null);
			LoadNotesAppointmentExtendedInfosResp loadNotesAppointmentExtendedInfosResp = new LoadNotesAppointmentExtendedInfosResp();
			IList<NotesAppointmentExtendedInfoDTO> notesAppointmentExtendedInfos;
			if (list == null)
			{
				notesAppointmentExtendedInfos = null;
			}
			else
			{
				notesAppointmentExtendedInfos = (from g in list
				select g.ToDTO()).ToList<NotesAppointmentExtendedInfoDTO>();
			}
			loadNotesAppointmentExtendedInfosResp.NotesAppointmentExtendedInfos = notesAppointmentExtendedInfos;
			return loadNotesAppointmentExtendedInfosResp;
		}
	}
}
