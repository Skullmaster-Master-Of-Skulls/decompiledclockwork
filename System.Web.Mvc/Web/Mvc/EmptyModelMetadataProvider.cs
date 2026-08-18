using System;
using System.Collections.Generic;

namespace System.Web.Mvc
{
	// Token: 0x0200014F RID: 335
	public class EmptyModelMetadataProvider : AssociatedMetadataProvider
	{
		// Token: 0x06000897 RID: 2199 RVA: 0x00017DC7 File Offset: 0x00015FC7
		protected override ModelMetadata CreateMetadata(IEnumerable<Attribute> attributes, Type containerType, Func<object> modelAccessor, Type modelType, string propertyName)
		{
			return new ModelMetadata(this, containerType, modelAccessor, modelType, propertyName);
		}
	}
}
