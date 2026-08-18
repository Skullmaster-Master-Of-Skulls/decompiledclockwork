using System;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities.RequiredSessionForm;

namespace TechnoPro.Common.ClientManager.ICore.RequiredSessionForm
{
	// Token: 0x02000023 RID: 35
	public interface IRequiredSessionFormClientManager : IWebService
	{
		// Token: 0x060000CE RID: 206
		RequiredSessionFormItem[] GetRequiredSessionFormInfo();

		// Token: 0x060000CF RID: 207
		int LoadInfoPmIdForCurrentSession(int StudentPersonId, int ScreenNum);

		// Token: 0x060000D0 RID: 208
		int LoadInfoPmIdForSession(int StudentPersonId, int ScreenNum, DateTime DateInSession);
	}
}
