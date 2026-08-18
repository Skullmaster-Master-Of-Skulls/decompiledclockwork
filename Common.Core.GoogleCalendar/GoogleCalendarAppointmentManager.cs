using System;
using System.Collections.Generic;
using System.Linq;
using TechnoPro.Common.Core.Adapters;
using TechnoPro.Common.Core.Appointments;
using TechnoPro.Common.DAO.AppointmentSync;
using TechnoPro.Common.DAO.GoogleCalendar.Impl.V3;
using TechnoPro.Common.ICore.Appointments;
using TechnoPro.Common.ICore.AppointmentSync;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities.AppointmentSync;
using TechnoPro.Common.Public.Entities.AppointmentSync.Adapters;
using TechnoPro.Common.Public.Entities.AppointmentSync.FastSync;

namespace TechnoPro.Common.Core.GoogleCalendar
{
	// Token: 0x02000002 RID: 2
	public class GoogleCalendarAppointmentManager : IExternalAppointmentManager, IBaseOperationContext<SyncOperationContext>
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		// (set) Token: 0x06000002 RID: 2 RVA: 0x00002058 File Offset: 0x00000258
		private IExternalAppointmentDAO ExternalAppointmentDAO { get; set; }

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000003 RID: 3 RVA: 0x00002064 File Offset: 0x00000264
		public IList<eSyncAppointmentComparisonIdType> AppointmentCompareWorkflow
		{
			get
			{
				return GoogleCalendarAppointmentManager._appointmentCompareWorkflow;
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000004 RID: 4 RVA: 0x0000207C File Offset: 0x0000027C
		public IList<eSyncAppointmentComparisonIdType> RecurrenceAppointmentCompareWorkflow
		{
			get
			{
				return GoogleCalendarAppointmentManager._recurrenceAppointmentCompareWorkflow;
			}
		}

		// Token: 0x06000005 RID: 5 RVA: 0x00002093 File Offset: 0x00000293
		public GoogleCalendarAppointmentManager(SyncOperationContext opContext)
		{
			this.OpContext = opContext;
			this.ApplicationSyncAdministrationMng = new GoogleCalendarSyncAdministrationManager(this.OpContext);
			this.ExternalAppointmentDAO = new GoogleCalendarV3AppointmentDAO(this.OpContext);
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000006 RID: 6 RVA: 0x000020C9 File Offset: 0x000002C9
		// (set) Token: 0x06000007 RID: 7 RVA: 0x000020D1 File Offset: 0x000002D1
		public SyncOperationContext OpContext { get; set; }

		// Token: 0x06000008 RID: 8 RVA: 0x000020DC File Offset: 0x000002DC
		public bool AppointmentsAreEqual(ClockWorkSyncAppointment cwapp, ExternalAppointment exapp)
		{
			bool flag = false;
			IList<eSyncAppointmentComparisonIdType> list = exapp.IsRecurring ? this.RecurrenceAppointmentCompareWorkflow : this.AppointmentCompareWorkflow;
			using (IEnumerator<eSyncAppointmentComparisonIdType> enumerator = list.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					switch (enumerator.Current)
					{
					case eSyncAppointmentComparisonIdType.ClockWorkAppId:
						flag = (exapp.Mapping != null && exapp.Mapping.ClockWorkAppointmentId > 0 && exapp.Mapping.ClockWorkAppointmentId == cwapp.AppointmentId);
						break;
					case eSyncAppointmentComparisonIdType.GlobalAppId:
						flag = (cwapp.Mapping != null && !string.IsNullOrEmpty(cwapp.Mapping.ExternalApplicationGlobalAppointmentId) && cwapp.Mapping.ExternalApplicationGlobalAppointmentId.Equals(exapp.LegacyGlobalAppointmentId));
						break;
					case eSyncAppointmentComparisonIdType.UniqueId:
						flag = (cwapp.Mapping != null && !string.IsNullOrEmpty(cwapp.Mapping.ExternalApplicationUniqueAppointmentId) && cwapp.Mapping.ExternalApplicationUniqueAppointmentId.Equals(exapp.UniqueId));
						break;
					}
					bool flag2 = flag;
					if (flag2)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002210 File Offset: 0x00000410
		public bool AppointmentsAreEqual(ExternalAppointment exapp1, ExternalAppointment exapp2)
		{
			bool flag = false;
			IList<eSyncAppointmentComparisonIdType> list = (exapp1.IsRecurring || exapp2.IsRecurring) ? this.RecurrenceAppointmentCompareWorkflow : this.AppointmentCompareWorkflow;
			using (IEnumerator<eSyncAppointmentComparisonIdType> enumerator = list.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					switch (enumerator.Current)
					{
					case eSyncAppointmentComparisonIdType.ClockWorkAppId:
						flag = (exapp1.Mapping != null && exapp2.Mapping != null && exapp1.Mapping.ClockWorkAppointmentId > 0 && exapp1.Mapping.ClockWorkAppointmentId == exapp2.Mapping.ClockWorkAppointmentId);
						break;
					case eSyncAppointmentComparisonIdType.GlobalAppId:
						flag = (!string.IsNullOrEmpty(exapp1.LegacyGlobalAppointmentId) && exapp1.UniqueId.Equals(exapp2.LegacyGlobalAppointmentId));
						break;
					case eSyncAppointmentComparisonIdType.UniqueId:
						flag = (!string.IsNullOrEmpty(exapp1.UniqueId) && exapp1.UniqueId.Equals(exapp2.UniqueId));
						break;
					}
					bool flag2 = flag;
					if (flag2)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x0600000A RID: 10 RVA: 0x00002334 File Offset: 0x00000534
		public bool ExternalAppointmentIdAreEquals(ExternalAppointmentId exappId1, ExternalAppointmentId exappId2)
		{
			bool flag = !string.IsNullOrEmpty(exappId1.UniqueId2) && exappId1.UniqueId2 == exappId2.UniqueId2;
			bool result;
			if (flag)
			{
				result = true;
			}
			else
			{
				bool flag2 = !string.IsNullOrEmpty(exappId1.UniqueId) && exappId1.UniqueId == exappId2.UniqueId2;
				if (flag2)
				{
					result = true;
				}
				else
				{
					bool flag3 = !string.IsNullOrEmpty(exappId1.GlobalAppId) && exappId1.GlobalAppId == exappId2.GlobalAppId;
					result = flag3;
				}
			}
			return result;
		}

		// Token: 0x0600000B RID: 11 RVA: 0x000023C4 File Offset: 0x000005C4
		public ExternalAppointment LoadAppointment(ExternalAppointmentId appId, string calendarId)
		{
			return this.LoadAppointment(appId.UniqueId, calendarId);
		}

		// Token: 0x0600000C RID: 12 RVA: 0x000023E4 File Offset: 0x000005E4
		public ExternalAppointment LoadOcurrenceOfRecurringSerieByAnyOcurrenceId(string uniqueId, int ocurrenceIndex)
		{
			return this.ExternalAppointmentDAO.LoadOcurrenceOfRecurringSerieByAnyOcurrenceId(uniqueId, ocurrenceIndex);
		}

		// Token: 0x0600000D RID: 13 RVA: 0x00002404 File Offset: 0x00000604
		public ExternalAppointment LoadOccurrenceOfRecurringSerieByMasterId(string masterAppUid, int occurenceIndex)
		{
			return this.ExternalAppointmentDAO.LoadOccurrenceOfRecurringSerieByMasterId(masterAppUid, occurenceIndex);
		}

		// Token: 0x0600000E RID: 14 RVA: 0x00002424 File Offset: 0x00000624
		public bool SupportsFastSync()
		{
			return false;
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00002438 File Offset: 0x00000638
		public ExternalSyncAppointmentChangesResponse LoadAppointmentChanges(ExternalSyncAppointmentChangesRequest request)
		{
			return this.ExternalAppointmentDAO.LoadAppointmentChanges(request);
		}

		// Token: 0x06000010 RID: 16 RVA: 0x00002458 File Offset: 0x00000658
		public string LoadNativeAppointmentInfo(string appId)
		{
			return this.ExternalAppointmentDAO.LoadNativeAppointmentInfo(appId);
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00002478 File Offset: 0x00000678
		public ExternalAppointment LoadAppointment(ExternalAppointmentId appId)
		{
			ExternalAppointment externalAppointment = null;
			foreach (eSyncAppointmentComparisonIdType item in this.AppointmentCompareWorkflow)
			{
				switch (item)
				{
				case eSyncAppointmentComparisonIdType.ClockWorkAppId:
				{
					bool flag = appId.ClockWorkAppId > 0;
					if (flag)
					{
						externalAppointment = this.LoadAppointmentByClockWorkAppointmentId(appId.ClockWorkAppId);
					}
					break;
				}
				case eSyncAppointmentComparisonIdType.UniqueId:
				{
					bool flag2 = !string.IsNullOrEmpty(appId.UniqueId);
					if (flag2)
					{
						externalAppointment = this.LoadAppointment(appId.UniqueId);
					}
					break;
				}
				}
				bool flag3 = externalAppointment != null && externalAppointment.IsRecurring && !this.RecurrenceAppointmentCompareWorkflow.Contains(item);
				if (flag3)
				{
					externalAppointment = null;
				}
				bool flag4 = externalAppointment != null;
				if (flag4)
				{
					return externalAppointment;
				}
			}
			return null;
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002564 File Offset: 0x00000764
		public void DeleteAppointment(ExternalAppointmentId appId, string calendarId)
		{
			ExternalAppointment externalAppointment = this.LoadAppointment(appId, calendarId);
			bool flag = externalAppointment != null;
			if (flag)
			{
				this.ExternalAppointmentDAO.DeleteAppointment(externalAppointment);
			}
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002590 File Offset: 0x00000790
		public IList<ExternalAppointment> LoadAppointments(ExternalAttendee user, DateTime startdate, DateTime endDate)
		{
			IList<ExternalAppointment> list = this.ExternalAppointmentDAO.LoadAppointments(user, startdate, endDate);
			Func<ExternalAttendee, bool> <>9__0;
			foreach (ExternalAppointment externalAppointment in list)
			{
				IEnumerable<ExternalAttendee> attendees = externalAppointment.Attendees;
				Func<ExternalAttendee, bool> predicate;
				if ((predicate = <>9__0) == null)
				{
					predicate = (<>9__0 = ((ExternalAttendee att) => att.Username.Equals(user.Username, StringComparison.OrdinalIgnoreCase)));
				}
				bool flag = !attendees.Any(predicate);
				if (flag)
				{
					externalAppointment.Attendees.Add(user);
				}
			}
			return list;
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002648 File Offset: 0x00000848
		public IList<ExternalAppointment> TryToLoadAppointments(IList<ExternalAppointmentId> appUidList)
		{
			return null;
		}

		// Token: 0x06000015 RID: 21 RVA: 0x0000265C File Offset: 0x0000085C
		public IList<ExternalAppointment> LoadModifiedAppointments(ExternalAttendee user, DateTime startdate, DateTime thresholdTime, bool sortedByDate = true)
		{
			return this.ExternalAppointmentDAO.LoadModifiedAppointments(user, startdate, thresholdTime, sortedByDate);
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00002680 File Offset: 0x00000880
		public IList<ExternalAppointment> LoadOccurrenceAppointmentsOfRecurrenceSerie(string masterAppUid, DateTime? startDatetime = null, int count = 100, bool loadMapping = false)
		{
			return this.ExternalAppointmentDAO.LoadOccurrenceAppointmentsOfRecurrenceSerie(masterAppUid, startDatetime, count, loadMapping);
		}

		// Token: 0x06000017 RID: 23 RVA: 0x000026A4 File Offset: 0x000008A4
		public ExternalAppointment LoadAppointment(string uniqueId)
		{
			return this.ExternalAppointmentDAO.LoadAppointment(uniqueId);
		}

		// Token: 0x06000018 RID: 24 RVA: 0x000026C4 File Offset: 0x000008C4
		public ExternalAppointment LoadAppointment(string uniqueId, string calendarId)
		{
			string calendarUsername = this.OpContext.CalendarUsername;
			ExternalAppointment result;
			try
			{
				bool flag = calendarId != null;
				if (flag)
				{
					this.OpContext.CalendarUsername = calendarId;
				}
				result = this.ExternalAppointmentDAO.LoadAppointment(uniqueId);
			}
			finally
			{
				this.OpContext.CalendarUsername = calendarUsername;
			}
			return result;
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00002724 File Offset: 0x00000924
		public ExternalAppointment LoadAppointmentByClockWorkAppointmentId(int cwappid)
		{
			return this.ExternalAppointmentDAO.LoadAppointmentByClockWorkAppointmentId(cwappid);
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00002744 File Offset: 0x00000944
		public void CreateAppointment(ExternalAppointment appointment)
		{
			ExternalAppointment externalAppointment = this.ExternalAppointmentDAO.CreateAppointment(appointment);
			ExternalAttendee externalAttendee = externalAppointment.Attendees.FirstOrDefault((ExternalAttendee a) => a.AttendeeType == eAttendeeType.EVENT_ORGANIZER);
			bool flag = externalAttendee != null;
			if (flag)
			{
				IBaseAppointmentManager baseAppointmentManager = new BaseAppointmentManager(this.OpContext);
				baseAppointmentManager.UpdateAppointmentExternalId(appointment.Mapping.ClockWorkAppointmentId, externalAttendee.GetPid(this.OpContext));
			}
		}

		// Token: 0x0600001B RID: 27 RVA: 0x000027C0 File Offset: 0x000009C0
		public void UpdateAppointment(ExternalAppointment appointment)
		{
			DelegatePermissionLevel delegatePermissionLevel = this.ApplicationSyncAdministrationMng.GetDelegatePermissionLevel(appointment.Organizer.Username);
			bool flag = (delegatePermissionLevel & DelegatePermissionLevel.Write) == DelegatePermissionLevel.Write;
			if (flag)
			{
				ExternalAppointment externalAppointment = this.LoadAppointment(new ExternalAppointmentId
				{
					ClockWorkAppId = ((appointment.Mapping != null) ? appointment.Mapping.ClockWorkAppointmentId : 0),
					UniqueId = appointment.UniqueId,
					GlobalAppId = appointment.LegacyGlobalAppointmentId
				}, appointment.FirstClockWorkSyncAttendee(this.OpContext.SyncSettings));
				bool flag2 = externalAppointment.MergeWithAppointment(appointment, this.OpContext.SyncSettings);
				bool flag3 = flag2;
				if (flag3)
				{
					this.ExternalAppointmentDAO.UpdateAppointment(externalAppointment);
				}
			}
		}

		// Token: 0x0600001C RID: 28 RVA: 0x0000286C File Offset: 0x00000A6C
		public void DeleteAppointment(ExternalAppointmentId appId)
		{
			ExternalAppointment externalAppointment = this.LoadAppointment(appId);
			bool flag = externalAppointment != null;
			if (flag)
			{
				this.ExternalAppointmentDAO.DeleteAppointment(externalAppointment);
			}
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00002898 File Offset: 0x00000A98
		public bool IsAppointmentEditable(ExternalAppointmentId extAppId)
		{
			ExternalAppointment externalAppointment = this.LoadAppointment(extAppId);
			bool flag = externalAppointment.Attendees.Any((ExternalAttendee a) => a.MailboxType == eMailboxType.Group);
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				DelegatePermissionLevel delegatePermissionLevel = this.ApplicationSyncAdministrationMng.GetDelegatePermissionLevel(externalAppointment.Organizer.Username);
				result = ((delegatePermissionLevel & DelegatePermissionLevel.Write) == DelegatePermissionLevel.Write);
			}
			return result;
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00002904 File Offset: 0x00000B04
		public bool IsAppointmentEditable(ExternalAppointmentId extAppId, string calendarId)
		{
			ExternalAppointment externalAppointment = this.LoadAppointment(extAppId, calendarId);
			bool flag = externalAppointment.Attendees.Any((ExternalAttendee a) => a.MailboxType == eMailboxType.Group);
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				DelegatePermissionLevel delegatePermissionLevel = this.ApplicationSyncAdministrationMng.GetDelegatePermissionLevel(externalAppointment.Organizer.Username);
				result = ((delegatePermissionLevel & DelegatePermissionLevel.Write) == DelegatePermissionLevel.Write);
			}
			return result;
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00002970 File Offset: 0x00000B70
		public void ReloadExternalCalendarLastDateTimeModified(ExternalAppointment appointment)
		{
			string calendarUsername = this.OpContext.CalendarUsername;
			ExternalAppointment externalAppointment;
			try
			{
				this.OpContext.CalendarUsername = appointment.FirstClockWorkSyncAttendee(this.OpContext.SyncSettings);
				externalAppointment = this.ExternalAppointmentDAO.LoadAppointment(appointment.UniqueId);
			}
			finally
			{
				this.OpContext.CalendarUsername = calendarUsername;
			}
			bool flag = externalAppointment != null;
			if (flag)
			{
				appointment.LastModifiedTime = externalAppointment.LastModifiedTime;
				bool flag2 = appointment.Mapping != null;
				if (flag2)
				{
					appointment.Mapping.ExternalApplicationLastUpdatedDate = new DateTime?(externalAppointment.LastModifiedTime);
				}
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000020 RID: 32 RVA: 0x00002A1C File Offset: 0x00000C1C
		// (set) Token: 0x06000021 RID: 33 RVA: 0x00002A24 File Offset: 0x00000C24
		public IApplicationSyncAdministrationManager ApplicationSyncAdministrationMng { get; set; }

		// Token: 0x04000001 RID: 1
		private static readonly eSyncAppointmentComparisonIdType[] _appointmentCompareWorkflow = new eSyncAppointmentComparisonIdType[]
		{
			eSyncAppointmentComparisonIdType.UniqueId
		};

		// Token: 0x04000002 RID: 2
		private static readonly eSyncAppointmentComparisonIdType[] _recurrenceAppointmentCompareWorkflow = new eSyncAppointmentComparisonIdType[]
		{
			eSyncAppointmentComparisonIdType.ClockWorkAppId,
			eSyncAppointmentComparisonIdType.UniqueId
		};
	}
}
