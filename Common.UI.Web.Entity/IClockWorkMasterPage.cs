using System;

namespace TechnoPro.Common.UI.Web.Entity
{
	// Token: 0x02000008 RID: 8
	public interface IClockWorkMasterPage
	{
		// Token: 0x0600001C RID: 28
		void SetCurrentPage(eClockWorkWebPage page);

		// Token: 0x0600001D RID: 29
		void SetCausesValidationForAllMenuItems(bool newCausesValidation);
	}
}
