using System;
using System.Collections.Generic;
using ClockWorkLogger;
using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;
using TechnoPro.Common.DAO.AppointmentSync;
using TechnoPro.Common.DAO.GoogleCalendar.Impl.Adapters;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities.AppointmentSync;

namespace TechnoPro.Common.DAO.GoogleCalendar.Impl.V3
{
	// Token: 0x02000002 RID: 2
	public class GoogleCalendarSyncAdministrationV3DAO : IApplicationSyncAdministrationDAO, IBaseOperationContext<SyncOperationContext>
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		// (set) Token: 0x06000002 RID: 2 RVA: 0x00002057 File Offset: 0x00000257
		private static IDictionary<string, DelegatePermissionLevel> DelegateCalendarPermissions { get; set; }

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000003 RID: 3 RVA: 0x0000205F File Offset: 0x0000025F
		// (set) Token: 0x06000004 RID: 4 RVA: 0x00002067 File Offset: 0x00000267
		protected CalendarService CalendarService { get; set; }

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000005 RID: 5 RVA: 0x00002070 File Offset: 0x00000270
		// (set) Token: 0x06000006 RID: 6 RVA: 0x00002088 File Offset: 0x00000288
		public SyncOperationContext OpContext
		{
			get
			{
				return this._opContext;
			}
			set
			{
				this._opContext = value;
				this.CalendarService = this._opContext.CreateGoogleCalendarService();
			}
		}

		// Token: 0x06000007 RID: 7 RVA: 0x000020A4 File Offset: 0x000002A4
		public GoogleCalendarSyncAdministrationV3DAO(SyncOperationContext opContext)
		{
			this.OpContext = opContext;
		}

		// Token: 0x06000008 RID: 8 RVA: 0x000020B8 File Offset: 0x000002B8
		public DelegatePermissionLevel GetDelegatePermissionLevel(string userEmailAddress)
		{
			bool flag = GoogleCalendarSyncAdministrationV3DAO.DelegateCalendarPermissions == null;
			if (flag)
			{
				this.LoadDelegateCalendarPermissions();
			}
			bool flag2 = GoogleCalendarSyncAdministrationV3DAO.DelegateCalendarPermissions != null && GoogleCalendarSyncAdministrationV3DAO.DelegateCalendarPermissions.ContainsKey(userEmailAddress);
			DelegatePermissionLevel result;
			if (flag2)
			{
				result = GoogleCalendarSyncAdministrationV3DAO.DelegateCalendarPermissions[userEmailAddress];
			}
			else
			{
				List<Event> list = new List<Event>();
				try
				{
					EventsResource.ListRequest listRequest = this.CalendarService.Events.List(this.OpContext.CalendarUsername);
					listRequest.MaxResults = new int?(10);
					listRequest.TimeMin = new DateTime?(DateTime.Now.Date);
					listRequest.TimeMax = new DateTime?(DateTime.Now.AddDays(1.0).Date);
					listRequest.SingleEvents = new bool?(true);
					string text = null;
					do
					{
						listRequest.PageToken = text;
						Events events = listRequest.Execute();
						bool flag3 = events.Items != null && events.Items.Count > 0;
						if (flag3)
						{
							list.AddRange(events.Items);
						}
						text = events.NextPageToken;
					}
					while (text != null);
					CWLogger.Logger.Info("GoogleCalendarSyncAdministrationV3DAO::GetDelegatePermissionLevel: Delegate was able to read appointments on calendar=" + userEmailAddress);
					result = (DelegatePermissionLevel.Read | DelegatePermissionLevel.Write);
				}
				catch (Exception ex)
				{
					CWLogger.Logger.ErrorException(string.Format("GoogleCalendarSyncAdministrationV3DAO::GetDelegatePermissionLevel:: Delegate {0} does not have access to {1} calendar: {2}", this.OpContext.SyncSettings.SyncConnection.UserCredentials.Username, userEmailAddress, ex.ToString()), ex);
					CWLogger.Logger.Info("GoogleCalendarSyncAdministrationV3DAO::GetDelegatePermissionLevel: Delegate email='{0}', User email='{1}', permissions = None. Error occurs", this.OpContext.SyncSettings.SyncConnection.UserCredentials.Username ?? "NULL", userEmailAddress ?? "NULL");
					result = DelegatePermissionLevel.None;
				}
			}
			return result;
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002294 File Offset: 0x00000494
		private void LoadDelegateCalendarPermissions()
		{
			GoogleCalendarSyncAdministrationV3DAO.DelegateCalendarPermissions = new Dictionary<string, DelegatePermissionLevel>();
			try
			{
				CWLogger.Logger.Info("********** Delegate permissions ***********");
				string text = null;
				CalendarListResource.ListRequest listRequest = this.CalendarService.CalendarList.List();
				do
				{
					listRequest.PageToken = text;
					CalendarList calendarList = listRequest.Execute();
					IList<CalendarListEntry> items = calendarList.Items;
					bool flag = items == null;
					if (flag)
					{
						break;
					}
					foreach (CalendarListEntry calendarListEntry in items)
					{
						string id = calendarListEntry.Id;
						DelegatePermissionLevel delegatePermissionLevel = calendarListEntry.AccessRole.ToDelegatePermissionLevel();
						GoogleCalendarSyncAdministrationV3DAO.DelegateCalendarPermissions.Add(id, delegatePermissionLevel);
						CWLogger.Logger.Info(string.Format("{0} = {1}", id, delegatePermissionLevel));
					}
					text = calendarList.NextPageToken;
				}
				while (text != null);
			}
			catch (Exception ex)
			{
				CWLogger.Logger.ErrorException(string.Format("GoogleCalendarSyncAdministrationDAO::LoadDelegateCalendarPermissions:: {0}", ex.ToString()), ex);
			}
			finally
			{
				CWLogger.Logger.Info("********** End of Delegate permissions ***********");
			}
		}

		// Token: 0x04000003 RID: 3
		private SyncOperationContext _opContext;
	}
}
