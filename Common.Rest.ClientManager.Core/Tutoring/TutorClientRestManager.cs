using System;
using System.Collections.Generic;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentBookingStudent.BookingRequest;
using TechnoPro.ClockWorkServer.Contracts.DTO.Tutoring;
using TechnoPro.Common.ClientManager.ICore;
using TechnoPro.Common.ClientManager.ICore.Tutoring;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Adapters;
using TechnoPro.Common.Public.Entities.Tutoring;
using TechnoPro.Common.Unity.IoC;
using TechnoPro.Common.Web.Security.Proxy;

namespace TechnoPro.Common.Rest.ClientManager.Core.Tutoring
{
	// Token: 0x0200000A RID: 10
	public class TutorClientRestManager : BearerTokenRestProxy<ITutorClientManager>, ITutorClientManager, IWebService
	{
		// Token: 0x0600003E RID: 62 RVA: 0x00002CD9 File Offset: 0x00000ED9
		public TutorClientRestManager(string serviceAddress, string token = null) : base(serviceAddress, token)
		{
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00002CE3 File Offset: 0x00000EE3
		public TutorClientRestManager(string serviceAddress, string serviceAddressSuffix, string token = null) : base(serviceAddress, serviceAddressSuffix, token)
		{
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00002CEE File Offset: 0x00000EEE
		public IList<TutorWithActiveStatusDTO> LoadAllTutors()
		{
			return base.GetMany<TutorWithActiveStatusDTO>("tutor", true);
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00002CFC File Offset: 0x00000EFC
		public int CreateTutor(string FirstName, string MiddleName, string LastName, string StudentNumber)
		{
			CreateTutorReq createTutorReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<CreateTutorReq>();
			createTutorReq.FirstName = FirstName;
			createTutorReq.MiddleName = MiddleName;
			createTutorReq.LastName = LastName;
			createTutorReq.StudentNumber = StudentNumber;
			return base.Post<CreateTutorReq, int>(createTutorReq, "tutor");
		}

		// Token: 0x06000042 RID: 66 RVA: 0x00002D3D File Offset: 0x00000F3D
		public void ActivateTutor(int TutorPersonId)
		{
			base.Post<int>(TutorPersonId, "tutor/activate");
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00002D4B File Offset: 0x00000F4B
		public void DeActivateTutor(int TutorPersonId)
		{
			base.Post<int>(TutorPersonId, "tutor/deactivate");
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00002D59 File Offset: 0x00000F59
		public TutorWithActiveStatusDTO LoadTutorWithActiveStatusById(int TutorPersonId)
		{
			return base.Get<TutorWithActiveStatusDTO>(string.Format("tutor/tutorwithactivestatus/tutorpersonid/{0}", TutorPersonId), true);
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00002D72 File Offset: 0x00000F72
		public SearchForTutorsResp SearchForTutors(int LuCourseId, string SearchString, int MaxResultCount = 100)
		{
			return base.Get<SearchForTutorsResp>(string.Format("tutor/matching?searchstring={0}&lucourseid={1}&maxreturnresults={2}", SearchString, LuCourseId, MaxResultCount), true);
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00002D94 File Offset: 0x00000F94
		public AppointmentBookingResDTO TryToBookTutorAppointment(AppointmentBookingReqDTO BookingRequest, bool BookAppointmentNow = true)
		{
			TryToBookTutorAppointmentReq tryToBookTutorAppointmentReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<TryToBookTutorAppointmentReq>();
			tryToBookTutorAppointmentReq.BookingRequest = BookingRequest;
			tryToBookTutorAppointmentReq.BookAppointmentNow = BookAppointmentNow;
			return base.Post<TryToBookTutorAppointmentReq, AppointmentBookingResDTO>(tryToBookTutorAppointmentReq, "tutor/trytobookappointment");
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00002DC6 File Offset: 0x00000FC6
		public TutorDTO LoadTutorById(int PersonId)
		{
			return base.Get<TutorDTO>(string.Format("tutor/personid/{0}", PersonId), true);
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00002DDF File Offset: 0x00000FDF
		public void RecordConfidentialityAgreementSignedByTutor(int TutorPersonId)
		{
			base.Post<int>(TutorPersonId, "tutor/recordconfidentialityagreementsigned");
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00002DED File Offset: 0x00000FED
		public eTutorStatus GetTutorStatus(int TutorPersonId)
		{
			return base.Get<eTutorStatus>(string.Format("tutor/status/tutorpersonid/{0}", TutorPersonId), true);
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00002E06 File Offset: 0x00001006
		public void RegisterTutorByExistingPersonId(int PersonId)
		{
			base.Post<int>(PersonId, "tutor/registertutorbyexistingperson");
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00002E14 File Offset: 0x00001014
		public TutorAppointmentDTO LoadTutorAppointment(int AppointmentId)
		{
			return base.Get<TutorAppointmentDTO>(string.Format("tutor/appointment/id/{0}", AppointmentId), true);
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00002E2D File Offset: 0x0000102D
		public IDictionary<int, eTutorStatus> GetTutorStatuses(int[] tutorPersonIds)
		{
			return base.Get<GetTutorStatusesResp>(string.Format("tutor/statuses/tutorpersonids/{0}", tutorPersonIds.CommaSeparatedValuesWithoutSpace<int>()), true).TutorsWithStatus;
		}
	}
}
