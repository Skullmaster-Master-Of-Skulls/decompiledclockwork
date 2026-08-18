using System;

namespace System.Data.Entity.Core.Metadata.Edm
{
	// Token: 0x02000023 RID: 35
	internal class EdmModelValidationRule<TItem> : DataModelValidationRule<TItem> where TItem : class
	{
		// Token: 0x0600013C RID: 316 RVA: 0x00007AAE File Offset: 0x00005CAE
		internal EdmModelValidationRule(Action<EdmModelValidationContext, TItem> validate) : base(validate)
		{
		}
	}
}
