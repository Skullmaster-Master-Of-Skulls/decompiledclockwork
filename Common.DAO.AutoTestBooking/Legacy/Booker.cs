using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using ClockWorkLogger;
using Databases;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.AppointmentsTestBooking.AutoTestBookingHelper;

namespace TechnoPro.Common.DAO.AutoTestBooking.Legacy
{
	// Token: 0x02000004 RID: 4
	public class Booker
	{
		// Token: 0x0600000F RID: 15 RVA: 0x000024B8 File Offset: 0x000006B8
		private static DataTable LoadAvailability(IList<int> pids, IList<int> agids, DateTime sdate, DateTime edate, OperationContext opContext)
		{
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(eDatabaseConnectionStringName.ClockWork, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] array = new DbParameter[4];
			array[0] = databaseLayer.GetParameter("@pids", DbType.String, string.Join(",", pids.ToList<int>().ConvertAll<string>((int g) => g.ToString()).ToArray()));
			array[1] = databaseLayer.GetParameter("@agids", DbType.String, string.Join(",", agids.ToList<int>().ConvertAll<string>((int g) => g.ToString()).ToArray()));
			array[2] = databaseLayer.GetParameter("@sdate", DbType.DateTime, sdate);
			array[3] = databaseLayer.GetParameter("@edate", DbType.DateTime, edate);
			DbParameter[] parameters = array;
			return databaseLayer.ExecuteQuery("SELECT    a.personid,a.availabilitygroupid,a.availabilitydate,a.availability,-1 AS roomid \r\nFROM        availabilityschedule a \r\nWHERE       a.availabilitydate>=@sdate AND a.availabilitydate <=@edate AND a.personid IN (SELECT orderid AS personid FROM splitorderids(@pids,',')) AND a.availabilitygroupid IN (SELECT orderid AS availabilitygroupid FROM splitorderids( @agids, ',' ) ) \r\nORDER BY a.personid,a.availabilitydate,a.availabilitygroupid", parameters);
		}

		// Token: 0x06000010 RID: 16 RVA: 0x000025A9 File Offset: 0x000007A9
		private static DateTime FixDate(DateTime dateOnly)
		{
			return new DateTime(dateOnly.Year, dateOnly.Month, dateOnly.Day);
		}

		// Token: 0x06000011 RID: 17 RVA: 0x000025C8 File Offset: 0x000007C8
		private static AvailabilitySchedule GetAvailabilitySchedule(IList<int> pids, IList<int> availabilityGroupIds, DateTime startDate, DateTime endDate, OperationContext opContext)
		{
			DateTime sdate = Booker.FixDate(startDate.AddMinutes(1.0));
			DateTime edate = Booker.FixDate(endDate).AddMinutes(1439.0);
			return new AvailabilitySchedule(Booker.LoadAvailability(pids, availabilityGroupIds, sdate, edate, opContext), true, pids.ToList<int>(), availabilityGroupIds.ToList<int>());
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002620 File Offset: 0x00000820
		private static AvailabilitySchedule GetAvailabilitySchedule(int overridePid, IList<int> pids, IList<int> availabilityGroupIds, DateTime startDate, DateTime endDate, OperationContext opContext)
		{
			DateTime sdate = Booker.FixDate(startDate.AddMinutes(1.0));
			DateTime edate = Booker.FixDate(endDate).AddMinutes(1439.0);
			List<int> list = new List<int>(pids.Count + 1);
			for (int i = 0; i <= pids.Count; i++)
			{
				list.Add(0);
			}
			for (int j = 0; j < pids.Count; j++)
			{
				list[j] = pids[j];
			}
			list[list.Count - 1] = overridePid;
			DataTable dataTable = Booker.LoadAvailability(list, availabilityGroupIds, sdate, edate, opContext);
			DataTable dataTable2 = dataTable.Clone();
			foreach (object obj in dataTable.Rows)
			{
				DataRow dataRow = (DataRow)obj;
				if ((int)dataRow["personid"] == overridePid)
				{
					using (IEnumerator<int> enumerator2 = pids.GetEnumerator())
					{
						while (enumerator2.MoveNext())
						{
							int num = enumerator2.Current;
							dataTable2.ImportRow(dataRow);
							dataTable2.Rows[dataTable2.Rows.Count - 1]["personid"] = num;
						}
						continue;
					}
				}
				dataTable2.ImportRow(dataRow);
			}
			return new AvailabilitySchedule(new DataView(dataTable2)
			{
				Sort = "personid,availabilitydate,availabilitygroupid"
			}, true, pids.ToList<int>(), availabilityGroupIds.ToList<int>());
		}

		// Token: 0x06000013 RID: 19 RVA: 0x000027C8 File Offset: 0x000009C8
		private static void MoveTestToAnotherDay(Test test, int numDaysOffset, int overrideRoomAvailabilityPid, IList<Room> availableRooms, OperationContext opContext)
		{
			string roomPids;
			if (overrideRoomAvailabilityPid < 1)
			{
				roomPids = string.Join(",", availableRooms.ToList<Room>().ConvertAll<string>((Room rm) => rm.RoomId.ToString()).ToArray());
			}
			else
			{
				roomPids = overrideRoomAvailabilityPid.ToString();
			}
			int testBookingAvailabilityGroupId = 2;
			DatabaseLayerFactory.GetDatabaseLayer(eDatabaseConnectionStringName.ClockWork, (opContext != null) ? opContext.TenantId : null);
			DateTime date = test.StartDate.Date.AddDays((double)numDaysOffset);
			if (numDaysOffset < -1)
			{
				numDaysOffset = -1;
			}
			else if (numDaysOffset > 1)
			{
				numDaysOffset = 1;
			}
			else if (numDaysOffset == 0)
			{
				numDaysOffset = 1;
			}
			for (int i = 0; i < 15; i++)
			{
				if (date.DayOfWeek != DayOfWeek.Sunday && date.DayOfWeek != DayOfWeek.Saturday)
				{
					if (!Booker.IsHoliday(testBookingAvailabilityGroupId, roomPids, date, opContext))
					{
						test.StartDate = new DateTime(date.Year, date.Month, date.Day, test.StartDate.Hour, test.StartDate.Minute, 0);
					}
					test.EndDate = new DateTime(date.Year, date.Month, date.Day, test.EndDate.Hour, test.EndDate.Minute, 0);
					return;
				}
			}
			date = date.AddDays((double)numDaysOffset);
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002928 File Offset: 0x00000B28
		private static bool IsHoliday(int testBookingAvailabilityGroupId, string roomPids, DateTime date, OperationContext opContext)
		{
			string query = "SELECT personid FROM availabilityschedule WHERE availabilitydate=@dt \r\n        AND availabilitygroupid=@gid \r\n        AND \r\n        (\r\n            personid IN (SELECT orderid AS personid FROM splitorderids(@pids,','))\r\n            --OR roomid2 IN (SELECT orderid AS roomid2 FROM splitorderids(@pids,','))\r\n        )";
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(eDatabaseConnectionStringName.ClockWork, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@gid", DbType.Int32, testBookingAvailabilityGroupId),
				databaseLayer.GetParameter("@pids", DbType.String, roomPids),
				databaseLayer.GetParameter("@dt", DbType.DateTime, date.Date)
			};
			return databaseLayer.ExecuteQuery(query, parameters).Rows.Count < 1;
		}

		// Token: 0x06000015 RID: 21 RVA: 0x000029AC File Offset: 0x00000BAC
		public static int CalculateBreakTime(int classDurationInMinutes, IList<AccommodationBasic> accommodationsToUse, IList<SpecialAccommodation> allSpecialAccommodationRules)
		{
			IEnumerable<SpecialAccommodation> enumerable = from g in allSpecialAccommodationRules
			where g.SpecialAccommodationType == SpecialAccommodationType.Breaks
			select g;
			if (enumerable.Count<SpecialAccommodation>() < 1)
			{
				return 0;
			}
			List<Accommodation> accommodationsToUse2 = accommodationsToUse.ToList<AccommodationBasic>().ConvertAll<Accommodation>((AccommodationBasic g) => new Accommodation
			{
				ControlId = g.ControlId,
				LookupText = g.ControlCaptionAndValue,
				SubText = "",
				Title = ""
			});
			List<int> list = new List<int>();
			using (IEnumerator<SpecialAccommodation> enumerator = enumerable.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					SpecialAccommodation rule = enumerator.Current;
					IEnumerable<AccommodationBasic> enumerable2 = from g in accommodationsToUse
					where g.ControlId == rule.ControlId
					select g;
					if (enumerable2.Count<AccommodationBasic>() > 0)
					{
						foreach (AccommodationBasic accommodationBasic in enumerable2)
						{
							string s = accommodationBasic.ControlCaptionAndValue ?? "";
							DateTime date = DateTime.Now.Date;
							int num = Booker.CalculateBreakTime(new Test(date, date.AddMinutes((double)classDurationInMinutes), null), accommodationsToUse2, classDurationInMinutes, rule, Booker.ExtractNumberFromString(s));
							if (num > 0)
							{
								list.Add(num);
							}
						}
					}
				}
			}
			if (list.Count >= 1)
			{
				return list.Max((int f) => f);
			}
			return 0;
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00002B3C File Offset: 0x00000D3C
		private static int CalculateBreakTime(Test targetTest, IList<Accommodation> accommodationsToUse, int classDurationInMinutes, SpecialAccommodation acc, double num)
		{
			string arg = acc.GetArg("ignoreifcheckedcontrolid", "");
			int ignoreIfCheckedCid;
			if (!string.IsNullOrEmpty(arg) && int.TryParse(arg, out ignoreIfCheckedCid))
			{
				Accommodation accommodation = accommodationsToUse.FirstOrDefault((Accommodation atu) => atu.ControlId == ignoreIfCheckedCid);
				if (accommodation != null)
				{
					string arg2 = acc.GetArg("ignoreifcheckedvalue", "");
					if (string.IsNullOrEmpty(arg2))
					{
						return 0;
					}
					if (accommodation.Title.ToLower().IndexOf(arg2.ToLower()) >= 0)
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
			if (classDurationInMinutes < argInt)
			{
				return 0;
			}
			Accommodation.CalculateExtraTimeMethod calculateExtraTimeMethod = Accommodation.ParseExtraTimeMethod(argInt3.ToString());
			if (calculateExtraTimeMethod == Accommodation.CalculateExtraTimeMethod.Guess)
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
				if (secondCid > 0)
				{
					Accommodation accommodation2 = accommodationsToUse.FirstOrDefault((Accommodation e) => e.ControlId == secondCid);
					if (accommodation2 != null)
					{
						string text2 = accommodation2.Title + accommodation2.LookupText;
						int num5;
						if ((string.IsNullOrEmpty(text) ? -1 : text2.ToLower().IndexOf(text)) >= 0)
						{
							num3 = argInt4;
							CWLogger.Logger.Trace("TESTBOOK:Booker:CalculateBreakTime:MinPerHourInTwoControls:FLATTIMEAMT:breaktimeminutes={0}", num3.ToString());
						}
						else if (int.TryParse(Regex.Match(text2, "[0-9]+").Value, out num5))
						{
							if (num2 > 0 && num5 > 0)
							{
								double num4 = Convert.ToDouble(num2) / Convert.ToDouble(num5);
								num3 = Convert.ToInt32((double)classDurationInMinutes * num4);
							}
						}
						else
						{
							CWLogger.Logger.Trace("TESTBOOK:Booker:CalculateBreakTime:MinPerHourInTwoControls:NotFlatTime:breaktimeminutes={0}:secondnumber={1}:number={2}", num3.ToString());
						}
					}
					else
					{
						CWLogger.Logger.Trace("TESTBOOK:Booker:CalculateBreakTime:MinPerHourInTwoControls:secondcidnotfound:secondcid={0}", secondCid.ToString());
					}
				}
				break;
			default:
				num3 = 0;
				break;
			}
			if (argInt2 > 0 && num3 > argInt2)
			{
				num3 = argInt2;
			}
			targetTest.StartDate.AddMinutes((double)num3);
			return num3;
		}

		// Token: 0x06000017 RID: 23 RVA: 0x00002E64 File Offset: 0x00001064
		private static List<TimeTableItem> LoadTimetable(int pid, int lucidToExclude, DateTime classTestDate, OperationContext opContext)
		{
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(eDatabaseConnectionStringName.ClockWork, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@lucid", DbType.Int32, lucidToExclude),
				databaseLayer.GetParameter("@pid", DbType.Int32, pid),
				databaseLayer.GetParameter("@targetdate", DbType.DateTime, classTestDate)
			};
			DataTable dataTable = databaseLayer.ExecuteQuery("SELECT  luc.StartDate,luc.EndDate,t.*\r\nFROM Courses c LEFT JOIN LUCourses luc ON luc.LUCourseID=c.luCourseID \r\n\t\tLEFT JOIN timetable t ON t.lucourseid=c.luCourseID \r\nWHERE\tc.personID=@pid \r\n\t\tAND NOT c.lucourseid=@lucid \r\n\t\tAND (c.registrationstatus IS NULL OR NOT c.registrationstatus=2)\r\n\t\tAND @targetdate >= luc.startdate AND @targetdate <= luc.enddate \r\n\t\tAND NOT t.timetableid IS NULL", parameters);
			List<TimeTableItem> list = new List<TimeTableItem>();
			foreach (object obj in dataTable.Rows)
			{
				List<TimeTableItem> timetableItems = TimeTableItem.GetTimetableItems((DataRow)obj);
				if (timetableItems.Count > 0)
				{
					list.AddRange(timetableItems);
				}
			}
			return list;
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00002F40 File Offset: 0x00001140
		public static IList<string> DoDebugCheckOnSettings(FindPotentialBookingsReq req)
		{
			List<string> list = new List<string>();
			if (req.Accommodations == null || req.Accommodations.Count < 1)
			{
				list.Add("Warning: Empty accommodations list.");
			}
			DateTime startDate = req.ClassTest.StartDate;
			DateTime endDate = req.ClassTest.EndDate;
			if (endDate <= startDate)
			{
				list.Add("Error: Class end time is smaller than class start time: " + startDate.ToString("yyyy-MM-dd h:mm tt") + " to " + endDate.ToString("yyyy-MM-dd h:mm tt"));
			}
			double totalMinutes = startDate.TimeOfDay.TotalMinutes;
			if (totalMinutes <= 360.0)
			{
				list.Add("Warning: Class start time is very early: " + startDate.ToString("h:mm tt"));
			}
			else if (totalMinutes >= 1320.0)
			{
				list.Add("Warning: Class start time is very late: " + startDate.ToString("h:mm tt"));
			}
			else if ((endDate - startDate).TotalMinutes >= 300.0)
			{
				list.Add("Class test duration is very long: " + startDate.ToString("yyyy-MM-dd h:mm tt") + " to " + endDate.ToString("yyyy-MM-dd h:mm tt"));
			}
			if (req.AvailableRooms0 == null || req.AvailableRooms0.Count < 1)
			{
				list.Add("Warning: Empty rooms list");
			}
			else
			{
				if (req.AvailableRooms0.FirstOrDefault((Room g) => g.RoomType == RoomType.VirtualRoom) == null)
				{
					list.Add("Warning: There are no virtual rooms available.");
				}
				int score;
				List<Asset> list2 = (from g in req.AvailableAssets
				where req.AvailableRooms0.FirstOrDefault((Room h) => h.SupportsRequiredAssets(new List<Asset>
				{
					g
				}, out score)) == null
				select g).ToList<Asset>();
				if (list2.Count > 0)
				{
					list.Add("Warning: One or more assets do not have rooms that support them (if a student has an accommodation for this asset there will never be a room found): " + string.Join(",", list2.ConvertAll<string>((Asset g) => g.Title ?? "").ToArray()));
				}
			}
			return list;
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00003174 File Offset: 0x00001374
		public static FindPotentialBookingsResp FindPotentialTestBookings(FindPotentialBookingsReq req, OperationContext opContext)
		{
			bool debugMode = req.DebugMode;
			IList<TestRule> rules = req.Rules;
			IList<string> list2;
			if (!debugMode)
			{
				IList<string> list = new List<string>();
				list2 = list;
			}
			else
			{
				list2 = Booker.DoDebugCheckOnSettings(req);
			}
			IList<string> list3 = list2;
			string campus = Booker.LoadCampus(req.Lucid, opContext);
			BookingResults bookingResults = new BookingResults();
			List<Room> availableRooms = (string.IsNullOrEmpty(campus) || !req.RestrictByCampus) ? new List<Room>(req.AvailableRooms0) : req.AvailableRooms0.ToList<Room>().FindAll((Room eg) => eg.SupportsCampus(campus));
			FindPotentialBookingInfo pbookingInfo = new FindPotentialBookingInfo(req.DebugMode, req.Pid, req.Lucid, req.DayToLookIn, req.ClassTest, req.Accommodations, req.AvailableAssets, availableRooms, req.SpecialAccommodations);
			CustomTestBookingRulesClass customTestBookingRules = req.CustomTestBookingRules;
			int num = 0;
			IList<PotentialTest> list4 = new List<PotentialTest>();
			int item = 2;
			int num2 = 1;
			IList<int> list5 = new List<int>();
			StringBuilder stringBuilder = new StringBuilder();
			int num3 = 0;
			IList<PrivateNote> list6 = new List<PrivateNote>();
			Test test;
			if (req.ApplySpecialAccommodationRules)
			{
				test = Booker.ApplySpecialAccommodationRules(debugMode, req.Pid, req.Lucid, req.SpecialAccommodations, req.ClassTest, req.Accommodations, out list6, out stringBuilder, out list5, req.AppIdToIgnoreWhenCheckingStudentsSchedule, req.OverrideRoomAvailabilityPid, availableRooms, req.IgnoreStudentsSchedule, req.IgnoreStudentAppointmentIds, opContext);
				if (test != null && test.BreakTime > 0)
				{
					test.EndDate = test.EndDate.AddMinutes((double)test.BreakTime);
				}
			}
			else
			{
				test = req.ClassTest;
			}
			int duration = test.Duration;
			num3 = test.BreakTime;
			DateTime dateTime = new DateTime(test.StartDate.Year, test.StartDate.Month, test.StartDate.Day);
			List<TimeTableItem> list7 = req.IgnoreTimetable ? new List<TimeTableItem>() : Booker.LoadTimetable(req.Pid, req.Lucid, req.ClassTest.StartDate.Date, opContext);
			DataTable dataTable = req.IgnoreStudentsSchedule ? new DataTable("t") : Booker.LoadStudentSchedule(req.Pid, dateTime, req.AppIdToIgnoreWhenCheckingStudentsSchedule, opContext);
			if (req.IgnoreStudentAppointmentIds != null && req.IgnoreStudentAppointmentIds.Count > 0)
			{
				List<DataRow> list8 = new List<DataRow>();
				foreach (int num4 in req.IgnoreStudentAppointmentIds)
				{
					foreach (DataRow item2 in dataTable.Select("appointmentid=" + num4.ToString()))
					{
						list8.Add(item2);
					}
				}
				StringBuilder stringBuilder2 = new StringBuilder();
				foreach (DataRow dataRow in list8)
				{
					stringBuilder2.AppendFormat("{0},", dataRow["appointmentid"].ToString());
					dataTable.Rows.Remove(dataRow);
				}
				if (list8.Count > 0 && CWLogger.Logger.IsDebugEnabled)
				{
					CWLogger.Logger.Debug("TESTBOOK:FindPotentials:RemoveAppointmentsFromStudentsSchedule:pid={0}:lucid={1}:appidsRemoved={2}", req.Pid.ToString(), req.Lucid.ToString(), stringBuilder2.ToString());
				}
			}
			List<DateRange> list9 = DateRange.FromTable(dataTable);
			if (list7.Count < 1)
			{
				if (debugMode && !req.IgnoreTimetable)
				{
					list3.Add("Warning: ignoreTimetable is false and the current course has no timetable in the system.  Timetable cannot be checked.");
				}
				if (CWLogger.Logger.IsDebugEnabled)
				{
					CWLogger.Logger.Debug("TESTBOOK:FindPotentials:MissingTimetables:pid={0}:lucid={1}", req.Pid.ToString(), req.Lucid.ToString());
				}
			}
			int num5 = Asset.GetMaxAccommodationLevel(req.AvailableAssets, req.Accommodations);
			if (num5 < 1)
			{
				num5 = 1;
			}
			IList<int> iconIds = new List<int>();
			string emailBody = "";
			StringBuilder stringBuilder3 = new StringBuilder();
			DateTime baseDate = new DateTime(2000, 1, 1);
			CWLogger.Logger.Debug("TESTBOOK:FindPotentials:Entry:pid={0}:lucid={1}:studentschedule={2}:othercoursestimetable={3}", new object[]
			{
				req.Pid.ToString(),
				req.Lucid.ToString(),
				stringBuilder3.ToString(),
				string.Join(", ", list7.ConvertAll<string>((TimeTableItem tti) => string.Format("{0}-{1} [{2} to {3}]", new object[]
				{
					tti.LuCourseId.ToString(),
					tti.DayOfWeek.ToString(),
					baseDate.AddMinutes((double)tti.StartMinutes).ToString("H:mm"),
					baseDate.AddMinutes((double)tti.EndMinutes).ToString("H:mm")
				})).ToArray())
			});
			List<Accommodation>[] array2 = new List<Accommodation>[num5];
			int i;
			int i2;
			for (i = num5 - 1; i >= 0; i = i2 - 1)
			{
				array2[i] = new List<Accommodation>();
				using (IEnumerator<Accommodation> enumerator3 = req.Accommodations.GetEnumerator())
				{
					while (enumerator3.MoveNext())
					{
						Accommodation acc = enumerator3.Current;
						Func<Accommodation, bool> <>9__6;
						if (i == 0 || req.AvailableAssets.FirstOrDefault(delegate(Asset aa)
						{
							IEnumerable<Accommodation> accommodationsSupported = aa.AccommodationsSupported;
							Func<Accommodation, bool> predicate;
							if ((predicate = <>9__6) == null)
							{
								predicate = (<>9__6 = ((Accommodation accs) => accs.ControlId == acc.ControlId && accs.Level == i + 1));
							}
							return accommodationsSupported.FirstOrDefault(predicate) != null;
						}) != null)
						{
							array2[i].Add(new Accommodation(acc.ControlId, acc.Title, acc.LookupText, i + 1));
						}
					}
				}
				i2 = i;
			}
			List<List<Accommodation>> list10 = new List<List<Accommodation>>();
			for (int j = 0; j < array2.Length; j++)
			{
				if (j == 0)
				{
					List<Accommodation> list11 = new List<Accommodation>();
					foreach (Accommodation accommodation in array2[j])
					{
						list11.Add(new Accommodation(accommodation.ControlId, accommodation.Title, accommodation.LookupText, accommodation.SubText, 1));
					}
					list10.Add(list11);
				}
				else
				{
					for (int k = 0; k < array2[j].Count; k++)
					{
						List<List<Accommodation>> list12 = new List<List<Accommodation>>();
						foreach (List<Accommodation> item3 in list10)
						{
							list12.Add(item3);
						}
						Accommodation accommodation2 = array2[j][k];
						foreach (List<Accommodation> list13 in list12)
						{
							List<Accommodation> list14 = new List<Accommodation>();
							foreach (Accommodation accommodation3 in list13)
							{
								Accommodation accommodation4 = new Accommodation(accommodation3.ControlId, accommodation3.Title, accommodation3.LookupText, accommodation3.SubText, accommodation3.Level);
								if (accommodation4.ControlId == accommodation2.ControlId)
								{
									accommodation4.Level = j + 1;
								}
								list14.Add(accommodation4);
							}
							list10.Add(list14);
						}
					}
				}
			}
			try
			{
				if (CWLogger.Logger.IsDebugEnabled)
				{
					CWLogger.Logger.Debug("FindPotentialTestBookings:LogAccommodationCombos:combos={0}", string.Join("\r\n • ", list10.ConvertAll<string>((List<Accommodation> a2) => string.Join(", ", a2.ConvertAll<string>((Accommodation attt) => string.Format("cid={0}; level={1}", attt.ControlId.ToString(), attt.Level.ToString())).ToArray())).ToArray()));
				}
			}
			catch
			{
			}
			List<List<Asset>> list15 = new List<List<Asset>>();
			foreach (List<Accommodation> list16 in list10)
			{
				if (list16.Count > 0)
				{
					List<Asset> item4 = Booker.FigureOutRequiredAssets(req.Pid, req.Lucid, req.AvailableAssets, list16);
					list15.Add(item4);
				}
			}
			if (list15.Count < 1)
			{
				list15.Add(new List<Asset>());
			}
			if (CWLogger.Logger.IsDebugEnabled)
			{
				CWLogger.Logger.Debug("FindPotentialTestBookings:LogAssetLevelsList:assetlevels={0}", string.Join("\r\n • ", list15.ConvertAll<string>((List<Asset> a2) => string.Join(", ", a2.ConvertAll<string>((Asset attt) => attt.AssetId).ToArray())).ToArray()));
			}
			int count = list15.Count;
			for (int l = 0; l < count; l++)
			{
				IList<Asset> list17 = list15[l];
				IList<PotentialRoom> list18 = Booker.FigureOutRequiredRoomsInOrder(req.Pid, req.Lucid, list17, availableRooms, req.Accommodations);
				if (count > 0 && l < count - 1)
				{
					list18 = (from rr in list18
					where rr.Room.RoomType == RoomType.RegularRoom
					select rr).ToList<PotentialRoom>();
				}
				AvailabilitySchedule roomAvailability;
				if (req.OverrideRoomAvailabilityPid > 0)
				{
					IList<int> roomPids = PotentialRoom.GetRoomPids(list18);
					roomAvailability = Booker.GetAvailabilitySchedule(req.OverrideRoomAvailabilityPid, roomPids, new List<int>
					{
						item
					}, dateTime, dateTime.AddDays(1.0).AddMinutes(-1.0), opContext);
				}
				else
				{
					roomAvailability = Booker.GetAvailabilitySchedule(PotentialRoom.GetRoomPids(list18), new List<int>
					{
						item
					}, dateTime, dateTime.AddDays(1.0).AddMinutes(-1.0), opContext);
				}
				if (debugMode)
				{
					if (list18.Count < 1)
					{
						list3.Add("No matching rooms were found");
					}
					else
					{
						foreach (PotentialRoom potentialRoom in from g in list18
						where !roomAvailability.Pids.Contains(g.Room.RoomId) || roomAvailability.Ranges == null || roomAvailability.Ranges.FirstOrDefault((AvailabilityScheduleRange h) => h.Pid == g.Room.RoomId) == null
						select g)
						{
							list3.Add("Room has no availability: " + (potentialRoom.Room.Title ?? "") + "-" + potentialRoom.Room.RoomId.ToString());
						}
					}
				}
				string text;
				if (!debugMode && !CWLogger.Logger.IsTraceEnabled)
				{
					text = "";
				}
				else
				{
					text = string.Join(", ", roomAvailability.Ranges.ConvertAll<string>((AvailabilityScheduleRange r) => r.ToStringDebug()).ToArray());
				}
				string argument = text;
				if (CWLogger.Logger.IsTraceEnabled)
				{
					CWLogger.Logger.Trace("TESTBOOK:FindPotentials:LoadRoomAvailability:RoomsWithAvailability={0}", argument);
				}
				foreach (AvailabilityScheduleRange availabilityScheduleRange in roomAvailability.Ranges)
				{
					foreach (PotentialRoom potentialRoom2 in list18)
					{
						if (potentialRoom2.Room.RoomId == availabilityScheduleRange.Pid)
						{
							if (potentialRoom2.AvailabilityStartTimeForTheDay == DateTime.MinValue || availabilityScheduleRange.Start < potentialRoom2.AvailabilityStartTimeForTheDay)
							{
								potentialRoom2.AvailabilityStartTimeForTheDay = availabilityScheduleRange.Start;
							}
							if (potentialRoom2.AvailabilityEndTimeForTheDay == DateTime.MinValue || availabilityScheduleRange.End > potentialRoom2.AvailabilityEndTimeForTheDay)
							{
								potentialRoom2.AvailabilityEndTimeForTheDay = availabilityScheduleRange.End;
							}
						}
					}
				}
				DataTable t;
				if (req.LoadRoomSchedules)
				{
					t = Booker.LoadRoomSchedules(list18, dateTime, req.AppIdToIgnoreWhenCheckingStudentsSchedule, opContext);
				}
				else
				{
					t = new DataTable("t");
				}
				List<DateRange> list19 = DateRange.FromTable(t);
				if (req.UnavailableRoomBookings != null)
				{
					using (IEnumerator<PotentialRoom> enumerator6 = list18.GetEnumerator())
					{
						while (enumerator6.MoveNext())
						{
							PotentialRoom proom = enumerator6.Current;
							foreach (Booking booking in from e in req.UnavailableRoomBookings
							where e.Pid == proom.Room.RoomId
							select e)
							{
								list19.Add(new DateRange(booking.Pid, booking.StartDateTime, booking.EndDateTime));
							}
						}
					}
				}
				CWLogger logger = CWLogger.Logger;
				string message = "TESTBOOK:FindPotentials:PreCheck:pid={0}:lucid={1}:AssetsReqd={2}:RoomsMatchingAssets:{3}";
				object[] array3 = new object[4];
				array3[0] = req.Pid.ToString();
				array3[1] = req.Lucid.ToString();
				array3[2] = string.Join(", ", list17.ToList<Asset>().ConvertAll<string>((Asset asst) => asst.ToStringDebug()).ToArray());
				array3[3] = string.Join(", ", list18.ToList<PotentialRoom>().ConvertAll<string>(delegate(PotentialRoom rm)
				{
					if (rm.Room != null)
					{
						return rm.Room.ToStringDebug();
					}
					return "NULL";
				}).ToArray());
				logger.Debug(message, array3);
				emailBody = stringBuilder.ToString();
				iconIds = list5;
				if (customTestBookingRules != null)
				{
					Exception ex;
					customTestBookingRules.FindPotentialBookingsStart(ref list4, ref rules, ref list17, ref list18, pbookingInfo, out ex, Array.Empty<object>());
				}
				int num6 = 0;
				foreach (TestRule testRule in req.Rules)
				{
					num6++;
					DateTime dateTime2 = new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, test.StartDate.Hour, test.StartDate.Minute, 0);
					DateTime dateTime3 = dateTime2.AddMinutes((double)duration);
					IList<DateRange> list20 = new List<DateRange>();
					int num7 = 15;
					int num8 = 1;
					if (testRule.EnforceOverlapWithClassTime)
					{
						num8 = 5;
					}
					if (num8 > num2)
					{
						num2 = num8;
					}
					if (debugMode && !testRule.IncludeNonVirtualRooms && !testRule.IncludeVirtualRooms)
					{
						list3.Add("Test rule ignores virtual and non-virtual rooms so no rooms can be matched.  Rule#" + num6.ToString());
					}
					if (testRule.EnforceOverlapWithClassTime)
					{
						DateTime startDate = req.ClassTest.StartDate;
						DateTime dateTime4 = startDate.AddMinutes((double)duration);
						while (dateTime4 >= req.ClassTest.EndDate)
						{
							list20.Add(new DateRange(startDate, dateTime4));
							startDate = startDate.AddMinutes((double)(-(double)num7));
							dateTime4 = dateTime4.AddMinutes((double)(-(double)num7));
						}
					}
					else
					{
						for (int m = 0; m <= testRule.MinutesPost; m += num7)
						{
							list20.Add(new DateRange(dateTime2.AddMinutes((double)m), dateTime3.AddMinutes((double)m)));
						}
						for (int n = 0; n <= testRule.MinutesPre; n += num7)
						{
							list20.Add(new DateRange(dateTime2.AddMinutes((double)(-(double)n)), dateTime3.AddMinutes((double)(-(double)n))));
						}
						if (testRule.MinutesPost > 0 && testRule.MinutesPre > 0 && num2 <= 1)
						{
							num2 = 10;
						}
						else if ((testRule.MinutesPost > 0 || testRule.MinutesPre > 0) && num2 <= 1)
						{
							num2 = 5;
						}
					}
					List<PotentialTest> list21 = new List<PotentialTest>();
					IList<PotentialTest> list22 = null;
					if (customTestBookingRules != null)
					{
						Exception ex;
						customTestBookingRules.FindPotentialBookingsMid('a', ref list20, testRule, ref list22, ref list4, ref rules, ref list17, ref list18, pbookingInfo, out ex, Array.Empty<object>());
					}
					List<PotentialRoom> list23 = new List<PotentialRoom>();
					foreach (PotentialRoom potentialRoom3 in list18)
					{
						bool flag = potentialRoom3.Room.RoomType == RoomType.VirtualRoom || potentialRoom3.Room.RoomType == RoomType.SuperVirtualRoom;
						PotentialRoom potentialRoom4;
						if (testRule.IncludeNonVirtualRooms && !flag)
						{
							potentialRoom4 = potentialRoom3;
						}
						else if (testRule.IncludeVirtualRooms && flag)
						{
							potentialRoom4 = potentialRoom3;
						}
						else
						{
							potentialRoom4 = null;
						}
						if (potentialRoom4 != null && !testRule.RoomIdsToExclud.Contains(potentialRoom4.Room.RoomId))
						{
							list23.Add(potentialRoom4);
						}
					}
					CWLogger logger2 = CWLogger.Logger;
					string message2 = "TESTBOOK:FindPotentials:EvaluateRule:RuleCounter={0}:pid={1}:lucid={2}:overlapclasstime={3}:NonVirtualRooms={4}:VirtualRooms={5}:TimesToInvestigate={6}:Rooms={7}:currentlevel={8}";
					object[] array4 = new object[9];
					array4[0] = num6.ToString();
					array4[1] = req.Pid.ToString();
					array4[2] = req.Lucid.ToString();
					array4[3] = testRule.EnforceOverlapWithClassTime.ToString();
					array4[4] = testRule.IncludeNonVirtualRooms.ToString();
					array4[5] = testRule.IncludeVirtualRooms.ToString();
					array4[6] = string.Join(", ", list20.ToList<DateRange>().ConvertAll<string>((DateRange dr) => string.Format("{0} to {1}", dr.StartDate.ToString("yyyy-MM-dd H:mm"), dr.EndDate.ToString("H:mm"))).ToArray());
					array4[7] = string.Join(", ", list23.ConvertAll<string>((PotentialRoom rm) => string.Format("{0}-{1} . AvailabilityMinMax={2} to {3}", new object[]
					{
						(rm.Room == null) ? "NULL" : rm.Room.Title,
						(rm.Room == null) ? "NULL" : rm.Room.RoomId.ToString(),
						rm.AvailabilityStartTimeForTheDay.ToString("H:mm"),
						rm.AvailabilityEndTimeForTheDay.ToString("H:mm")
					})).ToArray());
					array4[8] = l.ToString();
					logger2.Debug(message2, array4);
					foreach (PotentialRoom potentialRoom5 in list23)
					{
						Room room = potentialRoom5.Room;
						bool isVirtualRoom = potentialRoom5.Room.IsVirtualRoom;
						IList<PotentialTest> list24 = new List<PotentialTest>();
						if (isVirtualRoom)
						{
							using (IEnumerator<DateRange> enumerator11 = list20.GetEnumerator())
							{
								while (enumerator11.MoveNext())
								{
									DateRange dateRange = enumerator11.Current;
									DateTime sd = dateRange.StartDate;
									DateTime ed = dateRange.EndDate;
									bool flag2 = potentialRoom5.IsAvailableByStartAndEndofDayAvailabilityTimes(sd, ed);
									bool flag3 = !TimeTableItem.Overlaps(list7, sd, ed);
									if (flag3)
									{
										bookingResults.FailedTimetableCheck = new bool?(false);
									}
									else if (bookingResults.FailedTimetableCheck == null)
									{
										bookingResults.FailedTimetableCheck = new bool?(true);
									}
									bool flag4 = list9.Exists((DateRange ss) => ss.Intersects(sd, ed));
									if (!flag4)
									{
										bookingResults.StudentIsDoubleBooked = new bool?(false);
									}
									else if (bookingResults.StudentIsDoubleBooked == null)
									{
										bookingResults.StudentIsDoubleBooked = new bool?(true);
									}
									if (flag3 && !flag4)
									{
										if (flag2)
										{
											PotentialTest potentialTest = new PotentialTest(num++, sd, ed, room, true);
											potentialTest.AddMethodFoundNote("Virtual room - ok to double book");
											potentialTest.AddMethodFoundNote("Passed room availability & timetable checks");
											potentialTest.AddMethodFoundNote("Rule #{0} of {1} [{2}]", new string[]
											{
												num6.ToString(),
												req.Rules.Count.ToString(),
												testRule.ToString()
											});
											list24.Add(potentialTest);
										}
										else if (testRule.ShiftTimeToMatchEndOfDay)
										{
											if (potentialRoom5.AvailabilityEndTimeForTheDay != DateTime.MinValue && potentialRoom5.AvailabilityEndTimeForTheDay < ed)
											{
												DateTime[] array5 = new DateTime[]
												{
													potentialRoom5.AvailabilityEndTimeForTheDay,
													ed
												};
												TimeSpan timeSpan = array5[1] - array5[0];
												DateTime dateTime5 = sd.AddMinutes(-timeSpan.TotalMinutes);
												DateTime dateTime6 = ed.AddMinutes(-timeSpan.TotalMinutes);
												if (potentialRoom5.IsAvailableByStartAndEndofDayAvailabilityTimes(dateTime5, dateTime6))
												{
													PotentialTest potentialTest2 = new PotentialTest(num++, dateTime5, dateTime6, room, true);
													potentialTest2.AddMethodFoundNote("Virtual room - ok to double book");
													potentialTest2.AddMethodFoundNote("Passed room availability & timetable checks");
													potentialTest2.AddMethodFoundNote("Rule #{0} of {1} [{2}]", new string[]
													{
														num6.ToString(),
														req.Rules.Count.ToString(),
														testRule.ToString()
													});
													potentialTest2.AddMethodFoundNote("Shift back end of day time availability (room end time={0})", new string[]
													{
														potentialRoom5.AvailabilityEndTimeForTheDay.ToString("yyyy-MM-dd h:mm tt")
													});
													list24.Add(potentialTest2);
													list6.Add(new PrivateNote(string.Format("Moved from {0} to {1} due to end of day.", req.ClassTest.StartDate.ToString("h:mm tt"), dateTime5.ToString("h:mm tt"))));
												}
											}
										}
										else if (potentialRoom5.AvailabilityStartTimeForTheDay != DateTime.MinValue && potentialRoom5.AvailabilityStartTimeForTheDay > sd)
										{
											DateTime[] array6 = new DateTime[]
											{
												potentialRoom5.AvailabilityStartTimeForTheDay,
												sd
											};
											TimeSpan timeSpan2 = array6[0] - array6[1];
											DateTime dateTime7 = sd.AddMinutes(timeSpan2.TotalMinutes);
											DateTime dateTime8 = ed.AddMinutes(timeSpan2.TotalMinutes);
											if (potentialRoom5.IsAvailableByStartAndEndofDayAvailabilityTimes(dateTime7, dateTime8))
											{
												PotentialTest potentialTest3 = new PotentialTest(num++, dateTime7, dateTime8, room, true);
												potentialTest3.AddMethodFoundNote("Virtual room - ok to double book");
												potentialTest3.AddMethodFoundNote("Passed room availability & timetable checks");
												potentialTest3.AddMethodFoundNote("Rule #{0} of {1} [{2}]", new string[]
												{
													num6.ToString(),
													req.Rules.Count.ToString(),
													testRule.ToString()
												});
												potentialTest3.AddMethodFoundNote("Shift forward start of day time availability (room start time={0})", new string[]
												{
													potentialRoom5.AvailabilityStartTimeForTheDay.ToString("yyyy-MM-dd h:mm tt")
												});
												list24.Add(potentialTest3);
												list6.Add(new PrivateNote(string.Format("Moved from {0} to {1} due to start of day.", req.ClassTest.StartDate.ToString("h:mm tt"), dateTime7.ToString("h:mm tt"))));
											}
										}
									}
								}
								goto IL_1D9C;
							}
							goto IL_1599;
						}
						goto IL_1599;
						IL_1D9C:
						if (customTestBookingRules != null)
						{
							Exception ex;
							customTestBookingRules.FindPotentialBookingsMid('d', ref list20, testRule, ref list24, ref list4, ref rules, ref list17, ref list18, pbookingInfo, out ex, Array.Empty<object>());
						}
						foreach (PotentialTest item5 in list24)
						{
							list21.Add(item5);
						}
						if (num8 > 0 && list21.Count > num8)
						{
							break;
						}
						continue;
						IL_1599:
						foreach (DateRange dateRange2 in list20)
						{
							DateTime sd = dateRange2.StartDate;
							DateTime ed = dateRange2.EndDate;
							bool flag5 = !TimeTableItem.Overlaps(list7, sd, ed);
							if (flag5)
							{
								bookingResults.FailedTimetableCheck = new bool?(false);
							}
							else if (bookingResults.FailedTimetableCheck == null)
							{
								bookingResults.FailedTimetableCheck = new bool?(true);
							}
							List<List<DateRange>> list25 = new List<List<DateRange>>();
							List<DateRange> list26 = new List<DateRange>();
							list25.Add(list26);
							if (flag5 || !testRule.ShiftTimeAroundTimetable)
							{
								list26.Add(dateRange2);
							}
							else
							{
								int num9 = Convert.ToInt32((ed - sd).TotalMinutes);
								DateTime dateTime9 = sd;
								while (dateTime9 <= sd.Date.AddHours(23.0))
								{
									list26.Add(new DateRange(dateTime9, dateTime9.AddMinutes((double)num9)));
									dateTime9 = dateTime9.AddMinutes(15.0);
								}
								List<DateRange> list27 = new List<DateRange>();
								dateTime9 = sd;
								while (dateTime9 >= sd.Date.AddHours(7.0))
								{
									list27.Add(new DateRange(dateTime9, dateTime9.AddMinutes((double)num9)));
									dateTime9 = dateTime9.AddMinutes(-15.0);
								}
								if (list27.Count > 0)
								{
									list25.Add(list27);
								}
							}
							Predicate<DateRange> <>9__19;
							foreach (List<DateRange> list28 in list25)
							{
								bool flag6 = false;
								foreach (DateRange dateRange3 in list28)
								{
									if (testRule.ShiftTimeToMatchStartOfDay && dateRange3.StartDate < potentialRoom5.AvailabilityStartTimeForTheDay)
									{
										TimeSpan timeSpan3 = dateRange3.EndDate - dateRange3.StartDate;
										dateRange3.StartDate = new DateTime(dateRange3.StartDate.Year, dateRange3.StartDate.Month, dateRange3.StartDate.Day, potentialRoom5.AvailabilityStartTimeForTheDay.Hour, potentialRoom5.AvailabilityStartTimeForTheDay.Minute, 0);
										dateRange3.EndDate = dateRange3.StartDate.AddMinutes(timeSpan3.TotalMinutes);
									}
									else if (testRule.ShiftTimeToMatchEndOfDay && dateRange3.EndDate > potentialRoom5.AvailabilityEndTimeForTheDay)
									{
										TimeSpan timeSpan4 = dateRange3.EndDate - dateRange3.StartDate;
										dateRange3.EndDate = new DateTime(dateRange3.EndDate.Year, dateRange3.EndDate.Month, dateRange3.EndDate.Day, potentialRoom5.AvailabilityEndTimeForTheDay.Hour, potentialRoom5.AvailabilityEndTimeForTheDay.Minute, 0);
										dateRange3.StartDate = dateRange3.EndDate.AddMinutes(-timeSpan4.TotalMinutes);
									}
									sd = dateRange3.StartDate;
									ed = dateRange3.EndDate;
									flag5 = !TimeTableItem.Overlaps(list7, sd, ed);
									if (flag5)
									{
										bookingResults.FailedTimetableCheck = new bool?(false);
									}
									else if (bookingResults.FailedTimetableCheck == null)
									{
										bookingResults.FailedTimetableCheck = new bool?(true);
									}
									List<DateRange> list29 = list9;
									Predicate<DateRange> match;
									if ((match = <>9__19) == null)
									{
										match = (<>9__19 = ((DateRange ss) => ss.Intersects(sd, ed)));
									}
									bool flag7 = list29.Exists(match);
									if (!flag7)
									{
										bookingResults.StudentIsDoubleBooked = new bool?(false);
									}
									else if (bookingResults.StudentIsDoubleBooked == null)
									{
										bookingResults.StudentIsDoubleBooked = new bool?(true);
									}
									DateTime sdWithBuffer = sd.AddMinutes((double)(-(double)req.BufferMinutesPre));
									DateTime edWithBuffer = ed.AddMinutes((double)req.BufferMinutesPost);
									bool flag8 = list19.Exists((DateRange rs) => rs.Scope == room.RoomId && rs.Intersects(sdWithBuffer, edWithBuffer));
									if (!flag8)
									{
										bookingResults.RoomIsDoubleBooked = new bool?(false);
									}
									else if (bookingResults.RoomIsDoubleBooked == null)
									{
										bookingResults.RoomIsDoubleBooked = new bool?(true);
									}
									CWLogger.Logger.Debug("TESTBOOK:FindPotentials:EvaluateNonVirtualRoom_TimeToInvestigate:pid={0}:lucid={1}:time={2} to {3}:passedtimetablecheck={4}:studentdoublebooked={5},roomdoublebooked={6}", new object[]
									{
										req.Pid.ToString(),
										req.Lucid.ToString(),
										sd.ToString("yyyy-MM-dd H:mm"),
										ed.ToString("H:mm"),
										flag5.ToString(),
										flag7.ToString(),
										flag8.ToString()
									});
									if (flag5 && !flag7 && !flag8)
									{
										foreach (AvailabilityScheduleRange availabilityScheduleRange2 in roomAvailability.Ranges)
										{
											if (availabilityScheduleRange2.Pid == room.RoomId && availabilityScheduleRange2.Start <= sd && availabilityScheduleRange2.End >= ed)
											{
												PotentialTest potentialTest4 = new PotentialTest(num++, sd, ed, room, false);
												potentialTest4.AddMethodFoundNote("NON virtual room - NOT ok to double book");
												potentialTest4.AddMethodFoundNote("Passed student schedule & timetable & room availability checks");
												potentialTest4.AddMethodFoundNote("Rule #{0} of {1} [{2}]", new string[]
												{
													num6.ToString(),
													req.Rules.Count.ToString(),
													testRule.ToString()
												});
												list24.Add(potentialTest4);
												bookingResults.NoRoomAvailability = new bool?(false);
												flag6 = true;
											}
											if (num8 > 0 && list24.Count > num8)
											{
												break;
											}
										}
										if (bookingResults.NoRoomAvailability == null)
										{
											bookingResults.NoRoomAvailability = new bool?(true);
											CWLogger logger3 = CWLogger.Logger;
											string message3 = "Common.DAO.AutoTestBooking.Legacy.FindPotentialTest.RegularRoomChecks:NoAvailabilityWasFound:roomAvailability.Ranges.Count={0}:ranges={1}";
											object arg = (roomAvailability == null || roomAvailability.Ranges == null) ? "NULL" : roomAvailability.Ranges.Count.ToString();
											object arg2;
											if (roomAvailability != null && roomAvailability.Ranges != null)
											{
												arg2 = string.Join(", ", roomAvailability.Ranges.ConvertAll<string>((AvailabilityScheduleRange g) => string.Format("rid={0};start={1};end={2}", g.Rid.ToString(), g.Start.ToString(), g.End.ToString())).ToArray());
											}
											else
											{
												arg2 = "NULL";
											}
											logger3.Warn(message3, arg, arg2);
										}
										if (num8 > 0 && list24.Count > num8)
										{
											break;
										}
									}
									if (num8 > 0 && list24.Count > num8)
									{
										break;
									}
								}
								if (flag6)
								{
									break;
								}
							}
						}
						goto IL_1D9C;
					}
					foreach (PotentialTest item6 in list21)
					{
						list4.Add(item6);
					}
					if (num8 > 0 && list4.Count > num8)
					{
						break;
					}
				}
				if (customTestBookingRules != null)
				{
					Exception ex;
					customTestBookingRules.FindPotentialBookingsEnd(ref list4, ref rules, ref list17, ref list18, pbookingInfo, out ex, Array.Empty<object>());
				}
				if (list4.Count > 0)
				{
					break;
				}
			}
			int num10 = list4.Count - num2;
			if (num10 > 0)
			{
				for (int num11 = 0; num11 < num10; num11++)
				{
					if (list4.Count > 0)
					{
						list4.RemoveAt(list4.Count - 1);
					}
				}
			}
			if (num3 > 0)
			{
				foreach (PotentialTest potentialTest5 in list4)
				{
					potentialTest5.Test.BreakTime = num3;
				}
			}
			CWLogger logger4 = CWLogger.Logger;
			string message4 = "TESTBOOK:FindPotentials:ReturnPotentialTests:pid={0}:lucid={1}:bookingresults.noroomavailability={2}:PotentialTests={3}";
			object[] array7 = new object[4];
			array7[0] = req.Pid.ToString();
			array7[1] = req.Lucid.ToString();
			array7[2] = ((bookingResults == null || bookingResults.NoRoomAvailability == null) ? "NULL" : bookingResults.NoRoomAvailability.ToString());
			array7[3] = string.Join(", ", list4.ToList<PotentialTest>().ConvertAll<string>((PotentialTest pt) => pt.ToStringDebug()).ToArray());
			logger4.Debug(message4, array7);
			List<PotentialTest> list30 = new List<PotentialTest>();
			foreach (PotentialTest potentialTest6 in list4)
			{
				List<PotentialTestMethodFoundNote> list31 = new List<PotentialTestMethodFoundNote>();
				if (potentialTest6.MethodFoundNotes != null)
				{
					foreach (PotentialTestMethodFoundNote potentialTestMethodFoundNote in potentialTest6.MethodFoundNotes)
					{
						list31.Add(new PotentialTestMethodFoundNote
						{
							Note = potentialTestMethodFoundNote.Note
						});
					}
				}
				Test test2;
				if (potentialTest6.Test != null)
				{
					Room room2;
					if (potentialTest6.Test.Room != null)
					{
						room2 = new Room
						{
							Campuses = potentialTest6.Test.Room.Campuses,
							PriorityNumber = potentialTest6.Test.Room.PriorityNumber,
							RoomId = potentialTest6.Test.Room.RoomId,
							RoomType = potentialTest6.Test.Room.RoomType,
							Title = potentialTest6.Test.Room.Title
						};
					}
					else
					{
						room2 = null;
					}
					test2 = new Test
					{
						BreakTime = potentialTest6.Test.BreakTime,
						CourseDescription = potentialTest6.Test.CourseDescription,
						EndDate = potentialTest6.Test.EndDate,
						Location = potentialTest6.Test.Location,
						Lucid = potentialTest6.Test.Lucid,
						Room = room2,
						StartDate = potentialTest6.Test.StartDate
					};
				}
				else
				{
					test2 = null;
				}
				list30.Add(new PotentialTest
				{
					MethodFoundNotes = list31,
					OkToDoubleBook = potentialTest6.OkToDoubleBook,
					Test = test2
				});
			}
			return new FindPotentialBookingsResp
			{
				PotentialTests = list30,
				BookingResults = bookingResults,
				EmailBody = emailBody,
				IconIds = iconIds,
				PrivateNotes = list6,
				DebugNotes = list3
			};
		}

		// Token: 0x0600001A RID: 26 RVA: 0x000055F4 File Offset: 0x000037F4
		private static string LoadCampus(int lucid, OperationContext opContext)
		{
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(eDatabaseConnectionStringName.ClockWork, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@lucid", DbType.Int32, lucid)
			};
			DataTable dataTable = databaseLayer.ExecuteQuery("SELECT campus FROM lucourses WHERE lucourseid=@lucid", parameters);
			if (dataTable.Rows.Count <= 0)
			{
				return "";
			}
			return dataTable.Rows[0][0].ToString().Trim();
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00005670 File Offset: 0x00003870
		public static ApplySpecialAccommodationsResp ApplySpecialAccommodationRules(bool debugMode, int pid, int lucid, IList<SpecialAccommodation> specialAccommodations, DateTime classTestStartDateTime, DateTime classTestEndDateTime, IList<Accommodation> accommodationsToUse, int appIdToIgnoreWhenCheckingStudentsSchedule, int overrideRoomAvailabilityPid, IList<Room> availableRooms, bool IgnoreStudentsSchedule, IList<int> IgnoreStudentAppointmentIds, OperationContext opContext)
		{
			Test classTest = new Test(classTestStartDateTime, classTestEndDateTime, null);
			IList<PrivateNote> privateNotes;
			StringBuilder emailBodySb;
			IList<int> iconsToBookWith;
			Test newTestScheduledTimeAndRoom = Booker.ApplySpecialAccommodationRules(debugMode, pid, lucid, specialAccommodations, classTest, accommodationsToUse, out privateNotes, out emailBodySb, out iconsToBookWith, appIdToIgnoreWhenCheckingStudentsSchedule, overrideRoomAvailabilityPid, availableRooms, IgnoreStudentsSchedule, IgnoreStudentAppointmentIds, opContext);
			return new ApplySpecialAccommodationsResp
			{
				EmailBodySb = emailBodySb,
				IconsToBookWith = iconsToBookWith,
				PrivateNotes = privateNotes,
				NewTestScheduledTimeAndRoom = newTestScheduledTimeAndRoom
			};
		}

		// Token: 0x0600001C RID: 28 RVA: 0x000056CC File Offset: 0x000038CC
		private static Test ApplySpecialAccommodationRules(bool debugMode, int pid, int lucid, IList<SpecialAccommodation> specialAccommodations, Test classTest, IList<Accommodation> accommodationsToUse, out IList<PrivateNote> privateNotes, out StringBuilder emailBodySb, out IList<int> iconsToBookWith, int appIdToIgnoreWhenCheckingStudentsSchedule, int overrideRoomAvailabilityPid, IList<Room> availableRooms, bool IgnoreStudentsSchedule, IList<int> IgnoreStudentAppointmentIds, OperationContext opContext)
		{
			privateNotes = new List<PrivateNote>();
			iconsToBookWith = new List<int>();
			emailBodySb = new StringBuilder();
			int duration = classTest.Duration;
			Test test = new Test(classTest.StartDate, classTest.EndDate, classTest.Room);
			int num = duration;
			List<SpecialAccommodation> list = specialAccommodations.ToList<SpecialAccommodation>();
			list.Sort((SpecialAccommodation s1, SpecialAccommodation s2) => s1.SpecialAccommodationTypeOrder.CompareTo(s2.SpecialAccommodationTypeOrder));
			foreach (SpecialAccommodation specialAccommodation in list)
			{
				if (specialAccommodation.SpecialAccommodationType == SpecialAccommodationType.SnapTime)
				{
					string arg = specialAccommodation.GetArg("rules", "");
					if (!string.IsNullOrEmpty(arg))
					{
						string str = "2000-01-01 ";
						foreach (string text in arg.Split(new char[]
						{
							','
						}, StringSplitOptions.RemoveEmptyEntries))
						{
							int num2 = text.LastIndexOf('=');
							if (num2 > 0)
							{
								string text2 = text.Substring(0, num2);
								string text3 = text.Substring(num2 + 1).Trim();
								int numDaysDirection = 0;
								if (text3.StartsWith("n", StringComparison.OrdinalIgnoreCase))
								{
									text3 = text3.Substring(1);
									numDaysDirection = 1;
								}
								else if (text3.StartsWith("p", StringComparison.OrdinalIgnoreCase))
								{
									text3 = text3.Substring(1);
									numDaysDirection = -1;
								}
								if (text3.Length <= 2)
								{
									text3 += ":00";
								}
								string s = str + text3;
								DateTime dateTime = new DateTime(2000, 1, 1, test.StartDate.Hour, test.StartDate.Minute, 0);
								DateTime dateTime2;
								if (DateTime.TryParse(s, out dateTime2))
								{
									if (text2.StartsWith("<="))
									{
										string text4 = text2.Substring(2).Trim();
										if (text4.Length <= 2)
										{
											text4 += ":00";
										}
										text4 = str + text4;
										DateTime dateTime3;
										if (DateTime.TryParse(text4, out dateTime3) && !(dateTime > dateTime3))
										{
											test.ShiftToStartAt(numDaysDirection, dateTime2.Hour, dateTime2.Minute);
											break;
										}
									}
									else if (text2.StartsWith("<"))
									{
										string text4 = text2.Substring(1).Trim();
										if (text4.Length <= 2)
										{
											text4 += ":00";
										}
										text4 = str + text4;
										DateTime dateTime3;
										if (DateTime.TryParse(text4, out dateTime3) && !(dateTime >= dateTime3))
										{
											test.ShiftToStartAt(numDaysDirection, dateTime2.Hour, dateTime2.Minute);
											break;
										}
									}
									else if (text2.StartsWith(">="))
									{
										string text4 = text2.Substring(2).Trim();
										if (text4.Length <= 2)
										{
											text4 += ":00";
										}
										text4 = str + text4;
										DateTime dateTime3;
										if (DateTime.TryParse(text4, out dateTime3) && !(dateTime < dateTime3))
										{
											test.ShiftToStartAt(numDaysDirection, dateTime2.Hour, dateTime2.Minute);
											break;
										}
									}
									else if (text2.StartsWith(">"))
									{
										string text4 = text2.Substring(1).Trim();
										if (text4.Length <= 2)
										{
											text4 += ":00";
										}
										text4 = str + text4;
										DateTime dateTime3;
										if (DateTime.TryParse(text4, out dateTime3) && !(dateTime <= dateTime3))
										{
											test.ShiftToStartAt(numDaysDirection, dateTime2.Hour, dateTime2.Minute);
											break;
										}
									}
									else if (text2.StartsWith("="))
									{
										string text4 = text2.Substring(1).Trim();
										if (text4.Length <= 2)
										{
											text4 += ":00";
										}
										text4 = str + text4;
										DateTime dateTime3;
										if (DateTime.TryParse(text4, out dateTime3) && !(dateTime != dateTime3))
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
				else
				{
					int cid = specialAccommodation.ControlId;
					Accommodation accommodation = accommodationsToUse.ToList<Accommodation>().Find((Accommodation e) => e.ControlId == cid);
					if (accommodation != null && !string.IsNullOrEmpty(specialAccommodation.ControlIdSpecificValue) && (string.IsNullOrEmpty(accommodation.LookupText) || accommodation.LookupText.IndexOf(specialAccommodation.ControlIdSpecificValue, StringComparison.OrdinalIgnoreCase) < 0 || (!string.IsNullOrEmpty(accommodation.SubText) && accommodation.SubText.IndexOf(specialAccommodation.ControlIdSpecificValue, StringComparison.OrdinalIgnoreCase) >= 0)))
					{
						accommodation = null;
					}
					if (accommodation == null && specialAccommodation.SpecialAccommodationType == SpecialAccommodationType.Breaks)
					{
						int secondCid = specialAccommodation.GetArgInt("secondcid", 0);
						if (secondCid > 0)
						{
							accommodation = accommodationsToUse.FirstOrDefault((Accommodation e) => e.ControlId == secondCid);
							if (accommodation != null)
							{
								accommodation.LookupText = "";
								accommodation.Title = "";
							}
						}
					}
					if (accommodation != null || specialAccommodation.SpecialAccommodationType == SpecialAccommodationType.StartEndOfDaySlide)
					{
						double num3;
						if (accommodation != null)
						{
							num3 = Accommodation.ExtractNumber(accommodation.Title);
							if (num3 <= 0.0)
							{
								num3 = Accommodation.ExtractNumber(accommodation.LookupText);
							}
						}
						else
						{
							num3 = 0.0;
						}
						if (specialAccommodation.SpecialAccommodationType == SpecialAccommodationType.Extra_Time)
						{
							int num4 = Booker.CalculateExtraTime(classTest, test, accommodationsToUse, specialAccommodation, num3, accommodation);
							if (num4 > 0)
							{
								int num5 = num4;
								string arg2 = specialAccommodation.GetArg("overridebookingnote", "Added extra time ({0})");
								if (num5 > num)
								{
									DateTime endDate = test.StartDate.AddMinutes((double)num5);
									num = num5;
									test.EndDate = endDate;
									string durationDescription = Booker.GetDurationDescription(num4 - classTest.Duration);
									privateNotes.Add(new PrivateNote(string.Format(arg2, durationDescription)));
									CWLogger.Logger.Trace("TESTBOOK:FindPotentials:ApplySpecialAccommodationRules:ExtraTime:pid={0}:lucid={1}:extra time amount={2}:extratimeminutes:{3}", new object[]
									{
										pid.ToString(),
										lucid.ToString(),
										num3.ToString(),
										num4.ToString()
									});
								}
							}
						}
						else if (specialAccommodation.SpecialAccommodationType == SpecialAccommodationType.Breaks)
						{
							int num6 = Booker.CalculateBreakTime(test, accommodationsToUse, test.Duration, specialAccommodation, num3);
							if (num6 > 0 && num6 > test.BreakTime)
							{
								string arg2 = specialAccommodation.GetArg("overridebookingnote", "Applied break time of {0} minutes");
								test.BreakTime = num6;
								privateNotes.Add(new PrivateNote(string.Format("Applied break time of {0} minutes", num6.ToString())));
							}
							if (debugMode)
							{
							}
						}
						else if (specialAccommodation.SpecialAccommodationType == SpecialAccommodationType.AddIcon)
						{
							int argInt = specialAccommodation.GetArgInt("iconnum", 0);
							if (argInt > 0)
							{
								iconsToBookWith.Add(argInt);
							}
							if (debugMode)
							{
							}
						}
						else if (specialAccommodation.SpecialAccommodationType == SpecialAccommodationType.DaysRest)
						{
							Test test2 = Booker.CalculateDaysRest(pid, specialAccommodation, num3, test, overrideRoomAvailabilityPid, availableRooms, opContext);
							if (test2 != null && (test.StartDate != test2.StartDate || test.EndDate != test2.EndDate))
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
						else if (specialAccommodation.SpecialAccommodationType == SpecialAccommodationType.MaxPerDay)
						{
							Test test3 = Booker.CalculateMaxPerDay(pid, specialAccommodation, num3, test, overrideRoomAvailabilityPid, availableRooms, opContext);
							if (test3 != null && (test.StartDate != test3.StartDate || test.EndDate != test3.EndDate))
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
							if (specialAccommodation.SpecialAccommodationType == SpecialAccommodationType.CantBookOnline)
							{
								return null;
							}
							if (specialAccommodation.SpecialAccommodationType == SpecialAccommodationType.EmailCoordinator)
							{
								if (emailBodySb.Length > 0)
								{
									emailBodySb.Append(Environment.NewLine);
								}
								emailBodySb.Append("• ");
								emailBodySb.Append(accommodation.Title);
							}
							else if (specialAccommodation.SpecialAccommodationType == SpecialAccommodationType.TimeOfDay)
							{
								Test test4 = Booker.CalculateTimeOfDay(specialAccommodation, num3, test, appIdToIgnoreWhenCheckingStudentsSchedule, pid, overrideRoomAvailabilityPid, availableRooms, IgnoreStudentsSchedule, IgnoreStudentAppointmentIds, opContext);
								if (test4 != null && (test.StartDate != test4.StartDate || test.EndDate != test4.EndDate))
								{
									string arg2 = specialAccommodation.GetArg("overridebookingnote", "Moved from {0} at {1} to {2} at {3} due to time-of-day accommodation");
									if (test.StartDate.Date != test4.StartDate.Date)
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
							else if (specialAccommodation.SpecialAccommodationType == SpecialAccommodationType.StartEndOfDaySlide)
							{
								int num7;
								Test test5 = Booker.CalculateStartOfDayOrEndOfDaySlide(specialAccommodation, test, classTest, out num7);
								if (num7 < 0)
								{
									string arg2 = specialAccommodation.GetArg("overridebookingnotemoveforward", "Moved from {0} to {1} due to end of day.");
									privateNotes.Add(new PrivateNote(string.Format(arg2, test.StartDate.ToString("h:mm tt"), test5.StartDate.ToString("h:mm tt"))));
								}
								else if (num7 > 0)
								{
									string arg2 = specialAccommodation.GetArg("overridebookingnotemovebackward", "Moved from {0} to {1} due to start of day.");
									privateNotes.Add(new PrivateNote(string.Format(arg2, test.StartDate.ToString("h:mm tt"), test5.StartDate.ToString("h:mm tt"))));
								}
								test = test5;
							}
						}
					}
				}
			}
			return test;
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00006260 File Offset: 0x00004460
		private static string GetDurationDescription(int tmins)
		{
			int num = (int)(Convert.ToDouble(tmins) / 60.0);
			int num2 = tmins - num * 60;
			string text = "";
			if (num == 1)
			{
				text = "1 hour";
				if (num2 > 0)
				{
					text += "; ";
				}
			}
			else if (num > 1)
			{
				text = num.ToString() + " hours";
				if (num2 > 0)
				{
					text += "; ";
				}
			}
			if (num2 == 1)
			{
				text += "1 minute";
			}
			else if (num2 > 1)
			{
				text = text + num2.ToString() + " minutes";
			}
			return text;
		}

		// Token: 0x0600001E RID: 30 RVA: 0x000062F8 File Offset: 0x000044F8
		private static Test CalculateStartOfDayOrEndOfDaySlide(SpecialAccommodation acc, Test targetTest, Test classTest, out int shiftedResult)
		{
			string arg = acc.GetArg("starttime", "");
			string arg2 = acc.GetArg("endtime", "");
			DateTime startDate = targetTest.StartDate;
			DateTime t = targetTest.EndDate;
			if (targetTest.BreakTime > 0)
			{
				t = t.AddMinutes((double)targetTest.BreakTime);
			}
			shiftedResult = 0;
			DateTime dateTime;
			if (!string.IsNullOrEmpty(arg) && DateTime.TryParse(string.Format("{0} {1}", targetTest.StartDate.ToString("yyyy-MM-dd"), arg), out dateTime) && startDate < dateTime)
			{
				int duration = targetTest.Duration;
				Test test = new Test(dateTime, targetTest.EndDate, targetTest.Room);
				test.BreakTime = targetTest.BreakTime;
				test.EndDate = test.StartDate.AddMinutes((double)duration);
				shiftedResult = 1;
				CWLogger.Logger.Trace("Moved test forward to bring to start of day:newstart={0}:newend={1}", targetTest.StartDate.ToString("yyyy-MM-dd h:mm tt"), targetTest.EndDate.ToString("yyyy-MM-dd h:mm tt"));
				return test;
			}
			DateTime dateTime2;
			if (!string.IsNullOrEmpty(arg2) && DateTime.TryParse(string.Format("{0} {1}", targetTest.StartDate.ToString("yyyy-MM-dd"), arg2), out dateTime2) && t > dateTime2)
			{
				int duration2 = targetTest.Duration;
				Test test2 = new Test(targetTest.StartDate, dateTime2, targetTest.Room);
				test2.BreakTime = targetTest.BreakTime;
				if (test2.BreakTime > 0)
				{
					test2.EndDate = test2.EndDate.AddMinutes((double)(-(double)targetTest.BreakTime));
				}
				test2.StartDate = test2.EndDate.AddMinutes((double)(-(double)duration2));
				shiftedResult = -1;
				CWLogger.Logger.Trace("Moved test backward to bring to end of day:newstart={0}:newend={1}", targetTest.StartDate.ToString("yyyy-MM-dd h:mm tt"), targetTest.EndDate.ToString("yyyy-MM-dd h:mm tt"));
				return test2;
			}
			return targetTest;
		}

		// Token: 0x0600001F RID: 31 RVA: 0x000064FC File Offset: 0x000046FC
		private static Test CalculateTimeOfDay(SpecialAccommodation acc, double num, Test targetTest, int appIdToIgnoreWhenCheckingStudentsSchedule, int pid, int overrideRoomAvailabilityPid, IList<Room> availableRooms, bool IgnoreStudentsSchedule, IList<int> IgnoreStudentAppointmentIds, OperationContext opContext)
		{
			string text = acc.GetArg("starttime", "");
			string text2 = acc.GetArg("endtime", "");
			string arg = acc.GetArg("pushtotomorrowtime", "");
			DateTime? dateTime;
			if (!string.IsNullOrEmpty(arg))
			{
				DateTime value;
				if (!DateTime.TryParse(string.Format("{0} {1}", targetTest.StartDate.ToString("yyyy-MM-dd"), arg), out value))
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
			DateTime value2;
			if (!string.IsNullOrEmpty(arg2) && DateTime.TryParse(arg2, out value2))
			{
				dateTime2 = new DateTime?(value2);
			}
			CWLogger.Logger.Trace("TESTBOOK:Booker:CalculateTimeOfDay:Info1:targetTest={0}:st={1}:et={2}:pushtotomorrow={3}:pushtotomorrowoverride={4}", new object[]
			{
				targetTest.ToStringDebug(),
				text,
				text2,
				arg,
				(dateTime2 == null) ? "NULL" : dateTime2.Value.ToString("yyyy-MM-dd")
			});
			if (string.IsNullOrEmpty(text))
			{
				return targetTest;
			}
			DateTime minValue = DateTime.MinValue;
			text = string.Format("{0} {1}", targetTest.StartDate.ToString("yyyy-MM-dd"), text);
			text2 = ((!string.IsNullOrEmpty(text2)) ? string.Format("{0} {1}", targetTest.EndDate.ToString("yyyy-MM-dd"), text2) : "");
			DateTime dateTime3;
			if (!DateTime.TryParse(text, out dateTime3) || (!string.IsNullOrEmpty(text2) && !DateTime.TryParse(text2, out minValue)))
			{
				return targetTest;
			}
			bool flag;
			if (!string.IsNullOrEmpty(text2))
			{
				flag = (targetTest.StartDate < dateTime3 || targetTest.StartDate >= minValue);
			}
			else
			{
				flag = (targetTest.StartDate.Hour != dateTime3.Hour || targetTest.StartDate.Minute != dateTime3.Minute);
			}
			if (flag)
			{
				Test test = new Test(dateTime3, dateTime3.AddMinutes((double)targetTest.Duration), targetTest.Room);
				test.BreakTime = targetTest.BreakTime;
				if (dateTime != null && targetTest.StartDate > dateTime.Value)
				{
					Booker.MoveTestToAnotherDay(test, 1, overrideRoomAvailabilityPid, availableRooms, opContext);
					if (dateTime2 != null)
					{
						DateTime value3 = dateTime2.Value;
						int duration = test.Duration;
						test.StartDate = new DateTime(test.StartDate.Year, test.StartDate.Month, test.StartDate.Day, value3.Hour, value3.Minute, 0);
						test.EndDate = test.StartDate.AddMinutes((double)duration);
					}
				}
				else if (dateTime2 != null)
				{
					DateTime value4 = dateTime2.Value;
					int duration2 = test.Duration;
					test.StartDate = new DateTime(test.StartDate.Year, test.StartDate.Month, test.StartDate.Day, value4.Hour, value4.Minute, 0);
					test.EndDate = test.StartDate.AddMinutes((double)duration2);
				}
				if (!IgnoreStudentsSchedule && Booker.IsStudentDoubleBooked(pid, test.StartDate, test.EndDate, appIdToIgnoreWhenCheckingStudentsSchedule, IgnoreStudentAppointmentIds, opContext) && dateTime != null)
				{
					Booker.MoveTestToAnotherDay(test, 1, overrideRoomAvailabilityPid, availableRooms, opContext);
					if (dateTime2 != null)
					{
						DateTime value5 = dateTime2.Value;
						int duration3 = test.Duration;
						test.StartDate = new DateTime(test.StartDate.Year, test.StartDate.Month, test.StartDate.Day, value5.Hour, value5.Minute, 0);
						test.EndDate = test.StartDate.AddMinutes((double)duration3);
					}
					else if (dateTime2 != null)
					{
						DateTime value6 = dateTime2.Value;
						int duration4 = test.Duration;
						test.StartDate = new DateTime(test.StartDate.Year, test.StartDate.Month, test.StartDate.Day, value6.Hour, value6.Minute, 0);
						test.EndDate = test.StartDate.AddMinutes((double)duration4);
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
				return test;
			}
			CWLogger.Logger.Debug("TESTBOOK:Booker:CalculateTimeOfDay:OutsideOfSpecifiedTimeRange=false:targetTest={0}:newTest={1}:st={2}:et={3}:pushtotomorrow={4}", new object[]
			{
				targetTest.ToStringDebug(),
				targetTest.ToStringDebug(),
				text,
				text2,
				arg
			});
			return targetTest;
		}

		// Token: 0x06000020 RID: 32 RVA: 0x00006A18 File Offset: 0x00004C18
		private static bool IsStudentDoubleBooked(int pid, DateTime startDateTime, DateTime endDateTime, int appIdToIgnoreWhenCheckingStudentsSchedule, IList<int> IgnoreStudentAppointmentIds, OperationContext opContext)
		{
			DataTable dataTable = Booker.LoadStudentSchedule(pid, startDateTime, appIdToIgnoreWhenCheckingStudentsSchedule, opContext);
			if (IgnoreStudentAppointmentIds != null && IgnoreStudentAppointmentIds.Count > 0)
			{
				try
				{
					List<DataRow> list = new List<DataRow>();
					foreach (object obj in dataTable.Rows)
					{
						DataRow dataRow = (DataRow)obj;
						if (IgnoreStudentAppointmentIds.Contains((int)dataRow["appointmentid"]))
						{
							list.Add(dataRow);
						}
					}
					foreach (DataRow row in list)
					{
						dataTable.Rows.Remove(row);
					}
				}
				catch (Exception ex)
				{
					CWLogger.Logger.Error("IsStudentDoubleBooked1:{0}", ex.ToString());
				}
			}
			return DateRange.FromTable(dataTable).Exists((DateRange ss) => ss.Intersects(startDateTime, endDateTime));
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00006B54 File Offset: 0x00004D54
		private static Test CalculateDaysRest(int pid, SpecialAccommodation acc, double num, Test targetTest, int overrideRoomAvailabilityPid, IList<Room> availableRooms, OperationContext opContext)
		{
			string text = acc.GetArg("defaultfuturestarttime", "");
			int argInt = acc.GetArgInt("daysbetween", 0);
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(eDatabaseConnectionStringName.ClockWork, (opContext != null) ? opContext.TenantId : null);
			Test result;
			try
			{
				int num2;
				if (argInt <= 0)
				{
					num2 = Convert.ToInt32(num);
					if (num2 < 1)
					{
						return targetTest;
					}
				}
				else
				{
					num2 = argInt;
				}
				DateTime dateTime = targetTest.StartDate.Date.AddDays((double)(-(double)num2));
				DateTime dateTime2 = dateTime.AddDays(30.0);
				DbParameter[] parameters = new DbParameter[]
				{
					databaseLayer.GetParameter("@startdate", DbType.DateTime, dateTime),
					databaseLayer.GetParameter("@enddate", DbType.DateTime, dateTime2),
					databaseLayer.GetParameter("@pid", DbType.Int32, pid)
				};
				DataTable dataTable = databaseLayer.ExecuteQuery("SELECT DISTINCT a.appointmentid,a.startdate \r\nFROM    apps a \r\nWHERE   a.personid=@pid AND a.startdate>=@startdate \r\n        AND a.startdate<@enddate AND NOT a.examid IS NULL \r\n        AND a.cancelled=0 \r\nORDER BY a.startdate", parameters);
				DateTime? dateTime3 = null;
				if (dataTable.Rows.Count > 0)
				{
					string roomPids;
					if (overrideRoomAvailabilityPid < 1)
					{
						roomPids = string.Join(",", availableRooms.ToList<Room>().ConvertAll<string>((Room rm) => rm.RoomId.ToString()).ToArray());
					}
					else
					{
						roomPids = overrideRoomAvailabilityPid.ToString();
					}
					DateTime value = targetTest.StartDate.Date;
					int i = 0;
					while (i < 30)
					{
						bool flag = true;
						for (int j = -num2; j <= num2; j++)
						{
							DateTime dateTime4 = value.AddDays((double)j);
							if (j >= 0 && j > 0)
							{
								if (dateTime4.DayOfWeek == DayOfWeek.Saturday)
								{
									dateTime4 = dateTime4.AddDays(1.0);
								}
								else if (dateTime4.DayOfWeek == DayOfWeek.Sunday)
								{
									dateTime4 = dateTime4.AddDays(2.0);
								}
							}
							if (dataTable.Select(string.Concat(new string[]
							{
								"startdate>='",
								dateTime4.ToString("yyyy-MM-dd"),
								"' AND startdate<'",
								dateTime4.AddDays(1.0).ToString("yyyy-MM-dd"),
								"'"
							})).Length != 0)
							{
								flag = false;
								break;
							}
						}
						if (flag && !Booker.IsHoliday(2, roomPids, value.Date, opContext))
						{
							dateTime3 = new DateTime?(value);
							break;
						}
						value = value.AddDays(1.0);
						i++;
						DayOfWeek dayOfWeek = value.DayOfWeek;
						if (dayOfWeek != DayOfWeek.Sunday)
						{
							if (dayOfWeek == DayOfWeek.Saturday)
							{
								value = value.AddDays(2.0);
							}
						}
						else
						{
							value = value.AddDays(1.0);
						}
					}
				}
				else
				{
					dateTime3 = new DateTime?(targetTest.StartDate.Date);
				}
				if (dateTime3 == null)
				{
					result = null;
				}
				else if (dateTime3.Value.Date.Equals(targetTest.StartDate.Date))
				{
					result = targetTest;
				}
				else
				{
					DateTime startDate = new DateTime(dateTime3.Value.Year, dateTime3.Value.Month, dateTime3.Value.Day, targetTest.StartDate.Hour, targetTest.StartDate.Minute, 0);
					if (!string.IsNullOrEmpty(text))
					{
						text = "2001-01-01  " + text;
						DateTime dateTime5;
						if (DateTime.TryParse(text, out dateTime5))
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
			catch (Exception)
			{
				result = targetTest;
			}
			return result;
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00006F5C File Offset: 0x0000515C
		private static Test CalculateMaxPerDay(int pid, SpecialAccommodation acc, double num, Test targetTest, int overrideRoomAvailabilityPid, IList<Room> availableRooms, OperationContext opContext)
		{
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(eDatabaseConnectionStringName.ClockWork, (opContext != null) ? opContext.TenantId : null);
			int num2 = acc.GetArgInt("max", 0);
			string text = acc.GetArg("defaultfuturestarttime", "");
			bool flag = true;
			if (num > 0.0)
			{
				num2 = Convert.ToInt32(num);
			}
			if (num2 < 1)
			{
				return targetTest;
			}
			DateTime dateTime = targetTest.StartDate.Date.AddDays(-1.0);
			DateTime dateTime2 = dateTime.AddDays(30.0);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@startdate", DbType.DateTime, dateTime),
				databaseLayer.GetParameter("@enddate", DbType.DateTime, dateTime2),
				databaseLayer.GetParameter("@pid", DbType.Int32, pid)
			};
			string query = "SELECT DISTINCT a.appointmentid,a.startdate \r\nFROM    apps a \r\nWHERE   a.personid=@pid AND a.startdate>=@startdate \r\n        AND a.startdate<@enddate AND NOT a.examid IS NULL \r\n        AND a.cancelled=0 \r\nORDER BY a.startdate";
			DataTable dataTable = databaseLayer.ExecuteQuery(query, parameters);
			DateTime? dateTime3 = null;
			if (dataTable.Rows.Count > 0)
			{
				string roomPids;
				if (overrideRoomAvailabilityPid < 1)
				{
					roomPids = string.Join(",", availableRooms.ToList<Room>().ConvertAll<string>((Room rm) => rm.RoomId.ToString()).ToArray());
				}
				else
				{
					roomPids = overrideRoomAvailabilityPid.ToString();
				}
				int testBookingAvailabilityGroupId = 2;
				for (int i = 0; i < 30; i++)
				{
					DateTime value = targetTest.StartDate.AddDays((double)i);
					if (value.DayOfWeek != DayOfWeek.Saturday && value.DayOfWeek != DayOfWeek.Sunday)
					{
						DataRow[] array = dataTable.Select(string.Concat(new string[]
						{
							"startdate>='",
							value.ToString("yyyy-MM-dd"),
							"' AND startdate<'",
							value.AddDays(1.0).ToString("yyyy-MM-dd"),
							"'"
						}));
						if (array.Length < num2 && !Booker.IsHoliday(testBookingAvailabilityGroupId, roomPids, value.Date, opContext))
						{
							dateTime3 = new DateTime?(value);
							break;
						}
						if (i == 0 && flag && array.Length == 1)
						{
							DateTime dateTime4 = (DateTime)array[0]["startdate"];
							if (dateTime4.Hour > 12 && targetTest.StartDate.Hour < 12)
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
			if (dateTime3 == null)
			{
				return null;
			}
			DateTime startDate = new DateTime(dateTime3.Value.Year, dateTime3.Value.Month, dateTime3.Value.Day, targetTest.StartDate.Hour, targetTest.StartDate.Minute, 0);
			if (startDate.Date != targetTest.StartDate.Date && !string.IsNullOrEmpty(text))
			{
				text = string.Format("{0} {1}", "2001-01-01 ", text);
				DateTime dateTime5;
				if (DateTime.TryParse(text, out dateTime5))
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
			return new Test(startDate, endDate, targetTest.Room)
			{
				BreakTime = targetTest.BreakTime
			};
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00007468 File Offset: 0x00005668
		private static double ExtractNumberFromString(string s)
		{
			int num = -1;
			int num2 = -1;
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = s.Length - 1; i >= 0; i--)
			{
				char c = s[i];
				if (c == '.')
				{
					if (num2 >= 0)
					{
						if (num >= 0)
						{
							break;
						}
						num = i;
						stringBuilder.Insert(0, c);
					}
				}
				else if (char.IsDigit(c))
				{
					if (num2 < 0)
					{
						num2 = i;
					}
					stringBuilder.Insert(0, c);
				}
				else if (num2 >= 0)
				{
					break;
				}
			}
			double result;
			if (!double.TryParse(stringBuilder.ToString(), out result))
			{
				return 0.0;
			}
			return result;
		}

		// Token: 0x06000024 RID: 36 RVA: 0x000074F8 File Offset: 0x000056F8
		public static int CalculateExtraTime(int classDurationInMinutes, IList<AccommodationBasic> accommodationsToUse, IList<SpecialAccommodation> allSpecialAccommodationRules)
		{
			IEnumerable<SpecialAccommodation> enumerable = from g in allSpecialAccommodationRules
			where g.SpecialAccommodationType == SpecialAccommodationType.Extra_Time
			select g;
			if (enumerable.Count<SpecialAccommodation>() < 1)
			{
				return classDurationInMinutes;
			}
			List<int> list = new List<int>();
			using (IEnumerator<SpecialAccommodation> enumerator = enumerable.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					SpecialAccommodation rule = enumerator.Current;
					IEnumerable<AccommodationBasic> enumerable2 = from g in accommodationsToUse
					where g.ControlId == rule.ControlId
					select g;
					if (enumerable2.Count<AccommodationBasic>() > 0)
					{
						foreach (AccommodationBasic accommodationBasic in enumerable2)
						{
							string studentAccommodationCaptionAndValue = accommodationBasic.ControlCaptionAndValue ?? "";
							int num = Booker.CalculateExtraTime(classDurationInMinutes, rule, studentAccommodationCaptionAndValue);
							if (num > 0)
							{
								list.Add(num);
							}
						}
					}
				}
			}
			if (list.Count >= 1)
			{
				return list.Max((int f) => f);
			}
			return classDurationInMinutes;
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00007634 File Offset: 0x00005834
		private static int CalculateExtraTime(int classDurationInMinutes, SpecialAccommodation acc, string studentAccommodationCaptionAndValue)
		{
			double num = Booker.ExtractNumberFromString(studentAccommodationCaptionAndValue);
			return Booker.CalculateExtraTime(classDurationInMinutes, acc, num, studentAccommodationCaptionAndValue);
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00007654 File Offset: 0x00005854
		private static int CalculateExtraTime(int classDurationInMinutes, SpecialAccommodation acc, double num, string studentAccommodationCaptionAndValue)
		{
			string arg = acc.GetArg("flattimetext", "");
			bool flag = false;
			if (!string.IsNullOrEmpty(arg) && studentAccommodationCaptionAndValue.ToLower().IndexOf(arg.ToLower()) >= 0)
			{
				flag = true;
			}
			int result;
			if (flag)
			{
				result = classDurationInMinutes + Convert.ToInt32(num);
			}
			else
			{
				string arg2 = acc.GetArg("type", "0");
				double extraTimePercent = Accommodation.GetExtraTimePercent(num, arg2);
				result = ((extraTimePercent > 0.0) ? Accommodation.ApplyExtraTime(classDurationInMinutes, extraTimePercent) : 0);
			}
			return result;
		}

		// Token: 0x06000027 RID: 39 RVA: 0x000076D4 File Offset: 0x000058D4
		private static int CalculateExtraTime(Test classTest, Test targetTest, IList<Accommodation> accommodationsToUse, SpecialAccommodation acc, double num, Accommodation studentAccommodation)
		{
			return Booker.CalculateExtraTime(classTest.Duration, acc, num, studentAccommodation.Title + " * " + studentAccommodation.LookupText);
		}

		// Token: 0x06000028 RID: 40 RVA: 0x000076FC File Offset: 0x000058FC
		private static DataTable LoadRoomSchedules(IList<PotentialRoom> rooms, DateTime day, int ignoreAppointmentId, OperationContext opContext)
		{
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(eDatabaseConnectionStringName.ClockWork, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] array = new DbParameter[4];
			array[0] = databaseLayer.GetParameter("@pids", DbType.String, string.Join(",", PotentialRoom.GetRoomPids(rooms).ToList<int>().ConvertAll<string>((int g) => g.ToString()).ToArray()));
			array[1] = databaseLayer.GetParameter("@sdate", DbType.DateTime, day);
			array[2] = databaseLayer.GetParameter("@edate", DbType.DateTime, day.AddDays(1.0).AddMinutes(-1.0));
			array[3] = databaseLayer.GetParameter("@appid", DbType.Int32, ignoreAppointmentId);
			DbParameter[] parameters = array;
			return databaseLayer.ExecuteQuery("SELECT    app.appointmentid,app.startdate,app.enddate,app.cancelled,app.apptypeid,att.personid \r\nFROM        appointments app LEFT JOIN attendees att ON att.appointmentid=app.appointmentid \r\nWHERE       NOT app.appointmentid=@appid AND att.personid IN (SELECT orderid AS personid FROM splitorderids(@pids,',')) AND app.startdate>=@sdate AND app.enddate<=@edate AND app.cancelled=0 ORDER BY app.startdate", parameters);
		}

		// Token: 0x06000029 RID: 41 RVA: 0x000077E0 File Offset: 0x000059E0
		private static IList<PotentialRoom> FigureOutRequiredRoomsInOrder(int pid, int lucid, IList<Asset> assets, IList<Room> availableRooms, IList<Accommodation> accommodations)
		{
			List<PotentialRoom> list = new List<PotentialRoom>();
			foreach (Room room in availableRooms)
			{
				int score;
				bool flag = room.SupportsRequiredAssets(assets, out score);
				if (flag)
				{
					list.Add(new PotentialRoom(room, score));
				}
				if (flag)
				{
					CWLogger logger = CWLogger.Logger;
					string message = "Booker:FindPotentialBookings:FigureOutRequiredRooms:RoomAdded:room={0}:score={1}:requiredassets={2}";
					object arg = room.ToStringDebug();
					object arg2 = score.ToString();
					object arg3;
					if (assets != null)
					{
						arg3 = string.Join(" ** ", assets.ToList<Asset>().ConvertAll<string>((Asset asset) => asset.ToStringDebug()).ToArray());
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
						arg6 = string.Join(" ** ", assets.ToList<Asset>().ConvertAll<string>((Asset asset) => asset.ToStringDebug()).ToArray());
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

		// Token: 0x0600002A RID: 42 RVA: 0x0000791C File Offset: 0x00005B1C
		private static int SortPotentialRoomFunction(PotentialRoom pr1, PotentialRoom pr2)
		{
			if (pr1.Score == pr2.Score && pr1.Room.PriorityNumber == pr2.Room.PriorityNumber)
			{
				return 0;
			}
			if (pr1.Score < pr2.Score || pr1.Room.PriorityNumber < pr2.Room.PriorityNumber)
			{
				return -1;
			}
			return 1;
		}

		// Token: 0x0600002B RID: 43 RVA: 0x0000797C File Offset: 0x00005B7C
		private static List<Asset> FigureOutRequiredAssets(int pid, int lucid, IList<Asset> availableAssets, IList<Accommodation> accommodations)
		{
			List<Asset> list = new List<Asset>();
			using (IEnumerator<Accommodation> enumerator = accommodations.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					Accommodation acc = enumerator.Current;
					bool flag = false;
					int cLevel = acc.Level;
					while (!flag && cLevel > 0)
					{
						foreach (Asset asset in availableAssets)
						{
							string accTitle = acc.Title + acc.SubText;
							if (asset.AccommodationsSupported.FirstOrDefault((Accommodation am) => am.ControlId == acc.ControlId && (string.IsNullOrEmpty(am.SubText) || (!string.IsNullOrEmpty(am.SubText) && accTitle.IndexOf(am.SubText, StringComparison.OrdinalIgnoreCase) >= 0)) && am.Level == cLevel) != null)
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

		// Token: 0x0600002C RID: 44 RVA: 0x00007AA8 File Offset: 0x00005CA8
		private static DataTable LoadStudentSchedule(int pid, DateTime day, int appIdToIgnoreWhenCheckingStudentsSchedule, OperationContext opContext)
		{
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(eDatabaseConnectionStringName.ClockWork, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@pids", DbType.String, pid.ToString()),
				databaseLayer.GetParameter("@sdate", DbType.DateTime, day),
				databaseLayer.GetParameter("@edate", DbType.DateTime, day.AddDays(1.0).AddMinutes(-1.0)),
				databaseLayer.GetParameter("@appid", DbType.Int32, appIdToIgnoreWhenCheckingStudentsSchedule)
			};
			return databaseLayer.ExecuteQuery("SELECT    app.appointmentid,app.startdate,app.enddate,app.cancelled,app.apptypeid,att.personid \r\nFROM        appointments app LEFT JOIN attendees att ON att.appointmentid=app.appointmentid \r\nWHERE       NOT app.appointmentid=@appid AND att.personid IN (SELECT orderid AS personid FROM splitorderids(@pids,',')) AND app.startdate>=@sdate AND app.enddate<=@edate AND app.cancelled=0 ORDER BY app.startdate", parameters);
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00007B54 File Offset: 0x00005D54
		private static List<PotentialTest> ExtractBookingsToPresentToStudent(List<PotentialTest> bookings)
		{
			bookings.Sort();
			List<PotentialTest> list = new List<PotentialTest>(bookings.Count);
			PotentialTest potentialTest = null;
			foreach (PotentialTest potentialTest2 in bookings)
			{
				if (potentialTest == null || !potentialTest.Test.SameTime(potentialTest2.Test))
				{
					list.Add(potentialTest2);
					potentialTest = potentialTest2;
				}
			}
			return list;
		}
	}
}
