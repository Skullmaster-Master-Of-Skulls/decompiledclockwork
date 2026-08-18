using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClockWorkLogger;
using TechnoPro.Common.Core.AppointmentLog;
using TechnoPro.Common.Core.Appointments;
using TechnoPro.Common.Core.AppointmentsWorkshops;
using TechnoPro.Common.DAO.Appointments;
using TechnoPro.Common.DAO.AppointmentsRecurring;
using TechnoPro.Common.DAO.Impl.Appointments;
using TechnoPro.Common.DAO.Impl.AppointmentsRecurring;
using TechnoPro.Common.ICore.AppointmentLog;
using TechnoPro.Common.ICore.Appointments;
using TechnoPro.Common.ICore.AppointmentsRecurring;
using TechnoPro.Common.ICore.AppointmentsWorkshops;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Appointments;
using TechnoPro.Common.Public.Entities.AppointmentsRecurring;
using TechnoPro.Common.Public.Entities.AppointmentsWorkshops;

namespace TechnoPro.Common.Core.AppointmentsRecurring
{
	// Token: 0x02000127 RID: 295
	public class RecurringAppointmentManager : IRecurringAppointmentManager, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x06000C65 RID: 3173 RVA: 0x00056B11 File Offset: 0x00054D11
		public RecurringAppointmentManager(OperationContext opContext)
		{
			this.OpContext = opContext;
			this.dao = new RecurringAppointmentDAO(this.OpContext);
		}

		// Token: 0x170001BE RID: 446
		// (get) Token: 0x06000C66 RID: 3174 RVA: 0x00056B34 File Offset: 0x00054D34
		// (set) Token: 0x06000C67 RID: 3175 RVA: 0x00056B3C File Offset: 0x00054D3C
		public OperationContext OpContext { get; set; }

		// Token: 0x170001BF RID: 447
		// (get) Token: 0x06000C68 RID: 3176 RVA: 0x00056B48 File Offset: 0x00054D48
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

		// Token: 0x06000C69 RID: 3177 RVA: 0x00056B80 File Offset: 0x00054D80
		public IList<AppointmentForNotification> UpdateRecurringAppointmentAttendees(int groupCode, int appIdAlreadyUpdated, IList<Attendee> attendeesAdded, IList<Attendee> attendeesModified, IList<int> attendeePersonIdsRemoved)
		{
			List<AppointmentForNotification> list = new List<AppointmentForNotification>();
			bool flag = attendeesAdded.Count < 1 && attendeesModified.Count < 1 && attendeePersonIdsRemoved.Count < 1;
			IList<AppointmentForNotification> result;
			if (flag)
			{
				result = list;
			}
			else
			{
				IAppointmentAttendeeManager appointmentAttendeeManager = new AppointmentAttendeeManager(this.OpContext);
				AppointmentRecurringInfo appointmentRecurringInfo = this.LoadCurrentRecurringAppointmentsSet(groupCode);
				foreach (RecurringAppointment recurringAppointment in appointmentRecurringInfo.Appointments)
				{
					bool flag2 = recurringAppointment.AppointmentId == appIdAlreadyUpdated;
					if (!flag2)
					{
						List<Attendee> attendees = recurringAppointment.Attendees;
						List<int> list2 = (from g in attendeePersonIdsRemoved
						where attendees.Any((Attendee h) => h.Person.PersonId == g)
						select g).ToList<int>();
						foreach (int personId in list2)
						{
							appointmentAttendeeManager.DeleteAttendee(false, recurringAppointment.AppointmentId, personId);
						}
						List<Attendee> list3 = (from g in attendeesAdded
						where !attendees.Any((Attendee h) => h.Person.PersonId == g.Person.PersonId)
						select g).ToList<Attendee>();
						foreach (Attendee attendee in list3)
						{
							appointmentAttendeeManager.InsertOrUpdateAppointmentAttendee(false, recurringAppointment.AppointmentId, attendee);
						}
						List<Attendee> list4 = (from g in attendeesModified
						where attendees.Any((Attendee h) => h.Person.PersonId == g.Person.PersonId)
						select g).ToList<Attendee>();
						foreach (Attendee attendee2 in list4)
						{
							appointmentAttendeeManager.InsertOrUpdateAppointmentAttendee(false, recurringAppointment.AppointmentId, attendee2);
						}
						bool flag3 = list2.Count > 0 || list3.Count > 0 || list4.Count > 0;
						if (flag3)
						{
							List<AppointmentForNotification> list5 = list;
							AppointmentForNotification appointmentForNotification = new AppointmentForNotification();
							appointmentForNotification.AppointmentId = recurringAppointment.AppointmentId;
							appointmentForNotification.StartDateTime = recurringAppointment.StartDateTime;
							appointmentForNotification.EndDateTime = recurringAppointment.EndDateTime;
							AppointmentForNotification appointmentForNotification2 = appointmentForNotification;
							IList<int>[] array = new IList<int>[4];
							array[0] = (from g in recurringAppointment.Attendees
							select g.Person.PersonId).ToList<int>();
							array[1] = list2;
							array[2] = (from g in list3
							select g.Person.PersonId).ToList<int>();
							array[3] = (from g in list4
							select g.Person.PersonId).ToList<int>();
							appointmentForNotification2.AttendeePersonIds = RecurringAppointmentManager.GetMergedAttendeePersonIds(array);
							list5.Add(appointmentForNotification);
						}
					}
				}
				result = list;
			}
			return result;
		}

		// Token: 0x06000C6A RID: 3178 RVA: 0x00056EC4 File Offset: 0x000550C4
		private static int[] GetMergedAttendeePersonIds(params IList<int>[] attendeePersonIds)
		{
			return attendeePersonIds.SelectMany((IList<int> g) => g).Distinct<int>().ToArray<int>();
		}

		// Token: 0x06000C6B RID: 3179 RVA: 0x00056F08 File Offset: 0x00055108
		public void UpdateRecurringAppointmentGroupInformationAndDates(AppointmentRecurringInfo RecurringItems)
		{
			bool flag = RecurringItems.Appointments.Count < 1;
			if (!flag)
			{
				bool flag2 = RecurringItems.Appointments.Count == 1;
				if (flag2)
				{
					bool flag3 = RecurringItems.MasterGroupCode < 1;
					if (flag3)
					{
						RecurringItems.MasterGroupCode = RecurringItems.Appointments[0].GroupCode;
					}
					bool flag4 = RecurringItems.MasterGroupCode > 0;
					if (flag4)
					{
						this.RemoveAllRecurringAppointmentsExceptionMaster(RecurringItems.MasterGroupCode, RecurringItems.Appointments[0].AppointmentId);
					}
				}
				else
				{
					IBaseAppointmentManager baseAppointmentManager = new BaseAppointmentManager(this.OpContext);
					bool flag5 = RecurringItems.MasterGroupCode > 0;
					AppointmentRecurringInfo appointmentRecurringInfo;
					if (flag5)
					{
						appointmentRecurringInfo = this.LoadCurrentRecurringAppointmentsSet(RecurringItems.MasterGroupCode);
					}
					else
					{
						appointmentRecurringInfo = new AppointmentRecurringInfo
						{
							Appointments = new List<RecurringAppointment>()
						};
					}
					int num = (RecurringItems.MasterGroupCode > 0 && RecurringItems.Appointments.FirstOrDefault((RecurringAppointment q) => q.AppointmentId == RecurringItems.MasterGroupCode) != null) ? RecurringItems.MasterGroupCode : RecurringItems.Appointments[0].AppointmentId;
					List<RecurringAppointment> list = appointmentRecurringInfo.Appointments.FindAll((RecurringAppointment g) => RecurringItems.Appointments.FirstOrDefault((RecurringAppointment h) => h.AppointmentId == g.AppointmentId) == null);
					foreach (RecurringAppointment recurringAppointment in RecurringItems.Appointments)
					{
						bool flag6 = recurringAppointment.AppointmentId > 0;
						if (flag6)
						{
							baseAppointmentManager.UpdateAppointmentParts(false, recurringAppointment, eAppointmentPart.DateTimeAndDuration);
							bool flag7 = num != RecurringItems.MasterGroupCode;
							if (flag7)
							{
								this.dao.UpdateRecurringGroupCode(recurringAppointment.AppointmentId, num, null);
							}
						}
						else
						{
							recurringAppointment.GroupCode = num;
							int appointmentId = baseAppointmentManager.CreateBaseBasicAppointment(true, recurringAppointment);
							recurringAppointment.AppointmentId = appointmentId;
						}
					}
					foreach (RecurringAppointment recurringAppointment2 in list)
					{
						baseAppointmentManager.DeleteAppointment(false, recurringAppointment2.AppointmentId);
					}
				}
			}
		}

		// Token: 0x06000C6C RID: 3180 RVA: 0x00057184 File Offset: 0x00055384
		public AppointmentRecurringInfo LoadCurrentRecurringAppointmentsSet(int MasterGroupCode)
		{
			bool flag = MasterGroupCode < 1;
			AppointmentRecurringInfo result;
			if (flag)
			{
				result = new AppointmentRecurringInfo();
			}
			else
			{
				result = this.dao.LoadCurrentRecurringAppointmentsSet(MasterGroupCode);
			}
			return result;
		}

		// Token: 0x06000C6D RID: 3181 RVA: 0x000571B4 File Offset: 0x000553B4
		public void RemoveAllRecurringAppointmentsExceptionMaster(int MasterGroupCode, int AppointmentId)
		{
			AppointmentRecurringInfo appointmentRecurringInfo = this.LoadCurrentRecurringAppointmentsSet(MasterGroupCode);
			IBaseAppointmentManager baseAppointmentManager = new BaseAppointmentManager(this.OpContext);
			foreach (RecurringAppointment recurringAppointment in appointmentRecurringInfo.Appointments)
			{
				bool flag = recurringAppointment.AppointmentId != AppointmentId;
				if (flag)
				{
					baseAppointmentManager.DeleteAppointment(false, recurringAppointment.AppointmentId);
				}
			}
			this.dao.UpdateRecurringGroupCode(AppointmentId, -1, null);
		}

		// Token: 0x06000C6E RID: 3182 RVA: 0x00057248 File Offset: 0x00055448
		public void UpdateRecurringGroupCode(bool runInTransaction, int AppointmentId, int GroupCode)
		{
			bool flag = !runInTransaction;
			if (flag)
			{
				this.appLogDao.LogAppModificationsPreChangeCommitted(AppointmentId);
			}
			this.dao.UpdateRecurringGroupCode(AppointmentId, GroupCode, null);
			bool flag2 = !runInTransaction;
			if (flag2)
			{
				Task.Run(delegate()
				{
					IAppointmentLogManager appointmentLogManager = new AppointmentLogManager(this.OpContext);
					appointmentLogManager.LogAppModifications(AppointmentId, eAppointmentModifiedItemType.RecurringInfo);
				});
			}
		}

		// Token: 0x06000C6F RID: 3183 RVA: 0x000572B4 File Offset: 0x000554B4
		public IList<RecurringInstance> UpdateRecurringWorkshopAppointmentInstances(WorkshopAppointment workshopApp, IList<RecurringInstance> RecurringInstances, RecurringInstanceSetModifyBehaviour ModifyBehaivour)
		{
			IList<RecurringInstance> list = this.UpdateRecurringAppointmentInstances(workshopApp, RecurringInstances, ModifyBehaivour);
			bool flag = list == null;
			IList<RecurringInstance> result;
			if (flag)
			{
				result = null;
			}
			else
			{
				IWorkshopAppointmentManager workshopAppointmentManager = new WorkshopAppointmentManager(this.OpContext);
				foreach (RecurringInstance recurringInstance in list)
				{
					workshopAppointmentManager.UpdateWorkshopAppointmentMaxAttendees(recurringInstance.AppointmentId, workshopApp.MaxAttendeeCount);
				}
				result = list;
			}
			return result;
		}

		// Token: 0x06000C70 RID: 3184 RVA: 0x0005733C File Offset: 0x0005553C
		public IList<RecurringInstance> UpdateRecurringAppointmentInstances(BaseExtendedAppointment MasterAppointment, IList<RecurringInstance> RecurringInstances, RecurringInstanceSetModifyBehaviour ModifyBehaivour)
		{
			IBaseAppointmentManager baseAppointmentManager = new BaseAppointmentManager(this.OpContext);
			bool flag = MasterAppointment.GroupCode > 0;
			int groupCode;
			bool flag2;
			if (flag)
			{
				groupCode = MasterAppointment.GroupCode;
				flag2 = false;
			}
			else
			{
				groupCode = MasterAppointment.AppointmentId;
				flag2 = true;
			}
			int masterAppointmentId = MasterAppointment.AppointmentId;
			DateTime startDateTime = MasterAppointment.StartDateTime;
			DateTime endDateTime = MasterAppointment.EndDateTime;
			RecurringInstance recurringInstance = RecurringInstances.FirstOrDefault((RecurringInstance g) => g.AppointmentId == masterAppointmentId);
			bool flag3 = recurringInstance != null;
			if (flag3)
			{
				RecurringInstances.Remove(recurringInstance);
			}
			IList<BaseExtendedAppointment> existingAppointments = baseAppointmentManager.LoadBaseExtendedAppointmentsByGroupCode<BaseExtendedAppointment>(groupCode);
			IEnumerable<BaseExtendedAppointment> enumerable = from g in existingAppointments
			where g.AppointmentId > 0 && g.AppointmentId != masterAppointmentId && RecurringInstances.FirstOrDefault((RecurringInstance h) => h.AppointmentId == g.AppointmentId) == null
			select g;
			IEnumerable<RecurringInstance> enumerable2 = from g in RecurringInstances
			where g.AppointmentId > 0 && existingAppointments.FirstOrDefault((BaseExtendedAppointment h) => h.AppointmentId == g.AppointmentId) == null
			select g;
			IEnumerable<RecurringInstance> enumerable3 = from g in RecurringInstances
			where g.AppointmentId < 1
			select g;
			foreach (BaseExtendedAppointment baseExtendedAppointment in enumerable)
			{
				baseAppointmentManager.DeleteAppointment(false, baseExtendedAppointment.AppointmentId);
			}
			foreach (RecurringInstance recurringInstance2 in enumerable2)
			{
				this.UpdateRecurringGroupCode(false, recurringInstance2.AppointmentId, groupCode);
			}
			List<int> list = new List<int>();
			foreach (RecurringInstance recurringInstance3 in enumerable3)
			{
				MasterAppointment.AppointmentId = 0;
				MasterAppointment.GroupCode = groupCode;
				MasterAppointment.StartDateTime = recurringInstance3.StartDateTime;
				MasterAppointment.EndDateTime = recurringInstance3.EndDateTime;
				recurringInstance3.AppointmentId = baseAppointmentManager.CreateBaseExtendedAppointment(false, MasterAppointment);
				list.Add(recurringInstance3.AppointmentId);
			}
			MasterAppointment.AppointmentId = masterAppointmentId;
			MasterAppointment.StartDateTime = startDateTime;
			MasterAppointment.EndDateTime = endDateTime;
			bool flag4 = flag2;
			if (flag4)
			{
				this.UpdateRecurringGroupCode(false, masterAppointmentId, groupCode);
			}
			existingAppointments = baseAppointmentManager.LoadBaseExtendedAppointmentsByGroupCode<BaseExtendedAppointment>(groupCode);
			double totalMinutes = startDateTime.TimeOfDay.TotalMinutes;
			double totalMinutes2 = endDateTime.TimeOfDay.TotalMinutes;
			using (IEnumerator<BaseExtendedAppointment> enumerator4 = existingAppointments.GetEnumerator())
			{
				while (enumerator4.MoveNext())
				{
					BaseExtendedAppointment app = enumerator4.Current;
					bool flag5 = app.AppointmentId != masterAppointmentId && !list.Contains(app.AppointmentId);
					if (flag5)
					{
						RecurringInstance recurringInstance4 = RecurringInstances.FirstOrDefault((RecurringInstance g) => g.AppointmentId == app.AppointmentId);
						bool flag6 = recurringInstance4 != null;
						if (flag6)
						{
							DateTime date = recurringInstance4.StartDateTime.Date;
							app.StartDateTime = recurringInstance4.StartDateTime;
							app.EndDateTime = recurringInstance4.EndDateTime;
							app.Memo = MasterAppointment.Memo;
							app.AppType = MasterAppointment.AppType;
							app.ShowTimeAs = MasterAppointment.ShowTimeAs;
							app.OverrideColour = MasterAppointment.OverrideColour;
							app.SubTitle = MasterAppointment.SubTitle;
							app.Location = MasterAppointment.Location;
							app.IsCancelled = MasterAppointment.IsCancelled;
							app.IsLocked = MasterAppointment.IsLocked;
							app.IsPrivate = MasterAppointment.IsPrivate;
							app.Room = MasterAppointment.Room;
							app.Attendees = MasterAppointment.Attendees;
							app.ExtraAttendeesCount = MasterAppointment.ExtraAttendeesCount;
							baseAppointmentManager.UpdateBaseExtendedAppointment(false, app, ModifyBehaivour);
						}
						else
						{
							CWLogger.Logger.Warn("RecurringAppointmentManager:UpdateRecurringAppointmentInstances:Found an appointment in the group set that doesn't exist in the recurringinstances passed (but should be there!):Appid={0}", app.AppointmentId.ToString());
						}
					}
				}
			}
			bool flag7 = existingAppointments.Count <= 1;
			if (flag7)
			{
				this.UpdateRecurringGroupCode(false, masterAppointmentId, -1);
			}
			return RecurringInstances;
		}

		// Token: 0x06000C71 RID: 3185 RVA: 0x0005783C File Offset: 0x00055A3C
		public IDictionary<int, bool> LoadAppointmentsInARecurringSetWithPermissionsToEditForASpecificUser(int AppointmentId, int PersonId)
		{
			return this.dao.LoadAppointmentsInARecurringSetWithPermissionsToEditForASpecificUser(AppointmentId, PersonId);
		}

		// Token: 0x06000C72 RID: 3186 RVA: 0x0005785C File Offset: 0x00055A5C
		public bool IsUserAllowedToEditAllAppointmentsInARecurringSet(int AppointmentId, int PersonId)
		{
			IDictionary<int, bool> source = this.dao.LoadAppointmentsInARecurringSetWithPermissionsToEditForASpecificUser(AppointmentId, PersonId);
			List<KeyValuePair<int, bool>> list = (from g in source
			where !g.Value
			select g).ToList<KeyValuePair<int, bool>>();
			return list.Count < 1;
		}

		// Token: 0x04000255 RID: 597
		private IRecurringAppointmentDAO dao;

		// Token: 0x04000257 RID: 599
		private IAppointmentLogDAO _appLogDao;
	}
}
