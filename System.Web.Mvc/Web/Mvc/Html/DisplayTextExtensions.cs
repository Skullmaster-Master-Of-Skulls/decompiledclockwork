using System;
using System.Linq.Expressions;

namespace System.Web.Mvc.Html
{
	// Token: 0x0200011C RID: 284
	public static class DisplayTextExtensions
	{
		// Token: 0x06000771 RID: 1905 RVA: 0x0001445F File Offset: 0x0001265F
		public static MvcHtmlString DisplayText(this HtmlHelper html, string name)
		{
			return DisplayTextExtensions.DisplayTextHelper(html, ModelMetadata.FromStringExpression(name, html.ViewContext.ViewData));
		}

		// Token: 0x06000772 RID: 1906 RVA: 0x00014478 File Offset: 0x00012678
		public static MvcHtmlString DisplayTextFor<TModel, TResult>(this HtmlHelper<TModel> html, Expression<Func<TModel, TResult>> expression)
		{
			return DisplayTextExtensions.DisplayTextHelper(html, ModelMetadata.FromLambdaExpression<TModel, TResult>(expression, html.ViewData));
		}

		// Token: 0x06000773 RID: 1907 RVA: 0x0001448C File Offset: 0x0001268C
		private static MvcHtmlString DisplayTextHelper(HtmlHelper html, ModelMetadata metadata)
		{
			string value = metadata.SimpleDisplayText;
			if (metadata.HtmlEncode)
			{
				value = html.Encode(value);
			}
			return MvcHtmlString.Create(value);
		}
	}
}
