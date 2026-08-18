using System;
using TechnoPro.ClockWorkServer.Common.Services.Impl.Adapters;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.Startup;
using TechnoPro.Common.Core.Mappers.Startup;
using TechnoPro.Common.Core.Startup;
using TechnoPro.Common.ICore.Startup;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities.Startup;

namespace TechnoPro.ClockWorkServer.Common.Services.Impl
{
	// Token: 0x0200008B RID: 139
	public class ClockWorkClientStartupServiceManager : IClockWorkClientStartup, IService
	{
		// Token: 0x0600050D RID: 1293 RVA: 0x00017A98 File Offset: 0x00015C98
		public GetClockWorkClientStartupResp GetClockWorkClientStartup(GetClockWorkClientStartupReq Request)
		{
			IClockWorkClientStartupManager clockWorkClientStartupManager = new ClockWorkClientStartupManager(Request.GetOperationContext());
			ClockWorkClientStartup clockWorkClientStartup = clockWorkClientStartupManager.GetClockWorkClientStartup(Request.PersonId);
			return new GetClockWorkClientStartupResp
			{
				StartupValues = ((clockWorkClientStartup == null) ? null : clockWorkClientStartup.ToDTO())
			};
		}
	}
}
