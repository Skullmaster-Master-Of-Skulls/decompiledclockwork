using System;
using System.ComponentModel;

namespace System.Transactions
{
	// Token: 0x02000003 RID: 3
	[AttributeUsage(AttributeTargets.All)]
	internal sealed class SRCategoryAttribute : CategoryAttribute
	{
		// Token: 0x06000003 RID: 3 RVA: 0x00029244 File Offset: 0x00028644
		public SRCategoryAttribute(string category) : base(category)
		{
		}

		// Token: 0x06000004 RID: 4 RVA: 0x00029264 File Offset: 0x00028664
		protected override string GetLocalizedString(string value)
		{
			return SR.GetString(value);
		}
	}
}
