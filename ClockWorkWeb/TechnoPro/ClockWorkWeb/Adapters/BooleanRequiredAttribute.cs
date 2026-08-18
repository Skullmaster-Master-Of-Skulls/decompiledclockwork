using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace TechnoPro.ClockWorkWeb.Adapters
{
	// Token: 0x02000193 RID: 403
	public class BooleanRequiredAttribute : ValidationAttribute, IClientValidatable
	{
		// Token: 0x06000BD5 RID: 3029 RVA: 0x0004D278 File Offset: 0x0004B478
		public override bool IsValid(object value)
		{
			bool flag = value is bool;
			return !flag || (bool)value;
		}

		// Token: 0x06000BD6 RID: 3030 RVA: 0x0004D2A1 File Offset: 0x0004B4A1
		public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
		{
			yield return new ModelClientValidationRule
			{
				ErrorMessage = this.FormatErrorMessage(metadata.GetDisplayName()),
				ValidationType = "booleanrequired"
			};
			yield break;
		}
	}
}
