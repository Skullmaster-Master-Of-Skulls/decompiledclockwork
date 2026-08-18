using System;
using System.Collections.Generic;
using System.Linq;
using TechnoPro.Common.Core.AppointmentsTestBooking;
using TechnoPro.Common.Core.Settings;
using TechnoPro.Common.DAO.Impl.Room;
using TechnoPro.Common.DAO.Room;
using TechnoPro.Common.ICore.AppointmentsTestBooking;
using TechnoPro.Common.ICore.Room;
using TechnoPro.Common.ICore.Settings;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.AppointmentsTestBooking;
using TechnoPro.Common.Public.Entities.AppointmentsTestBooking.AutoTestBookingHelper;
using TechnoPro.Common.Public.Entities.Room;
using TechnoPro.Common.Unity.IoC;

namespace TechnoPro.Common.Core.Room
{
	// Token: 0x02000058 RID: 88
	public class RoomManager : IRoomManager, IBaseOperationContext<OperationContext>
	{
		// Token: 0x06000399 RID: 921 RVA: 0x000121C9 File Offset: 0x000103C9
		public RoomManager(OperationContext opContext)
		{
			this.OpContext = opContext;
		}

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x0600039A RID: 922 RVA: 0x000121DB File Offset: 0x000103DB
		// (set) Token: 0x0600039B RID: 923 RVA: 0x000121E3 File Offset: 0x000103E3
		public OperationContext OpContext { get; set; }

		// Token: 0x0600039C RID: 924 RVA: 0x000121EC File Offset: 0x000103EC
		public SeatCollection LoadAllSeats(bool ignoreCache = false, string ClockWorkSettingsInstanceName = null)
		{
			ICacheStorageManager cacheStorageManager = ObjectFactory.Resolve<ICacheStorageManager>();
			IList<Seat> list = ignoreCache ? null : ((IList<Seat>)cacheStorageManager["uAllSeats"]);
			bool flag = list == null;
			if (flag)
			{
				IRoomDAO roomDAO = new RoomDAO(this.OpContext);
				list = roomDAO.LoadAllSeats();
				bool flag2 = !ignoreCache;
				if (flag2)
				{
					cacheStorageManager.Insert("uAllSeats", list, TimeSpan.FromMinutes(30.0));
				}
			}
			ISettingManager sm = new SettingManager(ClockWorkSettingsInstanceName, this.OpContext);
			IAutoTestBookingManager autoTestBookingManager = new AutoTestBookingManager(this.OpContext);
			IList<Asset> list2 = autoTestBookingManager.LoadAvailableAssets(eTestExamSettingType.Final, sm, cacheStorageManager, ClockWorkSettingsInstanceName, !ignoreCache);
			IList<Room> source = autoTestBookingManager.LoadAvailableRooms(eTestExamSettingType.Final, sm, cacheStorageManager, list2, ClockWorkSettingsInstanceName, !ignoreCache);
			IList<Asset> list3 = autoTestBookingManager.LoadAvailableAssets(eTestExamSettingType.Final, sm, cacheStorageManager, ClockWorkSettingsInstanceName, !ignoreCache);
			IList<Room> source2 = autoTestBookingManager.LoadAvailableRooms(eTestExamSettingType.Final, sm, cacheStorageManager, list3, ClockWorkSettingsInstanceName, !ignoreCache);
			IEnumerable<Asset> source3 = list2.Concat(list3);
			SeatCollection seatCollection = new SeatCollection();
			seatCollection.AllAssets = source3.Select(delegate(Asset g)
			{
				SeatAsset seatAsset = new SeatAsset();
				seatAsset.SeatAssetId = g.AssetId;
				seatAsset.AccommodationsBehind = (from h in g.AccommodationsSupported
				select new SeatAssetAccommodation
				{
					ControlId = h.ControlId,
					Title = h.Title,
					LookupText = h.LookupText,
					SubText = h.SubText,
					Level = h.Level
				}).ToList<SeatAssetAccommodation>();
				seatAsset.IsActive = g.IsActive;
				seatAsset.Score = g.Score;
				seatAsset.Title = g.Title;
				return seatAsset;
			}).ToList<SeatAsset>();
			seatCollection.AllSeatGroups = new List<SeatGroup>();
			SeatCollection seatCollection2 = seatCollection;
			List<Seat> list4 = new List<Seat>();
			using (IEnumerator<Seat> enumerator = list.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					Seat allSeat = enumerator.Current;
					Room room = source2.FirstOrDefault((Room g) => g.RoomId == allSeat.RoomId);
					allSeat.SeatType = eTestExamSeatType.AllRooms;
					List<Seat> list5 = new List<Seat>();
					bool flag3 = room != null;
					if (flag3)
					{
						allSeat.SeatType = eTestExamSeatType.Midterm;
						allSeat.OrderNum = room.PriorityNumber;
						allSeat.Campus = this.ExtractCampuses(allSeat, list5, room.Campuses);
					}
					else
					{
						room = source.FirstOrDefault((Room g) => g.RoomId == allSeat.RoomId);
						bool flag4 = room != null;
						if (flag4)
						{
							allSeat.SeatType = eTestExamSeatType.Final;
							allSeat.OrderNum = room.PriorityNumber;
							allSeat.Campus = this.ExtractCampuses(allSeat, list5, room.Campuses);
						}
					}
					list4.Add(allSeat);
					foreach (Seat item in list5)
					{
						seatCollection2.Seats.Add(item);
					}
				}
			}
			list4.Sort(delegate(Seat g1, Seat g2)
			{
				int num = g1.SeatType.CompareTo(g2.SeatType);
				bool flag5 = num != 0;
				int result;
				if (flag5)
				{
					result = num;
				}
				else
				{
					num = g1.Campus.CompareTo(g2.Campus);
					bool flag6 = num != 0;
					if (flag6)
					{
						result = num;
					}
					else
					{
						num = g1.ParentSeatGroupId.CompareTo(g2.ParentSeatGroupId);
						bool flag7 = num != 0;
						if (flag7)
						{
							result = num;
						}
						else
						{
							num = g1.OrderNum.CompareTo(g2.OrderNum);
							bool flag8 = num != 0;
							if (flag8)
							{
								result = num;
							}
							else
							{
								result = (g1.RoomTitle ?? "").CompareTo(g2.RoomTitle ?? "");
							}
						}
					}
				}
				return result;
			});
			seatCollection2.Seats = list4;
			return seatCollection2;
		}

		// Token: 0x0600039D RID: 925 RVA: 0x000124F4 File Offset: 0x000106F4
		private string ExtractCampuses(Seat seat, IList<Seat> additionalCampusSeats, IList<string> campuses)
		{
			bool flag = campuses == null || campuses.Count < 1;
			string result;
			if (flag)
			{
				result = string.Empty;
			}
			else
			{
				for (int i = 1; i < campuses.Count; i++)
				{
					Seat seat2 = seat.Clone();
					seat2.Campus = campuses[i];
					additionalCampusSeats.Add(seat2);
				}
				result = campuses[0];
			}
			return result;
		}
	}
}
