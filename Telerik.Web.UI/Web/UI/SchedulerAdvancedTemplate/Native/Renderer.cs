using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI.SchedulerAdvancedTemplate.Native
{
	// Token: 0x02000813 RID: 2067
	internal class Renderer : RendererBase
	{
		// Token: 0x170018BD RID: 6333
		// (get) Token: 0x06004BCF RID: 19407 RVA: 0x000EDA0E File Offset: 0x000EBC0E
		// (set) Token: 0x06004BD0 RID: 19408 RVA: 0x000EDA16 File Offset: 0x000EBC16
		public Panel RecurrenceControlsPanel { get; set; }

		// Token: 0x06004BD1 RID: 19409 RVA: 0x000EDA1F File Offset: 0x000EBC1F
		public Renderer(IAdvancedTemplateView view) : base(view)
		{
		}

		// Token: 0x06004BD2 RID: 19410 RVA: 0x000EDA28 File Offset: 0x000EBC28
		public override void CreateLayout(Control container)
		{
			base.CreateLayout(container);
			this.RecurrenceControlsPanel = new Panel();
			base.OptionsPanel.Controls.Add(this.RecurrenceControlsPanel);
			this.RecurrenceControlsPanel.ID = "RecurrenceControlsPanel";
			this.RecurrenceControlsPanel.CssClass = "rsAdvRecurrenceControls";
		}

		// Token: 0x06004BD3 RID: 19411 RVA: 0x000EDA7D File Offset: 0x000EBC7D
		public override void CreateControls(Control container)
		{
			base.CreateCloseButton();
			this.CreateAppointmentBasicControls();
			this.CreateAppointmentValidators();
			base.CreateAppointmentAdvancedControls();
			if (base.ParentScheduler.RecurrenceSupport)
			{
				this.CreateAppointmentRecurrenceControls();
			}
		}

		// Token: 0x06004BD4 RID: 19412 RVA: 0x000EDAAA File Offset: 0x000EBCAA
		public override void CreateEditButtons()
		{
			base.CreateEditButtons();
			if (base.ParentScheduler.UsingWebServiceBinding || base.View.Appointment.AllowDelete)
			{
				base.AddButton(((View)base.View).DeleteButton);
			}
		}

		// Token: 0x06004BD5 RID: 19413 RVA: 0x000EDAE8 File Offset: 0x000EBCE8
		protected override void CreateAppointmentBasicControls()
		{
			WebControl webControl = new WebControl(HtmlTextWriterTag.Ul)
			{
				CssClass = "rfbGroup"
			};
			base.BasicControlsPanel.Controls.Add(webControl);
			this.CreateSubjectRow(webControl);
			base.CreateStartTimeRow(webControl);
			base.CreateEndTimeRow(webControl);
			this.CreateAllDayRow(webControl);
			base.CreateTimeZonesRow(webControl);
			base.CreateReminderRow(webControl);
		}

		// Token: 0x06004BD6 RID: 19414 RVA: 0x000EDB48 File Offset: 0x000EBD48
		protected override void CreateSubjectRow(WebControl container)
		{
			WebControl webControl = base.CreateRow("");
			container.Controls.Add(webControl);
			WebControl label = base.CreateLabel(webControl, base.Localization.AdvancedSubject);
			base.AttachLabel(label, base.View.Subject);
			webControl.Controls.Add(base.View.Subject);
		}

		// Token: 0x06004BD7 RID: 19415 RVA: 0x000EDBA8 File Offset: 0x000EBDA8
		protected override void AddTimeZonesControls(WebControl rfbRowTimeZone)
		{
			rfbRowTimeZone.Controls.Add(this.CreateSeparator());
			base.AddTimeZonesControls(rfbRowTimeZone);
		}

		// Token: 0x06004BD8 RID: 19416 RVA: 0x000EDBC2 File Offset: 0x000EBDC2
		protected override void AddReminderControls(WebControl rfbRowReminder)
		{
			rfbRowReminder.Controls.Add(this.CreateSeparator());
			base.AddReminderControls(rfbRowReminder);
		}

		// Token: 0x06004BD9 RID: 19417 RVA: 0x000EDBDC File Offset: 0x000EBDDC
		protected override void CreateResourceControls(Control container)
		{
			if (base.ParentScheduler.ResourceTypes.Count > 0)
			{
				container.Controls.Add(this.CreateSeparator());
			}
			base.CreateResourceControls(container);
		}

		// Token: 0x06004BDA RID: 19418 RVA: 0x000EDC0C File Offset: 0x000EBE0C
		protected override void CreateDescriptionRow(WebControl container)
		{
			WebControl webControl = base.CreateRow("");
			container.Controls.Add(webControl);
			webControl.Controls.Add(this.CreateSeparator());
			WebControl label = base.CreateLabel(webControl, base.ParentScheduler.Localization.AdvancedDescription);
			base.AttachLabel(label, base.View.Description);
			webControl.Controls.Add(base.View.Description);
		}

		// Token: 0x06004BDB RID: 19419 RVA: 0x000EDC84 File Offset: 0x000EBE84
		protected override void CreateAppointmentRecurrenceControls()
		{
			WebControl webControl = new WebControl(HtmlTextWriterTag.Div);
			this.RecurrenceControlsPanel.Controls.Add(webControl);
			webControl.CssClass = "rsAdvOptionsPanel";
			WebControl webControl2 = new WebControl(HtmlTextWriterTag.Ul)
			{
				CssClass = "rfbGroup"
			};
			webControl.Controls.Add(webControl2);
			WebControl webControl3 = base.CreateRow("");
			webControl2.Controls.Add(webControl3);
			webControl3.Controls.Add(this.CreateSeparator());
			WebControl webControl4 = new WebControl(HtmlTextWriterTag.Label)
			{
				CssClass = "rfbLabel"
			};
			webControl4.Controls.Add(new LiteralControl(base.Localization.AdvancedRepeat));
			webControl3.Controls.Add(webControl4);
			WebControl child = new WebControl(HtmlTextWriterTag.Span)
			{
				CssClass = "rsButton rsAdvRepeat rfbFull",
				ID = "AppointmentRepeat"
			};
			webControl3.Controls.Add(child);
			WebControl webControl5 = base.CreateRow("");
			webControl2.Controls.Add(webControl5);
			WebControl webControl6 = new WebControl(HtmlTextWriterTag.Span);
			webControl5.Controls.Add(webControl6);
			webControl6.CssClass = "rsAdvResetExceptions";
			webControl6.Controls.Add(base.View.ResetExceptions);
		}

		// Token: 0x06004BDC RID: 19420 RVA: 0x000EDDC8 File Offset: 0x000EBFC8
		private WebControl CreateSeparator()
		{
			return new WebControl(HtmlTextWriterTag.Hr)
			{
				CssClass = "rfbSeparator"
			};
		}
	}
}
