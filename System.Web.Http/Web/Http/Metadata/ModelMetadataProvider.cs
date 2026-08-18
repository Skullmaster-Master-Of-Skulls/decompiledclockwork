using System;
using System.Collections.Generic;

namespace System.Web.Http.Metadata
{
	// Token: 0x02000130 RID: 304
	public abstract class ModelMetadataProvider
	{
		// Token: 0x0600077C RID: 1916
		public abstract IEnumerable<ModelMetadata> GetMetadataForProperties(object container, Type containerType);

		// Token: 0x0600077D RID: 1917
		public abstract ModelMetadata GetMetadataForProperty(Func<object> modelAccessor, Type containerType, string propertyName);

		// Token: 0x0600077E RID: 1918
		public abstract ModelMetadata GetMetadataForType(Func<object> modelAccessor, Type modelType);
	}
}
