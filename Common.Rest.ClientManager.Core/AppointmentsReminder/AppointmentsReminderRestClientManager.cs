using System;
using TechnoPro.Common.ClientManager.ICore.AppointmentsReminder;
using TechnoPro.Common.Public;
using TechnoPro.Common.Web.Security.Proxy;

namespace TechnoPro.Common.Rest.ClientManager.Core.AppointmentsReminder
{
	// Token: 0x0200007F RID: 127
	public class AppointmentsReminderRestClientManager : BearerTokenRestProxy<IAppointmentsReminderClientManager>, IAppointmentsReminderClientManager, IWebService
	{
		// Token: 0x060004EB RID: 1259 RVA: 0x0000DCBF File Offset: 0x0000BEBF
		public AppointmentsReminderRestClientManager(string serviceAddress, string token = null) : base(serviceAddress, token)
		{
		}

		// Token: 0x060004EC RID: 1260 RVA: 0x0000DCC9 File Offset: 0x0000BEC9
		public AppointmentsReminderRestClientManager(string serviceAddress, string serviceAddressSuffix, string token = null) : base(serviceAddress, serviceAddressSuffix, token)
		{
		}

		// Token: 0x060004ED RID: 1261 RVA: 0x0000DCD4 File Offset: 0x0000BED4
		public void AddMeToExclusionList()
		{
			base.Post("appointmentsreminder/addmetoexclusionlist");
		}

		// Token: 0x060004EE RID: 1262 RVA: 0x0000DCE1 File Offset: 0x0000BEE1
		public void RemoveMeFromExclusionList()
		{
			base.Post("appointmentsreminder/removemetoexclusionlist");
		}

		// Token: 0x060004EF RID: 1263 RVA: 0x0000DCEE File Offset: 0x0000BEEE
		public bool IsAppointmentsReminderEnable()
		{
			return base.Get<bool>("appointmentsreminder/isenable", true);
		}
	}
}
