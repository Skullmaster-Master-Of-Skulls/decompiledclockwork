using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;
using ClockWorkLogger;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Adapters;
using TechnoPro.Common.Public.Entities.AppointmentsReminder;
using TechnoPro.Common.Unity.IoC;
using TechnoPro.Common.Win32;

namespace TechnoPro.Common.ClientManager.Notifications.AppointmentsReminder
{
	// Token: 0x0200001B RID: 27
	public class AppointmentsReminderNotificationManager : IDisposable
	{
		// Token: 0x17000022 RID: 34
		// (get) Token: 0x060000B6 RID: 182 RVA: 0x00003550 File Offset: 0x00001750
		public static AppointmentsReminderNotificationManager CurrentInstance
		{
			get
			{
				AppointmentsReminderNotificationManager result;
				if ((result = AppointmentsReminderNotificationManager._currentInstance) == null)
				{
					result = (AppointmentsReminderNotificationManager._currentInstance = new AppointmentsReminderNotificationManager());
				}
				return result;
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x060000B7 RID: 183 RVA: 0x00003568 File Offset: 0x00001768
		// (set) Token: 0x060000B8 RID: 184 RVA: 0x000035C9 File Offset: 0x000017C9
		public List<AppointmentReminder> ActiveAppointmentReminderList
		{
			get
			{
				ICacheStorageManager cacheStorageManager = ObjectFactory.Resolve<ICacheStorageManager>();
				object obj = cacheStorageManager["cAppointmentReminderList"];
				if (obj != null)
				{
					List<AppointmentReminder> list = (from a in (List<AppointmentReminder>)obj
					where !a.AlreadyNotified && a.EndDate >= DateTime.Now
					select a).ToList<AppointmentReminder>();
					cacheStorageManager["cAppointmentReminderList"] = list;
					return list;
				}
				return null;
			}
			set
			{
				ObjectFactory.Resolve<ICacheStorageManager>()["cAppointmentReminderList"] = value;
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x060000B9 RID: 185 RVA: 0x000035DB File Offset: 0x000017DB
		// (set) Token: 0x060000BA RID: 186 RVA: 0x000035F2 File Offset: 0x000017F2
		public bool IsAppointmentsReminderEnable
		{
			get
			{
				return this._displayAppointmentReminderListTimer != null && this._displayAppointmentReminderListTimer.Enabled;
			}
			set
			{
				if (this._displayAppointmentReminderListTimer != null)
				{
					this._displayAppointmentReminderListTimer.Enabled = value;
				}
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x060000BB RID: 187 RVA: 0x00003608 File Offset: 0x00001808
		// (set) Token: 0x060000BC RID: 188 RVA: 0x00003610 File Offset: 0x00001810
		private Timer _displayAppointmentReminderListTimer { get; set; }

		// Token: 0x1400000F RID: 15
		// (add) Token: 0x060000BD RID: 189 RVA: 0x0000361C File Offset: 0x0000181C
		// (remove) Token: 0x060000BE RID: 190 RVA: 0x00003654 File Offset: 0x00001854
		public event EventHandler OnAppointmentReminderDisplayRequired;

		// Token: 0x060000BF RID: 191 RVA: 0x000028FC File Offset: 0x00000AFC
		protected AppointmentsReminderNotificationManager()
		{
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x00003689 File Offset: 0x00001889
		public void Init()
		{
			this.Init(300L, true);
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x00003698 File Offset: 0x00001898
		public void Init(long totalSecondsToDisplayAppointmentReminderList)
		{
			this.Init(totalSecondsToDisplayAppointmentReminderList, true);
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x000036A2 File Offset: 0x000018A2
		public void Init(bool isAppointmentsReminderEnable)
		{
			this.Init(300L, isAppointmentsReminderEnable);
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x000036B1 File Offset: 0x000018B1
		public void Init(long totalSecondsToDisplayAppointmentReminderList, bool isAppointmentsReminderEnable)
		{
			this._displayAppointmentReminderListTimer = new Timer
			{
				Interval = (double)(totalSecondsToDisplayAppointmentReminderList * 1000L)
			};
			this.IsAppointmentsReminderEnable = isAppointmentsReminderEnable;
			this.LoadFromRegistry();
			this._displayAppointmentReminderListTimer.Elapsed += this.DisplayAppointmentReminderListTimerOnElapsed;
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x000036F4 File Offset: 0x000018F4
		public void NotifyAppointmentsReminder(List<AppointmentReminder> apps)
		{
			List<AppointmentReminder> list = this.ActiveAppointmentReminderList;
			if (list != null)
			{
				list.AddRange(apps);
			}
			else
			{
				list = apps;
			}
			this.ActiveAppointmentReminderList = list;
			this.FireOnAppointmentReminderDisplayRequired();
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x00003724 File Offset: 0x00001924
		public void Dismiss(AppointmentReminder appointmentReminder)
		{
			List<AppointmentReminder> activeAppointmentReminderList = this.ActiveAppointmentReminderList;
			if (activeAppointmentReminderList.Remove(appointmentReminder))
			{
				this.ActiveAppointmentReminderList = activeAppointmentReminderList;
				this.FireOnAppointmentReminderDisplayRequired();
			}
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x0000374E File Offset: 0x0000194E
		public void DismissALL()
		{
			this.ActiveAppointmentReminderList = new List<AppointmentReminder>();
			this.FireOnAppointmentReminderDisplayRequired();
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x00003761 File Offset: 0x00001961
		private void DisplayAppointmentReminderListTimerOnElapsed(object sender, ElapsedEventArgs elapsedEventArgs)
		{
			this.FireOnAppointmentReminderDisplayRequired();
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x0000376C File Offset: 0x0000196C
		private void FireOnAppointmentReminderDisplayRequired()
		{
			if (!this.IsAppointmentsReminderEnable)
			{
				return;
			}
			EventHandler onAppointmentReminderDisplayRequired = this.OnAppointmentReminderDisplayRequired;
			if (onAppointmentReminderDisplayRequired != null)
			{
				onAppointmentReminderDisplayRequired(this, EventArgs.Empty);
			}
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x00003798 File Offset: 0x00001998
		public void LoadFromRegistry()
		{
			string text = new RegistryHelper().ReadCurrentUserRegistry<string>(new string[]
			{
				"ClockWork",
				"ActiveAppointmentReminderList"
			});
			if (!string.IsNullOrEmpty(text))
			{
				this.ActiveAppointmentReminderList = text.Deserialize<List<AppointmentReminder>>();
			}
		}

		// Token: 0x060000CA RID: 202 RVA: 0x000037DC File Offset: 0x000019DC
		public void SaveToRegistry()
		{
			List<AppointmentReminder> activeAppointmentReminderList = this.ActiveAppointmentReminderList;
			RegistryHelper registryHelper = new RegistryHelper();
			if (activeAppointmentReminderList != null)
			{
				registryHelper.WriteCurrentUserRegistry<string>(activeAppointmentReminderList.Serialize<List<AppointmentReminder>>(), new string[]
				{
					"ClockWork",
					"ActiveAppointmentReminderList"
				});
			}
		}

		// Token: 0x060000CB RID: 203 RVA: 0x0000381B File Offset: 0x00001A1B
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x060000CC RID: 204 RVA: 0x0000382A File Offset: 0x00001A2A
		private void Dispose(bool disposing)
		{
			if (!this.disposed)
			{
				if (disposing)
				{
					if (this._displayAppointmentReminderListTimer != null)
					{
						this._displayAppointmentReminderListTimer.Close();
					}
					this.SaveToRegistry();
				}
				this.disposed = true;
				CWLogger.Logger.Debug("AppointmentsReminderNotificationManager::Dispose::It has been disposed.");
			}
		}

		// Token: 0x0400004E RID: 78
		private static AppointmentsReminderNotificationManager _currentInstance;

		// Token: 0x04000051 RID: 81
		protected bool disposed;
	}
}
