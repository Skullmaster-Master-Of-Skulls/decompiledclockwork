using System;
using System.Collections.Generic;

namespace System.Web.Mvc
{
	// Token: 0x02000081 RID: 129
	public interface IClientValidatable
	{
		// Token: 0x060003DA RID: 986
		IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context);
	}
}
