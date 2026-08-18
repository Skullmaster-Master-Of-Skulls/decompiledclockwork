using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq.Expressions;
using System.Web.Mvc.Properties;

namespace System.Web.Mvc.Html
{
	// Token: 0x020001BC RID: 444
	public static class TextAreaExtensions
	{
		// Token: 0x06000CE3 RID: 3299 RVA: 0x00022220 File Offset: 0x00020420
		private static Dictionary<string, object> GetRowsAndColumnsDictionary(int rows, int columns)
		{
			if (rows < 0)
			{
				throw new ArgumentOutOfRangeException("rows", MvcResources.HtmlHelper_TextAreaParameterOutOfRange);
			}
			if (columns < 0)
			{
				throw new ArgumentOutOfRangeException("columns", MvcResources.HtmlHelper_TextAreaParameterOutOfRange);
			}
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			if (rows > 0)
			{
				dictionary.Add("rows", rows.ToString(CultureInfo.InvariantCulture));
			}
			if (columns > 0)
			{
				dictionary.Add("cols", columns.ToString(CultureInfo.InvariantCulture));
			}
			return dictionary;
		}

		// Token: 0x06000CE4 RID: 3300 RVA: 0x00022292 File Offset: 0x00020492
		public static MvcHtmlString TextArea(this HtmlHelper htmlHelper, string name)
		{
			return htmlHelper.TextArea(name, null, null);
		}

		// Token: 0x06000CE5 RID: 3301 RVA: 0x0002229D File Offset: 0x0002049D
		public static MvcHtmlString TextArea(this HtmlHelper htmlHelper, string name, object htmlAttributes)
		{
			return htmlHelper.TextArea(name, null, HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
		}

		// Token: 0x06000CE6 RID: 3302 RVA: 0x000222AD File Offset: 0x000204AD
		public static MvcHtmlString TextArea(this HtmlHelper htmlHelper, string name, IDictionary<string, object> htmlAttributes)
		{
			return htmlHelper.TextArea(name, null, htmlAttributes);
		}

		// Token: 0x06000CE7 RID: 3303 RVA: 0x000222B8 File Offset: 0x000204B8
		public static MvcHtmlString TextArea(this HtmlHelper htmlHelper, string name, string value)
		{
			return htmlHelper.TextArea(name, value, null);
		}

		// Token: 0x06000CE8 RID: 3304 RVA: 0x000222C3 File Offset: 0x000204C3
		public static MvcHtmlString TextArea(this HtmlHelper htmlHelper, string name, string value, object htmlAttributes)
		{
			return htmlHelper.TextArea(name, value, HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
		}

		// Token: 0x06000CE9 RID: 3305 RVA: 0x000222D4 File Offset: 0x000204D4
		public static MvcHtmlString TextArea(this HtmlHelper htmlHelper, string name, string value, IDictionary<string, object> htmlAttributes)
		{
			ModelMetadata modelMetadata = ModelMetadata.FromStringExpression(name, htmlHelper.ViewContext.ViewData);
			if (value != null)
			{
				modelMetadata.Model = value;
			}
			return TextAreaExtensions.TextAreaHelper(htmlHelper, modelMetadata, name, TextAreaExtensions.implicitRowsAndColumns, htmlAttributes, null);
		}

		// Token: 0x06000CEA RID: 3306 RVA: 0x0002230C File Offset: 0x0002050C
		public static MvcHtmlString TextArea(this HtmlHelper htmlHelper, string name, string value, int rows, int columns, object htmlAttributes)
		{
			return htmlHelper.TextArea(name, value, rows, columns, HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
		}

		// Token: 0x06000CEB RID: 3307 RVA: 0x00022320 File Offset: 0x00020520
		public static MvcHtmlString TextArea(this HtmlHelper htmlHelper, string name, string value, int rows, int columns, IDictionary<string, object> htmlAttributes)
		{
			ModelMetadata modelMetadata = ModelMetadata.FromStringExpression(name, htmlHelper.ViewContext.ViewData);
			if (value != null)
			{
				modelMetadata.Model = value;
			}
			return TextAreaExtensions.TextAreaHelper(htmlHelper, modelMetadata, name, TextAreaExtensions.GetRowsAndColumnsDictionary(rows, columns), htmlAttributes, null);
		}

		// Token: 0x06000CEC RID: 3308 RVA: 0x0002235C File Offset: 0x0002055C
		public static MvcHtmlString TextAreaFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression)
		{
			return htmlHelper.TextAreaFor(expression, null);
		}

		// Token: 0x06000CED RID: 3309 RVA: 0x00022366 File Offset: 0x00020566
		public static MvcHtmlString TextAreaFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, object htmlAttributes)
		{
			return htmlHelper.TextAreaFor(expression, HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
		}

		// Token: 0x06000CEE RID: 3310 RVA: 0x00022375 File Offset: 0x00020575
		public static MvcHtmlString TextAreaFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, IDictionary<string, object> htmlAttributes)
		{
			if (expression == null)
			{
				throw new ArgumentNullException("expression");
			}
			return TextAreaExtensions.TextAreaHelper(htmlHelper, ModelMetadata.FromLambdaExpression<TModel, TProperty>(expression, htmlHelper.ViewData), ExpressionHelper.GetExpressionText(expression), TextAreaExtensions.implicitRowsAndColumns, htmlAttributes, null);
		}

		// Token: 0x06000CEF RID: 3311 RVA: 0x000223A4 File Offset: 0x000205A4
		public static MvcHtmlString TextAreaFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, int rows, int columns, object htmlAttributes)
		{
			return htmlHelper.TextAreaFor(expression, rows, columns, HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
		}

		// Token: 0x06000CF0 RID: 3312 RVA: 0x000223B6 File Offset: 0x000205B6
		public static MvcHtmlString TextAreaFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, int rows, int columns, IDictionary<string, object> htmlAttributes)
		{
			if (expression == null)
			{
				throw new ArgumentNullException("expression");
			}
			return TextAreaExtensions.TextAreaHelper(htmlHelper, ModelMetadata.FromLambdaExpression<TModel, TProperty>(expression, htmlHelper.ViewData), ExpressionHelper.GetExpressionText(expression), TextAreaExtensions.GetRowsAndColumnsDictionary(rows, columns), htmlAttributes, null);
		}

		// Token: 0x06000CF1 RID: 3313 RVA: 0x000223E8 File Offset: 0x000205E8
		internal static MvcHtmlString TextAreaHelper(HtmlHelper htmlHelper, ModelMetadata modelMetadata, string name, IDictionary<string, object> rowsAndColumns, IDictionary<string, object> htmlAttributes, string innerHtmlPrefix = null)
		{
			string fullHtmlFieldName = htmlHelper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(name);
			if (string.IsNullOrEmpty(fullHtmlFieldName))
			{
				throw new ArgumentException(MvcResources.Common_NullOrEmpty, "name");
			}
			TagBuilder tagBuilder = new TagBuilder("textarea");
			tagBuilder.GenerateId(fullHtmlFieldName);
			tagBuilder.MergeAttributes<string, object>(htmlAttributes, true);
			tagBuilder.MergeAttributes<string, object>(rowsAndColumns, rowsAndColumns != TextAreaExtensions.implicitRowsAndColumns);
			tagBuilder.MergeAttribute("name", fullHtmlFieldName, true);
			ModelState modelState;
			if (htmlHelper.ViewData.ModelState.TryGetValue(fullHtmlFieldName, out modelState) && modelState.Errors.Count > 0)
			{
				tagBuilder.AddCssClass(HtmlHelper.ValidationInputCssClassName);
			}
			tagBuilder.MergeAttributes<string, object>(htmlHelper.GetUnobtrusiveValidationAttributes(name, modelMetadata));
			string s;
			if (modelState != null && modelState.Value != null)
			{
				s = modelState.Value.AttemptedValue;
			}
			else if (modelMetadata.Model != null)
			{
				s = modelMetadata.Model.ToString();
			}
			else
			{
				s = string.Empty;
			}
			tagBuilder.InnerHtml = (innerHtmlPrefix ?? Environment.NewLine) + HttpUtility.HtmlEncode(s);
			return tagBuilder.ToMvcHtmlString(TagRenderMode.Normal);
		}

		// Token: 0x0400035D RID: 861
		private const int TextAreaRows = 2;

		// Token: 0x0400035E RID: 862
		private const int TextAreaColumns = 20;

		// Token: 0x0400035F RID: 863
		private static Dictionary<string, object> implicitRowsAndColumns = new Dictionary<string, object>
		{
			{
				"rows",
				2.ToString(CultureInfo.InvariantCulture)
			},
			{
				"cols",
				20.ToString(CultureInfo.InvariantCulture)
			}
		};
	}
}
