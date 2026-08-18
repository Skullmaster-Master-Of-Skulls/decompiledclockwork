using System;
using System.ComponentModel;

namespace System.Linq.Expressions
{
	// Token: 0x02000274 RID: 628
	[AttributeUsage(AttributeTargets.All)]
	internal sealed class SRCategoryAttribute : CategoryAttribute
	{
		// Token: 0x0600166F RID: 5743 RVA: 0x0004A183 File Offset: 0x00048383
		public SRCategoryAttribute(string category) : base(category)
		{
		}

		// Token: 0x06001670 RID: 5744 RVA: 0x0004A18C File Offset: 0x0004838C
		protected override string GetLocalizedString(string value)
		{
			return SR.GetString(value);
		}
	}
}
