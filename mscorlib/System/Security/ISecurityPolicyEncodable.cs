using System;
using System.Runtime.InteropServices;
using System.Security.Policy;

namespace System.Security
{
	// Token: 0x0200048F RID: 1167
	[ComVisible(true)]
	public interface ISecurityPolicyEncodable
	{
		// Token: 0x06002E62 RID: 11874
		SecurityElement ToXml(PolicyLevel level);

		// Token: 0x06002E63 RID: 11875
		void FromXml(SecurityElement e, PolicyLevel level);
	}
}
