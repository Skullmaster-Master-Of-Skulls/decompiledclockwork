using System;
using TechnoPro.Common.UI.Web.Entity.Web.EventArgs;

namespace TechnoPro.Common.UI.ClientManager.Web.Core.Tutoring
{
	// Token: 0x0200000B RID: 11
	public interface ITutoringTuteeStudentSchedulingWizardPage
	{
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000025 RID: 37
		// (remove) Token: 0x06000026 RID: 38
		event EventHandler<TutoringTuteeStudentSchedulingWizardPageArgs> OnTabChanging;
	}
}
