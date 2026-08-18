using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Linq;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web.Mvc.Properties;
using System.Web.Routing;
using System.Web.UI.WebControls;

namespace System.Web.Mvc.Html
{
	// Token: 0x0200015C RID: 348
	internal static class DefaultEditorTemplates
	{
		// Token: 0x060008EC RID: 2284 RVA: 0x00018B9C File Offset: 0x00016D9C
		internal static string BooleanTemplate(HtmlHelper html)
		{
			bool? flag = null;
			if (html.ViewContext.ViewData.Model != null)
			{
				flag = new bool?(Convert.ToBoolean(html.ViewContext.ViewData.Model, CultureInfo.InvariantCulture));
			}
			if (!html.ViewContext.ViewData.ModelMetadata.IsNullableValueType)
			{
				return DefaultEditorTemplates.BooleanTemplateCheckbox(html, flag ?? false);
			}
			return DefaultEditorTemplates.BooleanTemplateDropDownList(html, flag);
		}

		// Token: 0x060008ED RID: 2285 RVA: 0x00018C1E File Offset: 0x00016E1E
		private static string BooleanTemplateCheckbox(HtmlHelper html, bool value)
		{
			return html.CheckBox(string.Empty, value, DefaultEditorTemplates.CreateHtmlAttributes(html, "check-box", null)).ToHtmlString();
		}

		// Token: 0x060008EE RID: 2286 RVA: 0x00018C3D File Offset: 0x00016E3D
		private static string BooleanTemplateDropDownList(HtmlHelper html, bool? value)
		{
			return html.DropDownList(string.Empty, DefaultEditorTemplates.TriStateValues(value), DefaultEditorTemplates.CreateHtmlAttributes(html, "list-box tri-state", null)).ToHtmlString();
		}

		// Token: 0x060008EF RID: 2287 RVA: 0x00018C61 File Offset: 0x00016E61
		internal static string CollectionTemplate(HtmlHelper html)
		{
			return DefaultEditorTemplates.CollectionTemplate(html, new TemplateHelpers.TemplateHelperDelegate(TemplateHelpers.TemplateHelper));
		}

		// Token: 0x060008F0 RID: 2288 RVA: 0x00018C88 File Offset: 0x00016E88
		internal static string CollectionTemplate(HtmlHelper html, TemplateHelpers.TemplateHelperDelegate templateHelper)
		{
			ViewDataDictionary viewData = html.ViewContext.ViewData;
			object model = viewData.ModelMetadata.Model;
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
			string htmlFieldPrefix = viewData.TemplateInfo.HtmlFieldPrefix;
			string result;
			try
			{
				viewData.TemplateInfo.HtmlFieldPrefix = string.Empty;
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
						string value = templateHelper(html, metadataForType, htmlFieldName, null, DataBoundControlMode.Edit, null);
						stringBuilder.Append(value);
					}
				}
				result = stringBuilder.ToString();
			}
			finally
			{
				viewData.TemplateInfo.HtmlFieldPrefix = htmlFieldPrefix;
			}
			return result;
		}

		// Token: 0x060008F1 RID: 2289 RVA: 0x00018E64 File Offset: 0x00017064
		internal static string DecimalTemplate(HtmlHelper html)
		{
			if (html.ViewContext.ViewData.TemplateInfo.FormattedModelValue == html.ViewContext.ViewData.ModelMetadata.Model)
			{
				html.ViewContext.ViewData.TemplateInfo.FormattedModelValue = string.Format(CultureInfo.CurrentCulture, "{0:0.00}", new object[]
				{
					html.ViewContext.ViewData.ModelMetadata.Model
				});
			}
			return DefaultEditorTemplates.StringTemplate(html);
		}

		// Token: 0x060008F2 RID: 2290 RVA: 0x00018EE8 File Offset: 0x000170E8
		internal static string HiddenInputTemplate(HtmlHelper html)
		{
			ViewDataDictionary viewData = html.ViewContext.ViewData;
			string str;
			if (viewData.ModelMetadata.HideSurroundingHtml)
			{
				str = string.Empty;
			}
			else
			{
				str = DefaultDisplayTemplates.StringTemplate(html);
			}
			object obj = viewData.Model;
			Binary binary = obj as Binary;
			if (binary != null)
			{
				obj = Convert.ToBase64String(binary.ToArray());
			}
			else
			{
				byte[] array = obj as byte[];
				if (array != null)
				{
					obj = Convert.ToBase64String(array);
				}
			}
			object obj2 = viewData["htmlAttributes"];
			IDictionary<string, object> dictionary = obj2 as IDictionary<string, object>;
			MvcHtmlString mvcHtmlString;
			if (dictionary != null)
			{
				mvcHtmlString = html.Hidden(string.Empty, obj, dictionary);
			}
			else
			{
				mvcHtmlString = html.Hidden(string.Empty, obj, obj2);
			}
			return str + mvcHtmlString.ToHtmlString();
		}

		// Token: 0x060008F3 RID: 2291 RVA: 0x00018FA1 File Offset: 0x000171A1
		internal static string MultilineTextTemplate(HtmlHelper html)
		{
			return html.TextArea(string.Empty, html.ViewContext.ViewData.TemplateInfo.FormattedModelValue.ToString(), 0, 0, DefaultEditorTemplates.CreateHtmlAttributes(html, "text-box multi-line", null)).ToHtmlString();
		}

		// Token: 0x060008F4 RID: 2292 RVA: 0x00018FDC File Offset: 0x000171DC
		private static IDictionary<string, object> CreateHtmlAttributes(HtmlHelper html, string className, string inputType = null)
		{
			object obj = html.ViewContext.ViewData["htmlAttributes"];
			if (obj != null)
			{
				return DefaultEditorTemplates.MergeHtmlAttributes(obj, className, inputType);
			}
			Dictionary<string, object> dictionary = new Dictionary<string, object>
			{
				{
					"class",
					className
				}
			};
			if (inputType != null)
			{
				dictionary.Add("type", inputType);
			}
			return dictionary;
		}

		// Token: 0x060008F5 RID: 2293 RVA: 0x00019030 File Offset: 0x00017230
		private static IDictionary<string, object> MergeHtmlAttributes(object htmlAttributesObject, string className, string inputType)
		{
			IDictionary<string, object> dictionary = htmlAttributesObject as IDictionary<string, object>;
			RouteValueDictionary routeValueDictionary = (dictionary != null) ? new RouteValueDictionary(dictionary) : HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributesObject);
			string text;
			if (routeValueDictionary.TryGetValue("class", out text))
			{
				text = text + " " + className;
				routeValueDictionary["class"] = text;
			}
			else
			{
				routeValueDictionary.Add("class", className);
			}
			if (inputType != null && !routeValueDictionary.ContainsKey("type"))
			{
				routeValueDictionary.Add("type", inputType);
			}
			return routeValueDictionary;
		}

		// Token: 0x060008F6 RID: 2294 RVA: 0x000190A9 File Offset: 0x000172A9
		internal static string ObjectTemplate(HtmlHelper html)
		{
			return DefaultEditorTemplates.ObjectTemplate(html, new TemplateHelpers.TemplateHelperDelegate(TemplateHelpers.TemplateHelper));
		}

		// Token: 0x060008F7 RID: 2295 RVA: 0x000190D4 File Offset: 0x000172D4
		internal static string ObjectTemplate(HtmlHelper html, TemplateHelpers.TemplateHelperDelegate templateHelper)
		{
			ViewDataDictionary viewData = html.ViewContext.ViewData;
			TemplateInfo templateInfo = viewData.TemplateInfo;
			ModelMetadata modelMetadata = viewData.ModelMetadata;
			StringBuilder stringBuilder = new StringBuilder();
			if (templateInfo.TemplateDepth <= 1)
			{
				foreach (ModelMetadata modelMetadata2 in from pm in modelMetadata.Properties
				where DefaultEditorTemplates.ShouldShow(pm, templateInfo)
				select pm)
				{
					if (!modelMetadata2.HideSurroundingHtml)
					{
						string text = LabelExtensions.LabelHelper(html, modelMetadata2, modelMetadata2.PropertyName, null, null).ToHtmlString();
						if (!string.IsNullOrEmpty(text))
						{
							stringBuilder.AppendFormat(CultureInfo.InvariantCulture, "<div class=\"editor-label\">{0}</div>\r\n", new object[]
							{
								text
							});
						}
						stringBuilder.Append("<div class=\"editor-field\">");
					}
					stringBuilder.Append(templateHelper(html, modelMetadata2, modelMetadata2.PropertyName, null, DataBoundControlMode.Edit, null));
					if (!modelMetadata2.HideSurroundingHtml)
					{
						stringBuilder.Append(" ");
						stringBuilder.Append(html.ValidationMessage(modelMetadata2.PropertyName));
						stringBuilder.Append("</div>\r\n");
					}
				}
				return stringBuilder.ToString();
			}
			if (modelMetadata.Model == null)
			{
				return modelMetadata.NullDisplayText;
			}
			string text2 = modelMetadata.SimpleDisplayText;
			if (modelMetadata.HtmlEncode)
			{
				text2 = html.Encode(text2);
			}
			return text2;
		}

		// Token: 0x060008F8 RID: 2296 RVA: 0x00019248 File Offset: 0x00017448
		internal static string PasswordTemplate(HtmlHelper html)
		{
			return html.Password(string.Empty, html.ViewContext.ViewData.TemplateInfo.FormattedModelValue, DefaultEditorTemplates.CreateHtmlAttributes(html, "text-box single-line password", null)).ToHtmlString();
		}

		// Token: 0x060008F9 RID: 2297 RVA: 0x0001927B File Offset: 0x0001747B
		private static bool ShouldShow(ModelMetadata metadata, TemplateInfo templateInfo)
		{
			return metadata.ShowForEdit && metadata.ModelType != typeof(EntityState) && !metadata.IsComplexType && !templateInfo.Visited(metadata);
		}

		// Token: 0x060008FA RID: 2298 RVA: 0x000192B0 File Offset: 0x000174B0
		internal static string StringTemplate(HtmlHelper html)
		{
			return DefaultEditorTemplates.HtmlInputTemplateHelper(html, null);
		}

		// Token: 0x060008FB RID: 2299 RVA: 0x000192B9 File Offset: 0x000174B9
		internal static string PhoneNumberInputTemplate(HtmlHelper html)
		{
			return DefaultEditorTemplates.HtmlInputTemplateHelper(html, "tel");
		}

		// Token: 0x060008FC RID: 2300 RVA: 0x000192C6 File Offset: 0x000174C6
		internal static string UrlInputTemplate(HtmlHelper html)
		{
			return DefaultEditorTemplates.HtmlInputTemplateHelper(html, "url");
		}

		// Token: 0x060008FD RID: 2301 RVA: 0x000192D3 File Offset: 0x000174D3
		internal static string EmailAddressInputTemplate(HtmlHelper html)
		{
			return DefaultEditorTemplates.HtmlInputTemplateHelper(html, "email");
		}

		// Token: 0x060008FE RID: 2302 RVA: 0x000192E0 File Offset: 0x000174E0
		internal static string DateTimeInputTemplate(HtmlHelper html)
		{
			DefaultEditorTemplates.ApplyRfc3339DateFormattingIfNeeded(html, "{0:yyyy-MM-ddTHH:mm:ss.fffK}");
			return DefaultEditorTemplates.HtmlInputTemplateHelper(html, "datetime");
		}

		// Token: 0x060008FF RID: 2303 RVA: 0x000192F8 File Offset: 0x000174F8
		internal static string DateTimeLocalInputTemplate(HtmlHelper html)
		{
			DefaultEditorTemplates.ApplyRfc3339DateFormattingIfNeeded(html, "{0:yyyy-MM-ddTHH:mm:ss.fff}");
			return DefaultEditorTemplates.HtmlInputTemplateHelper(html, "datetime-local");
		}

		// Token: 0x06000900 RID: 2304 RVA: 0x00019310 File Offset: 0x00017510
		internal static string DateInputTemplate(HtmlHelper html)
		{
			DefaultEditorTemplates.ApplyRfc3339DateFormattingIfNeeded(html, "{0:yyyy-MM-dd}");
			return DefaultEditorTemplates.HtmlInputTemplateHelper(html, "date");
		}

		// Token: 0x06000901 RID: 2305 RVA: 0x00019328 File Offset: 0x00017528
		internal static string TimeInputTemplate(HtmlHelper html)
		{
			DefaultEditorTemplates.ApplyRfc3339DateFormattingIfNeeded(html, "{0:HH:mm:ss.fff}");
			return DefaultEditorTemplates.HtmlInputTemplateHelper(html, "time");
		}

		// Token: 0x06000902 RID: 2306 RVA: 0x00019340 File Offset: 0x00017540
		internal static string NumberInputTemplate(HtmlHelper html)
		{
			return DefaultEditorTemplates.HtmlInputTemplateHelper(html, "number");
		}

		// Token: 0x06000903 RID: 2307 RVA: 0x00019350 File Offset: 0x00017550
		internal static string ColorInputTemplate(HtmlHelper html)
		{
			string value = null;
			if (html.ViewContext.ViewData.Model != null)
			{
				if (html.ViewContext.ViewData.Model is Color)
				{
					Color color = (Color)html.ViewContext.ViewData.Model;
					value = string.Format(CultureInfo.InvariantCulture, "#{0:X2}{1:X2}{2:X2}", new object[]
					{
						color.R,
						color.G,
						color.B
					});
				}
				else
				{
					value = html.ViewContext.ViewData.Model.ToString();
				}
			}
			return DefaultEditorTemplates.HtmlInputTemplateHelper(html, "color", value);
		}

		// Token: 0x06000904 RID: 2308 RVA: 0x0001940C File Offset: 0x0001760C
		private static void ApplyRfc3339DateFormattingIfNeeded(HtmlHelper html, string format)
		{
			if (html.Html5DateRenderingMode != Html5DateRenderingMode.Rfc3339)
			{
				return;
			}
			ModelMetadata modelMetadata = html.ViewContext.ViewData.ModelMetadata;
			object model = modelMetadata.Model;
			if (html.ViewContext.ViewData.TemplateInfo.FormattedModelValue != model && modelMetadata.HasNonDefaultEditFormat)
			{
				return;
			}
			if (model is DateTime || model is DateTimeOffset)
			{
				html.ViewContext.ViewData.TemplateInfo.FormattedModelValue = string.Format(CultureInfo.InvariantCulture, format, new object[]
				{
					model
				});
			}
		}

		// Token: 0x06000905 RID: 2309 RVA: 0x00019498 File Offset: 0x00017698
		private static string HtmlInputTemplateHelper(HtmlHelper html, string inputType = null)
		{
			return DefaultEditorTemplates.HtmlInputTemplateHelper(html, inputType, html.ViewContext.ViewData.TemplateInfo.FormattedModelValue);
		}

		// Token: 0x06000906 RID: 2310 RVA: 0x000194B6 File Offset: 0x000176B6
		private static string HtmlInputTemplateHelper(HtmlHelper html, string inputType, object value)
		{
			return html.TextBox(string.Empty, value, DefaultEditorTemplates.CreateHtmlAttributes(html, "text-box single-line", inputType)).ToHtmlString();
		}

		// Token: 0x06000907 RID: 2311 RVA: 0x000194D8 File Offset: 0x000176D8
		internal static List<SelectListItem> TriStateValues(bool? value)
		{
			return new List<SelectListItem>
			{
				new SelectListItem
				{
					Text = MvcResources.Common_TriState_NotSet,
					Value = string.Empty,
					Selected = (value == null)
				},
				new SelectListItem
				{
					Text = MvcResources.Common_TriState_True,
					Value = "true",
					Selected = (value != null && value.Value)
				},
				new SelectListItem
				{
					Text = MvcResources.Common_TriState_False,
					Value = "false",
					Selected = (value != null && !value.Value)
				}
			};
		}

		// Token: 0x0400027E RID: 638
		private const string HtmlAttributeKey = "htmlAttributes";
	}
}
