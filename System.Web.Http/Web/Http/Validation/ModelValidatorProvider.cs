using System;
using System.Collections.Generic;
using System.Web.Http.Metadata;

namespace System.Web.Http.Validation
{
	// Token: 0x0200018A RID: 394
	public abstract class ModelValidatorProvider
	{
		// Token: 0x06000A2A RID: 2602
		public abstract IEnumerable<ModelValidator> GetValidators(ModelMetadata metadata, IEnumerable<ModelValidatorProvider> validatorProviders);
	}
}
