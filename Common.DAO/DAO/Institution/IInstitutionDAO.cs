using System;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;

namespace TechnoPro.Common.DAO.Institution
{
	// Token: 0x0200006F RID: 111
	public interface IInstitutionDAO : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x060002B3 RID: 691
		string GetInstitutionUniqueName();

		// Token: 0x060002B4 RID: 692
		string GetInstitutionName();
	}
}
