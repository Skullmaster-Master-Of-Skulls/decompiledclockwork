using System;

namespace System.Security.Policy
{
	// Token: 0x02000492 RID: 1170
	internal interface IReportMatchMembershipCondition : IMembershipCondition, ISecurityEncodable, ISecurityPolicyEncodable
	{
		// Token: 0x06002E68 RID: 11880
		bool Check(Evidence evidence, out object usedEvidence);
	}
}
