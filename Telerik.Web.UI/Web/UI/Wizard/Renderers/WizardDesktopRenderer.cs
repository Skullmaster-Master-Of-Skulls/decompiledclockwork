using System;
using System.Web.UI;

namespace Telerik.Web.UI.Wizard.Renderers
{
	// Token: 0x02000999 RID: 2457
	internal class WizardDesktopRenderer : WizardRendererBase
	{
		// Token: 0x06005DC0 RID: 24000 RVA: 0x0011E32F File Offset: 0x0011C52F
		public WizardDesktopRenderer(RadWizard wizard) : base(wizard)
		{
		}

		// Token: 0x06005DC1 RID: 24001 RVA: 0x0011E338 File Offset: 0x0011C538
		public override void RenderBeginTag(HtmlTextWriter writer)
		{
			if (base.Owner.DisplayNavigationBar && base.Owner.NavigationBarPosition != RadWizardNavigationBarPosition.Bottom && base.Owner.ActiveStep.StepType != RadWizardStepType.Complete)
			{
				this.RenderNavigationBar(writer);
			}
			if (base.Owner.DisplayProgressBar && base.Owner.ActiveStep.StepType != RadWizardStepType.Complete && (base.Owner.NavigationBarPosition == RadWizardNavigationBarPosition.Left || base.Owner.NavigationBarPosition == RadWizardNavigationBarPosition.Right || (base.Owner.NavigationBarPosition == RadWizardNavigationBarPosition.Bottom && base.Owner.ProgressBarPosition == RadWizardProgressBarPosition.Top) || (base.Owner.NavigationBarPosition == RadWizardNavigationBarPosition.Top && base.Owner.ProgressBarPosition == RadWizardProgressBarPosition.Top)))
			{
				base.RenderProgressBar(writer);
			}
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "rwzContentWrapper");
			writer.RenderBeginTag(HtmlTextWriterTag.Div);
			if (base.Owner.DisplayNavigationButtons && base.Owner.NavigationButtonsPosition == RadWizardNavigationButtonsPosition.Top)
			{
				this.RenderButtonsNavigation(writer);
			}
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "rwzContent");
			writer.RenderBeginTag(HtmlTextWriterTag.Div);
		}

		// Token: 0x06005DC2 RID: 24002 RVA: 0x0011E440 File Offset: 0x0011C640
		public override void RenderEndTag(HtmlTextWriter writer)
		{
			writer.RenderEndTag();
			if (base.Owner.DisplayNavigationButtons && base.Owner.NavigationButtonsPosition == RadWizardNavigationButtonsPosition.Bottom)
			{
				this.RenderButtonsNavigation(writer);
			}
			writer.RenderEndTag();
			if (base.Owner.DisplayProgressBar && base.Owner.ActiveStep.StepType != RadWizardStepType.Complete && ((base.Owner.NavigationBarPosition == RadWizardNavigationBarPosition.Bottom && base.Owner.ProgressBarPosition == RadWizardProgressBarPosition.Bottom) || (base.Owner.NavigationBarPosition == RadWizardNavigationBarPosition.Top && base.Owner.ProgressBarPosition == RadWizardProgressBarPosition.Bottom)))
			{
				base.RenderProgressBar(writer);
			}
			if (base.Owner.DisplayNavigationBar && base.Owner.NavigationBarPosition == RadWizardNavigationBarPosition.Bottom && base.Owner.ActiveStep.StepType != RadWizardStepType.Complete)
			{
				this.RenderNavigationBar(writer);
			}
		}

		// Token: 0x06005DC3 RID: 24003 RVA: 0x0011E510 File Offset: 0x0011C710
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
			writer.RenderBeginTag(HtmlTextWriterTag.Ul);
			base.RenderNavigationBarButtons(writer, num);
			writer.RenderEndTag();
		}

		// Token: 0x06005DC4 RID: 24004 RVA: 0x0011E604 File Offset: 0x0011C804
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
			if (!string.IsNullOrEmpty(step.CssClass))
			{
				text = RadWizard.Styles.Combine(new string[]
				{
					text,
					step.CssClass
				});
			}
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
			if (base.Owner.NavigationBarPosition == RadWizardNavigationBarPosition.Right || base.Owner.NavigationBarPosition == RadWizardNavigationBarPosition.Left || step.Index != base.Owner.WizardSteps.Count - numberOFCompleteSteps - 1)
			{
				this.RenderCallOutStepElement(writer);
			}
			writer.RenderEndTag();
			writer.RenderEndTag();
		}

		// Token: 0x06005DC5 RID: 24005 RVA: 0x0011E7CF File Offset: 0x0011C9CF
		public virtual void RenderCallOutStepElement(HtmlTextWriter writer)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06005DC6 RID: 24006 RVA: 0x0011E7D8 File Offset: 0x0011C9D8
		public override void RenderButtonsNavigation(HtmlTextWriter writer)
		{
			bool flag = base.Owner.RenderedSteps == RadWizardRenderedSteps.Active;
			RadWizardStep activeStep = base.Owner.ActiveStep;
			RadWizardStepType stepType = base.Owner.GetStepType(activeStep);
			string text = string.Empty;
			if (stepType == RadWizardStepType.Complete)
			{
				text = "rwzHidden";
			}
			writer.AddAttribute(HtmlTextWriterAttribute.Class, RadWizard.Styles.Combine(new string[]
			{
				"rwzNav",
				text
			}));
			writer.RenderBeginTag(HtmlTextWriterTag.Ul);
			if (!flag)
			{
				base.RenderNavigationButton(writer, activeStep, "rwzCancel", string.Empty, base.Owner.Localization.Cancel);
				base.RenderNavigationButton(writer, activeStep, "rwzFinish", "rwzRight", base.Owner.Localization.Finish);
				base.RenderNavigationButton(writer, activeStep, "rwzNext", "rwzRight", base.Owner.Localization.Next);
				base.RenderNavigationButton(writer, activeStep, "rwzPrevious", "rwzRight", base.Owner.Localization.Previous);
			}
			else if (stepType != RadWizardStepType.Complete)
			{
				if (activeStep.ResolvedDisplayCancelButton)
				{
					base.RenderNavigationButton(writer, activeStep, "rwzCancel", string.Empty, base.Owner.Localization.Cancel);
				}
				if (stepType == RadWizardStepType.Finish)
				{
					base.RenderNavigationButton(writer, activeStep, "rwzFinish", "rwzRight", base.Owner.Localization.Finish);
				}
				if (stepType == RadWizardStepType.Step || stepType == RadWizardStepType.Start)
				{
					base.RenderNavigationButton(writer, activeStep, "rwzNext", "rwzRight", base.Owner.Localization.Next);
				}
				if ((stepType == RadWizardStepType.Step || stepType == RadWizardStepType.Finish) && activeStep.Index != 0)
				{
					base.RenderNavigationButton(writer, activeStep, "rwzPrevious", "rwzRight", base.Owner.Localization.Previous);
				}
			}
			writer.RenderEndTag();
		}
	}
}
