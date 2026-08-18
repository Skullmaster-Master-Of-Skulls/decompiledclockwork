using System;
using System.Collections.Generic;

namespace System.Web.Mvc
{
	// Token: 0x020000E1 RID: 225
	public abstract class ModelValidatorProvider
	{
		// Token: 0x060005D1 RID: 1489
		public abstract IEnumerable<ModelValidator> GetValidators(ModelMetadata metadata, ControllerContext context);
	}
}
