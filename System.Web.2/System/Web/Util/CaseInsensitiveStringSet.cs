using System;

namespace System.Web.Util
{
	// Token: 0x0200020E RID: 526
	internal class CaseInsensitiveStringSet : StringSet
	{
		// Token: 0x17000766 RID: 1894
		// (get) Token: 0x060019AF RID: 6575 RVA: 0x000097B7 File Offset: 0x000079B7
		protected override bool CaseInsensitive
		{
			get
			{
				return true;
			}
		}
	}
}
