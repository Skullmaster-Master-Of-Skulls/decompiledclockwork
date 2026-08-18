using System;
using TechnoPro.ClockWorkServer.Client.ServiceFactory;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.ClockWorkAudit;
using TechnoPro.Common.ClientManager.ICore;
using TechnoPro.Common.ClientManager.ICore.ClockWorkAudit;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities.ClockWorkAudit;
using TechnoPro.Common.Unity.IoC;

namespace TechnoPro.Common.ClientManager.Core.ClockWorkAudit
{
	// Token: 0x02000078 RID: 120
	public class ClockWorkAuditClientManager : IClockWorkAuditClientManager, IWebService
	{
		// Token: 0x0600046A RID: 1130 RVA: 0x00014674 File Offset: 0x00012874
		public AuditResultDTO ExecuteAudit(eClockWorkAuditType AuditType)
		{
			ExecuteAuditReq executeAuditReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<ExecuteAuditReq>();
			executeAuditReq.AuditType = AuditType;
			return ClientServiceFactory.GetClientInstance<IClockWorkAudit>().ExecuteAudit(executeAuditReq).Result;
		}
	}
}
