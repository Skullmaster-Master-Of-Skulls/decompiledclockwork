using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Web.UI;

namespace Telerik.Web.UI.Scheduling
{
	// Token: 0x02001320 RID: 4896
	internal class AppointmentController
	{
		// Token: 0x0600CC89 RID: 52361 RVA: 0x002D910A File Offset: 0x002D730A
		public AppointmentController(RadScheduler scheduler)
		{
			this._scheduler = scheduler;
			this._originalStartTimes = new Dictionary<Appointment, DateTime>();
		}

		// Token: 0x0600CC8A RID: 52362 RVA: 0x002D9124 File Offset: 0x002D7324
		public void InsertAppointment(ISchedulerInfo schedulerInfo, Appointment appointment)
		{
			this.InsertAppointmentThroughProvider(schedulerInfo, appointment);
			this.AddAppointmentAndExpand(appointment);
		}

		// Token: 0x0600CC8B RID: 52363 RVA: 0x002D9138 File Offset: 0x002D7338
		public void UpdateAppointment(ISchedulerInfo schedulerInfo, Appointment originalAppointment, IOrderedDictionary newData)
		{
			Appointment appointment = originalAppointment.Clone();
			appointment.LoadFromDictionary(newData);
			this.UpdateAppointment(schedulerInfo, originalAppointment, appointment);
		}

		// Token: 0x0600CC8C RID: 52364 RVA: 0x002D915C File Offset: 0x002D735C
		public void UpdateAppointment(ISchedulerInfo schedulerInfo, Appointment originalAppointment, Appointment modifiedAppointment)
		{
			if (modifiedAppointment.ID == null && modifiedAppointment.RecurrenceState == RecurrenceState.Exception)
			{
				this.UpdateException(modifiedAppointment, originalAppointment, schedulerInfo, false);
				return;
			}
			this._scheduler.ProviderContext = new UpdateAppointmentContext(originalAppointment);
			int num = this._scheduler.Appointments.IndexOf(originalAppointment);
			if (num > -1)
			{
				this._scheduler.Appointments[num] = modifiedAppointment;
			}
			if (modifiedAppointment.RecurrenceState == RecurrenceState.Master)
			{
				AppointmentController.FixRecurrenceRule(originalAppointment, modifiedAppointment);
				this.ResetOccurrences(modifiedAppointment);
				if (string.IsNullOrEmpty(modifiedAppointment.RecurrenceRule))
				{
					this.DetachRecurrenceExceptions(originalAppointment);
				}
			}
			this.UpdateAppointmentThroughProvider(schedulerInfo, modifiedAppointment);
			this._scheduler.ProviderContext = null;
		}

		// Token: 0x0600CC8D RID: 52365 RVA: 0x002D9200 File Offset: 0x002D7400
		public Appointment PrepareToEdit(Appointment appointmentToEdit, bool editSeries)
		{
			if (appointmentToEdit.RecurrenceState != RecurrenceState.NotRecurring)
			{
				Appointment appointmentParent = this.GetAppointmentParent(appointmentToEdit);
				if (editSeries)
				{
					return appointmentParent;
				}
				if (appointmentToEdit.RecurrenceState != RecurrenceState.Exception)
				{
					Appointment appointment = appointmentToEdit.Clone();
					appointment.Owner = this._scheduler;
					appointment.ID = null;
					appointment.RecurrenceParentID = appointmentParent.ID;
					appointment.RecurrenceRule = string.Empty;
					appointment.RecurrenceState = RecurrenceState.Exception;
					this._originalStartTimes[appointment] = appointment.Start;
					return appointment;
				}
			}
			return appointmentToEdit;
		}

		// Token: 0x0600CC8E RID: 52366 RVA: 0x002D9278 File Offset: 0x002D7478
		public void DeleteAppointment(ISchedulerInfo schedulerInfo, Appointment appointmentToDelete, bool deleteSeries)
		{
			if (appointmentToDelete == null)
			{
				throw new ArgumentException("Cannot find the appointment to delete in the Appointments collection.");
			}
			List<Appointment> list = new List<Appointment>();
			if (appointmentToDelete.RecurrenceState != RecurrenceState.NotRecurring)
			{
				Appointment appointmentParent = this.GetAppointmentParent(appointmentToDelete);
				if (appointmentParent == null)
				{
					throw new InvalidOperationException(string.Format("Cannot locate the parent of appointment with ID = '{0}'. Ensure that the parent appointment with ID = '{1}' exists and is loaded.", appointmentToDelete.ID, appointmentToDelete.RecurrenceParentID));
				}
				if (deleteSeries)
				{
					foreach (Appointment item in this._scheduler.Appointments.FindByRecurrenceParentID(appointmentParent.ID))
					{
						list.Add(item);
					}
					list.Add(appointmentParent);
				}
				else
				{
					RecurrenceRule recurrenceRule;
					if (!RecurrenceRule.TryParse(appointmentParent.RecurrenceRule, out recurrenceRule))
					{
						string message = string.Format("Cannot parse parentAppointment recurrence rule during delete operation. Appointment ID is {0}, parentAppointment ID is {1}.", appointmentToDelete.ID, appointmentParent.ID);
						throw new InvalidOperationException(message);
					}
					if (appointmentToDelete.RecurrenceState == RecurrenceState.Master || appointmentToDelete.RecurrenceState == RecurrenceState.Occurrence)
					{
						if (!this._scheduler.OnOccurrenceDelete(appointmentParent, appointmentToDelete))
						{
							return;
						}
						Appointment originalAppointment = appointmentParent.Clone();
						recurrenceRule.Exceptions.Add(appointmentToDelete.Start);
						appointmentParent.RecurrenceRule = recurrenceRule.ToString();
						AppointmentUpdateEventArgs appointmentUpdateEventArgs = new AppointmentUpdateEventArgs(originalAppointment, appointmentParent, schedulerInfo);
						if (!this._scheduler.OnAppointmentUpdate(appointmentUpdateEventArgs))
						{
							return;
						}
						this._scheduler.ProviderContext = new UpdateAppointmentContext(originalAppointment);
						this.UpdateAppointmentThroughProvider(appointmentUpdateEventArgs.SchedulerInfo, appointmentParent);
						this.ResetOccurrences(appointmentParent);
					}
					else
					{
						list.Add(appointmentToDelete);
					}
					int num = 0;
					foreach (Appointment item2 in this._scheduler.Appointments.FindByRecurrenceParentID(appointmentParent.ID, RecurrenceState.Exception))
					{
						if (!list.Contains(item2))
						{
							num++;
						}
					}
					if (!recurrenceRule.HasOccurrences && 0 >= num)
					{
						list.Add(appointmentParent);
					}
				}
			}
			else
			{
				list.Add(appointmentToDelete);
			}
			foreach (Appointment appointment in list)
			{
				bool flag = false;
				if (appointment.RecurrenceState != RecurrenceState.Occurrence)
				{
					if (appointment.ID == null)
					{
						throw new InvalidOperationException("Delete operation failed. An attempt to delete a appointment with an empty ID has occurred. Any dynamically created appointments (such as recurrence exceptions) must be round-tripped to the datasource in order to be deleted from it.");
					}
					AppointmentDeleteEventArgs appointmentDeleteEventArgs = new AppointmentDeleteEventArgs(appointment, schedulerInfo);
					if (this._scheduler.OnAppointmentDelete(appointmentDeleteEventArgs))
					{
						this.DeleteAppointmentThroughProvider(appointmentDeleteEventArgs.SchedulerInfo, appointment);
					}
					else
					{
						flag = true;
					}
				}
				if (!flag)
				{
					this.SafelyRemoveAppointmentFromScheduler(appointment);
				}
			}
		}

		// Token: 0x0600CC8F RID: 52367 RVA: 0x002D9500 File Offset: 0x002D7700
		public void AddAppointmentAndExpand(Appointment appointment)
		{
			this.AddAppointmentAndExpand(appointment, this._scheduler.VisibleRangeStart, this._scheduler.VisibleRangeEnd);
		}

		// Token: 0x0600CC90 RID: 52368 RVA: 0x002D9520 File Offset: 0x002D7720
		public void AddAppointmentAndExpand(Appointment appointment, DateTime rangeStart, DateTime rangeEnd)
		{
			appointment.Owner = this._scheduler;
			this._scheduler.Appointments.Add(appointment);
			this._scheduler.OnAppointmentDataBound(appointment);
			Appointment[] array = this.ExpandRecurrence(appointment, rangeStart, rangeEnd);
			foreach (Appointment appointment2 in array)
			{
				this._scheduler.Appointments.Add(appointment2);
				this._scheduler.OnAppointmentDataBound(appointment2);
			}
		}

		// Token: 0x0600CC91 RID: 52369 RVA: 0x002D9594 File Offset: 0x002D7794
		public void RemoveRecurrenceExceptions(ISchedulerInfo schedulerInfo, Appointment master)
		{
			if (master.RecurrenceState != RecurrenceState.Master)
			{
				return;
			}
			this._scheduler.ProviderContext = new RemoveRecurrenceExceptionsContext();
			List<Appointment> list = new List<Appointment>();
			RecurrenceRule recurrenceRule;
			if (RecurrenceRule.TryParse(master.RecurrenceRule, out recurrenceRule))
			{
				if (recurrenceRule.HasExceptions)
				{
					Appointment appointment = master.Clone();
					recurrenceRule.Exceptions.Clear();
					appointment.RecurrenceRule = recurrenceRule.ToString();
					AppointmentUpdateEventArgs appointmentUpdateEventArgs = new AppointmentUpdateEventArgs(master, appointment, schedulerInfo);
					if (this._scheduler.OnAppointmentUpdate(appointmentUpdateEventArgs))
					{
						master.RecurrenceRule = appointment.RecurrenceRule;
						this.UpdateAppointmentThroughProvider(appointmentUpdateEventArgs.SchedulerInfo, appointment);
					}
				}
				foreach (Appointment appointment2 in this._scheduler.Appointments.FindByRecurrenceParentID(master.ID))
				{
					if (appointment2.RecurrenceState == RecurrenceState.Exception)
					{
						list.Add(appointment2);
					}
				}
			}
			foreach (Appointment appointmentToDelete in list)
			{
				this.DeleteAppointment(schedulerInfo, appointmentToDelete, false);
			}
			this.ResetOccurrences(master);
			this._scheduler.ProviderContext = null;
		}

		// Token: 0x0600CC92 RID: 52370 RVA: 0x002D96E0 File Offset: 0x002D78E0
		public static void FixRecurrenceRule(Appointment originalAppointment, Appointment appointmentToUpdate)
		{
			RecurrenceRule recurrenceRule;
			if (RecurrenceRule.TryParse(appointmentToUpdate.RecurrenceRule, out recurrenceRule))
			{
				recurrenceRule.Range.Start = appointmentToUpdate.Start;
				recurrenceRule.Range.EventDuration = appointmentToUpdate.End - appointmentToUpdate.Start;
				TimeSpan value = appointmentToUpdate.Start - originalAppointment.Start;
				for (int i = 0; i < recurrenceRule.Exceptions.Count; i++)
				{
					recurrenceRule.Exceptions[i] = recurrenceRule.Exceptions[i].Add(value);
				}
				appointmentToUpdate.RecurrenceRule = recurrenceRule.ToString();
			}
		}

		// Token: 0x0600CC93 RID: 52371 RVA: 0x002D9781 File Offset: 0x002D7981
		internal void DismissAppointmentReminder(ISchedulerInfo schedulerInfo, Appointment appointmentToUpdate, Appointment originalAppointment)
		{
			if (appointmentToUpdate.ID == null && appointmentToUpdate.RecurrenceState == RecurrenceState.Exception)
			{
				this.UpdateException(appointmentToUpdate, originalAppointment, schedulerInfo, true);
				return;
			}
			this.UpdateAppointment(schedulerInfo, originalAppointment, appointmentToUpdate);
		}

		// Token: 0x0600CC94 RID: 52372 RVA: 0x002D97A8 File Offset: 0x002D79A8
		private void UpdateException(Appointment modifiedAppointment, Appointment originalAppointment, ISchedulerInfo schedulerInfo, bool getParentThroughProvider)
		{
			Appointment appointment = originalAppointment.Clone();
			appointment.RecurrenceState = RecurrenceState.Occurrence;
			Appointment appointmentParent = this.GetAppointmentParent(modifiedAppointment, getParentThroughProvider);
			if (this._scheduler.OnRecurrenceExceptionCreated(appointmentParent, modifiedAppointment, appointment))
			{
				DateTime dateTime = originalAppointment.Start;
				if (this._originalStartTimes.ContainsKey(originalAppointment))
				{
					dateTime = this._originalStartTimes[originalAppointment];
				}
				this._scheduler.ProviderContext = new CreateRecurrenceExceptionContext(dateTime, appointmentParent);
				this.AttachRecurrenceException(appointmentParent, modifiedAppointment, dateTime, schedulerInfo);
				this._scheduler.ProviderContext = null;
			}
		}

		// Token: 0x0600CC95 RID: 52373 RVA: 0x002D9828 File Offset: 0x002D7A28
		private void AttachRecurrenceException(Appointment parent, Appointment exceptionAppointment, DateTime explicitExceptionDate, ISchedulerInfo schedulerInfo)
		{
			RecurrenceRule recurrenceRule;
			RecurrenceRule.TryParse(parent.RecurrenceRule, out recurrenceRule);
			recurrenceRule.Exceptions.Add(explicitExceptionDate);
			Appointment appointment = parent.Clone();
			appointment.RecurrenceRule = recurrenceRule.ToString();
			AppointmentUpdateEventArgs appointmentUpdateEventArgs = new AppointmentUpdateEventArgs(parent, appointment, schedulerInfo);
			if (this._scheduler.OnAppointmentUpdate(appointmentUpdateEventArgs))
			{
				this.UpdateAppointmentThroughProvider(appointmentUpdateEventArgs.SchedulerInfo, appointment);
				exceptionAppointment.Owner = this._scheduler;
				if (!string.IsNullOrEmpty(exceptionAppointment.RecurrenceRule))
				{
					exceptionAppointment.RecurrenceState = RecurrenceState.Master;
					exceptionAppointment.RecurrenceParentID = null;
					this.ResetOccurrences(exceptionAppointment);
				}
				AppointmentInsertEventArgs appointmentInsertEventArgs = new AppointmentInsertEventArgs(exceptionAppointment, schedulerInfo);
				if (this._scheduler.OnAppointmentInsert(appointmentInsertEventArgs))
				{
					this._scheduler.Provider.LegacyOwner = this._scheduler;
					this._scheduler.Provider.Insert(appointmentInsertEventArgs.SchedulerInfo, exceptionAppointment);
					this._scheduler.Appointments.Add(exceptionAppointment);
				}
				this.ResetOccurrences(appointment);
			}
		}

		// Token: 0x0600CC96 RID: 52374 RVA: 0x002D9914 File Offset: 0x002D7B14
		private void DetachRecurrenceExceptions(Appointment recurrenceMaster)
		{
			foreach (Appointment appointment in this._scheduler.Appointments.FindByRecurrenceParentID(recurrenceMaster.ID))
			{
				if (appointment.RecurrenceState == RecurrenceState.Exception)
				{
					Appointment appointment2 = appointment.Clone();
					appointment2.RecurrenceParentID = null;
					appointment2.RecurrenceState = RecurrenceState.NotRecurring;
					AppointmentUpdateEventArgs appointmentUpdateEventArgs = new AppointmentUpdateEventArgs(appointment, appointment2, new SchedulerInfo(this._scheduler));
					if (this._scheduler.OnAppointmentUpdate(appointmentUpdateEventArgs))
					{
						this.UpdateAppointmentThroughProvider(appointmentUpdateEventArgs.SchedulerInfo, appointment2);
						appointment.Owner = this._scheduler;
					}
				}
			}
		}

		// Token: 0x0600CC97 RID: 52375 RVA: 0x002D99C4 File Offset: 0x002D7BC4
		private Appointment GetAppointmentParent(Appointment appointment)
		{
			return this.GetAppointmentParent(appointment, false);
		}

		// Token: 0x0600CC98 RID: 52376 RVA: 0x002D99D0 File Offset: 0x002D7BD0
		private Appointment GetAppointmentParent(Appointment appointment, bool getParentThroughProvider)
		{
			if (appointment.RecurrenceParentID == null)
			{
				return appointment;
			}
			if (!getParentThroughProvider)
			{
				return this._scheduler.Appointments.FindByID(appointment.RecurrenceParentID);
			}
			this._scheduler.Provider.LegacyOwner = this._scheduler;
			IEnumerable<Appointment> appointments = this._scheduler.Provider.GetAppointments(new SchedulerInfo(this._scheduler));
			foreach (Appointment appointment2 in appointments)
			{
				if (appointment2.ID != null && appointment2.ID.Equals(appointment.RecurrenceParentID))
				{
					return appointment2;
				}
			}
			return null;
		}

		// Token: 0x0600CC99 RID: 52377 RVA: 0x002D9A8C File Offset: 0x002D7C8C
		private void InsertAppointmentThroughProvider(ISchedulerInfo schedulerInfo, Appointment appointment)
		{
			this._scheduler.Provider.LegacyOwner = this._scheduler;
			appointment.Validate();
			this._scheduler.Provider.Insert(schedulerInfo, appointment);
		}

		// Token: 0x0600CC9A RID: 52378 RVA: 0x002D9ABC File Offset: 0x002D7CBC
		private void UpdateAppointmentThroughProvider(ISchedulerInfo schedulerInfo, Appointment appointmentToUpdate)
		{
			this._scheduler.Provider.LegacyOwner = this._scheduler;
			appointmentToUpdate.Validate();
			this._scheduler.Provider.Update(schedulerInfo, appointmentToUpdate);
		}

		// Token: 0x0600CC9B RID: 52379 RVA: 0x002D9AEC File Offset: 0x002D7CEC
		private void DeleteAppointmentThroughProvider(ISchedulerInfo schedulerInfo, Appointment appointmentToDelete)
		{
			DataSourceView dataSourceView = this._scheduler.DataSourceView;
			if (appointmentToDelete.ID != null)
			{
				this._scheduler.Provider.LegacyOwner = this._scheduler;
				this._scheduler.Provider.Delete(schedulerInfo, appointmentToDelete);
				return;
			}
			if (dataSourceView != null)
			{
				throw new InvalidOperationException("Delete operation failed. An attempt to delete a appointment with an empty ID has occurred. Any dynamically created appointments (such as recurrence exceptions) must be round-tripped to the datasource in order to be deleted from it.");
			}
		}

		// Token: 0x0600CC9C RID: 52380 RVA: 0x002D9B44 File Offset: 0x002D7D44
		private void SafelyRemoveAppointmentFromScheduler(Appointment appointmentToDelete)
		{
			if (this._scheduler.Appointments.Contains(appointmentToDelete))
			{
				this._scheduler.Appointments.Remove(appointmentToDelete);
			}
			Appointment appointment = this._scheduler.Appointments.FindByID(appointmentToDelete.ID);
			if (appointment != null)
			{
				this._scheduler.Appointments.Remove(appointment);
			}
		}

		// Token: 0x0600CC9D RID: 52381 RVA: 0x002D9BA0 File Offset: 0x002D7DA0
		private void ResetOccurrences(Appointment parent)
		{
			List<Appointment> list = new List<Appointment>();
			foreach (Appointment appointment in this._scheduler.Appointments.FindByRecurrenceParentID(parent.ID))
			{
				if (appointment.RecurrenceState == RecurrenceState.Occurrence)
				{
					list.Add(appointment);
				}
			}
			foreach (Appointment apt in list)
			{
				this._scheduler.Appointments.Remove(apt);
			}
			this.SafelyRemoveAppointmentFromScheduler(parent);
			parent.Owner = this._scheduler;
			this._scheduler.Appointments.Add(parent);
			this._scheduler.Appointments.AddRange(this.ExpandRecurrence(parent));
		}

		// Token: 0x0600CC9E RID: 52382 RVA: 0x002D9C90 File Offset: 0x002D7E90
		private Appointment[] ExpandRecurrence(Appointment parent)
		{
			return this.ExpandRecurrence(parent, this._scheduler.VisibleRangeStart, this._scheduler.VisibleRangeEnd);
		}

		// Token: 0x0600CC9F RID: 52383 RVA: 0x002D9CB0 File Offset: 0x002D7EB0
		private Appointment[] ExpandRecurrence(Appointment parent, DateTime rangeStart, DateTime rangeEnd)
		{
			List<Appointment> list = new List<Appointment>();
			RecurrenceRule recurrenceRule;
			if (RecurrenceRule.TryParse(parent.RecurrenceRule, parent.TimeZoneID, out recurrenceRule))
			{
				bool visible = parent.Visible;
				parent.Visible = false;
				DateTime d = TimeZoneInfoProvider.UtcToLocal(rangeStart, TimeZoneInfoProvider.GetTimeZoneModelById(parent.TimeZoneID));
				DateTime end = TimeZoneInfoProvider.UtcToLocal(rangeEnd, TimeZoneInfoProvider.GetTimeZoneModelById(parent.TimeZoneID));
				recurrenceRule.SetEffectiveRange(d - parent.Duration, end);
				recurrenceRule.MaximumCandidates = this._scheduler.MaximumRecurrenceCandidates;
				DateTime startLocal = parent.StartLocal;
				int num = 0;
				foreach (DateTime dateTime in recurrenceRule.Occurrences)
				{
					if (dateTime == startLocal)
					{
						parent.Visible = visible;
					}
					else
					{
						Appointment appointment = parent.Clone();
						appointment.Owner = this._scheduler;
						appointment.ID = string.Format("{0}_{1}", parent.ID, num++);
						appointment.RecurrenceParentID = parent.ID;
						appointment.RecurrenceState = RecurrenceState.Occurrence;
						appointment.RecurrenceRule = null;
						appointment.TimeZoneID = parent.TimeZoneID;
						appointment.StartLocal = dateTime;
						appointment.EndLocal = dateTime.Add(parent.EndLocal - parent.StartLocal);
						appointment.Visible = true;
						list.Add(appointment);
					}
				}
			}
			return list.ToArray();
		}

		// Token: 0x0400368A RID: 13962
		private readonly RadScheduler _scheduler;

		// Token: 0x0400368B RID: 13963
		private readonly Dictionary<Appointment, DateTime> _originalStartTimes;
	}
}
