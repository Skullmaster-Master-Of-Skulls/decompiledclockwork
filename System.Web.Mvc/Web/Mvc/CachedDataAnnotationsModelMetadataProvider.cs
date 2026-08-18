using System;
using System.Collections.Generic;

namespace System.Web.Mvc
{
	// Token: 0x02000072 RID: 114
	public class CachedDataAnnotationsModelMetadataProvider : CachedAssociatedMetadataProvider<CachedDataAnnotationsModelMetadata>
	{
		// Token: 0x060003B1 RID: 945 RVA: 0x0000AFD2 File Offset: 0x000091D2
		protected override CachedDataAnnotationsModelMetadata CreateMetadataPrototype(IEnumerable<Attribute> attributes, Type containerType, Type modelType, string propertyName)
		{
			return new CachedDataAnnotationsModelMetadata(this, containerType, modelType, propertyName, attributes);
		}

		// Token: 0x060003B2 RID: 946 RVA: 0x0000AFDF File Offset: 0x000091DF
		protected override CachedDataAnnotationsModelMetadata CreateMetadataFromPrototype(CachedDataAnnotationsModelMetadata prototype, Func<object> modelAccessor)
		{
			return new CachedDataAnnotationsModelMetadata(prototype, modelAccessor);
		}
	}
}
