using System;
using System.Collections.Generic;
using TechnoPro.ClockWorkServer.Client.ServiceFactory;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentBookingStudent.BookingRequest;
using TechnoPro.ClockWorkServer.Contracts.DTO.Tutoring;
using TechnoPro.Common.ClientManager.ICore;
using TechnoPro.Common.ClientManager.ICore.Tutoring;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities.Tutoring;
using TechnoPro.Common.Unity.IoC;

namespace TechnoPro.Common.ClientManager.Core.Tutoring
{
	// Token: 0x0200000E RID: 14
	public class TutorClientManager : ITutorClientManager, IWebService
	{
		// Token: 0x0600005F RID: 95 RVA: 0x000039C0 File Offset: 0x00001BC0
		public IList<TutorWithActiveStatusDTO> LoadAllTutors()
		{
			LoadAllTutorsReq request = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadAllTutorsReq>();
			return ClientServiceFactory.GetClientInstance<ITutor>().LoadAllTutors(request).Tutors;
		}

		// Token: 0x06000060 RID: 96 RVA: 0x000039F0 File Offset: 0x00001BF0
		public virtual int CreateTutor(string FirstName, string MiddleName, string LastName, string StudentNumber)
		{
			CreateTutorReq createTutorReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<CreateTutorReq>();
			createTutorReq.FirstName = FirstName;
			createTutorReq.MiddleName = MiddleName;
			createTutorReq.LastName = LastName;
			createTutorReq.StudentNumber = StudentNumber;
			return ClientServiceFactory.GetClientInstance<ITutor>().CreateTutor(createTutorReq).PersonId;
		}

		// Token: 0x06000061 RID: 97 RVA: 0x00003A40 File Offset: 0x00001C40
		public void ActivateTutor(int TutorPersonId)
		{
			ActivateTutorReq activateTutorReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<ActivateTutorReq>();
			activateTutorReq.TutorPersonId = TutorPersonId;
			ClientServiceFactory.GetClientInstance<ITutor>().ActivateTutor(activateTutorReq);
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00003A70 File Offset: 0x00001C70
		public void DeActivateTutor(int TutorPersonId)
		{
			DeActivateTutorReq deActivateTutorReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<DeActivateTutorReq>();
			deActivateTutorReq.TutorPersonId = TutorPersonId;
			ClientServiceFactory.GetClientInstance<ITutor>().DeActivateTutor(deActivateTutorReq);
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00003AA0 File Offset: 0x00001CA0
		public TutorWithActiveStatusDTO LoadTutorWithActiveStatusById(int TutorPersonId)
		{
			LoadTutorWithActiveStatusByIdReq loadTutorWithActiveStatusByIdReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadTutorWithActiveStatusByIdReq>();
			loadTutorWithActiveStatusByIdReq.TutorPersonId = TutorPersonId;
			return ClientServiceFactory.GetClientInstance<ITutor>().LoadTutorWithActiveStatusById(loadTutorWithActiveStatusByIdReq).TutorWithStatus;
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00003AD8 File Offset: 0x00001CD8
		public SearchForTutorsResp SearchForTutors(int LuCourseId, string SearchString, int MaxResultCount = 100)
		{
			SearchForTutorsReq searchForTutorsReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<SearchForTutorsReq>();
			searchForTutorsReq.LuCourseId = LuCourseId;
			searchForTutorsReq.SearchString = SearchString;
			searchForTutorsReq.MaxReturnResults = MaxResultCount;
			return ClientServiceFactory.GetClientInstance<ITutor>().SearchForTutors(searchForTutorsReq);
		}

		// Token: 0x06000065 RID: 101 RVA: 0x00003B18 File Offset: 0x00001D18
		public AppointmentBookingResDTO TryToBookTutorAppointment(AppointmentBookingReqDTO BookingRequest, bool BookAppointmentNow = true)
		{
			TryToBookTutorAppointmentReq tryToBookTutorAppointmentReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<TryToBookTutorAppointmentReq>();
			tryToBookTutorAppointmentReq.BookingRequest = BookingRequest;
			tryToBookTutorAppointmentReq.BookAppointmentNow = BookAppointmentNow;
			return ClientServiceFactory.GetClientInstance<ITutor>().TryToBookTutorAppointment(tryToBookTutorAppointmentReq).BookingResult;
		}

		// Token: 0x06000066 RID: 102 RVA: 0x00003B58 File Offset: 0x00001D58
		public TutorDTO LoadTutorById(int PersonId)
		{
			LoadTutorByPersonIdReq loadTutorByPersonIdReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadTutorByPersonIdReq>();
			loadTutorByPersonIdReq.PersonId = PersonId;
			return ClientServiceFactory.GetClientInstance<ITutor>().LoadTutorByPersonId(loadTutorByPersonIdReq).Tutor;
		}

		// Token: 0x06000067 RID: 103 RVA: 0x00003B90 File Offset: 0x00001D90
		public virtual void RecordConfidentialityAgreementSignedByTutor(int TutorPersonId)
		{
			RecordConfidentialityAgreementSignedByTutorReq recordConfidentialityAgreementSignedByTutorReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<RecordConfidentialityAgreementSignedByTutorReq>();
			recordConfidentialityAgreementSignedByTutorReq.TutorPersonId = TutorPersonId;
			ClientServiceFactory.GetClientInstance<ITutor>().RecordConfidentialityAgreementSignedByTutor(recordConfidentialityAgreementSignedByTutorReq);
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00003BC0 File Offset: 0x00001DC0
		public virtual eTutorStatus GetTutorStatus(int TutorPersonId)
		{
			GetTutorStatusReq getTutorStatusReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<GetTutorStatusReq>();
			getTutorStatusReq.TutorPersonId = TutorPersonId;
			return ClientServiceFactory.GetClientInstance<ITutor>().GetTutorStatus(getTutorStatusReq).Status;
		}

		// Token: 0x06000069 RID: 105 RVA: 0x00003BF8 File Offset: 0x00001DF8
		public virtual void RegisterTutorByExistingPersonId(int PersonId)
		{
			RegisterTutorByExistingPersonIdReq registerTutorByExistingPersonIdReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<RegisterTutorByExistingPersonIdReq>();
			registerTutorByExistingPersonIdReq.TutorPersonId = PersonId;
			ClientServiceFactory.GetClientInstance<ITutor>().RegisterTutorByExistingPersonId(registerTutorByExistingPersonIdReq);
		}

		// Token: 0x0600006A RID: 106 RVA: 0x00003C28 File Offset: 0x00001E28
		public TutorAppointmentDTO LoadTutorAppointment(int AppointmentId)
		{
			LoadTutorAppointmentReq loadTutorAppointmentReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadTutorAppointmentReq>();
			loadTutorAppointmentReq.AppointmentId = AppointmentId;
			return ClientServiceFactory.GetClientInstance<ITutor>().LoadTutorAppointment(loadTutorAppointmentReq).TutorAppointment;
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00003C60 File Offset: 0x00001E60
		public IDictionary<int, eTutorStatus> GetTutorStatuses(int[] tutorPersonIds)
		{
			GetTutorStatusesReq getTutorStatusesReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<GetTutorStatusesReq>();
			getTutorStatusesReq.TutorPersonIds = tutorPersonIds;
			return ClientServiceFactory.GetClientInstance<ITutor>().GetTutorStatuses(getTutorStatusesReq).TutorsWithStatus;
		}
	}
}
