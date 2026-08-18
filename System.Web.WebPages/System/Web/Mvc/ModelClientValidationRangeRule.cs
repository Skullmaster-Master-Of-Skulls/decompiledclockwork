using System;
using System.Runtime.CompilerServices;

namespace System.Web.Mvc
{
	// Token: 0x02000049 RID: 73
	[TypeForwardedFrom("System.Web.Mvc, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35")]
	public class ModelClientValidationRangeRule : ModelClientValidationRule
	{
		// Token: 0x060001E4 RID: 484 RVA: 0x00007BDA File Offset: 0x00005DDA
		public ModelClientValidationRangeRule(string errorMessage, object minValue, object maxValue)
		{
			base.ErrorMessage = errorMessage;
			base.ValidationType = "range";
			base.ValidationParameters["min"] = minValue;
			base.ValidationParameters["max"] = maxValue;
		}
	}
}
