using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace System.Web.Mvc
{
	// Token: 0x02000119 RID: 281
	public class StringLengthAttributeAdapter : DataAnnotationsModelValidator<StringLengthAttribute>
	{
		// Token: 0x06000761 RID: 1889 RVA: 0x00013D55 File Offset: 0x00011F55
		public StringLengthAttributeAdapter(ModelMetadata metadata, ControllerContext context, StringLengthAttribute attribute) : base(metadata, context, attribute)
		{
		}

		// Token: 0x06000762 RID: 1890 RVA: 0x00013D60 File Offset: 0x00011F60
		public override IEnumerable<ModelClientValidationRule> GetClientValidationRules()
		{
			return new ModelClientValidationStringLengthRule[]
			{
				new ModelClientValidationStringLengthRule(base.ErrorMessage, base.Attribute.MinimumLength, base.Attribute.MaximumLength)
			};
		}
	}
}
