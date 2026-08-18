using System;
using TechnoPro.ClockWorkServer.Client.ServiceFactory;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.Room;
using TechnoPro.Common.ClientManager.ICore;
using TechnoPro.Common.ClientManager.ICore.Room;
using TechnoPro.Common.Public;
using TechnoPro.Common.Unity.IoC;

namespace TechnoPro.Common.ClientManager.Core.Room
{
	// Token: 0x02000025 RID: 37
	public class RoomClientManager : IRoomClientManager, IWebService
	{
		// Token: 0x06000118 RID: 280 RVA: 0x000064A4 File Offset: 0x000046A4
		public SeatCollectionDTO LoadAllSeats(bool ignoreCache, string ClockWorkSettingsInstanceName = null)
		{
			LoadAllSeatsReq loadAllSeatsReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadAllSeatsReq>();
			loadAllSeatsReq.IgnoreCache = ignoreCache;
			loadAllSeatsReq.ClockWorkSettingsInstanceName = ClockWorkSettingsInstanceName;
			return ClientServiceFactory.GetClientInstance<IRoom>().LoadAllSeats(loadAllSeatsReq).SeatCollection;
		}
	}
}
