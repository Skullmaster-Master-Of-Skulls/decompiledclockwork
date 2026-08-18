using System;
using TechnoPro.Common.Public;

namespace TechnoPro.Common.ClientManager.ICore.Legacy
{
	// Token: 0x02000042 RID: 66
	public interface ILegacyAccommodationClientManager : IWebService
	{
		// Token: 0x060001DF RID: 479
		void LogLoaIssuedDate(int pid, int lucid, string loaString);

		// Token: 0x060001E0 RID: 480
		void CreateOrAddAccommodationApprovalNote(int pid, string note);

		// Token: 0x060001E1 RID: 481
		string GetAccommodationsApprovalSummary(int pid);
	}
}
