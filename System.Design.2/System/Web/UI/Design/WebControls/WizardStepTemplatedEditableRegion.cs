using System;
using System.Web.UI.WebControls;

namespace System.Web.UI.Design.WebControls
{
	// Token: 0x02000136 RID: 310
	public class WizardStepTemplatedEditableRegion : TemplatedEditableDesignerRegion, IWizardStepEditableRegion
	{
		// Token: 0x06000B47 RID: 2887 RVA: 0x00048ED1 File Offset: 0x000470D1
		public WizardStepTemplatedEditableRegion(TemplateDefinition templateDefinition, WizardStepBase wizardStep) : base(templateDefinition)
		{
			this._wizardStep = wizardStep;
			base.EnsureSize = true;
		}

		// Token: 0x17000282 RID: 642
		// (get) Token: 0x06000B48 RID: 2888 RVA: 0x00048EE8 File Offset: 0x000470E8
		public WizardStepBase Step
		{
			get
			{
				return this._wizardStep;
			}
		}

		// Token: 0x040006C1 RID: 1729
		private WizardStepBase _wizardStep;
	}
}
