using System;
using System.ComponentModel;

namespace System.ServiceModel
{
	// Token: 0x0200017F RID: 383
	[AttributeUsage(AttributeTargets.All)]
	internal sealed class SRCategoryAttribute : CategoryAttribute
	{
		// Token: 0x06000B37 RID: 2871 RVA: 0x000291F2 File Offset: 0x000273F2
		public SRCategoryAttribute(string category) : base(category)
		{
		}

		// Token: 0x06000B38 RID: 2872 RVA: 0x000291FB File Offset: 0x000273FB
		protected override string GetLocalizedString(string value)
		{
			return SR.GetString(value);
		}
	}
}
