using System;
using System.Collections.Generic;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;
using TechnoPro.Common.ClientManager.ICore.People;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Adapters;
using TechnoPro.Common.Web.Security.Proxy;

namespace TechnoPro.Common.Rest.ClientManager.Core.People
{
	// Token: 0x0200002A RID: 42
	public class StudentCommonInfoRestClientManager : BearerTokenRestProxy<IStudentCommonInfoClientManager>, IStudentCommonInfoClientManager, IWebService
	{
		// Token: 0x0600017F RID: 383 RVA: 0x00005AA5 File Offset: 0x00003CA5
		public StudentCommonInfoRestClientManager(string serviceAddress, string token = null) : base(serviceAddress, token)
		{
		}

		// Token: 0x06000180 RID: 384 RVA: 0x00005AAF File Offset: 0x00003CAF
		public StudentCommonInfoRestClientManager(string serviceAddress, string serviceAddressSuffix, string token = null) : base(serviceAddress, serviceAddressSuffix, token)
		{
		}

		// Token: 0x06000181 RID: 385 RVA: 0x00005ABA File Offset: 0x00003CBA
		public StudentCommonInfoDTO LoadStudentCommonInfo(int PersonId)
		{
			return base.Get<StudentCommonInfoDTO>(string.Format("studentcommoninfo/personid/{0}", PersonId), true);
		}

		// Token: 0x06000182 RID: 386 RVA: 0x00005AD3 File Offset: 0x00003CD3
		public PersonBaseDTO LoadStudentByEmailAddress(string EmailAddress)
		{
			return base.Get<PersonBaseDTO>(string.Format("studentcommoninfo/emailaddress/{0}", EmailAddress), true);
		}

		// Token: 0x06000183 RID: 387 RVA: 0x00005AE8 File Offset: 0x00003CE8
		public IList<StudentWithCommonInfoDTO> LoadMyStudents(int CounsellorPersonId, DateTime StartDate, DateTime EndDate, bool ShowStudentsIHaveAppsWith, bool ShowStudentsIAmAdvisorFor, bool IncludeCancelledAppointments = false, bool IncludeNoShowAppointments = true, int OverrideAssignedAdvisorControlId = 0)
		{
			return base.GetMany<StudentWithCommonInfoDTO>(string.Format("studentcommoninfo/mystudents/counsellorpersonid/{0}/range/{1}/{2}?showstudentsihaveappswith={3}&showstudentsiamadvisorfor={4}&cancelledapps={5}&noshowapps={6}&overrideassignedcounsellorcontrolid={7}", new object[]
			{
				CounsellorPersonId,
				StartDate,
				EndDate,
				ShowStudentsIHaveAppsWith,
				ShowStudentsIAmAdvisorFor,
				IncludeCancelledAppointments,
				IncludeNoShowAppointments,
				OverrideAssignedAdvisorControlId
			}), true);
		}

		// Token: 0x06000184 RID: 388 RVA: 0x00005B59 File Offset: 0x00003D59
		public IList<StudentWithCommonInfoDTO> LoadStudentsWithCommonInfo(IList<int> PersonIds)
		{
			return base.GetMany<StudentWithCommonInfoDTO>(string.Format("studentcommoninfo/personids/{0}", PersonIds.CommaSeparatedValuesWithoutSpace<int>()), true);
		}
	}
}
