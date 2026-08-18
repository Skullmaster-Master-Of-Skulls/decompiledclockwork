using System;
using System.Web.Http.Metadata;

namespace System.Web.Http.Validation
{
	// Token: 0x020000AC RID: 172
	internal interface IModelValidatorCache
	{
		// Token: 0x06000401 RID: 1025
		ModelValidator[] GetValidators(ModelMetadata metadata);
	}
}
