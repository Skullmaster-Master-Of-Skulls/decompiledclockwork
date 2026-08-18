using System;

namespace Telerik.Web.UI
{
	// Token: 0x02000992 RID: 2450
	public class WizardStepCreatedEventArgs : EventArgs
	{
		// Token: 0x06005D3A RID: 23866 RVA: 0x0011C73E File Offset: 0x0011A93E
		public WizardStepCreatedEventArgs(RadWizardStep wizardStep)
		{
			this._wizardStep = wizardStep;
		}

		// Token: 0x17001EBF RID: 7871
		// (get) Token: 0x06005D3B RID: 23867 RVA: 0x0011C74D File Offset: 0x0011A94D
		// (set) Token: 0x06005D3C RID: 23868 RVA: 0x0011C755 File Offset: 0x0011A955
		public RadWizardStep RadWizardStep
		{
			get
			{
				return this._wizardStep;
			}
			set
			{
				this._wizardStep = value;
			}
		}

		// Token: 0x04001671 RID: 5745
		private RadWizardStep _wizardStep;
	}
}
