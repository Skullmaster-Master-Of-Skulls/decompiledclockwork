using System;

namespace System.Web.Configuration
{
	// Token: 0x0200071E RID: 1822
	internal class NullRuntimeConfig : RuntimeConfig
	{
		// Token: 0x060057B1 RID: 22449 RVA: 0x0013345C File Offset: 0x0013165C
		internal NullRuntimeConfig() : base(null, true)
		{
		}

		// Token: 0x060057B2 RID: 22450 RVA: 0x0000298D File Offset: 0x00000B8D
		protected override object GetSectionObject(string sectionName)
		{
			return null;
		}
	}
}
