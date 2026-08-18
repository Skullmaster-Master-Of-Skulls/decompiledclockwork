using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web.Mvc.Properties;
using System.Web.UI.WebControls;

namespace System.Web.Mvc.Html
{
	// Token: 0x0200015B RID: 347
	internal static class DefaultDisplayTemplates
	{
		// Token: 0x060008DE RID: 2270 RVA: 0x00018480 File Offset: 0x00016680
		internal static string BooleanTemplate(HtmlHelper html)
		{
			bool? flag = null;
			if (html.ViewContext.ViewData.Model != null)
			{
				flag = new bool?(Convert.ToBoolean(html.ViewContext.ViewData.Model, CultureInfo.InvariantCulture));
			}
			if (!html.ViewContext.ViewData.ModelMetadata.IsNullableValueType)
			{
				return DefaultDisplayTemplates.BooleanTemplateCheckbox(flag ?? false);
			}
			return DefaultDisplayTemplates.BooleanTemplateDropDownList(flag);
		}

		// Token: 0x060008DF RID: 2271 RVA: 0x00018500 File Offset: 0x00016700
		private static string BooleanTemplateCheckbox(bool value)
		{
			TagBuilder tagBuilder = new TagBuilder("input");
			tagBuilder.AddCssClass("check-box");
			tagBuilder.Attributes["disabled"] = "disabled";
			tagBuilder.Attributes["type"] = "checkbox";
			if (value)
			{
				tagBuilder.Attributes["checked"] = "checked";
			}
			return tagBuilder.ToString(TagRenderMode.SelfClosing);
		}

		// Token: 0x060008E0 RID: 2272 RVA: 0x0001856C File Offset: 0x0001676C
		private static string BooleanTemplateDropDownList(bool? value)
		{
			StringBuilder stringBuilder = new StringBuilder();
			TagBuilder tagBuilder = new TagBuilder("select");
			tagBuilder.AddCssClass("list-box");
			tagBuilder.AddCssClass("tri-state");
			tagBuilder.Attributes["disabled"] = "disabled";
			stringBuilder.Append(tagBuilder.ToString(TagRenderMode.StartTag));
			foreach (SelectListItem item in DefaultEditorTemplates.TriStateValues(value))
			{
				stringBuilder.Append(SelectExtensions.ListItemToOption(item));
			}
			stringBuilder.Append(tagBuilder.ToString(TagRenderMode.EndTag));
			return stringBuilder.ToString();
		}

		// Token: 0x060008E1 RID: 2273 RVA: 0x00018624 File Offset: 0x00016824
		internal static string CollectionTemplate(HtmlHelper html)
		{
			return DefaultDisplayTemplates.CollectionTemplate(html, new TemplateHelpers.TemplateHelperDelegate(TemplateHelpers.TemplateHelper));
		}

		// Token: 0x060008E2 RID: 2274 RVA: 0x00018648 File Offset: 0x00016848
		internal static string CollectionTemplate(HtmlHelper html, TemplateHelpers.TemplateHelperDelegate templateHelper)
		{
			object model = html.ViewContext.ViewData.ModelMetadata.Model;
			if (model == null)
			{
				return string.Empty;
			}
			IEnumerable enumerable = model as IEnumerable;
			if (enumerable == null)
			{
				throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, MvcResources.Templates_TypeMustImplementIEnumerable, new object[]
				{
					model.GetType().FullName
				}));
			}
			Type type = typeof(string);
			Type type2 = TypeHelpers.ExtractGenericInterface(enumerable.GetType(), typeof(IEnumerable<>));
			if (type2 != null)
			{
				type = type2.GetGenericArguments()[0];
			}
			bool flag = TypeHelpers.IsNullableValueType(type);
			string htmlFieldPrefix = html.ViewContext.ViewData.TemplateInfo.HtmlFieldPrefix;
			string result;
			try
			{
				html.ViewContext.ViewData.TemplateInfo.HtmlFieldPrefix = string.Empty;
				string text = htmlFieldPrefix;
				StringBuilder stringBuilder = new StringBuilder();
				int num = 0;
				using (IEnumerator enumerator = enumerable.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						object item = enumerator.Current;
						Type modelType = type;
						if (item != null && !flag)
						{
							modelType = item.GetType();
						}
						ModelMetadata metadataForType = ModelMetadataProviders.Current.GetMetadataForType(() => item, modelType);
						string htmlFieldName = string.Format(CultureInfo.InvariantCulture, "{0}[{1}]", new object[]
						{
							text,
							num++
						});
						string value = templateHelper(html, metadataForType, htmlFieldName, null, DataBoundControlMode.ReadOnly, null);
						stringBuilder.Append(value);
					}
				}
				result = stringBuilder.ToString();
			}
			finally
			{
				html.ViewContext.ViewData.TemplateInfo.HtmlFieldPrefix = htmlFieldPrefix;
			}
			return result;
		}

		// Token: 0x060008E3 RID: 2275 RVA: 0x00018854 File Offset: 0x00016A54
		internal static string DecimalTemplate(HtmlHelper html)
		{
			if (html.ViewContext.ViewData.TemplateInfo.FormattedModelValue == html.ViewContext.ViewData.ModelMetadata.Model)
			{
				html.ViewContext.ViewData.TemplateInfo.FormattedModelValue = string.Format(CultureInfo.CurrentCulture, "{0:0.00}", new object[]
				{
					html.ViewContext.ViewData.ModelMetadata.Model
				});
			}
			return DefaultDisplayTemplates.StringTemplate(html);
		}

		// Token: 0x060008E4 RID: 2276 RVA: 0x000188D8 File Offset: 0x00016AD8
		internal static string EmailAddressTemplate(HtmlHelper html)
		{
			return string.Format(CultureInfo.InvariantCulture, "<a href=\"mailto:{0}\">{1}</a>", new object[]
			{
				html.AttributeEncode(html.ViewContext.ViewData.Model),
				html.Encode(html.ViewContext.ViewData.TemplateInfo.FormattedModelValue)
			});
		}

		// Token: 0x060008E5 RID: 2277 RVA: 0x00018933 File Offset: 0x00016B33
		internal static string HiddenInputTemplate(HtmlHelper html)
		{
			if (html.ViewContext.ViewData.ModelMetadata.HideSurroundingHtml)
			{
				return string.Empty;
			}
			return DefaultDisplayTemplates.StringTemplate(html);
		}

		// Token: 0x060008E6 RID: 2278 RVA: 0x00018958 File Offset: 0x00016B58
		internal static string HtmlTemplate(HtmlHelper html)
		{
			return html.ViewContext.ViewData.TemplateInfo.FormattedModelValue.ToString();
		}

		// Token: 0x060008E7 RID: 2279 RVA: 0x00018974 File Offset: 0x00016B74
		internal static string ObjectTemplate(HtmlHelper html)
		{
			return DefaultDisplayTemplates.ObjectTemplate(html, new TemplateHelpers.TemplateHelperDelegate(TemplateHelpers.TemplateHelper));
		}

		// Token: 0x060008E8 RID: 2280 RVA: 0x000189A0 File Offset: 0x00016BA0
		internal static string ObjectTemplate(HtmlHelper html, TemplateHelpers.TemplateHelperDelegate templateHelper)
		{
			ViewDataDictionary viewData = html.ViewContext.ViewData;
			TemplateInfo templateInfo = viewData.TemplateInfo;
			ModelMetadata modelMetadata = viewData.ModelMetadata;
			StringBuilder stringBuilder = new StringBuilder();
			if (modelMetadata.Model == null)
			{
				return modelMetadata.NullDisplayText;
			}
			if (templateInfo.TemplateDepth > 1)
			{
				string text = modelMetadata.SimpleDisplayText;
				if (modelMetadata.HtmlEncode)
				{
					text = html.Encode(text);
				}
				return text;
			}
			foreach (ModelMetadata modelMetadata2 in from pm in modelMetadata.Properties
			where DefaultDisplayTemplates.ShouldShow(pm, templateInfo)
			select pm)
			{
				if (!modelMetadata2.HideSurroundingHtml)
				{
					string displayName = modelMetadata2.GetDisplayName();
					if (!string.IsNullOrEmpty(displayName))
					{
						stringBuilder.AppendFormat(CultureInfo.InvariantCulture, "<div class=\"display-label\">{0}</div>", new object[]
						{
							displayName
						});
						stringBuilder.AppendLine();
					}
					stringBuilder.Append("<div class=\"display-field\">");
				}
				stringBuilder.Append(templateHelper(html, modelMetadata2, modelMetadata2.PropertyName, null, DataBoundControlMode.ReadOnly, null));
				if (!modelMetadata2.HideSurroundingHtml)
				{
					stringBuilder.AppendLine("</div>");
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060008E9 RID: 2281 RVA: 0x00018AEC File Offset: 0x00016CEC
		private static bool ShouldShow(ModelMetadata metadata, TemplateInfo templateInfo)
		{
			return metadata.ShowForDisplay && metadata.ModelType != typeof(EntityState) && !metadata.IsComplexType && !templateInfo.Visited(metadata);
		}

		// Token: 0x060008EA RID: 2282 RVA: 0x00018B21 File Offset: 0x00016D21
		internal static string StringTemplate(HtmlHelper html)
		{
			return html.Encode(html.ViewContext.ViewData.TemplateInfo.FormattedModelValue);
		}

		// Token: 0x060008EB RID: 2283 RVA: 0x00018B40 File Offset: 0x00016D40
		internal static string UrlTemplate(HtmlHelper html)
		{
			return string.Format(CultureInfo.InvariantCulture, "<a href=\"{0}\">{1}</a>", new object[]
			{
				html.AttributeEncode(html.ViewContext.ViewData.Model),
				html.Encode(html.ViewContext.ViewData.TemplateInfo.FormattedModelValue)
			});
		}
	}
}
