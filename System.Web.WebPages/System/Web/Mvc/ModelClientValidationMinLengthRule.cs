using System;

namespace System.Web.Mvc
{
	// Token: 0x02000047 RID: 71
	public class ModelClientValidationMinLengthRule : ModelClientValidationRule
	{
		// Token: 0x060001E2 RID: 482 RVA: 0x00007B37 File Offset: 0x00005D37
		public ModelClientValidationMinLengthRule(string errorMessage, int minimumLength)
		{
			base.ErrorMessage = errorMessage;
			base.ValidationType = "minlength";
			base.ValidationParameters["min"] = minimumLength;
		}
	}
}
