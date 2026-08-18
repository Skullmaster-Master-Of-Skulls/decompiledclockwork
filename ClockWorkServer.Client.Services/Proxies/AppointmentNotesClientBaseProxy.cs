using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x02000085 RID: 133
	internal class AppointmentNotesClientBaseProxy : ClientBase<IAppointmentNotes>, IAppointmentNotes, IService
	{
		// Token: 0x0600057E RID: 1406 RVA: 0x0000F3D5 File Offset: 0x0000D5D5
		public AppointmentNotesClientBaseProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x0600057F RID: 1407 RVA: 0x0000F3E0 File Offset: 0x0000D5E0
		public AppointmentNotesClientBaseProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x06000580 RID: 1408 RVA: 0x0000F3EC File Offset: 0x0000D5EC
		public LoadAllAppointmentIdsWithNotesResp LoadAllAppointmentIdsWithNotes(LoadAllAppointmentIdsWithNotesReq Request)
		{
			return base.Channel.LoadAllAppointmentIdsWithNotes(Request);
		}

		// Token: 0x06000581 RID: 1409 RVA: 0x0000F40C File Offset: 0x0000D60C
		public GetAppointmentNotesSummaryHtmlResp GetAppointmentNotesSummaryHtml(GetAppointmentNotesSummaryHtmlReq Request)
		{
			return base.Channel.GetAppointmentNotesSummaryHtml(Request);
		}

		// Token: 0x06000582 RID: 1410 RVA: 0x0000F42C File Offset: 0x0000D62C
		public LoadAppointmentNotesRtfFromFirstRtfInFirstFormAttachedToAppointmentTypeResp LoadAppointmentNotesRtfFromFirstRtfInFirstFormAttachedToAppointmentType(LoadAppointmentNotesRtfFromFirstRtfInFirstFormAttachedToAppointmentTypeReq Request)
		{
			return base.Channel.LoadAppointmentNotesRtfFromFirstRtfInFirstFormAttachedToAppointmentType(Request);
		}

		// Token: 0x06000583 RID: 1411 RVA: 0x0000F44C File Offset: 0x0000D64C
		public LoadNotesAppointmentsForStudentNoAttendeesResp LoadNotesAppointmentsForStudentNoAttendees(LoadNotesAppointmentsForStudentNoAttendeesReq Request)
		{
			return base.Channel.LoadNotesAppointmentsForStudentNoAttendees(Request);
		}

		// Token: 0x06000584 RID: 1412 RVA: 0x0000F46C File Offset: 0x0000D66C
		public LoadNotesAppointmentByAppointmentIdResp LoadNotesAppointmentByAppointmentId(LoadNotesAppointmentByAppointmentIdReq Request)
		{
			return base.Channel.LoadNotesAppointmentByAppointmentId(Request);
		}

		// Token: 0x06000585 RID: 1413 RVA: 0x0000F48C File Offset: 0x0000D68C
		public LoadNotesAppointmentExtendedInfosResp LoadNotesAppointmentExtendedInfos(LoadNotesAppointmentExtendedInfosReq Request)
		{
			return base.Channel.LoadNotesAppointmentExtendedInfos(Request);
		}

		// Token: 0x06000586 RID: 1414 RVA: 0x0000F4AA File Offset: 0x0000D6AA
		public void SaveAppointmentNotesToFirstRtfInFirstFormAttachedToAppointmentType(SaveAppointmentNotesToFirstRtfInFirstFormAttachedToAppointmentTypeReq Request)
		{
			base.Channel.SaveAppointmentNotesToFirstRtfInFirstFormAttachedToAppointmentType(Request);
		}
	}
}
