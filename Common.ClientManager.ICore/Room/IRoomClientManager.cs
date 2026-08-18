using System;
using TechnoPro.ClockWorkServer.Contracts.DTO.Room;
using TechnoPro.Common.Public;

namespace TechnoPro.Common.ClientManager.ICore.Room
{
	// Token: 0x02000022 RID: 34
	public interface IRoomClientManager : IWebService
	{
		// Token: 0x060000CD RID: 205
		SeatCollectionDTO LoadAllSeats(bool ignoreCache, string ClockWorkSettingsInstanceName = null);
	}
}
