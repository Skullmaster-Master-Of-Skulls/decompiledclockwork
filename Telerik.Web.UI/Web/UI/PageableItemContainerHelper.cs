using System;
using System.Diagnostics.CodeAnalysis;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x02001966 RID: 6502
	internal static class PageableItemContainerHelper
	{
		// Token: 0x0600FBA7 RID: 64423 RVA: 0x0038B6F4 File Offset: 0x003898F4
		[SuppressMessage("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
		public static IRadPageableItemContainer WrapContainer(Control container)
		{
			IRadPageableItemContainer result = null;
			if (container is IRadPageableItemContainer)
			{
				result = (IRadPageableItemContainer)container;
			}
			else if (container is IPageableItemContainer)
			{
				result = new PageableItemContainerWrapper((IPageableItemContainer)container);
			}
			return result;
		}
	}
}
