using System;
using System.Runtime.CompilerServices;

namespace System.Web.Mvc
{
	// Token: 0x0200004B RID: 75
	[TypeForwardedFrom("System.Web.Mvc, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35")]
	public class ModelClientValidationRemoteRule : ModelClientValidationRule
	{
		// Token: 0x060001E6 RID: 486 RVA: 0x00007C44 File Offset: 0x00005E44
		public ModelClientValidationRemoteRule(string errorMessage, string url, string httpMethod, string additionalFields)
		{
			base.ErrorMessage = errorMessage;
			base.ValidationType = "remote";
			base.ValidationParameters["url"] = url;
			if (!string.IsNullOrEmpty(httpMethod))
			{
				base.ValidationParameters["type"] = httpMethod;
			}
			base.ValidationParameters["additionalfields"] = additionalFields;
		}
	}
}
