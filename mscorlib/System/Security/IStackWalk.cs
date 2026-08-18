using System;
using System.Runtime.InteropServices;

namespace System.Security
{
	// Token: 0x02000625 RID: 1573
	[ComVisible(true)]
	public interface IStackWalk
	{
		// Token: 0x060038A8 RID: 14504
		void Assert();

		// Token: 0x060038A9 RID: 14505
		void Demand();

		// Token: 0x060038AA RID: 14506
		void Deny();

		// Token: 0x060038AB RID: 14507
		void PermitOnly();
	}
}
