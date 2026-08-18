using System;
using System.ComponentModel;

namespace System.Data
{
	// Token: 0x02000027 RID: 39
	[AttributeUsage(AttributeTargets.All)]
	internal sealed class ResCategoryAttribute : CategoryAttribute
	{
		// Token: 0x06000088 RID: 136 RVA: 0x001D9AD8 File Offset: 0x001D8ED8
		public ResCategoryAttribute(string category) : base(category)
		{
		}

		// Token: 0x06000089 RID: 137 RVA: 0x001D9AF8 File Offset: 0x001D8EF8
		protected override string GetLocalizedString(string value)
		{
			return Res.GetString(value);
		}
	}
}
