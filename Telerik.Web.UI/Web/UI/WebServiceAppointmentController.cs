using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web.Configuration;
using Telerik.Web.UI.Scheduling;

namespace Telerik.Web.UI
{
	// Token: 0x02001A27 RID: 6695
	public class WebServiceAppointmentController : IDisposable
	{
		// Token: 0x17004EBF RID: 20159
		// (get) Token: 0x06010405 RID: 66565 RVA: 0x003A159C File Offset: 0x0039F79C
		private SchedulerProviderBase Provider
		{
			get
			{
				return this._provider;
			}
		}

		// Token: 0x17004EC0 RID: 20160
		// (get) Token: 0x06010406 RID: 66566 RVA: 0x003A15A4 File Offset: 0x0039F7A4
		private RadScheduler Scheduler
		{
			get
			{
				if (this._scheduler == null)
				{
					this._scheduler = new RadScheduler();
					this._scheduler.Provider = this.Provider;
				}
				return this._scheduler;
			}
		}

		// Token: 0x17004EC1 RID: 20161
		// (get) Token: 0x06010407 RID: 66567 RVA: 0x003A15D0 File Offset: 0x0039F7D0
		private AppointmentController Controller
		{
			get
			{
				if (this._controller == null)
				{
					this._controller = new AppointmentController(this.Scheduler);
				}
				return this._controller;
			}
		}

		// Token: 0x17004EC2 RID: 20162
		// (get) Token: 0x06010408 RID: 66568 RVA: 0x003A15F1 File Offset: 0x0039F7F1
		// (set) Token: 0x06010409 RID: 66569 RVA: 0x003A160C File Offset: 0x0039F80C
		public IAppointmentFactory AppointmentFactory
		{
			get
			{
				if (this._appointmentFactory == null)
				{
					this._appointmentFactory = new DefaultAppointmentFactory();
				}
				return this._appointmentFactory;
			}
			set
			{
				this._appointmentFactory = value;
			}
		}

		// Token: 0x17004EC3 RID: 20163
		// (get) Token: 0x0601040A RID: 66570 RVA: 0x003A1615 File Offset: 0x0039F815
		// (set) Token: 0x0601040B RID: 66571 RVA: 0x003A1630 File Offset: 0x0039F830
		public IComparer<Appointment> AppointmentComparer
		{
			get
			{
				if (this._appointmentComparer == null)
				{
					this._appointmentComparer = new AppointmentComparer();
				}
				return this._appointmentComparer;
			}
			set
			{
				this._appointmentComparer = value;
			}
		}

		// Token: 0x0601040C RID: 66572 RVA: 0x003A163C File Offset: 0x0039F83C
		public WebServiceAppointmentController()
		{
			RadSchedulerConfigurationSection radSchedulerConfigurationSection = (RadSchedulerConfigurationSection)WebConfigurationManager.GetSection("telerik.web.ui/radScheduler");
			if (radSchedulerConfigurationSection == null)
			{
				throw new ConfigurationErrorsException("The telerik.web.ui/radScheduler section is missing from web.config. Unable to load default provider.");
			}
			this.LoadProvider(radSchedulerConfigurationSection.DefaultAppointmentProvider);
		}

		// Token: 0x0601040D RID: 66573 RVA: 0x003A168D File Offset: 0x0039F88D
		public WebServiceAppointmentController(string providerName)
		{
			this.LoadProvider(providerName);
		}

		// Token: 0x0601040E RID: 66574 RVA: 0x003A16B0 File Offset: 0x0039F8B0
		public WebServiceAppointmentController(SchedulerProviderBase provider)
		{
			this._provider = provider;
		}

		// Token: 0x0601040F RID: 66575 RVA: 0x003A16D3 File Offset: 0x0039F8D3
		private IEnumerable<T> GetAppointments<T>(ISchedulerInfo schedulerInfo, Appointment appointment) where T : IAppointmentData, new()
		{
			if (schedulerInfo.UpdateMode != AppointmentUpdateMode.Batch)
			{
				return this.GetAppointment<T>(schedulerInfo, appointment);
			}
			return this.GetAppointments<T>(schedulerInfo);
		}

		// Token: 0x06010410 RID: 66576 RVA: 0x003A16ED File Offset: 0x0039F8ED
		public IEnumerable<AppointmentData> GetAppointments(ISchedulerInfo schedulerInfo)
		{
			return this.GetAppointments<AppointmentData>(schedulerInfo);
		}

		// Token: 0x06010411 RID: 66577 RVA: 0x003A17D4 File Offset: 0x0039F9D4
		public IEnumerable<T> GetAppointment<T>(ISchedulerInfo schedulerInfo, Appointment appointment) where T : IAppointmentData, new()
		{
			this.PopulateAppointments(schedulerInfo);
			List<T> appointmentData = new List<T>();
			if (appointment == null)
			{
				return appointmentData;
			}
			Action<IEnumerable<Appointment>> action = delegate(IEnumerable<Appointment> appointments)
			{
				foreach (Appointment srcAppointment in appointments)
				{
					T item = (default(T) == null) ? Activator.CreateInstance<T>() : default(T);
					item.CopyFrom(srcAppointment);
					appointmentData.Add(item);
				}
			};
			if (appointment.IsMaster())
			{
				RecurrenceRule recurrenceRule;
				if (RecurrenceRule.TryParse(appointment.RecurrenceRule, out recurrenceRule))
				{
					recurrenceRule.SetEffectiveRange(schedulerInfo.ViewStart, schedulerInfo.ViewEnd);
					action(from a in this.Scheduler.Appointments
					where object.Equals(a.ID, appointment.ID) || object.Equals(a.RecurrenceParentID, appointment.ID)
					select a);
				}
			}
			else if (appointment.IsException())
			{
				Appointment parent = this.Scheduler.Appointments.FindByID(appointment.RecurrenceParentID);
				List<Appointment> list = (from a in this.Scheduler.Appointments
				where object.Equals(a.RecurrenceParentID, parent.ID)
				select a).ToList<Appointment>();
				list.Insert(0, parent);
				action(list);
			}
			else if (appointment.IsNotRecurring())
			{
				action(new Appointment[]
				{
					appointment
				});
			}
			return appointmentData;
		}

		// Token: 0x06010412 RID: 66578 RVA: 0x003A1DFC File Offset: 0x0039FFFC
		public IEnumerable<T> GetAppointments<T>(ISchedulerInfo schedulerInfo) where T : IAppointmentData, new()
		{
			this.PopulateAppointments(schedulerInfo);
			bool hasDayLimit = schedulerInfo.VisibleAppointmentsPerDay > 0 && schedulerInfo.UpdateMode == AppointmentUpdateMode.Batch;
			if (hasDayLimit)
			{
				this.Scheduler.Appointments.Sort(this.AppointmentComparer);
			}
			Hashtable parentAppointmentKeys = new Hashtable();
			DateTime start = this.Scheduler.UtcDayStart(schedulerInfo.ViewStart);
			DateTime end = this.Scheduler.UtcDayStart(schedulerInfo.ViewEnd);
			end = end.AddDays(1.0);
			LinkedList<T> filteredAppointments = new LinkedList<T>();
			foreach (Appointment appointment in this.Scheduler.Appointments)
			{
				if (appointment.Overlaps(start, end) || appointment.RecurrenceState == RecurrenceState.Master || appointment.Reminders.Count > 0)
				{
					T value = (default(T) == null) ? Activator.CreateInstance<T>() : default(T);
					value.CopyFrom(appointment);
					filteredAppointments.AddLast(value);
					if (appointment.RecurrenceParentID != null && !parentAppointmentKeys.ContainsKey(appointment.RecurrenceParentID))
					{
						parentAppointmentKeys.Add(appointment.RecurrenceParentID, null);
					}
				}
			}
			AppointmentsPerDayCounter counter = new AppointmentsPerDayCounter();
			foreach (T aptData in filteredAppointments)
			{
				T t = aptData;
				bool flag;
				if (t.RecurrenceState == RecurrenceState.Master)
				{
					T t2 = aptData;
					flag = t2.Visible;
				}
				else
				{
					flag = false;
				}
				bool visibleMaster = flag;
				T t3 = aptData;
				bool flag2;
				if (t3.RecurrenceState == RecurrenceState.Master)
				{
					Hashtable hashtable = parentAppointmentKeys;
					T t4 = aptData;
					flag2 = hashtable.ContainsKey(t4.ID);
				}
				else
				{
					flag2 = false;
				}
				bool masterWithOccurrences = flag2;
				T t5 = aptData;
				bool withReminders = t5.Reminders.Count > 0;
				T t6 = aptData;
				if (t6.RecurrenceState != RecurrenceState.Master || visibleMaster || masterWithOccurrences || withReminders)
				{
					if (hasDayLimit && !masterWithOccurrences)
					{
						RadScheduler scheduler = this.Scheduler;
						T t7 = aptData;
						DateTime date = scheduler.UtcToDisplay(t7.Start).Date;
						T t8 = aptData;
						DateTime end2 = t8.End;
						T t9 = aptData;
						TimeSpan duration = end2 - t9.Start;
						bool flag3 = counter.RegisterAppointment(date, duration, schedulerInfo);
						if (!flag3)
						{
							continue;
						}
					}
					yield return aptData;
				}
			}
			yield break;
		}

		// Token: 0x06010413 RID: 66579 RVA: 0x003A1E20 File Offset: 0x003A0020
		public IEnumerable<AppointmentData> InsertAppointment(ISchedulerInfo schedulerInfo, AppointmentData appointmentData)
		{
			return this.InsertAppointment<AppointmentData>(schedulerInfo, appointmentData);
		}

		// Token: 0x06010414 RID: 66580 RVA: 0x003A1E2C File Offset: 0x003A002C
		public IEnumerable<T> InsertAppointment<T>(ISchedulerInfo schedulerInfo, T appointmentData) where T : IAppointmentData, new()
		{
			this.InitializeScheduler(schedulerInfo);
			Appointment appointment = this.Scheduler.CreateAppointment();
			appointmentData.CopyTo(appointment);
			this.Controller.InsertAppointment(schedulerInfo, appointment);
			return this.GetAppointments<T>(schedulerInfo, appointment);
		}

		// Token: 0x06010415 RID: 66581 RVA: 0x003A1E6F File Offset: 0x003A006F
		public IEnumerable<AppointmentData> UpdateAppointment(ISchedulerInfo schedulerInfo, AppointmentData appointmentData)
		{
			return this.UpdateAppointment<AppointmentData>(schedulerInfo, appointmentData);
		}

		// Token: 0x06010416 RID: 66582 RVA: 0x003A1E7C File Offset: 0x003A007C
		public IEnumerable<T> UpdateAppointment<T>(ISchedulerInfo schedulerInfo, T appointmentData) where T : IAppointmentData, new()
		{
			this.PopulateAppointments(schedulerInfo);
			Appointment appointment = this.Scheduler.Appointments.FindByID(appointmentData.ID);
			if (appointment == null)
			{
				return this.GetAppointments<T>(schedulerInfo, null);
			}
			Appointment appointment2 = this.Scheduler.CreateAppointment();
			appointmentData.CopyTo(appointment2);
			this.Controller.UpdateAppointment(schedulerInfo, appointment, appointment2);
			if (schedulerInfo.UpdateMode == AppointmentUpdateMode.Single && appointment2.IsException())
			{
				T item = (default(T) == null) ? Activator.CreateInstance<T>() : default(T);
				item.CopyFrom(appointment2);
				return new List<T>
				{
					item
				};
			}
			return this.GetAppointments<T>(schedulerInfo, appointment2);
		}

		// Token: 0x06010417 RID: 66583 RVA: 0x003A1F3B File Offset: 0x003A013B
		public IEnumerable<AppointmentData> CreateRecurrenceException(ISchedulerInfo schedulerInfo, AppointmentData recurrenceExceptionData)
		{
			return this.CreateRecurrenceException<AppointmentData>(schedulerInfo, recurrenceExceptionData);
		}

		// Token: 0x06010418 RID: 66584 RVA: 0x003A1F48 File Offset: 0x003A0148
		public IEnumerable<T> CreateRecurrenceException<T>(ISchedulerInfo schedulerInfo, T recurrenceExceptionData) where T : IAppointmentData, new()
		{
			this.PopulateAppointments(schedulerInfo);
			Appointment appointment = this.Scheduler.Appointments.FindByID(recurrenceExceptionData.ID);
			Appointment appointment2 = this.Controller.PrepareToEdit(appointment, false);
			object recurrenceParentID = appointment2.RecurrenceParentID;
			appointment2.Resources.Clear();
			appointment2.Reminders.Clear();
			recurrenceExceptionData.CopyTo(appointment2);
			appointment2.ID = null;
			appointment2.RecurrenceParentID = recurrenceParentID;
			appointment2.RecurrenceState = RecurrenceState.Exception;
			this.Controller.UpdateAppointment(schedulerInfo, appointment, appointment2);
			return this.GetAppointments<T>(schedulerInfo, appointment2);
		}

		// Token: 0x06010419 RID: 66585 RVA: 0x003A1FDE File Offset: 0x003A01DE
		public IEnumerable<AppointmentData> RemoveRecurrenceExceptions(ISchedulerInfo schedulerInfo, AppointmentData masterAppointmentData)
		{
			return this.RemoveRecurrenceExceptions<AppointmentData>(schedulerInfo, masterAppointmentData);
		}

		// Token: 0x0601041A RID: 66586 RVA: 0x003A1FE8 File Offset: 0x003A01E8
		public IEnumerable<T> RemoveRecurrenceExceptions<T>(ISchedulerInfo schedulerInfo, T masterAppointmentData) where T : IAppointmentData, new()
		{
			this.PopulateAppointments(schedulerInfo);
			Appointment appointment = this.Scheduler.CreateAppointment();
			appointment.Owner = this.Scheduler;
			masterAppointmentData.CopyTo(appointment);
			this.Controller.RemoveRecurrenceExceptions(schedulerInfo, appointment);
			return this.GetAppointments<T>(schedulerInfo, appointment);
		}

		// Token: 0x0601041B RID: 66587 RVA: 0x003A2037 File Offset: 0x003A0237
		public IEnumerable<ResourceData> GetResources(ISchedulerInfo schedulerInfo)
		{
			return this.GetResources<ResourceData>(schedulerInfo);
		}

		// Token: 0x0601041C RID: 66588 RVA: 0x003A2040 File Offset: 0x003A0240
		public IEnumerable<T> GetResources<T>(ISchedulerInfo schedulerInfo) where T : IResourceData, new()
		{
			this.InitializeScheduler(schedulerInfo);
			List<T> list = new List<T>();
			this.Provider.LegacyOwner = this.Scheduler;
			IDictionary<ResourceType, IEnumerable<Resource>> resources = this.Provider.GetResources(schedulerInfo);
			if (resources != null)
			{
				foreach (ResourceType key in resources.Keys)
				{
					foreach (Resource srcResource in resources[key])
					{
						T item = (default(T) == null) ? Activator.CreateInstance<T>() : default(T);
						item.CopyFrom(srcResource);
						list.Add(item);
					}
				}
			}
			return list;
		}

		// Token: 0x0601041D RID: 66589 RVA: 0x003A2134 File Offset: 0x003A0334
		public IEnumerable<AppointmentData> DeleteAppointment(ISchedulerInfo schedulerInfo, AppointmentData appointmentData, bool deleteSeries)
		{
			return this.DeleteAppointment<AppointmentData>(schedulerInfo, appointmentData, deleteSeries);
		}

		// Token: 0x0601041E RID: 66590 RVA: 0x003A2140 File Offset: 0x003A0340
		public IEnumerable<T> DeleteAppointment<T>(ISchedulerInfo schedulerInfo, T appointmentData, bool deleteSeries) where T : IAppointmentData, new()
		{
			this.PopulateAppointments(schedulerInfo);
			Appointment appointment = this.Scheduler.CreateAppointment();
			appointmentData.CopyTo(appointment);
			this.Controller.DeleteAppointment(schedulerInfo, appointment, deleteSeries);
			IEnumerable<T> result = new List<T>();
			if (schedulerInfo.UpdateMode == AppointmentUpdateMode.Single)
			{
				if (!deleteSeries)
				{
					switch (appointment.RecurrenceState)
					{
					case RecurrenceState.Master:
						result = this.GetAppointments<T>(schedulerInfo, appointment);
						break;
					case RecurrenceState.Occurrence:
						result = this.GetAppointments<T>(schedulerInfo, this.Scheduler.Appointments.FindByID(appointment.RecurrenceParentID));
						break;
					}
				}
				return result;
			}
			return this.GetAppointments<T>(schedulerInfo);
		}

		// Token: 0x0601041F RID: 66591 RVA: 0x003A21D9 File Offset: 0x003A03D9
		private void LoadProvider(string providerName)
		{
			if (providerName == "Integrated")
			{
				throw new ConfigurationErrorsException("The Integrated provider is not supported when binding to a Web Service.");
			}
			this._provider = SchedulerProviderFactory.GetProvider(providerName);
		}

		// Token: 0x06010420 RID: 66592 RVA: 0x003A2200 File Offset: 0x003A0400
		private void PopulateAppointments(ISchedulerInfo schedulerInfo)
		{
			this.InitializeScheduler(schedulerInfo);
			this.Provider.LegacyOwner = this.Scheduler;
			IEnumerable<Appointment> appointments = this.Provider.GetAppointments(schedulerInfo);
			foreach (Appointment appointment in appointments)
			{
				appointment.Validate();
				DateTime viewStart = schedulerInfo.ViewStart;
				DateTime dateTime = schedulerInfo.ViewEnd;
				if (appointment.Reminders.Count > 0)
				{
					DateTime dateTime2 = viewStart.Add(this.maxReminderTrigger);
					if (dateTime < dateTime2)
					{
						dateTime = dateTime2;
					}
				}
				this.Controller.AddAppointmentAndExpand(appointment, viewStart, dateTime);
			}
		}

		// Token: 0x06010421 RID: 66593 RVA: 0x003A22B8 File Offset: 0x003A04B8
		private void InitializeScheduler(ISchedulerInfo schedulerInfo)
		{
			this.Scheduler.Appointments.Clear();
			this.Scheduler.VisibleRangeStart = schedulerInfo.ViewStart;
			this.Scheduler.VisibleRangeEnd = schedulerInfo.ViewEnd;
			this.Scheduler.AppointmentFactory = this.AppointmentFactory;
			this.Scheduler.EnableDescriptionField = schedulerInfo.EnableDescriptionField;
			if (schedulerInfo.MinutesPerRow > 0)
			{
				this.Scheduler.MinutesPerRow = schedulerInfo.MinutesPerRow;
			}
			this.Scheduler.TimeZoneOffset = TimeSpan.FromMilliseconds((double)schedulerInfo.TimeZoneOffset);
		}

		// Token: 0x06010422 RID: 66594 RVA: 0x003A234A File Offset: 0x003A054A
		public void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x06010423 RID: 66595 RVA: 0x003A2353 File Offset: 0x003A0553
		protected virtual void Dispose(bool disposing)
		{
			if (disposing && this._scheduler != null)
			{
				this._scheduler.Dispose();
				this._scheduler = null;
			}
		}

		// Token: 0x04004948 RID: 18760
		private SchedulerProviderBase _provider;

		// Token: 0x04004949 RID: 18761
		private AppointmentController _controller;

		// Token: 0x0400494A RID: 18762
		private RadScheduler _scheduler;

		// Token: 0x0400494B RID: 18763
		private IAppointmentFactory _appointmentFactory;

		// Token: 0x0400494C RID: 18764
		private IComparer<Appointment> _appointmentComparer;

		// Token: 0x0400494D RID: 18765
		private readonly TimeSpan maxReminderTrigger = TimeSpan.FromDays(14.0);
	}
}
