using System;
using System.Collections.Generic;

namespace Telerik.Web.UI.PivotGrid.Core.Olap
{
	// Token: 0x02000D23 RID: 3363
	internal class UniqueSchemaElementValidator : SchemaElementValidator
	{
		// Token: 0x06007D41 RID: 32065 RVA: 0x001CB6F4 File Offset: 0x001C98F4
		protected override IList<string> GetValidationErrors(SchemaElement element)
		{
			UniqueSchemaElement element2 = element as UniqueSchemaElement;
			IList<string> validationErrors = base.GetValidationErrors(element);
			if (UniqueSchemaElementValidator.UniqueNameIsInvalid(element2))
			{
				string errorForMissingProperty = SchemaElementValidator.GetErrorForMissingProperty("UniqueName");
				validationErrors.Add(errorForMissingProperty);
			}
			return validationErrors;
		}

		// Token: 0x06007D42 RID: 32066 RVA: 0x001CB72B File Offset: 0x001C992B
		private static bool UniqueNameIsInvalid(UniqueSchemaElement element)
		{
			return element == null || element.UniqueName == null || element.UniqueName.Trim().Length == 0;
		}
	}
}
