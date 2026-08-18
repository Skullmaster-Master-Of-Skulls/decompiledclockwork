using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TechnoPro.Common.Core.AppointmentLog;
using TechnoPro.Common.DAO.Appointments;
using TechnoPro.Common.DAO.Impl.Appointments;
using TechnoPro.Common.ICore.AppointmentLog;
using TechnoPro.Common.ICore.Appointments;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Appointments;

namespace TechnoPro.Common.Core.Appointments
{
	// Token: 0x02000129 RID: 297
	public class AppointmentAttendeeManager : IAppointmentAttendeeManager, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x06000C87 RID: 3207 RVA: 0x0005835F File Offset: 0x0005655F
		public AppointmentAttendeeManager(OperationContext opContext)
		{
			this.OpContext = opContext;
			this.dao = new AppointmentAttendeeDAO(opContext);
		}

		// Token: 0x170001C4 RID: 452
		// (get) Token: 0x06000C88 RID: 3208 RVA: 0x0005837D File Offset: 0x0005657D
		// (set) Token: 0x06000C89 RID: 3209 RVA: 0x00058385 File Offset: 0x00056585
		public OperationContext OpContext { get; set; }

		// Token: 0x170001C5 RID: 453
		// (get) Token: 0x06000C8A RID: 3210 RVA: 0x00058390 File Offset: 0x00056590
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

		// Token: 0x06000C8B RID: 3211 RVA: 0x000583C8 File Offset: 0x000565C8
		public IDictionary<int, IList<Attendee>> LoadAttendeesByAppointmentIds(IList<int> appointmentIds)
		{
			AppointmentAttendeeDAO appointmentAttendeeDAO = new AppointmentAttendeeDAO(this.OpContext);
			return appointmentAttendeeDAO.LoadAttendeesByAppointmentIds(appointmentIds);
		}

		// Token: 0x06000C8C RID: 3212 RVA: 0x000583F0 File Offset: 0x000565F0
		public IList<Attendee> LoadAttendeesByAppointmentId(int appointmentId)
		{
			return this.dao.LoadAttendeesByAppointmentId(appointmentId);
		}

		// Token: 0x06000C8D RID: 3213 RVA: 0x00058410 File Offset: 0x00056610
		public Attendee LoadAttendeeById(int appointmentId, int personId)
		{
			return this.dao.LoadAttendeeById(appointmentId, personId);
		}

		// Token: 0x06000C8E RID: 3214 RVA: 0x00058430 File Offset: 0x00056630
		public Attendee LoadAttendeeById(int attendeeId)
		{
			return this.dao.LoadAttendeeById(attendeeId);
		}

		// Token: 0x06000C8F RID: 3215 RVA: 0x00058450 File Offset: 0x00056650
		public void InsertOrUpdateAppointmentAttendees(bool runInTransaction, int appointmentId, IList<Attendee> attendees)
		{
			bool flag = !runInTransaction;
			if (flag)
			{
				this.appLogDao.LogAppModificationsPreChangeCommitted(appointmentId);
			}
			this.dao.InsertOrUpdateAppointmentAttendees(appointmentId, attendees, null);
			bool flag2 = !runInTransaction;
			if (flag2)
			{
				Task.Run(delegate()
				{
					IAppointmentLogManager appointmentLogManager = new AppointmentLogManager(this.OpContext);
					appointmentLogManager.LogAppModifications(appointmentId, eAppointmentModifiedItemType.Attendees);
				});
			}
		}

		// Token: 0x06000C90 RID: 3216 RVA: 0x000584BC File Offset: 0x000566BC
		public int InsertOrUpdateAppointmentAttendee(bool runInTransaction, int appointmentId, Attendee attendee)
		{
			bool flag = !runInTransaction;
			if (flag)
			{
				this.appLogDao.LogAppModificationsPreChangeCommitted(appointmentId);
			}
			int result = this.dao.InsertOrUpdateAppointmentAttendee(appointmentId, attendee, null);
			bool flag2 = !runInTransaction;
			if (flag2)
			{
				Task.Run(delegate()
				{
					IAppointmentLogManager appointmentLogManager = new AppointmentLogManager(this.OpContext);
					appointmentLogManager.LogAppModifications(appointmentId, eAppointmentModifiedItemType.Attendees);
				});
			}
			return result;
		}

		// Token: 0x06000C91 RID: 3217 RVA: 0x00058530 File Offset: 0x00056730
		public void DeleteAttendee(bool runInTransaction, int appointmentId, int personId)
		{
			bool flag = this.TryToRemoveAttendees(appointmentId, new int[]
			{
				personId
			}) != null;
			if (flag)
			{
				throw new InvalidOperationException(string.Format("You cannot remove attendee personid ({0}) from appointment id ({1})", personId, appointmentId));
			}
			bool flag2 = !runInTransaction;
			if (flag2)
			{
				this.appLogDao.LogAppModificationsPreChangeCommitted(appointmentId);
			}
			this.dao.DeleteAttendee(appointmentId, personId, null);
			bool flag3 = !runInTransaction;
			if (flag3)
			{
				Task.Run(delegate()
				{
					IAppointmentLogManager appointmentLogManager = new AppointmentLogManager(this.OpContext);
					appointmentLogManager.LogAppModifications(appointmentId, eAppointmentModifiedItemType.Attendees);
				});
			}
		}

		// Token: 0x06000C92 RID: 3218 RVA: 0x000585DC File Offset: 0x000567DC
		private int LoadAppointmentIdByAttendee(int attendeeId)
		{
			return this.dao.LoadAppointmentIdByAttendee(attendeeId);
		}

		// Token: 0x06000C93 RID: 3219 RVA: 0x000585FC File Offset: 0x000567FC
		public void DeleteAttendee(bool runInTransaction, int attendeeId)
		{
			bool flag = this.TryToRemoveAttendees(new List<int>
			{
				attendeeId
			}) != null;
			if (flag)
			{
				throw new InvalidOperationException(string.Format("You cannot remove attendee id ({0}) from the appointment", attendeeId));
			}
			bool flag2 = !runInTransaction;
			if (flag2)
			{
				int num = this.LoadAppointmentIdByAttendee(attendeeId);
				bool flag3 = num > 0;
				if (flag3)
				{
					this.appLogDao.LogAppModificationsPreChangeCommitted(num);
				}
			}
			int appId = this.dao.DeleteAttendee(attendeeId, null);
			bool flag4 = appId > 0 && !runInTransaction;
			if (flag4)
			{
				Task.Run(delegate()
				{
					IAppointmentLogManager appointmentLogManager = new AppointmentLogManager(this.OpContext);
					appointmentLogManager.LogAppModifications(appId, eAppointmentModifiedItemType.Attendees);
				});
			}
		}

		// Token: 0x06000C94 RID: 3220 RVA: 0x000586B0 File Offset: 0x000568B0
		public void RemoveAttendeesNotInList(bool runInTransaction, int appointmentId, IList<int> personIds)
		{
			bool flag = !runInTransaction;
			if (flag)
			{
				this.appLogDao.LogAppModificationsPreChangeCommitted(appointmentId);
			}
			this.dao.RemoveAttendeesNotInList(appointmentId, personIds, null);
			bool flag2 = !runInTransaction;
			if (flag2)
			{
				Task.Run(delegate()
				{
					IAppointmentLogManager appointmentLogManager = new AppointmentLogManager(this.OpContext);
					appointmentLogManager.LogAppModifications(appointmentId, eAppointmentModifiedItemType.Attendees);
				});
			}
		}

		// Token: 0x06000C95 RID: 3221 RVA: 0x0005871C File Offset: 0x0005691C
		public void UpdateNoShowValue(bool runInTransaction, int appointmentId, int personId, bool noShowValue)
		{
			bool flag = !runInTransaction;
			if (flag)
			{
				this.appLogDao.LogAppModificationsPreChangeCommitted(appointmentId);
			}
			this.dao.UpdateNoShowValue(appointmentId, personId, noShowValue, null);
			bool flag2 = !runInTransaction;
			if (flag2)
			{
				Task.Run(delegate()
				{
					IAppointmentLogManager appointmentLogManager = new AppointmentLogManager(this.OpContext);
					appointmentLogManager.LogAppModifications(appointmentId, eAppointmentModifiedItemType.NoShow);
				});
			}
		}

		// Token: 0x06000C96 RID: 3222 RVA: 0x0005878C File Offset: 0x0005698C
		public void UpdateNoShowValue(bool runInTransaction, int attendeeId, bool noShowValue)
		{
			bool flag = !runInTransaction;
			if (flag)
			{
				int num = this.LoadAppointmentIdByAttendee(attendeeId);
				bool flag2 = num > 0;
				if (flag2)
				{
					this.appLogDao.LogAppModificationsPreChangeCommitted(num);
				}
			}
			int appointmentId = this.dao.UpdateNoShowValue(attendeeId, noShowValue, null);
			bool flag3 = appointmentId > 0 && !runInTransaction;
			if (flag3)
			{
				Task.Run(delegate()
				{
					IAppointmentLogManager appointmentLogManager = new AppointmentLogManager(this.OpContext);
					appointmentLogManager.LogAppModifications(appointmentId, eAppointmentModifiedItemType.NoShow);
				});
			}
		}

		// Token: 0x06000C97 RID: 3223 RVA: 0x0005880C File Offset: 0x00056A0C
		public void UpdateMiscCodeValue(bool runInTransaction, int appointmentId, int personId, int misccodeValue)
		{
			bool flag = !runInTransaction;
			if (flag)
			{
				this.appLogDao.LogAppModificationsPreChangeCommitted(appointmentId);
			}
			this.dao.UpdateMiscCodeValue(appointmentId, personId, misccodeValue, null);
			bool flag2 = !runInTransaction;
			if (flag2)
			{
				Task.Run(delegate()
				{
					IAppointmentLogManager appointmentLogManager = new AppointmentLogManager(this.OpContext);
					appointmentLogManager.LogAppModifications(appointmentId, eAppointmentModifiedItemType.None);
				});
			}
		}

		// Token: 0x06000C98 RID: 3224 RVA: 0x0005887C File Offset: 0x00056A7C
		public void UpdateMiscCodeValue(bool runInTransaction, int attendeeId, int misccodeValue)
		{
			bool flag = !runInTransaction;
			if (flag)
			{
				int num = this.LoadAppointmentIdByAttendee(attendeeId);
				bool flag2 = num > 0;
				if (flag2)
				{
					this.appLogDao.LogAppModificationsPreChangeCommitted(num);
				}
			}
			int appointmentId = this.dao.UpdateMiscCodeValue(attendeeId, misccodeValue, null);
			bool flag3 = appointmentId > 0 && !runInTransaction;
			if (flag3)
			{
				Task.Run(delegate()
				{
					IAppointmentLogManager appointmentLogManager = new AppointmentLogManager(this.OpContext);
					appointmentLogManager.LogAppModifications(appointmentId, eAppointmentModifiedItemType.None);
				});
			}
		}

		// Token: 0x06000C99 RID: 3225 RVA: 0x000588FC File Offset: 0x00056AFC
		public void SwapAttendee(bool runInTransaction, int AppointmentId, int OldPersonId, int NewPersonId)
		{
			bool flag = !runInTransaction;
			if (flag)
			{
				this.appLogDao.LogAppModificationsPreChangeCommitted(AppointmentId);
			}
			this.dao.SwapAttendee(AppointmentId, OldPersonId, NewPersonId, null);
			bool flag2 = !runInTransaction;
			if (flag2)
			{
				Task.Run(delegate()
				{
					IAppointmentLogManager appointmentLogManager = new AppointmentLogManager(this.OpContext);
					appointmentLogManager.LogAppModifications(AppointmentId, eAppointmentModifiedItemType.Attendees);
				});
			}
		}

		// Token: 0x06000C9A RID: 3226 RVA: 0x0005896C File Offset: 0x00056B6C
		public IList<AttendeeWithAppointmentId> LoadAttendeesWhoHaveNoShowedInThePast(DateTime? minimumDateToCheckFrom, int SkipAppointmentsWithThisIconId = -1, int[] AppTypeIds = null)
		{
			return this.dao.LoadAttendeesWhoHaveNoShowedInThePast((minimumDateToCheckFrom != null) ? minimumDateToCheckFrom.Value : DateTime.Now.Date.AddDays(-14.0), SkipAppointmentsWithThisIconId, AppTypeIds);
		}

		// Token: 0x06000C9B RID: 3227 RVA: 0x000589BC File Offset: 0x00056BBC
		public bool IsAttendeeDoubleBooked(int PersonId, DateTime StartDateTime, DateTime EndDateTime, int AppointmentIdToSkip)
		{
			IList<int> doubleBookedAttendees = this.GetDoubleBookedAttendees(new List<int>
			{
				PersonId
			}, StartDateTime, EndDateTime, AppointmentIdToSkip);
			return doubleBookedAttendees.Contains(PersonId);
		}

		// Token: 0x06000C9C RID: 3228 RVA: 0x000589F0 File Offset: 0x00056BF0
		public IList<int> GetDoubleBookedAttendees(IList<int> PersonIdsToCheck, DateTime StartDateTime, DateTime EndDateTime, int AppointmentIdToSkip)
		{
			return this.dao.GetDoubleBookedAttendees(PersonIdsToCheck, StartDateTime, EndDateTime, AppointmentIdToSkip);
		}

		// Token: 0x06000C9D RID: 3229 RVA: 0x00058A14 File Offset: 0x00056C14
		public bool CheckIfDoubleBooked(int PersonId, DateTime StartDateTime, DateTime EndDateTime, params int[] AppTypeIds)
		{
			return this.dao.CheckIfDoubleBooked(PersonId, StartDateTime, EndDateTime, AppTypeIds);
		}

		// Token: 0x06000C9E RID: 3230 RVA: 0x00058A38 File Offset: 0x00056C38
		public IList<int> TryToRemoveAttendees(int appointmentId, params int[] attendeeIds)
		{
			bool flag = attendeeIds == null;
			IList<int> result;
			if (flag)
			{
				result = null;
			}
			else
			{
				result = this.dao.TryToRemoveAttendees(appointmentId, attendeeIds);
			}
			return result;
		}

		// Token: 0x06000C9F RID: 3231 RVA: 0x00058A64 File Offset: 0x00056C64
		public IList<int> TryToRemoveAttendees(IList<int> attendeeIds)
		{
			bool flag = attendeeIds == null || attendeeIds.Count == 0;
			IList<int> result;
			if (flag)
			{
				result = null;
			}
			else
			{
				result = this.dao.TryToRemoveAttendees(attendeeIds);
			}
			return result;
		}

		// Token: 0x0400025D RID: 605
		private IAppointmentAttendeeDAO dao;

		// Token: 0x0400025F RID: 607
		private IAppointmentLogDAO _appLogDao;
	}
}
