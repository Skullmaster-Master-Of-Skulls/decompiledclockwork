using System;
using System.ComponentModel;

namespace System.Linq
{
	// Token: 0x02000173 RID: 371
	[AttributeUsage(AttributeTargets.All)]
	internal sealed class SRCategoryAttribute : CategoryAttribute
	{
		// Token: 0x06000DB3 RID: 3507 RVA: 0x00030B26 File Offset: 0x0002ED26
		public SRCategoryAttribute(string category) : base(category)
		{
		}

		// Token: 0x06000DB4 RID: 3508 RVA: 0x00030B2F File Offset: 0x0002ED2F
		protected override string GetLocalizedString(string value)
		{
			return SR.GetString(value);
		}
	}
}
