using System;
using System.ComponentModel;

namespace System.Web
{
	// Token: 0x02000114 RID: 276
	[AttributeUsage(AttributeTargets.All)]
	internal sealed class SRCategoryAttribute : CategoryAttribute
	{
		// Token: 0x0600113F RID: 4415 RVA: 0x0002E1A6 File Offset: 0x0002C3A6
		public SRCategoryAttribute(string category) : base(category)
		{
		}

		// Token: 0x06001140 RID: 4416 RVA: 0x000305BA File Offset: 0x0002E7BA
		protected override string GetLocalizedString(string value)
		{
			return SR.GetString(value);
		}
	}
}
