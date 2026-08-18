using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Web.Mvc.Properties;
using System.Web.Routing;

namespace System.Web.Mvc.Html
{
	// Token: 0x020001BD RID: 445
	public static class ValidationExtensions
	{
		// Token: 0x170002E1 RID: 737
		// (get) Token: 0x06000CF3 RID: 3315 RVA: 0x00022540 File Offset: 0x00020740
		// (set) Token: 0x06000CF4 RID: 3316 RVA: 0x00022550 File Offset: 0x00020750
		public static string ResourceClassKey
		{
			get
			{
				return ValidationExtensions._resourceClassKey ?? string.Empty;
			}
			set
			{
				ValidationExtensions._resourceClassKey = value;
			}
		}

		// Token: 0x06000CF5 RID: 3317 RVA: 0x00022560 File Offset: 0x00020760
		private static FieldValidationMetadata ApplyFieldValidationMetadata(HtmlHelper htmlHelper, ModelMetadata modelMetadata, string modelName)
		{
			FormContext formContext = htmlHelper.ViewContext.FormContext;
			FieldValidationMetadata validationMetadataForField = formContext.GetValidationMetadataForField(modelName, true);
			IEnumerable<ModelValidator> validators = ModelValidatorProviders.Providers.GetValidators(modelMetadata, htmlHelper.ViewContext);
			foreach (ModelClientValidationRule item in validators.SelectMany((ModelValidator v) => v.GetClientValidationRules()))
			{
				validationMetadataForField.ValidationRules.Add(item);
			}
			return validationMetadataForField;
		}

		// Token: 0x06000CF6 RID: 3318 RVA: 0x000225FC File Offset: 0x000207FC
		private static string GetInvalidPropertyValueResource(HttpContextBase httpContext)
		{
			string text = null;
			if (!string.IsNullOrEmpty(ValidationExtensions.ResourceClassKey) && httpContext != null)
			{
				text = (httpContext.GetGlobalResourceObject(ValidationExtensions.ResourceClassKey, "InvalidPropertyValue", CultureInfo.CurrentUICulture) as string);
			}
			return text ?? MvcResources.Common_ValueNotValidForProperty;
		}

		// Token: 0x06000CF7 RID: 3319 RVA: 0x00022640 File Offset: 0x00020840
		private static string GetUserErrorMessageOrDefault(HttpContextBase httpContext, ModelError error, ModelState modelState)
		{
			if (!string.IsNullOrEmpty(error.ErrorMessage))
			{
				return error.ErrorMessage;
			}
			if (modelState == null)
			{
				return null;
			}
			string text = (modelState.Value != null) ? modelState.Value.AttemptedValue : null;
			return string.Format(CultureInfo.CurrentCulture, ValidationExtensions.GetInvalidPropertyValueResource(httpContext), new object[]
			{
				text
			});
		}

		// Token: 0x06000CF8 RID: 3320 RVA: 0x00022699 File Offset: 0x00020899
		public static void Validate(this HtmlHelper htmlHelper, string modelName)
		{
			if (modelName == null)
			{
				throw new ArgumentNullException("modelName");
			}
			ValidationExtensions.ValidateHelper(htmlHelper, ModelMetadata.FromStringExpression(modelName, htmlHelper.ViewContext.ViewData), modelName);
		}

		// Token: 0x06000CF9 RID: 3321 RVA: 0x000226C1 File Offset: 0x000208C1
		public static void ValidateFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression)
		{
			ValidationExtensions.ValidateHelper(htmlHelper, ModelMetadata.FromLambdaExpression<TModel, TProperty>(expression, htmlHelper.ViewData), ExpressionHelper.GetExpressionText(expression));
		}

		// Token: 0x06000CFA RID: 3322 RVA: 0x000226DC File Offset: 0x000208DC
		private static void ValidateHelper(HtmlHelper htmlHelper, ModelMetadata modelMetadata, string expression)
		{
			FormContext formContextForClientValidation = htmlHelper.ViewContext.GetFormContextForClientValidation();
			if (formContextForClientValidation == null || htmlHelper.ViewContext.UnobtrusiveJavaScriptEnabled)
			{
				return;
			}
			string fullHtmlFieldName = htmlHelper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(expression);
			ValidationExtensions.ApplyFieldValidationMetadata(htmlHelper, modelMetadata, fullHtmlFieldName);
		}

		// Token: 0x06000CFB RID: 3323 RVA: 0x00022726 File Offset: 0x00020926
		public static MvcHtmlString ValidationMessage(this HtmlHelper htmlHelper, string modelName)
		{
			return htmlHelper.ValidationMessage(modelName, null, new RouteValueDictionary());
		}

		// Token: 0x06000CFC RID: 3324 RVA: 0x00022735 File Offset: 0x00020935
		public static MvcHtmlString ValidationMessage(this HtmlHelper htmlHelper, string modelName, object htmlAttributes)
		{
			return htmlHelper.ValidationMessage(modelName, null, HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
		}

		// Token: 0x06000CFD RID: 3325 RVA: 0x00022745 File Offset: 0x00020945
		public static MvcHtmlString ValidationMessage(this HtmlHelper htmlHelper, string modelName, object htmlAttributes, string tag)
		{
			return htmlHelper.ValidationMessage(modelName, null, HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes), tag);
		}

		// Token: 0x06000CFE RID: 3326 RVA: 0x00022756 File Offset: 0x00020956
		public static MvcHtmlString ValidationMessage(this HtmlHelper htmlHelper, string modelName, string validationMessage)
		{
			return htmlHelper.ValidationMessage(modelName, validationMessage, new RouteValueDictionary());
		}

		// Token: 0x06000CFF RID: 3327 RVA: 0x00022765 File Offset: 0x00020965
		public static MvcHtmlString ValidationMessage(this HtmlHelper htmlHelper, string modelName, string validationMessage, object htmlAttributes)
		{
			return htmlHelper.ValidationMessage(modelName, validationMessage, HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
		}

		// Token: 0x06000D00 RID: 3328 RVA: 0x00022775 File Offset: 0x00020975
		public static MvcHtmlString ValidationMessage(this HtmlHelper htmlHelper, string modelName, string validationMessage, object htmlAttributes, string tag)
		{
			return htmlHelper.ValidationMessage(modelName, validationMessage, HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes), tag);
		}

		// Token: 0x06000D01 RID: 3329 RVA: 0x00022787 File Offset: 0x00020987
		public static MvcHtmlString ValidationMessage(this HtmlHelper htmlHelper, string modelName, string validationMessage, string tag)
		{
			return htmlHelper.ValidationMessage(modelName, validationMessage, new RouteValueDictionary(), tag);
		}

		// Token: 0x06000D02 RID: 3330 RVA: 0x00022797 File Offset: 0x00020997
		public static MvcHtmlString ValidationMessage(this HtmlHelper htmlHelper, string modelName, IDictionary<string, object> htmlAttributes)
		{
			return htmlHelper.ValidationMessage(modelName, null, htmlAttributes);
		}

		// Token: 0x06000D03 RID: 3331 RVA: 0x000227A2 File Offset: 0x000209A2
		public static MvcHtmlString ValidationMessage(this HtmlHelper htmlHelper, string modelName, IDictionary<string, object> htmlAttributes, string tag)
		{
			return htmlHelper.ValidationMessage(modelName, null, htmlAttributes, tag);
		}

		// Token: 0x06000D04 RID: 3332 RVA: 0x000227AE File Offset: 0x000209AE
		public static MvcHtmlString ValidationMessage(this HtmlHelper htmlHelper, string modelName, string validationMessage, IDictionary<string, object> htmlAttributes)
		{
			return htmlHelper.ValidationMessage(modelName, validationMessage, htmlAttributes, null);
		}

		// Token: 0x06000D05 RID: 3333 RVA: 0x000227BA File Offset: 0x000209BA
		public static MvcHtmlString ValidationMessage(this HtmlHelper htmlHelper, string modelName, string validationMessage, IDictionary<string, object> htmlAttributes, string tag)
		{
			if (modelName == null)
			{
				throw new ArgumentNullException("modelName");
			}
			return htmlHelper.ValidationMessageHelper(ModelMetadata.FromStringExpression(modelName, htmlHelper.ViewContext.ViewData), modelName, validationMessage, htmlAttributes, tag);
		}

		// Token: 0x06000D06 RID: 3334 RVA: 0x000227E6 File Offset: 0x000209E6
		public static MvcHtmlString ValidationMessageFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression)
		{
			return htmlHelper.ValidationMessageFor(expression, null, new RouteValueDictionary());
		}

		// Token: 0x06000D07 RID: 3335 RVA: 0x000227F5 File Offset: 0x000209F5
		public static MvcHtmlString ValidationMessageFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, string validationMessage)
		{
			return htmlHelper.ValidationMessageFor(expression, validationMessage, new RouteValueDictionary());
		}

		// Token: 0x06000D08 RID: 3336 RVA: 0x00022804 File Offset: 0x00020A04
		public static MvcHtmlString ValidationMessageFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, string validationMessage, object htmlAttributes)
		{
			return htmlHelper.ValidationMessageFor(expression, validationMessage, HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
		}

		// Token: 0x06000D09 RID: 3337 RVA: 0x00022814 File Offset: 0x00020A14
		public static MvcHtmlString ValidationMessageFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, string validationMessage, IDictionary<string, object> htmlAttributes)
		{
			return htmlHelper.ValidationMessageFor(expression, validationMessage, htmlAttributes, null);
		}

		// Token: 0x06000D0A RID: 3338 RVA: 0x00022820 File Offset: 0x00020A20
		public static MvcHtmlString ValidationMessageFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, string validationMessage, string tag)
		{
			return htmlHelper.ValidationMessageFor(expression, validationMessage, null, tag);
		}

		// Token: 0x06000D0B RID: 3339 RVA: 0x0002282C File Offset: 0x00020A2C
		public static MvcHtmlString ValidationMessageFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, string validationMessage, object htmlAttributes, string tag)
		{
			return htmlHelper.ValidationMessageFor(expression, validationMessage, HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes), tag);
		}

		// Token: 0x06000D0C RID: 3340 RVA: 0x0002283E File Offset: 0x00020A3E
		public static MvcHtmlString ValidationMessageFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, string validationMessage, IDictionary<string, object> htmlAttributes, string tag)
		{
			return htmlHelper.ValidationMessageHelper(ModelMetadata.FromLambdaExpression<TModel, TProperty>(expression, htmlHelper.ViewData), ExpressionHelper.GetExpressionText(expression), validationMessage, htmlAttributes, tag);
		}

		// Token: 0x06000D0D RID: 3341 RVA: 0x0002286C File Offset: 0x00020A6C
		private static MvcHtmlString ValidationMessageHelper(this HtmlHelper htmlHelper, ModelMetadata modelMetadata, string expression, string validationMessage, IDictionary<string, object> htmlAttributes, string tag)
		{
			string fullHtmlFieldName = htmlHelper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(expression);
			FormContext formContextForClientValidation = htmlHelper.ViewContext.GetFormContextForClientValidation();
			if (!htmlHelper.ViewData.ModelState.ContainsKey(fullHtmlFieldName) && formContextForClientValidation == null)
			{
				return null;
			}
			ModelState modelState = htmlHelper.ViewData.ModelState[fullHtmlFieldName];
			ModelErrorCollection modelErrorCollection = (modelState == null) ? null : modelState.Errors;
			ModelError modelError;
			if (modelErrorCollection != null && modelErrorCollection.Count != 0)
			{
				modelError = (modelErrorCollection.FirstOrDefault((ModelError m) => !string.IsNullOrEmpty(m.ErrorMessage)) ?? modelErrorCollection[0]);
			}
			else
			{
				modelError = null;
			}
			ModelError modelError2 = modelError;
			if (modelError2 == null && formContextForClientValidation == null)
			{
				return null;
			}
			if (string.IsNullOrEmpty(tag))
			{
				tag = htmlHelper.ViewContext.ValidationMessageElement;
			}
			TagBuilder tagBuilder = new TagBuilder(tag);
			tagBuilder.MergeAttributes<string, object>(htmlAttributes);
			tagBuilder.AddCssClass((modelError2 != null) ? HtmlHelper.ValidationMessageCssClassName : HtmlHelper.ValidationMessageValidCssClassName);
			if (!string.IsNullOrEmpty(validationMessage))
			{
				tagBuilder.SetInnerText(validationMessage);
			}
			else if (modelError2 != null)
			{
				tagBuilder.SetInnerText(ValidationExtensions.GetUserErrorMessageOrDefault(htmlHelper.ViewContext.HttpContext, modelError2, modelState));
			}
			if (formContextForClientValidation != null)
			{
				bool replaceValidationMessageContents = string.IsNullOrEmpty(validationMessage);
				if (htmlHelper.ViewContext.UnobtrusiveJavaScriptEnabled)
				{
					tagBuilder.MergeAttribute("data-valmsg-for", fullHtmlFieldName);
					tagBuilder.MergeAttribute("data-valmsg-replace", replaceValidationMessageContents.ToString().ToLowerInvariant());
				}
				else
				{
					FieldValidationMetadata fieldValidationMetadata = ValidationExtensions.ApplyFieldValidationMetadata(htmlHelper, modelMetadata, fullHtmlFieldName);
					fieldValidationMetadata.ReplaceValidationMessageContents = replaceValidationMessageContents;
					tagBuilder.GenerateId(fullHtmlFieldName + "_validationMessage");
					fieldValidationMetadata.ValidationMessageId = tagBuilder.Attributes["id"];
				}
			}
			return tagBuilder.ToMvcHtmlString(TagRenderMode.Normal);
		}

		// Token: 0x06000D0E RID: 3342 RVA: 0x00022A0D File Offset: 0x00020C0D
		public static MvcHtmlString ValidationSummary(this HtmlHelper htmlHelper)
		{
			return htmlHelper.ValidationSummary(false);
		}

		// Token: 0x06000D0F RID: 3343 RVA: 0x00022A16 File Offset: 0x00020C16
		public static MvcHtmlString ValidationSummary(this HtmlHelper htmlHelper, bool excludePropertyErrors)
		{
			return htmlHelper.ValidationSummary(excludePropertyErrors, null);
		}

		// Token: 0x06000D10 RID: 3344 RVA: 0x00022A20 File Offset: 0x00020C20
		public static MvcHtmlString ValidationSummary(this HtmlHelper htmlHelper, string message)
		{
			return htmlHelper.ValidationSummary(false, message, null, null);
		}

		// Token: 0x06000D11 RID: 3345 RVA: 0x00022A2C File Offset: 0x00020C2C
		public static MvcHtmlString ValidationSummary(this HtmlHelper htmlHelper, string message, string headingTag)
		{
			return htmlHelper.ValidationSummary(false, message, null, headingTag);
		}

		// Token: 0x06000D12 RID: 3346 RVA: 0x00022A38 File Offset: 0x00020C38
		public static MvcHtmlString ValidationSummary(this HtmlHelper htmlHelper, bool excludePropertyErrors, string message)
		{
			return htmlHelper.ValidationSummary(excludePropertyErrors, message, null, null);
		}

		// Token: 0x06000D13 RID: 3347 RVA: 0x00022A44 File Offset: 0x00020C44
		public static MvcHtmlString ValidationSummary(this HtmlHelper htmlHelper, bool excludePropertyErrors, string message, string headingTag)
		{
			return htmlHelper.ValidationSummary(excludePropertyErrors, message, null, headingTag);
		}

		// Token: 0x06000D14 RID: 3348 RVA: 0x00022A50 File Offset: 0x00020C50
		public static MvcHtmlString ValidationSummary(this HtmlHelper htmlHelper, string message, object htmlAttributes)
		{
			return htmlHelper.ValidationSummary(false, message, HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes), null);
		}

		// Token: 0x06000D15 RID: 3349 RVA: 0x00022A61 File Offset: 0x00020C61
		public static MvcHtmlString ValidationSummary(this HtmlHelper htmlHelper, string message, object htmlAttributes, string headingTag)
		{
			return htmlHelper.ValidationSummary(false, message, HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes), headingTag);
		}

		// Token: 0x06000D16 RID: 3350 RVA: 0x00022A72 File Offset: 0x00020C72
		public static MvcHtmlString ValidationSummary(this HtmlHelper htmlHelper, bool excludePropertyErrors, string message, object htmlAttributes)
		{
			return htmlHelper.ValidationSummary(excludePropertyErrors, message, HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes), null);
		}

		// Token: 0x06000D17 RID: 3351 RVA: 0x00022A83 File Offset: 0x00020C83
		public static MvcHtmlString ValidationSummary(this HtmlHelper htmlHelper, bool excludePropertyErrors, string message, object htmlAttributes, string headingTag)
		{
			return htmlHelper.ValidationSummary(excludePropertyErrors, message, HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes), headingTag);
		}

		// Token: 0x06000D18 RID: 3352 RVA: 0x00022A95 File Offset: 0x00020C95
		public static MvcHtmlString ValidationSummary(this HtmlHelper htmlHelper, string message, IDictionary<string, object> htmlAttributes)
		{
			return htmlHelper.ValidationSummary(false, message, htmlAttributes, null);
		}

		// Token: 0x06000D19 RID: 3353 RVA: 0x00022AA1 File Offset: 0x00020CA1
		public static MvcHtmlString ValidationSummary(this HtmlHelper htmlHelper, string message, IDictionary<string, object> htmlAttributes, string headingTag)
		{
			return htmlHelper.ValidationSummary(false, message, htmlAttributes, headingTag);
		}

		// Token: 0x06000D1A RID: 3354 RVA: 0x00022AAD File Offset: 0x00020CAD
		public static MvcHtmlString ValidationSummary(this HtmlHelper htmlHelper, bool excludePropertyErrors, string message, IDictionary<string, object> htmlAttributes)
		{
			return htmlHelper.ValidationSummary(excludePropertyErrors, message, htmlAttributes, null);
		}

		// Token: 0x06000D1B RID: 3355 RVA: 0x00022ABC File Offset: 0x00020CBC
		public static MvcHtmlString ValidationSummary(this HtmlHelper htmlHelper, bool excludePropertyErrors, string message, IDictionary<string, object> htmlAttributes, string headingTag)
		{
			if (htmlHelper == null)
			{
				throw new ArgumentNullException("htmlHelper");
			}
			FormContext formContextForClientValidation = htmlHelper.ViewContext.GetFormContextForClientValidation();
			if (htmlHelper.ViewData.ModelState.IsValid)
			{
				if (formContextForClientValidation == null)
				{
					return null;
				}
				if (htmlHelper.ViewContext.UnobtrusiveJavaScriptEnabled && excludePropertyErrors)
				{
					return null;
				}
			}
			string str;
			if (!string.IsNullOrEmpty(message))
			{
				if (string.IsNullOrEmpty(headingTag))
				{
					headingTag = htmlHelper.ViewContext.ValidationSummaryMessageElement;
				}
				TagBuilder tagBuilder = new TagBuilder(headingTag);
				tagBuilder.SetInnerText(message);
				str = tagBuilder.ToString(TagRenderMode.Normal) + Environment.NewLine;
			}
			else
			{
				str = null;
			}
			StringBuilder stringBuilder = new StringBuilder();
			TagBuilder tagBuilder2 = new TagBuilder("ul");
			IEnumerable<ModelState> modelStateList = ValidationExtensions.GetModelStateList(htmlHelper, excludePropertyErrors);
			foreach (ModelState modelState in modelStateList)
			{
				foreach (ModelError error in modelState.Errors)
				{
					string userErrorMessageOrDefault = ValidationExtensions.GetUserErrorMessageOrDefault(htmlHelper.ViewContext.HttpContext, error, null);
					if (!string.IsNullOrEmpty(userErrorMessageOrDefault))
					{
						TagBuilder tagBuilder3 = new TagBuilder("li");
						tagBuilder3.SetInnerText(userErrorMessageOrDefault);
						stringBuilder.AppendLine(tagBuilder3.ToString(TagRenderMode.Normal));
					}
				}
			}
			if (stringBuilder.Length == 0)
			{
				stringBuilder.AppendLine("<li style=\"display:none\"></li>");
			}
			tagBuilder2.InnerHtml = stringBuilder.ToString();
			TagBuilder tagBuilder4 = new TagBuilder("div");
			tagBuilder4.MergeAttributes<string, object>(htmlAttributes);
			tagBuilder4.AddCssClass(htmlHelper.ViewData.ModelState.IsValid ? HtmlHelper.ValidationSummaryValidCssClassName : HtmlHelper.ValidationSummaryCssClassName);
			tagBuilder4.InnerHtml = str + tagBuilder2.ToString(TagRenderMode.Normal);
			if (formContextForClientValidation != null)
			{
				if (htmlHelper.ViewContext.UnobtrusiveJavaScriptEnabled)
				{
					if (!excludePropertyErrors)
					{
						tagBuilder4.MergeAttribute("data-valmsg-summary", "true");
					}
				}
				else
				{
					tagBuilder4.GenerateId("validationSummary");
					formContextForClientValidation.ValidationSummaryId = tagBuilder4.Attributes["id"];
					formContextForClientValidation.ReplaceValidationSummary = !excludePropertyErrors;
				}
			}
			return tagBuilder4.ToMvcHtmlString(TagRenderMode.Normal);
		}

		// Token: 0x06000D1C RID: 3356 RVA: 0x00022E58 File Offset: 0x00021058
		private static IEnumerable<ModelState> GetModelStateList(HtmlHelper htmlHelper, bool excludePropertyErrors)
		{
			if (!excludePropertyErrors)
			{
				Dictionary<string, int> ordering = new Dictionary<string, int>();
				ModelMetadata modelMetadata = htmlHelper.ViewData.ModelMetadata;
				if (modelMetadata != null)
				{
					foreach (ModelMetadata modelMetadata2 in modelMetadata.Properties)
					{
						ordering[modelMetadata2.PropertyName] = modelMetadata2.Order;
					}
				}
				return from kv in htmlHelper.ViewData.ModelState
				let name = kv.Key
				orderby ordering.GetOrDefault(name, 10000)
				select kv.Value;
			}
			ModelState modelState;
			htmlHelper.ViewData.ModelState.TryGetValue(htmlHelper.ViewData.TemplateInfo.HtmlFieldPrefix, out modelState);
			if (modelState != null)
			{
				return new ModelState[]
				{
					modelState
				};
			}
			return new ModelState[0];
		}

		// Token: 0x04000360 RID: 864
		private const string HiddenListItem = "<li style=\"display:none\"></li>";

		// Token: 0x04000361 RID: 865
		private static string _resourceClassKey;
	}
}
