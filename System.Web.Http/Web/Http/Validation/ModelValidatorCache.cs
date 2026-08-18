using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Metadata;

namespace System.Web.Http.Validation
{
	// Token: 0x020000AD RID: 173
	internal class ModelValidatorCache : IModelValidatorCache
	{
		// Token: 0x06000402 RID: 1026 RVA: 0x0000C99E File Offset: 0x0000AB9E
		public ModelValidatorCache(Lazy<IEnumerable<ModelValidatorProvider>> validatorProviders)
		{
			this._validatorProviders = validatorProviders;
		}

		// Token: 0x06000403 RID: 1027 RVA: 0x0000C9B8 File Offset: 0x0000ABB8
		public ModelValidator[] GetValidators(ModelMetadata metadata)
		{
			ModelValidator[] array;
			if (!this._validatorCache.TryGetValue(metadata.CacheKey, out array))
			{
				array = metadata.GetValidators(this._validatorProviders.Value).ToArray<ModelValidator>();
				this._validatorCache.TryAdd(metadata.CacheKey, array);
			}
			return array;
		}

		// Token: 0x0400012C RID: 300
		private ConcurrentDictionary<EfficientTypePropertyKey<Type, string>, ModelValidator[]> _validatorCache = new ConcurrentDictionary<EfficientTypePropertyKey<Type, string>, ModelValidator[]>();

		// Token: 0x0400012D RID: 301
		private Lazy<IEnumerable<ModelValidatorProvider>> _validatorProviders;
	}
}
