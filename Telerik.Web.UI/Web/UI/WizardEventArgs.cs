using System;

namespace Telerik.Web.UI
{
	// Token: 0x0200098F RID: 2447
	public class WizardEventArgs : EventArgs
	{
		// Token: 0x06005D28 RID: 23848 RVA: 0x0011C6A2 File Offset: 0x0011A8A2
		public WizardEventArgs(RadWizardStep currentStep, RadWizardStep nextStep)
		{
			this.CurrentStepIndex = currentStep.Index;
			this.NextStepIndex = nextStep.Index;
			this.CurrentStep = currentStep;
			this.NextStep = nextStep;
		}

		// Token: 0x17001EB9 RID: 7865
		// (get) Token: 0x06005D29 RID: 23849 RVA: 0x0011C6D0 File Offset: 0x0011A8D0
		// (set) Token: 0x06005D2A RID: 23850 RVA: 0x0011C6D8 File Offset: 0x0011A8D8
		public int CurrentStepIndex { get; set; }

		// Token: 0x17001EBA RID: 7866
		// (get) Token: 0x06005D2B RID: 23851 RVA: 0x0011C6E1 File Offset: 0x0011A8E1
		// (set) Token: 0x06005D2C RID: 23852 RVA: 0x0011C6E9 File Offset: 0x0011A8E9
		public RadWizardStep CurrentStep { get; set; }

		// Token: 0x17001EBB RID: 7867
		// (get) Token: 0x06005D2D RID: 23853 RVA: 0x0011C6F2 File Offset: 0x0011A8F2
		// (set) Token: 0x06005D2E RID: 23854 RVA: 0x0011C6FA File Offset: 0x0011A8FA
		public int NextStepIndex { get; set; }

		// Token: 0x17001EBC RID: 7868
		// (get) Token: 0x06005D2F RID: 23855 RVA: 0x0011C703 File Offset: 0x0011A903
		// (set) Token: 0x06005D30 RID: 23856 RVA: 0x0011C70B File Offset: 0x0011A90B
		public RadWizardStep NextStep { get; set; }
	}
}
