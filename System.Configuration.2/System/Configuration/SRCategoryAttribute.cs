using System;
using System.ComponentModel;

namespace System.Configuration
{
	// Token: 0x020000A7 RID: 167
	[AttributeUsage(AttributeTargets.All)]
	internal sealed class SRCategoryAttribute : CategoryAttribute
	{
		// Token: 0x0600069E RID: 1694 RVA: 0x0001F45F File Offset: 0x0001D65F
		public SRCategoryAttribute(string category) : base(category)
		{
		}

		// Token: 0x0600069F RID: 1695 RVA: 0x0001F468 File Offset: 0x0001D668
		protected override string GetLocalizedString(string value)
		{
			return SR.GetString(value);
		}
	}
}
