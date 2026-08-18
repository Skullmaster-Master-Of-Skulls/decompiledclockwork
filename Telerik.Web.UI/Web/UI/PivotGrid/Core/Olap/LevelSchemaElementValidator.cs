using System;
using System.Collections.Generic;

namespace Telerik.Web.UI.PivotGrid.Core.Olap
{
	// Token: 0x02000D28 RID: 3368
	internal class LevelSchemaElementValidator : UniqueSchemaElementValidator
	{
		// Token: 0x06007D62 RID: 32098 RVA: 0x001CB948 File Offset: 0x001C9B48
		protected override IList<string> GetValidationErrors(SchemaElement element)
		{
			LevelSchemaElement element2 = element as LevelSchemaElement;
			IList<string> validationErrors = base.GetValidationErrors(element);
			if (LevelSchemaElementValidator.DimensionUniqueNameIsInvalid(element2))
			{
				string errorForMissingProperty = SchemaElementValidator.GetErrorForMissingProperty("DimensionUniqueName");
				validationErrors.Add(errorForMissingProperty);
			}
			if (LevelSchemaElementValidator.HierarchyUniqueNameIsInvalid(element2))
			{
				string errorForMissingProperty2 = SchemaElementValidator.GetErrorForMissingProperty("HierarchyUniqueName");
				validationErrors.Add(errorForMissingProperty2);
			}
			return validationErrors;
		}

		// Token: 0x06007D63 RID: 32099 RVA: 0x001CB999 File Offset: 0x001C9B99
		private static bool DimensionUniqueNameIsInvalid(LevelSchemaElement element)
		{
			return element == null || element.DimensionUniqueName == null || element.DimensionUniqueName.Trim().Length == 0;
		}

		// Token: 0x06007D64 RID: 32100 RVA: 0x001CB9BB File Offset: 0x001C9BBB
		private static bool HierarchyUniqueNameIsInvalid(LevelSchemaElement element)
		{
			return element == null || element.HierarchyUniqueName == null || element.HierarchyUniqueName.Trim().Length == 0;
		}
	}
}
