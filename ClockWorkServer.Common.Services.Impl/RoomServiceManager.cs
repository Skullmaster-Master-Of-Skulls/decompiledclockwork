using System;
using TechnoPro.ClockWorkServer.Common.Services.Impl.Adapters;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.Room;
using TechnoPro.Common.Core.Mappers.Room;
using TechnoPro.Common.Core.Room;
using TechnoPro.Common.ICore.Room;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities.Room;

namespace TechnoPro.ClockWorkServer.Common.Services.Impl
{
	// Token: 0x0200007E RID: 126
	public class RoomServiceManager : IRoom, IService
	{
		// Token: 0x060004BA RID: 1210 RVA: 0x0001683C File Offset: 0x00014A3C
		public LoadAllSeatsResp LoadAllSeats(LoadAllSeatsReq Request)
		{
			IRoomManager roomManager = new RoomManager(Request.GetOperationContext());
			SeatCollection seatCollection = roomManager.LoadAllSeats(Request.IgnoreCache, Request.ClockWorkSettingsInstanceName);
			return new LoadAllSeatsResp
			{
				SeatCollection = ((seatCollection == null) ? null : seatCollection.ToDTO())
			};
		}
	}
}
