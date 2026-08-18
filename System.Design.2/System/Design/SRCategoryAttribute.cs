using System;
using System.ComponentModel;

namespace System.Design
{
	// Token: 0x02000285 RID: 645
	[AttributeUsage(AttributeTargets.All)]
	internal sealed class SRCategoryAttribute : CategoryAttribute
	{
		// Token: 0x060018AD RID: 6317 RVA: 0x0008B11D File Offset: 0x0008931D
		public SRCategoryAttribute(string category) : base(category)
		{
		}

		// Token: 0x060018AE RID: 6318 RVA: 0x0008B126 File Offset: 0x00089326
		protected override string GetLocalizedString(string value)
		{
			return SR.GetString(value);
		}
	}
}
