using System;
using System.ComponentModel;

namespace System
{
	// Token: 0x02000038 RID: 56
	[AttributeUsage(AttributeTargets.All)]
	internal sealed class SRCategoryAttribute : CategoryAttribute
	{
		// Token: 0x0600015D RID: 349 RVA: 0x00003DEF File Offset: 0x00001FEF
		public SRCategoryAttribute(string category) : base(category)
		{
		}

		// Token: 0x0600015E RID: 350 RVA: 0x00003DF8 File Offset: 0x00001FF8
		protected override string GetLocalizedString(string value)
		{
			return SR.GetString(value);
		}
	}
}
