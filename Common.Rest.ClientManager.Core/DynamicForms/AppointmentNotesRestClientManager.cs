using System;
using System.Collections.Generic;
using TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms;
using TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms.AppointmentNotes;
using TechnoPro.Common.ClientManager.ICore;
using TechnoPro.Common.ClientManager.ICore.DynamicForms;
using TechnoPro.Common.DataStructure;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Adapters;
using TechnoPro.Common.Unity.IoC;
using TechnoPro.Common.Web.Security.Proxy;

namespace TechnoPro.Common.Rest.ClientManager.Core.DynamicForms
{
	// Token: 0x02000052 RID: 82
	public class AppointmentNotesRestClientManager : BearerTokenRestProxy<IAppointmentNotesClientManager>, IAppointmentNotesClientManager, IWebService
	{
		// Token: 0x06000315 RID: 789 RVA: 0x00009713 File Offset: 0x00007913
		public AppointmentNotesRestClientManager(string serviceAddress, string token = null) : base(serviceAddress, token)
		{
		}

		// Token: 0x06000316 RID: 790 RVA: 0x0000971D File Offset: 0x0000791D
		public AppointmentNotesRestClientManager(string serviceAddress, string serviceAddressSuffix, string token = null) : base(serviceAddress, serviceAddressSuffix, token)
		{
		}

		// Token: 0x06000317 RID: 791 RVA: 0x00009728 File Offset: 0x00007928
		public IList<int> LoadAllAppointmentIdsWithNotes(int PersonId, Range<DateTime> DateRange, params int[] ScreenNums)
		{
			return base.GetMany<int>(string.Format("appointmentnotes/allappointmentidswithnotes/studentpid/{0}/range/{1}/{2}/screennums/{3}", new object[]
			{
				PersonId,
				DateRange.Start,
				DateRange.End,
				ScreenNums.CommaSeparatedValuesWithoutSpace<int>()
			}), true);
		}

		// Token: 0x06000318 RID: 792 RVA: 0x0000977A File Offset: 0x0000797A
		public string GetAppointmentNotesSummaryHtml(int PersonId, int[] AppointmentIds, int[] ScreenNums)
		{
			return base.Get<string>(string.Format("appointmentnotes/summaryhtml/studentpid/{0}/appointmentids/{1}/screennums/{2}", PersonId, AppointmentIds.CommaSeparatedValuesWithoutSpace<int>(), ScreenNums.CommaSeparatedValuesWithoutSpace<int>()), true);
		}

		// Token: 0x06000319 RID: 793 RVA: 0x000097A0 File Offset: 0x000079A0
		public void SaveAppointmentNotesToFirstRtfInFirstFormAttachedToAppointmentType(int StudentPersonId, int AppointmentId, int AppTypeId, string NotesRtf)
		{
			SaveAppointmentNotesToFirstRtfInFirstFormAttachedToAppointmentTypeReq saveAppointmentNotesToFirstRtfInFirstFormAttachedToAppointmentTypeReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<SaveAppointmentNotesToFirstRtfInFirstFormAttachedToAppointmentTypeReq>();
			saveAppointmentNotesToFirstRtfInFirstFormAttachedToAppointmentTypeReq.StudentPersonId = StudentPersonId;
			saveAppointmentNotesToFirstRtfInFirstFormAttachedToAppointmentTypeReq.AppointmentId = AppointmentId;
			saveAppointmentNotesToFirstRtfInFirstFormAttachedToAppointmentTypeReq.AppTypeId = AppTypeId;
			saveAppointmentNotesToFirstRtfInFirstFormAttachedToAppointmentTypeReq.NotesRtf = NotesRtf;
			base.Post<SaveAppointmentNotesToFirstRtfInFirstFormAttachedToAppointmentTypeReq>(saveAppointmentNotesToFirstRtfInFirstFormAttachedToAppointmentTypeReq, "appointmentnotes/savetofirstrtfinfirstformattachedtoappointmenttype");
		}

		// Token: 0x0600031A RID: 794 RVA: 0x000097E1 File Offset: 0x000079E1
		public string LoadAppointmentNotesRtfFromFirstRtfInFirstFormAttachedToAppointmentType(int StudentPersonId, int AppointmentId, int AppTypeId)
		{
			return base.Get<string>(string.Format("appointmentnotes/rtffromfirstrtfinfirstformattachedtoappointmenttype/studentpid/{0}/appointmentid/{1}/apptypeid/{2}", StudentPersonId, AppointmentId, AppTypeId), true);
		}

		// Token: 0x0600031B RID: 795 RVA: 0x00009808 File Offset: 0x00007A08
		public IList<NotesAppointmentDTO> LoadNotesAppointmentsForStudentNoAttendees(int primaryStudentPersonId, Range<DateTime> dateRange, IList<int> appTypeIds, IList<int> screenNums)
		{
			return base.GetMany<NotesAppointmentDTO>(string.Format("appointmentnotes/forstudentnoattendees/studentpid/{0}/range/{1}/{2}/apptypesids/{3}/screennums/{4}", new object[]
			{
				primaryStudentPersonId,
				(dateRange != null) ? new DateTime?(dateRange.Start) : null,
				(dateRange != null) ? new DateTime?(dateRange.End) : null,
				appTypeIds.CommaSeparatedValuesWithoutSpace<int>(),
				screenNums.CommaSeparatedValuesWithoutSpace<int>()
			}), true);
		}

		// Token: 0x0600031C RID: 796 RVA: 0x0000988A File Offset: 0x00007A8A
		public NotesAppointmentDTO LoadNotesAppointmentByAppointmentId(int appointmentId, int primaryStudentPersonId, IList<int> screenNums)
		{
			return base.Get<NotesAppointmentDTO>(string.Format("appointmentnotes/appid/{0}/studentpid/{1}/screennums/{2}", appointmentId, primaryStudentPersonId, screenNums.CommaSeparatedValuesWithoutSpace<int>()), true);
		}
	}
}
