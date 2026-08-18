using System;
using System.IO;

namespace Org.BouncyCastle.Cms
{
	// Token: 0x020003DE RID: 990
	public class CmsStreamException : IOException
	{
		// Token: 0x06002277 RID: 8823 RVA: 0x000D60F2 File Offset: 0x000D50F2
		public CmsStreamException()
		{
		}

		// Token: 0x06002278 RID: 8824 RVA: 0x000D60FA File Offset: 0x000D50FA
		public CmsStreamException(string name) : base(name)
		{
		}

		// Token: 0x06002279 RID: 8825 RVA: 0x000D6103 File Offset: 0x000D5103
		public CmsStreamException(string name, Exception e) : base(name, e)
		{
		}
	}
}
