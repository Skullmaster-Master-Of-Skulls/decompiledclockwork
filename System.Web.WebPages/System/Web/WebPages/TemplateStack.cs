using System;
using System.Collections.Generic;
using System.Linq;

namespace System.Web.WebPages
{
	// Token: 0x0200007E RID: 126
	public static class TemplateStack
	{
		// Token: 0x060003C5 RID: 965 RVA: 0x0000C7E2 File Offset: 0x0000A9E2
		public static ITemplateFile GetCurrentTemplate(HttpContextBase httpContext)
		{
			if (httpContext == null)
			{
				throw new ArgumentNullException("httpContext");
			}
			return TemplateStack.GetStack(httpContext).FirstOrDefault<ITemplateFile>();
		}

		// Token: 0x060003C6 RID: 966 RVA: 0x0000C7FD File Offset: 0x0000A9FD
		public static ITemplateFile Pop(HttpContextBase httpContext)
		{
			if (httpContext == null)
			{
				throw new ArgumentNullException("httpContext");
			}
			return TemplateStack.GetStack(httpContext).Pop();
		}

		// Token: 0x060003C7 RID: 967 RVA: 0x0000C818 File Offset: 0x0000AA18
		public static void Push(HttpContextBase httpContext, ITemplateFile templateFile)
		{
			if (templateFile == null)
			{
				throw new ArgumentNullException("templateFile");
			}
			if (httpContext == null)
			{
				throw new ArgumentNullException("httpContext");
			}
			TemplateStack.GetStack(httpContext).Push(templateFile);
		}

		// Token: 0x060003C8 RID: 968 RVA: 0x0000C844 File Offset: 0x0000AA44
		private static Stack<ITemplateFile> GetStack(HttpContextBase httpContext)
		{
			Stack<ITemplateFile> stack = httpContext.Items[TemplateStack._contextKey] as Stack<ITemplateFile>;
			if (stack == null)
			{
				stack = new Stack<ITemplateFile>();
				httpContext.Items[TemplateStack._contextKey] = stack;
			}
			return stack;
		}

		// Token: 0x0400011A RID: 282
		private static readonly object _contextKey = new object();
	}
}
