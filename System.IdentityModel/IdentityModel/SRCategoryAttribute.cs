using System;
using System.ComponentModel;

namespace System.IdentityModel
{
	// Token: 0x020000E9 RID: 233
	[AttributeUsage(AttributeTargets.All)]
	internal sealed class SRCategoryAttribute : CategoryAttribute
	{
		// Token: 0x0600065A RID: 1626 RVA: 0x0001A18D File Offset: 0x0001838D
		public SRCategoryAttribute(string category) : base(category)
		{
		}

		// Token: 0x0600065B RID: 1627 RVA: 0x0001A196 File Offset: 0x00018396
		protected override string GetLocalizedString(string value)
		{
			return SR.GetString(value);
		}
	}
}
