using System;
using System.Web.UI.WebControls;

namespace System.Web.UI
{
	// Token: 0x020002B7 RID: 695
	public interface IStyleSheet
	{
		// Token: 0x06001FCE RID: 8142
		void CreateStyleRule(Style style, IUrlResolutionService urlResolver, string selector);

		// Token: 0x06001FCF RID: 8143
		void RegisterStyle(Style style, IUrlResolutionService urlResolver);
	}
}
