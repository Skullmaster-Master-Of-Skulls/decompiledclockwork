using System;
using System.Runtime.InteropServices;

namespace System.Security.Policy
{
	// Token: 0x02000490 RID: 1168
	[ComVisible(true)]
	public interface IMembershipCondition : ISecurityEncodable, ISecurityPolicyEncodable
	{
		// Token: 0x06002E64 RID: 11876
		bool Check(Evidence evidence);

		// Token: 0x06002E65 RID: 11877
		IMembershipCondition Copy();

		// Token: 0x06002E66 RID: 11878
		string ToString();

		// Token: 0x06002E67 RID: 11879
		bool Equals(object obj);
	}
}
