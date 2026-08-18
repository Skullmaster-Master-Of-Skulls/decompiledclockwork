using System;
using System.Web.UI.WebControls;

namespace System.Web.UI.Design.WebControls
{
	// Token: 0x02000135 RID: 309
	public class WizardStepEditableRegion : EditableDesignerRegion, IWizardStepEditableRegion
	{
		// Token: 0x06000B45 RID: 2885 RVA: 0x00048EAA File Offset: 0x000470AA
		public WizardStepEditableRegion(WizardDesigner designer, WizardStepBase wizardStep) : base(designer, designer.GetRegionName(wizardStep), false)
		{
			this._wizardStep = wizardStep;
			base.EnsureSize = true;
		}

		// Token: 0x17000281 RID: 641
		// (get) Token: 0x06000B46 RID: 2886 RVA: 0x00048EC9 File Offset: 0x000470C9
		public WizardStepBase Step
		{
			get
			{
				return this._wizardStep;
			}
		}

		// Token: 0x040006C0 RID: 1728
		private WizardStepBase _wizardStep;
	}
}
