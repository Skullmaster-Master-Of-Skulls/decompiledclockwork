using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace System.Web.Mvc.Html
{
	// Token: 0x0200008B RID: 139
	public static class DisplayNameExtensions
	{
		// Token: 0x060003FF RID: 1023 RVA: 0x0000BF6C File Offset: 0x0000A16C
		public static MvcHtmlString DisplayName(this HtmlHelper html, string expression)
		{
			return html.DisplayNameInternal(expression, null);
		}

		// Token: 0x06000400 RID: 1024 RVA: 0x0000BF76 File Offset: 0x0000A176
		internal static MvcHtmlString DisplayNameInternal(this HtmlHelper html, string expression, ModelMetadataProvider metadataProvider)
		{
			return DisplayNameExtensions.DisplayNameHelper(ModelMetadata.FromStringExpression(expression, html.ViewData, metadataProvider), expression);
		}

		// Token: 0x06000401 RID: 1025 RVA: 0x0000BF8B File Offset: 0x0000A18B
		public static MvcHtmlString DisplayNameFor<TModel, TValue>(this HtmlHelper<IEnumerable<TModel>> html, Expression<Func<TModel, TValue>> expression)
		{
			return html.DisplayNameForInternal(expression, null);
		}

		// Token: 0x06000402 RID: 1026 RVA: 0x0000BF95 File Offset: 0x0000A195
		internal static MvcHtmlString DisplayNameForInternal<TModel, TValue>(this HtmlHelper<IEnumerable<TModel>> html, Expression<Func<TModel, TValue>> expression, ModelMetadataProvider metadataProvider)
		{
			return DisplayNameExtensions.DisplayNameHelper(ModelMetadata.FromLambdaExpression<TModel, TValue>(expression, new ViewDataDictionary<TModel>(), metadataProvider), ExpressionHelper.GetExpressionText(expression));
		}

		// Token: 0x06000403 RID: 1027 RVA: 0x0000BFAE File Offset: 0x0000A1AE
		public static MvcHtmlString DisplayNameFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression)
		{
			return html.DisplayNameForInternal(expression, null);
		}

		// Token: 0x06000404 RID: 1028 RVA: 0x0000BFB8 File Offset: 0x0000A1B8
		internal static MvcHtmlString DisplayNameForInternal<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, ModelMetadataProvider metadataProvider)
		{
			return DisplayNameExtensions.DisplayNameHelper(ModelMetadata.FromLambdaExpression<TModel, TValue>(expression, html.ViewData, metadataProvider), ExpressionHelper.GetExpressionText(expression));
		}

		// Token: 0x06000405 RID: 1029 RVA: 0x0000BFD2 File Offset: 0x0000A1D2
		public static MvcHtmlString DisplayNameForModel(this HtmlHelper html)
		{
			return DisplayNameExtensions.DisplayNameHelper(html.ViewData.ModelMetadata, string.Empty);
		}

		// Token: 0x06000406 RID: 1030 RVA: 0x0000BFEC File Offset: 0x0000A1EC
		internal static MvcHtmlString DisplayNameHelper(ModelMetadata metadata, string htmlFieldName)
		{
			string text;
			if ((text = metadata.DisplayName) == null && (text = metadata.PropertyName) == null)
			{
				text = htmlFieldName.Split(new char[]
				{
					'.'
				}).Last<string>();
			}
			string s = text;
			return new MvcHtmlString(HttpUtility.HtmlEncode(s));
		}
	}
}
