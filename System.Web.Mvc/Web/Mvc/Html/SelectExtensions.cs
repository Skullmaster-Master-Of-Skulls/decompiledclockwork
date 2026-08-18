using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Web.Mvc.Properties;

namespace System.Web.Mvc.Html
{
	// Token: 0x020001BB RID: 443
	public static class SelectExtensions
	{
		// Token: 0x06000CBE RID: 3262 RVA: 0x00021971 File Offset: 0x0001FB71
		public static MvcHtmlString DropDownList(this HtmlHelper htmlHelper, string name)
		{
			return htmlHelper.DropDownList(name, null, null, null);
		}

		// Token: 0x06000CBF RID: 3263 RVA: 0x0002197D File Offset: 0x0001FB7D
		public static MvcHtmlString DropDownList(this HtmlHelper htmlHelper, string name, string optionLabel)
		{
			return htmlHelper.DropDownList(name, null, optionLabel, null);
		}

		// Token: 0x06000CC0 RID: 3264 RVA: 0x00021989 File Offset: 0x0001FB89
		public static MvcHtmlString DropDownList(this HtmlHelper htmlHelper, string name, IEnumerable<SelectListItem> selectList)
		{
			return htmlHelper.DropDownList(name, selectList, null, null);
		}

		// Token: 0x06000CC1 RID: 3265 RVA: 0x00021995 File Offset: 0x0001FB95
		public static MvcHtmlString DropDownList(this HtmlHelper htmlHelper, string name, IEnumerable<SelectListItem> selectList, object htmlAttributes)
		{
			return htmlHelper.DropDownList(name, selectList, null, HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
		}

		// Token: 0x06000CC2 RID: 3266 RVA: 0x000219A6 File Offset: 0x0001FBA6
		public static MvcHtmlString DropDownList(this HtmlHelper htmlHelper, string name, IEnumerable<SelectListItem> selectList, IDictionary<string, object> htmlAttributes)
		{
			return htmlHelper.DropDownList(name, selectList, null, htmlAttributes);
		}

		// Token: 0x06000CC3 RID: 3267 RVA: 0x000219B2 File Offset: 0x0001FBB2
		public static MvcHtmlString DropDownList(this HtmlHelper htmlHelper, string name, IEnumerable<SelectListItem> selectList, string optionLabel)
		{
			return htmlHelper.DropDownList(name, selectList, optionLabel, null);
		}

		// Token: 0x06000CC4 RID: 3268 RVA: 0x000219BE File Offset: 0x0001FBBE
		public static MvcHtmlString DropDownList(this HtmlHelper htmlHelper, string name, IEnumerable<SelectListItem> selectList, string optionLabel, object htmlAttributes)
		{
			return htmlHelper.DropDownList(name, selectList, optionLabel, HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
		}

		// Token: 0x06000CC5 RID: 3269 RVA: 0x000219D0 File Offset: 0x0001FBD0
		public static MvcHtmlString DropDownList(this HtmlHelper htmlHelper, string name, IEnumerable<SelectListItem> selectList, string optionLabel, IDictionary<string, object> htmlAttributes)
		{
			return SelectExtensions.DropDownListHelper(htmlHelper, null, name, selectList, optionLabel, htmlAttributes);
		}

		// Token: 0x06000CC6 RID: 3270 RVA: 0x000219DE File Offset: 0x0001FBDE
		public static MvcHtmlString DropDownListFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, IEnumerable<SelectListItem> selectList)
		{
			return htmlHelper.DropDownListFor(expression, selectList, null, null);
		}

		// Token: 0x06000CC7 RID: 3271 RVA: 0x000219EA File Offset: 0x0001FBEA
		public static MvcHtmlString DropDownListFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, IEnumerable<SelectListItem> selectList, object htmlAttributes)
		{
			return htmlHelper.DropDownListFor(expression, selectList, null, HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
		}

		// Token: 0x06000CC8 RID: 3272 RVA: 0x000219FB File Offset: 0x0001FBFB
		public static MvcHtmlString DropDownListFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, IEnumerable<SelectListItem> selectList, IDictionary<string, object> htmlAttributes)
		{
			return htmlHelper.DropDownListFor(expression, selectList, null, htmlAttributes);
		}

		// Token: 0x06000CC9 RID: 3273 RVA: 0x00021A07 File Offset: 0x0001FC07
		public static MvcHtmlString DropDownListFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, IEnumerable<SelectListItem> selectList, string optionLabel)
		{
			return htmlHelper.DropDownListFor(expression, selectList, optionLabel, null);
		}

		// Token: 0x06000CCA RID: 3274 RVA: 0x00021A13 File Offset: 0x0001FC13
		public static MvcHtmlString DropDownListFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, IEnumerable<SelectListItem> selectList, string optionLabel, object htmlAttributes)
		{
			return htmlHelper.DropDownListFor(expression, selectList, optionLabel, HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
		}

		// Token: 0x06000CCB RID: 3275 RVA: 0x00021A28 File Offset: 0x0001FC28
		public static MvcHtmlString DropDownListFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, IEnumerable<SelectListItem> selectList, string optionLabel, IDictionary<string, object> htmlAttributes)
		{
			if (expression == null)
			{
				throw new ArgumentNullException("expression");
			}
			ModelMetadata metadata = ModelMetadata.FromLambdaExpression<TModel, TProperty>(expression, htmlHelper.ViewData);
			return SelectExtensions.DropDownListHelper(htmlHelper, metadata, ExpressionHelper.GetExpressionText(expression), selectList, optionLabel, htmlAttributes);
		}

		// Token: 0x06000CCC RID: 3276 RVA: 0x00021A61 File Offset: 0x0001FC61
		public static MvcHtmlString EnumDropDownListFor<TModel, TEnum>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TEnum>> expression)
		{
			return htmlHelper.EnumDropDownListFor(expression, null);
		}

		// Token: 0x06000CCD RID: 3277 RVA: 0x00021A6B File Offset: 0x0001FC6B
		public static MvcHtmlString EnumDropDownListFor<TModel, TEnum>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TEnum>> expression, object htmlAttributes)
		{
			return htmlHelper.EnumDropDownListFor(expression, null, htmlAttributes);
		}

		// Token: 0x06000CCE RID: 3278 RVA: 0x00021A76 File Offset: 0x0001FC76
		public static MvcHtmlString EnumDropDownListFor<TModel, TEnum>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TEnum>> expression, IDictionary<string, object> htmlAttributes)
		{
			return htmlHelper.EnumDropDownListFor(expression, null, htmlAttributes);
		}

		// Token: 0x06000CCF RID: 3279 RVA: 0x00021A81 File Offset: 0x0001FC81
		public static MvcHtmlString EnumDropDownListFor<TModel, TEnum>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TEnum>> expression, string optionLabel)
		{
			return htmlHelper.EnumDropDownListFor(expression, optionLabel, null);
		}

		// Token: 0x06000CD0 RID: 3280 RVA: 0x00021A8C File Offset: 0x0001FC8C
		public static MvcHtmlString EnumDropDownListFor<TModel, TEnum>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TEnum>> expression, string optionLabel, object htmlAttributes)
		{
			return htmlHelper.EnumDropDownListFor(expression, optionLabel, HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
		}

		// Token: 0x06000CD1 RID: 3281 RVA: 0x00021A9C File Offset: 0x0001FC9C
		public static MvcHtmlString EnumDropDownListFor<TModel, TEnum>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TEnum>> expression, string optionLabel, IDictionary<string, object> htmlAttributes)
		{
			if (expression == null)
			{
				throw Error.ArgumentNull("expression");
			}
			ModelMetadata modelMetadata = ModelMetadata.FromLambdaExpression<TModel, TEnum>(expression, htmlHelper.ViewData);
			if (modelMetadata == null)
			{
				throw Error.Argument("expression", MvcResources.SelectExtensions_InvalidExpressionParameterNoMetadata, new object[]
				{
					expression.ToString()
				});
			}
			if (modelMetadata.ModelType == null)
			{
				throw Error.Argument("expression", MvcResources.SelectExtensions_InvalidExpressionParameterNoModelType, new object[]
				{
					expression.ToString()
				});
			}
			if (!EnumHelper.IsValidForEnumHelper(modelMetadata.ModelType))
			{
				string messageFormat;
				if (EnumHelper.HasFlags(modelMetadata.ModelType))
				{
					messageFormat = MvcResources.SelectExtensions_InvalidExpressionParameterTypeHasFlags;
				}
				else
				{
					messageFormat = MvcResources.SelectExtensions_InvalidExpressionParameterType;
				}
				throw Error.Argument("expression", messageFormat, new object[]
				{
					modelMetadata.ModelType.FullName,
					"Flags"
				});
			}
			string expressionText = ExpressionHelper.GetExpressionText(expression);
			string fullHtmlFieldName = htmlHelper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(expressionText);
			Enum @enum = null;
			if (!string.IsNullOrEmpty(fullHtmlFieldName))
			{
				@enum = (htmlHelper.GetModelStateValue(fullHtmlFieldName, modelMetadata.ModelType) as Enum);
			}
			if (@enum == null && !string.IsNullOrEmpty(expressionText))
			{
				@enum = (htmlHelper.ViewData.Eval(expressionText) as Enum);
			}
			if (@enum == null)
			{
				@enum = (modelMetadata.Model as Enum);
			}
			IList<SelectListItem> selectList = EnumHelper.GetSelectList(modelMetadata.ModelType, @enum);
			if (!string.IsNullOrEmpty(optionLabel) && selectList.Count != 0 && string.IsNullOrEmpty(selectList[0].Text))
			{
				selectList[0].Text = optionLabel;
				optionLabel = null;
			}
			return SelectExtensions.DropDownListHelper(htmlHelper, modelMetadata, expressionText, selectList, optionLabel, htmlAttributes);
		}

		// Token: 0x06000CD2 RID: 3282 RVA: 0x00021C31 File Offset: 0x0001FE31
		private static MvcHtmlString DropDownListHelper(HtmlHelper htmlHelper, ModelMetadata metadata, string expression, IEnumerable<SelectListItem> selectList, string optionLabel, IDictionary<string, object> htmlAttributes)
		{
			return htmlHelper.SelectInternal(metadata, optionLabel, expression, selectList, false, htmlAttributes);
		}

		// Token: 0x06000CD3 RID: 3283 RVA: 0x00021C41 File Offset: 0x0001FE41
		public static MvcHtmlString ListBox(this HtmlHelper htmlHelper, string name)
		{
			return htmlHelper.ListBox(name, null, null);
		}

		// Token: 0x06000CD4 RID: 3284 RVA: 0x00021C4C File Offset: 0x0001FE4C
		public static MvcHtmlString ListBox(this HtmlHelper htmlHelper, string name, IEnumerable<SelectListItem> selectList)
		{
			return htmlHelper.ListBox(name, selectList, null);
		}

		// Token: 0x06000CD5 RID: 3285 RVA: 0x00021C57 File Offset: 0x0001FE57
		public static MvcHtmlString ListBox(this HtmlHelper htmlHelper, string name, IEnumerable<SelectListItem> selectList, object htmlAttributes)
		{
			return htmlHelper.ListBox(name, selectList, HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
		}

		// Token: 0x06000CD6 RID: 3286 RVA: 0x00021C67 File Offset: 0x0001FE67
		public static MvcHtmlString ListBox(this HtmlHelper htmlHelper, string name, IEnumerable<SelectListItem> selectList, IDictionary<string, object> htmlAttributes)
		{
			return SelectExtensions.ListBoxHelper(htmlHelper, null, name, selectList, htmlAttributes);
		}

		// Token: 0x06000CD7 RID: 3287 RVA: 0x00021C73 File Offset: 0x0001FE73
		public static MvcHtmlString ListBoxFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, IEnumerable<SelectListItem> selectList)
		{
			return htmlHelper.ListBoxFor(expression, selectList, null);
		}

		// Token: 0x06000CD8 RID: 3288 RVA: 0x00021C7E File Offset: 0x0001FE7E
		public static MvcHtmlString ListBoxFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, IEnumerable<SelectListItem> selectList, object htmlAttributes)
		{
			return htmlHelper.ListBoxFor(expression, selectList, HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
		}

		// Token: 0x06000CD9 RID: 3289 RVA: 0x00021C90 File Offset: 0x0001FE90
		public static MvcHtmlString ListBoxFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, IEnumerable<SelectListItem> selectList, IDictionary<string, object> htmlAttributes)
		{
			if (expression == null)
			{
				throw new ArgumentNullException("expression");
			}
			ModelMetadata metadata = ModelMetadata.FromLambdaExpression<TModel, TProperty>(expression, htmlHelper.ViewData);
			return SelectExtensions.ListBoxHelper(htmlHelper, metadata, ExpressionHelper.GetExpressionText(expression), selectList, htmlAttributes);
		}

		// Token: 0x06000CDA RID: 3290 RVA: 0x00021CC7 File Offset: 0x0001FEC7
		private static MvcHtmlString ListBoxHelper(HtmlHelper htmlHelper, ModelMetadata metadata, string name, IEnumerable<SelectListItem> selectList, IDictionary<string, object> htmlAttributes)
		{
			return htmlHelper.SelectInternal(metadata, null, name, selectList, true, htmlAttributes);
		}

		// Token: 0x06000CDB RID: 3291 RVA: 0x00021CD8 File Offset: 0x0001FED8
		private static IEnumerable<SelectListItem> GetSelectData(this HtmlHelper htmlHelper, string name)
		{
			object obj = null;
			if (htmlHelper.ViewData != null)
			{
				obj = htmlHelper.ViewData.Eval(name);
			}
			if (obj == null)
			{
				throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, MvcResources.HtmlHelper_MissingSelectData, new object[]
				{
					name,
					"IEnumerable<SelectListItem>"
				}));
			}
			IEnumerable<SelectListItem> enumerable = obj as IEnumerable<SelectListItem>;
			if (enumerable == null)
			{
				throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, MvcResources.HtmlHelper_WrongSelectDataType, new object[]
				{
					name,
					obj.GetType().FullName,
					"IEnumerable<SelectListItem>"
				}));
			}
			return enumerable;
		}

		// Token: 0x06000CDC RID: 3292 RVA: 0x00021D6C File Offset: 0x0001FF6C
		internal static string ListItemToOption(SelectListItem item)
		{
			TagBuilder tagBuilder = new TagBuilder("option")
			{
				InnerHtml = HttpUtility.HtmlEncode(item.Text)
			};
			if (item.Value != null)
			{
				tagBuilder.Attributes["value"] = item.Value;
			}
			if (item.Selected)
			{
				tagBuilder.Attributes["selected"] = "selected";
			}
			if (item.Disabled)
			{
				tagBuilder.Attributes["disabled"] = "disabled";
			}
			return tagBuilder.ToString(TagRenderMode.Normal);
		}

		// Token: 0x06000CDD RID: 3293 RVA: 0x00021E10 File Offset: 0x00020010
		private static IEnumerable<SelectListItem> GetSelectListWithDefaultValue(IEnumerable<SelectListItem> selectList, object defaultValue, bool allowMultiple)
		{
			IEnumerable enumerable;
			if (allowMultiple)
			{
				enumerable = (defaultValue as IEnumerable);
				if (enumerable == null || enumerable is string)
				{
					throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, MvcResources.HtmlHelper_SelectExpressionNotEnumerable, new object[]
					{
						"expression"
					}));
				}
			}
			else
			{
				enumerable = new object[]
				{
					defaultValue
				};
			}
			IEnumerable<string> enumerable2 = from object value in enumerable
			select Convert.ToString(value, CultureInfo.CurrentCulture);
			IEnumerable<string> second = from Enum value in enumerable.OfType<Enum>()
			select value.ToString("d");
			enumerable2 = enumerable2.Concat(second);
			HashSet<string> hashSet = new HashSet<string>(enumerable2, StringComparer.OrdinalIgnoreCase);
			List<SelectListItem> list = new List<SelectListItem>();
			foreach (SelectListItem selectListItem in selectList)
			{
				selectListItem.Selected = ((selectListItem.Value != null) ? hashSet.Contains(selectListItem.Value) : hashSet.Contains(selectListItem.Text));
				list.Add(selectListItem);
			}
			return list;
		}

		// Token: 0x06000CDE RID: 3294 RVA: 0x00021F50 File Offset: 0x00020150
		private static MvcHtmlString SelectInternal(this HtmlHelper htmlHelper, ModelMetadata metadata, string optionLabel, string name, IEnumerable<SelectListItem> selectList, bool allowMultiple, IDictionary<string, object> htmlAttributes)
		{
			string fullHtmlFieldName = htmlHelper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(name);
			if (string.IsNullOrEmpty(fullHtmlFieldName))
			{
				throw new ArgumentException(MvcResources.Common_NullOrEmpty, "name");
			}
			bool flag = false;
			if (selectList == null)
			{
				selectList = htmlHelper.GetSelectData(name);
				flag = true;
			}
			object obj = allowMultiple ? htmlHelper.GetModelStateValue(fullHtmlFieldName, typeof(string[])) : htmlHelper.GetModelStateValue(fullHtmlFieldName, typeof(string));
			if (obj == null && !string.IsNullOrEmpty(name))
			{
				if (!flag)
				{
					obj = htmlHelper.ViewData.Eval(name);
				}
				else if (metadata != null)
				{
					obj = metadata.Model;
				}
			}
			if (obj != null)
			{
				selectList = SelectExtensions.GetSelectListWithDefaultValue(selectList, obj, allowMultiple);
			}
			StringBuilder stringBuilder = SelectExtensions.BuildItems(optionLabel, selectList);
			TagBuilder tagBuilder = new TagBuilder("select")
			{
				InnerHtml = stringBuilder.ToString()
			};
			tagBuilder.MergeAttributes<string, object>(htmlAttributes);
			tagBuilder.MergeAttribute("name", fullHtmlFieldName, true);
			tagBuilder.GenerateId(fullHtmlFieldName);
			if (allowMultiple)
			{
				tagBuilder.MergeAttribute("multiple", "multiple");
			}
			ModelState modelState;
			if (htmlHelper.ViewData.ModelState.TryGetValue(fullHtmlFieldName, out modelState) && modelState.Errors.Count > 0)
			{
				tagBuilder.AddCssClass(HtmlHelper.ValidationInputCssClassName);
			}
			tagBuilder.MergeAttributes<string, object>(htmlHelper.GetUnobtrusiveValidationAttributes(name, metadata));
			return tagBuilder.ToMvcHtmlString(TagRenderMode.Normal);
		}

		// Token: 0x06000CDF RID: 3295 RVA: 0x000220BC File Offset: 0x000202BC
		private static StringBuilder BuildItems(string optionLabel, IEnumerable<SelectListItem> selectList)
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (optionLabel != null)
			{
				stringBuilder.AppendLine(SelectExtensions.ListItemToOption(new SelectListItem
				{
					Text = optionLabel,
					Value = string.Empty,
					Selected = false
				}));
			}
			IEnumerable<IGrouping<int, SelectListItem>> enumerable = selectList.GroupBy(delegate(SelectListItem i)
			{
				if (i.Group != null)
				{
					return i.Group.GetHashCode();
				}
				return i.GetHashCode();
			});
			foreach (IGrouping<int, SelectListItem> grouping in enumerable)
			{
				SelectListGroup group = grouping.First<SelectListItem>().Group;
				TagBuilder tagBuilder = null;
				if (group != null)
				{
					tagBuilder = new TagBuilder("optgroup");
					if (group.Name != null)
					{
						tagBuilder.MergeAttribute("label", group.Name);
					}
					if (group.Disabled)
					{
						tagBuilder.MergeAttribute("disabled", "disabled");
					}
					stringBuilder.AppendLine(tagBuilder.ToString(TagRenderMode.StartTag));
				}
				foreach (SelectListItem item in grouping)
				{
					stringBuilder.AppendLine(SelectExtensions.ListItemToOption(item));
				}
				if (group != null)
				{
					stringBuilder.AppendLine(tagBuilder.ToString(TagRenderMode.EndTag));
				}
			}
			return stringBuilder;
		}
	}
}
