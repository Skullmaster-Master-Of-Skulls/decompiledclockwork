using System;

namespace TechnoPro.Common.Public.Entities.Vets
{
	// Token: 0x020000FD RID: 253
	public class VetsBenefitApplicationStepAttribute : Attribute
	{
		// Token: 0x060005D4 RID: 1492 RVA: 0x0000EC26 File Offset: 0x0000CE26
		public VetsBenefitApplicationStepAttribute()
		{
		}

		// Token: 0x060005D5 RID: 1493 RVA: 0x0000ED08 File Offset: 0x0000CF08
		public VetsBenefitApplicationStepAttribute(string title)
		{
			this.Title = title;
		}

		// Token: 0x17000208 RID: 520
		// (get) Token: 0x060005D6 RID: 1494 RVA: 0x0000ED1A File Offset: 0x0000CF1A
		// (set) Token: 0x060005D7 RID: 1495 RVA: 0x0000ED22 File Offset: 0x0000CF22
		public string Title { get; set; }
	}
}
