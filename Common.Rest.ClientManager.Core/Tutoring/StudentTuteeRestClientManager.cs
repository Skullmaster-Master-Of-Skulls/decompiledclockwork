using System;
using System.Collections.Generic;
using System.Linq;
using TechnoPro.ClockWorkServer.Contracts.DTO.Tutoring;
using TechnoPro.Common.ClientManager.ICore;
using TechnoPro.Common.ClientManager.ICore.Tutoring;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities.Tutoring;
using TechnoPro.Common.Unity.IoC;
using TechnoPro.Common.Web.Security.Proxy;

namespace TechnoPro.Common.Rest.ClientManager.Core.Tutoring
{
	// Token: 0x02000009 RID: 9
	public class StudentTuteeRestClientManager : BearerTokenRestProxy<IStudentTuteeClientManager>, IStudentTuteeClientManager, IWebService
	{
		// Token: 0x06000037 RID: 55 RVA: 0x00002BF5 File Offset: 0x00000DF5
		public StudentTuteeRestClientManager(string serviceAddress, string token = null) : base(serviceAddress, token)
		{
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00002BFF File Offset: 0x00000DFF
		public StudentTuteeRestClientManager(string serviceAddress, string serviceAddressSuffix, string token = null) : base(serviceAddress, serviceAddressSuffix, token)
		{
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00002C0A File Offset: 0x00000E0A
		public eTuteeStatus GetTuteeStatus(int StudentPersonId)
		{
			return base.Get<eTuteeStatus>(string.Format("studenttutee/status/studentpersonid/{0}", StudentPersonId), true);
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00002C23 File Offset: 0x00000E23
		public void RecordConfidentialityAgreementSignedByStudent(int StudentPersonId)
		{
			base.Post<int>(StudentPersonId, "studenttutee/recordconfidentialityagreementsigned");
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00002C31 File Offset: 0x00000E31
		public IList<MyTutorDTO> GetStudentMyTutors(int StudentPersonId, DateTime? StartDateTime, DateTime? EndDate)
		{
			return base.GetMany<MyTutorDTO>(string.Format("studenttutee/mytutors/studentpersonid/{0}/range/{1}/{2}", StudentPersonId, StartDateTime, EndDate), true);
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00002C58 File Offset: 0x00000E58
		public void MarkStudentCantFindTutor(int PersonId, int searchLucid, string searchLuc, string searchString)
		{
			MarkStudentCantFindTutorReq markStudentCantFindTutorReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<MarkStudentCantFindTutorReq>();
			markStudentCantFindTutorReq.StudentPersonId = PersonId;
			markStudentCantFindTutorReq.SearchLucid = searchLucid;
			markStudentCantFindTutorReq.SearchLuc = searchLuc;
			markStudentCantFindTutorReq.SearchString = searchString;
			base.Post<MarkStudentCantFindTutorReq>(markStudentCantFindTutorReq, "studenttutee/marstudentcantfindtutor");
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00002C9C File Offset: 0x00000E9C
		public void MarkStudentCantFindAvailability(int PersonId, params int[] TutorPids)
		{
			MarkStudentCantFindAvailabilityReq markStudentCantFindAvailabilityReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<MarkStudentCantFindAvailabilityReq>();
			markStudentCantFindAvailabilityReq.StudentPersonId = PersonId;
			markStudentCantFindAvailabilityReq.TutorPids = ((TutorPids != null) ? TutorPids.ToList<int>() : null);
			base.Post<MarkStudentCantFindAvailabilityReq>(markStudentCantFindAvailabilityReq, "studenttutee/marstudentcantfindtutor");
		}
	}
}
