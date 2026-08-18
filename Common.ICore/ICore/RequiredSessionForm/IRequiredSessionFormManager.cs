using System;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;

namespace TechnoPro.Common.ICore.RequiredSessionForm
{
	// Token: 0x0200004C RID: 76
	public interface IRequiredSessionFormManager : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x060001E4 RID: 484
		int LoadInfoPmIdForCurrentSession(int StudentPersonId, int ScreenNum);

		// Token: 0x060001E5 RID: 485
		int LoadInfoPmIdForSession(int StudentPersonId, int ScreenNum, DateTime DateInSession);
	}
}
