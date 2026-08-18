using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Web.Mvc;
using System.Web.WebPages.Resources;
using Microsoft.Internal.Web.Utils;

namespace System.Web.WebPages
{
	// Token: 0x02000060 RID: 96
	public abstract class Validator
	{
		// Token: 0x06000258 RID: 600 RVA: 0x00009740 File Offset: 0x00007940
		public static IValidator Required(string errorMessage = null)
		{
			errorMessage = Validator.DefaultIfEmpty(errorMessage, WebPageResources.ValidationDefault_Required);
			ModelClientValidationRequiredRule clientValidationRule = new ModelClientValidationRequiredRule(errorMessage);
			return new ValidationAttributeAdapter(new RequiredAttribute(), errorMessage, clientValidationRule, true);
		}

		// Token: 0x06000259 RID: 601 RVA: 0x00009770 File Offset: 0x00007970
		public static IValidator Range(int minValue, int maxValue, string errorMessage = null)
		{
			errorMessage = string.Format(CultureInfo.CurrentCulture, Validator.DefaultIfEmpty(errorMessage, WebPageResources.ValidationDefault_IntegerRange), new object[]
			{
				minValue,
				maxValue
			});
			ModelClientValidationRangeRule clientValidationRule = new ModelClientValidationRangeRule(errorMessage, minValue, maxValue);
			return new ValidationAttributeAdapter(new RangeAttribute(minValue, maxValue), errorMessage, clientValidationRule);
		}

		// Token: 0x0600025A RID: 602 RVA: 0x000097D0 File Offset: 0x000079D0
		public static IValidator Range(double minValue, double maxValue, string errorMessage = null)
		{
			errorMessage = string.Format(CultureInfo.CurrentCulture, Validator.DefaultIfEmpty(errorMessage, WebPageResources.ValidationDefault_FloatRange), new object[]
			{
				minValue,
				maxValue
			});
			ModelClientValidationRangeRule clientValidationRule = new ModelClientValidationRangeRule(errorMessage, minValue, maxValue);
			return new ValidationAttributeAdapter(new RangeAttribute(minValue, maxValue), errorMessage, clientValidationRule);
		}

		// Token: 0x0600025B RID: 603 RVA: 0x00009830 File Offset: 0x00007A30
		public static IValidator StringLength(int maxLength, int minLength = 0, string errorMessage = null)
		{
			if (minLength == 0)
			{
				errorMessage = string.Format(CultureInfo.CurrentCulture, Validator.DefaultIfEmpty(errorMessage, WebPageResources.ValidationDefault_StringLength), new object[]
				{
					maxLength
				});
			}
			else
			{
				errorMessage = Validator.DefaultIfEmpty(errorMessage, WebPageResources.ValidationDefault_StringLengthRange);
				errorMessage = string.Format(CultureInfo.CurrentCulture, errorMessage, new object[]
				{
					minLength,
					maxLength
				});
			}
			ModelClientValidationStringLengthRule clientValidationRule = new ModelClientValidationStringLengthRule(errorMessage, minLength, maxLength);
			return new ValidationAttributeAdapter(new StringLengthAttribute(maxLength)
			{
				MinimumLength = minLength
			}, errorMessage, clientValidationRule, true);
		}

		// Token: 0x0600025C RID: 604 RVA: 0x000098C0 File Offset: 0x00007AC0
		public static IValidator Regex(string pattern, string errorMessage = null)
		{
			if (string.IsNullOrEmpty(pattern))
			{
				throw new ArgumentException(CommonResources.Argument_Cannot_Be_Null_Or_Empty, "pattern");
			}
			errorMessage = Validator.DefaultIfEmpty(errorMessage, WebPageResources.ValidationDefault_Regex);
			ModelClientValidationRegexRule clientValidationRule = new ModelClientValidationRegexRule(errorMessage, pattern);
			return new ValidationAttributeAdapter(new RegularExpressionAttribute(pattern), errorMessage, clientValidationRule);
		}

		// Token: 0x0600025D RID: 605 RVA: 0x00009907 File Offset: 0x00007B07
		public static IValidator EqualsTo(string otherFieldName, string errorMessage = null)
		{
			if (string.IsNullOrEmpty(otherFieldName))
			{
				throw new ArgumentException(CommonResources.Argument_Cannot_Be_Null_Or_Empty, "otherFieldName");
			}
			errorMessage = Validator.DefaultIfEmpty(errorMessage, WebPageResources.ValidationDefault_EqualsTo);
			return new CompareValidator(otherFieldName, errorMessage);
		}

		// Token: 0x0600025E RID: 606 RVA: 0x00009935 File Offset: 0x00007B35
		public static IValidator DateTime(string errorMessage = null)
		{
			errorMessage = Validator.DefaultIfEmpty(errorMessage, WebPageResources.ValidationDefault_DataType);
			return new DataTypeValidator(DataTypeValidator.SupportedValidationDataType.DateTime, errorMessage);
		}

		// Token: 0x0600025F RID: 607 RVA: 0x0000994B File Offset: 0x00007B4B
		public static IValidator Decimal(string errorMessage = null)
		{
			errorMessage = Validator.DefaultIfEmpty(errorMessage, WebPageResources.ValidationDefault_DataType);
			return new DataTypeValidator(DataTypeValidator.SupportedValidationDataType.Decimal, errorMessage);
		}

		// Token: 0x06000260 RID: 608 RVA: 0x00009961 File Offset: 0x00007B61
		public static IValidator Integer(string errorMessage = null)
		{
			errorMessage = Validator.DefaultIfEmpty(errorMessage, WebPageResources.ValidationDefault_DataType);
			return new DataTypeValidator(DataTypeValidator.SupportedValidationDataType.Integer, errorMessage);
		}

		// Token: 0x06000261 RID: 609 RVA: 0x00009977 File Offset: 0x00007B77
		public static IValidator Url(string errorMessage = null)
		{
			errorMessage = Validator.DefaultIfEmpty(errorMessage, WebPageResources.ValidationDefault_DataType);
			return new DataTypeValidator(DataTypeValidator.SupportedValidationDataType.Url, errorMessage);
		}

		// Token: 0x06000262 RID: 610 RVA: 0x0000998D File Offset: 0x00007B8D
		public static IValidator Float(string errorMessage = null)
		{
			errorMessage = Validator.DefaultIfEmpty(errorMessage, WebPageResources.ValidationDefault_DataType);
			return new DataTypeValidator(DataTypeValidator.SupportedValidationDataType.Float, errorMessage);
		}

		// Token: 0x06000263 RID: 611 RVA: 0x000099A3 File Offset: 0x00007BA3
		private static string DefaultIfEmpty(string errorMessage, string defaultErrorMessage)
		{
			if (string.IsNullOrEmpty(errorMessage))
			{
				return defaultErrorMessage;
			}
			return errorMessage;
		}
	}
}
