using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x02000084 RID: 132
	public class AppointmentNotesReusableClientProxy : WCFTokenBasedReusableClientProxy<IAppointmentNotes>, IAppointmentNotes, IService
	{
		// Token: 0x06000575 RID: 1397 RVA: 0x0000F236 File Offset: 0x0000D436
		public AppointmentNotesReusableClientProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x06000576 RID: 1398 RVA: 0x0000F241 File Offset: 0x0000D441
		public AppointmentNotesReusableClientProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x06000577 RID: 1399 RVA: 0x0000F250 File Offset: 0x0000D450
		public LoadAllAppointmentIdsWithNotesResp LoadAllAppointmentIdsWithNotes(LoadAllAppointmentIdsWithNotesReq Request)
		{
			return this.WrapServiceMethod<LoadAllAppointmentIdsWithNotesResp>(() => this.Proxy.LoadAllAppointmentIdsWithNotes(Request));
		}

		// Token: 0x06000578 RID: 1400 RVA: 0x0000F288 File Offset: 0x0000D488
		public GetAppointmentNotesSummaryHtmlResp GetAppointmentNotesSummaryHtml(GetAppointmentNotesSummaryHtmlReq Request)
		{
			return this.WrapServiceMethod<GetAppointmentNotesSummaryHtmlResp>(() => this.Proxy.GetAppointmentNotesSummaryHtml(Request));
		}

		// Token: 0x06000579 RID: 1401 RVA: 0x0000F2C0 File Offset: 0x0000D4C0
		public LoadAppointmentNotesRtfFromFirstRtfInFirstFormAttachedToAppointmentTypeResp LoadAppointmentNotesRtfFromFirstRtfInFirstFormAttachedToAppointmentType(LoadAppointmentNotesRtfFromFirstRtfInFirstFormAttachedToAppointmentTypeReq Request)
		{
			return this.WrapServiceMethod<LoadAppointmentNotesRtfFromFirstRtfInFirstFormAttachedToAppointmentTypeResp>(() => this.Proxy.LoadAppointmentNotesRtfFromFirstRtfInFirstFormAttachedToAppointmentType(Request));
		}

		// Token: 0x0600057A RID: 1402 RVA: 0x0000F2F8 File Offset: 0x0000D4F8
		public LoadNotesAppointmentsForStudentNoAttendeesResp LoadNotesAppointmentsForStudentNoAttendees(LoadNotesAppointmentsForStudentNoAttendeesReq Request)
		{
			return this.WrapServiceMethod<LoadNotesAppointmentsForStudentNoAttendeesResp>(() => this.Proxy.LoadNotesAppointmentsForStudentNoAttendees(Request));
		}

		// Token: 0x0600057B RID: 1403 RVA: 0x0000F330 File Offset: 0x0000D530
		public LoadNotesAppointmentByAppointmentIdResp LoadNotesAppointmentByAppointmentId(LoadNotesAppointmentByAppointmentIdReq Request)
		{
			return this.WrapServiceMethod<LoadNotesAppointmentByAppointmentIdResp>(() => this.Proxy.LoadNotesAppointmentByAppointmentId(Request));
		}

		// Token: 0x0600057C RID: 1404 RVA: 0x0000F368 File Offset: 0x0000D568
		public LoadNotesAppointmentExtendedInfosResp LoadNotesAppointmentExtendedInfos(LoadNotesAppointmentExtendedInfosReq Request)
		{
			return this.WrapServiceMethod<LoadNotesAppointmentExtendedInfosResp>(() => this.Proxy.LoadNotesAppointmentExtendedInfos(Request));
		}

		// Token: 0x0600057D RID: 1405 RVA: 0x0000F3A0 File Offset: 0x0000D5A0
		public void SaveAppointmentNotesToFirstRtfInFirstFormAttachedToAppointmentType(SaveAppointmentNotesToFirstRtfInFirstFormAttachedToAppointmentTypeReq Request)
		{
			this.WrapServiceMethod(delegate()
			{
				this.Proxy.SaveAppointmentNotesToFirstRtfInFirstFormAttachedToAppointmentType(Request);
			});
		}
	}
}
