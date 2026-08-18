using System;

namespace TechnoPro.Common.DAO.Legacy
{
	// Token: 0x0200005E RID: 94
	public interface ILegacyAccommodationDAO
	{
		// Token: 0x06000222 RID: 546
		void AddAccommodationLoaIssuedRow(int pid, int lucid, string loaString);

		// Token: 0x06000223 RID: 547
		void CreateOrAddAccommodationApprovalNote(int pid, string note);

		// Token: 0x06000224 RID: 548
		string GetAccommodationsApprovalSummary(int pid);
	}
}
