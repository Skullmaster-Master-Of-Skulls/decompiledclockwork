using System;
using TechnoPro.ClockWorkServer.Contracts.DTO.Room;
using TechnoPro.Common.ClientManager.ICore.Room;
using TechnoPro.Common.Public;
using TechnoPro.Common.Web.Security.Proxy;

namespace TechnoPro.Common.Rest.ClientManager.Core.Room
{
	// Token: 0x0200001F RID: 31
	public class RoomRestClientManager : BearerTokenRestProxy<IRoomClientManager>, IRoomClientManager, IWebService
	{
		// Token: 0x060000FB RID: 251 RVA: 0x0000465E File Offset: 0x0000285E
		public RoomRestClientManager(string serviceAddress, string token = null) : base(serviceAddress, token)
		{
		}

		// Token: 0x060000FC RID: 252 RVA: 0x00004668 File Offset: 0x00002868
		public RoomRestClientManager(string serviceAddress, string serviceAddressSuffix, string token = null) : base(serviceAddress, serviceAddressSuffix, token)
		{
		}

		// Token: 0x060000FD RID: 253 RVA: 0x00004673 File Offset: 0x00002873
		public SeatCollectionDTO LoadAllSeats(bool ignoreCache, string ClockWorkSettingsInstanceName = null)
		{
			if (!string.IsNullOrEmpty(ClockWorkSettingsInstanceName))
			{
				return base.Get<SeatCollectionDTO>(string.Format("room/allseats?ignorecache={0}&settingsinstancename={1}", ignoreCache, ClockWorkSettingsInstanceName), true);
			}
			return base.Get<SeatCollectionDTO>(string.Format("room/allseats?ignorecache={0}", ignoreCache), true);
		}
	}
}
