using System;
using System.Collections.Generic;
using System.Linq;
using ClockWorkLogger;
using TechnoPro.Common.Core.ApplicationSyncFactories;
using TechnoPro.Common.Core.AppointmentLog;
using TechnoPro.Common.Core.Appointments;
using TechnoPro.Common.Core.AppointmentsCalendar;
using TechnoPro.Common.DAO.Appointments;
using TechnoPro.Common.DAO.AppointmentSync;
using TechnoPro.Common.DAO.Impl.Appointments;
using TechnoPro.Common.DAO.Impl.AppointmentSync;
using TechnoPro.Common.ICore.AppointmentLog;
using TechnoPro.Common.ICore.Appointments;
using TechnoPro.Common.ICore.AppointmentsCalendar;
using TechnoPro.Common.ICore.AppointmentSync;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities.Appointments;
using TechnoPro.Common.Public.Entities.AppointmentSync;
using TechnoPro.Common.Public.Entities.AppointmentSync.FastSync;

namespace TechnoPro.Common.Core.AppointmentSync
{
	// Token: 0x02000135 RID: 309
	public class ClockWorkSyncAppointmentManager : IClockWorkSyncAppointmentManager, IBaseOperationContext<SyncOperationContext>
	{
		// Token: 0x170001DC RID: 476
		// (get) Token: 0x06000D55 RID: 3413 RVA: 0x00061338 File Offset: 0x0005F538
		// (set) Token: 0x06000D56 RID: 3414 RVA: 0x00061340 File Offset: 0x0005F540
		public IClockWorkSyncDAO ClockWorkSyncDAO { get; set; }

		// Token: 0x170001DD RID: 477
		// (get) Token: 0x06000D57 RID: 3415 RVA: 0x00061349 File Offset: 0x0005F549
		// (set) Token: 0x06000D58 RID: 3416 RVA: 0x00061351 File Offset: 0x0005F551
		public IAppointmentSyncMappingManager AppointmentSyncMappingManager { get; set; }

		// Token: 0x170001DE RID: 478
		// (get) Token: 0x06000D59 RID: 3417 RVA: 0x0006135A File Offset: 0x0005F55A
		// (set) Token: 0x06000D5A RID: 3418 RVA: 0x00061362 File Offset: 0x0005F562
		public IExternalAppointmentManager ExternalAppointmentManager { get; set; }

		// Token: 0x06000D5B RID: 3419 RVA: 0x0006136C File Offset: 0x0005F56C
		public ClockWorkSyncAppointmentManager(SyncOperationContext opContext)
		{
			this.OpContext = opContext;
			this.ClockWorkSyncDAO = new ClockWorkSyncDAO(opContext);
			this.AppointmentSyncMappingManager = new AppointmentSyncMappingManager(this.OpContext);
			this.ExternalAppointmentManager = ApplicationSyncFactory.GetSyncFactory(this.OpContext).CreateExternalAppointmentManager();
		}

		// Token: 0x170001DF RID: 479
		// (get) Token: 0x06000D5C RID: 3420 RVA: 0x000613C0 File Offset: 0x0005F5C0
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

		// Token: 0x06000D5D RID: 3421 RVA: 0x000613F6 File Offset: 0x0005F5F6
		public void UpdateClockWorkSyncAppointmentReadOnlyStatus(bool runInTransaction, int appointmentId, bool newReadOnlyStatus)
		{
			this.ClockWorkSyncDAO.UpdateClockWorkSyncAppointmentReadOnlyStatus(appointmentId, newReadOnlyStatus);
		}

		// Token: 0x06000D5E RID: 3422 RVA: 0x00061408 File Offset: 0x0005F608
		public List<ClockWorkSyncAppointment> LoadClockWorkAppointments(List<int> personIds, DateTime startDate, DateTime endDate, bool includeCancelled)
		{
			return this.ClockWorkSyncDAO.LoadClockWorkAppointments(personIds, startDate, endDate, includeCancelled);
		}

		// Token: 0x06000D5F RID: 3423 RVA: 0x0006142C File Offset: 0x0005F62C
		public ClockWorkSyncAppointment LoadClockWorkAppointmentById(int appointmentId)
		{
			return this.ClockWorkSyncDAO.LoadClockWorkAppointmentById(appointmentId);
		}

		// Token: 0x06000D60 RID: 3424 RVA: 0x0006144C File Offset: 0x0005F64C
		public DateTime GetClockWorkAppointmentLastModifiedDateTime(int appointmentId)
		{
			return this.ClockWorkSyncDAO.GetClockWorkAppointmentLastModifiedDateTime(appointmentId);
		}

		// Token: 0x06000D61 RID: 3425 RVA: 0x0006146C File Offset: 0x0005F66C
		public bool CreateClockWorkSyncAppointment(bool runInTransaction, ClockWorkSyncAppointment Appointment, ExternalAppointment exapp)
		{
			IAppointmentSyncMappingManager appointmentSyncMappingManager = new AppointmentSyncMappingManager(this.OpContext);
			ClockWorkExternalAppMapping clockWorkExternalAppMapping = (Appointment.Mapping != null) ? appointmentSyncMappingManager.LoadMappingByExternalId(exapp) : null;
			bool flag = clockWorkExternalAppMapping == null;
			bool result;
			if (flag)
			{
				this.ClockWorkSyncDAO.CreateClockWorkSyncAppointment(Appointment);
				result = true;
			}
			else
			{
				Appointment.AppointmentId = clockWorkExternalAppMapping.ClockWorkAppointmentId;
				result = false;
			}
			return result;
		}

		// Token: 0x06000D62 RID: 3426 RVA: 0x000614C8 File Offset: 0x0005F6C8
		public void UpdateClockWorkSyncAppointment(bool runInTransaction, ClockWorkSyncAppointment Appointment)
		{
			bool flag = !runInTransaction;
			if (flag)
			{
				this.appLogDao.LogAppModificationsPreChangeCommitted(Appointment.AppointmentId);
			}
			this.ClockWorkSyncDAO.UpdateClockWorkSyncAppointment(Appointment);
			bool flag2 = !runInTransaction;
			if (flag2)
			{
				IAppointmentLogManager appointmentLogManager = new AppointmentLogManager(this.OpContext);
				appointmentLogManager.LogAppModifications(Appointment.AppointmentId, eAppointmentModifiedItemType.None);
			}
		}

		// Token: 0x06000D63 RID: 3427 RVA: 0x00061524 File Offset: 0x0005F724
		public void DeleteClockWorkSyncAppointment(bool runInTransaction, int AppointmentId)
		{
			IBaseAppointmentManager baseAppointmentManager = new BaseAppointmentManager(this.OpContext);
			baseAppointmentManager.DeleteAppointment(runInTransaction, AppointmentId);
		}

		// Token: 0x06000D64 RID: 3428 RVA: 0x00061548 File Offset: 0x0005F748
		public void CancelClockWorkSyncAppointment(bool runInTransaction, int AppointmentId)
		{
			IAppointmentManager appointmentManager = new AppointmentManager(this.OpContext);
			appointmentManager.CancelAppointment(runInTransaction, AppointmentId, new AppCancelInfo
			{
				CancelReasonText = "Cancelled by Outlook Sync"
			});
		}

		// Token: 0x06000D65 RID: 3429 RVA: 0x0006157C File Offset: 0x0005F77C
		public eClockWorkExternalApplicationAppointmentCompareResult CheckAppointmentDiff(ExternalAppointment externalAppointment, ClockWorkSyncAppointment clockWorkAppointment)
		{
			ClockWorkExternalAppMapping mapping = clockWorkAppointment.Mapping;
			bool flag = mapping.ClockWorkLastUpdatedDate != null;
			bool flag2;
			if (flag)
			{
				TimeSpan timeSpan = clockWorkAppointment.LastModifiedTime - mapping.ClockWorkLastUpdatedDate.Value;
				flag2 = (timeSpan.TotalSeconds >= 1.0 || timeSpan.TotalSeconds <= -1.0);
			}
			else
			{
				flag2 = false;
			}
			bool flag3 = mapping.ExternalApplicationLastUpdatedDate != null;
			bool flag4;
			if (flag3)
			{
				TimeSpan timeSpan2 = externalAppointment.LastModifiedTime - mapping.ExternalApplicationLastUpdatedDate.Value;
				flag4 = (timeSpan2.TotalSeconds >= 1.0 || timeSpan2.TotalSeconds <= -1.0);
			}
			else
			{
				flag4 = false;
			}
			CWLogger.Logger.Debug("ClockWorkSyncAppointmentManager:CheckAppointmentDiff:ClockWorkAppId={0}:External.Uniqueid2={1}:clockWorkChanged={2}:OutlookChanged={3}:mapping.ClockWorkLastModified={4}:mapping.OutlookLastModified={5},ClockWorkLastModified={6},OutlookLastModified={7}", new object[]
			{
				clockWorkAppointment.AppointmentId.ToString(),
				externalAppointment.UniqueId2 ?? "NULL",
				flag2.ToString(),
				flag4.ToString(),
				(mapping.ClockWorkLastUpdatedDate != null) ? mapping.ClockWorkLastUpdatedDate.Value.ToString("yyyy-MM-dd H:mm:ss") : "NULL",
				(mapping.ExternalApplicationLastUpdatedDate != null) ? mapping.ExternalApplicationLastUpdatedDate.Value.ToString("yyyy-MM-dd H:mm:ss") : "NULL",
				clockWorkAppointment.LastModifiedTime.ToString("yyyy-MM-dd H:mm:ss"),
				externalAppointment.LastModifiedTime.ToString("yyyy-MM-dd H:mm:ss")
			});
			bool flag5 = flag2 && flag4;
			eClockWorkExternalApplicationAppointmentCompareResult result;
			if (flag5)
			{
				bool flag6 = clockWorkAppointment.LastModifiedTime >= externalAppointment.LastModifiedTime;
				if (flag6)
				{
					result = eClockWorkExternalApplicationAppointmentCompareResult.ClockWorkChangedLast;
				}
				else
				{
					result = eClockWorkExternalApplicationAppointmentCompareResult.ExternalApplicationChangedLast;
				}
			}
			else
			{
				bool flag7 = flag2;
				if (flag7)
				{
					result = eClockWorkExternalApplicationAppointmentCompareResult.ClockWorkChangedLast;
				}
				else
				{
					bool flag8 = flag4;
					if (flag8)
					{
						result = eClockWorkExternalApplicationAppointmentCompareResult.ExternalApplicationChangedLast;
					}
					else
					{
						TimeSpan timeSpan3 = (externalAppointment.IsAllDayEvent && clockWorkAppointment.IsAllDayEvent) ? TimeSpan.Zero : (externalAppointment.StartDate - clockWorkAppointment.StartDateTime);
						TimeSpan timeSpan4 = (externalAppointment.IsAllDayEvent && clockWorkAppointment.IsAllDayEvent) ? TimeSpan.Zero : (externalAppointment.EndDate - clockWorkAppointment.EndDateTime);
						bool flag9 = externalAppointment.IsCancelled != clockWorkAppointment.IsCancelled || timeSpan3.TotalMinutes >= 1.0 || timeSpan3.TotalMinutes <= -1.0 || timeSpan4.TotalMinutes >= 1.0 || timeSpan4.TotalMinutes <= -1.0 || (externalAppointment.Location != null && clockWorkAppointment.Location != null && !externalAppointment.Location.Equals(clockWorkAppointment.Location, StringComparison.OrdinalIgnoreCase));
						if (flag9)
						{
							result = ((clockWorkAppointment.LastModifiedTime >= externalAppointment.LastModifiedTime) ? eClockWorkExternalApplicationAppointmentCompareResult.ClockWorkChangedLast : eClockWorkExternalApplicationAppointmentCompareResult.ExternalApplicationChangedLast);
						}
						else
						{
							IEnumerable<ExternalAttendee> source = from att in externalAppointment.Attendees
							where this.OpContext.SyncSettings.SyncUsers.Exists((ClockWorkExternalApplicationSyncUser syncUser) => syncUser.ExternalApplicationUsername.Equals(att.Username, StringComparison.OrdinalIgnoreCase))
							select att;
							IEnumerable<ClockWorkSyncAttendee> source2 = from cwAtt in clockWorkAppointment.Attendees
							where this.OpContext.SyncSettings.SyncUsers.Exists((ClockWorkExternalApplicationSyncUser syncUser) => syncUser.ClockWorkUser.PersonId.Equals(cwAtt.Attendee.PersonId))
							select cwAtt;
							int num = source.Count<ExternalAttendee>();
							int num2 = source2.Count<ClockWorkSyncAttendee>();
							CWLogger.Logger.Debug("CheckAppointmentDiff:nOutlookAttendeesInSyncUsers={0}, nClockworkAttendeesInSyncUsers={1}:cwappid={2}:outlookuniqueid={3}", new object[]
							{
								num,
								num2,
								(clockWorkAppointment == null) ? "NULL" : clockWorkAppointment.AppointmentId.ToString(),
								(externalAppointment == null) ? "NULL" : externalAppointment.UniqueId.ToString()
							});
							bool flag10 = num != num2;
							if (flag10)
							{
								CWLogger.Logger.Debug("CheckAppointmentDiff:DifferentAttendeeCountTriggerAppointmentUpdate:cwappid={0}:outlookuniqueid={1}", (clockWorkAppointment == null) ? "NULL" : clockWorkAppointment.AppointmentId.ToString(), (externalAppointment == null) ? "NULL" : externalAppointment.UniqueId.ToString());
								CWLogger.Logger.Debug("CheckAppointmentDiff::DifferentAttendeeCount::{0}", (num > num2) ? "OutlookChangedLast" : "ClockWorkChangedLast");
								result = ((num > num2) ? eClockWorkExternalApplicationAppointmentCompareResult.ExternalApplicationChangedLast : eClockWorkExternalApplicationAppointmentCompareResult.ClockWorkChangedLast);
							}
							else
							{
								result = eClockWorkExternalApplicationAppointmentCompareResult.AppointmentsAreEqual;
							}
						}
					}
				}
			}
			return result;
		}

		// Token: 0x06000D66 RID: 3430 RVA: 0x000619B0 File Offset: 0x0005FBB0
		public ClockWorkSyncAppointmentChangeResponse LoadAppointmentChanges(ClockWorkSyncAppointmentChangeRequest request)
		{
			bool flag = request.ClockWorkSyncState == null || request.ClockWorkSyncState.Value.AddHours(8.0) < DateTime.Now;
			if (flag)
			{
				request.ClockWorkSyncState = new DateTime?(this.ClockWorkSyncDAO.ResetSyncState(request.ClockWorkPersonId));
			}
			return this.ClockWorkSyncDAO.LoadAppointmentChanges(request);
		}

		// Token: 0x170001E0 RID: 480
		// (get) Token: 0x06000D67 RID: 3431 RVA: 0x00061A2D File Offset: 0x0005FC2D
		// (set) Token: 0x06000D68 RID: 3432 RVA: 0x00061A35 File Offset: 0x0005FC35
		public SyncOperationContext OpContext { get; set; }

		// Token: 0x0400027E RID: 638
		private IAppointmentLogDAO _appLogDao;
	}
}
