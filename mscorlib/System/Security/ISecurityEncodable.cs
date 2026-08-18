using System;
using System.Runtime.InteropServices;

namespace System.Security
{
	// Token: 0x0200048E RID: 1166
	[ComVisible(true)]
	public interface ISecurityEncodable
	{
		// Token: 0x06002E60 RID: 11872
		SecurityElement ToXml();

		// Token: 0x06002E61 RID: 11873
		void FromXml(SecurityElement e);
	}
}
