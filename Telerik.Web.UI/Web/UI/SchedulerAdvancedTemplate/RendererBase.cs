using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI.SchedulerAdvancedTemplate
{
	// Token: 0x0200080F RID: 2063
	internal abstract class RendererBase : IAdvancedTemplateRenderer
	{
		// Token: 0x170018B2 RID: 6322
		// (get) Token: 0x06004B8D RID: 19341 RVA: 0x000EC7C4 File Offset: 0x000EA9C4
		// (set) Token: 0x06004B8E RID: 19342 RVA: 0x000EC7CC File Offset: 0x000EA9CC
		public IAdvancedTemplateView View
		{
			get
			{
				return this._view;
			}
			protected set
			{
				this._view = value;
			}
		}

		// Token: 0x170018B3 RID: 6323
		// (get) Token: 0x06004B8F RID: 19343 RVA: 0x000EC7D5 File Offset: 0x000EA9D5
		// (set) Token: 0x06004B90 RID: 19344 RVA: 0x000EC7DD File Offset: 0x000EA9DD
		public SchedulerStrings Localization { get; set; }

		// Token: 0x170018B4 RID: 6324
		// (get) Token: 0x06004B91 RID: 19345 RVA: 0x000EC7E6 File Offset: 0x000EA9E6
		// (set) Token: 0x06004B92 RID: 19346 RVA: 0x000EC7EE File Offset: 0x000EA9EE
		public RadScheduler ParentScheduler { get; set; }

		// Token: 0x170018B5 RID: 6325
		// (get) Token: 0x06004B93 RID: 19347 RVA: 0x000EC7F7 File Offset: 0x000EA9F7
		protected string ClientValidationFunction
		{
			get
			{
				return "Telerik.Web.UI.Scheduling.AdvancedTemplate._customValidationHandler";
			}
		}

		// Token: 0x170018B6 RID: 6326
		// (get) Token: 0x06004B94 RID: 19348 RVA: 0x000EC7FE File Offset: 0x000EA9FE
		// (set) Token: 0x06004B95 RID: 19349 RVA: 0x000EC806 File Offset: 0x000EAA06
		public Panel TitleBarOuterPanel { get; set; }

		// Token: 0x170018B7 RID: 6327
		// (get) Token: 0x06004B96 RID: 19350 RVA: 0x000EC80F File Offset: 0x000EAA0F
		// (set) Token: 0x06004B97 RID: 19351 RVA: 0x000EC817 File Offset: 0x000EAA17
		public Panel TitleBarInnerPanel { get; set; }

		// Token: 0x170018B8 RID: 6328
		// (get) Token: 0x06004B98 RID: 19352 RVA: 0x000EC820 File Offset: 0x000EAA20
		// (set) Token: 0x06004B99 RID: 19353 RVA: 0x000EC828 File Offset: 0x000EAA28
		public Panel OptionsPanel { get; set; }

		// Token: 0x170018B9 RID: 6329
		// (get) Token: 0x06004B9A RID: 19354 RVA: 0x000EC831 File Offset: 0x000EAA31
		// (set) Token: 0x06004B9B RID: 19355 RVA: 0x000EC839 File Offset: 0x000EAA39
		public Panel OptionsPanelScroll { get; set; }

		// Token: 0x170018BA RID: 6330
		// (get) Token: 0x06004B9C RID: 19356 RVA: 0x000EC842 File Offset: 0x000EAA42
		// (set) Token: 0x06004B9D RID: 19357 RVA: 0x000EC84A File Offset: 0x000EAA4A
		public Panel BasicControlsPanel { get; set; }

		// Token: 0x170018BB RID: 6331
		// (get) Token: 0x06004B9E RID: 19358 RVA: 0x000EC853 File Offset: 0x000EAA53
		// (set) Token: 0x06004B9F RID: 19359 RVA: 0x000EC85B File Offset: 0x000EAA5B
		public Panel AdvancedControlsPanel { get; set; }

		// Token: 0x170018BC RID: 6332
		// (get) Token: 0x06004BA0 RID: 19360 RVA: 0x000EC864 File Offset: 0x000EAA64
		// (set) Token: 0x06004BA1 RID: 19361 RVA: 0x000EC86C File Offset: 0x000EAA6C
		public Panel ButtonsPanel { get; set; }

		// Token: 0x06004BA2 RID: 19362 RVA: 0x000EC875 File Offset: 0x000EAA75
		public RendererBase(IAdvancedTemplateView view)
		{
			this.View = view;
			this.ParentScheduler = view.Owner.Owner;
			this.Localization = view.Owner.Owner.Localization;
		}

		// Token: 0x06004BA3 RID: 19363 RVA: 0x000EC8AC File Offset: 0x000EAAAC
		public virtual void CreateLayout(Control container)
		{
			Panel panel = new Panel();
			container.Controls.Add(panel);
			panel.CssClass = "rsDialog rsAdvancedEdit";
			if (this.ParentScheduler.AdvancedForm.Modal)
			{
				Panel panel2 = new Panel();
				panel2.CssClass = "rsModalBgTopLeft";
				Panel panel3 = new Panel();
				panel3.CssClass = "rsModalBgTopRight";
				Panel panel4 = new Panel();
				panel4.CssClass = "rsModalBgBottomLeft";
				Panel panel5 = new Panel();
				panel5.CssClass = "rsModalBgBottomRight";
				panel.Controls.Add(panel2);
				panel.Controls.Add(panel3);
				panel.Controls.Add(panel4);
				panel.Controls.Add(panel5);
			}
			this.TitleBarOuterPanel = new Panel();
			panel.Controls.Add(this.TitleBarOuterPanel);
			this.TitleBarOuterPanel.CssClass = "rsAdvTitle";
			this.TitleBarInnerPanel = new Panel();
			this.TitleBarOuterPanel.Controls.Add(this.TitleBarInnerPanel);
			this.TitleBarInnerPanel.CssClass = "rsAdvInnerTitle";
			Panel panel6 = new Panel();
			panel.Controls.Add(panel6);
			panel6.CssClass = "rsAdvContentWrapper";
			this.OptionsPanelScroll = new Panel();
			panel6.Controls.Add(this.OptionsPanelScroll);
			this.OptionsPanelScroll.CssClass = "rsAdvOptionsScroll";
			this.OptionsPanel = new Panel();
			this.OptionsPanelScroll.Controls.Add(this.OptionsPanel);
			this.OptionsPanel.CssClass = "rsAdvOptions";
			Panel panel7 = new Panel
			{
				ID = "BasicControlsPanel"
			};
			this.OptionsPanel.Controls.Add(panel7);
			panel7.CssClass = "rsAdvBasicControls";
			this.BasicControlsPanel = new Panel();
			panel7.Controls.Add(this.BasicControlsPanel);
			this.BasicControlsPanel.CssClass = "rsAdvOptionsPanel";
			this.BasicControlsPanel.DataBinding += this.ControlsPanel_DataBinding;
			this.AdvancedControlsPanel = new Panel();
			this.OptionsPanel.Controls.Add(this.AdvancedControlsPanel);
			this.AdvancedControlsPanel.ID = "AdvancedControlsPanel";
			this.AdvancedControlsPanel.CssClass = "rsAdvMoreControls";
			Panel panel8 = new Panel();
			panel6.Controls.Add(panel8);
			panel8.CssClass = "rsAdvancedSubmitArea";
			this.ButtonsPanel = new Panel();
			panel8.Controls.Add(this.ButtonsPanel);
			this.ButtonsPanel.ID = "ButtonsPanel";
			this.ButtonsPanel.CssClass = "rsAdvButtonWrapper";
		}

		// Token: 0x06004BA4 RID: 19364 RVA: 0x000ECB52 File Offset: 0x000EAD52
		protected void ControlsPanel_DataBinding(object sender, EventArgs e)
		{
			this.View.AllDayEvent.Checked = this.View.Owner.IsAllDayAppointment(this.View.Appointment);
		}

		// Token: 0x06004BA5 RID: 19365 RVA: 0x000ECB7F File Offset: 0x000EAD7F
		public virtual void CreateControls(Control container)
		{
			this.CreateCloseButton();
			this.CreateSharedCalendar(container);
			this.CreateAppointmentBasicControls();
			this.CreateAppointmentValidators();
			this.CreateAppointmentAdvancedControls();
			if (this.ParentScheduler.RecurrenceSupport)
			{
				this.CreateAppointmentRecurrenceControls();
			}
		}

		// Token: 0x06004BA6 RID: 19366 RVA: 0x000ECBB3 File Offset: 0x000EADB3
		public virtual void CreateTitle(string title)
		{
			this.TitleBarInnerPanel.Controls.Add(new LiteralControl(title));
		}

		// Token: 0x06004BA7 RID: 19367 RVA: 0x000ECBCB File Offset: 0x000EADCB
		public void CreateInsertButtons()
		{
			this.AddButton(this.View.InsertButton);
			this.AddButton(this.View.CancelButton);
		}

		// Token: 0x06004BA8 RID: 19368 RVA: 0x000ECBEF File Offset: 0x000EADEF
		public virtual void CreateEditButtons()
		{
			this.AddButton(this.View.UpdateButton);
			this.AddButton(this.View.CancelButton);
		}

		// Token: 0x06004BA9 RID: 19369 RVA: 0x000ECC13 File Offset: 0x000EAE13
		protected void AddButton(LinkButton button)
		{
			this.ButtonsPanel.Controls.Add(button);
		}

		// Token: 0x06004BAA RID: 19370 RVA: 0x000ECC26 File Offset: 0x000EAE26
		protected void CreateCloseButton()
		{
			this.TitleBarOuterPanel.Controls.Add(this.View.CloseButton);
		}

		// Token: 0x06004BAB RID: 19371 RVA: 0x000ECC43 File Offset: 0x000EAE43
		protected void CreateSharedCalendar(Control container)
		{
			container.Controls.Add(this.View.SharedCalendar);
		}

		// Token: 0x06004BAC RID: 19372 RVA: 0x000ECC5C File Offset: 0x000EAE5C
		protected virtual void CreateAppointmentBasicControls()
		{
			WebControl webControl = new WebControl(HtmlTextWriterTag.Ul)
			{
				CssClass = "rfbGroup"
			};
			this.BasicControlsPanel.Controls.Add(webControl);
			this.CreateSubjectRow(webControl);
			this.CreateStartTimeRow(webControl);
			this.CreateTimeZonesRow(webControl);
			this.CreateAllDayRow(webControl);
			this.CreateEndTimeRow(webControl);
			this.CreateReminderRow(webControl);
		}

		// Token: 0x06004BAD RID: 19373 RVA: 0x000ECCBC File Offset: 0x000EAEBC
		protected virtual void CreateSubjectRow(WebControl container)
		{
			WebControl webControl = this.CreateRow("");
			container.Controls.Add(webControl);
			webControl.Controls.Add(this.View.Subject);
		}

		// Token: 0x06004BAE RID: 19374 RVA: 0x000ECCF8 File Offset: 0x000EAEF8
		protected void CreateStartTimeRow(WebControl container)
		{
			WebControl webControl = this.CreateCompactRow("rsTimePick");
			container.Controls.Add(webControl);
			WebControl label = this.CreateLabel(webControl, this.Localization.AdvancedFrom);
			webControl.Controls.Add(this.View.StartDate);
			webControl.Controls.Add(this.View.StartTime);
			this.AttachLabel(label, this.View.StartDate);
		}

		// Token: 0x06004BAF RID: 19375 RVA: 0x000ECD70 File Offset: 0x000EAF70
		protected void CreateEndTimeRow(WebControl container)
		{
			string className = string.Format("{0} {1}", "rsTimePick", "rsEndTimePick");
			WebControl webControl = this.CreateRow(className);
			container.Controls.Add(webControl);
			WebControl label = this.CreateLabel(webControl, this.Localization.AdvancedTo);
			webControl.Controls.Add(this.View.EndDate);
			webControl.Controls.Add(this.View.EndTime);
			this.AttachLabel(label, this.View.EndDate);
		}

		// Token: 0x06004BB0 RID: 19376 RVA: 0x000ECDF8 File Offset: 0x000EAFF8
		protected void CreateTimeZonesRow(WebControl container)
		{
			WebControl webControl = this.CreateCompactRow("rsTimeZonesWrapper");
			webControl.EnableViewState = false;
			if (this.ParentScheduler.AdvancedForm.EnableTimeZonesEditing)
			{
				container.Controls.Add(webControl);
				this.AddTimeZonesControls(webControl);
			}
		}

		// Token: 0x06004BB1 RID: 19377 RVA: 0x000ECE40 File Offset: 0x000EB040
		protected virtual void AddTimeZonesControls(WebControl rfbRowTimeZone)
		{
			WebControl label = this.CreateLabel(rfbRowTimeZone, this.Localization.AdvancedTimeZone);
			rfbRowTimeZone.Controls.Add(this.View.TimeZones);
			this.AttachLabel(label, this.View.TimeZones);
		}

		// Token: 0x06004BB2 RID: 19378 RVA: 0x000ECE88 File Offset: 0x000EB088
		protected virtual void CreateAllDayRow(WebControl container)
		{
			string className = string.Format("rfbNoLabel {0}", "rsAllDayWrapper");
			WebControl webControl = this.CreateCompactRow(className);
			container.Controls.Add(webControl);
			webControl.Controls.Add(this.View.AllDayEvent);
		}

		// Token: 0x06004BB3 RID: 19379 RVA: 0x000ECED0 File Offset: 0x000EB0D0
		protected void CreateReminderRow(WebControl container)
		{
			WebControl webControl = this.CreateRow("rsReminderWrapper");
			if (this.ParentScheduler.RemindersSupport)
			{
				container.Controls.Add(webControl);
				this.AddReminderControls(webControl);
			}
		}

		// Token: 0x06004BB4 RID: 19380 RVA: 0x000ECF0C File Offset: 0x000EB10C
		protected virtual void AddReminderControls(WebControl rfbRowReminder)
		{
			WebControl label = this.CreateLabel(rfbRowReminder, this.Localization.Reminder);
			rfbRowReminder.Controls.Add(this.View.Reminder);
			this.AttachLabel(label, this.View.Reminder);
		}

		// Token: 0x06004BB5 RID: 19381 RVA: 0x000ECF54 File Offset: 0x000EB154
		protected virtual void CreateAppointmentValidators()
		{
			this.CreateControlsValidators(this.BasicControlsPanel);
		}

		// Token: 0x06004BB6 RID: 19382 RVA: 0x000ECF9C File Offset: 0x000EB19C
		protected void CreateControlsValidators(WebControl container)
		{
			RequiredFieldValidator child = this.CreateRequiredFieldValidator("SubjectValidator", this.View.Subject.ID, this.Localization.AdvancedSubjectRequired);
			container.Controls.Add(child);
			RequiredFieldValidator child2 = this.CreateRequiredFieldValidator("StartDateValidator", this.View.StartDate.ID, this.Localization.AdvancedStartDateRequired);
			container.Controls.Add(child2);
			RequiredFieldValidator child3 = this.CreateRequiredFieldValidator("StartTimeValidator", this.View.StartTime.ID, this.Localization.AdvancedStartTimeRequired);
			container.Controls.Add(child3);
			RequiredFieldValidator child4 = this.CreateRequiredFieldValidator("EndDateValidator", this.View.EndDate.ID, this.Localization.AdvancedEndDateRequired);
			container.Controls.Add(child4);
			RequiredFieldValidator child5 = this.CreateRequiredFieldValidator("EndTimeValidator", this.View.EndTime.ID, this.Localization.AdvancedEndTimeRequired);
			container.Controls.Add(child5);
			CustomValidator customValidator = this.CreateCustomValidator("DurationValidatorStartDate", this.View.StartDate.ID, this.Localization.AdvancedStartTimeBeforeEndTime);
			customValidator.Display = ValidatorDisplay.Dynamic;
			customValidator.ServerValidate += delegate(object source, ServerValidateEventArgs args)
			{
				args.IsValid = (this.View.Owner.End - this.View.Owner.Start > TimeSpan.Zero);
			};
			container.Controls.Add(customValidator);
			CustomValidator child6 = this.CreateCustomValidator("DurationValidatorEndDate", this.View.EndDate.ID, this.Localization.AdvancedStartTimeBeforeEndTime);
			container.Controls.Add(child6);
			CustomValidator child7 = this.CreateCustomValidator("DurationValidatorStartTime", this.View.StartTime.ID, "");
			container.Controls.Add(child7);
			CustomValidator child8 = this.CreateCustomValidator("DurationValidatorEndTime", this.View.EndTime.ID, "");
			container.Controls.Add(child8);
		}

		// Token: 0x06004BB7 RID: 19383 RVA: 0x000ED190 File Offset: 0x000EB390
		protected void CreateAppointmentAdvancedControls()
		{
			WebControl webControl = new WebControl(HtmlTextWriterTag.Div);
			this.AdvancedControlsPanel.Controls.Add(webControl);
			webControl.CssClass = "rsAdvOptionsPanel";
			WebControl webControl2 = new WebControl(HtmlTextWriterTag.Div);
			webControl.Controls.Add(webControl2);
			webControl2.ID = "AttributeControls";
			webControl2.Style["display"] = (this.ParentScheduler.AdvancedForm.EnableCustomAttributeEditing ? "block" : "none");
			this.CreateAttributeControls(webControl2);
			WebControl webControl3 = new WebControl(HtmlTextWriterTag.Div);
			webControl.Controls.Add(webControl3);
			webControl3.ID = "ResourceControls";
			webControl3.Style["display"] = (this.ParentScheduler.AdvancedForm.EnableResourceEditing ? "block" : "none");
			this.CreateResourceControls(webControl3);
			if (this.ParentScheduler.HasDescriptionField)
			{
				WebControl webControl4 = new WebControl(HtmlTextWriterTag.Ul)
				{
					CssClass = "rfbGroup"
				};
				this.AdvancedControlsPanel.Controls.Add(webControl4);
				this.CreateDescriptionRow(webControl4);
			}
			if (this.View.ResourceControls.Count == 0 && this.View.AttributeControls.Count == 0 && !this.ParentScheduler.HasDescriptionField)
			{
				this.AdvancedControlsPanel.Visible = false;
			}
		}

		// Token: 0x06004BB8 RID: 19384 RVA: 0x000ED2E4 File Offset: 0x000EB4E4
		protected void CreateAttributeControls(Control container)
		{
			if (!this.ParentScheduler.AdvancedForm.EnableCustomAttributeEditing || this.ParentScheduler.CustomAttributeNames.Length == 0)
			{
				return;
			}
			WebControl webControl = new WebControl(HtmlTextWriterTag.Ul)
			{
				CssClass = string.Format("{0} {1}", "rfbGroup", "rsAttributeControls")
			};
			container.Controls.Add(webControl);
			this.CreateAttributeRows(webControl);
		}

		// Token: 0x06004BB9 RID: 19385 RVA: 0x000ED34C File Offset: 0x000EB54C
		protected void CreateAttributeRows(Control container)
		{
			foreach (string text in this.ParentScheduler.CustomAttributeNames)
			{
				WebControl webControl = this.CreateRow("");
				WebControl label = this.CreateLabel(webControl, text);
				WebControl webControl2 = this.View.AttributeControls[text];
				webControl.Controls.Add(webControl2);
				container.Controls.Add(webControl);
				this.AttachLabel(label, webControl2);
			}
		}

		// Token: 0x06004BBA RID: 19386 RVA: 0x000ED3C8 File Offset: 0x000EB5C8
		protected virtual void CreateResourceControls(Control container)
		{
			if (this.ParentScheduler.ResourceTypes.Count == 0)
			{
				return;
			}
			WebControl webControl = new WebControl(HtmlTextWriterTag.Ul)
			{
				CssClass = string.Format("{0} {1}", "rfbGroup", "rsResourceControls")
			};
			container.Controls.Add(webControl);
			this.CreateResourceRows(webControl);
		}

		// Token: 0x06004BBB RID: 19387 RVA: 0x000ED420 File Offset: 0x000EB620
		protected void CreateResourceRows(Control container)
		{
			foreach (object obj in this.ParentScheduler.ResourceTypes)
			{
				ResourceType resourceType = (ResourceType)obj;
				DataBoundControl dataBoundControl = this.View.ResourceControls[resourceType.Name];
				WebControl webControl = this.CreateRow("");
				container.Controls.Add(webControl);
				WebControl webControl2 = this.CreateLabel(webControl, resourceType.Name);
				webControl2.ID = "Lbl" + dataBoundControl.ID;
				webControl.Controls.Add(dataBoundControl);
				this.AttachLabel(webControl2, dataBoundControl);
			}
		}

		// Token: 0x06004BBC RID: 19388 RVA: 0x000ED4E8 File Offset: 0x000EB6E8
		protected virtual void CreateDescriptionRow(WebControl container)
		{
			WebControl webControl = this.CreateRow("");
			container.Controls.Add(webControl);
			webControl.Controls.Add(this.View.Description);
		}

		// Token: 0x06004BBD RID: 19389 RVA: 0x000ED524 File Offset: 0x000EB724
		protected virtual void CreateAppointmentRecurrenceControls()
		{
			WebControl webControl = new WebControl(HtmlTextWriterTag.Span);
			this.OptionsPanelScroll.Controls.Add(webControl);
			webControl.CssClass = "rsAdvResetExceptions";
			webControl.Controls.Add(this.View.ResetExceptions);
		}

		// Token: 0x06004BBE RID: 19390 RVA: 0x000ED56B File Offset: 0x000EB76B
		protected virtual WebControl CreateCompactRow(string className = "")
		{
			className = string.Format("{0} {1}", "rfbCompactRow", className).Trim();
			return this.CreateRow(className);
		}

		// Token: 0x06004BBF RID: 19391 RVA: 0x000ED58C File Offset: 0x000EB78C
		protected WebControl CreateRow(string className = "")
		{
			return new WebControl(HtmlTextWriterTag.Li)
			{
				CssClass = string.Format("{0} {1}", "rfbRow", className).Trim()
			};
		}

		// Token: 0x06004BC0 RID: 19392 RVA: 0x000ED5C0 File Offset: 0x000EB7C0
		protected WebControl CreateLabel(Control container, string text)
		{
			WebControl webControl = new WebControl(HtmlTextWriterTag.Label);
			webControl.Controls.Add(new LiteralControl(text));
			webControl.CssClass = "rfbLabel";
			container.Controls.Add(webControl);
			return webControl;
		}

		// Token: 0x06004BC1 RID: 19393 RVA: 0x000ED600 File Offset: 0x000EB800
		protected void AttachLabel(WebControl label, Control target)
		{
			string text = target.ClientID;
			if (target is RadDatePicker)
			{
				text += "_dateInput";
			}
			else if (target is RadComboBox)
			{
				text += "_Input";
			}
			label.Attributes["for"] = text;
		}

		// Token: 0x06004BC2 RID: 19394 RVA: 0x000ED650 File Offset: 0x000EB850
		private RequiredFieldValidator CreateRequiredFieldValidator(string id, string controlID, string errorMessage)
		{
			return new RequiredFieldValidator
			{
				ID = id,
				ControlToValidate = controlID,
				EnableClientScript = true,
				ErrorMessage = errorMessage,
				Display = ValidatorDisplay.None,
				CssClass = "rsValidatorMsg",
				ValidationGroup = this.ParentScheduler.ValidationGroup
			};
		}

		// Token: 0x06004BC3 RID: 19395 RVA: 0x000ED6A8 File Offset: 0x000EB8A8
		private CustomValidator CreateCustomValidator(string id, string controlID, string errorMessage)
		{
			return new CustomValidator
			{
				ID = id,
				ControlToValidate = controlID,
				Display = ValidatorDisplay.None,
				CssClass = string.Format("{0} {1}", "rsValidatorMsg", "rsInvalid"),
				ValidationGroup = this.ParentScheduler.ValidationGroup,
				ClientValidationFunction = this.ClientValidationFunction,
				ErrorMessage = errorMessage
			};
		}

		// Token: 0x04001317 RID: 4887
		private IAdvancedTemplateView _view;
	}
}
