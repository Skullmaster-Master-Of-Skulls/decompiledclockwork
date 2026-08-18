using System;
using System.Web.UI;
using Telerik.Web.UI.Renderers;

namespace Telerik.Web.UI
{
	// Token: 0x02000998 RID: 2456
	public class WizardRendererBase : RendererBase
	{
		// Token: 0x17001EDF RID: 7903
		// (get) Token: 0x06005DB1 RID: 23985 RVA: 0x0011DE8D File Offset: 0x0011C08D
		// (set) Token: 0x06005DB2 RID: 23986 RVA: 0x0011DE95 File Offset: 0x0011C095
		protected RadWizard Owner { get; set; }

		// Token: 0x06005DB3 RID: 23987 RVA: 0x0011DE9E File Offset: 0x0011C09E
		public WizardRendererBase(RadWizard owner)
		{
			this.Owner = owner;
		}

		// Token: 0x17001EE0 RID: 7904
		// (get) Token: 0x06005DB4 RID: 23988 RVA: 0x0011DEB0 File Offset: 0x0011C0B0
		public override string CssClassFormatString
		{
			get
			{
				string text = RadWizard.Styles.Combine(new string[]
				{
					"RadWizard",
					"RadWizard_{0}"
				});
				if (!this.Owner.IsControlEnabled)
				{
					text = RadWizard.Styles.Combine(new string[]
					{
						text,
						"rwzDisabled"
					});
				}
				if (this.Owner.Attributes["dir"] == "rtl")
				{
					text = RadWizard.Styles.Combine(new string[]
					{
						text,
						"RadWizard_rtl"
					});
				}
				switch (this.Owner.NavigationBarPosition)
				{
				case RadWizardNavigationBarPosition.Right:
					text = RadWizard.Styles.Combine(new string[]
					{
						text,
						"rwzVertical",
						"rwzRightBreadCrumb"
					});
					if (this.Owner.ProgressBarPosition == RadWizardProgressBarPosition.Left)
					{
						text = RadWizard.Styles.Combine(new string[]
						{
							text,
							"rwzLeftProgressBar"
						});
					}
					break;
				case RadWizardNavigationBarPosition.Bottom:
					text = RadWizard.Styles.Combine(new string[]
					{
						text,
						"rwzHorizontal",
						"rwzBottomBreadCrumb"
					});
					if (this.Owner.ProgressBarPosition == RadWizardProgressBarPosition.Top)
					{
						text = RadWizard.Styles.Combine(new string[]
						{
							text,
							"rwzTopProgressBar"
						});
					}
					break;
				case RadWizardNavigationBarPosition.Left:
					text = RadWizard.Styles.Combine(new string[]
					{
						text,
						"rwzVertical"
					});
					if (this.Owner.ProgressBarPosition == RadWizardProgressBarPosition.Right)
					{
						text = RadWizard.Styles.Combine(new string[]
						{
							text,
							"rwzRightProgressBar"
						});
					}
					break;
				case RadWizardNavigationBarPosition.Top:
					text = RadWizard.Styles.Combine(new string[]
					{
						text,
						"rwzHorizontal"
					});
					if (this.Owner.ProgressBarPosition == RadWizardProgressBarPosition.Bottom)
					{
						text = RadWizard.Styles.Combine(new string[]
						{
							text,
							"rwzBottomProgressBar"
						});
					}
					break;
				}
				if (this.Owner.WizardSteps.Count > 0 && this.Owner.ActiveStep.StepType == RadWizardStepType.Complete)
				{
					text = RadWizard.Styles.Combine(new string[]
					{
						text,
						"rwzComplete"
					});
				}
				return text;
			}
		}

		// Token: 0x17001EE1 RID: 7905
		// (get) Token: 0x06005DB5 RID: 23989 RVA: 0x0011E0F6 File Offset: 0x0011C2F6
		public override HtmlTextWriterTag TagKey
		{
			get
			{
				return HtmlTextWriterTag.Div;
			}
		}

		// Token: 0x06005DB6 RID: 23990 RVA: 0x0011E0FA File Offset: 0x0011C2FA
		public override void AddAttributesToRender(HtmlTextWriter writer)
		{
			base.AddAttributesToRender(writer);
		}

		// Token: 0x06005DB7 RID: 23991 RVA: 0x0011E103 File Offset: 0x0011C303
		public void RenderProgressBar(HtmlTextWriter writer)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "rwzProgressBar");
			writer.RenderBeginTag(HtmlTextWriterTag.Div);
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "rwzProgress");
			writer.RenderBeginTag(HtmlTextWriterTag.Span);
			writer.RenderEndTag();
			writer.RenderEndTag();
		}

		// Token: 0x06005DB8 RID: 23992 RVA: 0x0011E13C File Offset: 0x0011C33C
		protected void RenderNavigationBarButtons(HtmlTextWriter writer, int numberOFCompleteSteps)
		{
			for (int i = 0; i < this.Owner.WizardSteps.Count; i++)
			{
				if (this.Owner.WizardSteps[i].StepType != RadWizardStepType.Complete)
				{
					this.RenderStepButton(writer, this.Owner.WizardSteps[i], numberOFCompleteSteps);
				}
			}
		}

		// Token: 0x06005DB9 RID: 23993 RVA: 0x0011E196 File Offset: 0x0011C396
		public virtual void RenderStepButton(HtmlTextWriter writer, RadWizardStep step, int numberOFCompleteSteps)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06005DBA RID: 23994 RVA: 0x0011E1A0 File Offset: 0x0011C3A0
		protected void RenderImage(HtmlTextWriter writer, RadWizardStep step)
		{
			if (!string.IsNullOrEmpty(step.CurrentImageUrl))
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Class, "rwzImage");
				writer.AddAttribute(HtmlTextWriterAttribute.Alt, string.Empty);
				writer.AddAttribute(HtmlTextWriterAttribute.Src, step.CurrentImageUrl);
				writer.RenderBeginTag(HtmlTextWriterTag.Img);
				writer.RenderEndTag();
				return;
			}
			if (!string.IsNullOrEmpty(step.SpriteCssClass))
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Class, RadWizard.Styles.Combine(new string[]
				{
					"rwzImage",
					step.SpriteCssClass
				}));
				writer.RenderBeginTag(HtmlTextWriterTag.Span);
				writer.RenderEndTag();
			}
		}

		// Token: 0x06005DBB RID: 23995 RVA: 0x0011E231 File Offset: 0x0011C431
		public virtual void RenderNavigationBar(HtmlTextWriter writer)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06005DBC RID: 23996 RVA: 0x0011E238 File Offset: 0x0011C438
		public virtual void RenderButtonsNavigation(HtmlTextWriter writer)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06005DBD RID: 23997 RVA: 0x0011E23F File Offset: 0x0011C43F
		public virtual void RenderBeginTag(HtmlTextWriter writer)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06005DBE RID: 23998 RVA: 0x0011E246 File Offset: 0x0011C446
		public virtual void RenderEndTag(HtmlTextWriter writer)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06005DBF RID: 23999 RVA: 0x0011E250 File Offset: 0x0011C450
		protected void RenderNavigationButton(HtmlTextWriter writer, RadWizardStep activeStep, string linkCssClass, string liCssClass, string buttonText)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Class, RadWizard.Styles.Combine(new string[]
			{
				"rwzLI",
				liCssClass
			}));
			writer.RenderBeginTag(HtmlTextWriterTag.Li);
			if (!activeStep.Enabled)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Class, RadWizard.Styles.Combine(new string[]
				{
					"rwzButton",
					"rwzDisabled",
					linkCssClass
				}));
				writer.AddAttribute("disabled", "disabled");
			}
			else
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Class, RadWizard.Styles.Combine(new string[]
				{
					"rwzButton",
					linkCssClass
				}));
			}
			writer.AddAttribute(HtmlTextWriterAttribute.Type, "button");
			writer.RenderBeginTag(HtmlTextWriterTag.Button);
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "rwzText");
			writer.RenderBeginTag(HtmlTextWriterTag.Span);
			writer.Write(buttonText);
			writer.RenderEndTag();
			writer.RenderEndTag();
			writer.RenderEndTag();
		}
	}
}
