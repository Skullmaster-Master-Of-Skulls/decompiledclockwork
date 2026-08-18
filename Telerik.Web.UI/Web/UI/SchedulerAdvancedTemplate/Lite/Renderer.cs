using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI.SchedulerAdvancedTemplate.Lite
{
	// Token: 0x02000812 RID: 2066
	internal class Renderer : RendererBase
	{
		// Token: 0x06004BC7 RID: 19399 RVA: 0x000ED71A File Offset: 0x000EB91A
		public Renderer(IAdvancedTemplateView view) : base(view)
		{
		}

		// Token: 0x06004BC8 RID: 19400 RVA: 0x000ED724 File Offset: 0x000EB924
		public override void CreateLayout(Control container)
		{
			Panel panel = new Panel();
			container.Controls.Add(panel);
			panel.CssClass = "rsDialog rsAdvancedEdit";
			base.TitleBarOuterPanel = new Panel();
			panel.Controls.Add(base.TitleBarOuterPanel);
			base.TitleBarOuterPanel.CssClass = string.Format("{0} {1}", "rsAdvTitle", "rsTitle");
			base.TitleBarInnerPanel = new Panel();
			base.TitleBarOuterPanel.Controls.Add(base.TitleBarInnerPanel);
			base.TitleBarInnerPanel.CssClass = "rsAdvInnerTitle";
			Panel panel2 = new Panel();
			panel.Controls.Add(panel2);
			panel2.CssClass = string.Format("{0} {1}", "rsAdvContentWrapper", "rsBody");
			base.OptionsPanelScroll = new Panel();
			panel2.Controls.Add(base.OptionsPanelScroll);
			base.OptionsPanelScroll.CssClass = "rsAdvOptionsScroll";
			base.OptionsPanelScroll.DataBinding += base.ControlsPanel_DataBinding;
			base.ButtonsPanel = new Panel();
			panel2.Controls.Add(base.ButtonsPanel);
			base.ButtonsPanel.ID = "ButtonsPanel";
			base.ButtonsPanel.CssClass = string.Format("{0} {1}", "rsAdvButtonWrapper", "rsButtons");
		}

		// Token: 0x06004BC9 RID: 19401 RVA: 0x000ED874 File Offset: 0x000EBA74
		public override void CreateControls(Control container)
		{
			base.CreateCloseButton();
			base.CreateSharedCalendar(container);
			this.CreateAppointmentControls();
			if (base.ParentScheduler.RecurrenceSupport)
			{
				this.CreateAppointmentRecurrenceControls();
			}
		}

		// Token: 0x06004BCA RID: 19402 RVA: 0x000ED89C File Offset: 0x000EBA9C
		public override void CreateTitle(string title)
		{
			base.TitleBarInnerPanel.Controls.AddAt(0, new LiteralControl(title));
		}

		// Token: 0x06004BCB RID: 19403 RVA: 0x000ED8B8 File Offset: 0x000EBAB8
		protected void CreateAppointmentControls()
		{
			WebControl webControl = new WebControl(HtmlTextWriterTag.Ul)
			{
				CssClass = "rfbGroup"
			};
			base.OptionsPanelScroll.Controls.Add(webControl);
			this.CreateSubjectRow(webControl);
			base.CreateStartTimeRow(webControl);
			base.CreateEndTimeRow(webControl);
			this.CreateAppointmentValidators(webControl);
			this.CreateAllDayRow(webControl);
			base.CreateTimeZonesRow(webControl);
			base.CreateReminderRow(webControl);
			if (base.ParentScheduler.AdvancedForm.EnableResourceEditing)
			{
				base.CreateResourceRows(webControl);
			}
			if (base.ParentScheduler.HasDescriptionField)
			{
				this.CreateDescriptionRow(webControl);
			}
			if (base.ParentScheduler.AdvancedForm.EnableCustomAttributeEditing)
			{
				base.CreateAttributeRows(webControl);
			}
		}

		// Token: 0x06004BCC RID: 19404 RVA: 0x000ED964 File Offset: 0x000EBB64
		protected void CreateAppointmentValidators(WebControl container)
		{
			WebControl webControl = base.CreateRow("");
			container.Controls.Add(webControl);
			base.CreateControlsValidators(webControl);
		}

		// Token: 0x06004BCD RID: 19405 RVA: 0x000ED990 File Offset: 0x000EBB90
		protected override void CreateAllDayRow(WebControl container)
		{
			WebControl webControl = this.CreateCompactRow("rsAllDayWrapper");
			container.Controls.Add(webControl);
			base.View.AllDayEvent.Text = "";
			WebControl label = base.CreateLabel(webControl, base.Localization.AdvancedAllDayEvent);
			webControl.Controls.Add(base.View.AllDayEvent);
			base.AttachLabel(label, base.View.AllDayEvent);
		}

		// Token: 0x06004BCE RID: 19406 RVA: 0x000EDA05 File Offset: 0x000EBC05
		protected override WebControl CreateCompactRow(string className = "")
		{
			return base.CreateRow(className);
		}
	}
}
