using System;
using System.Web.UI.WebControls;

namespace System.Web.UI.Design.WebControls
{
	// Token: 0x02000503 RID: 1283
	internal class WizardSelectableRegion : DesignerRegion
	{
		// Token: 0x06002DC0 RID: 11712 RVA: 0x00103737 File Offset: 0x00102737
		internal WizardSelectableRegion(WizardDesigner designer, string name, WizardStepBase wizardStep) : base(designer, name, true)
		{
			this._wizardStep = wizardStep;
		}

		// Token: 0x170008A1 RID: 2209
		// (get) Token: 0x06002DC1 RID: 11713 RVA: 0x00103749 File Offset: 0x00102749
		internal WizardStepBase Step
		{
			get
			{
				return this._wizardStep;
			}
		}

		// Token: 0x04001F15 RID: 7957
		private WizardStepBase _wizardStep;
	}
}
