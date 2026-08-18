using System;
using System.Collections;
using System.Web.Util;

namespace System.Web.UI
{
	// Token: 0x02000248 RID: 584
	internal class PageDependencyParser : TemplateControlDependencyParser
	{
		// Token: 0x17000799 RID: 1945
		// (get) Token: 0x06001B09 RID: 6921 RVA: 0x00054F3D File Offset: 0x0005313D
		internal override string DefaultDirectiveName
		{
			get
			{
				return "page";
			}
		}

		// Token: 0x06001B0A RID: 6922 RVA: 0x00054F44 File Offset: 0x00053144
		protected override void PrepareParse()
		{
			if (base.PagesConfig != null && base.PagesConfig.MasterPageFileInternal != null && base.PagesConfig.MasterPageFileInternal.Length != 0)
			{
				base.AddDependency(VirtualPath.Create(base.PagesConfig.MasterPageFileInternal));
			}
		}

		// Token: 0x06001B0B RID: 6923 RVA: 0x00054F84 File Offset: 0x00053184
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
