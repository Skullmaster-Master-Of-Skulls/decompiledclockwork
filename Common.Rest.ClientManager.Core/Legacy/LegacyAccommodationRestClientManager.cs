using System;
using TechnoPro.ClockWorkServer.Contracts.DTO.Legacy.Accommodation;
using TechnoPro.Common.ClientManager.ICore;
using TechnoPro.Common.ClientManager.ICore.Legacy;
using TechnoPro.Common.Public;
using TechnoPro.Common.Unity.IoC;
using TechnoPro.Common.Web.Security.Proxy;

namespace TechnoPro.Common.Rest.ClientManager.Core.Legacy
{
	// Token: 0x0200003B RID: 59
	public class LegacyAccommodationRestClientManager : BearerTokenRestProxy<ILegacyAccommodationClientManager>, ILegacyAccommodationClientManager, IWebService
	{
		// Token: 0x06000226 RID: 550 RVA: 0x0000728F File Offset: 0x0000548F
		public LegacyAccommodationRestClientManager(string serviceAddress, string token = null) : base(serviceAddress, token)
		{
		}

		// Token: 0x06000227 RID: 551 RVA: 0x00007299 File Offset: 0x00005499
		public LegacyAccommodationRestClientManager(string serviceAddress, string serviceAddressSuffix, string token = null) : base(serviceAddress, serviceAddressSuffix, token)
		{
		}

		// Token: 0x06000228 RID: 552 RVA: 0x000072A4 File Offset: 0x000054A4
		public void LogLoaIssuedDate(int pid, int lucid, string loaString)
		{
			LogLoaIssuedDateReq logLoaIssuedDateReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LogLoaIssuedDateReq>();
			logLoaIssuedDateReq.Pid = pid;
			logLoaIssuedDateReq.Lucid = lucid;
			logLoaIssuedDateReq.LoaString = loaString;
			base.Post<LogLoaIssuedDateReq>(logLoaIssuedDateReq, "legacyaccommodation/logloaissueddate");
		}

		// Token: 0x06000229 RID: 553 RVA: 0x00002BEE File Offset: 0x00000DEE
		public void CreateOrAddAccommodationApprovalNote(int pid, string note)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600022A RID: 554 RVA: 0x00002BEE File Offset: 0x00000DEE
		public string GetAccommodationsApprovalSummary(int pid)
		{
			throw new NotImplementedException();
		}
	}
}
