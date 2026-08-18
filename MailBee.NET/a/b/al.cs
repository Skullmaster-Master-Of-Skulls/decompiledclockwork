using System;
using System.Collections.Specialized;

namespace a.b
{
	// Token: 0x0200033B RID: 827
	internal class al : bf
	{
		// Token: 0x06001E15 RID: 7701 RVA: 0x000817C4 File Offset: 0x000807C4
		public al(string A_0)
		{
			if (A_0 == null)
			{
				throw new ArgumentNullException("selectorName");
			}
			this.b = A_0;
		}

		// Token: 0x06001E16 RID: 7702 RVA: 0x000817EC File Offset: 0x000807EC
		public NameValueCollection bd()
		{
			return this.a;
		}

		// Token: 0x06001E17 RID: 7703 RVA: 0x000817F4 File Offset: 0x000807F4
		public string be()
		{
			return this.b;
		}

		// Token: 0x040013B8 RID: 5048
		private readonly NameValueCollection a = new NameValueCollection();

		// Token: 0x040013B9 RID: 5049
		private readonly string b;
	}
}
