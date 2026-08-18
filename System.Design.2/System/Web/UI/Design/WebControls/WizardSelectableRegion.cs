using System;
using System.Web.UI.WebControls;

namespace System.Web.UI.Design.WebControls
{
	// Token: 0x02000138 RID: 312
	internal class WizardSelectableRegion : DesignerRegion
	{
		// Token: 0x06000B4C RID: 2892 RVA: 0x00048FE0 File Offset: 0x000471E0
		internal WizardSelectableRegion(WizardDesigner designer, string name, WizardStepBase wizardStep) : base(designer, name, true)
		{
			this._wizardStep = wizardStep;
		}

		// Token: 0x17000284 RID: 644
		// (get) Token: 0x06000B4D RID: 2893 RVA: 0x00048FF2 File Offset: 0x000471F2
		internal WizardStepBase Step
		{
			get
			{
				return this._wizardStep;
			}
		}

		// Token: 0x040006C3 RID: 1731
		private WizardStepBase _wizardStep;
	}
}
