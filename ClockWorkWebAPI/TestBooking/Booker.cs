using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.UI;
using ClockWorkLogger;
using ClockWorkWebAPI.ClockWorkAPIReplacement;
using ClockWorkWebAPI.Settings;
using Databases;
using EncryptionClassLibrary;
using TechnoPro.Common.ClientManager.Core.Settings;
using TechnoPro.Common.ClientManager.ICore.Settings;
using TechnoPro.Common.Public.Entities.Settings;

namespace ClockWorkWebAPI.TestBooking
{
	// Token: 0x0200003B RID: 59
	public class Booker
	{
		// Token: 0x06000306 RID: 774 RVA: 0x00012CBC File Offset: 0x00010EBC
		public static CustomTestBookingRulesClass GetCustomRules(string binPath, string code_FindPotentialBookingsStart, string code_FindPotentialBookingsEnd, string code_FindPotentialBookingsMid)
		{
			CustomTestBookingRulesClass customTestBookingRulesClass;
			try
			{
				customTestBookingRulesClass = CustomTestBookingRulesClass.GetCustomRules();
				bool flag = customTestBookingRulesClass != null && customTestBookingRulesClass.NeedsRecompile(code_FindPotentialBookingsStart, code_FindPotentialBookingsEnd, code_FindPotentialBookingsMid);
				if (flag)
				{
					customTestBookingRulesClass = null;
				}
				bool flag2 = customTestBookingRulesClass == null && (!string.IsNullOrEmpty(code_FindPotentialBookingsStart) || !string.IsNullOrEmpty(code_FindPotentialBookingsMid) || !string.IsNullOrEmpty(code_FindPotentialBookingsEnd));
				if (flag2)
				{
					customTestBookingRulesClass = CustomTestBookingRulesClass.GetCustomRules(binPath, code_FindPotentialBookingsStart, code_FindPotentialBookingsEnd, code_FindPotentialBookingsMid);
				}
				return customTestBookingRulesClass;
			}
			catch
			{
				customTestBookingRulesClass = null;
			}
			return customTestBookingRulesClass;
		}

		// Token: 0x06000307 RID: 775 RVA: 0x00012D38 File Offset: 0x00010F38
		public static List<PotentialTest> FindPotentialTestBookings(string binPath, int pid, int lucid, Test classTest, List<Accommodation> accommodations, out List<int> iconIdsToBookWith, out string emailBody, out List<PrivateNote> privateNotes, FindPotentialBookingsInfo findPotentialBookingsInfo)
		{
			IWebSettingsClientManager webSettingsClientManager = new WebSettingsClientManager();
			string settingValue = webSettingsClientManager.GetSettingValue<string>(Setting.TESTBOOKING_Assets);
			List<Asset> availableAssets = Asset.LoadAssets(settingValue);
			string settingValue2 = webSettingsClientManager.GetSettingValue<string>(Setting.TESTBOOKING_Rooms);
			List<Room> availableRooms = Room.LoadRooms(settingValue2, availableAssets);
			string settingValue3 = webSettingsClientManager.GetSettingValue<string>(Setting.TESTBOOKING_SpecialAccommodations);
			List<SpecialAccommodation> specialAccommodations = SpecialAccommodation.LoadSpecialAccommodations(settingValue3);
			string settingValue4 = webSettingsClientManager.GetSettingValue<string>(Setting.TESTBOOKING_Rules);
			List<Rule> rules = Rule.FromXml(settingValue4);
			int settingValue5 = webSettingsClientManager.GetSettingValue<int>(Setting.TESTBOOKING_OverrideRoomPidForAvailability);
			string settingValue6 = webSettingsClientManager.GetSettingValue<string>(Setting.TESTBOOKING_code_FindPotentialBookingsStart);
			string settingValue7 = webSettingsClientManager.GetSettingValue<string>(Setting.TESTBOOKING_code_FindPotentialBookingsEnd);
			string settingValue8 = webSettingsClientManager.GetSettingValue<string>(Setting.TESTBOOKING_code_FindPotentialBookingsMid);
			findPotentialBookingsInfo.BufferMinutesPre = webSettingsClientManager.GetSettingValue<int>(Setting.TESTBOOKING_BufferMinutesPre);
			findPotentialBookingsInfo.BufferMinutesPost = webSettingsClientManager.GetSettingValue<int>(Setting.TESTBOOKING_BufferMinutesPost);
			return Booker.FindPotentialTestBookings(binPath, false, null, pid, lucid, classTest.StartDate.Date, classTest, accommodations, availableAssets, availableRooms, specialAccommodations, out emailBody, out iconIdsToBookWith, rules, settingValue5, settingValue6, settingValue7, settingValue8, out privateNotes, findPotentialBookingsInfo);
		}

		// Token: 0x06000308 RID: 776 RVA: 0x00012E34 File Offset: 0x00011034
		public static List<PotentialTest> FindPotentialTestBookings(string binPath, bool debugMode, Page page, int pid, int lucid, DateTime dayToLookIn, Test classTest, List<Accommodation> accommodations, List<Asset> availableAssets, List<Room> availableRooms, List<SpecialAccommodation> specialAccommodations, out string emailBody, out List<int> iconIds, List<Rule> rules, int overrideRoomAvailabilityPid, string code_FindPotentialBookingsStart, string code_FindPotentialBookingsEnd, string code_FindPotentialBookingsMid, out List<PrivateNote> privateNotes, FindPotentialBookingsInfo findPotentialBookingsInfo)
		{
			return Booker.FindPotentialTestBookings(binPath, debugMode, page, pid, lucid, dayToLookIn, classTest, accommodations, availableAssets, availableRooms, specialAccommodations, out emailBody, out iconIds, rules, overrideRoomAvailabilityPid, code_FindPotentialBookingsStart, code_FindPotentialBookingsEnd, code_FindPotentialBookingsMid, out privateNotes, null, true, true, 0, findPotentialBookingsInfo);
		}

		// Token: 0x06000309 RID: 777 RVA: 0x00012E74 File Offset: 0x00011074
		private static string LoadCampus(int lucid)
		{
			DatabaseLayer clockWork = DatabaseLayerFactory.ClockWork;
			string query = "SELECT campus FROM lucourses WHERE lucourseid=@lucid";
			DataTable dataTable = clockWork.ExecuteQuery(query, new DbParameter[]
			{
				clockWork.GetParameter("@lucid", DbType.Int32, lucid)
			});
			return (dataTable.Rows.Count > 0) ? dataTable.Rows[0][0].ToString().Trim() : "";
		}

		// Token: 0x0600030A RID: 778 RVA: 0x00012EEC File Offset: 0x000110EC
		public static List<PotentialTest> FindPotentialTestBookings(string binPath, bool debugMode, Page page, int pid, int lucid, DateTime dayToLookIn, Test classTest, List<Accommodation> accommodations, List<Asset> availableAssets, List<Room> availableRooms0, List<SpecialAccommodation> specialAccommodations, out string emailBody, out List<int> iconIds, List<Rule> rules, int overrideRoomAvailabilityPid, string code_FindPotentialBookingsStart, string code_FindPotentialBookingsEnd, string code_FindPotentialBookingsMid, out List<PrivateNote> privateNotes, List<Booking> unavailableRoomBookings, bool loadRoomSchedules, bool applySpecialAccommodationRules, int appIdToIgnoreWhenCheckingStudentsSchedule, FindPotentialBookingsInfo findPotentialBookingsInfo)
		{
			BookingResults bookingResults;
			return Booker.FindPotentialTestBookings(binPath, debugMode, page, pid, lucid, dayToLookIn, classTest, accommodations, availableAssets, availableRooms0, specialAccommodations, out emailBody, out iconIds, rules, overrideRoomAvailabilityPid, code_FindPotentialBookingsStart, code_FindPotentialBookingsEnd, code_FindPotentialBookingsMid, out privateNotes, unavailableRoomBookings, loadRoomSchedules, applySpecialAccommodationRules, appIdToIgnoreWhenCheckingStudentsSchedule, findPotentialBookingsInfo, out bookingResults);
		}

		// Token: 0x0600030B RID: 779 RVA: 0x00012F34 File Offset: 0x00011134
		public static List<PotentialTest> FindPotentialTestBookings(string binPath, bool debugMode, Page page, int pid, int lucid, DateTime dayToLookIn, Test classTest, List<Accommodation> accommodations, List<Asset> availableAssets, List<Room> availableRooms0, List<SpecialAccommodation> specialAccommodations, out string emailBody, out List<int> iconIds, List<Rule> rules, int overrideRoomAvailabilityPid, string code_FindPotentialBookingsStart, string code_FindPotentialBookingsEnd, string code_FindPotentialBookingsMid, out List<PrivateNote> privateNotes, List<Booking> unavailableRoomBookings, bool loadRoomSchedules, bool applySpecialAccommodationRules, int appIdToIgnoreWhenCheckingStudentsSchedule, FindPotentialBookingsInfo findPotentialBookingsInfo, out BookingResults bookingResults)
		{
			IWebSettingsClientManager webSettingsClientManager = new WebSettingsClientManager();
			int bufferMinutesPre = findPotentialBookingsInfo.BufferMinutesPre;
			int bufferMinutesPost = findPotentialBookingsInfo.BufferMinutesPost;
			string campus = Booker.LoadCampus(lucid);
			bookingResults = new BookingResults();
			bool flag = string.IsNullOrEmpty(campus) || !findPotentialBookingsInfo.RestrictByCampus;
			List<Room> list;
			if (flag)
			{
				list = new List<Room>(availableRooms0.Count);
				list.AddRange(availableRooms0);
			}
			else
			{
				list = availableRooms0.FindAll((Room eg) => eg.SupportsCampus(campus));
			}
			FindPotentialBookingInfo pbookingInfo = new FindPotentialBookingInfo(debugMode, pid, lucid, dayToLookIn, classTest, accommodations, availableAssets, list, specialAccommodations);
			CustomTestBookingRulesClass customRules = Booker.GetCustomRules(binPath, code_FindPotentialBookingsStart, code_FindPotentialBookingsEnd, code_FindPotentialBookingsMid);
			int num = 0;
			List<PotentialTest> list2 = new List<PotentialTest>();
			int item = 2;
			int num2 = 1;
			List<int> list3 = new List<int>();
			StringBuilder stringBuilder = new StringBuilder();
			int num3 = 0;
			Test test;
			if (applySpecialAccommodationRules)
			{
				test = Booker.ApplySpecialAccommodationRules(debugMode, pid, lucid, specialAccommodations, classTest, accommodations, out privateNotes, out stringBuilder, out list3);
				bool flag2 = test != null && test.BreakTime > 0;
				if (flag2)
				{
					test.EndDate = test.EndDate.AddMinutes((double)test.BreakTime);
				}
			}
			else
			{
				test = classTest;
				privateNotes = new List<PrivateNote>();
			}
			int duration = test.Duration;
			num3 = test.BreakTime;
			DateTime dateTime = new DateTime(test.StartDate.Year, test.StartDate.Month, test.StartDate.Day);
			bool settingValue = webSettingsClientManager.GetSettingValue<bool>(Setting.TESTBOOKING_IgnoreStudentTimetable);
			bool flag3 = settingValue;
			List<TimeTableItem> list4;
			if (flag3)
			{
				list4 = new List<TimeTableItem>();
			}
			else
			{
				list4 = Course.LoadTimetable(pid, lucid, classTest.StartDate.Date);
			}
			bool ignoreStudentsSchedule = findPotentialBookingsInfo.IgnoreStudentsSchedule;
			DataTable dataTable;
			if (ignoreStudentsSchedule)
			{
				dataTable = new DataTable();
			}
			else
			{
				dataTable = Booker.LoadStudentSchedule(pid, dateTime, appIdToIgnoreWhenCheckingStudentsSchedule);
			}
			bool flag4 = findPotentialBookingsInfo.IgnoreStudentAppointmentIds != null && findPotentialBookingsInfo.IgnoreStudentAppointmentIds.Count > 0;
			if (flag4)
			{
				List<DataRow> list5 = new List<DataRow>();
				foreach (int num4 in findPotentialBookingsInfo.IgnoreStudentAppointmentIds)
				{
					DataRow[] array = dataTable.Select("appointmentid=" + num4.ToString());
					foreach (DataRow item2 in array)
					{
						list5.Add(item2);
					}
				}
				StringBuilder stringBuilder2 = new StringBuilder();
				foreach (DataRow dataRow in list5)
				{
					stringBuilder2.AppendFormat("{0},", dataRow["appointmentid"].ToString());
					dataTable.Rows.Remove(dataRow);
				}
				bool flag5 = list5.Count > 0;
				if (flag5)
				{
					CWLogger.Logger.Debug("TESTBOOK:FindPotentials:RemoveAppointmentsFromStudentsSchedule:pid={0}:lucid={1}:appidsRemoved={2}", pid.ToString(), lucid.ToString(), stringBuilder2.ToString());
				}
			}
			List<DateRange> list6 = DateRange.FromTable(dataTable);
			bool flag6 = list4.Count < 1;
			if (flag6)
			{
				CWLogger.Logger.Debug("TESTBOOK:FindPotentials:MissingTimetables:pid={0}:lucid={1}", pid.ToString(), lucid.ToString());
			}
			int num5 = Asset.GetMaxAccommodationLevel(availableAssets, accommodations);
			bool flag7 = num5 < 1;
			if (flag7)
			{
				num5 = 1;
			}
			iconIds = new List<int>();
			emailBody = "";
			StringBuilder stringBuilder3 = new StringBuilder();
			DateTime baseDate = new DateTime(2000, 1, 1);
			CWLogger.Logger.Debug("TESTBOOK:FindPotentials:Entry:pid={0}:lucid={1}:studentschedule={2}:othercoursestimetable={3}", new object[]
			{
				pid.ToString(),
				lucid.ToString(),
				stringBuilder3.ToString(),
				string.Join(", ", list4.ConvertAll<string>((TimeTableItem tti) => string.Format("{0}-{1} [{2} to {3}]", new object[]
				{
					tti.LuCourseId.ToString(),
					tti.DayOfWeek.ToString(),
					baseDate.AddMinutes((double)tti.StartMinutes).ToString("H:mm"),
					baseDate.AddMinutes((double)tti.EndMinutes).ToString("H:mm")
				})).ToArray())
			});
			List<Accommodation>[] array3 = new List<Accommodation>[num5];
			int i;
			int i2;
			for (i = num5 - 1; i >= 0; i = i2 - 1)
			{
				array3[i] = new List<Accommodation>();
				using (List<Accommodation>.Enumerator enumerator3 = accommodations.GetEnumerator())
				{
					while (enumerator3.MoveNext())
					{
						Accommodation acc = enumerator3.Current;
						Predicate<Accommodation> <>9__6;
						bool flag8 = i == 0 || availableAssets.Find(delegate(Asset aa)
						{
							List<Accommodation> accommodationsSupported = aa.AccommodationsSupported;
							Predicate<Accommodation> match2;
							if ((match2 = <>9__6) == null)
							{
								match2 = (<>9__6 = ((Accommodation accs) => accs.Controlid == acc.Controlid && accs.Level == i + 1));
							}
							return accommodationsSupported.Find(match2) != null;
						}) != null;
						if (flag8)
						{
							array3[i].Add(new Accommodation(acc.Controlid, acc.Title, acc.LookupText, i + 1));
						}
					}
				}
				i2 = i;
			}
			List<List<Accommodation>> list7 = new List<List<Accommodation>>();
			for (int j = 0; j < array3.Length; j++)
			{
				bool flag9 = j == 0;
				if (flag9)
				{
					List<Accommodation> list8 = new List<Accommodation>();
					foreach (Accommodation accommodation in array3[j])
					{
						Accommodation item3 = new Accommodation(accommodation.Controlid, accommodation.Title, accommodation.LookupText, accommodation.SubText, 1);
						list8.Add(item3);
					}
					list7.Add(list8);
				}
				else
				{
					for (int k = 0; k < array3[j].Count; k++)
					{
						List<List<Accommodation>> list9 = new List<List<Accommodation>>();
						foreach (List<Accommodation> item4 in list7)
						{
							list9.Add(item4);
						}
						Accommodation accommodation2 = array3[j][k];
						foreach (List<Accommodation> list10 in list9)
						{
							List<Accommodation> list11 = new List<Accommodation>();
							foreach (Accommodation accommodation3 in list10)
							{
								Accommodation accommodation4 = new Accommodation(accommodation3.Controlid, accommodation3.Title, accommodation3.LookupText, accommodation3.SubText, accommodation3.Level);
								bool flag10 = accommodation4.Controlid == accommodation2.Controlid;
								if (flag10)
								{
									accommodation4.Level = j + 1;
								}
								list11.Add(accommodation4);
							}
							list7.Add(list11);
						}
					}
				}
			}
			try
			{
				CWLogger.Logger.Trace("FindPotentialTestBookings:LogAccommodationCombos:combos={0}", string.Join("\r\n • ", list7.ConvertAll<string>((List<Accommodation> a2) => string.Join(", ", a2.ConvertAll<string>((Accommodation attt) => string.Format("cid={0}; level={1}", attt.Controlid.ToString(), attt.Level.ToString())).ToArray())).ToArray()));
			}
			catch
			{
			}
			List<List<Asset>> list12 = new List<List<Asset>>();
			foreach (List<Accommodation> list13 in list7)
			{
				bool flag11 = list13.Count > 0;
				if (flag11)
				{
					List<Asset> item5 = Booker.FigureOutRequiredAssets(pid, lucid, availableAssets, list13);
					list12.Add(item5);
				}
			}
			bool flag12 = list12.Count < 1;
			if (flag12)
			{
				list12.Add(new List<Asset>());
			}
			CWLogger.Logger.Trace("FindPotentialTestBookings:LogAssetLevelsList:assetlevels={0}", string.Join("\r\n • ", list12.ConvertAll<string>((List<Asset> a2) => string.Join(", ", a2.ConvertAll<string>((Asset attt) => attt.AssetId).ToArray())).ToArray()));
			int count = list12.Count;
			for (int l = 0; l < count; l++)
			{
				List<Asset> list14 = list12[l];
				List<PotentialRoom> list15 = Booker.FigureOutRequiredRoomsInOrder(pid, lucid, list14, list, accommodations);
				bool flag13 = count > 0 && l < count - 1;
				if (flag13)
				{
					List<PotentialRoom> list16 = list15.FindAll((PotentialRoom rr) => rr.Room.RoomType == RoomType.RegularRoom);
					list15 = list16;
				}
				bool flag14 = overrideRoomAvailabilityPid > 0;
				AvailabilitySchedule availabilitySchedule;
				if (flag14)
				{
					availabilitySchedule = new AvailabilitySchedule(overrideRoomAvailabilityPid, PotentialRoom.GetRoomPids(list15), new List<int>
					{
						item
					}, dateTime, dateTime.AddDays(1.0).AddMinutes(-1.0));
				}
				else
				{
					availabilitySchedule = new AvailabilitySchedule(PotentialRoom.GetRoomPids(list15), new List<int>
					{
						item
					}, dateTime, dateTime.AddDays(1.0).AddMinutes(-1.0));
				}
				CWLogger.Logger.Trace("TESTBOOK:FindPotentials:LoadRoomAvailability:RoomsWithAvailability={0}", string.Join(", ", availabilitySchedule.Ranges.ConvertAll<string>((AvailabilityScheduleRange r) => r.ToStringDebug()).ToArray()));
				foreach (AvailabilityScheduleRange availabilityScheduleRange in availabilitySchedule.Ranges)
				{
					foreach (PotentialRoom potentialRoom in list15)
					{
						bool flag15 = potentialRoom.Room.RoomId == availabilityScheduleRange.Pid;
						if (flag15)
						{
							bool flag16 = potentialRoom.AvailabilityStartTimeForTheDay == DateTime.MinValue || availabilityScheduleRange.Start < potentialRoom.AvailabilityStartTimeForTheDay;
							if (flag16)
							{
								potentialRoom.AvailabilityStartTimeForTheDay = availabilityScheduleRange.Start;
							}
							bool flag17 = potentialRoom.AvailabilityEndTimeForTheDay == DateTime.MinValue || availabilityScheduleRange.End > potentialRoom.AvailabilityEndTimeForTheDay;
							if (flag17)
							{
								potentialRoom.AvailabilityEndTimeForTheDay = availabilityScheduleRange.End;
							}
						}
					}
				}
				DataTable t;
				if (loadRoomSchedules)
				{
					t = Booker.LoadRoomSchedules(list15, dateTime);
				}
				else
				{
					t = new DataTable();
				}
				List<DateRange> list17 = DateRange.FromTable(t);
				DataTable dataTable2 = Booker.LoadStudentTimetable(pid, lucid);
				bool flag18 = unavailableRoomBookings != null;
				if (flag18)
				{
					using (List<PotentialRoom>.Enumerator enumerator11 = list15.GetEnumerator())
					{
						while (enumerator11.MoveNext())
						{
							PotentialRoom proom = enumerator11.Current;
							List<Booking> list18 = unavailableRoomBookings.FindAll((Booking e) => e.Pid == proom.Room.RoomId);
							foreach (Booking booking in list18)
							{
								list17.Add(new DateRange(booking.Pid, booking.StartDateTime, booking.EndDateTime));
							}
						}
					}
				}
				CWLogger logger = CWLogger.Logger;
				string message = "TESTBOOK:FindPotentials:PreCheck:pid={0}:lucid={1}:AssetsReqd={2}:RoomsMatchingAssets:{3}";
				object[] array4 = new object[4];
				array4[0] = pid.ToString();
				array4[1] = lucid.ToString();
				array4[2] = string.Join(", ", list14.ConvertAll<string>((Asset asst) => asst.ToStringDebug()).ToArray());
				array4[3] = string.Join(", ", list15.ConvertAll<string>((PotentialRoom rm) => (rm.Room == null) ? "NULL" : rm.Room.ToStringDebug()).ToArray());
				logger.Debug(message, array4);
				emailBody = stringBuilder.ToString();
				iconIds = list3;
				bool flag19 = customRules != null;
				if (flag19)
				{
					Exception ex;
					object obj = customRules.FindPotentialBookingsStart(code_FindPotentialBookingsStart, code_FindPotentialBookingsEnd, code_FindPotentialBookingsMid, ref list2, ref rules, ref list14, ref list15, pbookingInfo, out ex, Array.Empty<object>());
				}
				int num6 = 0;
				foreach (Rule rule in rules)
				{
					num6++;
					DateTime dateTime2 = new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, test.StartDate.Hour, test.StartDate.Minute, 0);
					DateTime dateTime3 = dateTime2.AddMinutes((double)duration);
					List<DateRange> list19 = new List<DateRange>();
					int num7 = 15;
					int num8 = 1;
					bool enforceOverlapWithClassTime = rule.EnforceOverlapWithClassTime;
					if (enforceOverlapWithClassTime)
					{
						num8 = 5;
					}
					bool flag20 = num8 > num2;
					if (flag20)
					{
						num2 = num8;
					}
					bool enforceOverlapWithClassTime2 = rule.EnforceOverlapWithClassTime;
					if (enforceOverlapWithClassTime2)
					{
						DateTime startDate = classTest.StartDate;
						DateTime dateTime4 = startDate.AddMinutes((double)duration);
						while (dateTime4 >= classTest.EndDate)
						{
							list19.Add(new DateRange(startDate, dateTime4));
							startDate = startDate.AddMinutes((double)(-(double)num7));
							dateTime4 = dateTime4.AddMinutes((double)(-(double)num7));
						}
					}
					else
					{
						for (int m = 0; m <= rule.MinutesPost; m += num7)
						{
							list19.Add(new DateRange(dateTime2.AddMinutes((double)m), dateTime3.AddMinutes((double)m)));
						}
						for (int n = 0; n <= rule.MinutesPre; n += num7)
						{
							list19.Add(new DateRange(dateTime2.AddMinutes((double)(-(double)n)), dateTime3.AddMinutes((double)(-(double)n))));
						}
						bool flag21 = rule.MinutesPost > 0 && rule.MinutesPre > 0 && num2 <= 1;
						if (flag21)
						{
							num2 = 10;
						}
						else
						{
							bool flag22 = (rule.MinutesPost > 0 || rule.MinutesPre > 0) && num2 <= 1;
							if (flag22)
							{
								num2 = 5;
							}
						}
					}
					List<PotentialTest> list20 = new List<PotentialTest>();
					List<PotentialTest> list21 = null;
					bool flag23 = customRules != null;
					if (flag23)
					{
						Exception ex;
						object obj = customRules.FindPotentialBookingsMid(code_FindPotentialBookingsStart, code_FindPotentialBookingsEnd, code_FindPotentialBookingsMid, 'a', ref list19, rule, ref list21, ref list2, ref rules, ref list14, ref list15, pbookingInfo, out ex, Array.Empty<object>());
					}
					List<PotentialRoom> list22 = new List<PotentialRoom>();
					foreach (PotentialRoom potentialRoom2 in list15)
					{
						Room room2 = potentialRoom2.Room;
						bool flag24 = potentialRoom2.Room.RoomType == RoomType.VirtualRoom || potentialRoom2.Room.RoomType == RoomType.SuperVirtualRoom;
						bool flag25 = rule.IncludeNonVirtualRooms && !flag24;
						PotentialRoom potentialRoom3;
						if (flag25)
						{
							potentialRoom3 = potentialRoom2;
						}
						else
						{
							bool flag26 = rule.IncludeVirtualRooms && flag24;
							if (flag26)
							{
								potentialRoom3 = potentialRoom2;
							}
							else
							{
								potentialRoom3 = null;
							}
						}
						bool flag27 = potentialRoom3 != null && !rule.RoomIdsToExclud.Contains(potentialRoom3.Room.RoomId);
						if (flag27)
						{
							list22.Add(potentialRoom3);
						}
					}
					CWLogger logger2 = CWLogger.Logger;
					string message2 = "TESTBOOK:FindPotentials:EvaluateRule:RuleCounter={0}:pid={1}:lucid={2}:overlapclasstime={3}:NonVirtualRooms={4}:VirtualRooms={5}:TimesToInvestigate={6}:Rooms={7}:currentlevel={8}";
					object[] array5 = new object[9];
					array5[0] = num6.ToString();
					array5[1] = pid.ToString();
					array5[2] = lucid.ToString();
					array5[3] = rule.EnforceOverlapWithClassTime.ToString();
					array5[4] = rule.IncludeNonVirtualRooms.ToString();
					array5[5] = rule.IncludeVirtualRooms.ToString();
					array5[6] = string.Join(", ", list19.ConvertAll<string>((DateRange dr) => string.Format("{0} to {1}", dr.StartDate.ToString("yyyy-MM-dd H:mm"), dr.EndDate.ToString("H:mm"))).ToArray());
					array5[7] = string.Join(", ", list22.ConvertAll<string>((PotentialRoom rm) => string.Format("{0}-{1} . AvailabilityMinMax={2} to {3}", new object[]
					{
						(rm.Room == null) ? "NULL" : rm.Room.Title,
						(rm.Room == null) ? "NULL" : rm.Room.RoomId.ToString(),
						rm.AvailabilityStartTimeForTheDay.ToString("H:mm"),
						rm.AvailabilityEndTimeForTheDay.ToString("H:mm")
					})).ToArray());
					array5[8] = l.ToString();
					logger2.Debug(message2, array5);
					foreach (PotentialRoom potentialRoom4 in list22)
					{
						Room room = potentialRoom4.Room;
						bool isVirtualRoom = potentialRoom4.Room.IsVirtualRoom;
						List<PotentialTest> list23 = new List<PotentialTest>();
						bool flag28 = isVirtualRoom;
						if (flag28)
						{
							foreach (DateRange dateRange in list19)
							{
								DateTime sd = dateRange.StartDate;
								DateTime ed = dateRange.EndDate;
								bool flag29 = potentialRoom4.IsAvailableByStartAndEndofDayAvailabilityTimes(sd, ed);
								bool flag30 = !TimeTableItem.Overlaps(list4, sd, ed);
								bool flag31 = flag30;
								if (flag31)
								{
									bookingResults.FailedTimetableCheck = new bool?(false);
								}
								else
								{
									bool flag32 = bookingResults.FailedTimetableCheck == null;
									if (flag32)
									{
										bookingResults.FailedTimetableCheck = new bool?(true);
									}
								}
								bool flag33 = list6.Exists((DateRange ss) => ss.Intersects(sd, ed));
								bool flag34 = !flag33;
								if (flag34)
								{
									bookingResults.StudentIsDoubleBooked = new bool?(false);
								}
								else
								{
									bool flag35 = bookingResults.StudentIsDoubleBooked == null;
									if (flag35)
									{
										bookingResults.StudentIsDoubleBooked = new bool?(true);
									}
								}
								bool flag36 = flag30 && !flag33;
								if (flag36)
								{
									bool flag37 = flag29;
									if (flag37)
									{
										PotentialTest potentialTest = new PotentialTest(num++, sd, ed, room, true);
										potentialTest.AddMethodFoundNote("Virtual room - ok to double book");
										potentialTest.AddMethodFoundNote("Passed room availability & timetable checks");
										potentialTest.AddMethodFoundNote("Rule #{0} of {1} [{2}]", new string[]
										{
											num6.ToString(),
											rules.Count.ToString(),
											rule.ToString()
										});
										list23.Add(potentialTest);
									}
									else
									{
										bool shiftTimeToMatchEndOfDay = rule.ShiftTimeToMatchEndOfDay;
										if (shiftTimeToMatchEndOfDay)
										{
											bool flag38 = potentialRoom4.AvailabilityEndTimeForTheDay != DateTime.MinValue && potentialRoom4.AvailabilityEndTimeForTheDay < ed;
											if (flag38)
											{
												DateTime[] array6 = new DateTime[]
												{
													potentialRoom4.AvailabilityEndTimeForTheDay,
													ed
												};
												TimeSpan timeSpan = array6[1] - array6[0];
												DateTime dateTime5 = sd.AddMinutes(-timeSpan.TotalMinutes);
												DateTime dateTime6 = ed.AddMinutes(-timeSpan.TotalMinutes);
												bool flag39 = potentialRoom4.IsAvailableByStartAndEndofDayAvailabilityTimes(dateTime5, dateTime6);
												if (flag39)
												{
													PotentialTest potentialTest2 = new PotentialTest(num++, dateTime5, dateTime6, room, true);
													potentialTest2.AddMethodFoundNote("Virtual room - ok to double book");
													potentialTest2.AddMethodFoundNote("Passed room availability & timetable checks");
													potentialTest2.AddMethodFoundNote("Rule #{0} of {1} [{2}]", new string[]
													{
														num6.ToString(),
														rules.Count.ToString(),
														rule.ToString()
													});
													potentialTest2.AddMethodFoundNote("Shift back end of day time availability (room end time={0})", new string[]
													{
														potentialRoom4.AvailabilityEndTimeForTheDay.ToString("yyyy-MM-dd h:mm tt")
													});
													list23.Add(potentialTest2);
													privateNotes.Add(new PrivateNote(string.Format("Moved from {0} to {1} due to end of day.", classTest.StartDate.ToString("h:mm tt"), dateTime5.ToString("h:mm tt"))));
												}
											}
										}
										else
										{
											bool flag40 = potentialRoom4.AvailabilityStartTimeForTheDay != DateTime.MinValue && potentialRoom4.AvailabilityStartTimeForTheDay > sd;
											if (flag40)
											{
												DateTime[] array7 = new DateTime[]
												{
													potentialRoom4.AvailabilityStartTimeForTheDay,
													sd
												};
												TimeSpan timeSpan2 = array7[0] - array7[1];
												DateTime dateTime7 = sd.AddMinutes(timeSpan2.TotalMinutes);
												DateTime dateTime8 = ed.AddMinutes(timeSpan2.TotalMinutes);
												bool flag41 = potentialRoom4.IsAvailableByStartAndEndofDayAvailabilityTimes(dateTime7, dateTime8);
												if (flag41)
												{
													PotentialTest potentialTest3 = new PotentialTest(num++, dateTime7, dateTime8, room, true);
													potentialTest3.AddMethodFoundNote("Virtual room - ok to double book");
													potentialTest3.AddMethodFoundNote("Passed room availability & timetable checks");
													potentialTest3.AddMethodFoundNote("Rule #{0} of {1} [{2}]", new string[]
													{
														num6.ToString(),
														rules.Count.ToString(),
														rule.ToString()
													});
													potentialTest3.AddMethodFoundNote("Shift forward start of day time availability (room start time={0})", new string[]
													{
														potentialRoom4.AvailabilityStartTimeForTheDay.ToString("yyyy-MM-dd h:mm tt")
													});
													list23.Add(potentialTest3);
													privateNotes.Add(new PrivateNote(string.Format("Moved from {0} to {1} due to start of day.", classTest.StartDate.ToString("h:mm tt"), dateTime7.ToString("h:mm tt"))));
												}
											}
										}
									}
								}
							}
						}
						else
						{
							foreach (DateRange dateRange2 in list19)
							{
								DateTime sd = dateRange2.StartDate;
								DateTime ed = dateRange2.EndDate;
								bool flag42 = !TimeTableItem.Overlaps(list4, sd, ed);
								bool flag43 = flag42;
								if (flag43)
								{
									bookingResults.FailedTimetableCheck = new bool?(false);
								}
								else
								{
									bool flag44 = bookingResults.FailedTimetableCheck == null;
									if (flag44)
									{
										bookingResults.FailedTimetableCheck = new bool?(true);
									}
								}
								List<List<DateRange>> list24 = new List<List<DateRange>>();
								List<DateRange> list25 = new List<DateRange>();
								list24.Add(list25);
								bool flag45 = flag42 || !rule.ShiftTimeAroundTimetable;
								if (flag45)
								{
									list25.Add(dateRange2);
								}
								else
								{
									int num9 = Convert.ToInt32((ed - sd).TotalMinutes);
									DateTime dateTime9 = sd;
									while (dateTime9 <= sd.Date.AddHours(23.0))
									{
										list25.Add(new DateRange(dateTime9, dateTime9.AddMinutes((double)num9)));
										dateTime9 = dateTime9.AddMinutes(15.0);
									}
									List<DateRange> list26 = new List<DateRange>();
									dateTime9 = sd;
									while (dateTime9 >= sd.Date.AddHours(7.0))
									{
										list26.Add(new DateRange(dateTime9, dateTime9.AddMinutes((double)num9)));
										dateTime9 = dateTime9.AddMinutes(-15.0);
									}
									bool flag46 = list26.Count > 0;
									if (flag46)
									{
										list24.Add(list26);
									}
								}
								Predicate<DateRange> <>9__17;
								foreach (List<DateRange> list27 in list24)
								{
									bool flag47 = false;
									foreach (DateRange dateRange3 in list27)
									{
										bool flag48 = rule.ShiftTimeToMatchStartOfDay && dateRange3.StartDate < potentialRoom4.AvailabilityStartTimeForTheDay;
										if (flag48)
										{
											TimeSpan timeSpan3 = dateRange3.EndDate - dateRange3.StartDate;
											dateRange3.StartDate = new DateTime(dateRange3.StartDate.Year, dateRange3.StartDate.Month, dateRange3.StartDate.Day, potentialRoom4.AvailabilityStartTimeForTheDay.Hour, potentialRoom4.AvailabilityStartTimeForTheDay.Minute, 0);
											dateRange3.EndDate = dateRange3.StartDate.AddMinutes(timeSpan3.TotalMinutes);
										}
										else
										{
											bool flag49 = rule.ShiftTimeToMatchEndOfDay && dateRange3.EndDate > potentialRoom4.AvailabilityEndTimeForTheDay;
											if (flag49)
											{
												TimeSpan timeSpan4 = dateRange3.EndDate - dateRange3.StartDate;
												dateRange3.EndDate = new DateTime(dateRange3.EndDate.Year, dateRange3.EndDate.Month, dateRange3.EndDate.Day, potentialRoom4.AvailabilityEndTimeForTheDay.Hour, potentialRoom4.AvailabilityEndTimeForTheDay.Minute, 0);
												dateRange3.StartDate = dateRange3.EndDate.AddMinutes(-timeSpan4.TotalMinutes);
											}
										}
										sd = dateRange3.StartDate;
										ed = dateRange3.EndDate;
										flag42 = !TimeTableItem.Overlaps(list4, sd, ed);
										bool flag50 = flag42;
										if (flag50)
										{
											bookingResults.FailedTimetableCheck = new bool?(false);
										}
										else
										{
											bool flag51 = bookingResults.FailedTimetableCheck == null;
											if (flag51)
											{
												bookingResults.FailedTimetableCheck = new bool?(true);
											}
										}
										List<DateRange> list28 = list6;
										Predicate<DateRange> match;
										if ((match = <>9__17) == null)
										{
											match = (<>9__17 = ((DateRange ss) => ss.Intersects(sd, ed)));
										}
										bool flag52 = list28.Exists(match);
										bool flag53 = !flag52;
										if (flag53)
										{
											bookingResults.StudentIsDoubleBooked = new bool?(false);
										}
										else
										{
											bool flag54 = bookingResults.StudentIsDoubleBooked == null;
											if (flag54)
											{
												bookingResults.StudentIsDoubleBooked = new bool?(true);
											}
										}
										DateTime sdWithBuffer = sd.AddMinutes((double)(-(double)bufferMinutesPre));
										DateTime edWithBuffer = ed.AddMinutes((double)bufferMinutesPost);
										bool flag55 = list17.Exists((DateRange rs) => rs.Scope == room.RoomId && rs.Intersects(sdWithBuffer, edWithBuffer));
										bool flag56 = !flag55;
										if (flag56)
										{
											bookingResults.RoomIsDoubleBooked = new bool?(false);
										}
										else
										{
											bool flag57 = bookingResults.RoomIsDoubleBooked == null;
											if (flag57)
											{
												bookingResults.RoomIsDoubleBooked = new bool?(true);
											}
										}
										CWLogger.Logger.Debug("TESTBOOK:FindPotentials:EvaluateNonVirtualRoom_TimeToInvestigate:pid={0}:lucid={1}:time={2} to {3}:passedtimetablecheck={4}:studentdoublebooked={5},roomdoublebooked={6}", new object[]
										{
											pid.ToString(),
											lucid.ToString(),
											sd.ToString("yyyy-MM-dd H:mm"),
											ed.ToString("H:mm"),
											flag42.ToString(),
											flag52.ToString(),
											flag55.ToString()
										});
										bool flag58 = flag42 && !flag52 && !flag55;
										if (flag58)
										{
											foreach (AvailabilityScheduleRange availabilityScheduleRange2 in availabilitySchedule.Ranges)
											{
												bool flag59 = availabilityScheduleRange2.Pid == room.RoomId;
												if (flag59)
												{
													bool flag60 = availabilityScheduleRange2.Start <= sd && availabilityScheduleRange2.End >= ed;
													if (flag60)
													{
														PotentialTest potentialTest4 = new PotentialTest(num++, sd, ed, room, false);
														potentialTest4.AddMethodFoundNote("NON virtual room - NOT ok to double book");
														potentialTest4.AddMethodFoundNote("Passed student schedule & timetable & room availability checks");
														potentialTest4.AddMethodFoundNote("Rule #{0} of {1} [{2}]", new string[]
														{
															num6.ToString(),
															rules.Count.ToString(),
															rule.ToString()
														});
														list23.Add(potentialTest4);
														bookingResults.NoRoomAvailability = new bool?(false);
														flag47 = true;
													}
												}
												bool flag61 = num8 > 0 && list23.Count > num8;
												if (flag61)
												{
													break;
												}
											}
											bool flag62 = bookingResults.NoRoomAvailability == null;
											if (flag62)
											{
												bookingResults.NoRoomAvailability = new bool?(true);
											}
											bool flag63 = num8 > 0 && list23.Count > num8;
											if (flag63)
											{
												break;
											}
										}
										bool flag64 = num8 > 0 && list23.Count > num8;
										if (flag64)
										{
											break;
										}
									}
									bool flag65 = flag47;
									if (flag65)
									{
										break;
									}
								}
							}
						}
						bool flag66 = customRules != null;
						if (flag66)
						{
							Exception ex;
							object obj = customRules.FindPotentialBookingsMid(code_FindPotentialBookingsStart, code_FindPotentialBookingsEnd, code_FindPotentialBookingsMid, 'd', ref list19, rule, ref list23, ref list2, ref rules, ref list14, ref list15, pbookingInfo, out ex, Array.Empty<object>());
						}
						foreach (PotentialTest item6 in list23)
						{
							list20.Add(item6);
						}
						bool flag67 = num8 > 0 && list20.Count > num8;
						if (flag67)
						{
							break;
						}
					}
					foreach (PotentialTest item7 in list20)
					{
						list2.Add(item7);
					}
					bool flag68 = num8 > 0 && list2.Count > num8;
					if (flag68)
					{
						break;
					}
				}
				bool flag69 = customRules != null;
				if (flag69)
				{
					Exception ex;
					object obj = customRules.FindPotentialBookingsEnd(code_FindPotentialBookingsStart, code_FindPotentialBookingsEnd, code_FindPotentialBookingsMid, ref list2, ref rules, ref list14, ref list15, pbookingInfo, out ex, Array.Empty<object>());
				}
				bool flag70 = list2.Count > 0;
				if (flag70)
				{
					break;
				}
			}
			int num10 = list2.Count - num2;
			bool flag71 = num10 > 0;
			if (flag71)
			{
				for (int num11 = 0; num11 < num10; num11++)
				{
					bool flag72 = list2.Count > 0;
					if (flag72)
					{
						list2.RemoveAt(list2.Count - 1);
					}
				}
			}
			bool flag73 = num3 > 0;
			if (flag73)
			{
				foreach (PotentialTest potentialTest5 in list2)
				{
					potentialTest5.Test.BreakTime = num3;
				}
			}
			CWLogger logger3 = CWLogger.Logger;
			string message3 = "TESTBOOK:FindPotentials:ReturnPotentialTests:pid={0}:lucid={1}:bookingresults.noroomavailability={2}:PotentialTests={3}";
			object[] array8 = new object[4];
			array8[0] = pid.ToString();
			array8[1] = lucid.ToString();
			array8[2] = ((bookingResults == null || bookingResults.NoRoomAvailability == null) ? "NULL" : bookingResults.NoRoomAvailability.ToString());
			array8[3] = string.Join(", ", list2.ConvertAll<string>((PotentialTest pt) => pt.ToStringDebug()).ToArray());
			logger3.Debug(message3, array8);
			return list2;
		}

		// Token: 0x0600030C RID: 780 RVA: 0x000151C0 File Offset: 0x000133C0
		public static Test ApplySpecialAccommodationRules(bool debugMode, int pid, int lucid, List<SpecialAccommodation> specialAccommodations, Test classTest, List<Accommodation> accommodationsToUse, out List<PrivateNote> privateNotes, out StringBuilder emailBodySb, out List<int> iconsToBookWith)
		{
			privateNotes = new List<PrivateNote>();
			iconsToBookWith = new List<int>();
			emailBodySb = new StringBuilder();
			int num = classTest.Duration;
			Test test = new Test(classTest.StartDate, classTest.EndDate, classTest.Room);
			int num2 = num;
			specialAccommodations.Sort((SpecialAccommodation s1, SpecialAccommodation s2) => s1.SpecialAccommodationTypeOrder.CompareTo(s2.SpecialAccommodationTypeOrder));
			foreach (SpecialAccommodation specialAccommodation in specialAccommodations)
			{
				bool flag = specialAccommodation.SpecialAccommodationType == SpecialAccommodationType.SnapTime;
				if (flag)
				{
					string arg = specialAccommodation.GetArg("rules", "");
					bool flag2 = !string.IsNullOrEmpty(arg);
					if (flag2)
					{
						string str = "2000-01-01 ";
						string[] array = arg.Split(new char[]
						{
							','
						}, StringSplitOptions.RemoveEmptyEntries);
						foreach (string text in array)
						{
							int num3 = text.LastIndexOf('=');
							bool flag3 = num3 > 0;
							if (flag3)
							{
								string text2 = text.Substring(0, num3);
								string text3 = text.Substring(num3 + 1).Trim();
								int numDaysDirection = 0;
								bool flag4 = text3.StartsWith("n", StringComparison.OrdinalIgnoreCase);
								if (flag4)
								{
									text3 = text3.Substring(1);
									numDaysDirection = 1;
								}
								else
								{
									bool flag5 = text3.StartsWith("p", StringComparison.OrdinalIgnoreCase);
									if (flag5)
									{
										text3 = text3.Substring(1);
										numDaysDirection = -1;
									}
								}
								bool flag6 = text3.Length <= 2;
								if (flag6)
								{
									text3 += ":00";
								}
								string s = str + text3;
								DateTime dateTime = new DateTime(2000, 1, 1, test.StartDate.Hour, test.StartDate.Minute, 0);
								DateTime dateTime2;
								bool flag7 = DateTime.TryParse(s, out dateTime2);
								if (flag7)
								{
									bool flag8 = text2.StartsWith("<=");
									if (flag8)
									{
										string text4 = text2.Substring(2).Trim();
										bool flag9 = text4.Length <= 2;
										if (flag9)
										{
											text4 += ":00";
										}
										text4 = str + text4;
										DateTime dateTime3;
										bool flag10 = DateTime.TryParse(text4, out dateTime3);
										if (flag10)
										{
											bool flag11 = dateTime <= dateTime3;
											if (flag11)
											{
												test.ShiftToStartAt(numDaysDirection, dateTime2.Hour, dateTime2.Minute);
												break;
											}
										}
									}
									else
									{
										bool flag12 = text2.StartsWith("<");
										if (flag12)
										{
											string text4 = text2.Substring(1).Trim();
											bool flag13 = text4.Length <= 2;
											if (flag13)
											{
												text4 += ":00";
											}
											text4 = str + text4;
											DateTime dateTime3;
											bool flag14 = DateTime.TryParse(text4, out dateTime3);
											if (flag14)
											{
												bool flag15 = dateTime < dateTime3;
												if (flag15)
												{
													test.ShiftToStartAt(numDaysDirection, dateTime2.Hour, dateTime2.Minute);
													break;
												}
											}
										}
										else
										{
											bool flag16 = text2.StartsWith(">=");
											if (flag16)
											{
												string text4 = text2.Substring(2).Trim();
												bool flag17 = text4.Length <= 2;
												if (flag17)
												{
													text4 += ":00";
												}
												text4 = str + text4;
												DateTime dateTime3;
												bool flag18 = DateTime.TryParse(text4, out dateTime3);
												if (flag18)
												{
													bool flag19 = dateTime >= dateTime3;
													if (flag19)
													{
														test.ShiftToStartAt(numDaysDirection, dateTime2.Hour, dateTime2.Minute);
														break;
													}
												}
											}
											else
											{
												bool flag20 = text2.StartsWith(">");
												if (flag20)
												{
													string text4 = text2.Substring(1).Trim();
													bool flag21 = text4.Length <= 2;
													if (flag21)
													{
														text4 += ":00";
													}
													text4 = str + text4;
													DateTime dateTime3;
													bool flag22 = DateTime.TryParse(text4, out dateTime3);
													if (flag22)
													{
														bool flag23 = dateTime > dateTime3;
														if (flag23)
														{
															test.ShiftToStartAt(numDaysDirection, dateTime2.Hour, dateTime2.Minute);
															break;
														}
													}
												}
												else
												{
													bool flag24 = text2.StartsWith("=");
													if (flag24)
													{
														string text4 = text2.Substring(1).Trim();
														bool flag25 = text4.Length <= 2;
														if (flag25)
														{
															text4 += ":00";
														}
														text4 = str + text4;
														DateTime dateTime3;
														bool flag26 = DateTime.TryParse(text4, out dateTime3);
														if (flag26)
														{
															bool flag27 = dateTime == dateTime3;
															if (flag27)
															{
																test.ShiftToStartAt(numDaysDirection, dateTime2.Hour, dateTime2.Minute);
																break;
															}
														}
													}
												}
											}
										}
									}
								}
							}
						}
					}
				}
				else
				{
					int cid = specialAccommodation.ControlId;
					Accommodation accommodation = accommodationsToUse.Find((Accommodation e) => e.Controlid == cid);
					bool flag28 = accommodation != null && !string.IsNullOrEmpty(specialAccommodation.ControlIdSpecificValue);
					if (flag28)
					{
						bool flag29 = string.IsNullOrEmpty(accommodation.LookupText) || accommodation.LookupText.IndexOf(specialAccommodation.ControlIdSpecificValue, StringComparison.OrdinalIgnoreCase) < 0 || (!string.IsNullOrEmpty(accommodation.SubText) && accommodation.SubText.IndexOf(specialAccommodation.ControlIdSpecificValue, StringComparison.OrdinalIgnoreCase) >= 0);
						if (flag29)
						{
							accommodation = null;
						}
					}
					bool flag30 = accommodation == null && specialAccommodation.SpecialAccommodationType == SpecialAccommodationType.Breaks;
					if (flag30)
					{
						int secondCid = specialAccommodation.GetArgInt("secondcid", 0);
						bool flag31 = secondCid > 0;
						if (flag31)
						{
							accommodation = accommodationsToUse.Find((Accommodation e) => e.Controlid == secondCid);
							bool flag32 = accommodation != null;
							if (flag32)
							{
								accommodation.LookupText = "";
								accommodation.Title = "";
							}
						}
					}
					bool flag33 = accommodation != null || specialAccommodation.SpecialAccommodationType == SpecialAccommodationType.StartEndOfDaySlide;
					if (flag33)
					{
						bool flag34 = accommodation != null;
						double num4;
						if (flag34)
						{
							num4 = Accommodation.ExtractNumber(accommodation.Title);
							bool flag35 = num4 <= 0.0;
							if (flag35)
							{
								num4 = Accommodation.ExtractNumber(accommodation.LookupText);
							}
						}
						else
						{
							num4 = 0.0;
						}
						bool flag36 = specialAccommodation.SpecialAccommodationType == SpecialAccommodationType.Extra_Time;
						if (flag36)
						{
							int num5 = Booker.CalculateExtraTime(classTest, test, accommodationsToUse, specialAccommodation, num4, accommodation);
							bool flag37 = num5 > 0;
							if (flag37)
							{
								int num6 = num5;
								string arg2 = specialAccommodation.GetArg("overridebookingnote", "Added extra time ({0})");
								bool flag38 = num6 > num2;
								if (flag38)
								{
									DateTime endDate = test.StartDate.AddMinutes((double)num6);
									num = num6;
									num2 = num;
									test.EndDate = endDate;
									string durationDescription = Booker.GetDurationDescription(num5 - classTest.Duration);
									privateNotes.Add(new PrivateNote(string.Format(arg2, durationDescription)));
									CWLogger.Logger.Trace("TESTBOOK:FindPotentials:ApplySpecialAccommodationRules:ExtraTime:pid={0}:lucid={1}:extra time amount={2}:extratimeminutes:{3}", new object[]
									{
										pid.ToString(),
										lucid.ToString(),
										num4.ToString(),
										num5.ToString()
									});
								}
							}
						}
						else
						{
							bool flag39 = specialAccommodation.SpecialAccommodationType == SpecialAccommodationType.Breaks;
							if (flag39)
							{
								int num7 = Booker.CalculateBreakTime(test, accommodationsToUse, test.Duration, specialAccommodation, num4, accommodation);
								bool flag40 = num7 > 0 && num7 > test.BreakTime;
								if (flag40)
								{
									string arg2 = specialAccommodation.GetArg("overridebookingnote", "Applied break time of {0} minutes");
									test.BreakTime = num7;
									privateNotes.Add(new PrivateNote(string.Format("Applied break time of {0} minutes", num7.ToString())));
								}
								if (debugMode)
								{
								}
							}
							else
							{
								bool flag41 = specialAccommodation.SpecialAccommodationType == SpecialAccommodationType.AddIcon;
								if (flag41)
								{
									int argInt = specialAccommodation.GetArgInt("iconnum", 0);
									bool flag42 = argInt > 0;
									if (flag42)
									{
										iconsToBookWith.Add(argInt);
									}
									if (debugMode)
									{
									}
								}
								else
								{
									bool flag43 = specialAccommodation.SpecialAccommodationType == SpecialAccommodationType.DaysRest;
									if (flag43)
									{
										Test test2 = Booker.CalculateDaysRest(pid, specialAccommodation, num4, test);
										bool flag44 = test2 != null && (test.StartDate != test2.StartDate || test.EndDate != test2.EndDate);
										if (flag44)
										{
											string arg2 = specialAccommodation.GetArg("overridebookingnote", "Changed day from {0} at {1} to {2} at {3} due to days-rest");
											privateNotes.Add(new PrivateNote(string.Format(arg2, new object[]
											{
												test.StartDate.ToString("yyyy-MM-dd"),
												test.StartDate.ToString("h:mm tt"),
												test2.StartDate.ToString("yyyy-MM-dd"),
												test2.StartDate.ToString("h:mm tt")
											})));
											test = test2;
										}
									}
									else
									{
										bool flag45 = specialAccommodation.SpecialAccommodationType == SpecialAccommodationType.MaxPerDay;
										if (flag45)
										{
											Test test3 = Booker.CalculateMaxPerDay(pid, specialAccommodation, num4, test);
											bool flag46 = test3 != null && (test.StartDate != test3.StartDate || test.EndDate != test3.EndDate);
											if (flag46)
											{
												string arg2 = specialAccommodation.GetArg("overridebookingnote", "Changed day from {0} at {1} to {2} at {3} due to max per day");
												privateNotes.Add(new PrivateNote(string.Format(arg2, new object[]
												{
													test.StartDate.ToString("yyyy-MM-dd"),
													test.StartDate.ToString("h:mm tt"),
													test3.StartDate.ToString("yyyy-MM-dd"),
													test3.StartDate.ToString("h:mm tt")
												})));
												test = test3;
											}
										}
										else
										{
											bool flag47 = specialAccommodation.SpecialAccommodationType == SpecialAccommodationType.CantBookOnline;
											if (flag47)
											{
												return null;
											}
											bool flag48 = specialAccommodation.SpecialAccommodationType == SpecialAccommodationType.EmailCoordinator;
											if (flag48)
											{
												bool flag49 = emailBodySb.Length > 0;
												if (flag49)
												{
													emailBodySb.Append(Environment.NewLine);
												}
												emailBodySb.Append("• ");
												emailBodySb.Append(accommodation.Title);
											}
											else
											{
												bool flag50 = specialAccommodation.SpecialAccommodationType == SpecialAccommodationType.TimeOfDay;
												if (flag50)
												{
													Test test4 = Booker.CalculateTimeOfDay(specialAccommodation, num4, test);
													bool flag51 = test4 != null && (test.StartDate != test4.StartDate || test.EndDate != test4.EndDate);
													if (flag51)
													{
														string arg2 = specialAccommodation.GetArg("overridebookingnote", "Moved from {0} at {1} to {2} at {3} due to time-of-day accommodation");
														bool flag52 = test.StartDate.Date != test4.StartDate.Date;
														if (flag52)
														{
															privateNotes.Add(new PrivateNote(string.Format(arg2, new object[]
															{
																test.StartDate.ToString("MMM d"),
																test.StartDate.ToString("h:mm tt"),
																test4.StartDate.ToString("MMM d"),
																test4.StartDate.ToString("h:mm tt")
															})));
														}
														else
														{
															privateNotes.Add(new PrivateNote(string.Format(arg2, new object[]
															{
																test.StartDate.ToString("MMM d"),
																test.StartDate.ToString("h:mm tt"),
																test4.StartDate.ToString("MMM d"),
																test4.StartDate.ToString("h:mm tt")
															})));
														}
														test = test4;
													}
												}
												else
												{
													bool flag53 = specialAccommodation.SpecialAccommodationType == SpecialAccommodationType.StartEndOfDaySlide;
													if (flag53)
													{
														int num8;
														Test test5 = Booker.CalculateStartOfDayOrEndOfDaySlide(specialAccommodation, test, classTest, out num8);
														bool flag54 = num8 < 0;
														if (flag54)
														{
															string arg2 = specialAccommodation.GetArg("overridebookingnotemoveforward", "Moved from {0} to {1} due to end of day.");
															privateNotes.Add(new PrivateNote(string.Format(arg2, test.StartDate.ToString("h:mm tt"), test5.StartDate.ToString("h:mm tt"))));
														}
														else
														{
															bool flag55 = num8 > 0;
															if (flag55)
															{
																string arg2 = specialAccommodation.GetArg("overridebookingnotemovebackward", "Moved from {0} to {1} due to start of day.");
																privateNotes.Add(new PrivateNote(string.Format(arg2, test.StartDate.ToString("h:mm tt"), test5.StartDate.ToString("h:mm tt"))));
															}
														}
														test = test5;
													}
												}
											}
										}
									}
								}
							}
						}
					}
				}
			}
			return test;
		}

		// Token: 0x0600030D RID: 781 RVA: 0x00015F24 File Offset: 0x00014124
		public static string GetDurationDescription(int tmins)
		{
			int num = (int)(Convert.ToDouble(tmins) / 60.0);
			int num2 = tmins - num * 60;
			string text = "";
			bool flag = num == 1;
			if (flag)
			{
				text = "1 hour";
				bool flag2 = num2 > 0;
				if (flag2)
				{
					text += "; ";
				}
			}
			else
			{
				bool flag3 = num > 1;
				if (flag3)
				{
					text = num.ToString() + " hours";
					bool flag4 = num2 > 0;
					if (flag4)
					{
						text += "; ";
					}
				}
			}
			bool flag5 = num2 == 1;
			if (flag5)
			{
				text += "1 minute";
			}
			else
			{
				bool flag6 = num2 > 1;
				if (flag6)
				{
					text = text + num2.ToString() + " minutes";
				}
			}
			return text;
		}

		// Token: 0x0600030E RID: 782 RVA: 0x00015FF0 File Offset: 0x000141F0
		public static Test CalculateStartOfDayOrEndOfDaySlide(SpecialAccommodation acc, Test targetTest, Test classTest, out int shiftedResult)
		{
			string arg = acc.GetArg("starttime", "");
			string arg2 = acc.GetArg("endtime", "");
			DateTime startDate = targetTest.StartDate;
			DateTime t = targetTest.EndDate;
			bool flag = targetTest.BreakTime > 0;
			if (flag)
			{
				t = t.AddMinutes((double)targetTest.BreakTime);
			}
			shiftedResult = 0;
			bool flag2 = !string.IsNullOrEmpty(arg);
			if (flag2)
			{
				DateTime dateTime;
				bool flag3 = DateTime.TryParse(string.Format("{0} {1}", targetTest.StartDate.ToString("yyyy-MM-dd"), arg), out dateTime);
				if (flag3)
				{
					bool flag4 = startDate < dateTime;
					if (flag4)
					{
						int duration = targetTest.Duration;
						Test test = new Test(dateTime, targetTest.EndDate, targetTest.Room);
						test.BreakTime = targetTest.BreakTime;
						test.EndDate = test.StartDate.AddMinutes((double)duration);
						shiftedResult = 1;
						CWLogger.Logger.Trace("Moved test forward to bring to start of day:newstart={0}:newend={1}", targetTest.StartDate.ToString("yyyy-MM-dd h:mm tt"), targetTest.EndDate.ToString("yyyy-MM-dd h:mm tt"));
						return test;
					}
				}
			}
			bool flag5 = !string.IsNullOrEmpty(arg2);
			if (flag5)
			{
				DateTime dateTime2;
				bool flag6 = DateTime.TryParse(string.Format("{0} {1}", targetTest.StartDate.ToString("yyyy-MM-dd"), arg2), out dateTime2);
				if (flag6)
				{
					bool flag7 = t > dateTime2;
					if (flag7)
					{
						int duration2 = targetTest.Duration;
						Test test2 = new Test(targetTest.StartDate, dateTime2, targetTest.Room);
						test2.BreakTime = targetTest.BreakTime;
						bool flag8 = test2.BreakTime > 0;
						if (flag8)
						{
							test2.EndDate = test2.EndDate.AddMinutes((double)(-(double)targetTest.BreakTime));
						}
						test2.StartDate = test2.EndDate.AddMinutes((double)(-(double)duration2));
						shiftedResult = -1;
						CWLogger.Logger.Trace("Moved test backward to bring to end of day:newstart={0}:newend={1}", targetTest.StartDate.ToString("yyyy-MM-dd h:mm tt"), targetTest.EndDate.ToString("yyyy-MM-dd h:mm tt"));
						return test2;
					}
				}
			}
			return targetTest;
		}

		// Token: 0x0600030F RID: 783 RVA: 0x00016248 File Offset: 0x00014448
		public static Test CalculateTimeOfDay(SpecialAccommodation acc, double num, Test targetTest)
		{
			string text = acc.GetArg("starttime", "");
			string text2 = acc.GetArg("endtime", "");
			string arg = acc.GetArg("pushtotomorrowtime", "");
			bool flag = !string.IsNullOrEmpty(arg);
			DateTime? dateTime;
			if (flag)
			{
				DateTime value;
				bool flag2 = !DateTime.TryParse(string.Format("{0} {1}", targetTest.StartDate.ToString("yyyy-MM-dd"), arg), out value);
				if (flag2)
				{
					dateTime = null;
				}
				else
				{
					dateTime = new DateTime?(value);
				}
			}
			else
			{
				dateTime = null;
			}
			string arg2 = acc.GetArg("pushtotomorrowtimeoverridestart", "");
			DateTime? dateTime2 = null;
			bool flag3 = !string.IsNullOrEmpty(arg2);
			if (flag3)
			{
				DateTime value2;
				bool flag4 = DateTime.TryParse(arg2, out value2);
				if (flag4)
				{
					dateTime2 = new DateTime?(value2);
				}
			}
			CWLogger.Logger.Trace("TESTBOOK:Booker:CalculateTimeOfDay:Info1:targetTest={0}:st={1}:et={2}:pushtotomorrow={3}:pushtotomorrowoverride={4}", new object[]
			{
				targetTest.ToStringDebug(),
				text,
				text2,
				arg,
				(dateTime2 == null) ? "NULL" : dateTime2.Value.ToString("yyyy-MM-dd")
			});
			bool flag5 = !string.IsNullOrEmpty(text);
			Test result;
			if (flag5)
			{
				DateTime minValue = DateTime.MinValue;
				text = string.Format("{0} {1}", targetTest.StartDate.ToString("yyyy-MM-dd"), text);
				text2 = ((!string.IsNullOrEmpty(text2)) ? string.Format("{0} {1}", targetTest.EndDate.ToString("yyyy-MM-dd"), text2) : "");
				DateTime dateTime3;
				bool flag6 = DateTime.TryParse(text, out dateTime3) && (string.IsNullOrEmpty(text2) || DateTime.TryParse(text2, out minValue));
				if (flag6)
				{
					bool flag7 = !string.IsNullOrEmpty(text2);
					bool flag8;
					if (flag7)
					{
						flag8 = (targetTest.StartDate < dateTime3 || targetTest.StartDate >= minValue);
					}
					else
					{
						flag8 = (targetTest.StartDate.Hour != dateTime3.Hour || targetTest.StartDate.Minute != dateTime3.Minute);
					}
					bool flag9 = flag8;
					if (flag9)
					{
						Test test = new Test(dateTime3, dateTime3.AddMinutes((double)targetTest.Duration), targetTest.Room);
						test.BreakTime = targetTest.BreakTime;
						bool flag10 = dateTime != null && targetTest.StartDate >= dateTime;
						if (flag10)
						{
							test.MoveToAnotherDay(1);
							bool flag11 = dateTime2 != null;
							if (flag11)
							{
								DateTime value3 = dateTime2.Value;
								int duration = test.Duration;
								test.StartDate = new DateTime(test.StartDate.Year, test.StartDate.Month, test.StartDate.Day, value3.Hour, value3.Minute, 0);
								test.EndDate = test.StartDate.AddMinutes((double)duration);
							}
						}
						else
						{
							bool flag12 = dateTime2 != null;
							if (flag12)
							{
								DateTime value4 = dateTime2.Value;
								int duration2 = test.Duration;
								test.StartDate = new DateTime(test.StartDate.Year, test.StartDate.Month, test.StartDate.Day, value4.Hour, value4.Minute, 0);
								test.EndDate = test.StartDate.AddMinutes((double)duration2);
							}
						}
						CWLogger.Logger.Debug("TESTBOOK:Booker:CalculateTimeOfDay:OutsideOfSpecifiedTimeRange=true:targetTest={0}:newTest={1}:st={2}:et={3}:pushtotomorrow={4}", new object[]
						{
							targetTest.ToStringDebug(),
							test.ToStringDebug(),
							text,
							text2,
							arg
						});
						result = test;
					}
					else
					{
						CWLogger.Logger.Debug("TESTBOOK:Booker:CalculateTimeOfDay:OutsideOfSpecifiedTimeRange=false:targetTest={0}:newTest={1}:st={2}:et={3}:pushtotomorrow={4}", new object[]
						{
							targetTest.ToStringDebug(),
							targetTest.ToStringDebug(),
							text,
							text2,
							arg
						});
						result = targetTest;
					}
				}
				else
				{
					result = targetTest;
				}
			}
			else
			{
				result = targetTest;
			}
			return result;
		}

		// Token: 0x06000310 RID: 784 RVA: 0x00016690 File Offset: 0x00014890
		public static Test CalculateDaysRest(int pid, SpecialAccommodation acc, double num, Test targetTest)
		{
			IWebSettingsClientManager webSettingsClientManager = new WebSettingsClientManager();
			string text = acc.GetArg("defaultfuturestarttime", "");
			int argInt = acc.GetArgInt("daysbetween", 0);
			DatabaseLayer clockWork = DatabaseLayerFactory.ClockWork;
			Test result;
			try
			{
				bool flag = argInt <= 0;
				int num2;
				if (flag)
				{
					num2 = Convert.ToInt32(num);
					bool flag2 = num2 < 1;
					if (flag2)
					{
						return targetTest;
					}
				}
				else
				{
					num2 = argInt;
				}
				int num3 = num2 * 2 + 1;
				DateTime dateTime = targetTest.StartDate.Date.AddDays((double)(-(double)num2));
				DateTime dateTime2 = dateTime.AddDays(30.0);
				DbParameter[] parameters = new DbParameter[]
				{
					clockWork.GetParameter("@startdate", DbType.DateTime, dateTime),
					clockWork.GetParameter("@enddate", DbType.DateTime, dateTime2),
					clockWork.GetParameter("@pid", DbType.Int32, pid)
				};
				string query = "SELECT DISTINCT a.appointmentid,a.startdate \r\nFROM    apps a \r\nWHERE   a.personid=@pid AND a.startdate>=@startdate \r\n        AND a.startdate<@enddate AND NOT a.examid IS NULL \r\n        AND a.cancelled=0 \r\nORDER BY a.startdate";
				DataTable dataTable = clockWork.ExecuteQuery(query, parameters);
				DateTime? dateTime3 = null;
				bool flag3 = dataTable.Rows.Count > 0;
				if (flag3)
				{
					int settingValue = webSettingsClientManager.GetSettingValue<int>(Setting.TESTBOOKING_OverrideRoomPidForAvailability);
					bool flag4 = settingValue < 1;
					string roomPids;
					if (flag4)
					{
						string settingValue2 = webSettingsClientManager.GetSettingValue<string>(Setting.TESTBOOKING_Assets);
						List<Asset> availableAssets = Asset.LoadAssets(settingValue2);
						string settingValue3 = webSettingsClientManager.GetSettingValue<string>(Setting.TESTBOOKING_Rooms);
						List<Room> list = Room.LoadRooms(settingValue3, availableAssets);
						roomPids = string.Join(",", list.ConvertAll<string>((Room rm) => rm.RoomId.ToString()).ToArray());
					}
					else
					{
						roomPids = settingValue.ToString();
					}
					int testBookingAvailabilityGroupId = 2;
					DateTime value = targetTest.StartDate.Date;
					int i = 0;
					while (i < 30)
					{
						bool flag5 = true;
						for (int j = -num2; j <= num2; j++)
						{
							DateTime dateTime4 = value.AddDays((double)j);
							bool flag6 = j < 0;
							if (!flag6)
							{
								bool flag7 = j > 0;
								if (flag7)
								{
									bool flag8 = dateTime4.DayOfWeek == DayOfWeek.Saturday;
									if (flag8)
									{
										dateTime4 = dateTime4.AddDays(1.0);
									}
									else
									{
										bool flag9 = dateTime4.DayOfWeek == DayOfWeek.Sunday;
										if (flag9)
										{
											dateTime4 = dateTime4.AddDays(2.0);
										}
									}
								}
							}
							DataRow[] array = dataTable.Select(string.Concat(new string[]
							{
								"startdate>='",
								dateTime4.ToString("yyyy-MM-dd"),
								"' AND startdate<'",
								dateTime4.AddDays(1.0).ToString("yyyy-MM-dd"),
								"'"
							}));
							bool flag10 = array.Length != 0;
							if (flag10)
							{
								flag5 = false;
								break;
							}
						}
						bool flag11 = flag5 && !Test.IsHoliday(testBookingAvailabilityGroupId, roomPids, value.Date);
						if (flag11)
						{
							dateTime3 = new DateTime?(value);
							break;
						}
						value = value.AddDays(1.0);
						i++;
						bool flag12 = value.DayOfWeek == DayOfWeek.Saturday;
						if (flag12)
						{
							value = value.AddDays(2.0);
						}
						else
						{
							bool flag13 = value.DayOfWeek == DayOfWeek.Sunday;
							if (flag13)
							{
								value = value.AddDays(1.0);
							}
						}
					}
				}
				else
				{
					dateTime3 = new DateTime?(targetTest.StartDate.Date);
				}
				bool flag14 = dateTime3 == null;
				if (flag14)
				{
					result = null;
				}
				else
				{
					bool flag15 = dateTime3.Value.Date.Equals(targetTest.StartDate.Date);
					if (flag15)
					{
						result = targetTest;
					}
					else
					{
						DateTime startDate = new DateTime(dateTime3.Value.Year, dateTime3.Value.Month, dateTime3.Value.Day, targetTest.StartDate.Hour, targetTest.StartDate.Minute, 0);
						bool flag16 = !string.IsNullOrEmpty(text);
						if (flag16)
						{
							text = string.Format("{0} {1}", "2001-01-01 ", text);
							DateTime dateTime5;
							bool flag17 = DateTime.TryParse(text, out dateTime5);
							if (flag17)
							{
								startDate = new DateTime(startDate.Year, startDate.Month, startDate.Day, dateTime5.Hour, dateTime5.Minute, 0);
							}
						}
						DateTime endDate = startDate.AddMinutes((targetTest.EndDate - targetTest.StartDate).TotalMinutes);
						result = new Test(startDate, endDate, targetTest.Room)
						{
							BreakTime = targetTest.BreakTime
						};
					}
				}
			}
			catch (Exception ex)
			{
				result = targetTest;
			}
			return result;
		}

		// Token: 0x06000311 RID: 785 RVA: 0x00016B88 File Offset: 0x00014D88
		public static Test CalculateMaxPerDay(int pid, SpecialAccommodation acc, double num, Test targetTest)
		{
			IWebSettingsClientManager webSettingsClientManager = new WebSettingsClientManager();
			DatabaseLayer clockWork = DatabaseLayerFactory.ClockWork;
			IEncryption encryption = clockWork.Encryption;
			int num2 = acc.GetArgInt("max", 0);
			string text = acc.GetArg("defaultfuturestarttime", "");
			bool flag = true;
			bool flag2 = num > 0.0;
			if (flag2)
			{
				num2 = Convert.ToInt32(num);
			}
			bool flag3 = num2 < 1;
			Test result;
			if (flag3)
			{
				result = targetTest;
			}
			else
			{
				DateTime dateTime = targetTest.StartDate.Date.AddDays(-1.0);
				DateTime dateTime2 = dateTime.AddDays(30.0);
				DbParameter[] parameters = new DbParameter[]
				{
					clockWork.GetParameter("@startdate", DbType.DateTime, dateTime),
					clockWork.GetParameter("@enddate", DbType.DateTime, dateTime2),
					clockWork.GetParameter("@pid", DbType.Int32, pid)
				};
				string query = "SELECT DISTINCT a.appointmentid,a.startdate \r\nFROM    apps a \r\nWHERE   a.personid=@pid AND a.startdate>=@startdate \r\n        AND a.startdate<@enddate AND NOT a.examid IS NULL \r\n        AND a.cancelled=0 \r\nORDER BY a.startdate";
				DataTable dataTable = clockWork.ExecuteQuery(query, parameters);
				DateTime? dateTime3 = null;
				bool flag4 = dataTable.Rows.Count > 0;
				if (flag4)
				{
					int settingValue = webSettingsClientManager.GetSettingValue<int>(Setting.TESTBOOKING_OverrideRoomPidForAvailability);
					bool flag5 = settingValue < 1;
					string roomPids;
					if (flag5)
					{
						string settingValue2 = webSettingsClientManager.GetSettingValue<string>(Setting.TESTBOOKING_Assets);
						List<Asset> availableAssets = Asset.LoadAssets(settingValue2);
						string settingValue3 = webSettingsClientManager.GetSettingValue<string>(Setting.TESTBOOKING_Rooms);
						List<Room> list = Room.LoadRooms(settingValue3, availableAssets);
						roomPids = string.Join(",", list.ConvertAll<string>((Room rm) => rm.RoomId.ToString()).ToArray());
					}
					else
					{
						roomPids = settingValue.ToString();
					}
					int testBookingAvailabilityGroupId = 2;
					for (int i = 0; i < 30; i++)
					{
						DateTime value = targetTest.StartDate.AddDays((double)i);
						bool flag6 = value.DayOfWeek != DayOfWeek.Saturday && value.DayOfWeek > DayOfWeek.Sunday;
						if (flag6)
						{
							DataRow[] array = dataTable.Select(string.Concat(new string[]
							{
								"startdate>='",
								value.ToString("yyyy-MM-dd"),
								"' AND startdate<'",
								value.AddDays(1.0).ToString("yyyy-MM-dd"),
								"'"
							}));
							bool flag7 = array.Length < num2 && !Test.IsHoliday(testBookingAvailabilityGroupId, roomPids, value.Date);
							if (flag7)
							{
								dateTime3 = new DateTime?(value);
								break;
							}
							bool flag8 = i == 0 && flag && array.Length == 1;
							if (flag8)
							{
								DataRow dataRow = array[0];
								DateTime dateTime4 = (DateTime)dataRow["startdate"];
								bool flag9 = dateTime4.Hour > 12 && targetTest.StartDate.Hour < 12;
								if (flag9)
								{
									CWLogger.Logger.Debug("TESTBOOK:Booker:CalculateMaxPerDay:SWAPTESTS:number={0}:defaultFutureStartTime={1}:targetTest={2}:founddate={3}:rowcount={4}:existingstartdatetime:{5}", new object[]
									{
										num2.ToString(),
										text,
										targetTest.ToStringDebug(),
										(dateTime3 == null) ? "NULL" : dateTime3.Value.ToString("yyyy-MM-dd H:mm"),
										dataTable.Rows.Count.ToString(),
										dateTime4.ToString("yyyy-MM-dd H:mm")
									});
									break;
								}
							}
						}
					}
					CWLogger.Logger.Debug("TESTBOOK:Booker:CalculateMaxPerDay:number={0}:defaultFutureStartTime={1}:targetTest={2}:founddate={3}:rowcount={4}", new object[]
					{
						num2.ToString(),
						text,
						targetTest.ToStringDebug(),
						(dateTime3 == null) ? "NULL" : dateTime3.Value.ToString("yyyy-MM-dd H:mm"),
						dataTable.Rows.Count.ToString()
					});
				}
				else
				{
					dateTime3 = new DateTime?(targetTest.StartDate.Date);
					CWLogger.Logger.Debug("TESTBOOK:Booker:CalculateMaxPerDay:StudentHadNoExistingTestBookings:number={0}:defaultFutureStartTime={1}:targetTest={2}:founddate={3}", new object[]
					{
						num2.ToString(),
						text,
						targetTest.ToStringDebug(),
						(dateTime3 == null) ? "NULL" : dateTime3.Value.ToString("yyyy-MM-dd H:mm")
					});
				}
				bool flag10 = dateTime3 == null;
				if (flag10)
				{
					result = null;
				}
				else
				{
					DateTime startDate = new DateTime(dateTime3.Value.Year, dateTime3.Value.Month, dateTime3.Value.Day, targetTest.StartDate.Hour, targetTest.StartDate.Minute, 0);
					bool flag11 = startDate.Date != targetTest.StartDate.Date && !string.IsNullOrEmpty(text);
					if (flag11)
					{
						text = string.Format("{0} {1}", "2001-01-01 ", text);
						DateTime dateTime5;
						bool flag12 = DateTime.TryParse(text, out dateTime5);
						if (flag12)
						{
							startDate = new DateTime(startDate.Year, startDate.Month, startDate.Day, dateTime5.Hour, dateTime5.Minute, 0);
							CWLogger.Logger.Debug("TESTBOOK:Booker:CalculateMaxPerDay:MoveToFutureStartTime:number={0}:defaultFutureStartTime={1}:targetTest={2}:founddate={3}", new object[]
							{
								num2.ToString(),
								text,
								targetTest.ToStringDebug(),
								startDate.ToString("yyyy-MM-dd H:mm")
							});
						}
					}
					DateTime endDate = startDate.AddMinutes((targetTest.EndDate - targetTest.StartDate).TotalMinutes);
					result = new Test(startDate, endDate, targetTest.Room)
					{
						BreakTime = targetTest.BreakTime
					};
				}
			}
			return result;
		}

		// Token: 0x06000312 RID: 786 RVA: 0x0001715C File Offset: 0x0001535C
		public static int CalculateExtraTime(Test classTest, Test targetTest, List<Accommodation> accommodationsToUse, SpecialAccommodation acc, double num, Accommodation studentAccommodation)
		{
			int duration = classTest.Duration;
			string arg = acc.GetArg("flattimetext", "");
			bool flag = false;
			bool flag2 = !string.IsNullOrEmpty(arg);
			if (flag2)
			{
				string text = studentAccommodation.Title + " * " + studentAccommodation.LookupText;
				bool flag3 = text.ToLower().IndexOf(arg.ToLower()) >= 0;
				if (flag3)
				{
					flag = true;
				}
			}
			bool flag4 = flag;
			int result;
			if (flag4)
			{
				result = classTest.Duration + Convert.ToInt32(num);
			}
			else
			{
				string arg2 = acc.GetArg("type", "0");
				double extraTimePercent = Accommodation.GetExtraTimePercent(num, arg2);
				bool flag5 = extraTimePercent > 0.0;
				if (flag5)
				{
					result = Accommodation.ApplyExtraTime(classTest.Duration, extraTimePercent);
				}
				else
				{
					result = 0;
				}
			}
			return result;
		}

		// Token: 0x06000313 RID: 787 RVA: 0x00017244 File Offset: 0x00015444
		public static int CalculateBreakTime(Test targetTest, List<Accommodation> accommodationsToUse, int classDurationInMinutes, SpecialAccommodation acc, double num, Accommodation studentAcc)
		{
			string arg = acc.GetArg("ignoreifcheckedcontrolid", "");
			int ignoreIfCheckedCid;
			bool flag = !string.IsNullOrEmpty(arg) && int.TryParse(arg, out ignoreIfCheckedCid);
			if (flag)
			{
				Accommodation accommodation = accommodationsToUse.Find((Accommodation atu) => atu.Controlid == ignoreIfCheckedCid);
				bool flag2 = accommodation != null;
				if (flag2)
				{
					string arg2 = acc.GetArg("ignoreifcheckedvalue", "");
					bool flag3 = string.IsNullOrEmpty(arg2);
					if (flag3)
					{
						return 0;
					}
					bool flag4 = accommodation.Title.ToLower().IndexOf(arg2.ToLower()) >= 0;
					if (flag4)
					{
						return 0;
					}
				}
			}
			int num2 = Convert.ToInt32(num);
			int argInt = acc.GetArgInt("mintesttime", 0);
			int argInt2 = acc.GetArgInt("max", 0);
			int argInt3 = acc.GetArgInt("type", 0);
			int secondCid = acc.GetArgInt("secondcid", 0);
			string text = acc.GetArg("flattimetext", "").ToLower();
			int argInt4 = acc.GetArgInt("flattimeamount", 0);
			bool flag5 = classDurationInMinutes < argInt;
			int result;
			if (flag5)
			{
				result = 0;
			}
			else
			{
				Accommodation.CalculateExtraTimeMethod calculateExtraTimeMethod = Accommodation.ParseExtraTimeMethod(argInt3.ToString());
				bool flag6 = calculateExtraTimeMethod == Accommodation.CalculateExtraTimeMethod.Guess;
				if (flag6)
				{
					calculateExtraTimeMethod = Accommodation.CalculateExtraTimeMethod.MinPerHour;
				}
				CWLogger.Logger.Trace("TESTBOOK:Booker:CalculateBreakTime:number={0}:mintesttime={1}:maxbreakminutes={2}:calcmethodstr={3}:flattimestring={4}:flattimeamount={5}:secondcid={6}", new object[]
				{
					num2.ToString(),
					argInt.ToString(),
					argInt2.ToString(),
					argInt3,
					text,
					argInt4.ToString(),
					secondCid.ToString()
				});
				int num3 = 0;
				switch (calculateExtraTimeMethod)
				{
				case Accommodation.CalculateExtraTimeMethod.MinPerHour:
				{
					double num4 = (double)num2 / 60.0;
					num3 = Convert.ToInt32((double)classDurationInMinutes * num4);
					break;
				}
				case Accommodation.CalculateExtraTimeMethod.Percentage_1_33:
				{
					double num4 = (double)num2 / 60.0;
					num3 = Convert.ToInt32((double)classDurationInMinutes * num4);
					break;
				}
				case Accommodation.CalculateExtraTimeMethod.Percentage_0_33:
				{
					double num4 = (double)num2 / 60.0;
					num3 = Convert.ToInt32((double)classDurationInMinutes * num4);
					break;
				}
				case Accommodation.CalculateExtraTimeMethod.Percentage_33_0:
				{
					double num4 = (double)num2 / 60.0;
					num3 = Convert.ToInt32((double)classDurationInMinutes * num4);
					break;
				}
				case Accommodation.CalculateExtraTimeMethod.FlatRate:
					num3 = num2;
					break;
				case Accommodation.CalculateExtraTimeMethod.MinPerHourInTwoControls:
				{
					bool flag7 = secondCid > 0;
					if (flag7)
					{
						Accommodation accommodation2 = accommodationsToUse.Find((Accommodation e) => e.Controlid == secondCid);
						bool flag8 = accommodation2 != null;
						if (flag8)
						{
							string text2 = accommodation2.Title + accommodation2.LookupText;
							int num5 = string.IsNullOrEmpty(text) ? -1 : text2.ToLower().IndexOf(text);
							bool flag9 = num5 >= 0;
							if (flag9)
							{
								num3 = argInt4;
								CWLogger.Logger.Trace("TESTBOOK:Booker:CalculateBreakTime:MinPerHourInTwoControls:FLATTIMEAMT:breaktimeminutes={0}", num3.ToString());
							}
							else
							{
								string value = Regex.Match(text2, "[0-9]+").Value;
								int num6;
								bool flag10 = int.TryParse(value, out num6);
								if (flag10)
								{
									bool flag11 = num2 > 0 && num6 > 0;
									if (flag11)
									{
										double num4 = Convert.ToDouble(num2) / Convert.ToDouble(num6);
										num3 = Convert.ToInt32((double)classDurationInMinutes * num4);
									}
								}
								else
								{
									CWLogger.Logger.Trace("TESTBOOK:Booker:CalculateBreakTime:MinPerHourInTwoControls:NotFlatTime:breaktimeminutes={0}:secondnumber={1}:number={2}", num3.ToString());
								}
							}
						}
						else
						{
							CWLogger.Logger.Trace("TESTBOOK:Booker:CalculateBreakTime:MinPerHourInTwoControls:secondcidnotfound:secondcid={0}", secondCid.ToString());
						}
					}
					break;
				}
				default:
					num3 = 0;
					break;
				}
				bool flag12 = argInt2 > 0 && num3 > argInt2;
				if (flag12)
				{
					num3 = argInt2;
				}
				DateTime dateTime = targetTest.StartDate.AddMinutes((double)num3);
				result = num3;
			}
			return result;
		}

		// Token: 0x06000314 RID: 788 RVA: 0x000175F8 File Offset: 0x000157F8
		private static DataTable LoadStudentTimetable(int pid, int lucid)
		{
			return new DataTable();
		}

		// Token: 0x06000315 RID: 789 RVA: 0x00017610 File Offset: 0x00015810
		private static DataTable LoadRoomSchedules(List<PotentialRoom> rooms, DateTime day)
		{
			DatabaseLayer clockWork = DatabaseLayerFactory.ClockWork;
			DbParameter[] parameters = new DbParameter[]
			{
				clockWork.GetParameter("@pids", DbType.String, AppSettingsV2.IntListToString(PotentialRoom.GetRoomPids(rooms))),
				clockWork.GetParameter("@sdate", DbType.DateTime, day),
				clockWork.GetParameter("@edate", DbType.DateTime, day.AddDays(1.0).AddMinutes(-1.0))
			};
			return clockWork.ExecuteQuery(QueryStorage.QS_Select_RoomSchedules, parameters);
		}

		// Token: 0x06000316 RID: 790 RVA: 0x000176A4 File Offset: 0x000158A4
		private static List<PotentialRoom> FigureOutRequiredRoomsInOrder(int pid, int lucid, List<Asset> assets, List<Room> availableRooms, List<Accommodation> accommodations)
		{
			List<PotentialRoom> list = new List<PotentialRoom>();
			foreach (Room room in availableRooms)
			{
				int score;
				bool flag = room.SupportsRequiredAssets(assets, out score);
				bool flag2 = flag;
				if (flag2)
				{
					list.Add(new PotentialRoom(room, score));
				}
				bool flag3 = flag;
				if (flag3)
				{
					CWLogger logger = CWLogger.Logger;
					string message = "Booker:FindPotentialBookings:FigureOutRequiredRooms:RoomAdded:room={0}:score={1}:requiredassets={2}";
					object arg = room.ToStringDebug();
					object arg2 = score.ToString();
					object arg3;
					if (assets != null)
					{
						arg3 = string.Join(" ** ", assets.ConvertAll<string>((Asset asset) => asset.ToStringDebug()).ToArray());
					}
					else
					{
						arg3 = "NULL";
					}
					logger.Trace(message, arg, arg2, arg3);
				}
				else
				{
					CWLogger logger2 = CWLogger.Logger;
					string message2 = "Booker:FindPotentialBookings:FigureOutRequiredRooms:RoomNOTAdded:room={0}:score={1}:requiredassets={2}";
					object arg4 = room.ToStringDebug();
					object arg5 = score.ToString();
					object arg6;
					if (assets != null)
					{
						arg6 = string.Join(" ** ", assets.ConvertAll<string>((Asset asset) => asset.ToStringDebug()).ToArray());
					}
					else
					{
						arg6 = "NULL";
					}
					logger2.Trace(message2, arg4, arg5, arg6);
				}
			}
			list.Sort(new Comparison<PotentialRoom>(Booker.SortPotentialRoomFunction));
			return list;
		}

		// Token: 0x06000317 RID: 791 RVA: 0x0001780C File Offset: 0x00015A0C
		private static int SortPotentialRoomFunction(PotentialRoom pr1, PotentialRoom pr2)
		{
			bool flag = pr1.Score == pr2.Score && pr1.Room.PriorityNumber == pr2.Room.PriorityNumber;
			int result;
			if (flag)
			{
				result = 0;
			}
			else
			{
				bool flag2 = pr1.Score < pr2.Score || pr1.Room.PriorityNumber < pr2.Room.PriorityNumber;
				if (flag2)
				{
					result = -1;
				}
				else
				{
					result = 1;
				}
			}
			return result;
		}

		// Token: 0x06000318 RID: 792 RVA: 0x00017884 File Offset: 0x00015A84
		private static List<Asset> FigureOutRequiredAssets(int pid, int lucid, List<Asset> availableAssets, List<Accommodation> accommodations)
		{
			List<Asset> list = new List<Asset>();
			using (List<Accommodation>.Enumerator enumerator = accommodations.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					Booker.<>c__DisplayClass18_0 CS$<>8__locals1 = new Booker.<>c__DisplayClass18_0();
					CS$<>8__locals1.acc = enumerator.Current;
					bool flag = false;
					int cLevel = CS$<>8__locals1.acc.Level;
					while (!flag && cLevel > 0)
					{
						foreach (Asset asset in availableAssets)
						{
							string accTitle = CS$<>8__locals1.acc.Title + ((CS$<>8__locals1.acc.SubText == null) ? "" : CS$<>8__locals1.acc.SubText);
							Accommodation accommodation = asset.AccommodationsSupported.Find((Accommodation am) => am.Controlid == CS$<>8__locals1.acc.Controlid && (string.IsNullOrEmpty(am.SubText) || (!string.IsNullOrEmpty(am.SubText) && accTitle.IndexOf(am.SubText, StringComparison.OrdinalIgnoreCase) >= 0)) && am.Level == cLevel);
							bool flag2 = accommodation != null;
							if (flag2)
							{
								flag = true;
								list.Add(asset);
							}
						}
						int cLevel2 = cLevel;
						cLevel = cLevel2 - 1;
					}
				}
			}
			return list;
		}

		// Token: 0x06000319 RID: 793 RVA: 0x00017A3C File Offset: 0x00015C3C
		private static DataTable LoadStudentSchedule(int pid, DateTime day)
		{
			return Booker.LoadStudentSchedule(pid, day, 0);
		}

		// Token: 0x0600031A RID: 794 RVA: 0x00017A58 File Offset: 0x00015C58
		private static DataTable LoadStudentSchedule(int pid, DateTime day, int appIdToIgnoreWhenCheckingStudentsSchedule)
		{
			DatabaseLayer clockWork = DatabaseLayerFactory.ClockWork;
			DbParameter[] parameters = new DbParameter[]
			{
				clockWork.GetParameter("@pids", DbType.String, pid.ToString()),
				clockWork.GetParameter("@sdate", DbType.DateTime, day),
				clockWork.GetParameter("@edate", DbType.DateTime, day.AddDays(1.0).AddMinutes(-1.0)),
				clockWork.GetParameter("@appid", DbType.Int32, appIdToIgnoreWhenCheckingStudentsSchedule)
			};
			return clockWork.ExecuteQuery(QueryStorage.QS_Select_StudentScheduleExceptAppointment, parameters);
		}

		// Token: 0x0600031B RID: 795 RVA: 0x00017B00 File Offset: 0x00015D00
		public static List<PotentialTest> ExtractBookingsToPresentToStudent(List<PotentialTest> bookings)
		{
			bookings.Sort();
			List<PotentialTest> list = new List<PotentialTest>(bookings.Count);
			PotentialTest potentialTest = null;
			foreach (PotentialTest potentialTest2 in bookings)
			{
				bool flag = potentialTest == null || !potentialTest.Test.SameTime(potentialTest2.Test);
				if (flag)
				{
					list.Add(potentialTest2);
					potentialTest = potentialTest2;
				}
			}
			return list;
		}
	}
}
