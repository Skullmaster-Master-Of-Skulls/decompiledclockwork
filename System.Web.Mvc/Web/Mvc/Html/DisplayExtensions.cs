using System;
using System.Linq.Expressions;
using System.Web.UI.WebControls;

namespace System.Web.Mvc.Html
{
	// Token: 0x0200015D RID: 349
	public static class DisplayExtensions
	{
		// Token: 0x06000908 RID: 2312 RVA: 0x0001959A File Offset: 0x0001779A
		public static MvcHtmlString Display(this HtmlHelper html, string expression)
		{
			return TemplateHelpers.Template(html, expression, null, null, DataBoundControlMode.ReadOnly, null);
		}

		// Token: 0x06000909 RID: 2313 RVA: 0x000195A7 File Offset: 0x000177A7
		public static MvcHtmlString Display(this HtmlHelper html, string expression, object additionalViewData)
		{
			return TemplateHelpers.Template(html, expression, null, null, DataBoundControlMode.ReadOnly, additionalViewData);
		}

		// Token: 0x0600090A RID: 2314 RVA: 0x000195B4 File Offset: 0x000177B4
		public static MvcHtmlString Display(this HtmlHelper html, string expression, string templateName)
		{
			return TemplateHelpers.Template(html, expression, templateName, null, DataBoundControlMode.ReadOnly, null);
		}

		// Token: 0x0600090B RID: 2315 RVA: 0x000195C1 File Offset: 0x000177C1
		public static MvcHtmlString Display(this HtmlHelper html, string expression, string templateName, object additionalViewData)
		{
			return TemplateHelpers.Template(html, expression, templateName, null, DataBoundControlMode.ReadOnly, additionalViewData);
		}

		// Token: 0x0600090C RID: 2316 RVA: 0x000195CE File Offset: 0x000177CE
		public static MvcHtmlString Display(this HtmlHelper html, string expression, string templateName, string htmlFieldName)
		{
			return TemplateHelpers.Template(html, expression, templateName, htmlFieldName, DataBoundControlMode.ReadOnly, null);
		}

		// Token: 0x0600090D RID: 2317 RVA: 0x000195DB File Offset: 0x000177DB
		public static MvcHtmlString Display(this HtmlHelper html, string expression, string templateName, string htmlFieldName, object additionalViewData)
		{
			return TemplateHelpers.Template(html, expression, templateName, htmlFieldName, DataBoundControlMode.ReadOnly, additionalViewData);
		}

		// Token: 0x0600090E RID: 2318 RVA: 0x000195E9 File Offset: 0x000177E9
		public static MvcHtmlString DisplayFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression)
		{
			return html.TemplateFor(expression, null, null, DataBoundControlMode.ReadOnly, null);
		}

		// Token: 0x0600090F RID: 2319 RVA: 0x000195F6 File Offset: 0x000177F6
		public static MvcHtmlString DisplayFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, object additionalViewData)
		{
			return html.TemplateFor(expression, null, null, DataBoundControlMode.ReadOnly, additionalViewData);
		}

		// Token: 0x06000910 RID: 2320 RVA: 0x00019603 File Offset: 0x00017803
		public static MvcHtmlString DisplayFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, string templateName)
		{
			return html.TemplateFor(expression, templateName, null, DataBoundControlMode.ReadOnly, null);
		}

		// Token: 0x06000911 RID: 2321 RVA: 0x00019610 File Offset: 0x00017810
		public static MvcHtmlString DisplayFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, string templateName, object additionalViewData)
		{
			return html.TemplateFor(expression, templateName, null, DataBoundControlMode.ReadOnly, additionalViewData);
		}

		// Token: 0x06000912 RID: 2322 RVA: 0x0001961D File Offset: 0x0001781D
		public static MvcHtmlString DisplayFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, string templateName, string htmlFieldName)
		{
			return html.TemplateFor(expression, templateName, htmlFieldName, DataBoundControlMode.ReadOnly, null);
		}

		// Token: 0x06000913 RID: 2323 RVA: 0x0001962A File Offset: 0x0001782A
		public static MvcHtmlString DisplayFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, string templateName, string htmlFieldName, object additionalViewData)
		{
			return html.TemplateFor(expression, templateName, htmlFieldName, DataBoundControlMode.ReadOnly, additionalViewData);
		}

		// Token: 0x06000914 RID: 2324 RVA: 0x00019638 File Offset: 0x00017838
		public static MvcHtmlString DisplayForModel(this HtmlHelper html)
		{
			return MvcHtmlString.Create(TemplateHelpers.TemplateHelper(html, html.ViewData.ModelMetadata, string.Empty, null, DataBoundControlMode.ReadOnly, null));
		}

		// Token: 0x06000915 RID: 2325 RVA: 0x00019658 File Offset: 0x00017858
		public static MvcHtmlString DisplayForModel(this HtmlHelper html, object additionalViewData)
		{
			return MvcHtmlString.Create(TemplateHelpers.TemplateHelper(html, html.ViewData.ModelMetadata, string.Empty, null, DataBoundControlMode.ReadOnly, additionalViewData));
		}

		// Token: 0x06000916 RID: 2326 RVA: 0x00019678 File Offset: 0x00017878
		public static MvcHtmlString DisplayForModel(this HtmlHelper html, string templateName)
		{
			return MvcHtmlString.Create(TemplateHelpers.TemplateHelper(html, html.ViewData.ModelMetadata, string.Empty, templateName, DataBoundControlMode.ReadOnly, null));
		}

		// Token: 0x06000917 RID: 2327 RVA: 0x00019698 File Offset: 0x00017898
		public static MvcHtmlString DisplayForModel(this HtmlHelper html, string templateName, object additionalViewData)
		{
			return MvcHtmlString.Create(TemplateHelpers.TemplateHelper(html, html.ViewData.ModelMetadata, string.Empty, templateName, DataBoundControlMode.ReadOnly, additionalViewData));
		}

		// Token: 0x06000918 RID: 2328 RVA: 0x000196B8 File Offset: 0x000178B8
		public static MvcHtmlString DisplayForModel(this HtmlHelper html, string templateName, string htmlFieldName)
		{
			return MvcHtmlString.Create(TemplateHelpers.TemplateHelper(html, html.ViewData.ModelMetadata, htmlFieldName, templateName, DataBoundControlMode.ReadOnly, null));
		}

		// Token: 0x06000919 RID: 2329 RVA: 0x000196D4 File Offset: 0x000178D4
		public static MvcHtmlString DisplayForModel(this HtmlHelper html, string templateName, string htmlFieldName, object additionalViewData)
		{
			return MvcHtmlString.Create(TemplateHelpers.TemplateHelper(html, html.ViewData.ModelMetadata, htmlFieldName, templateName, DataBoundControlMode.ReadOnly, additionalViewData));
		}
	}
}
