using System;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;

namespace TechnoPro.Common.ICore.Legacy
{
	// Token: 0x02000073 RID: 115
	public interface ILegacyAccommodationManager : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x06000341 RID: 833
		void LogLoaIssuedDate(int pid, int lucid, string loaString);

		// Token: 0x06000342 RID: 834
		void CreateOrAddAccommodationApprovalNote(int pid, string note);

		// Token: 0x06000343 RID: 835
		string GetAccommodationsApprovalSummary(int pid);
	}
}
