using System;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;

namespace TechnoPro.Common.DAO.People
{
	// Token: 0x02000041 RID: 65
	public interface IStudentActivationDAO : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x06000133 RID: 307
		void MergeActivations(int PersonIdNew, int PersonIdOld);
	}
}
