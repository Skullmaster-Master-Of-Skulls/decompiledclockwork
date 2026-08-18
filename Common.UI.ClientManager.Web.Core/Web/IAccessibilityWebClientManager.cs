using System;
using TechnoPro.Common.UI.Web.Entity.Accessible;

namespace TechnoPro.Common.UI.ClientManager.Web.Core.Web
{
	// Token: 0x02000002 RID: 2
	public interface IAccessibilityWebClientManager
	{
		// Token: 0x06000001 RID: 1
		void SetStudentAccessibleViewSetting(int studentPersonId, eClockWorkWebAccessibleView accessibleView);

		// Token: 0x06000002 RID: 2
		eClockWorkWebAccessibleView GetStudentAccessibleViewSetting(int studentPersonId);
	}
}
