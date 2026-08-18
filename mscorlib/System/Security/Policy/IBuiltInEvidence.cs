using System;

namespace System.Security.Policy
{
	// Token: 0x02000494 RID: 1172
	internal interface IBuiltInEvidence
	{
		// Token: 0x06002E74 RID: 11892
		int OutputToBuffer(char[] buffer, int position, bool verbose);

		// Token: 0x06002E75 RID: 11893
		int InitFromBuffer(char[] buffer, int position);

		// Token: 0x06002E76 RID: 11894
		int GetRequiredSize(bool verbose);
	}
}
