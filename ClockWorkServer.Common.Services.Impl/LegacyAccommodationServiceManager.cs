using System;
using TechnoPro.ClockWorkServer.Common.Services.Impl.Adapters;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.Legacy.Accommodation;
using TechnoPro.Common.Core.Legacy;
using TechnoPro.Common.ICore.Legacy;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Common.Services.Impl
{
	// Token: 0x02000058 RID: 88
	public class LegacyAccommodationServiceManager : ILegacyAccommodation, IService
	{
		// Token: 0x06000356 RID: 854 RVA: 0x0000FC24 File Offset: 0x0000DE24
		public LogLoaIssuedDateResp LogLoaIssuedDate(LogLoaIssuedDateReq Request)
		{
			ILegacyAccommodationManager legacyAccommodationManager = new LegacyAccommodationManager(Request.GetOperationContext());
			legacyAccommodationManager.LogLoaIssuedDate(Request.Pid, Request.Lucid, Request.LoaString);
			return new LogLoaIssuedDateResp();
		}
	}
}
