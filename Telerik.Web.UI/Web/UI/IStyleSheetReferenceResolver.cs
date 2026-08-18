using System;

namespace Telerik.Web.UI
{
	// Token: 0x02001817 RID: 6167
	internal interface IStyleSheetReferenceResolver
	{
		// Token: 0x0600F027 RID: 61479
		void ResolveStyleSheetReference(StyleSheetReference styleSheet);

		// Token: 0x0600F028 RID: 61480
		Uri ResoveSkinUri(string resourceUri);
	}
}
