using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using TechnoPro.Common.Core.AppointmentLog;
using TechnoPro.Common.Core.AppointmentsRecurring;
using TechnoPro.Common.Core.People;
using TechnoPro.Common.DAO.Appointments;
using TechnoPro.Common.DAO.Impl.Appointments;
using TechnoPro.Common.ICore.AppointmentLog;
using TechnoPro.Common.ICore.Appointments;
using TechnoPro.Common.ICore.AppointmentsRecurring;
using TechnoPro.Common.ICore.People;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Appointments;
using TechnoPro.Common.Public.Entities.AppointmentsRecurring;
using TechnoPro.Common.Public.Entities.People;

namespace TechnoPro.Common.Core.Appointments
{
	// Token: 0x02000131 RID: 305
	public class BaseAppointmentManager : IBaseAppointmentManager, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x06000CF4 RID: 3316 RVA: 0x00059DE5 File Offset: 0x00057FE5
		public BaseAppointmentManager(OperationContext opContext)
		{
			this.OpContext = opContext;
			this._dao = new BaseAppointmentDAO(opContext);
		}

		// Token: 0x170001D0 RID: 464
		// (get) Token: 0x06000CF5 RID: 3317 RVA: 0x00059E03 File Offset: 0x00058003
		// (set) Token: 0x06000CF6 RID: 3318 RVA: 0x00059E0B File Offset: 0x0005800B
		public OperationContext OpContext { get; set; }

		// Token: 0x06000CF7 RID: 3319 RVA: 0x00059E14 File Offset: 0x00058014
		internal static void HideAppointmentInfoBasedOnPermissions<T>(int whoami, IList<int> allowedAttendeePersonIds, ref IList<T> apps) where T : BaseExtendedAppointment
		{
			IAppointmentTypeManager appointmentTypeManager = new AppointmentTypeManager(new OperationContext
			{
				WhoAmI = whoami
			});
			IList<int> allowedAppTypeIds = appointmentTypeManager.GetAllowedAppTypeIds(whoami);
			List<T> list = new List<T>();
			Func<Attendee, bool> <>9__0;
			Func<Attendee, bool> <>9__1;
			foreach (T t in apps)
			{
				bool flag = t == null;
				if (!flag)
				{
					AppType appType = t.AppType;
					int item = (appType != null) ? appType.AppTypeId : 0;
					bool isPrivate = t.IsPrivate;
					if (isPrivate)
					{
						bool flag2 = t.WhoBooked != null && t.WhoBooked.PersonId == whoami;
						if (!flag2)
						{
							List<Attendee> attendees = t.Attendees;
							object obj;
							if (attendees == null)
							{
								obj = null;
							}
							else
							{
								Func<Attendee, bool> predicate;
								if ((predicate = <>9__0) == null)
								{
									predicate = (<>9__0 = ((Attendee g) => g.Person.PersonId == whoami));
								}
								obj = attendees.FirstOrDefault(predicate);
							}
							bool flag3 = obj == null;
							if (flag3)
							{
								list.Add(t);
							}
						}
					}
					else
					{
						bool flag4 = !allowedAppTypeIds.Contains(item);
						if (flag4)
						{
							bool flag5 = t.WhoBooked != null && t.WhoBooked.PersonId == whoami;
							if (!flag5)
							{
								List<Attendee> attendees2 = t.Attendees;
								object obj2;
								if (attendees2 == null)
								{
									obj2 = null;
								}
								else
								{
									Func<Attendee, bool> predicate2;
									if ((predicate2 = <>9__1) == null)
									{
										predicate2 = (<>9__1 = ((Attendee g) => g.Person.PersonId == whoami));
									}
									obj2 = attendees2.FirstOrDefault(predicate2);
								}
								bool flag6 = obj2 != null;
								if (!flag6)
								{
									t.IsPrivate = true;
									list.Add(t);
								}
							}
						}
					}
				}
			}
			Func<Attendee, bool> <>9__2;
			foreach (T t2 in list)
			{
				apps.Remove(t2);
				T t3 = Activator.CreateInstance<T>();
				t3.AppointmentId = t2.AppointmentId;
				t3.StartDateTime = t2.StartDateTime;
				t3.EndDateTime = t2.EndDateTime;
				BaseBasicAppointment baseBasicAppointment = t3;
				IEnumerable<Attendee> attendees3 = t2.Attendees;
				Func<Attendee, bool> predicate3;
				if ((predicate3 = <>9__2) == null)
				{
					predicate3 = (<>9__2 = ((Attendee g) => allowedAttendeePersonIds == null || allowedAttendeePersonIds.Contains(g.Person.PersonId)));
				}
				baseBasicAppointment.Attendees = attendees3.Where(predicate3).ToList<Attendee>();
				t3.IsPrivate = t2.IsPrivate;
				t3.IsCancelled = t2.IsCancelled;
				t3.IsLocked = t2.IsLocked;
				apps.Add(t3);
			}
		}

		// Token: 0x06000CF8 RID: 3320 RVA: 0x0005A168 File Offset: 0x00058368
		[DebuggerStepThrough]
		internal static Task<IList<T>> HideAppointmentInfoBasedOnPermissionsAsync<T>(int whoami, IList<int> allowedAttendeePersonIds, IList<T> apps) where T : BaseExtendedAppointment
		{
			BaseAppointmentManager.<HideAppointmentInfoBasedOnPermissionsAsync>d__7<T> <HideAppointmentInfoBasedOnPermissionsAsync>d__ = new BaseAppointmentManager.<HideAppointmentInfoBasedOnPermissionsAsync>d__7<T>();
			<HideAppointmentInfoBasedOnPermissionsAsync>d__.<>t__builder = AsyncTaskMethodBuilder<IList<T>>.Create();
			<HideAppointmentInfoBasedOnPermissionsAsync>d__.whoami = whoami;
			<HideAppointmentInfoBasedOnPermissionsAsync>d__.allowedAttendeePersonIds = allowedAttendeePersonIds;
			<HideAppointmentInfoBasedOnPermissionsAsync>d__.apps = apps;
			<HideAppointmentInfoBasedOnPermissionsAsync>d__.<>1__state = -1;
			<HideAppointmentInfoBasedOnPermissionsAsync>d__.<>t__builder.Start<BaseAppointmentManager.<HideAppointmentInfoBasedOnPermissionsAsync>d__7<T>>(ref <HideAppointmentInfoBasedOnPermissionsAsync>d__);
			return <HideAppointmentInfoBasedOnPermissionsAsync>d__.<>t__builder.Task;
		}

		// Token: 0x170001D1 RID: 465
		// (get) Token: 0x06000CF9 RID: 3321 RVA: 0x0005A1BC File Offset: 0x000583BC
		private IAppointmentLogDAO appLogDao
		{
			get
			{
				bool flag = this._appLogDao == null;
				if (flag)
				{
					this._appLogDao = new AppointmentLogDAO(this.OpContext);
				}
				return this._appLogDao;
			}
		}

		// Token: 0x06000CFA RID: 3322 RVA: 0x0005A1F4 File Offset: 0x000583F4
		public int CreateBaseExtendedAppointment(bool runInTransaction, BaseExtendedAppointment basicAppointment)
		{
			return this._dao.CreateBaseExtendedAppointment(basicAppointment, null);
		}

		// Token: 0x06000CFB RID: 3323 RVA: 0x0005A218 File Offset: 0x00058418
		public int CreateBaseBasicAppointment(bool runInTransaction, BaseBasicAppointment basicAppointment)
		{
			return this._dao.CreateBaseBasicAppointment(basicAppointment, null);
		}

		// Token: 0x06000CFC RID: 3324 RVA: 0x0005A23C File Offset: 0x0005843C
		public void UpdateBaseBasicAppointment(bool runInTransaction, BaseBasicAppointment basicAppointment)
		{
			bool flag = !runInTransaction;
			if (flag)
			{
				this.appLogDao.LogAppModificationsPreChangeCommitted(basicAppointment.AppointmentId);
			}
			this._dao.UpdateBaseBasicAppointment(basicAppointment, null);
			if (!runInTransaction)
			{
				Task.Run(delegate()
				{
					IAppointmentLogManager appointmentLogManager = new AppointmentLogManager(this.OpContext);
					appointmentLogManager.LogAppModifications(basicAppointment.AppointmentId, eAppointmentModifiedItemType.None);
				});
			}
		}

		// Token: 0x06000CFD RID: 3325 RVA: 0x0005A2AC File Offset: 0x000584AC
		public void UpdateBaseExtendedAppointment(bool runInTransaction, BaseExtendedAppointment basicAppointment)
		{
			bool flag = !runInTransaction;
			if (flag)
			{
				this.appLogDao.LogAppModificationsPreChangeCommitted(basicAppointment.AppointmentId);
			}
			this._dao.UpdateBaseExtendedAppointment(basicAppointment, null);
			if (!runInTransaction)
			{
				Task.Run(delegate()
				{
					IAppointmentLogManager appointmentLogManager = new AppointmentLogManager(this.OpContext);
					appointmentLogManager.LogAppModifications(basicAppointment.AppointmentId, eAppointmentModifiedItemType.None);
				});
			}
		}

		// Token: 0x06000CFE RID: 3326 RVA: 0x0005A31C File Offset: 0x0005851C
		public void UpdateBaseExtendedAppointment(bool runInTransaction, BaseExtendedAppointment basicAppointment, RecurringInstanceSetModifyBehaviour ModifyBehaivour)
		{
			bool flag = !runInTransaction;
			if (flag)
			{
				this.appLogDao.LogAppModificationsPreChangeCommitted(basicAppointment.AppointmentId);
			}
			this._dao.UpdateBaseExtendedAppointment(basicAppointment, ModifyBehaivour, null);
			if (!runInTransaction)
			{
				Task.Run(delegate()
				{
					IAppointmentLogManager appointmentLogManager = new AppointmentLogManager(this.OpContext);
					appointmentLogManager.LogAppModifications(basicAppointment.AppointmentId, eAppointmentModifiedItemType.None);
				});
			}
		}

		// Token: 0x06000CFF RID: 3327 RVA: 0x0005A38C File Offset: 0x0005858C
		public void DeleteAppointment(bool runInTransaction, int AppointmentId)
		{
			BaseBasicAppointment baseBasicAppointment = this.LoadBaseBasicAppointmentById(AppointmentId);
			int groupCode = baseBasicAppointment.GroupCode;
			this._dao.DeleteMemo(AppointmentId, null);
			this._dao.DeleteCancelledReason(AppointmentId, null);
			this._dao.DeleteIcons(AppointmentId, null);
			this._dao.DeleteAttendees(AppointmentId, null);
			this._dao.DeleteAppointmentWorkshopInfo(AppointmentId, null);
			this._dao.DeleteTestExamInfo(AppointmentId, null);
			this._dao.DeleteAppData(AppointmentId, null);
			this._dao.DeleteMainAppointment(AppointmentId, null);
			bool flag = groupCode > 0;
			if (flag)
			{
				IRecurringAppointmentManager recurringAppointmentManager = new RecurringAppointmentManager(this.OpContext);
				AppointmentRecurringInfo appointmentRecurringInfo = recurringAppointmentManager.LoadCurrentRecurringAppointmentsSet(groupCode);
				bool flag2 = appointmentRecurringInfo.Appointments.Count == 1;
				if (flag2)
				{
					appointmentRecurringInfo.Appointments[0].GroupCode = -1;
					this.UpdateAppointmentParts(true, appointmentRecurringInfo.Appointments[0], eAppointmentPart.RecurringGroupCode);
				}
			}
			if (!runInTransaction)
			{
				Task.Run(delegate()
				{
					IAppointmentLogManager appointmentLogManager = new AppointmentLogManager(this.OpContext);
					appointmentLogManager.LogAppModifications(AppointmentId, eAppointmentModifiedItemType.None);
				});
			}
		}

		// Token: 0x06000D00 RID: 3328 RVA: 0x0005A4D8 File Offset: 0x000586D8
		public void UpdateDateAndTime(bool runInTransaction, int appId, DateTime startDateTime, DateTime endDateTime)
		{
			bool flag = !runInTransaction;
			if (flag)
			{
				this.appLogDao.LogAppModificationsPreChangeCommitted(appId);
			}
			this._dao.UpdateDateAndTime(appId, startDateTime, endDateTime, null);
			if (!runInTransaction)
			{
				Task.Run(delegate()
				{
					IAppointmentLogManager appointmentLogManager = new AppointmentLogManager(this.OpContext);
					appointmentLogManager.LogAppModifications(appId, eAppointmentModifiedItemType.DateTime);
				});
			}
		}

		// Token: 0x06000D01 RID: 3329 RVA: 0x0005A544 File Offset: 0x00058744
		public void UpdateAppointmentCancelledValue(bool runInTransaction, int appId, bool cancelledValue, AppCancelInfo cancelInfo)
		{
			bool flag = !runInTransaction;
			if (flag)
			{
				this.appLogDao.LogAppModificationsPreChangeCommitted(appId);
			}
			this._dao.UpdateAppointmentCancelledValue(appId, cancelledValue, cancelInfo, null);
			if (!runInTransaction)
			{
				Task.Run(delegate()
				{
					IAppointmentLogManager appointmentLogManager = new AppointmentLogManager(this.OpContext);
					appointmentLogManager.LogAppModifications(appId, eAppointmentModifiedItemType.Cancelled);
				});
			}
		}

		// Token: 0x06000D02 RID: 3330 RVA: 0x0005A5B0 File Offset: 0x000587B0
		public void UpdateAppointmentAppCodeValue(bool runInTransaction, int appId, int appCodeValue)
		{
			bool flag = !runInTransaction;
			if (flag)
			{
				this.appLogDao.LogAppModificationsPreChangeCommitted(appId);
			}
			this._dao.UpdateAppointmentAppCodeValue(appId, appCodeValue, null);
			if (!runInTransaction)
			{
				Task.Run(delegate()
				{
					IAppointmentLogManager appointmentLogManager = new AppointmentLogManager(this.OpContext);
					appointmentLogManager.LogAppModifications(appId, eAppointmentModifiedItemType.ShowTimeAs);
				});
			}
		}

		// Token: 0x06000D03 RID: 3331 RVA: 0x0005A61C File Offset: 0x0005881C
		public BaseBasicAppointment LoadBaseBasicAppointmentById(int appointmentId)
		{
			return this._dao.LoadBaseBasicAppointmentById(appointmentId);
		}

		// Token: 0x06000D04 RID: 3332 RVA: 0x0005A63C File Offset: 0x0005883C
		public T LoadBaseExtendedAppointmentById<T>(int appointmentId) where T : BaseExtendedAppointment
		{
			return this._dao.LoadBaseExtendedAppointmentById<T>(appointmentId);
		}

		// Token: 0x06000D05 RID: 3333 RVA: 0x0005A65C File Offset: 0x0005885C
		public IList<T> LoadBaseExtendedAppointmentsByDateRangeAndAppType<T>(DateTime StartDateTime, DateTime EndDateTime, IList<int> AppTypeIds) where T : BaseExtendedAppointment
		{
			IList<T> result = this._dao.LoadBaseExtendedAppointmentsByDateRangeAndAppType<T>(StartDateTime, EndDateTime, AppTypeIds);
			BaseAppointmentManager.HideAppointmentInfoBasedOnPermissions<T>(this.OpContext.WhoAmI, null, ref result);
			return result;
		}

		// Token: 0x06000D06 RID: 3334 RVA: 0x0005A694 File Offset: 0x00058894
		public IList<T> LoadBaseExtendedAppointmentsByDateRangeAndPersonIds<T>(DateTime StartDate, DateTime EndDate, IList<int> PersonIds) where T : BaseExtendedAppointment
		{
			IList<T> result = this._dao.LoadBaseExtendedAppointmentsByDateRangeAndPersonIds<T>(StartDate, EndDate, PersonIds);
			BaseAppointmentManager.HideAppointmentInfoBasedOnPermissions<T>(this.OpContext.WhoAmI, PersonIds, ref result);
			return result;
		}

		// Token: 0x06000D07 RID: 3335 RVA: 0x0005A6CC File Offset: 0x000588CC
		public int InsertOrUpdateAppointmentRoom(bool runInTransaction, int appId, int roomId)
		{
			bool flag = !runInTransaction;
			if (flag)
			{
				this.appLogDao.LogAppModificationsPreChangeCommitted(appId);
			}
			int num = this._dao.InsertOrUpdateAppointmentRoom(appId, roomId, null);
			int result;
			if (runInTransaction)
			{
				result = num;
			}
			else
			{
				Task.Run(delegate()
				{
					IAppointmentLogManager appointmentLogManager = new AppointmentLogManager(this.OpContext);
					appointmentLogManager.LogAppModifications(appId, eAppointmentModifiedItemType.Room);
				});
				result = num;
			}
			return result;
		}

		// Token: 0x06000D08 RID: 3336 RVA: 0x0005A740 File Offset: 0x00058940
		public void DeleteAppointmentRoom(bool runInTransaction, int appId)
		{
			this._dao.DeleteAppointmentRoom(appId, null);
			if (!runInTransaction)
			{
				Task.Run(delegate()
				{
					IAppointmentLogManager appointmentLogManager = new AppointmentLogManager(this.OpContext);
					appointmentLogManager.LogAppModifications(appId, eAppointmentModifiedItemType.Room);
				});
			}
		}

		// Token: 0x06000D09 RID: 3337 RVA: 0x0005A790 File Offset: 0x00058990
		public int FindMatchingExistingAppointment(BaseExtendedAppointment Appointment)
		{
			return this._dao.FindMatchingExistingAppointment(Appointment);
		}

		// Token: 0x06000D0A RID: 3338 RVA: 0x0005A7B0 File Offset: 0x000589B0
		public void UpdateAppointmentParts(bool runInTransaction, BaseBasicAppointment Appointment, eAppointmentPart PartsToUpdate)
		{
			bool flag = (PartsToUpdate & eAppointmentPart.DateTimeAndDuration) > eAppointmentPart.None;
			bool flag2 = (PartsToUpdate & eAppointmentPart.RecurringGroupCode) > eAppointmentPart.None;
			bool flag3 = !runInTransaction;
			if (flag3)
			{
				this.appLogDao.LogAppModificationsPreChangeCommitted(Appointment.AppointmentId);
			}
			bool flag4 = flag;
			if (flag4)
			{
				this._dao.UpdateDateAndTime(Appointment.AppointmentId, Appointment.StartDateTime, Appointment.EndDateTime, null);
			}
			bool flag5 = flag2;
			if (flag5)
			{
				IRecurringAppointmentManager recurringAppointmentManager = new RecurringAppointmentManager(this.OpContext);
				recurringAppointmentManager.UpdateRecurringGroupCode(true, Appointment.AppointmentId, Appointment.GroupCode);
			}
			if (!runInTransaction)
			{
				eAppointmentModifiedItemType m = eAppointmentModifiedItemType.None;
				bool flag6 = flag;
				if (flag6)
				{
					m |= eAppointmentModifiedItemType.DateTime;
				}
				bool flag7 = flag2;
				if (flag7)
				{
					m |= eAppointmentModifiedItemType.RecurringInfo;
				}
				Task.Run(delegate()
				{
					IAppointmentLogManager appointmentLogManager = new AppointmentLogManager(this.OpContext);
					appointmentLogManager.LogAppModifications(Appointment.AppointmentId, m);
				});
			}
		}

		// Token: 0x06000D0B RID: 3339 RVA: 0x0005A8BC File Offset: 0x00058ABC
		public IList<PersonBase> LoadDistinctUsersAStudentHasHadAtLeastOneAppointmentWith(int StudentPersonId, IList<int> StaffGroupIds)
		{
			return this._dao.LoadDistinctUsersAStudentHasHadAtLeastOneAppointmentWith(StudentPersonId, StaffGroupIds);
		}

		// Token: 0x06000D0C RID: 3340 RVA: 0x0005A8DC File Offset: 0x00058ADC
		public void InsertOrUpdateAppointmentMemo(bool runInTransaction, int AppointmentId, string MemoText)
		{
			bool flag = !runInTransaction;
			if (flag)
			{
				this.appLogDao.LogAppModificationsPreChangeCommitted(AppointmentId);
			}
			this._dao.InsertOrUpdateAppointmentMemo(AppointmentId, MemoText, null);
			if (!runInTransaction)
			{
				Task.Run(delegate()
				{
					IAppointmentLogManager appointmentLogManager = new AppointmentLogManager(this.OpContext);
					appointmentLogManager.LogAppModifications(AppointmentId, eAppointmentModifiedItemType.Memo);
				});
			}
		}

		// Token: 0x06000D0D RID: 3341 RVA: 0x0005A948 File Offset: 0x00058B48
		public IList<T> LoadBaseExtendedAppointmentsByGroupCode<T>(int GroupCode) where T : BaseExtendedAppointment
		{
			IList<T> result = this._dao.LoadBaseExtendedAppointmentsByGroupCode<T>(GroupCode);
			BaseAppointmentManager.HideAppointmentInfoBasedOnPermissions<T>(this.OpContext.WhoAmI, null, ref result);
			return result;
		}

		// Token: 0x06000D0E RID: 3342 RVA: 0x0005A97C File Offset: 0x00058B7C
		public IList<T> LoadBaseExtendedAppointmentsByAppointmentIds<T>(IList<int> AppointmentIds) where T : BaseExtendedAppointment
		{
			IAppointmentTypeManager appointmentTypeManager = new AppointmentTypeManager(this.OpContext);
			IList<int> allowedAppTypeIds = appointmentTypeManager.GetAllowedAppTypeIds(this.OpContext.WhoAmI);
			IList<T> result = this._dao.LoadBaseExtendedAppointmentsByAppointmentIds<T>(AppointmentIds, allowedAppTypeIds);
			BaseAppointmentManager.HideAppointmentInfoBasedOnPermissions<T>(this.OpContext.WhoAmI, null, ref result);
			return result;
		}

		// Token: 0x06000D0F RID: 3343 RVA: 0x0005A9D0 File Offset: 0x00058BD0
		public IList<T> LoadBaseExtendedAppointmentsByPersonId<T>(int PersonId) where T : BaseExtendedAppointment
		{
			IAppointmentTypeManager appointmentTypeManager = new AppointmentTypeManager(this.OpContext);
			IList<int> allowedAppTypeIds = appointmentTypeManager.GetAllowedAppTypeIds(this.OpContext.WhoAmI);
			IList<T> result = this._dao.LoadBaseExtendedAppointmentsByPersonId<T>(PersonId);
			BaseAppointmentManager.HideAppointmentInfoBasedOnPermissions<T>(this.OpContext.WhoAmI, null, ref result);
			return result;
		}

		// Token: 0x06000D10 RID: 3344 RVA: 0x0005AA24 File Offset: 0x00058C24
		public IList<BaseBasicAppointment> FreeTimeSearch(FreeTimeSearchContext Context)
		{
			DateTime searchStartDateTime = Context.SearchStartDateTime;
			DateTime t = searchStartDateTime.Add(Context.SearchEnd);
			List<BaseExtendedAppointment> existingAppointments = (from g in this.LoadBaseExtendedAppointmentsByDateRangeAndPersonIds<BaseExtendedAppointment>(searchStartDateTime.Date, t.Date.AddDays(1.0).AddMinutes(-1.0), Context.PersonIds)
			where !g.IsCancelled
			select g).ToList<BaseExtendedAppointment>();
			bool flag = Convert.ToInt32(Context.SearchAppointmentDuration.TotalMinutes) == 0;
			if (flag)
			{
				Context.SearchAppointmentDuration = TimeSpan.FromMinutes(60.0);
			}
			IPeopleManager peopleManager = new PeopleManager(this.OpContext);
			IList<PersonBase> source = peopleManager.LoadPersonsByIds(Context.PersonIds);
			DateTime currStartDateTime = searchStartDateTime;
			DateTime currEndDateTime = searchStartDateTime.Add(Context.SearchAppointmentDuration);
			List<BaseBasicAppointment> list = new List<BaseBasicAppointment>();
			int num = 0;
			Func<FreeTimeSearchRecurringRule, bool> <>9__1;
			Func<int, BaseExtendedAppointment> <>9__5;
			while (currEndDateTime <= t && num < 100000)
			{
				num++;
				bool flag2 = Context.RecurringRules != null && currStartDateTime.Date == currEndDateTime.Date;
				bool flag3;
				if (flag2)
				{
					IEnumerable<FreeTimeSearchRecurringRule> recurringRules = Context.RecurringRules;
					Func<FreeTimeSearchRecurringRule, bool> predicate;
					if ((predicate = <>9__1) == null)
					{
						predicate = (<>9__1 = ((FreeTimeSearchRecurringRule rr) => rr.DayOfWeek == currStartDateTime.DayOfWeek && !(rr.EndTime < currEndDateTime.TimeOfDay) && !(rr.StartTime > currStartDateTime.TimeOfDay)));
					}
					FreeTimeSearchRecurringRule freeTimeSearchRecurringRule = recurringRules.FirstOrDefault(predicate);
					flag3 = (freeTimeSearchRecurringRule != null);
				}
				else
				{
					flag3 = false;
				}
				bool flag4 = flag3;
				if (flag4)
				{
					bool flag5 = Context.SearchMethod == eFreeTimeSearchMethod.FindFirstAvailablePerson;
					if (flag5)
					{
						using (IEnumerator<int> enumerator = Context.PersonIds.GetEnumerator())
						{
							while (enumerator.MoveNext())
							{
								int pid = enumerator.Current;
								Func<Attendee, bool> <>9__3;
								BaseExtendedAppointment baseExtendedAppointment = existingAppointments.FirstOrDefault(delegate(BaseExtendedAppointment g)
								{
									if (g.Attendees != null)
									{
										IEnumerable<Attendee> attendees3 = g.Attendees;
										Func<Attendee, bool> predicate2;
										if ((predicate2 = <>9__3) == null)
										{
											predicate2 = (<>9__3 = ((Attendee att) => att.Person.PersonId == pid));
										}
										if (attendees3.FirstOrDefault(predicate2) != null)
										{
											return !(currEndDateTime < g.StartDateTime) && !(currStartDateTime > g.EndDateTime);
										}
									}
									return false;
								});
								bool flag6 = baseExtendedAppointment != null;
								if (!flag6)
								{
									BaseBasicAppointment baseBasicAppointment = new BaseBasicAppointment
									{
										StartDateTime = currStartDateTime,
										EndDateTime = currEndDateTime,
										Attendees = new List<Attendee>()
									};
									PersonBase personBase = source.FirstOrDefault((PersonBase g) => g.PersonId == pid);
									List<Attendee> attendees = baseBasicAppointment.Attendees;
									Attendee item;
									if (personBase == null)
									{
										(item = new Attendee()).Person = new PersonBase
										{
											PersonId = pid
										};
									}
									else
									{
										(item = new Attendee()).Person = personBase;
									}
									attendees.Add(item);
									list.Add(baseBasicAppointment);
								}
							}
						}
					}
					else
					{
						IEnumerable<int> personIds = Context.PersonIds;
						Func<int, BaseExtendedAppointment> selector;
						if ((selector = <>9__5) == null)
						{
							selector = (<>9__5 = delegate(int pid)
							{
								Func<Attendee, bool> <>9__8;
								return existingAppointments.FirstOrDefault(delegate(BaseExtendedAppointment g)
								{
									List<Attendee> attendees3 = g.Attendees;
									bool flag9;
									if (attendees3 == null)
									{
										flag9 = (null != null);
									}
									else
									{
										Func<Attendee, bool> predicate2;
										if ((predicate2 = <>9__8) == null)
										{
											predicate2 = (<>9__8 = ((Attendee att) => att.Person.PersonId == pid));
										}
										flag9 = (attendees3.FirstOrDefault(predicate2) != null);
									}
									return flag9 && !(currEndDateTime < g.StartDateTime) && !(currStartDateTime > g.EndDateTime);
								});
							});
						}
						bool flag7 = personIds.Select(selector).All((BaseExtendedAppointment foundOverlappingAppointment) => foundOverlappingAppointment == null);
						bool flag8 = flag7;
						if (flag8)
						{
							BaseBasicAppointment baseBasicAppointment2 = new BaseBasicAppointment
							{
								StartDateTime = currStartDateTime,
								EndDateTime = currEndDateTime,
								Attendees = new List<Attendee>()
							};
							using (IEnumerator<int> enumerator2 = Context.PersonIds.GetEnumerator())
							{
								while (enumerator2.MoveNext())
								{
									int pid = enumerator2.Current;
									PersonBase personBase2 = source.FirstOrDefault((PersonBase g) => g.PersonId == pid);
									List<Attendee> attendees2 = baseBasicAppointment2.Attendees;
									Attendee item2;
									if (personBase2 == null)
									{
										(item2 = new Attendee()).Person = new PersonBase
										{
											PersonId = pid
										};
									}
									else
									{
										(item2 = new Attendee()).Person = personBase2;
									}
									attendees2.Add(item2);
								}
							}
							list.Add(baseBasicAppointment2);
						}
					}
				}
				currStartDateTime = currEndDateTime;
				currEndDateTime = currEndDateTime.Add(Context.SearchAppointmentDuration);
			}
			return list;
		}

		// Token: 0x06000D11 RID: 3345 RVA: 0x0005AE74 File Offset: 0x00059074
		public IList<T> LoadBaseExtendedAppointmentsByDateRange<T>(DateTime StartDate, int NumDays, bool ShowCancelled = false) where T : BaseExtendedAppointment
		{
			return this._dao.LoadBaseExtendedAppointmentsByDateRange<T>(StartDate, StartDate.Date.AddDays((double)NumDays).AddMinutes(-1.0), ShowCancelled);
		}

		// Token: 0x06000D12 RID: 3346 RVA: 0x0005AEB8 File Offset: 0x000590B8
		public IList<BaseBasicAppointment> LoadBaseBasicAppointmentsByPersonAndDateRange(int PersonId, bool hideCancelled, DateTime StartDate, DateTime EndDate)
		{
			return this._dao.LoadBaseBasicAppointmentsByPersonAndDateRange(PersonId, hideCancelled, StartDate, EndDate);
		}

		// Token: 0x06000D13 RID: 3347 RVA: 0x0005AEDA File Offset: 0x000590DA
		public void UpdateAppointmentExternalId(int appId, int externalId)
		{
			this._dao.UpdateAppointmentExternalId(appId, externalId);
		}

		// Token: 0x0400026F RID: 623
		private readonly IBaseAppointmentDAO _dao;

		// Token: 0x04000271 RID: 625
		private IAppointmentLogDAO _appLogDao;
	}
}
