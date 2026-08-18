using System;
using System.Collections.Generic;

namespace System.Web.ModelBinding
{
	// Token: 0x02000661 RID: 1633
	public abstract class ModelValidatorProvider
	{
		// Token: 0x06005032 RID: 20530
		public abstract IEnumerable<ModelValidator> GetValidators(ModelMetadata metadata, ModelBindingExecutionContext context);
	}
}
