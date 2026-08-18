using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI.Scheduling;

namespace Telerik.Web.UI.Scheduler.Views.Agenda
{
	// Token: 0x02000822 RID: 2082
	internal class AgendaViewAppointmentControl : AppointmentControl
	{
		// Token: 0x06004D07 RID: 19719 RVA: 0x000F2645 File Offset: 0x000F0845
		internal AgendaViewAppointmentControl(Appointment appointment) : this(appointment, true)
		{
		}

		// Token: 0x06004D08 RID: 19720 RVA: 0x000F264F File Offset: 0x000F084F
		internal AgendaViewAppointmentControl(Appointment appointment, bool registerWithAppointment) : base(appointment, registerWithAppointment)
		{
			this.Initialize();
		}

		// Token: 0x06004D09 RID: 19721 RVA: 0x000F2660 File Offset: 0x000F0860
		protected override void ApplyAppointmentStyles()
		{
			this.ForeColor = base.Appointment.ForeColor;
			this.Font.CopyFrom(base.Appointment.Font);
			this.CssClass = base.GetClassName();
			this.ToolTip = base.Appointment.ToolTip;
		}

		// Token: 0x06004D0A RID: 19722 RVA: 0x000F26B4 File Offset: 0x000F08B4
		protected override void Initialize()
		{
			this.ID = string.Format("{0}_{1}", this._appointment.Owner.Appointments.IndexOf(this._appointment), this._appointment.AppointmentControls.IndexOf(this));
			this.AddContents(this);
		}

		// Token: 0x06004D0B RID: 19723 RVA: 0x000F2710 File Offset: 0x000F0910
		private void AddContents(Control container)
		{
			this._contentWrap = new WebControl(HtmlTextWriterTag.Div);
			this._contentWrap.CssClass = "rsAptContent";
			container.Controls.Add(this._contentWrap);
			if (base.Appointment.Owner.AgendaView.ResourceMarkerType != ResourceMarkerType.None && this._appointment.Resources.Count > 0)
			{
				this.AddResourceMarker(this._contentWrap);
			}
			base.AppointmentContainer = new SchedulerAppointmentContainer(this._appointment.Owner);
			this._contentWrap.Controls.Add(base.AppointmentContainer);
			base.AppointmentContainer.Appointment = this._appointment;
			if (!this._appointment.Owner.DesignMode)
			{
				base.AppointmentContainer.Template = this._appointment.Owner.AppointmentTemplate;
			}
			else
			{
				base.AppointmentContainer.Template = new AppointmentTemplate(this._appointment.Owner);
			}
			base.AppointmentContainer.Template.InstantiateIn(base.AppointmentContainer);
			if (!base.Appointment.Owner.ActiveModel.ReadOnly)
			{
				if (base.IsLightweight)
				{
					base.AddDeleteCommand(container);
					return;
				}
				base.AddDeleteCommand(this._contentWrap);
			}
		}

		// Token: 0x06004D0C RID: 19724 RVA: 0x000F2854 File Offset: 0x000F0A54
		private void AddResourceMarker(Control container)
		{
			WebControl webControl = new WebControl(HtmlTextWriterTag.Span);
			string arg = (base.Appointment.Owner.AgendaView.ResourceMarkerType == ResourceMarkerType.Block) ? "rsMarkerBlock" : "rsMarkerBar";
			webControl.CssClass = string.Format("{0} {1}", "rsResourceMarker", arg);
			container.Controls.Add(webControl);
		}

		// Token: 0x06004D0D RID: 19725 RVA: 0x000F28B0 File Offset: 0x000F0AB0
		protected override Unit GetHeight()
		{
			return base.Appointment.Owner.RowHeight;
		}

		// Token: 0x06004D0E RID: 19726 RVA: 0x000F28C2 File Offset: 0x000F0AC2
		protected override Unit GetWidth()
		{
			return Unit.Percentage(100.0);
		}

		// Token: 0x04001352 RID: 4946
		private WebControl _contentWrap;
	}
}
