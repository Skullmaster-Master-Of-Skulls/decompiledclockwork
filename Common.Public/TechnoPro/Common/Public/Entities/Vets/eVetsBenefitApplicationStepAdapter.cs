using System;
using TechnoPro.Common.Public.Adapters;

namespace TechnoPro.Common.Public.Entities.Vets
{
	// Token: 0x020000FE RID: 254
	public static class eVetsBenefitApplicationStepAdapter
	{
		// Token: 0x060005D8 RID: 1496 RVA: 0x0000ED2C File Offset: 0x0000CF2C
		public static eVetsBenefitApplicationStep? NextStep(this eVetsBenefitApplicationStep step)
		{
			int num = (int)(step + 1);
			return Enum.IsDefined(typeof(eVetsBenefitApplicationStep), num) ? ((eVetsBenefitApplicationStep?)Enum.Parse(typeof(eVetsBenefitApplicationStep), num.ToString())) : null;
		}

		// Token: 0x060005D9 RID: 1497 RVA: 0x0000ED80 File Offset: 0x0000CF80
		public static bool IsLastStep(this eVetsBenefitApplicationStep step)
		{
			int num = (int)(step + 1);
			return !Enum.IsDefined(typeof(eVetsBenefitApplicationStep), num);
		}

		// Token: 0x060005DA RID: 1498 RVA: 0x0000EDB0 File Offset: 0x0000CFB0
		public static string GetTitle(this eVetsBenefitApplicationStep step)
		{
			return step.GetAttribute<VetsBenefitApplicationStepAttribute>().Title;
		}
	}
}
