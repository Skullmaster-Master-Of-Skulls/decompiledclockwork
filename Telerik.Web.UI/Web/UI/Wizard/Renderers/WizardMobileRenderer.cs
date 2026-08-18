using System;
using System.Web.UI;

namespace Telerik.Web.UI.Wizard.Renderers
{
	// Token: 0x0200099B RID: 2459
	internal class WizardMobileRenderer : WizardRendererBase
	{
		// Token: 0x06005DC9 RID: 24009 RVA: 0x0011E9A1 File Offset: 0x0011CBA1
		public WizardMobileRenderer(RadWizard wizard) : base(wizard)
		{
		}

		// Token: 0x06005DCA RID: 24010 RVA: 0x0011E9AC File Offset: 0x0011CBAC
		public override void RenderBeginTag(HtmlTextWriter writer)
		{
			string text = string.Empty;
			if (base.Owner.ActiveStep.StepType == RadWizardStepType.Complete)
			{
				text = "rwzHidden";
			}
			writer.AddAttribute(HtmlTextWriterAttribute.Class, RadWizard.Styles.Combine(new string[]
			{
				"rwzHeader",
				text
			}));
			writer.RenderBeginTag(HtmlTextWriterTag.Div);
			if (base.Owner.DisplayNavigationBar && base.Owner.ActiveStep.StepType != RadWizardStepType.Complete)
			{
				this.RenderNavigationBar(writer);
			}
			if (base.Owner.DisplayProgressBar && base.Owner.ActiveStep.StepType != RadWizardStepType.Complete)
			{
				base.RenderProgressBar(writer);
			}
			writer.RenderEndTag();
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "rwzContent");
			writer.RenderBeginTag(HtmlTextWriterTag.Div);
		}

		// Token: 0x06005DCB RID: 24011 RVA: 0x0011EA6A File Offset: 0x0011CC6A
		public override void RenderEndTag(HtmlTextWriter writer)
		{
			writer.RenderEndTag();
			this.RenderButtonsNavigation(writer);
		}

		// Token: 0x06005DCC RID: 24012 RVA: 0x0011EA7C File Offset: 0x0011CC7C
		public override void RenderNavigationBar(HtmlTextWriter writer)
		{
			bool flag = false;
			int num = 0;
			foreach (object obj in base.Owner.WizardSteps)
			{
				RadWizardStep radWizardStep = (RadWizardStep)obj;
				if (!string.IsNullOrEmpty(radWizardStep.ImageUrl) || !string.IsNullOrEmpty(radWizardStep.SpriteCssClass))
				{
					flag = true;
				}
				if (radWizardStep.StepType == RadWizardStepType.Complete)
				{
					num++;
				}
			}
			if (flag)
			{
				string text = (base.Owner.ImagePosition == RadWizardImagePostion.Left) ? "rwzLeftImages" : "rwzRightImages";
				writer.AddAttribute(HtmlTextWriterAttribute.Class, RadWizard.Styles.Combine(new string[]
				{
					"rwzBreadCrumb",
					text
				}));
			}
			else
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Class, "rwzBreadCrumb");
			}
			writer.RenderBeginTag(HtmlTextWriterTag.Div);
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "rwzUL");
			writer.RenderBeginTag(HtmlTextWriterTag.Ul);
			base.RenderNavigationBarButtons(writer, num);
			writer.RenderEndTag();
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "rwzPager");
			writer.RenderBeginTag(HtmlTextWriterTag.Span);
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "rwzText");
			writer.RenderBeginTag(HtmlTextWriterTag.Span);
			int num2 = base.Owner.WizardSteps.Count - num;
			writer.Write(base.Owner.ActiveStepIndex + 1 + "/" + num2);
			writer.RenderEndTag();
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "rwzCallout");
			writer.RenderBeginTag(HtmlTextWriterTag.Span);
			writer.RenderEndTag();
			writer.RenderEndTag();
			writer.RenderEndTag();
		}

		// Token: 0x06005DCD RID: 24013 RVA: 0x0011EC18 File Offset: 0x0011CE18
		public override void RenderStepButton(HtmlTextWriter writer, RadWizardStep step, int numberOFCompleteSteps)
		{
			string text = "rwzLI";
			if (step.Active)
			{
				text = RadWizard.Styles.Combine(new string[]
				{
					text,
					"rwzSelected"
				});
			}
			if (step.Index == 0)
			{
				text = RadWizard.Styles.Combine(new string[]
				{
					text,
					"rwzFirst"
				});
			}
			if (step.Index == base.Owner.WizardSteps.Count - numberOFCompleteSteps - 1)
			{
				text = RadWizard.Styles.Combine(new string[]
				{
					text,
					"rwzLast"
				});
			}
			if (!step.Enabled)
			{
				text = RadWizard.Styles.Combine(new string[]
				{
					text,
					"rwzDisabled"
				});
			}
			writer.Write(writer.NewLine);
			writer.AddAttribute(HtmlTextWriterAttribute.Class, text);
			writer.RenderBeginTag(HtmlTextWriterTag.Li);
			string value = "rwzLink";
			writer.AddAttribute(HtmlTextWriterAttribute.Class, value);
			writer.AddAttribute(HtmlTextWriterAttribute.Href, "#");
			if (!string.IsNullOrEmpty(step.ToolTip))
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Title, step.ToolTip);
			}
			writer.RenderBeginTag(HtmlTextWriterTag.A);
			base.RenderImage(writer, step);
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "rwzText");
			writer.RenderBeginTag(HtmlTextWriterTag.Span);
			string value2 = (step.Title != string.Empty) ? step.Title : step.ID;
			writer.Write(value2);
			writer.RenderEndTag();
			writer.RenderEndTag();
			writer.RenderEndTag();
		}

		// Token: 0x06005DCE RID: 24014 RVA: 0x0011ED88 File Offset: 0x0011CF88
		public override void RenderButtonsNavigation(HtmlTextWriter writer)
		{
			bool flag = base.Owner.RenderedSteps == RadWizardRenderedSteps.Active;
			string text = string.Empty;
			if (base.Owner.ActiveStep.StepType == RadWizardStepType.Complete)
			{
				text = "rwzHidden";
			}
			writer.AddAttribute(HtmlTextWriterAttribute.Class, RadWizard.Styles.Combine(new string[]
			{
				"rwzFooter",
				text
			}));
			writer.RenderBeginTag(HtmlTextWriterTag.Div);
			if (base.Owner.DisplayNavigationButtons)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Class, "rwzNav");
				writer.RenderBeginTag(HtmlTextWriterTag.Ul);
				RadWizardStep activeStep = base.Owner.ActiveStep;
				RadWizardStepType stepType = base.Owner.GetStepType(activeStep);
				if (!flag)
				{
					base.RenderNavigationButton(writer, activeStep, "rwzPrevious", string.Empty, base.Owner.Localization.Previous);
					base.RenderNavigationButton(writer, activeStep, "rwzNext", "rwzRight", base.Owner.Localization.Next);
					base.RenderNavigationButton(writer, activeStep, "rwzFinish", "rwzRight", base.Owner.Localization.Finish);
				}
				else if (stepType != RadWizardStepType.Complete)
				{
					if ((stepType == RadWizardStepType.Step || stepType == RadWizardStepType.Finish) && activeStep.Index != 0)
					{
						base.RenderNavigationButton(writer, activeStep, "rwzPrevious", string.Empty, base.Owner.Localization.Previous);
					}
					if (stepType == RadWizardStepType.Step || stepType == RadWizardStepType.Start)
					{
						base.RenderNavigationButton(writer, activeStep, "rwzNext", "rwzRight", base.Owner.Localization.Next);
					}
					if (stepType == RadWizardStepType.Finish)
					{
						base.RenderNavigationButton(writer, activeStep, "rwzFinish", "rwzRight", base.Owner.Localization.Finish);
					}
				}
				writer.RenderEndTag();
				if (activeStep.ResolvedDisplayCancelButton)
				{
					writer.AddAttribute(HtmlTextWriterAttribute.Class, "rwzCancelWrapper");
					writer.RenderBeginTag(HtmlTextWriterTag.Div);
					writer.AddAttribute(HtmlTextWriterAttribute.Class, "rwzCancelBtn");
					writer.RenderBeginTag(HtmlTextWriterTag.Span);
					writer.Write(base.Owner.Localization.Cancel);
					writer.RenderEndTag();
					writer.RenderEndTag();
				}
			}
			writer.RenderEndTag();
		}
	}
}
