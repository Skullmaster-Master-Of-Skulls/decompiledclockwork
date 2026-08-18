using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace System.Web.Mvc
{
	// Token: 0x02000116 RID: 278
	public class RequiredAttributeAdapter : DataAnnotationsModelValidator<RequiredAttribute>
	{
		// Token: 0x0600075C RID: 1884 RVA: 0x00013CEE File Offset: 0x00011EEE
		public RequiredAttributeAdapter(ModelMetadata metadata, ControllerContext context, RequiredAttribute attribute) : base(metadata, context, attribute)
		{
		}

		// Token: 0x0600075D RID: 1885 RVA: 0x00013CFC File Offset: 0x00011EFC
		public override IEnumerable<ModelClientValidationRule> GetClientValidationRules()
		{
			return new ModelClientValidationRequiredRule[]
			{
				new ModelClientValidationRequiredRule(base.ErrorMessage)
			};
		}
	}
}
