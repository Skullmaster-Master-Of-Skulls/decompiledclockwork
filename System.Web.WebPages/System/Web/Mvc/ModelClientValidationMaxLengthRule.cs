using System;

namespace System.Web.Mvc
{
	// Token: 0x0200004D RID: 77
	public class ModelClientValidationMaxLengthRule : ModelClientValidationRule
	{
		// Token: 0x060001E8 RID: 488 RVA: 0x00007CBF File Offset: 0x00005EBF
		public ModelClientValidationMaxLengthRule(string errorMessage, int maximumLength)
		{
			base.ErrorMessage = errorMessage;
			base.ValidationType = "maxlength";
			base.ValidationParameters["max"] = maximumLength;
		}
	}
}
