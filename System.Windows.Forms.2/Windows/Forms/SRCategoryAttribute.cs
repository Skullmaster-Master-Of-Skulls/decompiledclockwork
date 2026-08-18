using System;
using System.ComponentModel;

namespace System.Windows.Forms
{
	// Token: 0x02000452 RID: 1106
	[AttributeUsage(AttributeTargets.All)]
	internal sealed class SRCategoryAttribute : CategoryAttribute
	{
		// Token: 0x06004D62 RID: 19810 RVA: 0x0013AFD4 File Offset: 0x001391D4
		public SRCategoryAttribute(string category) : base(category)
		{
		}

		// Token: 0x06004D63 RID: 19811 RVA: 0x0013FD34 File Offset: 0x0013DF34
		protected override string GetLocalizedString(string value)
		{
			return SR.GetString(value);
		}
	}
}
