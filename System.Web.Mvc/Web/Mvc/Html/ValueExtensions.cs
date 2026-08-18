using System;
using System.Linq.Expressions;

namespace System.Web.Mvc.Html
{
	// Token: 0x0200008D RID: 141
	public static class ValueExtensions
	{
		// Token: 0x0600040D RID: 1037 RVA: 0x0000C0A3 File Offset: 0x0000A2A3
		public static MvcHtmlString Value(this HtmlHelper html, string name)
		{
			return html.Value(name, null);
		}

		// Token: 0x0600040E RID: 1038 RVA: 0x0000C0AD File Offset: 0x0000A2AD
		public static MvcHtmlString Value(this HtmlHelper html, string name, string format)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			return ValueExtensions.ValueForHelper(html, name, null, format, true);
		}

		// Token: 0x0600040F RID: 1039 RVA: 0x0000C0C7 File Offset: 0x0000A2C7
		public static MvcHtmlString ValueFor<TModel, TProperty>(this HtmlHelper<TModel> html, Expression<Func<TModel, TProperty>> expression)
		{
			return html.ValueFor(expression, null);
		}

		// Token: 0x06000410 RID: 1040 RVA: 0x0000C0D4 File Offset: 0x0000A2D4
		public static MvcHtmlString ValueFor<TModel, TProperty>(this HtmlHelper<TModel> html, Expression<Func<TModel, TProperty>> expression, string format)
		{
			ModelMetadata modelMetadata = ModelMetadata.FromLambdaExpression<TModel, TProperty>(expression, html.ViewData);
			return ValueExtensions.ValueForHelper(html, ExpressionHelper.GetExpressionText(expression), modelMetadata.Model, format, false);
		}

		// Token: 0x06000411 RID: 1041 RVA: 0x0000C102 File Offset: 0x0000A302
		public static MvcHtmlString ValueForModel(this HtmlHelper html)
		{
			return html.ValueForModel(null);
		}

		// Token: 0x06000412 RID: 1042 RVA: 0x0000C10B File Offset: 0x0000A30B
		public static MvcHtmlString ValueForModel(this HtmlHelper html, string format)
		{
			return html.Value(string.Empty, format);
		}

		// Token: 0x06000413 RID: 1043 RVA: 0x0000C11C File Offset: 0x0000A31C
		internal static MvcHtmlString ValueForHelper(HtmlHelper html, string name, object value, string format, bool useViewData)
		{
			string fullHtmlFieldName = html.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(name);
			string text = (string)html.GetModelStateValue(fullHtmlFieldName, typeof(string));
			string value2;
			if (text != null)
			{
				value2 = text;
			}
			else if (useViewData)
			{
				if (name.Length == 0)
				{
					ModelMetadata modelMetadata = ModelMetadata.FromStringExpression(string.Empty, html.ViewContext.ViewData);
					value2 = html.FormatValue(modelMetadata.Model, format);
				}
				else
				{
					value2 = html.EvalString(name, format);
				}
			}
			else
			{
				value2 = html.FormatValue(value, format);
			}
			return MvcHtmlString.Create(html.AttributeEncode(value2));
		}
	}
}
