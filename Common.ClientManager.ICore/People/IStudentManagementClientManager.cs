using System;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;
using TechnoPro.Common.Public;

namespace TechnoPro.Common.ClientManager.ICore.People
{
	// Token: 0x0200002F RID: 47
	public interface IStudentManagementClientManager : IWebService
	{
		// Token: 0x0600013F RID: 319
		StudentSummaryDTO LoadStudentSummary(int PersonId);
	}
}
