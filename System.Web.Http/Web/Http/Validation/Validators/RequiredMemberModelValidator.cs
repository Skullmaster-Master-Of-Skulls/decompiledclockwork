using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Metadata;

namespace System.Web.Http.Validation.Validators
{
	// Token: 0x02000194 RID: 404
	public class RequiredMemberModelValidator : ModelValidator
	{
		// Token: 0x06000A5D RID: 2653 RVA: 0x00022A86 File Offset: 0x00020C86
		public RequiredMemberModelValidator(IEnumerable<ModelValidatorProvider> validatorProviders) : base(validatorProviders)
		{
		}

		// Token: 0x170002E8 RID: 744
		// (get) Token: 0x06000A5E RID: 2654 RVA: 0x00022A8F File Offset: 0x00020C8F
		public override bool IsRequired
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000A5F RID: 2655 RVA: 0x00022A92 File Offset: 0x00020C92
		public override IEnumerable<ModelValidationResult> Validate(ModelMetadata metadata, object container)
		{
			return Enumerable.Empty<ModelValidationResult>();
		}
	}
}
