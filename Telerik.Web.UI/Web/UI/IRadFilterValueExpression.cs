using System;
using System.Collections;

namespace Telerik.Web.UI
{
	// Token: 0x0200187E RID: 6270
	public interface IRadFilterValueExpression
	{
		// Token: 0x1700493A RID: 18746
		// (get) Token: 0x0600F2ED RID: 62189
		ArrayList Values { get; }

		// Token: 0x0600F2EE RID: 62190
		void SetValues(ArrayList values);
	}
}
