using System;
using System.Collections.Generic;
using System.Web.Http.Metadata;

namespace System.Web.Http.Validation.Validators
{
	// Token: 0x02000193 RID: 403
	public class ErrorModelValidator : ModelValidator
	{
		// Token: 0x06000A5B RID: 2651 RVA: 0x00022A55 File Offset: 0x00020C55
		public ErrorModelValidator(IEnumerable<ModelValidatorProvider> validatorProviders, string errorMessage) : base(validatorProviders)
		{
			if (errorMessage == null)
			{
				throw Error.ArgumentNull("errorMessage");
			}
			this._errorMessage = errorMessage;
		}

		// Token: 0x06000A5C RID: 2652 RVA: 0x00022A73 File Offset: 0x00020C73
		public override IEnumerable<ModelValidationResult> Validate(ModelMetadata metadata, object container)
		{
			throw Error.InvalidOperation(this._errorMessage, new object[0]);
		}

		// Token: 0x0400030E RID: 782
		private string _errorMessage;
	}
}
