using System;
using System.Collections;
using System.Web.UI.WebControls;
using System.Web.Util;

namespace System.Web.UI.HtmlControls
{
	// Token: 0x02000359 RID: 857
	public class HtmlSelectBuilder : ControlBuilder
	{
		// Token: 0x06002765 RID: 10085 RVA: 0x000800C8 File Offset: 0x0007E2C8
		public override Type GetChildControlType(string tagName, IDictionary attribs)
		{
			if (StringUtil.EqualsIgnoreCase(tagName, "option"))
			{
				return typeof(ListItem);
			}
			return null;
		}

		// Token: 0x06002766 RID: 10086 RVA: 0x00007722 File Offset: 0x00005922
		public override bool AllowWhitespaceLiterals()
		{
			return false;
		}
	}
}
