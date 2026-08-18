using System;
using TechnoPro.ClockWorkServer.Client.ServiceFactory;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;
using TechnoPro.ClockWorkServer.Contracts.DTO.People.PeopleParameters;
using TechnoPro.Common.ClientManager.ICore;
using TechnoPro.Common.ClientManager.ICore.People;
using TechnoPro.Common.Public;
using TechnoPro.Common.Unity.IoC;

namespace TechnoPro.Common.ClientManager.Core.People
{
	// Token: 0x02000033 RID: 51
	public class StudentManagementClientManager : IStudentManagementClientManager, IWebService
	{
		// Token: 0x060001CE RID: 462 RVA: 0x00008E88 File Offset: 0x00007088
		public StudentSummaryDTO LoadStudentSummary(int PersonId)
		{
			LoadStudentSummaryReq loadStudentSummaryReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadStudentSummaryReq>();
			loadStudentSummaryReq.PersonId = PersonId;
			return ClientServiceFactory.GetClientInstance<IStudentManagement>().LoadStudentSummary(loadStudentSummaryReq).StudentSummary;
		}
	}
}
