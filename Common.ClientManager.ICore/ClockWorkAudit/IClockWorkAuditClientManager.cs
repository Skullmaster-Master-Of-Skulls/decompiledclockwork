using System;
using TechnoPro.ClockWorkServer.Contracts.DTO.ClockWorkAudit;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities.ClockWorkAudit;

namespace TechnoPro.Common.ClientManager.ICore.ClockWorkAudit
{
	// Token: 0x02000071 RID: 113
	public interface IClockWorkAuditClientManager : IWebService
	{
		// Token: 0x06000355 RID: 853
		AuditResultDTO ExecuteAudit(eClockWorkAuditType AuditType);
	}
}
