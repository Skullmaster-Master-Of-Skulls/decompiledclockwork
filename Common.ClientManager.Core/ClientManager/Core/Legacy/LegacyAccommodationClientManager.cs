using System;
using TechnoPro.ClockWorkServer.Client.ServiceFactory;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.Legacy.Accommodation;
using TechnoPro.Common.ClientManager.ICore;
using TechnoPro.Common.ClientManager.ICore.Legacy;
using TechnoPro.Common.Public;
using TechnoPro.Common.Unity.IoC;

namespace TechnoPro.Common.ClientManager.Core.Legacy
{
	// Token: 0x02000047 RID: 71
	public class LegacyAccommodationClientManager : ILegacyAccommodationClientManager, IWebService
	{
		// Token: 0x06000293 RID: 659 RVA: 0x0000BF60 File Offset: 0x0000A160
		public void LogLoaIssuedDate(int pid, int lucid, string loaString)
		{
			LogLoaIssuedDateReq logLoaIssuedDateReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LogLoaIssuedDateReq>();
			logLoaIssuedDateReq.Pid = pid;
			logLoaIssuedDateReq.Lucid = lucid;
			logLoaIssuedDateReq.LoaString = loaString;
			ClientServiceFactory.GetClientInstance<ILegacyAccommodation>().LogLoaIssuedDate(logLoaIssuedDateReq);
		}

		// Token: 0x06000294 RID: 660 RVA: 0x0000387F File Offset: 0x00001A7F
		public void CreateOrAddAccommodationApprovalNote(int pid, string note)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000295 RID: 661 RVA: 0x0000387F File Offset: 0x00001A7F
		public string GetAccommodationsApprovalSummary(int pid)
		{
			throw new NotImplementedException();
		}
	}
}
