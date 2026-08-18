using System;
using System.Collections;
using System.Security.Permissions;
using System.Web.UI.WebControls;
using System.Web.Util;

namespace System.Web.UI.HtmlControls
{
	// Token: 0x020004A8 RID: 1192
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class HtmlSelectBuilder : ControlBuilder
	{
		// Token: 0x060037A2 RID: 14242 RVA: 0x000EE510 File Offset: 0x000ED510
		public override Type GetChildControlType(string tagName, IDictionary attribs)
		{
			if (StringUtil.EqualsIgnoreCase(tagName, "option"))
			{
				return typeof(ListItem);
			}
			return null;
		}

		// Token: 0x060037A3 RID: 14243 RVA: 0x000EE52B File Offset: 0x000ED52B
		public override bool AllowWhitespaceLiterals()
		{
			return false;
		}
	}
}
