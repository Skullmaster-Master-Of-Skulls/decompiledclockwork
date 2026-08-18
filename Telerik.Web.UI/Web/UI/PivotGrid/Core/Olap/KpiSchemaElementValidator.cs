using System;
using System.Collections.Generic;

namespace Telerik.Web.UI.PivotGrid.Core.Olap
{
	// Token: 0x02000D26 RID: 3366
	internal class KpiSchemaElementValidator : SchemaElementValidator
	{
		// Token: 0x06007D56 RID: 32086 RVA: 0x001CB838 File Offset: 0x001C9A38
		protected override IList<string> GetValidationErrors(SchemaElement element)
		{
			KpiSchemaElement element2 = element as KpiSchemaElement;
			IList<string> validationErrors = base.GetValidationErrors(element);
			if (KpiSchemaElementValidator.DoesNotHaveDefinedMembers(element2))
			{
				string item = "Should have at least one KPI member defined (Goal, Status, Treand, Value)";
				validationErrors.Add(item);
			}
			return validationErrors;
		}

		// Token: 0x06007D57 RID: 32087 RVA: 0x001CB86A File Offset: 0x001C9A6A
		private static bool DoesNotHaveDefinedMembers(KpiSchemaElement element)
		{
			return KpiSchemaElementValidator.UniqueNameForGoalIsInvalid(element) && KpiSchemaElementValidator.UniqueNameForStatusIsInvalid(element) && KpiSchemaElementValidator.UniqueNameForTrendIsInvalid(element) && KpiSchemaElementValidator.UniqueNameForValueIsInvalid(element);
		}

		// Token: 0x06007D58 RID: 32088 RVA: 0x001CB88C File Offset: 0x001C9A8C
		private static bool UniqueNameForGoalIsInvalid(KpiSchemaElement element)
		{
			return element == null || element.GoalMemberUniqueName == null || element.GoalMemberUniqueName.Trim().Length == 0;
		}

		// Token: 0x06007D59 RID: 32089 RVA: 0x001CB8AE File Offset: 0x001C9AAE
		private static bool UniqueNameForStatusIsInvalid(KpiSchemaElement element)
		{
			return element == null || element.StatusMemberUniqueName == null || element.StatusMemberUniqueName.Trim().Length == 0;
		}

		// Token: 0x06007D5A RID: 32090 RVA: 0x001CB8D0 File Offset: 0x001C9AD0
		private static bool UniqueNameForTrendIsInvalid(KpiSchemaElement element)
		{
			return element == null || element.TrendMemberUniqueName == null || element.TrendMemberUniqueName.Trim().Length == 0;
		}

		// Token: 0x06007D5B RID: 32091 RVA: 0x001CB8F2 File Offset: 0x001C9AF2
		private static bool UniqueNameForValueIsInvalid(KpiSchemaElement element)
		{
			return element == null || element.ValueMemberUniqueName == null || element.ValueMemberUniqueName.Trim().Length == 0;
		}
	}
}
