using System;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;

namespace TechnoPro.Common.ICore.People
{
	// Token: 0x02000052 RID: 82
	public interface IStudentActivationManager : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x06000205 RID: 517
		void MergeActivations(int PersonIdNew, int PersonIdOld);
	}
}
