using System;

namespace Org.BouncyCastle.Cms
{
	// Token: 0x02000096 RID: 150
	public class CmsException : Exception
	{
		// Token: 0x060004D3 RID: 1235 RVA: 0x0001A7DA File Offset: 0x000197DA
		public CmsException()
		{
		}

		// Token: 0x060004D4 RID: 1236 RVA: 0x0001A7E2 File Offset: 0x000197E2
		public CmsException(string name) : base(name)
		{
		}

		// Token: 0x060004D5 RID: 1237 RVA: 0x0001A7EB File Offset: 0x000197EB
		public CmsException(string name, Exception e) : base(name, e)
		{
		}
	}
}
