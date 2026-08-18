using System;
using System.Collections.Specialized;

namespace System.Web.UI
{
	// Token: 0x0200029A RID: 666
	public interface IBindableTemplate : ITemplate
	{
		// Token: 0x06001F7A RID: 8058
		IOrderedDictionary ExtractValues(Control container);
	}
}
