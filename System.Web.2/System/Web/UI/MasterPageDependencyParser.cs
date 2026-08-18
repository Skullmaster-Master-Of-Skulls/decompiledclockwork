using System;
using System.Collections;
using System.Web.Util;

namespace System.Web.UI
{
	// Token: 0x0200024A RID: 586
	internal class MasterPageDependencyParser : UserControlDependencyParser
	{
		// Token: 0x1700079B RID: 1947
		// (get) Token: 0x06001B0F RID: 6927 RVA: 0x00054FDE File Offset: 0x000531DE
		internal override string DefaultDirectiveName
		{
			get
			{
				return "master";
			}
		}

		// Token: 0x06001B10 RID: 6928 RVA: 0x00054FE8 File Offset: 0x000531E8
		internal override void ProcessDirective(string directiveName, IDictionary directive)
		{
			base.ProcessDirective(directiveName, directive);
			if (StringUtil.EqualsIgnoreCase(directiveName, "masterType"))
			{
				VirtualPath andRemoveVirtualPathAttribute = Util.GetAndRemoveVirtualPathAttribute(directive, "virtualPath");
				if (andRemoveVirtualPathAttribute != null)
				{
					base.AddDependency(andRemoveVirtualPathAttribute);
				}
			}
		}
	}
}
