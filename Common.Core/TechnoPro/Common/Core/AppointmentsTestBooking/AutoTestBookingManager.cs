using System;
using System.Collections.Generic;
using System.Linq;
using ClockWorkLogger;
using NewBooker.Entities.AutoTestBooking.Booker2;
using TechnoPro.Common.Core.Appointments;
using TechnoPro.Common.Core.DynamicForms;
using TechnoPro.Common.Core.LookupCourses;
using TechnoPro.Common.Core.People;
using TechnoPro.Common.Core.Settings;
using TechnoPro.Common.DAO.AppointmentsTestBooking;
using TechnoPro.Common.DAO.AutoTestBooking;
using TechnoPro.Common.ICore.Appointments;
using TechnoPro.Common.ICore.AppointmentsTestBooking;
using TechnoPro.Common.ICore.DynamicForms;
using TechnoPro.Common.ICore.LookupCourses;
using TechnoPro.Common.ICore.People;
using TechnoPro.Common.ICore.Settings;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Adapters;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Accommodations;
using TechnoPro.Common.Public.Entities.Adapters;
using TechnoPro.Common.Public.Entities.Appointments;
using TechnoPro.Common.Public.Entities.AppointmentsTestBooking;
using TechnoPro.Common.Public.Entities.AppointmentsTestBooking.AutoTestBookingHelper;
using TechnoPro.Common.Public.Entities.AppointmentsTestBooking.FullTest;
using TechnoPro.Common.Public.Entities.DynamicForms;
using TechnoPro.Common.Public.Entities.LookupCourses;
using TechnoPro.Common.Public.Entities.People;
using TechnoPro.Common.Public.Entities.Settings;
using TechnoPro.Common.Unity.IoC;

namespace TechnoPro.Common.Core.AppointmentsTestBooking
{
	// Token: 0x0200013B RID: 315
	public class AutoTestBookingManager : IAutoTestBookingManager, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x06000DD6 RID: 3542 RVA: 0x00067D51 File Offset: 0x00065F51
		public AutoTestBookingManager(OperationContext opContext)
		{
			this.OpContext = opContext;
			this.dao = new AutoTestBookingDAO(this.OpContext);
		}

		// Token: 0x170001E9 RID: 489
		// (get) Token: 0x06000DD7 RID: 3543 RVA: 0x00067D88 File Offset: 0x00065F88
		// (set) Token: 0x06000DD8 RID: 3544 RVA: 0x00067D90 File Offset: 0x00065F90
		public OperationContext OpContext { get; set; }

		// Token: 0x06000DD9 RID: 3545 RVA: 0x00067D9C File Offset: 0x00065F9C
		public void ClearAutoTestBookingCache(eTestExamSettingType TestType, ISettingManager sm, ICacheStorageManager cache, string ClockWorkSettingsInstanceName)
		{
			bool flag = sm == null;
			if (flag)
			{
				sm = (string.IsNullOrEmpty(ClockWorkSettingsInstanceName) ? SettingManager.CurrentInstance : SettingManager.GetInstance(ClockWorkSettingsInstanceName));
			}
			bool flag2 = cache == null;
			if (flag2)
			{
				cache = CacheStorageManager.GetCacheManager(ClockWorkSettingsInstanceName);
			}
			cache.Remove((TestType == eTestExamSettingType.Final) ? "AutoTestBooking_AvailableAssets_FINAL" : "AutoTestBooking_AvailableAssets_MIDTERM");
			cache.Remove((TestType == eTestExamSettingType.Final) ? "AutoTestBooking_AvailableRooms_FINAL" : "AutoTestBooking_AvailableRooms_MIDTERM");
			cache.Remove((TestType == eTestExamSettingType.Final) ? "AutoTestBooking_TestRules_FINAL" : "AutoTestBooking_TestRules_MIDTERM");
			cache.Remove((TestType == eTestExamSettingType.Final) ? "AutoTestBooking_SpecialAccommodations_FINAL" : "AutoTestBooking_SpecialAccommodations_MIDTERM");
			cache.Remove((TestType == eTestExamSettingType.Final) ? "AutoTestBooking_Request_FINAL" : "AutoTestBooking_Request_MIDTERM");
			sm.RemoveSettings((TestType == eTestExamSettingType.Final) ? TechnoPro.Common.Public.Entities.Settings.Group.EXAMBOOKING : TechnoPro.Common.Public.Entities.Settings.Group.TESTBOOKING);
		}

		// Token: 0x06000DDA RID: 3546 RVA: 0x00067E68 File Offset: 0x00066068
		public IList<Asset> LoadAvailableAssets(eTestExamSettingType TestType, ISettingManager sm, ICacheStorageManager cache, string ClockWorkSettingsInstanceName, bool clearCacheFirst)
		{
			bool flag = sm == null;
			if (flag)
			{
				sm = (string.IsNullOrEmpty(ClockWorkSettingsInstanceName) ? SettingManager.CurrentInstance : SettingManager.GetInstance(ClockWorkSettingsInstanceName));
			}
			bool flag2 = cache == null;
			if (flag2)
			{
				cache = CacheStorageManager.GetCacheManager(ClockWorkSettingsInstanceName);
			}
			string key = (TestType == eTestExamSettingType.Final) ? "AutoTestBooking_AvailableAssets_FINAL" : "AutoTestBooking_AvailableAssets_MIDTERM";
			IList<Asset> list = clearCacheFirst ? null : ((IList<Asset>)cache[key]);
			bool flag3 = list == null;
			if (flag3)
			{
				string xml = (TestType == eTestExamSettingType.Final) ? sm.GetSettingValue<string>(Setting.EXAMBOOKING_Assets) : sm.GetSettingValue<string>(Setting.TESTBOOKING_Assets);
				list = Asset.LoadAssets(xml);
				cache.Insert(key, list, this.timeout);
			}
			return list;
		}

		// Token: 0x06000DDB RID: 3547 RVA: 0x00067F18 File Offset: 0x00066118
		public IList<SpecialAccommodation> LoadSpecialAccommodations(eTestExamSettingType TestType, ISettingManager sm, ICacheStorageManager cache, string ClockWorkSettingsInstanceName, bool clearCacheFirst)
		{
			bool flag = sm == null;
			if (flag)
			{
				sm = (string.IsNullOrEmpty(ClockWorkSettingsInstanceName) ? SettingManager.CurrentInstance : SettingManager.GetInstance(ClockWorkSettingsInstanceName));
			}
			bool flag2 = cache == null;
			if (flag2)
			{
				cache = CacheStorageManager.GetCacheManager(ClockWorkSettingsInstanceName);
			}
			string key = (TestType == eTestExamSettingType.Final) ? "AutoTestBooking_SpecialAccommodations_FINAL" : "AutoTestBooking_SpecialAccommodations_MIDTERM";
			IList<SpecialAccommodation> list = clearCacheFirst ? null : ((IList<SpecialAccommodation>)cache[key]);
			bool flag3 = list == null;
			if (flag3)
			{
				string xml = (TestType == eTestExamSettingType.Final) ? sm.GetSettingValue<string>(Setting.EXAMBOOKING_SpecialAccommodations) : sm.GetSettingValue<string>(Setting.TESTBOOKING_SpecialAccommodations);
				list = (from g in SpecialAccommodation.LoadSpecialAccommodations(xml, "")
				where g.IsActive
				select g).ToList<SpecialAccommodation>();
				cache.Insert(key, list, this.timeout);
			}
			return list;
		}

		// Token: 0x06000DDC RID: 3548 RVA: 0x00067FF4 File Offset: 0x000661F4
		public IList<Room> LoadAvailableRooms(eTestExamSettingType TestType, ISettingManager sm, ICacheStorageManager cache, IList<Asset> availableAssets, string ClockWorkSettingsInstanceName, bool clearCacheFirst)
		{
			bool flag = sm == null;
			if (flag)
			{
				sm = (string.IsNullOrEmpty(ClockWorkSettingsInstanceName) ? SettingManager.CurrentInstance : SettingManager.GetInstance(ClockWorkSettingsInstanceName));
			}
			bool flag2 = cache == null;
			if (flag2)
			{
				cache = CacheStorageManager.GetCacheManager(ClockWorkSettingsInstanceName);
			}
			string key = (TestType == eTestExamSettingType.Final) ? "AutoTestBooking_AvailableRooms_FINAL" : "AutoTestBooking_AvailableRooms_MIDTERM";
			IList<Room> list = clearCacheFirst ? null : ((IList<Room>)cache[key]);
			bool flag3 = list == null;
			if (flag3)
			{
				string xml = (TestType == eTestExamSettingType.Final) ? sm.GetSettingValue<string>(Setting.EXAMBOOKING_Rooms) : sm.GetSettingValue<string>(Setting.TESTBOOKING_Rooms);
				bool flag4 = availableAssets == null;
				if (flag4)
				{
					availableAssets = this.LoadAvailableAssets(TestType, sm, cache, ClockWorkSettingsInstanceName, clearCacheFirst);
				}
				list = Room.LoadRooms(xml, availableAssets);
				cache.Insert(key, list, this.timeout);
			}
			return list;
		}

		// Token: 0x06000DDD RID: 3549 RVA: 0x000680C0 File Offset: 0x000662C0
		public IList<TestRule> LoadTestRules(eTestExamSettingType TestType, ISettingManager sm, ICacheStorageManager cache, string ClockWorkSettingsInstanceName, bool clearCacheFirst)
		{
			string key = (TestType == eTestExamSettingType.Final) ? "AutoTestBooking_TestRules_FINAL" : "AutoTestBooking_TestRules_MIDTERM";
			IList<TestRule> list = clearCacheFirst ? null : ((IList<TestRule>)cache[key]);
			bool flag = list == null;
			if (flag)
			{
				string xml = (TestType == eTestExamSettingType.Final) ? sm.GetSettingValue<string>(Setting.EXAMBOOKING_Rules) : sm.GetSettingValue<string>(Setting.TESTBOOKING_Rules);
				list = TestRule.FromXml(xml);
				cache.Insert(key, list, this.timeout);
			}
			return list;
		}

		// Token: 0x06000DDE RID: 3550 RVA: 0x00068138 File Offset: 0x00066338
		public FindPotentialBookingsReq LoadBaseAutoTestBookingSettings(eTestExamSettingType TestType, eAutoTestBookingContext TestBookingContext, string ClockWorkSettingsInstanceName, bool clearCacheFirst)
		{
			ISettingManager settingManager = string.IsNullOrEmpty(ClockWorkSettingsInstanceName) ? SettingManager.CurrentInstance : SettingManager.GetInstance(ClockWorkSettingsInstanceName);
			ICacheStorageManager cacheManager = CacheStorageManager.GetCacheManager(ClockWorkSettingsInstanceName);
			if (clearCacheFirst)
			{
				settingManager.RemoveSettings((TestType == eTestExamSettingType.Final) ? TechnoPro.Common.Public.Entities.Settings.Group.EXAMBOOKING : TechnoPro.Common.Public.Entities.Settings.Group.TESTBOOKING);
			}
			string key = (TestType == eTestExamSettingType.Final) ? "AutoTestBooking_Request_FINAL" : "AutoTestBooking_Request_MIDTERM";
			FindPotentialBookingsReq findPotentialBookingsReq = clearCacheFirst ? null : ((FindPotentialBookingsReq)cacheManager[key]);
			bool flag = findPotentialBookingsReq != null;
			FindPotentialBookingsReq result;
			if (flag)
			{
				result = findPotentialBookingsReq.Clone();
			}
			else
			{
				IList<Asset> availableAssets = this.LoadAvailableAssets(TestType, settingManager, cacheManager, ClockWorkSettingsInstanceName, clearCacheFirst);
				IList<Room> availableRooms = this.LoadAvailableRooms(TestType, settingManager, cacheManager, availableAssets, ClockWorkSettingsInstanceName, clearCacheFirst);
				IList<TestRule> rules = this.LoadTestRules(TestType, settingManager, cacheManager, ClockWorkSettingsInstanceName, clearCacheFirst);
				IList<SpecialAccommodation> list = this.LoadSpecialAccommodations(TestType, settingManager, cacheManager, ClockWorkSettingsInstanceName, clearCacheFirst);
				bool flag2 = TestBookingContext == eAutoTestBookingContext.StudentBooking;
				if (flag2)
				{
					string settingValue = settingManager.GetSettingValue<string>((TestType == eTestExamSettingType.Final) ? Setting.EXAMBOOKING_SpecialAccommodationsToIgnore : Setting.TESTBOOKING_SpecialAccommodationsToIgnore);
					bool flag3 = !string.IsNullOrEmpty(settingValue);
					if (flag3)
					{
						string[] array = settingValue.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
						List<SpecialAccommodationType> typesToIgnore = new List<SpecialAccommodationType>();
						foreach (string s in array)
						{
							int num;
							bool flag4 = int.TryParse(s, out num);
							if (flag4)
							{
								bool flag5 = Enum.IsDefined(typeof(SpecialAccommodationType), num);
								if (flag5)
								{
									SpecialAccommodationType item = (SpecialAccommodationType)num;
									bool flag6 = !typesToIgnore.Contains(item);
									if (flag6)
									{
										typesToIgnore.Add(item);
									}
								}
							}
						}
						bool flag7 = typesToIgnore.Count > 0;
						if (flag7)
						{
							list = (from g in list
							where !typesToIgnore.Contains(g.SpecialAccommodationType)
							select g).ToList<SpecialAccommodation>();
						}
					}
				}
				string text = (TestType == eTestExamSettingType.Final) ? settingManager.GetSettingValue<string>(Setting.EXAMBOOKING_code_FindPotentialBookingsStart) : settingManager.GetSettingValue<string>(Setting.TESTBOOKING_code_FindPotentialBookingsStart);
				string value = (TestType == eTestExamSettingType.Final) ? settingManager.GetSettingValue<string>(Setting.EXAMBOOKING_code_FindPotentialBookingsMid) : settingManager.GetSettingValue<string>(Setting.TESTBOOKING_code_FindPotentialBookingsMid);
				string text2 = (TestType == eTestExamSettingType.Final) ? settingManager.GetSettingValue<string>(Setting.EXAMBOOKING_code_FindPotentialBookingsEnd) : settingManager.GetSettingValue<string>(Setting.TESTBOOKING_code_FindPotentialBookingsEnd);
				string text3 = (TestType == eTestExamSettingType.Final) ? settingManager.GetSettingValue<string>(Setting.EXAMBOOKING_code_FindPotentialBookingsMisc) : settingManager.GetSettingValue<string>(Setting.TESTBOOKING_code_FindPotentialBookingsMisc);
				bool flag8 = !string.IsNullOrEmpty(text) || !string.IsNullOrEmpty(value) || !string.IsNullOrEmpty(text2) || !string.IsNullOrEmpty(text3);
				findPotentialBookingsReq = new FindPotentialBookingsReq
				{
					Accommodations = null,
					Pid = 0,
					Lucid = 0,
					TestBookingType = TestType,
					AppIdToIgnoreWhenCheckingStudentsSchedule = 0,
					ApplySpecialAccommodationRules = true,
					AvailableAssets = availableAssets,
					AvailableRooms0 = availableRooms,
					BufferMinutesPost = settingManager.GetSettingValue<int>(Setting.TESTBOOKING_BufferMinutesPost),
					BufferMinutesPre = settingManager.GetSettingValue<int>(Setting.TESTBOOKING_BufferMinutesPre),
					ClassTest = null,
					CustomTestBookingRules = (flag8 ? new CustomTestBookingRulesClass(text3, text, text2) : null),
					DayToLookIn = DateTime.Now,
					DebugMode = false,
					IgnoreStudentAppointmentIds = null,
					IgnoreStudentsSchedule = ((TestType == eTestExamSettingType.Final) ? settingManager.GetSettingValue<bool>(Setting.EXAMBOOKING_IgnoreStudentSchedule) : settingManager.GetSettingValue<bool>(Setting.TESTBOOKING_IgnoreStudentSchedule)),
					IgnoreTimetable = ((TestType == eTestExamSettingType.Final) ? settingManager.GetSettingValue<bool>(Setting.EXAMBOOKING_IgnoreStudentTimetable) : settingManager.GetSettingValue<bool>(Setting.TESTBOOKING_IgnoreStudentTimetable)),
					IgnoreTwoTestsSameCourseSameDay = ((TestType == eTestExamSettingType.Final) ? settingManager.GetSettingValue<bool>(Setting.EXAMBOOKING_IgnoreStudentTwoTestsSameCourseSameDay) : settingManager.GetSettingValue<bool>(Setting.TESTBOOKING_IgnoreStudentTwoTestsSameCourseSameDay)),
					LoadRoomSchedules = false,
					OverrideRoomAvailabilityPid = ((TestType == eTestExamSettingType.Final) ? settingManager.GetSettingValue<int>(Setting.EXAMBOOKING_OverrideRoomPidForAvailability) : settingManager.GetSettingValue<int>(Setting.TESTBOOKING_OverrideRoomPidForAvailability)),
					RestrictByCampus = ((TestType == eTestExamSettingType.Final) ? settingManager.GetSettingValue<bool>(Setting.EXAMBOOKING_RestrictCoursesToCampus_EnableMatchCampusToRoom) : settingManager.GetSettingValue<bool>(Setting.TESTBOOKING_RestrictCoursesToCampus_EnableMatchCampusToRoom)),
					Rules = rules,
					SpecialAccommodations = list,
					TestBookingContext = TestBookingContext,
					UnavailableRoomBookings = null
				};
				cacheManager.Insert(key, findPotentialBookingsReq, this.timeout);
				result = findPotentialBookingsReq;
			}
			return result;
		}

		// Token: 0x06000DDF RID: 3551 RVA: 0x00068540 File Offset: 0x00066740
		public FindPotentialBookingsResp FindPotentialBookings(eTestExamSettingType testType, eAutoTestBookingContext testBookingContext, int pid, int lucid, DateTime classStartDateTime, DateTime classEndDateTime, IList<Accommodation> accs, string ClockWorkInstanceName, bool clearCacheFirst, bool debugMode)
		{
			FindPotentialBookingsReq findPotentialBookingsReq = this.LoadBaseAutoTestBookingSettings(testType, testBookingContext, ClockWorkInstanceName, clearCacheFirst);
			findPotentialBookingsReq.DebugMode = debugMode;
			findPotentialBookingsReq.Pid = pid;
			findPotentialBookingsReq.ClassTest = new TechnoPro.Common.Public.Entities.AppointmentsTestBooking.AutoTestBookingHelper.Test(classStartDateTime, classEndDateTime, null);
			findPotentialBookingsReq.Lucid = lucid;
			findPotentialBookingsReq.Accommodations = accs;
			return this.FindPotentialBookingsExplicit(findPotentialBookingsReq);
		}

		// Token: 0x06000DE0 RID: 3552 RVA: 0x0006859C File Offset: 0x0006679C
		public FindPotentialBookingsResp FindPotentialBookingsExplicit(FindPotentialBookingsReq req)
		{
			return this.dao.FindPotentialBookingsExplicit(req);
		}

		// Token: 0x06000DE1 RID: 3553 RVA: 0x000685BC File Offset: 0x000667BC
		public FindPotentialBookingsResp FindPotentialBookings(eTestExamSettingType testType, eAutoTestBookingContext testBookingContext, int pid, int lucid, DateTime classStartDateTime, DateTime classEndDateTime, IList<Accommodation> accs, string ClockWorkInstanceName, bool clearCacheFirst)
		{
			return this.FindPotentialBookings(testType, testBookingContext, pid, lucid, classStartDateTime, classEndDateTime, accs, ClockWorkInstanceName, clearCacheFirst, false);
		}

		// Token: 0x06000DE2 RID: 3554 RVA: 0x000685E4 File Offset: 0x000667E4
		public AutoRescheduleTestExamResult AutoRescheduleTestOrExam(int appId)
		{
			ITestBookingManager testBookingManager = new TestBookingManager(this.OpContext);
			TechnoPro.Common.Public.Entities.AppointmentsTestBooking.Test test = testBookingManager.LoadTestByAppointmentId(appId);
			eClassTestType examType = test.ClassTestInfo.ExamType;
			eTestExamSettingType[] source = (eTestExamSettingType[])Enum.GetValues(typeof(eTestExamSettingType));
			eTestExamSettingType eTestExamSettingType = source.FirstOrDefault(delegate(eTestExamSettingType g)
			{
				TestExamSettingTypeAttribute attribute = g.GetAttribute<TestExamSettingTypeAttribute>();
				eClassTestType eClassTestType = (attribute != null) ? attribute.ClassTestType : eClassTestType.Unknown;
				return eClassTestType == examType;
			});
			eTestExamSettingType testType = eTestExamSettingType;
			eAutoTestBookingContext testBookingContext = eAutoTestBookingContext.StaffBooking;
			PersonBase firstStudent = test.GetFirstStudent();
			int pid = (firstStudent != null) ? firstStudent.PersonId : 0;
			ClassTestBase classTestInfo = test.ClassTestInfo;
			int? num;
			if (classTestInfo == null)
			{
				num = null;
			}
			else
			{
				LookupCourseBase course = classTestInfo.Course;
				num = ((course != null) ? new int?(course.LuCourseId) : null);
			}
			int? num2 = num;
			AutoBookTestExamPreviewResult autoBookTestExamPreviewResult = this.AutoRescheduleTestOrExamPreview(appId, testType, testBookingContext, pid, num2.GetValueOrDefault(), test.ClassTestInfo.StartDateTime, test.ClassTestInfo.EndDateTime, true);
			bool flag = !autoBookTestExamPreviewResult.Succeeded;
			AutoRescheduleTestExamResult result;
			if (flag)
			{
				result = new AutoRescheduleTestExamResult
				{
					PreviewResult = autoBookTestExamPreviewResult
				};
			}
			else
			{
				IBaseAppointmentManager baseAppointmentManager = new BaseAppointmentManager(this.OpContext);
				BaseBasicAppointment baseBasicAppointment = baseAppointmentManager.LoadBaseBasicAppointmentById(appId);
				baseBasicAppointment.StartDateTime = autoBookTestExamPreviewResult.PotentialStartDateTime.Value;
				baseBasicAppointment.EndDateTime = autoBookTestExamPreviewResult.PotentialEndDateTime.Value;
				baseAppointmentManager.UpdateAppointmentParts(false, baseBasicAppointment, eAppointmentPart.DateTimeAndDuration);
				testBookingManager.UpdateBreakTime(appId, autoBookTestExamPreviewResult.AppliedBreakMinutes);
				Attendee attendee = baseBasicAppointment.Attendees.FirstOrDefault((Attendee g) => g.Person.CoreGroup == eCoreGroup.Rooms);
				int num3 = (attendee != null) ? attendee.Person.PersonId : 0;
				IAppointmentAttendeeManager appointmentAttendeeManager = new AppointmentAttendeeManager(this.OpContext);
				bool flag2 = num3 > 0;
				if (flag2)
				{
					appointmentAttendeeManager.SwapAttendee(false, appId, num3, autoBookTestExamPreviewResult.PotentialRoom.RoomId);
				}
				else
				{
					appointmentAttendeeManager.InsertOrUpdateAppointmentAttendee(false, appId, new Attendee
					{
						Person = new PersonBase
						{
							PersonId = autoBookTestExamPreviewResult.PotentialRoom.RoomId
						}
					});
				}
				IStudentClassTestInfoManager studentClassTestInfoManager = new StudentClassTestInfoManager(this.OpContext);
				studentClassTestInfoManager.UpdateStudentReportedClassDateAndTimeToMatchExamClassDateAndTime(appId);
				result = new AutoRescheduleTestExamResult
				{
					Successful = true,
					PreviewResult = autoBookTestExamPreviewResult
				};
			}
			return result;
		}

		// Token: 0x06000DE3 RID: 3555 RVA: 0x00068814 File Offset: 0x00066A14
		private AppType FindFinalExamAppTypeToUseForNewAutoBooking()
		{
			ICacheStorageManager cacheStorageManager = ObjectFactory.Resolve<ICacheStorageManager>();
			AppType appType = cacheStorageManager["newautoexamapptypeid"] as AppType;
			bool flag = appType != null;
			AppType result;
			if (flag)
			{
				result = appType;
			}
			else
			{
				int num = this.dao.FindFinalExamAppTypeToUseForNewExamAutoBooking();
				bool flag2 = num < 0;
				if (flag2)
				{
					result = null;
				}
				else
				{
					IAppointmentTypeManager appointmentTypeManager = new AppointmentTypeManager(this.OpContext);
					appType = appointmentTypeManager.LoadAppTypeById(num);
					bool flag3 = appType == null;
					if (flag3)
					{
						result = null;
					}
					else
					{
						cacheStorageManager.Insert("newautoexamapptypeid", appType, TimeSpan.FromHours(10.0));
						result = appType;
					}
				}
			}
			return result;
		}

		// Token: 0x06000DE4 RID: 3556 RVA: 0x000688A8 File Offset: 0x00066AA8
		public AutoBookTestExamResult AutoBookTestOrExam(int examId, eTestExamSettingType testType, eAutoTestBookingContext testBookingContext, int pid, int lucid, DateTime classStartDateTime, DateTime classEndDateTime, bool clearCacheFirst)
		{
			AutoBookTestExamPreviewResult autoBookTestExamPreviewResult = this.AutoBookTestOrExamPreview(testType, testBookingContext, pid, lucid, classStartDateTime, classEndDateTime, clearCacheFirst);
			AutoBookTestExamResult autoBookTestExamResult = new AutoBookTestExamResult(autoBookTestExamPreviewResult);
			bool flag = !autoBookTestExamResult.Succeeded;
			AutoBookTestExamResult result;
			if (flag)
			{
				result = autoBookTestExamResult;
			}
			else
			{
				bool flag2 = autoBookTestExamResult.PotentialStartDateTime == null || autoBookTestExamResult.PotentialEndDateTime == null;
				if (flag2)
				{
					autoBookTestExamResult.Failures.Add(new TryToBookFailure
					{
						Type = eTryToBookFailureType.Unknown
					});
					result = autoBookTestExamResult;
				}
				else
				{
					AppType appType = this.FindFinalExamAppTypeToUseForNewAutoBooking();
					ITestBookingManager testBookingManager = new TestBookingManager(this.OpContext);
					TestForEdit2 test = new TestForEdit2
					{
						Room = autoBookTestExamResult.PotentialRoom,
						Attendees = new List<Attendee>
						{
							new Attendee(new PersonBase
							{
								PersonId = pid,
								CoreGroup = eCoreGroup.Students
							}, false, 0)
						},
						ExamId = examId,
						AppType = appType,
						BookingSpecificInfo = new TestForEditBookingSpecific
						{
							BookingNote = "Scheduled using batch scheduler",
							AccommodationCids = autoBookTestExamPreviewResult.AccommodationCids
						},
						ClassTestDefinitionSpecificInfo = new TestForEditClassDefinitionSpecific
						{
							ExamType = testType.GetAttribute<TestExamSettingTypeAttribute>().ClassTestType
						},
						Memo = "Auto booked on " + DateTime.Now.ToString("yyyy-MM-dd h:mm tt"),
						StartDateTime = autoBookTestExamResult.PotentialStartDateTime.Value,
						EndDateTime = autoBookTestExamResult.PotentialEndDateTime.Value,
						BreakTimeMinutes = autoBookTestExamPreviewResult.AppliedBreakMinutes
					};
					int appointmentId = testBookingManager.CreateTest(test, null, null, null, null);
					autoBookTestExamResult.AppointmentId = appointmentId;
					result = autoBookTestExamResult;
				}
			}
			return result;
		}

		// Token: 0x06000DE5 RID: 3557 RVA: 0x00068A60 File Offset: 0x00066C60
		public AutoBookTestExamPreviewResult AutoBookTestOrExamPreview(eTestExamSettingType testType, eAutoTestBookingContext testBookingContext, int pid, int lucid, DateTime classStartDateTime, DateTime classEndDateTime, bool clearCacheFirst)
		{
			IAccommodationsManager accommodationsManager = new AccommodationsManager(this.OpContext);
			List<AccommodationData> source = (from g in accommodationsManager.LoadAccommodationsByStudentAndCourseOrTemplate(pid, lucid)
			where (g.Detail.Group | eAccommodationGroup.TestExam) == eAccommodationGroup.TestExam
			select g).ToList<AccommodationData>();
			List<int> list = source.Select(delegate(AccommodationData g)
			{
				DynamicData data = g.Data;
				int? num;
				if (data == null)
				{
					num = null;
				}
				else
				{
					DynamicField field = data.Field;
					num = ((field != null) ? new int?(field.ControlId) : null);
				}
				int? num2 = num;
				return num2.GetValueOrDefault();
			}).ToList<int>();
			TryToBookResult tryToBookResult = this.TryToFindBooking(testType, true, pid, lucid, classStartDateTime, Convert.ToInt32((classEndDateTime - classStartDateTime).TotalMinutes), list, false, 0, new List<TryToBookAccommodationToUse>(), true, null);
			IPeopleManager peopleManager = new PeopleManager(this.OpContext);
			ILookupCourseManager lookupCourseManager = new LookupCourseManager(this.OpContext);
			AutoBookTestExamPreviewResult autoBookTestExamPreviewResult = new AutoBookTestExamPreviewResult();
			AutoBookTestExamPreviewResult autoBookTestExamPreviewResult2 = autoBookTestExamPreviewResult;
			PersonBase personBase = peopleManager.LoadPerson(pid);
			autoBookTestExamPreviewResult2.Student = ((personBase != null) ? personBase.ToBasicPerson() : null);
			AutoBookTestExamPreviewResult autoBookTestExamPreviewResult3 = autoBookTestExamPreviewResult;
			IList<LookupCourseBase> list2 = lookupCourseManager.LoadCourseBasesByIds(new int[]
			{
				lucid
			});
			autoBookTestExamPreviewResult3.Course = ((list2 != null) ? list2.FirstOrDefault<LookupCourseBase>() : null);
			AutoBookTestExamPreviewResult autoBookTestExamPreviewResult4 = autoBookTestExamPreviewResult;
			bool flag = tryToBookResult.PotentialBookings == null || tryToBookResult.PotentialBookings.Count < 1;
			AutoBookTestExamPreviewResult result;
			if (flag)
			{
				bool flag2 = tryToBookResult.Failures == null || tryToBookResult.Failures.Count < 1;
				if (flag2)
				{
					bool studentIsDoubleBooked = tryToBookResult.StudentIsDoubleBooked;
					eTryToBookFailureType type;
					if (studentIsDoubleBooked)
					{
						type = eTryToBookFailureType.StudentIsDoubleBooked;
					}
					else
					{
						bool studentAlreadyHadAnotherTestBookedForSameDayAndCourse = tryToBookResult.StudentAlreadyHadAnotherTestBookedForSameDayAndCourse;
						if (studentAlreadyHadAnotherTestBookedForSameDayAndCourse)
						{
							type = eTryToBookFailureType.StudentAlreadyBookedATestForThisClassDateTime;
						}
						else
						{
							type = eTryToBookFailureType.Unknown;
						}
					}
					tryToBookResult.Failures = new TryToBookFailure[]
					{
						new TryToBookFailure
						{
							Type = type
						}
					}.ToList<TryToBookFailure>();
				}
				result = new AutoBookTestExamPreviewResult
				{
					Succeeded = false,
					Failures = tryToBookResult.Failures,
					AccommodationCids = list
				};
			}
			else
			{
				TryToBookPotentialBooking tryToBookPotentialBooking = tryToBookResult.PotentialBookings[0];
				autoBookTestExamPreviewResult4.Succeeded = true;
				autoBookTestExamPreviewResult4.AppliedBreakMinutes = tryToBookResult.AppliedBreakMinutes;
				autoBookTestExamPreviewResult4.AccommodationCids = list;
				autoBookTestExamPreviewResult4.PotentialStartDateTime = new DateTime?(tryToBookPotentialBooking.StartDateTime);
				autoBookTestExamPreviewResult4.PotentialEndDateTime = new DateTime?(tryToBookPotentialBooking.EndDateTime);
				AutoBookTestExamPreviewResult autoBookTestExamPreviewResult5 = autoBookTestExamPreviewResult4;
				AppointmentRoom potentialRoom;
				if (tryToBookPotentialBooking.Room != null)
				{
					AppointmentRoom appointmentRoom = new AppointmentRoom();
					appointmentRoom.RoomId = tryToBookPotentialBooking.Room.PersonId;
					potentialRoom = appointmentRoom;
					appointmentRoom.RoomDescription = tryToBookPotentialBooking.Room.Title;
				}
				else
				{
					potentialRoom = null;
				}
				autoBookTestExamPreviewResult5.PotentialRoom = potentialRoom;
				result = autoBookTestExamPreviewResult4;
			}
			return result;
		}

		// Token: 0x06000DE6 RID: 3558 RVA: 0x00068CC0 File Offset: 0x00066EC0
		public AutoBookTestExamPreviewResult AutoRescheduleTestOrExamPreview(int existingAppId, eTestExamSettingType testType, eAutoTestBookingContext testBookingContext, int pid, int lucid, DateTime classStartDateTime, DateTime classEndDateTime, bool clearCacheFirst)
		{
			IAccommodationsManager accommodationsManager = new AccommodationsManager(this.OpContext);
			List<AccommodationData> source = (from g in accommodationsManager.LoadAccommodationsByStudentAndCourseOrTemplate(pid, lucid)
			where (g.Detail.Group | eAccommodationGroup.TestExam) == eAccommodationGroup.TestExam
			select g).ToList<AccommodationData>();
			TryToBookResult tryToBookResult = this.TryToFindBooking(testType, true, pid, lucid, classStartDateTime, Convert.ToInt32((classEndDateTime - classStartDateTime).TotalMinutes), source.Select(delegate(AccommodationData g)
			{
				DynamicData data = g.Data;
				int? num;
				if (data == null)
				{
					num = null;
				}
				else
				{
					DynamicField field = data.Field;
					num = ((field != null) ? new int?(field.ControlId) : null);
				}
				int? num2 = num;
				return num2.GetValueOrDefault();
			}).ToList<int>(), false, existingAppId, new List<TryToBookAccommodationToUse>(), true, null);
			IPeopleManager peopleManager = new PeopleManager(this.OpContext);
			ILookupCourseManager lookupCourseManager = new LookupCourseManager(this.OpContext);
			AutoBookTestExamPreviewResult autoBookTestExamPreviewResult = new AutoBookTestExamPreviewResult();
			AutoBookTestExamPreviewResult autoBookTestExamPreviewResult2 = autoBookTestExamPreviewResult;
			PersonBase personBase = peopleManager.LoadPerson(pid);
			autoBookTestExamPreviewResult2.Student = ((personBase != null) ? personBase.ToBasicPerson() : null);
			AutoBookTestExamPreviewResult autoBookTestExamPreviewResult3 = autoBookTestExamPreviewResult;
			IList<LookupCourseBase> list = lookupCourseManager.LoadCourseBasesByIds(new int[]
			{
				lucid
			});
			autoBookTestExamPreviewResult3.Course = ((list != null) ? list.FirstOrDefault<LookupCourseBase>() : null);
			AutoBookTestExamPreviewResult autoBookTestExamPreviewResult4 = autoBookTestExamPreviewResult;
			bool flag = tryToBookResult.PotentialBookings == null || tryToBookResult.PotentialBookings.Count < 1;
			AutoBookTestExamPreviewResult result;
			if (flag)
			{
				result = new AutoBookTestExamPreviewResult
				{
					Succeeded = false,
					Failures = tryToBookResult.Failures
				};
			}
			else
			{
				TryToBookPotentialBooking tryToBookPotentialBooking = tryToBookResult.PotentialBookings[0];
				autoBookTestExamPreviewResult4.Succeeded = true;
				autoBookTestExamPreviewResult4.AppliedBreakMinutes = tryToBookResult.AppliedBreakMinutes;
				autoBookTestExamPreviewResult4.PotentialStartDateTime = new DateTime?(tryToBookPotentialBooking.StartDateTime);
				autoBookTestExamPreviewResult4.PotentialEndDateTime = new DateTime?(tryToBookPotentialBooking.EndDateTime);
				AutoBookTestExamPreviewResult autoBookTestExamPreviewResult5 = autoBookTestExamPreviewResult4;
				AppointmentRoom potentialRoom;
				if (tryToBookPotentialBooking.Room != null)
				{
					AppointmentRoom appointmentRoom = new AppointmentRoom();
					appointmentRoom.RoomId = tryToBookPotentialBooking.Room.PersonId;
					potentialRoom = appointmentRoom;
					appointmentRoom.RoomDescription = tryToBookPotentialBooking.Room.Title;
				}
				else
				{
					potentialRoom = null;
				}
				autoBookTestExamPreviewResult5.PotentialRoom = potentialRoom;
				result = autoBookTestExamPreviewResult4;
			}
			return result;
		}

		// Token: 0x06000DE7 RID: 3559 RVA: 0x00068EA4 File Offset: 0x000670A4
		public int CalculateExtraTime(eTestExamSettingType TestType, int ClassTestDurationInMinutes, IList<Accommodation> AccommodationsToUse)
		{
			IList<SpecialAccommodation> source = this.LoadSpecialAccommodations(TestType, null, null, null, false);
			AutoBooker2Manager autoBooker2Manager = new AutoBooker2Manager(this.OpContext);
			return autoBooker2Manager.CalculateExtraTime(ClassTestDurationInMinutes, source.Select(delegate(SpecialAccommodation g)
			{
				TryToBookSpecialAccommodation tryToBookSpecialAccommodation = new TryToBookSpecialAccommodation();
				tryToBookSpecialAccommodation.ControlId = g.ControlId;
				tryToBookSpecialAccommodation.Args = g.Args.Keys.Cast<string>().ToDictionary((string key) => key, (string key) => g.Args[key]);
				tryToBookSpecialAccommodation.Type = (eSpecialAccommodationType)(Enum.IsDefined(typeof(eSpecialAccommodationType), (int)g.SpecialAccommodationType) ? g.SpecialAccommodationType : SpecialAccommodationType.Unknown);
				return tryToBookSpecialAccommodation;
			}).ToList<TryToBookSpecialAccommodation>(), (from g in AccommodationsToUse
			select new TryToBookAccommodationToUse
			{
				ControlId = g.ControlId,
				Caption = g.Title,
				Value = (g.LookupText ?? "") + (g.SubText ?? "")
			}).ToList<TryToBookAccommodationToUse>());
		}

		// Token: 0x06000DE8 RID: 3560 RVA: 0x00068F2C File Offset: 0x0006712C
		public int CalculateBreakTime(eTestExamSettingType TestType, int ClassTestDurationInMinutes, IList<Accommodation> AccommodationsToUse)
		{
			IList<SpecialAccommodation> source = this.LoadSpecialAccommodations(TestType, null, null, null, false);
			AutoBooker2Manager autoBooker2Manager = new AutoBooker2Manager(this.OpContext);
			return autoBooker2Manager.CalculateBreakTime(ClassTestDurationInMinutes, source.Select(delegate(SpecialAccommodation g)
			{
				TryToBookSpecialAccommodation tryToBookSpecialAccommodation = new TryToBookSpecialAccommodation();
				tryToBookSpecialAccommodation.ControlId = g.ControlId;
				tryToBookSpecialAccommodation.Args = g.Args.Keys.Cast<string>().ToDictionary((string key) => key, (string key) => g.Args[key]);
				tryToBookSpecialAccommodation.Type = (eSpecialAccommodationType)(Enum.IsDefined(typeof(eSpecialAccommodationType), (int)g.SpecialAccommodationType) ? g.SpecialAccommodationType : SpecialAccommodationType.Unknown);
				return tryToBookSpecialAccommodation;
			}).ToList<TryToBookSpecialAccommodation>(), (from g in AccommodationsToUse
			select new TryToBookAccommodationToUse
			{
				ControlId = g.ControlId,
				Caption = g.Title,
				Value = (g.LookupText ?? "") + (g.SubText ?? "")
			}).ToList<TryToBookAccommodationToUse>());
		}

		// Token: 0x06000DE9 RID: 3561 RVA: 0x00068FB4 File Offset: 0x000671B4
		public ApplySpecialAccommodationsResp ApplySpecialAccommodations(bool debugMode, eTestExamSettingType testType, eAutoTestBookingContext testBookingContext, int pid, int lucid, DateTime classStartDateTime, DateTime classEndDateTime, IList<Accommodation> accs, string ClockWorkInstanceName, bool clearCacheFirst)
		{
			FindPotentialBookingsReq findPotentialBookingsReq = this.LoadBaseAutoTestBookingSettings(testType, testBookingContext, ClockWorkInstanceName, clearCacheFirst);
			findPotentialBookingsReq.Pid = pid;
			findPotentialBookingsReq.ClassTest = new TechnoPro.Common.Public.Entities.AppointmentsTestBooking.AutoTestBookingHelper.Test(classStartDateTime, classEndDateTime, null);
			findPotentialBookingsReq.Lucid = lucid;
			findPotentialBookingsReq.Accommodations = accs;
			return this.dao.ApplySpecialAccommodationRules(debugMode, pid, lucid, findPotentialBookingsReq.SpecialAccommodations, classStartDateTime, classEndDateTime, accs, findPotentialBookingsReq.AppIdToIgnoreWhenCheckingStudentsSchedule, findPotentialBookingsReq.OverrideRoomAvailabilityPid, findPotentialBookingsReq.AvailableRooms0, findPotentialBookingsReq.IgnoreStudentsSchedule, findPotentialBookingsReq.IgnoreStudentAppointmentIds);
		}

		// Token: 0x06000DEA RID: 3562 RVA: 0x0006903C File Offset: 0x0006723C
		private Dictionary<int, IList<int>> ParseRoomAvailabilityScheduleMappings(string roomAvailabilityMappings)
		{
			return (from h in roomAvailabilityMappings.Split(new char[]
			{
				';'
			}).Select(new Func<string, AutoTestBookingManager.AvailabilityScheduleMapping>(AutoTestBookingManager.ParseAvailabilityScheduleMapping))
			where h != null
			select h).ToDictionary((AutoTestBookingManager.AvailabilityScheduleMapping g) => g.PrimaryRoomPid, (AutoTestBookingManager.AvailabilityScheduleMapping g) => g.SecondaryRoomPids);
		}

		// Token: 0x06000DEB RID: 3563 RVA: 0x000690D8 File Offset: 0x000672D8
		private static AutoTestBookingManager.AvailabilityScheduleMapping ParseAvailabilityScheduleMapping(string s0)
		{
			string text = (s0 ?? "").Trim().Replace(':', ',');
			int num;
			List<int> list = (from g in text.Split(new char[]
			{
				','
			})
			select int.TryParse(g.Trim(), out num) ? num : 0 into h
			where h > 0
			select h).ToList<int>();
			bool flag = list.Count < 2;
			AutoTestBookingManager.AvailabilityScheduleMapping result;
			if (flag)
			{
				result = null;
			}
			else
			{
				result = new AutoTestBookingManager.AvailabilityScheduleMapping
				{
					PrimaryRoomPid = list[0],
					SecondaryRoomPids = list.GetRange(1, list.Count - 1)
				};
			}
			return result;
		}

		// Token: 0x06000DEC RID: 3564 RVA: 0x00069194 File Offset: 0x00067394
		private TryToBookResult TryToFindBookingUsingOldAlgorithm(eTestExamSettingType TestType, bool StaffIsBooking, int PersonId, int LuCourseId, DateTime ClassStartDateTime, int ClassTestDurationInMinutes, IList<TryToBookAccommodationToUse> AccommodationsToUse, bool clearCacheFirst, string ClockWorkInstanceNameToUse = null)
		{
			List<Accommodation> accs = (from g in AccommodationsToUse
			select new Accommodation(g.ControlId, g.Caption, g.Value, 0)).ToList<Accommodation>();
			FindPotentialBookingsResp findPotentialBookingsResp = this.FindPotentialBookings(TestType, StaffIsBooking ? eAutoTestBookingContext.StaffBooking : eAutoTestBookingContext.StudentBooking, PersonId, LuCourseId, ClassStartDateTime, ClassStartDateTime.AddMinutes((double)ClassTestDurationInMinutes), accs, ClockWorkInstanceNameToUse, clearCacheFirst);
			List<string> notices = (from g in findPotentialBookingsResp.PrivateNotes
			select g.Note).ToList<string>();
			TryToBookResult tryToBookResult = new TryToBookResult();
			tryToBookResult.PotentialBookings = (from h in findPotentialBookingsResp.PotentialTests
			where h.PotentialTestStartTime != null && h.PotentialTestDate != null && h.PotentialTestEndTime != null
			select h).Select(delegate(PotentialTest g)
			{
				DateTime date = g.PotentialTestDate.Value.Date;
				return new TryToBookPotentialBooking
				{
					StartDateTime = date.Add(g.PotentialTestStartTime.Value.TimeOfDay),
					EndDateTime = date.Add(g.PotentialTestEndTime.Value.TimeOfDay),
					Notices = notices,
					Room = new TryToBookRoom
					{
						PersonId = g.PotentialRoomPid,
						Title = g.PotentialRoom,
						RoomType = (g.OkToDoubleBook ? eRoomType.VirtualRoom : eRoomType.RegularRoom)
					}
				};
			}).ToList<TryToBookPotentialBooking>();
			return tryToBookResult;
		}

		// Token: 0x06000DED RID: 3565 RVA: 0x0006927C File Offset: 0x0006747C
		public TryToBookResult TryToFindBooking(eTestExamSettingType TestType, bool StaffIsBooking, int PersonId, int LuCourseId, DateTime ClassStartDateTime, int ClassTestDurationInMinutes, IList<int> AccommodationsToUse, bool IgnoreSpecialAccommodations, int BookingAlreadyExistsAppointmentId, IList<TryToBookAccommodationToUse> AdditionalAccommodationsToUse, bool clearCacheFirst, string ClockWorkInstanceNameToUse = null)
		{
			IAccommodationsManager accommodationsManager = new AccommodationsManager(this.OpContext);
			IList<AccommodationData> source = accommodationsManager.LoadAccommodationsByStudentAndCourseOrTemplate(PersonId, LuCourseId);
			List<TryToBookAccommodationToUse> list = (from g in source
			where AccommodationsToUse.Contains(g.Data.Field.ControlId)
			select g).Select(delegate(AccommodationData h)
			{
				object value = h.Data.Value;
				return new TryToBookAccommodationToUse
				{
					ControlId = h.Data.Field.ControlId,
					Caption = h.Data.Field.ControlCaption,
					Value = ((value == null) ? "" : value.ToString())
				};
			}).ToList<TryToBookAccommodationToUse>();
			bool flag = AdditionalAccommodationsToUse != null && AdditionalAccommodationsToUse.Count > 0;
			if (flag)
			{
				list.AddRange(AdditionalAccommodationsToUse);
			}
			ISettingManager settingManager = string.IsNullOrEmpty(ClockWorkInstanceNameToUse) ? SettingManager.CurrentInstance : SettingManager.GetInstance(ClockWorkInstanceNameToUse);
			bool settingValue = settingManager.GetSettingValue<bool>(Setting.TESTBOOKING_UseOldPotentialRoomAlgorithm);
			bool flag2 = settingValue;
			TryToBookResult result;
			if (flag2)
			{
				CWLogger.Logger.Warn("Common.Core.AppointmentsTestBooking.AutoTestBookingManager:TryToFindBooking:Using old 'find potential bookings' algorithm because of setting.");
				result = this.TryToFindBookingUsingOldAlgorithm(TestType, StaffIsBooking, PersonId, LuCourseId, ClassStartDateTime, ClassTestDurationInMinutes, list, clearCacheFirst, ClockWorkInstanceNameToUse);
			}
			else
			{
				string settingValue2 = settingManager.GetSettingValue<string>(Setting.TESTBOOKING_RoomAvailabilityMappings);
				FindPotentialBookingsReq findPotentialBookingsReq = this.LoadBaseAutoTestBookingSettings(TestType, StaffIsBooking ? eAutoTestBookingContext.StaffBooking : eAutoTestBookingContext.StudentBooking, ClockWorkInstanceNameToUse, clearCacheFirst);
				ILookupCourseManager lookupCourseManager = new LookupCourseManager(this.OpContext);
				IList<LookupCourseBase> list2 = lookupCourseManager.LoadCourseBasesByIds(new int[]
				{
					LuCourseId
				});
				LookupCourseBase lookupCourseBase = (list2.Count > 0) ? list2[0] : null;
				TryToBookContext context = new TryToBookContext
				{
					PersonId = PersonId,
					LuCourseId = LuCourseId,
					ClassTestDate = ClassStartDateTime.Date,
					ClassStartTime = ClassStartDateTime.TimeOfDay,
					ClassTestMinutes = ClassTestDurationInMinutes,
					AccommodationsToUse = list,
					CourseCampus = ((lookupCourseBase == null) ? null : lookupCourseBase.Campus)
				};
				bool flag3 = !string.IsNullOrEmpty(settingValue2);
				Dictionary<int, IList<int>> roomAvailabilityScheduleMappings;
				if (flag3)
				{
					roomAvailabilityScheduleMappings = this.ParseRoomAvailabilityScheduleMappings(settingValue2);
				}
				else
				{
					bool flag4 = findPotentialBookingsReq.OverrideRoomAvailabilityPid > 0;
					if (flag4)
					{
						Dictionary<int, IList<int>> dictionary = new Dictionary<int, IList<int>>();
						dictionary.Add(findPotentialBookingsReq.OverrideRoomAvailabilityPid, (from g in findPotentialBookingsReq.AvailableRooms0
						select g.RoomId).ToList<int>());
						roomAvailabilityScheduleMappings = dictionary;
					}
					else
					{
						roomAvailabilityScheduleMappings = new Dictionary<int, IList<int>>();
					}
				}
				TryToBookSearchOptions tryToBookSearchOptions = new TryToBookSearchOptions();
				tryToBookSearchOptions.AllowStudentsToBeDoubleBooked = findPotentialBookingsReq.IgnoreStudentsSchedule;
				tryToBookSearchOptions.AllowStudentsToBookSameCourseSameDay = (findPotentialBookingsReq.IgnoreTwoTestsSameCourseSameDay || BookingAlreadyExistsAppointmentId > 0);
				tryToBookSearchOptions.AllowToBookWithoutAnyAccommodations = true;
				tryToBookSearchOptions.BufferMinutesPost = findPotentialBookingsReq.BufferMinutesPost;
				tryToBookSearchOptions.BufferMinutesPre = findPotentialBookingsReq.BufferMinutesPre;
				tryToBookSearchOptions.Rules = (from g in findPotentialBookingsReq.Rules
				select new TryToBookRule
				{
					AllowShiftingTimeToWorkAroundTimetableForOtherCourses = g.ShiftTimeAroundTimetable,
					TimetableShiftMaxNumMinutesBeforeClassTime = g.TimetableShiftMaxNumMinutesBeforeClassTime,
					TimetableShiftMaxNumMinutesAfterClassTime = g.TimetableShiftMaxNumMinutesAfterClassTime,
					AllowedMinutesAfter = g.MinutesPost,
					AllowedMinutesBefore = g.MinutesPre,
					EnforceOverlapWithClassTime = g.EnforceOverlapWithClassTime,
					ShiftTimeToMatchEndOfDay = g.ShiftTimeToMatchEndOfDay,
					ShiftTimeToMatchStartOfDay = g.ShiftTimeToMatchStartOfDay,
					IgnoreAssetRules = g.IgnoreAssetRules,
					OnlyOverlapFirstXMinutesOfClassTest = new int?(g.EnforceOverlapWithClassTime_firstXMinutes),
					RoomsToExclude = g.RoomIdsToExclud,
					RoomUsage = ((g.IncludeVirtualRooms && g.IncludeNonVirtualRooms) ? eTryToBookRuleRoomUsage.UseBothVirtualAndNonVirtualRooms : (g.IncludeVirtualRooms ? eTryToBookRuleRoomUsage.UseVirtualRoomsOnly : (g.IncludeNonVirtualRooms ? eTryToBookRuleRoomUsage.UseNonVirtualRoomsOnly : eTryToBookRuleRoomUsage.UseNone))),
					StopLookingIfFoundAtLeastOne = g.StopLookingIfFoundAtLeastOne
				}).ToList<TryToBookRule>();
				tryToBookSearchOptions.RoomAvailabilityScheduleMappings = roomAvailabilityScheduleMappings;
				tryToBookSearchOptions.RestrictRoomByCampusEnabled = findPotentialBookingsReq.RestrictByCampus;
				tryToBookSearchOptions.IgnoreSpecialAccommodations = IgnoreSpecialAccommodations;
				tryToBookSearchOptions.BookingAlreadyExistsAppointmentId = BookingAlreadyExistsAppointmentId;
				tryToBookSearchOptions.MatchUpTimetable = !findPotentialBookingsReq.IgnoreTimetable;
				TryToBookSearchOptions searchOptions = tryToBookSearchOptions;
				TryToBookEnvironment tryToBookEnvironment = new TryToBookEnvironment();
				tryToBookEnvironment.AllAssets = findPotentialBookingsReq.AvailableAssets.Select(delegate(Asset g)
				{
					TryToBookAsset tryToBookAsset = new TryToBookAsset();
					tryToBookAsset.Id = g.AssetId;
					tryToBookAsset.Score = g.Score;
					tryToBookAsset.AssetAccommodations = (from h in g.AccommodationsSupported
					select new TryToBookAssetAccommodation
					{
						ControlId = h.ControlId,
						Level = h.Level,
						SubText = (h.LookupText ?? "") + (h.SubText ?? "")
					}).ToList<TryToBookAssetAccommodation>();
					return tryToBookAsset;
				}).ToList<TryToBookAsset>();
				tryToBookEnvironment.AllRooms = findPotentialBookingsReq.AvailableRooms0.Select(delegate(Room g)
				{
					TryToBookRoom tryToBookRoom = new TryToBookRoom();
					tryToBookRoom.PersonId = g.RoomId;
					tryToBookRoom.RoomType = (eRoomType)(Enum.IsDefined(typeof(eRoomType), (int)g.RoomType) ? g.RoomType : RoomType.unknown);
					tryToBookRoom.Campuses = ((g.Campuses == null) ? new string[0] : g.Campuses.ToArray());
					tryToBookRoom.Title = g.Title;
					tryToBookRoom.AssetsSupported = (from h in g.Assets
					select h.AssetId).ToList<string>();
					return tryToBookRoom;
				}).ToList<TryToBookRoom>();
				tryToBookEnvironment.AllSpecialAccommodations = findPotentialBookingsReq.SpecialAccommodations.Select(delegate(SpecialAccommodation g)
				{
					TryToBookSpecialAccommodation tryToBookSpecialAccommodation = new TryToBookSpecialAccommodation();
					tryToBookSpecialAccommodation.ControlId = g.ControlId;
					tryToBookSpecialAccommodation.Args = g.Args.Keys.Cast<string>().ToDictionary((string key) => key, (string key) => g.Args[key]);
					tryToBookSpecialAccommodation.Type = (eSpecialAccommodationType)(Enum.IsDefined(typeof(eSpecialAccommodationType), (int)g.SpecialAccommodationType) ? g.SpecialAccommodationType : SpecialAccommodationType.Unknown);
					return tryToBookSpecialAccommodation;
				}).ToList<TryToBookSpecialAccommodation>();
				TryToBookEnvironment environment = tryToBookEnvironment;
				AutoBooker2Manager autoBooker2Manager = new AutoBooker2Manager(this.OpContext);
				result = autoBooker2Manager.TryToBookTest(context, searchOptions, environment);
			}
			return result;
		}

		// Token: 0x04000289 RID: 649
		private IAutoTestBookingDAO dao;

		// Token: 0x0400028B RID: 651
		private const string ASSETS_KEY_FINAL = "AutoTestBooking_AvailableAssets_FINAL";

		// Token: 0x0400028C RID: 652
		private const string ASSETS_KEY_MIDTERM = "AutoTestBooking_AvailableAssets_MIDTERM";

		// Token: 0x0400028D RID: 653
		private const string ROOMS_KEY_FINAL = "AutoTestBooking_AvailableRooms_FINAL";

		// Token: 0x0400028E RID: 654
		private const string ROOMS_KEY_MIDTERM = "AutoTestBooking_AvailableRooms_MIDTERM";

		// Token: 0x0400028F RID: 655
		private const string TESTRULES_KEY_FINAL = "AutoTestBooking_TestRules_FINAL";

		// Token: 0x04000290 RID: 656
		private const string TESTRULES_KEY_MIDTERM = "AutoTestBooking_TestRules_MIDTERM";

		// Token: 0x04000291 RID: 657
		private const string SPECIALACCOMMODATIONS_KEY_FINAL = "AutoTestBooking_SpecialAccommodations_FINAL";

		// Token: 0x04000292 RID: 658
		private const string SPECIALACCOMMODATIONS_KEY_MIDTERM = "AutoTestBooking_SpecialAccommodations_MIDTERM";

		// Token: 0x04000293 RID: 659
		private const string REQ_KEY_FINAL = "AutoTestBooking_Request_FINAL";

		// Token: 0x04000294 RID: 660
		private const string REQ_KEY_MIDTERM = "AutoTestBooking_Request_MIDTERM";

		// Token: 0x04000295 RID: 661
		private TimeSpan timeout = TimeSpan.FromMinutes(60.0);

		// Token: 0x02000419 RID: 1049
		internal class AvailabilityScheduleMapping
		{
			// Token: 0x170002AD RID: 685
			// (get) Token: 0x060019B3 RID: 6579 RVA: 0x00090AC1 File Offset: 0x0008ECC1
			// (set) Token: 0x060019B4 RID: 6580 RVA: 0x00090AC9 File Offset: 0x0008ECC9
			public int PrimaryRoomPid { get; set; }

			// Token: 0x170002AE RID: 686
			// (get) Token: 0x060019B5 RID: 6581 RVA: 0x00090AD2 File Offset: 0x0008ECD2
			// (set) Token: 0x060019B6 RID: 6582 RVA: 0x00090ADA File Offset: 0x0008ECDA
			public IList<int> SecondaryRoomPids { get; set; }
		}
	}
}
