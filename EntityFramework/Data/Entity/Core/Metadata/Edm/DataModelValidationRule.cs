using System;

namespace System.Data.Entity.Core.Metadata.Edm
{
	// Token: 0x02000015 RID: 21
	internal abstract class DataModelValidationRule
	{
		// Token: 0x17000011 RID: 17
		// (get) Token: 0x060000B7 RID: 183
		internal abstract Type ValidatedType { get; }

		// Token: 0x060000B8 RID: 184
		internal abstract void Evaluate(EdmModelValidationContext context, MetadataItem item);
	}
}
