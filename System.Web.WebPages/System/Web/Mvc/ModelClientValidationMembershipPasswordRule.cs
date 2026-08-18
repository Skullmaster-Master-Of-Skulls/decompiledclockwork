using System;

namespace System.Web.Mvc
{
	// Token: 0x02000048 RID: 72
	internal class ModelClientValidationMembershipPasswordRule : ModelClientValidationRule
	{
		// Token: 0x060001E3 RID: 483 RVA: 0x00007B68 File Offset: 0x00005D68
		public ModelClientValidationMembershipPasswordRule(string errorMessage, int minRequiredPasswordLength, int minRequiredNonAlphanumericCharacters, string passwordStrengthRegularExpression)
		{
			base.ErrorMessage = errorMessage;
			base.ValidationType = "password";
			if (minRequiredPasswordLength != 0)
			{
				base.ValidationParameters["min"] = minRequiredPasswordLength;
			}
			if (minRequiredNonAlphanumericCharacters != 0)
			{
				base.ValidationParameters["nonalphamin"] = minRequiredNonAlphanumericCharacters;
			}
			if (!string.IsNullOrEmpty(passwordStrengthRegularExpression))
			{
				base.ValidationParameters["regex"] = passwordStrengthRegularExpression;
			}
		}
	}
}
