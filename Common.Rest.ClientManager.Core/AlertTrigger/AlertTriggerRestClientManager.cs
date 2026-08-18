using System;
using TechnoPro.ClockWorkServer.Contracts.DTO.AlertTrigger;
using TechnoPro.Common.ClientManager.ICore.AlertTrigger;
using TechnoPro.Common.Public;
using TechnoPro.Common.Web.Security.Proxy;

namespace TechnoPro.Common.Rest.ClientManager.Core.AlertTrigger
{
	// Token: 0x0200008E RID: 142
	public class AlertTriggerRestClientManager : BearerTokenRestProxy<IAlertTriggerClientManager>, IAlertTriggerClientManager, IWebService
	{
		// Token: 0x060005E5 RID: 1509 RVA: 0x0001069E File Offset: 0x0000E89E
		public AlertTriggerRestClientManager(string serviceAddress, string token = null) : base(serviceAddress, token)
		{
		}

		// Token: 0x060005E6 RID: 1510 RVA: 0x000106A8 File Offset: 0x0000E8A8
		public AlertTriggerRestClientManager(string serviceAddress, string serviceAddressSuffix, string token = null) : base(serviceAddress, serviceAddressSuffix, token)
		{
		}

		// Token: 0x060005E7 RID: 1511 RVA: 0x000106B3 File Offset: 0x0000E8B3
		public AlertTriggerForUserSetDTO CheckForTriggerAlerts(int StudentPersonId)
		{
			return base.Get<AlertTriggerForUserSetDTO>(string.Format("alerttrigger/checkfor/{0}", StudentPersonId), true);
		}

		// Token: 0x060005E8 RID: 1512 RVA: 0x000106CC File Offset: 0x0000E8CC
		public bool AllowedToBookAppointmentForStudent(int StudentPersonId)
		{
			return base.Get<bool>(string.Format("alerttrigger/allowtobookappt/{0}", StudentPersonId), true);
		}
	}
}
