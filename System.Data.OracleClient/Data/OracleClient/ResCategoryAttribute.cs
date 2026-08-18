using System;
using System.ComponentModel;

namespace System.Data.OracleClient
{
	// Token: 0x02000003 RID: 3
	[AttributeUsage(AttributeTargets.All)]
	internal sealed class ResCategoryAttribute : CategoryAttribute
	{
		// Token: 0x06000003 RID: 3 RVA: 0x00054AB4 File Offset: 0x00053EB4
		public ResCategoryAttribute(string category) : base(category)
		{
		}

		// Token: 0x06000004 RID: 4 RVA: 0x00054AD4 File Offset: 0x00053ED4
		protected override string GetLocalizedString(string value)
		{
			return Res.GetString(value);
		}
	}
}
