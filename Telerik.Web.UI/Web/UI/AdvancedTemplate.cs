using System;
using System.Collections;
using System.Collections.Specialized;
using System.Diagnostics.CodeAnalysis;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI.SchedulerAdvancedTemplate;

namespace Telerik.Web.UI
{
	// Token: 0x0200080C RID: 2060
	internal abstract class AdvancedTemplate : IBindableTemplate, ITemplate, IDisposable
	{
		// Token: 0x170018A1 RID: 6305
		// (get) Token: 0x06004B53 RID: 19283 RVA: 0x000EBA3E File Offset: 0x000E9C3E
		// (set) Token: 0x06004B54 RID: 19284 RVA: 0x000EBA46 File Offset: 0x000E9C46
		internal IAdvancedTemplateView View { get; set; }

		// Token: 0x170018A2 RID: 6306
		// (get) Token: 0x06004B55 RID: 19285 RVA: 0x000EBA4F File Offset: 0x000E9C4F
		// (set) Token: 0x06004B56 RID: 19286 RVA: 0x000EBA57 File Offset: 0x000E9C57
		internal IAdvancedTemplateRenderer Renderer { get; set; }

		// Token: 0x170018A3 RID: 6307
		// (get) Token: 0x06004B57 RID: 19287 RVA: 0x000EBA60 File Offset: 0x000E9C60
		// (set) Token: 0x06004B58 RID: 19288 RVA: 0x000EBA68 File Offset: 0x000E9C68
		public RadScheduler Owner { get; private set; }

		// Token: 0x170018A4 RID: 6308
		// (get) Token: 0x06004B59 RID: 19289 RVA: 0x000EBA71 File Offset: 0x000E9C71
		// (set) Token: 0x06004B5A RID: 19290 RVA: 0x000EBA79 File Offset: 0x000E9C79
		internal Appointment Appointment { get; private set; }

		// Token: 0x170018A5 RID: 6309
		// (get) Token: 0x06004B5B RID: 19291 RVA: 0x000EBA82 File Offset: 0x000E9C82
		protected bool HasTimeZoneOffset
		{
			get
			{
				return !this.Owner.TimeZonesEnabled && this.Owner.TimeZoneOffset != TimeSpan.Zero;
			}
		}

		// Token: 0x170018A6 RID: 6310
		// (get) Token: 0x06004B5C RID: 19292 RVA: 0x000EBAA8 File Offset: 0x000E9CA8
		internal DateTime Start
		{
			get
			{
				DateTime dateTime;
				if (this.View.AllDayEvent.Checked)
				{
					dateTime = this.View.StartDateValue.Date;
				}
				else
				{
					dateTime = this.View.StartDateValue.Date.Add(this.View.StartTimeValue);
				}
				if (this.HasTimeZoneOffset)
				{
					return this.Owner.DisplayToUtc(dateTime);
				}
				return TimeZoneInfoProvider.LocalToUtc(dateTime, TimeZoneInfoProvider.GetTimeZoneModelById(this.View.SelectedTimeZone));
			}
		}

		// Token: 0x170018A7 RID: 6311
		// (get) Token: 0x06004B5D RID: 19293 RVA: 0x000EBB30 File Offset: 0x000E9D30
		internal DateTime End
		{
			get
			{
				DateTime dateTime;
				if (this.View.AllDayEvent.Checked)
				{
					dateTime = this.View.EndDateValue.Date.AddDays(1.0);
				}
				else
				{
					dateTime = this.View.EndDateValue.Date.Add(this.View.EndTimeValue);
				}
				if (this.HasTimeZoneOffset)
				{
					return this.Owner.DisplayToUtc(dateTime);
				}
				return TimeZoneInfoProvider.LocalToUtc(dateTime, TimeZoneInfoProvider.GetTimeZoneModelById(this.View.SelectedTimeZone));
			}
		}

		// Token: 0x170018A8 RID: 6312
		// (get) Token: 0x06004B5E RID: 19294 RVA: 0x000EBBCC File Offset: 0x000E9DCC
		// (set) Token: 0x06004B5F RID: 19295 RVA: 0x000EBC50 File Offset: 0x000E9E50
		protected string RecurrenceRuleText
		{
			get
			{
				if (!this.Owner.RecurrenceSupport)
				{
					return string.Empty;
				}
				this._recurrenceEditor.StartDate = this.Start;
				this._recurrenceEditor.EndDate = this.End;
				RecurrenceRule recurrenceRule = this._recurrenceEditor.RecurrenceRule;
				if (recurrenceRule == null)
				{
					return string.Empty;
				}
				RecurrenceRule recurrenceRule2;
				if (RecurrenceRule.TryParse(this._originalRecurrenceRule.Value, out recurrenceRule2))
				{
					recurrenceRule.Exceptions = recurrenceRule2.Exceptions;
				}
				return recurrenceRule.ToString();
			}
			set
			{
				RecurrenceRule recurrenceRule;
				RecurrenceRule.TryParse(value, out recurrenceRule);
				if (recurrenceRule != null)
				{
					this._recurrenceEditor.RecurrenceRule = recurrenceRule;
					this._originalRecurrenceRule.Value = value;
				}
			}
		}

		// Token: 0x170018A9 RID: 6313
		// (get) Token: 0x06004B60 RID: 19296 RVA: 0x000EBC87 File Offset: 0x000E9E87
		public RenderMode ResolvedRenderMode
		{
			get
			{
				return this.Owner.ResolvedRenderMode;
			}
		}

		// Token: 0x06004B61 RID: 19297 RVA: 0x000EBC94 File Offset: 0x000E9E94
		protected AdvancedTemplate(RadScheduler owner, string runtimeSkin)
		{
			this.Owner = owner;
			this._runtimeSkin = runtimeSkin;
		}

		// Token: 0x06004B62 RID: 19298 RVA: 0x000EBCAA File Offset: 0x000E9EAA
		public void InstantiateIn(Control container)
		{
			this.Appointment = ((SchedulerFormContainer)container).Appointment;
			this.CreateView();
			this.CreateRenderer();
			this.CreateLayout(container);
			this.CreateControls(container);
			this.CreateChildControls(container);
			this.CreateButtons();
		}

		// Token: 0x06004B63 RID: 19299 RVA: 0x000EBCE4 File Offset: 0x000E9EE4
		private void CreateView()
		{
			this.View = new ViewFactory(this).CreateView();
		}

		// Token: 0x06004B64 RID: 19300 RVA: 0x000EBCF7 File Offset: 0x000E9EF7
		private void CreateRenderer()
		{
			this.Renderer = new RendererFactory(this).CreateRenderer();
		}

		// Token: 0x06004B65 RID: 19301 RVA: 0x000EBD0A File Offset: 0x000E9F0A
		protected virtual void CreateLayout(Control container)
		{
			this.Renderer.CreateLayout(container);
		}

		// Token: 0x06004B66 RID: 19302 RVA: 0x000EBD18 File Offset: 0x000E9F18
		protected virtual void CreateControls(Control container)
		{
			this.View.CreateControls();
			this.Renderer.CreateControls(container);
			this.CreateRecurrenceEditorControl();
		}

		// Token: 0x06004B67 RID: 19303
		protected abstract void CreateChildControls(Control container);

		// Token: 0x06004B68 RID: 19304
		protected abstract void CreateButtons();

		// Token: 0x06004B69 RID: 19305
		internal abstract bool IncludeResource(Resource res);

		// Token: 0x06004B6A RID: 19306 RVA: 0x000EBD38 File Offset: 0x000E9F38
		protected void CreateRecurrenceEditorControl()
		{
			this._recurrenceEditor = new IntegratedRecurrenceEditor
			{
				ID = "RecurrenceEditor",
				SharedCalendar = this.View.SharedCalendar,
				RenderMode = this.Owner.ResolvedRenderMode
			};
			this._recurrenceEditor.EnableEmbeddedSkins = this.Owner.EnableEmbeddedSkins;
			this._recurrenceEditor.EnableEmbeddedScripts = this.Owner.EnableEmbeddedScripts;
			this._recurrenceEditor.DataBinding += this.RecurrenceEditor_DataBinding;
			this._recurrenceEditor.Culture = this.Owner.Culture;
			this._recurrenceEditor.Skin = this._runtimeSkin;
			this._recurrenceEditor.DateFormat = this.Owner.AdvancedForm.DateFormat;
			this._recurrenceEditor.StartDate = (this.HasTimeZoneOffset ? this.Appointment.Start : this.Appointment.StartLocal);
			this._recurrenceEditor.EndDate = (this.HasTimeZoneOffset ? this.Appointment.End : this.Appointment.EndLocal);
			this._recurrenceEditor.ZIndex = this.Owner.AdvancedForm.ZIndex + 100;
			this._recurrenceEditor.FirstDayOfWeek = this.Owner.FirstDayOfWeek;
			if (this.Owner.EnableRecurrenceSupport)
			{
				this.Renderer.OptionsPanelScroll.Controls.Add(this._recurrenceEditor);
			}
			this.ApplyOwnerLocalizationToRecuranceEditor();
			this._originalRecurrenceRule = new HiddenField
			{
				ID = "_originalRule"
			};
			this.Renderer.OptionsPanelScroll.Controls.Add(this._originalRecurrenceRule);
		}

		// Token: 0x06004B6B RID: 19307 RVA: 0x000EBEEF File Offset: 0x000EA0EF
		private void RecurrenceEditor_DataBinding(object sender, EventArgs e)
		{
			this.RecurrenceRuleText = this.Appointment.RecurrenceRule;
		}

		// Token: 0x06004B6C RID: 19308 RVA: 0x000EBF04 File Offset: 0x000EA104
		private void ApplyOwnerLocalizationToRecuranceEditor()
		{
			this._recurrenceEditor.Localization.CalendarCancel = this.Owner.Localization.AdvancedCalendarCancel;
			this._recurrenceEditor.Localization.CalendarOK = this.Owner.Localization.AdvancedCalendarOK;
			this._recurrenceEditor.Localization.CalendarToday = this.Owner.Localization.AdvancedCalendarToday;
			this._recurrenceEditor.Localization.Daily = this.Owner.Localization.AdvancedDaily;
			this._recurrenceEditor.Localization.Day = this.Owner.Localization.AdvancedDay;
			this._recurrenceEditor.Localization.Days = this.Owner.Localization.AdvancedDays;
			this._recurrenceEditor.Localization.EndAfter = this.Owner.Localization.AdvancedEndAfter;
			this._recurrenceEditor.Localization.EndByThisDate = this.Owner.Localization.AdvancedEndByThisDate;
			this._recurrenceEditor.Localization.Every = this.Owner.Localization.AdvancedEvery;
			this._recurrenceEditor.Localization.EveryWeekday = this.Owner.Localization.AdvancedEveryWeekday;
			this._recurrenceEditor.Localization.First = this.Owner.Localization.AdvancedFirst;
			this._recurrenceEditor.Localization.Fourth = this.Owner.Localization.AdvancedFourth;
			this._recurrenceEditor.Localization.Hourly = this.Owner.Localization.AdvancedHourly;
			this._recurrenceEditor.Localization.Hours = this.Owner.Localization.AdvancedHours;
			this._recurrenceEditor.Localization.Last = this.Owner.Localization.AdvancedLast;
			this._recurrenceEditor.Localization.MaskDay = this.Owner.Localization.AdvancedMaskDay;
			this._recurrenceEditor.Localization.MaskWeekday = this.Owner.Localization.AdvancedMaskWeekday;
			this._recurrenceEditor.Localization.MaskWeekendDay = this.Owner.Localization.AdvancedMaskWeekendDay;
			this._recurrenceEditor.Localization.Monthly = this.Owner.Localization.AdvancedMonthly;
			this._recurrenceEditor.Localization.Months = this.Owner.Localization.AdvancedMonths;
			this._recurrenceEditor.Localization.Never = this.Owner.Localization.AdvancedNever;
			this._recurrenceEditor.Localization.NoEndDate = this.Owner.Localization.AdvancedNoEndDate;
			this._recurrenceEditor.Localization.Occurrences = this.Owner.Localization.AdvancedOccurrences;
			this._recurrenceEditor.Localization.Of = this.Owner.Localization.AdvancedOf;
			this._recurrenceEditor.Localization.OfEvery = this.Owner.Localization.AdvancedOfEvery;
			this._recurrenceEditor.Localization.RecurEvery = this.Owner.Localization.AdvancedRecurEvery;
			this._recurrenceEditor.Localization.Recurrence = this.Owner.Localization.AdvancedRecurrence;
			this._recurrenceEditor.Localization.Second = this.Owner.Localization.AdvancedSecond;
			this._recurrenceEditor.Localization.The = this.Owner.Localization.AdvancedThe;
			this._recurrenceEditor.Localization.Third = this.Owner.Localization.AdvancedThird;
			this._recurrenceEditor.Localization.Weekly = this.Owner.Localization.AdvancedWeekly;
			this._recurrenceEditor.Localization.Weeks = this.Owner.Localization.AdvancedWeeks;
			this._recurrenceEditor.Localization.Yearly = this.Owner.Localization.AdvancedYearly;
		}

		// Token: 0x06004B6D RID: 19309 RVA: 0x000EC334 File Offset: 0x000EA534
		internal bool IsAllDayAppointment(Appointment appointment)
		{
			DateTime dateTime = this.HasTimeZoneOffset ? this.Owner.UtcToDisplay(appointment.Start) : appointment.StartLocal;
			DateTime value = this.HasTimeZoneOffset ? this.Owner.UtcToDisplay(appointment.End) : appointment.EndLocal;
			return dateTime.CompareTo(dateTime.Date) == 0 && value.CompareTo(value.Date) == 0 && dateTime.CompareTo(value) != 0;
		}

		// Token: 0x06004B6E RID: 19310 RVA: 0x000EC3B8 File Offset: 0x000EA5B8
		public virtual IOrderedDictionary ExtractValues(Control container)
		{
			OrderedDictionary orderedDictionary = new OrderedDictionary();
			orderedDictionary["Subject"] = this.View.SubjectText;
			orderedDictionary["Start"] = this.Start;
			orderedDictionary["End"] = this.End;
			orderedDictionary["TimeZoneID"] = this.View.SelectedTimeZone;
			if (this.Owner.RemindersSupport)
			{
				this.ExtractReminderValue(orderedDictionary);
			}
			if (this.Owner.HasDescriptionField)
			{
				orderedDictionary["$$Description$$"] = this.View.DescriptionText;
			}
			if (this.Owner.RecurrenceSupport)
			{
				orderedDictionary["RecurrenceRule"] = this.RecurrenceRuleText;
			}
			this.View.ExtractResourceValues(orderedDictionary);
			this.View.ExtractAttributeValues(orderedDictionary);
			return orderedDictionary;
		}

		// Token: 0x06004B6F RID: 19311 RVA: 0x000EC494 File Offset: 0x000EA694
		private void ExtractReminderValue(IDictionary values)
		{
			Appointment appointment = this.Appointment.Clone();
			if (!string.IsNullOrEmpty(this.View.SelectedReminder))
			{
				int num = int.Parse(this.View.SelectedReminder);
				if (appointment.Reminders.Count > 0)
				{
					appointment.Reminders[0].Trigger = TimeSpan.FromMinutes((double)num);
				}
				else
				{
					appointment.Reminders.Add(new Reminder(num));
				}
			}
			else if (appointment.Reminders.Count > 0)
			{
				appointment.Reminders.RemoveAt(0);
			}
			values["$$Reminders$$"] = appointment.Reminders.ToString();
		}

		// Token: 0x06004B70 RID: 19312 RVA: 0x000EC53B File Offset: 0x000EA73B
		[SuppressMessage("Microsoft.Usage", "CA2213:DisposableFieldsShouldBeDisposed")]
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06004B71 RID: 19313 RVA: 0x000EC54A File Offset: 0x000EA74A
		[SuppressMessage("Microsoft.Usage", "CA2213:DisposableFieldsShouldBeDisposed")]
		protected virtual void Dispose(bool disposing)
		{
			if (disposing && this.Appointment != null)
			{
				this.Appointment.Dispose();
			}
		}

		// Token: 0x0400130E RID: 4878
		private const int RecurrenceEditorZIndexStep = 100;

		// Token: 0x0400130F RID: 4879
		internal readonly string _runtimeSkin;

		// Token: 0x04001310 RID: 4880
		protected HiddenField _originalRecurrenceRule;

		// Token: 0x04001311 RID: 4881
		protected RecurrenceEditor _recurrenceEditor;
	}
}
