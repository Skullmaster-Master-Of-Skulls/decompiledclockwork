using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace System.Web.Mvc
{
	// Token: 0x02000045 RID: 69
	public class MaxLengthAttributeAdapter : DataAnnotationsModelValidator<MaxLengthAttribute>
	{
		// Token: 0x06000157 RID: 343 RVA: 0x000065E4 File Offset: 0x000047E4
		public MaxLengthAttributeAdapter(ModelMetadata metadata, ControllerContext context, MaxLengthAttribute attribute) : base(metadata, context, attribute)
		{
		}

		// Token: 0x06000158 RID: 344 RVA: 0x000065F0 File Offset: 0x000047F0
		public override IEnumerable<ModelClientValidationRule> GetClientValidationRules()
		{
			return new ModelClientValidationMaxLengthRule[]
			{
				new ModelClientValidationMaxLengthRule(base.ErrorMessage, base.Attribute.Length)
			};
		}
	}
}
