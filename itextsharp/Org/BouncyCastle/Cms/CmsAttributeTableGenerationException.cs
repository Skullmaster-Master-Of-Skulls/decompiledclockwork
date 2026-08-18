using System;

namespace Org.BouncyCastle.Cms
{
	// Token: 0x020003E4 RID: 996
	public class CmsAttributeTableGenerationException : CmsException
	{
		// Token: 0x0600229F RID: 8863 RVA: 0x000D6B38 File Offset: 0x000D5B38
		public CmsAttributeTableGenerationException()
		{
		}

		// Token: 0x060022A0 RID: 8864 RVA: 0x000D6B40 File Offset: 0x000D5B40
		public CmsAttributeTableGenerationException(string name) : base(name)
		{
		}

		// Token: 0x060022A1 RID: 8865 RVA: 0x000D6B49 File Offset: 0x000D5B49
		public CmsAttributeTableGenerationException(string name, Exception e) : base(name, e)
		{
		}
	}
}
