using System;
using System.Collections;

namespace System.Web.UI.HtmlControls
{
	// Token: 0x02000348 RID: 840
	public class HtmlHeadBuilder : ControlBuilder
	{
		// Token: 0x060026AA RID: 9898 RVA: 0x0007EA68 File Offset: 0x0007CC68
		public override Type GetChildControlType(string tagName, IDictionary attribs)
		{
			if (string.Equals(tagName, "title", StringComparison.OrdinalIgnoreCase))
			{
				return typeof(HtmlTitle);
			}
			if (string.Equals(tagName, "link", StringComparison.OrdinalIgnoreCase))
			{
				return typeof(HtmlLink);
			}
			if (string.Equals(tagName, "meta", StringComparison.OrdinalIgnoreCase))
			{
				return typeof(HtmlMeta);
			}
			return null;
		}

		// Token: 0x060026AB RID: 9899 RVA: 0x00007722 File Offset: 0x00005922
		public override bool AllowWhitespaceLiterals()
		{
			return false;
		}
	}
}
