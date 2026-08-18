using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;
using System.Web.UI;

namespace Telerik.Web.UI.Scheduler.Views
{
	// Token: 0x0200082D RID: 2093
	internal abstract class SchedulerModel : ISchedulerModel
	{
		// Token: 0x17001950 RID: 6480
		// (get) Token: 0x06004D6C RID: 19820
		// (set) Token: 0x06004D6D RID: 19821
		public abstract IScheduler Owner { get; protected set; }

		// Token: 0x17001951 RID: 6481
		// (get) Token: 0x06004D6E RID: 19822
		// (set) Token: 0x06004D6F RID: 19823
		public abstract AppointmentCollection Appointments { get; protected set; }

		// Token: 0x17001952 RID: 6482
		// (get) Token: 0x06004D70 RID: 19824
		public abstract DateTime SelectedDate { get; }

		// Token: 0x17001953 RID: 6483
		// (get) Token: 0x06004D71 RID: 19825
		public abstract DateTime NextPeriodDate { get; }

		// Token: 0x17001954 RID: 6484
		// (get) Token: 0x06004D72 RID: 19826
		public abstract DateTime PreviousPeriodDate { get; }

		// Token: 0x17001955 RID: 6485
		// (get) Token: 0x06004D73 RID: 19827
		// (set) Token: 0x06004D74 RID: 19828
		public abstract DateTime VisibleRangeStart { get; protected set; }

		// Token: 0x17001956 RID: 6486
		// (get) Token: 0x06004D75 RID: 19829
		// (set) Token: 0x06004D76 RID: 19830
		public abstract DateTime VisibleRangeEnd { get; protected set; }

		// Token: 0x17001957 RID: 6487
		// (get) Token: 0x06004D77 RID: 19831
		public abstract bool ReadOnly { get; }

		// Token: 0x17001958 RID: 6488
		// (get) Token: 0x06004D78 RID: 19832 RVA: 0x000F30D2 File Offset: 0x000F12D2
		public virtual bool EnableExactTimeRendering
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001959 RID: 6489
		// (get) Token: 0x06004D79 RID: 19833
		public abstract string CssClass { get; }

		// Token: 0x06004D7A RID: 19834
		public abstract IEnumerable<ScriptReference> GetScriptReferences();

		// Token: 0x06004D7B RID: 19835
		public abstract void DataBind(AppointmentCollection appointments);

		// Token: 0x06004D7C RID: 19836
		public abstract ISchedulerRenderer GetRenderer();

		// Token: 0x06004D7D RID: 19837
		public abstract ISchedulerTimeSlot GetSlotByIndex(string index);

		// Token: 0x06004D7E RID: 19838
		public abstract ISchedulerTimeSlot GetAppointmentSlot(Appointment appointment);

		// Token: 0x06004D7F RID: 19839
		public abstract IList<ISchedulerTimeSlot> GetTimeSlots();

		// Token: 0x06004D80 RID: 19840
		public abstract void HandleInsert(ISchedulerTimeSlot targetSlot, ISchedulerTimeSlot lastSlot, Appointment appointmentToInsert);

		// Token: 0x06004D81 RID: 19841
		public abstract void HandleMove(Appointment appointment, ISchedulerTimeSlot sourceSlot, ISchedulerTimeSlot targetSlot, bool editSeries);

		// Token: 0x06004D82 RID: 19842
		public abstract void HandleResize(Appointment appointment, ISchedulerTimeSlot sourceSlot, DateTime appointmentStart, DateTime appointmentEnd, bool editSeries);

		// Token: 0x06004D83 RID: 19843 RVA: 0x000F3120 File Offset: 0x000F1320
		public virtual Dictionary<string, ContextMenuAction> GetTimeSlotContextMenuCommands()
		{
			Dictionary<string, ContextMenuAction> dictionary = new Dictionary<string, ContextMenuAction>();
			dictionary.Add("CommandAddAppointment", delegate(ISchedulerModel ActiveModel, SchedulerPostBackEvent postBack)
			{
				postBack.Command = SchedulerPostBackCommand.Insert;
				ActiveModel.ProcessPostBackCommand(postBack);
			});
			dictionary.Add("CommandAddRecurringAppointment", delegate(ISchedulerModel ActiveModel, SchedulerPostBackEvent postBack)
			{
				postBack.Command = SchedulerPostBackCommand.AdvancedInsertRecurring;
				ActiveModel.ProcessPostBackCommand(postBack);
			});
			dictionary.Add("CommandGoToToday", delegate(ISchedulerModel ActiveModel, SchedulerPostBackEvent postBack)
			{
				ActiveModel.Owner.SelectedDate = DateTime.Now.Date;
			});
			return dictionary;
		}

		// Token: 0x06004D84 RID: 19844 RVA: 0x000F31AC File Offset: 0x000F13AC
		public virtual IList<RadMenuItem> GetTimeSlotContextMenuItems()
		{
			List<RadMenuItem> list = new List<RadMenuItem>
			{
				new RadMenuItem
				{
					Text = this.Owner.Localization.ContextMenuAddAppointment,
					Value = "CommandAddAppointment"
				},
				new RadMenuItem
				{
					Text = this.Owner.Localization.ContextMenuGoToToday,
					Value = "CommandGoToToday"
				}
			};
			if (this.Owner.EnableAdvancedForm && this.Owner.RecurrenceSupport)
			{
				list.Insert(1, new RadMenuItem
				{
					Text = this.Owner.Localization.ContextMenuAddRecurringAppointment,
					Value = "CommandAddRecurringAppointment"
				});
			}
			return list;
		}

		// Token: 0x06004D85 RID: 19845 RVA: 0x000F326B File Offset: 0x000F146B
		public virtual void DescribeModelData(string propertyName, JavaScriptSerializer serializer, IScriptDescriptor descriptor)
		{
		}

		// Token: 0x06004D86 RID: 19846 RVA: 0x000F3270 File Offset: 0x000F1470
		public virtual void ProcessPostBackCommand(SchedulerPostBackEvent postBack)
		{
			Appointment appointment = this.Appointments.FindByID(postBack.AppointmentID);
			ISchedulerTimeSlot schedulerTimeSlot = null;
			if (!string.IsNullOrEmpty(postBack.SourceSlotIndex))
			{
				schedulerTimeSlot = this.GetSlotByIndex(postBack.SourceSlotIndex);
			}
			ISchedulerTimeSlot schedulerTimeSlot2 = null;
			if (!string.IsNullOrEmpty(postBack.TargetSlotIndex))
			{
				schedulerTimeSlot2 = this.GetSlotByIndex(postBack.TargetSlotIndex);
			}
			ISchedulerTimeSlot lastSlot = null;
			if (!string.IsNullOrEmpty(postBack.LastSlotIndex))
			{
				lastSlot = this.GetSlotByIndex(postBack.LastSlotIndex);
			}
			SchedulerPostBackCommand command = postBack.Command;
			switch (command)
			{
			case SchedulerPostBackCommand.Insert:
				break;
			case SchedulerPostBackCommand.InsertAppointment:
			case SchedulerPostBackCommand.Edit:
				return;
			case SchedulerPostBackCommand.Resize:
				if (!this.ReadOnly && appointment != null)
				{
					DateTime startDateParsed = postBack.StartDateParsed;
					DateTime endDateParsed = postBack.EndDateParsed;
					this.HandleResize(appointment, schedulerTimeSlot, postBack.StartDateParsed, postBack.EndDateParsed, postBack.EditSeries);
					return;
				}
				return;
			case SchedulerPostBackCommand.Move:
				if (!this.ReadOnly && appointment != null && schedulerTimeSlot != null && schedulerTimeSlot2 != null)
				{
					this.HandleMove(appointment, schedulerTimeSlot, schedulerTimeSlot2, postBack.EditSeries);
					return;
				}
				return;
			default:
				if (command != SchedulerPostBackCommand.AdvancedInsertRecurring)
				{
					return;
				}
				break;
			}
			if (!this.ReadOnly && schedulerTimeSlot2 != null)
			{
				this.Owner.ActiveSlotIndex = schedulerTimeSlot2.Index;
				Appointment appointment2 = this.Owner.CreateAppointment();
				appointment2.TimeZoneID = this.Owner.TimeZonesProvider.OperationTimeZone.TimeZoneId;
				if (postBack.Command == SchedulerPostBackCommand.AdvancedInsertRecurring)
				{
					appointment2.RecurrenceState = RecurrenceState.Master;
				}
				this.HandleInsert(schedulerTimeSlot2, lastSlot, appointment2);
				return;
			}
		}

		// Token: 0x06004D87 RID: 19847 RVA: 0x000F33D8 File Offset: 0x000F15D8
		protected virtual string CreateDefaultRecurrenceRule(Appointment appointment)
		{
			RecurrenceRange recurrenceRange = new RecurrenceRange();
			recurrenceRange.Start = appointment.Start;
			recurrenceRange.EventDuration = appointment.Duration;
			RecurrenceDay daysOfWeekMask = SchedulerModel.RecurrenceDayMap[this.Owner.UtcToDisplay(appointment.Start).DayOfWeek];
			WeeklyRecurrenceRule weeklyRecurrenceRule = new WeeklyRecurrenceRule(1, daysOfWeekMask, recurrenceRange);
			return weeklyRecurrenceRule.ToString();
		}

		// Token: 0x0400135B RID: 4955
		private static Dictionary<DayOfWeek, RecurrenceDay> RecurrenceDayMap = new Dictionary<DayOfWeek, RecurrenceDay>
		{
			{
				DayOfWeek.Sunday,
				RecurrenceDay.Sunday
			},
			{
				DayOfWeek.Monday,
				RecurrenceDay.Monday
			},
			{
				DayOfWeek.Tuesday,
				RecurrenceDay.Tuesday
			},
			{
				DayOfWeek.Wednesday,
				RecurrenceDay.Wednesday
			},
			{
				DayOfWeek.Thursday,
				RecurrenceDay.Thursday
			},
			{
				DayOfWeek.Friday,
				RecurrenceDay.Friday
			},
			{
				DayOfWeek.Saturday,
				RecurrenceDay.Saturday
			}
		};
	}
}
