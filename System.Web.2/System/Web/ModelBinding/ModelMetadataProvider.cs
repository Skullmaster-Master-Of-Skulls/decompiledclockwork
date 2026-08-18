using System;
using System.Collections.Generic;

namespace System.Web.ModelBinding
{
	// Token: 0x0200065B RID: 1627
	public abstract class ModelMetadataProvider
	{
		// Token: 0x06005000 RID: 20480
		public abstract IEnumerable<ModelMetadata> GetMetadataForProperties(object container, Type containerType);

		// Token: 0x06005001 RID: 20481
		public abstract ModelMetadata GetMetadataForProperty(Func<object> modelAccessor, Type containerType, string propertyName);

		// Token: 0x06005002 RID: 20482
		public abstract ModelMetadata GetMetadataForType(Func<object> modelAccessor, Type modelType);
	}
}
