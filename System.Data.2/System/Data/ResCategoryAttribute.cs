using System;
using System.ComponentModel;

namespace System.Data
{
	// Token: 0x02000149 RID: 329
	[AttributeUsage(AttributeTargets.All)]
	internal sealed class ResCategoryAttribute : CategoryAttribute
	{
		// Token: 0x06001354 RID: 4948 RVA: 0x00099FA0 File Offset: 0x000993A0
		public ResCategoryAttribute(string category) : base(category)
		{
		}

		// Token: 0x06001355 RID: 4949 RVA: 0x00099FB4 File Offset: 0x000993B4
		protected override string GetLocalizedString(string value)
		{
			return Res.GetString(value);
		}
	}
}
