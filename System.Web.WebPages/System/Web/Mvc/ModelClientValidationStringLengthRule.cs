using System;
using System.Runtime.CompilerServices;

namespace System.Web.Mvc
{
	// Token: 0x0200004E RID: 78
	[TypeForwardedFrom("System.Web.Mvc, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35")]
	public class ModelClientValidationStringLengthRule : ModelClientValidationRule
	{
		// Token: 0x060001E9 RID: 489 RVA: 0x00007CF0 File Offset: 0x00005EF0
		public ModelClientValidationStringLengthRule(string errorMessage, int minimumLength, int maximumLength)
		{
			base.ErrorMessage = errorMessage;
			base.ValidationType = "length";
			if (minimumLength != 0)
			{
				base.ValidationParameters["min"] = minimumLength;
			}
			if (maximumLength != 2147483647)
			{
				base.ValidationParameters["max"] = maximumLength;
			}
		}
	}
}
