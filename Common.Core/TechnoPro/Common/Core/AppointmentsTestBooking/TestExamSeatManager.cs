using System;
using System.Collections.Generic;
using System.Linq;
using TechnoPro.Common.Core.Appointments;
using TechnoPro.Common.Core.UserSettingsPermissions;
using TechnoPro.Common.ICore.Appointments;
using TechnoPro.Common.ICore.AppointmentsTestBooking;
using TechnoPro.Common.ICore.UserSettingsPermissions;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Appointments;
using TechnoPro.Common.Public.Entities.AppointmentsTestBooking;
using TechnoPro.Common.Public.Entities.AppointmentsTestBooking.AutoTestBookingHelper;
using TechnoPro.Common.Public.Entities.UserSettingsPermissions.OldUserSettings;

namespace TechnoPro.Common.Core.AppointmentsTestBooking
{
	// Token: 0x02000143 RID: 323
	public class TestExamSeatManager : ITestExamSeatManager, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x06000E50 RID: 3664 RVA: 0x0006B686 File Offset: 0x00069886
		public TestExamSeatManager(OperationContext opContext)
		{
			this.OpContext = opContext;
		}

		// Token: 0x170001F2 RID: 498
		// (get) Token: 0x06000E51 RID: 3665 RVA: 0x0006B698 File Offset: 0x00069898
		// (set) Token: 0x06000E52 RID: 3666 RVA: 0x0006B6A0 File Offset: 0x000698A0
		public OperationContext OpContext { get; set; }

		// Token: 0x06000E53 RID: 3667 RVA: 0x0006B6AC File Offset: 0x000698AC
		public IList<AppointmentRoom> LoadAllowedSeats(eTestExamSeatType ClassTestType)
		{
			IAppointmentRoomManager appointmentRoomManager = new AppointmentRoomManager(this.OpContext);
			IList<AppointmentRoom> list = appointmentRoomManager.LoadAllowedRooms();
			IOldUserSettingManager oldUserSettingManager = new OldUserSettingManager(this.OpContext);
			List<int> list2 = oldUserSettingManager.GetSettingValue_ConcatenatedIntList(this.OpContext.WhoAmI, eSettingCode.SETTING_Tests_RoomGroupsToShow) ?? new List<int>();
			IList<AppointmentRoom> roomsToAdd = (list2.Count > 0) ? appointmentRoomManager.LoadRoomsInGrousp(list2.ToArray()) : null;
			bool flag = ClassTestType == eTestExamSeatType.AllRooms;
			IList<AppointmentRoom> result;
			if (flag)
			{
				result = this.AddRoomsToList(list, roomsToAdd, true);
			}
			else
			{
				eTestExamSettingType testType = (ClassTestType == eTestExamSeatType.Final) ? eTestExamSettingType.Final : eTestExamSettingType.Midterm;
				IAutoTestBookingManager autoTestBookingManager = new AutoTestBookingManager(this.OpContext);
				IList<Room> rms = autoTestBookingManager.LoadAvailableRooms(testType, null, null, null, null, false);
				List<AppointmentRoom> masterList = (from g in list
				where rms.FirstOrDefault((Room h) => h.RoomId == g.RoomId) != null
				select g).ToList<AppointmentRoom>();
				result = this.AddRoomsToList(masterList, roomsToAdd, false);
			}
			return result;
		}

		// Token: 0x06000E54 RID: 3668 RVA: 0x0006B78C File Offset: 0x0006998C
		private IList<AppointmentRoom> AddRoomsToList(IList<AppointmentRoom> masterList, IList<AppointmentRoom> roomsToAdd, bool returnCopyOfMasterList)
		{
			IList<AppointmentRoom> list;
			if (!returnCopyOfMasterList)
			{
				list = masterList;
			}
			else
			{
				IList<AppointmentRoom> list2 = new List<AppointmentRoom>(masterList);
				list = list2;
			}
			IList<AppointmentRoom> list3 = list;
			bool flag = roomsToAdd != null;
			if (flag)
			{
				using (IEnumerator<AppointmentRoom> enumerator = roomsToAdd.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						AppointmentRoom room = enumerator.Current;
						bool flag2 = list3.FirstOrDefault((AppointmentRoom g) => g.RoomId == room.RoomId) == null;
						if (flag2)
						{
							list3.Add(room);
						}
					}
				}
			}
			return list3;
		}

		// Token: 0x06000E55 RID: 3669 RVA: 0x0006B828 File Offset: 0x00069A28
		public AppointmentRoom LoadSeatById(int RoomId)
		{
			IAppointmentRoomManager appointmentRoomManager = new AppointmentRoomManager(this.OpContext);
			return appointmentRoomManager.LoadRoomById(RoomId);
		}

		// Token: 0x06000E56 RID: 3670 RVA: 0x0006B850 File Offset: 0x00069A50
		public IList<AppointmentRoomWithAvailability> LoadRoomsWithAvailability(eTestExamSeatType TestType, DateTime StartDateTime, DateTime EndDateTime, IList<int> RoomIdsToIgnore)
		{
			IAppointmentRoomManager appointmentRoomManager = new AppointmentRoomManager(this.OpContext);
			IList<AppointmentRoom> source = appointmentRoomManager.LoadAllowedRooms();
			IAutoTestBookingManager autoTestBookingManager = new AutoTestBookingManager(this.OpContext);
			List<int> availableRoomIds = new List<int>();
			bool flag = TestType == eTestExamSeatType.AllRooms;
			if (flag)
			{
				availableRoomIds = source.ToList<AppointmentRoom>().ConvertAll<int>((AppointmentRoom g) => g.RoomId);
			}
			else
			{
				IList<Room> source2 = autoTestBookingManager.LoadAvailableRooms((TestType == eTestExamSeatType.Final) ? eTestExamSettingType.Final : eTestExamSettingType.Midterm, null, null, null, null, false);
				availableRoomIds = source2.ToList<Room>().ConvertAll<int>((Room g) => g.RoomId);
			}
			IEnumerable<AppointmentRoom> source3 = from g in source
			where availableRoomIds.Contains(g.RoomId)
			select g;
			bool flag2 = RoomIdsToIgnore != null && RoomIdsToIgnore.Count > 0;
			if (flag2)
			{
				source3 = (from g in source3
				where !RoomIdsToIgnore.Contains(g.RoomId)
				select g).ToList<AppointmentRoom>();
			}
			IList<AppointmentRoomWithAvailability> list = appointmentRoomManager.LoadRoomsWithAvailability(source3.ToList<AppointmentRoom>().ConvertAll<int>((AppointmentRoom g) => g.RoomId), StartDateTime, EndDateTime);
			bool flag3 = RoomIdsToIgnore != null && RoomIdsToIgnore.Count > 0;
			IList<AppointmentRoomWithAvailability> result;
			if (flag3)
			{
				IEnumerable<AppointmentRoom> enumerable = from g in source
				where RoomIdsToIgnore.Contains(g.RoomId)
				select g;
				using (IEnumerator<AppointmentRoom> enumerator = enumerable.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						AppointmentRoom r = enumerator.Current;
						bool flag4 = list.FirstOrDefault((AppointmentRoomWithAvailability g) => g.RoomId == r.RoomId) == null;
						if (flag4)
						{
							list.Add(new AppointmentRoomWithAvailability(r)
							{
								IsAvailable = true
							});
						}
					}
				}
				List<AppointmentRoomWithAvailability> list2 = list.ToList<AppointmentRoomWithAvailability>();
				list2.Sort((AppointmentRoomWithAvailability g1, AppointmentRoomWithAvailability g2) => (g1.RoomTitle ?? "").CompareTo(g2.RoomTitle ?? ""));
				result = list2;
			}
			else
			{
				result = list;
			}
			return result;
		}
	}
}
