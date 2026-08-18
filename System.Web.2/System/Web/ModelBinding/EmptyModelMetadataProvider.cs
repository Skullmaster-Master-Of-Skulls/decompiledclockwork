using System;
using System.Collections.Generic;

namespace System.Web.ModelBinding
{
	// Token: 0x0200064E RID: 1614
	public class EmptyModelMetadataProvider : AssociatedMetadataProvider
	{
		// Token: 0x06004F97 RID: 20375 RVA: 0x00114823 File Offset: 0x00112A23
		protected override ModelMetadata CreateMetadata(IEnumerable<Attribute> attributes, Type containerType, Func<object> modelAccessor, Type modelType, string propertyName)
		{
			return new ModelMetadata(this, containerType, modelAccessor, modelType, propertyName);
		}
	}
}
