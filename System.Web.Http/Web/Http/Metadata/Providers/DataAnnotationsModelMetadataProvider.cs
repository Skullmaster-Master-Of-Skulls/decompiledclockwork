using System;
using System.Collections.Generic;

namespace System.Web.Http.Metadata.Providers
{
	// Token: 0x02000138 RID: 312
	public class DataAnnotationsModelMetadataProvider : AssociatedMetadataProvider<CachedDataAnnotationsModelMetadata>
	{
		// Token: 0x060007D1 RID: 2001 RVA: 0x0001A0E1 File Offset: 0x000182E1
		protected override CachedDataAnnotationsModelMetadata CreateMetadataPrototype(IEnumerable<Attribute> attributes, Type containerType, Type modelType, string propertyName)
		{
			return new CachedDataAnnotationsModelMetadata(this, containerType, modelType, propertyName, attributes);
		}

		// Token: 0x060007D2 RID: 2002 RVA: 0x0001A0EE File Offset: 0x000182EE
		protected override CachedDataAnnotationsModelMetadata CreateMetadataFromPrototype(CachedDataAnnotationsModelMetadata prototype, Func<object> modelAccessor)
		{
			return new CachedDataAnnotationsModelMetadata(prototype, modelAccessor);
		}
	}
}
