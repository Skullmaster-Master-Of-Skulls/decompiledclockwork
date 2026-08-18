using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace System.Web.Mvc
{
	// Token: 0x02000046 RID: 70
	public class MinLengthAttributeAdapter : DataAnnotationsModelValidator<MinLengthAttribute>
	{
		// Token: 0x06000159 RID: 345 RVA: 0x0000661E File Offset: 0x0000481E
		public MinLengthAttributeAdapter(ModelMetadata metadata, ControllerContext context, MinLengthAttribute attribute) : base(metadata, context, attribute)
		{
		}

		// Token: 0x0600015A RID: 346 RVA: 0x0000662C File Offset: 0x0000482C
		public override IEnumerable<ModelClientValidationRule> GetClientValidationRules()
		{
			return new ModelClientValidationMinLengthRule[]
			{
				new ModelClientValidationMinLengthRule(base.ErrorMessage, base.Attribute.Length)
			};
		}
	}
}
