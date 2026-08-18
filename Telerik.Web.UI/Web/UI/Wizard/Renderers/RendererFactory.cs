using System;

namespace Telerik.Web.UI.Wizard.Renderers
{
	// Token: 0x0200099C RID: 2460
	internal static class RendererFactory
	{
		// Token: 0x06005DCF RID: 24015 RVA: 0x0011EF88 File Offset: 0x0011D188
		public static IRenderer CreateWizardRenderer(RadWizard wizard)
		{
			IRenderer result;
			if (wizard.ResolvedRenderMode == RenderMode.Mobile)
			{
				result = new WizardMobileRenderer(wizard);
			}
			else if (wizard.ResolvedRenderMode == RenderMode.Lightweight)
			{
				result = new WizardLiteRenderer(wizard);
			}
			else
			{
				result = new WizardClassicRenderer(wizard);
			}
			return result;
		}
	}
}
