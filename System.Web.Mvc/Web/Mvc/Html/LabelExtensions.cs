using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace System.Web.Mvc.Html
{
	// Token: 0x0200015F RID: 351
	public static class LabelExtensions
	{
		// Token: 0x0600092C RID: 2348 RVA: 0x00019846 File Offset: 0x00017A46
		public static MvcHtmlString Label(this HtmlHelper html, string expression)
		{
			return html.Label(expression, null);
		}

		// Token: 0x0600092D RID: 2349 RVA: 0x00019850 File Offset: 0x00017A50
		public static MvcHtmlString Label(this HtmlHelper html, string expression, string labelText)
		{
			return html.Label(expression, labelText, null, null);
		}

		// Token: 0x0600092E RID: 2350 RVA: 0x0001985C File Offset: 0x00017A5C
		public static MvcHtmlString Label(this HtmlHelper html, string expression, object htmlAttributes)
		{
			return html.Label(expression, null, htmlAttributes, null);
		}

		// Token: 0x0600092F RID: 2351 RVA: 0x00019868 File Offset: 0x00017A68
		public static MvcHtmlString Label(this HtmlHelper html, string expression, IDictionary<string, object> htmlAttributes)
		{
			return html.Label(expression, null, htmlAttributes, null);
		}

		// Token: 0x06000930 RID: 2352 RVA: 0x00019874 File Offset: 0x00017A74
		public static MvcHtmlString Label(this HtmlHelper html, string expression, string labelText, object htmlAttributes)
		{
			return html.Label(expression, labelText, htmlAttributes, null);
		}

		// Token: 0x06000931 RID: 2353 RVA: 0x00019880 File Offset: 0x00017A80
		public static MvcHtmlString Label(this HtmlHelper html, string expression, string labelText, IDictionary<string, object> htmlAttributes)
		{
			return html.Label(expression, labelText, htmlAttributes, null);
		}

		// Token: 0x06000932 RID: 2354 RVA: 0x0001988C File Offset: 0x00017A8C
		internal static MvcHtmlString Label(this HtmlHelper html, string expression, string labelText, object htmlAttributes, ModelMetadataProvider metadataProvider)
		{
			return html.Label(expression, labelText, HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes), metadataProvider);
		}

		// Token: 0x06000933 RID: 2355 RVA: 0x0001989E File Offset: 0x00017A9E
		internal static MvcHtmlString Label(this HtmlHelper html, string expression, string labelText, IDictionary<string, object> htmlAttributes, ModelMetadataProvider metadataProvider)
		{
			return LabelExtensions.LabelHelper(html, ModelMetadata.FromStringExpression(expression, html.ViewData, metadataProvider), expression, labelText, htmlAttributes);
		}

		// Token: 0x06000934 RID: 2356 RVA: 0x000198B7 File Offset: 0x00017AB7
		public static MvcHtmlString LabelFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression)
		{
			return html.LabelFor(expression, null);
		}

		// Token: 0x06000935 RID: 2357 RVA: 0x000198C1 File Offset: 0x00017AC1
		public static MvcHtmlString LabelFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, string labelText)
		{
			return html.LabelFor(expression, labelText, null, null);
		}

		// Token: 0x06000936 RID: 2358 RVA: 0x000198CD File Offset: 0x00017ACD
		public static MvcHtmlString LabelFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, object htmlAttributes)
		{
			return html.LabelFor(expression, null, htmlAttributes, null);
		}

		// Token: 0x06000937 RID: 2359 RVA: 0x000198D9 File Offset: 0x00017AD9
		public static MvcHtmlString LabelFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, IDictionary<string, object> htmlAttributes)
		{
			return html.LabelFor(expression, null, htmlAttributes, null);
		}

		// Token: 0x06000938 RID: 2360 RVA: 0x000198E5 File Offset: 0x00017AE5
		public static MvcHtmlString LabelFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, string labelText, object htmlAttributes)
		{
			return html.LabelFor(expression, labelText, htmlAttributes, null);
		}

		// Token: 0x06000939 RID: 2361 RVA: 0x000198F1 File Offset: 0x00017AF1
		public static MvcHtmlString LabelFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, string labelText, IDictionary<string, object> htmlAttributes)
		{
			return html.LabelFor(expression, labelText, htmlAttributes, null);
		}

		// Token: 0x0600093A RID: 2362 RVA: 0x000198FD File Offset: 0x00017AFD
		internal static MvcHtmlString LabelFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, string labelText, object htmlAttributes, ModelMetadataProvider metadataProvider)
		{
			return html.LabelFor(expression, labelText, HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes), metadataProvider);
		}

		// Token: 0x0600093B RID: 2363 RVA: 0x0001990F File Offset: 0x00017B0F
		internal static MvcHtmlString LabelFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, string labelText, IDictionary<string, object> htmlAttributes, ModelMetadataProvider metadataProvider)
		{
			return LabelExtensions.LabelHelper(html, ModelMetadata.FromLambdaExpression<TModel, TValue>(expression, html.ViewData, metadataProvider), ExpressionHelper.GetExpressionText(expression), labelText, htmlAttributes);
		}

		// Token: 0x0600093C RID: 2364 RVA: 0x0001992D File Offset: 0x00017B2D
		public static MvcHtmlString LabelForModel(this HtmlHelper html)
		{
			return html.LabelForModel(null);
		}

		// Token: 0x0600093D RID: 2365 RVA: 0x00019936 File Offset: 0x00017B36
		public static MvcHtmlString LabelForModel(this HtmlHelper html, string labelText)
		{
			return LabelExtensions.LabelHelper(html, html.ViewData.ModelMetadata, string.Empty, labelText, null);
		}

		// Token: 0x0600093E RID: 2366 RVA: 0x00019950 File Offset: 0x00017B50
		public static MvcHtmlString LabelForModel(this HtmlHelper html, object htmlAttributes)
		{
			return LabelExtensions.LabelHelper(html, html.ViewData.ModelMetadata, string.Empty, null, HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
		}

		// Token: 0x0600093F RID: 2367 RVA: 0x0001996F File Offset: 0x00017B6F
		public static MvcHtmlString LabelForModel(this HtmlHelper html, IDictionary<string, object> htmlAttributes)
		{
			return LabelExtensions.LabelHelper(html, html.ViewData.ModelMetadata, string.Empty, null, htmlAttributes);
		}

		// Token: 0x06000940 RID: 2368 RVA: 0x00019989 File Offset: 0x00017B89
		public static MvcHtmlString LabelForModel(this HtmlHelper html, string labelText, object htmlAttributes)
		{
			return LabelExtensions.LabelHelper(html, html.ViewData.ModelMetadata, string.Empty, labelText, HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
		}

		// Token: 0x06000941 RID: 2369 RVA: 0x000199A8 File Offset: 0x00017BA8
		public static MvcHtmlString LabelForModel(this HtmlHelper html, string labelText, IDictionary<string, object> htmlAttributes)
		{
			return LabelExtensions.LabelHelper(html, html.ViewData.ModelMetadata, string.Empty, labelText, htmlAttributes);
		}

		// Token: 0x06000942 RID: 2370 RVA: 0x000199C4 File Offset: 0x00017BC4
		internal static MvcHtmlString LabelHelper(HtmlHelper html, ModelMetadata metadata, string htmlFieldName, string labelText = null, IDictionary<string, object> htmlAttributes = null)
		{
			string text = labelText;
			if (labelText == null && (text = metadata.DisplayName) == null && (text = metadata.PropertyName) == null)
			{
				text = htmlFieldName.Split(new char[]
				{
					'.'
				}).Last<string>();
			}
			string text2 = text;
			if (string.IsNullOrEmpty(text2))
			{
				return MvcHtmlString.Empty;
			}
			TagBuilder tagBuilder = new TagBuilder("label");
			tagBuilder.Attributes.Add("for", TagBuilder.CreateSanitizedId(html.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(htmlFieldName)));
			tagBuilder.SetInnerText(text2);
			tagBuilder.MergeAttributes<string, object>(htmlAttributes, true);
			return tagBuilder.ToMvcHtmlString(TagRenderMode.Normal);
		}
	}
}
