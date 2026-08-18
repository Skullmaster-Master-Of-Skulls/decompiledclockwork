using System;
using System.Collections;

namespace Telerik.Web.UI
{
	// Token: 0x02000A41 RID: 2625
	internal abstract class ComboBoxEnumerableHelper
	{
		// Token: 0x06006459 RID: 25689
		public abstract int GetCount(IEnumerable source);

		// Token: 0x0600645A RID: 25690
		public abstract IEnumerable GetPage(IEnumerable enumerable, int startIndex, int pageSize);
	}
}
