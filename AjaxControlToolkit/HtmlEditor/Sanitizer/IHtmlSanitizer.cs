using System;
using System.Collections.Generic;

namespace AjaxControlToolkit.HtmlEditor.Sanitizer
{
	// Token: 0x020000EA RID: 234
	public interface IHtmlSanitizer
	{
		// Token: 0x0600069F RID: 1695
		string GetSafeHtmlFragment(string htmlFragment, Dictionary<string, string[]> whiteList);
	}
}
