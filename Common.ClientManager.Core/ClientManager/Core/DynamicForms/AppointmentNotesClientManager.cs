using System;
using System.Collections.Generic;
using TechnoPro.ClockWorkServer.Client.ServiceFactory;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms;
using TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms.AppointmentNotes;
using TechnoPro.Common.ClientManager.ICore;
using TechnoPro.Common.ClientManager.ICore.DynamicForms;
using TechnoPro.Common.DataStructure;
using TechnoPro.Common.Public;
using TechnoPro.Common.Unity.IoC;

namespace TechnoPro.Common.ClientManager.Core.DynamicForms
{
	// Token: 0x02000063 RID: 99
	public class AppointmentNotesClientManager : IAppointmentNotesClientManager, IWebService
	{
		// Token: 0x06000389 RID: 905 RVA: 0x0000FC28 File Offset: 0x0000DE28
		public IList<int> LoadAllAppointmentIdsWithNotes(int PersonId, Range<DateTime> DateRange, params int[] ScreenNums)
		{
			LoadAllAppointmentIdsWithNotesReq loadAllAppointmentIdsWithNotesReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadAllAppointmentIdsWithNotesReq>();
			loadAllAppointmentIdsWithNotesReq.StudentPersonId = PersonId;
			loadAllAppointmentIdsWithNotesReq.StartDate = ((DateRange == null) ? null : new DateTime?(DateRange.Start));
			loadAllAppointmentIdsWithNotesReq.EndDate = ((DateRange == null) ? null : new DateTime?(DateRange.End));
			loadAllAppointmentIdsWithNotesReq.ScreenNums = ScreenNums;
			return ClientServiceFactory.GetClientInstance<IAppointmentNotes>().LoadAllAppointmentIdsWithNotes(loadAllAppointmentIdsWithNotesReq).AppointmentIds;
		}

		// Token: 0x0600038A RID: 906 RVA: 0x0000FCA8 File Offset: 0x0000DEA8
		public string GetAppointmentNotesSummaryHtml(int PersonId, int[] AppointmentIds, int[] ScreenNums)
		{
			GetAppointmentNotesSummaryHtmlReq getAppointmentNotesSummaryHtmlReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<GetAppointmentNotesSummaryHtmlReq>();
			getAppointmentNotesSummaryHtmlReq.StudentPersonId = PersonId;
			getAppointmentNotesSummaryHtmlReq.AppointmentIds = AppointmentIds;
			getAppointmentNotesSummaryHtmlReq.ScreenNums = ScreenNums;
			return ClientServiceFactory.GetClientInstance<IAppointmentNotes>().GetAppointmentNotesSummaryHtml(getAppointmentNotesSummaryHtmlReq).SummaryHtml;
		}

		// Token: 0x0600038B RID: 907 RVA: 0x0000FCF0 File Offset: 0x0000DEF0
		public void SaveAppointmentNotesToFirstRtfInFirstFormAttachedToAppointmentType(int StudentPersonId, int AppointmentId, int AppTypeId, string NotesRtf)
		{
			SaveAppointmentNotesToFirstRtfInFirstFormAttachedToAppointmentTypeReq saveAppointmentNotesToFirstRtfInFirstFormAttachedToAppointmentTypeReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<SaveAppointmentNotesToFirstRtfInFirstFormAttachedToAppointmentTypeReq>();
			saveAppointmentNotesToFirstRtfInFirstFormAttachedToAppointmentTypeReq.StudentPersonId = StudentPersonId;
			saveAppointmentNotesToFirstRtfInFirstFormAttachedToAppointmentTypeReq.AppointmentId = AppointmentId;
			saveAppointmentNotesToFirstRtfInFirstFormAttachedToAppointmentTypeReq.AppTypeId = AppTypeId;
			saveAppointmentNotesToFirstRtfInFirstFormAttachedToAppointmentTypeReq.NotesRtf = NotesRtf;
			ClientServiceFactory.GetClientInstance<IAppointmentNotes>().SaveAppointmentNotesToFirstRtfInFirstFormAttachedToAppointmentType(saveAppointmentNotesToFirstRtfInFirstFormAttachedToAppointmentTypeReq);
		}

		// Token: 0x0600038C RID: 908 RVA: 0x0000FD38 File Offset: 0x0000DF38
		public string LoadAppointmentNotesRtfFromFirstRtfInFirstFormAttachedToAppointmentType(int StudentPersonId, int AppointmentId, int AppTypeId)
		{
			LoadAppointmentNotesRtfFromFirstRtfInFirstFormAttachedToAppointmentTypeReq loadAppointmentNotesRtfFromFirstRtfInFirstFormAttachedToAppointmentTypeReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadAppointmentNotesRtfFromFirstRtfInFirstFormAttachedToAppointmentTypeReq>();
			loadAppointmentNotesRtfFromFirstRtfInFirstFormAttachedToAppointmentTypeReq.StudentPersonId = StudentPersonId;
			loadAppointmentNotesRtfFromFirstRtfInFirstFormAttachedToAppointmentTypeReq.AppointmentId = AppointmentId;
			loadAppointmentNotesRtfFromFirstRtfInFirstFormAttachedToAppointmentTypeReq.AppTypeId = AppTypeId;
			return ClientServiceFactory.GetClientInstance<IAppointmentNotes>().LoadAppointmentNotesRtfFromFirstRtfInFirstFormAttachedToAppointmentType(loadAppointmentNotesRtfFromFirstRtfInFirstFormAttachedToAppointmentTypeReq).NotesRtf;
		}

		// Token: 0x0600038D RID: 909 RVA: 0x0000FD80 File Offset: 0x0000DF80
		public IList<NotesAppointmentDTO> LoadNotesAppointmentsForStudentNoAttendees(int primaryStudentPersonId, Range<DateTime> dateRange, IList<int> appTypeIds, IList<int> screenNums)
		{
			LoadNotesAppointmentsForStudentNoAttendeesReq loadNotesAppointmentsForStudentNoAttendeesReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadNotesAppointmentsForStudentNoAttendeesReq>();
			loadNotesAppointmentsForStudentNoAttendeesReq.PrimaryStudentPersonId = primaryStudentPersonId;
			loadNotesAppointmentsForStudentNoAttendeesReq.StartDate = ((dateRange != null) ? new DateTime?(dateRange.Start) : null);
			loadNotesAppointmentsForStudentNoAttendeesReq.EndDate = ((dateRange != null) ? new DateTime?(dateRange.End) : null);
			loadNotesAppointmentsForStudentNoAttendeesReq.AppTypeIds = appTypeIds;
			loadNotesAppointmentsForStudentNoAttendeesReq.ScreenNums = screenNums;
			LoadNotesAppointmentsForStudentNoAttendeesResp loadNotesAppointmentsForStudentNoAttendeesResp = ClientServiceFactory.GetClientInstance<IAppointmentNotes>().LoadNotesAppointmentsForStudentNoAttendees(loadNotesAppointmentsForStudentNoAttendeesReq);
			return (loadNotesAppointmentsForStudentNoAttendeesResp != null) ? loadNotesAppointmentsForStudentNoAttendeesResp.NotesAppointmentsNoAttendees : null;
		}

		// Token: 0x0600038E RID: 910 RVA: 0x0000FE10 File Offset: 0x0000E010
		public NotesAppointmentDTO LoadNotesAppointmentByAppointmentId(int appointmentId, int primaryStudentPersonId, IList<int> screenNums)
		{
			LoadNotesAppointmentByAppointmentIdReq loadNotesAppointmentByAppointmentIdReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadNotesAppointmentByAppointmentIdReq>();
			loadNotesAppointmentByAppointmentIdReq.PrimaryStudentPersonId = primaryStudentPersonId;
			loadNotesAppointmentByAppointmentIdReq.AppointmentId = appointmentId;
			loadNotesAppointmentByAppointmentIdReq.ScreenNums = screenNums;
			LoadNotesAppointmentByAppointmentIdResp loadNotesAppointmentByAppointmentIdResp = ClientServiceFactory.GetClientInstance<IAppointmentNotes>().LoadNotesAppointmentByAppointmentId(loadNotesAppointmentByAppointmentIdReq);
			return (loadNotesAppointmentByAppointmentIdResp != null) ? loadNotesAppointmentByAppointmentIdResp.NotesAppointment : null;
		}

		// Token: 0x0600038F RID: 911 RVA: 0x0000FE5C File Offset: 0x0000E05C
		public IList<NotesAppointmentExtendedInfoDTO> LoadNotesAppointmentExtendedInfos(params int[] appointmentIds)
		{
			LoadNotesAppointmentExtendedInfosReq loadNotesAppointmentExtendedInfosReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadNotesAppointmentExtendedInfosReq>();
			loadNotesAppointmentExtendedInfosReq.AppointmentIds = appointmentIds;
			LoadNotesAppointmentExtendedInfosResp loadNotesAppointmentExtendedInfosResp = ClientServiceFactory.GetClientInstance<IAppointmentNotes>().LoadNotesAppointmentExtendedInfos(loadNotesAppointmentExtendedInfosReq);
			return (loadNotesAppointmentExtendedInfosResp != null) ? loadNotesAppointmentExtendedInfosResp.NotesAppointmentExtendedInfos : null;
		}
	}
}
