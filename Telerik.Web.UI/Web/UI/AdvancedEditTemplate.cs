using System;
using System.Collections.Specialized;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x0200080D RID: 2061
	internal class AdvancedEditTemplate : AdvancedTemplate
	{
		// Token: 0x06004B72 RID: 19314 RVA: 0x000EC562 File Offset: 0x000EA762
		public AdvancedEditTemplate(RadScheduler owner, string runtimeSkin) : base(owner, runtimeSkin)
		{
		}

		// Token: 0x06004B73 RID: 19315 RVA: 0x000EC56C File Offset: 0x000EA76C
		protected override void CreateButtons()
		{
			base.View.CreateEditButtons();
			base.Renderer.CreateEditButtons();
		}

		// Token: 0x06004B74 RID: 19316 RVA: 0x000EC584 File Offset: 0x000EA784
		protected override void CreateChildControls(Control container)
		{
			if (base.Owner.RecurrenceSupport)
			{
				base.View.ResetExceptions.DataBinding += this._resetExceptions_DataBinding;
				base.View.ResetExceptions.Click += this._resetExceptions_OnClick;
			}
			base.Renderer.CreateTitle(base.Owner.Localization.AdvancedEditAppointment);
		}

		// Token: 0x06004B75 RID: 19317 RVA: 0x000EC5F4 File Offset: 0x000EA7F4
		private void _resetExceptions_OnClick(object sender, EventArgs e)
		{
			IOrderedDictionary value = this.ExtractValues(null);
			base.Appointment.LoadFromDictionary(value);
			base.Owner.RemoveRecurrenceExceptions(base.Appointment);
			base.Owner.Rebind();
			base.Owner.ShowAdvancedEditForm(base.Appointment, true);
			base.Appointment.Attributes["__ExceptionsReset"] = "true";
		}

		// Token: 0x06004B76 RID: 19318 RVA: 0x000EC660 File Offset: 0x000EA860
		private void _resetExceptions_DataBinding(object sender, EventArgs e)
		{
			SchedulerFormContainer schedulerFormContainer = (SchedulerFormContainer)base.View.ResetExceptions.BindingContainer;
			RecurrenceRule recurrenceRule;
			if (!RecurrenceRule.TryParse(schedulerFormContainer.Appointment.RecurrenceRule, out recurrenceRule))
			{
				base.View.ResetExceptions.Style.Add(HtmlTextWriterStyle.Display, "none");
				return;
			}
			if (!string.IsNullOrEmpty(base.Appointment.Attributes["__ExceptionsReset"]))
			{
				base.View.ResetExceptions.Text = base.Owner.Localization.AdvancedDone;
				base.Appointment.Attributes.Remove("__ExceptionsReset");
				return;
			}
			if (recurrenceRule.Exceptions.Count > 0)
			{
				base.View.ResetExceptions.Text = base.Owner.Localization.AdvancedReset;
				return;
			}
			base.View.ResetExceptions.Visible = false;
		}

		// Token: 0x06004B77 RID: 19319 RVA: 0x000EC746 File Offset: 0x000EA946
		internal override bool IncludeResource(Resource res)
		{
			return res.Available || this.ResourceIsInUse(res);
		}

		// Token: 0x06004B78 RID: 19320 RVA: 0x000EC75C File Offset: 0x000EA95C
		private bool ResourceIsInUse(Resource res)
		{
			foreach (object obj in base.Appointment.Resources)
			{
				Resource o = (Resource)obj;
				if (res == o)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x04001316 RID: 4886
		private const string ExceptionsResetAttributeKey = "__ExceptionsReset";
	}
}
