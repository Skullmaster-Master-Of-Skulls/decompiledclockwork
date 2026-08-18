using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace System.Web.Mvc
{
	// Token: 0x02000115 RID: 277
	public class RegularExpressionAttributeAdapter : DataAnnotationsModelValidator<RegularExpressionAttribute>
	{
		// Token: 0x0600075A RID: 1882 RVA: 0x00013CB3 File Offset: 0x00011EB3
		public RegularExpressionAttributeAdapter(ModelMetadata metadata, ControllerContext context, RegularExpressionAttribute attribute) : base(metadata, context, attribute)
		{
		}

		// Token: 0x0600075B RID: 1883 RVA: 0x00013CC0 File Offset: 0x00011EC0
		public override IEnumerable<ModelClientValidationRule> GetClientValidationRules()
		{
			return new ModelClientValidationRegexRule[]
			{
				new ModelClientValidationRegexRule(base.ErrorMessage, base.Attribute.Pattern)
			};
		}
	}
}
