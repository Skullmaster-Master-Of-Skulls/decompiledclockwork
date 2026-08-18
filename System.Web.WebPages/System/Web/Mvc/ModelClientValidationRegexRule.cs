using System;
using System.Runtime.CompilerServices;

namespace System.Web.Mvc
{
	// Token: 0x0200004A RID: 74
	[TypeForwardedFrom("System.Web.Mvc, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35")]
	public class ModelClientValidationRegexRule : ModelClientValidationRule
	{
		// Token: 0x060001E5 RID: 485 RVA: 0x00007C16 File Offset: 0x00005E16
		public ModelClientValidationRegexRule(string errorMessage, string pattern)
		{
			base.ErrorMessage = errorMessage;
			base.ValidationType = "regex";
			base.ValidationParameters.Add("pattern", pattern);
		}
	}
}
