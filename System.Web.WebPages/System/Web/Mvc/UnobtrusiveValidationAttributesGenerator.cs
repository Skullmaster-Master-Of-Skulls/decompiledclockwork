using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.WebPages.Resources;

namespace System.Web.Mvc
{
	// Token: 0x0200004F RID: 79
	public static class UnobtrusiveValidationAttributesGenerator
	{
		// Token: 0x060001EA RID: 490 RVA: 0x00007D4C File Offset: 0x00005F4C
		public static void GetValidationAttributes(IEnumerable<ModelClientValidationRule> clientRules, IDictionary<string, object> results)
		{
			if (clientRules == null)
			{
				throw new ArgumentNullException("clientRules");
			}
			if (results == null)
			{
				throw new ArgumentNullException("results");
			}
			bool flag = false;
			foreach (ModelClientValidationRule modelClientValidationRule in clientRules)
			{
				flag = true;
				string text = "data-val-" + modelClientValidationRule.ValidationType;
				UnobtrusiveValidationAttributesGenerator.ValidateUnobtrusiveValidationRule(modelClientValidationRule, results, text);
				results.Add(text, modelClientValidationRule.ErrorMessage ?? string.Empty);
				text += "-";
				foreach (KeyValuePair<string, object> keyValuePair in modelClientValidationRule.ValidationParameters)
				{
					results.Add(text + keyValuePair.Key, keyValuePair.Value ?? string.Empty);
				}
			}
			if (flag)
			{
				results.Add("data-val", "true");
			}
		}

		// Token: 0x060001EB RID: 491 RVA: 0x00007E84 File Offset: 0x00006084
		private static void ValidateUnobtrusiveValidationRule(ModelClientValidationRule rule, IDictionary<string, object> resultsDictionary, string dictionaryKey)
		{
			if (string.IsNullOrWhiteSpace(rule.ValidationType))
			{
				throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, WebPageResources.UnobtrusiveJavascript_ValidationTypeCannotBeEmpty, new object[]
				{
					rule.GetType().FullName
				}));
			}
			if (resultsDictionary.ContainsKey(dictionaryKey))
			{
				throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, WebPageResources.UnobtrusiveJavascript_ValidationTypeMustBeUnique, new object[]
				{
					rule.ValidationType
				}));
			}
			if (rule.ValidationType.Any((char c) => !char.IsLower(c)))
			{
				throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, WebPageResources.UnobtrusiveJavascript_ValidationTypeMustBeLegal, new object[]
				{
					rule.ValidationType,
					rule.GetType().FullName
				}));
			}
			foreach (string text in rule.ValidationParameters.Keys)
			{
				if (string.IsNullOrWhiteSpace(text))
				{
					throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, WebPageResources.UnobtrusiveJavascript_ValidationParameterCannotBeEmpty, new object[]
					{
						rule.GetType().FullName
					}));
				}
				if (char.IsLower(text.First<char>()))
				{
					if (!text.Any((char c) => !char.IsLower(c) && !char.IsDigit(c)))
					{
						continue;
					}
				}
				throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, WebPageResources.UnobtrusiveJavascript_ValidationParameterMustBeLegal, new object[]
				{
					text,
					rule.GetType().FullName
				}));
			}
		}
	}
}
