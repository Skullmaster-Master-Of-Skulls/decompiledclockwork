using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using ClockWorkLogger;
using TechnoPro.Common.Core.AppointmentLog;
using TechnoPro.Common.Core.Appointments;
using TechnoPro.Common.Core.AppointmentsWorkshops;
using TechnoPro.Common.Core.AvailabilitySchedule;
using TechnoPro.Common.Core.ClockWorkDatabase;
using TechnoPro.Common.Core.LookupCourses;
using TechnoPro.Common.Core.UserSettingsPermissions;
using TechnoPro.Common.DAO.Appointments;
using TechnoPro.Common.DAO.AppointmentsCalendar;
using TechnoPro.Common.DAO.Impl.Appointments;
using TechnoPro.Common.DAO.Impl.AppointmentsCalendar;
using TechnoPro.Common.ICore.AppointmentLog;
using TechnoPro.Common.ICore.Appointments;
using TechnoPro.Common.ICore.AppointmentsCalendar;
using TechnoPro.Common.ICore.AppointmentsWorkshops;
using TechnoPro.Common.ICore.AvailabilitySchedule;
using TechnoPro.Common.ICore.ClockWorkDatabase;
using TechnoPro.Common.ICore.LookupCourses;
using TechnoPro.Common.ICore.UserSettingsPermissions;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Adapters;
using TechnoPro.Common.Public.Entities.AppointmentBookingStudent;
using TechnoPro.Common.Public.Entities.Appointments;
using TechnoPro.Common.Public.Entities.AppointmentsCalendar;
using TechnoPro.Common.Public.Entities.AvailabilitySchedule;
using TechnoPro.Common.Public.Entities.LookupCourses;
using TechnoPro.Common.Public.Entities.People;
using TechnoPro.Common.Public.Entities.UserSettingsPermissions;
using TechnoPro.Common.Public.Entities.UserSettingsPermissions.OldUserSettings;
using TechnoPro.Common.Unity.IoC;

namespace TechnoPro.Common.Core.AppointmentsCalendar
{
	// Token: 0x0200014B RID: 331
	public class AppointmentManager : IAppointmentManager, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x17000200 RID: 512
		// (get) Token: 0x06000EDD RID: 3805 RVA: 0x0007007A File Offset: 0x0006E27A
		// (set) Token: 0x06000EDE RID: 3806 RVA: 0x00070082 File Offset: 0x0006E282
		public IAppointmentDAO dao { get; set; }

		// Token: 0x06000EDF RID: 3807 RVA: 0x0007008B File Offset: 0x0006E28B
		public AppointmentManager(OperationContext opContext)
		{
			this.OpContext = opContext;
			this.dao = new AppointmentDAO(opContext);
		}

		// Token: 0x17000201 RID: 513
		// (get) Token: 0x06000EE0 RID: 3808 RVA: 0x000700AA File Offset: 0x0006E2AA
		// (set) Token: 0x06000EE1 RID: 3809 RVA: 0x000700B2 File Offset: 0x0006E2B2
		public OperationContext OpContext { get; set; }

		// Token: 0x17000202 RID: 514
		// (get) Token: 0x06000EE2 RID: 3810 RVA: 0x000700BC File Offset: 0x0006E2BC
		private IAppointmentLogDAO appLogDao
		{
			get
			{
				IAppointmentLogDAO result;
				if ((result = this._appLogDao) == null)
				{
					result = (this._appLogDao = new AppointmentLogDAO(this.OpContext));
				}
				return result;
			}
		}

		// Token: 0x06000EE3 RID: 3811 RVA: 0x000700E8 File Offset: 0x0006E2E8
		public int CreateAppointmentEnsureUsersNotDoubleBooked(bool RunInTransaction, Appointment Appointment, int[] PidsToEnsureNotDoubleBooked)
		{
			return (PidsToEnsureNotDoubleBooked == null || PidsToEnsureNotDoubleBooked.Length < 1) ? this.CreateAppointment(RunInTransaction, Appointment) : this.dao.CreateAppointmentEnsureUsersNotDoubleBooked(Appointment, PidsToEnsureNotDoubleBooked, null);
		}

		// Token: 0x06000EE4 RID: 3812 RVA: 0x00070120 File Offset: 0x0006E320
		public int GetNumberOfAppointmentsWithAppType(int appTypeId)
		{
			return this.dao.GetNumberOfAppointmentsWithAppType(appTypeId);
		}

		// Token: 0x06000EE5 RID: 3813 RVA: 0x0007013E File Offset: 0x0006E33E
		public void SwapAppointmentTypeForAllAppointments(int appTypeIdToReplace, int appTypeIdToKeep)
		{
			this.dao.SwapAppointmentTypeForAllAppointments(appTypeIdToReplace, appTypeIdToKeep);
		}

		// Token: 0x06000EE6 RID: 3814 RVA: 0x00070150 File Offset: 0x0006E350
		public int CreateAppointment(bool RunInTransaction, Appointment Appointment)
		{
			return this.dao.CreateAppointment(Appointment, null);
		}

		// Token: 0x06000EE7 RID: 3815 RVA: 0x00070174 File Offset: 0x0006E374
		public void UpdateAppointment(bool RunInTransaction, Appointment Appointment)
		{
			bool flag = !RunInTransaction;
			if (flag)
			{
				this.appLogDao.LogAppModificationsPreChangeCommitted(Appointment.AppointmentId);
			}
			this.dao.UpdateAppointment(Appointment, null);
			bool flag2 = !RunInTransaction;
			if (flag2)
			{
				Task.Run(delegate()
				{
					IAppointmentLogManager appointmentLogManager = new AppointmentLogManager(this.OpContext);
					appointmentLogManager.LogAppModifications(Appointment.AppointmentId, eAppointmentModifiedItemType.None);
				});
			}
		}

		// Token: 0x06000EE8 RID: 3816 RVA: 0x000701E4 File Offset: 0x0006E3E4
		public void DeleteAppointment(bool RunInTransaction, int AppointmentId)
		{
			IBaseAppointmentManager baseAppointmentManager = new BaseAppointmentManager(this.OpContext);
			baseAppointmentManager.DeleteAppointment(false, AppointmentId);
		}

		// Token: 0x06000EE9 RID: 3817 RVA: 0x00070208 File Offset: 0x0006E408
		public List<Appointment> LoadAppointments(List<int> PersonIds, List<int> AppTypeIds, bool HideCancelled, bool LoadPerStudentDataIcons, bool LoadPerAnonymousDataIcons, DateTime StartDateTime, DateTime EndDateTime)
		{
			IList<Appointment> source = this.dao.LoadAppointments(PersonIds, AppTypeIds, HideCancelled, LoadPerStudentDataIcons, LoadPerAnonymousDataIcons, StartDateTime, EndDateTime);
			BaseAppointmentManager.HideAppointmentInfoBasedOnPermissions<Appointment>(this.OpContext.WhoAmI, PersonIds, ref source);
			return source.ToList<Appointment>();
		}

		// Token: 0x06000EEA RID: 3818 RVA: 0x0007024C File Offset: 0x0006E44C
		public Appointment LoadAppointmentWithSpecialPermissions(int appointmentId, out IList<eAppointmentPermissionRestriction> permissionRestrictions)
		{
			Appointment appointment = this.LoadAppointment(appointmentId);
			bool flag = appointment == null;
			Appointment result;
			if (flag)
			{
				permissionRestrictions = new List<eAppointmentPermissionRestriction>();
				result = null;
			}
			else
			{
				List<Appointment> apps = new List<Appointment>
				{
					appointment
				};
				IDictionary<int, IList<eAppointmentPermissionRestriction>> dictionary = new Dictionary<int, IList<eAppointmentPermissionRestriction>>();
				IList<Appointment> source = this.LoadAppointmentsSpecialPermissions(apps, new List<int>
				{
					this.OpContext.WhoAmI
				}, out dictionary);
				IList<eAppointmentPermissionRestriction> list2;
				if (!dictionary.ContainsKey(appointmentId))
				{
					IList<eAppointmentPermissionRestriction> list = new List<eAppointmentPermissionRestriction>();
					list2 = list;
				}
				else
				{
					list2 = dictionary[appointmentId];
				}
				permissionRestrictions = list2;
				result = source.FirstOrDefault<Appointment>();
			}
			return result;
		}

		// Token: 0x06000EEB RID: 3819 RVA: 0x000702D8 File Offset: 0x0006E4D8
		public IList<Appointment> LoadAppointmentsWithSpecialPermissions(List<int> PersonIds, List<int> AppTypeIds, bool HideCancelled, DateTime StartDateTime, DateTime EndDateTime, out IDictionary<int, IList<eAppointmentPermissionRestriction>> permissionRestrictions)
		{
			IList<Appointment> apps = this.dao.LoadAppointments(PersonIds, AppTypeIds, HideCancelled, false, false, StartDateTime, EndDateTime);
			return this.LoadAppointmentsSpecialPermissions(apps, PersonIds, out permissionRestrictions);
		}

		// Token: 0x06000EEC RID: 3820 RVA: 0x0007030C File Offset: 0x0006E50C
		private IList<Appointment> LoadAppointmentsSpecialPermissions(IList<Appointment> apps, List<int> PersonIds, out IDictionary<int, IList<eAppointmentPermissionRestriction>> permissionRestrictions)
		{
			IOldUserSettingManager oldUserSettingManager = new OldUserSettingManager(this.OpContext);
			string settingValue_String = oldUserSettingManager.GetSettingValue_String(this.OpContext.WhoAmI, eSettingCode.SETTING_Appointments_DisallowCreatingEditingDeletingCutoff, false);
			CutoffTime cutoffTime = string.IsNullOrEmpty(settingValue_String) ? null : settingValue_String.CutoffTimeFromXml();
			IPermissionManager permissionManager = new PermissionManager(this.OpContext);
			bool flag = permissionManager.IsUserAllowed(this.OpContext.WhoAmI, UserPermissionEnum.DeleteAppointments);
			bool flag2 = permissionManager.IsUserAllowed(this.OpContext.WhoAmI, UserPermissionEnum.DeleteAppointmentsIDidntCreate);
			bool flag3 = permissionManager.IsUserAllowed(this.OpContext.WhoAmI, UserPermissionEnum.CreateModifyAppWithNoAppType);
			bool flag4 = permissionManager.IsUserAllowed(this.OpContext.WhoAmI, UserPermissionEnum.ModifyAppointments);
			bool flag5 = permissionManager.IsUserAllowed(this.OpContext.WhoAmI, UserPermissionEnum.ViewOthersSchedlue);
			permissionRestrictions = new Dictionary<int, IList<eAppointmentPermissionRestriction>>();
			foreach (Appointment appointment in apps)
			{
				int appointmentId = appointment.AppointmentId;
				bool flag6 = (appointment.WhoBooked == null || appointment.WhoBooked.PersonId < 1) && appointment.IsLocked;
				if (flag6)
				{
					AppointmentManager.AddSpecialPermission(ref permissionRestrictions, appointmentId, eAppointmentPermissionRestriction.ExternalAppointmentReadOnly);
				}
				else
				{
					bool flag7 = appointment.IsPrivate || appointment.IsLocked || !flag5;
					if (flag7)
					{
						bool flag8 = !AppointmentManager.IsUserInAttendeesOrBooker(appointment, this.OpContext.WhoAmI);
						if (flag8)
						{
							eAppointmentPermissionRestriction permission = eAppointmentPermissionRestriction.Unknown;
							bool isPrivate = appointment.IsPrivate;
							if (isPrivate)
							{
								permission = eAppointmentPermissionRestriction.PrivateNotInAttendeesOrBooker;
							}
							else
							{
								bool isLocked = appointment.IsLocked;
								if (isLocked)
								{
									permission = eAppointmentPermissionRestriction.LockedNotInAttendeesOrBooker;
								}
								else
								{
									bool flag9 = flag5;
									if (flag9)
									{
										permission = eAppointmentPermissionRestriction.UserNotAllowedToViewOthersSchedules;
									}
								}
							}
							AppointmentManager.AddSpecialPermission(ref permissionRestrictions, appointmentId, permission);
						}
					}
				}
				DateTime? dateTime = (cutoffTime != null) ? cutoffTime.GetMinimumDateForBeforeTypeCutoff() : null;
				bool flag10 = dateTime == null || appointment.StartDateTime <= dateTime.Value;
				bool flag11 = !flag10;
				if (flag11)
				{
					AppointmentManager.AddSpecialPermission(ref permissionRestrictions, appointmentId, eAppointmentPermissionRestriction.UserNotAllowedToModifyAppointmentsCutoffPassed);
				}
				bool flag12 = !flag4;
				if (flag12)
				{
					AppointmentManager.AddSpecialPermission(ref permissionRestrictions, appointmentId, eAppointmentPermissionRestriction.UserNotAllowedToModifyAppointments);
				}
				bool flag13 = !flag;
				if (flag13)
				{
					AppointmentManager.AddSpecialPermission(ref permissionRestrictions, appointmentId, eAppointmentPermissionRestriction.UserNotAllowedToDeleteAppointments);
				}
				bool flag14 = !flag2 && (appointment.WhoBooked == null || appointment.WhoBooked.PersonId != this.OpContext.WhoAmI);
				if (flag14)
				{
					AppointmentManager.AddSpecialPermission(ref permissionRestrictions, appointmentId, eAppointmentPermissionRestriction.UserNotAllowedToDeleteAppointmentsTheyDidntCreate);
				}
				bool flag15;
				if (!flag3)
				{
					AppType appType = appointment.AppType;
					flag15 = (((appType != null) ? appType.AppTypeId : 0) < 1);
				}
				else
				{
					flag15 = false;
				}
				bool flag16 = flag15;
				if (flag16)
				{
					AppointmentManager.AddSpecialPermission(ref permissionRestrictions, appointmentId, eAppointmentPermissionRestriction.UserNotAllowedToModifyAppointmentsWithNoAppType);
				}
			}
			BaseAppointmentManager.HideAppointmentInfoBasedOnPermissions<Appointment>(this.OpContext.WhoAmI, PersonIds, ref apps);
			return apps.ToList<Appointment>();
		}

		// Token: 0x06000EED RID: 3821 RVA: 0x000705DC File Offset: 0x0006E7DC
		private static bool IsUserInAttendeesOrBooker(Appointment app, int whoAmI)
		{
			bool flag = app.WhoBooked != null && app.WhoBooked.PersonId == whoAmI;
			bool result;
			if (flag)
			{
				result = true;
			}
			else
			{
				List<Attendee> attendees = app.Attendees;
				result = (attendees != null && attendees.Any(delegate(Attendee g)
				{
					PersonBase person = g.Person;
					return person != null && person.PersonId == whoAmI;
				}));
			}
			return result;
		}

		// Token: 0x06000EEE RID: 3822 RVA: 0x00070640 File Offset: 0x0006E840
		private static void AddSpecialPermission(ref IDictionary<int, IList<eAppointmentPermissionRestriction>> specialPermissions, int appId, eAppointmentPermissionRestriction permission)
		{
			bool flag = specialPermissions.ContainsKey(appId);
			if (flag)
			{
				specialPermissions[appId].Add(permission);
			}
			else
			{
				specialPermissions.Add(appId, new List<eAppointmentPermissionRestriction>
				{
					permission
				});
			}
		}

		// Token: 0x06000EEF RID: 3823 RVA: 0x00070684 File Offset: 0x0006E884
		public Appointment LoadAppointment(int AppointmentId)
		{
			Appointment item = this.dao.LoadAppointment(AppointmentId);
			IList<Appointment> list = new List<Appointment>
			{
				item
			};
			BaseAppointmentManager.HideAppointmentInfoBasedOnPermissions<Appointment>(this.OpContext.WhoAmI, null, ref list);
			return (list.Count > 0) ? list[0] : null;
		}

		// Token: 0x06000EF0 RID: 3824 RVA: 0x000706D8 File Offset: 0x0006E8D8
		public void CancelAppointment(bool RunInTransaction, int AppointmentId, AppCancelInfo CancelInfo)
		{
			bool flag = !RunInTransaction;
			if (flag)
			{
				this.appLogDao.LogAppModificationsPreChangeCommitted(AppointmentId);
			}
			this.dao.CancelAppointment(AppointmentId, CancelInfo, null);
			if (!RunInTransaction)
			{
				Task.Run(delegate()
				{
					IAppointmentLogManager appointmentLogManager = new AppointmentLogManager(this.OpContext);
					appointmentLogManager.LogAppModifications(AppointmentId, eAppointmentModifiedItemType.Cancelled);
				});
			}
		}

		// Token: 0x06000EF1 RID: 3825 RVA: 0x00070744 File Offset: 0x0006E944
		public void CancelAttendeeAppointment(int appointmentId, int personId, AppCancelInfo CancelInfo)
		{
			IWorkshopAppointmentManager workshopAppointmentManager = new WorkshopAppointmentManager(this.OpContext);
			bool flag = workshopAppointmentManager.IsAppointmentAWorkshop(appointmentId);
			bool flag2 = flag;
			if (flag2)
			{
				IAppointmentAttendeeManager appointmentAttendeeManager = new AppointmentAttendeeManager(this.OpContext);
				appointmentAttendeeManager.DeleteAttendee(false, appointmentId, personId);
			}
			else
			{
				this.CancelAppointment(false, appointmentId, CancelInfo);
			}
		}

		// Token: 0x06000EF2 RID: 3826 RVA: 0x00070790 File Offset: 0x0006E990
		public void UnCancelAppointment(bool RunInTransaction, int AppointmentId)
		{
			bool flag = !RunInTransaction;
			if (flag)
			{
				this.appLogDao.LogAppModificationsPreChangeCommitted(AppointmentId);
			}
			this.dao.UnCancelAppointment(AppointmentId, null);
			bool flag2 = !RunInTransaction;
			if (flag2)
			{
				Task.Run(delegate()
				{
					IAppointmentLogManager appointmentLogManager = new AppointmentLogManager(this.OpContext);
					appointmentLogManager.LogAppModifications(AppointmentId, eAppointmentModifiedItemType.Cancelled);
				});
			}
		}

		// Token: 0x06000EF3 RID: 3827 RVA: 0x000707FC File Offset: 0x0006E9FC
		public void MarkAppointmentTentative(bool RunInTransaction, int AppointmentId)
		{
			bool flag = !RunInTransaction;
			if (flag)
			{
				this.appLogDao.LogAppModificationsPreChangeCommitted(AppointmentId);
			}
			this.dao.MarkAppointmentTentative(AppointmentId, null);
			if (!RunInTransaction)
			{
				Task.Run(delegate()
				{
					IAppointmentLogManager appointmentLogManager = new AppointmentLogManager(this.OpContext);
					appointmentLogManager.LogAppModifications(AppointmentId, eAppointmentModifiedItemType.None);
				});
			}
		}

		// Token: 0x06000EF4 RID: 3828 RVA: 0x00070864 File Offset: 0x0006EA64
		public void UnMarkAppointmentTentative(bool RunInTransaction, int AppointmentId)
		{
			bool flag = !RunInTransaction;
			if (flag)
			{
				this.appLogDao.LogAppModificationsPreChangeCommitted(AppointmentId);
			}
			this.dao.UnMarkAppointmentTentative(AppointmentId, null);
			if (!RunInTransaction)
			{
				Task.Run(delegate()
				{
					IAppointmentLogManager appointmentLogManager = new AppointmentLogManager(this.OpContext);
					appointmentLogManager.LogAppModifications(AppointmentId, eAppointmentModifiedItemType.ShowTimeAs);
				});
			}
		}

		// Token: 0x06000EF5 RID: 3829 RVA: 0x000708CC File Offset: 0x0006EACC
		public Appointment LoadDeletedAppointmentById(int AppointmentId)
		{
			return this.dao.LoadDeletedAppointmentById(AppointmentId);
		}

		// Token: 0x06000EF6 RID: 3830 RVA: 0x000708EC File Offset: 0x0006EAEC
		public int RecoverDeletedAppointment(bool RunInTransaction, int AppointmentId)
		{
			int appId = this.dao.RecoverDeletedAppointment(AppointmentId, null);
			int appId2;
			if (RunInTransaction)
			{
				appId2 = appId;
			}
			else
			{
				Task.Run(delegate()
				{
					IAppointmentLogManager appointmentLogManager = new AppointmentLogManager(this.OpContext);
					appointmentLogManager.LogAppModifications(appId, eAppointmentModifiedItemType.DateTime);
				});
				appId2 = appId;
			}
			return appId2;
		}

		// Token: 0x06000EF7 RID: 3831 RVA: 0x00070944 File Offset: 0x0006EB44
		public void MergeAllAppointments(bool RunInTransaction, int PersonIdNew, int PersonIdOld)
		{
			this.dao.MergeAllAppointments(PersonIdNew, PersonIdOld);
		}

		// Token: 0x06000EF8 RID: 3832 RVA: 0x00070958 File Offset: 0x0006EB58
		public IList<Appointment> LoadAllAppointmentsInADay(DateTime DayToLoadAppointmentsFor, bool ShowCancelled = false, int NumDaysToLoadAppointmentsFor = 1, int[] AppTypeIds = null)
		{
			return this.dao.LoadAllAppointmentsInADay(DayToLoadAppointmentsFor, ShowCancelled, NumDaysToLoadAppointmentsFor, null);
		}

		// Token: 0x06000EF9 RID: 3833 RVA: 0x0007097C File Offset: 0x0006EB7C
		public IDictionary<int, IList<AppointmentBasicSlot>> LoadUncancelledBookedSlots(IList<int> personIds, DateTime startDate, int numDays)
		{
			return this.dao.LoadUncancelledBookedSlots(personIds, startDate, numDays);
		}

		// Token: 0x06000EFA RID: 3834 RVA: 0x0007099C File Offset: 0x0006EB9C
		public AppointmentsWithAvailabilityAndTimetable LoadAppointmentsAndAvailability(AppointmentLoadOptions LoadOptions)
		{
			IList<Appointment> appointments = this.dao.LoadAppointments(LoadOptions.PersonIds.ToList<int>(), LoadOptions.AppointmentTypeIds.ToList<int>(), LoadOptions.HideCancelledAppointments, LoadOptions.LoadPerStudentDataIcons, LoadOptions.LoadPerAnonymousDataIcons, LoadOptions.StartDateTime, LoadOptions.EndDateTime);
			BaseAppointmentManager.HideAppointmentInfoBasedOnPermissions<Appointment>(this.OpContext.WhoAmI, LoadOptions.PersonIds, ref appointments);
			IList<AvailabilityScheduleItemsForContext> list = new List<AvailabilityScheduleItemsForContext>();
			bool loadRecurringSchedule = LoadOptions.LoadRecurringSchedule;
			if (loadRecurringSchedule)
			{
				IAvailabilityScheduleManager availabilityScheduleManager = new AvailabilityScheduleManager(this.OpContext);
				bool flag = LoadOptions.AvailabilityGroupIdsByPersonId == null || LoadOptions.AvailabilityGroupIdsByPersonId.Count < 1;
				if (flag)
				{
					list = availabilityScheduleManager.LoadAvailabilityForMultipleContextsAndDates(LoadOptions.PersonIds, null, LoadOptions.StartDateTime, LoadOptions.EndDateTime);
				}
				else
				{
					int numDays = Convert.ToInt32((LoadOptions.EndDateTime - LoadOptions.StartDateTime).TotalDays) + 1;
					foreach (int num in LoadOptions.PersonIds)
					{
						bool flag2 = LoadOptions.AvailabilityGroupIdsByPersonId.ContainsKey(num);
						IList<AvailabilityScheduleItemsForContext> list2;
						if (flag2)
						{
							list2 = availabilityScheduleManager.LoadAvailabilityForMultipleContextsAndDates(new List<int>
							{
								num
							}, LoadOptions.AvailabilityGroupIdsByPersonId[num], LoadOptions.StartDateTime, numDays);
						}
						else
						{
							AvailabilityScheduleItemsForContext availabilityScheduleItemsForContext = availabilityScheduleManager.LoadAvailabilityItemsByContextAndDateRange(new AvailabilityScheduleContext
							{
								PersonId = num
							}, LoadOptions.StartDateTime, numDays);
							list2 = new List<AvailabilityScheduleItemsForContext>();
							bool flag3 = availabilityScheduleItemsForContext != null;
							if (flag3)
							{
								list2.Add(availabilityScheduleItemsForContext);
							}
						}
						bool flag4 = list2 == null || list2.Count <= 0;
						if (!flag4)
						{
							foreach (AvailabilityScheduleItemsForContext item in list2)
							{
								list.Add(item);
							}
						}
					}
				}
			}
			Dictionary<int, IList<AppointmentTimetableItem>> dictionary = new Dictionary<int, IList<AppointmentTimetableItem>>();
			bool flag5 = LoadOptions.StudentPersonIdsForTimetableLoad != null && LoadOptions.StudentPersonIdsForTimetableLoad.Count > 0;
			if (flag5)
			{
				ILookupTimetableItemManager lookupTimetableItemManager = new LookupTimetableItemManager(this.OpContext);
				foreach (int num2 in LoadOptions.StudentPersonIdsForTimetableLoad)
				{
					IList<LookupCourse> list3 = lookupTimetableItemManager.LoadLookupTimetableItemsByStudent(num2, LoadOptions.StartDateTime, LoadOptions.EndDateTime);
					List<AppointmentTimetableItem> list4 = new List<AppointmentTimetableItem>();
					using (IEnumerator<LookupCourse> enumerator4 = list3.GetEnumerator())
					{
						while (enumerator4.MoveNext())
						{
							AppointmentManager.<>c__DisplayClass35_0 CS$<>8__locals1 = new AppointmentManager.<>c__DisplayClass35_0();
							CS$<>8__locals1.course = enumerator4.Current;
							string courseDescription = CS$<>8__locals1.course.GetCourseDescription();
							list4.AddRange(from timetableItem in CS$<>8__locals1.course.TimetableItems
							select new AppointmentTimetableItem
							{
								LuCourseId = CS$<>8__locals1.course.LuCourseId,
								CourseDescription = courseDescription,
								TimetableItem = timetableItem
							});
						}
					}
					dictionary.Add(num2, list4);
				}
			}
			else
			{
				dictionary = new Dictionary<int, IList<AppointmentTimetableItem>>();
			}
			IList<Holiday> holidays = new List<Holiday>();
			bool flag6 = !LoadOptions.DontLoadHolidays;
			if (flag6)
			{
				ICacheStorageManager cacheStorageManager = ObjectFactory.Resolve<ICacheStorageManager>();
				object obj = cacheStorageManager["disableLoadHolidays"];
				bool flag7 = obj == null;
				if (flag7)
				{
					try
					{
						IAppointmentHolidayManager appointmentHolidayManager = new AppointmentHolidayManager(this.OpContext);
						holidays = appointmentHolidayManager.LoadHolidays(LoadOptions.StartDateTime, LoadOptions.EndDateTime);
					}
					catch (Exception ex)
					{
						CWLogger.Logger.Error("AppointmentManager:LoadAppointmentsAndAvailability:FailedToLoadHolidays:{0}", ex.ToString());
						try
						{
							IClockWorkDatabaseManager clockWorkDatabaseManager = new ClockWorkDatabaseManager(this.OpContext);
							bool flag8 = clockWorkDatabaseManager.DoesTableExist("appointmentsholidays");
							bool flag9 = !flag8;
							if (flag9)
							{
								cacheStorageManager.Insert("disableLoadHolidays", true);
							}
						}
						catch
						{
						}
					}
				}
			}
			return new AppointmentsWithAvailabilityAndTimetable
			{
				Appointments = appointments,
				AvailabilitySchedules = list,
				Holidays = holidays,
				TimetableItems = dictionary
			};
		}

		// Token: 0x06000EFB RID: 3835 RVA: 0x00070E00 File Offset: 0x0006F000
		[DebuggerStepThrough]
		public Task<AppointmentsWithAvailabilityAndTimetable> LoadAppointmentsAndAvailabilityAsync(AppointmentLoadOptions LoadOptions)
		{
			AppointmentManager.<LoadAppointmentsAndAvailabilityAsync>d__36 <LoadAppointmentsAndAvailabilityAsync>d__ = new AppointmentManager.<LoadAppointmentsAndAvailabilityAsync>d__36();
			<LoadAppointmentsAndAvailabilityAsync>d__.<>t__builder = AsyncTaskMethodBuilder<AppointmentsWithAvailabilityAndTimetable>.Create();
			<LoadAppointmentsAndAvailabilityAsync>d__.<>4__this = this;
			<LoadAppointmentsAndAvailabilityAsync>d__.LoadOptions = LoadOptions;
			<LoadAppointmentsAndAvailabilityAsync>d__.<>1__state = -1;
			<LoadAppointmentsAndAvailabilityAsync>d__.<>t__builder.Start<AppointmentManager.<LoadAppointmentsAndAvailabilityAsync>d__36>(ref <LoadAppointmentsAndAvailabilityAsync>d__);
			return <LoadAppointmentsAndAvailabilityAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000EFC RID: 3836 RVA: 0x00070E4C File Offset: 0x0006F04C
		public int GetNumberOfNonCancelledAppointments(int PersonId, DateTime StartDate, DateTime? EndDate, params int[] AppTypeIdsToCheck)
		{
			return this.dao.GetNumberOfNonCancelledAppointments(PersonId, StartDate, EndDate, false, AppTypeIdsToCheck);
		}

		// Token: 0x06000EFD RID: 3837 RVA: 0x00070E70 File Offset: 0x0006F070
		public int GetNumberOfNonCancelledAppointments(int PersonId, DateTime StartDate, DateTime? EndDate, bool excludeTestsExams, params int[] AppTypeIdsToCheck)
		{
			return this.dao.GetNumberOfNonCancelledAppointments(PersonId, StartDate, EndDate, excludeTestsExams, AppTypeIdsToCheck);
		}

		// Token: 0x06000EFE RID: 3838 RVA: 0x00070E94 File Offset: 0x0006F094
		public int GetNumberOfConsecutiveNoshows(int PersonId, DateTime StartDate, int MaxNumberOfNoShowsToCheckFor, params int[] AppTypeIdsToCheck)
		{
			return this.dao.GetNumberOfConsecutiveNoshows(PersonId, StartDate, MaxNumberOfNoShowsToCheckFor, AppTypeIdsToCheck);
		}

		// Token: 0x06000EFF RID: 3839 RVA: 0x00070EB8 File Offset: 0x0006F0B8
		public int LoadAppointmentOrganizerPersonId(int appointmentId)
		{
			IBaseAppointmentDAO baseAppointmentDAO = new BaseAppointmentDAO(this.OpContext);
			return baseAppointmentDAO.LoadAppointmentExternalId(appointmentId);
		}

		// Token: 0x040002B7 RID: 695
		private IAppointmentLogDAO _appLogDao;
	}
}
