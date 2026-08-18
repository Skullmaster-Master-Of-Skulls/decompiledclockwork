using System;
using System.Collections;
using System.Web.Util;

namespace System.Web.UI
{
	// Token: 0x0200038F RID: 911
	internal class PageDependencyParser : TemplateControlDependencyParser
	{
		// Token: 0x17000990 RID: 2448
		// (get) Token: 0x06002C5F RID: 11359 RVA: 0x000C6432 File Offset: 0x000C5432
		internal override string DefaultDirectiveName
		{
			get
			{
				return "page";
			}
		}

		// Token: 0x06002C60 RID: 11360 RVA: 0x000C6439 File Offset: 0x000C5439
		protected override void PrepareParse()
		{
			if (base.PagesConfig != null && base.PagesConfig.MasterPageFileInternal != null && base.PagesConfig.MasterPageFileInternal.Length != 0)
			{
				base.AddDependency(VirtualPath.Create(base.PagesConfig.MasterPageFileInternal));
			}
		}

		// Token: 0x06002C61 RID: 11361 RVA: 0x000C6478 File Offset: 0x000C5478
		internal override void ProcessDirective(string directiveName, IDictionary directive)
		{
			base.ProcessDirective(directiveName, directive);
			if (StringUtil.EqualsIgnoreCase(directiveName, "previousPageType") || StringUtil.EqualsIgnoreCase(directiveName, "masterType"))
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
