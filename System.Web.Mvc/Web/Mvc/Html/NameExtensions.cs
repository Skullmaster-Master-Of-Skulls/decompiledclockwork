using System;
using System.Linq.Expressions;

namespace System.Web.Mvc.Html
{
	// Token: 0x0200008C RID: 140
	public static class NameExtensions
	{
		// Token: 0x06000407 RID: 1031 RVA: 0x0000C031 File Offset: 0x0000A231
		public static MvcHtmlString Id(this HtmlHelper html, string name)
		{
			return MvcHtmlString.Create(html.AttributeEncode(html.ViewData.TemplateInfo.GetFullHtmlFieldId(name)));
		}

		// Token: 0x06000408 RID: 1032 RVA: 0x0000C04F File Offset: 0x0000A24F
		public static MvcHtmlString IdFor<TModel, TProperty>(this HtmlHelper<TModel> html, Expression<Func<TModel, TProperty>> expression)
		{
			return html.Id(ExpressionHelper.GetExpressionText(expression));
		}

		// Token: 0x06000409 RID: 1033 RVA: 0x0000C05D File Offset: 0x0000A25D
		public static MvcHtmlString IdForModel(this HtmlHelper html)
		{
			return html.Id(string.Empty);
		}

		// Token: 0x0600040A RID: 1034 RVA: 0x0000C06A File Offset: 0x0000A26A
		public static MvcHtmlString Name(this HtmlHelper html, string name)
		{
			return MvcHtmlString.Create(html.AttributeEncode(html.ViewData.TemplateInfo.GetFullHtmlFieldName(name)));
		}

		// Token: 0x0600040B RID: 1035 RVA: 0x0000C088 File Offset: 0x0000A288
		public static MvcHtmlString NameFor<TModel, TProperty>(this HtmlHelper<TModel> html, Expression<Func<TModel, TProperty>> expression)
		{
			return html.Name(ExpressionHelper.GetExpressionText(expression));
		}

		// Token: 0x0600040C RID: 1036 RVA: 0x0000C096 File Offset: 0x0000A296
		public static MvcHtmlString NameForModel(this HtmlHelper html)
		{
			return html.Name(string.Empty);
		}
	}
}
