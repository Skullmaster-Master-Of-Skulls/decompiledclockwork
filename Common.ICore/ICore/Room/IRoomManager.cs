using System;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Room;

namespace TechnoPro.Common.ICore.Room
{
	// Token: 0x0200004B RID: 75
	public interface IRoomManager : IBaseOperationContext<OperationContext>
	{
		// Token: 0x060001E3 RID: 483
		SeatCollection LoadAllSeats(bool ignoreCache = false, string ClockWorkSettingsInstanceName = null);
	}
}
