using System;
using System.Runtime.InteropServices;

namespace System.Security
{
	// Token: 0x02000624 RID: 1572
	[ComVisible(true)]
	public interface IPermission : ISecurityEncodable
	{
		// Token: 0x060038A3 RID: 14499
		IPermission Copy();

		// Token: 0x060038A4 RID: 14500
		IPermission Intersect(IPermission target);

		// Token: 0x060038A5 RID: 14501
		IPermission Union(IPermission target);

		// Token: 0x060038A6 RID: 14502
		bool IsSubsetOf(IPermission target);

		// Token: 0x060038A7 RID: 14503
		void Demand();
	}
}
