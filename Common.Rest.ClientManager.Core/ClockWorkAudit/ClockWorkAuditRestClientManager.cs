using System;
using TechnoPro.ClockWorkServer.Contracts.DTO.ClockWorkAudit;
using TechnoPro.Common.ClientManager.ICore;
using TechnoPro.Common.ClientManager.ICore.ClockWorkAudit;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities.ClockWorkAudit;
using TechnoPro.Common.Unity.IoC;
using TechnoPro.Common.Web.Security.Proxy;

namespace TechnoPro.Common.Rest.ClientManager.Core.ClockWorkAudit
{
	// Token: 0x02000063 RID: 99
	public class ClockWorkAuditRestClientManager : BearerTokenRestProxy<IClockWorkAuditClientManager>, IClockWorkAuditClientManager, IWebService
	{
		// Token: 0x060003CB RID: 971 RVA: 0x0000B7FA File Offset: 0x000099FA
		public ClockWorkAuditRestClientManager(string serviceAddress, string token = null) : base(serviceAddress, token)
		{
		}

		// Token: 0x060003CC RID: 972 RVA: 0x0000B804 File Offset: 0x00009A04
		public ClockWorkAuditRestClientManager(string serviceAddress, string serviceAddressSuffix, string token = null) : base(serviceAddress, serviceAddressSuffix, token)
		{
		}

		// Token: 0x060003CD RID: 973 RVA: 0x0000B810 File Offset: 0x00009A10
		public AuditResultDTO ExecuteAudit(eClockWorkAuditType AuditType)
		{
			ExecuteAuditReq executeAuditReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<ExecuteAuditReq>();
			executeAuditReq.AuditType = AuditType;
			return base.Post<ExecuteAuditReq, AuditResultDTO>(executeAuditReq, "cases/executeaudit");
		}
	}
}
