using System;

namespace System.Web.Mvc
{
	// Token: 0x020000D6 RID: 214
	internal static class TagBuilderExtensions
	{
		// Token: 0x06000586 RID: 1414 RVA: 0x0000F752 File Offset: 0x0000D952
		internal static MvcHtmlString ToMvcHtmlString(this TagBuilder tagBuilder, TagRenderMode renderMode)
		{
			return new MvcHtmlString(tagBuilder.ToString(renderMode));
		}
	}
}
