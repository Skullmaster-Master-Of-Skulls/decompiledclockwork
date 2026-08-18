using System;
using System.Collections;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI.SchedulerAdvancedTemplate
{
	// Token: 0x02000816 RID: 2070
	internal abstract class ViewBase : IAdvancedTemplateView
	{
		// Token: 0x170018DA RID: 6362
		// (get) Token: 0x06004C12 RID: 19474 RVA: 0x000EDE61 File Offset: 0x000EC061
		// (set) Token: 0x06004C13 RID: 19475 RVA: 0x000EDE69 File Offset: 0x000EC069
		public AdvancedTemplate Owner
		{
			get
			{
				return this._owner;
			}
			protected set
			{
				this._owner = value;
			}
		}

		// Token: 0x170018DB RID: 6363
		// (get) Token: 0x06004C14 RID: 19476 RVA: 0x000EDE72 File Offset: 0x000EC072
		// (set) Token: 0x06004C15 RID: 19477 RVA: 0x000EDE7A File Offset: 0x000EC07A
		public Appointment Appointment { get; set; }

		// Token: 0x170018DC RID: 6364
		// (get) Token: 0x06004C16 RID: 19478 RVA: 0x000EDE83 File Offset: 0x000EC083
		// (set) Token: 0x06004C17 RID: 19479 RVA: 0x000EDE8B File Offset: 0x000EC08B
		public SchedulerStrings Localization { get; set; }

		// Token: 0x170018DD RID: 6365
		// (get) Token: 0x06004C18 RID: 19480 RVA: 0x000EDE94 File Offset: 0x000EC094
		// (set) Token: 0x06004C19 RID: 19481 RVA: 0x000EDE9C File Offset: 0x000EC09C
		public RadScheduler ParentScheduler { get; set; }

		// Token: 0x170018DE RID: 6366
		// (get) Token: 0x06004C1A RID: 19482 RVA: 0x000EDEA5 File Offset: 0x000EC0A5
		protected bool HasTimeZoneOffset
		{
			get
			{
				return !this.ParentScheduler.TimeZonesEnabled && this.ParentScheduler.TimeZoneOffset != TimeSpan.Zero;
			}
		}

		// Token: 0x170018DF RID: 6367
		// (get) Token: 0x06004C1B RID: 19483 RVA: 0x000EDECB File Offset: 0x000EC0CB
		// (set) Token: 0x06004C1C RID: 19484 RVA: 0x000EDED3 File Offset: 0x000EC0D3
		public RadCalendar SharedCalendar { get; set; }

		// Token: 0x170018E0 RID: 6368
		// (get) Token: 0x06004C1D RID: 19485 RVA: 0x000EDEDC File Offset: 0x000EC0DC
		// (set) Token: 0x06004C1E RID: 19486 RVA: 0x000EDEE4 File Offset: 0x000EC0E4
		public IDictionary<string, DataBoundControl> ResourceControls { get; set; }

		// Token: 0x170018E1 RID: 6369
		// (get) Token: 0x06004C1F RID: 19487 RVA: 0x000EDEED File Offset: 0x000EC0ED
		// (set) Token: 0x06004C20 RID: 19488 RVA: 0x000EDEF5 File Offset: 0x000EC0F5
		public IDictionary<string, WebControl> AttributeControls { get; set; }

		// Token: 0x170018E2 RID: 6370
		// (get) Token: 0x06004C21 RID: 19489 RVA: 0x000EDEFE File Offset: 0x000EC0FE
		// (set) Token: 0x06004C22 RID: 19490 RVA: 0x000EDF06 File Offset: 0x000EC106
		public LinkButton CloseButton { get; set; }

		// Token: 0x170018E3 RID: 6371
		// (get) Token: 0x06004C23 RID: 19491 RVA: 0x000EDF0F File Offset: 0x000EC10F
		// (set) Token: 0x06004C24 RID: 19492 RVA: 0x000EDF17 File Offset: 0x000EC117
		public LinkButton CancelButton { get; set; }

		// Token: 0x170018E4 RID: 6372
		// (get) Token: 0x06004C25 RID: 19493 RVA: 0x000EDF20 File Offset: 0x000EC120
		// (set) Token: 0x06004C26 RID: 19494 RVA: 0x000EDF28 File Offset: 0x000EC128
		public LinkButton InsertButton { get; set; }

		// Token: 0x170018E5 RID: 6373
		// (get) Token: 0x06004C27 RID: 19495 RVA: 0x000EDF31 File Offset: 0x000EC131
		// (set) Token: 0x06004C28 RID: 19496 RVA: 0x000EDF39 File Offset: 0x000EC139
		public LinkButton UpdateButton { get; set; }

		// Token: 0x170018E6 RID: 6374
		// (get) Token: 0x06004C29 RID: 19497 RVA: 0x000EDF42 File Offset: 0x000EC142
		// (set) Token: 0x06004C2A RID: 19498 RVA: 0x000EDF4A File Offset: 0x000EC14A
		public WebControl Subject { get; set; }

		// Token: 0x170018E7 RID: 6375
		// (get) Token: 0x06004C2B RID: 19499 RVA: 0x000EDF53 File Offset: 0x000EC153
		// (set) Token: 0x06004C2C RID: 19500 RVA: 0x000EDF5B File Offset: 0x000EC15B
		public WebControl Description { get; set; }

		// Token: 0x170018E8 RID: 6376
		// (get) Token: 0x06004C2D RID: 19501 RVA: 0x000EDF64 File Offset: 0x000EC164
		// (set) Token: 0x06004C2E RID: 19502 RVA: 0x000EDF6C File Offset: 0x000EC16C
		public Control StartTime { get; set; }

		// Token: 0x170018E9 RID: 6377
		// (get) Token: 0x06004C2F RID: 19503 RVA: 0x000EDF75 File Offset: 0x000EC175
		// (set) Token: 0x06004C30 RID: 19504 RVA: 0x000EDF7D File Offset: 0x000EC17D
		public Control StartDate { get; set; }

		// Token: 0x170018EA RID: 6378
		// (get) Token: 0x06004C31 RID: 19505 RVA: 0x000EDF86 File Offset: 0x000EC186
		// (set) Token: 0x06004C32 RID: 19506 RVA: 0x000EDF8E File Offset: 0x000EC18E
		public Control EndTime { get; set; }

		// Token: 0x170018EB RID: 6379
		// (get) Token: 0x06004C33 RID: 19507 RVA: 0x000EDF97 File Offset: 0x000EC197
		// (set) Token: 0x06004C34 RID: 19508 RVA: 0x000EDF9F File Offset: 0x000EC19F
		public Control EndDate { get; set; }

		// Token: 0x170018EC RID: 6380
		// (get) Token: 0x06004C35 RID: 19509 RVA: 0x000EDFA8 File Offset: 0x000EC1A8
		// (set) Token: 0x06004C36 RID: 19510 RVA: 0x000EDFB0 File Offset: 0x000EC1B0
		public CheckBox AllDayEvent { get; set; }

		// Token: 0x170018ED RID: 6381
		// (get) Token: 0x06004C37 RID: 19511 RVA: 0x000EDFB9 File Offset: 0x000EC1B9
		// (set) Token: 0x06004C38 RID: 19512 RVA: 0x000EDFC1 File Offset: 0x000EC1C1
		public DataBoundControl Reminder { get; set; }

		// Token: 0x170018EE RID: 6382
		// (get) Token: 0x06004C39 RID: 19513 RVA: 0x000EDFCA File Offset: 0x000EC1CA
		// (set) Token: 0x06004C3A RID: 19514 RVA: 0x000EDFD2 File Offset: 0x000EC1D2
		public DataBoundControl TimeZones { get; set; }

		// Token: 0x170018EF RID: 6383
		// (get) Token: 0x06004C3B RID: 19515 RVA: 0x000EDFDB File Offset: 0x000EC1DB
		// (set) Token: 0x06004C3C RID: 19516 RVA: 0x000EDFE3 File Offset: 0x000EC1E3
		public LinkButton ResetExceptions { get; set; }

		// Token: 0x170018F0 RID: 6384
		// (get) Token: 0x06004C3D RID: 19517 RVA: 0x000EDFEC File Offset: 0x000EC1EC
		public virtual DateTime StartDateValue
		{
			get
			{
				return (this.StartDate as RadDatePicker).SelectedDate.Value.Date;
			}
		}

		// Token: 0x170018F1 RID: 6385
		// (get) Token: 0x06004C3E RID: 19518 RVA: 0x000EE01C File Offset: 0x000EC21C
		public virtual TimeSpan StartTimeValue
		{
			get
			{
				return (this.StartTime as RadTimePicker).SelectedDate.Value.TimeOfDay;
			}
		}

		// Token: 0x170018F2 RID: 6386
		// (get) Token: 0x06004C3F RID: 19519 RVA: 0x000EE04C File Offset: 0x000EC24C
		public virtual DateTime EndDateValue
		{
			get
			{
				return (this.EndDate as RadDatePicker).SelectedDate.Value.Date;
			}
		}

		// Token: 0x170018F3 RID: 6387
		// (get) Token: 0x06004C40 RID: 19520 RVA: 0x000EE07C File Offset: 0x000EC27C
		public virtual TimeSpan EndTimeValue
		{
			get
			{
				return (this.EndTime as RadTimePicker).SelectedDate.Value.TimeOfDay;
			}
		}

		// Token: 0x170018F4 RID: 6388
		// (get) Token: 0x06004C41 RID: 19521
		public abstract string SelectedTimeZone { get; }

		// Token: 0x170018F5 RID: 6389
		// (get) Token: 0x06004C42 RID: 19522
		public abstract string SelectedReminder { get; }

		// Token: 0x170018F6 RID: 6390
		// (get) Token: 0x06004C43 RID: 19523 RVA: 0x000EE0A9 File Offset: 0x000EC2A9
		public virtual string SubjectText
		{
			get
			{
				return (this.Subject as RadTextBox).Text;
			}
		}

		// Token: 0x170018F7 RID: 6391
		// (get) Token: 0x06004C44 RID: 19524 RVA: 0x000EE0BB File Offset: 0x000EC2BB
		public virtual string DescriptionText
		{
			get
			{
				return (this.Description as RadTextBox).Text;
			}
		}

		// Token: 0x06004C45 RID: 19525 RVA: 0x000EE0D0 File Offset: 0x000EC2D0
		public ViewBase(AdvancedTemplate owner)
		{
			this.Owner = owner;
			this.Appointment = owner.Appointment;
			this.Localization = owner.Owner.Localization;
			this.ParentScheduler = owner.Owner;
			this._runtimeSkin = owner._runtimeSkin;
		}

		// Token: 0x06004C46 RID: 19526 RVA: 0x000EE131 File Offset: 0x000EC331
		public void CreateControls()
		{
			this.CreateCloseButton();
			this.CreateSharedCalendar();
			this.CreateAppointmentBasicControls();
			this.CreateAppointmentAdvancedControls();
			if (this.ParentScheduler.RecurrenceSupport)
			{
				this.CreateAppointmentRecurrenceControls();
			}
		}

		// Token: 0x06004C47 RID: 19527 RVA: 0x000EE160 File Offset: 0x000EC360
		protected virtual void CreateCloseButton()
		{
			this.CloseButton = new LinkButton
			{
				ID = "AdvancedEditCloseButton",
				CssClass = "rsAdvEditClose",
				CommandName = "Cancel",
				CausesValidation = false,
				ToolTip = this.Localization.AdvancedClose,
				Text = this.Localization.AdvancedClose
			};
		}

		// Token: 0x06004C48 RID: 19528 RVA: 0x000EE1C4 File Offset: 0x000EC3C4
		protected void CreateSharedCalendar()
		{
			this.SharedCalendar = new RadCalendar();
			this.SharedCalendar.ID = "SharedCalendar";
			this.SharedCalendar.CultureInfo = this.ParentScheduler.Culture;
			if (this.SharedCalendar.RuntimeSkin != this._runtimeSkin)
			{
				this.SharedCalendar.Skin = this._runtimeSkin;
			}
			this.SharedCalendar.EnableEmbeddedSkins = this.ParentScheduler.EnableEmbeddedSkins;
			this.SharedCalendar.EnableEmbeddedScripts = this.ParentScheduler.EnableEmbeddedScripts;
			this.SharedCalendar.RenderMode = this.Owner.ResolvedRenderMode;
			this.SharedCalendar.FastNavigationSettings.OkButtonCaption = this.Localization.AdvancedCalendarOK;
			this.SharedCalendar.FastNavigationSettings.CancelButtonCaption = this.Localization.AdvancedCalendarCancel;
			this.SharedCalendar.FastNavigationSettings.TodayButtonCaption = this.Localization.AdvancedCalendarToday;
			this.SharedCalendar.ShowRowHeaders = false;
			this.SharedCalendar.UseColumnHeadersAsSelectors = false;
			this.SharedCalendar.ShowOtherMonthsDays = false;
			this.SharedCalendar.RangeMinDate = this.MinDate;
		}

		// Token: 0x06004C49 RID: 19529 RVA: 0x000EE2F4 File Offset: 0x000EC4F4
		private void CreateAppointmentBasicControls()
		{
			this.Subject = this.CreateSubjectTextBox();
			this.StartDate = this.CreateDatePicker("StartDate");
			this.StartDate.DataBinding += this.StartDatePicker_DataBinding;
			this.StartTime = this.CreateTimePicker("StartTime");
			this.StartTime.DataBinding += this.StartTimePicker_DataBinding;
			this.EndDate = this.CreateDatePicker("EndDate");
			this.EndDate.DataBinding += this.EndDatePicker_DataBinding;
			this.EndTime = this.CreateTimePicker("EndTime");
			this.EndTime.DataBinding += this.EndTimePicker_DataBinding;
			if (this.ParentScheduler.AdvancedForm.EnableTimeZonesEditing)
			{
				this.TimeZones = this.CreateTimeZonesControl("TimeZones");
			}
			this.AllDayEvent = this.CreateAllDayCheckBox();
			if (this.ParentScheduler.RemindersSupport)
			{
				this.Reminder = this.CreateReminderControl("Reminder");
				this.Reminder.DataBinding += this.Reminder_DataBinding;
			}
		}

		// Token: 0x06004C4A RID: 19530 RVA: 0x000EE416 File Offset: 0x000EC616
		private void CreateAppointmentAdvancedControls()
		{
			this.CreateAttributeControls();
			this.CreateResourceControls();
			if (this.ParentScheduler.HasDescriptionField)
			{
				this.Description = this.CreateDescriptionTextBox();
			}
		}

		// Token: 0x06004C4B RID: 19531 RVA: 0x000EE43D File Offset: 0x000EC63D
		protected virtual void CreateAppointmentRecurrenceControls()
		{
			this.ResetExceptions = new LinkButton();
			this.ResetExceptions.ID = "ResetExceptions";
		}

		// Token: 0x06004C4C RID: 19532 RVA: 0x000EE474 File Offset: 0x000EC674
		protected virtual WebControl CreateSubjectTextBox()
		{
			RadTextBox radTextBox = this.CreateTextBox("Subject");
			radTextBox.TextMode = (this.ParentScheduler.HasDescriptionField ? InputMode.SingleLine : InputMode.MultiLine);
			radTextBox.Rows = 1;
			radTextBox.Columns = 50;
			radTextBox.Width = Unit.Percentage(100.0);
			radTextBox.Label = this.Localization.AdvancedSubject;
			radTextBox.LabelWidth = Unit.Empty;
			radTextBox.LabelCssClass = "rfbLabel";
			radTextBox.RenderMode = this.Owner.ResolvedRenderMode;
			radTextBox.DataBinding += delegate(object sender, EventArgs e)
			{
				((RadTextBox)sender).Text = this.Appointment.Subject;
			};
			return radTextBox;
		}

		// Token: 0x06004C4D RID: 19533
		protected abstract DataBoundControl CreateTimeZonesControl(string id);

		// Token: 0x06004C4E RID: 19534 RVA: 0x000EE514 File Offset: 0x000EC714
		private CheckBox CreateAllDayCheckBox()
		{
			return new CheckBox
			{
				CssClass = "rsAdvChkWrap",
				ID = "AllDayEvent",
				Checked = false,
				Text = this.Localization.AdvancedAllDayEvent
			};
		}

		// Token: 0x06004C4F RID: 19535
		protected abstract DataBoundControl CreateReminderControl(string id);

		// Token: 0x06004C50 RID: 19536 RVA: 0x000EE558 File Offset: 0x000EC758
		private void CreateAttributeControls()
		{
			this.AttributeControls = new Dictionary<string, WebControl>();
			if (this.ParentScheduler.AdvancedForm.EnableCustomAttributeEditing && this.ParentScheduler.CustomAttributeNames.Length > 0)
			{
				foreach (string text in this.ParentScheduler.CustomAttributeNames)
				{
					WebControl webControl = this.CreateAttributeTextBox("Attr" + text);
					webControl.DataBinding += this.AttributeControl_DataBinding;
					this.AttributeControls.Add(text, webControl);
				}
			}
		}

		// Token: 0x06004C51 RID: 19537 RVA: 0x000EE5E4 File Offset: 0x000EC7E4
		protected virtual WebControl CreateAttributeTextBox(string id)
		{
			RadTextBox radTextBox = this.CreateTextBox(id);
			radTextBox.Width = Unit.Percentage(100.0);
			return radTextBox;
		}

		// Token: 0x06004C52 RID: 19538 RVA: 0x000EE628 File Offset: 0x000EC828
		protected virtual WebControl CreateDescriptionTextBox()
		{
			RadTextBox radTextBox = this.CreateTextBox("Description");
			radTextBox.TextMode = InputMode.MultiLine;
			radTextBox.Rows = 5;
			radTextBox.Columns = 50;
			radTextBox.Width = Unit.Percentage(100.0);
			radTextBox.Label = this.Localization.AdvancedDescription;
			radTextBox.LabelWidth = Unit.Empty;
			radTextBox.LabelCssClass = "rfbLabel";
			radTextBox.RenderMode = this.Owner.ResolvedRenderMode;
			radTextBox.DataBinding += delegate(object sender, EventArgs e)
			{
				((RadTextBox)sender).Text = this.Appointment.Description;
			};
			return radTextBox;
		}

		// Token: 0x06004C53 RID: 19539 RVA: 0x000EE6B8 File Offset: 0x000EC8B8
		private void CreateResourceControls()
		{
			this.ResourceControls = new Dictionary<string, DataBoundControl>();
			foreach (object obj in this.ParentScheduler.ResourceTypes)
			{
				ResourceType resourceType = (ResourceType)obj;
				DataBoundControl dataBoundControl;
				if (resourceType.AllowMultipleValues)
				{
					SchedulerCheckBoxList schedulerCheckBoxList = new SchedulerCheckBoxList();
					this.PopulateResourceControl(schedulerCheckBoxList, resourceType.Name, !resourceType.AllowMultipleValues);
					dataBoundControl = schedulerCheckBoxList;
				}
				else
				{
					dataBoundControl = this.CreateResourceControl();
					this.PopulateResourceControl(dataBoundControl, resourceType.Name, !resourceType.AllowMultipleValues);
				}
				dataBoundControl.ID = "Res" + resourceType.Name;
				dataBoundControl.DataBound += this.ResourceControl_DataBound;
				this.ResourceControls.Add(resourceType.Name, dataBoundControl);
			}
		}

		// Token: 0x06004C54 RID: 19540
		protected abstract DataBoundControl CreateResourceControl();

		// Token: 0x06004C55 RID: 19541 RVA: 0x000EE7A4 File Offset: 0x000EC9A4
		private void PopulateResourceControl(SchedulerCheckBoxList resourceControl, string resType, bool addNullValue)
		{
			if (addNullValue)
			{
				resourceControl.Items.Add(new ListItem("-", "NULL"));
			}
			foreach (Resource resource in this.GetResources(resType))
			{
				resourceControl.Items.Add(new ListItem(resource.Text, LosSerializer.Serialize(resource.Key)));
			}
		}

		// Token: 0x06004C56 RID: 19542
		protected abstract void PopulateResourceControl(DataBoundControl resourceControl, string resType, bool addNullValue);

		// Token: 0x06004C57 RID: 19543 RVA: 0x000EE82C File Offset: 0x000ECA2C
		protected IEnumerable<Resource> GetResources(string resType)
		{
			List<Resource> list = new List<Resource>();
			IEnumerable<Resource> resourcesByType = this.Appointment.Owner.Resources.GetResourcesByType(resType);
			foreach (Resource resource in resourcesByType)
			{
				if (this.Owner.IncludeResource(resource))
				{
					list.Add(resource);
				}
			}
			return list;
		}

		// Token: 0x06004C58 RID: 19544 RVA: 0x000EE8A0 File Offset: 0x000ECAA0
		private RadTextBox CreateTextBox(string id)
		{
			RadTextBox radTextBox = new RadTextBox
			{
				ID = id,
				EnableEmbeddedSkins = this.ParentScheduler.EnableEmbeddedSkins,
				EnableEmbeddedScripts = this.ParentScheduler.EnableEmbeddedScripts,
				RenderMode = this.Owner.ResolvedRenderMode
			};
			if (radTextBox.RuntimeSkin != this._runtimeSkin)
			{
				radTextBox.Skin = this._runtimeSkin;
			}
			return radTextBox;
		}

		// Token: 0x06004C59 RID: 19545 RVA: 0x000EE910 File Offset: 0x000ECB10
		protected virtual Control CreateDatePicker(string id)
		{
			RadDatePicker radDatePicker = new RadDatePicker
			{
				ID = id,
				CssClass = "rsAdvDatePicker",
				EnableEmbeddedSkins = this.ParentScheduler.EnableEmbeddedSkins,
				EnableEmbeddedScripts = this.ParentScheduler.EnableEmbeddedScripts
			};
			radDatePicker.Style[HtmlTextWriterStyle.ZIndex] = this.ParentScheduler.AdvancedForm.ZIndex.ToString();
			if (radDatePicker.RuntimeSkin != this._runtimeSkin)
			{
				radDatePicker.Skin = this._runtimeSkin;
			}
			radDatePicker.DateInput.Skin = this._runtimeSkin;
			radDatePicker.Culture = this.ParentScheduler.Culture;
			radDatePicker.SharedCalendar = this.SharedCalendar;
			radDatePicker.Calendar.Skin = this.SharedCalendar.Skin;
			radDatePicker.DateInput.DateFormat = this.ParentScheduler.AdvancedForm.DateFormat;
			radDatePicker.RenderMode = this.Owner.ResolvedRenderMode;
			radDatePicker.SelectedDate = new DateTime?(this.ParentScheduler.UtcToDisplay(DateTime.Now));
			radDatePicker.DateInput.EmptyMessageStyle.CssClass = "riError";
			radDatePicker.DateInput.EmptyMessage = " ";
			radDatePicker.MinDate = this.MinDate;
			return radDatePicker;
		}

		// Token: 0x06004C5A RID: 19546 RVA: 0x000EEA5C File Offset: 0x000ECC5C
		protected virtual Control CreateTimePicker(string id)
		{
			RadTimePicker radTimePicker = new RadTimePicker
			{
				ID = id,
				CssClass = "rsAdvTimePicker",
				EnableEmbeddedSkins = this.ParentScheduler.EnableEmbeddedSkins,
				EnableEmbeddedScripts = this.ParentScheduler.EnableEmbeddedScripts
			};
			radTimePicker.Style[HtmlTextWriterStyle.ZIndex] = this.ParentScheduler.AdvancedForm.ZIndex.ToString();
			radTimePicker.DateInput.Label = "hidden label";
			radTimePicker.DateInput.LabelWidth = Unit.Pixel(0);
			radTimePicker.DateInput.LabelCssClass = "rsHidden";
			if (radTimePicker.RuntimeSkin != this._runtimeSkin)
			{
				radTimePicker.Skin = this._runtimeSkin;
			}
			radTimePicker.Culture = this.ParentScheduler.Culture;
			radTimePicker.SelectedDate = new DateTime?(this.ParentScheduler.UtcToDisplay(DateTime.Now));
			radTimePicker.DateInput.EmptyMessageStyle.CssClass = "riError";
			radTimePicker.DateInput.EmptyMessage = " ";
			radTimePicker.DateInput.DateFormat = this.ParentScheduler.AdvancedForm.TimeFormat;
			radTimePicker.RenderMode = this.Owner.ResolvedRenderMode;
			radTimePicker.TimeView.Columns = 2;
			radTimePicker.TimeView.ShowHeader = false;
			radTimePicker.TimeView.StartTime = TimeSpan.FromHours(8.0);
			radTimePicker.TimeView.EndTime = TimeSpan.FromHours(18.0);
			radTimePicker.TimeView.Interval = TimeSpan.FromMinutes(30.0);
			radTimePicker.TimeView.TimeFormat = this.ParentScheduler.AdvancedForm.TimeFormat;
			radTimePicker.MinDate = this.MinDate;
			return radTimePicker;
		}

		// Token: 0x06004C5B RID: 19547 RVA: 0x000EEC24 File Offset: 0x000ECE24
		public void CreateInsertButtons()
		{
			this.CancelButton = this.CreateButton("CancelButton", "rsAdvEditCancel", "Cancel", this.Localization.Cancel);
			this.InsertButton = this.CreateButton("InsertButton", "rsPrimary rsAdvEditSave", "Insert", this.Localization.Save);
			this.InsertButton.ValidationGroup = this.ParentScheduler.ValidationGroup;
		}

		// Token: 0x06004C5C RID: 19548 RVA: 0x000EEC94 File Offset: 0x000ECE94
		public virtual void CreateEditButtons()
		{
			this.CancelButton = this.CreateButton("CancelButton", "rsAdvEditCancel", "Cancel", this.Localization.Cancel);
			this.UpdateButton = this.CreateButton("UpdateButton", "rsPrimary rsAdvEditSave", "Update", this.Localization.Save);
			this.UpdateButton.ValidationGroup = this.ParentScheduler.ValidationGroup;
		}

		// Token: 0x06004C5D RID: 19549 RVA: 0x000EED04 File Offset: 0x000ECF04
		protected virtual LinkButton CreateButton(string id, string cssClass, string commandName, string text)
		{
			LinkButton linkButton = new LinkButton
			{
				ID = id,
				CssClass = cssClass,
				CommandName = commandName
			};
			linkButton.Attributes["onclick"] = "javascript:return false;";
			WebControl webControl = new WebControl(HtmlTextWriterTag.Span);
			webControl.Controls.Add(new LiteralControl(text));
			linkButton.Controls.Add(webControl);
			return linkButton;
		}

		// Token: 0x06004C5E RID: 19550
		protected abstract void Reminder_DataBinding(object sender, EventArgs e);

		// Token: 0x06004C5F RID: 19551 RVA: 0x000EED6A File Offset: 0x000ECF6A
		protected virtual void StartDatePicker_DataBinding(object sender, EventArgs e)
		{
			((RadDatePicker)sender).SelectedDate = new DateTime?(this.HasTimeZoneOffset ? this.ParentScheduler.UtcToDisplay(this.Appointment.Start) : this.Appointment.StartLocal);
		}

		// Token: 0x06004C60 RID: 19552 RVA: 0x000EEDA7 File Offset: 0x000ECFA7
		protected virtual void StartTimePicker_DataBinding(object sender, EventArgs e)
		{
			((RadDatePicker)sender).SelectedDate = new DateTime?(this.HasTimeZoneOffset ? this.ParentScheduler.UtcToDisplay(this.Appointment.Start) : this.Appointment.StartLocal);
		}

		// Token: 0x06004C61 RID: 19553 RVA: 0x000EEDE4 File Offset: 0x000ECFE4
		protected virtual void EndDatePicker_DataBinding(object sender, EventArgs e)
		{
			DateTime dateTime = this.HasTimeZoneOffset ? this.Appointment.End : this.Appointment.EndLocal;
			if (this.Owner.IsAllDayAppointment(this.Appointment))
			{
				dateTime = dateTime.AddDays(-1.0);
			}
			((RadDatePicker)sender).SelectedDate = new DateTime?(this.HasTimeZoneOffset ? this.ParentScheduler.UtcToDisplay(dateTime) : dateTime);
		}

		// Token: 0x06004C62 RID: 19554 RVA: 0x000EEE60 File Offset: 0x000ED060
		protected virtual void EndTimePicker_DataBinding(object sender, EventArgs e)
		{
			DateTime dateTime = this.HasTimeZoneOffset ? this.Appointment.End : this.Appointment.EndLocal;
			if (this.Owner.IsAllDayAppointment(this.Appointment))
			{
				dateTime = dateTime.AddDays(-1.0);
			}
			((RadDatePicker)sender).SelectedDate = new DateTime?(this.HasTimeZoneOffset ? this.ParentScheduler.UtcToDisplay(dateTime) : dateTime);
		}

		// Token: 0x06004C63 RID: 19555 RVA: 0x000EEEDC File Offset: 0x000ED0DC
		protected virtual void AttributeControl_DataBinding(object sender, EventArgs e)
		{
			RadTextBox radTextBox = (RadTextBox)sender;
			radTextBox.Text = this.Appointment.Attributes[radTextBox.ID.Substring("Attr".Length)];
		}

		// Token: 0x06004C64 RID: 19556
		protected abstract void ResourceControl_DataBound(object sender, EventArgs e);

		// Token: 0x06004C65 RID: 19557 RVA: 0x000EEF1C File Offset: 0x000ED11C
		public virtual void ExtractAttributeValues(IDictionary target)
		{
			foreach (string key in this.AttributeControls.Keys)
			{
				RadTextBox radTextBox = this.AttributeControls[key] as RadTextBox;
				target[key] = radTextBox.Text;
			}
		}

		// Token: 0x06004C66 RID: 19558
		public abstract void ExtractResourceValues(IDictionary target);

		// Token: 0x04001323 RID: 4899
		private readonly DateTime MinDate = new DateTime(1900, 1, 1);

		// Token: 0x04001324 RID: 4900
		internal readonly string _runtimeSkin;

		// Token: 0x04001325 RID: 4901
		private AdvancedTemplate _owner;
	}
}
