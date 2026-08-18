using System;
using System.Collections.Generic;

namespace Telerik.Web.UI.PivotGrid.Core.Olap
{
	// Token: 0x02000D2C RID: 3372
	internal class NamedSetSchemaElementValidator : SchemaElementValidator
	{
		// Token: 0x06007D85 RID: 32133 RVA: 0x001CBB5C File Offset: 0x001C9D5C
		protected override IList<string> GetValidationErrors(SchemaElement element)
		{
			NamedSetSchemaElement element2 = element as NamedSetSchemaElement;
			IList<string> validationErrors = base.GetValidationErrors(element);
			if (NamedSetSchemaElementValidator.DimensionsIsInvalid(element2))
			{
				string errorForMissingProperty = SchemaElementValidator.GetErrorForMissingProperty("Dimensions");
				validationErrors.Add(errorForMissingProperty);
			}
			return validationErrors;
		}

		// Token: 0x06007D86 RID: 32134 RVA: 0x001CBB93 File Offset: 0x001C9D93
		private static bool DimensionsIsInvalid(NamedSetSchemaElement element)
		{
			return element == null || element.Dimensions == null || element.Dimensions.Trim().Length == 0;
		}
	}
}
