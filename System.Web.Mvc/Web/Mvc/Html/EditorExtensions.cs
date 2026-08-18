using System;
using System.Linq.Expressions;
using System.Web.UI.WebControls;

namespace System.Web.Mvc.Html
{
	// Token: 0x0200015E RID: 350
	public static class EditorExtensions
	{
		// Token: 0x0600091A RID: 2330 RVA: 0x000196F0 File Offset: 0x000178F0
		public static MvcHtmlString Editor(this HtmlHelper html, string expression)
		{
			return TemplateHelpers.Template(html, expression, null, null, DataBoundControlMode.Edit, null);
		}

		// Token: 0x0600091B RID: 2331 RVA: 0x000196FD File Offset: 0x000178FD
		public static MvcHtmlString Editor(this HtmlHelper html, string expression, object additionalViewData)
		{
			return TemplateHelpers.Template(html, expression, null, null, DataBoundControlMode.Edit, additionalViewData);
		}

		// Token: 0x0600091C RID: 2332 RVA: 0x0001970A File Offset: 0x0001790A
		public static MvcHtmlString Editor(this HtmlHelper html, string expression, string templateName)
		{
			return TemplateHelpers.Template(html, expression, templateName, null, DataBoundControlMode.Edit, null);
		}

		// Token: 0x0600091D RID: 2333 RVA: 0x00019717 File Offset: 0x00017917
		public static MvcHtmlString Editor(this HtmlHelper html, string expression, string templateName, object additionalViewData)
		{
			return TemplateHelpers.Template(html, expression, templateName, null, DataBoundControlMode.Edit, additionalViewData);
		}

		// Token: 0x0600091E RID: 2334 RVA: 0x00019724 File Offset: 0x00017924
		public static MvcHtmlString Editor(this HtmlHelper html, string expression, string templateName, string htmlFieldName)
		{
			return TemplateHelpers.Template(html, expression, templateName, htmlFieldName, DataBoundControlMode.Edit, null);
		}

		// Token: 0x0600091F RID: 2335 RVA: 0x00019731 File Offset: 0x00017931
		public static MvcHtmlString Editor(this HtmlHelper html, string expression, string templateName, string htmlFieldName, object additionalViewData)
		{
			return TemplateHelpers.Template(html, expression, templateName, htmlFieldName, DataBoundControlMode.Edit, additionalViewData);
		}

		// Token: 0x06000920 RID: 2336 RVA: 0x0001973F File Offset: 0x0001793F
		public static MvcHtmlString EditorFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression)
		{
			return html.TemplateFor(expression, null, null, DataBoundControlMode.Edit, null);
		}

		// Token: 0x06000921 RID: 2337 RVA: 0x0001974C File Offset: 0x0001794C
		public static MvcHtmlString EditorFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, object additionalViewData)
		{
			return html.TemplateFor(expression, null, null, DataBoundControlMode.Edit, additionalViewData);
		}

		// Token: 0x06000922 RID: 2338 RVA: 0x00019759 File Offset: 0x00017959
		public static MvcHtmlString EditorFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, string templateName)
		{
			return html.TemplateFor(expression, templateName, null, DataBoundControlMode.Edit, null);
		}

		// Token: 0x06000923 RID: 2339 RVA: 0x00019766 File Offset: 0x00017966
		public static MvcHtmlString EditorFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, string templateName, object additionalViewData)
		{
			return html.TemplateFor(expression, templateName, null, DataBoundControlMode.Edit, additionalViewData);
		}

		// Token: 0x06000924 RID: 2340 RVA: 0x00019773 File Offset: 0x00017973
		public static MvcHtmlString EditorFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, string templateName, string htmlFieldName)
		{
			return html.TemplateFor(expression, templateName, htmlFieldName, DataBoundControlMode.Edit, null);
		}

		// Token: 0x06000925 RID: 2341 RVA: 0x00019780 File Offset: 0x00017980
		public static MvcHtmlString EditorFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, string templateName, string htmlFieldName, object additionalViewData)
		{
			return html.TemplateFor(expression, templateName, htmlFieldName, DataBoundControlMode.Edit, additionalViewData);
		}

		// Token: 0x06000926 RID: 2342 RVA: 0x0001978E File Offset: 0x0001798E
		public static MvcHtmlString EditorForModel(this HtmlHelper html)
		{
			return MvcHtmlString.Create(TemplateHelpers.TemplateHelper(html, html.ViewData.ModelMetadata, string.Empty, null, DataBoundControlMode.Edit, null));
		}

		// Token: 0x06000927 RID: 2343 RVA: 0x000197AE File Offset: 0x000179AE
		public static MvcHtmlString EditorForModel(this HtmlHelper html, object additionalViewData)
		{
			return MvcHtmlString.Create(TemplateHelpers.TemplateHelper(html, html.ViewData.ModelMetadata, string.Empty, null, DataBoundControlMode.Edit, additionalViewData));
		}

		// Token: 0x06000928 RID: 2344 RVA: 0x000197CE File Offset: 0x000179CE
		public static MvcHtmlString EditorForModel(this HtmlHelper html, string templateName)
		{
			return MvcHtmlString.Create(TemplateHelpers.TemplateHelper(html, html.ViewData.ModelMetadata, string.Empty, templateName, DataBoundControlMode.Edit, null));
		}

		// Token: 0x06000929 RID: 2345 RVA: 0x000197EE File Offset: 0x000179EE
		public static MvcHtmlString EditorForModel(this HtmlHelper html, string templateName, object additionalViewData)
		{
			return MvcHtmlString.Create(TemplateHelpers.TemplateHelper(html, html.ViewData.ModelMetadata, string.Empty, templateName, DataBoundControlMode.Edit, additionalViewData));
		}

		// Token: 0x0600092A RID: 2346 RVA: 0x0001980E File Offset: 0x00017A0E
		public static MvcHtmlString EditorForModel(this HtmlHelper html, string templateName, string htmlFieldName)
		{
			return MvcHtmlString.Create(TemplateHelpers.TemplateHelper(html, html.ViewData.ModelMetadata, htmlFieldName, templateName, DataBoundControlMode.Edit, null));
		}

		// Token: 0x0600092B RID: 2347 RVA: 0x0001982A File Offset: 0x00017A2A
		public static MvcHtmlString EditorForModel(this HtmlHelper html, string templateName, string htmlFieldName, object additionalViewData)
		{
			return MvcHtmlString.Create(TemplateHelpers.TemplateHelper(html, html.ViewData.ModelMetadata, htmlFieldName, templateName, DataBoundControlMode.Edit, additionalViewData));
		}
	}
}
