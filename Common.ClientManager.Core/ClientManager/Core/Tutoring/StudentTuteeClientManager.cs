using System;
using System.Collections.Generic;
using System.Linq;
using TechnoPro.ClockWorkServer.Client.ServiceFactory;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.Tutoring;
using TechnoPro.Common.ClientManager.ICore;
using TechnoPro.Common.ClientManager.ICore.Tutoring;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities.Tutoring;
using TechnoPro.Common.Unity.IoC;

namespace TechnoPro.Common.ClientManager.Core.Tutoring
{
	// Token: 0x0200000D RID: 13
	public class StudentTuteeClientManager : IStudentTuteeClientManager, IWebService
	{
		// Token: 0x06000059 RID: 89 RVA: 0x00003888 File Offset: 0x00001A88
		public virtual eTuteeStatus GetTuteeStatus(int StudentPersonId)
		{
			GetTuteeStatusReq getTuteeStatusReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<GetTuteeStatusReq>();
			getTuteeStatusReq.StudentPersonId = StudentPersonId;
			return ClientServiceFactory.GetClientInstance<IStudentTutee>().GetTuteeStatus(getTuteeStatusReq).Status;
		}

		// Token: 0x0600005A RID: 90 RVA: 0x000038C0 File Offset: 0x00001AC0
		public virtual void RecordConfidentialityAgreementSignedByStudent(int StudentPersonId)
		{
			RecordConfidentialityAgreementSignedByStudentReq recordConfidentialityAgreementSignedByStudentReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<RecordConfidentialityAgreementSignedByStudentReq>();
			recordConfidentialityAgreementSignedByStudentReq.StudentPersonId = StudentPersonId;
			ClientServiceFactory.GetClientInstance<IStudentTutee>().RecordConfidentialityAgreementSignedByStudent(recordConfidentialityAgreementSignedByStudentReq);
		}

		// Token: 0x0600005B RID: 91 RVA: 0x000038F0 File Offset: 0x00001AF0
		public void MarkStudentCantFindTutor(int PersonId, int searchLucid, string searchLuc, string searchString)
		{
			MarkStudentCantFindTutorReq markStudentCantFindTutorReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<MarkStudentCantFindTutorReq>();
			markStudentCantFindTutorReq.StudentPersonId = PersonId;
			markStudentCantFindTutorReq.SearchLucid = searchLucid;
			markStudentCantFindTutorReq.SearchLuc = searchLuc;
			markStudentCantFindTutorReq.SearchString = searchString;
			ClientServiceFactory.GetClientInstance<IStudentTutee>().MarkStudentCantFindTutor(markStudentCantFindTutorReq);
		}

		// Token: 0x0600005C RID: 92 RVA: 0x00003938 File Offset: 0x00001B38
		public void MarkStudentCantFindAvailability(int PersonId, params int[] TutorPids)
		{
			MarkStudentCantFindAvailabilityReq markStudentCantFindAvailabilityReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<MarkStudentCantFindAvailabilityReq>();
			markStudentCantFindAvailabilityReq.StudentPersonId = PersonId;
			markStudentCantFindAvailabilityReq.TutorPids = ((TutorPids == null) ? null : TutorPids.ToList<int>());
			ClientServiceFactory.GetClientInstance<IStudentTutee>().MarkStudentCantFindAvailability(markStudentCantFindAvailabilityReq);
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00003978 File Offset: 0x00001B78
		public IList<MyTutorDTO> GetStudentMyTutors(int StudentPersonId, DateTime? StartDateTime, DateTime? EndDate)
		{
			GetStudentMyTutorsReq getStudentMyTutorsReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<GetStudentMyTutorsReq>();
			getStudentMyTutorsReq.StudentPersonId = StudentPersonId;
			getStudentMyTutorsReq.StartDateTime = StartDateTime;
			getStudentMyTutorsReq.EndDate = EndDate;
			return ClientServiceFactory.GetClientInstance<IStudentTutee>().GetStudentMyTutors(getStudentMyTutorsReq).MyTutors;
		}
	}
}
