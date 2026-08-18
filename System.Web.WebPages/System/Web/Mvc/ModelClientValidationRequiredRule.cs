using System;
using System.Runtime.CompilerServices;

namespace System.Web.Mvc
{
	// Token: 0x0200004C RID: 76
	[TypeForwardedFrom("System.Web.Mvc, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35")]
	public class ModelClientValidationRequiredRule : ModelClientValidationRule
	{
		// Token: 0x060001E7 RID: 487 RVA: 0x00007CA5 File Offset: 0x00005EA5
		public ModelClientValidationRequiredRule(string errorMessage)
		{
			base.ErrorMessage = errorMessage;
			base.ValidationType = "required";
		}
	}
}
