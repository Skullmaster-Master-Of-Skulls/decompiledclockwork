using System;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;
using TechnoPro.Common.ClientManager.ICore.People;
using TechnoPro.Common.Public;
using TechnoPro.Common.Web.Security.Proxy;

namespace TechnoPro.Common.Rest.ClientManager.Core.People
{
	// Token: 0x0200002B RID: 43
	public class StudentManagementRestClientManager : BearerTokenRestProxy<IStudentManagementClientManager>, IStudentManagementClientManager, IWebService
	{
		// Token: 0x06000185 RID: 389 RVA: 0x00005B72 File Offset: 0x00003D72
		public StudentManagementRestClientManager(string serviceAddress, string token = null) : base(serviceAddress, token)
		{
		}

		// Token: 0x06000186 RID: 390 RVA: 0x00005B7C File Offset: 0x00003D7C
		public StudentManagementRestClientManager(string serviceAddress, string serviceAddressSuffix, string token = null) : base(serviceAddress, serviceAddressSuffix, token)
		{
		}

		// Token: 0x06000187 RID: 391 RVA: 0x00005B87 File Offset: 0x00003D87
		public StudentSummaryDTO LoadStudentSummary(int PersonId)
		{
			return base.Get<StudentSummaryDTO>(string.Format("studentmanagement/studentsummary/personid/{0}", PersonId), true);
		}
	}
}
