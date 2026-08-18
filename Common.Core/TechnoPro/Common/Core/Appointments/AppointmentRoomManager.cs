using System;
using System.Collections.Generic;
using System.Linq;
using TechnoPro.Common.Core.People;
using TechnoPro.Common.DAO.Appointments;
using TechnoPro.Common.DAO.Impl.Appointments;
using TechnoPro.Common.ICore;
using TechnoPro.Common.ICore.Appointments;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Appointments;
using TechnoPro.Common.Public.Entities.Caching;

namespace TechnoPro.Common.Core.Appointments
{
	// Token: 0x0200012E RID: 302
	public class AppointmentRoomManager : IAppointmentRoomManager, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x06000CC2 RID: 3266 RVA: 0x00059218 File Offset: 0x00057418
		public AppointmentRoomManager(OperationContext opContext)
		{
			this.OpContext = opContext;
			this.dao = new AppointmentRoomDAO(this.OpContext);
		}

		// Token: 0x170001CD RID: 461
		// (get) Token: 0x06000CC3 RID: 3267 RVA: 0x0005923B File Offset: 0x0005743B
		// (set) Token: 0x06000CC4 RID: 3268 RVA: 0x00059243 File Offset: 0x00057443
		public OperationContext OpContext { get; set; }

		// Token: 0x06000CC5 RID: 3269 RVA: 0x0005924C File Offset: 0x0005744C
		public IList<AppointmentRoom> LoadAllRooms()
		{
			return this.dao.LoadAllRooms();
		}

		// Token: 0x06000CC6 RID: 3270 RVA: 0x0005926C File Offset: 0x0005746C
		public IList<AppointmentRoom> LoadAllowedRooms()
		{
			IList<int> allowedPids = this.LoadAllowedRoomPids();
			IList<AppointmentRoom> source = this.LoadAllRooms();
			return (from g in source
			where allowedPids.Contains(g.RoomId)
			select g).ToList<AppointmentRoom>();
		}

		// Token: 0x06000CC7 RID: 3271 RVA: 0x000592B0 File Offset: 0x000574B0
		public IList<int> LoadAllowedRoomPids()
		{
			int whoAmI = this.OpContext.WhoAmI;
			eServerCacheItemType eServerCacheItemType = eServerCacheItemType.uAllowedRoomPids;
			OperationContext opContext = this.OpContext;
			IUserDatabaseCacheStorageManager userDatabaseCacheStorageManager = new UserDatabaseCacheStorageManager((opContext != null) ? opContext.TenantId : null);
			object obj = userDatabaseCacheStorageManager[whoAmI, eServerCacheItemType];
			bool flag = obj == null;
			List<int> list;
			if (flag)
			{
				list = PeopleManager.LoadAllowedRoomPids(this.OpContext, null, null);
				userDatabaseCacheStorageManager.Insert(whoAmI, eServerCacheItemType, list);
			}
			else
			{
				list = (List<int>)obj;
			}
			return list;
		}

		// Token: 0x06000CC8 RID: 3272 RVA: 0x00059330 File Offset: 0x00057530
		public AppointmentRoom LoadRoomById(int RoomId)
		{
			return this.dao.LoadRoomById(RoomId);
		}

		// Token: 0x06000CC9 RID: 3273 RVA: 0x00059350 File Offset: 0x00057550
		public IList<AppointmentRoomWithAvailability> LoadRoomsWithAvailability(IList<int> RoomIds, DateTime StartDateTime, DateTime EndDateTime)
		{
			return this.dao.LoadRoomsWithAvailability(RoomIds, StartDateTime, EndDateTime);
		}

		// Token: 0x06000CCA RID: 3274 RVA: 0x00059370 File Offset: 0x00057570
		public IList<AppointmentRoom> LoadRoomsInGrousp(params int[] GroupIds)
		{
			return this.dao.LoadRoomsInGrousp(GroupIds);
		}

		// Token: 0x04000269 RID: 617
		private IAppointmentRoomDAO dao;
	}
}
