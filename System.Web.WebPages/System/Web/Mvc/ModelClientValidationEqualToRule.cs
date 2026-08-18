using System;
using System.Runtime.CompilerServices;

namespace System.Web.Mvc
{
	// Token: 0x02000046 RID: 70
	[TypeForwardedFrom("System.Web.Mvc, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35")]
	public class ModelClientValidationEqualToRule : ModelClientValidationRule
	{
		// Token: 0x060001E1 RID: 481 RVA: 0x00007B0C File Offset: 0x00005D0C
		public ModelClientValidationEqualToRule(string errorMessage, object other)
		{
			base.ErrorMessage = errorMessage;
			base.ValidationType = "equalto";
			base.ValidationParameters["other"] = other;
		}
	}
}
