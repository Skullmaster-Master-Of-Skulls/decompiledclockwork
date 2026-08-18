using System;
using System.Collections.Generic;

namespace Telerik.Web.UI.PivotGrid.Core.Olap
{
	// Token: 0x02000D24 RID: 3364
	internal class HierarchySchemaElementValidator : UniqueSchemaElementValidator
	{
		// Token: 0x06007D44 RID: 32068 RVA: 0x001CB758 File Offset: 0x001C9958
		protected override IList<string> GetValidationErrors(SchemaElement element)
		{
			HierarchySchemaElement element2 = element as HierarchySchemaElement;
			IList<string> validationErrors = base.GetValidationErrors(element);
			if (HierarchySchemaElementValidator.DimensionUniqueNameIsInvalid(element2))
			{
				string errorForMissingProperty = SchemaElementValidator.GetErrorForMissingProperty("DimensionUniqueName");
				validationErrors.Add(errorForMissingProperty);
			}
			return validationErrors;
		}

		// Token: 0x06007D45 RID: 32069 RVA: 0x001CB78F File Offset: 0x001C998F
		private static bool DimensionUniqueNameIsInvalid(HierarchySchemaElement element)
		{
			return element == null || element.DimensionUniqueName == null || element.DimensionUniqueName.Trim().Length == 0;
		}
	}
}
