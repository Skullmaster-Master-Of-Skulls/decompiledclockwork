using System;
using System.Collections.Specialized;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI.Scheduling
{
	// Token: 0x02001325 RID: 4901
	internal abstract class InlineTemplate : IBindableTemplate, ITemplate
	{
		// Token: 0x170041DD RID: 16861
		// (get) Token: 0x0600CCC3 RID: 52419 RVA: 0x002DA3FE File Offset: 0x002D85FE
		protected RadScheduler Owner
		{
			get
			{
				return this._owner;
			}
		}

		// Token: 0x0600CCC4 RID: 52420 RVA: 0x002DA406 File Offset: 0x002D8606
		protected InlineTemplate(RadScheduler owner)
		{
			this._owner = owner;
		}

		// Token: 0x0600CCC5 RID: 52421 RVA: 0x002DA418 File Offset: 0x002D8618
		public void InstantiateIn(Control container)
		{
			this.CreateSubjectBox(container);
			this._renderDeleteButton = ((SchedulerAppointmentContainer)container).Appointment.AllowDelete;
			WebControl webControl = new WebControl(HtmlTextWriterTag.Div);
			webControl.CssClass = "rsEditOptions";
			if (this.Owner.ResolvedRenderMode == RenderMode.Lightweight)
			{
				webControl.CssClass = "rsButtons";
			}
			container.Controls.Add(webControl);
			this.CreateChildControls(webControl);
			this.CreateCommonButtons(webControl);
		}

		// Token: 0x0600CCC6 RID: 52422 RVA: 0x002DA488 File Offset: 0x002D8688
		private void CreateSubjectBox(Control container)
		{
			WebControl webControl = new WebControl(HtmlTextWriterTag.Div);
			container.Controls.Add(webControl);
			webControl.CssClass = "rsAptEditTextareaWrapper";
			if (this.Owner.ResolvedRenderMode == RenderMode.Lightweight)
			{
				WebControl webControl2 = webControl;
				webControl2.CssClass += " rsTextarea";
			}
			TextBox textBox = new TextBox();
			textBox.CssClass = "radPreventDecorate";
			webControl.Controls.Add(textBox);
			textBox.ID = "SubjectTextBox";
			textBox.TextMode = TextBoxMode.MultiLine;
			if (this.Owner.ResolvedRenderMode == RenderMode.Mobile)
			{
				textBox.Rows = 5;
			}
			if (this.Owner.Page != null)
			{
				ScriptManager current = ScriptManager.GetCurrent(this.Owner.Page);
				if (current != null)
				{
					current.SetFocus(textBox.ClientID);
				}
			}
			textBox.DataBinding += InlineTemplate.subjectTextBox_DataBinding;
		}

		// Token: 0x0600CCC7 RID: 52423 RVA: 0x002DA55C File Offset: 0x002D875C
		private void CreateCommonButtons(Control optionsSpan)
		{
			LinkButton linkButton = new LinkButton();
			optionsSpan.Controls.Add(linkButton);
			linkButton.CssClass = "rsAptEditCancel";
			if (this.Owner.ResolvedRenderMode == RenderMode.Lightweight)
			{
				LinkButton linkButton2 = linkButton;
				linkButton2.CssClass += " rsButton";
			}
			linkButton.ID = "cancel";
			linkButton.CommandName = "Cancel";
			linkButton.Text = this.Owner.Localization.Cancel;
			if (this.Owner.AdvancedForm.Enabled)
			{
				LinkButton linkButton3 = new LinkButton();
				optionsSpan.Controls.Add(linkButton3);
				linkButton3.CssClass = "rsAptEditMore";
				if (this.Owner.ResolvedRenderMode == RenderMode.Lightweight)
				{
					LinkButton linkButton4 = linkButton3;
					linkButton4.CssClass += " rsButton";
				}
				linkButton3.CommandName = "More";
				linkButton3.Text = this.Owner.Localization.ShowAdvancedForm;
				linkButton3.ID = "more";
			}
		}

		// Token: 0x0600CCC8 RID: 52424
		protected abstract void CreateChildControls(Control container);

		// Token: 0x0600CCC9 RID: 52425 RVA: 0x002DA658 File Offset: 0x002D8858
		private static void subjectTextBox_DataBinding(object sender, EventArgs e)
		{
			TextBox textBox = (TextBox)sender;
			SchedulerFormContainer schedulerFormContainer = (SchedulerFormContainer)textBox.BindingContainer;
			textBox.Text = Convert.ToString(DataBinder.Eval(schedulerFormContainer.Appointment, "Subject"));
		}

		// Token: 0x0600CCCA RID: 52426 RVA: 0x002DA694 File Offset: 0x002D8894
		public IOrderedDictionary ExtractValues(Control container)
		{
			OrderedDictionary orderedDictionary = new OrderedDictionary();
			TextBox textBox = (TextBox)container.FindControl("SubjectTextBox");
			orderedDictionary["Subject"] = textBox.Text;
			return orderedDictionary;
		}

		// Token: 0x04003694 RID: 13972
		private const string SubjectTextBoxID = "SubjectTextBox";

		// Token: 0x04003695 RID: 13973
		private readonly RadScheduler _owner;

		// Token: 0x04003696 RID: 13974
		protected bool _renderDeleteButton;
	}
}
