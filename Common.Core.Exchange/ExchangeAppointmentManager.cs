using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using ClockWorkLogger;
using TechnoPro.Common.Core.Adapters;
using TechnoPro.Common.Core.Appointments;
using TechnoPro.Common.Core.Settings;
using TechnoPro.Common.DAO.AppointmentSync;
using TechnoPro.Common.DAO.Exchange.Impl;
using TechnoPro.Common.ICore.Appointments;
using TechnoPro.Common.ICore.AppointmentSync;
using TechnoPro.Common.ICore.Settings;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities.AppointmentSync;
using TechnoPro.Common.Public.Entities.AppointmentSync.Adapters;
using TechnoPro.Common.Public.Entities.AppointmentSync.FastSync;
using TechnoPro.Common.Public.Entities.Settings;

namespace TechnoPro.Common.Core.Exchange
{
	// Token: 0x02000002 RID: 2
	public class ExchangeAppointmentManager : IExternalAppointmentManager, IBaseOperationContext<SyncOperationContext>
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		// (set) Token: 0x06000002 RID: 2 RVA: 0x00002058 File Offset: 0x00000258
		private IExternalAppointmentDAO ExternalAppointmentDAO { get; set; }

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000003 RID: 3 RVA: 0x00002061 File Offset: 0x00000261
		// (set) Token: 0x06000004 RID: 4 RVA: 0x00002069 File Offset: 0x00000269
		public IApplicationSyncAdministrationManager ApplicationSyncAdministrationMng { get; set; }

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000005 RID: 5 RVA: 0x00002074 File Offset: 0x00000274
		public IList<eSyncAppointmentComparisonIdType> AppointmentCompareWorkflow
		{
			get
			{
				return ExchangeAppointmentManager._appointmentCompareWorkflow;
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000006 RID: 6 RVA: 0x0000208C File Offset: 0x0000028C
		public IList<eSyncAppointmentComparisonIdType> RecurrenceAppointmentCompareWorkflow
		{
			get
			{
				return ExchangeAppointmentManager._recurrenceAppointmentCompareWorkflow;
			}
		}

		// Token: 0x06000007 RID: 7 RVA: 0x000020A4 File Offset: 0x000002A4
		public ExchangeAppointmentManager(SyncOperationContext opContext)
		{
			this.OpContext = opContext;
			this.ApplicationSyncAdministrationMng = new ExchangeSyncAdministrationManager(this.OpContext);
			this.ExternalAppointmentDAO = new ExchangeAppointmentDAO(this.OpContext);
			ISettingManager currentInstance = SettingManager.CurrentInstance;
			this.ExternalAppointmentDAO.PagingSize = currentInstance.GetSettingValue<int>(Setting.CLOCKWORKAPPOINTMENTSYNC_PagingSize);
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002104 File Offset: 0x00000304
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
					case eSyncAppointmentComparisonIdType.UniqueId2:
					{
						bool flag2 = cwapp.Mapping != null && !string.IsNullOrEmpty(cwapp.Mapping.ExternalApplicationUniqueAppointmentId2) && !string.IsNullOrEmpty(exapp.UniqueId2);
						if (flag2)
						{
							return cwapp.Mapping.ExternalApplicationUniqueAppointmentId2.Equals(exapp.UniqueId2);
						}
						break;
					}
					}
					bool flag3 = flag;
					if (flag3)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06000009 RID: 9 RVA: 0x0000229C File Offset: 0x0000049C
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
					case eSyncAppointmentComparisonIdType.UniqueId2:
					{
						bool flag2 = !string.IsNullOrEmpty(exapp1.UniqueId2) && !string.IsNullOrEmpty(exapp2.UniqueId2);
						if (flag2)
						{
							return exapp1.UniqueId.Equals(exapp2.UniqueId2);
						}
						break;
					}
					}
					bool flag3 = flag;
					if (flag3)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x0600000A RID: 10 RVA: 0x00002410 File Offset: 0x00000610
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

		// Token: 0x0600000B RID: 11 RVA: 0x000024A0 File Offset: 0x000006A0
		public void UpdateClockWorkAppId(ExternalAppointmentId appId)
		{
			ExternalAppointment externalAppointment = this.LoadAppointment(appId);
			bool flag = externalAppointment != null;
			if (flag)
			{
				this.ExternalAppointmentDAO.UpdateClockWorkAppId(externalAppointment.UniqueId, appId.ClockWorkAppId);
			}
		}

		// Token: 0x0600000C RID: 12 RVA: 0x000024D8 File Offset: 0x000006D8
		public ExternalAppointment LoadAppointment(ExternalAppointmentId appId, string calendarId)
		{
			return this.LoadAppointment(appId);
		}

		// Token: 0x0600000D RID: 13 RVA: 0x000024F4 File Offset: 0x000006F4
		public ExternalAppointment LoadOcurrenceOfRecurringSerieByAnyOcurrenceId(string uniqueId, int ocurrenceIndex)
		{
			return this.ExternalAppointmentDAO.LoadOcurrenceOfRecurringSerieByAnyOcurrenceId(uniqueId, ocurrenceIndex);
		}

		// Token: 0x0600000E RID: 14 RVA: 0x00002514 File Offset: 0x00000714
		public ExternalAppointment LoadOccurrenceOfRecurringSerieByMasterId(string masterAppUid, int occurenceIndex)
		{
			return this.ExternalAppointmentDAO.LoadOccurrenceOfRecurringSerieByMasterId(masterAppUid, occurenceIndex);
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00002534 File Offset: 0x00000734
		public ExternalAppointment LoadAppointment(ExternalAppointmentId appId)
		{
			ExternalAppointment exApp = null;
			Func<ExternalAttendee, bool> <>9__0;
			foreach (eSyncAppointmentComparisonIdType item in this.AppointmentCompareWorkflow)
			{
				switch (item)
				{
				case eSyncAppointmentComparisonIdType.ClockWorkAppId:
				{
					bool flag = appId.ClockWorkAppId > 0;
					if (flag)
					{
						exApp = this.LoadAppointmentByClockWorkAppointmentId(appId.ClockWorkAppId);
					}
					break;
				}
				case eSyncAppointmentComparisonIdType.GlobalAppId:
				{
					bool flag2 = !string.IsNullOrEmpty(appId.GlobalAppId);
					if (flag2)
					{
						exApp = this.LoadAppointmentByICalUid(appId.GlobalAppId, true);
					}
					break;
				}
				case eSyncAppointmentComparisonIdType.UniqueId:
				{
					bool flag3 = !string.IsNullOrEmpty(appId.UniqueId);
					if (flag3)
					{
						exApp = this.LoadAppointment(appId.UniqueId);
					}
					break;
				}
				case eSyncAppointmentComparisonIdType.UniqueId2:
				{
					bool flag4 = !string.IsNullOrEmpty(appId.UniqueId2);
					if (flag4)
					{
						exApp = this.LoadAppointmentByUniqueId2(appId.UniqueId2);
					}
					break;
				}
				}
				bool flag5 = exApp != null && exApp.IsRecurring && !this.RecurrenceAppointmentCompareWorkflow.Contains(item);
				if (flag5)
				{
					exApp = null;
				}
				bool flag6 = exApp != null;
				if (flag6)
				{
					bool flag7 = appId.ClockWorkAppId > 0;
					if (flag7)
					{
						bool flag8 = exApp.Mapping == null;
						if (flag8)
						{
							exApp.Mapping = new ClockWorkExternalAppMapping();
						}
						exApp.Mapping.ClockWorkAppointmentId = appId.ClockWorkAppId;
						exApp.Mapping.ExternalApplicationUniqueAppointmentId = exApp.UniqueId;
						exApp.Mapping.ExternalApplicationUniqueAppointmentId2 = exApp.UniqueId2;
						exApp.Mapping.ExternalApplicationGlobalAppointmentId = exApp.LegacyGlobalAppointmentId;
					}
					bool flag9 = exApp.Organizer != null;
					if (flag9)
					{
						bool flag10 = exApp.Attendees == null;
						if (flag10)
						{
							bool flag11 = exApp.Organizer != null;
							if (flag11)
							{
								exApp.Attendees = new List<ExternalAttendee>
								{
									exApp.Organizer
								};
							}
						}
						else
						{
							bool flag12 = exApp.Attendees.Count == 0;
							if (flag12)
							{
								bool flag13 = exApp.Organizer != null;
								if (flag13)
								{
									exApp.Attendees.Add(exApp.Organizer);
								}
							}
							else
							{
								IEnumerable<ExternalAttendee> attendees = exApp.Attendees;
								Func<ExternalAttendee, bool> predicate;
								if ((predicate = <>9__0) == null)
								{
									predicate = (<>9__0 = ((ExternalAttendee a) => a.Username.Equals(exApp.Organizer.Username, StringComparison.OrdinalIgnoreCase)));
								}
								ExternalAttendee externalAttendee = attendees.FirstOrDefault(predicate);
								bool flag14 = externalAttendee == null;
								if (flag14)
								{
									exApp.Attendees.Add(exApp.Organizer);
								}
							}
						}
					}
					return exApp;
				}
			}
			return null;
		}

		// Token: 0x06000010 RID: 16 RVA: 0x0000286C File Offset: 0x00000A6C
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

		// Token: 0x06000011 RID: 17 RVA: 0x000028D8 File Offset: 0x00000AD8
		public bool IsAppointmentEditable(ExternalAppointmentId extAppId, string calendarId)
		{
			return this.IsAppointmentEditable(extAppId);
		}

		// Token: 0x06000012 RID: 18 RVA: 0x000028F1 File Offset: 0x00000AF1
		public void DeleteAppointment(ExternalAppointmentId appId, string calendarId)
		{
			this.DeleteAppointment(appId);
		}

		// Token: 0x06000013 RID: 19 RVA: 0x000028FC File Offset: 0x00000AFC
		public IList<ExternalAppointment> LoadAppointments(ExternalAttendee user, DateTime startdate, DateTime endDate)
		{
			List<ExternalAppointment> list = (from a in this.ExternalAppointmentDAO.LoadAppointments(user, startdate, endDate)
			where !a.IsCancelled
			select a).ToList<ExternalAppointment>();
			foreach (ExternalAppointment externalAppointment in list)
			{
				bool flag = externalAppointment.Attendees == null;
				if (flag)
				{
					externalAppointment.Attendees = new List<ExternalAttendee>
					{
						user
					};
				}
				else
				{
					bool flag2 = externalAppointment.Attendees.Count == 0;
					if (flag2)
					{
						externalAppointment.Attendees.Add(user);
					}
				}
			}
			Func<ExternalAttendee, bool> <>9__1;
			foreach (ExternalAppointment externalAppointment2 in list)
			{
				IEnumerable<ExternalAttendee> attendees = externalAppointment2.Attendees;
				Func<ExternalAttendee, bool> predicate;
				if ((predicate = <>9__1) == null)
				{
					predicate = (<>9__1 = ((ExternalAttendee att) => att.Username != null && att.Username.Equals(user.Username, StringComparison.OrdinalIgnoreCase)));
				}
				bool flag3 = !attendees.Any(predicate);
				if (flag3)
				{
					externalAppointment2.Attendees.Add(user);
				}
			}
			return list;
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002A74 File Offset: 0x00000C74
		public IList<ExternalAppointment> TryToLoadAppointments(IList<ExternalAppointmentId> appUidList)
		{
			List<string> list;
			if (appUidList != null && appUidList.Count != 0)
			{
				list = (from i in appUidList
				where i != null && !string.IsNullOrEmpty(i.UniqueId)
				select i.UniqueId).ToList<string>();
			}
			else
			{
				list = null;
			}
			List<string> list2 = list;
			IList<ExternalAppointment> list3 = (list2 == null || list2.Count == 0) ? null : this.ExternalAppointmentDAO.LoadAppointments(list2.ToList<string>());
			using (IEnumerator<ExternalAppointment> enumerator = list3.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					ExternalAppointment app = enumerator.Current;
					bool flag = app.Organizer != null;
					if (flag)
					{
						bool flag2 = app.Attendees == null;
						if (flag2)
						{
							bool flag3 = app.Organizer != null;
							if (flag3)
							{
								app.Attendees = new List<ExternalAttendee>
								{
									app.Organizer
								};
							}
						}
						else
						{
							bool flag4 = app.Attendees.Count == 0;
							if (flag4)
							{
								bool flag5 = app.Organizer != null;
								if (flag5)
								{
									app.Attendees.Add(app.Organizer);
								}
							}
							else
							{
								ExternalAttendee externalAttendee = app.Attendees.FirstOrDefault((ExternalAttendee a) => a.Username.Equals(app.Organizer.Username, StringComparison.OrdinalIgnoreCase));
								bool flag6 = externalAttendee == null;
								if (flag6)
								{
									app.Attendees.Add(app.Organizer);
								}
							}
						}
					}
				}
			}
			return list3;
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00002C5C File Offset: 0x00000E5C
		public IList<ExternalAppointment> LoadModifiedAppointments(ExternalAttendee user, DateTime startdate, DateTime thresholdTime, bool sortedByDate = true)
		{
			return this.ExternalAppointmentDAO.LoadModifiedAppointments(user, startdate, thresholdTime, sortedByDate);
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00002C80 File Offset: 0x00000E80
		public IList<ExternalAppointment> LoadOccurrenceAppointmentsOfRecurrenceSerie(string masterAppUid, DateTime? startDatetime = null, int count = 100, bool loadMapping = false)
		{
			return this.ExternalAppointmentDAO.LoadOccurrenceAppointmentsOfRecurrenceSerie(masterAppUid, startDatetime, count, loadMapping);
		}

		// Token: 0x06000017 RID: 23 RVA: 0x00002CA4 File Offset: 0x00000EA4
		public void CreateAppointment(ExternalAppointment appointment)
		{
			ExternalAppointment externalAppointment = this.ExternalAppointmentDAO.CreateAppointment(appointment);
			ExternalAttendee externalAttendee;
			if (externalAppointment == null)
			{
				externalAttendee = null;
			}
			else
			{
				IList<ExternalAttendee> attendees = externalAppointment.Attendees;
				if (attendees == null)
				{
					externalAttendee = null;
				}
				else
				{
					externalAttendee = attendees.FirstOrDefault((ExternalAttendee a) => a.AttendeeType == eAttendeeType.EVENT_ORGANIZER);
				}
			}
			ExternalAttendee externalAttendee2 = externalAttendee;
			bool flag = externalAttendee2 != null;
			if (flag)
			{
				IBaseAppointmentManager baseAppointmentManager = new BaseAppointmentManager(this.OpContext);
				baseAppointmentManager.UpdateAppointmentExternalId(appointment.Mapping.ClockWorkAppointmentId, externalAttendee2.GetPid(this.OpContext));
			}
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00002D2C File Offset: 0x00000F2C
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
					GlobalAppId = appointment.LegacyGlobalAppointmentId,
					UniqueId2 = appointment.UniqueId2
				});
				bool flag2 = externalAppointment != null;
				if (flag2)
				{
					bool flag3 = externalAppointment.MergeWithAppointment(appointment, this.OpContext.SyncSettings);
					bool flag4 = flag3;
					if (flag4)
					{
						this.ExternalAppointmentDAO.UpdateAppointment(externalAppointment);
					}
				}
			}
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00002DE4 File Offset: 0x00000FE4
		public void DeleteAppointment(ExternalAppointmentId appId)
		{
			ExternalAppointment externalAppointment = this.LoadAppointment(appId);
			bool flag = externalAppointment != null;
			if (flag)
			{
				this.ExternalAppointmentDAO.DeleteAppointment(externalAppointment);
			}
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00002E10 File Offset: 0x00001010
		public void ReloadExternalCalendarLastDateTimeModified(ExternalAppointment appointment)
		{
			ExternalAppointment externalAppointment = this.LoadAppointment(appointment.ExternalAppointmentId());
			bool flag = externalAppointment != null;
			if (flag)
			{
				bool flag2 = externalAppointment.LastModifiedTime > appointment.LastModifiedTime;
				if (flag2)
				{
					appointment.LastModifiedTime = externalAppointment.LastModifiedTime;
				}
				bool flag3 = appointment.Mapping != null && (appointment.Mapping.ExternalApplicationLastUpdatedDate == null || externalAppointment.LastModifiedTime > appointment.Mapping.ExternalApplicationLastUpdatedDate);
				if (flag3)
				{
					appointment.Mapping.ExternalApplicationLastUpdatedDate = new DateTime?(externalAppointment.LastModifiedTime);
				}
			}
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00002EC8 File Offset: 0x000010C8
		public bool SupportsFastSync()
		{
			return true;
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00002EDC File Offset: 0x000010DC
		public ExternalSyncAppointmentChangesResponse LoadAppointmentChanges(ExternalSyncAppointmentChangesRequest request)
		{
			bool flag = string.IsNullOrEmpty(request.SyncState) || request.LastSyncDateTime == null || request.LastSyncDateTime.Value.AddHours(8.0) < DateTime.Now;
			if (flag)
			{
				request.SyncState = this.ExternalAppointmentDAO.ResetSyncState(request.Username);
			}
			ExternalSyncAppointmentChangesResponse externalSyncAppointmentChangesResponse = this.ExternalAppointmentDAO.LoadAppointmentChanges(request);
			externalSyncAppointmentChangesResponse.AppointmentChanges = this.ProccessAppChangesList(externalSyncAppointmentChangesResponse.AppointmentChanges);
			return externalSyncAppointmentChangesResponse;
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00002F78 File Offset: 0x00001178
		public string LoadNativeAppointmentInfo(string appId)
		{
			return this.ExternalAppointmentDAO.LoadNativeAppointmentInfo(appId);
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600001E RID: 30 RVA: 0x00002F96 File Offset: 0x00001196
		// (set) Token: 0x0600001F RID: 31 RVA: 0x00002F9E File Offset: 0x0000119E
		public SyncOperationContext OpContext { get; set; }

		// Token: 0x06000020 RID: 32 RVA: 0x00002FA8 File Offset: 0x000011A8
		private IList<ExternalSyncAppointmentChange> ProccessAppChangesList(IEnumerable<ExternalSyncAppointmentChange> appChangesList)
		{
			List<ExternalSyncAppointmentChange> list = new List<ExternalSyncAppointmentChange>();
			using (IEnumerator<ExternalSyncAppointmentChange> enumerator = appChangesList.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					ExternalSyncAppointmentChange appChange = enumerator.Current;
					ExternalSyncAppointmentChange externalSyncAppointmentChange = (!string.IsNullOrEmpty(appChange.ExternalAppointmentID.UniqueId2) && !string.IsNullOrEmpty(appChange.ExternalAppointmentID.GlobalAppId)) ? list.Find((ExternalSyncAppointmentChange c) => c.ExternalAppointmentID.UniqueId2 == appChange.ExternalAppointmentID.UniqueId2 && c.ExternalAppointmentID.GlobalAppId == appChange.ExternalAppointmentID.GlobalAppId) : null;
					bool flag = externalSyncAppointmentChange != null;
					if (flag)
					{
						CWLogger.Logger.Debug("ExchangeAppointmentManager::ProccessAppChangesList: Making actions '{0}' and '{1}' into 'Modified' action, UniqueId2={2}, GlobalId={3}", new object[]
						{
							externalSyncAppointmentChange.AppointmentSyncChangeType,
							appChange.AppointmentSyncChangeType,
							externalSyncAppointmentChange.ExternalAppointmentID.UniqueId2,
							externalSyncAppointmentChange.ExternalAppointmentID.GlobalAppId
						});
						externalSyncAppointmentChange.AppointmentSyncChangeType = eAppointmentSyncChangeType.Modified;
					}
					else
					{
						list.Add(appChange);
					}
				}
			}
			return list;
		}

		// Token: 0x06000021 RID: 33 RVA: 0x000030C8 File Offset: 0x000012C8
		private ExternalAppointment LoadAppointment(string uniqueId)
		{
			return this.ExternalAppointmentDAO.LoadAppointment(uniqueId);
		}

		// Token: 0x06000022 RID: 34 RVA: 0x000030E8 File Offset: 0x000012E8
		private ExternalAppointment LoadAppointmentByClockWorkAppointmentId(int cwappid)
		{
			return this.ExternalAppointmentDAO.LoadAppointmentByClockWorkAppointmentId(cwappid);
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00003108 File Offset: 0x00001308
		private ExternalAppointment LoadAppointmentByICalUid(string icaluid, bool base64 = true)
		{
			return ((ExchangeAppointmentDAO)this.ExternalAppointmentDAO).LoadAppointmentByICalUid(icaluid, base64);
		}

		// Token: 0x06000024 RID: 36 RVA: 0x0000312C File Offset: 0x0000132C
		private ExternalAppointment LoadAppointmentByUniqueId2(string uniqueId2)
		{
			ExchangeAppointmentDAO exchangeAppointmentDAO = (ExchangeAppointmentDAO)this.ExternalAppointmentDAO;
			ExternalAppointment externalAppointment = exchangeAppointmentDAO.LoadAppointmentByUniqueId2(uniqueId2);
			bool flag = externalAppointment != null;
			ExternalAppointment result;
			if (flag)
			{
				ExchangeAppointmentManager._foundAtLeastOneAppointmentBySearchingUniqueId2OnGeneralCalendarFolder = true;
				result = externalAppointment;
			}
			else
			{
				bool flag2 = !ExchangeAppointmentManager._foundAtLeastOneAppointmentBySearchingUniqueId2OnGeneralCalendarFolder;
				if (flag2)
				{
					foreach (ClockWorkExternalApplicationSyncUser clockWorkExternalApplicationSyncUser in this.OpContext.SyncSettings.SyncUsers)
					{
						externalAppointment = exchangeAppointmentDAO.LoadAppointmentByUniqueId2(clockWorkExternalApplicationSyncUser.ExternalApplicationUsername, uniqueId2);
						bool flag3 = externalAppointment != null;
						if (flag3)
						{
							return externalAppointment;
						}
					}
				}
				result = null;
			}
			return result;
		}

		// Token: 0x06000025 RID: 37 RVA: 0x000031E4 File Offset: 0x000013E4
		// Note: this type is marked as 'beforefieldinit'.
		static ExchangeAppointmentManager()
		{
			eSyncAppointmentComparisonIdType[] array = new eSyncAppointmentComparisonIdType[3];
			RuntimeHelpers.InitializeArray(array, fieldof(<PrivateImplementationDetails>.9413299B9D7B8A2439C8CC65F08204AF9DDDDF8C904DE2C55CA35529AEA247BE).FieldHandle);
			ExchangeAppointmentManager._appointmentCompareWorkflow = array;
			ExchangeAppointmentManager._recurrenceAppointmentCompareWorkflow = new eSyncAppointmentComparisonIdType[]
			{
				eSyncAppointmentComparisonIdType.UniqueId2,
				eSyncAppointmentComparisonIdType.UniqueId
			};
			ExchangeAppointmentManager._foundAtLeastOneAppointmentBySearchingUniqueId2OnGeneralCalendarFolder = false;
		}

		// Token: 0x04000001 RID: 1
		private static readonly eSyncAppointmentComparisonIdType[] _appointmentCompareWorkflow;

		// Token: 0x04000002 RID: 2
		private static readonly eSyncAppointmentComparisonIdType[] _recurrenceAppointmentCompareWorkflow;

		// Token: 0x04000006 RID: 6
		private static bool _foundAtLeastOneAppointmentBySearchingUniqueId2OnGeneralCalendarFolder;
	}
}
