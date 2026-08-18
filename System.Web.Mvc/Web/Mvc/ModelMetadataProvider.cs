using System;
using System.Collections.Generic;

namespace System.Web.Mvc
{
	// Token: 0x0200006B RID: 107
	public abstract class ModelMetadataProvider
	{
		// Token: 0x060002E1 RID: 737
		public abstract IEnumerable<ModelMetadata> GetMetadataForProperties(object container, Type containerType);

		// Token: 0x060002E2 RID: 738
		public abstract ModelMetadata GetMetadataForProperty(Func<object> modelAccessor, Type containerType, string propertyName);

		// Token: 0x060002E3 RID: 739
		public abstract ModelMetadata GetMetadataForType(Func<object> modelAccessor, Type modelType);
	}
}
