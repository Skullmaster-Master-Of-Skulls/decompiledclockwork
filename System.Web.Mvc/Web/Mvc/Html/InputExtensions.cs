using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Globalization;
using System.Linq.Expressions;
using System.Text;
using System.Web.Mvc.Properties;
using System.Web.Routing;

namespace System.Web.Mvc.Html
{
	// Token: 0x020001B9 RID: 441
	public static class InputExtensions
	{
		// Token: 0x06000C86 RID: 3206 RVA: 0x00021158 File Offset: 0x0001F358
		public static MvcHtmlString CheckBox(this HtmlHelper htmlHelper, string name)
		{
			return htmlHelper.CheckBox(name, null);
		}

		// Token: 0x06000C87 RID: 3207 RVA: 0x00021162 File Offset: 0x0001F362
		public static MvcHtmlString CheckBox(this HtmlHelper htmlHelper, string name, bool isChecked)
		{
			return htmlHelper.CheckBox(name, isChecked, null);
		}

		// Token: 0x06000C88 RID: 3208 RVA: 0x0002116D File Offset: 0x0001F36D
		public static MvcHtmlString CheckBox(this HtmlHelper htmlHelper, string name, bool isChecked, object htmlAttributes)
		{
			return htmlHelper.CheckBox(name, isChecked, HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
		}

		// Token: 0x06000C89 RID: 3209 RVA: 0x0002117D File Offset: 0x0001F37D
		public static MvcHtmlString CheckBox(this HtmlHelper htmlHelper, string name, object htmlAttributes)
		{
			return htmlHelper.CheckBox(name, HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
		}

		// Token: 0x06000C8A RID: 3210 RVA: 0x0002118C File Offset: 0x0001F38C
		public static MvcHtmlString CheckBox(this HtmlHelper htmlHelper, string name, IDictionary<string, object> htmlAttributes)
		{
			return InputExtensions.CheckBoxHelper(htmlHelper, null, name, null, htmlAttributes);
		}

		// Token: 0x06000C8B RID: 3211 RVA: 0x000211AB File Offset: 0x0001F3AB
		public static MvcHtmlString CheckBox(this HtmlHelper htmlHelper, string name, bool isChecked, IDictionary<string, object> htmlAttributes)
		{
			return InputExtensions.CheckBoxHelper(htmlHelper, null, name, new bool?(isChecked), htmlAttributes);
		}

		// Token: 0x06000C8C RID: 3212 RVA: 0x000211BC File Offset: 0x0001F3BC
		public static MvcHtmlString CheckBoxFor<TModel>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, bool>> expression)
		{
			return htmlHelper.CheckBoxFor(expression, null);
		}

		// Token: 0x06000C8D RID: 3213 RVA: 0x000211C6 File Offset: 0x0001F3C6
		public static MvcHtmlString CheckBoxFor<TModel>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, bool>> expression, object htmlAttributes)
		{
			return htmlHelper.CheckBoxFor(expression, HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
		}

		// Token: 0x06000C8E RID: 3214 RVA: 0x000211D8 File Offset: 0x0001F3D8
		public static MvcHtmlString CheckBoxFor<TModel>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, bool>> expression, IDictionary<string, object> htmlAttributes)
		{
			if (expression == null)
			{
				throw new ArgumentNullException("expression");
			}
			ModelMetadata modelMetadata = ModelMetadata.FromLambdaExpression<TModel, bool>(expression, htmlHelper.ViewData);
			bool? isChecked = null;
			bool value;
			if (modelMetadata.Model != null && bool.TryParse(modelMetadata.Model.ToString(), out value))
			{
				isChecked = new bool?(value);
			}
			return InputExtensions.CheckBoxHelper(htmlHelper, modelMetadata, ExpressionHelper.GetExpressionText(expression), isChecked, htmlAttributes);
		}

		// Token: 0x06000C8F RID: 3215 RVA: 0x0002123C File Offset: 0x0001F43C
		private static MvcHtmlString CheckBoxHelper(HtmlHelper htmlHelper, ModelMetadata metadata, string name, bool? isChecked, IDictionary<string, object> htmlAttributes)
		{
			RouteValueDictionary routeValueDictionary = InputExtensions.ToRouteValueDictionary(htmlAttributes);
			bool flag = isChecked != null;
			if (flag)
			{
				routeValueDictionary.Remove("checked");
			}
			return InputExtensions.InputHelper(htmlHelper, InputType.CheckBox, metadata, name, "true", !flag, isChecked ?? false, true, false, null, routeValueDictionary);
		}

		// Token: 0x06000C90 RID: 3216 RVA: 0x00021293 File Offset: 0x0001F493
		public static MvcHtmlString Hidden(this HtmlHelper htmlHelper, string name)
		{
			return htmlHelper.Hidden(name, null, null);
		}

		// Token: 0x06000C91 RID: 3217 RVA: 0x0002129E File Offset: 0x0001F49E
		public static MvcHtmlString Hidden(this HtmlHelper htmlHelper, string name, object value)
		{
			return htmlHelper.Hidden(name, value, null);
		}

		// Token: 0x06000C92 RID: 3218 RVA: 0x000212A9 File Offset: 0x0001F4A9
		public static MvcHtmlString Hidden(this HtmlHelper htmlHelper, string name, object value, object htmlAttributes)
		{
			return htmlHelper.Hidden(name, value, HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
		}

		// Token: 0x06000C93 RID: 3219 RVA: 0x000212B9 File Offset: 0x0001F4B9
		public static MvcHtmlString Hidden(this HtmlHelper htmlHelper, string name, object value, IDictionary<string, object> htmlAttributes)
		{
			return InputExtensions.HiddenHelper(htmlHelper, null, value, value == null, name, htmlAttributes);
		}

		// Token: 0x06000C94 RID: 3220 RVA: 0x000212C9 File Offset: 0x0001F4C9
		public static MvcHtmlString HiddenFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression)
		{
			return htmlHelper.HiddenFor(expression, null);
		}

		// Token: 0x06000C95 RID: 3221 RVA: 0x000212D3 File Offset: 0x0001F4D3
		public static MvcHtmlString HiddenFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, object htmlAttributes)
		{
			return htmlHelper.HiddenFor(expression, HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
		}

		// Token: 0x06000C96 RID: 3222 RVA: 0x000212E4 File Offset: 0x0001F4E4
		public static MvcHtmlString HiddenFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, IDictionary<string, object> htmlAttributes)
		{
			ModelMetadata modelMetadata = ModelMetadata.FromLambdaExpression<TModel, TProperty>(expression, htmlHelper.ViewData);
			return InputExtensions.HiddenHelper(htmlHelper, modelMetadata, modelMetadata.Model, false, ExpressionHelper.GetExpressionText(expression), htmlAttributes);
		}

		// Token: 0x06000C97 RID: 3223 RVA: 0x00021314 File Offset: 0x0001F514
		private static MvcHtmlString HiddenHelper(HtmlHelper htmlHelper, ModelMetadata metadata, object value, bool useViewData, string expression, IDictionary<string, object> htmlAttributes)
		{
			Binary binary = value as Binary;
			if (binary != null)
			{
				value = binary.ToArray();
			}
			byte[] array = value as byte[];
			if (array != null)
			{
				value = Convert.ToBase64String(array);
			}
			return InputExtensions.InputHelper(htmlHelper, InputType.Hidden, metadata, expression, value, useViewData, false, true, true, null, htmlAttributes);
		}

		// Token: 0x06000C98 RID: 3224 RVA: 0x0002135D File Offset: 0x0001F55D
		public static MvcHtmlString Password(this HtmlHelper htmlHelper, string name)
		{
			return htmlHelper.Password(name, null);
		}

		// Token: 0x06000C99 RID: 3225 RVA: 0x00021367 File Offset: 0x0001F567
		public static MvcHtmlString Password(this HtmlHelper htmlHelper, string name, object value)
		{
			return htmlHelper.Password(name, value, null);
		}

		// Token: 0x06000C9A RID: 3226 RVA: 0x00021372 File Offset: 0x0001F572
		public static MvcHtmlString Password(this HtmlHelper htmlHelper, string name, object value, object htmlAttributes)
		{
			return htmlHelper.Password(name, value, HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
		}

		// Token: 0x06000C9B RID: 3227 RVA: 0x00021382 File Offset: 0x0001F582
		public static MvcHtmlString Password(this HtmlHelper htmlHelper, string name, object value, IDictionary<string, object> htmlAttributes)
		{
			return InputExtensions.PasswordHelper(htmlHelper, null, name, value, htmlAttributes);
		}

		// Token: 0x06000C9C RID: 3228 RVA: 0x0002138E File Offset: 0x0001F58E
		public static MvcHtmlString PasswordFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression)
		{
			return htmlHelper.PasswordFor(expression, null);
		}

		// Token: 0x06000C9D RID: 3229 RVA: 0x00021398 File Offset: 0x0001F598
		public static MvcHtmlString PasswordFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, object htmlAttributes)
		{
			return htmlHelper.PasswordFor(expression, HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
		}

		// Token: 0x06000C9E RID: 3230 RVA: 0x000213A7 File Offset: 0x0001F5A7
		public static MvcHtmlString PasswordFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, IDictionary<string, object> htmlAttributes)
		{
			if (expression == null)
			{
				throw new ArgumentNullException("expression");
			}
			return InputExtensions.PasswordHelper(htmlHelper, ModelMetadata.FromLambdaExpression<TModel, TProperty>(expression, htmlHelper.ViewData), ExpressionHelper.GetExpressionText(expression), null, htmlAttributes);
		}

		// Token: 0x06000C9F RID: 3231 RVA: 0x000213D4 File Offset: 0x0001F5D4
		private static MvcHtmlString PasswordHelper(HtmlHelper htmlHelper, ModelMetadata metadata, string name, object value, IDictionary<string, object> htmlAttributes)
		{
			return InputExtensions.InputHelper(htmlHelper, InputType.Password, metadata, name, value, false, false, true, true, null, htmlAttributes);
		}

		// Token: 0x06000CA0 RID: 3232 RVA: 0x000213F2 File Offset: 0x0001F5F2
		public static MvcHtmlString RadioButton(this HtmlHelper htmlHelper, string name, object value)
		{
			return htmlHelper.RadioButton(name, value, null);
		}

		// Token: 0x06000CA1 RID: 3233 RVA: 0x000213FD File Offset: 0x0001F5FD
		public static MvcHtmlString RadioButton(this HtmlHelper htmlHelper, string name, object value, object htmlAttributes)
		{
			return htmlHelper.RadioButton(name, value, HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
		}

		// Token: 0x06000CA2 RID: 3234 RVA: 0x00021410 File Offset: 0x0001F610
		public static MvcHtmlString RadioButton(this HtmlHelper htmlHelper, string name, object value, IDictionary<string, object> htmlAttributes)
		{
			string b = Convert.ToString(value, CultureInfo.CurrentCulture);
			bool isChecked = !string.IsNullOrEmpty(name) && string.Equals(htmlHelper.EvalString(name), b, StringComparison.OrdinalIgnoreCase);
			RouteValueDictionary routeValueDictionary = InputExtensions.ToRouteValueDictionary(htmlAttributes);
			if (routeValueDictionary.ContainsKey("checked"))
			{
				return InputExtensions.InputHelper(htmlHelper, InputType.Radio, null, name, value, false, false, true, true, null, routeValueDictionary);
			}
			return htmlHelper.RadioButton(name, value, isChecked, htmlAttributes);
		}

		// Token: 0x06000CA3 RID: 3235 RVA: 0x00021472 File Offset: 0x0001F672
		public static MvcHtmlString RadioButton(this HtmlHelper htmlHelper, string name, object value, bool isChecked)
		{
			return htmlHelper.RadioButton(name, value, isChecked, null);
		}

		// Token: 0x06000CA4 RID: 3236 RVA: 0x0002147E File Offset: 0x0001F67E
		public static MvcHtmlString RadioButton(this HtmlHelper htmlHelper, string name, object value, bool isChecked, object htmlAttributes)
		{
			return htmlHelper.RadioButton(name, value, isChecked, HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
		}

		// Token: 0x06000CA5 RID: 3237 RVA: 0x00021490 File Offset: 0x0001F690
		public static MvcHtmlString RadioButton(this HtmlHelper htmlHelper, string name, object value, bool isChecked, IDictionary<string, object> htmlAttributes)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			RouteValueDictionary routeValueDictionary = InputExtensions.ToRouteValueDictionary(htmlAttributes);
			routeValueDictionary.Remove("checked");
			return InputExtensions.InputHelper(htmlHelper, InputType.Radio, null, name, value, false, isChecked, true, true, null, routeValueDictionary);
		}

		// Token: 0x06000CA6 RID: 3238 RVA: 0x000214CF File Offset: 0x0001F6CF
		public static MvcHtmlString RadioButtonFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, object value)
		{
			return htmlHelper.RadioButtonFor(expression, value, null);
		}

		// Token: 0x06000CA7 RID: 3239 RVA: 0x000214DA File Offset: 0x0001F6DA
		public static MvcHtmlString RadioButtonFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, object value, object htmlAttributes)
		{
			return htmlHelper.RadioButtonFor(expression, value, HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
		}

		// Token: 0x06000CA8 RID: 3240 RVA: 0x000214EC File Offset: 0x0001F6EC
		public static MvcHtmlString RadioButtonFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, object value, IDictionary<string, object> htmlAttributes)
		{
			ModelMetadata modelMetadata = ModelMetadata.FromLambdaExpression<TModel, TProperty>(expression, htmlHelper.ViewData);
			return InputExtensions.RadioButtonHelper(htmlHelper, modelMetadata, modelMetadata.Model, ExpressionHelper.GetExpressionText(expression), value, null, htmlAttributes);
		}

		// Token: 0x06000CA9 RID: 3241 RVA: 0x00021524 File Offset: 0x0001F724
		private static MvcHtmlString RadioButtonHelper(HtmlHelper htmlHelper, ModelMetadata metadata, object model, string name, object value, bool? isChecked, IDictionary<string, object> htmlAttributes)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			RouteValueDictionary routeValueDictionary = InputExtensions.ToRouteValueDictionary(htmlAttributes);
			bool flag = isChecked != null;
			if (flag)
			{
				routeValueDictionary.Remove("checked");
			}
			else
			{
				string b = Convert.ToString(value, CultureInfo.CurrentCulture);
				isChecked = new bool?(model != null && !string.IsNullOrEmpty(name) && string.Equals(model.ToString(), b, StringComparison.OrdinalIgnoreCase));
			}
			return InputExtensions.InputHelper(htmlHelper, InputType.Radio, metadata, name, value, false, isChecked ?? false, true, true, null, routeValueDictionary);
		}

		// Token: 0x06000CAA RID: 3242 RVA: 0x000215B6 File Offset: 0x0001F7B6
		public static MvcHtmlString TextBox(this HtmlHelper htmlHelper, string name)
		{
			return htmlHelper.TextBox(name, null);
		}

		// Token: 0x06000CAB RID: 3243 RVA: 0x000215C0 File Offset: 0x0001F7C0
		public static MvcHtmlString TextBox(this HtmlHelper htmlHelper, string name, object value)
		{
			return htmlHelper.TextBox(name, value, null);
		}

		// Token: 0x06000CAC RID: 3244 RVA: 0x000215CB File Offset: 0x0001F7CB
		public static MvcHtmlString TextBox(this HtmlHelper htmlHelper, string name, object value, string format)
		{
			return htmlHelper.TextBox(name, value, format, null);
		}

		// Token: 0x06000CAD RID: 3245 RVA: 0x000215D7 File Offset: 0x0001F7D7
		public static MvcHtmlString TextBox(this HtmlHelper htmlHelper, string name, object value, object htmlAttributes)
		{
			return htmlHelper.TextBox(name, value, null, htmlAttributes);
		}

		// Token: 0x06000CAE RID: 3246 RVA: 0x000215E3 File Offset: 0x0001F7E3
		public static MvcHtmlString TextBox(this HtmlHelper htmlHelper, string name, object value, string format, object htmlAttributes)
		{
			return htmlHelper.TextBox(name, value, format, HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
		}

		// Token: 0x06000CAF RID: 3247 RVA: 0x000215F5 File Offset: 0x0001F7F5
		public static MvcHtmlString TextBox(this HtmlHelper htmlHelper, string name, object value, IDictionary<string, object> htmlAttributes)
		{
			return htmlHelper.TextBox(name, value, null, htmlAttributes);
		}

		// Token: 0x06000CB0 RID: 3248 RVA: 0x00021604 File Offset: 0x0001F804
		public static MvcHtmlString TextBox(this HtmlHelper htmlHelper, string name, object value, string format, IDictionary<string, object> htmlAttributes)
		{
			return InputExtensions.InputHelper(htmlHelper, InputType.Text, null, name, value, value == null, false, true, true, format, htmlAttributes);
		}

		// Token: 0x06000CB1 RID: 3249 RVA: 0x00021625 File Offset: 0x0001F825
		public static MvcHtmlString TextBoxFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression)
		{
			return htmlHelper.TextBoxFor(expression, null);
		}

		// Token: 0x06000CB2 RID: 3250 RVA: 0x0002162F File Offset: 0x0001F82F
		public static MvcHtmlString TextBoxFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, string format)
		{
			return htmlHelper.TextBoxFor(expression, format, null);
		}

		// Token: 0x06000CB3 RID: 3251 RVA: 0x0002163A File Offset: 0x0001F83A
		public static MvcHtmlString TextBoxFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, object htmlAttributes)
		{
			return htmlHelper.TextBoxFor(expression, null, htmlAttributes);
		}

		// Token: 0x06000CB4 RID: 3252 RVA: 0x00021645 File Offset: 0x0001F845
		public static MvcHtmlString TextBoxFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, string format, object htmlAttributes)
		{
			return htmlHelper.TextBoxFor(expression, format, HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
		}

		// Token: 0x06000CB5 RID: 3253 RVA: 0x00021655 File Offset: 0x0001F855
		public static MvcHtmlString TextBoxFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, IDictionary<string, object> htmlAttributes)
		{
			return htmlHelper.TextBoxFor(expression, null, htmlAttributes);
		}

		// Token: 0x06000CB6 RID: 3254 RVA: 0x00021660 File Offset: 0x0001F860
		public static MvcHtmlString TextBoxFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, string format, IDictionary<string, object> htmlAttributes)
		{
			ModelMetadata modelMetadata = ModelMetadata.FromLambdaExpression<TModel, TProperty>(expression, htmlHelper.ViewData);
			return htmlHelper.TextBoxHelper(modelMetadata, modelMetadata.Model, ExpressionHelper.GetExpressionText(expression), format, htmlAttributes);
		}

		// Token: 0x06000CB7 RID: 3255 RVA: 0x00021690 File Offset: 0x0001F890
		private static MvcHtmlString TextBoxHelper(this HtmlHelper htmlHelper, ModelMetadata metadata, object model, string expression, string format, IDictionary<string, object> htmlAttributes)
		{
			return InputExtensions.InputHelper(htmlHelper, InputType.Text, metadata, expression, model, false, false, true, true, format, htmlAttributes);
		}

		// Token: 0x06000CB8 RID: 3256 RVA: 0x000216B0 File Offset: 0x0001F8B0
		private static MvcHtmlString InputHelper(HtmlHelper htmlHelper, InputType inputType, ModelMetadata metadata, string name, object value, bool useViewData, bool isChecked, bool setId, bool isExplicitValue, string format, IDictionary<string, object> htmlAttributes)
		{
			string fullHtmlFieldName = htmlHelper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(name);
			if (string.IsNullOrEmpty(fullHtmlFieldName))
			{
				throw new ArgumentException(MvcResources.Common_NullOrEmpty, "name");
			}
			TagBuilder tagBuilder = new TagBuilder("input");
			tagBuilder.MergeAttributes<string, object>(htmlAttributes);
			tagBuilder.MergeAttribute("type", HtmlHelper.GetInputTypeString(inputType));
			tagBuilder.MergeAttribute("name", fullHtmlFieldName, true);
			string text = htmlHelper.FormatValue(value, format);
			bool flag = false;
			switch (inputType)
			{
			case InputType.CheckBox:
			{
				bool? flag2 = htmlHelper.GetModelStateValue(fullHtmlFieldName, typeof(bool)) as bool?;
				if (flag2 != null)
				{
					isChecked = flag2.Value;
					flag = true;
				}
				break;
			}
			case InputType.Hidden:
				goto IL_131;
			case InputType.Password:
				if (value != null)
				{
					tagBuilder.MergeAttribute("value", text, isExplicitValue);
					goto IL_16C;
				}
				goto IL_16C;
			case InputType.Radio:
				break;
			default:
				goto IL_131;
			}
			if (!flag)
			{
				string text2 = htmlHelper.GetModelStateValue(fullHtmlFieldName, typeof(string)) as string;
				if (text2 != null)
				{
					isChecked = string.Equals(text2, text, StringComparison.Ordinal);
					flag = true;
				}
			}
			if (!flag && useViewData)
			{
				isChecked = htmlHelper.EvalBoolean(fullHtmlFieldName);
			}
			if (isChecked)
			{
				tagBuilder.MergeAttribute("checked", "checked");
			}
			tagBuilder.MergeAttribute("value", text, isExplicitValue);
			goto IL_16C;
			IL_131:
			string text3 = (string)htmlHelper.GetModelStateValue(fullHtmlFieldName, typeof(string));
			tagBuilder.MergeAttribute("value", text3 ?? (useViewData ? htmlHelper.EvalString(fullHtmlFieldName, format) : text), isExplicitValue);
			IL_16C:
			if (setId)
			{
				tagBuilder.GenerateId(fullHtmlFieldName);
			}
			ModelState modelState;
			if (htmlHelper.ViewData.ModelState.TryGetValue(fullHtmlFieldName, out modelState) && modelState.Errors.Count > 0)
			{
				tagBuilder.AddCssClass(HtmlHelper.ValidationInputCssClassName);
			}
			tagBuilder.MergeAttributes<string, object>(htmlHelper.GetUnobtrusiveValidationAttributes(name, metadata));
			if (inputType == InputType.CheckBox)
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append(tagBuilder.ToString(TagRenderMode.SelfClosing));
				TagBuilder tagBuilder2 = new TagBuilder("input");
				tagBuilder2.MergeAttribute("type", HtmlHelper.GetInputTypeString(InputType.Hidden));
				tagBuilder2.MergeAttribute("name", fullHtmlFieldName);
				tagBuilder2.MergeAttribute("value", "false");
				stringBuilder.Append(tagBuilder2.ToString(TagRenderMode.SelfClosing));
				return MvcHtmlString.Create(stringBuilder.ToString());
			}
			return tagBuilder.ToMvcHtmlString(TagRenderMode.SelfClosing);
		}

		// Token: 0x06000CB9 RID: 3257 RVA: 0x000218EA File Offset: 0x0001FAEA
		private static RouteValueDictionary ToRouteValueDictionary(IDictionary<string, object> dictionary)
		{
			if (dictionary != null)
			{
				return new RouteValueDictionary(dictionary);
			}
			return new RouteValueDictionary();
		}
	}
}
