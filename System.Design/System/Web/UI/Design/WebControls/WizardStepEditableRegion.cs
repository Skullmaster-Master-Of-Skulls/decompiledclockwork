using System;
using System.Web.UI.WebControls;

namespace System.Web.UI.Design.WebControls
{
	// Token: 0x02000500 RID: 1280
	public class WizardStepEditableRegion : EditableDesignerRegion, IWizardStepEditableRegion
	{
		// Token: 0x06002DB9 RID: 11705 RVA: 0x00103600 File Offset: 0x00102600
		public WizardStepEditableRegion(WizardDesigner designer, WizardStepBase wizardStep) : base(designer, designer.GetRegionName(wizardStep), false)
		{
			this._wizardStep = wizardStep;
			base.EnsureSize = true;
		}

		// Token: 0x1700089E RID: 2206
		// (get) Token: 0x06002DBA RID: 11706 RVA: 0x0010361F File Offset: 0x0010261F
		public WizardStepBase Step
		{
			get
			{
				return this._wizardStep;
			}
		}

		// Token: 0x04001F12 RID: 7954
		private WizardStepBase _wizardStep;
	}
}
