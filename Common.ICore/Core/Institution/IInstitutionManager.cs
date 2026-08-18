using System;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;

namespace TechnoPro.Common.Core.Institution
{
	// Token: 0x02000002 RID: 2
	public interface IInstitutionManager : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x06000001 RID: 1
		string GetInstitutionUniqueName();

		// Token: 0x06000002 RID: 2
		string GetInstitutionName();
	}
}
