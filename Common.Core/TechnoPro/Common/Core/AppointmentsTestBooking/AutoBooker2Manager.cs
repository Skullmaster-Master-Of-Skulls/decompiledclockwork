using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using NewBooker.Entities.AutoTestBooking.Booker2;
using TechnoPro.Common.Core.Adapters;
using TechnoPro.Common.Core.Appointments;
using TechnoPro.Common.Core.AvailabilitySchedule;
using TechnoPro.Common.Core.CourseRegistrations;
using TechnoPro.Common.DAO.AutoTestBooking;
using TechnoPro.Common.ICore.Appointments;
using TechnoPro.Common.ICore.AvailabilitySchedule;
using TechnoPro.Common.ICore.CourseRegistrations;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Adapters;
using TechnoPro.Common.Public.Entities.Appointments;
using TechnoPro.Common.Public.Entities.AvailabilitySchedule;
using TechnoPro.Common.Public.Entities.CourseRegistrations;
using TechnoPro.Common.Public.Entities.LookupCourses;

namespace TechnoPro.Common.Core.AppointmentsTestBooking
{
	// Token: 0x0200013A RID: 314
	public class AutoBooker2Manager
	{
		// Token: 0x170001E8 RID: 488
		// (get) Token: 0x06000D95 RID: 3477 RVA: 0x00062361 File Offset: 0x00060561
		// (set) Token: 0x06000D96 RID: 3478 RVA: 0x00062369 File Offset: 0x00060569
		public OperationContext OpContext { get; set; }

		// Token: 0x06000D97 RID: 3479 RVA: 0x00062372 File Offset: 0x00060572
		public AutoBooker2Manager(OperationContext opContext)
		{
			this.OpContext = opContext;
			this.dao = new AutoBooker2DAO(opContext);
		}

		// Token: 0x06000D98 RID: 3480 RVA: 0x00062390 File Offset: 0x00060590
		public TryToBookResult TryToBookTest(TryToBookContext context, TryToBookSearchOptions searchOptions, TryToBookEnvironment environment)
		{
			TryToBookWorking tryToBookWorking = new TryToBookWorking
			{
				Caches = new TryToBookCaches(),
				Context = context,
				SearchOptions = searchOptions,
				Environment = environment
			};
			List<TryToBookFailure> list = new List<TryToBookFailure>();
			List<TryToBookWarning> list2 = new List<TryToBookWarning>();
			List<string> list3 = new List<string>();
			List<string> list4 = (list3.Count == 0) ? new List<string>() : null;
			TryToBookResult tryToBookResult = new TryToBookResult
			{
				Failures = list,
				Warnings = list2,
				Messages = list3,
				DebuggingLogItems = list4
			};
			tryToBookWorking.Result = tryToBookResult;
			bool flag = environment.AllRooms.Count < 1;
			if (flag)
			{
				tryToBookWorking.Result.Messages.Add("No rooms available (in environment.AllRooms)");
			}
			bool flag2 = searchOptions.RestrictRoomByCampusEnabled && !string.IsNullOrEmpty(context.CourseCampus);
			if (flag2)
			{
				bool flag3 = list4 != null;
				if (flag3)
				{
					list4.Add("Restricting by campus enabled; context.CourseCampus=" + context.CourseCampus);
				}
				Func<string, bool> <>9__3;
				List<TryToBookRoom> list5 = environment.AllRooms.Where(delegate(TryToBookRoom g)
				{
					bool result2;
					if (g.Campuses != null)
					{
						IEnumerable<string> campuses = g.Campuses;
						Func<string, bool> predicate;
						if ((predicate = <>9__3) == null)
						{
							predicate = (<>9__3 = ((string c) => context.CourseCampus.Equals(c, StringComparison.OrdinalIgnoreCase)));
						}
						result2 = campuses.Any(predicate);
					}
					else
					{
						result2 = false;
					}
					return result2;
				}).ToList<TryToBookRoom>();
				bool flag4 = list5.Count < 1;
				if (flag4)
				{
					tryToBookWorking.Result.Messages.Add("No rooms - rooms were restricted by campus; rooms before campus filter=" + string.Join(",", (from g in environment.AllRooms
					select g.PersonId.ToString()).ToArray<string>()));
				}
				tryToBookWorking.AllRooms = list5;
			}
			else
			{
				tryToBookWorking.AllRooms = environment.AllRooms;
			}
			bool flag5 = list4 != null;
			if (flag5)
			{
				List<string> list6 = list4;
				string str = "working.AllRooms=";
				string str2;
				if (tryToBookWorking.AllRooms != null)
				{
					str2 = string.Join(", ", (from g in tryToBookWorking.AllRooms
					select g.PersonId.ToString()).ToArray<string>());
				}
				else
				{
					str2 = "NULL";
				}
				list6.Add(str + str2);
			}
			this.SetupSpecialAccommodationActionsRequired(tryToBookWorking);
			bool flag6 = !tryToBookWorking.SearchOptions.AllowStudentsToBookSameCourseSameDay;
			if (flag6)
			{
				bool flag7 = this.dao.DoesStudentHaveAnExistingTestWithClassDateMatching(context.PersonId, context.LuCourseId, context.ClassTestDate);
				if (flag7)
				{
					bool flag8 = list4 != null;
					if (flag8)
					{
						list4.Add(string.Concat(new string[]
						{
							"Student already has another test booked for same day and course:day=",
							context.ClassTestDate.ToString("yyyy-MM-dd"),
							"; lucid=",
							context.LuCourseId.ToString(),
							"; pid=",
							context.PersonId.ToString()
						}));
					}
					tryToBookResult.StudentAlreadyHadAnotherTestBookedForSameDayAndCourse = true;
					list.Add(new TryToBookFailure
					{
						Type = eTryToBookFailureType.StudentAlreadyBookedATestForThisClassDateTime
					});
					return tryToBookResult;
				}
			}
			bool flag9 = this.IsHoliday(tryToBookWorking.Context.ClassTestDate.Date, tryToBookWorking.Caches.Holidays);
			bool flag10 = flag9;
			TryToBookResult result;
			if (flag10)
			{
				bool flag11 = list4 != null;
				if (flag11)
				{
					list4.Add("Class test is a holiday; date=" + tryToBookWorking.Context.ClassTestDate.Date.ToString("yyyy-MM-dd"));
				}
				list.Add(new TryToBookFailure
				{
					Type = eTryToBookFailureType.ClassTestIsAHoliday
				});
				result = tryToBookResult;
			}
			else
			{
				bool flag12 = !tryToBookWorking.SearchOptions.AllowToBookWithoutAnyAccommodations && (tryToBookWorking.Context.AccommodationsToUse == null || tryToBookWorking.Context.AccommodationsToUse.Count < 1);
				if (flag12)
				{
					bool flag13 = list4 != null;
					if (flag13)
					{
						list4.Add("No accommodations");
					}
					list.Add(new TryToBookFailure
					{
						Type = eTryToBookFailureType.NoAccommodationsToUse
					});
					result = tryToBookResult;
				}
				else
				{
					bool flag14 = tryToBookWorking.SpecialAccommodationActionsRequired.ContainsKey(eSpecialAccommodationApplyMethod.OnInitialization);
					if (flag14)
					{
						foreach (SpecialAccommodationReq specialAccommodationReq in tryToBookWorking.SpecialAccommodationActionsRequired[eSpecialAccommodationApplyMethod.OnInitialization])
						{
							bool flag15 = specialAccommodationReq.SpecialAccommodationsToApply.Count > 0;
							if (flag15)
							{
								specialAccommodationReq.Working = tryToBookWorking;
								SpecialAccommodationRes specialAccommodationRes = specialAccommodationReq.Func(specialAccommodationReq);
								bool abortFindPotentialBookingsProcess = specialAccommodationRes.AbortFindPotentialBookingsProcess;
								if (abortFindPotentialBookingsProcess)
								{
									return tryToBookResult;
								}
							}
						}
					}
					bool flag16 = tryToBookWorking.StudentTestDuration < 1;
					if (flag16)
					{
						tryToBookWorking.StudentTestDuration = tryToBookWorking.Context.ClassTestMinutes;
					}
					bool flag17 = list4 != null;
					if (flag17)
					{
						list4.Add("working.StudentTestDuration=" + tryToBookWorking.StudentTestDuration.ToString());
					}
					List<TryToBookPotentialBooking> list7 = new List<TryToBookPotentialBooking>();
					bool flag18 = tryToBookWorking.Context.AccommodationsToUse != null && tryToBookWorking.Context.AccommodationsToUse.Count > 0;
					if (flag18)
					{
						IDictionary<int, IList<string>> levelsWithAssets = this.GetLevelsWithAssets(tryToBookWorking.Context.AccommodationsToUse, tryToBookWorking.Environment.AllAssets);
						foreach (KeyValuePair<int, IList<string>> keyValuePair in levelsWithAssets)
						{
							int key = keyValuePair.Key;
							tryToBookWorking.AssetIdsRequired = keyValuePair.Value;
							bool flag19 = list4 != null;
							if (flag19)
							{
								list4.Add("Checking new level with assets:level=" + key.ToString() + "; working.AssetIdsRequired=" + string.Join(", ", tryToBookWorking.AssetIdsRequired.ToArray<string>()));
							}
							IList<TryToBookPotentialBooking> potentialBookingsToAdd = this.TryToBookTestForSingleAssetLevel(key, tryToBookWorking, environment, searchOptions);
							bool flag20 = !this.AddBookingsToList(potentialBookingsToAdd, list7, tryToBookWorking);
							if (flag20)
							{
								break;
							}
						}
					}
					else
					{
						list2.Add(new TryToBookWarning
						{
							Type = eTryToBookWarningType.NoAccommodationsToUse
						});
						tryToBookWorking.AssetIdsRequired = new List<string>();
						IList<TryToBookPotentialBooking> list8 = this.TryToBookTestForSingleAssetLevel(1, tryToBookWorking, environment, searchOptions);
						bool flag21 = list8 != null;
						if (flag21)
						{
							list7.AddRange(list8);
						}
					}
					tryToBookResult.PotentialBookings = list7;
					bool flag22 = tryToBookResult.PotentialBookings.Count > 0;
					if (flag22)
					{
						List<TryToBookSpecialAccommodation> list9 = (from g in tryToBookWorking.SpecialAccommodationsRequired
						where g.Type == eSpecialAccommodationType.AddIcon
						select g).ToList<TryToBookSpecialAccommodation>();
						bool flag23 = list9.Count > 0;
						if (flag23)
						{
							tryToBookResult.IconIdsToBookWith = new List<int>();
							foreach (TryToBookSpecialAccommodation tryToBookSpecialAccommodation in list9)
							{
								int num = this.ExtractIntFromArgs(tryToBookSpecialAccommodation.Args, "iconnum", -1);
								bool flag24 = num >= 0 && !tryToBookResult.IconIdsToBookWith.Contains(num);
								if (flag24)
								{
									tryToBookResult.IconIdsToBookWith.Add(num);
								}
							}
						}
					}
					result = tryToBookResult;
				}
			}
			return result;
		}

		// Token: 0x06000D99 RID: 3481 RVA: 0x00062AF0 File Offset: 0x00060CF0
		private static bool AreDateTimesEqual(DateTime dt1, DateTime dt2)
		{
			return dt1.Date == dt2.Date && dt1.Hour == dt2.Hour && dt1.Minute == dt2.Minute;
		}

		// Token: 0x06000D9A RID: 3482 RVA: 0x00062B3C File Offset: 0x00060D3C
		private IList<TryToBookAvailability> SubtractAvailability(TryToBookAvailability availability, DateTime sdtToSubtract, DateTime edtToSubtract)
		{
			bool flag = AutoBooker2Manager.AreDateTimesEqual(sdtToSubtract, availability.StartDateTime) && AutoBooker2Manager.AreDateTimesEqual(edtToSubtract, availability.EndDateTime);
			IList<TryToBookAvailability> result;
			if (flag)
			{
				result = new List<TryToBookAvailability>();
			}
			else
			{
				bool flag2 = sdtToSubtract >= availability.EndDateTime || edtToSubtract <= availability.StartDateTime;
				if (flag2)
				{
					result = new List<TryToBookAvailability>
					{
						availability
					};
				}
				else
				{
					bool flag3 = sdtToSubtract > availability.StartDateTime && edtToSubtract < availability.EndDateTime;
					if (flag3)
					{
						result = new List<TryToBookAvailability>
						{
							new TryToBookAvailability
							{
								StartDateTime = availability.StartDateTime,
								EndDateTime = sdtToSubtract
							},
							new TryToBookAvailability
							{
								StartDateTime = edtToSubtract,
								EndDateTime = availability.EndDateTime
							}
						};
					}
					else
					{
						bool flag4 = sdtToSubtract > availability.StartDateTime;
						if (flag4)
						{
							result = new List<TryToBookAvailability>
							{
								new TryToBookAvailability
								{
									StartDateTime = availability.StartDateTime,
									EndDateTime = sdtToSubtract
								}
							};
						}
						else
						{
							result = new List<TryToBookAvailability>
							{
								new TryToBookAvailability
								{
									StartDateTime = edtToSubtract,
									EndDateTime = availability.EndDateTime
								}
							};
						}
					}
				}
			}
			return result;
		}

		// Token: 0x06000D9B RID: 3483 RVA: 0x00062C7C File Offset: 0x00060E7C
		private IList<TryToBookAvailability> ReduceAvailabilityByScheduledAppointments(TryToBookAvailability availability, IList<TryToBookAvailability> apps)
		{
			List<TryToBookAvailability> list = new List<TryToBookAvailability>
			{
				new TryToBookAvailability
				{
					StartDateTime = availability.StartDateTime,
					EndDateTime = availability.EndDateTime
				}
			};
			foreach (TryToBookAvailability tryToBookAvailability in apps)
			{
				List<TryToBookAvailability> list2 = new List<TryToBookAvailability>();
				List<TryToBookAvailability> list3 = new List<TryToBookAvailability>();
				foreach (TryToBookAvailability tryToBookAvailability2 in list)
				{
					bool flag = !(tryToBookAvailability.EndDateTime <= tryToBookAvailability2.StartDateTime) && !(tryToBookAvailability.StartDateTime >= tryToBookAvailability2.EndDateTime);
					bool flag2 = flag;
					if (flag2)
					{
						list2.Add(tryToBookAvailability2);
					}
					else
					{
						list3.Add(tryToBookAvailability2);
					}
				}
				bool flag3 = list2.Count > 0;
				if (flag3)
				{
					List<TryToBookAvailability> list4 = new List<TryToBookAvailability>();
					foreach (TryToBookAvailability availability2 in list2)
					{
						IList<TryToBookAvailability> list5 = this.SubtractAvailability(availability2, tryToBookAvailability.StartDateTime, tryToBookAvailability.EndDateTime);
						foreach (TryToBookAvailability item in list5)
						{
							list4.Add(item);
						}
					}
					list = list4;
					list.AddRange(list3);
				}
			}
			return list;
		}

		// Token: 0x06000D9C RID: 3484 RVA: 0x00062E7C File Offset: 0x0006107C
		private IList<TryToBookAvailability> ReduceAvailabilityUsingScheduledNonCancelledAppointments(IList<TryToBookAvailability> availability, IList<TryToBookAvailability> scheduledNonCancelledAppointments, int bufferMinutesPre, int bufferMinutesPost)
		{
			IList<TryToBookAvailability> list2;
			if (bufferMinutesPre <= 0 && bufferMinutesPost <= 0)
			{
				IList<TryToBookAvailability> list = (from g in scheduledNonCancelledAppointments
				select new TryToBookAvailability
				{
					StartDateTime = g.StartDateTime.AddMinutes((double)(-(double)bufferMinutesPre)),
					EndDateTime = g.EndDateTime.AddMinutes((double)bufferMinutesPost)
				}).ToList<TryToBookAvailability>();
				list2 = list;
			}
			else
			{
				list2 = scheduledNonCancelledAppointments;
			}
			IList<TryToBookAvailability> apps = list2;
			List<TryToBookAvailability> list3 = new List<TryToBookAvailability>();
			foreach (TryToBookAvailability availability2 in availability)
			{
				IList<TryToBookAvailability> list4 = this.ReduceAvailabilityByScheduledAppointments(availability2, apps);
				bool flag = list4 != null;
				if (flag)
				{
					list3.AddRange(list4);
				}
			}
			return list3;
		}

		// Token: 0x06000D9D RID: 3485 RVA: 0x00062F38 File Offset: 0x00061138
		private IList<TryToBookAvailability> LoadRoomAvailability(int availabilityGroupId, int roomPersonId, DateTime dt)
		{
			IAvailabilityScheduleManager availabilityScheduleManager = new AvailabilityScheduleManager(this.OpContext);
			AvailabilityScheduleContext context = new AvailabilityScheduleContext
			{
				AvailabilityGroupId = availabilityGroupId,
				PersonId = roomPersonId
			};
			AvailabilityScheduleItemsForContext availabilityScheduleItemsForContext = availabilityScheduleManager.LoadAvailabilityItemsByContextAndDateRange(context, dt.Date, 1);
			return availabilityScheduleItemsForContext.AvailabilityScheduleItems.Select(delegate(AvailabilityScheduleItemInfo g)
			{
				DateTime date = g.DayAndTime.Date;
				return new TryToBookAvailability
				{
					StartDateTime = date.Add(g.DayAndTime.Time.StartTime),
					EndDateTime = date.Add(g.DayAndTime.Time.EndTime)
				};
			}).ToList<TryToBookAvailability>();
		}

		// Token: 0x06000D9E RID: 3486 RVA: 0x00062FAC File Offset: 0x000611AC
		private IList<TryToBookAvailability> LoadApps(int pid, DateTime dt)
		{
			IBaseAppointmentManager baseAppointmentManager = new BaseAppointmentManager(this.OpContext);
			IList<BaseBasicAppointment> source = baseAppointmentManager.LoadBaseBasicAppointmentsByPersonAndDateRange(pid, true, dt.Date, dt.Date);
			return (from g in source
			select new TryToBookAvailability
			{
				StartDateTime = g.StartDateTime,
				EndDateTime = g.EndDateTime
			}).ToList<TryToBookAvailability>();
		}

		// Token: 0x06000D9F RID: 3487 RVA: 0x0006300C File Offset: 0x0006120C
		private TryToBookSchedule GetRoomSchedule(TryToBookRoom room, DateTime dt, IList<TryToBookSchedule> roomScheduleCache, IDictionary<int, IList<int>> roomAvailabilityScheduleMappings, int bufferMinutesPre, int bufferMinutesPost)
		{
			int roomPid = room.PersonId;
			KeyValuePair<int, IList<int>> keyValuePair = roomAvailabilityScheduleMappings.FirstOrDefault((KeyValuePair<int, IList<int>> g) => g.Value.Contains(roomPid));
			int roomPersonId = (keyValuePair.Key > 0) ? keyValuePair.Key : roomPid;
			TryToBookSchedule tryToBookSchedule = roomScheduleCache.FirstOrDefault((TryToBookSchedule g) => g.RoomPersonId == roomPid && g.Date == dt);
			bool flag = tryToBookSchedule == null;
			if (flag)
			{
				IList<TryToBookAvailability> list = this.LoadRoomAvailability(2, roomPersonId, dt);
				IList<TryToBookAvailability> list2 = this.LoadApps(roomPid, dt);
				foreach (TryToBookAvailability tryToBookAvailability in list2)
				{
					bool flag2 = bufferMinutesPre > 0;
					if (flag2)
					{
						tryToBookAvailability.StartDateTime = tryToBookAvailability.StartDateTime.AddMinutes((double)(-(double)bufferMinutesPre));
					}
					bool flag3 = bufferMinutesPost > 0;
					if (flag3)
					{
						tryToBookAvailability.EndDateTime = tryToBookAvailability.EndDateTime.AddMinutes((double)bufferMinutesPost);
					}
				}
				tryToBookSchedule = new TryToBookSchedule
				{
					RoomPersonId = roomPid,
					Date = dt,
					JustAvailability = new List<TryToBookAvailability>(list)
				};
				IList<TryToBookAvailability> availability = this.ReduceAvailabilityUsingScheduledNonCancelledAppointments(list, list2, bufferMinutesPre, bufferMinutesPost);
				tryToBookSchedule.Availability = availability;
				roomScheduleCache.Add(tryToBookSchedule);
			}
			return tryToBookSchedule;
		}

		// Token: 0x06000DA0 RID: 3488 RVA: 0x0006318C File Offset: 0x0006138C
		private IList<TryToBookAvailability> GetRoomAvailability(TryToBookRoom room, DateTime dt, IList<TryToBookSchedule> roomScheduleCache, IDictionary<int, IList<int>> roomAvailabilityScheduleMappings, int bufferMinutesPre, int bufferMinutesPost)
		{
			TryToBookSchedule roomSchedule = this.GetRoomSchedule(room, dt, roomScheduleCache, roomAvailabilityScheduleMappings, bufferMinutesPre, bufferMinutesPost);
			return (room.RoomType == eRoomType.RegularRoom) ? roomSchedule.Availability : roomSchedule.JustAvailability;
		}

		// Token: 0x06000DA1 RID: 3489 RVA: 0x000631C8 File Offset: 0x000613C8
		private bool IsRoomAvailable(TryToBookRoom room, DateTime StartDateTime, DateTime EndDateTime, IList<TryToBookSchedule> roomScheduleCache, IDictionary<int, IList<int>> roomAvailabilityScheduleMappings, int bufferMinutesPre, int bufferMinutesPost)
		{
			DateTime date = StartDateTime.Date;
			IList<TryToBookAvailability> roomAvailability = this.GetRoomAvailability(room, date, roomScheduleCache, roomAvailabilityScheduleMappings, bufferMinutesPre, bufferMinutesPost);
			TryToBookAvailability tryToBookAvailability = roomAvailability.FirstOrDefault((TryToBookAvailability g) => g.StartDateTime <= StartDateTime && g.EndDateTime >= EndDateTime);
			return tryToBookAvailability != null;
		}

		// Token: 0x06000DA2 RID: 3490 RVA: 0x00063224 File Offset: 0x00061424
		private int CalculateBreakTimeAdditionalMinutes(TryToBookSpecialAccommodation breakTimeSpecialAccommodation, TryToBookAccommodationToUse matchingAccommodation, IList<TryToBookAccommodationToUse> accommodationsToUse, int classTestDuration, int currentStudentTestDuration)
		{
			IDictionary<string, string> args = breakTimeSpecialAccommodation.Args;
			int num = this.ExtractIntFromArgs(args, "mintesttime", 0);
			int num2 = this.ExtractIntFromArgs(args, "max", 0);
			eBreakTimeType eBreakTimeType = this.ExtractEnumFromArgs<eBreakTimeType>(args, "type");
			int secondCid = this.ExtractIntFromArgs(args, "secondcid", 0);
			string text = this.ExtractStringFromArgs(args, "flattimetext");
			int num3 = this.ExtractIntFromArgs(args, "flattimeamount", 0);
			int ignoreIfCheckedControlId = this.ExtractIntFromArgs(args, "ignoreifcheckedcontrolid", 0);
			string ignoreIfCheckedValue = this.ExtractStringFromArgs(args, "ignoreifcheckedvalue");
			bool flag = num > 0 && currentStudentTestDuration > num;
			int result;
			if (flag)
			{
				result = 0;
			}
			else
			{
				bool flag2 = ignoreIfCheckedControlId > 0 && accommodationsToUse.FirstOrDefault((TryToBookAccommodationToUse g) => g.ControlId == ignoreIfCheckedControlId && (ignoreIfCheckedValue.Length < 1 || (g.Value ?? "").IndexOf(ignoreIfCheckedValue, StringComparison.OrdinalIgnoreCase) >= 0)) != null;
				if (flag2)
				{
					result = 0;
				}
				else
				{
					string text2 = matchingAccommodation.Caption + (matchingAccommodation.Value ?? "");
					bool flag3 = text.Length > 0 && text2.IndexOf(text, StringComparison.OrdinalIgnoreCase) >= 0;
					if (flag3)
					{
						result = num3;
					}
					else
					{
						double num4 = this.ExtractNumberFromText(text2);
						int num5 = 0;
						switch (eBreakTimeType)
						{
						case eBreakTimeType.MinutesPerHour:
						{
							double num6 = num4 / 60.0;
							num5 = Convert.ToInt32(Convert.ToDouble(currentStudentTestDuration) * num6);
							break;
						}
						case eBreakTimeType.Percent_1_33:
						{
							double num6 = num4 - 1.0;
							num5 = Convert.ToInt32(Convert.ToDouble(currentStudentTestDuration) * num6);
							break;
						}
						case eBreakTimeType.Percent_0_33:
						{
							double num6 = num4;
							num5 = Convert.ToInt32(Convert.ToDouble(currentStudentTestDuration) * num6);
							break;
						}
						case eBreakTimeType.Percent_33_0:
						{
							double num6 = num4 / 100.0;
							num5 = Convert.ToInt32(Convert.ToDouble(currentStudentTestDuration) * num6);
							break;
						}
						case eBreakTimeType.FlatTime:
						{
							bool flag4 = num3 > 0;
							if (flag4)
							{
								num5 = num3;
							}
							else
							{
								num5 = (int)num4;
							}
							break;
						}
						case eBreakTimeType.MinPerHourInTwoControls:
						{
							bool flag5 = secondCid > 0;
							if (flag5)
							{
								TryToBookAccommodationToUse tryToBookAccommodationToUse = accommodationsToUse.FirstOrDefault((TryToBookAccommodationToUse g) => g.ControlId == secondCid);
								bool flag6 = tryToBookAccommodationToUse != null;
								if (flag6)
								{
									string text3 = tryToBookAccommodationToUse.Caption + (tryToBookAccommodationToUse.Value ?? "");
									int num7 = string.IsNullOrEmpty(text) ? -1 : text3.IndexOf(text, StringComparison.OrdinalIgnoreCase);
									bool flag7 = num7 >= 0;
									if (flag7)
									{
										num5 = num3;
									}
									string value = Regex.Match(text3, "[0-9]+").Value;
									int num8;
									bool flag8 = int.TryParse(value, out num8);
									if (flag8)
									{
										bool flag9 = num4 > 0.0 && num8 > 0;
										if (flag9)
										{
											double num6 = Convert.ToDouble(num4) / Convert.ToDouble(num8);
											num5 = Convert.ToInt32((double)currentStudentTestDuration * num6);
										}
									}
								}
							}
							break;
						}
						case eBreakTimeType.Percent_133_0:
						{
							double num6 = (num4 - 100.0) / 100.0;
							num5 = Convert.ToInt32(Convert.ToDouble(currentStudentTestDuration) * num6);
							break;
						}
						}
						bool flag10 = num2 > 0 && num5 > num2;
						if (flag10)
						{
							num5 = num2;
						}
						result = num5;
					}
				}
			}
			return result;
		}

		// Token: 0x06000DA3 RID: 3491 RVA: 0x0006355C File Offset: 0x0006175C
		private int CalculateExtraTimeAdditionalMinutes(TryToBookSpecialAccommodation extraTimeSpecialAccommodation, TryToBookAccommodationToUse matchingAccommodation, IList<TryToBookAccommodationToUse> accommodationsToUse, int classTestDuration, int currentStudentTestDuration, out bool appliedFlatTime)
		{
			IDictionary<string, string> args = extraTimeSpecialAccommodation.Args;
			eExtraTimeType eExtraTimeType = this.ExtractEnumFromArgs<eExtraTimeType>(args, "type");
			string text = this.ExtractStringFromArgs(args, "flattimetext");
			int num = 2440;
			string text2 = matchingAccommodation.Caption + (matchingAccommodation.Value ?? "");
			bool flag = text.Length > 0 && text2.IndexOf(text, StringComparison.OrdinalIgnoreCase) >= 0;
			if (flag)
			{
				eExtraTimeType = eExtraTimeType.FlatTime;
			}
			double num2 = this.ExtractNumberFromText(text2);
			int num3 = 0;
			appliedFlatTime = (eExtraTimeType == eExtraTimeType.FlatTime);
			switch (eExtraTimeType)
			{
			case eExtraTimeType.MinutesPerHour:
			{
				double num4 = num2 / 60.0;
				num3 = Convert.ToInt32(Convert.ToDouble(classTestDuration) * num4);
				break;
			}
			case eExtraTimeType.Percent_1_33:
			{
				double num4 = num2 - 1.0;
				num3 = Convert.ToInt32(Convert.ToDouble(classTestDuration) * num4);
				break;
			}
			case eExtraTimeType.Percent_0_33:
			{
				double num4 = num2;
				num3 = Convert.ToInt32(Convert.ToDouble(classTestDuration) * num4);
				break;
			}
			case eExtraTimeType.Percent_33_0:
			{
				double num4 = num2 / 100.0;
				num3 = Convert.ToInt32(Convert.ToDouble(classTestDuration) * num4);
				break;
			}
			case eExtraTimeType.FlatTime:
				num3 = Convert.ToInt32(num2);
				break;
			case eExtraTimeType.Percent_133_0:
			{
				double num4 = (num2 - 100.0) / 100.0;
				num3 = Convert.ToInt32(Convert.ToDouble(classTestDuration) * num4);
				break;
			}
			}
			bool flag2 = num > 0 && num3 > num;
			if (flag2)
			{
				num3 = num;
			}
			return num3;
		}

		// Token: 0x06000DA4 RID: 3492 RVA: 0x000636E4 File Offset: 0x000618E4
		private IList<TryToBookRoom> GetRoomsToInvestigate(TryToBookRule rule, TryToBookWorking working)
		{
			IList<TryToBookRoom> list;
			switch (rule.RoomUsage)
			{
			case eTryToBookRuleRoomUsage.UseVirtualRoomsOnly:
				list = new List<TryToBookRoom>(working.AllVirtualRooms);
				break;
			case eTryToBookRuleRoomUsage.UseNonVirtualRoomsOnly:
				list = new List<TryToBookRoom>(working.AllNonVirtualRooms);
				break;
			case eTryToBookRuleRoomUsage.UseBothVirtualAndNonVirtualRooms:
				list = new List<TryToBookRoom>(working.Environment.AllRooms);
				break;
			default:
				working.Result.Messages.Add("No rooms are being used because rule.RoomUsage=" + rule.RoomUsage.ToString());
				list = new List<TryToBookRoom>();
				break;
			}
			bool flag = list.Count < 1;
			IList<TryToBookRoom> result;
			if (flag)
			{
				working.Result.Messages.Add("roomPool is empty");
				result = list;
			}
			else
			{
				IList<string> assetIdsRequired = working.AssetIdsRequired;
				bool flag2 = assetIdsRequired.Count < 1;
				if (flag2)
				{
					result = list;
				}
				else
				{
					List<AutoBooker2Manager.TryToBookRoomWithContextScore> list2 = new List<AutoBooker2Manager.TryToBookRoomWithContextScore>();
					int num = -1;
					using (IEnumerator<TryToBookRoom> enumerator = list.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							TryToBookRoom room = enumerator.Current;
							bool flag3 = room.OrderNum <= num;
							if (flag3)
							{
								room.OrderNum = num + 1;
							}
							num++;
							bool flag4 = list2.FirstOrDefault((AutoBooker2Manager.TryToBookRoomWithContextScore g) => g.Room.PersonId == room.PersonId) == null;
							if (flag4)
							{
								int num2 = (room.RoomType == eRoomType.SuperVirtualRoom) ? -100 : this.GetRoomScore(room, assetIdsRequired, working.Environment.AllAssets);
								bool flag5 = num2 != 0;
								if (flag5)
								{
									list2.Add(new AutoBooker2Manager.TryToBookRoomWithContextScore
									{
										Room = room,
										ContextScore = num2
									});
								}
							}
						}
					}
					list2.Sort(delegate(AutoBooker2Manager.TryToBookRoomWithContextScore g1, AutoBooker2Manager.TryToBookRoomWithContextScore g2)
					{
						int num3 = g2.ContextScore.CompareTo(g1.ContextScore);
						bool flag6 = num3 != 0;
						int result2;
						if (flag6)
						{
							result2 = num3;
						}
						else
						{
							result2 = g1.Room.OrderNum.CompareTo(g2.Room.OrderNum);
						}
						return result2;
					});
					result = (from g in list2
					select g.Room).ToList<TryToBookRoom>();
				}
			}
			return result;
		}

		// Token: 0x06000DA5 RID: 3493 RVA: 0x0006391C File Offset: 0x00061B1C
		private int GetRoomScore(TryToBookRoom room, IList<string> assetIdsRequired, IList<TryToBookAsset> allAssets)
		{
			bool flag = (from g in assetIdsRequired
			where room.AssetsSupported.FirstOrDefault((string h) => h.Equals(g, StringComparison.OrdinalIgnoreCase)) == null
			select g).Count<string>() > 0;
			int result;
			if (flag)
			{
				result = 0;
			}
			else
			{
				int num = -100;
				using (IEnumerator<string> enumerator = room.AssetsSupported.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						string assetId = enumerator.Current;
						TryToBookAsset tryToBookAsset = allAssets.FirstOrDefault((TryToBookAsset g) => g.Id.Equals(assetId, StringComparison.OrdinalIgnoreCase));
						bool flag2 = tryToBookAsset != null && assetIdsRequired.FirstOrDefault((string g) => g.Equals(assetId, StringComparison.OrdinalIgnoreCase)) == null;
						if (flag2)
						{
							num -= tryToBookAsset.Score;
						}
					}
				}
				result = num;
			}
			return result;
		}

		// Token: 0x06000DA6 RID: 3494 RVA: 0x000639FC File Offset: 0x00061BFC
		private void SetupSpecialAccommodationActionsRequired(TryToBookWorking working)
		{
			bool ignoreSpecialAccommodations = working.SearchOptions.IgnoreSpecialAccommodations;
			if (ignoreSpecialAccommodations)
			{
				working.SpecialAccommodationActionsRequired = new Dictionary<eSpecialAccommodationApplyMethod, List<SpecialAccommodationReq>>();
				working.SpecialAccommodationsRequired = new List<TryToBookSpecialAccommodation>();
			}
			else
			{
				List<TryToBookSpecialAccommodation> list = (from g in working.Environment.AllSpecialAccommodations
				where working.Context.AccommodationsToUse.FirstOrDefault((TryToBookAccommodationToUse h) => h.ControlId < 1 || h.ControlId == g.ControlId) != null
				select g).ToList<TryToBookSpecialAccommodation>();
				List<TryToBookSpecialAccommodation> collection = (from g in working.Environment.AllSpecialAccommodations
				where g.ControlId == 0
				select g).ToList<TryToBookSpecialAccommodation>();
				list.AddRange(collection);
				working.SpecialAccommodationsRequired = list;
				list.Sort(delegate(TryToBookSpecialAccommodation g1, TryToBookSpecialAccommodation g2)
				{
					int num = SpecialAccommodationTypeAttribute.GetAttribute(g1.Type).OrderNum.CompareTo(SpecialAccommodationTypeAttribute.GetAttribute(g2.Type).OrderNum);
					bool flag3 = num != 0;
					int result;
					if (flag3)
					{
						result = num;
					}
					else
					{
						result = g1.Type.CompareTo(g2.Type);
					}
					return result;
				});
				working.SpecialAccommodationActionsRequired = new Dictionary<eSpecialAccommodationApplyMethod, List<SpecialAccommodationReq>>();
				Array values = Enum.GetValues(typeof(eSpecialAccommodationApplyMethod));
				using (IEnumerator enumerator = values.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						eSpecialAccommodationApplyMethod applyMethod = (eSpecialAccommodationApplyMethod)enumerator.Current;
						List<SpecialAccommodationReq> list2 = new List<SpecialAccommodationReq>();
						List<TryToBookSpecialAccommodation> list3 = (from g in list
						where SpecialAccommodationTypeAttribute.GetAttribute(g.Type).ApplyMethod == applyMethod
						select g).ToList<TryToBookSpecialAccommodation>();
						int j;
						for (int i = 0; i < list3.Count; i = j)
						{
							eSpecialAccommodationType type = list3[i].Type;
							for (j = i + 1; j < list3.Count; j++)
							{
								eSpecialAccommodationType type2 = list3[j].Type;
								bool flag = type2 != type;
								if (flag)
								{
									break;
								}
							}
							List<TryToBookSpecialAccommodation> list4 = new List<TryToBookSpecialAccommodation>();
							for (int k = i; k < j; k++)
							{
								list4.Add(list3[k]);
							}
							SpecialAccommodationReq specialAccommodationReq = new SpecialAccommodationReq
							{
								SpecialAccommodationsToApply = list4
							};
							eSpecialAccommodationType eSpecialAccommodationType = type;
							eSpecialAccommodationType eSpecialAccommodationType2 = eSpecialAccommodationType;
							if (eSpecialAccommodationType2 <= eSpecialAccommodationType.TimeOfDay)
							{
								if (eSpecialAccommodationType2 <= eSpecialAccommodationType.Breaks)
								{
									if (eSpecialAccommodationType2 != eSpecialAccommodationType.Extra_Time)
									{
										if (eSpecialAccommodationType2 == eSpecialAccommodationType.Breaks)
										{
											specialAccommodationReq.Func = new Func<SpecialAccommodationReq, SpecialAccommodationRes>(this.ApplyBreakTime);
										}
									}
									else
									{
										specialAccommodationReq.Func = new Func<SpecialAccommodationReq, SpecialAccommodationRes>(this.ApplyExtraTime);
									}
								}
								else if (eSpecialAccommodationType2 != eSpecialAccommodationType.EmailCoordinator)
								{
									if (eSpecialAccommodationType2 == eSpecialAccommodationType.TimeOfDay)
									{
										specialAccommodationReq.Func = new Func<SpecialAccommodationReq, SpecialAccommodationRes>(this.ApplyTimeOfDay);
									}
								}
								else
								{
									specialAccommodationReq.Func = new Func<SpecialAccommodationReq, SpecialAccommodationRes>(this.ApplyEmailCoordinator);
								}
							}
							else if (eSpecialAccommodationType2 <= eSpecialAccommodationType.DaysRest)
							{
								if (eSpecialAccommodationType2 != eSpecialAccommodationType.MaxPerDay)
								{
									if (eSpecialAccommodationType2 == eSpecialAccommodationType.DaysRest)
									{
										specialAccommodationReq.Func = new Func<SpecialAccommodationReq, SpecialAccommodationRes>(this.ApplyDaysRest);
									}
								}
								else
								{
									specialAccommodationReq.Func = new Func<SpecialAccommodationReq, SpecialAccommodationRes>(this.ApplyMaxPerDay);
								}
							}
							else if (eSpecialAccommodationType2 != eSpecialAccommodationType.StartEndOfDaySlide)
							{
								if (eSpecialAccommodationType2 == eSpecialAccommodationType.SnapTime)
								{
									specialAccommodationReq.Func = new Func<SpecialAccommodationReq, SpecialAccommodationRes>(this.ApplySnapTime);
								}
							}
							else
							{
								specialAccommodationReq.Func = new Func<SpecialAccommodationReq, SpecialAccommodationRes>(this.ApplyStartEndOfDaySlide);
							}
							list2.Add(specialAccommodationReq);
						}
						bool flag2 = list2.Count > 0;
						if (flag2)
						{
							working.SpecialAccommodationActionsRequired.Add(applyMethod, list2);
						}
					}
				}
			}
			working.SpecialAccommodationActionsRequired.Add(eSpecialAccommodationApplyMethod.AfterPotentialBookingFound, new List<SpecialAccommodationReq>
			{
				new SpecialAccommodationReq
				{
					SpecialAccommodationsToApply = new List<TryToBookSpecialAccommodation>(),
					Func = new Func<SpecialAccommodationReq, SpecialAccommodationRes>(this.ApplyRoundStartTime)
				}
			});
		}

		// Token: 0x06000DA7 RID: 3495 RVA: 0x00063E10 File Offset: 0x00062010
		private SpecialAccommodationRes ApplyRoundStartTime(SpecialAccommodationReq req)
		{
			DateTime startDateTime = req.PotentialBookingToAdd.StartDateTime;
			DateTime endDateTime = req.PotentialBookingToAdd.EndDateTime;
			int num = (int)(endDateTime - startDateTime).TotalMinutes;
			int num2 = this.RoundDuration(num);
			bool flag = num != num2;
			SpecialAccommodationRes result;
			if (flag)
			{
				DateTime endDateTime2 = startDateTime.AddMinutes((double)num2);
				req.PotentialBookingToAdd.EndDateTime = endDateTime2;
				result = new SpecialAccommodationRes
				{
					PotentialBookingToAdd = req.PotentialBookingToAdd
				};
			}
			else
			{
				result = new SpecialAccommodationRes
				{
					PotentialBookingToAdd = req.PotentialBookingToAdd
				};
			}
			return result;
		}

		// Token: 0x06000DA8 RID: 3496 RVA: 0x00063EA8 File Offset: 0x000620A8
		private int RoundDuration(int duration)
		{
			int num = duration % 5;
			bool flag = num == 0;
			int result;
			if (flag)
			{
				result = duration;
			}
			else
			{
				result = (Convert.ToInt32(duration / 5) + 1) * 5;
			}
			return result;
		}

		// Token: 0x06000DA9 RID: 3497 RVA: 0x00063ED8 File Offset: 0x000620D8
		private void UpdateNotices(TryToBookWorking working, DateTime sdtOld, DateTime sdtNew)
		{
			IDictionary<DateTime, IList<string>> noticesCache = working.Caches.NoticesCache;
			bool flag = noticesCache.ContainsKey(sdtOld);
			if (flag)
			{
				IList<string> list = noticesCache[sdtOld];
				noticesCache.Remove(sdtOld);
				bool flag2 = noticesCache.ContainsKey(sdtNew);
				if (flag2)
				{
					foreach (string item in list)
					{
						noticesCache[sdtNew].Add(item);
					}
				}
				else
				{
					noticesCache.Add(sdtNew, list);
				}
			}
		}

		// Token: 0x06000DAA RID: 3498 RVA: 0x00063F70 File Offset: 0x00062170
		private void AddNotice(TryToBookWorking working, DateTime sdt, string notice)
		{
			bool flag = working.Caches.NoticesCache.ContainsKey(sdt);
			if (flag)
			{
				IList<string> list = working.Caches.NoticesCache[sdt];
				list.Add(notice);
			}
			else
			{
				working.Caches.NoticesCache.Add(sdt, new List<string>
				{
					notice
				});
			}
		}

		// Token: 0x06000DAB RID: 3499 RVA: 0x00063FD0 File Offset: 0x000621D0
		private IList<DateTime> LoadHolidayDates(DateTime startDate, int numDays)
		{
			return new List<DateTime>();
		}

		// Token: 0x06000DAC RID: 3500 RVA: 0x00063FE8 File Offset: 0x000621E8
		private bool IsHoliday(DateTime targetDate, IDictionary<DateTime, bool> holidaysCache)
		{
			bool flag = holidaysCache.ContainsKey(targetDate);
			bool result;
			if (flag)
			{
				result = holidaysCache[targetDate];
			}
			else
			{
				DateTime startDate = targetDate.AddDays(-7.0);
				IList<DateTime> list = this.LoadHolidayDates(startDate, 14);
				for (int i = 0; i < 14; i++)
				{
					DateTime dateTime = startDate.AddDays((double)i);
					bool flag2 = !holidaysCache.ContainsKey(dateTime);
					if (flag2)
					{
						holidaysCache.Add(dateTime, list.Contains(dateTime));
					}
				}
				result = (holidaysCache.ContainsKey(targetDate) && holidaysCache[targetDate]);
			}
			return result;
		}

		// Token: 0x06000DAD RID: 3501 RVA: 0x00064084 File Offset: 0x00062284
		private double ExtractNumberFromText(string s)
		{
			int num = -1;
			int num2 = -1;
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = s.Length - 1; i >= 0; i--)
			{
				char c = s[i];
				bool flag = c == '.';
				if (flag)
				{
					bool flag2 = num2 >= 0;
					if (flag2)
					{
						bool flag3 = num >= 0;
						if (flag3)
						{
							break;
						}
						num = i;
						stringBuilder.Insert(0, c);
					}
				}
				else
				{
					bool flag4 = char.IsDigit(c);
					if (flag4)
					{
						bool flag5 = num2 < 0;
						if (flag5)
						{
							num2 = i;
						}
						stringBuilder.Insert(0, c);
					}
					else
					{
						bool flag6 = num2 >= 0;
						if (flag6)
						{
							break;
						}
					}
				}
			}
			double num3;
			bool flag7 = !double.TryParse(stringBuilder.ToString(), out num3);
			double result;
			if (flag7)
			{
				result = 0.0;
			}
			else
			{
				result = num3;
			}
			return result;
		}

		// Token: 0x06000DAE RID: 3502 RVA: 0x0006416C File Offset: 0x0006236C
		private TEnum ExtractEnumFromArgs<TEnum>(IDictionary<string, string> args, string name) where TEnum : struct, IComparable, IFormattable, IConvertible
		{
			object obj = this.ExtractIntFromArgs(args, name, 0);
			Type typeFromHandle = typeof(TEnum);
			bool flag = Enum.IsDefined(typeFromHandle, obj);
			TEnum result;
			if (flag)
			{
				result = (TEnum)((object)obj);
			}
			else
			{
				result = default(TEnum);
			}
			return result;
		}

		// Token: 0x06000DAF RID: 3503 RVA: 0x000641B8 File Offset: 0x000623B8
		private string ExtractStringFromArgs(IDictionary<string, string> args, string name)
		{
			return this.ExtractStringFromArgs(args, name, "");
		}

		// Token: 0x06000DB0 RID: 3504 RVA: 0x000641D8 File Offset: 0x000623D8
		private string ExtractStringFromArgs(IDictionary<string, string> args, string name, string defaultValue)
		{
			bool flag = args != null && args.ContainsKey(name);
			string result;
			if (flag)
			{
				result = args[name];
			}
			else
			{
				result = defaultValue;
			}
			return result;
		}

		// Token: 0x06000DB1 RID: 3505 RVA: 0x00064208 File Offset: 0x00062408
		private int ExtractIntFromArgs(IDictionary<string, string> args, string name, int defaultValue = 0)
		{
			bool flag = args != null && args.ContainsKey(name);
			int result;
			if (flag)
			{
				int num;
				bool flag2 = !int.TryParse(args[name], out num);
				if (flag2)
				{
					result = defaultValue;
				}
				else
				{
					result = num;
				}
			}
			else
			{
				result = defaultValue;
			}
			return result;
		}

		// Token: 0x06000DB2 RID: 3506 RVA: 0x0006424C File Offset: 0x0006244C
		private TimeSpan? ExtractTimeSpanFromArgs(IDictionary<string, string> args, string name)
		{
			bool flag = args != null && args.ContainsKey(name) && args[name].Length > 0;
			if (flag)
			{
				string str = DateTime.Now.Date.ToString("yyyy-MM-dd");
				string s = str + " " + args[name];
				DateTime d;
				bool flag2 = DateTime.TryParse(s, out d) && d.Date != d;
				if (flag2)
				{
					return new TimeSpan?(d.TimeOfDay);
				}
			}
			return null;
		}

		// Token: 0x06000DB3 RID: 3507 RVA: 0x000642F0 File Offset: 0x000624F0
		private IDictionary<int, IList<string>> GetLevelsWithAssets(IList<TryToBookAccommodationToUse> accommodationsToUse, IList<TryToBookAsset> allAssets)
		{
			int num = 1;
			List<TryToBookAssetWithAssetAccommodation> list = new List<TryToBookAssetWithAssetAccommodation>();
			foreach (TryToBookAsset tryToBookAsset in allAssets)
			{
				foreach (TryToBookAssetAccommodation tryToBookAssetAccommodation in tryToBookAsset.AssetAccommodations)
				{
					bool flag = tryToBookAssetAccommodation.Level < 1;
					if (flag)
					{
						tryToBookAssetAccommodation.Level = 1;
					}
					bool flag2 = tryToBookAssetAccommodation.Level > num;
					if (flag2)
					{
						num = tryToBookAssetAccommodation.Level;
					}
					list.Add(new TryToBookAssetWithAssetAccommodation
					{
						Id = tryToBookAsset.Id,
						Score = tryToBookAsset.Score,
						AssetAccommodation = tryToBookAssetAccommodation
					});
				}
			}
			list.Sort((TryToBookAssetWithAssetAccommodation g1, TryToBookAssetWithAssetAccommodation g2) => g1.AssetAccommodation.Level.CompareTo(g2.AssetAccommodation.Level));
			Dictionary<int, List<AutoBooker2Manager.AssetWithFoundAccommodations>> dictionary = new Dictionary<int, List<AutoBooker2Manager.AssetWithFoundAccommodations>>();
			using (List<TryToBookAssetWithAssetAccommodation>.Enumerator enumerator3 = list.GetEnumerator())
			{
				while (enumerator3.MoveNext())
				{
					TryToBookAssetWithAssetAccommodation assetWithAccommodation = enumerator3.Current;
					TryToBookAccommodationToUse tryToBookAccommodationToUse = accommodationsToUse.FirstOrDefault((TryToBookAccommodationToUse g) => g.ControlId == assetWithAccommodation.AssetAccommodation.ControlId);
					bool flag3 = tryToBookAccommodationToUse != null;
					if (flag3)
					{
						string text = (assetWithAccommodation.AssetAccommodation.SubText ?? "").Trim();
						bool flag4 = text.Length > 0;
						if (flag4)
						{
							string text2 = (tryToBookAccommodationToUse.Value ?? "").Trim();
							bool flag5 = !text2.Equals(text, StringComparison.OrdinalIgnoreCase);
							if (flag5)
							{
								tryToBookAccommodationToUse = null;
							}
						}
						bool flag6 = tryToBookAccommodationToUse != null;
						if (flag6)
						{
							int level = assetWithAccommodation.AssetAccommodation.Level;
							bool flag7 = dictionary.ContainsKey(level);
							List<AutoBooker2Manager.AssetWithFoundAccommodations> list2;
							if (flag7)
							{
								list2 = dictionary[level];
							}
							else
							{
								list2 = new List<AutoBooker2Manager.AssetWithFoundAccommodations>();
								dictionary.Add(level, list2);
							}
							AutoBooker2Manager.AssetWithFoundAccommodations assetWithFoundAccommodations = list2.FirstOrDefault((AutoBooker2Manager.AssetWithFoundAccommodations g) => g.AssetId.Equals(assetWithAccommodation.Id, StringComparison.OrdinalIgnoreCase));
							bool flag8 = assetWithFoundAccommodations == null;
							if (flag8)
							{
								assetWithFoundAccommodations = new AutoBooker2Manager.AssetWithFoundAccommodations
								{
									AssetId = assetWithAccommodation.Id,
									ControlIds = new List<int>()
								};
								list2.Add(assetWithFoundAccommodations);
							}
							assetWithFoundAccommodations.ControlIds.Add(tryToBookAccommodationToUse.ControlId);
						}
					}
				}
			}
			bool flag9 = dictionary.Count < 1;
			if (flag9)
			{
				dictionary.Add(1, new List<AutoBooker2Manager.AssetWithFoundAccommodations>());
			}
			for (int i = 1; i < dictionary.Count; i++)
			{
				List<List<AutoBooker2Manager.AssetWithFoundAccommodations>> list3 = dictionary.Values.ToList<List<AutoBooker2Manager.AssetWithFoundAccommodations>>();
				List<AutoBooker2Manager.AssetWithFoundAccommodations> source = list3[i].ToList<AutoBooker2Manager.AssetWithFoundAccommodations>();
				List<int> source2 = source.SelectMany((AutoBooker2Manager.AssetWithFoundAccommodations cidList) => cidList.ControlIds).ToList<int>();
				List<AutoBooker2Manager.AssetWithFoundAccommodations> list4 = list3[i - 1].ToList<AutoBooker2Manager.AssetWithFoundAccommodations>();
				using (List<AutoBooker2Manager.AssetWithFoundAccommodations>.Enumerator enumerator4 = list4.GetEnumerator())
				{
					while (enumerator4.MoveNext())
					{
						AutoBooker2Manager.AssetWithFoundAccommodations item = enumerator4.Current;
						bool flag10 = source.FirstOrDefault((AutoBooker2Manager.AssetWithFoundAccommodations g) => g.AssetId.Equals(item.AssetId, StringComparison.OrdinalIgnoreCase)) == null;
						if (flag10)
						{
							int num2 = source2.FirstOrDefault((int g) => item.ControlIds.Contains(g));
							bool flag11 = num2 < 1;
							if (flag11)
							{
								list3[i].Add(new AutoBooker2Manager.AssetWithFoundAccommodations
								{
									AssetId = item.AssetId
								});
							}
						}
					}
				}
			}
			return dictionary.ToDictionary((KeyValuePair<int, List<AutoBooker2Manager.AssetWithFoundAccommodations>> kvp) => kvp.Key, (KeyValuePair<int, List<AutoBooker2Manager.AssetWithFoundAccommodations>> kvp) => (from g in kvp.Value
			select g.AssetId).ToList<string>());
		}

		// Token: 0x06000DB4 RID: 3508 RVA: 0x00064768 File Offset: 0x00062968
		private SpecialAccommodationRes ApplyTimeOfDay(SpecialAccommodationReq req)
		{
			SpecialAccommodationRes specialAccommodationRes = new SpecialAccommodationRes
			{
				TimeToInvestigate = req.Working.TimeToInvestigate
			};
			bool flag = req.SpecialAccommodationsToApply.Count < 1;
			SpecialAccommodationRes result;
			if (flag)
			{
				result = specialAccommodationRes;
			}
			else
			{
				foreach (TryToBookSpecialAccommodation tryToBookSpecialAccommodation in req.SpecialAccommodationsToApply)
				{
					IDictionary<string, string> args = tryToBookSpecialAccommodation.Args;
					TimeSpan? timeSpan = this.ExtractTimeSpanFromArgs(args, "starttime");
					TimeSpan? timeSpan2 = this.ExtractTimeSpanFromArgs(args, "endtime");
					bool flag2 = timeSpan != null && timeSpan2 != null;
					if (flag2)
					{
						DateTime dateTime = specialAccommodationRes.TimeToInvestigate.StartDateTime;
						TimeSpan timeOfDay = dateTime.TimeOfDay;
						string text = this.ExtractStringFromArgs(args, "pushtotomorrowtime").Trim();
						bool flag3 = text.Length > 0 && text != "0";
						TimeSpan? timeSpan3 = flag3 ? this.ExtractTimeSpanFromArgs(args, "pushtotomorrowtimeoverridestart") : timeSpan;
						bool skipWeekends = this.ExtractStringFromArgs(args, "skipweekends").Trim() != "0";
						DateTime? dateTime2 = null;
						bool flag4 = timeOfDay < timeSpan.Value;
						if (flag4)
						{
							bool flag5 = flag3;
							if (flag5)
							{
								dateTime = specialAccommodationRes.TimeToInvestigate.StartDateTime;
								dateTime = dateTime.Date;
								DateTime dateTime3 = dateTime.AddDays(1.0).SkipWeekendsIfNecessary(skipWeekends);
								bool flag6 = timeSpan3 != null;
								if (flag6)
								{
									dateTime2 = new DateTime?(dateTime3.Add(timeSpan3.Value));
								}
								else
								{
									dateTime2 = new DateTime?(dateTime3.Add(timeSpan.Value));
								}
							}
							else
							{
								dateTime = specialAccommodationRes.TimeToInvestigate.StartDateTime;
								dateTime = dateTime.Date;
								dateTime2 = new DateTime?(dateTime.Add(timeSpan.Value));
							}
						}
						else
						{
							bool flag7 = timeOfDay > timeSpan2.Value;
							if (flag7)
							{
								bool flag8 = !flag3;
								if (flag8)
								{
									specialAccommodationRes.TimeToInvestigate = null;
									return specialAccommodationRes;
								}
								dateTime = specialAccommodationRes.TimeToInvestigate.StartDateTime;
								DateTime date = dateTime.Date;
								dateTime = req.Working.Context.ClassTestDate;
								DateTime? dateTime4 = this.GetNextNonHoliday(date, dateTime.AddDays((double)req.Working.SearchOptions.MaxNumberOfDaysAfterClass), req.Working.Caches.Holidays).SkipWeekendsIfNecessary(skipWeekends);
								bool flag9 = dateTime4 == null;
								if (flag9)
								{
									specialAccommodationRes.TimeToInvestigate = null;
									return specialAccommodationRes;
								}
								dateTime = dateTime4.Value;
								dateTime = dateTime.Date;
								dateTime2 = new DateTime?(dateTime.Add((timeSpan3 != null) ? timeSpan3.Value : timeSpan.Value));
							}
						}
						bool flag10 = dateTime2 == null;
						if (!flag10)
						{
							DateTime value = dateTime2.Value;
							string text2 = this.ExtractStringFromArgs(tryToBookSpecialAccommodation.Args, "overridebookingnote", "Moved from {0} at {1} to {2} at {3} due to time-of-day");
							TryToBookWorking working = req.Working;
							DateTime sdt = value;
							string format = text2;
							object[] array = new object[4];
							int num = 0;
							dateTime = specialAccommodationRes.TimeToInvestigate.StartDateTime;
							array[num] = dateTime.ToString("yyyy-MM-dd");
							int num2 = 1;
							dateTime = specialAccommodationRes.TimeToInvestigate.StartDateTime;
							array[num2] = dateTime.ToString("h:mm tt");
							array[2] = value.ToString("yyyy-MM-dd");
							array[3] = value.ToString("h:mm tt");
							this.AddGeneralNotice(working, sdt, string.Format(format, array));
							specialAccommodationRes.TimeToInvestigate = new TryToBookTimeToInvestigate
							{
								StartDateTime = value,
								EndDateTime = value.AddMinutes((double)req.Working.StudentTestDuration)
							};
							break;
						}
					}
				}
				result = specialAccommodationRes;
			}
			return result;
		}

		// Token: 0x06000DB5 RID: 3509 RVA: 0x00064B48 File Offset: 0x00062D48
		private void AddGeneralNotice(TryToBookWorking working, DateTime sdt, string notice)
		{
			bool flag = working.Result.NoticesForAllPotentialBookings == null;
			if (flag)
			{
				working.Result.NoticesForAllPotentialBookings = new List<string>();
			}
			working.Result.NoticesForAllPotentialBookings.Add(notice);
		}

		// Token: 0x06000DB6 RID: 3510 RVA: 0x00064B8C File Offset: 0x00062D8C
		private SpecialAccommodationRes ApplyStartEndOfDaySlide(SpecialAccommodationReq req)
		{
			SpecialAccommodationRes specialAccommodationRes = new SpecialAccommodationRes
			{
				TimeToInvestigate = req.Working.TimeToInvestigate
			};
			bool flag = req.SpecialAccommodationsToApply.Count < 1;
			SpecialAccommodationRes result;
			if (flag)
			{
				result = specialAccommodationRes;
			}
			else
			{
				TryToBookTimeToInvestigate timeToInvestigate = specialAccommodationRes.TimeToInvestigate;
				TryToBookSpecialAccommodation tryToBookSpecialAccommodation = req.SpecialAccommodationsToApply[0];
				TimeSpan? timeSpan = this.ExtractTimeSpanFromArgs(tryToBookSpecialAccommodation.Args, "starttime");
				string text = null;
				bool flag2 = timeSpan != null && timeToInvestigate.StartDateTime.TimeOfDay < timeSpan.Value;
				if (flag2)
				{
					DateTime startDateTime = timeToInvestigate.StartDateTime.Date.Add(timeSpan.Value);
					text = this.ExtractStringFromArgs(tryToBookSpecialAccommodation.Args, "overridebookingnotemovebackward", "Moved from {0} to {1} due to end of day");
					specialAccommodationRes.TimeToInvestigate = new TryToBookTimeToInvestigate
					{
						StartDateTime = startDateTime,
						EndDateTime = startDateTime.AddMinutes((double)req.Working.StudentTestDuration)
					};
				}
				else
				{
					TimeSpan? timeSpan2 = this.ExtractTimeSpanFromArgs(tryToBookSpecialAccommodation.Args, "endtime");
					bool flag3 = timeSpan2 != null && (timeToInvestigate.EndDateTime.TimeOfDay > timeSpan2.Value || timeToInvestigate.EndDateTime.Date > timeToInvestigate.StartDateTime.Date);
					if (flag3)
					{
						DateTime endDateTime = timeToInvestigate.StartDateTime.Date.Add(timeSpan2.Value);
						text = this.ExtractStringFromArgs(tryToBookSpecialAccommodation.Args, "overridebookingnotemoveforward", "Moved from {0} to {1} due to start of day");
						specialAccommodationRes.TimeToInvestigate = new TryToBookTimeToInvestigate
						{
							EndDateTime = endDateTime,
							StartDateTime = endDateTime.AddMinutes((double)(-(double)req.Working.StudentTestDuration))
						};
					}
				}
				bool flag4 = text != null;
				if (flag4)
				{
					this.AddGeneralNotice(req.Working, specialAccommodationRes.TimeToInvestigate.StartDateTime, string.Format(text, timeToInvestigate.StartDateTime.ToString("yyyy-MM-dd h:mm tt"), specialAccommodationRes.TimeToInvestigate.StartDateTime.ToString("yyyy-MM-dd h:mm tt")));
				}
				result = specialAccommodationRes;
			}
			return result;
		}

		// Token: 0x06000DB7 RID: 3511 RVA: 0x00064DC8 File Offset: 0x00062FC8
		private TimeSpan? ParseTimeSpan(string s0)
		{
			bool flag = string.IsNullOrEmpty(s0);
			TimeSpan? result;
			if (flag)
			{
				result = null;
			}
			else
			{
				string text = s0;
				bool flag2 = text.IndexOf(':') < 0;
				if (flag2)
				{
					text += ":00";
				}
				int num = text.IndexOf(':');
				string s = text.Substring(0, num).Trim();
				int num2;
				bool flag3 = int.TryParse(s, out num2) && num2 > 24;
				bool flag4;
				if (flag3)
				{
					text = (num2 - 24).ToString() + text.Substring(num);
					flag4 = true;
				}
				else
				{
					flag4 = false;
				}
				string s2 = "2040-01-01 " + (text ?? "");
				DateTime dateTime;
				bool flag5 = !DateTime.TryParse(s2, out dateTime);
				if (flag5)
				{
					result = null;
				}
				else
				{
					bool flag6 = !flag4;
					if (flag6)
					{
						result = new TimeSpan?(dateTime.TimeOfDay);
					}
					else
					{
						result = new TimeSpan?(dateTime.TimeOfDay.Add(TimeSpan.FromDays(1.0)));
					}
				}
			}
			return result;
		}

		// Token: 0x06000DB8 RID: 3512 RVA: 0x00064EE0 File Offset: 0x000630E0
		private SpecialAccommodationRes ApplyEmailCoordinator(SpecialAccommodationReq req)
		{
			SpecialAccommodationRes specialAccommodationRes = new SpecialAccommodationRes
			{
				TimeToInvestigate = req.Working.TimeToInvestigate
			};
			bool flag = req.SpecialAccommodationsToApply.Count < 1;
			SpecialAccommodationRes result;
			if (flag)
			{
				result = specialAccommodationRes;
			}
			else
			{
				List<int> list = (from g in req.Working.Context.AccommodationsToUse
				where req.SpecialAccommodationsToApply.FirstOrDefault((TryToBookSpecialAccommodation h) => h.ControlId == g.ControlId) != null
				select g into m
				select m.ControlId).ToList<int>();
				bool flag2 = req.Working.Result.AccommodationCidsForEmail == null;
				if (flag2)
				{
					req.Working.Result.AccommodationCidsForEmail = new List<int>();
				}
				foreach (int item in list)
				{
					bool flag3 = !req.Working.Result.AccommodationCidsForEmail.Contains(item);
					if (flag3)
					{
						req.Working.Result.AccommodationCidsForEmail.Add(item);
					}
				}
				result = new SpecialAccommodationRes
				{
					TimeToInvestigate = req.Working.TimeToInvestigate
				};
			}
			return result;
		}

		// Token: 0x06000DB9 RID: 3513 RVA: 0x00065064 File Offset: 0x00063264
		private SpecialAccommodationRes ApplySnapTime(SpecialAccommodationReq req)
		{
			SpecialAccommodationRes specialAccommodationRes = new SpecialAccommodationRes
			{
				TimeToInvestigate = req.Working.TimeToInvestigate
			};
			bool flag = req.SpecialAccommodationsToApply.Count < 1;
			SpecialAccommodationRes result;
			if (flag)
			{
				result = specialAccommodationRes;
			}
			else
			{
				List<AutoBooker2Manager.SnapTimeRule> list = req.Working.Caches.GeneralCache.ContainsKey("snapToRules") ? ((List<AutoBooker2Manager.SnapTimeRule>)req.Working.Caches.GeneralCache["snapToRules"]) : null;
				bool flag2 = list == null;
				if (flag2)
				{
					List<string> list2 = (from g in req.SpecialAccommodationsToApply
					select this.ExtractStringFromArgs(g.Args, "rules").Trim() into h
					where h.Length > 0
					select h).ToList<string>();
					string text = string.Join(",", list2.ToArray());
					list = (from h in text.Split(new char[]
					{
						','
					}).ToList<string>().Select(delegate(string g)
					{
						int num = g.LastIndexOf('=');
						AutoBooker2Manager.SnapTimeRule snapTimeRule2 = new AutoBooker2Manager.SnapTimeRule
						{
							IsValid = false
						};
						bool flag6 = num <= 0;
						AutoBooker2Manager.SnapTimeRule result2;
						if (flag6)
						{
							result2 = snapTimeRule2;
						}
						else
						{
							TimeSpan? snapToTime = this.ParseTimeSpan(g.Substring(num + 1));
							bool flag7 = snapToTime == null;
							if (flag7)
							{
								result2 = snapTimeRule2;
							}
							else
							{
								string text2 = g.Substring(0, num);
								bool flag8 = text2.Length < 2;
								if (flag8)
								{
									result2 = snapTimeRule2;
								}
								else
								{
									TimeSpan? time = this.ParseTimeSpan(char.IsDigit(text2[1]) ? text2.Substring(1) : text2.Substring(2));
									bool flag9 = time == null;
									if (flag9)
									{
										result2 = snapTimeRule2;
									}
									else
									{
										bool flag10 = text2.StartsWith("<=");
										if (flag10)
										{
											result2 = new AutoBooker2Manager.SnapTimeRule
											{
												IsValid = true,
												Operation = ((TimeSpan ts) => (ts <= time) ? snapToTime : null)
											};
										}
										else
										{
											bool flag11 = text2.StartsWith(">=");
											if (flag11)
											{
												result2 = new AutoBooker2Manager.SnapTimeRule
												{
													IsValid = true,
													Operation = ((TimeSpan ts) => (ts >= time) ? snapToTime : null)
												};
											}
											else
											{
												bool flag12 = text2.StartsWith("<");
												if (flag12)
												{
													result2 = new AutoBooker2Manager.SnapTimeRule
													{
														IsValid = true,
														Operation = ((TimeSpan ts) => (ts < time) ? snapToTime : null)
													};
												}
												else
												{
													bool flag13 = text2.StartsWith(">");
													if (flag13)
													{
														result2 = new AutoBooker2Manager.SnapTimeRule
														{
															IsValid = true,
															Operation = ((TimeSpan ts) => (ts > time) ? snapToTime : null)
														};
													}
													else
													{
														bool flag14 = text2.StartsWith("=");
														if (flag14)
														{
															result2 = new AutoBooker2Manager.SnapTimeRule
															{
																IsValid = true,
																Operation = ((TimeSpan ts) => (ts == time) ? snapToTime : null)
															};
														}
														else
														{
															result2 = snapTimeRule2;
														}
													}
												}
											}
										}
									}
								}
							}
						}
						return result2;
					})
					where h.IsValid
					select h).ToList<AutoBooker2Manager.SnapTimeRule>();
					req.Working.Caches.GeneralCache.Add("snapToRules", list);
				}
				foreach (AutoBooker2Manager.SnapTimeRule snapTimeRule in list)
				{
					TimeSpan? timeSpan = snapTimeRule.Operation(req.Working.TimeToInvestigate.StartDateTime.TimeOfDay);
					bool flag3 = timeSpan == null;
					if (!flag3)
					{
						int studentTestDuration = req.Working.StudentTestDuration;
						DateTime dateTime = req.Working.TimeToInvestigate.StartDateTime.Date.Add(timeSpan.Value);
						bool flag4 = dateTime.Date == req.Working.TimeToInvestigate.StartDateTime.Date.AddDays(1.0);
						if (flag4)
						{
							DateTime? nextNonHoliday = this.GetNextNonHoliday(dateTime.Date.AddDays(-1.0), dateTime.Date.AddDays(7.0), req.Working.Caches.Holidays);
							bool flag5 = nextNonHoliday == null;
							if (flag5)
							{
								continue;
							}
							dateTime = nextNonHoliday.Value.Date.Add(dateTime.TimeOfDay);
						}
						TryToBookTimeToInvestigate tryToBookTimeToInvestigate = new TryToBookTimeToInvestigate
						{
							StartDateTime = dateTime,
							EndDateTime = dateTime.AddMinutes((double)studentTestDuration)
						};
						string format = this.ExtractStringFromArgs(req.SpecialAccommodationsToApply[0].Args, "overridebookingnote", "Changed time from {0} to {1} due to snap time");
						this.AddGeneralNotice(req.Working, dateTime, string.Format(format, specialAccommodationRes.TimeToInvestigate.StartDateTime.ToString("yyyy-MM-dd h:mm tt"), tryToBookTimeToInvestigate.StartDateTime.ToString("yyyy-MM-dd h:mm tt")));
						return new SpecialAccommodationRes
						{
							TimeToInvestigate = tryToBookTimeToInvestigate
						};
					}
				}
				result = specialAccommodationRes;
			}
			return result;
		}

		// Token: 0x06000DBA RID: 3514 RVA: 0x000653DC File Offset: 0x000635DC
		private SpecialAccommodationRes ApplyMaxPerDay(SpecialAccommodationReq req)
		{
			var list = (from g in req.SpecialAccommodationsToApply
			select new
			{
				SpecialAccommodation = g,
				Max = this.ExtractIntFromArgs(g.Args, "max", 0)
			}).ToList();
			list.Sort((g1, g2) => g1.Max.CompareTo(g2.Max));
			var <>f__AnonymousType = list.FirstOrDefault();
			bool flag = <>f__AnonymousType == null;
			SpecialAccommodationRes result;
			if (flag)
			{
				result = new SpecialAccommodationRes
				{
					TimeToInvestigate = req.Working.TimeToInvestigate
				};
			}
			else
			{
				IDictionary<string, string> args = <>f__AnonymousType.SpecialAccommodation.Args;
				TimeSpan? timeSpan = this.ExtractTimeSpanFromArgs(args, "futurestarttime");
				bool flag2 = this.ExtractStringFromArgs(args, "skipweekends").Trim() != "0";
				TryToBookTimeToInvestigate tryToBookTimeToInvestigate = req.Working.TimeToInvestigate.Clone();
				bool flag3 = flag2;
				if (flag3)
				{
					tryToBookTimeToInvestigate.StartDateTime = tryToBookTimeToInvestigate.StartDateTime.SkipWeekendsIfNecessary(true);
					tryToBookTimeToInvestigate.EndDateTime = tryToBookTimeToInvestigate.StartDateTime.AddMinutes((double)req.Working.StudentTestDuration);
				}
				SpecialAccommodationRes specialAccommodationRes = new SpecialAccommodationRes
				{
					TimeToInvestigate = tryToBookTimeToInvestigate
				};
				bool flag4 = req.SpecialAccommodationsToApply.Count < 1 || <>f__AnonymousType.Max < 1;
				if (flag4)
				{
					result = specialAccommodationRes;
				}
				else
				{
					TryToBookTimeToInvestigate tryToBookTimeToInvestigate2 = new TryToBookTimeToInvestigate
					{
						StartDateTime = tryToBookTimeToInvestigate.StartDateTime.Date.Add(tryToBookTimeToInvestigate.StartDateTime.TimeOfDay),
						EndDateTime = tryToBookTimeToInvestigate.EndDateTime.Date.Add(tryToBookTimeToInvestigate.EndDateTime.TimeOfDay)
					};
					bool flag5 = false;
					DateTime maxDate = req.Working.Context.ClassTestDate.AddDays((double)req.Working.SearchOptions.MaxNumberOfDaysAfterClass);
					bool flag6 = true;
					for (;;)
					{
						int numberOfCurrentTestsAndExamsPerDayForStudent = this.GetNumberOfCurrentTestsAndExamsPerDayForStudent(tryToBookTimeToInvestigate2.StartDateTime.Date, req.Working.Context.PersonId, req.Working.Context.LuCourseId, req.Working.Caches.NumberOfOtherTestsExamsStudentHasByDate);
						bool flag7 = numberOfCurrentTestsAndExamsPerDayForStudent < <>f__AnonymousType.Max;
						if (flag7)
						{
							break;
						}
						DateTime? dateTime = this.GetNextNonHoliday(tryToBookTimeToInvestigate2.StartDateTime.Date, maxDate, req.Working.Caches.Holidays).SkipWeekendsIfNecessary(flag2);
						bool flag8 = dateTime == null;
						if (flag8)
						{
							goto Block_8;
						}
						bool flag9 = timeSpan != null && !flag5;
						if (flag9)
						{
							int studentTestDuration = req.Working.StudentTestDuration;
							tryToBookTimeToInvestigate2.StartDateTime = dateTime.Value.Add(timeSpan.Value);
							tryToBookTimeToInvestigate2.EndDateTime = dateTime.Value.Add(timeSpan.Value).AddMinutes((double)studentTestDuration);
							flag5 = true;
						}
						else
						{
							tryToBookTimeToInvestigate2.StartDateTime = dateTime.Value.Add(tryToBookTimeToInvestigate2.StartDateTime.TimeOfDay);
							tryToBookTimeToInvestigate2.EndDateTime = dateTime.Value.Add(tryToBookTimeToInvestigate2.EndDateTime.TimeOfDay);
						}
						flag6 = false;
					}
					bool flag10 = !flag6;
					if (flag10)
					{
						string format = this.ExtractStringFromArgs(<>f__AnonymousType.SpecialAccommodation.Args, "overridebookingnote", "Changed day from {0} to {1} due to max per day");
						this.AddGeneralNotice(req.Working, tryToBookTimeToInvestigate2.StartDateTime, string.Format(format, specialAccommodationRes.TimeToInvestigate.StartDateTime.ToString("yyyy-MM-dd h:mm tt"), tryToBookTimeToInvestigate2.StartDateTime.ToString("yyyy-MM-dd h:mm tt")));
					}
					specialAccommodationRes.TimeToInvestigate = tryToBookTimeToInvestigate2;
					return specialAccommodationRes;
					Block_8:
					specialAccommodationRes.TimeToInvestigate = null;
					result = specialAccommodationRes;
				}
			}
			return result;
		}

		// Token: 0x06000DBB RID: 3515 RVA: 0x000657CC File Offset: 0x000639CC
		private DateTime? GetNextNonHoliday(DateTime startDate, DateTime maxDate, IDictionary<DateTime, bool> holidaysCache)
		{
			DateTime dateTime = startDate.AddDays(1.0).Date;
			for (;;)
			{
				bool flag = !this.IsHoliday(dateTime, holidaysCache);
				if (flag)
				{
					break;
				}
				dateTime = dateTime.AddDays(1.0);
				bool flag2 = dateTime > maxDate;
				if (flag2)
				{
					goto Block_2;
				}
			}
			return new DateTime?(dateTime);
			Block_2:
			return null;
		}

		// Token: 0x06000DBC RID: 3516 RVA: 0x00065840 File Offset: 0x00063A40
		private SpecialAccommodationRes ApplyDaysRest(SpecialAccommodationReq req)
		{
			var list = (from g in req.SpecialAccommodationsToApply
			select new
			{
				SpecialAccommodation = g,
				DaysBetween = this.ExtractIntFromArgs(g.Args, "daysbetween", 0)
			}).ToList();
			list.Sort((g1, g2) => g1.DaysBetween.CompareTo(g2.DaysBetween));
			var <>f__AnonymousType = list.FirstOrDefault();
			bool flag = <>f__AnonymousType == null;
			SpecialAccommodationRes result;
			if (flag)
			{
				result = new SpecialAccommodationRes
				{
					TimeToInvestigate = req.Working.TimeToInvestigate
				};
			}
			else
			{
				IDictionary<string, string> args = <>f__AnonymousType.SpecialAccommodation.Args;
				string text = this.ExtractStringFromArgs(args, "futurestarttime");
				bool flag2 = text.Length > 0;
				TimeSpan? timeSpan;
				if (flag2)
				{
					string s = DateTime.Now.Date.ToString("yyyy-MM-dd") + " " + text;
					DateTime dateTime;
					timeSpan = (DateTime.TryParse(s, out dateTime) ? new TimeSpan?(dateTime.TimeOfDay) : null);
				}
				else
				{
					timeSpan = null;
				}
				bool flag3 = this.ExtractStringFromArgs(args, "skipweekends").Trim() != "0";
				TryToBookTimeToInvestigate tryToBookTimeToInvestigate = req.Working.TimeToInvestigate.Clone();
				bool flag4 = flag3;
				if (flag4)
				{
					tryToBookTimeToInvestigate.StartDateTime = tryToBookTimeToInvestigate.StartDateTime.SkipWeekendsIfNecessary(true);
					tryToBookTimeToInvestigate.EndDateTime = tryToBookTimeToInvestigate.StartDateTime.AddMinutes((double)req.Working.StudentTestDuration);
				}
				SpecialAccommodationRes specialAccommodationRes = new SpecialAccommodationRes
				{
					TimeToInvestigate = tryToBookTimeToInvestigate
				};
				bool flag5 = <>f__AnonymousType.DaysBetween < 1 || req.SpecialAccommodationsToApply.Count < 1;
				if (flag5)
				{
					result = specialAccommodationRes;
				}
				else
				{
					TryToBookTimeToInvestigate tryToBookTimeToInvestigate2 = new TryToBookTimeToInvestigate
					{
						StartDateTime = tryToBookTimeToInvestigate.StartDateTime.Date.Add(tryToBookTimeToInvestigate.StartDateTime.TimeOfDay),
						EndDateTime = tryToBookTimeToInvestigate.EndDateTime.Date.Add(tryToBookTimeToInvestigate.EndDateTime.TimeOfDay)
					};
					bool flag6 = false;
					DateTime maxDate = req.Working.Context.ClassTestDate.AddDays((double)req.Working.SearchOptions.MaxNumberOfDaysAfterClass);
					List<int> list2 = new List<int>
					{
						0
					};
					for (int i = 1; i <= <>f__AnonymousType.DaysBetween; i++)
					{
						list2.Add(i);
						list2.Add(-i);
					}
					bool flag7 = true;
					for (;;)
					{
						bool flag8 = true;
						foreach (int num in list2)
						{
							int numberOfCurrentTestsAndExamsPerDayForStudent = this.GetNumberOfCurrentTestsAndExamsPerDayForStudent(tryToBookTimeToInvestigate2.StartDateTime.Date.AddDays((double)num), req.Working.Context.PersonId, req.Working.Context.LuCourseId, req.Working.Caches.NumberOfOtherTestsExamsStudentHasByDate);
							bool flag9 = numberOfCurrentTestsAndExamsPerDayForStudent > 0;
							if (flag9)
							{
								flag8 = false;
								break;
							}
						}
						bool flag10 = flag8;
						if (flag10)
						{
							break;
						}
						DateTime? dateTime2 = this.GetNextNonHoliday(tryToBookTimeToInvestigate2.StartDateTime.Date, maxDate, req.Working.Caches.Holidays).SkipWeekendsIfNecessary(flag3);
						bool flag11 = dateTime2 == null;
						if (flag11)
						{
							goto Block_12;
						}
						bool flag12 = timeSpan != null && !flag6;
						if (flag12)
						{
							int studentTestDuration = req.Working.StudentTestDuration;
							tryToBookTimeToInvestigate2.StartDateTime = dateTime2.Value.Add(timeSpan.Value);
							tryToBookTimeToInvestigate2.EndDateTime = dateTime2.Value.Add(timeSpan.Value).AddMinutes((double)studentTestDuration);
							flag6 = true;
						}
						else
						{
							tryToBookTimeToInvestigate2.StartDateTime = dateTime2.Value.Add(tryToBookTimeToInvestigate2.StartDateTime.TimeOfDay);
							tryToBookTimeToInvestigate2.EndDateTime = dateTime2.Value.Add(tryToBookTimeToInvestigate2.EndDateTime.TimeOfDay);
						}
						flag7 = false;
					}
					bool flag13 = !flag7;
					if (flag13)
					{
						string format = this.ExtractStringFromArgs(<>f__AnonymousType.SpecialAccommodation.Args, "overridebookingnote", "Changed day from {0} to {1} due to days-rest");
						this.AddGeneralNotice(req.Working, tryToBookTimeToInvestigate2.StartDateTime, string.Format(format, specialAccommodationRes.TimeToInvestigate.StartDateTime.ToString("yyyy-MM-dd h:mm tt"), tryToBookTimeToInvestigate2.StartDateTime.ToString("yyyy-MM-dd h:mm tt")));
					}
					specialAccommodationRes.TimeToInvestigate = tryToBookTimeToInvestigate2;
					return specialAccommodationRes;
					Block_12:
					specialAccommodationRes.TimeToInvestigate = null;
					result = specialAccommodationRes;
				}
			}
			return result;
		}

		// Token: 0x06000DBD RID: 3517 RVA: 0x00065D38 File Offset: 0x00063F38
		private int GetNumberOfCurrentTestsAndExamsPerDayForStudent(DateTime date, int pid, int lucid, IDictionary<DateTime, int> numberOfOtherTestsExamsStudentHasByDateCache)
		{
			bool flag = numberOfOtherTestsExamsStudentHasByDateCache.ContainsKey(date);
			int result;
			if (flag)
			{
				result = numberOfOtherTestsExamsStudentHasByDateCache[date];
			}
			else
			{
				int numberOfTestsAndExamsStudentHasInADay = this.dao.GetNumberOfTestsAndExamsStudentHasInADay(pid, lucid, date);
				numberOfOtherTestsExamsStudentHasByDateCache.Add(date, numberOfTestsAndExamsStudentHasInADay);
				result = numberOfTestsAndExamsStudentHasInADay;
			}
			return result;
		}

		// Token: 0x06000DBE RID: 3518 RVA: 0x00065D7C File Offset: 0x00063F7C
		private SpecialAccommodationRes ApplyBreakTime(SpecialAccommodationReq req)
		{
			int num = (req.Working.StudentTestDuration > 0) ? req.Working.StudentTestDuration : req.Working.Context.ClassTestMinutes;
			IList<TryToBookSpecialAccommodation> specialAccommodationsToApply = req.SpecialAccommodationsToApply;
			string text;
			req.Working.StudentTestDuration = this.CalculateBreakTime(req.Working.Context.ClassTestMinutes, new int?(num), specialAccommodationsToApply, req.Working.Context.AccommodationsToUse, out text);
			bool flag = !string.IsNullOrEmpty(text);
			if (flag)
			{
				req.Working.Result.NoticesForAllPotentialBookings.Add(text);
				req.Working.Result.AppliedBreakMinutes = req.Working.StudentTestDuration - num;
			}
			return new SpecialAccommodationRes();
		}

		// Token: 0x06000DBF RID: 3519 RVA: 0x00065E48 File Offset: 0x00064048
		public int CalculateBreakTime(int classTestDuration, IList<TryToBookSpecialAccommodation> allSpecialAccommodations, IList<TryToBookAccommodationToUse> accommodationsToUse)
		{
			List<TryToBookSpecialAccommodation> breakTimeSpecialAccommodations = (from g in allSpecialAccommodations
			where g.Type == eSpecialAccommodationType.Extra_Time
			select g).ToList<TryToBookSpecialAccommodation>();
			string text;
			return this.CalculateBreakTime(classTestDuration, null, breakTimeSpecialAccommodations, accommodationsToUse, out text);
		}

		// Token: 0x06000DC0 RID: 3520 RVA: 0x00065E9C File Offset: 0x0006409C
		private int CalculateBreakTime(int classTestDuration, int? currentStudentTestDuration0, IList<TryToBookSpecialAccommodation> breakTimeSpecialAccommodations, IList<TryToBookAccommodationToUse> accommodationsToUse, out string appliedBreakTimeMessage)
		{
			int num = (currentStudentTestDuration0 != null) ? currentStudentTestDuration0.Value : classTestDuration;
			int num2 = 0;
			TryToBookSpecialAccommodation tryToBookSpecialAccommodation = null;
			using (IEnumerator<TryToBookSpecialAccommodation> enumerator = breakTimeSpecialAccommodations.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					TryToBookSpecialAccommodation specialBreak = enumerator.Current;
					IEnumerable<TryToBookAccommodationToUse> enumerable = from g in accommodationsToUse
					where g.ControlId == specialBreak.ControlId
					select g;
					foreach (TryToBookAccommodationToUse matchingAccommodation in enumerable)
					{
						int num3 = this.CalculateBreakTimeAdditionalMinutes(specialBreak, matchingAccommodation, accommodationsToUse, classTestDuration, num);
						bool flag = num3 > num2;
						if (flag)
						{
							num2 = num3;
							tryToBookSpecialAccommodation = specialBreak;
						}
					}
				}
			}
			bool flag2 = tryToBookSpecialAccommodation != null && num2 > 0;
			int result;
			if (flag2)
			{
				string text = this.ExtractStringFromArgs(tryToBookSpecialAccommodation.Args, "overridebookingnote");
				bool flag3 = text.Length < 1;
				if (flag3)
				{
					text = "Applied break time of {0}";
				}
				appliedBreakTimeMessage = string.Format(text, num2.GetDurationDescription());
				result = num + num2;
			}
			else
			{
				appliedBreakTimeMessage = null;
				result = num;
			}
			return result;
		}

		// Token: 0x06000DC1 RID: 3521 RVA: 0x00065FE8 File Offset: 0x000641E8
		private SpecialAccommodationRes ApplyExtraTime(SpecialAccommodationReq req)
		{
			int value = (req.Working.StudentTestDuration > 0) ? req.Working.StudentTestDuration : req.Working.Context.ClassTestMinutes;
			IList<TryToBookSpecialAccommodation> specialAccommodationsToApply = req.SpecialAccommodationsToApply;
			string text;
			req.Working.StudentTestDuration = this.CalculateExtraTime(req.Working.Context.ClassTestMinutes, new int?(value), specialAccommodationsToApply, req.Working.Context.AccommodationsToUse, out text);
			bool flag = !string.IsNullOrEmpty(text);
			if (flag)
			{
				req.Working.Result.NoticesForAllPotentialBookings.Add(text);
			}
			return new SpecialAccommodationRes();
		}

		// Token: 0x06000DC2 RID: 3522 RVA: 0x00066094 File Offset: 0x00064294
		public int CalculateExtraTime(int classTestDuration, IList<TryToBookSpecialAccommodation> allSpecialAccommodations, IList<TryToBookAccommodationToUse> accommodationsToUse)
		{
			List<TryToBookSpecialAccommodation> extraTimeSpecialAccommodations = (from g in allSpecialAccommodations
			where g.Type == eSpecialAccommodationType.Extra_Time
			select g).ToList<TryToBookSpecialAccommodation>();
			string text;
			return this.CalculateExtraTime(classTestDuration, null, extraTimeSpecialAccommodations, accommodationsToUse, out text);
		}

		// Token: 0x06000DC3 RID: 3523 RVA: 0x000660E8 File Offset: 0x000642E8
		private int CalculateExtraTime(int classTestDuration, int? currentStudentTestDuration0, IList<TryToBookSpecialAccommodation> extraTimeSpecialAccommodations, IList<TryToBookAccommodationToUse> accommodationsToUse, out string appliedExtraTimeMessage)
		{
			int num = (currentStudentTestDuration0 != null) ? currentStudentTestDuration0.Value : classTestDuration;
			int num2 = 0;
			TryToBookSpecialAccommodation tryToBookSpecialAccommodation = null;
			bool flag = false;
			using (IEnumerator<TryToBookSpecialAccommodation> enumerator = extraTimeSpecialAccommodations.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					TryToBookSpecialAccommodation specialExtraTime = enumerator.Current;
					IEnumerable<TryToBookAccommodationToUse> enumerable = from g in accommodationsToUse
					where g.ControlId == specialExtraTime.ControlId
					select g;
					foreach (TryToBookAccommodationToUse matchingAccommodation in enumerable)
					{
						bool flag2;
						int num3 = this.CalculateExtraTimeAdditionalMinutes(specialExtraTime, matchingAccommodation, accommodationsToUse, classTestDuration, num, out flag2);
						bool flag3 = num3 > num2;
						if (flag3)
						{
							num2 = num3;
							tryToBookSpecialAccommodation = specialExtraTime;
							flag = flag2;
						}
					}
				}
			}
			bool flag4 = tryToBookSpecialAccommodation != null && num2 > 0;
			int result;
			if (flag4)
			{
				bool flag5 = flag;
				string text;
				if (flag5)
				{
					text = this.ExtractStringFromArgs(tryToBookSpecialAccommodation.Args, "overridebookingnoteflattime");
					bool flag6 = text.Length < 1;
					if (flag6)
					{
						text = "Added extra time flat rate ({0})";
					}
				}
				else
				{
					text = this.ExtractStringFromArgs(tryToBookSpecialAccommodation.Args, "overridebookingnote");
					bool flag7 = text.Length < 1;
					if (flag7)
					{
						text = "Added extra time ({0})";
					}
				}
				appliedExtraTimeMessage = string.Format(text, num2.GetDurationDescription());
				result = num + num2;
			}
			else
			{
				appliedExtraTimeMessage = null;
				result = num;
			}
			return result;
		}

		// Token: 0x06000DC4 RID: 3524 RVA: 0x00066278 File Offset: 0x00064478
		private IList<TryToBookPotentialBooking> TryToBookTestForSingleAssetLevel(int level, TryToBookWorking working, TryToBookEnvironment environment, TryToBookSearchOptions searchOptions)
		{
			IList<TryToBookPotentialBooking> list = null;
			int num = 0;
			using (IEnumerator<TryToBookRule> enumerator = working.SearchOptions.Rules.GetEnumerator())
			{
				Func<string, bool> <>9__12;
				Func<TryToBookRoom, bool> <>9__13;
				while (enumerator.MoveNext())
				{
					TryToBookRule rule = enumerator.Current;
					num++;
					int num2 = rule.StopLookingIfFoundAtLeastOne ? 1 : ((working.SearchOptions.MaxNumberOfPotentialTestsToReturn > 0) ? working.SearchOptions.MaxNumberOfPotentialTestsToReturn : 5);
					bool flag = num2 < 1;
					if (flag)
					{
						num2 = 1;
					}
					working.MaxNumberOfPotentialBookings = num2;
					bool flag2 = working.Result.DebuggingLogItems != null;
					if (flag2)
					{
						ICollection<string> debuggingLogItems = working.Result.DebuggingLogItems;
						string[] array = new string[8];
						array[0] = "New rule: rulenum=";
						array[1] = num.ToString();
						array[2] = "; maxNumPotBookings=";
						array[3] = num2.ToString();
						array[4] = "; working.AllRooms=";
						array[5] = string.Join(", ", (from g in working.AllRooms
						select g.PersonId.ToString()).ToArray<string>());
						array[6] = "; rule.RoomsToExclude=";
						int num3 = 7;
						string text;
						if (rule.RoomsToExclude != null)
						{
							text = string.Join(", ", (from g in rule.RoomsToExclude
							select g.ToString()).ToArray<string>());
						}
						else
						{
							text = "NULL";
						}
						array[num3] = text;
						debuggingLogItems.Add(string.Concat(array));
					}
					IList<TryToBookRoom> list2;
					if (rule.RoomsToExclude == null || rule.RoomsToExclude.Count <= 0)
					{
						list2 = working.AllRooms;
					}
					else
					{
						IList<TryToBookRoom> list3 = (from g in working.AllRooms
						where !rule.RoomsToExclude.Contains(g.PersonId)
						select g).ToList<TryToBookRoom>();
						list2 = list3;
					}
					IList<TryToBookRoom> list4 = list2;
					bool flag3 = list4.Count < 1;
					if (flag3)
					{
						working.Result.Messages.Add("allRooms1 is empty");
					}
					bool flag4 = searchOptions.RestrictRoomByCampusEnabled && !string.IsNullOrEmpty(working.Context.CourseCampus);
					if (flag4)
					{
						string campusToLookFor = working.Context.CourseCampus;
						List<TryToBookRoom> unmatchedRooms = new List<TryToBookRoom>();
						list4 = list4.Where(delegate(TryToBookRoom g)
						{
							Func<TryToBookRoom, bool> <>9__10;
							return g.Campuses != null && g.Campuses.FirstOrDefault(delegate(string h)
							{
								bool flag9 = h.Equals(campusToLookFor, StringComparison.OrdinalIgnoreCase);
								bool flag10 = !flag9;
								if (flag10)
								{
									IEnumerable<TryToBookRoom> unmatchedRooms = unmatchedRooms;
									Func<TryToBookRoom, bool> predicate3;
									if ((predicate3 = <>9__10) == null)
									{
										predicate3 = (<>9__10 = ((TryToBookRoom m) => m.PersonId == g.PersonId));
									}
									bool flag11 = unmatchedRooms.FirstOrDefault(predicate3) == null;
									if (flag11)
									{
										unmatchedRooms.Add(g);
									}
								}
								return flag9;
							}) != null;
						}).ToList<TryToBookRoom>();
						bool flag5 = unmatchedRooms.Count > 0;
						if (flag5)
						{
							working.Result.Messages.Add(string.Format("Restricted room list by campus: removed [{0}]; kept [{1}]", string.Join(", ", (from g in unmatchedRooms
							select (g.Title ?? "") + "." + g.PersonId.ToString()).ToArray<string>()), string.Join(", ", (from g in list4
							select (g.Title ?? "") + "." + g.PersonId.ToString()).ToArray<string>())));
						}
					}
					bool flag6 = list4.Count < 1;
					if (flag6)
					{
						working.Result.Messages.Add("allRooms2 is empty");
					}
					working.AllVirtualRooms = (from g in list4
					where g.RoomType == eRoomType.VirtualRoom || g.RoomType == eRoomType.SuperVirtualRoom
					select g).ToList<TryToBookRoom>();
					working.AllNonVirtualRooms = (from g in list4
					where g.RoomType == eRoomType.RegularRoom
					select g).ToList<TryToBookRoom>();
					TryToBookWorking working2 = working;
					IList<string> assetIdsRequired;
					if (!rule.IgnoreAssetRules)
					{
						assetIdsRequired = working.AssetIdsRequired;
					}
					else
					{
						IList<string> list5 = new List<string>();
						assetIdsRequired = list5;
					}
					working2.AssetIdsRequired = assetIdsRequired;
					IEnumerable<string> source = from assetId in working.AssetIdsRequired
					select assetId.ToUpper();
					Func<string, bool> predicate;
					if ((predicate = <>9__12) == null)
					{
						predicate = (<>9__12 = ((string id) => !working.Result.AssetsRequiredAtSomePoint.Contains(id)));
					}
					foreach (string item in source.Where(predicate))
					{
						working.Result.AssetsRequiredAtSomePoint.Add(item);
					}
					working.RoomsToInvestigate = this.GetRoomsToInvestigate(rule, working);
					IEnumerable<TryToBookRoom> roomsToInvestigate = working.RoomsToInvestigate;
					Func<TryToBookRoom, bool> predicate2;
					if ((predicate2 = <>9__13) == null)
					{
						predicate2 = (<>9__13 = ((TryToBookRoom rti) => !working.Result.RoomIdsConsidered.Contains(rti.PersonId)));
					}
					foreach (TryToBookRoom tryToBookRoom in roomsToInvestigate.Where(predicate2))
					{
						working.Result.RoomIdsConsidered.Add(tryToBookRoom.PersonId);
					}
					string item2 = string.Format("rule #{0}; assetsRequired={1}; roomsToInvestigate={2}", num.ToString(), string.Join(", ", working.AssetIdsRequired.ToArray<string>()), string.Join(", ", (from g in working.RoomsToInvestigate
					select g.PersonId.ToString()).ToArray<string>()));
					working.Result.Messages.Add(item2);
					bool flag7 = working.Result.DebuggingLogItems != null;
					if (flag7)
					{
						working.Result.DebuggingLogItems.Add(item2);
					}
					working.Caches.DateTimesAlreadyChecked.Clear();
					list = this.TryToFindPotentialBookingForRule(rule, working);
					bool flag8 = list.Count > 0;
					if (flag8)
					{
						break;
					}
				}
			}
			return list ?? new List<TryToBookPotentialBooking>();
		}

		// Token: 0x06000DC5 RID: 3525 RVA: 0x00066928 File Offset: 0x00064B28
		private IList<TryToBookPotentialBooking> TryToFindPotentialBookingForRule(TryToBookRule rule, TryToBookWorking working)
		{
			working.CurrentRule = rule;
			DateTime startDateTime = working.Context.ClassTestDate.Add(working.Context.ClassStartTime);
			TryToBookTimeToInvestigate tryToBookTimeToInvestigate = new TryToBookTimeToInvestigate
			{
				StartDateTime = startDateTime,
				EndDateTime = startDateTime.AddMinutes((double)working.StudentTestDuration)
			};
			List<int> list = new List<int>
			{
				0
			};
			bool enforceOverlapWithClassTime = rule.EnforceOverlapWithClassTime;
			if (enforceOverlapWithClassTime)
			{
				int num = (rule.OnlyOverlapFirstXMinutesOfClassTest != null && rule.OnlyOverlapFirstXMinutesOfClassTest.Value > 0) ? rule.OnlyOverlapFirstXMinutesOfClassTest.Value : working.Context.ClassTestMinutes;
				bool flag = working.Result.DebuggingLogItems != null;
				if (flag)
				{
					working.Result.DebuggingLogItems.Add("EnforceOverlapWithClassTime enabled: minuteOfClassTestToOverlap=" + num.ToString());
				}
				int num2 = working.StudentTestDuration - num;
				bool flag2 = num2 > 0;
				if (flag2)
				{
					int num3 = (int)(Convert.ToDouble(num2) / Convert.ToDouble(15));
					for (int i = 0; i < num3; i++)
					{
						list.Add(-(15 * (i + 1)));
					}
				}
			}
			else
			{
				int num4 = 15;
				while (num4 <= rule.AllowedMinutesAfter || num4 <= rule.AllowedMinutesBefore)
				{
					bool flag3 = num4 <= rule.AllowedMinutesAfter;
					if (flag3)
					{
						list.Add(num4);
					}
					bool flag4 = num4 <= rule.AllowedMinutesBefore;
					if (flag4)
					{
						list.Add(-num4);
					}
					num4 += 15;
				}
			}
			bool flag5 = working.Result.DebuggingLogItems != null;
			if (flag5)
			{
				ICollection<string> debuggingLogItems = working.Result.DebuggingLogItems;
				string[] array = new string[6];
				array[0] = "timeToInvestigate=";
				array[1] = tryToBookTimeToInvestigate.StartDateTime.ToString("yyyy-MM-dd: h:mm tt");
				array[2] = "-";
				array[3] = tryToBookTimeToInvestigate.EndDateTime.ToString("h:mm tt");
				array[4] = "; incremental Minutes=";
				array[5] = string.Join(", ", (from g in list
				select g.ToString()).ToArray<string>());
				debuggingLogItems.Add(string.Concat(array));
			}
			return this.TryToFindPotentialBookingsForSingleDayTimeWithIncrementsList(working, tryToBookTimeToInvestigate, list);
		}

		// Token: 0x06000DC6 RID: 3526 RVA: 0x00066B94 File Offset: 0x00064D94
		private IList<TryToBookPotentialBooking> TryToFindPotentialBookingsForSingleDayTimeWithIncrementsList(TryToBookWorking working, TryToBookTimeToInvestigate timeToInvestigate, IList<int> incrementalMinutes)
		{
			bool flag = incrementalMinutes.Count < 1;
			if (flag)
			{
				incrementalMinutes.Add(0);
			}
			List<TryToBookPotentialBooking> list = new List<TryToBookPotentialBooking>();
			DateTime startDateTime = timeToInvestigate.StartDateTime;
			DateTime endDateTime = timeToInvestigate.EndDateTime;
			foreach (int num in incrementalMinutes)
			{
				timeToInvestigate.StartDateTime = startDateTime.AddMinutes((double)num);
				timeToInvestigate.EndDateTime = endDateTime.AddMinutes((double)num);
				bool flag2 = working.Result.DebuggingLogItems != null;
				if (flag2)
				{
					working.Result.DebuggingLogItems.Add(string.Concat(new string[]
					{
						"Loop:diff=",
						num.ToString(),
						"; sdt=",
						timeToInvestigate.StartDateTime.ToString("yyyy-MM-dd h:mm tt"),
						" to ",
						timeToInvestigate.EndDateTime.ToString("h:mm tt")
					}));
				}
				working.TimeToInvestigate = timeToInvestigate;
				TryToBookTimeToInvestigate tryToBookTimeToInvestigate = this.EvaluateDateTimeChanged(working);
				bool flag3 = tryToBookTimeToInvestigate == null;
				if (flag3)
				{
					bool flag4 = working.Result.DebuggingLogItems != null;
					if (flag4)
					{
						working.Result.DebuggingLogItems.Add("Exiting loop because timeToInvestigate2 from EvaluateDateTimeChanged is null");
					}
					break;
				}
				bool flag5 = timeToInvestigate.StartDateTime != tryToBookTimeToInvestigate.StartDateTime;
				timeToInvestigate = tryToBookTimeToInvestigate;
				long ticks = timeToInvestigate.StartDateTime.Ticks;
				bool flag6 = !working.Caches.DateTimesAlreadyChecked.Contains(ticks);
				if (flag6)
				{
					bool flag7;
					TryToBookPotentialBooking tryToBookPotentialBooking = this.TryToFindPotentialBookingForSingleDayTime(timeToInvestigate, working, true, out flag7);
					working.Caches.DateTimesAlreadyChecked.Add(ticks);
					bool flag8 = tryToBookPotentialBooking != null && !this.AddBookingToList(tryToBookPotentialBooking, list, working);
					if (flag8)
					{
						break;
					}
					bool flag9 = tryToBookPotentialBooking == null && flag7 && working.CurrentRule.AllowShiftingTimeToWorkAroundTimetableForOtherCourses;
					if (flag9)
					{
						flag5 = true;
						TryToBookTimeToInvestigate[] array = this.TryToShiftTimeAroundTimetable(timeToInvestigate, working);
						foreach (TryToBookTimeToInvestigate tryToBookTimeToInvestigate2 in array)
						{
							bool flag10 = tryToBookTimeToInvestigate2 == null;
							if (!flag10)
							{
								ticks = tryToBookTimeToInvestigate2.StartDateTime.Ticks;
								bool flag11 = working.Caches.DateTimesAlreadyChecked.Contains(ticks);
								if (!flag11)
								{
									tryToBookPotentialBooking = this.TryToFindPotentialBookingForSingleDayTime(tryToBookTimeToInvestigate2, working, true, out flag7);
									working.Caches.DateTimesAlreadyChecked.Add(ticks);
									bool flag12 = tryToBookPotentialBooking == null;
									if (!flag12)
									{
										bool flag13 = !this.AddBookingToList(tryToBookPotentialBooking, list, working);
										if (!flag13)
										{
											string text = "Changed time from {0} to {1} due to 'Allow shifting time to work around timetables for other courses' rule setting";
											text = string.Format(text, timeToInvestigate.StartDateTime.ToString("h:mm tt"), tryToBookPotentialBooking.StartDateTime.ToString("h:mm tt"));
											this.AddNotice(working, tryToBookPotentialBooking.StartDateTime, text);
											break;
										}
									}
								}
							}
						}
					}
				}
				bool flag14 = flag5;
				if (flag14)
				{
					break;
				}
			}
			return list;
		}

		// Token: 0x06000DC7 RID: 3527 RVA: 0x00066EC8 File Offset: 0x000650C8
		private TryToBookTimeToInvestigate[] TryToShiftTimeAroundTimetable(TryToBookTimeToInvestigate preferredTime, TryToBookWorking working)
		{
			DayOfWeek dow = preferredTime.StartDateTime.DayOfWeek;
			Func<LookupTimetableItem, bool> <>9__10;
			List<LookupTimetableItem> allTimetableItemsForOtherCoursesOnSameDayOfWeek = (from g in working.Caches.StudentCourses
			where g.LuCourseId != working.Context.LuCourseId
			select g).SelectMany(delegate(LookupCourse g)
			{
				IEnumerable<LookupTimetableItem> source2 = g.TimetableItems ?? new List<LookupTimetableItem>();
				Func<LookupTimetableItem, bool> predicate;
				if ((predicate = <>9__10) == null)
				{
					predicate = (<>9__10 = ((LookupTimetableItem h) => h.DayOfWeek == dow));
				}
				return source2.Where(predicate);
			}).ToList<LookupTimetableItem>();
			IList<TryToBookAvailability> source = this.LoadStudentAppointments(working.Context.PersonId, preferredTime.StartDateTime.Date, working.SearchOptions.BookingAlreadyExistsAppointmentId, working.Caches.StudentScheduleCache);
			allTimetableItemsForOtherCoursesOnSameDayOfWeek.AddRange(from g in source
			select new LookupTimetableItem
			{
				DayOfWeek = dow,
				StartTime = g.StartDateTime.TimeOfDay,
				EndTime = g.EndDateTime.TimeOfDay
			});
			List<int> list = new List<int>();
			int num = 15;
			for (;;)
			{
				DateTime proposedStartTime = preferredTime.EndDateTime.AddMinutes((double)num);
				DateTime proposedStartTime2 = preferredTime.StartDateTime.AddMinutes((double)(-(double)num));
				bool flag = AutoBooker2Manager.IsTimetableMoveDateTimeValid(proposedStartTime, preferredTime.StartDateTime, preferredTime.EndDateTime, working.CurrentRule.TimetableShiftMaxNumMinutesBeforeClassTime, working.CurrentRule.TimetableShiftMaxNumMinutesAfterClassTime);
				bool flag2 = AutoBooker2Manager.IsTimetableMoveDateTimeValid(proposedStartTime2, preferredTime.StartDateTime, preferredTime.EndDateTime, 0, 0);
				bool flag3 = !flag && !flag2;
				if (flag3)
				{
					break;
				}
				bool flag4 = flag;
				if (flag4)
				{
					list.Add(num);
				}
				bool flag5 = flag2;
				if (flag5)
				{
					list.Add(-num);
				}
				num += 15;
			}
			return (from incMin in list
			let nextSdt = preferredTime.StartDateTime.AddMinutes((double)incMin)
			let nextSdtWithBuffer = nextSdt.AddMinutes((double)(-(double)working.SearchOptions.BufferMinutesPre))
			let nextEdt = preferredTime.EndDateTime.AddMinutes((double)incMin)
			let nextEdtWithBuffer = nextEdt.AddMinutes((double)working.SearchOptions.BufferMinutesPost)
			let firstOverlapping = allTimetableItemsForOtherCoursesOnSameDayOfWeek.FirstOrDefault((LookupTimetableItem g) => !(nextEdtWithBuffer.TimeOfDay <= g.StartTime) && !(nextSdtWithBuffer.TimeOfDay >= g.EndTime))
			where firstOverlapping == null && nextSdt.Date == nextEdt.Date
			select new TryToBookTimeToInvestigate
			{
				StartDateTime = nextSdt,
				EndDateTime = nextEdt
			}).ToArray<TryToBookTimeToInvestigate>();
		}

		// Token: 0x06000DC8 RID: 3528 RVA: 0x00067138 File Offset: 0x00065338
		private static bool IsTimetableMoveDateTimeValid(DateTime proposedStartTime, DateTime originalStartTime, DateTime orignalEndTime, int maxNumMinutesBeforeClassTime, int maxNumMinutesAfterClassTime)
		{
			bool flag = proposedStartTime.Date != originalStartTime.Date;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				bool flag2 = proposedStartTime < originalStartTime;
				if (flag2)
				{
					bool flag3 = maxNumMinutesBeforeClassTime > 0;
					result = (!flag3 || (originalStartTime - proposedStartTime).TotalMinutes <= (double)maxNumMinutesBeforeClassTime);
				}
				else
				{
					bool flag4 = maxNumMinutesAfterClassTime > 0;
					result = (!flag4 || (proposedStartTime - originalStartTime).TotalMinutes <= (double)maxNumMinutesAfterClassTime);
				}
			}
			return result;
		}

		// Token: 0x06000DC9 RID: 3529 RVA: 0x000671C0 File Offset: 0x000653C0
		private TryToBookTimeToInvestigate EvaluateDateTimeChanged(TryToBookWorking working)
		{
			TryToBookTimeToInvestigate timeToInvestigate = working.TimeToInvestigate;
			List<SpecialAccommodationReq> list = working.SpecialAccommodationActionsRequired.ContainsKey(eSpecialAccommodationApplyMethod.OnInvestigatingNewDateOrTime) ? working.SpecialAccommodationActionsRequired[eSpecialAccommodationApplyMethod.OnInvestigatingNewDateOrTime] : new List<SpecialAccommodationReq>();
			foreach (SpecialAccommodationReq specialAccommodationReq in list)
			{
				specialAccommodationReq.Working = working;
				SpecialAccommodationRes specialAccommodationRes = specialAccommodationReq.Func(specialAccommodationReq);
				bool abortFindPotentialBookingsProcess = specialAccommodationRes.AbortFindPotentialBookingsProcess;
				if (abortFindPotentialBookingsProcess)
				{
					return null;
				}
				timeToInvestigate = specialAccommodationRes.TimeToInvestigate;
				working.TimeToInvestigate = timeToInvestigate;
			}
			return timeToInvestigate;
		}

		// Token: 0x06000DCA RID: 3530 RVA: 0x00067274 File Offset: 0x00065474
		private bool AddBookingsToList(IList<TryToBookPotentialBooking> potentialBookingsToAdd, IList<TryToBookPotentialBooking> potentialBookings, TryToBookWorking working)
		{
			return potentialBookingsToAdd.All((TryToBookPotentialBooking pbook) => this.AddBookingToList(pbook, potentialBookings, working));
		}

		// Token: 0x06000DCB RID: 3531 RVA: 0x000672B4 File Offset: 0x000654B4
		private bool AddBookingToList(TryToBookPotentialBooking potentialBooking, IList<TryToBookPotentialBooking> potentialBookings, TryToBookWorking working)
		{
			TryToBookPotentialBooking tryToBookPotentialBooking = potentialBooking;
			bool flag = working.SpecialAccommodationActionsRequired.ContainsKey(eSpecialAccommodationApplyMethod.AfterPotentialBookingFound);
			if (flag)
			{
				foreach (SpecialAccommodationReq specialAccommodationReq in working.SpecialAccommodationActionsRequired[eSpecialAccommodationApplyMethod.AfterPotentialBookingFound])
				{
					specialAccommodationReq.Working = working;
					specialAccommodationReq.PotentialBookingToAdd = tryToBookPotentialBooking;
					SpecialAccommodationRes specialAccommodationRes = specialAccommodationReq.Func(specialAccommodationReq);
					bool abortFindPotentialBookingsProcess = specialAccommodationRes.AbortFindPotentialBookingsProcess;
					if (abortFindPotentialBookingsProcess)
					{
						return false;
					}
					tryToBookPotentialBooking = specialAccommodationRes.PotentialBookingToAdd;
				}
			}
			potentialBookings.Add(tryToBookPotentialBooking);
			bool flag2 = potentialBookings.Count >= working.MaxNumberOfPotentialBookings;
			return !flag2;
		}

		// Token: 0x06000DCC RID: 3532 RVA: 0x00067384 File Offset: 0x00065584
		private IList<TryToBookAvailability> LoadStudentAppointments(int pid, DateTime dt, int appIdToIgnore, IDictionary<DateTime, List<TryToBookAvailability>> studentScheduleCache)
		{
			bool flag = studentScheduleCache.ContainsKey(dt);
			IList<TryToBookAvailability> result;
			if (flag)
			{
				result = studentScheduleCache[dt];
			}
			else
			{
				List<TryToBookAvailability> list = this.dao.LoadStudentAppointments(pid, dt, appIdToIgnore).ToList<TryToBookAvailability>();
				studentScheduleCache.Add(dt, list);
				result = list;
			}
			return result;
		}

		// Token: 0x06000DCD RID: 3533 RVA: 0x000673CC File Offset: 0x000655CC
		private bool DoesStudentHaveAnExistingAppointment(int pid, DateTime startDateTime, DateTime endDateTime, int appIdToIgnore, IDictionary<DateTime, List<TryToBookAvailability>> studentScheduleCache)
		{
			IList<TryToBookAvailability> source = this.LoadStudentAppointments(pid, startDateTime.Date, appIdToIgnore, studentScheduleCache);
			TryToBookAvailability tryToBookAvailability = source.FirstOrDefault((TryToBookAvailability g) => !(endDateTime <= g.StartDateTime) && !(startDateTime >= g.EndDateTime));
			return tryToBookAvailability != null;
		}

		// Token: 0x06000DCE RID: 3534 RVA: 0x00067420 File Offset: 0x00065620
		private bool IsConflictingWithTimetableForAnotherCourse(TryToBookTimeToInvestigate timeToInvestigate, TryToBookWorking working, out int ConflictingLuCourseId)
		{
			bool flag = working.Caches.StudentCourses == null;
			if (flag)
			{
				ICourseRegistrationManager courseRegistrationManager = new CourseRegistrationManager(this.OpContext);
				working.Caches.StudentCourses = (from g in courseRegistrationManager.LoadStudentsCourses(working.Context.ClassTestDate.Date, working.Context.ClassTestDate.Date, working.Context.PersonId, false)
				select g.Course).ToList<LookupCourse>();
			}
			IEnumerable<LookupCourse> source = from g in working.Caches.StudentCourses
			where g.LuCourseId != working.Context.LuCourseId
			select g;
			TimeSpan st = timeToInvestigate.StartDateTime.TimeOfDay;
			TimeSpan et = timeToInvestigate.EndDateTime.TimeOfDay;
			DayOfWeek dow = timeToInvestigate.StartDateTime.DayOfWeek;
			int conflictingLuCourseId = 0;
			LookupCourse lookupCourse = source.FirstOrDefault((LookupCourse g) => g.TimetableItems != null && g.TimetableItems.FirstOrDefault(delegate(LookupTimetableItem h)
			{
				bool flag2 = h.DayOfWeek == dow && !(et <= h.StartTime) && !(st >= h.EndTime);
				bool flag3 = flag2;
				if (flag3)
				{
					conflictingLuCourseId = g.LuCourseId;
				}
				return flag2;
			}) != null);
			ConflictingLuCourseId = conflictingLuCourseId;
			return lookupCourse != null;
		}

		// Token: 0x06000DCF RID: 3535 RVA: 0x00067578 File Offset: 0x00065778
		private bool IsStudentDoubleBooked(int pid, DateTime sdt, DateTime edt, TryToBookWorking working)
		{
			return !working.SearchOptions.AllowStudentsToBeDoubleBooked && this.DoesStudentHaveAnExistingAppointment(pid, sdt, edt, working.SearchOptions.BookingAlreadyExistsAppointmentId, working.Caches.StudentScheduleCache);
		}

		// Token: 0x06000DD0 RID: 3536 RVA: 0x000675BC File Offset: 0x000657BC
		private TryToBookPotentialBooking TryToFindPotentialBookingForSingleDayTime(TryToBookTimeToInvestigate timeToInvestigate, TryToBookWorking working, bool tryShiftTimeRules, out bool timetableConflict)
		{
			bool flag = timeToInvestigate.EndDateTime.Date != timeToInvestigate.StartDateTime.Date;
			timetableConflict = false;
			working.Caches.DateTimesAlreadyChecked.Add(timeToInvestigate.StartDateTime.Ticks);
			DateTime startDateTime = timeToInvestigate.StartDateTime;
			DateTime endDateTime = timeToInvestigate.EndDateTime;
			bool flag2 = this.IsStudentDoubleBooked(working.Context.PersonId, startDateTime, endDateTime, working);
			bool flag3 = false;
			bool flag4 = !flag && !flag2 && working.SearchOptions.MatchUpTimetable;
			if (flag4)
			{
				int num;
				flag3 = this.IsConflictingWithTimetableForAnotherCourse(timeToInvestigate, working, out num);
				bool flag5 = flag3;
				if (flag5)
				{
					timetableConflict = true;
					bool flag6 = working.Result.StartDateTimesNotUseableBecauseOfTimetableConflict == null;
					if (flag6)
					{
						working.Result.StartDateTimesNotUseableBecauseOfTimetableConflict = new List<DateTime>();
					}
					working.Result.StartDateTimesNotUseableBecauseOfTimetableConflict.Add(timeToInvestigate.StartDateTime);
					working.Result.Messages.Add(string.Concat(new string[]
					{
						"Timetable conflict: ",
						timeToInvestigate.StartDateTime.ToString("yyyy-MM-dd h:mm tt"),
						" to ",
						timeToInvestigate.EndDateTime.ToString("h:mm tt"),
						" not useable because of timetable conflict with lucid=",
						num.ToString()
					}));
				}
			}
			IList<TryToBookRoom> roomsToInvestigate = working.RoomsToInvestigate;
			bool flag7 = !flag && !flag2 && !flag3;
			if (flag7)
			{
				foreach (TryToBookRoom room in roomsToInvestigate)
				{
					bool flag8 = this.IsRoomAvailable(room, startDateTime, endDateTime, working.Caches.RoomScheduleCache, working.SearchOptions.RoomAvailabilityScheduleMappings, working.SearchOptions.BufferMinutesPre, working.SearchOptions.BufferMinutesPost);
					if (flag8)
					{
						List<string> list = new List<string>(working.Result.NoticesForAllPotentialBookings);
						bool flag9 = working.Caches.NoticesCache.ContainsKey(startDateTime);
						if (flag9)
						{
							list.AddRange(working.Caches.NoticesCache[startDateTime]);
						}
						return new TryToBookPotentialBooking
						{
							Room = room,
							StartDateTime = startDateTime,
							EndDateTime = endDateTime,
							Notices = list
						};
					}
				}
			}
			bool flag10 = !tryShiftTimeRules;
			TryToBookPotentialBooking result;
			if (flag10)
			{
				result = null;
			}
			else
			{
				bool shiftTimeToMatchEndOfDay = working.CurrentRule.ShiftTimeToMatchEndOfDay;
				if (shiftTimeToMatchEndOfDay)
				{
					TryToBookPotentialBooking tryToBookPotentialBooking = this.TryToFindRoomAvailabilityWithShiftTimeRule(false, roomsToInvestigate, startDateTime, endDateTime, working);
					bool flag11 = tryToBookPotentialBooking != null;
					if (flag11)
					{
						this.AddNotice(working, tryToBookPotentialBooking.StartDateTime, "Shifted time to match end of day: from " + timeToInvestigate.StartDateTime.ToString("yyyy-MM-dd h:mm tt") + " to " + timeToInvestigate.EndDateTime.ToString("h:mm tt"));
						return tryToBookPotentialBooking;
					}
				}
				bool flag12 = !working.CurrentRule.ShiftTimeToMatchStartOfDay;
				if (flag12)
				{
					result = null;
				}
				else
				{
					TryToBookPotentialBooking tryToBookPotentialBooking2 = this.TryToFindRoomAvailabilityWithShiftTimeRule(true, roomsToInvestigate, startDateTime, endDateTime, working);
					bool flag13 = tryToBookPotentialBooking2 == null;
					if (flag13)
					{
						result = null;
					}
					else
					{
						this.AddNotice(working, tryToBookPotentialBooking2.StartDateTime, "Shifted time to match start of day: from " + timeToInvestigate.StartDateTime.ToString("yyyy-MM-dd h:mm tt") + " to " + timeToInvestigate.EndDateTime.ToString("h:mm tt"));
						result = tryToBookPotentialBooking2;
					}
				}
			}
			return result;
		}

		// Token: 0x06000DD1 RID: 3537 RVA: 0x00067944 File Offset: 0x00065B44
		private TryToBookPotentialBooking TryToFindRoomAvailabilityWithShiftTimeRule(bool isStartOfDay, IList<TryToBookRoom> roomsToInvestigate, DateTime startDateTime, DateTime endDateTime, TryToBookWorking working)
		{
			foreach (TryToBookRoom room in roomsToInvestigate)
			{
				TryToBookSchedule roomSchedule = this.GetRoomSchedule(room, startDateTime.Date, working.Caches.RoomScheduleCache, working.SearchOptions.RoomAvailabilityScheduleMappings, working.SearchOptions.BufferMinutesPre, working.SearchOptions.BufferMinutesPost);
				IList<TryToBookAvailability> justAvailability = roomSchedule.JustAvailability;
				bool flag = justAvailability.Count <= 0;
				if (!flag)
				{
					DateTime startDateTime2;
					DateTime endDateTime2;
					if (isStartOfDay)
					{
						DateTime dateTime = justAvailability.Min((TryToBookAvailability g) => g.StartDateTime);
						startDateTime2 = ((startDateTime < dateTime) ? dateTime : startDateTime);
						endDateTime2 = startDateTime2.AddMinutes((double)working.StudentTestDuration);
					}
					else
					{
						DateTime dateTime2 = justAvailability.Max((TryToBookAvailability g) => g.EndDateTime);
						endDateTime2 = ((endDateTime > dateTime2) ? dateTime2 : endDateTime);
						startDateTime2 = endDateTime2.AddMinutes((double)(-(double)working.StudentTestDuration));
					}
					int num = startDateTime2.Minute % 5;
					bool flag2 = num != 0;
					if (flag2)
					{
						startDateTime2 = startDateTime2.Add(new TimeSpan(0, -num, 0));
						endDateTime2 = startDateTime2.AddMinutes((double)working.StudentTestDuration);
					}
					TryToBookTimeToInvestigate timeToInvestigate = new TryToBookTimeToInvestigate
					{
						StartDateTime = startDateTime2,
						EndDateTime = endDateTime2
					};
					bool flag3;
					TryToBookPotentialBooking tryToBookPotentialBooking = this.TryToFindPotentialBookingForSingleDayTime(timeToInvestigate, working, false, out flag3);
					bool flag4 = tryToBookPotentialBooking != null;
					if (flag4)
					{
						return tryToBookPotentialBooking;
					}
				}
			}
			return null;
		}

		// Token: 0x04000287 RID: 647
		private AutoBooker2DAO dao;

		// Token: 0x020003F8 RID: 1016
		internal class TryToBookRoomWithContextScore
		{
			// Token: 0x170002A7 RID: 679
			// (get) Token: 0x06001930 RID: 6448 RVA: 0x0008FD55 File Offset: 0x0008DF55
			// (set) Token: 0x06001931 RID: 6449 RVA: 0x0008FD5D File Offset: 0x0008DF5D
			public TryToBookRoom Room { get; set; }

			// Token: 0x170002A8 RID: 680
			// (get) Token: 0x06001932 RID: 6450 RVA: 0x0008FD66 File Offset: 0x0008DF66
			// (set) Token: 0x06001933 RID: 6451 RVA: 0x0008FD6E File Offset: 0x0008DF6E
			public int ContextScore { get; set; }
		}

		// Token: 0x020003F9 RID: 1017
		internal class AssetWithFoundAccommodations
		{
			// Token: 0x170002A9 RID: 681
			// (get) Token: 0x06001935 RID: 6453 RVA: 0x0008FD77 File Offset: 0x0008DF77
			// (set) Token: 0x06001936 RID: 6454 RVA: 0x0008FD7F File Offset: 0x0008DF7F
			public string AssetId { get; set; }

			// Token: 0x170002AA RID: 682
			// (get) Token: 0x06001937 RID: 6455 RVA: 0x0008FD88 File Offset: 0x0008DF88
			// (set) Token: 0x06001938 RID: 6456 RVA: 0x0008FD90 File Offset: 0x0008DF90
			public IList<int> ControlIds { get; set; }
		}

		// Token: 0x020003FA RID: 1018
		internal class SnapTimeRule
		{
			// Token: 0x170002AB RID: 683
			// (get) Token: 0x0600193A RID: 6458 RVA: 0x0008FD99 File Offset: 0x0008DF99
			// (set) Token: 0x0600193B RID: 6459 RVA: 0x0008FDA1 File Offset: 0x0008DFA1
			public bool IsValid { get; set; }

			// Token: 0x170002AC RID: 684
			// (get) Token: 0x0600193C RID: 6460 RVA: 0x0008FDAA File Offset: 0x0008DFAA
			// (set) Token: 0x0600193D RID: 6461 RVA: 0x0008FDB2 File Offset: 0x0008DFB2
			public Func<TimeSpan, TimeSpan?> Operation { get; set; }
		}
	}
}
