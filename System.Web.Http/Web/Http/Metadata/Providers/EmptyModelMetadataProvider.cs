using System;
using System.Collections.Generic;

namespace System.Web.Http.Metadata.Providers
{
	// Token: 0x02000139 RID: 313
	public class EmptyModelMetadataProvider : AssociatedMetadataProvider<ModelMetadata>
	{
		// Token: 0x060007D4 RID: 2004 RVA: 0x0001A0FF File Offset: 0x000182FF
		protected override ModelMetadata CreateMetadataPrototype(IEnumerable<Attribute> attributes, Type containerType, Type modelType, string propertyName)
		{
			return new ModelMetadata(this, containerType, null, modelType, propertyName);
		}

		// Token: 0x060007D5 RID: 2005 RVA: 0x0001A10C File Offset: 0x0001830C
		protected override ModelMetadata CreateMetadataFromPrototype(ModelMetadata prototype, Func<object> modelAccessor)
		{
			return new ModelMetadata(this, prototype.ContainerType, modelAccessor, prototype.ModelType, prototype.PropertyName);
		}
	}
}
