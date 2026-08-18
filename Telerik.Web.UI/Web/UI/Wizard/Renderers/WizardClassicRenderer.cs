using System;
using System.Web.UI;

namespace Telerik.Web.UI.Wizard.Renderers
{
	// Token: 0x0200099D RID: 2461
	internal class WizardClassicRenderer : WizardDesktopRenderer
	{
		// Token: 0x06005DD0 RID: 24016 RVA: 0x0011EFC1 File Offset: 0x0011D1C1
		public WizardClassicRenderer(RadWizard wizard) : base(wizard)
		{
		}

		// Token: 0x06005DD1 RID: 24017 RVA: 0x0011EFCA File Offset: 0x0011D1CA
		public override void RenderCallOutStepElement(HtmlTextWriter writer)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "rwzCallout");
			writer.RenderBeginTag(HtmlTextWriterTag.Span);
			writer.Write(" ");
			writer.RenderEndTag();
		}
	}
}
