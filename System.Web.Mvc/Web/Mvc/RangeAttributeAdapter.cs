using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace System.Web.Mvc
{
	// Token: 0x02000114 RID: 276
	public class RangeAttributeAdapter : DataAnnotationsModelValidator<RangeAttribute>
	{
		// Token: 0x06000758 RID: 1880 RVA: 0x00013C6A File Offset: 0x00011E6A
		public RangeAttributeAdapter(ModelMetadata metadata, ControllerContext context, RangeAttribute attribute) : base(metadata, context, attribute)
		{
		}

		// Token: 0x06000759 RID: 1881 RVA: 0x00013C78 File Offset: 0x00011E78
		public override IEnumerable<ModelClientValidationRule> GetClientValidationRules()
		{
			string errorMessage = base.ErrorMessage;
			return new ModelClientValidationRangeRule[]
			{
				new ModelClientValidationRangeRule(errorMessage, base.Attribute.Minimum, base.Attribute.Maximum)
			};
		}
	}
}
