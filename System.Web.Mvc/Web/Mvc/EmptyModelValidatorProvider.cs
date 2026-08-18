using System;
using System.Collections.Generic;
using System.Linq;

namespace System.Web.Mvc
{
	// Token: 0x02000145 RID: 325
	public class EmptyModelValidatorProvider : ModelValidatorProvider
	{
		// Token: 0x0600086B RID: 2155 RVA: 0x0001747D File Offset: 0x0001567D
		public override IEnumerable<ModelValidator> GetValidators(ModelMetadata metadata, ControllerContext context)
		{
			return Enumerable.Empty<ModelValidator>();
		}
	}
}
